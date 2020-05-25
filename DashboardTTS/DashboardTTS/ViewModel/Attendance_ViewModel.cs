using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class Attendance_ViewModel
    {
        public class Overall
        {
            public string department { get; set; }
            public double manPower { get; set; }
            public double attendance { get; set; }
            public double annualLeave { get; set; }
            public double mc { get; set; }
            public double unPaid { get; set; }
            public double maternity { get; set; }
            public double marriage { get; set; }
            public double compassionate { get; set; }
            public double childCare { get; set; }
            public double absent { get; set; }
            public double businessTrip { get; set; }
            public double pending { get; set; }
            public double reserviced { get; set; }
            public double attendanceRate { get; set; }

        }

        public class Detail
        {
            public string department { get; set; }
            public string empID { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string remark { get; set; }
            public string time { get; set; }
        }


        public class Chart
        {
            public string empoyeeID { get; set; }
            public string userName { get; set; }
            public double attendanceDays { get; set; }
            public double leaveDays { get; set; }
            public double attendaneRate { get; set; }
        }



    }
}