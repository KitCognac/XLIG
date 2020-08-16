using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLIG.WorkspaceManager.ActionPaneModule.TextSearch
{
    public class XLObject
    {
        /// <summary>
        /// Data Transfer Object that contains data about Workbook
        /// </summary>
        readonly List<XLObject> _children = new List<XLObject>();
        public IList<XLObject> Children
        {
            get { return _children; }
        }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
