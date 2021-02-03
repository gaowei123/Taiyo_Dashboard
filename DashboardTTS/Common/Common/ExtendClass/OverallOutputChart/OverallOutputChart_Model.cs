using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.OverallOutputChart
{
    public class OverallOutputChart_Model
    {
        public string Department { get; set; }
        public decimal TotalQty { get; set; }
        public decimal RejQty { get; set; }
        public string RejRate { get; set; }        
    }
}
