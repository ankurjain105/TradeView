using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Service;
using AA.CommoditiesDashboard.Service.Queries.CurrentSnapshot;
using AA.CommoditiesDashboard.Service.Queries.HistoricalPnl;
using AA.CommoditiesDashboard.Service.Queries.Lookups;
using AA.CommoditiesDashboard.Service.Queries.TradeActionHistory;
using Microsoft.Extensions.Logging;

namespace AA.CommoditiesDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/commodities")]
    public class CommoditiesController
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return new OkObjectResult(DateTime.UtcNow.ToString());
        }

        private readonly IQueryHandler<TradeActionHistoryQuery, TradeActionHistoryQueryResult> _queryHandler;
        private readonly IQueryHandler<HistoricalPnlQuery, HistoricalPnlQueryResult> _historicalPnlQueryHandler;
        private readonly IQueryHandler<CurrentSnapshotQuery, CurrentSnapshotQueryResult> _currentSnapshotQueryHandler;
        private readonly IQueryHandler<LookupQuery, LookupQueryResult> _lookupQueryHandler;
        private readonly ILogger<CommoditiesController> _logger;

        public CommoditiesController(IQueryHandler<TradeActionHistoryQuery, TradeActionHistoryQueryResult> queryHandler,
            IQueryHandler<HistoricalPnlQuery, HistoricalPnlQueryResult> historicalPnlQueryHandler,
            IQueryHandler<CurrentSnapshotQuery, CurrentSnapshotQueryResult> currentSnapshotQueryHandler,
            IQueryHandler<LookupQuery, LookupQueryResult> lookupQueryHandler,
            ILogger<CommoditiesController> logger)
        {
            _queryHandler = queryHandler;
            _historicalPnlQueryHandler = historicalPnlQueryHandler;
            _currentSnapshotQueryHandler = currentSnapshotQueryHandler;
            _lookupQueryHandler = lookupQueryHandler;
            _logger = logger;
        }

        [HttpGet("lookups")]
        public async Task<LookupQueryResult> GetLookups()
        {
            var result = await _lookupQueryHandler.HandleAsync(new LookupQuery());
            return result;
        }


        [HttpGet("trade-action-history")]
        public async Task<TradeActionHistoryQueryResult> Get([FromQuery] TradeActionHistoryQuery query)
        {
            var result = await _queryHandler.HandleAsync(query);
            return result;
        }

        [HttpGet("pnl-history")]
        public async Task<HistoricalPnlQueryResult> Get([FromQuery] HistoricalPnlQuery query)
        {
            var result = await _historicalPnlQueryHandler.HandleAsync(query);
            return result;
        }

        [HttpGet("current-snapshot")]
        public async Task<CurrentSnapshotQueryResult> Get()
        {
            var result = await _currentSnapshotQueryHandler.HandleAsync(new CurrentSnapshotQuery());
            return result;
        }
    }
}
