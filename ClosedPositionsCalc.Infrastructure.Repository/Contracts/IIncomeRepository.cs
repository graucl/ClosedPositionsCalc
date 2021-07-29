using ClosedPositionsCalc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Infrastructure.Repository.Contracts
{
    public interface IIncomeRepository
    {
        List<PositionEntity> GetAllPositions(string path);
        Task<List<CryptocurrencyEntity>> GetAllCryptocurrencies(string path);
        Task<bool> UpdateRent(List<PositionEntity> rentList, string path);
        Task<bool> UpdateCryptocurrencies(List<CryptocurrencyEntity> cryptoList, string path);
    }
}
