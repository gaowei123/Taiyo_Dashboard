﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Common.ExtendClass.Home
{
    internal class Home_DAL
    {

        public DataTable GetMouldingDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            year(day) as year,
                            month(day) as month,
                            day(day) as day,
                            sum(acountReading) as output 
                            from MouldingViTracking 
                            where 1=1 
                            and day >= @DateFrom
                            and day < @DateTo
                            group by year(day), month(day), day(day)  
                            order by year(day) asc, month(day) asc, day(day) asc");



            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPaintingDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            YEAR(updatedTime) as [year],
                            MONTH(updatedTime) as [month],
                            DAY(updatedTime) as [day],
                            SUM(inQuantity * case when isnull(b.materialCount, '') = '' then 1 else b.materialCount end) as output
                            FROM PaintingDeliveryHis a
                            left join ( 
	                            select partNumber, count(1) as materialCount 
	                            from  LMMS_TAIYO.dbo.lmmsbomDetail 
	                            group by partNumber 
                            ) b on a.partNumber  = b.partNumber

                            where 1 = 1   and updatedTime >= @DateFrom  and updatedTime <= @DateTo
                            group by YEAR(updatedTime), MONTH(updatedTime),  DAY(updatedTime)
                            order by YEAR(updatedTime) asc, MONTH(updatedTime) asc, DAY(updatedTime) asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetLaserDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            year(day) as year,
                            month(day) as month,
                            day(day) as day,
                            sum(totalPass + totalFail) as output 
                            from LMMSWatchDog_Shift 
                            where 1=1 
                            and day >= @DateFrom
                            and day < @DateTo
                            group by year(day), month(day), day(day)  
                            order by year(day) asc, month(day) asc, day(day) asc");            
          
            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPQCOnlineDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            year(day) as year,
                            month(day) as month,
                            day(day) as day,
                            sum(acceptQty + RejectQty) as output 
                            from PQCQaViTracking a 
                            left join PQCBom b on a.partNumber = b.partNumber 
                            where 1=1
                            and CHARINDEX('Laser',b.processes,0) > 0 and a.processes = 'CHECK#1'
                            and day >= @DateFrom 
                            and day < @DateTo
                            group by year(day), month(a.day), day(a.day)  
                            order by year(day) asc, month(a.day) asc , day(a.day)  asc");

       

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds =  DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable  GetPQCWIPDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            year(day) as year,
                            month(day) as month,
                            day(day) as day,
                            sum(acceptQty + RejectQty) as output
                            from PQCQaViTracking a
                            left
                            join PQCBom b on a.partNumber = b.partNumber
                            where 1 = 1
                            and(CHARINDEX('Laser', b.processes, 0) = 0 or(CHARINDEX('Laser', b.processes, 0) > 0 and  a.processes != 'CHECK#1'))
                            and day >= @DateFrom
                            and day < @DateTo
                            group by year(day), month(a.day), day(a.day)
                            order by year(day) asc, month(a.day) asc, day(a.day)  asc");
            

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };



            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPQCPackingDaily(Taiyo.SearchParam.HomeParam param)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select 
                            year(day) as year,
                            month(day) as month,
                            day(day) as day,
                            sum(acceptQty + RejectQty) as output
                            from PQCPackTracking 
                            where 1=1
                            and day >= @DateFrom
                            and day < @DateTo
                            group by year(day), month(day), day(day)
                            order by year(day) asc, month(day) asc, day(day)  asc");
         

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date;
            paras[1].Value = param.DateTo.Value.Date;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

       
    }
}
