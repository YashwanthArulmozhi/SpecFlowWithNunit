using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpecFlowWithNunit.Utils
{
    class ExcelUtils
    {
        static FileStream fileStream;
        static IWorkbook workBook;

        public static string ReadDataFromExcel(String columnName)
        {
            string pathOfExcelFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + @"\TestData.xlsx";
            string testData;
            fileStream = new FileStream(pathOfExcelFile, FileMode.Open, FileAccess.ReadWrite);
            string extension = pathOfExcelFile.Split(".")[1].Trim();
            if (extension.Equals("xls"))
            {
                workBook = new HSSFWorkbook(fileStream);
            }
            else
            {
                workBook = new XSSFWorkbook(fileStream);
            }
            ISheet sheet = workBook.GetSheet(FileReaderUtils.ReadDataFromConfigFile("Environment"));
            IRow rowobj;
            int cell = -1;
            //  int row = -1;
            rowobj = sheet.GetRow(0);
            int Columncount = rowobj.PhysicalNumberOfCells;
            for (int i = 0; i <= Columncount - 1; i++)
            {
                if (rowobj.GetCell(i).StringCellValue.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    cell = i;
                    break;
                }
            }
            rowobj = sheet.GetRow(1);
            testData = rowobj.GetCell(cell).StringCellValue.Trim();
            return testData;
        }

        public static string ReadDataFromExcel(string rowName, string columnName)
        {
            string pathOfExcelFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + @"\ScenarioLevelTestData.xlsx";
            string testData = "";
            fileStream = new FileStream(pathOfExcelFile, FileMode.Open, FileAccess.ReadWrite);
            string extension = pathOfExcelFile.Split(".")[1].Trim();
            if (extension.Equals("xls"))
            {
                workBook = new HSSFWorkbook(fileStream);
            }
            else
            {
                workBook = new XSSFWorkbook(fileStream);
            }
            ISheet sheet = workBook.GetSheet(FileReaderUtils.ReadDataFromConfigFile("Environment"));
            IRow rowobj;
            int cell = -1;
            int row = -1;
            int rowCount = sheet.PhysicalNumberOfRows;
            for (int j = 0; j <= rowCount - 1; j++)
            {
                if (sheet.GetRow(j).GetCell(0).StringCellValue.Trim().Equals(rowName, StringComparison.OrdinalIgnoreCase))
                {
                    row = j;
                    break;
                }
            }
            rowobj = sheet.GetRow(0);
            int Columncount = rowobj.PhysicalNumberOfCells;
            for (int i = 0; i <= Columncount - 1; i++)
            {
                if (rowobj.GetCell(i).StringCellValue.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    cell = i;
                    break;
                }
            }
            rowobj = sheet.GetRow(row);
            if (!String.IsNullOrEmpty(rowobj.GetCell(cell).StringCellValue))
            {
                testData = rowobj.GetCell(cell).StringCellValue.Trim();
            }
            return testData;
        }
    }
}
