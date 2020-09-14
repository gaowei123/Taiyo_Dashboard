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

    }
}
