using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class LMMSBomDetail_BLL
    {
        public LMMSBomDetail_BLL()
        {
             
        }

        Common.Class.DAL.LMMSBomDetail_DAL dal = new DAL.LMMSBomDetail_DAL();

        public DataTable GetBomDetailListByPartNumber(string PartNumber)
        {
            DataSet ds = new DataSet();
            ds = dal.GetListByPartNumber(PartNumber);

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
