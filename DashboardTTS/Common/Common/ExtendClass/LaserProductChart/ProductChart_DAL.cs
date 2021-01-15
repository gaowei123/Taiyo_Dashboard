using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Taiyo.SearchParam.LaserParam;


namespace Common.ExtendClass.LaserProductChart
{
    internal class ProductChart_DAL
    {
        public List<ProductChart_Model.Detail> GetList(LaserProductChartParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select 
day,
machineID,
partNumber,
jobNumber,
totalpass, 
totalfail,
ISNULL(setUpQty,0) as setUpQty,
ISNULL(buyOffQty,0) as buyOffQty,
ISNULL(shortage,0) as shortage
from lmmswatchdog_shift
where day >= @dateFrom
and day < @dateTo ");
            if (!string.IsNullOrEmpty(param.PartNo)) strSql.AppendLine(" and partNumber = @partNo ");


            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2),
                new SqlParameter("@dateTo", SqlDbType.DateTime2),
                new SqlParameter("@partNo", SqlDbType.VarChar)
            };

            parameters[0].Value = param.DateFrom;
            parameters[1].Value = param.DateTo;

            if (!string.IsNullOrEmpty(param.PartNo)) parameters[2].Value = param.PartNo; else parameters[2] = null;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds ==null || ds.Tables.Count== 0)
                return null;


            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            List<ProductChart_Model.Detail> list = new List<ProductChart_Model.Detail>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductChart_Model.Detail model = new ProductChart_Model.Detail();
                model.Date = DateTime.Parse(dr["day"].ToString());
                model.Year = model.Date.Year;
                model.Month = model.Date.Month;
                model.Day = model.Date.Day;

                model.MachineID = dr["machineID"].ToString();
                model.PartNo = dr["partNumber"].ToString();
                model.JobNo = dr["jobNumber"].ToString();

                model.PassQty = decimal.Parse(dr["totalpass"].ToString());
                model.RejQty = decimal.Parse(dr["totalfail"].ToString());

                list.Add(model);
            }



            return list;
        }

    }
}
