using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using org.in2bits.MyXls;

namespace Demo.Component.Tools
{
    /// <summary>
    /// 导出excel表格
    /// </summary>
    public class ExcelHelper
    {
        const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";

        /// <summary>
        /// 导出EXECL 表格
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static string DataTableToExcel(System.Data.DataTable dt, string excelPath)
        {


            if (dt == null)
            {
                return "DataTable不能为空";
            }

            //命名sheet1表名
            dt.TableName = "Sheet1";

            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder sb;
            string connString;

            if (rows == 0)
            {
                return "没有数据";
            }

            sb = new StringBuilder();
            connString = string.Format(ConnectionString, excelPath);

            //生成创建表的脚本
            sb.Append("CREATE TABLE ");
            sb.Append(dt.TableName + " ( ");

            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1)
                    sb.Append(string.Format("{0} nvarchar,", dt.Columns[i].ColumnName));
                else
                    sb.Append(string.Format("{0} nvarchar)", dt.Columns[i].ColumnName));
            }

            using (OleDbConnection objConn = new OleDbConnection(connString))
            {
                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;

                objCmd.CommandText = sb.ToString();

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (System.Exception e)
                {
                    return "在Excel中创建表失败，错误信息：" + e.Message;
                }

                #region 生成插入数据脚本
                sb.Remove(0, sb.Length);
                sb.Append("INSERT INTO ");
                sb.Append(dt.TableName + " ( ");

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append(dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append(dt.Columns[i].ColumnName + ") values (");
                }

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append("@" + dt.Columns[i].ColumnName + ",");
                    else
                        sb.Append("@" + dt.Columns[i].ColumnName + ")");
                }
                #endregion


                //建立插入动作的Command
                objCmd.CommandText = sb.ToString();
                OleDbParameterCollection param = objCmd.Parameters;

                for (int i = 0; i < cols; i++)
                {
                    param.Add(new OleDbParameter("@" + dt.Columns[i].ColumnName, OleDbType.VarChar));
                }

