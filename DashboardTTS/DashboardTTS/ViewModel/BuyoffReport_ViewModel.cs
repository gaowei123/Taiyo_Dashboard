using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class BuyoffReport_ViewModel
    {
        public class LaserRecord
        {
            public string machineID { get; set; }
            public string lotNo { get; set; }
            public string lotQty { get; set; }
            public string partNo { get; set; }
            public string current { get; set; }

            public string blackMark1st { get; set; }
            public string blackDot1st { get; set; }
            public string pinHole1st { get; set; }
            public string jagged1st { get; set; }
            public string checkGuide1st { get; set; }
            public string navitas1st { get; set; }
            public string smartScope1st { get; set; }

            public string blackMark2nd { get; set; }
            public string blackDot2nd { get; set; }
            public string pinHole2nd { get; set; }
            public string jagged2nd { get; set; }
            public string checkGuide2nd { get; set; }
            public string navitas2nd { get; set; }
            public string smartScope2nd { get; set; }

            public string blackMarkIN { get; set; }
            public string blackDotIN { get; set; }
            public string pinHoleIN { get; set; }
            public string jaggedIN { get; set; }
            public string checkGuideIN { get; set; }
            public string navitasIN { get; set; }
            public string smartScopeIN { get; set; }



            public string blackMark1stColor { get; set; }
            public string blackDot1stColor { get; set; }
            public string pinHole1stColor { get; set; }
            public string jagged1stColor { get; set; }
            public string checkGuide1stColor { get; set; }
            public string navitas1stColor { get; set; }
            public string smartScope1stColor { get; set; }

            public string blackMark2ndColor { get; set; }
            public string blackDot2ndColor { get; set; }
            public string pinHole2ndColor { get; set; }
            public string jagged2ndColor { get; set; }
            public string checkGuide2ndColor { get; set; }
            public string navitas2ndColor { get; set; }
            public string smartScope2ndColor { get; set; }

            public string blackMarkINColor { get; set; }
            public string blackDotINColor { get; set; }
            public string pinHoleINColor { get; set; }
            public string jaggedINColor { get; set; }
            public string checkGuideINColor { get; set; }
            public string navitasINColor { get; set; }
            public string smartScopeINColor { get; set; }


            public string mcOperator { get; set; }
            public string buyoffBy { get; set; }
            public string approvedBy { get; set; }
            public string checkBy { get; set; }

            public DateTime? date { get; set; }
            
        }



        public class LaserParameter
        {
            public string rate { get; set; }
            public string frequency { get; set; }
            public string power { get; set; }
            public string repeat { get; set; }

        }


        public class MouldDefect
        {
            public string materialNo { get; set; }

            public double rawPartScratch { get; set; }
            public double oilStain { get; set; }
            public double dented { get; set; }
            public double dust { get; set; }
            public double flyout { get; set; }
            public double overSpray { get; set; }
            public double weldline  { get; set; }
            public double crack { get; set; }
            public double gasMark { get; set; }
            public double sinkMark  { get; set; }
            public double bubble { get; set; }
            public double whiteDot { get; set; }
            public double blackDot  { get; set; }
            public double redDot  { get; set; }
            public double poorGateCut { get; set; }
            public double highGate { get; set; }
            public double whiteMark { get; set; }
            public double dragMark { get; set; }
            public double foreighMaterial { get; set; }
            public double doubleClaim { get; set; }
            public double shortMould { get; set; }
            public double flashing { get; set; }
            public double pinkMark { get; set; }
            public double deform { get; set; }
            public double damage { get; set; }
            public double mouldDirt { get; set; }
            public double yelloWish { get; set; }
            public double oilMark { get; set; }
            public double printingMark { get; set; }
            public double printingUneven { get; set; }
            public double printingColorDark { get; set; }
            public double wrongOrietation { get; set; }
            public double other { get; set; }


            public double totalRej { get; set; }
        }


        public class PaintDefect
        {
            public string materialNo { get; set; }

            public double particle { get; set; }
            public double fibre { get; set; }
            public double manyParticle { get; set; }
            public double stainMark { get; set; }
            public double unevenPaint { get; set; }
            public double underCoatUnevenPaint { get; set; }
            public double underSpray { get; set; }
            public double whiteDot { get; set; }
            public double silverDot { get; set; }
            public double dust { get; set; }
            public double paintCrack { get; set; }
            public double bubble { get; set; }
            public double scratch { get; set; }
            public double abrasionMark { get; set; }
            public double paintDripping { get; set; }
            public double roughSurface { get; set; }
            public double shinning { get; set; }
            public double matt { get; set; }
            public double paintPinHole { get; set; }
            public double lightLeakage { get; set; }
            public double whiteMark { get; set; }
            public double dented { get; set; }
            public double particleForLaserSetup { get; set; }
            public double buyoff { get; set; }
            //public double shortage { get; set; }
            //public double qa { get; set; }
            public double setup { get; set; }
            public double other { get; set; }


            public double totalRej { get; set; }
        }


        public class LaserDefect
        {
            public string materialNo { get; set; }
            

            public double blackMark { get; set; }
            public double blackDot { get; set; }
            public double graphicShiftCheckByPQC { get; set; }
            public double graphicShiftCheckByMC { get; set; }
            public double scratch { get; set; }
            public double jagged { get; set; }
            public double laserBubble { get; set; }
            public double doublOuterLine { get; set; }
            public double pinHold { get; set; }
            public double poorLaser { get; set; }
            public double burmMark { get; set; }
            public double stainMark { get; set; }
            public double graphicSmall { get; set; }
            public double doubleLaser { get; set; }
            public double colorYellow { get; set; }
            public double crack { get; set; }
            public double smoke { get; set; }
            public double wrongOrientation { get; set; }
            public double dented { get; set; }
            public double setup { get; set; }
            public double buyoff { get; set; }

            public double other { get; set; }
           
            public double totalRej { get; set; }
        }



        public class OthersDefect
        {
            public string materialNo { get; set; }


            public double pqcScratch { get; set; }
          
            public double overSpray { get; set; }
            public double bubble { get; set; }
            public double oilStain { get; set; }
            public double dragMark { get; set; }
            public double lightLeakage { get; set; }
            public double lightBubble { get; set; }
            public double whiteDotInMaterial { get; set; }

            public double other { get; set; }

            public double totalRej { get; set; }
            public double qa { get; set; }
        }



        public class PQCBuyoffList
        {
            public string jobNo { get; set; }
            public string materialNo { get; set; }



            
            public DateTime? topDate { get; set; }
            public string topMachine { get; set; }
            public string topRunTime { get; set; }
            public string topOvenTime { get; set; }
            public string topPaintLot { get; set; }
            public string topThinnersLot { get; set; }
            public string topThichness { get; set; }
            public string topPaintPIC { get; set; }




            public DateTime? middleDate { get; set; }
            public string middleMachine { get; set; }
            public string middleRunTime { get; set; }
            public string middleOvenTime { get; set; }
            public string middlePaintLot { get; set; }
            public string middleThinnersLot { get; set; }
            public string middleThichness { get; set; }
            public string middlePaintPIC { get; set; }




            public DateTime? underDate { get; set; }
            public string underMachine { get; set; }
            public string underRunTime { get; set; }
            public string underOvenTime { get; set; }
            public string underPaintLot { get; set; }
            public string underThinnersLot { get; set; }
            public string underThichness { get; set; }
            public string underPaintPIC { get; set; }



            public string temperatureFront { get; set; }
            public string temperatureRear { get; set; }
            public string humidityFront { get; set; }
            public string humidityRear { get; set; }



            public DateTime dateTime { get; set; }





        }



    }
}

