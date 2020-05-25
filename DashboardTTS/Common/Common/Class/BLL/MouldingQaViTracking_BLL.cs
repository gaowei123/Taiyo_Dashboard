using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public   class MouldingQaViTracking_BLL
    {
        Common.Class.DAL.MouldingQaViTracking_DAL dal = new DAL.MouldingQaViTracking_DAL();
        public MouldingQaViTracking_BLL()  {   }

        public DataTable SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID, sPartNo, sShift, sModule);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            
            


            return dt;
        }

    }
}
