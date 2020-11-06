using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Taiyo.Enum.Production
{
    public enum PQCStatus
    {
        Checking = 0,

        Packing= 1, 

        [Description("No Schedule")]
        NoSchedule =2,

        Shutdown = 3
    }
}
