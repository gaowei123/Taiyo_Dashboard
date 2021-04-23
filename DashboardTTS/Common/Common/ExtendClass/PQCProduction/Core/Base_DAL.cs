using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taiyo.SearchParam.PQCParam;
using Taiyo.Enum.Production;

namespace Common.ExtendClass.PQCProduction.Core
{
    internal class Base_DAL
    {


        internal List<BaseVI_Model> GetCheckViList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select
trackingID,
DAY,
machineID,
shift,
processes,
status,
a.partNumber,
nextViFlag,
jobId,
userID,
startTime,
stopTime,
TotalQty,
acceptQty,
rejectQty,
rejectQty * ISNULL( b.unitCost,1) as LoseAmounts ,
case when b.containLaserFlag = 'true' and
		  a.processes = 'Check#1'
	 then 'Laser' 
	 else 'WIP' 
end as ProductType
from PQCQaViTracking a 
left join (
	select partNumber,
	unitCost,
	case when CHARINDEX('Laser',processes,0) > 1 then 'true' else  'false' end as containLaserFlag,
	case when CHARINDEX('Check#2',processes,0) > 1 then 'Check#2' else 'Check#1' end as lastCheckProcess
	from PQCBom
) b on a.partNumber = b.partNumber
where 1=1 ");

            if (param.DateFrom != null)
                strSql.AppendLine(" and a.day >= @DateFrom ");

            if (param.DateTo != null)
                strSql.AppendLine(" and a.day < @DateTo ");

            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and a.shift = @Shift ");

            if (!string.IsNullOrEmpty(param.OpID))
                strSql.AppendLine(" and a.userID = @UserID");

            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and a.jobId = @JobNo");

            if (!string.IsNullOrEmpty(param.TrackingID))
                strSql.AppendLine(" and a.trackingID = @TrackingID");


            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@JobNo",SqlDbType.VarChar),
                new SqlParameter("@TrackingID",SqlDbType.VarChar)
            };

            if (param.DateFrom != null) parameters[0].Value = param.DateFrom; else parameters[0] = null;
            if (param.DateTo != null) parameters[1].Value = param.DateTo; else parameters[1] = null;        
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.OpID)) parameters[3].Value = param.OpID; else parameters[3] = null;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[4].Value = param.JobNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(param.TrackingID)) parameters[5].Value = param.TrackingID; else parameters[5] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count ==0) return null;
            DataTable dt = ds.Tables[0];


            List<BaseVI_Model> viList = new List<BaseVI_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseVI_Model model = new BaseVI_Model();
                model.TrackingID = dr["trackingID"].ToString();
                model.MachineID = dr["machineID"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.Processes = dr["processes"].ToString();
                model.Status = dr["status"].ToString();
                model.NextViFlag = bool.Parse(dr["nextViFlag"].ToString());
                model.JobNo = dr["jobId"].ToString();
                model.Opertor = dr["userID"].ToString();
                model.Day = DateTime.Parse(dr["DAY"].ToString());
                model.Shift = dr["shift"].ToString();
                model.StartTime = DateTime.Parse(dr["startTime"].ToString());

                if (dr["stopTime"].ToString() == "")
                    model.EndTime = null;
                else
                    model.EndTime = DateTime.Parse(dr["stopTime"].ToString());

                model.TotalQty = decimal.Parse(dr["TotalQty"].ToString()); 
                model.PassQty = decimal.Parse(dr["acceptQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());
    
                model.LoseAmounts = decimal.Parse(dr["LoseAmounts"].ToString());
                model.ProductType = (PQCReportType)Enum.Parse(typeof(PQCReportType), dr["ProductType"].ToString()) ;

                viList.Add(model);
            }

            return viList;
        }


        internal List<BaseVIDetail_Model> GetCheckVIDetailList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"
select 
trackingID
,jobId
,materialName
,materialPartNo
,totalQty
,passQty
,rejectQty
from PQCQaViDetailTracking
where 1=1 ");

            if (param.DateFrom != null)
                strSql.AppendLine(" and day >= @DateFrom ");

            if (param.DateTo != null)
                strSql.AppendLine(" and day < @DateTo ");

            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and shift = @Shift ");

            if (!string.IsNullOrEmpty(param.OpID))
                strSql.AppendLine(" and userID = @UserID");

            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and jobId = @JobNo");

            if (!string.IsNullOrEmpty(param.TrackingID))
                strSql.AppendLine(" and trackingID = @TrackingID");


            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@JobNo",SqlDbType.VarChar),
                new SqlParameter("@TrackingID",SqlDbType.VarChar)
            };

            if (param.DateFrom != null) parameters[0].Value = param.DateFrom; else parameters[0] = null;
            if (param.DateTo != null) parameters[1].Value = param.DateTo; else parameters[1] = null;
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.OpID)) parameters[3].Value = param.OpID; else parameters[3] = null;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[4].Value = param.JobNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(param.TrackingID)) parameters[5].Value = param.TrackingID; else parameters[5] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
            DataTable dt = ds.Tables[0];


            List<BaseVIDetail_Model> viDetailList = new List<BaseVIDetail_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseVIDetail_Model model = new BaseVIDetail_Model();
                model.TrackingID = dr["trackingID"].ToString();          ;
                model.JobNo = dr["jobId"].ToString();
                model.MaterialName = dr["materialName"].ToString();
                model.MaterialPartNo = dr["materialPartNo"].ToString();
                model.TotalQty = decimal.Parse(dr["totalQty"].ToString());
                model.PassQty = decimal.Parse(dr["passQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());



                viDetailList.Add(model);
            }

            return viDetailList;
        }







        internal List<BaseVI_Model> GetPackViList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select
