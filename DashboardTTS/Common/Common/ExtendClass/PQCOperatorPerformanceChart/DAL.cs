using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PQCOperatorPerformanceChart
{
    internal class DAL
    {
        public List<Model> GetOpList(SearchingCondition.BaseCondition condition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select 
                            userID
                            ,sum(CheckQty) as CheckQty
                            ,sum(PackQty) as PackQty
                            from 
                            (
	                            select 
	                            Upper(userID) as userID,
	                            sum(TotalQty) as CheckQty,
	                            0 as PackQty
	                            from PQCQaViTracking 
	                            where 1=1 and day >= @dateFrom and day < @dateTo
	                            group by userID

	                            union all

	                            select 
	                            Upper(userID) as userID,
	                            0 as CheckQty,
	                            sum(TotalQty) as PackQty
	                            from PQCPackTracking 
	                            where 1=1 and day >= @dateFrom and day < @dateTo
	                            group by userID 
                            ) a 
                            group by a.userID ");
          
            
            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2),
            };
            parameters[0].Value = condition.DateFrom;
            parameters[1].Value = condition.DateTo;
         
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            var modelList = new List<Model>();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Model();

                model.UserID = dr["userID"].ToString();
                model.CheckQty = int.Parse(dr["CheckQty"].ToString());
                model.PackQty = int.Parse(dr["PackQty"].ToString());
                modelList.Add(model);
            }



            return modelList;
        }
        
    }
}
