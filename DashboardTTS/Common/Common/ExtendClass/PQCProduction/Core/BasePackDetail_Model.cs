using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BasePackDetail_Model
    {
        public string TrackingID { get; set; }
     
        public string JobNo { get; set; }

        public string MaterialName { get; set; }
        public string MaterialPartNo { get; set; }
        public decimal TotalQty { get; set; }

        public decimal PassQty { get; set; }

        public decimal RejQty { get; set; }
    }
}
