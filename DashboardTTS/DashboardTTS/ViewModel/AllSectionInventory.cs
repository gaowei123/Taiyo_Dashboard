using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class AllSectionInventory
    {
        public class paintingInfo
        {
            public string model { get; set; }
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public string jobNo { get; set; }
            public double output { get; set; }
            public string sendingTo { get; set; }
        }
        
        public class laserInfo
        {
            public string materialNo { get; set; }
            public double outputQty { get; set; }
        }
        
        public class pqcCheckInfo
        {
            public string jobNo { get; set; }
            public string materialNo { get; set; }
            public double outputQty { get; set; }
            public string process { get; set; }
        }
        
        public class pqcBinInfo
        {
            public string jobNo { get; set; }
            public string materialNo { get; set; }
            public string process { get; set; }
            public double binQty { get; set; }
            public string shipTo { get; set; }

            public string allProcess { get; set; }

        }





        public class report
        {
            public string model { get; set; }
            public string partNo { get; set; }
            public string jobNo { get; set; }
            public string materialNo { get; set; }
            
            public double? paintInventory { get; set; }
            public double? laserInventory { get; set; }
            public double? checkInventory_1 { get; set; }
            public double? checkInventory_2 { get; set; }
            public double? packingInventory { get; set; }
            

        }


    }
}