using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiyo.SearchParam.LaserParam
{
    public class LaserLiveParam:BaseParam
    {
        public string Shift { get; set; }
        public string Model { get; set; }
        public string PartNo { get; set; }
        public string MachineID { get; set; }
        public string JobNo { get; set; }
    }
}
