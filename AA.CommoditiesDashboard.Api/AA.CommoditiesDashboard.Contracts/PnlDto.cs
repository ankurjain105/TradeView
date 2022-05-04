using System;

namespace AA.CommoditiesDashboard.Contracts
{
    public class PnlDto
    {
        public DateTime Date { get; set; }
        public decimal? DailyPnl { get; set; }
    }
}