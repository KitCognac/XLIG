using ExcelDna.Integration;
using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace XL_IGNITION
{
    public class XLCommand
    {
        // Global Variables under this class
        public static Excel.Workbook wb = null;
        public static Excel.Worksheet ws = null;
        public static Excel.Application XLApp = AddinContext.XlApp;
        public static string PreWbookName = null;
        public static string PreWsheetName = null;
        [ExcelCommand(Description = "Open Sheets List", ShortCut = "^q")]
        public static void OpenSheetsList()
        {
            Excel.Workbook wb = XLApp.ActiveWorkbook;
            if (wb != null)
            {
                if (wb.Sheets.Count > 16)
                {
                    AddinContext.XlApp.CommandBars["Workbook Tabs"].Controls["More Sheet..."].Execute();
                }
                else
                {
                    AddinContext.XlApp.CommandBars["Workbook Tabs"].ShowPopup();
                }
            }
        }
        [ExcelFunction(Name = "ActiveSheetName", Description = "Return Current Sheet Name")]
        public static string ActiveSheetName()
        {
            return XLApp.ActiveSheet.Name;
        }
        [ExcelCommand(Description = "AutoFit Shortcut", ShortCut = "^e")]
        public static void AutoFitCol()
        {
            wb = XLApp.ActiveWorkbook;
            if (wb != null && null != XLApp.Selection as Excel.Range)
            {
                (XLApp.Selection as Excel.Range).EntireColumn.AutoFit();
            }
        }
        [ExcelCommand(Description = "New Sheet", ShortCut = "^T")]
        public static void NewSheetShortCut()
        {
            if (XLApp.ActiveWorkbook != null)
            {
                XLApp.ActiveWorkbook.Sheets.Add(Type.Missing, XLApp.ActiveSheet, Type.Missing, Type.Missing); //Add (object Before, object After, object Count, object Type);
            }
        }
        [ExcelCommand(Description = "Increase Column Width by 1 point", ShortCut = "^S")]
        public static void IncreaseColWidth()
        {
            Excel.Range sel = XLApp.Selection as Excel.Range;
            if (sel != null)
            {
                double selW = Convert.ToDouble(sel.EntireColumn.ColumnWidth);
                if (selW < 254) //Column Width Limit
                {
                    sel.EntireColumn.ColumnWidth = selW + 1.00;
                }
            }
        }
        [ExcelCommand(Description = "Decrease Column Width by 1 point", ShortCut = "^A")]
        public static void DecreaseColWidth()
        {
            Excel.Range sel = XLApp.Selection as Excel.Range;
            if (sel != null)
            {
                double selW = Convert.ToDouble(sel.EntireColumn.ColumnWidth);
                if (selW > 1) //Column Width Limit
                {
                    sel.EntireColumn.ColumnWidth = selW - 1.00;
                }
            }
        }
        [ExcelCommand(Description = "Back Previous Sheet", ShortCut = "^{tab}")]
        public static void Back_Prev_Sheet()
        {
            if (PreWsheetName == null && PreWbookName == null)
            {
                return;
            }
            try
            {
                if (PreWbookName == null)
                {
                    (XLApp.Sheets[PreWsheetName] as Excel.Worksheet).Select();
                }
                else
                {
                    if (XLApp.ActiveWorkbook.Name == PreWbookName)
                    {
                        (XLApp.Sheets[PreWsheetName] as Excel.Worksheet).Select();
                    }
                    else
                    {
                        XLApp.Workbooks[PreWbookName].Activate();
                    }
                }
            }
            catch { /* Sheet Deleted or workbook closed could cause errors. Throw catch here for that. */ }
        }
    }
}