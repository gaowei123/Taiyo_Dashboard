using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Drawing;


namespace StaticRes
{
    public static class LaserStautsColor
    {
        public static System.Drawing.Color Run = System.Drawing.Color.LawnGreen;
        public static System.Drawing.Color NoSchedule = System.Drawing.Color.Orange;
        public static System.Drawing.Color Breakdown = System.Drawing.Color.Red;
        public static System.Drawing.Color Shutdown = System.Drawing.Color.Gray;
    }


    public static class LaserStatusDetailColor
    {
        //run
        public static System.Drawing.Color Operating = System.Drawing.Color.LawnGreen;
        public static System.Drawing.Color Setup = System.Drawing.Color.Orange;
        public static System.Drawing.Color Adjustment = System.Drawing.Color.Orange; //adjustment后期改名成setup了, 其实是同一种状态
        public static System.Drawing.Color Buyoff = System.Drawing.Color.MediumPurple;
        public static System.Drawing.Color Testing = System.Drawing.Color.RoyalBlue;


        //idle
        public static System.Drawing.Color NoSchedule = System.Drawing.Color.Yellow;


        //breakdown
        public static System.Drawing.Color Maintainence = System.Drawing.Color.Brown;
        public static System.Drawing.Color BreakDown = System.Drawing.Color.Red;


        //shutdown
        public static System.Drawing.Color ShutDown = System.Drawing.Color.Gray;
    }

    public static class PQCStautsColor
    {
        public static System.Drawing.Color Run = System.Drawing.Color.LawnGreen;
        public static System.Drawing.Color NoSchedule = System.Drawing.Color.Orange;
        public static System.Drawing.Color Shutdown = System.Drawing.Color.Gray;
    }






    public static class McStatusColor
    {
        public static System.Drawing.Color Operating    = System.Drawing.Color.LawnGreen;
        public static System.Drawing.Color Setup        = System.Drawing.Color.Orange;
        public static System.Drawing.Color Maintainence = System.Drawing.Color.Brown;
        public static System.Drawing.Color Testing      = System.Drawing.Color.RoyalBlue;
        public static System.Drawing.Color NoWIP        = System.Drawing.Color.Yellow;
        public static System.Drawing.Color Buyoff       = System.Drawing.Color.MediumPurple;
        public static System.Drawing.Color BreakDown    = System.Drawing.Color.Red;
        public static System.Drawing.Color ShutDown     = System.Drawing.Color.Gray;

        public static System.Drawing.Color Adjustment = System.Drawing.Color.Orange;
        
    }

    public static class InspectionResColor
    {
        public static System.Drawing.Color OK = System.Drawing.Color.Chartreuse;
        public static System.Drawing.Color NG = System.Drawing.Color.Red;

        public static System.Drawing.Color Empty = System.Drawing.Color.White;
    }

    public static class MouldingStatusColor
    {
        public static System.Drawing.Color Running              = System.Drawing.Color.LawnGreen;
        public static System.Drawing.Color Adjustment           = System.Drawing.Color.Orange;
        public static System.Drawing.Color Mould_Testing        = System.Drawing.Color.Blue;
        public static System.Drawing.Color Material_Testing     = System.Drawing.Color.BlueViolet;
        public static System.Drawing.Color Change_Model         = System.Drawing.Color.Brown;
        public static System.Drawing.Color No_Schedule          = System.Drawing.Color.Yellow;
        public static System.Drawing.Color No_Operator          = System.Drawing.Color.DeepPink;
        public static System.Drawing.Color No_Material          = System.Drawing.Color.Fuchsia;
        public static System.Drawing.Color Break_Time           = System.Drawing.Color.BurlyWood;
        public static System.Drawing.Color ShutDown             = System.Drawing.Color.Gray;
        public static System.Drawing.Color Login_Out            = System.Drawing.Color.Chocolate;
        public static System.Drawing.Color MachineBreak         = System.Drawing.Color.Red;
        public static System.Drawing.Color DamageMould          = System.Drawing.Color.Red;

        
        public static System.Drawing.Color Running_Dark         = System.Drawing.Color.DarkGreen;
        public static System.Drawing.Color Adjustment_Dark      = System.Drawing.Color.DarkOrange;
        public static System.Drawing.Color Mould_Testing_Dark   = System.Drawing.Color.DarkBlue;
        public static System.Drawing.Color Material_Testing_Dark= System.Drawing.Color.Indigo;
        public static System.Drawing.Color Change_Model_Dark    = System.Drawing.Color.Maroon;
        public static System.Drawing.Color No_Schedule_Dark     = System.Drawing.Color.YellowGreen;
        public static System.Drawing.Color No_Operator_Dark     = System.Drawing.Color.MediumVioletRed;
        public static System.Drawing.Color No_Material_Dark     = System.Drawing.Color.DarkMagenta;
        public static System.Drawing.Color Break_Time_Dark      = System.Drawing.Color.Tan;
        public static System.Drawing.Color ShutDown_Dark        = System.Drawing.Color.DimGray;
        public static System.Drawing.Color Login_Out_Dark       = System.Drawing.Color.SaddleBrown;
        public static System.Drawing.Color MachineBreak_Dark    = System.Drawing.Color.DarkRed;
        public static System.Drawing.Color DamageMould_Dark     = System.Drawing.Color.DarkRed;
    }

}
