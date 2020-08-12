//using System;
//using System.Windows.Forms;
//using Gma.System.MouseKeyHook;

//namespace XL_IGNITION
//{
//    public class MouseHook_Main
//    {
//        private static Keys HoldingKey = Keys.None;
//        public static IKeyboardMouseEvents M_AppHook;
//        public static void Init_Unload(bool state)
//        {
//            if (state)
//            {
//                // Note: for the application hook, use the Hook.AppEvents() instead Hook.GlobalEvents();
//                // M_AppHook = Hook.AppEvents();
//                M_AppHook.KeyDown += Hook_KeyDown;
//                M_AppHook.KeyUp += Hook_KeyUp;
//                M_AppHook.MouseWheelExt += Hook_MWheelExt;
//            }
//            else if (M_AppHook != null)
//            {
//                M_AppHook.KeyDown -= Hook_KeyDown;
//                M_AppHook.KeyUp -= Hook_KeyUp;
//                M_AppHook.MouseWheelExt -= Hook_MWheelExt;
//                // M_AppHook.Dispose();
//            }

//        }
//        private static void Hook_MWheelExt(object sender, MouseEventExtArgs e)
//        {
//            if (HoldingKey == Keys.ShiftKey)
//            {
//                e.Handled = true;
//                HorizontalScroll(e.Delta);
//            }
//        }
//        private static void Hook_KeyDown(object sender, KeyEventArgs e)
//        {
//            HoldingKey = e.KeyCode;
//        }
//        private static void Hook_KeyUp(object sender, KeyEventArgs e)
//        {
//            HoldingKey = Keys.None;
//        }
//        private static void HorizontalScroll(int WheelDirection)
//        {
//            var XLApp = AddinContext.XlApp;
//            if (XLApp.ActiveWorkbook != null && XLApp.ActiveWindow != null)
//            {
//                if (WheelDirection < 0)
//                    XLApp.ActiveWindow.SmallScroll(Type.Missing, Type.Missing, 1, Type.Missing); //To the Right
//                else
//                    XLApp.ActiveWindow.SmallScroll(Type.Missing, Type.Missing, Type.Missing, 1); //To the Left
//            }
//            return;
//        }
//    }
//}