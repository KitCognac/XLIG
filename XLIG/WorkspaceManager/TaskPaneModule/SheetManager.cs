using ExcelDna.Integration.CustomUI;
using System.Collections.Generic;
using System.Linq;
using XL_IGNITION;

namespace XLIG.WorkspaceManager.TaskPaneModule
{
    class SheetManager
    {
        public static Dictionary<string, CustomTaskPane> CTP_DICT = new Dictionary<string, CustomTaskPane>();
        static int control_width;
        static int control_height;
        public static CustomTaskPane ctp;
        public static void SheetManagerCTP()
        {
            string paneID = null;
            if (AddinContext.XlApp != null)
            {
                paneID = "SheetManagerCTP" + AddinContext.XlApp.Hwnd.ToString();
                if (!CTP_DICT.ContainsKey(paneID))
                {
                    // Define Task Pane size on Action Pane Size.
                    var control = new SheetManagerCTP();
                    control_width = control.Width;
                    control_height = control.Height;

                    // Init Task Pane
                    ctp = CustomTaskPaneFactory.CreateCustomTaskPane(control, "Sheet Manager");

                    // Then dock state change event can not change size so we need to config size on floating mode first.
                    ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionFloating;
                    ctp.Height = control_height;
                    ctp.Width = control_width;
                    // Change back to default Dock Position
                    ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionLeft;
                    ctp.Width = control_width;
                    ctp.DockPositionStateChange += Ctp_DockPositionStateChange;
                    ctp.VisibleStateChange += Ctp_VisibleStateChange;

                    // Update Table List on first run

                    // Add Task Pane on each workbook opened.
                    CTP_DICT.Add(paneID, ctp);
                }
                else
                {
                    // If Custom Task Pane already created, Call it from Dict and refresh Table List to be exported.
                    ctp = CTP_DICT.Single(x => x.Key == paneID).Value;
                }
            }
        }
        static void Ctp_VisibleStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            XLRibbon._ribbonUi.InvalidateControl("ShtManager");
        }

        static void Ctp_DockPositionStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            // This event can not trigger size change for task pane.
        }
    }
}
