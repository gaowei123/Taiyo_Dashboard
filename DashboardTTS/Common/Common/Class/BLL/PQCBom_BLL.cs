
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

using Common.Model;
namespace Common.Class.BLL
{
    /// <summary>
    /// PQCBom_BLL
    /// </summary>
    public class PQCBom_BLL
    {
        private readonly Common.DAL.PQCBom_DAL dal = new Common.DAL.PQCBom_DAL();
        public PQCBom_BLL()
        { }

        #region  Method
        
        public void Add(Common.Class.Model.PQCBom_Model model)
        {
            dal.Add(model);
        }
        
        public SqlCommand AddCommand(Common.Class.Model.PQCBom_Model model)
        {
            return dal.AddCommand(model);
        }

        public bool Update(Common.Class.Model.PQCBom_Model model)
        {
            return dal.Update(model);
        }
        
        public SqlCommand UpdateCommand(Common.Class.Model.PQCBom_Model model)
        {
            return dal.UpdateCommand(model);
        }
        
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.Delete();
        }
        
        public List<Common.Class.Model.PQCBom_Model> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.PQCBom_Model> modelList = new List<Common.Class.Model.PQCBom_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCBom_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.PQCBom_Model();
                    model.partNumber = dt.Rows[n]["partNumber"].ToString();
                    model.customer = dt.Rows[n]["customer"].ToString();
                    model.model = dt.Rows[n]["model"].ToString();
                    model.jigNo = dt.Rows[n]["jigNo"].ToString();
                    if (dt.Rows[n]["cavityCount"].ToString() != "")
                    {
                        model.cavityCount = decimal.Parse(dt.Rows[n]["cavityCount"].ToString());
                    }
                    if (dt.Rows[n]["cycleTime"].ToString() != "")
                    {
                        model.cycleTime = decimal.Parse(dt.Rows[n]["cycleTime"].ToString());
                    }
                    if (dt.Rows[n]["blockCount"].ToString() != "")
                    {
                        model.blockCount = int.Parse(dt.Rows[n]["blockCount"].ToString());
                    }
                    if (dt.Rows[n]["unitCount"].ToString() != "")
                    {
                        model.unitCount = int.Parse(dt.Rows[n]["unitCount"].ToString());
                    }
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    model.userName = dt.Rows[n]["userName"].ToString();
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.color = dt.Rows[n]["color"].ToString();
                    model.processes = dt.Rows[n]["processes"].ToString();
                    model.remark_1 = dt.Rows[n]["remark_1"].ToString();
                    model.remark_2 = dt.Rows[n]["remark_2"].ToString();
                    model.remark_3 = dt.Rows[n]["remark_3"].ToString();
                    model.remark_4 = dt.Rows[n]["remark_4"].ToString();
                    model.remarks = dt.Rows[n]["remarks"].ToString();


                    model.ShipTo = dt.Rows[n]["shipTo"].ToString();
                    model.Type = dt.Rows[n]["type"].ToString();
                    model.Coating = dt.Rows[n]["coating"].ToString();
                    model.Description = dt.Rows[n]["description"].ToString();

                    if (dt.Rows[n]["unitCost"].ToString() != "")
                    {
                        model.UnitCost = decimal.Parse(dt.Rows[n]["unitCost"].ToString());
                    }
                    
                    model.Number = dt.Rows[n]["number"].ToString();



