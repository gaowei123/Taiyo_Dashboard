﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiyo.SearchParam.AttendanceParam
{
    public class MonthlyParams: BaseParam
    {
        public bool isDisplayFullDate { get; set; }
        public bool isExcludedAL { get; set; }
    }
}
