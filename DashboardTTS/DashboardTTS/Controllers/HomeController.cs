using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;

namespace DashboardTTS.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        

        public ActionResult GetMouldStatus()
        {
            Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus = new Common.Class.BLL.MouldingMachineStatus_BLL();
            Dictionary<int,string> dicStatus = MachineStatus.GetCurrentStatus();
            
            return Content(JsonConvert.SerializeObject(dicStatus));
        }

        public ActionResult GetLaserStatus()
        {
            Common.BLL.LMMSEventLog_BLL bll = new Common.BLL.LMMSEventLog_BLL();
            Dictionary<int, string> dicCurStatus = bll.GetCurrentStatus();
            
            
            string result = JsonConvert.SerializeObject(dicCurStatus);

       

            return Content(result);
        }

        public JsonResult GetPQCStatus()
        {
            Common.Class.BLL.PQCQaViTracking_BLL pqcbll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dtTracking = pqcbll.GetRealTime();

            List<UserControl.WebUserControlPQCStatus.UIModel> modelList = new List<UserControl.WebUserControlPQCStatus.UIModel>();
            for (int i = 1; i < 25; i++)
            {
                UserControl.WebUserControlPQCStatus.UIModel model = new UserControl.WebUserControlPQCStatus.UIModel();
                model.Station = "Station " + i.ToString();

                DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + i + "'", " datetime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    model.Status = StaticRes.Global.PQCStatus.Shutdown;                  
                }
                else
                {
                    DataRow dr = drArrTemp[0];
                    model.Status = dr["stopTime"].ToString() == "" ? dr["status"].ToString() : StaticRes.Global.PQCStatus.NoSchedule;
                }

                modelList.Add(model);
            }

            return Json(modelList);
        }
        

    }
}