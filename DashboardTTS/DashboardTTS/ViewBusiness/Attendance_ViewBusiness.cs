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


        //Daily Report --Summary list 
        public List<ViewModel.Attendance_ViewModel.Overall> GetOverall(DateTime dDay)
        {

            //department, manPower
            DataTable dtUserSummary = userBLL.GetManPower();
            if (dtUserSummary == null || dtUserSummary.Rows.Count == 0)
                return null;
            

            List < Common.Model.LMMSUserAttendanceTracking_Model > trackingList = new List<Common.Model.LMMSUserAttendanceTracking_Model>();
            trackingList = attendanceBLL.GetModelList(dDay, dDay.AddDays(1), "");



            List<ViewModel.Attendance_ViewModel.Overall> overallList = new List<ViewModel.Attendance_ViewModel.Overall>();
            foreach (DataRow dr in dtUserSummary.Rows)
            {
                string department = dr["department"].ToString();
                double manPower = double.Parse(dr["manPower"].ToString());

                
                double attendance = 0;
                double annualLeave = 0;
                double mc = 0;
                double unPaid = 0;
                double maternity = 0;
                double marriage = 0;
                double compassionate = 0;
                double childCare = 0;
                double absent = 0;
                double businessTrip = 0;
                double pending = 0;
                double reserviced = 0;
                bool isSubmit = false;


                if (trackingList == null || trackingList.Count == 0)
                {
                    #region no record, default 0
                    attendance = 0;
                    annualLeave = 0;
                    mc = 0;
                    unPaid = 0;
                    maternity = 0;
                    marriage = 0;
                    compassionate = 0;
                    childCare = 0;
                    absent = 0;
                    businessTrip = 0;
                    pending = 0;
                    reserviced = 0;

                    isSubmit = false;
                    #endregion
                }
                else
                {

                    var departmentList = from a in trackingList  where a.Department == department  select a;
                    if (departmentList == null || departmentList.Count() == 0)
                    {
                        #region  no record, default 0
                        attendance = 0;
                        annualLeave = 0;
                        mc = 0;
                        unPaid = 0;
                        maternity = 0;
                        marriage = 0;
                        compassionate = 0;
                        childCare = 0;
                        absent = 0;
                        businessTrip = 0;
                        pending = 0;
                        reserviced = 0;

                        isSubmit = false;
                        #endregion
                    }
                    else
                    {
                        #region moulding, painting, laser, pqc, assembly, office department
                        var attendList = from a in departmentList  where a.Attendance == "Attendance" select a;
                        var AnnualLeaveList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.annualLeave select a;
                        var McLeaveList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.mcLeave select a;
                        var UnPaidList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.unPaid select a;
                        var MaternityList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.maternity select a;
                        var MarriageList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.marriage select a;
                        var CompassionateList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.compassionate select a;
                        var ChildCareList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.childCare select a;
                        var AbsentList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.absent select a;
                        var BusinessTripList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.businessTrip select a;
                        var PendingList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.pending select a;
                        var ReservicedList = from a in departmentList where a.Attendance == StaticRes.Global.UserAttendance.LeaveReason.reserviced select a;


                        attendance = attendList == null || attendList.Count() == 0 ? 0 : attendList.Count();
                        annualLeave = AnnualLeaveList == null || AnnualLeaveList.Count() == 0 ? 0 : AnnualLeaveList.Count();
                        mc = McLeaveList == null || McLeaveList.Count() == 0 ? 0 : McLeaveList.Count();
                        unPaid = UnPaidList == null || UnPaidList.Count() == 0 ? 0 : UnPaidList.Count();
                        maternity = MaternityList == null || MaternityList.Count() == 0 ? 0 : MaternityList.Count();
                        marriage = MarriageList == null || MarriageList.Count() == 0 ? 0 : MarriageList.Count();
                        compassionate = CompassionateList == null || CompassionateList.Count() == 0 ? 0 : CompassionateList.Count();
                        childCare = ChildCareList == null || ChildCareList.Count() == 0 ? 0 : ChildCareList.Count();
                        absent = AbsentList == null || AbsentList.Count() == 0 ? 0 : AbsentList.Count();
                        businessTrip = BusinessTripList == null || BusinessTripList.Count() == 0 ? 0 : BusinessTripList.Count();
                        pending = PendingList == null || PendingList.Count() == 0 ? 0 : PendingList.Count();
                        reserviced = ReservicedList == null || ReservicedList.Count() == 0 ? 0 : ReservicedList.Count();


                        isSubmit = true;
                        #endregion
                    }
                    
                }
                

                ViewModel.Attendance_ViewModel.Overall overallModel = new ViewModel.Attendance_ViewModel.Overall();
                overallModel.department = isSubmit ? department : department + " : Pending Supervisor Submit";
                overallModel.manPower = manPower;

                overallModel.attendance = attendance;
                overallModel.annualLeave = annualLeave;
                overallModel.mc = mc;
                overallModel.unPaid = unPaid;
                overallModel.maternity = maternity;
                overallModel.marriage = marriage;
                overallModel.compassionate = compassionate;
                overallModel.childCare = childCare;
                overallModel.absent = absent;
                overallModel.businessTrip = businessTrip;
                overallModel.pending = pending;
                overallModel.reserviced = reserviced;

                //businessTrip 算在出勤
                overallModel.attendanceRate = Math.Round((attendance + businessTrip) / manPower * 100, 2);

                overallList.Add(overallModel);
            }



            #region add summary model
            double totalManPower = double.Parse(dtUserSummary.Compute(" sum(manPower) ", "").ToString());
            
            if (trackingList == null || trackingList.Count == 0)
            {
                ViewModel.Attendance_ViewModel.Overall overallSummaryModel = new ViewModel.Attendance_ViewModel.Overall();
                overallSummaryModel.department = "OverAll";
                overallSummaryModel.manPower = totalManPower;
                overallSummaryModel.attendance = 0;
                overallSummaryModel.annualLeave = 0;
                overallSummaryModel.mc = 0;
                overallSummaryModel.unPaid = 0;
                overallSummaryModel.maternity = 0;
                overallSummaryModel.marriage = 0;
                overallSummaryModel.compassionate = 0;
                overallSummaryModel.childCare = 0;
                overallSummaryModel.absent = 0;
                overallSummaryModel.businessTrip = 0;
                overallSummaryModel.pending = 0;
                overallSummaryModel.reserviced = 0;
                overallSummaryModel.attendanceRate = 0;
                overallList.Add(overallSummaryModel);
            }
            else
            {
                var AttendListSummary = from a in trackingList where a.Attendance == "Attendance" select a;
                var AnnualLeaveListSummary = from a in trackingList where a.Attendance == "AnnualLeave" select a;
                var McLeaveListSummary = from a in trackingList where a.Attendance == "McLeave" select a;
                var UnPaidListSummary = from a in trackingList where a.Attendance == "UnPaid" select a;
                var MaternityListSummary = from a in trackingList where a.Attendance == "Maternity" select a;
                var MarriageListSummary = from a in trackingList where a.Attendance == "Marriage" select a;
                var CompassionateListSummary = from a in trackingList where a.Attendance == "Compassionate" select a;
                var ChildCareListSummary = from a in trackingList where a.Attendance == "ChildCare" select a;
                var AbsentListSummary = from a in trackingList where a.Attendance == "Absent" select a;
                var BusinessTripListSummary = from a in trackingList where a.Attendance == "BusinessTrip" select a;
                var PendingListSummary = from a in trackingList where a.Attendance == "Pending" select a;
                var ReservicedListSummary = from a in trackingList where a.Attendance == "Reserviced" select a;

                ViewModel.Attendance_ViewModel.Overall overallSummaryModel = new ViewModel.Attendance_ViewModel.Overall();
                overallSummaryModel.department = "OverAll";
                overallSummaryModel.manPower = totalManPower;

                overallSummaryModel.attendance = AttendListSummary == null || AttendListSummary.Count() == 0 ? 0 : AttendListSummary.Count();
                overallSummaryModel.annualLeave = AnnualLeaveListSummary == null || AnnualLeaveListSummary.Count() == 0 ? 0 : AnnualLeaveListSummary.Count();
                overallSummaryModel.mc = McLeaveListSummary == null || McLeaveListSummary.Count() == 0 ? 0 : McLeaveListSummary.Count();
                overallSummaryModel.unPaid = UnPaidListSummary == null || UnPaidListSummary.Count() == 0 ? 0 : UnPaidListSummary.Count();
                overallSummaryModel.maternity = MaternityListSummary == null || MaternityListSummary.Count() == 0 ? 0 : MaternityListSummary.Count();
                overallSummaryModel.marriage = MarriageListSummary == null || MarriageListSummary.Count() == 0 ? 0 : MarriageListSummary.Count();
                overallSummaryModel.compassionate = CompassionateListSummary == null || CompassionateListSummary.Count() == 0 ? 0 : CompassionateListSummary.Count();
                overallSummaryModel.childCare = ChildCareListSummary == null || ChildCareListSummary.Count() == 0 ? 0 : ChildCareListSummary.Count();
                overallSummaryModel.absent = AbsentListSummary == null || AbsentListSummary.Count() == 0 ? 0 : AbsentListSummary.Count();
                overallSummaryModel.businessTrip = BusinessTripListSummary == null || BusinessTripListSummary.Count() == 0 ? 0 : BusinessTripListSummary.Count();
                overallSummaryModel.pending = PendingListSummary == null || PendingListSummary.Count() == 0 ? 0 : PendingListSummary.Count();
                overallSummaryModel.reserviced = ReservicedListSummary == null || ReservicedListSummary.Count() == 0 ? 0 : ReservicedListSummary.Count();

                //businessTrip 算在出勤
                overallSummaryModel.attendanceRate = Math.Round((overallSummaryModel.attendance + overallSummaryModel.businessTrip) / totalManPower * 100, 2);

                overallList.Add(overallSummaryModel);
            }
            #endregion



            var moulding = from a in overallList where a.department.Contains(StaticRes.Global.Department.Moulding) select a;
            var painting = from a in overallList where a.department.Contains(StaticRes.Global.Department.Painting) select a;
            var laser = from a in overallList where a.department.Contains(StaticRes.Global.Department.Laser) select a;
            var pqc = from a in overallList where a.department.Contains(StaticRes.Global.Department.PQC) select a;
            var assembly = from a in overallList where a.department.Contains(StaticRes.Global.Department.Assembly) select a;
            var office = from a in overallList where a.department.Contains(StaticRes.Global.Department.Office) select a;
            var overall = from a in overallList where a.department.Contains("OverAll") select a;


            List<ViewModel.Attendance_ViewModel.Overall> orderList = new List<ViewModel.Attendance_ViewModel.Overall>();


            orderList.AddRange(moulding.ToList());
            orderList.AddRange(painting.ToList());
            orderList.AddRange(laser.ToList());
            orderList.AddRange(pqc.ToList());
            orderList.AddRange(assembly.ToList());
            orderList.AddRange(office.ToList());
            orderList.AddRange(overall.ToList());



            return orderList;
        }


        //Daily Report --Detail list 
        public List<ViewModel.Attendance_ViewModel.Detail> GetDetail(DateTime dDay)
        {
            List<Common.Model.LMMSUserAttendanceTracking_Model> trackingList = attendanceBLL.GetModelList(dDay, dDay.AddDays(1), "");
            if (trackingList == null || trackingList.Count == 0)
                return null;


            var leaveList = from a in trackingList
                            where a.Attendance != "Attendance"
                            select a;


            List < ViewModel.Attendance_ViewModel.Detail > detailList = new List<ViewModel.Attendance_ViewModel.Detail>();
            foreach (Common.Model.LMMSUserAttendanceTracking_Model trackingModel in leaveList)
            {
                ViewModel.Attendance_ViewModel.Detail model = new ViewModel.Attendance_ViewModel.Detail();
                model.empID = trackingModel.EmployeeID;
                model.department = trackingModel.Department;
                model.name = trackingModel.UserName;
                model.type = trackingModel.Attendance;
                model.remark = trackingModel.Remarks;
                model.time = trackingModel.OnLeave;
                detailList.Add(model);
            }

            
            //按照 moulding, painting, laser, pqc, assembly, office 的顺序排列
            List<ViewModel.Attendance_ViewModel.Detail> detailListOrdered = new List<ViewModel.Attendance_ViewModel.Detail>();
            
            var mouldingList = from a in detailList where a.department == "Moulding" orderby a.empID ascending select a;
            var paintingList = from a in detailList where a.department == "Painting" orderby a.empID ascending select a;
            var laserList = from a in detailList where a.department == "Laser" orderby a.empID ascending select a;
            var pqcList = from a in detailList where a.department == "PQC" orderby a.empID ascending select a;
            var assemblyList = from a in detailList where a.department == "Assembly" orderby a.empID ascending select a;
            var office = from a in detailList where a.department == "Office" orderby a.empID ascending select a;


            if (mouldingList != null && mouldingList.Count() !=0)
                detailListOrdered.AddRange(mouldingList);
            
            if (paintingList != null && paintingList.Count() != 0)
                detailListOrdered.AddRange(paintingList);

            if (laserList != null && laserList.Count() != 0)
                detailListOrdered.AddRange(laserList);

            if (pqcList != null && pqcList.Count() != 0)
                detailListOrdered.AddRange(pqcList);

            if (assemblyList != null && assemblyList.Count() != 0)
                detailListOrdered.AddRange(assemblyList);

            if (office != null && office.Count() != 0)
                detailListOrdered.AddRange(office);




            return detailListOrdered;
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
                model.userName = dr["UserName"].ToString();


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