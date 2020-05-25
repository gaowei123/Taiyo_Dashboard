using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldRejTimeDetail
    {
        public DateTime date { get; set; }
        public string shift { get; set; }
        public string partNo { get; set; }
        public string machineID { get; set; }
        public string defectCode { get; set; }
        public double rejQty { get; set; }
        public string rejTime { get; set; }
        
    }
}