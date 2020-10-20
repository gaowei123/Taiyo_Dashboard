using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserInventory
{
    public class LaserInventory_Model
    {
        public class Summary
        {
            public string Customer { get; set; }
            public string Model { get; set; }
            public string PartNo { get; set; }
            public string JobNo { get; set; }
            public string MRPQty { get; set; }
            public string BeforeQty { get; set; }
            public string AfterQty { get; set; }
            public int JobCount { get; set; }
            public string HourlyQty { get; set; }
            public string CycleTime { get; set; }
            public string MFGDate { get; set; }
            public string BomFlag { get; set; }
        }

        public class Detail
        {

        }


    }
}