using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULD_DailyReportController : Controller
    {
        // GET: MOULD_DailyReport
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult GetData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString()).AddDays(1);
            string partNo = Request.Form["PartNo"] == null ? string.Empty : Request.Form["PartNo"].ToString();
            string jigNo = Request.Form["JigNo"] == null ? string.Empty : Request.Form["JigNo"].ToString();
            string shift = Request.Form["Shift"] == null ? string.Empty : Request.Form["Shift"].ToString();


            string jsonResult = "";


            ViewBusiness.MouldingDailyReport_ViewBusiness vBLL = new ViewBusiness.MouldingDailyReport_ViewBusiness();
            List<ViewModel.MouldDailyReport_ViewModel.VIData> viList = vBLL.GetViList(dateFrom, dateTo, partNo, jigNo, shift);
            List<ViewModel.MouldDailyReport_ViewModel.DefectData> defectList = vBLL.GetDefectList(dateFrom, dateTo, partNo, jigNo, shift);

            if (viList == null || defectList == null)
            {
                jsonResult = "";
            }
            else
            {
                var result = from a in viList
                             join b in defectList
                             on new { date = a.date, shift = a.shift, machineID = a.machineID, partNo = a.partNo } equals new { date = b.date, shift = b.shift, machineID = b.machineID, partNo = b.partNo }
                             select new
                             {
                                 date = a.date.ToString("yyyy-MM-dd"),
                                 a.shift,
                                 a.machineID,
                                 a.jigNo,
                                 a.partNo,
                                 a.output,
                                 a.passQty,
                                 a.rejQty,
                                 a.rejRate,
                                 b.whiteDot,
                                 b.scratches,
                                 b.dentedMark,
                                 b.shinningDot,
                                 b.blackMark,
                                 b.sinkMark,
                                 b.flowMark,
                                 b.highGate,
                                 b.silverSteak,
                                 b.blackDot,
                                 b.oilStain,
                                 b.flowLine,
                                 b.overCut,
                                 b.crack,
                                 b.shortMold,
                                 b.stainMark,
                                 b.weldLine,
                                 b.flashes,
                                 b.foreignMaterial,
                                 b.drag,
                                 b.materialBleed,
                                 b.bent,
                                 b.deform,
                                 b.gasMark,
                                 a.opID
                             };


                JavaScriptSerializer js = new JavaScriptSerializer();
                jsonResult = js.Serialize(result);
            }
          

           

            return Content(jsonResult);
        }



    }
}