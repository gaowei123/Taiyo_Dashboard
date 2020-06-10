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

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddInventoryRollback( List<Common.Class.Model.PQCInventory_Model> modelList)
        {
            List<SqlCommand> CommandList = new List<SqlCommand>();

            foreach (Common.Class.Model.PQCInventory_Model model in modelList)
            {
                CommandList.Add(dal.AddCMD(model));
            }


            return DBHelp.SqlDB.SetData_Rollback(CommandList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
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


        public List<SqlCommand> AddPQCinventoryCMDList(List<Common.Class.Model.PQCInventory_Model> modelList)
        {
            List<SqlCommand> CommandList = new List<SqlCommand>();

            foreach (Common.Class.Model.PQCInventory_Model model in modelList)
            {
                CommandList.Add(dal.AddCMD(model));
            }

            return CommandList;
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



        //public DataTable GetInventoryReport(DateTime dDateFrom, string sPartNo, string sModel)
        //{
        //    DataSet ds = dal.InventoryReport(dDateFrom, sPartNo, sModel);

        //    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count  == 0)
        //        return null;

        //    DataTable dt = ds.Tables[0];



           
        //    double totalMRPSet = 0;
        //    double totalMRPPcs = 0;
        //    double totalBeforeSet = 0;
        //    double totalBeforePCS = 0;
        //    double totalAfterSet = 0;
        //    double totalAfterPcs = 0;

        //    double totalJobs = 0;


        //    foreach (DataRow dr in dt.Rows )
        //    {

        //        string sMRPTemp = dr["MRPQty"].ToString();
        //        string sBeforeTemp= dr["Before"].ToString();
        //        string sAfterTemp = dr["After"].ToString();
        //        double dJobCount = double.Parse(dr["JobCount"].ToString());


        //        string[] sMRPTempArr = sMRPTemp.Replace(")", "").Split('(');
        //        double MRPSet = double.Parse(sMRPTempArr[0]);
        //        double MRPPcs = double.Parse(sMRPTempArr[1]);

        //        string[] sBeforeTempArr = sBeforeTemp.Replace(")", "").Split('(');
        //        double BeforeSet = double.Parse(sBeforeTempArr[0]);
        //        double BeforePcs = double.Parse(sBeforeTempArr[1]);

        //        string[] sAfterTempArr = sAfterTemp.Replace(")", "").Split('(');
        //        double AfterSet = double.Parse(sAfterTempArr[0]);
        //        double AfterPcs = double.Parse(sAfterTempArr[1]);






        //        totalMRPSet += MRPSet;
        //        totalMRPPcs += MRPPcs;
        //        totalBeforeSet += BeforeSet;
        //        totalBeforePCS += BeforePcs;
        //        totalAfterSet += AfterSet;
        //        totalAfterPcs += AfterPcs;

        //        totalJobs += dJobCount;

        //    }



        //    DataRow drNew = dt.NewRow();
        //    drNew[0] = "Total";

        //    drNew["MRPQty"] = totalMRPSet.ToString() + "(" + totalMRPPcs.ToString() + ")";
        //    drNew["Before"] = totalBeforeSet.ToString() + "(" + totalBeforePCS.ToString() + ")";
        //    drNew["After"] = totalAfterSet.ToString() + "(" + totalAfterPcs.ToString() + ")";
        //    drNew["JobCount"] = totalJobs;

        //    dt.Rows.Add(drNew);


        //    return dt;
        //}
        
        //public DataTable GetInventoryDetailReport(DateTime dDateFrom, DateTime dDateTo, string sJobLot, string sPartNo, string sStatus)
        //{
        //    DataSet ds = new DataSet();
        //    ds = dal.InventoryDetailReport(dDateFrom, dDateTo, sJobLot, sPartNo, sStatus);

        //    if (ds == null || ds.Tables.Count == 0)
        //    {
        //        return null;
        //    }

        //    DataTable dt = ds.Tables[0];



        //    double totalMRPSet = 0;
        //    double totalMRPPcs = 0;
        //    double totalBeforeSet = 0;
        //    double totalBeforePCS = 0;
        //    double totalAfterSet = 0;
        //    double totalAfterPcs = 0;

        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        string sMRPTemp = dr["MRPQty"].ToString();
        //        string sBeforeTemp = dr["Before"].ToString();
        //        string sAfterTemp = dr["After"].ToString();


        //        string[] sMRPTempArr = sMRPTemp.Replace(")", "").Split('(');
        //        double MRPSet = double.Parse(sMRPTempArr[0]);
        //        double MRPPcs = double.Parse(sMRPTempArr[1]);

        //        string[] sBeforeTempArr = sBeforeTemp.Replace(")", "").Split('(');
        //        double BeforeSet = double.Parse(sBeforeTempArr[0]);
        //        double BeforePcs = double.Parse(sBeforeTempArr[1]);

        //        string[] sAfterTempArr = sAfterTemp.Replace(")", "").Split('(');
        //        double AfterSet = double.Parse(sAfterTempArr[0]);
        //        double AfterPcs = double.Parse(sAfterTempArr[1]);

                

        //        totalMRPSet += MRPSet;
        //        totalMRPPcs += MRPPcs;
        //        totalBeforeSet += BeforeSet;
        //        totalBeforePCS += BeforePcs;
        //        totalAfterSet += AfterSet;
        //        totalAfterPcs += AfterPcs;
        //    }



        //    DataRow drNew = dt.NewRow();
        //    drNew["JobNumber"] = "Total";

        //    drNew["MRPQty"] = totalMRPSet.ToString() + "(" + totalMRPPcs.ToString() + ")";
        //    drNew["Before"] = totalBeforeSet.ToString() + "(" + totalBeforePCS.ToString() + ")";
        //    drNew["After"] = totalAfterSet.ToString() + "(" + totalAfterPcs.ToString() + ")";
         

        //    dt.Rows.Add(drNew);
            

        //    return dt;
        //}

        //public DataTable GetInventoryQty()
        //{
        //    DataSet ds = dal.GetInventoryQty();

        //    if (ds == null || ds.Tables.Count == 0)
        //    {
        //        return null;
        //    }


        //    return ds.Tables[0];
        //}


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
