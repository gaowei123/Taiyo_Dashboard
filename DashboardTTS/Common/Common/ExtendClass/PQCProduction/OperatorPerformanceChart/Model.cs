using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.OperatorPerformanceChart
{
    public class Model
    {
        public string UserID { get; set; }
        public decimal LaserQty { get; set; }
        public decimal WIPQty { get; set; }
        public decimal PackOnlineQty { get; set; }
        public decimal PackOfflineQty { get; set; }
    }
}
