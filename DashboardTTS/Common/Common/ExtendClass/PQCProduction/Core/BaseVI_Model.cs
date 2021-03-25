using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BaseVI_Model
    {
        public string TrackingID { get; set; }
        public string PartNo { get; set; }
        public string MachineID { get; set; }
        public string JobNo { get; set; }
        public string Opertor { get; set; }
        public DateTime Day { get; set; }
        public string Shift { get; set; }
        public bool NextViFlag { get; set; }
        public string Status { get; set; }
        public string Processes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public decimal TotalQty { get; set; }
        public decimal PassQty { get; set; }
        public decimal RejQty { get; set; }
        public decimal LoseAmounts { get; set; }

        public Taiyo.Enum.Production.PQCReportType ProductType { get; set; }
    }
}
