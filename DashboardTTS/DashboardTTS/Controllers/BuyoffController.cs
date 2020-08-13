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



        public ActionResult GetMouldDefect()
        {
            string jobID = Request.Form["JobID"];
            string trackingID = Request.Form["TrackingID"];


            List<ViewModel.BuyoffReport_ViewModel.MouldDefect> mouldDefectList = vbBuyoff.GetMaterialMouldDefectList(jobID, trackingID);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            if (mouldDefectList == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(mouldDefectList);
            }



            return Content(jsonResult);
        }
        
        public ActionResult GetPaintDefect()
        {
            string jobID = Request.Form["JobID"];
            string trackingID = Request.Form["TrackingID"];



            DBHelp.Reports.LogFile.Log("TouchPCBuyoff", string.Format("GetPaintDefect, receive para JobID:{0},trackingID:{1}", jobID, trackingID));

            List<ViewModel.BuyoffReport_ViewModel.PaintDefect> paintDefectList = vbBuyoff.GetMaterialPaintDefectList(jobID, trackingID);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            if (paintDefectList == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(paintDefectList);
            }
            



            return Content(jsonResult);
        }
        
        public ActionResult GetLaserDefect()
        {
            string jobID = Request.Form["JobID"];
            string trackingID = Request.Form["TrackingID"];


            List<ViewModel.BuyoffReport_ViewModel.LaserDefect> laserDefectList = vbBuyoff.GetMaterialLaserDefectList(jobID, trackingID);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            if (laserDefectList == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(laserDefectList);
            }



            return Content(jsonResult);
        }



        public ActionResult GetOthersDefect()
        {



            string jobID = Request.Form["JobID"];
            string trackingID = Request.Form["TrackingID"];

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";




            List<ViewModel.BuyoffReport_ViewModel.OthersDefect> othersDefectList = vbBuyoff.GetMaterialOthersDefectList(jobID, trackingID);

          

            if (othersDefectList == null)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                jsonResult = js.Serialize(othersDefectList);
            }



            return Content(jsonResult);
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