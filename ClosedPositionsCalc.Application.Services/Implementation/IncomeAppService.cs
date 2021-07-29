using ClosedPositionsCalc.Application.Services.Contracts;
using ClosedPositionsCalc.Domain.Entities;
using ClosedPositionsCalc.Infrastructure.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Application.Services.Implementation
{
    public class IncomeAppService : IIncomeAppService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeAppService(IIncomeRepository incomeRepository)
        {
            this._incomeRepository = incomeRepository;
        }

        public async Task Calculations(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("File path is null");
            }

            var allPositionsList = _incomeRepository.GetAllPositions(filePath);
            var positionsList = await RemoveCryptocurrencies(allPositionsList);
            var rentList = await GetRentList(positionsList);
            var cryptoList = await _incomeRepository.GetAllCryptocurrencies(filePath);
            await _incomeRepository.UpdateCryptocurrencies(cryptoList, filePath);
            await _incomeRepository.UpdateRent(rentList, filePath);

        }

        public double AddProfit(List<PositionEntity> positionsList)
        {
            double profit = 0;

            foreach (var position in positionsList)
            {
                profit = profit + position.Profit;
                profit = Math.Round(profit, 2, MidpointRounding.ToEven);
            }

            return profit;
        }

        public double AddRolloverFeesDividends(List<PositionEntity> positionsList)
        {
            double rfd = 0;

            foreach (var position in positionsList)
            {
                rfd = rfd + position.RolloverFeesDividends;
                rfd = Math.Round(rfd, 2, MidpointRounding.ToEven);
            }

            return rfd;
        }

        public async Task<List<PositionEntity>> RemoveCryptocurrencies(List<PositionEntity> positionsList)
        {
            positionsList.RemoveAll(x => x.Action.Contains("Bitcoin"));

            return positionsList;
        }

        public async Task<List<PositionEntity>> GetRentList(List<PositionEntity> positionsList)
        {
            var tempList = new List<PositionEntity>();
            var rentList = new List<PositionEntity>();
            var rentPosition = new PositionEntity();

            foreach (var position in positionsList)
            {
                var inRentList = (from rentPos in rentList
                                  where rentPos.Action == position.Action
                                  select rentPos).ToList();

                if (inRentList.Count == 0)
                {
                    tempList = (from pos in positionsList
                                where pos.Action == position.Action
                                select pos).ToList();

                    var profit = AddProfit(tempList);
                    var rfd = AddRolloverFeesDividends(tempList);
                    rentPosition = new PositionEntity(0, position.Action, profit, rfd);
                    rentList.Add(rentPosition);
                }
            }

            return rentList;
        }
    }
}