trackingID,
DAY,
machineID,
shift,
processes,
nextViFlag,
status,
a.partNumber,
jobId,
userID,
startTime,
stopTime,
TotalQty,
acceptQty,
rejectQty,
rejectQty * ISNULL(b.unitCost, 1) as LoseAmounts ,
case when b.containLaserFlag = 'true' and
		  b.lastCheckProcess = 'Check#1'
	 then 'PackOnline' 
	 else 'PackOffline' 
end as ProductType
from PQCPackTracking a 
left join (
	select partNumber,
    unitCost,
	case when CHARINDEX('Laser',processes,0) > 1 then 'true' else  'false' end as containLaserFlag,
	case when CHARINDEX('Check#2',processes,0) > 1 then 'Check#2' else 'Check#1' end as lastCheckProcess
	from PQCBom
) b on a.partNumber = b.partNumber
where 1=1 ");


            if (param.DateFrom != null)
                strSql.AppendLine(" and a.day >= @DateFrom ");

            if (param.DateTo != null)
                strSql.AppendLine(" and a.day < @DateTo ");

            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and a.shift = @Shift ");

            if (!string.IsNullOrEmpty(param.OpID))
                strSql.AppendLine(" and a.userID = @UserID");

            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and a.jobId = @JobNo");

            if (!string.IsNullOrEmpty(param.TrackingID))
                strSql.AppendLine(" and a.trackingID = @TrackingID");


            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@JobNo",SqlDbType.VarChar),
                new SqlParameter("@TrackingID",SqlDbType.VarChar)
            };
            if (param.DateFrom != null) parameters[0].Value = param.DateFrom; else parameters[0] = null;
            if (param.DateTo != null) parameters[1].Value = param.DateTo; else parameters[1] = null;
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.OpID)) parameters[3].Value = param.OpID; else parameters[3] = null;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[4].Value = param.JobNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(param.TrackingID)) parameters[5].Value = param.TrackingID; else parameters[5] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
            DataTable dt = ds.Tables[0];


            List<BaseVI_Model> viList = new List<BaseVI_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseVI_Model model = new BaseVI_Model();
                model.TrackingID = dr["trackingID"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.MachineID = dr["machineID"].ToString();
                model.NextViFlag = bool.Parse(dr["nextViFlag"].ToString());
                model.Status = dr["status"].ToString();
                model.JobNo = dr["jobId"].ToString();
                model.Opertor = dr["userID"].ToString();
                model.Processes = dr["processes"].ToString();
                model.Day = DateTime.Parse(dr["DAY"].ToString());
                model.StartTime = DateTime.Parse(dr["startTime"].ToString());
                model.Shift = dr["shift"].ToString();
                if (dr["stopTime"].ToString() == "")
                    model.EndTime = null;
                else
                    model.EndTime = DateTime.Parse(dr["stopTime"].ToString());

                model.TotalQty = decimal.Parse(dr["TotalQty"].ToString());
                model.PassQty = decimal.Parse(dr["acceptQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());
                model.ProductType = (PQCReportType)Enum.Parse(typeof(PQCReportType), dr["ProductType"].ToString());

                viList.Add(model);
            }

            return viList;
        }


        internal List<BasePackDetail_Model> GetPackDetailList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"
select 
trackingID
,jobId
,materialName
,materialPartNo
,totalQty
,passQty
,rejectQty
from PQCQaViDetailTracking
where 1=1 ");

            if (param.DateFrom != null)
                strSql.AppendLine(" and day >= @DateFrom ");

            if (param.DateTo != null)
                strSql.AppendLine(" and day < @DateTo ");

            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and shift = @Shift ");

            if (!string.IsNullOrEmpty(param.OpID))
                strSql.AppendLine(" and userID = @UserID");

            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and jobId = @JobNo");

            if (!string.IsNullOrEmpty(param.TrackingID))
                strSql.AppendLine(" and trackingID = @TrackingID");


            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@JobNo",SqlDbType.VarChar),
                new SqlParameter("@TrackingID",SqlDbType.VarChar)
            };

            if (param.DateFrom != null) parameters[0].Value = param.DateFrom; else parameters[0] = null;
            if (param.DateTo != null) parameters[1].Value = param.DateTo; else parameters[1] = null;
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.OpID)) parameters[3].Value = param.OpID; else parameters[3] = null;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[4].Value = param.JobNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(param.TrackingID)) parameters[5].Value = param.TrackingID; else parameters[5] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
            DataTable dt = ds.Tables[0];


            List<BasePackDetail_Model> packDetailModelList = new List<BasePackDetail_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BasePackDetail_Model model = new BasePackDetail_Model();
                model.TrackingID = dr["trackingID"].ToString(); ;
                model.JobNo = dr["jobId"].ToString();
                model.MaterialName = dr["materialName"].ToString();
                model.MaterialPartNo = dr["materialPartNo"].ToString();
                model.TotalQty = decimal.Parse(dr["totalQty"].ToString());
                model.PassQty = decimal.Parse(dr["passQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());



                packDetailModelList.Add(model);
            }

            return packDetailModelList;
        }



        internal List<BaseDefectSummary_Model> GetDefectList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select 
