using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.Home
{
    public class Home_Model
    {
        public class DailyTrend
        {
            public string Department { get; set; }
            public string Day { get; set; }
            public decimal Output { get; set; }
        }

        public class TodayOutput
        {
            public string Department { get; set; }
            public decimal Output { get; set; }
        }


        public class Status
        {
            public string MachineID { get; set; }
            public string Department { get; set; }
            public string StatusType { get; set; }
        }

    }
}
