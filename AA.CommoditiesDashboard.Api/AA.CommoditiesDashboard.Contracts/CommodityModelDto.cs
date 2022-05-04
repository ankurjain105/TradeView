namespace AA.CommoditiesDashboard.Contracts
{
    public class CommodityModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyValuePairDto Commodity { get; set; }
        public KeyValuePairDto Model { get; set; }
    }
}