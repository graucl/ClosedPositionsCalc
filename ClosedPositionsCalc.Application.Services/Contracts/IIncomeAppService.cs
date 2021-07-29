using ClosedPositionsCalc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Application.Services.Contracts
{
    public interface IIncomeAppService
    {
        Task Calculations(string filePath);
        double AddProfit(List<PositionEntity> positionsList);
        double AddRolloverFeesDividends(List<PositionEntity> positionsList);
        Task<List<PositionEntity>> RemoveCryptocurrencies(List<PositionEntity> positionsList);
        Task<List<PositionEntity>> GetRentList(List<PositionEntity> positionsList);
    }
}
