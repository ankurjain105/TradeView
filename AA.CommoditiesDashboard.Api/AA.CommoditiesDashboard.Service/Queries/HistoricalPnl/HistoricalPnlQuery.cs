namespace AA.CommoditiesDashboard.Service.Queries.HistoricalPnl
{
    public class HistoricalPnlQuery
    {
        public int? NoOfDays { get; set; } = null;
        public int? ModelId { get; set; }
        public int? CommodityId { get; set; }
    }
}