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

        public bool AddRollback(List<Common.Class.Model.MouldingDeliveryHis_Model> ModelList)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();

            foreach (Common.Class.Model.MouldingDeliveryHis_Model Model in ModelList)
            {
                if (Model != null)
                {
                    list_cmd.Add(dal.AddCMD(Model));
                }
            }


            return DBHelp.SqlDB.SetData_Rollback(list_cmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }





        //public bool ScanJobProcess(List<Common.Class.Model.LMMSInventory_Model> Laser_Model_List, List<Common.Class.Model.PaintingDeliveryHis_Model> Painting_DeliHisModel_List, List<Common.Class.Model.PQCInventory_Model> PQC_Model_List)
        //{
        //    bool Result_Laser = false;
        //    bool Result_Painting = false;
        //    bool Result_PQC = true;

        //    Common.Class.BLL.LMMSInventoty_BLL Laser_Inventory_BLL = new LMMSInventoty_BLL();
        //    Result_Laser = Laser_Inventory_BLL.AddInventoryRollback(Laser_Model_List);

        //    Result_Painting = AddRollback(Painting_DeliHisModel_List);

        //    Common.Class.BLL.PQCInventory_BLL PQC_Inventory_BLL = new PQCInventory_BLL();
        //    Result_PQC = PQC_Inventory_BLL.AddInventoryRollback(PQC_Model_List);


        //    if (Result_Laser && Result_Painting && Result_PQC)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}





        public bool ExistLotno(string sLotno)
        {
            bool Result = false;
            DataSet ds = dal.CheckLotno(sLotno);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Result = false;
            }
            else
            {
                Result = true;
            }


            return Result;
        }
        public bool ExistJobno(string sJobno)
        {
            bool Result = false;
            DataSet ds = dal.CheckJobno(sJobno);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Result = false;
            }
            else
            {
                Result = true;
            }


            return Result;
        }


    }
}
