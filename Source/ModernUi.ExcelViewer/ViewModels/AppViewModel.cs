using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using NetVortex.Office.SimpleExcelXml;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetVortex.ModernUi.ExcelViewer.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using NetVortex.Extensions;

namespace NetVortex.ModernUi.ExcelViewer.ViewModels
{
    class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        public BindableCollection<SheetViewModel> Sheets { get; private set; }
        public string DisplayName { get; set; }

        public AppViewModel()
        {
            DisplayName = "Excel Viewer";
            Sheets = new BindableCollection<SheetViewModel>();
            Sheets.CollectionChanged += (e, s) => NotifyOfPropertyChange(() => CanCreateSql);
        }

        public async void CreateSql()
        {
            var mainWin = Application.Current.MainWindow as MetroWindow;

            if (mainWin == null)
                return;

            var progressController = await mainWin.ShowProgressAsync("SQL generation", "Exporting Excel file to SQL...");

            var sqlBuilder = new StringBuilder();

            const string baseSql = "UPDATE [dbo].[Account_AccountLib_{0}] SET [ITManagerKonto] = '{1}' WHERE [ITManagerKonto] = '{2}' AND [Status] = 'deleted'";

            sqlBuilder.AppendLine("USE [comet3db]").AppendLine()
                      .AppendLine("BEGIN TRANSACTION");

            foreach (var sheet in Sheets)
            {
                sqlBuilder.AppendLine().AppendFormat("-- {0}", sheet.DisplayName.ToUpper()).AppendLine();
                for (int i = 1; i < sheet.SheetRows.Count; i++)
                {
                    var row = sheet.SheetRows[i];
                    var query = string.Format(baseSql, sheet.DisplayName.ToLower(), row.ItManagerNeu, row.ItManagerAlt);

                    row.DienstName.ExecuteIfNotNullOrWhitespace(() => query += string.Format(" AND [Name] = '{0}'", row.DienstName));
                    row.MitarbeiterUri.ExecuteIfNotNullOrWhitespace(() => query += string.Format(" AND [MitarbeiterUri] = '{0}'", row.MitarbeiterUri));

                    sqlBuilder.AppendFormat(query).Append(";").AppendLine();
                }
            }

            sqlBuilder.AppendLine().AppendLine("COMMIT");

            var saveDialog = new SaveFileDialog();
            var dialogResult = saveDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                using (var streamWriter = new StreamWriter(File.OpenWrite(saveDialog.FileName)))
                    await streamWriter.WriteAsync(sqlBuilder.ToString());
            }

            await progressController.CloseAsync();
        }

        public bool CanCreateSql
        {
            get { return Sheets != null && Sheets.Any(); }
        }

        public async void LoadFile()
        {
            var fileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx", Multiselect = false };

            var result = fileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Sheets.Clear();
                string file = fileDialog.FileName;

                dynamic excelObj = await ExcelParser.GetExcelAsync();
                
                await Task.Run(() => excelObj.Workbooks.Open(file));
                await Task.Run(() =>
                {
                    foreach (var sheetObj in excelObj.ActiveWorkbook.Sheets)
                    {
                        Sheet sheet = new Sheet(sheetObj.Name);
                        object[,] sheetValues = sheetObj.UsedRange.Value;

                        if (sheetValues == null || (sheetValues.GetUpperBound(1) - sheetValues.GetLowerBound(1) < 4))
                            break;

                        for (int i = sheetValues.GetLowerBound(0); i <= sheetValues.GetUpperBound(0); i++)
                        {
                            SheetRow row = new SheetRow()
                            {
                                ItManagerAlt = sheetValues[i, 1] as string,
                                MitarbeiterUri = sheetValues[i, 2] as string,
                                DienstName = sheetValues[i, 3] as string,
                                DisplayName = sheetValues[i, 4] as string,
                                ItManagerNeu = sheetValues[i, 5] as string
                            };

                            sheet.Add(row);
                        }

                        Sheets.Add(new SheetViewModel(sheet));
                    }
                });

                excelObj.Application.Quit();
            }
        }
    }
}
