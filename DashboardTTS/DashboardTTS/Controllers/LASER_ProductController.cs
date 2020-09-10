using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;

namespace DashboardTTS.Controllers
{
    public class LASER_ProductController : Controller
    {
        private readonly ViewBusiness.LaserProduction vBLL = new ViewBusiness.LaserProduction();
        private JavaScriptSerializer _js = new JavaScriptSerializer();

        
        #region View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SummaryReport()
        {
            return View();
        }
        public ActionResult MachineDailyReport()
        {
            return View();
        }
        #endregion




        


        #region laser summary report
        public JsonResult GetColumn()
        {
            List<ViewModel.LaserSummaryReport_ViewModel.typeColumn> modelList = vBLL.GetTypeColumn();
            return Json(modelList);
        }
        
        public ActionResult GetSummaryData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
            dateTo = dateTo.AddDays(1);
            
            string partNo = Request.Form["PartNo"] == null ? "" : Request.Form["PartNo"].ToString();
            string shift = Request.Form["Shift"] == null ? "" : Request.Form["Shift"].ToString();



            string jsonResult = vBLL.GetSummaryList(dateFrom, dateTo, partNo, shift);
            
            return Content(jsonResult);
        }

        #endregion



        #region Machine Daily Output



        #endregion



    }
}