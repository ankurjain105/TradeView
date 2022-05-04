using AA.CommoditiesDashboard.Api.Models;

namespace AA.CommoditiesDashboard.Service.Queries.TradeActionHistory
{
    public class TradeActionHistoryQuery
    {
        public int NoOfDays { get; set; } = 5;
        public int? ModelId { get; set; }
        public int? CommodityId { get; set; }
        public TradeAction? TradeAction { get; set; }
    }
}
