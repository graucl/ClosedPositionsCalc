using ClosedPositionsCalc.Domain.Entities;
using System.Collections.Generic;

namespace ClosedPositionsCalc.Infrastructure.Repository.Contracts
{
    public interface IIncomeRepository
    {
        List<PositionEntity> GetAllPositions(string path);
        List<CryptocurrencyEntity> GetAllCryptocurrencies(string path);
        bool UpdateRent(List<PositionEntity> rentList, string path);
        bool UpdateCryptocurrencies(List<CryptocurrencyEntity> cryptoList, string path);
    }
}
