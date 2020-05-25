using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class Material_Inventory_History_BLL
    {
        Common.Class.DAL.Material_Inventory_History_DAL dal = new DAL.Material_Inventory_History_DAL();
      

        public string GetLotNoByPart(string sMaterial_Part,string sLastEvent, string sSupplier)
        {
            DataSet ds_temp = dal.SelectLotNoByPart(sMaterial_Part, sLastEvent, sSupplier);
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

            foreach (DataRow dr in dt.Rows)
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



        public DataTable GetList(string sPartNumber, DateTime from, DateTime to, bool IsShowDetail,string sEvent)
        {
            DataTable dt_return = new DataTable();

            if (IsShowDetail)
            {
                DataSet ds = dal.SelectAll(sPartNumber, from, to, sEvent);

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
                DataTable dt_temp = GetListByMaterial(sPartNumber, from, to, sEvent);
                dt_temp.Columns.Add("Material_LotNo");

                foreach (DataRow dr in dt_temp.Rows)
                {
                    string MaterialPart = dr["Material_No"].ToString();
                    string LastEvent = dr["Last_Event"].ToString();
                    //string Supplier = dr["Supplier"].ToString();

                    dr["Material_LotNo"] = GetLotNoByPart(MaterialPart, LastEvent, "");

                }

                dt_return = dt_temp;

                if (dt_return.Rows[0]["Material_No"] != null)
                {
                    DataTable dt_data = new DataTable();

                    double TotalCost = 0;
                    double TotalTransaction_Weight = 0;
                    foreach (DataRow dr in dt_return.Rows)
                    {
                        try
                        {
                            TotalCost += double.Parse(dr["TotalCost_temp"].ToString());

                            TotalTransaction_Weight += double.Parse(dr["Transaction_Weight_temp"].ToString());
                        }
                        catch
                        { }
                    }

                    DataRow dr_total = dt_return.NewRow();
                    dr_total["Material_No"] = "Total :";
                    dr_total["TotalCost"] = "SGD " + TotalCost;
                    dr_total["Transaction_Weight"] = TotalTransaction_Weight + "Kg";
                    dt_return.Rows.Add(dr_total);
                }
            }
           

            return dt_return;
        }

        public DataTable GetListByMaterial(string sPartNumber, DateTime from, DateTime to,string sEvent)
        {
           
            DataSet ds = dal.SelectAllByMaterial(sPartNumber, from, to, sEvent);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public DataTable GetMonthlyReport(string sYear)
        {
            DataSet ds = dal.GetMonthLyReport(sYear);

            if (ds==null|| ds.Tables.Count ==0)
                return null;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            DataRow drTotal = dt.NewRow();
            drTotal["Month"] = "Grand Total:";
            drTotal["LoadKgs"] = dt.Compute("SUM(LoadKgs)", "").ToString();
            drTotal["UnLoadKgs"] = dt.Compute("SUM(UnLoadKgs)", "").ToString();
            drTotal["ReturnKgs"] = dt.Compute("SUM(ReturnKgs)", "").ToString();
            drTotal["UsageKgs"] = dt.Compute("SUM(UsageKgs)", "").ToString();
            drTotal["UsageCost"] = dt.Compute("SUM(UsageCost)", "").ToString();

            dt.Rows.Add(drTotal);
            
            return dt;
        }

        public DataTable GetMaterialDetailForMonth(int iMonth, int iYear)
        {
            DataSet ds = dal.GetMaterialDetailForMonth(iMonth, iYear);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            DataRow drTotal = dt.NewRow();
            drTotal["Material_No"] = "Grand Total:";
            drTotal["LoadKgs"] = dt.Compute("SUM(LoadKgs)", "").ToString();
            drTotal["LoadCost"] = dt.Compute("SUM(LoadCost)", "").ToString();

            drTotal["UsedKgs"] = dt.Compute("SUM(UsedKgs)", "").ToString();
            drTotal["UsedSGD"] = dt.Compute("SUM(UsedSGD)", "").ToString();

            dt.Rows.Add(drTotal);

            return dt;
        }


        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {

            DataSet ds = dal.GetList(dDateFrom, dDateTo, sMachineID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }



    }
}
