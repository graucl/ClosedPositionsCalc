using ClosedPositionsCalc.Domain.Entities;
using System.Collections.Generic;

namespace ClosedPositionsCalc.Application.Services.Contracts
{
    public interface IIncomeAppService
    {
        void Calculations(string filePath);
        double AddProfit(List<PositionEntity> positionsList);
        double AddRolloverFeesDividends(List<PositionEntity> positionsList);
        List<PositionEntity> RemoveCryptocurrencies(List<PositionEntity> positionsList);
        List<PositionEntity> GetRentList(List<PositionEntity> positionsList);
    }
}
