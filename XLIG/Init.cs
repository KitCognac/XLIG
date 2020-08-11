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

        }
        public void AutoClose()
        {
            // It is recommended to unattacth this IntelliSense server
            IntelliSenseServer.Uninstall();
            // Kill Shadow Excel Instance
            AddinContext.XlApp.Quit();
            // Dispose Mouse Hook
            MouseHook_Main.M_AppHook.Dispose();
        }
    }

}