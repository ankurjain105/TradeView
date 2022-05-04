using AA.CommoditiesDashboard.Contracts;

namespace AA.CommoditiesDashboard.Service.Queries.Lookups
{
    public class LookupQueryResult
    {
        public KeyValuePairDto[] Models { get; set; }
        public KeyValuePairDto[] Commodities { get; set; }
        public CommodityModelDto[] CommodityModels { get; set; }
        public KeyValuePairDto[] TradeActions { get; set; }
    }
}