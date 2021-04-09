using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class Attendance_ViewModel
    {
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