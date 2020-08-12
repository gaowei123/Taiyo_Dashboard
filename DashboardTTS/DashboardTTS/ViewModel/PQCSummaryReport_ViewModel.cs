using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCSummaryReport_ViewModel
    {
        
       

        public class Report
        {
            public string pqcDept { get; set; }
            public double totalOutput { get; set; }
            public double ttsMouldRej { get; set; }
            public double vendorsModelRej { get; set; }
            public double paintRej { get; set; }
            public double laserRej { get; set; }
            public double othersRej { get; set; }
            public double totalRej { get; set; }
            public double actualOutput { get; set; }


            public string ttsMouldRejRate { get; set; }
            public string vendorsModelRejRate { get; set; }
            public string paintRejRate { get; set; }
            public string laserRejRate { get; set; }
            public string othersRejRate { get; set; }
            public string totalRejRate { get; set; }
        }

        public class ViDetail
        {
            public string trackingID { get; set; }
            public string partNo { get; set; }
            public double totalQty { get; set; }
            public double acceptQty { get; set; }
            public double rejectQty { get; set; }
            public string currentProcess { get; set; }


            public string description { get; set; }
            public string number { get; set; }
            public bool isContainLaser { get; set; }
            public string lastCheckProcess { get; set; }



            public double ttsRej { get; set; }
            public double vendorRej { get; set; }
            public double paintRej { get; set; }
            public double laserRej { get; set; }
            public double othersRej { get; set; }

          
            
        }

       
       
        
    }
}