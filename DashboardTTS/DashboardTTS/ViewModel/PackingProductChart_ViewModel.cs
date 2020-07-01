using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class PackingProductChart_ViewModel
    {
        public int iYear { get; set; }
        public int iMonth { get; set; }
        public int iDay { get; set; }
        public DateTime dDay { get; set; }
        public double output { get; set; }
        public string op { get; set; }
        
    }
}