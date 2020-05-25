using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCRealTime_ViewModel
    {
        public double totalOutput { get; set; }
        public double totalRejQty { get; set; }
        public string totalRejRate { get; set; }


        public List<currentInfo> currentInfoList { get; set; }
        public class currentInfo {
            public int id { get; set; }
            public string station { get; set; }
            public string status { get; set; }
            
            public string lotNo { get; set; }
            public string jobNo { get; set; }
            public string partNo { get; set; }
            public double mrpTotal { get; set; }
            public double ok { get; set; }
            public double ng { get; set; }
            public string rejRate { get; set; }
            public double rejPPM { get; set; }
            public string op { get; set; }
            public string imgPath { get; set; }
        }


        public class baseInfo
        {
            public string station { get; set; }
            public string nextViFlag { get; set; }
          

            public string lotNo { get; set; }
            public string jobNo { get; set; }
            public string partNo { get; set; }
            public double mrpTotal { get; set; }
            public double ok { get; set; }
            public double ng { get; set; }
            public string op { get; set; }
            public DateTime dateTime { get; set; }
        }

    }
}