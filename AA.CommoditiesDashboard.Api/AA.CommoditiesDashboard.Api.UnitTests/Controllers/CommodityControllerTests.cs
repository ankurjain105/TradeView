using AA.CommoditiesDashboard.Api.Controllers;
using AA.CommoditiesDashboard.Interfaces.Models;
using AA.CommoditiesDashboard.Interfaces.Providers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AA.CommoditiesDashboard.Api.UnitTests.Controllers
{
    public class CommodityControllerTests
    {
        private readonly CommoditiesController _controller;
        private readonly Mock<ICommodityProvider> _provider;

        public CommodityControllerTests()
        {
            _provider = new Mock<ICommodityProvider>();
            _controller = new CommoditiesController(_provider.Object);
        }

        [Fact]
        public async Task GivenIRequestGetRecentHistory_ThenGetRecentHistoryShouldBeRetrieved()
        {
            await _controller.GetRecentHistory();

            _provider.Verify(x => x.GetRecentHistoryAsync(), Times.Once);
        }

        [Fact]
        public async Task GivenIRequestGetRecentHistory_ThenGetRecentHistoryShouldBeReturned()
        {
            var expected = new[]
            {
                new CommodityRecentHistory(
                    "model",
                    "commodity",
                    new []{new CommodityData(1.23M, -5, 3, 2.34M, DateTime.UtcNow)})
            };
            _provider
                .Setup(x => x.GetRecentHistoryAsync())
                .Returns(Task.FromResult(expected.AsEnumerable()));

            var response = await _controller.GetRecentHistory();
            var actual = ((OkObjectResult)response).Value;

            actual.Should().Be(expected);
        }
    }
}
