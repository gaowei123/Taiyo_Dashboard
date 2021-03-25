using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taiyo.SearchParam;

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

        


        public JsonResult GetAllSectionInventoryReport(string PartNo, string ShipTo, string Model, DateTime SearchDay)
        {
            var result = vBLL.GetAllSectionResult(new DateTime(2020, 12, 1), PartNo,  ShipTo, Model,SearchDay);
            return Json(result);
        }


        public JsonResult GetOutputChartData(DateTime DateFrom, DateTime DateTo)
        {
            var bll = new Common.ExtendClass.OverallOutputChart.OverallOutputChart_BLL();
            var result = bll.GetDataList(new BaseParam()
            {
                DateFrom = DateFrom,
                DateTo = DateTo.AddDays(1)
            });

            return result == null ? Json("") : Json(result);
        }

    }
}