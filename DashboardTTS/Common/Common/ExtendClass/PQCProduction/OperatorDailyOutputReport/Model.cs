using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.OperatorDailyOutputReport
{
    public class Model
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string OperatedTime { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public decimal CheckOnlineQty { get; set; }
        public decimal CheckWIPQty { get; set; }
        public decimal PackOnlineQty { get; set; }
        public decimal PackOfflineQty { get; set; }
        public decimal LotQty { get; set; }
        public string MouldRej { get; set; }
        public string PaintRej { get; set; }
        public string LaserRej { get; set; }
        public string OthersRej { get; set; }
        public string TotalRej { get; set; }
        public decimal PassQty { get; set; }
        public decimal LoseAmounts { get; set; }
        public string Operator { get; set; }
    }
}
