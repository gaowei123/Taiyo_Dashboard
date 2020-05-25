using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class LaserSummaryReport_ViewModel
    {

        public string machineID { get; set; }
        public double laserBtn { get; set; }
        public double printBtn { get; set; }
        public double lens784 { get; set; }
        public double lens824 { get; set; }
        public double lens833 { get; set; }
        public double bezel257 { get; set; }
        public double bezel830 { get; set; }
        public double bezel831 { get; set; }
        public double panel452 { get; set; }
        public double panel656 { get; set; }
        public double tks869 { get; set; }
        public double ok { get; set; }
        public double ng { get; set; }

        public double output { get; set; }
        public string rejRate { get; set; }

    }
}