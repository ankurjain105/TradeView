namespace AA.CommoditiesDashboard.Api.Models
{
    public class CommodityContract : Entity<int>
    {
        public Commodity Commodity { get; set; }
        public string Name { get; set; }
    }
}
