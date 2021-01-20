using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCSummaryReport
{
    public class Summary_Model
    {
        public class Report
        {
            public string Type { get; set; }
            public string PQCDept { get; set; }
            public decimal TotalOutput { get; set; }
            public string TTSMouldRej { get; set; }
            public string VendorsMouldRej { get; set; }
            public string PaintRej { get; set; }
            public string LaserRej { get; set; }
            public string OthersRej { get; set; }
            public string TotalRej { get; set; }
            public decimal ActualOutput { get; set; }
        }

        public class Detail
        {
            public string PartNo { get; set; }
            public string JobNo { get; set; }
            public string CurrentProcess { get; set; }
            public string Description { get; set; }
            public decimal TotalQty { get; set; }
            public decimal PassQty { get; set; }
            public decimal RejQty { get; set; }

            public decimal TTSMouldRej { get; set; }
            public decimal VendorMouldRej { get; set; }
            public decimal PaintRej { get; set; }
            public decimal LaserRej { get; set; }
            public decimal OthersRej { get; set; }

            public string IsContainLaser { get; set; }
            public string LastCheckProcess { get; set; }
            public string Number { get; set; }
        }
    }
}
