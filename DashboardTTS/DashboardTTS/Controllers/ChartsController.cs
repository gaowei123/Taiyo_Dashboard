using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taiyo.SearchParam;
using Taiyo.Enum.Organization;

namespace DashboardTTS.Controllers
{
    public class ChartsController : Controller
    {
        #region view
        public ActionResult LaserMachineActivityChart()
        {
            return View();
        }
        public ActionResult PQCOperatorPerformanceChart()
        {
            return View();
        }
        public ActionResult PQCOperatorSummaryChart()
        {
            return View();
        }
        public ActionResult PQCTopRejectChart()
        {
            return View();
        }




        public ActionResult LaserProductionChart()
        {
            return View();
        }
        public ActionResult LaserMachineChart()
        {
            return View();
        }
        public ActionResult PQCProductionChart()
        {
            return View();
        }
      
        
        #endregion







        MyChart.ChartFactory _chartFactory = new MyChart.ChartFactory();
        
        #region Home Page
        public JsonResult GetHomeTrendData(bool isDisplayOffday)
        {
            HomeParam param = new HomeParam();
            param.DateFrom = DateTime.Now.AddDays(-12).Date;
            param.DateTo = DateTime.Now.Date;
            param.IsDisplayOffday = isDisplayOffday;



            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("HomeTrend");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }

        public JsonResult GetTotalPieChartData()
        {
            HomeParam param = new HomeParam();
            param.DateFrom = DateTime.Now.Date;
            param.DateTo = DateTime.Now.Date.AddDays(1);
            param.IsDisplayOffday = true;

            
            

            //饼图数据结构和曲线/柱状图有区别, 直接传list过去. 再前端处理.
            Common.ExtendClass.Home.Home_BLL bll = new Common.ExtendClass.Home.Home_BLL();
            List<Common.ExtendClass.Home.Home_Model.DailyTrend> modelList = bll.GetDailyTrend(param);


            //遍历每个部门
            foreach (Taiyo.Enum.Organization.Department item in Enum.GetValues(typeof(Taiyo.Enum.Organization.Department)))
            {
                //排除不需要的部门, 
                if (item == Department.PQC || item == Department.Assembly || item == Department.TSS|| item ==Department.Office||
                    item == Department.HR_Finance || item == Department.Planning_Purchasing || item == Department.QA_QC_FA ||
                    item == Department.Sales_Project || item ==Department.Store)
                    continue;


                var temp = from a in modelList where a.Department == item.ToString() select a; 
                if (temp == null || temp.Count() == 0)
                {
                    //饼图只需要 department, output.  其余字段不需要赋值.
                    modelList.Add(new Common.ExtendClass.Home.Home_Model.DailyTrend()
                    {
                        Department = item.ToString(),
                        Output = 0
                    });
                }
            }


            return Json(modelList);
        }
        #endregion

        public JsonResult GetLaserActivityData(DateTime DateFrom, DateTime DateTo, string Shift)
        {
            var param = new Taiyo.SearchParam.LaserParam.LaserActivityCondition();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);
            param.Shift = Shift;


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserMachineActivity");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }

        public JsonResult GetPQCOperatorPerformanceData(DateTime DateFrom, DateTime DateTo)
        {
            var param = new Taiyo.SearchParam.BaseParam();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperatorPerformance");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);


            return chartData == null ? Json("") : Json(chartData);
        }

        public JsonResult GetPQCOperatorSummaryData()
        {
            var param = new Taiyo.SearchParam.PQCParam.PQCOperatorSummaryCondition();

            param.GroupBy = Request.Form["GroupBy"];
            if (param.GroupBy == "Daily")
            {
                param.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
                param.DateTo = DateTime.Parse(Request.Form["DateTo"]).AddDays(1);
            }
            else if (param.GroupBy == "Monthly")
            {
                int year = int.Parse(Request.Form["Year"]);
                param.DateFrom = DateTime.Parse($"{year}-1-1");
                param.DateTo = param.DateFrom.Value.AddYears(1);
            }
            else if (param.GroupBy == "Yearly")
            {
                param.DateFrom = DateTime.Parse("2017-1-1");
                param.DateTo = DateTime.Now.AddDays(1);
            }
            else
            {
                throw new NullReferenceException("No such type for group by!");
            }
            param.PIC = Request.Form["PIC"];

            

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperatorSummary");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);


            return chartData == null ? Json("") : Json(chartData);
        }


        #region Top Reject
        public JsonResult GetTopPartNoRejData(DateTime dateFrom, DateTime dateTo, int topCount)
        {
            var param = new Taiyo.SearchParam.PQCParam.PQCTopRejectCondition();
            param.DateFrom = dateFrom;
            param.DateTo = dateTo.AddDays(1);
            param.TopCount = topCount;
            

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCTopRejPartNo");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return chartData == null ? Json("") : Json(chartData);
        }
        
        public JsonResult GetTopDefectRejData(DateTime dateFrom, DateTime dateTo, int topCount)
        {
            var param = new Taiyo.SearchParam.PQCParam.PQCTopRejectCondition();
            param.DateFrom = dateFrom;
            param.DateTo = dateTo.AddDays(1);
            param.TopCount = topCount;


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCTopRejDefect");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return chartData == null ? Json("") : Json(chartData);
        }

        #endregion








        public JsonResult GetLaserProductionData()
        {
            var param = new Taiyo.SearchParam.LaserParam.LaserProductionCondition();
            param.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            param.DateTo = DateTime.Parse(Request.Form["DateTo"]);
            param.DateTo = param.DateTo.Value.AddDays(1);
            param.ChartType = Request.Form["ChartType"];

            
            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserProduction");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }
        
        public JsonResult GetLaserMachineData()
        {
            var param = new Taiyo.SearchParam.BaseParam();
            param.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            param.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserMachine");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }

        public JsonResult GetPqcProductionData()
        {
            var param = new Taiyo.SearchParam.BaseParam();
            param.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            param.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCProduction");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }

        public JsonResult GetPqcOperatorData()
        {
            var param = new Taiyo.SearchParam.BaseParam();
            param.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            param.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperator");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(param);

            return Json(chartData);
        }
        
      

      


        

      
    }
}