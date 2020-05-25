using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldMaterialTraceability
    {
        public DateTime day { get; set; }
        public string shift { get; set; }
        public string machineID { get; set; }
        public string partNumberALL { get; set; }


        public string clientSubmitInfo { get; set; }
        public string clientUserID { get; set; }
        
        public string dashboardUnloadInfo { get; set; }
        public string unloadUserID { get; set; }
        
               
    }
}