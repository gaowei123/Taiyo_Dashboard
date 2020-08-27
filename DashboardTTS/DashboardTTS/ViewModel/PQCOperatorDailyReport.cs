using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCOperatorDailyReport
    {


        public string sn { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string operatedTime { get; set; }
        public string partNo { get; set; }
        public string lotNo { get; set; }
        public string process { get; set; }
        public string lotQty { get; set; }
        public double mouldRej { get; set; }
        public double paintRej { get; set; }
        public double laserRej { get; set; }
        public double othersRej { get; set; }
        public double totalRej { get; set; }

        public string mouldRejDisplay { get; set; }
        public string paintRejDisplay { get; set; }
        public string laserRejDisplay { get; set; }
        public string othersRejDisplay { get; set; }
        public string totalRejDisplay { get; set; }

        public double passQty { get; set; }
        public double packingQty { get; set; }
        public double rejPrice { get; set; }



    }
}