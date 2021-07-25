using ClosedPositionsCalc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Infrastructure.Repository.Contracts
{
    public interface IIncomeRepository
    {
        List<Position> GetAllPositions(string path);
        bool UpdateRent(List<Position> rentList, string path);
    }
}
