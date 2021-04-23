using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BaseBin_Model
    {
        public DateTime Day { get; set; }
        public string Shift { get; set; }
        public string TrackingID { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string MaterialName { get; set; }
        public string MaterialPartNo { get; set; }
        public decimal MaterialQty { get; set; }
        public string Status { get; set; }
        public string Processes { get; set; }
        public string ShipTo { get; set; }
        public string PackBundle { get; set; }
        public string UserID { get; set; }
    }
}
