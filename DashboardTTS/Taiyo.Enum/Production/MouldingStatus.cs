using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Taiyo.Enum.Production
{
    public  enum  MouldingStatus
    {
        Running = 0,

        [Description("Material Testing")]
        MaterialTesting= 1,

        [Description("Mould Testing")]
        MouldTesting= 2,

        Adjustment = 3,

        [Description("Change Model")]
        ChangeModel = 4,

        [Description("No Operator")]
        NoOperator= 5,

        [Description("Login Out")]
        LoginOut = 6,

        [Description("No Material")]
        NoMaterial = 7,

        [Description("Login Late")]
        LoginLate = 8,

        [Description("No Schedule")]
        NoSchedule = 9,

        [Description("Break Time")]
        BreakTime = 10,

        [Description("Machine Break")]
        MachineBreak =  11,

        [Description("Damage Mould")]
        DamageMould = 12,

        Shutdown = 13
    }
}
