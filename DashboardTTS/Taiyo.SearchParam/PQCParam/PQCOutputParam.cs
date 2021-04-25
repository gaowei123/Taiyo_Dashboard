using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiyo.SearchParam.PQCParam
{
    public class PQCOutputParam:BaseParam
    {
        public string JobNo { get; set; }
        public string TrackingID { get; set; }
        public string PartNo { get; set; }
        public string MachineID { get; set; }

        public string Shift { get; set; }
        public string OpID { get; set; }
        
    }
}
