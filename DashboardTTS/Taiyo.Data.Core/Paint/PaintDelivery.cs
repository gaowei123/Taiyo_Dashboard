using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Taiyo;
using Taiyo.Tool.Extension;

namespace Taiyo.Data.Core
{
    public class PaintDelivery
    {
        public PaintDelivery()
        {
            DeliveryList = new List<Delivery_Model>();
        }

        public PaintDelivery(Taiyo.Data.Query.PaintQuery.Delivery querys)
        {
            DeliveryList = Fill(querys);
        }


        public List<Delivery_Model> DeliveryList { get; set; }


        public class Delivery_Model
        {
            public string PartNo { get; set; }
            public string JobNo { get; set; }
            public string LotNo { get; set; }
            public decimal? MrpQty { get; set; }

            [Obsolete] // 改字段弃用,仅作记录
            public decimal? RejQty { get; set; }
            public string PaintProcess { get; set; }
            public string SendingTo { get; set; }
            public string Description { get; set; }

            // 该字段在painting delivery记录是错误的 (mrp系统接口给的就是错的)
            // 在pqc做buyoff的时候会重新更新
            public DateTime? MFGDate { get; set; }
            public DateTime? UpdatedTime { get; set; }

            // 是否被laser/wip库存删除的flag.
            public string Status { get; set; }

        }

        public List<Delivery_Model> Fill(Taiyo.Data.Query.PaintQuery.Delivery querys)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                SELECT
                [partNumber] as PartNo
                ,[jobNumber] as JobNo
                ,[lotNo] as LotNo
                ,convert(decimal, [inQuantity]) as MrpQty
                ,convert(decimal,ISNULL([PaintRejQty],0)) as RejQty
                ,[paintProcess] as PaintProcess
                ,[sendingTo] as SendingTo
                ,[remark] as Description
                ,[dateTime] as MFGDate
                ,[updatedTime] as UpdatedTime
                ,ISNULL([status],'') as Status
                FROM [PaintingDeliveryHis]
                where 1=1 ");


            if (querys.DateFrom != null)
                strSql.AppendLine(" and updatedTime >= @dateFrom ");

            if (querys.DateTo != null)
                strSql.AppendLine(" and updatedTime < @dateTo ");

            if (!string.IsNullOrEmpty(querys.PartNo))
                strSql.AppendLine(" and partNumber = @partNo ");

            if (!string.IsNullOrEmpty(querys.JobNo))
                strSql.AppendLine(" and jobNumber = @jobNo ");

            if (!string.IsNullOrEmpty(querys.LotNo))
                strSql.AppendLine(" and lotNo = @lotNo ");

            if (!string.IsNullOrEmpty(querys.PaintProcess))
                strSql.AppendLine(" and paintProcess = @paintProcess ");

            if (!string.IsNullOrEmpty(querys.SendingTo))
                strSql.AppendLine(" and sendingTo = @sendingTo ");

            if (!string.IsNullOrEmpty(querys.Status))
                strSql.AppendLine(" and status = @status ");
            

            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2),
                new SqlParameter("@dateTo", SqlDbType.DateTime2),
                new SqlParameter("@partNo", SqlDbType.VarChar),
                new SqlParameter("@jobNo", SqlDbType.VarChar),
                new SqlParameter("@lotNo", SqlDbType.VarChar),
                new SqlParameter("@paintProcess", SqlDbType.VarChar),
                new SqlParameter("@sendingTo", SqlDbType.VarChar),
                new SqlParameter("@status", SqlDbType.VarChar)
            };
            if (querys.DateFrom != null)                    parameters[0].Value = querys.DateFrom.Value; else parameters[0] = null;
            if (querys.DateTo != null)                      parameters[1].Value = querys.DateTo.Value; else parameters[1] = null;
            if (!string.IsNullOrEmpty(querys.PartNo))       parameters[2].Value = querys.PartNo; else parameters[2] = null;
            if (!string.IsNullOrEmpty(querys.JobNo))        parameters[3].Value = querys.JobNo; else parameters[3] = null;
            if (!string.IsNullOrEmpty(querys.LotNo))        parameters[4].Value = querys.LotNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(querys.PaintProcess)) parameters[5].Value = querys.PaintProcess; else parameters[5] = null;
            if (!string.IsNullOrEmpty(querys.SendingTo))    parameters[6].Value = querys.SendingTo; else parameters[6] = null;
            if (!string.IsNullOrEmpty(querys.Status))       parameters[7].Value = querys.Status; else parameters[7] = null;

            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);          
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                // 2021-04-13 new logic 通过泛型映射,直接将datatable转换成对应model的集合
                return ds.Tables[0].ToList<Delivery_Model>();
        }

    }
}
