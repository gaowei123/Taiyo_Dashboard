using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Class.BLL
{
    public class MouldingMaintainenceInspection_BLL
    {
        Common.Class.DAL.MouldingMaintainenceInspection_DAL dal = new DAL.MouldingMaintainenceInspection_DAL();
        public MouldingMaintainenceInspection_BLL()
        {

        }


        public DataTable GetList(string sCheckPeriod,string sCheckItem)
        {
            DataSet ds = dal.SelectList(sCheckPeriod,sCheckItem);

            if (ds == null || ds.Tables.Count ==0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }

        public bool Add(Common.Class.Model.MouldingMaintenanceInspection_Model model)
        {
            int row = dal.Add(model);

            if (row > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool Update(Common.Class.Model.MouldingMaintenanceInspection_Model model)
        {
            int row = dal.Update(model);

            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exist(string sCheckItem, string sCheckPeriod)
        {
            bool result = false;
            DataSet ds = dal.SelectList(sCheckPeriod, sCheckItem);

            if (ds == null || ds.Tables.Count == 0)
            {
                result = false;
            }

            DataTable dt = ds.Tables[0];

            if (dt == null || dt.Rows.Count == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

    }
}
