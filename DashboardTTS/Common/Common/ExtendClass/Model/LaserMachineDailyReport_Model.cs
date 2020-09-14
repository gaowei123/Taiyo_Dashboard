using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.Model
{
    public class LaserMachineDailyReport_Model
    {

        public class watchShiftInfo
        {
            public DateTime Day { get; set; }
            public string Shift { get; set; }
            public string MachineID { get; set; }



            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string StartTimeDisplay { get; set; }
            public string EndTimeDisplay { get; set; }
            public string OperatedTime { get; set; }

            public string PartNo { get; set; }
            public string LotNo { get; set; }
            

            public decimal LotQty { get; set; }
            public decimal OK { get; set; }
            public decimal NG { get; set; }
            public decimal Setup { get; set; }
        }


        public class MachineStatusInfo
        {
            public DateTime Day { get; set; }
            public string Shift { get; set; }
            public string MachineID { get; set; }


            public string Status { get; set; }           
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Duration { get; set; }
        }

    }
}
