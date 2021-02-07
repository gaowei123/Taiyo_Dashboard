using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taiyo.SearchParam;
using Taiyo.Enum.Organization;

namespace Common.ExtendClass.OverallOutputChart
{
    internal class OverallOutputChart_DAL
    {
        //2021-2-3, 再次重写, 草泥马
        //┻━┻︵╰(‵□′)╯︵┻━┻
        //┻━┻︵╰(‵□′)╯︵┻━┻
        //┻━┻︵╰(‵□′)╯︵┻━┻


        /// <summary>
        /// 获取Moulding的totalqty, rejqty
        /// ** 其中Mould_Testing & Material_Testing不属于output.
        /// </summary>
        public OverallOutputChart_Model GetMouldOutput(BaseParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
SUM(acountReading) as TotalQty
,SUM(rejectQty) as RejQty
from MouldingViTracking
where 1=1 
and status != 'Mould_Testing' and status != 'Material_Testing'
and day >= @dateFrom and day<@dateTo ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };
            paras[0].Value = param.DateFrom;
            paras[1].Value = param.DateTo;
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ConvertModel(ds.Tables[0], Department.Moulding.ToString());
        }


        /// <summary>
        /// 获取Painting的totalqty, rejqty
        /// totalqty是set的数量, 关联pqc bom detail根据material数量来计算出 pcs的数量.
        /// </summary>
        public OverallOutputChart_Model GetPaintOutput(BaseParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"select 
SUM(a.inQuantity * ISNULL(b.materialCount,1)) as TotalQty,
SUM(ISNULL(a.PaintRejQty,0)) as RejQty
from PaintingDeliveryHis a
left join (
	select 
	partNumber,
	count(1) as materialCount 
	from opendatasource('SQLOLEDB',{StaticRes.Global.SqlConnection.SqlconnPQC}).Taiyo_PQC.dbo.PQCBomDetail
	group by partNumber
) b on a.partNumber COLLATE Chinese_PRC_CI_AS = b.partNumber COLLATE Chinese_PRC_CI_AS
where 1=1 
and updatedTime >= @dateFrom 
and updatedTime < @dateTo  ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom.Value.Date.AddHours(8);
            paras[1].Value = param.DateTo.Value.Date.AddHours(8);


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);

