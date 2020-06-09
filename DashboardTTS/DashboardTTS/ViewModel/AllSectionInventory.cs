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








        public class pqcBomInfo
        {
            public string model { get; set; }
            public string partNo { get; set; }
            public string materialName { get; set; }
            public string materialNo { get; set; }
         
            public string processes { get; set; }
            public string shipTo { get; set; }
        }

        public class laserOutputInfo
        {
            public string jobNo { get; set; }
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public double okQty { get; set; }
            public double ngQty { get; set; }
            public string jobStatus { get; set; }
        }
        
        public class laserInventoryInfo
        {
            public string jobNo { get; set; }
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public double qty { get; set; }
        }

        public class pqcOutputInfo
        {
            public string jobNo { get; set; }
            public string partNo { get; set; }
            public string checkProcess { get; set; }
            public bool nextViFlag { get; set; }
            public string materialNo { get; set; }
            public string materialName { get; set; }
            public double passQty { get; set; }
            public double rejectQty { get; set; }
            public string allProcess { get; set; }
        }






        public class report
        {
            public string model { get; set; }
            public string partNo { get; set; }
            public string jobNo { get; set; }
            public string materialName { get; set; }



            public double? rawPartMoulding { get; set; }
            public double? ucPaint { get; set; }
            public double? mcPaint { get; set; }
            public double? tcPaint { get; set; }


            public double? beforeLaser { get; set; }
            public double? afterLaser { get; set; }
            public double? beforeWIP { get; set; }
            public double? afterWIP { get; set; }
            public double? beforePack { get; set; }
            public double? afterPack { get; set; }


            public double? beforeFG { get; set; }
            public double? beforeAssembly { get; set; }

            public double? afterFG { get; set; }
            public double? afterAssembly { get; set; }


            public double? lineRej { get; set; }


        }


    }
}