                    modelList.Add(model);
                }
            }
            return modelList;
        }
        
        public Common.Class.Model.PQCBom_Model CopyObj(Common.Class.Model.PQCBom_Model objModel)
        {
            Common.Class.Model.PQCBom_Model model;
            model = new Common.Class.Model.PQCBom_Model();
            model.partNumber = objModel.partNumber;
            model.customer = objModel.customer;
            model.model = objModel.model;
            model.jigNo = objModel.jigNo;
            model.cavityCount = objModel.cavityCount;
            model.cycleTime = objModel.cycleTime;
            model.blockCount = objModel.blockCount;
            model.unitCount = objModel.unitCount;
            model.machineID = objModel.machineID;
            model.userName = objModel.userName;
            model.dateTime = objModel.dateTime;
            model.color = objModel.color;
            model.processes = objModel.processes;
            model.remark_1 = objModel.remark_1;
            model.remark_2 = objModel.remark_2;
            model.remark_3 = objModel.remark_3;
            model.remark_4 = objModel.remark_4;
            model.remarks = objModel.remarks;
            return model;
        }
        
        #endregion  Method










        public DataTable GetList(string sPartNo)
        {
            DataSet ds = new DataSet();

            ds = dal.GetList(sPartNo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetListWithDetail(string sPartNo)
        {
            DataSet ds = new DataSet();

            ds = dal.GetListWithDetail(sPartNo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public Common.Class.Model.PQCBom_Model GetModel(string sPartNo)
        {
            DataSet ds = dal.GetListForModel(sPartNo);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count ==0)
                return null;
            
            return DataRowToModel(ds.Tables[0].Rows[0]);
        }


        public bool AddRollback(Common.Class.Model.PQCBom_Model model, List<Common.Class.Model.PQCBomDetail_Model> detailModelList, string userName)
        {

            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(dal.AddCommand(model));


            Common.Class.DAL.PQCBomDetail_DAL detailBomDAL = new Common.Class.DAL.PQCBomDetail_DAL();

            foreach (Common.Class.Model.PQCBomDetail_Model detailModel in detailModelList)
            {
                detailModel.userName = userName;

                cmdList.Add(detailBomDAL.AddCommand(detailModel));
            }


            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public bool UpdateRollback(Common.Class.Model.PQCBom_Model model, List<Common.Class.Model.PQCBomDetail_Model> detailModelList, string userName)
        {

            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(dal.UpdateCommand(model));


            Common.Class.DAL.PQCBomDetail_DAL detailBomDAL = new Common.Class.DAL.PQCBomDetail_DAL();


            DataSet ds = detailBomDAL.GetList(model.partNumber);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {

            }
            else
            {
                //delete bom detail 
                cmdList.Add(detailBomDAL.DeleteCommand(model.partNumber));
            }

            foreach (Common.Class.Model.PQCBomDetail_Model detailModel in detailModelList)
            {
                detailModel.userName = userName;
                cmdList.Add(detailBomDAL.AddCommand(detailModel));
            }


            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public bool DeleteRollback(string sPartNo)
        {

            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(dal.DeleteCommand(sPartNo));


            Common.Class.DAL.PQCBomDetail_DAL detailBomDAL = new Common.Class.DAL.PQCBomDetail_DAL();
            cmdList.Add(detailBomDAL.DeleteCommand(sPartNo));




            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
        
        public bool Exist(string sPartNo)
        {
            bool result = false;

            DataSet ds = dal.GetList(sPartNo);

            if (ds ==null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                result = false;
            }else
            {
                result = true;
            }




            return result;
        }

        public List<Model.PQCBom_Model> GetModelList()
        {
            DataSet ds = dal.GetListForModel("");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;


            List<Model.PQCBom_Model> bomList = new List<Model.PQCBom_Model>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bomList.Add(DataRowToModel(dr));
            }


            return bomList;
        }



        public List<string> GetPartNoList()
        {
            DataSet ds = dal.GetList("");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            List<string> partList = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string partNo = dr["PartNumber"].ToString();

                if (partNo != "" && (!partList.Contains(partNo)))
                    partList.Add(partNo);
            }


            return partList.OrderBy(p=>p).ToList();
        }

        public List<string> GetModelNoList()
        {
            DataSet ds = dal.GetList("");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;



            List<string> partList = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string partNo = dr["model"].ToString();

                if (partNo != "" && (!partList.Contains(partNo)))
                    partList.Add(partNo);
            }


            return partList.OrderBy(p => p).ToList();
        }


      





        public Model.PQCBom_Model DataRowToModel(DataRow dr)
        {

            Common.Class.Model.PQCBom_Model model = new Common.Class.Model.PQCBom_Model();



            model.partNumber = dr["partNumber"].ToString();
            model.customer = dr["customer"].ToString();
            model.model = dr["model"].ToString();
            model.jigNo = dr["jigNo"].ToString();
            if (dr["cavityCount"].ToString() != "")
            {
                model.cavityCount = decimal.Parse(dr["cavityCount"].ToString());
            }
            if (dr["cycleTime"].ToString() != "")
            {
                model.cycleTime = decimal.Parse(dr["cycleTime"].ToString());
            }
            if (dr["blockCount"].ToString() != "")
            {
                model.blockCount = int.Parse(dr["blockCount"].ToString());
            }
            if (dr["unitCount"].ToString() != "")
            {
                model.unitCount = int.Parse(dr["unitCount"].ToString());
            }
            model.machineID = dr["machineID"].ToString();
            model.userName = dr["userName"].ToString();
            if (dr["dateTime"].ToString() != "")
            {
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
            }
            model.color = dr["color"].ToString();
            model.processes = dr["processes"].ToString();
            model.remark_1 = dr["remark_1"].ToString();
            model.remark_2 = dr["remark_2"].ToString();
            model.remark_3 = dr["remark_3"].ToString();
            model.remark_4 = dr["remark_4"].ToString();
            model.remarks = dr["remarks"].ToString();

            model.ShipTo = dr["shipTo"].ToString();
            model.Type = dr["type"].ToString();
            model.Coating = dr["coating"].ToString();
            model.Description = dr["description"].ToString();

            if (dr["unitCost"].ToString() != "")
            {
                model.UnitCost = decimal.Parse(dr["unitCost"].ToString());
            }

            model.Number = dr["number"].ToString();



            return model;
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
                string model = dr["model"].ToString();
                string supplier = dr["remark_1"].ToString();
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






    }
}

