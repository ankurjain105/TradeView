namespace AA.CommoditiesDashboard.Api.Models
{
    public class CommodityModel : Entity<int>
    {
        public string Name { get; set; }

        public Commodity Commodity { get; set; }

        public Model Model { get; set; }
    }
}
