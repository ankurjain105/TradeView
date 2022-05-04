using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Contracts;
using AA.CommoditiesDashboard.Service.Mappings;
using AA.CommoditiesDashboard.Service.Queries.HistoricalPnl;

namespace AA.CommoditiesDashboard.Service.Queries.Lookups
{
    public class LookupQueryHandler : IQueryHandler<LookupQuery, LookupQueryResult>
    {
        private readonly IModelRepository _modelRepository;
 
        public LookupQueryHandler(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<LookupQueryResult> HandleAsync(LookupQuery query)
        {
            var r = new LookupQueryResult();
            r.CommodityModels = _modelRepository.GetAll().Select(x => x.ToDto()).ToArray();
            r.Commodities = r.CommodityModels.Select(x => x.Commodity).Distinct().ToArray();
            r.Models = r.CommodityModels.Select(x => x.Model).Distinct().ToArray();
            r.TradeActions = Enum.GetValues(typeof(TradeAction)).Cast<TradeAction>().Select(i => new KeyValuePairDto() { Key = (int)i, Value = i.ToString() }).ToArray();
            return Task.FromResult(r);
        }
    }
}