using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class TestController : Controller
    {


        #region view
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Test_Bootstrap_Table()
        {
            return View();
        }


        public ActionResult ChartIndex()
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


        public ActionResult PQCOperatorChart()
        {
            return View();
        }

        public ActionResult PQCProductionChart()
        {
            return View();
        }


        #endregion








        public JsonResult GetData()
        {
            var aaa = Request.Form["jobno"];
            var bbb = Request.Form["partno"];




            List<ViewModel.test> listModels = new List<ViewModel.test>();

            for (int i = 0; i < 9; i++)
            {
                ViewModel.test model = new ViewModel.test();


                model.jobNo = "JOT19000" + i.ToString();
                model.machineID = i.ToString();
                model.partNo = "TKS" + i.ToString();
                model.rate = i;


                listModels.Add(model);
            }

            return Json(listModels);
        }









        MyChart.ChartFactory _chartFactory = new MyChart.ChartFactory();
        JavaScriptSerializer _js = new JavaScriptSerializer();


        public ActionResult TestLaserMachineChart()
        {
            MyChart.SearchingCondition.LaserMachineCondition cond = new MyChart.SearchingCondition.LaserMachineCondition();
            cond.DateFrom = DateTime.Now;
            cond.DateTo = DateTime.Now.AddDays(1);
            cond.MachineID = "1";
            cond.PartNo = "TKS908";


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserMachine");
            MyChart.ChartModel chart = chartProvidor.GetChartData(cond);
            
            
            return Content(_js.Serialize(chart));
        }


        public ActionResult TestLaserProductionChart()
        {
            MyChart.SearchingCondition.LaserMachineCondition cond = new MyChart.SearchingCondition.LaserMachineCondition();
            cond.DateFrom = DateTime.Now;
            cond.DateTo = DateTime.Now.AddDays(1);
            cond.MachineID = "1";
            cond.PartNo = "TKS908";


            MyChart.IChartMethod chartProvidor = _chartFactory.CreateInstance("LaserProduction");
            MyChart.ChartModel chart = chartProvidor.GetChartData(cond);
      

            return Content(_js.Serialize(chart));
        }


        

    }
}