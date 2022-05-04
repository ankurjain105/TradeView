using System;

namespace AA.CommoditiesDashboard.Api.Models
{
    public class CommodityModelValuation : Entity<long>
    {
        public CommodityModel CommodityModel { get; set; }

        public DateTime Date { get; set; }

        public decimal? Price { get; set; }

        public decimal? Pnl { get; set; }
    }
}
