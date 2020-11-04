using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PQCOperatorSummaryChart
{
    internal class DAL
    {

        public List<Model> GetDailyList(SearchingCondition.PQCOperatorSummaryCondition condition)
        {
            string strConditionPIC = string.IsNullOrEmpty(condition.PIC) ? "" : " and userID = @userID";
            
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select 
                                    a.[Day]
                                    ,a.[Month]
                                    ,a.[Year]
                                    ,sum(CheckQty) as CheckQty
                                    ,sum(PackQty) as PackQty
                                    from 
                                    (
	                                    select 
	                                    DAY([Day]) as [Day],	
	                                    MONTH([Day]) as [Month],
	                                    YEAR([Day]) as [Year],
	                                    SUM(TotalQty) as CheckQty,
	                                    0 as PackQty
	                                    from PQCQaViTracking 
	                                    where 1=1 and day >= @dateFrom and day < @dateTo {0}
	                                    group by DAY([Day]), MONTH([Day]), YEAR([Day])

	                                    union all

	                                    select 
	                                    DAY([Day]) as [Day],	
	                                    MONTH([Day]) as [Month],
	                                    YEAR([Day]) as [Year],
	                                    0 as CheckQty,
	                                    SUM(TotalQty) as PackQty	
	                                    from PQCPackTracking 
	                                    where 1=1 and day >= @dateFrom and day < @dateTo {1}
	                                    group by DAY([Day]), MONTH([Day]), YEAR([Day])
                                    ) a 
                                    group by a.[Day], a.[Month], a.[Year] ", strConditionPIC, strConditionPIC);


            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2),
                new SqlParameter("@userID",SqlDbType.VarChar)
            };
            parameters[0].Value = condition.DateFrom;
            parameters[1].Value = condition.DateTo;
            if (string.IsNullOrEmpty(condition.PIC)) parameters[2] = null; else parameters[2].Value = condition.PIC;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            var modelList = new List<Model>();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Model();

                model.Day = int.Parse(dr["Day"].ToString());
                model.Month = int.Parse(dr["Month"].ToString());
                model.Year = int.Parse(dr["Year"].ToString());

                model.CheckQty = int.Parse(dr["CheckQty"].ToString());
                model.PackQty = int.Parse(dr["PackQty"].ToString());
                modelList.Add(model);
            }



            return modelList;
        }




    }
}
