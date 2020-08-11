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


        /// <summary>
        /// 获取 part no, model, supplier, customer, Number信息列表
        /// 用于自动填充下拉框选择项.
        /// </summary>
        #region 

        const string constPartNo = "partNo";
        const string constModel = "model";
        const string constSupplier = "supplier";
        const string constCustomer = "customer";
        const string constNumber = "number";


        private List<string> GetGroupByList(string sGroupByField)
        {
            DataTable dt = dal.GetALL();

            if (dt == null || dt.Rows.Count == 0)
                return null;




            List<string> modelList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string partNo = dr["partNumber"].ToString();
                string model = dr["module"].ToString();
                string supplier = dr["Supplier"].ToString();
                string customer = dr["customer"].ToString();
                string number = dr["number"].ToString().ToUpper();

                switch (sGroupByField)
                {
                    case constPartNo:
                        modelList.Add(partNo);
                        break;
                    case constModel:
                        modelList.Add(model);
                        break;
                    case constSupplier:
                        modelList.Add(supplier);
                        break;
                    case constCustomer:
                        modelList.Add(customer);
                        break;
                    case constNumber:
                        modelList.Add(number);
                        break;

                    default:
                        break;
                }
            }


            //去重
            modelList = modelList.Distinct().ToList();

            //正序排序
            modelList.Sort();

            return modelList;
        }

        public List<string> GetModelList()
        {
            return GetGroupByList(constModel);
        }

        public List<string> GetPatNoList()
        {
            return GetGroupByList(constPartNo);
        }
        public List<string> GetSupplierList()
        {
            return GetGroupByList(constSupplier);
        }
        public List<string> GetCustomerList()
        {
            return GetGroupByList(constCustomer);
        }
        public List<string> GetNumberList()
        {
            return GetGroupByList(constNumber);
        }
        #endregion




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
        

        public DataTable GetNumberForColumn()
        {
            return dal.GetNumberForColumn();
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
