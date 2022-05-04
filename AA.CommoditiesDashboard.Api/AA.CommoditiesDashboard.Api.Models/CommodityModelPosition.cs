using System;

namespace AA.CommoditiesDashboard.Api.Models
{
    public class CommodityModelPosition : Entity<long>
    {
        public int CommodityModelId { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public decimal? NetPosition { get; set; }
    }
}
