using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Class.BLL
{
    public class MouldingMachineInformation_BLL
    {

        Common.Class.DAL.MouldingMachineInformation_DAL.Machine_DaL MachineDal = new DAL.MouldingMachineInformation_DAL.Machine_DaL();

        public MouldingMachineInformation_BLL()
        {

        }

        public DataTable SelectList()
        {
            DataSet ds = MachineDal.SelectAll();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }


        public bool Update(Common.Class.Model.MouldingMachineInformation_Model model)
        {

            int Result = MachineDal.Update(model);

            if (Result > 0)
            {
                return true;
            }
            else {
                return false;
            }
            
        }

    }
}