            return ConvertModel(ds.Tables[0], Department.Painting.ToString());
        }


        /// <summary>
        /// 获取Laser的totalqty, rejqty
        /// </summary>
        public OverallOutputChart_Model GetLaserOutput(BaseParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
SUM(ISNULL(totalpass,0) + ISNULL(totalfail,0)) as TotalQty
,SUM(totalFail) as RejQty
from LMMSWatchDog_Shift
where day >= @dateFrom and day < @dateTo ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom;
            paras[1].Value = param.DateTo;

            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            return ConvertModel(ds.Tables[0], Department.Laser.ToString());
        }


        /// <summary>
        /// 获取PQC Check的 online和wip的totalqty, rejqty
        /// </summary>
        public List<OverallOutputChart_Model> GetCheckingOutput(BaseParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select
TotalQty,
rejectQty as RejQty,
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
where 1=1 
and day >= @dateFrom 
and day < @dateTo");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom;
            paras[1].Value = param.DateTo;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            OverallOutputChart_Model laserModel;
            OverallOutputChart_Model wipModel;

            if (ds == null || ds.Tables.Count== 0 || ds.Tables[0].Rows.Count == 0)
            {
                laserModel = new OverallOutputChart_Model() { Department = "PQC Online", TotalQty = 0, RejQty = 0, RejRate = "0.00%" };
                wipModel = new OverallOutputChart_Model() { Department = "PQC WIP", TotalQty = 0, RejQty = 0, RejRate = "0.00%" };
            }
            else
            {
                laserModel = new OverallOutputChart_Model();
                laserModel.Department = "PQC Online";
                laserModel.TotalQty = decimal.Parse(ds.Tables[0].Compute("SUM(TotalQty)", "ProductType = 'Laser'").ToString());
                laserModel.RejQty = decimal.Parse(ds.Tables[0].Compute("SUM(RejQty)", "ProductType = 'Laser'").ToString());
                laserModel.RejRate = laserModel.TotalQty == 0 ? "0.00%" : Math.Round(laserModel.RejQty / laserModel.TotalQty * 100, 2).ToString("0.00") + "%";

                wipModel = new OverallOutputChart_Model();
                wipModel.Department = "PQC WIP";
                wipModel.TotalQty = decimal.Parse(ds.Tables[0].Compute("SUM(TotalQty)", "ProductType = 'WIP'").ToString());
                wipModel.RejQty = decimal.Parse(ds.Tables[0].Compute("SUM(RejQty)", "ProductType = 'WIP'").ToString());
                wipModel.RejRate = wipModel.TotalQty == 0 ? "0.00%" : Math.Round(wipModel.RejQty / wipModel.TotalQty * 100, 2).ToString("0.00") + "%";
            }
         

            return new List<OverallOutputChart_Model>() { laserModel, wipModel };
        }


        /// <summary>
        /// 获取PQC Pack的 online和offline的totalqty, rejqty
        /// </summary>
        public List<OverallOutputChart_Model> GetPackingOutput(BaseParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
TotalQty,
rejectQty as RejQty,
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
where 1=1 
and a.day >= @dateFrom 
and a.day < @dateTo ");

            

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };

            paras[0].Value = param.DateFrom;
            paras[1].Value = param.DateTo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            OverallOutputChart_Model onlineModel;
            OverallOutputChart_Model offlineModel;
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                onlineModel = new OverallOutputChart_Model() { Department = "Pack Online", TotalQty = 0, RejQty = 0, RejRate = "0.00%" };
                offlineModel = new OverallOutputChart_Model() { Department = "Pack Offline", TotalQty = 0, RejQty = 0, RejRate = "0.00%" };
            }
            else
            {
                onlineModel = new OverallOutputChart_Model();
                onlineModel.Department = "Pack Online";
                onlineModel.TotalQty = decimal.Parse(ds.Tables[0].Compute("SUM(TotalQty)", "ProductType = 'PackOnline'").ToString());
                onlineModel.RejQty = decimal.Parse(ds.Tables[0].Compute("SUM(RejQty)", "ProductType = 'PackOnline'").ToString());
                onlineModel.RejRate = onlineModel.TotalQty == 0 ? "0.00%" : Math.Round(onlineModel.RejQty / onlineModel.TotalQty * 100, 2).ToString("0.00") + "%";

                offlineModel = new OverallOutputChart_Model();
                offlineModel.Department = "Pack Offline";
                offlineModel.TotalQty = decimal.Parse(ds.Tables[0].Compute("SUM(TotalQty)", "ProductType = 'PackOffline'").ToString());
                offlineModel.RejQty = decimal.Parse(ds.Tables[0].Compute("SUM(RejQty)", "ProductType = 'PackOffline'").ToString());
                offlineModel.RejRate = offlineModel.TotalQty == 0 ? "0.00%" : Math.Round(offlineModel.RejQty / offlineModel.TotalQty * 100, 2).ToString("0.00") + "%";

            }

            return new List<OverallOutputChart_Model>() { onlineModel, offlineModel };
        }


        /// <summary>
        /// 将datatable转换为 OverallOutputChart_Model对象.
        /// sql中必须统一命名为 TotalQty, RejQty. 
        /// 程序指定sql中的这2个字段转换.
        /// </summary>
        private OverallOutputChart_Model ConvertModel(DataTable dt, string department)
        {                   
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var model = new OverallOutputChart_Model();
            model.Department = department;
            model.TotalQty = string.IsNullOrEmpty(dt.Rows[0]["TotalQty"].ToString()) ? 0 : decimal.Parse(dt.Rows[0]["TotalQty"].ToString());
            model.RejQty = string.IsNullOrEmpty(dt.Rows[0]["RejQty"].ToString()) ? 0 : decimal.Parse(dt.Rows[0]["RejQty"].ToString());
            model.RejRate = model.TotalQty == 0 ? "0.00%" : Math.Round(model.RejQty / model.TotalQty * 100, 2).ToString("0.00") + "%";

            return model;
        }

    }
}
