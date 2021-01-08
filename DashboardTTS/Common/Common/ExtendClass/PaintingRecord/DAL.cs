using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taiyo.SearchParam.PaintingParam;

namespace Common.ExtendClass.PaintingRecord
{
    internal class DAL
    {

        public List<Model> GetList(DeliveryRecordParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"select  
jobNumber as JobNo
,a.partNumber as PartNo
,sendingTo as SendingTo
,convert(varchar(50), convert(decimal(18, 0), inQuantity)) + '(' +
convert(varchar(50), convert(decimal(18, 0), inQuantity * case when isnull(b.materialCount, '') = '' then 1 else b.materialCount end))
+ ')' as MRPQty
,ISNULL(paintRejQty,0) as Rej
,paintProcess as Process
,[lotNo] as LotNo
,[remark] as Description
,[dateTime] as MFGDate
,[updatedTime] as ScanDate
,isnull(status, 'OK') as Status
FROM PaintingDeliveryHis a
left join (
	select partNumber, count(1) as materialCount 
	from  opendatasource('SQLOLEDB',{StaticRes.Global.SqlConnection.SqlconnPQC}).TAIYO_PQC.dbo.pqcbomdetail
    group by partNumber
) b on a.partNumber collate Chinese_PRC_CI_AS  = b.partNumber collate Chinese_PRC_CI_AS
where 1 = 1   
and updatedTime >= @DateFrom
and updatedTime <= @DateTo ");

            if (!string.IsNullOrEmpty(param.PartNo))
                strSql.AppendLine("and a.partNumber = @PartNo ");
            if (!string.IsNullOrEmpty(param.SendingTo))
                strSql.AppendLine("and a.sendingTo = @SendingTo ");
            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine("and a.jobNumber = @JobNo ");

            
            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@PartNo",SqlDbType.VarChar),
                new SqlParameter("@SendingTo",SqlDbType.VarChar),
                new SqlParameter("@JobNo",SqlDbType.VarChar)
            };

            parameters[0].Value = param.DateFrom.Value;
            parameters[1].Value = param.DateTo.Value;

            if (!string.IsNullOrEmpty(param.PartNo)) parameters[2].Value = param.PartNo; else parameters[2] = null;
            if (!string.IsNullOrEmpty(param.SendingTo)) parameters[3].Value = param.SendingTo; else parameters[3] = null;
            if (!string.IsNullOrEmpty(param.JobNo)) parameters[4].Value = param.JobNo; else parameters[4] = null;

            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            List<Model> modelList = new List<Model>();
            foreach (DataRow dr in dt.Rows)
            {
                Common.ExtendClass.PaintingRecord.Model model = new Model();
                model.JobNo = dr["JobNo"].ToString();
                model.PartNo = dr["PartNo"].ToString();
                model.SendingTo = dr["SendingTo"].ToString();
                model.MRPQty = dr["MRPQty"].ToString();
                model.Rej = decimal.Parse(dr["Rej"].ToString());
                model.Process = dr["Process"].ToString();
                model.LotNo = dr["LotNo"].ToString();
                model.Description = dr["Description"].ToString();
                model.MFGDate = DateTime.Parse(dr["MFGDate"].ToString()).ToString("dd/MM/yyyy");
                model.ScanDate = DateTime.Parse(dr["ScanDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                model.Status = dr["Status"].ToString();
                
                modelList.Add(model);
            }


            return modelList;
        }

    }
}
