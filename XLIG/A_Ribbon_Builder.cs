//using System.Runtime.InteropServices;
//using AddinX.Ribbon.Contract;
//using AddinX.Ribbon.Contract.Command;
//using AddinX.Ribbon.ExcelDna;
//using Excel = NetOffice.ExcelApi;

//namespace Ribbon
//{
//    public class AddinContext
//    {
//        public static Excel.Application ExcelApp { get; set; }
//    }
//    [ComVisible(true)]
//    public class Ribbon : RibbonFluent
//    {
//        private const string XLRib_ID = "XL_Ignition_Core";
//        private const string XLRib_Name = "XL Ignition";
//        private const string XLRib_Keytip = "X";

//        private const string XLRib_FM_ID = "XL_FormatGroup";
//        private const string XLRib_FM_Name = "Quick Format";
//        private const string XLRib_FM_QFCell_ID = "XL_FG_QF_Cell";
//        private const string XLRib_FM_QFCell_Name = "Cells";
//        private const string XLRib_FM_QFCell_Image = "AccessTableEvents";
//        private const string XLRib_FM_QFCell_CABtn_ID = "CellAccountingFM";
//        private const string XLRib_FM_QFCell_CABtn_Name = "Accounting Format";
//        private const string XLRib_FM_QFCell_CABtn_Image = "FormattingUnique";
//        private const string XLRib_FM_QFCell_CABtn_STip = "Accounting Quick Format";
//        private const string XLRib_FM_QFCell_CABtn_SuTip = "Format selected cell(s) with Accounting, remove 2 decimal, fit column for visible number";
//        private const string XLRib_FM_QFCell_FHBtn_ID = "HeaderFM";
//        private const string XLRib_FM_QFCell_FHBtn_Name = "Header Quick Format";
//        private const string XLRib_FM_QFCell_FHBtn_Image = "CreateFormSplitForm";
//        private const string XLRib_FM_QFCell_FHBtn_STip = "Header Quick Format";
//        private const string XLRib_FM_QFCell_FHBtn_SuTip = "Format selected cell(s) with blue fill, bold font, center.";

//        protected override void CreateFluentRibbon(IRibbonBuilder builder)
//        {
//            builder.CustomUi.Ribbon.Tabs(c =>
//            {
//                c.AddTab(XLRib_Name).SetId(XLRib_ID)
//                    .Groups(g =>
//                    {
//                        g.AddGroup(XLRib_FM_Name).SetId(XLRib_FM_ID)
//                            .Items(d =>
//                            {
//                                d.AddMenu(XLRib_FM_QFCell_Name).SetId(XLRib_FM_QFCell_ID).ShowLabel()
//                                .ImageMso(XLRib_FM_QFCell_Image).LargeSize()
//                                .ItemLargeSize().AddItems(e =>
//                                   {
//                                       e.AddButton(XLRib_FM_QFCell_CABtn_Name).SetId(XLRib_FM_QFCell_CABtn_ID)
//                                       .ImageMso(XLRib_FM_QFCell_CABtn_Image)
//                                       .ShowLabel()
//                                       .Screentip(XLRib_FM_QFCell_CABtn_STip)
//                                       .Supertip(XLRib_FM_QFCell_CABtn_SuTip)
//                                       ;
//                                       e.AddButton(XLRib_FM_QFCell_FHBtn_Name).SetId(XLRib_FM_QFCell_FHBtn_ID)
//                                       .ImageMso(XLRib_FM_QFCell_FHBtn_Image)
//                                       .ShowLabel()
//                                       .Screentip(XLRib_FM_QFCell_FHBtn_STip)
//                                       .Supertip(XLRib_FM_QFCell_FHBtn_SuTip)
//                                       ;
//                                   });

//                            });
//                    })
//                    .Keytip(XLRib_Keytip)
//                    ;
//            });
//        }

//        protected override void CreateRibbonCommand(IRibbonCommands cmds)
//        {
//            cmds.AddButtonCommand(XLRib_FM_QFCell_CABtn_ID).Action(() => XLIG.MyCommands.FormatNumber());
//            cmds.AddButtonCommand(XLRib_FM_QFCell_FHBtn_ID).Action(() => XLIG.MyCommands.FormatHeader());
//        }

//        public override void OnClosing()
//        {
//        }

//        public override void OnOpening()
//        {
//        }
//    }
//}