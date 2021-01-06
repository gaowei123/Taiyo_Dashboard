using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiyo.SearchParam.PaintingParam
{
    public class DeliveryRecordParam: BaseParam
    {
        public string PartNo { get; set; }
        public string SendingTo { get; set; }
        public string JobNo { get; set; }
    }
}
