using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class LaserSummaryReport_ViewModel
    {

        public string machineID { get; set; }
        public string number { get; set; }
        public double totalpass { get; set; }
        public double totalfail { get; set; }
        public double output { get; set; }



        public class typeColumn
        {
            public string name { get; set; }
            public string dataField { get; set; }
        }

    }
}