using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCRejDetailReport
{
    public class RejDetail_Model
    {
        public DateTime Day { get; set; }
        public string Shift { get; set; }
        public string Station { get; set; }
        public string Model { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string LotNo { get; set; }
        public string MaterialNo { get; set; }
        public string DefectDescriptoin { get; set; }
        public string DefectCode { get; set; }
        public decimal TotalQty { get; set; }
        public decimal RejQty { get; set; }
        public string RejRate { get; set; }
        public string PIC { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
