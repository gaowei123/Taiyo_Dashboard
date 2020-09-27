using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserProductionChart
{
    public class LaserProduction_Model
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Day { get; set; }

        public string MachineID { get; set; }
        public string PartNo { get; set; }
        public string Model { get; set; }
        public string Customer { get; set; }

        public decimal TotalQty{get;set;}
        public decimal PassQty { get; set; }
        public decimal RejQty { get; set; }

        public DateTime CreatedTime { get; set; }
        
    }
}
