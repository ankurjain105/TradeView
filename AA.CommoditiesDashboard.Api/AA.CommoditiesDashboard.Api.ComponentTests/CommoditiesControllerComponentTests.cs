using System.Net.Http;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Service.Queries.Lookups;
using AA.CommoditiesDashboard.Service.Queries.TradeActionHistory;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace AA.CommoditiesDashboard.Api.ComponentTests
{
    public class Tests : TestFixture
    {
        [Test]
        public async Task Ping_ReturnsOkResult()
        {
            // Arrange
            var request = "/api/commodities/ping";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task GetLookups_ReturnsCommoditiesModelsAndTradeActions()
        {
            // Arrange
            var request = "/api/commodities/lookups";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lookup = JsonConvert.DeserializeObject<LookupQueryResult>(result);
            lookup.ShouldNotBeNull();
            lookup.Commodities.Length.ShouldBe(2);
            lookup.Models.Length.ShouldBe(2);
            lookup.CommodityModels.Length.ShouldBe(3);
            lookup.TradeActions.Length.ShouldBe(2);
        }

        [Test]
        public async Task GetTradeActionHistory_ReturnsLast5DayTrades()
        {
            // Arrange
            var request = "/api/commodities/trade-action-history";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var tradeActions = JsonConvert.DeserializeObject<TradeActionHistoryQueryResult>(result);
            tradeActions.ShouldNotBeNull();
            tradeActions.Trades.Length.ShouldBe(7);
        }
    }
}