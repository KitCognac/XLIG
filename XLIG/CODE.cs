using System;
using System.Windows.Forms;
using ExcelDna.Integration;
using Excel = Microsoft.Office.Interop.Excel;

namespace XLIG
{//SHORTCUT FUNCTION
    public static class MyCommands
    {
        public static Excel.Workbook wb = null;
        public static Excel.Worksheet ws = null;
        public static Excel.Application xlApp = (Excel.Application)ExcelDnaUtil.Application;

        [ExcelCommand(Description = "AutoFit Shortcut", ShortCut = "^e")]
        public static void AutoFitCol()
        {
            wb = xlApp.ActiveWorkbook;
            if (wb == null && null == xlApp.Selection as Excel.Range)
                return;
            xlApp.Selection.EntireColumn.AutoFit();
        }
        [ExcelCommand(Description = "Open Sheets List", ShortCut = "^q")]
        public static void OpenSheetsList()
        {
            wb = xlApp.ActiveWorkbook;
            if (wb != null)
                if (wb.Sheets.Count > 16)
                    xlApp.CommandBars["Workbook Tabs"].Controls["More Sheets..."].Execute();
                else
                    xlApp.CommandBars["Workbook Tabs"].ShowPopup();
        }
        [ExcelCommand(Description = "Format Accounting", ShortCut = "^D")]
        public static void FormatNumber()
        {
            if (null != xlApp.Selection as Excel.Range)
                xlApp.Selection.NumberFormat = "_(* #,##0_);_(* (#,##0);_(* \" - \" ??_);_(@_)";
        }
        [ExcelCommand(Description = "Back Previous Sheet", ShortCut = "^{tab}")]
        public static void Back_Prev_Sheet()
        {
            if (MyAddIn.PreWsheetName == null && MyAddIn.PreWbookName == null)
                return;
            if (MyAddIn.PreWbookName == null)
                xlApp.Sheets[MyAddIn.PreWsheetName].Select();
            else
            {
                if (xlApp.ActiveWorkbook.Name == MyAddIn.PreWbookName)
                {
                    xlApp.Sheets[MyAddIn.PreWsheetName].Select();
                }
                else
                {
                    xlApp.Workbooks[MyAddIn.PreWbookName].Activate();
                }
            }
        }
    }
}
public static class AddinContext
{
    public static Application ExcelApp { get; set; }
}
public class MyAddIn : IExcelAddIn
{
    public static Excel.AppEvents_Event xlAppEvent = (Excel.Application)ExcelDnaUtil.Application;
    public static Excel.Application xlApp = (Excel.Application)ExcelDnaUtil.Application;
    public static string PreWbookName = null;
    public static string PreWsheetName = null;
    public void AutoOpen()
    {
        try
        {
            // The Excel Application object
            AddinContext.ExcelApp = new Application(null, ExcelDnaUtil.Application);
            //App Event
            xlAppEvent.NewWorkbook += XlAppEvent_NewWorkbook; ;
            xlAppEvent.WorkbookNewSheet += XlAppEvent_WorkbookNewSheet;
            xlAppEvent.SheetDeactivate += XlAppEvent_SheetDeactivate;
            xlAppEvent.WorkbookDeactivate += XlAppEvent_WorkbookDeactivate;
        }
        catch (Exception err)
        {
            MessageBox.Show(err.ToString());
        }
    }
    private void XlAppEvent_NewWorkbook(Excel.Workbook Wb)
    {
        xlApp.ActiveWindow.DisplayGridlines = false;
    }
    private void XlAppEvent_WorkbookNewSheet(Excel.Workbook Wb, object Sh)
    {
        xlApp.ActiveWindow.DisplayGridlines = false;
    }
    private void XlAppEvent_WorkbookDeactivate(Excel.Workbook Wb)
    {
        PreWbookName = Wb.Name;
    }
    private void XlAppEvent_SheetDeactivate(object Sh)
    {
        PreWsheetName = (Sh as Excel.Worksheet).Name;
        PreWbookName = xlApp.ActiveWorkbook.Name;
    }
    public void AutoClose()
    {
    }

}