using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
   public class LMMSWatchDog_DAL
    {

        public DataTable GetList(string sJobNo, string sMachineID )
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select * from LMMSWatchDog where 1=1 ");

            if (!string.IsNullOrEmpty(sJobNo) ) strSql.Append(" and jobnumber = @jobnumber  ");
            if (!string.IsNullOrEmpty(sMachineID)) strSql.Append(" and machineID = @machineID  ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobnumber", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar)
            };



            if (!string.IsNullOrEmpty(sJobNo)) parameters[0].Value = sJobNo; else parameters[0] = null;
            if (!string.IsNullOrEmpty(sMachineID)) parameters[1].Value = sMachineID; else parameters[1] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds == null || ds.Tables.Count == 0? null : ds.Tables[0];
        }




        internal DataTable GetMaterialList(DateTime? dStartTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
a.partNumber, a.jobNumber, a.materialNo,
sum(a.ok) as ok, sum(a.ng) as ng
from(
	select day, partNumber,jobNumber, model1Name  as materialNo, isnull(ok1count,0)  as ok, isnull(ng1count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model2Name  as materialNo, isnull(ok2count,0)  as ok, isnull(ng2count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model3Name  as materialNo, isnull(ok3count,0)  as ok, isnull(ng3count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model4Name  as materialNo, isnull(ok4count,0)  as ok, isnull(ng4count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model5Name  as materialNo, isnull(ok5count,0)  as ok, isnull(ng5count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model6Name  as materialNo, isnull(ok6count,0)  as ok, isnull(ng6count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model7Name  as materialNo, isnull(ok7count,0)  as ok, isnull(ng7count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model8Name  as materialNo, isnull(ok8count,0)  as ok, isnull(ng8count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model9Name  as materialNo, isnull(ok9count,0)  as ok, isnull(ng9count, 0)  as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model10Name as materialNo, isnull(ok10count,0) as ok, isnull(ng10count, 0) as ng from LMMSWatchDog_Shift  union all
	select day, partNumber,jobNumber, model11Name as materialNo, isnull(ok11count,0) as ok, isnull(ng11count, 0) as ng from LMMSWatchDog_Shift 
) a 
where a.ok + a.ng > 0
and a.day >= @startTime 
group by a.partNumber, a.jobNumber, a.materialNo ");



            SqlParameter[] parameters = {
                new SqlParameter("@startTime", SqlDbType.VarChar)
            };
            if (dStartTime == null) parameters[0] = null; else parameters[0].Value = dStartTime;
       
           

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }









    }
}
