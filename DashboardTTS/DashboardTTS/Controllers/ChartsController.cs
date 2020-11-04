using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult GetHomeTrendData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Now.AddDays(-13).Date;
            condition.DateTo = DateTime.Now.Date;

            //本地测试
            //condition.DateFrom = DateTime.Parse("2020-2-1");
            //condition.DateTo = DateTime.Parse("2020-2-9");


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("HomeTrend");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }

        public JsonResult GetTotalPieChartData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Now.Date;
            condition.DateTo = DateTime.Now.Date.AddDays(1);

            //本地测试
            //condition.DateFrom = DateTime.Parse("2020-2-4");
            //condition.DateTo = DateTime.Parse("2020-2-5");


            Common.ExtendClass.Home.Home_BLL bll = new Common.ExtendClass.Home.Home_BLL();
            List<Common.ExtendClass.Home.Home_Model.DailyTrend> modelList = bll.GetDailyTrend(condition);


            return Json(modelList);
        }
        #endregion

        public JsonResult GetLaserActivityData()
        {
            Common.SearchingCondition.LaserActivityCondition condition = new Common.SearchingCondition.LaserActivityCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateTo"]).AddDays(1);
            condition.Shift = Request.Form["Shift"];

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserMachineActivity");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }

        public JsonResult GetPQCOperatorPerformanceData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateTo"]).AddDays(1);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperatorPerformance");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);


            return chartData == null ? Json("") : Json(chartData);
        }

        public JsonResult GetPQCOperatorSummaryData()
        {
            Common.SearchingCondition.PQCOperatorSummaryCondition condition = new Common.SearchingCondition.PQCOperatorSummaryCondition();
            condition.GroupBy = Request.Form["GroupBy"];
            if (condition.GroupBy == "Daily")
            {
                condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
                condition.DateTo = DateTime.Parse(Request.Form["DateTo"]).AddDays(1);
            }
            else if (condition.GroupBy == "Monthly")
            {
                int year = int.Parse(Request.Form["Year"]);
                condition.DateFrom = DateTime.Parse($"{year}-1-1");
                condition.DateTo = condition.DateFrom.Value.AddYears(1);
            }
            else if (condition.GroupBy == "Yearly")
            {
                condition.DateFrom = DateTime.Parse("2017-1-1");
                condition.DateTo = DateTime.Now.AddDays(1);
            }
            else
            {
                throw new NullReferenceException("No such type for group by!");
            }
            condition.PIC = Request.Form["PIC"];


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperatorSummary");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);


            return chartData == null ? Json("") : Json(chartData);
        }


        #region Top Reject
        public JsonResult GetTopPartNoRejData(DateTime dateFrom, DateTime dateTo, int topCount)
        {
            Common.SearchingCondition.PQCTopRejectCondition condition = new Common.SearchingCondition.PQCTopRejectCondition();
            condition.DateFrom = dateFrom;
            condition.DateTo = dateTo;
            condition.TopCount = topCount;
            

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCTopRejPartNo");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return chartData == null ? Json("") : Json(chartData);
        }
        
        public JsonResult GetTopDefectRejData(DateTime dateFrom, DateTime dateTo, int topCount)
        {
            Common.SearchingCondition.PQCTopRejectCondition condition = new Common.SearchingCondition.PQCTopRejectCondition();
            condition.DateFrom = dateFrom;
            condition.DateTo = dateTo;
            condition.TopCount = topCount;


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCTopRejDefect");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return chartData == null ? Json("") : Json(chartData);
        }

        #endregion






        public JsonResult GetLaserProductionData()
        {
            Common.SearchingCondition.LaserProductionCondition condition = new Common.SearchingCondition.LaserProductionCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateTo"]);
            condition.DateTo = condition.DateTo.Value.AddDays(1);
            condition.ChartType = Request.Form["ChartType"];



            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserProduction");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition );

            return Json(chartData);
        }
        
        public JsonResult GetLaserMachineData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserMachine");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }

        public JsonResult GetPqcProductionData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCProduction");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }

        public JsonResult GetPqcOperatorData()
        {
            Common.SearchingCondition.BaseCondition condition = new Common.SearchingCondition.BaseCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateFrom"]);

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("PQCOperator");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }
        
      

      


        

      
    }
}