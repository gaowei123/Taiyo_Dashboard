using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULDProductController : Controller
    {
        private readonly ViewBusiness.MOULDProduct_ViewBusiness vBLL = new ViewBusiness.MOULDProduct_ViewBusiness();

        #region view
        // GET: MOULDProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DailyVerifyReport()
        {
            return View();
        }


        public ActionResult RejectionDetailReport()
        {
            return View();
        }



        #endregion






        #region Daily Report 
        public ActionResult GetDailyVerifyPCS()
        {
            DateTime date = DateTime.Parse(Request.Form["Date"]);
            string shift = Request.Form["Shift"];
            

            string result = vBLL.GetDailyVerifyReportPCS(date, shift);
            
            return Content(result);
        }
                
        public ActionResult GetDailyVerifySET()
        {
            DateTime date = DateTime.Parse(Request.Form["Date"]);
            string shift = Request.Form["Shift"];


            string result = vBLL.GetDailyVerifyReportSET(date, shift);

            return Content(result);
        }


        public ActionResult GetSummary()
        {
            DateTime date = DateTime.Parse(Request.Form["Date"]);


         
            JavaScriptSerializer js = new JavaScriptSerializer();
             

            List<ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport> summaryList    = vBLL.GetSummaryReport(date);

            if (summaryList == null)
            {
                return Content(js.Serialize(""));
            }
            else
            {
                return Content(js.Serialize(summaryList));
            }
        }




        //用defectInfo model的各个defect code, 赋值0/1,    0:不显示, 1显示.
        public ActionResult GetDefectDisplayFlag()
        {
            DateTime date = DateTime.Parse(Request.Form["Date"]);
     

            ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo model = new ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo();
            model = vBLL.GetDefectDisplayFlag(date);


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(model);

            return Content(jsonResult);
        }


        public ActionResult SetVerifyReport()
        {

            DateTime date = DateTime.Parse(Request.Form["Date"]);
            string shift = Request.Form["Shift"];
            string user = Request.Form["User"];

            Common.Class.BLL.MouldingCheckReport_BLL bll = new Common.Class.BLL.MouldingCheckReport_BLL();

            Common.Class.Model.MouldingCheckReport_Model model = new Common.Class.Model.MouldingCheckReport_Model();


            model.Report = shift;
            model.Verify_User = user;
            model.refField01 = date;
            model.Verify_Time = DateTime.Now;


            bool result = bll.Add(model);

            JavaScriptSerializer js = new JavaScriptSerializer();

            return Content(js.Serialize(result));
        }
        #endregion


        //Prod Rjection Detial
        public ActionResult GetRejTimeDetail()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string shift = Request.Form["Shift"];
            string machineID = Request.Form["MachineID"];
            string partNo = Request.Form["PartNo"];
            string defectCode = Request.Form["DefectCode"];



            JavaScriptSerializer js = new JavaScriptSerializer();


            List<ViewModel.MouldRejTimeDetail> modelList = vBLL.GetRejTimeDetail(dateFrom,dateTo, shift, machineID, partNo, defectCode);

            if (modelList == null)
            {
                return Content(js.Serialize(""));
            }
            else
            {
                return Content(js.Serialize(modelList));
            }
        }





    }
}