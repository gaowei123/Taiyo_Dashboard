using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULDMaterialController : Controller
    {
        private readonly ViewBusiness.MOULDMaterial vBLL = new ViewBusiness.MOULDMaterial();
        
        public ActionResult Traceability()
        {
            return View();
        }
        

        public JsonResult GetMaterialTraceability(DateTime DateFrom, DateTime DateTo, string machineID)
        {
            List<ViewModel.MouldMaterialTraceability> modelList = vBLL.GetMaterialTraceability(DateFrom, DateTo.AddDays(1), machineID);
            return Json(modelList);
        }

    }
}