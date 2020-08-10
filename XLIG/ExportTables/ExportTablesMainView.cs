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
using System.Security;

namespace XLIG.ExportTables
{
    [ComVisible(true)]

    public partial class ExportTablesMainView : UserControl
    {
        public static string ServerName;
        public static string Database;
        public static string Schema;
        public static string Username;
        public static string Password;
        public static string SqlConnectionString;
        public static bool truncateTables = false;
        public static bool dropTables = false;
        public static int SelectedTbls = 0;
        public static List<string> SelectedTblsList = new List<string>();
        public enum SqlAuthenticationType
        {
            Sql,
            Windows
        }
        public SqlAuthenticationType AuthenticationType;
        public ExportTablesMainView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AuthenticationType == SqlAuthenticationType.Windows)
            {
                SqlConnectionString = $"Server={ServerName};Database={Database};Trusted_Connection=True;";
            }
            else
            {
                SqlConnectionString = $"Server={ServerName};Database={Database};User Id={Username};Password={Password}";
            }
            CTPManager.ExportDataToSQLServer(SqlConnectionString, Schema, truncateTables, dropTables);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Refresh Table List
            CTPManager.InitCTManager();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                AuthenticationType = SqlAuthenticationType.Sql;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                AuthenticationType = SqlAuthenticationType.Windows;
                UserNameInput.Enabled = false;
                PasswordInput.Enabled = false;
                UserNameInput.BackColor = Color.Gray;
                PasswordInput.BackColor = Color.Gray;
            }
            else
            {
                UserNameInput.Enabled = true;
                PasswordInput.Enabled = true;
                UserNameInput.BackColor = Color.White;
                PasswordInput.BackColor = Color.White;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ServerName = ServerInput.Text;
        }

        private void UserNameInput_TextChanged(object sender, EventArgs e)
        {
            Username = UserNameInput.Text;
        }

        private void PasswordInput_TextChanged(object sender, EventArgs e)
        {
            Password = PasswordInput.Text;
        }

        private void DatabaseInput_TextChanged(object sender, EventArgs e)
        {
            Database = DatabaseInput.Text;
        }

        private void SchemaInput_TextChanged(object sender, EventArgs e)
        {
            Schema = SchemaInput.Text;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTbls = checkedListBox1.CheckedItems.Count;
            SelectedTblsList = checkedListBox1.CheckedItems.OfType<string>().ToList();
        }

        private void CheckTruncateTBL_CheckedChanged(object sender, EventArgs e)
        {
            truncateTables = CheckTruncateTBL.Checked;
            if (CheckTruncateTBL.Checked)
            {
                CheckDrop.Checked = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CheckDrop_CheckedChanged(object sender, EventArgs e)
        {
            dropTables = CheckDrop.Checked;
            if (CheckDrop.Checked)
            {
                CheckTruncateTBL.Checked = false;
            }
        }
    }
}
