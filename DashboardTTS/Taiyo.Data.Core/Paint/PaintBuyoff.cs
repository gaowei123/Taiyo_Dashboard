using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Taiyo.Tool.Extension;

namespace Taiyo.Data.Core
{
    public class PaintBuyoff
    {
        public PaintBuyoff()
        {
            BuyoffList = new List<Buyoff_Model>();
        }

        public PaintBuyoff(Query.PaintQuery.Buyoff querys)
        {
            BuyoffList = GetList(querys);
        }


        public List<Buyoff_Model> BuyoffList { get; set; }


        public class Buyoff_Model
        {
            public string JobNo { get; set; }
            public string LotNo { get; set; }
            public string PartNo { get; set; } 
            public string MaterialName { get; set; }

        }

        public List<Buyoff_Model> GetList(Query.PaintQuery.Buyoff querys)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"");


            SqlParameter[] parameters =
            {

            };


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);

            // 2021-04-13 new logic 通过泛型映射,直接将datatable转换成对应model的集合
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].ToList<Buyoff_Model>();

        }

    }
}
