using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Taiyo.Enum.Organization
{
    public  enum UserGroup
    {
        Admin,
        Manager,
        Supervisor,
        Engineer,
        Leader,

        MH,
        [Description("Temporary Staff")]
        TemporaryStaff,
        IPQC,
        Technician,


        Checker,
        Operator,
        Indirect
    }
}
