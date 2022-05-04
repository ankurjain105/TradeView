using AA.CommoditiesDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AA.CommoditiesDashboard.Persistence
{
    public class TradeRepository : ITradeRepository
    {
        private readonly IDbContext _context;

        public TradeRepository(IDbContext context)
        {
            _context = context;
        }

        public IAsyncEnumerable<CommodityModelTrade> GetAllTrades(DateTime startDate, 
            DateTime endDate,
            int? modelId = null, 
            int? commodityId = null,
            TradeAction? action = null)
        {
            var query = _context.Trades.Include(x => x.CommodityContract)
                .ThenInclude(x => x.Commodity)
                .Include(x => x.CommodityModel)
                .Include(x => x.CommodityModel.Model)
                .Include(x => x.CommodityModel.Commodity)
                .Where(x => x.TradeDate.Date >= startDate.Date && x.TradeDate.Date <= endDate.Date)
                .AsQueryable();
            if (commodityId.HasValue)
            {
                query = query.Where(x => x.CommodityModel.Commodity.Id == commodityId.Value);
            }
            if (modelId.HasValue)
            {
                query = query.Where(x => x.CommodityModel.Model.Id == modelId.Value);
            }
            if (action.HasValue)
            {
                query = query.Where(x => x.TradeAction == action.Value);
            }
            return query.AsAsyncEnumerable();
        }

        public IAsyncEnumerable<CommodityModelValuation> GetAllValuations(DateTime startDate,
            DateTime endDate,
            int? commodityModelId = null)
        {
            var query = _context.Valuations
                .Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date)
                .Include(x => x.CommodityModel)
                .Include(x => x.CommodityModel.Model)
                .Include(x => x.CommodityModel.Commodity)
                .AsQueryable();
            if (commodityModelId.HasValue)
            {
                query = query.Where(x => x.CommodityModel.Id == commodityModelId);
            }
            return query.AsAsyncEnumerable();
        }

        public IAsyncEnumerable<CommodityModelValuation> GetValuationsAsOfDate(DateTime date, int? commodityModelId = null)
        {
            var query = _context.Valuations
                .Where(x => x.Date.Date == date.Date)
                .Include(x => x.CommodityModel)
                .Include(x => x.CommodityModel.Model)
                .Include(x => x.CommodityModel.Commodity)
                .AsQueryable();
            if (commodityModelId.HasValue)
            {
                query = query.Where(x => x.CommodityModel.Id == commodityModelId);
            }
            return query.AsAsyncEnumerable();
        }

        public IAsyncEnumerable<CommodityModelPosition> GetPositionsAsOfDate(DateTime date, int? commodityModelId = null)
        {
            var query = _context.Positions
                .Where(x => x.FromDate <= date.Date && (x.ToDate == null || x.ToDate > date.Date));
            if (commodityModelId.HasValue)
            {
                query = query.Where(x => x.CommodityModelId == commodityModelId);
            }
            return query.AsAsyncEnumerable();
        }

        public IAsyncEnumerable<(int CommodityModelId, decimal AggregatedPnl)> GetAggregatedPnl(DateTime startDate, DateTime endDate, int? commodityModelId = null)
        {
            var query = _context.Valuations
                .Where(x => x.Date >= startDate.Date && x.Date <= endDate.Date);
            if (commodityModelId.HasValue)
            {
                query = query.Where(x => x.CommodityModel.Id == commodityModelId);
            }

            var aggQuery =  query.GroupBy(x => x.CommodityModel.Id)
                .Select(x => new { CommodityModelId = x.Key, AggregatedPnl = x.Sum(v => v.Pnl ?? 0)});
            return aggQuery.Select(x => new ValueTuple<int, decimal>(x.CommodityModelId, x.AggregatedPnl)).AsAsyncEnumerable();
        }
    }
}