using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.DAL
{
    public class LaserMachineDailyReport_DAL
    {

        public DataTable GetWatchDogShiftList(DateTime dDay, string sShift, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
 day
,shift
,machineid
,startTime
,stopTime
,DATEDIFF(second,startTime, stopTime) as totalSeconds
,partNumber
,totalPass
,totalFail
,isnull(setUpQTY,0) as setup
from LMMSWatchDog_Shift 
where 1=1 and day = @day ");

            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and shift = @shift ");
            if (!string.IsNullOrEmpty(sMachineID)) strSql.AppendLine(" and machineID = @machineID ");



            SqlParameter[] paras =
            {
                new SqlParameter("@day",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,8),
                new SqlParameter("@machineID",SqlDbType.VarChar,8)
            };

            paras[0].Value = dDay;
            if (!string.IsNullOrEmpty(sShift)) paras[1].Value = sShift; else paras[1] = null;
            if (!string.IsNullOrEmpty(sMachineID)) paras[2].Value = sMachineID; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }
        
        public DataTable GetMachineStatusList(DateTime dDay, string sShift, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
 day
,shift
,machineid
,startTime
,stopTime
,DATEDIFF(second,startTime, stopTime) as totalSeconds
,partNumber
,totalPass
,totalFail
,isnull(setUpQTY,0) as setup
from LMMSWatchDog_Shift 
where 1=1 and day = @day ");

            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and shift = @shift ");
            if (!string.IsNullOrEmpty(sMachineID)) strSql.AppendLine(" and machineID = @machineID ");



            SqlParameter[] paras =
            {
                new SqlParameter("@day",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,8),
                new SqlParameter("@machineID",SqlDbType.VarChar,8)
            };

            paras[0].Value = dDay;
            if (!string.IsNullOrEmpty(sShift)) paras[1].Value = sShift; else paras[1] = null;
            if (!string.IsNullOrEmpty(sMachineID)) paras[2].Value = sMachineID; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];


        }



    }
}
