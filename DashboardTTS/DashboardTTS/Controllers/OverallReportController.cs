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
            //各部门都以6-1开始计算
            DateTime dStartTime = DateTime.Parse("2020-6-1");

            string result = vBLL.GetAllSectionList(dStartTime);
            
            return Content(result);
        }




    }
}