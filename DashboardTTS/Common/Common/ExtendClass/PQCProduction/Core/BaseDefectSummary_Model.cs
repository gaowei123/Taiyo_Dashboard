using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BaseDefectSummary_Model
    {
        public string TrackingID { get; set; }
        public string JobNo { get; set; }
        public decimal TTSMouldRej { get; set; }
        public decimal VendorMouldRej { get; set; }
        public decimal MouldRej { get; set; }
        public decimal PaintRej { get; set; }
        public decimal LaserRej { get; set; }
        public decimal OthersRej { get; set; }
    }
}
