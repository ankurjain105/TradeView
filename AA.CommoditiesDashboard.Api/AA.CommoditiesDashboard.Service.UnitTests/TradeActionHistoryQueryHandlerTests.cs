using System;
using System.Linq;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Service.Queries.TradeActionHistory;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AA.CommoditiesDashboard.Service.UnitTests
{
    public class TradeActionHistoryQueryHandlerTests
    {
        private readonly Mock<IClock> _clock = new Mock<IClock>(MockBehavior.Loose);
        private readonly Mock<ITradeRepository> _tradeRep = new Mock<ITradeRepository>(MockBehavior.Strict);
        private TradeActionHistoryQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _clock.Reset();
            _tradeRep.Reset();
            _handler = new TradeActionHistoryQueryHandler(_clock.Object, _tradeRep.Object);
        }

        [Test]
        public async Task Handle_FilterByOnlyDates_ReturnsTransformedTrades()
        {
            var query = new TradeActionHistoryQuery() {NoOfDays = 5};
            var date = new DateTime(2020, 02, 01);
            _clock.Setup(x => x.Today).Returns(date);
            var trades = new Fixture().CreateMany<CommodityModelTrade>(1).ToArray();
            _tradeRep.Setup(x => x.GetAllTrades(date.AddDays(-5), date, null, null, null))
                .Returns(trades.ToAsyncEnumerable());

            //Act
            var result = await _handler.HandleAsync(query);

            //Assert
            result.Trades.Length.ShouldBe(trades.Length);
            var actual = result.Trades[0];
            actual.Quantity.ShouldBe(trades[0].TradedQuantity);
            actual.TradeAction.Value.ShouldBe(trades[0].TradeAction.ToString());
            actual.TradeDate.ShouldBe(trades[0].TradeDate);
            actual.CommodityModel.Id.ShouldBe(trades[0].CommodityModel.Id);
            actual.CommodityModel.Model.Key.ShouldBe(trades[0].CommodityModel.Model.Id);
            actual.CommodityModel.Model.Value.ShouldBe(trades[0].CommodityModel.Model.Name);
            actual.CommodityModel.Commodity.Key.ShouldBe(trades[0].CommodityModel.Commodity.Id);
            actual.CommodityModel.Commodity.Value.ShouldBe(trades[0].CommodityModel.Commodity.Name);
        }

        [Test]
        public async Task Handle_RepositoryThrowsError_ThrowsException()
        {
            var query = new TradeActionHistoryQuery() { NoOfDays = 5 };
            var date = new DateTime(2020, 02, 01);
            _clock.Setup(x => x.Today).Returns(date);
            var trades = new Fixture().CreateMany<CommodityModelTrade>(1).ToArray();
            _tradeRep.Setup(x => x.GetAllTrades(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, null, null))
                .Throws(new Exception("Test"));

            //Act
            await Should.ThrowAsync<Exception>(() => _handler.HandleAsync(query));
        }
    }
}