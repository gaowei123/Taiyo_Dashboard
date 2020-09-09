using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.Model
{
    public class OverallOutputChart_Model
    {

        public string Department { get; set; }

        public double TotalQty { get; set; }
        public double PassQty { get; set; }

        public double RejQty { get; set; }

        public double? MouldRej { get; set; }
        public double? TTSMouldRej { get; set; }
        public double? VendorMouldRej { get; set; }

        public double? PaintRej { get; set; }

        public double? LaserRej { get; set; }
        public double? OthersRej { get; set; }


        public double? MouldSetup { get; set; }
        public double? IPQCRej { get; set; }


        public double? PaintQARej { get; set; }
        public double? PaintSetupRej { get; set; }



        public double? LaserSetup { get; set; }
        public double? LaserBuyoff { get; set; }
        public double? LaserShortage { get; set; }
        
    }
}
