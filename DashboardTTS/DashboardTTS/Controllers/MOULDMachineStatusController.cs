using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULDMachineStatusController : Controller
    {
        private readonly ViewBusiness.MOULDMachineStatus vBLL = new ViewBusiness.MOULDMachineStatus();


        #region view
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MachineSummaryReport()
        {
            return View();
        }

        public ActionResult MachineTimeBar()
        {
            return View();
        }

        public ActionResult MachineUtilizationChart()
        {
            return View();
        }

        #endregion



        public ActionResult GetMachineSummaryList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string shift = Request.Form["Shift"];
            string dateNotIn = Request.Form["DateNotIn"];
            bool exceptWeekend = bool.Parse(Request.Form["ExceptWeekend"]);
            


            List<ViewModel.MouldMachineStatus.Summary> modelList = vBLL.GetMachineSummaryList(dateFrom, dateTo, shift, dateNotIn, exceptWeekend);

            JavaScriptSerializer js = new JavaScriptSerializer();

        


            return Content(js.Serialize(modelList));
        }




        public ActionResult GetMachineSummaryChart()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string shift = Request.Form["Shift"];
            string dateNotIn = Request.Form["DateNotIn"];
            bool exceptWeekend = bool.Parse(Request.Form["ExceptWeekend"]);



            List<ViewModel.MouldMachineStatus.SummaryChart> modelList = vBLL.GetMachineSummaryChart(dateFrom, dateTo, shift, dateNotIn, exceptWeekend);

            JavaScriptSerializer js = new JavaScriptSerializer();




            return Content(js.Serialize(modelList));
        }



        public ActionResult GetStatusChartData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"];
     



            List<ViewModel.MouldMachineStatus.StatusChart> modelList = vBLL.GetStatusChart(dateFrom, dateTo, shift);

            JavaScriptSerializer js = new JavaScriptSerializer();




            return Content(js.Serialize(modelList));
        }


        public ActionResult GetTimeBarData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);




            Common.Class.BLL.MouldingMachineStatus_BLL bll = new Common.Class.BLL.MouldingMachineStatus_BLL();
            List<Common.Class.Model.MouldingMachineStatus_Model> modelList = bll.GetModelList(dateFrom, dateTo, "", "1", "");


            JavaScriptSerializer js = new JavaScriptSerializer();

            return Content(js.Serialize(modelList));
        }









    }
}