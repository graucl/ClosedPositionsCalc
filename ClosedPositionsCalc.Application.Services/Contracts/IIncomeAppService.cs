using ClosedPositionsCalc.Domain.Entities;
using System.Collections.Generic;

namespace ClosedPositionsCalc.Application.Services.Contracts
{
    public interface IIncomeAppService
    {
        void Calculations(string filePath);
        double AddProfit(List<Position> positionsList);
        double AddRolloverFeesDividends(List<Position> positionsList);
        List<Position> RemoveCryptocurrency(List<Position> positionsList);
        List<Position> GetRentList(List<Position> positionsList);
    }
}
