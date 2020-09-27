using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class OverallReportController : Controller
    {
        private readonly ViewBusiness.OverallReport_ViewBusiness vBLL = new ViewBusiness.OverallReport_ViewBusiness();

        #region  view 
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AllSectionInventoryReport()
        {
            return View();
        }
                
        public ActionResult OutputSummaryChart()
        {
            return View();
        }
        #endregion

        


        public ActionResult GetAllSectionInventoryReport()
        {
            //全部从6-1号开始计算.

            string model = Request.Form["Model"];
            string shipTo = Request.Form["ShipTo"];

            DateTime startTime = DateTime.Parse("2020-6-1");



            string result = vBLL.GetAllSectionList(startTime, model, shipTo);
            return Content(result);
        }


        public JsonResult GetOutputChartData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"];

            Common.ExtendClass.OverallOutputChart.OverallOutputChart_BLL bll = new Common.ExtendClass.OverallOutputChart.OverallOutputChart_BLL();            
            var  result = bll.GetDataList(dateFrom, dateTo, shift);

            
            return Json(result);
        }

        



    }
}