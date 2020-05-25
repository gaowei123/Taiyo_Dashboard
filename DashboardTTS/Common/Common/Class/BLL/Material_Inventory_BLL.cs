using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class Material_Inventory_BLL
    {

        Common.Class.DAL.Material_Inventory_DAL dal = new DAL.Material_Inventory_DAL();
        Common.Class.DAL.Material_Inventory_History_DAL his_dal = new DAL.Material_Inventory_History_DAL();

        public DataTable GetList(string sPartNumber, bool IsShowDetail,string sEvent)
        {
            DataTable dt_return = new DataTable();

            if (IsShowDetail)
            {
                DataSet ds = new DataSet();
                ds = dal.SelectAll(sPartNumber, sEvent);

                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }

                dt_return = ds.Tables[0];
                if (dt_return.Rows[0]["Material_No"] != null)
                {
                    DataTable dt_data = new DataTable();

                    double TotalCost = 0;
                    double TotalInventory_Weight = 0;
                    double TotalTransaction_Weight = 0;
                    foreach (DataRow dr in dt_return.Rows)
                    {
                        try
                        {
                            TotalCost += double.Parse(dr["TotalCost_temp"].ToString());
                            TotalInventory_Weight += double.Parse(dr["Inventory_Weight_temp"].ToString());
                            TotalTransaction_Weight += double.Parse(dr["Transaction_Weight_temp"].ToString());
                        }
                        catch
                        { }
                    }

                    DataRow dr_total = dt_return.NewRow();
                    dr_total["Material_No"] = "Total :";
                    dr_total["Inventory_Weight"] = TotalInventory_Weight + "Kg";
                    dr_total["Transaction_Weight"] = TotalTransaction_Weight + "Kg";
                    dr_total["TotalCost"] = "SGD " + TotalCost;
                    dt_return.Rows.Add(dr_total);
                }
            }
            else
            {
                DataSet ds = new DataSet();
                ds = dal.SelectGroupByPart(sPartNumber);

                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }
                else
                {
                    DataTable dt = ds.Tables[0];

                    dt.Columns.Add("Material_LotNo");
                    foreach (DataRow dr in dt.Rows)
                    {
                        string Material_No = dr["Material_No"].ToString();
                        //string Supplier = dr["Supplier"].ToString();
                        dr["Material_LotNo"] = GetLotNoByPart(Material_No, "");
                    }

                    dt_return = dt;
                }
                if (dt_return.Rows[0]["Material_No"] != null)
                {
                    DataTable dt_data = new DataTable();

                    double TotalCost = 0;
                    double TotalInventory_Weight = 0;
                    foreach (DataRow dr in dt_return.Rows)
                    {
                        try
                        {
                            TotalCost += double.Parse(dr["TotalCost_temp"].ToString());
                            TotalInventory_Weight += double.Parse(dr["Inventory_Weight_temp"].ToString());
                        }
                        catch
                        { }
                    }

                    DataRow dr_total = dt_return.NewRow();
                    dr_total["Material_No"] = "Total :";
                    dr_total["Inventory_Weight"] = TotalInventory_Weight + "Kg";
                    dr_total["TotalCost"] = "SGD " + TotalCost;
                    dt_return.Rows.Add(dr_total);
                }
            }


            return dt_return;
        }




        public DataTable GetList(string sMaterialNo,string sMaterialLotNo,DateTime? dLoadDate)
        {
            DataSet ds = dal.SelectAll(sMaterialNo, sMaterialLotNo, dLoadDate);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable GetMaterialList()
        {
            DataSet ds = dal.SelectMaterialList();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable GetMaterialLotList(string sMaterialNo)
        {
            DataSet ds = dal.SelectMaterialLotList(sMaterialNo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable GetMaterialMachine(string sMaterialNo,string sMachine,DateTime dfrom, DateTime dto)
        {
            DataSet ds = dal.GetMaterialMachine(sMaterialNo, sMachine,dfrom,dto);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable GetMaterialMonthly( DateTime dfrom, DateTime dto)
        {
            DataSet ds = dal.GetMaterialMonthly( dfrom, dto);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }

        public string GetLotNoByPart(string sMaterial_Part ,string sSupplier)
        {
            DataSet ds_temp = dal.SelectLotNoByPart(sMaterial_Part, sSupplier);
            if (ds_temp == null || ds_temp.Tables.Count == 0)
            {
                return "";
            }

            DataTable dt = ds_temp.Tables[0];


            if (dt.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder Material_Lot_No = new StringBuilder();

            foreach (DataRow  dr in dt.Rows)
            {
                string Lot_temp = dr["Material_LotNo"].ToString();

                if (!Material_Lot_No.ToString().Contains(Lot_temp))
                {
                    Material_Lot_No.Append(Lot_temp);
                    Material_Lot_No.Append(",");
                }

              
            }

            string LotNo = Material_Lot_No.ToString();

            //去掉最后一个 ','
            LotNo = LotNo.Substring(0, LotNo.Length - 1);

            return LotNo;

        }

        


        public bool UnloadTransaction(List<Common.Class.Model.Material_Inventory> modelList,string sUsername)
        {
            List<SqlCommand> CmdList = new List<SqlCommand>();

            foreach (Common.Class.Model.Material_Inventory model in modelList)
            {

                model.User_Name = sUsername;

                // current wight == 0 delete
                if (model.Inventory_Weight == 0)
                {
                    CmdList.Add(dal.DeleteCMD(model.Material_No,model.Material_LotNo,model.Load_Time));
                }

                // current wight > 0 update wight
                else if (model.Inventory_Weight > 0)
                {
                    CmdList.Add(dal.UpdateCMD(model));
                }



                Common.Class.Model.Material_Inventory_History temp_HisModel = his_dal.copyobj(model);
                CmdList.Add(his_dal.AddCMD(temp_HisModel));
            }


            return DBHelp.SqlDB.SetData_Rollback(CmdList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        
        public bool AddTransaction(List<Common.Class.Model.Material_Inventory> modelList, string sUsername)
        {
            List<SqlCommand> CmdList = new List<SqlCommand>();
            

            foreach (Common.Class.Model.Material_Inventory model in modelList)
            {
                model.User_Name = sUsername;
                SqlCommand addcmd = dal.AddCMD(model);
                CmdList.Add(addcmd);



                Common.Class.Model.Material_Inventory_History temp_HisModel = his_dal.copyobj(model);
                SqlCommand addhiscmd = his_dal.AddCMD(temp_HisModel);
                CmdList.Add(addhiscmd);


            }

           
            return DBHelp.SqlDB.SetData_Rollback(CmdList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        
        public bool DeleteTransaction(Common.Class.Model.Material_Inventory model)
        {
            List<SqlCommand> CMDList = new List<SqlCommand>();


            CMDList.Add(dal.DeleteCMD(model.Material_No, model.Material_LotNo, model.Load_Time));

            Common.Class.Model.Material_Inventory_History temp_HisModel = his_dal.copyobj(model);
            CMDList.Add(his_dal.AddCMD(temp_HisModel));


            bool Result = DBHelp.SqlDB.SetData_Rollback(CMDList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return Result;
        }

        
    }
}
