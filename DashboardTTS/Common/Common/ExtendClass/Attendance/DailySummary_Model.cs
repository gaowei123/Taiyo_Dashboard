﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.Attendance
{
    public class DailySummary_Model
    {
        public string Department { get; set; }
        public decimal TotalUser { get; set; }
        public decimal DayShiftUserCount { get; set; }
        public decimal NightShiftUserCount { get; set; }
        public decimal AnnualLeave { get; set; }
        public decimal OthersLeave { get; set; }
        public decimal UnpaidLeave { get; set; }
        public decimal MC_UPMC { get; set; }
        public decimal Absent { get; set; }
        public decimal BussinessTrip_WFH { get; set; }
        public decimal Pending { get; set; }
        public decimal TotalPresent { get; set; }
        public string ExcludedAL { get; set; }
        public string IncludedAL { get; set; }
        public string Target { get; set; }
        public string Remarks { get; set; }
        /// <summary>
        /// 当天8:15还没提交, 显示黄色.
        /// 当天8:30还没提交, 显示红色
        /// </summary>
        public string  BackgroundColor { get; set; }
    }
}
