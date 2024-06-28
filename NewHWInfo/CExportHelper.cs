using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewHWInfo
{
    public class CExportHelper
    {
        public static void ExportDataGridToExcel(string OS, string CPU,
            string RAM, string Display, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Характеристики ПК");

                worksheet.Cells[1, 1].Value = "Операционная система";
                worksheet.Cells[1, 2].Value = "Центральный процессор";
                worksheet.Cells[1, 3].Value = "Оперативная память";
                worksheet.Cells[1, 4].Value = "Дисплей";

                worksheet.Cells[2, 1].Value = OS;
                worksheet.Cells[2, 2].Value = CPU;
                worksheet.Cells[2, 3].Value = RAM;
                worksheet.Cells[2, 4].Value = Display;

                worksheet.Row(1).Style.HorizontalAlignment = 
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Cells["A2:D2"].AutoFitColumns();

                FileInfo excelFile = new FileInfo(filePath);
                excel.SaveAs(excelFile);
                excel.Dispose();


            }
        }
    }
}
