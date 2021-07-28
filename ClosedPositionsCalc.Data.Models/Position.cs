using System;

namespace ClosedPositionsCalc.Data.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public double BuyImport { get; set; }
        public double SellImport { get; set; }
        public double Profit { get; set; }
        public double RolloverFeesDividends { get; set; }

        public Position()
        {
            
        }

        public Position(int positionId, string action, double profit, double rolloverFeesDividends) : this()
        {
            PositionId = positionId;
            Action = action;
            Profit = profit;
            RolloverFeesDividends = rolloverFeesDividends;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   PositionId == position.PositionId &&
                   Action == position.Action &&
                   Profit == position.Profit &&
                   RolloverFeesDividends == position.RolloverFeesDividends;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PositionId, Action, Profit, RolloverFeesDividends);
        }
    }
}
