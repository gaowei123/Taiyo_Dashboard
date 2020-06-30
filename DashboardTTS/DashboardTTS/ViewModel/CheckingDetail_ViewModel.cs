using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class CheckingDetail_ViewModel
    {
        public string trackingID { get; set; }

        public DateTime? day { get; set; }
        public string shift { get; set; }
        public string partNo { get; set; }
        public string station { get; set; }
        public string processes { get; set; }
        public string lotNo { get; set; }
        public string jobID { get; set; }



        public double okQty { get; set; }
        public double ngQty { get; set; }

        public double totalQty { get; set; }


        public double mouldRej { get; set; }
        public double paintRej { get; set; }
        public double laserRej { get; set; }
        public double othersRej { get; set; }


        public string PIC { get; set; }


        public DateTime dateTime { get; set; }


    }
}