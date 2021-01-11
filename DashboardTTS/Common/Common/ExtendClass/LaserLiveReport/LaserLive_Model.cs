using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserLiveReport
{
    public class LaserLive_Model
    {
        public string Date { get; set; }
        public string MachineID { get; set; }
        public string Model { get; set; }
        public string JobNo { get; set; }
        public string PartNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TakeTime { get; set; }
        public string OkQty { get; set; }
        public string NgQty { get; set; }
        public string Output { get; set; }
        public string Rej { get; set; }
        public string Setup { get; set;}
        public string MRPQty { get; set; }
    }
}
