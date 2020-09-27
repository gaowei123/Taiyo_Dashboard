using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardTTS.Controllers
{
    public class BomController : Controller
    {
        #region view          
        public ActionResult PQCBomList()
        {
            return View();
        }

        public ActionResult PQCBomMenu()
        {
            return View();
        }






        #endregion





        public JsonResult GetBomList()
        {
            return Json("");
        }

        public JsonResult GetBomModel()
        {
            return Json("");
        }

        public JsonResult GetBomDetailList()
        {
            return Json("");
        }

        public JsonResult GetPQCBomDetialList()
        {
            string partNo = Request.Form["PartNo"];

            Common.Class.BLL.PQCBomDetail_BLL bll = new Common.Class.BLL.PQCBomDetail_BLL();
            List<Common.Class.Model.PQCBomDetail_Model> modelList = bll.GetModelList(partNo);

            return Json(modelList);
        }

    }
}