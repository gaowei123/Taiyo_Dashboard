using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiyo.SearchParam.PQCParam
{
    public class CheckingLiveParam: PQCOperatorParam
    {
        public string PartNo { get; set; }
        public string MachineID { get; set; }
        public string JobNo { get; set; }
        public string LotNo { get; set; }
    }
}
