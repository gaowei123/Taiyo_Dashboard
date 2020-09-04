using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.DAL
{
    public class OverallOutputChart_DAL
    {


        /// <summary>
        /// 获取moulding的output
        /// 包含 total, pass, rej, ipqc, setup数量.
        /// </summary>
        public DataTable GetMouldOutput(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
SUM(acountReading) as TotalQty
,SUM(rejectQty) as RejQty
,SUM(acceptQty) as PassQty
,SUM(isnull(setup, 0)) as MouldSetup
,SUM(isnull(QCNGQTY,0)) as IPQCRej
from MouldingViTracking
where day >= @dateFrom and day<@dateTo ");

            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and shift = @shift");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) paras[2].Value = sShift; else paras[2] = null;
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }





        /// <summary>
        /// 获取Painting delivery的output
        /// 包含 total 数量,  该数量为SET数量
        /// </summary>
        public DataTable GetPaintOutput(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SUM(inQuantity) as TotalQty from PaintingDeliveryHis
where updatedTime >= @dateFrom and updatedTime < @dateTo  ");

            if (sShift == StaticRes.Global.Shift.Day)
            {
                strSql.Append("and (DATENAME(HOUR, updatedTime) >= 8 and DATENAME(HOUR,updatedTime) < 20)  ");
            }
            else if (sShift == StaticRes.Global.Shift.Night)
            {
                strSql.Append("and ! (DATENAME(HOUR, updatedTime) >= 8 and DATENAME(HOUR,updatedTime) < 20)  ");
            }
       



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = dDateFrom.Date.AddHours(8);
            paras[1].Value = dDateTo.Date.AddHours(8);


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }




        /// <summary>
        /// 获取 painting temp info中的 setup, qa 数量
        /// </summary>
        public DataTable GetPaintQASetup(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
SUM(isnull(setupRejQty,0)) as PaintSetupRej
,SUM(isnull(qaTestQty,0)) as PaintQARej
from paintingtempinfo 
where createdTime >= @dateFrom and createdTime < @dateTo ");

            if (sShift == StaticRes.Global.Shift.Day)
            {
                strSql.Append("and (DATENAME(HOUR, createdTime) >= 8 and DATENAME(HOUR,createdTime) < 20)  ");
            }
            else if (sShift == StaticRes.Global.Shift.Night)
            {
                strSql.Append("and ! (DATENAME(HOUR, createdTime) >= 8 and DATENAME(HOUR,createdTime) < 20)  ");
            }




            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = dDateFrom.Date.AddHours(8);
            paras[1].Value = dDateTo.Date.AddHours(8);


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }




        /// <summary>
        /// 获取laser的output
        /// 包含 totalQuantity, totalPass, totalFail
        /// </summary>
        public DataTable GetLaserOutput(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
 SUM(totalQuantity) as TotalQty
,SUM(totalPass) as PassQty
,SUM(totalFail) as RejQty
from LMMSWatchDog_Shift
where day >= @dateFrom and day < @dateTo ");

            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and shift = @shift ");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) paras[2].Value = sShift; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),paras);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }



        public DataTable GetLaserSetupBuyoffShortage(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
 SUM(a.Buyoff	 ) as LaserBuyoff
,SUM(a.Setup	 ) as LaserSetup
,SUM(a.Shortage	 ) as LaserShortage
from 
(
	select 
	 ISNULL(setUpQTY  ,0) as Setup
	,ISNULL(buyOffQty ,0) as Buyoff
	,ISNULL(shortage  ,0) as Shortage
	,Day
	,Shift
	from LMMSWatchDog_Shift where day >'2020-8-1'

	union all

	select 
	 ISNULL(pqcQuantity ,0) as Setup
	,ISNULL(setUpQTY	,0) as Buyoff
	,ISNULL(buyOffQty	,0) as Shortage
	,datetime as Day
	,case when DATENAME(HOUR, datetime) >= 8 and DATENAME(HOUR, datetime) < 20 then 'Day' else 'Night' end as Shift
	from LMMSInventory where datetime < '2020-8-1'
) a  
where a.day >=@dateFrom and a.day < @dateTo ");


            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and a.shift = @shift ");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) paras[2].Value = sShift; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }




        /// <summary>
        /// checking output
        /// 包含TotalQty, acceptQty, tts, vendor, paint, laser, others rej
        /// </summary>
        public DataTable GetCheckingOutput(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            string strSearching = string.IsNullOrEmpty(sShift) ? "" : " and a.shift = @shift ";

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
select 
    SUM(a.TotalQty) as TotalQty
    ,SUM(a.acceptQty) as PassQty
    ,SUM(b.TTSMouldRej) as TTSMouldRej
    ,SUM(b.VendorMouldRej) as VendorMouldRej
    ,SUM(b.PaintRej) as PaintRej
    ,SUM(b.LaserRej) as LaserRej
    ,SUM(b.OthersRej) as OthersRej
from PQCQaViTracking a
left join (
	select 
	    a.trackingID 
	    ,ISNULL(SUM( case when a.defectDescription = 'Mould' and c.remark_1 = 'TTS' then a.rejectQty  end),0) as VendorMouldRej
	    ,ISNULL(SUM(case when a.defectDescription = 'Mould' and c.remark_1 != 'TTS' then a.rejectQty  end),0) as TTSMouldRej
	    ,SUM(case when a.defectDescription = 'Paint' then a.rejectQty  end) as PaintRej
	    ,SUM(case when a.defectDescription = 'Laser' then a.rejectQty  end) as LaserRej
	    ,SUM(case when a.defectDescription = 'Others' then a.rejectQty  end) as OthersRej
	from PQCQaViDefectTracking a
	left join PQCQaViTracking b on a.trackingID = b.trackingID
	left join PQCBom c on b.partNumber = c.partNumber
	where a.day >= '2020-1-1' and a.day < '2021-1-1' {0}
	group by a.trackingID
) b on a.trackingID = b.trackingID
where a.day >= '2020-1-1' and a.day < '2021-1-1' {1}", strSearching, strSearching);



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) paras[2].Value = sShift; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }




        /// <summary>
        /// 获取packing的output
        /// 包含 total qty, pass qty, rej qty.
        /// </summary>
        public DataTable GetPackingOutput(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
 SUM(totalQty	) as TotalQty
,SUM(acceptQty	) as PassQty
,SUM(rejectQty	) as RejQty
from PQCPackTracking
where day >= @dateFrom and day < @dateTo ");

            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and shift = @shift");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) paras[2].Value = sShift; else paras[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);


            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }


    }
}
