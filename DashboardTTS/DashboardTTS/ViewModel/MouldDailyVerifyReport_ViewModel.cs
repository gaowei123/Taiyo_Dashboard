using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldDailyVerifyReport_ViewModel
    {

        public class TrackingInfo
        {
            public string trackingID { get; set; }
            public DateTime day { get; set; } 
            public string shift { get; set; }
            public string machineID { get; set; }
            public string model { get; set; }
            public string partNo { get; set; }
            public string jigNo { get; set; }
          
            public double cavityCount { get; set; }
            public double passQty { get; set; }
            public double rejQty { get; set; }
            public double totalQty { get; set; }
            public double ipqcRejQty { get; set; }
            public double setUp { get; set; }
            public decimal wastageMaterial01 { get; set; }
            public decimal wastageMaterial02 { get; set; }
            public string userID { get; set; }
            public string supervisor { get; set; }
            public string partNoALL { get; set; }
            public DateTime startTime { get; set; }
            public DateTime stopTime { get; set; }

        }



        public class DefectInfo
        {
            public string trackingID { get; set; }
            public DateTime day { get; set; }
            public string shift { get; set; }
            public string machineID { get; set; }
            public string partNo { get; set; }
            public string partNoALL { get; set; }


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


        public class Report
        {
            public string machineID { get; set; }
            public string model { get; set; }
            public string partNo { get; set; }
            public string partNoALL { get; set; }
            public string jigNo { get; set; }
            public string mcRunHours { get; set; }
            public double cavityCount { get; set; }
            public double totalShots { get; set; }
            public double passQty { get; set; }
            public double rejQty { get; set; }
            public string rejRate { get; set; }
         
           

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



            public double ipqcRejQty { get; set; }
            public decimal wastageMaterial { get; set; }
            public double setUp { get; set; }
            public string setUpRate { get; set; }
            public string userID { get; set; }
            public string supervisor { get; set; }
        }


        public class SummaryReport
        {
            public string description { get; set; }
            public string totalQty { get; set; }
            public string totalPass { get; set; }
            public string totalRejQty { get; set; }
            public string totalRejRate { get; set; }
            public string rejCost { get; set; }
        }



        

    }
}