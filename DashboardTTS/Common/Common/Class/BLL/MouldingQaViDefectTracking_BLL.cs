using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Class.BLL
{
  public  class MouldingQaViDefectTracking_BLL
    {
        public MouldingQaViDefectTracking_BLL()
        {

        }

        Common.Class.DAL.MouldingQaViDefectTracking_DAL dal = new DAL.MouldingQaViDefectTracking_DAL();

        public DataTable Getlist(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNumber, string sRejType, string sModel)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID, sPartNumber, sRejType, sModel);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            int Total_Rej = 0;
            decimal Total_RejCost = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    Total_Rej += int.Parse(dr["rejectQty"].ToString());
                    Total_RejCost += decimal.Parse(dr["rejectCost"].ToString());
                }
                catch { }
            }

            DataRow dr_total = dt.NewRow();
            dr_total["Day"] = "Total :";
            dr_total["rejectQty"] = Total_Rej;
            dr_total["rejectCost"] = Total_RejCost;
            dt.Rows.Add(dr_total);


            return dt;
        }

    }
}
