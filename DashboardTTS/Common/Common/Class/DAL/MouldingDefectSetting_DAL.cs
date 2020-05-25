using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
   public  class MouldingDefectSetting_DAL
    {
        public MouldingDefectSetting_DAL()
        {

        }

        public DataSet SelectList()
        {
            string strSql = @" select [defectCodeID]
                              ,[defectCode]
                              ,[defectDescription]
                              ,[partNumber]
                              ,[model]
                              ,[jigNo]
                              ,[machineID]
                              ,[userName]
                              ,[dateTime]
                              ,[remarks] from [MouldingDefectSetting] order by convert(int, defectCodeID ) asc";


            DataSet ds = DBHelp.SqlDB.Query(strSql, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;
        }

    }
}
