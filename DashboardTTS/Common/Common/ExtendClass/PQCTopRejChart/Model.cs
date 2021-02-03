using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCTopRejChart
{
    public class Model
    {
        public class TopPartNo
        {
            public string PartNo { get; set; }
            public decimal TotalQty { get; set; }
            public decimal RejQty { get; set; }
           
        }

        public class TopDefect
        {
            public string DefectCode { get; set; }
            public decimal TotalQty { get; set; }
            public decimal RejQty { get; set; }
        }
    }
}