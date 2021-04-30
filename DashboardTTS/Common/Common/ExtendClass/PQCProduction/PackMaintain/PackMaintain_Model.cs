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
            MaterialPartList = new List<MaterialInfo>();
        }


        public JobInfo Job { get; set; }
        public List<MaterialInfo> MaterialPartList { get; set; }

      

        
       
        public class JobInfo
        {
            public string JobNo { get; set; }
            public string TrackingID { get; set; }
            public DateTime? Day { get; set; }
            public string Shift { get; set; }
            public string PartNo { get; set; }
            public decimal MRPQty { get; set; }
            public decimal PackQty { get; set; }
        }

        public class MaterialInfo
        {
            public string MaterialName { get; set; }
            public string MaterialPartNo { get; set; }

            // bin check 的数量
            public decimal InventoryQty { get; set; }         
               
            // bin his scrap 的数量
            public decimal ScrapQty { get; set; }

            // pack detial tracking 中的数量
            public decimal MaterialQty { get; set; }

            // 维护后填的数量
            public decimal UpdatedQty { get; set; }

        }
    }
}
