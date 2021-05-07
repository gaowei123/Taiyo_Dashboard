using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taiyo.SearchParam.PQCParam;

namespace DashboardTTS.Controllers
{
    public class PQC_ProductController : Controller
    {
        private readonly ViewBusiness.PQCProduct vBLL = new ViewBusiness.PQCProduct();

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
        public ActionResult DailyReport()
        {
            return View();
        }
        public ActionResult WIPInventory()
        {
            return View();
        }
        public ActionResult JobOrderDetail()
        {
            return View();
        }
        public ActionResult Maintenance()
        {
            return View();
        }
        public ActionResult PackingDetailReport()
        {
            return View();
        }
        public ActionResult CheckingDetailReport()
        {
            return View();
        }
        public ActionResult PackingInventory()
        {
            return View();
        }
        public ActionResult PackingJobOrder()
        {
            return View();
        }
        public ActionResult PackingPicChart()
        {
            return View();
        }
        public ActionResult PackingProductionTrendChart()
        {
            return View();
        }
        public ActionResult PICDailyOutputReport()
        {
            return View();
        }
        public ActionResult CheckingLiveReport()
        {
            return View();
        }
        public ActionResult ScrapReport()
        {
            return View();
        }
        #endregion
        


        #region summary report  2021/1/21 new logic 
        public JsonResult GetSummaryCheckingList(DateTime DateFrom, DateTime DateTo, string Shift, string PartNo)
        {
            PQCSummaryParam param = new PQCSummaryParam();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);
            param.Shift = Shift;
            param.PartNo = PartNo;

            Common.ExtendClass.PQCSummaryReport.Summary_BLL bll = new Common.ExtendClass.PQCSummaryReport.Summary_BLL();
            var result = bll.GetCheckingList(param);

            return result == null ? Json("") : Json(result);
        }
        public JsonResult GetSummaryPackingList(DateTime DateFrom, DateTime DateTo, string Shift, string PartNo)
        {
            PQCSummaryParam param = new PQCSummaryParam();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);
            param.Shift = Shift;
            param.PartNo = PartNo;

            Common.ExtendClass.PQCSummaryReport.Summary_BLL bll = new Common.ExtendClass.PQCSummaryReport.Summary_BLL();
            var result = bll.GetPackingList(param);

