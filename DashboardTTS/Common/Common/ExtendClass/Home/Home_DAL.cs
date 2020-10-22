using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.Home
{
    internal class Home_DAL
    {

        public DataTable GetMouldingDaily(Common.SearchingCondition.BaseCondition cond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select day,sum(acountReading) as output from MouldingViTracking where 1=1 ");
            strSql.Append(" and day >= @DateFrom ");
            strSql.Append(" and day < @DateTo ");
            strSql.Append(" group by day  order by day asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = cond.DateFrom.Value.Date;
            paras[1].Value = cond.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetLaserDaily(Common.SearchingCondition.BaseCondition cond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select day,sum(totalquantity) as output from LMMSWatchDog_Shift where 1=1 ");
            strSql.Append(" and day >= @DateFrom ");
            strSql.Append(" and day < @DateTo ");
            strSql.Append(" group by day  order by day asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = cond.DateFrom.Value.Date;
            paras[1].Value = cond.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPQCOnlineDaily(Common.SearchingCondition.BaseCondition cond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select day,sum(TotalQty) as output from PQCQaViTracking a left join PQCBom b on a.partNumber = b.partNumber where 1=1 ");
            strSql.Append(" and CHARINDEX('Laser',b.processes,0) > 0 and a.processes = 'CHECK#1' ");
            strSql.Append(" and day >= @DateFrom ");
            strSql.Append(" and day < @DateTo ");
            strSql.Append(" group by day  order by day asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = cond.DateFrom.Value.Date;
            paras[1].Value = cond.DateTo.Value.Date;

            DataSet ds =  DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable  GetPQCWIPDaily(Common.SearchingCondition.BaseCondition cond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select day,sum(TotalQty) as output from PQCQaViTracking a left join PQCBom b on a.partNumber = b.partNumber where 1=1 ");
            strSql.Append(" and (CHARINDEX('Laser',b.processes,0) = 0 or  (CHARINDEX('Laser',b.processes,0) > 0 and  a.processes != 'CHECK#1') ) ");
            strSql.Append(" and day >= @DateFrom ");
            strSql.Append(" and day < @DateTo ");
            strSql.Append(" group by day  order by day asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = cond.DateFrom.Value.Date;
            paras[1].Value = cond.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPQCPackingDaily(Common.SearchingCondition.BaseCondition cond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select day,sum(TotalQty) as output from PQCPackTracking where 1=1 ");
            strSql.Append(" and day >= @DateFrom ");
            strSql.Append(" and day < @DateTo ");
            strSql.Append(" group by day  order by day asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = cond.DateFrom.Value.Date;
            paras[1].Value = cond.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

       

      





    }
}
