using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taiyo.SearchParam.PQCParam;


namespace Common.ExtendClass.PQCSummaryReport
{
    internal class Summary_DAL
    {

        /// <summary>
        /// vitracking 根据partNumber, jobId, processes汇总
        /// 通过part no关联 pqcbom
        /// 通过jobid关联 根据jobId, processes汇总的defect tracking
        /// 获取一个job一条的详细列表
        /// currentProcess, containLaserFlag, lastCheckProcess用于判断分类 laser, wip.
        /// 
        /// 具体的分组,归类在代码中根据具体需求再改.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable GetDataTableChecking(PQCSummaryParam param)
        {
            string strWhereShift = string.IsNullOrEmpty(param.Shift) ? "" : " and shift = @Shift ";
            string strWherePartNo = string.IsNullOrEmpty(param.PartNo) ? "" : " and partNumber = @PartNo ";

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select
a.jobId,
a.partNumber,
a.processes as currentProcess,
a.TotalQty,
a.acceptQty,
a.rejectQty,

ISNULL(case when b.supplier = 'TTS' then c.mouldRej end,0) as TTSMouldRej,
ISNULL(case when b.supplier != 'TTS' then c.mouldRej end,0) as VendorMouldRej,
c.paintRej,
c.laserRej,
c.othersRej,

b.containLaserFlag,
b.lastCheckProcess,
b.number,
b.description

from (
	select
	partNumber,
	jobId,
	processes,
	SUM(TotalQty) as TotalQty,
	SUM(acceptQty) as acceptQty,
	SUM(rejectQty) as rejectQty
	from PQCQaViTracking 
	where day >= @DateFrom and day < @DateTo {0} {1}
	group by partNumber, jobId, processes
)a 
left join (
	select 
	partNumber,
	case when CHARINDEX('Laser',processes,0) > 1 then 'true' else  'false' end as containLaserFlag,
	case when CHARINDEX('Check#2',processes,0) > 1 then 'Check#2' else 'Check#1' end as lastCheckProcess,
	remark_1 as supplier,
    description,
	ISNULL(number,'') as number
	from PQCBom 
)  b on a.partNumber = b.partNumber
left join (
	select 
	jobId,
	processes,
	ISNULL(SUM( case when defectdescription = 'Mould' then rejectQty end),0) as mouldRej,
	ISNULL(SUM( case when defectdescription = 'Paint' then rejectQty end),0) as paintRej,
	ISNULL(SUM( case when defectdescription = 'Laser' then rejectQty end),0) as laserRej,
	ISNULL(SUM( case when defectdescription = 'others' then rejectQty end),0) as othersRej
	from PQCQaViDefectTracking
	where day >= @DateFrom and day < @DateTo
	group by jobId, processes
)c on a.jobId = c.jobId and a.processes = c.processes
where 1=1 ", strWhereShift, strWherePartNo);
            
            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar),
                new SqlParameter("@PartNo", SqlDbType.VarChar)
            };

            parameters[0].Value = param.DateFrom.Value;
            parameters[1].Value = param.DateTo.Value;
            if (!string.IsNullOrEmpty(param.Shift)) parameters[2].Value = param.Shift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.PartNo)) parameters[3].Value = param.PartNo; else parameters[3] = null;
            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            return ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
        }
        


        /// <summary>
        /// datatable to list
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<Summary_Model.Detail> GetCheckingList(PQCSummaryParam param)
        {
            DataTable dt = GetDataTableChecking(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<Summary_Model.Detail> detailList = new List<Summary_Model.Detail>();
            foreach (DataRow dr in dt.Rows)
            {
                Summary_Model.Detail model = new Summary_Model.Detail();
                model.JobNo = dr["jobId"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.CurrentProcess = dr["currentProcess"].ToString();
                model.Description = dr["description"].ToString();
                model.TotalQty = decimal.Parse(dr["TotalQty"].ToString());
                model.PassQty = decimal.Parse(dr["acceptQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());

                model.TTSMouldRej = decimal.Parse(dr["TTSMouldRej"].ToString());
                model.VendorMouldRej = decimal.Parse(dr["VendorMouldRej"].ToString());
                model.PaintRej = decimal.Parse(dr["paintRej"].ToString());
                model.LaserRej = decimal.Parse(dr["laserRej"].ToString());
                model.OthersRej = decimal.Parse(dr["othersRej"].ToString());

                model.IsContainLaser = dr["containLaserFlag"].ToString();
                model.LastCheckProcess = dr["lastCheckProcess"].ToString();
                model.Number = dr["number"].ToString();
                
                detailList.Add(model);
            }

            return detailList;
        }




        

        /// <summary>
        /// 与checking相同的sql逻辑获取detial的列表,在代码中group by
        /// </summary>
        private DataTable GetDataTablePacking(PQCSummaryParam param)
        {
            string strWhereShift = string.IsNullOrEmpty(param.Shift) ? "" : " and shift = @Shift ";
            string strWherePartNo = string.IsNullOrEmpty(param.PartNo) ? "" : " and partNumber = @PartNo ";

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select
a.jobId,
a.partNumber,
a.processes as currentProcess,
a.TotalQty,
a.acceptQty,
a.rejectQty,

b.containLaserFlag,
b.lastCheckProcess,
b.number

from (
	select
	partNumber,
	jobId,
	processes,
	SUM(TotalQty) as TotalQty,
	SUM(acceptQty) as acceptQty,
	SUM(rejectQty) as rejectQty
	from PQCPackTracking 
	where day >= @DateFrom and day < @DateTo {0} {1}
	group by partNumber, jobId, processes
)a 
left join (
	select 
	partNumber,
	case when CHARINDEX('Laser',processes,0) > 1 then 'true' else  'false' end as containLaserFlag,
	case when CHARINDEX('Check#2',processes,0) > 1 then 'Check#2' else 'Check#1' end as lastCheckProcess,
	ISNULL(number,'') as number
	from PQCBom 
)  b on a.partNumber = b.partNumber
where 1=1 ", strWhereShift, strWherePartNo);
            


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@PartNo", SqlDbType.VarChar)
            };

            paras[0].Value = param.DateFrom.Value;
            paras[1].Value = param.DateTo.Value;
            if (param.Shift != "") paras[2].Value = param.Shift; else paras[2] = null;
            if (param.PartNo != "") paras[3].Value = param.PartNo; else paras[3] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            return ds == null || ds.Tables.Count == 0? null : ds.Tables[0];
        }
        

        public List<Summary_Model.Detail> GetPackingList(PQCSummaryParam param)
        {
            DataTable dt = GetDataTablePacking(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<Summary_Model.Detail> detailList = new List<Summary_Model.Detail>();
            foreach (DataRow dr in dt.Rows)
            {
                Summary_Model.Detail model = new Summary_Model.Detail();
                model.JobNo = dr["jobId"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.CurrentProcess = dr["currentProcess"].ToString();
                model.TotalQty = decimal.Parse(dr["TotalQty"].ToString());
                model.PassQty = decimal.Parse(dr["acceptQty"].ToString());
                model.RejQty = decimal.Parse(dr["rejectQty"].ToString());
                
                model.IsContainLaser = dr["containLaserFlag"].ToString();
                model.LastCheckProcess = dr["lastCheckProcess"].ToString();
                model.Number = dr["number"].ToString();

                detailList.Add(model);
            }

            return detailList;
        }
        

    }
}
