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
        

        #region view 
        // GET: MOULDMaterial
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Traceability()
        {
            return View();
        }

        #endregion





        public JsonResult GetMaterialTraceability()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string machineID = Request.Form["machineID"];
            
            List<ViewModel.MouldMaterialTraceability> modelList = vBLL.GetMaterialTraceability(dateFrom, dateTo, machineID);

        
            return Json(modelList);
        }
        

    }
}