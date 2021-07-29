using ClosedPositionsCalc.Domain.Entities;
using ClosedPositionsCalc.Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClosedPositionsCalc.Infrastructure.Repository.Implementation
{
    public class IncomeRepository : IIncomeRepository
    {
        public IncomeRepository()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        
        public List<PositionEntity> GetAllPositions(string path)
        {
            var list = new List<PositionEntity>();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var firstSheet = package.Workbook.Worksheets[0];
                for (int i = 2; i < firstSheet.Dimension.End.Row; i++)
                {
                    var position = new PositionEntity(Convert.ToInt32(firstSheet.Cells["A" + i].Text), firstSheet.Cells["B" + i].Text,
                                                double.Parse(firstSheet.Cells["I" + i].Text), double.Parse(firstSheet.Cells["N" + i].Text));
                    list.Add(position);
                }

                package.Save();
            }

            return list;
        }

        public async Task<List<CryptocurrencyEntity>> GetAllCryptocurrencies(string path)
        {
            var list = new List<CryptocurrencyEntity>();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var firstSheet = package.Workbook.Worksheets[0];
                for (int i = 2; i < firstSheet.Dimension.End.Row; i++)
                {
                    if (firstSheet.Cells["B" + i].Text.Contains("Bitcoin"))
                    {
                        var position = new CryptocurrencyEntity(Convert.ToInt32(firstSheet.Cells["A" + i].Text), firstSheet.Cells["B" + i].Text, "Bitcoin",
                                                DateTime.Parse(firstSheet.Cells["J" + i].Text), DateTime.Parse(firstSheet.Cells["K" + i].Text),
                                                double.Parse(firstSheet.Cells["F" + i].Text), double.Parse(firstSheet.Cells["G" + i].Text),
                                                double.Parse(firstSheet.Cells["I" + i].Text), double.Parse(firstSheet.Cells["N" + i].Text));
                        list.Add(position);
                    }
                }

                package.Save();
            }

            return list;
        }

        public async Task<bool> UpdateRent(List<PositionEntity> rentList, string path)
        {
            var success = false;

            double profit = 0;
            double rfd = 0;
            double total = 0;

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Renta");

                worksheet.Cells["B1"].Value = "Action";
                worksheet.Cells["C1"].Value = "Total Profit $";
                worksheet.Cells["D1"].Value = "Rollover fees and dividends $";

                int row = 2;
                for (int i = 0; i < rentList.Count; i++)
                {
                    worksheet.Cells["B" + row].Value = rentList[i].Action;
                    worksheet.Cells["C" + row].Value = rentList[i].Profit;
                    worksheet.Cells["D" + row].Value = rentList[i].RolloverFeesDividends;

                    row++;
                }

                worksheet.Cells["F1"].Value = "Total Profit $";
                worksheet.Cells["G1"].Value = "Rollover fees and dividends $";
                worksheet.Cells["H1"].Value = "Total $";

                foreach (var position in rentList)
                {
                    profit = profit + position.Profit;
                    profit = Math.Round(profit, 2, MidpointRounding.ToEven);
                }

                foreach (var position in rentList)
                {
                    rfd = rfd + position.RolloverFeesDividends;
                    rfd = Math.Round(rfd, 2, MidpointRounding.ToEven);
                }

                total = profit + rfd;
                total = Math.Round(total, 2, MidpointRounding.ToEven);

                worksheet.Cells["F2"].Value = profit.ToString();
                worksheet.Cells["G2"].Value = rfd.ToString();
                worksheet.Cells["H2"].Value = total.ToString();

                worksheet.Cells.AutoFitColumns();
                package.Save();

                success = true;
            }

            return success;
        }

        public async Task<bool> UpdateCryptocurrencies(List<CryptocurrencyEntity> cryptoList, string path)
        {
            var success = false;
            
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets.Add("CriptoMonedas");

                worksheet.Cells["A1"].Value = "Tipo de criptomoneda";
                worksheet.Cells["B1"].Value = "Data compra";
                worksheet.Cells["C1"].Value = "Data venta";
                worksheet.Cells["D1"].Value = "Import compra";
                worksheet.Cells["E1"].Value = "Import venta";
                worksheet.Cells["F1"].Value = "Profit";
                worksheet.Cells["G1"].Value = "Rollover fees and dividends";

                int row = 2;
                for (int i = 0; i < cryptoList.Count; i++)
                {
                    worksheet.Cells["A" + row].Value = cryptoList[i].Type;
                    worksheet.Cells["B" + row].Value = cryptoList[i].BuyDate.ToString("dd/MM/yyyy HH:mm");
                    worksheet.Cells["C" + row].Value = cryptoList[i].SellDate.ToString("dd/MM/yyyy HH:mm");
                    worksheet.Cells["D" + row].Value = cryptoList[i].BuyImport;
                    worksheet.Cells["E" + row].Value = cryptoList[i].SellImport;
                    worksheet.Cells["F" + row].Value = cryptoList[i].Profit;
                    worksheet.Cells["G" + row].Value = cryptoList[i].RolloverFeesDividends;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                package.Save();

                success = true;
            }

            return success;
        }
    }
}
