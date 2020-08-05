using System;
using System.Windows.Forms;
using ExcelDna.Integration;
using Excel = NetOffice.ExcelApi;
using Gma.System.MouseKeyHook;

namespace XLIG
{
    public static class AddinContext
    {
        public static Excel.Application XlApp { get; set; }
    }
    public class MyAddIn : IExcelAddIn
    {
        public static string PreWbookName = null;
        public static string PreWsheetName = null;
        public static Excel.Application XlApp = null;
        public static Keys HoldingKey = Keys.None;
        //Variable for Key/Mouse Hook
        private IKeyboardMouseEvents M_AppHook;
        public void AutoOpen()
        {
            try
            {
                //Catch ExcelApp through NetOffice API
                AddinContext.XlApp = new Excel.Application(null, ExcelDnaUtil.Application);
                Ribbon.AddinContext.ExcelApp = AddinContext.XlApp; //Initiate Ribbon class
                XlApp = AddinContext.XlApp;
                //App Event
                XlApp.NewWorkbookEvent += XlAppEvent_NewWorkbook; ;
                XlApp.WorkbookNewSheetEvent += XlAppEvent_WorkbookNewSheet;
                XlApp.SheetDeactivateEvent += XlAppEvent_SheetDeactivate;
                XlApp.WorkbookDeactivateEvent += XlAppEvent_WorkbookDeactivate;
                // Note: for the application hook, use the Hook.AppEvents() instead Hook.GlobalEvents();
                M_AppHook = Hook.AppEvents();
                M_AppHook.KeyDown += Hook_KeyDown;
                M_AppHook.KeyUp += Hook_KeyUp;
                M_AppHook.MouseWheelExt += Hook_MWheelExt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private void Hook_MWheelExt(object sender, MouseEventExtArgs e)
        {
            if (HoldingKey == Keys.ShiftKey)
            {
                e.Handled = true;
                MyCommands.Vertical_Scroll(e.Delta);
            }
        }
        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            HoldingKey = e.KeyCode;
        }
        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            HoldingKey = Keys.None;
        }
        private void XlAppEvent_NewWorkbook(Excel.Workbook Wb)
        {
            XlApp.ActiveWindow.DisplayGridlines = false;
        }
        private void XlAppEvent_WorkbookNewSheet(Excel.Workbook Wb, object Sh)
        {
            XlApp.ActiveWindow.DisplayGridlines = false;
        }
        private void XlAppEvent_WorkbookDeactivate(Excel.Workbook Wb)
        {
            PreWbookName = Wb.Name;
        }
        private void XlAppEvent_SheetDeactivate(object Sh)
        {
            PreWsheetName = (Sh as Excel.Worksheet).Name;
            PreWbookName = XlApp.ActiveWorkbook.Name;
        }
        public void AutoClose()
        {
            try
            {
                //App Event Close
                XlApp.NewWorkbookEvent -= XlAppEvent_NewWorkbook; ;
                XlApp.WorkbookNewSheetEvent -= XlAppEvent_WorkbookNewSheet;
                XlApp.SheetDeactivateEvent -= XlAppEvent_SheetDeactivate;
                XlApp.WorkbookDeactivateEvent -= XlAppEvent_WorkbookDeactivate;
                //App deeply close
                //Sometime App restart after close with no reason. Still working on this error.
                XlApp.DisposeChildInstances(true); 
                XlApp.Quit();
                XlApp = null;
                //It is recommened to dispose it
                M_AppHook.KeyDown -= Hook_KeyDown;
                M_AppHook.KeyUp -= Hook_KeyUp;
                M_AppHook.MouseWheelExt -= Hook_MWheelExt;
                M_AppHook.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
    }
    //SHORTCUT FUNCTION
    public static class MyCommands
    {
        public static Excel.Workbook wb = null;
        public static Excel.Worksheet ws = null;
        public static Excel.Application XlApp = AddinContext.XlApp;

        [ExcelCommand(Description = "Testing", ShortCut = "^R")]
        public static void TESTING()
        {
            XlApp.CommandBars.ExecuteMso("FileSaveAsMenu");
        }
        [ExcelCommand(Description = "AutoFit Shortcut", ShortCut = "^e")]
        public static void AutoFitCol()
        {
            wb = XlApp.ActiveWorkbook;
            if (wb != null && null != XlApp.Selection as Excel.Range)
            {
                (XlApp.Selection as Excel.Range).EntireColumn.AutoFit();
            }
        }
        [ExcelCommand(Description = "Open Sheets List", ShortCut = "^q")]
        public static void OpenSheetsList()
        {
            wb = XlApp.ActiveWorkbook;
            if (wb != null)
            {
                if (wb.Sheets.Count > 16)
                {
                    XlApp.CommandBars["Workbook Tabs"].Controls["More Sheet..."].Execute();
                }
                else
                {
                    XlApp.CommandBars["Workbook Tabs"].ShowPopup();
                }
            }
        }
        [ExcelCommand(Description = "Format Accounting", ShortCut = "^D")]
        public static void DeleteActiveSheet()
        {
            Excel.Worksheet wsh = (XlApp.ActiveSheet as Excel.Worksheet);
            if (wsh != null && XlApp.Sheets.Count > 1)
            {
                if (wsh.UsedRange != null)
                {
                    wsh.Delete();
                }
            }
        }
        [ExcelCommand(Description = "New SHeet", ShortCut = "^T")]
        public static void NewSheetShortCut()
        {
            if (XlApp.ActiveWorkbook != null)
            {
                XlApp.ActiveWorkbook.Sheets.Add(Type.Missing, XlApp.ActiveSheet, Type.Missing, Type.Missing); //Add (object Before, object After, object Count, object Type);
            }
        }
        [ExcelCommand(Description = "Increase Column Width by 1 point", ShortCut = "^S")]
        public static void IncreaseColWidth()
        {
            Excel.Range sel = XlApp.Selection as Excel.Range;
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
            Excel.Range sel = XlApp.Selection as Excel.Range;
            if (sel != null)
            {
                double selW = Convert.ToDouble(sel.EntireColumn.ColumnWidth);
                if (selW > 1) //Column Width Limit
                {
                    sel.EntireColumn.ColumnWidth = selW - 1.00;
                }
            }
        }
        [ExcelCommand(Description = "Format Accounting")]
        public static void FormatNumber()
        {
            Excel.Range sel = XlApp.Selection as Excel.Range;
            if (sel != null)
            {
                sel.NumberFormat = "_(* #,##0_);_(* (#,##0);_(* \" - \" ??_);_(@_)";
            }
        }
        [ExcelCommand(Description = "Format Header")]
        public static void FormatHeader()
        {
            Excel.Range Sel = XlApp.Selection as Excel.Range;
            if (Sel != null)
            {
                XlApp.ScreenUpdating = false;
                Sel.Interior.Color = 12611584; //Blue
                Sel.Interior.Pattern = 1; //xlSolid
                Sel.Interior.PatternColorIndex = -4105; //xlAutomatic
                Sel.Interior.TintAndShade = 0;
                Sel.Interior.PatternTintAndShade = 0;
                Sel.Font.Bold = true;
                Sel.Font.ThemeColor = 1; //xlThemeColorDark1
                Sel.Font.TintAndShade = 0;
                Sel.HorizontalAlignment = -4108; //xlCenter
                Sel.VerticalAlignment = -4108;
                Sel.WrapText = true;
                XlApp.ScreenUpdating = true;
            }
        }
        [ExcelCommand(Description = "Back Previous Sheet", ShortCut = "^{tab}")]
        public static void Back_Prev_Sheet()
        {
            if (MyAddIn.PreWsheetName == null && MyAddIn.PreWbookName == null)
            {
                return;
            }
            try
            {
                if (MyAddIn.PreWbookName == null)
                {
                    (XlApp.Sheets[MyAddIn.PreWsheetName] as Excel.Worksheet).Select();
                }
                else
                {
                    if (XlApp.ActiveWorkbook.Name == MyAddIn.PreWbookName)
                    {
                        (XlApp.Sheets[MyAddIn.PreWsheetName] as Excel.Worksheet).Select();
                    }
                    else
                    {
                        XlApp.Workbooks[MyAddIn.PreWbookName].Activate();
                    }
                }
            }
            catch { /* Sheet Deleted or workbook closed could cause errors. Throw catch here for that. */ }
        }
        [ExcelCommand(Description = "Vertical Scroll")]
        public static void Vertical_Scroll(int WheelDirection)
        {
            if (XlApp.ActiveWorkbook != null && XlApp.ActiveWindow != null)
            {
                if (WheelDirection < 0)
                    XlApp.ActiveWindow.SmallScroll(Type.Missing, Type.Missing, 1, Type.Missing); //To the Right
                else
                    XlApp.ActiveWindow.SmallScroll(Type.Missing, Type.Missing, Type.Missing, 1); //To the Left
            }
            return;
        }
    }
}