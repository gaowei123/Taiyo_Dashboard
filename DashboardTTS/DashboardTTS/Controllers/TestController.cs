using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Test_Bootstrap_Table()
        {
            return View();
        }






        public ActionResult GetData()
        {
            try
            {
                var aaa = Request.Form["jobno"];
                var bbb= Request.Form["partno"];

                


                List<ViewModel.test> listModels = new List<ViewModel.test>();

                for (int i = 0; i < 9; i++)
                {
                    ViewModel.test model = new ViewModel.test();


                    model.jobNo = "JOT19000" + i.ToString();
                    model.machineID = i.ToString();
                    model.partNo = "TKS" + i.ToString();
                    model.rate = i;


                    listModels.Add(model);
                }






                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonResult = js.Serialize(listModels);


                return Content(jsonResult);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserVisionSettingList", "btn_Generate_Click exception: " + ee.ToString());
                return null;
            }
        }










    }
}