using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.BLL
{
    public class LMMSInventoty_BLL
    {
        private readonly Common.Class.DAL.LMMSInventory_DAL dal = new DAL.LMMSInventory_DAL();
        public LMMSInventoty_BLL()
        {

        }
        

        public bool Add(Common.Class.Model.LMMSInventory_Model model)
        {
            int result = dal.Add(model);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public bool UpdateMFGDate(string jobNumber, DateTime MFGDate)
        {
            int result = dal.UpdateMFGDate(jobNumber, MFGDate);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool Exist(string jobnumber)
        {
            DataSet ds = dal.Exsit(jobnumber);

            DataTable dt = new DataTable();
            if (ds==null || ds.Tables.Count == 0)
            {
                return false;
            }else
            {
                dt = ds.Tables[0];
            }

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteJob(string jobnumber)
        {
            bool result = false;
            
            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(dal.deleteCommand(jobnumber));


            Common.DAL.LMMSWatchLog_DAL watchLogDAL = new Common.DAL.LMMSWatchLog_DAL();
            cmdList.Add(watchLogDAL.DeleteJob(jobnumber));


            Common.DAL.LMMSWatchDog_His_DAL watchdogShiftDAL = new Common.DAL.LMMSWatchDog_His_DAL();
            cmdList.Add(watchdogShiftDAL.DeleteJob(jobnumber));


            Common.Class.DAL.PaintingDeliveryHis_DAL paintingDAL = new DAL.PaintingDeliveryHis_DAL();
            cmdList.Add(paintingDAL.CancelJob(jobnumber));




            result = DBHelp.SqlDB.SetData_Rollback(cmdList);



            return result;
        }

       

        public DataTable DetailReport(string jobNo, string partNo, DateTime dateFrom, DateTime dateTo, string status)
        {
            DataTable dt = dal.SelectInventoryDetailReport(jobNo, partNo, dateFrom, dateTo, status);

            if (dt.Rows.Count == 0)
            {
                return null;
            }


            #region  add a dr total
            dt.Columns.Add("ID");
            int id = 1;

            double MRPSet_Count = 0;
            double MRPPCS_Count = 0;
           
            double BeforeLaser_SetCount = 0;
            double BeforeLaser_PCSCount = 0;

            double AfterLaser_SetCount = 0;
            double AfterLaser_PCSCount = 0;

            double EstTimeCount = 0;
            double PQCQuantity = 0;
            double BuyoffQty = 0;
            double SetupQty = 0;

            foreach (DataRow row in dt.Rows)
            {
                row["ID"] = id;
                id++;

                string MRPSet_PCS = row["MRPSet_PCS"].ToString();
                try
                {
                    MRPSet_Count += double.Parse(MRPSet_PCS.Trim(')').Split('(')[0].ToString());
                }
                catch   {}

                try
                {
                    MRPPCS_Count += double.Parse(MRPSet_PCS.Trim(')').Split('(')[1].ToString());
                }
                catch{}
                

                string BeforeLaser = row["BeforeLaser"].ToString();
                string AfterLaser = row["AfterLaser"].ToString();

                try
                {
                    BeforeLaser_SetCount += double.Parse(BeforeLaser.Trim(')').Split('(')[0].ToString());
                    BeforeLaser_PCSCount += double.Parse(BeforeLaser.Trim(')').Split('(')[1].ToString());
                }
                catch{}

                try
                {
                    AfterLaser_SetCount += double.Parse(AfterLaser.Trim(')').Split('(')[0].ToString());
                    AfterLaser_PCSCount += double.Parse(AfterLaser.Trim(')').Split('(')[1].ToString());
                }
                catch { }

                try
                {
                    EstTimeCount += double.Parse(row["estProcessTime"].ToString().Trim('H'));
                }
                catch { }

          

                PQCQuantity += double.Parse(row["pqcQuantity"].ToString());
                BuyoffQty += double.Parse(row["buyOffQty"].ToString());
                SetupQty += double.Parse(row["setUpQty"].ToString());

            }

            DataRow dr = dt.NewRow();
            dr["jobNumber"] = "Total :";
            dr["MRPSet_PCS"] = MRPSet_Count + "(" + MRPPCS_Count + ")";
            dr["BeforeLaser"] = BeforeLaser_SetCount + "(" + BeforeLaser_PCSCount + ")";
            dr["AfterLaser"] = AfterLaser_SetCount + "(" + AfterLaser_PCSCount + ")";

            dr["pqcQuantity"] = PQCQuantity;
            dr["buyOffQty"] = BuyoffQty;
            dr["setUpQty"] = SetupQty;
            dr["estProcessTime"] = Math.Round(EstTimeCount < 0 ? 0 : EstTimeCount, 2).ToString() + 'H';

            dt.Rows.Add(dr);
            #endregion

            return dt;
        }




        public DataTable GetListForPQCReport(DateTime dDateFrom, DateTime dDateTo)
        {
            DataSet ds = dal.GetListForPQCReport(dDateFrom, dDateTo);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetList(string sJobNo)
        {
            DataTable dt = dal.GetList(sJobNo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
     

        public DataTable GetInventoryInfoForAllInventoryReport(DateTime dStartTime)
        {
            return dal.GetInventoryInfoForAllInventoryReport(dStartTime);
        }
        
        public Common.Class.Model.LMMSInventory_Model GetModel(string sJobNo)
        {
            if (string.IsNullOrEmpty(sJobNo))
                return null;


            DataTable dt = dal.GetListForModel(sJobNo);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow drModel = dt.Rows[0];
            Model.LMMSInventory_Model model = new Model.LMMSInventory_Model();
            model.JobNumber = drModel["jobNumber"].ToString();
            model.partNumber = drModel["partNumber"].ToString();
            model.description = drModel["description"].ToString();
            model.quantity = double.Parse(drModel["quantity"].ToString());
            model.PQCQuantity = int.Parse(drModel["pqcQuantity"].ToString());
            model.startOnTime = DateTime.Parse(drModel["startOnTime"].ToString());
            model.dateTime = DateTime.Parse(drModel["dateTime"].ToString());
            model.day = drModel["day"].ToString();
            model.month = drModel["month"].ToString();
            model.year = drModel["year"].ToString();


            model.SetUp = int.Parse(drModel["setUpQTY"].ToString());
            model.Buyoff = int.Parse(drModel["buyOffQty"].ToString());
            model.Lotno = drModel["lotNo"].ToString();


            return model;
        }
        
    }
}
