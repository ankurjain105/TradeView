using AA.CommoditiesDashboard.Contracts;

namespace AA.CommoditiesDashboard.Service.Queries.TradeActionHistory
{
    public class TradeActionHistoryQueryResult
    {
       public TradeActionSummaryDto[] Trades { get; set; }
    }
}