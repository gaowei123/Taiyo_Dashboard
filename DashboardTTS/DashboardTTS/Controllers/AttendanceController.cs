using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taiyo.SearchParam;

namespace DashboardTTS.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ViewBusiness.Attendance_ViewBusiness vbllAttendance = new ViewBusiness.Attendance_ViewBusiness();
        private readonly Common.Class.BLL.User_DB_BLL _userBLL = new Common.Class.BLL.User_DB_BLL();
        private readonly Common.BLL.LMMSUserAttendanceTracking_BLL attendanceBLL = new Common.BLL.LMMSUserAttendanceTracking_BLL();


        private readonly Common.ExtendClass.Attendance.DailySummary_BLL dailySummaryBLL = new Common.ExtendClass.Attendance.DailySummary_BLL();
        private readonly Common.ExtendClass.Attendance.MonthlySummary_BLL monthlySummaryBLL = new Common.ExtendClass.Attendance.MonthlySummary_BLL();


        private readonly  JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();




        #region view
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

        public ActionResult DailySummaryReport()
        {
            return View();
        }

        public ActionResult MonthlySummaryReport()
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
        

        #region submit attendance
        public JsonResult GetAttendanceList(string Department, DateTime Date)
        {
            List<Common.Model.LMMSUserAttendanceTracking_Model> attendanceList = vbllAttendance.GetAttendanceList(Date, Department);

            //保留原本的排列顺序
            var orderList = attendanceList.OrderBy(p => p.Shift).OrderBy(p => p.UserID);
            return Json(orderList);
        }
        
        /// <summary>
        /// 判断这一天, 这个部门有没有提交过.
        /// 没提交过的,页面显示一句警告信息.
        /// </summary>
        /// <returns></returns>
        public JsonResult IsSubmit(string Department, DateTime Date)
        {
            bool result = attendanceBLL.IsSubmited(Date, Department);          
            return Json(result);
        }
        

        /// <summary>
        /// 保存Attendance信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SubmitAttendance()
        {
            string strJsonObj = Request.Form["AttendanceList"];
            DateTime day = DateTime.Parse(Request.Form["Day"]);
            string department = Request.Form["Department"];
            
            List<Common.Model.LMMSUserAttendanceTracking_Model> modelList = _jsSerializer.Deserialize<List<Common.Model.LMMSUserAttendanceTracking_Model>>(strJsonObj);
            if (modelList == null)
                return Json("false");

            
            bool result = attendanceBLL.SubmitAttendance(day, department, modelList);
            return Json(result);
        }
        
        #endregion




        /// <summary>
        /// New Daily Attendance Summary Report
        /// bootstrap table & echart 都访问同一个api
        /// </summary>
        /// <param name="Date">要查看的日期</param>
        /// <returns></returns>
        public JsonResult GetDailySummaryReport(DateTime Date)
        {
            BaseParam para = new BaseParam() { DateFrom = Date, DateTo = Date.AddDays(1) };
            var result = dailySummaryBLL.GetDailySummaryList(para);
            
            return result == null ? Json("") : Json(result);
        }


        /// <summary>
        /// New Monthly Attendance Summary Report
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isExcludedAL">
        /// 切换ExcludedAL, IncludedAL 的flag
        /// Included AL % = ( Nos of staff -mc -upL/upMC -absent -AL -OAL)/Nos of staff
        /// Excluded AL % =( Nos of staff -mc -upL/upMC -absent)/ Nos of staff
        /// </param>
        /// <returns></returns>
        public ActionResult GetMonthlySummaryReport(int Year, int Month, bool IsExcludedAL)
        {
            Taiyo.SearchParam.AttendanceParam.MonthlyParams param = new Taiyo.SearchParam.AttendanceParam.MonthlyParams();
            param.DateFrom = new DateTime(Year, Month, 1);
            param.DateTo = param.DateFrom.Value.AddMonths(1);
            param.isExcludedAL = IsExcludedAL;



            string strResult = monthlySummaryBLL.GetMonthlyListJsonString(param);

            return Content(strResult);
        }



        #region Attendenance Daily Report
        public ActionResult GetOverall()
        {
            DateTime dDay = DateTime.Parse(Request.Form["Date"]);


            List<ViewModel.Attendance_ViewModel.Overall> overallList = vbllAttendance.GetOverall(dDay);


            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(overallList);



            return Content(jsonResult);
        }

        public ActionResult GetDetail()
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
