using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Taiyo.Enum.Attendance
{
    public enum LeaveReason
    {
        [Description("Annual Leave")]
        AnnualLeave,

        [Description("MC")]
        MedicalCare,

        [Description("UPL/UPMC")]
        Unpaid,

        Maternity,//产假

        Paternity,//陪产假

        Marriage,//婚假

        WFH,

        Hospitalization,

        Compassionate,//私假

        [Description("Child Care Leave")]
        ChildCareLeave,

        Absent,//矿工

        [Description("Business Trip")]
        BusinessTrip,

        Reservist,

        Pending
    }
}
