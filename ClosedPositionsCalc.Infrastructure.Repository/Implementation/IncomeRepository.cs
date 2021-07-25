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

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            //Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            //Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            //int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;

            //for (int i = 1; i <= rowCount; i++)
            //{
            //    var prova = xlRange.Cells[i, "A"].Value.ToString();
            //    var position = new Position();
            //};

            //using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            //{
            //    using (var reader = ExcelReaderFactory.CreateReader(stream))
            //    {
            //        do
            //        {
            //            while (reader.Read())
            //            {
            //                for (int column = 0; column < reader.FieldCount; column++)
            //                {
            //                    //Console.WriteLine(reader.GetString(column));//Will blow up if the value is decimal etc. 
            //                    Console.WriteLine(reader.GetValue(column));//Get Value returns object
            //                }
            //            }
            //        } while (reader.NextResult());
            //    }
            //}
        }

        public bool UpdateRent(List<Position> rentList, string path)
        {
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Renta2");

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

                package.Save();
            }

            throw new NotImplementedException();
        }
    }
}
