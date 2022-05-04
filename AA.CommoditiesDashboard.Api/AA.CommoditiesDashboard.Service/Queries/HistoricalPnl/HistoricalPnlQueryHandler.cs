using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Contracts;
using AA.CommoditiesDashboard.Service.Mappings;

namespace AA.CommoditiesDashboard.Service.Queries.HistoricalPnl
{
    public class HistoricalPnlQueryHandler : IQueryHandler<HistoricalPnlQuery, HistoricalPnlQueryResult>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IClock _clock;

        public HistoricalPnlQueryHandler(ITradeRepository repository, IModelRepository modelRepository, IClock clock)
        {
            Repository = repository;
            _modelRepository = modelRepository;
            _clock = clock;
        }

        public ITradeRepository Repository { get; }

        public async Task<HistoricalPnlQueryResult> HandleAsync(HistoricalPnlQuery query)
        {
            var r = new List<PnlSummaryDto>();
            foreach(var model in  _modelRepository.GetAll())
            {
                var result = await Repository.GetAllValuations(query.NoOfDays.HasValue ? _clock.Today.AddDays(-1 * query.NoOfDays.Value) : DateTime.MinValue,
                    _clock.Today, model.Id).ToArrayAsync();
                foreach(var val in result)
                {
                    var q = r.FirstOrDefault(x => x.Model.Id == val.CommodityModel.Id);
                    if (q == null)
                    {
                        q = new PnlSummaryDto()
                        {
                            Model = val.CommodityModel.ToDto()
                        };
                        r.Add(q);
                    }
                    q.Pnls.Add(new PnlDto() { Date = val.Date, DailyPnl = val.Pnl });
                }

            }
            return new HistoricalPnlQueryResult { PnlSummaries = r.ToArray() };
        }
    }
}