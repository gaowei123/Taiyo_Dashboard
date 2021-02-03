using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.BLL
{
    public class PaintingInventory_BLL
    {
        private readonly Common.Class.DAL.PaintingInventory_DAL dal = new DAL.PaintingInventory_DAL();
        public PaintingInventory_BLL()
        {
        }
        public List<SqlCommand> AddInventoryCMDList(List<Common.Class.Model.PaintingInventory_Model> list_Inventory)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();
            foreach (Common.Class.Model.PaintingInventory_Model item in list_Inventory)
            {
                if (item != null)
                {
                    list_cmd.Add(dal.AddCommand(item));
                }
            }
            return list_cmd;
        }

        public bool Add(Common.Class.Model.PaintingInventory_Model model)
        {
            int result = dal.Add(model);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable Report(string partNo, string customer)
        {
            DataTable dt = dal.SelectInventoryDailyReport(partNo, customer);

            if (dt.Rows.Count == 0)
                return null;
            else
                return dt;
        }
    }
}
