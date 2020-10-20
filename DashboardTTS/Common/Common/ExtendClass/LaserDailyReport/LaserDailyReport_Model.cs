using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserDailyReport
{
    public class LaserDailyReport_Model
    {
        public class Main
        {
            public string MachineID { get; set; }
            public string Run { get; set; }
            public string Idle { get; set; }
            public string Breakdown { get; set; }
            public string Shutdown { get; set; }


            public double PassQty { get; set; }
            public double RejQty { get; set; }
            public double Setup { get; set; }
            public double Buyoff { get; set; }
            public double Output { get; set; }
        }


        public class DetailStatus
        {
            public string MachineID { get; set; }
            public string StartTime { get; set; }
            public string StopTime { get; set; }
            public string TakeTime { get; set; }
            public string Status { get; set; }

        }

        public class DetailOutput
        {
            public string MachineID { get; set; }
            public string Shift { get; set; }
            public string StartTime { get; set; }
            public string StopTime { get; set; }
            public string TakeTime { get; set; }
            public string PartNo { get; set; }
            public string JobNo { get; set; }
            public string LotNo { get; set; }
            public double PassQty { get; set; }
            public double RejQty { get; set; }
            public double Setup { get; set; }
            public double Buyoff { get; set; }
            public double Output { get; set; }
        }


    }
}
