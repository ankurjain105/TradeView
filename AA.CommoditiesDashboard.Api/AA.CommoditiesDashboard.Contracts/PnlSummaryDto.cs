using System.Collections.Generic;

namespace AA.CommoditiesDashboard.Contracts
{
    public class PnlSummaryDto
    {
        public CommodityModelDto Model { get; set; }
        public List<PnlDto> Pnls { get; set; } = new List<PnlDto>();
    }
}