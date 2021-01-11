using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.ExtendClass.LaserLiveReport
{
    internal class LaserLive_DAL
    {
        public List<LaserLive_Model> GetList(Taiyo.SearchParam.LaserParam.LaserLiveParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"select
	convert(varchar(50), a.day,111) + ' - '+a.shift as shift
	,'Machine' + a.machineID as machineID  
	,b.module as model
	,a.jobNumber as jobNo
	,a.partNumber as partNo  
	,startTime as startTime
	,stopTime as endTime
	,convert(varchar,  ISNULL([stopTime], GETDATE()) - [startTime], 108) as time
		
	--PCS
	,a.totalPass as ok
	,a.totalFail as ng
	,a.totalpass + a.totalFail as output
	,totalQuantity as mrpTotal
    ,isnull(a.setUpQTY,0) *  isnull(c.materialCount,1) as setupQty
	--PCS

	--SET
    ,dbo.GetMinOK(ok1Count,ok2Count,ok3Count,ok4Count,ok5Count,ok6Count,ok7Count,ok8Count,ok9Count,ok10Count,ok11Count,ok12Count,ok13Count,ok14Count,ok15Count,ok16Count) as setOK
	,dbo.GetMaxNG(ng1Count,ng2Count,ng3Count,ng4Count,ng5Count,ng6Count,ng7Count,ng8Count,ng9Count,ng10Count,ng11Count,ng12Count,ng13Count,ng14Count,ng15Count,ng16Count) as setNG	
	,dbo.GetMaxNG(ng1Count,ng2Count,ng3Count,ng4Count,ng5Count,ng6Count,ng7Count,ng8Count,ng9Count,ng10Count,ng11Count,ng12Count,ng13Count,ng14Count,ng15Count,ng16Count) 
	 +
	 dbo.GetMinOK(ok1Count,ok2Count,ok3Count,ok4Count,ok5Count,ok6Count,ok7Count,ok8Count,ok9Count,ok10Count,ok11Count,ok12Count,ok13Count,ok14Count,ok15Count,ok16Count) as setOutput
	,totalQuantity / isnull(c.materialcount,1) as setMrpTotal
	,isnull(a.setUpQTY,0) as setSetupQty
	--SET

	--Display  占个格子, 后续在代码中生成.
	,'' as displayOK
	,'' as displayNG
	,'' as displayOutput
	,'' as displayRejRate
	,'' as displaySetup
	,'' as displayMRP
	--Display

FROM[LMMSWatchDog_Shift] a 
left join LMMSBom b on a.partNumber = b.partNumber and a.machineID = b.machineID
left join (select partNumber, count(1) as materialCount from lmmsbomdetail group by partNumber) c on a.partNumber = c.partNumber
where 1=1  
--数量0的记录也显示 --2020 08 19
--and (a.totalPass + a.totalFail) > 0 ");

            strSql.AppendLine(" and a.day >= @dateFrom ");
            strSql.AppendLine(" and a.day < @dateTo ");


            if (!string.IsNullOrEmpty(param.Shift))
                strSql.AppendLine(" and a.shift = @shift ");

            if (!string.IsNullOrEmpty(param.Model))
                strSql.AppendLine(" and b.module = @model ");

            if (!string.IsNullOrEmpty(param.PartNo))
                strSql.AppendLine(" and a.partNumber = @partNumber ");

            if (!string.IsNullOrEmpty(param.MachineID))
                strSql.AppendLine(" and a.machineID = @machineID ");

            if (!string.IsNullOrEmpty(param.JobNo))
                strSql.AppendLine(" and a.jobNumber = @jobNumber ");


            strSql.Append(" order by a.datetime desc ");



            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@model",SqlDbType.VarChar),
                new SqlParameter("@partNo",SqlDbType.VarChar),
                new SqlParameter("@machineID",SqlDbType.VarChar),
                new SqlParameter("@jobNo",SqlDbType.VarChar)
            };
            parameters[0].Value = param.DateFrom;
            parameters[1].Value = param.DateTo;
            if (param.Shift == "") { parameters[2] = null; } else { parameters[2].Value = param.Shift; }
            if (param.Model == "") { parameters[3] = null; } else { parameters[3].Value = param.Model; }
            if (param.PartNo == ""){ parameters[4] = null; } else { parameters[4].Value = param.PartNo; }
            if (param.MachineID == "") { parameters[5] = null; } else { parameters[5].Value = param.MachineID; }
            if (param.JobNo == "") { parameters[6] = null; } else { parameters[6].Value = param.JobNo; }



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            List<LaserLive_Model> modelList = new List<LaserLive_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                LaserLive_Model model = new LaserLive_Model();

                model.Date = dr["shift"].ToString();
                model.MachineID = dr["machineID"].ToString();
                model.Model = dr["model"].ToString();
                model.PartNo = dr["partNo"].ToString();
                model.JobNo = dr["jobNo"].ToString();

                model.StartTime = DateTime.Parse(dr["startTime"].ToString()).ToString("HH:mm");
                model.EndTime = DateTime.Parse(dr["endTime"].ToString()).ToString("HH:mm");
             
                model.TakeTime = dr["time"].ToString();
              
                //set(pcs)
                model.OkQty = $"{dr["setOK"].ToString()}({dr["ok"].ToString()})";
                model.NgQty = $"{dr["setNG"].ToString()}({dr["ng"].ToString()})";
                model.Output = $"{dr["setOutput"].ToString()}({dr["output"].ToString()})";
                model.Setup = $"{dr["setSetupQty"].ToString()}({dr["setupQty"].ToString()})";
                model.MRPQty = $"{dr["setMrpTotal"].ToString()}({dr["mrpTotal"].ToString()})";

                //Math.Round((setNG + setSetupQty) / setOutput * 100, 2);
                double rejRate = Math.Round((double.Parse(dr["ng"].ToString()) + double.Parse(dr["setupQty"].ToString())) / double.Parse(dr["output"].ToString()) * 100, 2);
                double setRejRate = Math.Round((double.Parse(dr["setNG"].ToString()) + double.Parse(dr["setSetupQty"].ToString())) / double.Parse(dr["setOutput"].ToString()) * 100, 2);
                model.Rej = $"{setRejRate.ToString("0.00")}({rejRate.ToString("0.00")})";

                modelList.Add(model);
            }

            return modelList;
        }

    }
}
