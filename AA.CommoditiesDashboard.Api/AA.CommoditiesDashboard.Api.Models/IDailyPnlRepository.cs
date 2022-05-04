using System;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Models
{
    public interface IDailyPnlRepository
    {
        public Task<CommodityModelValuation[]> GetAllPnls(DateTime startDate,
           DateTime endDate,
           int? modelId,
           int? commodityId);
    }
}
