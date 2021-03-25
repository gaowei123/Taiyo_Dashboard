using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PQCButtonReport_ViewModel
    {
        public class Report
        {
            public int SN { get; set; }
            public string model { get; set; }

            public string partsType { get; set; }
            public string jobID { get; set; }
            public string lotNo { get; set; }
            public string partNo { get; set; }
            public string materialNo { get; set; }
            public double lotQty { get; set; }
            public double pass { get; set; }
            public double rejQty { get; set; }         
            public double rejCost { get; set; }
            public double rejRate { get; set; }
            public string rejRateDisplay { get; set; }
            public string supplier { get; set; }



            public double ttsLotQty { get; set; }
            public double vendorLotQty { get; set; }


            //mould TTS defect code
            public double TTS_Raw_Part_Scratch { get; set; }
            public double TTS_Oil_Stain { get; set; }
            public double TTS_Dented { get; set; }
            public double TTS_Dust { get; set; }
            public double TTS_Flyout { get; set; }
            public double TTS_Over_Spray { get; set; }
            public double TTS_Weld_line { get; set; }
            public double TTS_Crack { get; set; }
            public double TTS_Gas_mark { get; set; }
            public double TTS_Sink_mark { get; set; }
            public double TTS_Bubble { get; set; }
            public double TTS_White_dot { get; set; }
            public double TTS_Black_dot { get; set; }
            public double TTS_Red_Dot { get; set; }
            public double TTS_Poor_Gate_Cut { get; set; }
            public double TTS_High_Gate { get; set; }
            public double TTS_White_Mark { get; set; }
            public double TTS_Drag_mark { get; set; }
            public double TTS_Foreigh_Material { get; set; }
            public double TTS_Double_Claim { get; set; }
            public double TTS_Short_mould { get; set; }
            public double TTS_Flashing { get; set; }
            public double TTS_Pink_Mark { get; set; }
            public double TTS_Deform { get; set; }
            public double TTS_Damage { get; set; }
            public double TTS_Mould_Dirt { get; set; }
            public double TTS_Yellowish { get; set; }
            public double TTS_Oil_Mark { get; set; }
            public double TTS_Printing_Mark { get; set; }
            public double TTS_Printing_Uneven { get; set; }
            public double TTS_Printing_Color_Dark { get; set; }
            public double TTS_Wrong_Orietation { get; set; }
            public double TTS_Other { get; set; }

            //mould Vendor defect code
            public double Vendor_Raw_Part_Scratch { get; set; }
            public double Vendor_Oil_Stain { get; set; }
            public double Vendor_Dented { get; set; }
            public double Vendor_Dust { get; set; }
            public double Vendor_Flyout { get; set; }
            public double Vendor_Over_Spray { get; set; }
            public double Vendor_Weld_line { get; set; }
            public double Vendor_Crack { get; set; }
            public double Vendor_Gas_mark { get; set; }
            public double Vendor_Sink_mark { get; set; }
            public double Vendor_Bubble { get; set; }
            public double Vendor_White_dot { get; set; }
            public double Vendor_Black_dot { get; set; }
            public double Vendor_Red_Dot { get; set; }
            public double Vendor_Poor_Gate_Cut { get; set; }
            public double Vendor_High_Gate { get; set; }
            public double Vendor_White_Mark { get; set; }
            public double Vendor_Drag_mark { get; set; }
            public double Vendor_Foreigh_Material { get; set; }
            public double Vendor_Double_Claim { get; set; }
            public double Vendor_Short_mould { get; set; }
            public double Vendor_Flashing { get; set; }
            public double Vendor_Pink_Mark { get; set; }
            public double Vendor_Deform { get; set; }
            public double Vendor_Damage { get; set; }
            public double Vendor_Mould_Dirt { get; set; }
            public double Vendor_Yellowish { get; set; }
            public double Vendor_Oil_Mark { get; set; }
            public double Vendor_Printing_Mark { get; set; }
            public double Vendor_Printing_Uneven { get; set; }
            public double Vendor_Printing_Color_Dark { get; set; }
            public double Vendor_Wrong_Orietation { get; set; }
            public double Vendor_Other { get; set; }

            //paint defect code 
            public double Paint_Particle { get; set; }
            public double Paint_Fibre { get; set; }
            public double Paint_Many_particle { get; set; }
            public double Paint_Stain_mark { get; set; }
            public double Paint_Uneven_paint { get; set; }
            public double Paint_Under_coat_uneven_paint { get; set; }
            public double Paint_Under_spray { get; set; }
            public double Paint_White_dot { get; set; }
            public double Paint_Silver_dot { get; set; }
            public double Paint_Dust { get; set; }
            public double Paint_Paint_crack { get; set; }
            public double Paint_Bubble { get; set; }
            public double Paint_Scratch { get; set; }
            public double Paint_Abrasion_Mark { get; set; }
            public double Paint_Paint_Dripping { get; set; }
            public double Paint_Rough_Surface { get; set; }
            public double Paint_Shinning { get; set; }
            public double Paint_Matt { get; set; }
            public double Paint_Paint_Pin_Hole { get; set; }
            public double Paint_Light_Leakage { get; set; }
            public double Paint_White_Mark { get; set; }
            public double Paint_Dented { get; set; }
            public double Paint_Other { get; set; }
            public double Paint_Particle_for_laser_setup { get; set; }
            public double Paint_Buyoff { get; set; }
            public double Paint_Shortage { get; set; }

            //laser defect code
            public double Laser_Black_Mark { get; set; }
            public double Laser_Black_Dot { get; set; }
            public double Laser_Graphic_Shift_check_by_PQC { get; set; }
            public double Laser_Graphic_Shift_check_by_MC { get; set; }
            public double Laser_Scratch { get; set; }
            public double Laser_Jagged { get; set; }
            public double Laser_Laser_Bubble { get; set; }
            public double Laser_double_outer_line { get; set; }
            public double Laser_Pin_hold { get; set; }
            public double Laser_Poor_Laser { get; set; }
            public double Laser_Burm_Mark { get; set; }
            public double Laser_Stain_Mark { get; set; }
            public double Laser_Graphic_Small { get; set; }
            public double Laser_Double_Laser { get; set; }
            public double Laser_Color_Yellow { get; set; }
            public double Laser_Crack { get; set; }
            public double Laser_Smoke { get; set; }
            public double Laser_Wrong_Orientation { get; set; }
            public double Laser_Dented { get; set; }
            public double Laser_Other { get; set; }

            public double Laser_Buyoff { get; set; }
            public double Laser_Setup { get; set; }


            //others
            public double PQC_Scratch { get; set; }
            public double Over_Spray { get; set; }
            public double Bubble { get; set; }
            public double Oil_Stain { get; set; }
            public double Drag_Mark { get; set; }
            public double Light_Leakage { get; set; }
            public double Light_Bubble { get; set; }
            public double White_Dot_in_Material { get; set; }
            public double Other { get; set; }



            public double TTS_Mould_TotalRej { get; set; }
            public double TTS_Mould_TotalRejCost { get; set; }
            public double TTS_Mould_TotalRejRate { get; set; }
            public double Vendor_Mould_TotalRej { get; set; }
            public double Vendor_Mould_TotalRejCost { get; set; }
            public double Vendor_Mould_TotalRejRate { get; set; }
            public DateTime? MFGDate { get; set; }



            public double Paint_TotalRej { get; set; }
            public double Paint_TotalRejCost { get; set; }
            public double Paint_TotalRejRate { get; set; }
            public double Paint_SetupRej { get; set; }
            public double Paint_SetupRejCost { get; set; }
            public double Paint_SetupRejRate { get; set; }
            public double Paint_QATestRej { get; set; }
            public double Paint_QATestRejCost { get; set; }
            public double Paint_QATestRejRate { get; set; }

            public string paintCoat1st { get; set; }
            public string paintMachine1st { get; set; }
            public DateTime? paintDate1st { get; set; }
            public string paintCoat2nd { get; set; }
            public string paintMachine2nd { get; set; }
            public DateTime? paintDate2nd { get; set; }
            public string paintCoat3rd { get; set; }
            public string paintMachine3rd { get; set; }
            public DateTime? paintDate3rd { get; set; }




            public double Laser_TotalRej { get; set; }
            public double Laser_TotalRejCost { get; set; }
            public double Laser_TotalRejRate { get; set; }

            public string laserMachine { get; set; }
            public string laserOP { get; set; }
            public DateTime? laserDate { get; set; }




            public double Others_TotalRej { get; set; }
            public double Others_TotalRejCost { get; set; }
            public double Others_TotalRejRate { get; set; }

            public string InspBy { get; set; }

        }


      


        public class PQCDetail
        {
            public string jobID = "";
            public string model = "";
            public string partNumber = "";
            public string materialNo = "";
            public double lotQty = 0;
            public double passQty = 0;
            public double rejectQty = 0;
            public double rejectRate = 0;
            public string supplier = "";
            public string process = "";
            public string partsType = "";
            public string mouldType = "";
            public double unitCost = 0;


            public string OP = "";
            
        }

        public class PQCDefect
        {
            public string jobID = "";
            public string materialNo = "";
            public string defectCodeID = "";
            public string defectCode = "";
            public string defectDescription = "";
            public double rejectQty = 0;
            public string process = "";
        }

        public class LaserNG
        {
            public string jobNo = "";
            public string materialNo = "";
            public double ng = 0;
            public double shortage = 0;
            public double setup = 0;
            public double buyoff = 0;
        }

        public class LaserInfo
        {
            public string jobNo = "";
            public DateTime? laserDate = new DateTime();
            public string laserOP = "";
            public string laserMachine = "";
        }

        public class PaintTempInfo
        {
            public string jobNo = "";
            public string lotNo = "";
            public string materialName { get; set; }

            public DateTime? mfgDate = new DateTime();
            public double paintSetUpQty = 0;
            public double paintQAQty = 0;

            public string paintCoat1st = "";
            public string paintMachine1st = "";
            public DateTime? paintDate1st = new DateTime();

            public string paintCoat2nd = "";
            public string paintMachine2nd = "";
            public DateTime? paintDate2nd = new DateTime();

            public string paintCoat3rd = "";
            public string paintMachine3rd = "";
            public DateTime? paintDate3rd = new DateTime();
        }

        public class PaintDelivery
        {
            public string jobNo = "";
            public string lotNo = "";
            public string paintProcess = "";
            public int mrpQty = 0;
        }

        
    }

}