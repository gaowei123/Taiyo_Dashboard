using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiyo.Data.Query
{
    public class PaintQuery
    {
        public class Delivery: BaseQuery
        {
            public string PartNo { get; set; }
            public string JobNo { get; set; }
            public string LotNo { get; set; }
            public string PaintProcess { get; set; }
            public string SendingTo { get; set; }
            // 是否被laser/wip库存删除的flag.
            public bool? Status { get; set; }
        }
    }
}
