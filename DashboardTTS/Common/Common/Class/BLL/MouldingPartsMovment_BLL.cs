using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common.Class.BLL
{
    public class MouldingPartsMovment_BLL
    {
        private Common.Class.DAL.MouldingPartsMovment_DAL dal = new DAL.MouldingPartsMovment_DAL();
        public bool Add(Common.Class.Model.MouldingTransfer model)
        {
            if (model == null)
            {
                return false;
            }

            int result = dal.Add(model);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetList(string sMaterial_Part, string sTransfer_To,DateTime dTransfer_Date_from, DateTime dTransfer_Date_To,DateTime dProduction_Date)
        {
            DataSet ds = dal.GetModel(sMaterial_Part, sTransfer_To,dTransfer_Date_from,dTransfer_Date_To,dProduction_Date);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }

        public bool UpDdate(Common.Class.Model.MouldingTransfer model)
        {
            if (model == null)
            {
                return false;
            }

            int result = dal.Update(model);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
