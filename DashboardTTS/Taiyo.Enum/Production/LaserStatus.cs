using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Taiyo.Enum.Production
{
    public enum LaserStatus
    {
        Running = 0,

        Buyoff = 1,

        Setup = 2, 

        Testing= 3, 

        [Description("No Schedule")]
        NoSchedule = 4,

        Maintenance = 5, 

        Breakdown = 6, 

        Shutdown = 7
    }
}
