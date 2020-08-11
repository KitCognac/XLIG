using ExcelDna.Integration;
using ExcelDna.IntelliSense;
using Excel = Microsoft.Office.Interop.Excel;
using Gma.System.MouseKeyHook;

namespace XL_IGNITION
{
    public static class AddinContext
    {
        public static Excel.Application XlApp { get; set; }
    }
    public class XLAddin : IExcelAddIn
    {
        public void AutoOpen()
        {
            // Add Export Tables to SQL Custom Pane
            System.Windows.Forms.Application.EnableVisualStyles();
            XLIG.ExportTables.CTPManager.InitCTManager();
            // Enable IntelliSense for UDF
            IntelliSenseServer.Install();
            // Ref Current Excel App to Global Var for further usage
            AddinContext.XlApp = (Excel.Application)ExcelDnaUtil.Application;
            // Hook Mouse on First Load
            MouseHook_Main.M_AppHook = Hook.AppEvents();
            MouseHook_Main.Init_Unload(XLIG.Properties.Settings.Default.HScroll);
            // Hook ws event to Ctrl+Tab switch back to old sheet
            AddinContext.XlApp.SheetDeactivate += XlAppEvent_SheetDeactivate;
            AddinContext.XlApp.WorkbookDeactivate += XlAppEvent_WorkbookDeactivate;

        }
        public void AutoClose()
        {
            // Dispose Mouse Hook
            MouseHook_Main.M_AppHook.Dispose();
            // It is recommended to unattacth this IntelliSense server
            IntelliSenseServer.Uninstall();
            // Kill Shadow Excel Instance
            AddinContext.XlApp.Quit();
            AddinContext.XlApp.SheetDeactivate -= XlAppEvent_SheetDeactivate;
            AddinContext.XlApp.WorkbookDeactivate -= XlAppEvent_WorkbookDeactivate;
        }
        private void XlAppEvent_WorkbookDeactivate(Excel.Workbook Wb)
        {
            XLCommand.PreWbookName = Wb.Name;
        }
        private void XlAppEvent_SheetDeactivate(object Sh)
        {
            XLCommand.PreWsheetName = (Sh as Excel.Worksheet).Name;
            XLCommand.PreWbookName = AddinContext.XlApp.ActiveWorkbook.Name;
        }
    }

}