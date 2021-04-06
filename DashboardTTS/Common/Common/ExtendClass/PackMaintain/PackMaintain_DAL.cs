using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PackMaintain
{
    public class PackMaintain_DAL
    {
        //用来获取显示MRP Qty
        internal DataTable GetPaintDelivery(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
jobNumber
, lotNo
, partNumber
, convert(int, inQuantity) as MrpQty
from PaintingDeliveryHis
where 1=1 ");
            strSql.Append("and jobNumber = @jobNo ");

            
            SqlParameter[] parameters =
            {
               new SqlParameter("@jobNo",SqlDbType.VarChar,32)
            };
            parameters[0].Value = sJobNo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0];
        }

        internal PackMaintain_Model.JobInfo GetJobInfo(string sTrackingID)
        {
            var model = new PackMaintain_Model.JobInfo();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
jobId
,trackingID
,day
,shift
,partNumber
from PQCPackTracking
where trackingID = @trackingID ");

            SqlParameter[] parameters =
            {
               new SqlParameter("@trackingID",SqlDbType.VarChar,64)
            };
            parameters[0].Value = sTrackingID;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            model.JobNo = ds.Tables[0].Rows[0]["jobId"].ToString();
            model.TrackingID = ds.Tables[0].Rows[0]["trackingID"].ToString();
            model.Day = DateTime.Parse(ds.Tables[0].Rows[0]["day"].ToString());
            model.Shift = ds.Tables[0].Rows[0]["shift"].ToString();
            model.PartNo = ds.Tables[0].Rows[0]["partNumber"].ToString();

            return model;
        }


        //获取该trackingID, 最后一道check工序的记录
        internal DataTable GetMaterialBinQty(string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
materialName
,materialPartNo
,materialQty as Qty
from PQCQaViBinning a 
left join (
	select
	partnumber
	,processes
	,CASE 
        WHEN CHARINDEX('CHECK#3',processes,0)>1 THEN 'CHECK#3'
        WHEN CHARINDEX('CHECK#2',processes,0)>1 THEN 'CHECK#2'
        ELSE 'CHECK#1' 
        END as lastedCheckProsses
	from pqcbom
) b on a.PartNumber = b.partNumber
where 1 = 1 and a.processes = b.lastedCheckProsses
and trackingID = @trackingID ");

            SqlParameter[] parameters =
            {  
               new SqlParameter("@trackingID",SqlDbType.VarChar,64)
            };
            parameters[0].Value = sTrackingID;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            
            return ds.Tables[0];
        }

        internal DataTable GetMaterialPackQty(string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
b.materialName
,b.materialPartNo
,a.passQty as Qty
from PQCPackDetailTracking a
left join PQCBomDetail b on a.materialPartNo = b.materialPartNo
where 1=1 and trackingID=  @trackingID ");

            SqlParameter[] parameters =
            {
               new SqlParameter("@trackingID",SqlDbType.VarChar,64)
            };
            parameters[0].Value = sTrackingID;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0];
        }
    }
}
