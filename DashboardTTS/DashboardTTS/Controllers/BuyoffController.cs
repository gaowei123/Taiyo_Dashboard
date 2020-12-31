using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class BuyoffController : Controller
    {
        private readonly ViewBusiness.Buyoff_ViewBusiness vbBuyoff = new ViewBusiness.Buyoff_ViewBusiness();

        #region view
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OverallBuyoff()
        {
            return View();
        }
        
        public ActionResult TouchPCBuyoff()
        {
            return View();
        }

        public ActionResult TouchPCBuyoffWithoutCurrent()
        {
            return View();
        }
        
        public ActionResult PQCBuyoffList()
        {
            return View();
        }
        #endregion





        public ActionResult GetCheckingDate()
        {
            string jobNo = Request.Form["JobNo"];

            Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
            DateTime day = trackingBLL.GetCheckingDateByJob(jobNo);


            return Content(day.ToString("yyyy-MM-dd"));
        }


        public ActionResult GetJobList()
        {
            Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();

            JavaScriptSerializer js = new JavaScriptSerializer();

            DateTime? day = DateTime.Parse(Request.Form["Date"]);

            List<string> jobList = new List<string>();


            jobList = trackingBLL.GetBuyoffJobList(day);

            string jsonResult = "";

            if (jobList == null)
            {
                jsonResult = js.Serialize("");

            }
            else{
                jsonResult = js.Serialize(jobList);
            }



            return Content(jsonResult);
        }


        public ActionResult GetLaserBuyoffRecord()
        {
            string jobno = Request.Form["JobNo"];


            ViewModel.BuyoffReport_ViewModel.LaserRecord model = vbBuyoff.GetLaserModel(jobno);


            string jsonResult = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            if (model == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(model);
            }


            return Content(jsonResult);
        }


        public ActionResult GetLaserParameter()
        {
            string jobno = Request.Form["JobNo"];


            ViewModel.BuyoffReport_ViewModel.LaserParameter model = vbBuyoff.GetLaserParameter(jobno);


            string jsonResult = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            if (model == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(model);
            }


            return Content(jsonResult);
        }



        public JsonResult GetMouldDefect(string JobID, string TrackingID, string CheckProcess, bool IsExcludeTracking)
        {
            List<ViewModel.BuyoffReport_ViewModel.MouldDefect> mouldDefectList = vbBuyoff.GetMaterialMouldDefectList(JobID, TrackingID, CheckProcess,IsExcludeTracking);
            return mouldDefectList == null ? Json(""): Json(mouldDefectList);
        }
        public JsonResult GetPaintDefect(string JobID, string TrackingID, string CheckProcess, bool IsExcludeTracking)
        {
            List<ViewModel.BuyoffReport_ViewModel.PaintDefect> paintDefectList = vbBuyoff.GetMaterialPaintDefectList(JobID, TrackingID, CheckProcess, IsExcludeTracking);
            return paintDefectList == null ? Json("") : Json(paintDefectList);
        }
        public ActionResult GetLaserDefect(string JobID, string TrackingID, string CheckProcess, bool IsExcludeTracking)
        {
            List<ViewModel.BuyoffReport_ViewModel.LaserDefect> laserDefectList = vbBuyoff.GetMaterialLaserDefectList(JobID, TrackingID, CheckProcess, IsExcludeTracking);
            return laserDefectList == null ? Json("") : Json(laserDefectList);
        }
        public ActionResult GetOthersDefect(string JobID, string TrackingID, string CheckProcess, bool IsExcludeTracking)
        {
            List<ViewModel.BuyoffReport_ViewModel.OthersDefect> othersDefectList = vbBuyoff.GetMaterialOthersDefectList(JobID, TrackingID, CheckProcess, IsExcludeTracking);
            return othersDefectList == null ? Json("") : Json(othersDefectList);
        }



        public ActionResult GetPQCBuyoffList()
        {
            
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);

            dateTo = dateTo.AddDays(1);

            string partNo = Request.Form["PartNo"];
            string jobID = Request.Form["JobNo"];



            List<ViewModel.BuyoffReport_ViewModel.PQCBuyoffList> buyoffList = new List<ViewModel.BuyoffReport_ViewModel.PQCBuyoffList>();
            buyoffList = vbBuyoff.GetPQCBuyoffList(dateFrom, dateTo, partNo, jobID);


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            if (buyoffList == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(buyoffList);
            }



            return Content(jsonResult);
        }




    }
}