using System;
using System.Linq;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;

namespace AA.CommoditiesDashboard.Persistence.IntegrationTests
{
    public class TradeRepositoryTests
    {
        private TradeRepository _tradeRepository;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _tradeRepository = new TradeRepository(new RepositoryContext(optionsBuilder.Options));
        }

        [Test]
        public async Task GetAllTrades_HydratesPropertiesCorrectly()
        {
            var result = await _tradeRepository.GetAllTrades(
                DateTime.Parse("2019-01-07"),
                DateTime.Parse("2019-01-08"),
                1,
                1,
                TradeAction.Buy).ToArrayAsync();
            result.Length.ShouldBe(1);

            result[0].CommodityModel.Commodity.Id.ShouldBe(1);
            result[0].CommodityModel.Commodity.Name.ShouldBe("Commodity1");
            result[0].CommodityModel.Model.Id.ShouldBe(1);
            result[0].CommodityModel.Model.Name.ShouldBe("Model1");
            result[0].CommodityModel.Id.ShouldBe(1);
            result[0].CommodityModel.Name.ShouldBe("Model1_Commodity1");
            result[0].TradeAction.ShouldBe(TradeAction.Buy);
            result[0].TradeDate.ShouldBe(DateTime.Parse("2019-01-08"));
            result[0].TradedQuantity.ShouldBe(2);
        }

        [TestCase("Only Dates", "2019-01-01", "2019-03-01", null, null, null, 29)]
        [TestCase("Dates with Valid ModelId", "2019-01-01", "2019-03-01", 2, null, null, 24)]
        [TestCase("Dates with Valid CommodityId", "2019-01-01", "2019-03-01", null, 1, null, 17)]
        [TestCase("Dates with TradeAction", "2019-01-01", "2019-03-01", null, null, TradeAction.Buy, 15)]
        [TestCase("All Filters ModelId,CommodityId,TradeAction", "2019-01-01", "2019-03-01", 1, 1, TradeAction.Buy, 3)]
        [TestCase("Invalid Model/CommodityId combination", "2019-01-01", "2019-03-01", 1, 2, TradeAction.Buy, 0)]
        public async Task GetAllTrades_SearchReturnsCorrectNoOfRecords(string testCaseDesc, string startDate, string endDate, int? modelId, int? commodityId, TradeAction? action, int expectedResult)
        {
            var result = await _tradeRepository.GetAllTrades(
                DateTime.Parse(startDate),
                DateTime.Parse(endDate),
                modelId,
                commodityId,
                action).ToArrayAsync();
            result.Length.ShouldBe(expectedResult);
        }


        [Test]
        public async Task GetAllValuations_HydratesPropertiesCorrectly()
        {
            var result = await _tradeRepository.GetAllValuations(
                DateTime.Parse("2019-01-08"),
                DateTime.Parse("2019-01-08"),
                2).ToArrayAsync();
            result.Length.ShouldBe(1);

            result[0].CommodityModel.Commodity.Id.ShouldBe(1);
            result[0].CommodityModel.Commodity.Name.ShouldBe("Commodity1");
            result[0].CommodityModel.Model.Id.ShouldBe(2);
            result[0].CommodityModel.Model.Name.ShouldBe("Model2");
            result[0].CommodityModel.Id.ShouldBe(2);
            result[0].CommodityModel.Name.ShouldBe("Model2_Commodity1");
            result[0].Date.ShouldBe(DateTime.Parse("2019-01-08"));
            result[0].Pnl.ShouldBe(-1862.06m);
            result[0].Price.ShouldBe(30452.20m);
        }

        [TestCase("2018-01-02", "2018-01-03", 6)]
        [TestCase("2018-01-02", "2018-01-02", 3)]
        [TestCase("2018-01-04", "2018-01-04", 3)]
        [TestCase("2018-01-05", "2018-01-05", 3)]
        public async Task GetAllValuations_SearchReturnsCorrectNoOfRecords(string startDate, string endDate, int expectedRows)
        {
            //Act
            var actual = await _tradeRepository.GetAllValuations(DateTime.Parse(startDate), DateTime.Parse(endDate)).ToArrayAsync();

            //Assert
            actual.Length.ShouldBe(expectedRows);
        }

        [Test]
        public async Task GetValuationsAsOfDate_HydratesPropertiesCorrectly()
        {
            var result = await _tradeRepository.GetValuationsAsOfDate(
                DateTime.Parse("2019-01-08"),
                2).ToArrayAsync();
            result.Length.ShouldBe(1);

            result[0].CommodityModel.Commodity.Id.ShouldBe(1);
            result[0].CommodityModel.Commodity.Name.ShouldBe("Commodity1");
            result[0].CommodityModel.Model.Id.ShouldBe(2);
            result[0].CommodityModel.Model.Name.ShouldBe("Model2");
            result[0].CommodityModel.Id.ShouldBe(2);
            result[0].CommodityModel.Name.ShouldBe("Model2_Commodity1");
            result[0].Date.ShouldBe(DateTime.Parse("2019-01-08"));
            result[0].Pnl.ShouldBe(-1862.06m);
            result[0].Price.ShouldBe(30452.20m);
        }

        [Test]
        public async Task GetPositionsAsOfDate_HydratesPropertiesCorrectly()
        {
            var result = await _tradeRepository.GetPositionsAsOfDate(
                DateTime.Parse("2019-01-08"),
                2).ToArrayAsync();
            result.Length.ShouldBe(1);

            result[0].CommodityModelId.ShouldBe(2);
            result[0].NetPosition.ShouldBe(-1m);
        }

        [Test]
        public async Task GetAggregatedPnl_HydratesPropertiesCorrectly()
        {
            var result = await _tradeRepository.GetAggregatedPnl(
                DateTime.Parse("2019-01-01"),
                DateTime.Parse("2019-01-08"),
                2).ToArrayAsync();
            result.Length.ShouldBe(1);

            result[0].CommodityModelId.ShouldBe(2);
            result[0].AggregatedPnl.ShouldBe(15759.90m);
        }
    }
}