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

        [Description("Prod Office")]
        Office = 8,

        [Description("HR/Finance")]
        HR_Finance = 9,

        [Description("Planning/Purchasing")]
        Planning_Purchasing = 10,

    
        [Description("Sales/Project")]
        Sales_Project = 11,

        [Description("QA/QC/FA")]
        QA_QC_FA = 12,

        Store = 13,

        TSS = 14
    }
}
