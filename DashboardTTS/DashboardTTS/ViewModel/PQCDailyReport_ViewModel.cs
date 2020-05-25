using System;
using System.Collections.Generic;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCDailyReport_ViewModel
    {


        
        

        public string sDate { get; set; }
        public string station { get; set; }
        public string partNo { get; set; }
        public string jobNo { get; set; }
        public string lotNo { get; set; }

        public double dMrpQtyPcs { get; set; }
        public double dTotalQtyPcs { get; set; }
        public double dTotalPassPcs { get; set; }


        public double dMrpQtySet { get; set; }
        public double dTotalQtySet { get; set; }
        public double dTotalPassSet { get; set; }


        public string sMrpQty { get; set; }
        public string sTotalQty { get; set; }
        public string sTotalPass { get; set; }
        public double mouldingRej { get; set; }
        public double paintingRej { get; set; }
        public double laserRej { get; set; }
        public double othersRej { get; set; }
        public double totalRej { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? stopTime { get; set; }


        public double dUsedTime { get; set; }
        public string sUsedTime { get; set; }

        public string pic { get; set; }
    }
}