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
    public class Paint_Delivery
    {
        public Paint_Delivery()
        {
            DeliveryList = new List<Delivery_Model>();
        }

        public Paint_Delivery(List<string> queryList, SqlParameter[] parameters)
        {
            DeliveryList = GetList(queryList, parameters);
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
            public bool Status { get; set; }

        }

        public List<Delivery_Model> GetList(List<string> queryList, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                SELECT
                [partNumber] as PartNo
                ,[jobNumber] as JobNo
                ,[lotNo] as LotNo
                ,convert(int, [inQuantity]) as MrpQty
                ,ISNULL([PaintRejQty],0) as RejQty
                ,[paintProcess] as PaintProcess
                ,[sendingTo] as SendingTo
                ,[remark] as Description
                ,[dateTime] as MFGDate
                ,[updatedTime] as UpdatedTime
                ,[status] as Status
                FROM [PaintingDeliveryHis]
                where 1=1 ");

            foreach (string query in queryList)
            {
                strSql.AppendLine(query);
            }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);          
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                // 2021-04-13 new logic 通过泛型映射,直接将datatable转换成对应model的集合
                return ds.Tables[0].ToList<Delivery_Model>();
        }

    }
}
