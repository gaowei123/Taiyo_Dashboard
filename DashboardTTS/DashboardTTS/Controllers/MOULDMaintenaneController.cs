using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULDMaintenaneController : Controller
    {

        private readonly ViewBusiness.MouldingMaintenance_ViewBusines vBLL = new ViewBusiness.MouldingMaintenance_ViewBusines();

        // GET: MOULDMaintenane
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Maintenance()
        {
            return View();
        }





    

        public ActionResult GetTrackingList()
        {
            
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
            dateTo = dateTo.AddDays(1);
        
            string partNo = Request.Form["PartNo"] == null ? "" : Request.Form["PartNo"].ToString();
            string jigNo = Request.Form["JigNo"] == null ? "" : Request.Form["JigNo"].ToString();
            string machineID = Request.Form["MachineID"] == null ? "" : Request.Form["MachineID"].ToString();


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";




            try
            {

                List<ViewModel.MouldMaintenance.Tracking> trackingList = vBLL.GetTrackingList(dateFrom, dateTo, partNo, jigNo, machineID);

                if (trackingList == null || trackingList.Count == 0)
                {
                    jsonResult = js.Serialize("");
                }else
                {
                    jsonResult = js.Serialize(trackingList);
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MOULDMaintenaneController", "GetTrackingList Exception: " + ee.ToString());
            }






            return Content(jsonResult);
        }
        

        public ActionResult UpdateTracking()
        {

            string trackingID = Request.Form["TrackingID"] == null ? "" : Request.Form["TrackingID"].ToString();

            string model = Request.Form["Model"].ToString();
            string partNo = Request.Form["PartNo"].ToString();
            string jigNo = Request.Form["JigNo"].ToString();


            double totalQty = double.Parse(Request.Form["TotalQty"]);
            double setup = double.Parse(Request.Form["Setup"]);
            double wastedMaterial01 = double.Parse(Request.Form["WastedMaterial01"]);
            double wastedMaterial02 = double.Parse(Request.Form["WastedMaterial02"]);
            


            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
            Common.Class.Model.MouldingViHistory_Model viModel = bll.GetModel(trackingID);
            viModel.Model = model;
            viModel.PartNumber = partNo;
            viModel.JigNo = jigNo;
            viModel.AcountReading = totalQty;
            viModel.AcceptQty = totalQty - viModel.RejectQty - viModel.QCNGQTY;
            viModel.Setup = setup;
            viModel.WastageMaterial01 = wastedMaterial01;
            viModel.WastageMaterial02 = wastedMaterial02;
            viModel.LastUpdatedTime = DateTime.Now;



            



         
            bool result = bll.Maintenace(viModel);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(result);


            return Content(jsonResult);
        }


        public ActionResult DeleteTracking()
        {
            string trackingID = Request.Form["TrackingID"] == null ? "" : Request.Form["TrackingID"].ToString();
            

            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
            bool result = bll.DeleteTransaction(trackingID);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(result);

            return Content(jsonResult);
        }







    }
}