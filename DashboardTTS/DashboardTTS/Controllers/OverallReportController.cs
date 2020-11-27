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
        public ActionResult AllSectionInventoryReport()
        {
            return View();
        }
                
        public ActionResult OutputSummaryChart()
        {
            return View();
        }
        
        public ActionResult ProductivityReport()
        {
            return View();
        }
        
        #endregion

        


        public ActionResult GetAllSectionInventoryReport(string PartNo, string ShipTo)
        {
            string result = vBLL.GetAllSectionResult(new DateTime(2020, 11, 25), PartNo, ShipTo);
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