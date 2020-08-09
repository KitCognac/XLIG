using ExcelDna.Integration;
using Excel = Microsoft.Office.Interop.Excel;

namespace XL_IGNITION
{
    public class XLCommand
    {
        // Global Variables under this class
        public static Excel.Workbook wb = null;
        public static Excel.Worksheet ws = null;
        public static Excel.Application XLApp = AddinContext.XlApp;
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
    }
}