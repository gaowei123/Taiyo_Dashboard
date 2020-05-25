using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class LASER_ProductController : Controller
    {
        private readonly ViewBusiness.LaserProduction vBLL = new ViewBusiness.LaserProduction();


        #region View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SummaryReport()
        {
            return View();
        }
        
        #endregion






        #region data api

        public ActionResult GetSummaryData()
        {

            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
            dateTo = dateTo.AddDays(1);
            
            string partNo = Request.Form["PartNo"] == null ? "" : Request.Form["PartNo"].ToString();
            string shift = Request.Form["Shift"] == null ? "" : Request.Form["Shift"].ToString();



            string jsonResult = "";

            List<ViewModel.LaserSummaryReport_ViewModel> models = new List<ViewModel.LaserSummaryReport_ViewModel>();
            models = vBLL.GetSummaryList(dateFrom, dateTo, partNo, shift);

            if (models != null && models.Count != 0)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                jsonResult = js.Serialize(models);
            }

          

            return Content(jsonResult);
        }



        #endregion




    }
}