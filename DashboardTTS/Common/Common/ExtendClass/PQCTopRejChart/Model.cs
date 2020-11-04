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
            public int RejQty { get; set; }
        }

        public class TopDefect
        {
            public string DefectCode { get; set; }
            public int RejQty { get; set; }
        }
    }
}