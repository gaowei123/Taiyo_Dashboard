using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCProduction.PackMaintain
{
    public class PackMaintain_Model
    {
        public PackMaintain_Model()
        {
            Job = new JobInfo();
            MaterialNameList = new List<MaterialInfo>();
        }


        public JobInfo Job { get; set; }
        public List<MaterialInfo> MaterialNameList { get; set; }

      

        
       
        public class JobInfo
        {
            public string JobNo { get; set; }
            public string TrackingID { get; set; }
            public DateTime Day { get; set; }
            public string Shift { get; set; }
            public string PartNo { get; set; }
            public decimal MRPQty { get; set; }
        }

        public class MaterialInfo
        {
            public string MaterialName { get; set; }
            public string MaterialPartNo { get; set; }
            public decimal InventoryQty { get; set; }
            public decimal MaterialQty { get; set; }
            public decimal ScrapQty { get; set; }

        }
    }
}
