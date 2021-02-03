using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.BLL
{
    public class PQCInventory_BLL
    {
        private readonly Common.Class.DAL.PQCInventory_DAL dal = new Common.Class.DAL.PQCInventory_DAL();


        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exist(string sJobNo, string sProcess)
        {
            DataSet ds = new DataSet();

            ds = dal.GetList(sJobNo, sProcess);
            if (ds == null || ds.Tables.Count == 0)
            {
                return false;
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool Add(Common.Class.Model.PQCInventory_Model model)
        {
            bool result = dal.Add(model);


            return result;
        }

        public bool Delete(string sJobNo)
        {
            int row = dal.Delete(sJobNo);

            if (row > 0)
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
        public bool Update(Common.Class.Model.PQCInventory_Model model)
        {
            int result = dal.Update(model);

            if (result > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }


        public bool UpdateMFGDate(string jobNumber, DateTime mfgDate)
        {
            int result = dal.UpdateMFGDate(jobNumber,mfgDate);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public DataTable GetWIPInventoryReport(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sModel, string sJobNo)
        {
            DataTable dt = dal.GetWIPInventoryReport(dDateFrom, dDateTo, sPartNo, sModel, sJobNo);

            return dt;
        }



        public DataTable GetInventoryForAllSectionReport(DateTime dStartTime)
        {
            return dal.GetInventoryForAllSectionReport(dStartTime);
        }

        #endregion


    }
}
