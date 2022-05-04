using System.Linq;
using AA.CommoditiesDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Persistence
{
    public class ModelRepository : IModelRepository
    {
        private readonly IDbContext _context;

        public ModelRepository(IDbContext context)
        {
            _context = context;
        }

        public IQueryable<CommodityModel> GetAll(int? commodityId = null, int? modelId = null)
        {
            var query = _context.CommodityModels
                .Include(x => x.Commodity)
                .Include(x => x.Model)
                .AsQueryable();
            if (commodityId.HasValue)
            {
                query = query.Where(x => x.Commodity.Id == commodityId);
            }
            if (modelId.HasValue)
            {
                query = query.Where(x => x.Model.Id == modelId);
            }

            return query;
        } 
    }
}
