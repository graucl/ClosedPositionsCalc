using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Domain.Entities
{
    public class Cryptocurrency
    {
        public string Type { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public double BuyImport { get; set; }
        public double SellImport { get; set; }
        public double Profit { get; set; }
        public double RolloverFeesDividends { get; set; }

        public Cryptocurrency()
        {

        }

        public Cryptocurrency(string type, DateTime buyDate, DateTime sellDate, double buyImport, double sellImport, double profit, double rolloverFeesDividends)
        {
            Type = type;
            BuyDate = buyDate;
            SellDate = sellDate;
            BuyImport = buyImport;
            SellImport = sellImport;
            Profit = profit;
            RolloverFeesDividends = rolloverFeesDividends;
        }

        public override bool Equals(object obj)
        {
            return obj is Cryptocurrency cryptocurrency &&
                   Type == cryptocurrency.Type &&
                   BuyDate == cryptocurrency.BuyDate &&
                   SellDate == cryptocurrency.SellDate &&
                   BuyImport == cryptocurrency.BuyImport &&
                   SellImport == cryptocurrency.SellImport &&
                   Profit == cryptocurrency.Profit &&
                   RolloverFeesDividends == cryptocurrency.RolloverFeesDividends;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, BuyDate, SellDate, BuyImport, SellImport, Profit, RolloverFeesDividends);
        }
    }
}
