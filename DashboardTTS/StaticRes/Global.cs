using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Net;
using System.Configuration;

namespace StaticRes
{
    public class Global
    {

        
        public static class LaserStatus
        {
            public const string Run = "RUN";
            public const string Buyoff = "BUYOFF";
            public const string Setup = "SETUP";
            public const string Testing = "TESTING";
            public const string NoSchedule = "NO SCHEDULE";
            public const string Maintenance = "MAINTAINENCE";
            public const string Breakdown = "BREAKDOWN";
            public const string Shutdown = "SHUTDOWN";
        }


        public static class MouldingStatus
        {
            public const string Run = "Running";
            public const string Material_Testing = "Material Testing";
            public const string Mould_Testing = "Mould Testing";
            public const string Adjustment = "Adjustment";
            public const string Change_Model = "Change Model";

            public const string No_Operator = "No Operator";
            public const string Login_Out = "Login Out";
            public const string No_Material = "No Material";
            public const string Login_Late = "Login Late";
            public const string No_Schedule = "No_Schedule";
            public const string Break_Time = "Break Time";
            
            public const string MachineBreak = "MachineBreak";
            public const string DamageMould = "DamageMould";

            public const string ShutDown = "ShutDown";
        }


        public static class PQCStatus
        {
            public const string Checking = "CHECKING";
            public const string Packing = "PACKING";
            public const string NoSchedule = "NO SCHEDULE";
            public const string Shutdown = "SHUTDOWN";
        }






        public static class ErrorLevel
        {
            public const string Warning = "WARNING";
            public const string Error = "ERROR";
            public const string Exception = "EXCEPTION";
        }


        public static class Department
        {
            public const string Moulding = "Moulding";
            public const string Painting = "Painting";
            public const string Laser = "Laser";
            public const string PQC = "PQC";
            public const string Assembly = "Assembly";
            public const string Packing = "Packing";
            public const string Office = "Office";
        }
        
    

        
       
        //OEE 
        public enum StatusType
        {
            PD,
            Idle,
            NoWIP,
            Adjustment,
            BreakDown,
            ShutDown,
            Testing,
            Maintance,
            Setup,
            Buyoff,


            
            Running,
            No_Schedule,
            Mould_Testing,
            Material_Testing,
            Change_Model,
            No_Operator,
            No_Material,
            Break_Time,
            MachineBreak,
            DamageMould,
            Login_Out
        }

        
        public static class Shift
        {
            public const string ALL = "ALL";
            public const string Day = "Day";
            public const string Night = "Night";
        }
     
     
       
    
        public static class UserGroup
        {
            public const string ADMIN = "Admin";
            public const string SUPERVISOR = "Supervisor";
            public const string ENGINEER = "Engineer";
            public const string LEADER = "Leader";
            public const string OPERATOR = "Operator";
            public const string MH = "MH";
         
            public const string IPQC = "IPQC";
            public const string TECHNICIAN = "Technician";
            public const string CHECKER = "Checker";
            public const string TEMPORARYSTAFF = "TemporaryStaff";
        }
        
     

        public static class clsConstValue
        {
            public static class ConstCategory
            {
                public static string Sysem = "SYSTEM_OEE";
                public static string Technician = "TECHNICIAN_OEE";
                public static string StandBy = "STANDBY";
            }

            public static class ConstStatus
            {
                public static string PowerOn = "POWER ON";
                public static string PowerOff = "POWER OFF";
                public static string Adjustment = "ADJUSTMENT";
                public static string Maintainence = "MAINTAINENCE";
                public static string Teaching = "TEACHING";
                public static string Others = "OTHERS";
                public static string UnderPm = "UNDER PM";
                public static string BreakDown = "BREAKDOWN";
                public static string NoSchdule = "NO SCHEDULE";
                public static string UnderSetup = "UNDER SETUP";
                public static string Testing = "TESTING";
                public static string Setup = "SETUP";
                public static string Buyoff = "BUYOFF";
            }


            //base on the type recorded from Client
            public static class ConstMouldingStatus  
            {
                public const string No_Operator = "No Operator";
                public const string Material_Testing = "Material Testing";
                public const string MachineBreak = "MachineBreak";
                public const string Login_Out = "Login Out";
                public const string No_Material = "No Material";
                public const string Mould_Testing = "Mould Testing";
                public const string Login_Late = "Login Late";
                public const string Adjustment = "Adjustment";
                public const string No_Schedule = "No_Schedule";
                public const string Break_Time = "Break Time";
                public const string Running = "Running";
                public const string Change_Model = "Change Model";
                public const string DamageMould = "DamageMould";
                public const string ShutDown = "ShutDown";
            }



            public static class JobStatus
            {
                public static string pending = "Pending";
                public static string inprocess = "Inprocess";
                public static string complete = "Complete";
                public static string noComplete = "NoComplete";
               // public static string remove = "Remove";
            }
            
        }

        public static class ProductType
        {
            public const string BUTTON = "Button";
            public const string PRINT = "Print";
            public const string PANEL = "Panel";
            public const string LENS = "Lens";
            public const string BZ_257B = "Bezel-257B";
            public const string BZ_320B = "Bezel-320B";
            public const string Project_Testing = "Project Testing";

