using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserProductChart

{
    public class ProductChart_Model
    {
        public class Base
        {
            public int Year { get; set; }
            public decimal PassQty { get; set; }
            public decimal RejQty { get; set; }
        }


        public class Detail:Base
        {
            public DateTime Date { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }

            public string MachineID { get; set; }
            public string PartNo { get; set; }
            public string JobNo { get; set; }
        }



        public class Daily:Base
        {
            public int Month { get; set; }
            public int Day { get; set; }
        }

        public class Monthly : Base
        {
            public int Month { get; set; }
        }

        public class Yearly: Base
        {
            
        }
       
    }
}
