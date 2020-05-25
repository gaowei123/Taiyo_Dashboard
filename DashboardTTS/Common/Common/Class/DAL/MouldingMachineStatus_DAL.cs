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

        public DataSet SelectCurrentStatus()
        {
            DateTime CurrentDay = DateTime.Now.AddHours(-8).Date;
            string CurrentShift = DateTime.Now >= CurrentDay.AddHours(8) && DateTime.Now < CurrentDay.AddHours(20) ? StaticRes.Global.Shift.Day : StaticRes.Global.Shift.Night;

            string strSql = " Select * from MouldingMachineStatus where  Day = @Day and Shif = @Shift  ";

            SqlParameter[] paras =
            {
                new SqlParameter("@Day", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar,16)
            };

            paras[0].Value = CurrentDay;
            paras[1].Value = CurrentShift;

            return DBHelp.SqlDB.Query(strSql, paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        
        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select [MachineID],[Day],[Shif],[MachineStatus],[OEEStatus], convert(smalldatetime, [StartTime]) as StartTime, convert(smalldatetime, ISNULL( [EndTime],GETDATE()) )as EndTime,[PartNo],[UserID],[Remark] from MouldingMachineStatus  where 1=1  ");

            //strSql.Append("  select [MachineID],[Day],[Shif],[MachineStatus],[OEEStatus],convert(varchar(100), [StartTime],20) as [StartTime] , convert(varchar(100), ISNULL( [EndTime],GETDATE()),20 )as [EndTime] ,[PartNo],[UserID],[Remark] from MouldingMachineStatus  where 1=1  ");

            strSql.Append("  and Day >= @datefrom ");
            strSql.Append("  and Day <= @dateto ");
            strSql.Append("  and OEEStatus != '' ");

            if (sShift != "" && sShift.ToUpper() != "ALL")
            {
                strSql.Append(@"  and Shif = @shift ");
            }

            if (sMachineID != "" && sMachineID != "All")
            {
                strSql.Append(@"  and MachineID = @machineID ");
            }

            strSql.Append(@"   and OEEStatus !='' ");

            strSql.Append(@"   order by StartTime asc,machineid  asc   ");



            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sShift != "" && sShift != "All")
            {
                paras[2].Value = sShift;
            }
            else
            {
                paras[2] = null;
            }

            if (sMachineID != "" && sMachineID != "All")
            {
                paras[3].Value = sMachineID;
            }
            else
            {
                paras[3] = null;
            }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }
        
        public DataSet SelectCurrentStatus(DateTime CurrentDay,string CurrentShift,string CurrentMachineID)
        {
            
            string strSql = " Select MachineStatus from MouldingMachineStatus where  Day = @Day and Shif = @Shift  and MachineID = @MachineID order by StartTime desc";

            SqlParameter[] paras =
            {
                new SqlParameter("@Day", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar,16),

                new SqlParameter("@MachineID", SqlDbType.VarChar,16)
            };

            paras[0].Value = CurrentDay;
            paras[1].Value = CurrentShift;
            paras[2].Value = CurrentMachineID;

            return DBHelp.SqlDB.Query(strSql, paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
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
