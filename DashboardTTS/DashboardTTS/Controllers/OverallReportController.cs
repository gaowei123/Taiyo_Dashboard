using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taiyo.SearchParam;

namespace DashboardTTS.Controllers
{
    public class OverallReportController : Controller
    {
        private readonly ViewBusiness.OverallReport_ViewBusiness vBLL = new ViewBusiness.OverallReport_ViewBusiness();

        #region  view     
        public ActionResult AllSectionInventoryReport()
        {
            return View();
        }
                
        public ActionResult OutputSummaryChart()
        {
            return View();
        }
        
        public ActionResult ProductivityReport()
        {
            return View();
        }
        #endregion

        


        public JsonResult GetAllSectionInventoryReport(string PartNo, string ShipTo, DateTime SearchDay)
        {
            var list = vBLL.GetAllSectionResult(new DateTime(2020, 12, 1), PartNo, ShipTo, SearchDay);
            var result = new List<Common.Class.Model.ProductionInventoryHistory>();


            if (PartNo == "" && ShipTo == "")
            {
                result = list;              
            }
            else if (PartNo != "" && ShipTo == "")
            {
                result = (from a in list where a.PartNumber == PartNo select a).ToList();             
            }
            else if (PartNo == "" && ShipTo != "")
            {
                result = (from a in list where a.ShipTo == ShipTo select a).ToList();
            }
            else
            {
                result = (from a in list where a.ShipTo == ShipTo && a.Model == PartNo select a).ToList();
            }

            return Json(result);
        }


        public JsonResult GetOutputChartData(DateTime DateFrom, DateTime DateTo)
        {
            var bll = new Common.ExtendClass.OverallOutputChart.OverallOutputChart_BLL();
            var result = bll.GetDataList(new BaseParam()
            {
                DateFrom = DateFrom,
                DateTo = DateTo.AddDays(1)
            });

            return result == null ? Json("") : Json(result);
        }

    }
}