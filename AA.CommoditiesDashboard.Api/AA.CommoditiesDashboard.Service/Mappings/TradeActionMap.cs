using AA.CommoditiesDashboard.Api.Models;

namespace AA.CommoditiesDashboard.Service.Mappings
{
    public static class TradeActionMap
    {
        public static Contracts.KeyValuePairDto ToKeyValuePair(this TradeAction tradeAction)
        {
            return new Contracts.KeyValuePairDto()
            {
                Key = (int) tradeAction,
                Value = tradeAction.ToString()
            };
        }
    }
}
