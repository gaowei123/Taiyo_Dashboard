using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class BaseDefectDetail_Model
    {
        public string TrackingID { get; set; }
        public string MaterialPartNo { get; set; }
        
        public MouldDefectCode MouldDefect { get; set; }   
        public PaintDefectCode PaintDefect { get; set; }
        public LaserDefectCode LaserDefect { get; set; }
        public OthersDefectCode OthersDefect { get; set; }


        public class MouldDefectCode
        {
           
            public MouldType Mould_Raw_Part_Scratch { get; set; }
            public MouldType Mould_Oil_Stain { get; set; }
            public MouldType Mould_Dented { get; set; }
            public MouldType Mould_Dust { get; set; }
            public MouldType Mould_Flyout { get; set; }
            public MouldType Mould_Over_Spray { get; set; }
            public MouldType Mould_Weld_line { get; set; }
            public MouldType Mould_Crack { get; set; }
            public MouldType Mould_Gas_mark { get; set; }
            public MouldType Mould_Sink_mark { get; set; }
            public MouldType Mould_Bubble { get; set; }
            public MouldType Mould_White_dot { get; set; }
            public MouldType Mould_Black_dot { get; set; }
            public MouldType Mould_Red_Dot { get; set; }
            public MouldType Mould_Poor_Gate_Cut { get; set; }
            public MouldType Mould_High_Gate { get; set; }
            public MouldType Mould_White_Mark { get; set; }
            public MouldType Mould_Drag_mark { get; set; }
            public MouldType Mould_Foreigh_Material { get; set; }
            public MouldType Mould_int_Claim { get; set; }
            public MouldType Mould_Short_mould { get; set; }
            public MouldType Mould_Flashing { get; set; }
            public MouldType Mould_Pink_Mark { get; set; }
            public MouldType Mould_Deform { get; set; }
            public MouldType Mould_Damage { get; set; }
            public MouldType Mould_Mould_Dirt { get; set; }
            public MouldType Mould_Yellowish { get; set; }
            public MouldType Mould_Oil_Mark { get; set; }
            public MouldType Mould_Printing_Mark { get; set; }
            public MouldType Mould_Printing_Uneven { get; set; }
            public MouldType Mould_Printing_Color_Dark { get; set; }
            public MouldType Mould_Wrong_Orietation { get; set; }
            public MouldType Mould_Other { get; set; }
            
        }

        public class PaintDefectCode
        {
            public int Paint_Particle { get; set; }
            public int Paint_Fibre { get; set; }
            public int Paint_Many_particle { get; set; }
            public int Paint_Stain_mark { get; set; }
            public int Paint_Uneven_paint { get; set; }
            public int Paint_Under_coat_uneven_paint { get; set; }
            public int Paint_Under_spray { get; set; }
            public int Paint_White_dot { get; set; }
            public int Paint_Silver_dot { get; set; }
            public int Paint_Dust { get; set; }
            public int Paint_Paint_crack { get; set; }
            public int Paint_Bubble { get; set; }
            public int Paint_Scratch { get; set; }
            public int Paint_Abrasion_Mark { get; set; }
            public int Paint_Paint_Dripping { get; set; }
            public int Paint_Rough_Surface { get; set; }
            public int Paint_Shinning { get; set; }
            public int Paint_Matt { get; set; }
            public int Paint_Paint_Pin_Hole { get; set; }
            public int Paint_Light_Leakage { get; set; }
            public int Paint_White_Mark { get; set; }
            public int Paint_Dented { get; set; }
            public int Paint_Other { get; set; }
            public int Paint_Setup { get; set; }
            public int Paint_Shortage { get; set; }
        }

        public class LaserDefectCode
        {
            public int Laser_Black_Mark { get; set; }
            public int Laser_Black_Dot { get; set; }
            public int Laser_Graphic_Shift_check_by_PQC { get; set; }
            public int Laser_Graphic_Shift_check_by_MC { get; set; }
            public int Laser_Scratch { get; set; }
            public int Laser_Jagged { get; set; }
            public int Laser_Laser_Bubble { get; set; }
            public int Laser_int_outer_line { get; set; }
            public int Laser_Pin_hold { get; set; }
            public int Laser_Poor_Laser { get; set; }
            public int Laser_Burm_Mark { get; set; }
            public int Laser_Stain_Mark { get; set; }
            public int Laser_Graphic_Small { get; set; }
            public int Laser_Double_Laser { get; set; }
            public int Laser_Color_Yellow { get; set; }
            public int Laser_Crack { get; set; }
            public int Laser_Smoke { get; set; }
            public int Laser_Wrong_Orientation { get; set; }
            public int Laser_Dented { get; set; }
            public int Laser_Other { get; set; }

            public int Laser_Buyoff { get; set; }
            public int Laser_Setup { get; set; }
        }

        public class OthersDefectCode
        {
            public int Others_PQC_Scratch { get; set; }
            public int Others_Over_Spray { get; set; }
            public int Others_Bubble { get; set; }
            public int Others_Oil_Stain { get; set; }
            public int Others_Drag_Mark { get; set; }
            public int Others_Light_Leakage { get; set; }
            public int Others_Light_Bubble { get; set; }
            public int Others_White_Dot_in_Material { get; set; }
            public int Others_Other { get; set; }
            public int Others_QA { get; set; }
        }

        public class MouldType
        {
            public int TTS { get; set; }
            public int Vendor { get; set; }
        }

    }
}
