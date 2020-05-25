using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
namespace DBHelp.Connection
{
   public  class OleDb
    {
       public static OleDbConnection OleDbConn
       {
           get
           {
               
               return new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["OleDB_ConnStr"].ToString());
           
           }
       }

       
    }
}
