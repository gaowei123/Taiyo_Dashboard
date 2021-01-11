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
        private readonly Common.ExtendClass.LaserInventory.LaserInventory_BLL _inventoryBLL = new Common.ExtendClass.LaserInventory.LaserInventory_BLL();


        #region View
        public ActionResult SummaryReport()
        {
            return View();
        }
        public ActionResult InventoryReport()
        {
            return View();
        }
        public ActionResult DailyReport()
        {
            return View();
        }
        public ActionResult ProductivityLiveReport()
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


        public JsonResult GetInventoryList()
        {
            return Json(_inventoryBLL.GetInventoryList());
        }



        #region Laesr Daily Report
        //暂时不用
        //public JsonResult GetDailyMain()
        //{
        //    DateTime dDay = DateTime.Parse(Request.Form["Day"]);


        //    Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL bll = new Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL();
        //    return Json(bll.GetMainList(dDay));
        //}


        public JsonResult GetDailyDetailOutput()
        {
            DateTime dDay = DateTime.Parse(Request.Form["Day"]);
            string sMachineID = Request.Form["MachineID"];
            string sShift = Request.Form["Shift"];

            Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL bll = new Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL();
            return Json(bll.GetDetailOutputList(dDay, sShift, sMachineID));
        }

        public JsonResult GetDailyDetailStatus()
        {
            DateTime dDay = DateTime.Parse(Request.Form["Day"]);
            string sMachineID = Request.Form["MachineID"];
            string sShift = Request.Form["Shift"];

            Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL bll = new Common.ExtendClass.LaserDailyReport.LaserDailyReport_BLL();
            return Json(bll.GetDetailStatusList(dDay, sShift, sMachineID));
        }



        #endregion



        public JsonResult GetLiveReport(DateTime DateFrom, DateTime DateTo, string Shift, string Model, string PartNo, string MachineID, string JobNo)
        {
            Taiyo.SearchParam.LaserParam.LaserLiveParam param = new Taiyo.SearchParam.LaserParam.LaserLiveParam();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);
            param.Shift = Shift;
            param.Model = Model;
            param.PartNo = PartNo;
            param.MachineID = MachineID;
            param.JobNo = JobNo;

            Common.ExtendClass.LaserLiveReport.LaserLive_BLL bll = new Common.ExtendClass.LaserLiveReport.LaserLive_BLL();
            var list = bll.GetList(param);
            

            return list == null ? Json("") : Json(list);
        }


    }
}