            return result == null ? Json("") : Json(result);
        }
        #endregion



        #region daily report
        public ActionResult GetCheckingData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
            string pic = Request.Form["PIC"];
            string jobNo = Request.Form["JobNo"];
            string type = Request.Form["Type"];


          
            string jsonResult = "";


            List<ViewModel.PQCDailyReport_ViewModel> modelList = new List<ViewModel.PQCDailyReport_ViewModel>();

            try
            {
                modelList = vBLL.GetCheckingDailyList(dateFrom, dateTo, shift, partNo, station, pic, type);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetCheckingData exception:" + ee.ToString());
            }

            

            if (modelList != null && modelList.Count != 0)
            {
                jsonResult = _js.Serialize(modelList);
            }else
            {
                jsonResult = _js.Serialize("");
            }


            return Content(jsonResult);
        }
        
        public ActionResult GetPackingData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
            string jobNo = Request.Form["JobNo"];
            string pic = Request.Form["PIC"];


         
            string jsonResult = "";


            List<ViewModel.PQCDailyReport_ViewModel> modelList = new List<ViewModel.PQCDailyReport_ViewModel>();


            modelList = vBLL.GetPackingDailyList(dateFrom, dateTo, shift, partNo, station, pic);

            if (modelList != null && modelList.Count != 0)
            {
                jsonResult = _js.Serialize(modelList);
            }
            else
            {
                jsonResult = _js.Serialize("");
            }


            return Content(jsonResult);
        }

        #endregion



        #region pqc checking maintenance

        public JsonResult GetJobInfo(string TrackingID, string JobNo)
        {
            var model = new ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo();
            
            try
            {
                model = vBLL.GetJobInfo(TrackingID, JobNo);
            } 
            catch (Exception ee)
            {
                model = null;
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetJobInfo Exception" + ee.ToString());               
            }

            return model == null ? Json("") : Json(model);
        }

        public JsonResult GetMaterialInfo(string TrackingID, string JobNo)
        {
            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo>();
            
            try
            {
                modelList = vBLL.GetMaterialList(TrackingID, JobNo);
            }
            catch (Exception ee)
            {
                modelList = null;
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetMaterialInfo Exception" + ee.ToString());
            }

            return modelList == null ? Json("") : Json(modelList);
        }
        
        public ActionResult GetDefectInfo(string TrackingID, string MaterialNo, string TabSN)
        {
            List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo>();

            try
            {
                modelList = vBLL.GetDefectInfo(TrackingID, MaterialNo);
                modelList.ForEach(p => p.tabSN = TabSN);
            }
            catch (Exception ee)
            {
                modelList = null;
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetDefectInfo Exception" + ee.ToString());
            }

            return modelList == null ? Json("") : Json(modelList);
        }



        public JsonResult EndJob(string TrackingID, bool Complete)
        {
            bool updateResult;

            try
            {
                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                updateResult = bll.MaintenanceUpdateEndFlag(TrackingID, Complete);   
            }
            catch (Exception ee)
            {
                updateResult = false;
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetJobInfo Exception" + ee.ToString());
            }

            return Json(updateResult);
        }
        
        public JsonResult UpdateQty()
        {
            bool updateResult;
            
            try
            {
                updateResult = vBLL.UpdateQty(Request.Form);
            }
            catch (Exception ee)
            {
                updateResult = false;
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "UpdateQty Exception" + ee.ToString());
            }

            return Json(updateResult);
        }

        #endregion



        #region WIP Inventory Report 
        public ActionResult GetWIPInventory()
        {            
            string result = vBLL.GetWIPInventory();
            
            return Content(result);
        }
        
        public ActionResult GetJobOrderDetailList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
            dateTo = dateTo.AddDays(1);

            string partNo = Request.Form["PartNo"].ToString();
            string model = "";
            string jobNo = Request.Form["JobNo"].ToString();
            string jobStatus = Request.Form["Status"].ToString();
            



            List<ViewModel.PQCWIPInventory_ViewModel> modelList = vBLL.GetJobOrderDetailList(dateFrom, dateTo, partNo, model, jobNo, jobStatus);


          
            string jsonResult = "";

            if (modelList != null)
            {
                jsonResult = _js.Serialize(modelList);
            }
            else
            {
                jsonResult = _js.Serialize("");
            }


            return Content(jsonResult);
        }
        
        public ActionResult DeleteWIPJob()
        {
            if (Request.Form["JobNo"] == null || Request.Form["JobNo"] == "")
                return Content(_js.Serialize("Job No Can not be empty!"));


            Common.Class.BLL.PQCInventory_BLL bll = new Common.Class.BLL.PQCInventory_BLL();
            bool result =  bll.Delete(Request.Form["JobNo"]);


            return Content(_js.Serialize(result));
        }

        #endregion
        


        //new,  packing detail report 
        public ActionResult GetPackingDetailList()
        {

            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string pic = Request.Form["PIC"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
            string jobNo = Request.Form["JobNo"];
            string lotNo = Request.Form["LotNo"];
            string type = Request.Form["Type"];


            List<ViewModel.PackingDetail_ViewModel> modelList = vBLL.GetPackingDetailList(dateFrom, dateTo, partNo, station, pic, jobNo, lotNo, type);



          
            if (modelList == null)
            {
                return Content(_js.Serialize(""));
            }else
            {
                return Content(_js.Serialize(modelList));
            }
        }



        //new, checking detail report 
        public ActionResult GetCheckingDetailList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string pic = Request.Form["PIC"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
            string jobNo = Request.Form["JobNo"];
            string lotNo = Request.Form["LotNo"];
            string type = Request.Form["Type"];


            List<ViewModel.CheckingDetail_ViewModel> modelList = vBLL.GetCheckingDetailList(dateFrom, dateTo, partNo, station, pic, jobNo, lotNo, type);



        

            if (modelList == null)
            {
                return Content(_js.Serialize(""));
            }
            else
            {
                return Content(_js.Serialize(modelList));
            }
        }

       
        public ActionResult GetPicList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string pic = Request.Form["PIC"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
            string jobNo = Request.Form["JobNo"];
            string type = Request.Form["Type"];


            string result = vBLL.GetPicList(dateFrom, dateTo, partNo, station, pic, jobNo, type);
            return Content(result);
        }
        
       
        public ActionResult GetProductTrendList()
        {
            string groupBy = Request.Form["GroupBy"];

            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();
            string year = Request.Form["Year"];
            if (groupBy == "Day")
            {
                dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
                dateTo = DateTime.Parse(Request.Form["DateTo"]);
                dateTo = dateTo.AddDays(1);
            }
            else if (groupBy == "Month")
            {
                dateFrom = DateTime.Parse(year + "-1-1");
                dateTo = dateFrom.AddYears(1);
            }
            else
            {
                dateFrom = DateTime.Parse("2019-1-1");
                dateTo = DateTime.Now;
            }


            string type = Request.Form["Type"];
            string pic = Request.Form["PIC"];
            string partNo = Request.Form["PartNo"];
            string station = Request.Form["Station"];
           

            



            string result = vBLL.GetProductTrendList(groupBy, dateFrom, dateTo, partNo, station, pic, type);

            return Content(result);
        }



        // 2021/1/22 new logic, Operator Daily Output Report
        public JsonResult GetDailyOperatorList(DateTime Date, string Shift, string UserID)
        {
            var bll = new Common.ExtendClass.PQCProduction.OperatorDailyOutputReport.BLL();
            var result = bll.GetReportList(new PQCOutputParam()
            {
                DateFrom = Date,
                DateTo = Date.AddDays(1),
                Shift = Shift,
                OpID = UserID
            });

            return result == null ? Json("") : Json(result);
        }



        #region Packing Inventory

        public ActionResult GetPackingInventoryList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            string partNo = Request.Form["PartNo"];


            string result = vBLL.GetPackingInventory(dateFrom, partNo);
            return Content(result);
        }



        public ActionResult GetPackingJobOrderList()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string partNo = Request.Form["PartNo"];
            string jobNo = Request.Form["JobNo"];


            string result = vBLL.GetPackingInventoryDetail(dateFrom,dateTo, partNo, jobNo);

            return Content(result);
        }

        public JsonResult DeletePackInventory()
        {
            string jobNo = Request.Form["JobNo"];


            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            bool result = bll.DeleteJobRollBack(jobNo);


            return Json(result);
        }




        #endregion


        /// <summary>
        /// 2021/3/2,重写checking live report
        /// 从原本通过sqlserver opendatasource来跨库查询
        /// 现在拆分成分别单独查询paint, pqc的数据,在代码中合并.
        /// 提高查询效率
        /// </summary>
        public JsonResult GetCheckingLiveList(DateTime DateFrom, DateTime DateTo, string Shift, string PartNo, string Station, string JobNo, string LotNo )
        {
            var bll = new Common.ExtendClass.PQCProduction.CheckingLiveReport.BLL();
            var result = bll.GetReportList(new PQCOutputParam() {
                DateFrom = DateFrom,
                DateTo = DateTo.AddDays(1),
                Shift = Shift,
                PartNo = PartNo,
                MachineID = Station,
                JobNo = JobNo}, LotNo);

            return Json(result);
        }



        public JsonResult GetScrapList(DateTime DateFrom, DateTime DateTo, string Shift, string PartNo, string JobNo)
        {
            Common.ExtendClass.PQCProduction.ScrapReport.Scrap_BLL bll = new Common.ExtendClass.PQCProduction.ScrapReport.Scrap_BLL();
            var result = bll.GetScrapList(new PQCOutputParam()
            {
                DateFrom = DateFrom,
                DateTo = DateTo.AddDays(1),
                Shift = Shift,
                PartNo = PartNo,
                JobNo = JobNo
            });

            return result == null ? Json("") : Json(result);
        }

    }
}