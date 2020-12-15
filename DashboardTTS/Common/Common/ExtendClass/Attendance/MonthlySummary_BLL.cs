using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.AttendanceParam;
using Taiyo.Enum.Organization;
using Newtonsoft.Json;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.Attendance
{
    public class MonthlySummary_BLL
    {
        private readonly Base_BLL _bll;
        public MonthlySummary_BLL()
        {
            _bll = new Base_BLL();
        }


        public string GetMonthlyListJsonString(Taiyo.SearchParam.AttendanceParam.MonthlyParams param)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            List<Attendance_Model> departmentAttendanceList = _bll.GetDepartmentAttendanceList(param);
            if (departmentAttendanceList == null)
                return null;

            Dictionary<Department, decimal> dicDeparmentUserCount = _bll.GetDepartmentUserCount();



            #region departmentAttendanceList 中补全信息
            var dateTemp = param.DateFrom.Value.Date;
            while (dateTemp < param.DateTo.Value.Date)
            {
                if (dateTemp.DayOfWeek == DayOfWeek.Saturday || dateTemp.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateTemp = dateTemp.AddDays(1);
                    continue;
                }

                foreach (Department dpt in Enum.GetValues(typeof(Department)))
                {
                    if (dpt == Department.Online || dpt == Department.WIP || dpt == Department.Packing) continue;

                    var model = departmentAttendanceList.Where(p => p.Day == dateTemp && p.Department == dpt).FirstOrDefault();
                    if (model == null)
                    {
                        departmentAttendanceList.Add(new Attendance_Model()
                        {
                            Department = dpt,
                            Day = dateTemp,
                            DayShift = 0,
                            NightShift = 0,
                            TotalPresent = 0,
                            AnnualLeavel = 0,
                            MC_UPMC = 0,
                            Unpaid = 0,
                            Maternity = 0,
                            Paternity = 0,
                            Marriage = 0,
                            WFH = 0,
                            Hospitalization = 0,
                            Compassionate = 0,
                            ChildCareLeave = 0,
                            Absent = 0,
                            BusinessTrip = 0,
                            Reservist = 0,
                            Pending = 0,
                            LeaveReason = ""
                        });
                    }
                }

                dateTemp = dateTemp.AddDays(1);
            }
            #endregion



            //合并 dicDeparmentUserCount , departmentAttendanceList
            var joinList = from a in departmentAttendanceList
                              where a.Day.Value.DayOfWeek != DayOfWeek.Sunday &&
                                    a.Day.Value.DayOfWeek != DayOfWeek.Saturday
                              join b in dicDeparmentUserCount on a.Department equals b.Key
                              orderby a.Day ascending, a.Department ascending
                              select new
                              {
                                  a,
                                  b
                              };

            //根据 day, department分组
            var groupByList = from a in joinList
                              group a by new { a.a.Day, a.a.Department } into groupList
                              select new
                              {
                                  Department = groupList.Key.Department.GetDescription(),
                                  Day =  $"{groupList.Key.Day.Value.Day}-{groupList.Key.Day.Value.GetMonthName(false)}",



                                  //Excluded AL % =( Nos of staff -mc -upL/upMC -absent)/ Nos of staff
                                  ExcludedAL = groupList.Sum(p => p.a.TotalPresent) == 0 ? "0.00%" : 
                                  Math.Round(
                                      (groupList.Sum(p => p.b.Value) - groupList.Sum(p => p.a.MC_UPMC + p.a.Unpaid + p.a.Absent)) / groupList.Sum(p => p.b.Value) * 100
                                      , 2
                                  ).ToString("0.00") + "%",



                                  //Included AL % = ( Nos of staff -mc -upL/upMC -absent -AL -OAL)/Nos of staff
                                  IncludedAL = groupList.Sum(p => p.a.TotalPresent) == 0 ? "0.00%" :  
                                  Math.Round(
                                      (groupList.Sum(p => p.b.Value) - groupList.Sum(p => p.a.MC_UPMC + p.a.Unpaid + p.a.Absent  + p.a.AnnualLeavel  + p.a.Maternity + p.a.Paternity + p.a.Marriage + p.a.Hospitalization + p.a.Compassionate + p.a.ChildCareLeave)) / groupList.Sum(p => p.b.Value) * 100
                                      , 2
                                  ).ToString("0.00") + "%",

                              };
                     

            return JsonConvert.SerializeObject(groupByList);
        }





    }
}
