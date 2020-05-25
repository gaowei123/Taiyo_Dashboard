using System;
using System.Collections.Generic;
using System.Text;

namespace DBHelp.Connection
{
    public class Oracle
    {
        public static System.Data.OracleClient.OracleConnection OracleConn
        {
            get
            {
                return new System.Data.OracleClient.OracleConnection(System.Configuration.ConfigurationManager.AppSettings["Oracle_ConnStr"].ToString());
                    //"Data Source=;User ID=;Password=;Unicode=True");
            }
        }
        public static System.Data.OracleClient.OracleConnection OracleConn_OutSource1
        {
            get
            {
                //本地配置  
                return new System.Data.OracleClient.OracleConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_BOM_ConnStr1"].ToString());
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456"); 
            }

        }
        public static System.Data.OracleClient.OracleConnection OracleConn_OutSource2
        {
            get
            {
                //本地配置  
                return new System.Data.OracleClient.OracleConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_BOM_ConnStr2"].ToString());
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }

        public static System.Data.OracleClient.OracleConnection OracleConn_OutSource3
        {
            get
            {
                //本地配置  
                return new System.Data.OracleClient.OracleConnection(System.Configuration.ConfigurationManager.AppSettings["SQL_COM_ConnStr1"].ToString());
                //@"Data Source=WLJ\UBCTDB;Initial Catalog=PXXX-VISHAY-LMMS;User ID=sa;Password=Ab123456");  
            }

        }
    }
}
