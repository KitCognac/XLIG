using ExcelDna.Integration.CustomUI;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XLIG.ExportTables;
using XLIG.Properties;
using XLIG.WorkspaceManager.TaskPaneModule;

namespace XL_IGNITION
{
    [ComVisible(true)]
    public class XLRibbon : ExcelRibbon
    {
        public static IRibbonUI _ribbonUi;
        public void Ribbon_Load(IRibbonUI sender)
        {
            _ribbonUi = sender;
        }
        // Edit CustomUI in dna File
        public void SayHello(IRibbonControl control1)
        {
            //XLCommand.OpenSheetsList();
        }
        // Toggle Horizontal Scroll Section
        public bool Toggle_HScroll_GetPressed(IRibbonControl control1)
        {
            return Settings.Default.HScroll;
        }
        public void Toggle_HScroll_Control(IRibbonControl control1, bool pressed)
        {
            Settings.Default.HScroll = pressed;
            Settings.Default.Save();
            MouseHook_Main.Init_Unload(pressed);
        }
        public void OnLoadSettingsPressed(IRibbonControl control)
        {
            var magicNumber = Settings.Default.MagicNumber;
            var userName = Settings.Default.UserName;
            MessageBox.Show($"Magic Number:  {magicNumber}, User Name: {userName}");
        }

        public void OnOverrideSettingsPressed(IRibbonControl control)
        {
            //Settings.Default.AppKey = "EvenMoreMagix";
            Settings.Default.MagicNumber = 123.456;
            Settings.Default.UserName = "The real slim shady";
        }

        public void OnSaveSettingsPressed(IRibbonControl control)
        {
            Settings.Default.Save();
        }

        public void OnResetSettingsPressed(IRibbonControl control)
        {
            Settings.Default.Reset();
        }
        public void ShowCTPExportTables(IRibbonControl control, bool pressed)
        {
            CTPManager.InitCTManager();
            CTPManager.ctp.Visible = pressed;
        }
        public bool Toggle_PTSQL_GetPressed(IRibbonControl control1)
        {
            string paneID = "CTP" + AddinContext.XlApp.Hwnd.ToString();
            if (!CTPManager.CTP_DICT.ContainsKey(paneID))
            {
                return false;
            }
            else
            {
                CustomTaskPane ctp = CTPManager.CTP_DICT.Single(x => x.Key == paneID).Value;
                return ctp.Visible;
            }
        }
        public void SheetManagerRibControl(IRibbonControl control, bool pressed)
        {
            SheetManager.SheetManagerCTP();
            SheetManager.ctp.Visible = pressed;
        }
        public bool SheetManagerRibControlGetPressed(IRibbonControl control1)
        {
            string paneID = "SheetManagerCTP" + AddinContext.XlApp.Hwnd.ToString();
            if (!SheetManager.CTP_DICT.ContainsKey(paneID))
            {
                return false;
            }
            else
            {
                CustomTaskPane ctp = SheetManager.CTP_DICT.Single(x => x.Key == paneID).Value;
                return ctp.Visible;
            }
        }
    }
}