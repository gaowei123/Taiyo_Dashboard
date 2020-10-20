using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class HomeController : Controller
    {

       public readonly ViewBusiness.HomePage_ViewBusiness homepageVBLL = new ViewBusiness.HomePage_ViewBusiness();

        #region view
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        #endregion


        public ActionResult GetRefreshData()
        {
            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--Start Refresh");


            DateTime dDay = DateTime.Now.AddHours(-8).Date;

            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--day:"+dDay.ToString("yyyy-MM-dd"));



            List<ViewModel.HomePage_ViewModel> homeModelList = new List<ViewModel.HomePage_ViewModel>();


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetMouldModle");

            //moulding 
            ViewModel.HomePage_ViewModel mouldModel = homepageVBLL.GetMouldModle(dDay);
            homeModelList.Add(mouldModel);


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetPaintModel");
            //painting
            ViewModel.HomePage_ViewModel paintModel = homepageVBLL.GetPaintModel(dDay);
            homeModelList.Add(paintModel);


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetLaserModel");
            //laser
            ViewModel.HomePage_ViewModel laserModel = homepageVBLL.GetLaserModel(dDay);
            homeModelList.Add(laserModel);


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetPQCOnlineModel");
            //pqc online 
            ViewModel.HomePage_ViewModel pqcOnlineModel = homepageVBLL.GetPQCOnlineModel(dDay);
            homeModelList.Add(pqcOnlineModel);


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetPQCWIPModel");
            //pqc WIP
            ViewModel.HomePage_ViewModel pqcWIPModel = homepageVBLL.GetPQCWIPModel(dDay);
            homeModelList.Add(pqcWIPModel);


            DBHelp.Reports.LogFile.Log("Home_Debug", "[HomeController]--GetPQCPackingModel");
            //pqc packing
            ViewModel.HomePage_ViewModel pqcPackingModel = homepageVBLL.GetPQCPackingModel(dDay);
            homeModelList.Add(pqcPackingModel);
       
            


            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(homeModelList);
            
            return Content(strJson);
        }


         
      
    }
}