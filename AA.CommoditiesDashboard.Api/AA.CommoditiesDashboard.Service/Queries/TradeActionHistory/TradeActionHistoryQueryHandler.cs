using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Contracts;
using AA.CommoditiesDashboard.Service.Mappings;

namespace AA.CommoditiesDashboard.Service.Queries.TradeActionHistory
{
    public class TradeActionHistoryQueryHandler : IQueryHandler<TradeActionHistoryQuery, TradeActionHistoryQueryResult>
    {
        private readonly IClock _clock;

        public TradeActionHistoryQueryHandler(IClock clock, ITradeRepository repository)
        {
            _clock = clock;
            Repository = repository;
        }

        public ITradeRepository Repository { get; }

        public async Task<TradeActionHistoryQueryResult> HandleAsync(TradeActionHistoryQuery query)
        {
            var result = await Repository.GetAllTrades(_clock.Today.AddDays(-1 * query.NoOfDays), _clock.Today, query.ModelId, query.CommodityId, query.TradeAction).ToArrayAsync();
            return new TradeActionHistoryQueryResult()
            {
                Trades = Map(result).ToArray()
            };
        }

        private IEnumerable<TradeActionSummaryDto> Map(CommodityModelTrade[] trades)
        {
            foreach (var item in trades)
            {
                yield return Map(item);
            }
        }

        private TradeActionSummaryDto Map(CommodityModelTrade trades) =>
             new TradeActionSummaryDto
             {
                 CommodityModel = trades.CommodityModel.ToDto(),
                 TradeDate = trades.TradeDate,
                 TradeAction = trades.TradeAction.ToKeyValuePair(),
                 Quantity = trades.TradedQuantity
             };   
    }
}