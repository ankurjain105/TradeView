using AA.CommoditiesDashboard.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Interfaces.Providers
{
    public interface ICommodityProvider
    {
        Task<IEnumerable<CommodityRecentHistory>> GetRecentHistoryAsync();
    }
}