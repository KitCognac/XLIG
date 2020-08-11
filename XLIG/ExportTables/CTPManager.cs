using ExcelDna.Integration.CustomUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XL_IGNITION;
using Excel = Microsoft.Office.Interop.Excel;

namespace XLIG.ExportTables
{
    class CTPManager
    {
        public static CustomTaskPane ctp;
        public static bool CtpViewable = false;
        public static List<DataTable> SelectedTbls = new List<DataTable>();
        //public static bool CancelRequested { get; private set; }
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
                    ctp = CustomTaskPaneFactory.CreateCustomTaskPane(typeof(ExportTablesMainView), "EXPORT SQL");
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
            if (ctp != null)
            {
                var clr = ((ExportTablesMainView)ctp.ContentControl).BackColor;
                if ((clr.B + clr.R + clr.G) / 3 <= 128)
                {
                    var ctpView = (ExportTablesMainView)ctp.ContentControl;
                    foreach (var LabelCtl in ctpView.Controls.OfType<Label>())
                    {
                        LabelCtl.ForeColor = Color.White;
                    }
                    foreach (var LabelCtl in ctpView.Controls.OfType<RadioButton>())
                    {
                        LabelCtl.ForeColor = Color.White;
                    }
                    foreach (var LabelCtl in ctpView.Controls.OfType<CheckBox>())
                    {
                        LabelCtl.ForeColor = Color.White;
                    }
                    foreach (var LabelCtl in ctpView.Controls.OfType<Panel>())
                    {
                        LabelCtl.ForeColor = Color.White;
                    }

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
                        var ctpView = (ExportTablesMainView)ctp.ContentControl;
                        ctpView.checkedListBox1.Items.Add(item.Name);
                    }
                }
            }
        }
        public static void LoadExcelTableToDotnetDataTable()
        {
            SelectedTbls.Clear();
            var XLApp = AddinContext.XlApp;
            if (XLApp != null && XLApp.ActiveWorkbook != null)
            {
                foreach (var SelTbl in ExportTablesMainView.SelectedTblsList)
                {
                    foreach (Excel.Worksheet sht in XLApp.ActiveWorkbook.Worksheets)
                    {
                        foreach (Excel.ListObject item in sht.ListObjects)
                        {
                            if (item.Name == SelTbl)
                            {
                                SelectedTbls.Add(XLRangeToDataTable(item.Range, item.Name));
                            }
                        }
                    }
                }
            }
            return;

        }

        private static DataTable XLRangeToDataTable(Excel.Range rng, string TblName)
        {
            DataTable dt = new DataTable();
            dt.TableName = TblName;

            object[,] data = rng.Value2;

            for (int i = 1; i <= rng.Columns.Count; i++)
            {//Header always string cause no need to convert
                string header = (string)data[1, i];
                StringComparison comp = StringComparison.OrdinalIgnoreCase;
                if (header.IndexOf("date", comp) >= 0)
                {
                    dt.Columns.Add(header, typeof(DateTime));
                }
                else
                {
                    dt.Columns.Add(header);
                }
            }
            for (int o = 2; o <= rng.Rows.Count; o++)
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i <= rng.Columns.Count; i++)
                {
                    string colname = dt.Columns[i - 1].ColumnName;
                    if (dt.Columns[i - 1].DataType == typeof(DateTime))
                    {
                        dr[colname] = DateTime.FromOADate((double)data[o, i]);
                    }
                    else
                    {
                        dr[colname] = data[o, i];
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static DataTable ConvertToDatatable<T>(List<T> data)
        {//For Later usage
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        static void Ctp_VisibleStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            CtpViewable = ctp.Visible;
            XLRibbon._ribbonUi.InvalidateControl("Button1");
        }

        static void Ctp_DockPositionStateChange(ExcelDna.Integration.CustomUI.CustomTaskPane CustomTaskPaneInst)
        {
            //((ExportTablesMainView)ctp.ContentControl).label1.Text = "Moved to " + CustomTaskPaneInst.DockPosition.ToString();
        }
        public static void ExportDataToSQLServer(string connStr, string schemaName, bool truncateTables, bool dropTables)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            SqlConnectionStringBuilder builder;
            try
            {
                builder = new SqlConnectionStringBuilder(connStr);
            }
            catch (ArgumentException ex)
            {
                // wrap this exception and include the connection string that we could not parse
                throw new ArgumentException($"Error parsing connections string: {connStr} - {ex.Message}", ex);
            }
            // Load Tables from Excel to List
            LoadExcelTableToDotnetDataTable();

            if (SelectedTbls.Count == 0)
            {
                // if no table selected exit
                return;
            }
            try
            {
                using (var conn = new SqlConnection(builder.ToString()))
                {
                    conn.Open();

                    foreach (var table in SelectedTbls)
                    {
                        try
                        {
                            using (var reader = new DataTableReader(table))
                            {
                                string sqlTableName = $"[{schemaName}].[{table.TableName}]";

                                EnsureSQLTableExists(conn, sqlTableName, reader, dropTables);

                                using (var transaction = conn.BeginTransaction())
                                {
                                    if (truncateTables)
                                    {
                                        using (var cmd = new SqlCommand($"truncate table {sqlTableName}", conn))
                                        {
                                            cmd.Transaction = transaction;
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    var sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, transaction); //)//, transaction))

                                    sqlBulkCopy.DestinationTableName = sqlTableName;
                                    sqlBulkCopy.BatchSize = 10000;
                                    sqlBulkCopy.EnableStreaming = true;
                                    //sqlBulkCopy.NotifyAfter = 5000;
                                    //sqlBulkCopy.SqlRowsCopied += SqlBulkCopy_SqlRowsCopied;
                                    sqlBulkCopy.WriteToServerAsync(reader);
                                    transaction.Commit();

                                    MessageBox.Show(table.TableName + ": " + sqlBulkCopy.RowsCopiedCount().ToString() + " rows copied", "Notice");
                                    //var task = sqlBulkCopy.WriteToServerAsync(reader, cancellationTokenSource.Token);

                                    //WaitForTaskPollingForCancellation(cancellationTokenSource, task);
                                    // update the currentTable with the final rowcount
                                    //currentTable.RowCount = sqlBulkCopy.RowsCopiedCount();

                                    //if (CancelRequested)
                                    //{
                                    //    transaction.Rollback();
                                    //    //currentTable.Status = ExportStatus.Cancelled;
                                    //}
                                    //else
                                    //{
                                    //    transaction.Commit();
                                    //    //currentTable.Status = ExportStatus.Done;
                                    //}
                                }
                            }
                            // jump out of table loop if we have been cancelled
                            //    if (CancelRequested)
                            //    {
                            //        break;
                            //    }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            continue; // skip to next table on error
                        }
                    }
                }
            }
            catch
            {

            }

        }
        private static void SqlBulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            //if (CancelRequested) e.Abort = true;
            //new StatusBarMessage(ActiveDocument, $"Exporting Table {currentTableIdx} of {totalTableCnt} : {sqlTableName} ({e.RowsCopied:N0} rows)");
            //ActiveDocument.RefreshElapsedTime();
        }
        //private static void WaitForTaskPollingForCancellation(CancellationTokenSource cancellationTokenSource, Task task)
        //{
        //    // poll every 1 second to see if the Cancel button has been clicked
        //    while (!task.Wait(1000))
        //    {
        //        if (CancelRequested)
        //        {
        //            cancellationTokenSource.Cancel();
        //            try
        //            {
        //                task.Wait();
        //            }
        //            catch (AggregateException ex)
        //            {
        //                Console.WriteLine(ex.InnerException.Message);
        //                Console.WriteLine("WriteToServer Canceled");
        //                break;
        //            }
        //        }
        //        if (task.IsCompleted || task.IsCompleted || task.IsFaulted) { break; }
        //    }
        //}
        private static void EnsureSQLTableExists(SqlConnection conn, string sqlTableName, DataTableReader reader, bool dropTables)
        {
            var strColumns = new StringBuilder();

            var schemaTable = reader.GetSchemaTable();

            foreach (DataRow row in schemaTable.Rows)
            {
                var colName = row.Field<string>("ColumnName");

                var regEx = System.Text.RegularExpressions.Regex.Match(colName, @".+\[(.+)\]");

                if (regEx.Success)
                {
                    colName = regEx.Groups[1].Value;
                }
                colName.Replace('|', '_');
                var sqlType = ConvertDotNetToSQLType(row);

                strColumns.AppendLine($",[{colName}] {sqlType} NULL");
            }
            string cmdText;
            if (dropTables)
            {
                cmdText = @"                
                declare @sqlCmd nvarchar(max)

                IF object_id(@tableName, 'U') is not null
                BEGIN
                    raiserror('Droping Table ""%s""', 1, 1, @tableName)
                    set @sqlCmd = 'drop table if exists ' + @tableName + char(13)
                    exec sp_executesql @sqlCmd
                END";
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@tableName", sqlTableName);
                    cmd.Parameters.AddWithValue("@columns", strColumns.ToString().TrimStart(','));

                    cmd.ExecuteNonQuery();
                }
            }

            cmdText = @"
                declare @sqlCmd nvarchar(max)
                IF object_id(@tableName, 'U') is null
                BEGIN
                    declare @schemaName varchar(20)
		            set @sqlCmd = ''
                    set @schemaName = parsename(@tableName, 2)

                    IF NOT EXISTS(SELECT * FROM sys.schemas WHERE name = @schemaName)
                    BEGIN
                        set @sqlCmd = 'CREATE SCHEMA ' + @schemaName + char(13)
                    END

                    set @sqlCmd = @sqlCmd + 'CREATE TABLE ' + @tableName + '(' + @columns + ');'

                    raiserror('Creating Table ""%s""', 1, 1, @tableName)

                    exec sp_executesql @sqlCmd
                END
                ELSE
                BEGIN
                    raiserror('Table ""%s"" already exists', 1, 1, @tableName)
                END
                ";

            using (var cmd = new SqlCommand(cmdText, conn))
            {
                cmd.Parameters.AddWithValue("@tableName", sqlTableName);
                cmd.Parameters.AddWithValue("@columns", strColumns.ToString().TrimStart(','));

                cmd.ExecuteNonQuery();
            }
        }
        private static string ConvertDotNetToSQLType(DataRow row)
        {
            var dataType = row.Field<System.Type>("DataType").ToString();

            string dataTypeName = null;

            if (row.Table.Columns.Contains("DataTypeName"))
            {
                dataTypeName = row.Field<string>("DataTypeName");
            }

            switch (dataType)
            {
                case "System.Double":
                    {
                        return "float";
                    };
                case "System.Boolean":
                    {
                        return "bit";
                    }
                case "System.String":
                    {
                        var columnSize = row.Field<int?>("ColumnSize");

                        if (string.IsNullOrEmpty(dataTypeName))
                        {
                            dataTypeName = "nvarchar";
                        }

                        string columnSizeStr = "MAX";

                        if (columnSize == null || columnSize <= 0 || (dataTypeName == "varchar" && columnSize > 8000) || (dataTypeName == "nvarchar" && columnSize > 4000))
                        {
                            columnSizeStr = "MAX";
                        }
                        else
                        {
                            columnSizeStr = columnSize.ToString();
                        }

                        return $"{dataTypeName}({columnSizeStr})";
                    }
                case "System.Decimal":
                    {
                        var numericScale = row.Field<int>("NumericScale");
                        var numericPrecision = row.Field<int>("NumericPrecision");

                        if (numericScale == 0)
                        {
                            if (numericPrecision < 10)
                            {
                                return "int";
                            }
                            else
                            {
                                return "bigint";
                            }
                        }

                        if (!string.IsNullOrEmpty(dataTypeName) && dataTypeName.EndsWith("*money"))
                        {
                            return dataTypeName;
                        }

                        if (numericScale != 255)
                        {
                            return $"decimal({numericPrecision}, {numericScale})";
                        }

                        return "decimal(38,4)";
                    }
                case "System.Byte":
                    {
                        return "tinyint";
                    }
                case "System.Int16":
                    {
                        return "smallint";
                    }
                case "System.Int32":
                    {
                        return "int";
                    }
                case "System.Int64":
                    {
                        return "bigint";
                    }
                case "System.DateTime":
                    {
                        return "datetime2(0)";
                    }
                case "System.Byte[]":
                    {
                        return "varbinary(max)";
                    }
                case "System.Xml.XmlDocument":
                    {
                        return "xml";
                    }
                default:
                    {
                        return "nvarchar(MAX)";
                    }
            }
        }
    }
}
