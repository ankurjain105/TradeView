using System;

namespace AA.CommoditiesDashboard.Interfaces.Models
{
    public class CommodityData
    {
        public decimal Price { get; }
        public int Position { get; }
        public int NewTradeAction { get; }
        public decimal PnlDaily { get; }
        public DateTime Date { get; }

        public CommodityData(
            decimal price, 
            int position, 
            int newTradeAction, 
            decimal pnlDaily, 
            DateTime date)
        {
            Price = price;
            Position = position;
            NewTradeAction = newTradeAction;
            PnlDaily = pnlDaily;
            Date = date;
        }
    }
}