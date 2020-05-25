using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace DBHelp.Connection
{
    public class SqlServer
    {
        public static System.Data.SqlClient.SqlConnection SqlConn
        {
            get
            {
                //本地配置 
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")//Dwyane
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "YK-PC")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr4"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr3"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr"].ToString());
                }
               
              

            }
        }
        public static System.Data.SqlClient.SqlConnection SqlConn_Moulding_Server
        {
            get
            {
                //本地配置  
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever3"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_Sever"].ToString());
                }
               
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }
        public static System.Data.SqlClient.SqlConnection SqlConn_Moulding_PtrSql
        {
            get
            {
                //本地配置  
                return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Moulding_PtrSql"].ToString());
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }

        public static System.Data.SqlClient.SqlConnection SqlConn_Painting_Server
        {
            get
            {
                //本地配置  
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever3"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Painting_Sever"].ToString());
                }

                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }
        public static System.Data.SqlClient.SqlConnection SqlConn_PQC_Server
        {
            get
            {
                //本地配置  
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever3"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "YK-PC")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever4"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_PQC_Sever"].ToString());
                }

                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }
        public static System.Data.SqlClient.SqlConnection SqlConn_Assy_Server
        {
            get
            {
                //本地配置  
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever3"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Assy_Sever"].ToString());
                }

                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }


        public static System.Data.SqlClient.SqlConnection SqlConn_Office_Server
        {
            get
            {
                //本地配置  
                if (System.Environment.MachineName.ToUpper().ToString() == "WORKING")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever2"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "DWYANE")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever1"].ToString());
                }
                if (System.Environment.MachineName.ToUpper().ToString() == "WIN-MNJNRCKBALL")
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever3"].ToString());
                }
                else
                {
                    return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_ConnStr_Office_Sever"].ToString());
                }

                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }


        public static System.Data.SqlClient.SqlConnection SqlConn_OutSource3
        {
            get
            {
                //本地配置  
                return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_BOM_ConnStr3"].ToString());
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }

        

    }
}
