using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewModel
{
    public class MouldMonthlyHighestReject
    {


        public List<Parts> partsList;
        public List<Defect> defectList;


        public class Parts
        {
            public string partNo { get; set; }
            public double output { get; set; }
            public double rejQty { get; set; } 
            public string rejRate { get; set; }
            public string highestDefect_1st { get; set; }
            public string highestDefect_2nd { get; set; }
            public string highestDefect_3rd { get; set; }

        }


        public class Defect
        {
            public string defectCode { get; set; }
            public double rejQty { get; set; }
            public string rejRate { get; set; }
            public string affectedPart_1st { get; set; }
            public string affectedPart_2nd { get; set; }
            public string affectedPart_3rd { get; set; }
            
        }


    }
}