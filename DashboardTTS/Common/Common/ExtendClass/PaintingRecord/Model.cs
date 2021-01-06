using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PaintingRecord
{
    public class Model
    {
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string LotNo { get; set; }
        public string MRPQty { get; set; }
        public decimal Rej { get; set; }
        public string Process { get; set; }
        public string SendingTo { get; set; }
        public string Description { get; set; }
        public DateTime S_MFGDate { get; set; }
        public string MFGDate { get; set; }
        public DateTime S_ScanDate { get; set; }
        public string ScanDate { get; set; }
        public string Status { get; set; }
    }
}
