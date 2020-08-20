using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class OverallOutputChart_ViewModel
    {

        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }


        public string department { get; set; }

        public double lotQty { get; set; }
        public double passQty { get; set; }
        public double rejQty { get; set; }

        public double mouldRej { get; set; }
        public double mouldTTSRej { get; set; }
        public double mouldVendorRej { get; set; }
        public double paintRej { get; set; }
        public double laserRej { get; set; }
        public double othersRej { get; set; }






    }
}