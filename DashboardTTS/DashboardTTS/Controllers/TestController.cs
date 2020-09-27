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


        #region view
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Test_Bootstrap_Table()
        {
            return View();
        }



        public ActionResult TestFileInput()
        {
            return View();
        }


        public ActionResult ChartIndex()
        {
            return View();
        }

        public ActionResult LaserProductionChart()
        {
            return View();
        }

        public ActionResult LaserMachineChart()
        {
            return View();
        }


        public ActionResult PQCOperatorChart()
        {
            return View();
        }

        public ActionResult PQCProductionChart()
        {
            return View();
        }


        #endregion








        public JsonResult GetData()
        {
            var aaa = Request.Form["jobno"];
            var bbb = Request.Form["partno"];




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

            return Json(listModels);
        }

        


        #region test file 
        [HttpPost]
        public JsonResult GetFile()
        {

            var files = Request.Files;
            var file = Request.Files[0];


            string aaa = file.FileName;
            

            file.SaveAs("D://test.jpg");

            return Json("success");
        }

        #endregion




    }
}