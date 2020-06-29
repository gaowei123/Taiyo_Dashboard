using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class LaserMaintenance_ViewModel
    {

        public DateTime day { get; set; }
        public string shift { get; set; }
        public string machineID { get; set; }
        public string jobID { get; set; }

        public double setup { get; set; }
        public double buyoff { get; set; }
        public double shortage { get; set; }


        public List<Material> materialList { get; set; }

       
        public class Material
        {
            public int sn { get; set; }
            public string materialNo { get; set; }
            public double ng { get; set; }
        }




    }
}