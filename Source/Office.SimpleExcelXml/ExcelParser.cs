using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetVortex.Office.SimpleExcelXml
{
    public class ExcelParser
    {
        private const string EXCEL_PROGID = "Excel.Application";

        public static Func<dynamic> GetExcel = () => Activator.CreateInstance(Type.GetTypeFromProgID(EXCEL_PROGID));

        public static Func<Task<dynamic>> GetExcelAsync = async ()
            => await Task.Run(() => Activator.CreateInstance(Type.GetTypeFromProgID(EXCEL_PROGID)))
                .ConfigureAwait(false);

        public FileInfo ExcelFile { get; private set; }

        public ExcelParser(string file)
        {
            if (!File.Exists(file))
                throw new ArgumentException(string.Format("Error opening {0}. File not found.", file));

            ExcelFile = new FileInfo(file);
        }

        public async Task<XElement> ParseXmlAsync(string file)
        {
            var resultXml = new XElement("Workbook", new XAttribute("File", ExcelFile.Name));

            var excel = await GetExcelAsync();
            excel.Application.Workbooks.Open(ExcelFile);
            dynamic workBook = excel.ActiveWorkbook;

            foreach (var sheet in workBook.Sheets)
            {
                var sheetXml = new XElement("Sheet", new XAttribute("Name", sheet.Name));

                var sheetRange = sheet.UsedRange;
                object[,] sheetValues = sheetRange.Value;

                for (int i = sheetValues.GetLowerBound(0); i < sheetValues.GetUpperBound(0); i++)
                {
                    XElement rowXml = new XElement("Row");
                    for (int j = sheetValues.GetLowerBound(1); j < sheetValues.GetUpperBound(1); j++)
                    {
                        rowXml.Add(new XElement("Column" + j, sheetValues[i, j]));
                    }
                    sheetXml.Add(rowXml);
                }

                resultXml.Add(sheetXml);
            }

            excel.Application.Quit();

            return resultXml;
        }
    }
}

