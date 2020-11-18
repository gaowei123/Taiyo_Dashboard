using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ViewBusiness.Attendance_ViewBusiness vbllAttendance = new ViewBusiness.Attendance_ViewBusiness();
        private readonly Common.Class.BLL.User_DB_BLL _userBLL = new Common.Class.BLL.User_DB_BLL();
        private readonly Common.BLL.LMMSUserAttendanceTracking_BLL attendanceBLL = new Common.BLL.LMMSUserAttendanceTracking_BLL();



        #region view
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserManagement()
        {
            return View();
        }
        public ActionResult Attendance()
        {
            return View();
        }
        public ActionResult AttendanceReport()
        {
            return View();
        }
        public ActionResult AttendanceChart()
        {
            return View();
        }

        #endregion












        #region User Management
        public JsonResult GetUserList(string Department, string EmployeeID, string UserGroup)
        {
            var modelList = _userBLL.GetModelList(Department, EmployeeID, UserGroup, "");

            var result = from a in modelList
                         where a.USER_GROUP != Taiyo.Enum.Organization.UserGroup.Admin.ToString()
                         orderby a.EMPLOYEE_ID ascending
                         select a;
            
            return Json(result);
        }

        public JsonResult AddUser()
        {
            string employeeID = Request.Form["EmployeeID"];
            if (_userBLL.Exist(employeeID))
            {
                return Json($"EmployeeID {employeeID} is already exist!");       
            }
            else
            {
                var model = new Common.Class.Model.User_DB_Model();
                model.EMPLOYEE_ID = Request.Form["EmployeeID"];
                model.USER_ID = Request.Form["DepartmentID"];
                model.USER_NAME = Request.Form["Username"];
                model.PASSWORD = Request.Form["Password"];
                model.USER_GROUP = Request.Form["UserGroup"];
                model.UPDATED_TIME = DateTime.Now;
                model.UPDATED_BY = Request.Form["UpdatedBy"];
                model.DEPARTMENT = Request.Form["Department"];
                model.SHIFT = Request.Form["Shift"];
                model.FINGER_TEMPLATE = "";
                model.FINGER_TEMPLATE_1 = "";
                model.DEPARTMENT_ID = "";
                
                return Json(_userBLL.Add(model));
            }
        }

        public JsonResult UpdateUser()
        {
            var model = new Common.Class.Model.User_DB_Model();
            model.EMPLOYEE_ID = Request.Form["EmployeeID"];
            model.USER_ID = Request.Form["DepartmentID"];
            model.USER_NAME = Request.Form["Username"];
            model.PASSWORD = Request.Form["Password"];
            model.USER_GROUP = Request.Form["UserGroup"];
            model.UPDATED_TIME = DateTime.Now;
            model.UPDATED_BY = Request.Form["UpdatedBy"];
            model.DEPARTMENT = Request.Form["Department"];
            model.SHIFT = Request.Form["Shift"];
            model.FINGER_TEMPLATE = "";
            model.FINGER_TEMPLATE_1 = "";
            model.DEPARTMENT_ID = "";

            return Json(_userBLL.Update(model));
        }

        public JsonResult DeleteUser(string EmployeeID)
        {       
            return Json(_userBLL.Delete(EmployeeID));
        }

        #endregion


        #region Attendenance Daily Report
        public ActionResult GetOverall()
        {
            DateTime dDay = DateTime.Parse(Request.Form["Date"]);


            List<ViewModel.Attendance_ViewModel.Overall> overallList = vbllAttendance.GetOverall(dDay);


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(overallList);



            return  Content(jsonResult);
        }
        
        public ActionResult  GetDetail()
        {
            string jsonResult = "";
            JavaScriptSerializer js = new JavaScriptSerializer();


            DateTime dDay = DateTime.Parse(Request.Form["Date"]);
        

            List<ViewModel.Attendance_ViewModel.Detail> detailList = vbllAttendance.GetDetail(dDay);

            if (detailList == null || detailList.Count == 0)
            {
                jsonResult = js.Serialize("");
            }
            else
            {
                
                jsonResult = js.Serialize(detailList);
            }
            

            return Content(jsonResult);
        }

        #endregion

        
      


        #region submit attendance
        public ActionResult GetAttendanceList()
        {
            string department = Request.Form["Department"];
            DateTime day = DateTime.Parse(Request.Form["Date"]);



            JavaScriptSerializer js = new JavaScriptSerializer();


            List<Common.Model.LMMSUserAttendanceTracking_Model> attendanceList = vbllAttendance.GetAttendanceList(day, department);

            //保留原本的排列顺序
            var orderList = from a in attendanceList
                            orderby a.Shift ascending, a.UserID ascending
                            select a;



            return Content(js.Serialize(orderList));
        }
        
        public ActionResult IsSubmit()
        {
            string department = Request.Form["Department"];
            DateTime day = DateTime.Parse(Request.Form["Date"]);


            bool result = attendanceBLL.IsSubmited(day, department);

            JavaScriptSerializer js = new JavaScriptSerializer();

            return Content(js.Serialize(result));
        }
        
        public ActionResult SubmitAttendance()
        {
            string strAttendanceList = Request.Form["AttendanceList"];
            DateTime day = DateTime.Parse(Request.Form["Day"]);

            string department = Request.Form["Department"];

            JavaScriptSerializer js = new JavaScriptSerializer();

            List<Common.Model.LMMSUserAttendanceTracking_Model> modelList = js.Deserialize<List<Common.Model.LMMSUserAttendanceTracking_Model>>(strAttendanceList);
            if (modelList == null)
                return Content(js.Serialize("false"));



            bool result = attendanceBLL.SubmitAttendance(day, department, modelList);

            return Content(js.Serialize(result));
        }

        #endregion


        //attendance chart
        public ActionResult GetChartData()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"]);
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"]);
            dateTo = dateTo.AddDays(1);
            string department = Request.Form["Department"];


            List<ViewModel.Attendance_ViewModel.Chart> modelList = vbllAttendance.GetChartData(dateFrom, dateTo, department);


            

            if (modelList == null || modelList.Count == 0)
                return Content(js.Serialize(""));
            else
                return Content(js.Serialize(modelList));            
        }


    }
}