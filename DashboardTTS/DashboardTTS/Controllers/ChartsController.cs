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

        public ActionResult PQCOperatorChart()
        {
            return View();
        }


        public ActionResult LaserMachineActivityChart()
        {
            return View();
        }
        #endregion



        MyChart.ChartFactory _chartFactory = new MyChart.ChartFactory();
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


        public JsonResult GetLaserActivityData()
        {
            Common.SearchingCondition.LaserActivityCondition condition = new Common.SearchingCondition.LaserActivityCondition();
            condition.DateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            condition.DateTo = DateTime.Parse(Request.Form["DateTo"]).AddDays(1);
            condition.Shift = Request.Form["Shift"];

            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("Activity");
            MyChart.ChartModel chartData = chartProvidor.GetChartData(condition);

            return Json(chartData);
        }


    }
}