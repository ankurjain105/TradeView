using System;

namespace AA.CommoditiesDashboard.Contracts
{
    public class CommodityModelSnapshotDto
    {
        public CommodityModelDto CommodityModel { get; set; }
        public DateTime Date { get; set; }
        public decimal? Pnl { get; set; }
        public decimal? PnlYtd { get; set; }
        public decimal? PnlLtd { get; set; }
        public decimal? Price { get; set; }
        public decimal? OpenPosition { get; set; }
    }
}