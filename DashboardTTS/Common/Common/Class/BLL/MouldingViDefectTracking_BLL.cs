using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class MouldingViDefectTracking_BLL
    {
        Common.Class.DAL.MouldingViDefectTracking_DAL dal = new DAL.MouldingViDefectTracking_DAL();
       

        public DataTable Getlist(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNumber, string sRejType, string sModel, string sShift,string sType)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID, sPartNumber, sRejType, sModel, sShift, sType);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];               

                dt.Columns.Add("ChkerID");
                foreach (DataRow dr in dt.Rows)
                {
                    string PartNumber = dr["PartNumber"].ToString();
                  

                    try
                    {
                        DateTime? Day = DateTime.Parse(dr["dayForSearching"].ToString());
                        dr["ChkerID"] = GetCheckerByPart(PartNumber, Day);
                    }
                    catch  {  }
                  
               

                    dr["rejectCost"] = "SGD " + dr["rejectCost"].ToString();
                }
                


                int Total_Rej = 0;
                decimal Total_RejCost = 0;

                Dictionary<string, double> dicTotalOutput = new Dictionary<string, double>();

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        Total_Rej += int.Parse(dr["rejectQty"].ToString());
                        Total_RejCost += decimal.Parse(dr["rejectCost_1"].ToString());

                        string trackingID = dr["trackingID"].ToString();
                        double output = double.Parse(dr["OutPutQTY"].ToString());

                        if (!dicTotalOutput.ContainsKey(trackingID))
                        {
                            dicTotalOutput.Add(trackingID, output);
                        }
                    }
                    catch { }
                }

                DataRow dr_total = dt.NewRow();
                dr_total["day"] = "Total :";
                dr_total["rejectQty"] = Total_Rej;
                dr_total["rejectCost"] = "SGD " + Total_RejCost;
                dt.Rows.Add(dr_total);
                return dt;
            }

            
        }


        public string GetCheckerByPart(string sMaterial_Part, DateTime?  dDay)
        {
            DataSet ds_temp = dal.GetCheckerByPart(sMaterial_Part, dDay);
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
                string userID = dr["userID"].ToString();

                if (!Material_Lot_No.ToString().Contains(userID))
                {
                    Material_Lot_No.Append(userID);
                    Material_Lot_No.Append(",");
                }


            }

            string LotNo = Material_Lot_No.ToString();

            //去掉最后一个 ','
            LotNo = LotNo.Substring(0, LotNo.Length - 1);

            return LotNo;

        }
        

        public DataTable GetMonthlyReport(string sYear, string sPartNo)
        {

            DataSet ds = dal.GetMonthlyRejectReport(sYear, sPartNo);

            if (ds  == null || ds.Tables.Count == 0 )
            {
                return null;
            }


            DataTable dt = ds.Tables[0];



            double dTotalOutput = 0;
            double dTotalRej = 0;


            foreach (DataRow dr in dt.Rows)
            {
                dTotalOutput += double.Parse(dr["Output"].ToString());
                dTotalRej += double.Parse(dr["rejectQty"].ToString());
            }

            DataRow drTotal = dt.NewRow();

            drTotal[0] = "Total";
            drTotal["Output"] = dTotalOutput;
            drTotal["rejectQty"] = dTotalRej;
            drTotal["rejRate"] = Math.Round(dTotalRej / dTotalOutput * 100, 2).ToString("0.00") + "%";


            drTotal["White Dot"] = dt.Compute(" Sum([White Dot]) ", "");
            drTotal["Scratches"] = dt.Compute(" Sum([Scratches]) ", "");
            drTotal["Dented Mark"] = dt.Compute(" Sum([Dented Mark]) ", "");
            drTotal["Shinning Dot"] = dt.Compute(" Sum([Shinning Dot]) ", "");
            drTotal["Black Mark"] = dt.Compute(" Sum([Black Mark]) ", "");
            drTotal["Sink Mark"] = dt.Compute(" Sum([Sink Mark]) ", "");
            drTotal["Flow Mark"] = dt.Compute(" Sum([Flow Mark]) ", "");
            drTotal["High Gate"] = dt.Compute(" Sum([High Gate]) ", "");
            drTotal["Silver Steak"] = dt.Compute(" Sum([Silver Steak]) ", "");
            drTotal["Black Dot"] = dt.Compute(" Sum([Black Dot]) ", "");
            drTotal["Oil Stain"] = dt.Compute(" Sum([Oil Stain]) ", "");
            drTotal["Flow Line"] = dt.Compute(" Sum([Flow Line]) ", "");
            drTotal["Over-Cut"] = dt.Compute(" Sum([Over-Cut]) ", "");
            drTotal["Crack"] = dt.Compute(" Sum([Crack]) ", "");
            drTotal["Short Mold"] = dt.Compute(" Sum([Short Mold]) ", "");
            drTotal["Stain Mark"] = dt.Compute(" Sum([Stain Mark]) ", "");
            drTotal["Weld Line"] = dt.Compute(" Sum([Weld Line]) ", "");
            drTotal["Flashes"] = dt.Compute(" Sum([Flashes]) ", "");
            drTotal["Foreign Materials"] = dt.Compute(" Sum([Foreign Materials]) ", "");
            drTotal["Drag"] = dt.Compute(" Sum([Drag]) ", "");
            drTotal["Material Bleed"] = dt.Compute(" Sum([Material Bleed]) ", "");
            drTotal["Bent"] = dt.Compute(" Sum([Bent]) ", "");
            drTotal["Defrom"] = dt.Compute(" Sum([Defrom]) ", "");
            drTotal["Gas Mark"] = dt.Compute(" Sum([Gas Mark]) ", "");



            dt.Rows.Add(drTotal);

       

            return ds.Tables[0];
        }
        

        public DataTable GetDefectForDailyReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
           return  dal.GetDefectForDailyReport(dDateFrom, dDateTo, sPartNo, sJigNo, sShift);
        }


        public DataTable GetRejTimeDetail(DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineID, string sPartNo, string sDefectCode, string sJigNo)
        {
            return  dal.GetRejTimeDetail(dDateFrom, dDateTo, sShift, sMachineID, sPartNo, sDefectCode, sJigNo);        
        }



        public SqlCommand MaintenanceCommand(string sTrackingID, string sPartNo, string sModel, string sJigNo)
        {
            return dal.MaintenanceCommand(sTrackingID, sPartNo, sModel, sJigNo);
        }
    }
}
