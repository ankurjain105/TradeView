using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Contracts;
using AA.CommoditiesDashboard.Service.Mappings;

namespace AA.CommoditiesDashboard.Service.Queries.CurrentSnapshot
{
    public class CurrentSnapshotQueryHandler : IQueryHandler<CurrentSnapshotQuery, CurrentSnapshotQueryResult>
    {
        private readonly IClock _clock;

        public CurrentSnapshotQueryHandler(IClock clock, IModelRepository repository, ITradeRepository tradeRepository)
        {
            _clock = clock;
            Repository = repository;
            TradeRepository = tradeRepository;
        }

        public IModelRepository Repository { get; }
        public ITradeRepository TradeRepository { get; }

        public async Task<CurrentSnapshotQueryResult> HandleAsync(CurrentSnapshotQuery query)
        {
            var date = query.SnapshotDate ?? _clock.Today;
            var snapshots = new Dictionary<int, CommodityModelSnapshotDto>();
            foreach (var cm in Repository.GetAll())
            {
                snapshots.Add(cm.Id, new CommodityModelSnapshotDto() { CommodityModel = cm.ToDto(), Date = date });
            }

            await foreach (var trade in TradeRepository.GetPositionsAsOfDate(date))
            {
                snapshots[trade.CommodityModelId].OpenPosition = trade.NetPosition;
            }

            await foreach (var trade in TradeRepository.GetValuationsAsOfDate(date))
            {
                snapshots[trade.CommodityModel.Id].Pnl = trade.Pnl;
                snapshots[trade.CommodityModel.Id].Price = trade.Price;
            }
            await foreach (var trade in TradeRepository.GetAggregatedPnl(new DateTime(date.Year, 01, 01).Date, date))
            {
                snapshots[trade.CommodityModelId].PnlYtd = trade.AggregatedPnl;
            }

            await foreach (var trade in TradeRepository.GetAggregatedPnl(DateTime.MinValue.Date, date))
            {
                snapshots[trade.CommodityModelId].PnlLtd = trade.AggregatedPnl;
            }

            return new CurrentSnapshotQueryResult {Snapshots = snapshots.Values.ToArray()};
        }
    }
}