                //遍历DataTable将数据插入新建的Excel文件中
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }

                    objCmd.ExecuteNonQuery();
                }

                return "数据已成功导入Excel";
            }//end using
        }


        /// <summary> 
        /// 读取Excel文档 
        /// </summary> 
        /// <param name="Path">文件名称</param> 
        /// <returns>返回一个数据集</returns> 
        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            System.Data.OleDb.OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [Sheet1$]";
            myCommand = new System.Data.OleDb.OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            conn.Close();
            return ds;
        }

        /// <summary>
        /// 将tables写入EXCEL文件，已预制格式，可覆盖同名文件
        /// </summary>
        /// <param name="file_path">绝对路径，可覆盖同名文件</param>
        /// <param name="tables"></param>
        public static void WriteAsXls(string file_path, System.Data.DataTable[] tables)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = System.IO.Path.GetFileName(file_path);

            for (int i = 0; i < tables.Length; i++)
            {
                System.Data.DataTable table = tables[i];

                if (table.Columns.Count > BIFF8.MaxCols)
                    throw new ApplicationException(string.Format("Table {0} has too many columns {1} to fit on Worksheet {2} with the given startCol {3}",
                                                   table.TableName, table.Columns.Count, BIFF8.MaxCols, 1));
                if (table.Rows.Count > (BIFF8.MaxRows - 1))
                    throw new ApplicationException(string.Format("Table {0} has too many rows {1} to fit on Worksheet {2} with the given startRow {3}",
                                                   table.TableName, table.Rows.Count, (BIFF8.MaxRows - 1), 1));

                Worksheet sheet = xls.Workbook.Worksheets.Add(table.TableName);

                ColumnInfo ci = new ColumnInfo(xls, sheet);

                ci.Width = (20 * 256);
                ci.ColumnIndexEnd = (ushort)table.Rows.Count;
                sheet.AddColumnInfo(ci);

                int row = 1, col = 1;

                foreach (System.Data.DataColumn dataColumn in table.Columns)
                {
                    Cell cellHeader = sheet.Cells.Add(row, col++, dataColumn.ColumnName);

                    cellHeader.Font.Bold = true;
                    cellHeader.HorizontalAlignment = HorizontalAlignments.Centered;
                    cellHeader.Pattern = 1;
                    cellHeader.PatternColor = Colors.Silver;
                    cellHeader.UseBorder = true;
                    cellHeader.TopLineStyle = 1;
                    cellHeader.TopLineColor = Colors.Silver;
                    cellHeader.BottomLineStyle = 1;
                    cellHeader.BottomLineColor = Colors.Silver;
                    cellHeader.LeftLineStyle = 1;
                    cellHeader.LeftLineColor = Colors.Silver;
                    cellHeader.RightLineStyle = 1;
                    cellHeader.RightLineColor = Colors.Silver;
                }

                foreach (System.Data.DataRow dataRow in table.Rows)
                {
                    row++;
                    col = 1;
                    foreach (object dataItem in dataRow.ItemArray)
                    {
                        object value = dataItem;

                        if (dataItem == DBNull.Value)
                            value = null;
                        if (dataRow.Table.Columns[col - 1].DataType == typeof(byte[]))
                            value = string.Format("[ByteArray({0})]", ((byte[])value).Length);

                        Cell cellbody = sheet.Cells.Add(row, col++, value);
                    }
                }
            }

            xls.Save(System.IO.Path.GetDirectoryName(file_path), true);
        }

        /// <summary>
        /// 从EXCEL中生成data.tables,表名同EXCEL的sheet名称
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public static System.Data.DataTable[] ReadXls(string file_path)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
               Data Source=" + file_path + @";Extended Properties=""Excel 12.0;HDR=YES;""";

            string sql = "SELECT * FROM [{0}]";

            DataTable[] tables;

            using (OleDbConnection excelConnection = new OleDbConnection(connectionString))
            {
                excelConnection.Open(); // This code will open excel file.

                DataTable tblSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                List<string> tblnames = new List<string>();

                foreach (DataRow row in tblSchema.Rows)
                {
                    if (!((string)row["TABLE_NAME"]).Contains("$FilterDatabase"))
                    {
                        tblnames.Add(((string)row["TABLE_NAME"]));
                    }
                }

                tblSchema.Dispose();

                tables = new DataTable[tblnames.Count];

                OleDbDataAdapter da = new OleDbDataAdapter();

                for (int i = 0; i < tables.Length; i++)
                {
                    tables[i] = new DataTable();
                    tables[i].TableName = tblnames[i];

                    try
                    {
                        da.SelectCommand = new OleDbCommand(string.Format(sql, tblnames[i]), excelConnection);
                        da.Fill(tables[i]);
                    }
                    finally
                    {
                        if (excelConnection.State == ConnectionState.Open)
                            excelConnection.Close();

                        da.Dispose();
                    }
                }
            }

            return tables;
        }

        #region  清理过时的Excel文件

        private void ClearFile(string FilePath)
        {
            String[] Files = System.IO.Directory.GetFiles(FilePath);
            if (Files.Length > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        System.IO.File.Delete(Files[i]);
                    }
                    catch
                    {
                    }

                }
            }
        }
        #endregion

        /// <summary>
        /// 下载Excel并删除
        /// </summary>
        /// <param name="path"></param>
        public static void DownloadExcel(string fileName, string path, bool delete)
        {
            FileInfo fi = new FileInfo(path);//excelFile为文件在服务器上的地址
            HttpResponse contextResponse = HttpContext.Current.Response;
            contextResponse.Clear();
            contextResponse.Buffer = true;
            contextResponse.Charset = "utf-8"; //设置了类型为中文防止乱码的出现 
            contextResponse.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            contextResponse.AppendHeader("Content-Length", fi.Length.ToString());
            contextResponse.ContentEncoding = Encoding.Default;
            contextResponse.ContentType = "application/ms-excel";//设置输出文件类型为excel文件
            contextResponse.WriteFile(fi.FullName);
            contextResponse.Flush();
            if (delete)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            contextResponse.End();
        }

    }
}
