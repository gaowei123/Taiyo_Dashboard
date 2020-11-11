
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:LMMSEventLog_DAL
	/// </summary>
	internal class LMMSEventLog_DAL
	{
        public DataSet getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" select * from (
                            select id,dateTime,machineID,currentoperation,ownerID,eventtrigger,startTime,stopTime,ipSetting
                            ,currentoperation +'_'+eventtrigger as currentoperation_eventtrigger
                            from lmmseventlog ");
            strSql.Append(" ) a  where a.currentoperation_eventtrigger not in ('TECHNICIAN_OEE_POWER ON', 'TECHNICIAN_OEE_POWER OFF')");

            //2018 11 13 by wei lijia for eventlog case
            strSql.Append(@"  and  (select count(x.id) from [dbo].[LMMSWatchDog_Shift] x where  a.machineID = x.machineID  
                             and a.startTime < x.prepDateIn and a.stopTime > x.prepDateIn 
                             and a.eventTrigger <> 'POWER ON'
                              ) = 0  ");


            if (sMachineNo != "")
            {
                strSql.Append("  and MACHINEID = @MachineNo  ");
            }
            
            strSql.Append(" and dateTime >= @TimeFrom  ");
            strSql.Append(" and dateTime <= @TimeTo + 2   ");

            //operation status 排除 techion OEE power on/off
            //strSql.Append(" and dateTime <= @TimeTo + 1   ");

            // strSql.Append(" and currentOperation like '%oee'  ");
            strSql.Append(" ORDER BY starttime  DESC  ");
            SqlParameter[] parameters = {
                        new SqlParameter("@MachineNo", SqlDbType.VarChar,4) ,
                        new SqlParameter("@TimeFrom", SqlDbType.DateTime) ,
                        new SqlParameter("@TimeTo", SqlDbType.DateTime) };
            parameters[0].Value = sMachineNo;
            parameters[1].Value = dTimeFrom;
            parameters[2].Value = dTimeTo;


            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSEventLog_DAL", "Function:		public Common.Model.LMMSEventLog_Model getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", "");
           // Common.Model.LMMSEventLog_Model model = new Common.Model.LMMSEventLog_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds;
        }

        public DataTable GetTimeByStatus(DateTime dDateFrom, DateTime dDateTo, string sMachineID, params string[] sStatus)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select eventTrigger,starttime,stopTime from LMMSEventLog  ");
            strSql.Append(" where currentOperation = 'TECHNICIAN_OEE' ");
            strSql.Append(" and machineID = @machineID ");
            strSql.Append(" and startTime >= @dDateFrom ");
            strSql.Append(" and stopTime <= @dDateTo ");

            strSql.Append(" and eventTrigger in ( ");

            string strStatus = "";
            foreach (string  status in sStatus)
            {
                strStatus += "'" + status + "',";
            }


            strSql.Append(strStatus.Substring(0,strStatus.Length-1));

            strSql.Append(")");


            SqlParameter[] paras =
            {
                new SqlParameter("@machineID",SqlDbType.VarChar,16),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime)
            };

            paras[0].Value = sMachineID;
            paras[1].Value = dDateFrom;
            paras[2].Value = dDateTo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }




        


        public DataTable GetListForModelList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select id
,machineID
,currentOperation
,case when eventTrigger = 'ADJUSTMENT' then 'BUYOFF' 
when eventTrigger = 'POWER ON' then 'RUN'
when eventTrigger = 'POWER OFF' then 'SHUTDOWN'
else eventTrigger end as eventTrigger
,startTime
,stopTime
from LMMSEventLog 
where (currentOperation = 'TECHNICIAN_OEE' or currentOperation = 'SYSTEM_OEE') and eventTrigger != 'POWER ON'
and datetime >= @DateFrom and datetime <= @DateTo");

            if (sMachineID != "")
            {
                strSql.Append(" and machineID = @machineID ");
            }

            if (sStatus != "" && sStatus != "RUNNING" && sStatus != "UTILIZATION")
            {
                strSql.Append(" and eventTrigger = @status ");
            }

            

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@machineID", SqlDbType.VarChar,8),
                new SqlParameter("@status", SqlDbType.VarChar,32)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;
            if (sStatus != "") paras[3].Value = sStatus; else paras[3] = null;
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetCurrentStatusList(string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select id
,machineID
,currentOperation
,case when eventTrigger = 'ADJUSTMENT' then 'BUYOFF' 
when eventTrigger = 'POWER ON' then 'RUN'
when eventTrigger = 'POWER OFF' then 'SHUTDOWN'
else eventTrigger end as eventTrigger
,startTime
,stopTime
from LMMSEventLog 
where (currentOperation = 'TECHNICIAN_OEE' or currentOperation = 'SYSTEM_OEE')
and datetime >= @DateFrom and datetime <= @DateTo");

            if (sMachineID != "")
            {
                strSql.Append(" and machineID = @machineID ");
            }

          



            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@machineID", SqlDbType.VarChar,8)
            };

            paras[0].Value = DateTime.Now.Date;
            paras[1].Value = DateTime.Now.Date.AddDays(1);
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        
    }
}

