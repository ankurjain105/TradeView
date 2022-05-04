using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Persistence.IntegrationTests
{
    internal class ModelRepositoryTests
    {
        private ModelRepository _modelRepository;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _modelRepository = new ModelRepository(new RepositoryContext(optionsBuilder.Options));
        }

        [Test]
        public async Task GetAll_ModelIdIsNull_CommodityIdIsNull_ReturnsAll()
        {
            //Act
            var actual = await _modelRepository.GetAll().ToArrayAsync();

            //Assert
            actual.Length.ShouldBe(3);
        }

        [Test]
        public async Task GetAll_ModelIdIsNotNull_CommodityIdIsNull_ReturnsAllDataForThatModel()
        {
            //Act
            var actual = await _modelRepository.GetAll(modelId: 2).ToArrayAsync();

            //Assert
            actual.Length.ShouldBe(2);
            actual.All(x =>
            {
                x.Name.ShouldBeOneOf(new[] { "Model2_Commodity1", "Model2_Commodity2" });
                x.Model.Id.ShouldBe(2);
                x.Model.Name.ShouldBe("Model2");
                x.Commodity.Id.ShouldBeInRange(1, 2);
                x.Commodity.Name.ShouldBeOneOf(new [] { "Commodity1" , "Commodity2" });
                return true;
            }).ShouldBeTrue();
        }

        [Test]
        public async Task GetAll_ModelIdIsNull_CommodityIdIsNotNull_ReturnsAllDataForThatModel()
        {
            //Act
            var actual = await _modelRepository.GetAll(commodityId: 1).ToArrayAsync();

            //Assert
            actual.Length.ShouldBe(2);
            actual.All(x =>
            {
                x.Name.ShouldBeOneOf(new [] { "Model1_Commodity1", "Model2_Commodity1" });
                x.Model.Id.ShouldBeInRange(1, 2);
                x.Model.Name.ShouldBeOneOf( new [] {"Model1", "Model2"});
                x.Commodity.Id.ShouldBe(1);
                x.Commodity.Name.ShouldBe("Commodity1");
                return true;
            }).ShouldBeTrue();
        }
    }
}
