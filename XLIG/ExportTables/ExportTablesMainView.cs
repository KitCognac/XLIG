using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace XLIG.ExportTables
{
    [ComVisible(true)]
    public partial class ExportTablesMainView : UserControl
    {
        public ExportTablesMainView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CTPManager.InitCTManager();
        }
    }
}
