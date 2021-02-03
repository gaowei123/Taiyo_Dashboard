using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
  public  class MouldingMachineStatus_DAL
    {
        public MouldingMachineStatus_DAL()
        {

        }

        public DataTable GetTodayList()
        {
            string strSql = @"select * from mouldingmachinestatus where convert(varchar, Day,102)  = convert(varchar, DATEADD(Hour, -8, getdate()), 102)";
            
            DataSet ds = DBHelp.SqlDB.Query(strSql, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable GetList(DateTime? dDateTime, DateTime? dDateTo, string sShift, string sMachineID, string sStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT [MachineID]
      ,[Day]
      ,[Shif]
      ,[MachineStatus]
      ,[OEEStatus]
      ,[StartTime]
      ,[EndTime]
      ,[PartNo]
      ,[UserID]
      ,[Remark]
  FROM MouldingMachineStatus where 1=1 ");

            if (dDateTime != null)   strSql.Append(" and day >= @dateFrom ");
            if (dDateTo != null) strSql.Append(" and day < @dateTo ");
            if (sShift != "") strSql.Append(" and shif = @shift");
            if (sMachineID != "") strSql.Append(" and machineID = @machineID");
            if (sStatus != "") strSql.Append(" and machineStatus = @status ");








            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2),
                new SqlParameter("@dateTo", SqlDbType.DateTime2),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@status", SqlDbType.VarChar)
            };

            if (dDateTime != null) paras[0].Value = dDateTime.Value; else paras[0] = null;
            if (dDateTo != null) paras[1].Value = dDateTo.Value; else paras[1] = null;
            if (sShift != "") paras[2].Value = sShift; else paras[2] = null;
            if (sMachineID != "") paras[3].Value = sMachineID; else paras[3] = null;
            if (sStatus != "") paras[4].Value = sStatus; else paras[4] = null;

            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }

        }

    }
}
