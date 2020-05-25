using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldDailyReport_ViewModel
    {

        public class VIData
        {
            public DateTime date { get; set; }
            public string shift { get; set; }
            public string machineID { get; set; }
            public string jigNo { get; set; }
            public string partNo { get; set; }
            public double output { get; set; }
            public double passQty { get; set; }
            public double rejQty { get; set; }
            public string rejRate { get; set; }
            public string opID { get; set; }
        }

        public class DefectData
        {
            public DateTime date { get; set; }
            public string shift { get; set; }
            public string machineID { get; set; }
            public string partNo { get; set; }
            public int whiteDot { get; set; }
            public int scratches { get; set; }
            public int dentedMark { get; set; }
            public int shinningDot { get; set; }
            public int blackMark { get; set; }
            public int sinkMark { get; set; }
            public int flowMark { get; set; }
            public int highGate { get; set; }
            public int silverSteak { get; set; }
            public int blackDot { get; set; }
            public int oilStain { get; set; }
            public int flowLine { get; set; }
            public int overCut { get; set; }
            public int crack { get; set; }
            public int shortMold { get; set; }
            public int stainMark { get; set; }
            public int weldLine { get; set; }
            public int flashes { get; set; }
            public int foreignMaterial { get; set; }
            public int drag { get; set; }
            public int materialBleed { get; set; }
            public int bent { get; set; }
            public int deform { get; set; }
            public int gasMark { get; set; }
        }


      
       
        


    }
}