﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class AllSectionInventory
    {
        public class mainMaterialList
        {
            public string model { get; set; }
            public string partNo { get; set; }           
            public string materialName { get; set; }
            public string shipTo { get; set; }
        }


        public class pqcBomInfo
        {
            public string model { get; set; }
            public string partNo { get; set; }
            public string materialName { get; set; }
            public string materialNo { get; set; }
         
            public string processes { get; set; }
            public string shipTo { get; set; }
            public int outerBoxQty { get; set; }
        }


        public class laserOutputInfo
        {
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public double okQty { get; set; }
            public double ngQty { get; set; }
        }

        
        public class laserInventoryInfo
        {
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public double qty { get; set; }
        }


        public class wipOutputInfo
        {
            public string partNo { get; set; }
            public string materialName { get; set; }
            public double passQty { get; set; }
            public double rejectQty { get; set; }        
        }


        public class wipInventoryInfo
        {
            public string partNo { get; set; }
            public string materialName { get; set; }
            public double inventoryQty { get; set; }
        }
        

        public class fgAndAssembly
        {
            public string partNo { get; set; }
            public string materialName { get; set; }
            public double fg { get; set; }
            public double assembly { get; set; }
        }

        public class pqcBinInfo
        {
            public string partNo { get; set; }
            public string processes { get; set; }
            public string materialName { get; set; }
            public double qty { get; set; }
        }
        

        public class report
        {
            public string model { get; set; }
            public string partNo { get; set; }         
            public string materialName { get; set; }
            public string shipTo { get; set; }



            public double? rawPart { get; set; }
            public double? ucPaint { get; set; }
            public double? mcPaint { get; set; }
            public double? print { get; set; }
            public double? tcPaint { get; set; }


            public double? beforeLaser { get; set; }
            public double? afterLaser { get; set; }
            public double? beforeWIP { get; set; }
            public double? afterWIP { get; set; }
            public double? beforePack { get; set; }
            public double? afterPack { get; set; }


            public double? fg { get; set; }
            public double? assembly { get; set; }
            


        }


    }
}