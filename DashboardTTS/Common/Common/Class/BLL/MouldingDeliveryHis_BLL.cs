using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class MouldingDeliveryHis_BLL
    {
        private readonly Common.Class.DAL.MouldingDeliveryHis_DAL dal = new Common.Class.DAL.MouldingDeliveryHis_DAL();



        #region  Method

        public SqlCommand AddCMD(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
            return dal.AddCMD(model);
        }


        public bool Add(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
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

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
            int result = dal.Update(model);

            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            return dal.Delete();
        }

        #endregion

        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPartNo, string sSendingTo, string sLotno)
        {
            DataSet ds = new DataSet();

            ds = dal.GetList(dDateFrom, dDateTo, sJobNo, sPartNo, sSendingTo, sLotno);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            #region dr total

            double TotalInSET = 0;
            double TotalInPCS = 0;
            foreach (DataRow dr in dt.Rows)
            {
                TotalInSET += double.Parse(dr["inQtySET"].ToString());
                TotalInPCS += double.Parse(dr["inQtyPCS"].ToString());
            }

            DataRow drTotal = dt.NewRow();
            drTotal["partNumber"] = "Total :";
            drTotal["inQuantity"] = TotalInSET + "(" + TotalInPCS + ")";

            dt.Rows.Add(drTotal);


            #endregion

            return dt;

        }

    }
}
