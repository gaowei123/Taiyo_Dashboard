using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
namespace DBHelp.Connection
{
    public class SqlServer
    {
        //moulding sqlserver connection
        public static SqlConnection SqlConn_Moulding_Server
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever1"].ToString());
                        break;
                }

                return conn;
            }
        }
        


        //painting sqlserver connection
        public static SqlConnection SqlConn_Painting_Server
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever1"].ToString());
                        break;
                }

                return conn;
            }
        }



        //laser sqlserver connection
        public static SqlConnection SqlConn
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr1"].ToString());
                        break;
                }

                return conn;
            }
        }



        //pqc sqlserver connection
        public static SqlConnection SqlConn_PQC_Server
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever1"].ToString());
                        break;
                }

                return conn;
            }
        }



        //assembly sqlserver connection
        public static SqlConnection SqlConn_Assy_Server
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever1"].ToString());
                        break;
                }

                return conn;
            }
        }



        //office sqlserver connection
        public static SqlConnection SqlConn_Office_Server
        {
            get
            {
                string machineName = System.Environment.MachineName.ToUpper().ToString();
                SqlConnection conn;
                switch (machineName)
                {
                    case "DESKTOP-7DTS7E8":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever1"].ToString());
                        break;
                    case "PC1":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever2"].ToString());
                        break;
                    case "PC2":
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever3"].ToString());
                        break;
                    default:
                        conn = new SqlConnection(ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever1"].ToString());
                        break;
                }

                return conn;
            }
        }



    }
}
