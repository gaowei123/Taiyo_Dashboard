using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PackingInventory_ViewModel
    {

        public string customer { get; set; }
        public string model { get; set; }
        public string partNo { get; set; }
        public string jobNo { get; set; }

        public double mrpQtyPCS { get; set; }
        public double mrpQtySET { get; set; }
        public string sMrpQty { get; set; }


        public double beforeQtyPCS { get; set; }
        public double beforeQtySET { get; set; }
        public string sBeforeQty { get; set; }


        public double afterQtyPCS { get; set; }
        public double afterQtySET { get; set; }
        public string sAfterQty { get; set; }


        public double jobCount { get; set; }

        public DateTime? mfgDate { get; set; }


        public string bomFlag { get; set; }

        public string jobStatus { get; set; }

        public string type { get; set; }


    }
}