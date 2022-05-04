using System;

namespace AA.CommoditiesDashboard.Api.Models
{
    public class CommodityModelTrade : Entity<long>
    {
        public CommodityModel CommodityModel { get; set; }

        public CommodityContract CommodityContract { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal? TradedQuantity { get; set; }

        public TradeAction TradeAction { get; set; }
    }
}