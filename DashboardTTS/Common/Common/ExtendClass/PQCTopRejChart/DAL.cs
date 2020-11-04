using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PQCTopRejChart
{
    internal class DAL
    {

        public List<Model.TopPartNo> GetTopPartNoRej(SearchingCondition.PQCTopRejectCondition condition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select
                                    top {0}
                                    partNumber
                                    ,sum(rejectQty) as rejQty
                                    from PQCQaViTracking 
                                    where 1=1 and day >=@dateFrom and day < @dateTo
                                    group by partNumber order by rejQty desc", condition.TopCount);


            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2)
            };
            parameters[0].Value = condition.DateFrom;
            parameters[1].Value = condition.DateTo;

            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            var modelList = new List<Model.TopPartNo>();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Model.TopPartNo();
                model.PartNo = dr["partNumber"].ToString();
                model.RejQty = int.Parse(dr["rejQty"].ToString());
               
                modelList.Add(model);
            }



            return modelList;
        }
        

        public List<Model.TopDefect> GetTopDefectRej(SearchingCondition.PQCTopRejectCondition condition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select
                                    top {0}
                                    defectDescription +'-'+defectCode as defect
                                    ,sum(rejectQty) as rejQty
                                    from PQCQaViDefectTracking 
                                    where 1=1 and day >=@dateFrom and day < @dateTo
                                    group by defectDescription, defectCode order by  rejQty desc", condition.TopCount);


            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2)
            };
            parameters[0].Value = condition.DateFrom;
            parameters[1].Value = condition.DateTo;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            var modelList = new List<Model.TopDefect>();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Model.TopDefect();
                model.DefectCode = dr["defect"].ToString();
                model.RejQty = int.Parse(dr["rejQty"].ToString());

                modelList.Add(model);
            }



            return modelList;
        }
        
    }
}
