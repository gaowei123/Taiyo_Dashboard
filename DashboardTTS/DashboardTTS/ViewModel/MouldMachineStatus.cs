using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldMachineStatus 
    {
        public class Summary
        {
            public string machineID { get; set; }
            public string running { get; set; }
            public string adjustment { get; set; }
            public string noSchedule { get; set; }
            public string mouldTesting { get; set; }
            public string materialTesting { get; set; }
            public string changeModel { get; set; }
            public string noOperator { get; set; }
            public string mcStop { get; set; }
            public string mouldDamage { get; set; }
            public string breakdown { get; set; }
            public string meal { get; set; }
            public string shutdown { get; set; }
            public double utilization { get; set; }
        }

        public class SummaryChart
        {
            public string asixNo { get; set; }
            public double utilization { get; set; }
        }



        public class StatusChart
        {
            public string machineID { get; set; }

            public double running { get; set; }
            public double adjustment { get; set; }
            public double noSchedule { get; set; }
            public double mouldTesting { get; set; }
            public double materialTesting { get; set; }
            public double changeModel { get; set; }
            public double noOperator { get; set; }
            public double mcStop { get; set; }
            public double mouldDamage { get; set; }
            public double breakdown { get; set; }
            public double meal { get; set; }
            public double shutdown { get; set; }
            
        }



    }
}