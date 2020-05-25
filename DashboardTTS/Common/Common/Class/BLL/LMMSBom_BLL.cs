using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class LMMSBom_BLL
    {
        private readonly Common.Class.DAL.LMMSBom_DAL dal = new DAL.LMMSBom_DAL();
        private readonly Common.Class.DAL.LMMSBomDetail_DAL dal_BomDetail = new DAL.LMMSBomDetail_DAL();

        public LMMSBom_BLL()
        {
             
        }

        public DataTable GetList(string sPartNo, string sMachineID)
        {
            DataSet ds = dal.GetList(sPartNo, sMachineID,false);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable GetPartList()
        {
            DataSet ds = dal.GetAllPartList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public List<string> GetPartNoList()
        {
            DataTable dt = dal.GetAllPartNoList();
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<string> partList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["PartNumber"].ToString();
                if (partNo != "")
                    partList.Add(partNo);
            }

            return partList;
        }

     

        public DataTable GetModelList()
        {
            DataSet ds = dal.GetAllModelList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public Common.Class.Model.LMMSBom_Model GetBomModel(string sPartNumber,string sMachineID)
        {
            Common.Class.Model.LMMSBom_Model model = new Model.LMMSBom_Model();

            DataTable dt = GetList(sPartNumber, sMachineID);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];
            
            model.partNumber = dr["partNumber"].ToString();
            model.module = dr["module"].ToString();
            model.cycleTime = dr["cycleTime"].ToString() == "" ? 1 : double.Parse(dr["cycleTime"].ToString());
            model.blockCount = dr["blockCount"].ToString() == "" ? 1 : int.Parse(dr["blockCount"].ToString());
            model.unitCount = dr["unitCount"].ToString() == "" ? 1 : int.Parse(dr["unitCount"].ToString());
            model.machineID = dr["machineID"].ToString();
            model.remarks = dr["remarks"].ToString();
            model.Type = dr["type"].ToString();
            model.Customer = dr["customer"].ToString();
            model.Supplier = dr["Supplier"].ToString();

            model.Lighting = dr["lighting"].ToString();
            model.Camera = dr["camera"].ToString();
            model.CurrentPower = dr["currentPower"].ToString();
            model.PartBelongTo = dr["PartBelongTo"].ToString();

            model.Number = dr["Number"].ToString();



            return model;
        }

        public bool IsExist(string sPartNo, string sMachineID)
        {
            
            DataTable dt = GetList(sPartNo, sMachineID);

            bool Result = dt.Rows.Count > 0 ? true : false;
            
         
            return Result;
        }

        public DataTable GetList(string sPartNo, string sMachineID, bool EnableFuzzySearch)
        {
            DataSet ds = dal.GetList(sPartNo, sMachineID, EnableFuzzySearch);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }
        

        public DataTable GetOnefoldPartList()
        {
            DataSet ds = dal.GetOnefoldPartList();

            DataTable dt = new DataTable();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                dt = ds.Tables[0];
            }

            DataRow dr = dt.NewRow();

            dr[0] = "Total :";
            dt.Rows.Add(dr);
            return dt;
        }


        public DataTable GetMachineCapabilityList()
        {
            DataTable dt = dal.GetPartList();

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            
            DataRow dr = dt.NewRow();

            dr[0] = "Total :";
            dt.Rows.Add(dr);
            return dt;
        }


     

      
        public bool AddBomDetailRollback(Common.Class.Model.LMMSBom_Model Bom_Model,List< Common.Class.Model.LMMSBomDetail_Model> List_BomDetail_Model)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();

            list_cmd.Add(dal.AddCommand(Bom_Model));

            list_cmd.Add(dal_BomDetail.DeleteCommand(Bom_Model.partNumber));
            
            foreach (Common.Class.Model.LMMSBomDetail_Model item in List_BomDetail_Model)
            {
                if (item != null)
                {
                    list_cmd.Add(dal_BomDetail.AddCommand(item));
                }
            }

            return DBHelp.SqlDB.SetData_Rollback(list_cmd);
        }

        public bool DeleteRollback(string PartNumber)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();

            list_cmd.Add(dal.DeleteByPart(PartNumber));

            DataSet ds = dal_BomDetail.GetListByPartNumber(PartNumber);
            if (ds == null || ds.Tables.Count ==0)
            {

            }else
            {
                if (ds.Tables[0].Rows.Count >0)
                {
                    list_cmd.Add(dal_BomDetail.DeleteCommand(PartNumber));
                }
            }
           
            

            return DBHelp.SqlDB.SetData_Rollback(list_cmd);
        }

        public bool UpdateBomDetailRollback(Common.Class.Model.LMMSBom_Model Bom_Model, List<Common.Class.Model.LMMSBomDetail_Model> List_BomDetail_Model)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();

            DataTable dt = dal_BomDetail.SearchByPart(Bom_Model.partNumber);
            
            if (dt.Rows.Count != 0)
            {
                list_cmd.Add(dal_BomDetail.DeleteByPartCommadnd(Bom_Model.partNumber));
            }
            
            list_cmd.Add(dal.UpdateCommand(Bom_Model));

            foreach (Common.Class.Model.LMMSBomDetail_Model item in List_BomDetail_Model)
            {
                if (item != null)
                {
                    list_cmd.Add(dal_BomDetail.AddCommand(item));
                }
            }

            return DBHelp.SqlDB.SetData_Rollback(list_cmd);
        }

        public bool Insert(Common.Class.Model.LMMSBom_Model model)
        {
            try
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
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "LMMSBom insert error " + ee.ToString());
                return false;
            }
        }

        public bool UpdateByPartNo(Common.Class.Model.LMMSBom_Model model)
        {
            try
            {
                int result = dal.UpdateByPartNumber(model);

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "LMMSBom Update error " + ee.ToString());
                return false;
            }
        }

        public bool DeleteByPartNo(string PartNo,string machineID)
        {
            try 
            {
                int result = dal.Delete(PartNo, machineID);

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "LMMSBom Delete error " + ee.ToString());
                return false;
            }
        }
    }
}
