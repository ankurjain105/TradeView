using System;

namespace AA.CommoditiesDashboard.Contracts
{
    public class TradeActionSummaryDto
    {
        public CommodityModelDto CommodityModel { get; set; }
        public KeyValuePairDto TradeAction { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal? Quantity { get; set; }
    }
}