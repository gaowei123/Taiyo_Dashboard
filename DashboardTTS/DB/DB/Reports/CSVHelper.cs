using System;
using System.Collections.Generic; 
using System.Text;
using System.IO;
using System.Data;

namespace DBHelp.Reports
{
   public class CSVHelper
    {
        public static DataTable LoadDataFromExcel(string Path)
        { 
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + Path + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            System.Data.OleDb. OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            System.Data.OleDb.OleDbDataAdapter myCommand = null;
            DataTable dt = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new System.Data.OleDb.OleDbDataAdapter(strExcel, strConn);
            dt = new DataTable();
            myCommand.Fill(dt);
            return dt;
        }

        static public bool Export(
                                ref DataTable dt, 
                                string sCsvFileName)
        {
            if (dt.Rows.Count <= 0) return false;

            bool bSuccess = false;
            
            int i = 0;
            int j = 0;
            string sLine = "";
            StreamWriter file = null;

            try
            {
                #region Write to file

                if (!System.IO.Directory.Exists( System.IO.Path.GetDirectoryName(sCsvFileName) ))
                { 
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(sCsvFileName));
                }
                 


                file = new StreamWriter(sCsvFileName);

                #region Write the column names.

                sLine = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {
                    sLine += ((i == 0) ? "" : ", ") + dt.Columns[i].ColumnName;
                }

                file.WriteLine(sLine);

                #endregion

                #region Write all rows.

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    sLine = "";

                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        sLine += ((j == 0) ? "" : ", ") + dt.Rows[i][j].ToString();
                    }

                    file.WriteLine(sLine);
                }

                file.Close();

                #endregion

                bSuccess = true;

                #endregion
            }
            catch (Exception ex)
            {
                bSuccess = false;
                throw ex;
            }

            return bSuccess;
        }


        static public bool ExportWithDialog(
                                   ref DataTable dt,
                                   string sCsvFileName)
        {
            if (dt.Rows.Count <= 0) return false;

            bool bSuccess = false;

            int i = 0;
            int j = 0;
            string sLine = "";
            StreamWriter file = null;
           
            try
            {
                #region Write to file

                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(sCsvFileName)))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(sCsvFileName));
                }

                //file = new StreamWriter(sCsvFileName)
                //02 07 using default encodeing(ANSI) to display Chinese
                file = new StreamWriter(sCsvFileName,false,System.Text.Encoding.Default);
               
                #region Write the column names.

                sLine = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {
                    sLine += ((i == 0) ? "" : ", ") + dt.Columns[i].ColumnName;
                }

                file.WriteLine(sLine);

                #endregion

                #region Write all rows.

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    sLine = "";

                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        sLine += ((j == 0) ? "" : ", ") + dt.Rows[i][j].ToString().Replace(","," ");
                    }

                    file.WriteLine(sLine);
                }

                file.WriteLine("");

                file.Close();

                #endregion

                bSuccess = true;

                #endregion
            }
            catch (Exception ex)
            {
                bSuccess = false;
                throw ex;
            }

            return bSuccess;
        }
    }
}