using ClosedPositionsCalc.Domain.Entities;
using ClosedPositionsCalc.Infrastructure.Repository.Contracts;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClosedPositionsCalc.Infrastructure.Repository.Implementation
{
    public class IncomeRepository : IIncomeRepository
    {
        public IncomeRepository()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<Position> GetAllPositions(string path)
        {
            var list = new List<Position>();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var firstSheet = package.Workbook.Worksheets[0];
                for (int i = 2; i < firstSheet.Dimension.End.Row; i++)
                {
                    var position = new Position(Convert.ToInt32(firstSheet.Cells["A" + i].Text), firstSheet.Cells["B" + i].Text,
                                                double.Parse(firstSheet.Cells["I" + i].Text), double.Parse(firstSheet.Cells["N" + i].Text));
                    list.Add(position);
                }

                package.Save();
            }

            return list;
        }

        public bool UpdateRent(List<Position> rentList, string path)
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
    }
}
