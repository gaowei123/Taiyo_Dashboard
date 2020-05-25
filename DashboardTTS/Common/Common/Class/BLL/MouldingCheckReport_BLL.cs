using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Common.Class.BLL
{
    public class MouldingCheckReport_BLL
    {
        public MouldingCheckReport_BLL()
        { }

        private Common.Class.DAL.MouldingCheckReport_DAL dal = new DAL.MouldingCheckReport_DAL();

        public bool Add(Common.Class.Model.MouldingCheckReport_Model model)
        {
            return dal.Add(model);
        }


        public DataTable GetList(DateTime dDay, string sShift)
        {
            DataSet ds  = dal.GetList(dDay, sShift);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
             
        }
    }
}
