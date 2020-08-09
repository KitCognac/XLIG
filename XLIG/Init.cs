using ExcelDna.Integration;
using ExcelDna.IntelliSense;
using Excel = Microsoft.Office.Interop.Excel;

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

        }
        public void AutoClose()
        {
            // It is recommended to unattacth this IntelliSense server
            IntelliSenseServer.Uninstall();
            // Kill Shadow Excel Instance
            AddinContext.XlApp.Quit();
        }
    }

}