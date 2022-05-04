using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Models
{
    public interface ITradeRepository
    {
        public IAsyncEnumerable<CommodityModelTrade> GetAllTrades(DateTime startDate,
            DateTime endDate,
            int? modelId,
            int? commodityId,
            TradeAction? action);

        IAsyncEnumerable<CommodityModelValuation> GetAllValuations(DateTime startDate,
           DateTime endDate,
           int? commodityModelId = null);

        IAsyncEnumerable<CommodityModelValuation> GetValuationsAsOfDate(DateTime date, int? commodityModelId = null);
        IAsyncEnumerable<CommodityModelPosition> GetPositionsAsOfDate(DateTime date, int? commodityModelId = null);

        IAsyncEnumerable<(int CommodityModelId, decimal AggregatedPnl)> GetAggregatedPnl(DateTime startDate,
            DateTime endDate, int? commodityModelId = null);
    }
}