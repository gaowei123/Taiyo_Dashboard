using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.ExtendClass.LaserDailyReport
{
    internal class LaserDailyReport_DAL
    {
        public List<LaserDailyReport_Model.DetailOutput> GetList(DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
a.machineID as MachineID
,a.shift as Shift
,isnull(a.totalQuantity,0) as LotQty
,CONVERT(varchar(5), a.startTime, 8)  as StartTime
,CONVERT(varchar(5), a.stopTime, 8) as StopTime
,convert(varchar,  a.stopTime - a.startTime, 108) as TakeTime
,a.partNumber as PartNo
,a.jobNumber as JobNo
,b.lotNo as LotNo
,isnull(a.totalPass,0) as PassQty
,isnull(a.totalFail,0) as RejQty
,isnull(a.setUpQTY,0) * c.materialCount as Setup
,isnull(a.buyOffQty,0) * c.materialCount as Buyoff
,isnull(a.totalPass,0) + isnull(a.totalFail,0) + isnull(a.setUpQTY,0) * c.materialCount + isnull(a.buyOffQty,0) * c.materialCount as Output

from lmmswatchdog_shift a 
left join LMMSInventory b on a.jobNumber = b.jobNumber
left join 
(
	select partnumber, count(1) as materialCount from LMMSBomDetail group by partNumber
) c on a.partNumber = c.partNumber
where 1=1 and a.day = @day");


            SqlParameter[] parameters =
            {
                new SqlParameter("@day", SqlDbType.DateTime)
            };

            parameters[0].Value = dDay;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            List<LaserDailyReport_Model.DetailOutput> detailOutputList = new List<LaserDailyReport_Model.DetailOutput>();

            foreach (DataRow dr in dt.Rows)
            {
                var model = new LaserDailyReport_Model.DetailOutput();

                model.MachineID = dr["MachineID"].ToString();
                model.Shift = dr["Shift"].ToString();
                model.StartTime = dr["StartTime"].ToString();
                model.StopTime = dr["StopTime"].ToString();
                model.TakeTime = dr["TakeTime"].ToString();
                model.PartNo = dr["PartNo"].ToString();
                model.JobNo = dr["JobNo"].ToString();
                model.LotNo = dr["LotNo"].ToString();

                model.PassQty = double.Parse(dr["PassQty"].ToString());
                model.RejQty = double.Parse(dr["RejQty"].ToString());
                model.Setup = double.Parse(dr["Setup"].ToString());
                model.Buyoff = double.Parse(dr["Buyoff"].ToString());
                model.Output = double.Parse(dr["Output"].ToString());

                detailOutputList.Add(model);
            }



            return detailOutputList;
        }

    }
}
