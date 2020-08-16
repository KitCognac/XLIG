using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLIG.WorkspaceManager.ActionPaneModule.TextSearch
{
    public static class XLServer
    {
        #region GetXLWorkbookTree

        public static XLObject GetXLWorkbookTree()
        {
            return new XLObject
            {
                Name = "Book1",
                Type = "Workbook",
                Children =
                {
                    new XLObject
                    {
                        Name = "Sheet1",
                        Type = "Worksheet"
                    },
                    new XLObject
                    {
                        Name = "Sheet2",
                        Type = "Worksheet"
                    }
                }
            };
        }

        #endregion //GetXLWorkbookTree
    }
}
