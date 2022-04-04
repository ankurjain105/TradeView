using AA.CommoditiesDashboard.Interfaces.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Controllers
{
    [ApiController]
    public class CommoditiesController
    {
        private readonly ICommodityProvider _provider;

        public CommoditiesController(ICommodityProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        [Route("api/commodities/recent-history")]
        public async Task<IActionResult> GetRecentHistory()
        {
            var history = await _provider.GetRecentHistoryAsync();
            return new OkObjectResult(history);
        }
    }
}
