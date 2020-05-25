using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingPqmHistory_DAL
    {
   
        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT a.machineID,a.updatedTime,a.totalQTY,a.tempature11,a.tempature12 ,a.tempature13 ,a.tempature14
                            ,a.tempature15,a.tempature21,a.tempature22,a.tempature23,a.tempature24,a.tempature25

                            ,isnull(b.refField03,0) as Setting11
                            ,isnull(b.refField04,0) as Setting12
                            ,isnull(b.refField05,0) as Setting13
                            ,isnull(b.refField06,0) as Setting14
                            ,isnull(b.refField07,0) as Setting15
                            ,isnull(b.refField08,0) as Setting21
                            ,isnull(b.refField09,0) as Setting22
                            ,isnull(b.refField10,0) as Setting23
                            ,isnull(b.refField11,0) as Setting24
                            ,isnull(b.refField12,0) as Setting25

                            FROM MouldingPqmHistory a

                            left join MouldingPqm b on a.machineID = b.machineID  ");

            strSql.Append(" where 1=1 ");

            strSql.Append(" and a.updatedTime >= @dateFrom ");
            strSql.Append(" and a.updatedTime < @dateTo ");

            strSql.Append(" and a.MachineID = @machineid ");


            strSql.Append("  order by a.updatedTime asc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@machineid",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom.Date;
            paras[1].Value = dDateTo.AddDays(1);
            paras[2].Value = "MC0"+ sMachineID;


            return DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }




    }
}
