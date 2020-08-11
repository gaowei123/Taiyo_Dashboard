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

        public ActionResult LaserMaintenance()
        {
            return View();
        }

        #endregion




        


        #region laser summary report


        public ActionResult GetColumn()
        {
            List<ViewModel.LaserSummaryReport_ViewModel.typeColumn> modelList = new List<ViewModel.LaserSummaryReport_ViewModel.typeColumn>();

            modelList = vBLL.GetTypeColumn();


            string jsonResult = "";


            if (modelList == null || modelList.Count == 0)
                jsonResult = _js.Serialize("");
            else
                jsonResult = _js.Serialize(modelList);


            return Content(jsonResult);
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



        #region Laser Maintenance 

        public ActionResult GetMaintainJobInfo()
        {
            DateTime day = DateTime.Parse(Request.Form["Day"].ToString());
            string shift = Request.Form["Shift"];
            string machineID = Request.Form["MachineID"];
            string jobID = Request.Form["JobID"];

            DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[GetMaintainJobInfo] receive job info --  jobno:{0}, day:{1}, shift:{2}, machineID:{3}", jobID, day.ToString("yyyy-MM-dd"), shift, machineID));



          

            ViewModel.LaserMaintenance_ViewModel model = vBLL.GetMaintainJobInfo(day, shift, machineID, jobID);

            if (model == null)
            {
                return Content(_js.Serialize(""));
            }
            else
            {
                return Content(_js.Serialize(model));
            }
        }
        

        #endregion






    }
}