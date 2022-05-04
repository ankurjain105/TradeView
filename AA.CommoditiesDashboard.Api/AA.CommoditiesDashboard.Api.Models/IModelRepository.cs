using System.Linq;

namespace AA.CommoditiesDashboard.Api.Models
{
    public interface IModelRepository
    {
        IQueryable<CommodityModel> GetAll(int? commodityId = null, int? modelId = null);
    }
}
