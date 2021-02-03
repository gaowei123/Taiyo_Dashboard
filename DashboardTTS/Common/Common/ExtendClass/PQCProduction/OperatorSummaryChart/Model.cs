using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.OperatorSummaryChart
{
    public class Model
    {
        public DateTime Date { get; set; }
        public string AxisXLabelName { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }


        public decimal LaserQty { get; set; }
        public decimal WIPQty { get; set; }
        public decimal PackOnlineQty { get; set; }
        public decimal PackOfflineQty { get; set; }

    }
}
