using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class PQC_ProductController : Controller
    {
        private readonly ViewBusiness.PQCProduct vBLL = new ViewBusiness.PQCProduct();

        #region View
        // GET: PQC_Product
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



        #endregion





       
        
        //summary report
        public ActionResult GetSummaryData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"] == null ? "" : Request.Form["Shift"].ToString();
            string partNo = Request.Form["PartNo"] == null ? "" : Request.Form["PartNo"].ToString();


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            List<ViewModel.PQCSummaryReport_ViewModel.Report> models = new List<ViewModel.PQCSummaryReport_ViewModel.Report>();
            models = vBLL.GetSummaryList(dateFrom, dateTo, shift, partNo);

            if (models != null && models.Count != 0)
            {
                jsonResult = js.Serialize(models);
            }
            else
            {
                jsonResult = js.Serialize("");
            }



            return Content(jsonResult);
        }



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


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            List<ViewModel.PQCDailyReport_ViewModel> modelList = new List<ViewModel.PQCDailyReport_ViewModel>();

            try
            {
                modelList = vBLL.GetCheckingList(dateFrom, dateTo, shift, partNo, station, pic, type);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetCheckingData exception:" + ee.ToString());
            }

            

            if (modelList != null && modelList.Count != 0)
            {
                jsonResult = js.Serialize(modelList);
            }else
            {
                jsonResult = js.Serialize("");
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


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            List<ViewModel.PQCDailyReport_ViewModel> modelList = new List<ViewModel.PQCDailyReport_ViewModel>();


            modelList = vBLL.GetPackingDailyList(dateFrom, dateTo, shift, partNo, station, pic);

            if (modelList != null && modelList.Count != 0)
            {
                jsonResult = js.Serialize(modelList);
            }
            else
            {
                jsonResult = js.Serialize("");
            }


            return Content(jsonResult);
        }

        #endregion



        #region pqc checking maintenance

        public ActionResult GetJobInfo()
        {

            string trackingID = Request.Form["TrackingID"];
            string jobNo = Request.Form["JobNo"];
           
          


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo model = new ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo();



            try
            {
                model = vBLL.GetJobInfo(trackingID, jobNo);
            } 
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetJobInfo Exception" + ee.ToString());
            }


            if (model != null)
            {
                jsonResult = js.Serialize(model);
            }
            else
            {
                jsonResult = js.Serialize("");
            }


            return Content(jsonResult);
        }

        public ActionResult GetMaterialInfo()
        {
            string trackingID = Request.Form["TrackingID"];
            

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo>();
            
            try
            {
                modelList = vBLL.GetMaterialList(trackingID);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetMaterialInfo Exception" + ee.ToString());
            }


            if (modelList != null)
            {
                jsonResult = js.Serialize(modelList);
            }
            else
            {
                jsonResult = js.Serialize("");
            }


            return Content(jsonResult);
        }
        
        public ActionResult GetDefectInfo()
        {
            string trackingID = Request.Form["TrackingID"];
            string materialNo = Request.Form["MaterialNo"];

            string tabSN = Request.Form["TabSN"];


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo>();

            try
            {
                modelList = vBLL.GetDefectInfo(trackingID, materialNo);
                modelList.ForEach(p => p.tabSN = tabSN);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetDefectInfo Exception" + ee.ToString());
            }


            if (modelList != null)
            {
                jsonResult = js.Serialize(modelList);
            }
            else
            {
                jsonResult = js.Serialize("");
            }


            return Content(jsonResult);
        }



        public ActionResult EndJob()
        {
            string trackingID = Request.Form["TrackingID"];
            bool complete = bool.Parse(Request.Form["Complete"]);


            bool updateResult = false;

            try
            {
                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                updateResult = bll.MaintenanceUpdateEndFlag(trackingID, complete);   
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "GetJobInfo Exception" + ee.ToString());
            }





            JavaScriptSerializer js = new JavaScriptSerializer();


            return Content(js.Serialize(updateResult));
        }
        
        public ActionResult UpdateQty()
        {
            
            bool updateResult = false;




            try
            {
                updateResult = vBLL.UpdateQty(Request.Form);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQC_ProductController", "UpdateQty Exception" + ee.ToString());
            }



            JavaScriptSerializer js = new JavaScriptSerializer();

            return Content(js.Serialize(updateResult));
        }

        #endregion



        #region WIP Inventory Report 
        public ActionResult GetWIPInventory()
        {
            string partNo = Request.Form["PartNo"] == null ? "" : Request.Form["PartNo"].ToString();
            //string model = Request.Form["Model"] == null ? "" : Request.Form["Model"].ToString();
            
            string result = vBLL.GetWIPInventory(partNo);
            
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


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";

            if (modelList != null)
            {
                jsonResult = js.Serialize(modelList);
            }
            else
            {
                jsonResult = js.Serialize("");
            }


            return Content(jsonResult);
        }
        
        public ActionResult DeleteWIPJob()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
        
            if (Request.Form["JobNo"] == null || Request.Form["JobNo"] == "")
                return Content(js.Serialize("Job No Can not be empty!"));


            Common.Class.BLL.PQCInventory_BLL bll = new Common.Class.BLL.PQCInventory_BLL();
            bool result =  bll.Delete(Request.Form["JobNo"]);


            return Content(js.Serialize(result));
        }

        #endregion
        


        //new,  packing detail report 
        public ActionResult GetPackingDetailList()
        {

            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);

            string pic = Request.Form["PIC"];
            string station = Request.Form["Station"];
            string jobNo = Request.Form["JobNo"];


            List<ViewModel.PackingDetail_ViewModel> modelList = vBLL.GetPackingList(dateFrom, dateTo, pic, station, jobNo);



            JavaScriptSerializer js = new JavaScriptSerializer();

            if (modelList == null)
            {
                return Content(js.Serialize(""));
            }else
            {
                return Content(js.Serialize(modelList));
            }
        }



        //new, checking detail report 
        public ActionResult GetCheckingDetailList()
        {
            return Content("");
        }
        


        #region Packing Inventory

        //inventory 
        public ActionResult GetPackingInventoryList()
        {
            return Content("");
        }



        //job order 
        public ActionResult GetPackingJobOrderList()
        {
            return Content("");
        }



        //delete function


        


        #endregion
        
    }
}