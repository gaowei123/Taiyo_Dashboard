using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class Bom_Model
    {
        public Bom_Model()
        {
            MaterialPartList = new List<MaterialPart>();
        }

        public string PartNo { get; set; }
        public string Customer { get; set; }
        public string Model { get; set; }
        public string JigNo { get; set; }
        public string Color { get; set; }
        public string Processes { get; set; }
        public bool withLaser { get; set; }
        public string LastCheckProcess { get; set; }
        public string Supplier { get; set; }
        public string ShipTo { get; set; }
        public string Coating { get; set; }
        public string Description { get; set; }
        public string Num { get; set; }
        public decimal UnitCost { get; set; }
        public List<MaterialPart> MaterialPartList { get; set; }


        public class MaterialPart
        {
            public string MaterialName { get; set; }
            public string MaterialPartNo { get; set; }
            public int PartCount { get; set; }
            public int OuterBoxQty { get; set; }
            public string PackingTrays { get; set; }
            public string Module { get; set; }
        }

    }
}
