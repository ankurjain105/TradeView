using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Contracts;

namespace AA.CommoditiesDashboard.Service.Mappings
{
    internal static class CommodityModelTradeMap
    {
        public static CommodityModelDto ToDto(this CommodityModel entity)
        {
            return new CommodityModelDto()
            {
                Name = entity.Name,
                Id = entity.Id,
                Commodity = new Contracts.KeyValuePairDto() { Key = entity.Commodity.Id, Value = entity.Commodity.Name},
                Model = new Contracts.KeyValuePairDto() { Key = entity.Model.Id, Value = entity.Model.Name},
            };
        }
    }
}