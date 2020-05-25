using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCCheckingMaintenance_ViewModel
    {

        public class JobInfo 
        {
            public DateTime day { get; set; }
            public string shift { get; set; }
            public string jobNo { get; set; }
            public string trackingID { get; set; }
            

            public string partNo { get; set; }
            public double mrpQty { get; set; }
        }





        public class MaterialInfo
        {
            public int sn { get; set; }
            public string materialNo { get; set; }
            public double passQty { get; set; }
            public double rejQty { get; set; }
        }


        public class DefectInfo
        {

            public string tabSN { get; set; }


            public string materialNo { get; set; }
            public int defectCodeID { get; set; }
            public string defectCode { get; set; }
            public string defectDescription { get; set; }
            public double rejQty { get; set; }
        }



    }
}