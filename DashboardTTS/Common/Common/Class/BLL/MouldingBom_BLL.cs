using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Common.Class.BLL
{
   public  class MouldingBom_BLL
    {
        public MouldingBom_BLL()
        {

        }


        public List<String> GetPartNoList()
        {
            DataSet ds = dal.GetPartNoList();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

        

            List<string> listPart = new List<string>();
            listPart.Add("");
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["PartNumber"].ToString();

                if (partNo != "")
                {
                    listPart.Add(partNo);
                }
            }

            return listPart;
        }


        public List<String> GetJigNoList()
        {
            DataSet ds = dal.GetJigNoList();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];



            List<string> jigNoList = new List<string>();
          
            foreach (DataRow dr in dt.Rows)
            {
                string jigNo = dr["jigNo"].ToString();

                if (jigNo != "")
                {
                    jigNoList.Add(jigNo);
                }
            }



            return jigNoList;
        }




        Common.Class.DAL.MouldingBom_DAL dal = new DAL.MouldingBom_DAL();

        public DataTable GetList()
        {
            DataSet ds = dal.SelectAll("");

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }

        public List<String> GetPartNumberAllList()
        {
            DataSet ds = dal.SelectAll("");

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            dt.DefaultView.Sort = "PartNumberAll ASC";

            List<string> listPart = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string PartNumberAll = dr["PartNumberAll"].ToString();

                if (PartNumberAll != "")
                {
                    listPart.Add(PartNumberAll);
                }
            }
            
            return listPart;
        }

        public List<String> GetCheackCleanList()
        {
            DataSet ds = dal.SelectCleanList("");

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            dt.DefaultView.Sort = "ChecklistID ASC";

            List<string> listPart = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string PartNumberAll = dr["CheckList"].ToString();

                if (PartNumberAll != "")
                {
                    listPart.Add(PartNumberAll);
                }
            }

            return listPart;
        }

        public DataTable GetList(string sPartNumber)
        {
            DataSet ds = dal.SelectAll( sPartNumber);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable GetListForMaterialBudget()
        {
            DataTable dt = dal.GetListForMaterialBudget();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {

                //DataRow drTotal = dt.NewRow();
                //drTotal["partNumberAll"] = "Total";
                //dt.Rows.Add(drTotal);


                return dt;
            }
        }


        public List<Common.Class.Model.MouldingBom_Model> GetModelList()
        {
            DataTable dt = dal.GetList("", "");
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<Common.Class.Model.MouldingBom_Model> modelList = new List<Model.MouldingBom_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                modelList.Add(convertModel(dr));
            }


            return modelList;
        }

        public Common.Class.Model.MouldingBom_Model GetModel(string sPartNo, string sPartNoAll)
        {
            DataTable  dt = dal.GetList(sPartNo, sPartNoAll);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            
            return convertModel(dt.Rows[0]);
        }

     



        public Common.Class.Model.MouldingBom_Model convertModel(DataRow    dr)
        {
            if (dr == null )
                return null;



            Common.Class.Model.MouldingBom_Model model = new Model.MouldingBom_Model();

            model.partNumber = dr["partNumber"].ToString();
            model.matPart01 = dr["matPart01"].ToString();
            model.matPart02 = dr["matPart02"].ToString();
            model.materialWeight01 = double.Parse(dr["materialWeight01"].ToString());
            model.materialWeight02 = double.Parse(dr["materialWeight02"].ToString());


            model.customer = dr["customer"].ToString();

            model.model = dr["model"].ToString();
            model.jigNo = dr["jigNo"].ToString();
            model.cavityCount = double.Parse(dr["cavityCount"].ToString());
            model.partsWeight = double.Parse(dr["partsWeight"].ToString());
            model.cycleTime = double.Parse(dr["cycleTime"].ToString());
            model.blockCount = decimal.Parse(dr["blockCount"].ToString());
            model.unitCount = decimal.Parse(dr["unitCount"].ToString());
            model.machineID = dr["machineID"].ToString();
            model.userName = dr["userName"].ToString();
            model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
            model.remarks = dr["remarks"].ToString();
            model.partNumberAll = dr["partNumberAll"].ToString();
            model.suppiller = dr["suppiller"].ToString();
            model.refField01 = dr["refField01"].ToString();
            model.refField02 = dr["refField02"].ToString();
            model.refField03 = dr["refField03"].ToString();
            model.refField04 = dr["refField04"].ToString();
            model.refField05 = dr["refField05"].ToString();

            return model;
        }


       


        public bool DeleteByPartAll(string sPartNumberAll)
        {

            int result = dal.Delete(sPartNumberAll);

            
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Add(Common.Class.Model.MouldingBom_Model model)
        {
         
            bool result = false;
            try
            {
                string[] PartArr = model.partNumberAll.Split(',');

                List<SqlCommand> ListCMD = new List<SqlCommand>();

                foreach (string part in PartArr)
                {
                    Common.Class.Model.MouldingBom_Model modelNew = model;
                    modelNew.partNumber = part;


                    ListCMD.Add(dal.AddCmd(modelNew));
                }

              


                result = DBHelp.SqlDB.SetData_Rollback(ListCMD, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

             
            }
            catch (Exception ee)
            {
                result = false;
            
            }
            return result;
        }


        public bool Update(Common.Class.Model.MouldingBom_Model model)
        {
            string[] PartArr = model.partNumberAll.Split(',');

            List<SqlCommand> ListCMD = new List<SqlCommand>();

            foreach (string part in PartArr)
            {
                Common.Class.Model.MouldingBom_Model modelNew = model;
                modelNew.partNumber = part;

                SqlCommand cmd = dal.UpdateCmd(modelNew);
                ListCMD.Add(cmd);
            }

            bool result = DBHelp.SqlDB.SetData_Rollback(ListCMD, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return result;
        }

    }
}
