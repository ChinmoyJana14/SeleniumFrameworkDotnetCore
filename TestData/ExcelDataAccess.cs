using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.IO;
using Tests;
using TestSeleniumWithDotnetCore;

namespace TestSelenium.TestData
{
    public class ExcelDataAccess
    {

       private string value;
       private IConfiguration config;

        public string GetTestData(string testName,string testDataName)
        {
            config = ConfigurationCreater.SetConfiguration();
            string fileName = config["MyConfig:TestDataSheetPath"];
            FileInfo file = new FileInfo(fileName);
                
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Data"];
                var rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;
                int activeRow;
                for (int row = 1; row <= rowCount; row++)
                {
                    if(worksheet.Cells[row, 1].Value.ToString()==testName)
                    {
                        activeRow = row;
                        Console.Write($"The started test is {testName}");
                             for (int col = 1; col <= colCount; col++)
                             {
                                 if (worksheet.Cells[1, col].Value.ToString() == testDataName)
                                    {
                                       value = worksheet.Cells[activeRow, col].Value.ToString();
                                        Console.Write($"The value of {testDataName} is {value}");
                                
                                        break;
                                    }
                             }
                        break;
                    }                     
                }
                return value;
            }
        }
    }
}
