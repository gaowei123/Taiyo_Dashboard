using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.Enum;
using Taiyo.Enum.Organization;

namespace Common.ExtendClass.Attendance
{
    public class Base_Model
    {
        public DateTime? Day { get; set; }
        public CommonEnum.Shift Shift { get; set; }
        public Department Department { get; set; }
        public string EmployeeID { get; set; }
        public string Username { get; set; }
        public string Attendance { get; set; }
        public string OnLeave { get; set; } //Day,AM,PM, NA
    }



    public class Attendance_Model
    {
        public Department Department { get; set; }
        public DateTime? Day { get; set; }
        public decimal DayShift { get; set; }
        public decimal NightShift { get; set; }
        public decimal TotalPresent { get; set; }
        public decimal AnnualLeavel { get; set; }
        public decimal MC { get; set; }
        public decimal UPL_UPMC { get; set; }
        public decimal Maternity { get; set; }
        public decimal Paternity { get; set; }
        public decimal Marriage { get; set; }
        public decimal WFH { get; set; }
        public decimal Hospitalization { get; set; }
        public decimal Compassionate { get; set; }
        public decimal ChildCareLeave { get; set; }
        public decimal Absent { get; set; }
        public decimal BusinessTrip { get; set; }
        public decimal Reservist { get; set; }
        public decimal Pending { get; set; }

        public string LeaveReason { get; set; }      

    }
}