            public const string Mould_Test = "Mould Test";
            public const string LEN = "Len";
            public const string Knob = "Knob";


            
            //pqc
            public const string Laser_Btn = "Laser Btn";
            public const string wo_Laser_Btn = "w/o Laser Btn";
            public const string SBW_TKS784 = "SBW TKS784";
            public const string TMS_TKS824 = "TMS TKS824";
            public const string TAC_TKS833 = "TAC TKS833";
            public const string TRMI_452 = "TRMI 452";
            public const string TRMI_595_656 = "TRMI 595,656";
            public const string TKS830_320B = "320B TKS830";
            public const string TKS831_320B = "320B TKS831";
            public const string Packers = "Packers";


     

        }

        //For Machine info.
        public static class MouldingModelType      
        {
            public static string Temperature = "Temperature";
            public static string Dryer = "Dryer";
            public static string RobotArm = "RobotArm";
            public static string Machine = "Machine";
            public static string Main = "Main";
        }



        public static class UserAttendance
        {
            public static class LeaveReason
            {
                public static string annualLeave = "Annual Leave";
                public static string mcLeave = "MC";
                public static string unPaid = "UnPaid";
                public static string maternity = "Maternity";
                public static string marriage = "Marriage";
                public static string compassionate = "Compassionate";
                public static string childCare = "ChildCare";
                public static string absent = "Absent";
                public static string businessTrip = "Business Trip";
                public static string pending = "Pending";
                public static string reserviced = "Reserviced";
            }
        }

        public static class PQC
        {
            public static class Process
            {
                //Mould-Paint#1-Laser-Check#1-Packing-QA-FG
                public const string Mould = "Mould";
                public const string Print = "Print";
                public const string Paint1st = "Paint#1";
                public const string Paint2nd = "Paint#2";
                public const string Laser = "Laser";
                public const string Check1st = "Check#1";
                public const string Check2nd = "Check#2";
                public const string Check3rd = "Check#3";
                public const string Check4th = "Check#4";
                public const string QA = "QA";
                public const string Packing = "Packing";
                public const string Assembly = "Assembly";
                public const string FG = "FG";
            }

        }


        public static class SqlConnection
        {

            public static string SqlconnMoulding =  " ' Data Source="+Moulding.dataSource+";User ID="+Moulding.userID+";Password="+Moulding.password+" ' ";
            public static string SqlconnPainting = " ' Data Source=" + Painting.dataSource + ";User ID=" + Painting.userID + ";Password=" + Painting.password + " ' ";
            public static string SqlconnLaser = " ' Data Source=" + Laser.dataSource + ";User ID=" + Laser.userID + ";Password=" + Laser.password + " ' ";
            public static string SqlconnPQC = " ' Data Source=" + PQC.dataSource + ";User ID=" + PQC.userID + ";Password=" + PQC.password + " ' ";
            public static string SqlconnAssy = " ' Data Source=" + Assy.dataSource + ";User ID=" + Assy.userID + ";Password=" + Assy.password + " ' ";
            public static string SqlconnOffice = " ' Data Source=" + Office.dataSource + ";User ID=" + Office.userID + ";Password=" + Office.password + " ' ";



            public static class Laser
            {
                public static string dataSource = ConfigurationManager.AppSettings["LaserDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["LaserUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["LaserPassword"].ToString();
            }

            public static class Moulding
            {
                public static string dataSource = ConfigurationManager.AppSettings["MouldingDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["MouldingUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["MouldingPassword"].ToString();
            }

            public static class PQC
            {
                public static string dataSource = ConfigurationManager.AppSettings["PQCDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["PQCUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["PQCPassword"].ToString();
            }


            public static class Painting
            {
                public static string dataSource = ConfigurationManager.AppSettings["PaintingDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["PaintingUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["PaintingPassword"].ToString();
            }


            public static class Assy
            {
                public static string dataSource = ConfigurationManager.AppSettings["AssyDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["AssyUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["AssyPassword"].ToString();
            }


            public static class Office
            {
                public static string dataSource = ConfigurationManager.AppSettings["OfficeDataSource"].ToString();
                public static string userID = ConfigurationManager.AppSettings["OfficeUserID"].ToString();
                public static string password = ConfigurationManager.AppSettings["OfficePassword"].ToString();
            }
            
        }



        public static class MouldingMachineStatus
        {
            public const string NoOperator = "No Operator";
            public const string MaterialTesting = "Material Testing";
            public const string MachineBreak = "MachineBreak";
            public const string LoginOut = "Login Out";
            public const string McStop = "No Material";
            public const string MouldTesting = "Mould Testing";
            public const string LoginLate = "Login Late";
            public const string Adjustment = "Adjustment";
            public const string NoSchedule = "No_Schedule";
            public const string Running = "Running";
            public const string ChangeModel = "Change Model";
            public const string DamageMould = "DamageMould";
            public const string ShutDown = "ShutDown";

            public const string Meal = "Break Time";
        }


    }
}