trackingID
,isnull(sum(case when defectDescription = 'Mould' then isnull(rejectQty,0) end),0) as MouldRej
,isnull(sum(case when defectDescription = 'Paint' then isnull(rejectQty,0) end) ,0) as PaintRej
,isnull(sum(case when defectDescription = 'Laser' then isnull(rejectQty,0) end) ,0) as LaserRej
,isnull(sum(case when defectDescription = 'Others' then isnull(rejectQty,0) end),0)  as OthersRej
from PQCQaViDefectTracking
where 1=1 and day >= @DateFrom and day < @DateTo  ");

            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and shift = @Shift ");

            strSql.AppendLine(" group by trackingID ");


            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar)
            };
            parameters[0].Value = param.DateFrom;
            parameters[1].Value = param.DateTo;
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
            DataTable dt = ds.Tables[0];


            List<BaseDefectSummary_Model> viList = new List<BaseDefectSummary_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseDefectSummary_Model model = new BaseDefectSummary_Model();
                model.TrackingID = dr["trackingID"].ToString();
                //model.JobNo = dr["jobId"].ToString();
                model.MouldRej = decimal.Parse(dr["MouldRej"].ToString());
                model.PaintRej = decimal.Parse(dr["PaintRej"].ToString());
                model.LaserRej = decimal.Parse(dr["LaserRej"].ToString());
                model.OthersRej = decimal.Parse(dr["OthersRej"].ToString());

                viList.Add(model);
            }

            return viList;
        }



        /// <summary>
        /// 获取只有paint delivery记录的lotNo,inQuantity,paintProcess等信息.
        /// </summary>
        /// <param name="param">
        ///  param.starttime:  painting工序在先, 默认延长到3个月前的数据,以防数据找不到.
        /// </param>
        /// <returns></returns>
        internal List<BaseLotInfo_Model> GetLotInfoList(PQCOutputParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"
select 
jobNumber,
lotNo,
partNumber,
dateTime,
inQuantity,
paintProcess
from PaintingDeliveryHis
where 1=1  ");

            if (param.DateFrom != null)
                strSql.AppendLine(" and updatedTime >= @DateFrom ");

            if (param.DateTo != null)
                strSql.AppendLine(" and updatedTime < @DateTo ");
            
            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and jobNumber = @JobNo");
            
            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@JobNo",SqlDbType.VarChar)
            };

            //painting工序在先, 延长到3个月前的数据,以防数据找不到.
            parameters[0].Value = param.DateFrom.Value.AddMonths(-3);
            parameters[1].Value = param.DateTo;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[2].Value = param.JobNo; else parameters[2] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
            DataTable dt = ds.Tables[0];


            Common.Class.BLL.PQCBomDetail_BLL pqcbomBLL = new Class.BLL.PQCBomDetail_BLL();
            Dictionary<string, int> dicMaterialCount = pqcbomBLL.GetMaterialCountList(string.Empty);

            List<BaseLotInfo_Model> lotInfoList = new List<BaseLotInfo_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                BaseLotInfo_Model model = new BaseLotInfo_Model();
                model.JobNo = dr["jobNumber"].ToString();
                model.LotNo = dr["lotNo"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.PaintProcess = dr["paintProcess"].ToString();
                //painting送去print的记录没有mfgdate数据, 置空.
                if (dr["dateTime"].ToString() == string.Empty)
                    model.MFGDate = null;
                else
                    model.MFGDate = DateTime.Parse(dr["dateTime"].ToString());

                //从mrp中获取的inQuantity是set的数量, 转换成pcs
                decimal setQty = decimal.Parse(dr["inQuantity"].ToString());
                int materialCount = dicMaterialCount.ContainsKey(model.PartNo) ? dicMaterialCount[model.PartNo] : 1;
                model.LotQty = setQty * materialCount;

                lotInfoList.Add(model);
            }

            return lotInfoList;
        }



        internal DataTable GetBom()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"
select 
partNumber
,customer
,model
,jigNo
,color
,processes
,case when CHARINDEX('Laser',processes,0) > 0 then 'True' else 'False' end as withLaser
,case when CHARINDEX('Check#3',processes,0) > 0 then 'Check#3'
      when CHARINDEX('Check#2',processes,0) > 0 then 'Check#2'
	  else 'Check#1'
end as lastCheckProcess
,remark_1 as supplier
,shipTo
,coating
,description
,ISNULL(number,'') as num
,ISNULL(unitCost,0) as unitCost
from PQCBom");
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            return ds.Tables[0];
        }

        internal DataTable GetBomDetail()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"
select
partNumber
,materialName
,materialPartNo
,partCount
,outerBoxQty
,packingTrays
,module
from PQCBomDetail ");
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            return ds.Tables[0];
        }


        internal List<BaseBin_Model> GetBinList(PQCOutputParam param)
        {
            9uwafoeih;asklnfdoi

            return new List<BaseBin_Model>();
        }




    }
}
