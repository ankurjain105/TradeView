using System.Collections.Generic;

namespace AA.CommoditiesDashboard.Interfaces.Models
{
    public class CommodityRecentHistory
    {
        public string Model { get; }
        public string Commodity { get; }
        public IEnumerable<CommodityData> DataPoints { get; }

        public CommodityRecentHistory(
            string model, 
            string commodity, 
            IEnumerable<CommodityData> dataPoints)
        {
            Model = model;
            Commodity = commodity;
            DataPoints = dataPoints;
        }
    }
}
