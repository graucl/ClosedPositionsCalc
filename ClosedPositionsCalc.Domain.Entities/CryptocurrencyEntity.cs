using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Domain.Entities
{
    public class CryptocurrencyEntity : PositionEntity
    {
        public string Type { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public double BuyImport { get; set; }
        public double SellImport { get; set; }

        public CryptocurrencyEntity()
        {

        }

        public CryptocurrencyEntity(int positionId, string action, string type, DateTime buyDate, DateTime sellDate, double buyImport, double sellImport, 
                                    double profit, double rolloverFeesDividends) : base(positionId, action, profit, rolloverFeesDividends)
        {
            Type = type;
            BuyDate = buyDate;
            SellDate = sellDate;
            BuyImport = buyImport;
            SellImport = sellImport;
        }

        public override bool Equals(object obj)
        {
            return obj is CryptocurrencyEntity entity &&
                   base.Equals(obj) &&
                   PositionId == entity.PositionId &&
                   Action == entity.Action &&
                   Profit == entity.Profit &&
                   RolloverFeesDividends == entity.RolloverFeesDividends &&
                   Type == entity.Type &&
                   BuyDate == entity.BuyDate &&
                   SellDate == entity.SellDate &&
                   BuyImport == entity.BuyImport &&
                   SellImport == entity.SellImport;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(PositionId);
            hash.Add(Action);
            hash.Add(Profit);
            hash.Add(RolloverFeesDividends);
            hash.Add(Type);
            hash.Add(BuyDate);
            hash.Add(SellDate);
            hash.Add(BuyImport);
            hash.Add(SellImport);
            return hash.ToHashCode();
        }
    }
}
