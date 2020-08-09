using ExcelDna.Integration.CustomUI;
using Excel = Microsoft.Office.Interop.Excel;
using XL_IGNITION;
using System.Collections.Generic;
using System.Linq;

namespace XLIG.ExportTables
{
    class CTPManager
    {
        public static CustomTaskPane ctp;
        static Dictionary<string, CustomTaskPane> dict = new Dictionary<string, CustomTaskPane>();

        public static void InitCTManager()
        {
            string paneID = null;
            if (AddinContext.XlApp != null)
            {
                paneID = "CTP" + AddinContext.XlApp.Hwnd.ToString();
                if (!dict.ContainsKey(paneID))
                {
                    // Make a new one using ExcelDna.Integration.CustomUI.CustomTaskPaneFactory 
                    ctp = CustomTaskPaneFactory.CreateCustomTaskPane(typeof(ExportTablesMainView), "PUSH TABLE TO SQL");
                    ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionLeft;
                    ctp.DockPositionStateChange += Ctp_DockPositionStateChange;
                    ctp.VisibleStateChange += Ctp_VisibleStateChange;
                    // Minimum width for Custom Pane
                    ctp.Width = 250;
                    RefreshTableList();
                    dict.Add(paneID, ctp);
                }
                else
                {
                    ctp = dict.Single(x => x.Key == paneID).Value;
                    RefreshTableList();
                }
            }

        }

        public static void RefreshTableList()
        {
            ((ExportTablesMainView)ctp.ContentControl).checkedListBox1.Items.Clear();
            var XLApp = AddinContext.XlApp;
            if (XLApp != null && XLApp.ActiveWorkbook != null)
            {
                foreach (Excel.Worksheet sht in XLApp.ActiveWorkbook.Worksheets)
                {
                    foreach (Excel.ListObject item in sht.ListObjects)
                    {
                        ((ExportTablesMainView)ctp.ContentControl).checkedListBox1.Items.Add(item.Name);
                    }
                }
            }
        }

        static void Ctp_VisibleStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            //MessageBox.Show("Visibility changed to " + CustomTaskPaneInst.Visible);
        }

        static void Ctp_DockPositionStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            //((ExportTablesMainView)ctp.ContentControl).label1.Text = "Moved to " + CustomTaskPaneInst.DockPosition.ToString();
        }

    }
}
