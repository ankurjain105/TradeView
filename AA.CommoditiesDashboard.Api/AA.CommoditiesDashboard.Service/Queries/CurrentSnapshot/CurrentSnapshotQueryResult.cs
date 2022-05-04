using AA.CommoditiesDashboard.Contracts;

namespace AA.CommoditiesDashboard.Service.Queries.CurrentSnapshot
{
    public class CurrentSnapshotQueryResult
    {
        public CommodityModelSnapshotDto[] Snapshots { get; set; }
    }
}