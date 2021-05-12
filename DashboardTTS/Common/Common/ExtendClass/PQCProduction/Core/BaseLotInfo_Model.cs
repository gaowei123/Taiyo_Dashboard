using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BaseLotInfo_Model
    {
        public string JobNo { get; set; }
        public string LotNo { get; set; }
        public string PartNo { get; set; }
        public string PaintProcess { get; set; }
        public DateTime? MFGDate { get; set; }
        public decimal LotQtySET { get; set; }
        public decimal LotQtyPCS { get; set; }
    }
}
