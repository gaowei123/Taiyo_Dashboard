using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.CheckingLiveReport
{
    public class Model
    {
        public string Date { get; set; }
        public string TrackingID { get; set; }
        public string Station { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string LotNo { get; set; }
        public string Processes { get; set; }
        public string IsComplete { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CostTime { get; set; }
        public string Status { get; set; }
        public decimal OKQty { get; set; }
        public decimal NGQty { get; set; }
        public decimal Output { get; set; }
        public string RejRate { get; set; }
        public string Operator { get; set; }
    }
}