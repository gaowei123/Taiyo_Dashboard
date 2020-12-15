using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Taiyo.Enum.Organization
{
    public enum Department
    {
        Moulding = 0,

        Painting = 1,

        Laser = 2, 

        PQC = 3,
        Online = 4, 
        WIP = 5, 
        Packing = 6,

        Assembly = 7,

        [Description("Pord Office")]
        Office = 8,

        TSS = 9,

        [Description("HR/Finance")]
        HR_Finance = 10,

        [Description("Planning/Purchasing")]
        Planning_Purchasing = 11,

    
        [Description("Sales/Project")]
        Sales_Project = 12,

        [Description("QA/QC/FA")]
        QA_QC_FA = 13,

        Store = 14

    }
}
