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
        // GET: OverallReport
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AllSectionInventoryReport()
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




    }
}