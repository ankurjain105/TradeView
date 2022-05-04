using AA.CommoditiesDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Persistence
{
    public interface IDbContext
    {
        DbSet<CommodityModel> CommodityModels
        {
            get;
        }

        DbSet<CommodityModelTrade> Trades
        {
            get;
        }

        DbSet<CommodityModelValuation> Valuations
        {
            get;
        }

        DbSet<CommodityModelPosition> Positions
        {
            get;
        }
    }
}
