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
    internal class Paint_Buyoff
    {
        public Paint_Buyoff()
        {
            BuyoffList = new List<Buyoff_Model>();
        }

        public Paint_Buyoff(List<string> queryList, SqlParameter[] parameters)
        {
            BuyoffList = GetList(queryList, parameters);
        }


        public List<Buyoff_Model> BuyoffList { get; set; }


        internal class Buyoff_Model
        {
            public string JobNo { get; set; }
            public string LotNo { get; set; }
            public string PartNo { get; set; } 
            public string MaterialName { get; set; }

        }

        internal List<Buyoff_Model> GetList(List<string> queryList, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"");

            foreach (string query in queryList)
            {
                strSql.AppendLine(query);
            }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);

            // 2021-04-13 new logic 通过泛型映射,直接将datatable转换成对应model的集合
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].ToList<Buyoff_Model>();

        }

    }
}
