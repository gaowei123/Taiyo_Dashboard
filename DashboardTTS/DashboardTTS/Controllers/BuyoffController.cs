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

        
        // 获取选择日期下所有的job, (nextviflag == true)
        public JsonResult GetJobList(DateTime? Date)
        {
            Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
            List<string>  jobList = trackingBLL.GetBuyoffJobList(Date);

            return jobList == null ? Json("") : Json(jobList);
        }
        
        public JsonResult GetLaserBuyoffRecord(string JobNo)
        {
            var model = vbBuyoff.GetLaserModel(JobNo);
            return model == null ? Json("") : Json(model);            
        }
        
        public JsonResult GetLaserParameter(string JobNo)
        {
            var model = vbBuyoff.GetLaserParameter(JobNo);
            return model == null ? Json("") : Json(model);
        }



        public JsonResult GetMouldDefect(string JobID, string TrackingID, string CheckProcess, bool IsExcludeTracking)
        {
            DBHelp.Reports.LogFile.Log("BuyoffController_Debug", $"JobID:{JobID},TrackingID:{TrackingID},CheckProcess:{CheckProcess},IsExcludeTracking:{IsExcludeTracking}");
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