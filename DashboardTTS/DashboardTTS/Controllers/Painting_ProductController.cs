using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardTTS.Controllers
{
    public class Painting_ProductController : Controller
    {
        // GET: Painting_Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeliveryRecord()
        {
            return View();
        }







        public JsonResult GetRecordList(DateTime DateFrom, DateTime DateTo, string PartNo, string SendingTo, string JobNo)
        {
            Taiyo.SearchParam.PaintingParam.DeliveryRecordParam param = new Taiyo.SearchParam.PaintingParam.DeliveryRecordParam();
            param.DateFrom = DateFrom;
            param.DateTo = DateTo.AddDays(1);
            param.PartNo = PartNo;
            param.SendingTo = SendingTo;
            param.JobNo = JobNo;

            Common.ExtendClass.PaintingRecord.BLL bll = new Common.ExtendClass.PaintingRecord.BLL();
            var result = bll.GetList(param);
            

            return result == null ? Json("") : Json(result);
        }


    }
}