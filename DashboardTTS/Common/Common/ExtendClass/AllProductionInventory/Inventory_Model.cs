using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.AllProductionInventory
{
    public class Inventory_Model
    {

        public class PQCBomInfo
        {
            public string Model { get; set; }
            public string PartNo { get; set; }
            public string MaterialName { get; set; }
            public string MaterialNo { get; set; }

            public string Processes { get; set; }
            public string ShipTo { get; set; }
        }





        public class Inventory
        {
            public string Model { get; set; }
            public string PartNo { get; set; }
            public string MaterialNo { get; set; }
            public string MaterialName { get; set; }

            
            public double AssemblyQty { get; set; }
            public double FGQty { get; set; }

            public PackingInventory PackingInventory { get; set; }
            public WIPInventory WIPInventory { get; set; }
            public LaserInventory LaserInventory { get; set; }

            public PaintingQty PaintingQty { get; set; }

            public double PaintRawPartQty { get; set; }
        }
        


        public class LaserInventory
        {
            public string MaterialNo { get; set; }
            public string MaterialName { get; set; }

            public double BeforeQty { get; set; }
            public double AfterQty { get; set; }
        }
        
        public class WIPInventory
        {
            public string MaterialNo { get; set; }
            public string MaterialName { get; set; }

            public double BeforeQty { get; set; }
            public double AfterQty { get; set; }
        }
        
        public class PackingInventory
        {
            public string MaterialNo { get; set; }
            public string MaterialName { get; set; }

            public double BeforeQty { get; set; }
            public double AfterQty { get; set; }
        }
        
        public class PaintingQty
        {
            public string MaterialNo { get; set; }
            public string MaterialName { get; set; }

            public double TCQty { get; set; }
            public double MCQty { get; set; }
            public double UCQty { get; set; }
            public double PrintQty { get; set; }
        }





    }
}
