using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;

namespace DashboardTTS.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPartList()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            string department = Request.Form["Department"] == null ? "" : Request.Form["Department"].ToString();
            if (department == StaticRes.Global.Department.Laser ||
                department == StaticRes.Global.Department.PQC)
            {
                Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
                List<string> partList = bll.GetPartNoList();
                jsonResult = js.Serialize(partList);
            }
            else if (department == StaticRes.Global.Department.Moulding)
            {
                Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                List<string> partList = bll.GetPartNoList();

                var result = from a in partList where a != "" orderby a ascending select a;//去掉一个空的.
                jsonResult = js.Serialize(result);
            }
            else
            {
                jsonResult = js.Serialize("");
            }



            return Content(jsonResult);
        }

        public ActionResult GetModelList()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            string department = Request.Form["Department"] == null ? "" : Request.Form["Department"].ToString();

            if (department == StaticRes.Global.Department.Laser ||
                department == StaticRes.Global.Department.PQC)
            {
                Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
                List<string> modelList = bll.GetModelNoList();
                jsonResult = js.Serialize(modelList);
            }
            else if (department == StaticRes.Global.Department.Moulding)
            {
              
                jsonResult = js.Serialize("");
            }
            else
            {
           
                jsonResult = js.Serialize("");
            }



            return Content(jsonResult);
        }




        public ActionResult GetJigNoList()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            string department = Request.Form["Department"] == null ? "" : Request.Form["Department"].ToString();


            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
            List<string> jigNoList = bll.GetJigNoList();
            jsonResult = js.Serialize(jigNoList);
            

            return Content(jsonResult);
        }

        
        public ActionResult Login()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            string username = Request.Form["Username"] == null ? "" : Request.Form["Username"].ToString();
            string password = Request.Form["Password"] == null ? "" : Request.Form["Password"].ToString();
            string department = Request.Form["Department"] == null ? "" : Request.Form["Department"].ToString();
            string authority = Request.Form["Authority"] == null ? "" : Request.Form["Authority"].ToString();


            string resultMsg = "";

            Common.Class.BLL.User_DB_BLL bll = new Common.Class.BLL.User_DB_BLL();
            bool result = bll.Login(username, password, out resultMsg, department, authority);

            if (result == true)
            {
                jsonResult = js.Serialize(result);
            }else
            {
                jsonResult = js.Serialize(resultMsg);
            }
           
           


            return Content(jsonResult);
        }


        public ActionResult GetMouldingDefectList()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";
            

            Common.Class.BLL.MouldingDefectSetting_BLL bll = new Common.Class.BLL.MouldingDefectSetting_BLL();
            List<string> defectCodeList  = bll.RejTypeList();

            defectCodeList.RemoveAt(0);//第一个是all 去掉.

           

            jsonResult = js.Serialize(defectCodeList);


            return Content(jsonResult);
        }


        public ActionResult GetUserIDList()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = "";


            string department = Request.Form["Department"];


            Common.Class.BLL.User_DB_BLL bll = new Common.Class.BLL.User_DB_BLL();
            List<string> userIDList = bll.GetUserIDList(department);



             userIDList.Sort();
            jsonResult = js.Serialize(userIDList);


            return Content(jsonResult);
        }

    }
}