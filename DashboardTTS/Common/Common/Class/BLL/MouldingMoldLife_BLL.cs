using System;
using System.Data;
using System.Collections.Generic;
using Common.Model;
using System.Data.SqlClient;


namespace Common.Class.BLL
{
    /// <summary>
    /// MouldingChangeModel_BLL
    /// </summary>
    public class MouldingMoldLife_BLL
    {
        private readonly Common.Class.DAL.MouldingMoldLife_DAL dal = new Common.Class.DAL.MouldingMoldLife_DAL();
        public MouldingMoldLife_BLL()
        { }
   
  
        public DataTable GetList(string sMouldID, string sMachineID)
        {
            DataSet ds = dal.SelectList( sMouldID, sMachineID);

        
            if (ds == null|| ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }

        public bool UpdateMouldLife(string sMouldID, int iMouldLife)
        {
            int result = dal.UpdateMouldLife(sMouldID, iMouldLife);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable GetHistoryList(string sMouldID, string sMachineID)
        {
            DataSet ds = dal.SelectHistoryList(sMouldID, sMachineID);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }


        public List<string> GetMouldID(string sMachineID)
        {
            DataSet ds = dal.SelectList("", sMachineID);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            List<string> MouldIDList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string MouldID = dr["MoldID"].ToString();

                MouldIDList.Add(MouldID);
            }

            return MouldIDList;
        }


      
        public Common.Class.Model.MouldingMoldLife_Model GetModelbyChaseID(string sChaseID)
        {
            DataSet ds = dal.SelectList(sChaseID, "");

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            Common.Class.Model.MouldingMoldLife_Model model = new Model.MouldingMoldLife_Model();
            model.MoldID = dr["MoldID"].ToString();
            model.MachineID = dr["MachineID"].ToString();
            model.PartNumberAll = dr["PartNumberAll"].ToString();
            model.Accumulate = int.Parse(dr["Accumulate"].ToString());
            model.Clean1Qty = dr["Clean1Qty"].ToString() != "" ? int.Parse(dr["Clean1Qty"].ToString()) : 0;
            if (dr["Clean1Time"].ToString() != "")
            {
                model.Clean1Time = DateTime.Parse(dr["Clean1Time"].ToString());
            }
            model.Clean1TimeBy = dr["Clean1TimeBy"].ToString();

            model.Clean2Qty = dr["Clean2Qty"].ToString() != "" ? int.Parse(dr["Clean2Qty"].ToString()) : 0;
            if (dr["Clean2Time"].ToString() != "")
            {
                model.Clean2Time = DateTime.Parse(dr["Clean2Time"].ToString());
            }
            model.Clean2TimeBy = dr["Clean2TimeBy"].ToString();

            model.Clean3Qty = dr["Clean3Qty"].ToString() != "" ? int.Parse(dr["Clean3Qty"].ToString()) : 0;
            if (dr["Clean3Time"].ToString() != "")
            {
                model.Clean3Time = DateTime.Parse(dr["Clean3Time"].ToString());
            }
            model.Clean3TimeBy = dr["Clean3TimeBy"].ToString();

            model.Clean4Qty = dr["Clean4Qty"].ToString() != "" ? int.Parse(dr["Clean4Qty"].ToString()) : 0;
            if (dr["Clean4Time"].ToString() != "")
            {
                model.Clean4Time = DateTime.Parse(dr["Clean4Time"].ToString());
            }
            model.Clean4TimeBy = dr["Clean4TimeBy"].ToString();

            model.Clean5Qty = dr["Clean5Qty"].ToString() != "" ? int.Parse(dr["Clean5Qty"].ToString()) : 0;
            if (dr["Clean5Time"].ToString() != "")
            {
                model.Clean5Time = DateTime.Parse(dr["Clean5Time"].ToString());
            }
            model.Clean5TimeBy = dr["Clean5TimeBy"].ToString();

            model.ChangeQty = dr["ChangeQty"].ToString() != "" ? int.Parse(dr["ChangeQty"].ToString()) : 0;
            if (dr["ChangeTime"].ToString() != "")
            {
                model.ChangeTime = DateTime.Parse(dr["ChangeTime"].ToString());
            }
            model.ChangeBy = dr["ChangeBy"].ToString();

            if (dr["CreateTime"].ToString() != "")
            {
                model.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
            }

            if (dr["UpdatedTime"].ToString() != "")
            {
                model.UpdatedTime = DateTime.Parse(dr["UpdatedTime"].ToString());
            }



            return model;
        }
        public bool UpdateClean_wHis(Common.Class.Model.MouldingMoldLife_Model objMoldLife )
        {
            bool Result = false;

            List<SqlCommand> listCmd = new List<SqlCommand>();

            //insert to his 
            listCmd.Add(dal.AddCMD(objMoldLife));


            objMoldLife.Clean1Qty = 0;
            //update tracking
            listCmd.Add(dal.UpdateChangeCMD(objMoldLife));
             

            Result = DBHelp.SqlDB.SetData_Rollback(listCmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);


            return Result;
        }

    
        public bool UpdateChange(Common.Class.Model.MouldingMoldLife_Model model_new, Common.Class.Model.MouldingMoldLife_Model model_old)
        {
            bool Result = false;

            List<SqlCommand> listCmd = new List<SqlCommand>();

            //update tracking
            listCmd.Add(dal.UpdateChangeCMD(model_new));

            //insert to his
            if (model_old.MoldID != "")
            {
                listCmd.Add(dal.AddCMD(model_old));
            }
          



            Result = DBHelp.SqlDB.SetData_Rollback(listCmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            

            return Result;
        }


        public bool UpdateTransfer(Common.Class.Model.MouldingMoldLife_Model Model_New, Common.Class.Model.MouldingMoldLife_Model Model_His, Common.Class.Model.MouldingMoldLife_Model Model_Transfer)
        {
            bool Result = false;

            List<SqlCommand> listCmd = new List<SqlCommand>();

            //insert to his
            if (Model_His.MoldID != "")
            {
                listCmd.Add(dal.AddCMD(Model_His));
            }


            //update tracking
            listCmd.Add(dal.UpdateChangeCMD(Model_New));


            //update tracking
            listCmd.Add(dal.UpdateChangeCMD(Model_Transfer));

            



            Result = DBHelp.SqlDB.SetData_Rollback(listCmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);


            return Result;
        }



    

      

    }
}

