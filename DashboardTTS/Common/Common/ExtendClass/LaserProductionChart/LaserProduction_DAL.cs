using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.LaserProductionChart
{
    internal class LaserProduction_DAL
    {
        public List<LaserProduction_Model> GetProduction(Taiyo.SearchParam.LaserParam.LaserProductionCondition param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
day
, shift
, a.machineID
, a.partNumber
, b.customer
, b.module as model
, totalquantity
, totalPass
, totalFail
, a.datetime
from LMMSWatchDog_Shift a
left
join LMMSBom b on a.partnumber = b.partnumber and a.machineID = b.machineID 
where 1=1 ");

            strSql.AppendLine(" and a.day >= @dateFrom ");
            strSql.AppendLine(" and a.day < @dateTo ");

            //if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and a.shift = @shift");
            //if (!string.IsNullOrEmpty(sPartNo)) strSql.AppendLine(" and a.partNumber = @partNo");
            //if (!string.IsNullOrEmpty(sModel)) strSql.AppendLine(" and b.module = @model ");


            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2),
                //new SqlParameter("@shift",SqlDbType.VarChar,8),
                //new SqlParameter("@partNo",SqlDbType.VarChar),
                //new SqlParameter("@model",SqlDbType.VarChar)
            };
            parameters[0].Value = param.DateFrom;
            parameters[1].Value = param.DateTo;
            //if (!string.IsNullOrEmpty(sShift)) parameters[2].Value = sShift; else parameters[2] = null;
            //if (!string.IsNullOrEmpty(sPartNo)) parameters[3].Value = sPartNo; else parameters[3] = null;
            //if (!string.IsNullOrEmpty(sModel)) parameters[4].Value = sModel; else parameters[4] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0|| ds.Tables[0].Rows.Count == 0)
                return null;



            var modelList = new List<LaserProduction_Model>();
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var model = new LaserProduction_Model();

                DateTime dDay = DateTime.Parse(dr["day"].ToString());

                model.Year = dDay.Year;
                model.Month = dDay.Month;
                model.Day = dDay;

                model.MachineID = dr["machineID"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.Model = dr["model"].ToString();
                model.Customer = dr["customer"].ToString();
                model.TotalQty = decimal.Parse(dr["totalquantity"].ToString());
                model.PassQty = decimal.Parse(dr["totalPass"].ToString());
                model.RejQty = decimal.Parse(dr["totalFail"].ToString());
                model.CreatedTime = DateTime.Parse(dr["datetime"].ToString());

                modelList.Add(model);
            }



            return modelList;
        }
    }
}
