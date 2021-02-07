using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;

namespace DashboardTTS.ViewBusiness
{
    public class Attendance_ViewBusiness
    {
        private readonly Common.BLL.LMMSUserAttendanceTracking_BLL attendanceBLL = new Common.BLL.LMMSUserAttendanceTracking_BLL();
        private readonly Common.Class.BLL.User_DB_BLL userBLL = new Common.Class.BLL.User_DB_BLL();


        public Attendance_ViewBusiness()
        {

        }


  

        //attendance list
        public List<Common.Model.LMMSUserAttendanceTracking_Model> GetAttendanceList(DateTime dDay, string sDepartment)
        {
            List<Common.Model.LMMSUserAttendanceTracking_Model> displayList = new List<Common.Model.LMMSUserAttendanceTracking_Model>();


            List<Common.Class.Model.User_DB_Model> userList = userBLL.GetModelList(sDepartment, "", "", "");
            //去掉一个admin
            var attendanceUser = from a in userList
                                 where a.USER_GROUP != StaticRes.Global.UserGroup.ADMIN
                                 select a;
            //只有当是moulding部门时, 才不显示ipqc.
            if (sDepartment == Department.Moulding.GetDescription())
            {
                attendanceUser = from a in userList
                                 where a.USER_GROUP != StaticRes.Global.UserGroup.ADMIN
                                 && a.USER_GROUP != StaticRes.Global.UserGroup.IPQC
                                 select a;
            }

            List<Common.Model.LMMSUserAttendanceTracking_Model> currentAttendanceList = attendanceBLL.GetModelList(dDay, dDay.AddDays(1), sDepartment);

            #region 如果没记录, 没submit, 按照user db将每个人添加到 attendancelist中.
            if (currentAttendanceList == null|| currentAttendanceList.Count == 0)
            {
                foreach (var model in attendanceUser)
                {
                    Common.Model.LMMSUserAttendanceTracking_Model displayModel = new Common.Model.LMMSUserAttendanceTracking_Model();

                    displayModel.EmployeeID = model.EMPLOYEE_ID;
                    displayModel.UserID = model.USER_ID;
                    displayModel.UserName = model.USER_NAME;
                    displayModel.UserGroup = model.USER_GROUP;
                    displayModel.Shift = model.SHIFT;//没有submit的话, shift按照user db中设定的shift
                    displayModel.Attendance = "";
                    displayModel.OnLeave = "";
                    displayModel.Day = dDay;
                    displayModel.Department = sDepartment;
                    displayModel.UpdateBy = "";
                    displayModel.Remarks = "";
                    displayModel.DateTime = null;
                    displayList.Add(displayModel);
                }

                return displayList;
            }
            #endregion
            

            foreach (var model in attendanceUser)
            {
                var attendanceModel = (from a in currentAttendanceList
                                       where a.EmployeeID == model.EMPLOYEE_ID
                                       select a).FirstOrDefault();

                Common.Model.LMMSUserAttendanceTracking_Model displayModel = new Common.Model.LMMSUserAttendanceTracking_Model();
                displayModel.Day = dDay;
                displayModel.Department = sDepartment;
                displayModel.EmployeeID = model.EMPLOYEE_ID;
                displayModel.UserID = model.USER_ID;
                displayModel.UserName = model.USER_NAME;
                displayModel.UserGroup = model.USER_GROUP;
                displayModel.Shift = attendanceModel == null ? model.SHIFT : attendanceModel.Shift;  //没有submit的话, shift按照user db中设定的shift
                displayModel.Attendance = attendanceModel == null ? "" : attendanceModel.Attendance;
                displayModel.OnLeave = attendanceModel == null ? "" : attendanceModel.OnLeave;
                displayModel.UpdateBy = attendanceModel == null ? "" : attendanceModel.UpdateBy;
                displayModel.Remarks = attendanceModel == null ? "" : attendanceModel.Remarks;
                displayModel.DateTime = attendanceModel==null ? null : attendanceModel.DateTime;
                
                displayList.Add(displayModel);
            }


            return displayList;
        }



        //attendane chart data
        public List<ViewModel.Attendance_ViewModel.Chart> GetChartData(DateTime dDateFrom, DateTime dDateto, string sDepartment)
        {
            DataTable dt = attendanceBLL.GetUserAttendanceSummary(dDateFrom, dDateto, sDepartment);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            // total working days
            double workingDays = 0;
            DateTime dTemp = dDateFrom.Date;
            while (dTemp < dDateto)
            {
                if (dTemp.DayOfWeek != DayOfWeek.Sunday && dTemp.DayOfWeek != DayOfWeek.Saturday)
                    workingDays++;

                dTemp = dTemp.AddDays(1);
            }



            List<ViewModel.Attendance_ViewModel.Chart> modelList = new List<ViewModel.Attendance_ViewModel.Chart>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.Attendance_ViewModel.Chart model = new ViewModel.Attendance_ViewModel.Chart();


                model.empoyeeID = dr["EmployeeID"].ToString();
                model.userName = dr["USER_NAME"].ToString();


                double totalDays = (dDateto - dDateFrom).TotalDays;

                double attendanceDays = 0;
                if (dr["attendanceDays"] != null && dr["attendanceDays"].ToString() != "")
                    attendanceDays = double.Parse(dr["attendanceDays"].ToString());

                double leaveDays = 0;
                if (dr["leaveDays"] != null || dr["leaveDays"].ToString() != "")
                    leaveDays = double.Parse(dr["leaveDays"].ToString());


                model.attendanceDays = attendanceDays;
                model.leaveDays = leaveDays;
                model.attendaneRate = totalDays == 0? 0: Math.Round(attendanceDays / workingDays * 100, 2);


                modelList.Add(model);
            }


            var result = (from a in modelList
                          orderby a.empoyeeID ascending
                          select a).ToList();

            return result;
        }
        
    }
}