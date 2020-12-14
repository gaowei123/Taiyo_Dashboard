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
            List<Attendance_Model> departmentAttendanceList = _bll.GetDepartmentAttendanceList(param);
            if (departmentAttendanceList == null)
                return null;

            Dictionary<Department, decimal> dicDeparmentUserCount = _bll.GetDepartmentUserCount();

            JsonSerializer jsonSerializer = new JsonSerializer();

            //Excluded AL % =( Nos of staff -mc -upL/upMC -absent)/ Nos of staff
            //Included AL % = ( Nos of staff -mc -upL/upMC -absent -AL -OAL)/Nos of staff

            var monthlyList = from a in departmentAttendanceList
                              where a.Day.Value.DayOfWeek != DayOfWeek.Sunday
                              && a.Day.Value.DayOfWeek != DayOfWeek.Saturday
                              group a by new { a.Day, a.Department } into dayList
                              select new
                              {
                                  dayList.Key.Department,
                                  dayList.Key.Day,

                              };


            foreach (Department item in Enum.GetValues(typeof(Department)))
            {
                var deparmentList = from a in monthlyList where a.Department == Department.Laser select a;
                foreach (var model in deparmentList)
                {

                }
                
            }



            var strResult = JsonConvert.SerializeObject(monthlyList);




            return strResult;
        }





    }
}
