using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldMaintenance
    {

        public class Tracking
        {
            public string trackingID { get; set; }
            public DateTime day { get; set; }
            public string shift { get; set; }
            public string machineID { get; set; }
            public string partNo { get; set; }
            public string jigNo { get; set; }
            public string model { get; set; }
            public double totalQty { get; set; }
            public double passQty { get; set; }
            public double rejQty { get; set; }
            public double setup { get; set; }

            public double wastedMaterial01 { get; set; }
            public double wastedMaterial02 { get; set; }

            public DateTime datetime { get; set; }


        }


    }
}