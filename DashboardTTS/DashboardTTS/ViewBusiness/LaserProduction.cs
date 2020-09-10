using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace DashboardTTS.ViewBusiness
{
    public class LaserProduction
    {
        private readonly Common.BLL.LMMSWatchLog_BLL watchlogBll = new Common.BLL.LMMSWatchLog_BLL();
        private readonly Common.BLL.LMMSWatchDog_His_BLL watchdogBLL = new Common.BLL.LMMSWatchDog_His_BLL();
        private readonly Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
        private readonly Common.Class.BLL.LMMSBom_BLL bomBLL = new Common.Class.BLL.LMMSBom_BLL();



        /// <summary>
        /// 根据bom表中, number, type字段, 自动生成laser summary report中的列信息
        /// </summary>
        /// <returns></returns>
        public List<ViewModel.LaserSummaryReport_ViewModel.typeColumn> GetTypeColumn()
        {
            DataTable dt = bomBLL.GetNumberForColumn();
            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.LaserSummaryReport_ViewModel.typeColumn> modelList = new List<ViewModel.LaserSummaryReport_ViewModel.typeColumn>();

            foreach (DataRow dr in dt.Rows)
            {
                string num = dr["Number"].ToString();
                string type = dr["type"].ToString();
                if (num.ToUpper() == "LASER" || num.ToUpper() == "PRINT")
                    continue;


                ViewModel.LaserSummaryReport_ViewModel.typeColumn model = new ViewModel.LaserSummaryReport_ViewModel.typeColumn();
                model.dataField = num;
                model.name = type + num;

                if (!modelList.Contains(model))
                {
                    modelList.Add(model);
                }
            }


            return modelList.OrderBy(p => p.dataField).ToList();
        }


        /// <summary>
        /// 动态列版本
        /// 根据bom中的num自动设定表格列信息. 
        /// 对单一machine通过dictionary来动态保存num对应的数据信息
        /// 遍历8台机器, 并将8组dictionary保存到list中, 最终转换成json传递到前台
        /// </summary>
        /// <param name="dDateFrom">UI查询参数</param>
        /// <param name="dDateTo">UI查询参数</param>
        /// <param name="sPartNo">UI查询参数</param>
        /// <param name="sShift">UI查询参数</param>
        /// <returns></returns>
        public string GetSummaryList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();


            //dt column:  machineID, number, totalpass, totalfail
            DataTable dtSummary = watchlogBll.GetSummaryReport(dDateFrom, dDateTo, sPartNo, sShift);
            if (dtSummary == null || dtSummary.Rows.Count == 0)
                return js.Serialize("");


            List<string> numberList = bomBLL.GetNumberList();
            if (numberList == null)
                return js.Serialize("");




            //datatable to list
            List<ViewModel.LaserSummaryReport_ViewModel> baseModelList = new List<ViewModel.LaserSummaryReport_ViewModel>();
            foreach (DataRow dr in dtSummary.Rows)
            {
                ViewModel.LaserSummaryReport_ViewModel model = new ViewModel.LaserSummaryReport_ViewModel();
                model.machineID = dr["machineID"].ToString();
                model.number = dr["number"].ToString();
                model.totalpass = double.Parse(dr["totalpass"].ToString());
                model.totalfail = double.Parse(dr["totalfail"].ToString());
                model.output = double.Parse(dr["Output"].ToString());

                baseModelList.Add(model);
            }




            //遍历每台机器
            List<Dictionary<string, string>> objResult = new List<Dictionary<string, string>>();
            for (int i = 1; i < 9; i++)
            {
                Dictionary<string, string> dicMC = new Dictionary<string, string>();

                dicMC.Add("machineID", "Machine" + i.ToString());


                //获取对应机器的所有记录
                var machineList = from a in baseModelList where a.machineID == i.ToString() select a;
                if (machineList  == null)
                {
                    #region if null set 0
                    dicMC.Add("laser", "0");

                    //遍历各个number
                    foreach (string strNum in numberList)
                    {
                        dicMC.Add(strNum, "0");
                    }

                    dicMC.Add("ok", "0");
                    dicMC.Add("ng", "0");
                    dicMC.Add("rejRate", "0.00%");
                    #endregion
                }
                else
                {
                    //添加 laser-qty
                    var laserList = from a in machineList where a.number.ToUpper() == "LASER" select a;
                    double laserQty = laserList == null ? 0 : laserList.Sum(p => p.totalpass);
                    dicMC.Add("laser", laserQty.ToString());


                    //遍历各个number, 并添加number-qty
                    foreach (string strNum in numberList)
                    {
                        var numList = from a in machineList where a.number == strNum  select a;
                        double numQty = numList == null ? 0 : numList.Sum(p => p.totalpass);

                        dicMC.Add(strNum, numQty.ToString());
                    }



                    //添加total pass, total fail, output
                    double totalPass = machineList.Sum(p => p.totalpass);
                    double totalFail = machineList.Sum(p => p.totalfail);
                    double rejRate = totalPass + totalFail == 0 ? 0 : Math.Round(totalFail / (totalPass + totalFail) * 100, 2);
                    
                    dicMC.Add("ok", totalPass.ToString());
                    dicMC.Add("ng", totalFail.ToString());
                    dicMC.Add("output",(totalPass + totalFail).ToString());
                    dicMC.Add("rejRate", rejRate.ToString() + "%");

                }

                objResult.Add(dicMC);
            }


            #region add a total row
            Dictionary<string, string> dicTotal = new Dictionary<string, string>();
            dicTotal.Add("machineID", "Total");

            
            var totalLaserList = from a in baseModelList where a.number.ToUpper() == "LASER" select a;
            double totalLaserQty = totalLaserList == null ? 0 : totalLaserList.Sum(p => p.totalpass);
            dicTotal.Add("laser", totalLaserQty.ToString());




            //遍历各个number, 并添加number-qty
            foreach (string strNum in numberList)
            {
                var totalNumList = from a in baseModelList where a.number == strNum select a;
                double totalNumQty = totalNumList == null ? 0 : totalNumList.Sum(p => p.totalpass);

                dicTotal.Add(strNum, totalNumQty.ToString());
            }


            //添加total pass, total fail, output
            double totalPassSummary = baseModelList.Sum(p => p.totalpass);
            double totalFailSummary = baseModelList.Sum(p => p.totalfail);
            double totalRejRate = totalPassSummary + totalFailSummary == 0 ? 0 : Math.Round(totalFailSummary / (totalPassSummary + totalFailSummary) * 100, 2);

            dicTotal.Add("ok", totalPassSummary.ToString());
            dicTotal.Add("ng", totalFailSummary.ToString());
            dicTotal.Add("output", (totalPassSummary + totalFailSummary).ToString());
            dicTotal.Add("rejRate", totalRejRate.ToString() + "%");

            objResult.Add(dicTotal);
            #endregion






            return js.Serialize(objResult);
        }


         







    }
}