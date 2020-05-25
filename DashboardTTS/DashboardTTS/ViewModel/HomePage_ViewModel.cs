using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class HomePage_ViewModel
    {
        
        public string department { get; set; }
        public string output { get; set; }
        public string inventory { get; set; }
        public string utilization { get; set; }

        public List<machineStatus> machineStatusList { get; set; }


        public class machineStatus
        {
            public string machineID { get; set; }
            public string status { get; set; }

            public string statusColor { get; set; }
        }
        
    }
}