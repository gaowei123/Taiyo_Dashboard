using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class MouldingPqmHistory_BLL
    {
        private Common.Class.DAL.MouldingPqmHistory_DAL dal = new DAL.MouldingPqmHistory_DAL();

        public DataTable GetList(DateTime dDateFrom,DateTime dDateTo, string sMachineID , ref DataTable TempInfo)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID);

         
            if (ds==null|| ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            DataTable dt_TempInfo = new DataTable();

            #region dt Temperature Max Min AVG Setting info
            dt_TempInfo.Columns.Add("LineNo");

            dt_TempInfo.Columns.Add("AVG");
            dt_TempInfo.Columns.Add("Min");
            dt_TempInfo.Columns.Add("Max");
            dt_TempInfo.Columns.Add("Setting");

            DataRow dr_TempInfo11 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo12 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo13 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo14 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo15 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo21 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo22 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo23 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo24 = dt_TempInfo.NewRow();
            DataRow dr_TempInfo25 = dt_TempInfo.NewRow();

            #region  Temp value
            try
            {
                dr_TempInfo11["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature11)", "tempature11 <> 0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo11["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo11["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature11)", "tempature11 <> 0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo11["Min"] = "0℃"; }
            try
            {
                dr_TempInfo11["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature11)", "tempature11 <> 0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo11["Max"] = "0℃"; }
            dr_TempInfo11["Setting"] = dt.Rows[0]["Setting11"].ToString() + "℃";



            try
            {
                dr_TempInfo12["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature12)", "tempature12 <> 0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo12["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo12["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature12)", "tempature12 <>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo12["Min"] = "0℃"; }
            try
            {
                dr_TempInfo12["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature12)", "tempature12 <>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo12["Max"] = "0℃"; }
            dr_TempInfo12["Setting"] = dt.Rows[0]["Setting12"].ToString() + "℃";



            try
            {
                dr_TempInfo13["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature13)", "tempature13<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo13["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo13["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature13)", "tempature13<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo13["Min"] = "0℃"; }
            try
            {
                dr_TempInfo13["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature13)", "tempature13<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo13["Max"] = "0℃"; }
            dr_TempInfo13["Setting"] = dt.Rows[0]["Setting13"].ToString() + "℃";



            try
            {
                dr_TempInfo14["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature14)", "tempature14<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo14["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo14["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature14)", "tempature14<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo14["Min"] = "0℃"; }
            try
            {
                dr_TempInfo14["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature14)", "tempature14<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo14["Max"] = "0℃"; }
            dr_TempInfo14["Setting"] = dt.Rows[0]["Setting14"].ToString() + "℃";



            try
            {
                dr_TempInfo15["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature15)", "tempature15<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo15["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo15["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature15)", "tempature15<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo15["Min"] = "0℃"; }
            try
            {
                dr_TempInfo15["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature15)", "tempature15<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo15["Max"] = "0℃"; }
            dr_TempInfo15["Setting"] = dt.Rows[0]["Setting15"].ToString() + "℃";


            
            try
            {
                dr_TempInfo21["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature21)", "tempature21<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo21["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo21["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature21)", "tempature21<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo21["Min"] = "0℃"; }
            try
            {
                dr_TempInfo21["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature21)", "tempature21<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo21["Max"] = "0℃"; }
            dr_TempInfo21["Setting"] = dt.Rows[0]["Setting21"].ToString() + "℃";



            try
            {
                dr_TempInfo22["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature22)", "tempature22<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo22["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo22["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature22)", "tempature22<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo22["Min"] = "0℃"; }
            try
            {
                dr_TempInfo22["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature22)", "tempature22<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo22["Max"] = "0℃"; }
            dr_TempInfo22["Setting"] = dt.Rows[0]["Setting22"].ToString() + "℃";



            try
            {
                dr_TempInfo23["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature23)", "tempature23<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo23["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo23["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature23)", "tempature23<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo23["Min"] = "0℃"; }
            try
            {
                dr_TempInfo23["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature23)", "tempature23<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo23["Max"] = "0℃"; }
            dr_TempInfo23["Setting"] = dt.Rows[0]["Setting23"].ToString() + "℃";



            try
            {
                dr_TempInfo24["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature24)", "tempature24<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo24["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo24["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature24)", "tempature24<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo24["Min"] = "0℃"; }
            try
            {
                dr_TempInfo24["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature24)", "tempature24<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo24["Max"] = "0℃"; }
            dr_TempInfo24["Setting"] = dt.Rows[0]["Setting24"].ToString() + "℃";



            try
            {
                dr_TempInfo25["AVG"] = Math.Round(double.Parse(dt.Compute("AVG(tempature25)", "tempature25<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo25["AVG"] = "0℃"; }
            try
            {
                dr_TempInfo25["Min"] = Math.Round(double.Parse(dt.Compute("Min(tempature25)", "tempature25<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo25["Min"] = "0℃"; }
            try
            {
                dr_TempInfo25["Max"] = Math.Round(double.Parse(dt.Compute("Max(tempature25)", "tempature25<>0").ToString()), 2).ToString() + "℃";
            }
            catch { dr_TempInfo25["Max"] = "0℃"; }
            dr_TempInfo25["Setting"] = dt.Rows[0]["Setting25"].ToString() + "℃";
            #endregion 


            if (sMachineID == "1" || sMachineID == "6" )
            {
                dr_TempInfo11["LineNo"] = "A NOZ";
                dr_TempInfo12["LineNo"] = "A HEAD";
                dr_TempInfo13["LineNo"] = "A FRONT";
                dr_TempInfo14["LineNo"] = "A MIDDLE";
                dr_TempInfo15["LineNo"] = "A REAR";
                dr_TempInfo21["LineNo"] = "B NOZ";
                dr_TempInfo22["LineNo"] = "B HEAD";
                dr_TempInfo23["LineNo"] = "B FRONT";
                dr_TempInfo24["LineNo"] = "B MIDDLE";
                dr_TempInfo25["LineNo"] = "B REAR";

                dt_TempInfo.Rows.Add(dr_TempInfo11);
                dt_TempInfo.Rows.Add(dr_TempInfo12);
                dt_TempInfo.Rows.Add(dr_TempInfo13);
                dt_TempInfo.Rows.Add(dr_TempInfo14);
                dt_TempInfo.Rows.Add(dr_TempInfo15);
                dt_TempInfo.Rows.Add(dr_TempInfo21);
                dt_TempInfo.Rows.Add(dr_TempInfo22);
                dt_TempInfo.Rows.Add(dr_TempInfo23);
                dt_TempInfo.Rows.Add(dr_TempInfo24);
                dt_TempInfo.Rows.Add(dr_TempInfo25);
            }
            else if (sMachineID == "2" || sMachineID == "3" || sMachineID == "4" || sMachineID == "5")
            {
                dr_TempInfo11["LineNo"] = "A NOZ";
                dr_TempInfo13["LineNo"] = "A FRONT";
                dr_TempInfo14["LineNo"] = "A MIDDLE";
                dr_TempInfo15["LineNo"] = "A REAR";

                dr_TempInfo21["LineNo"] = "B NOZ";
                dr_TempInfo23["LineNo"] = "B FRONT";
                dr_TempInfo24["LineNo"] = "B MIDDLE";
                dr_TempInfo25["LineNo"] = "B REAR";

                dt_TempInfo.Rows.Add(dr_TempInfo11);
                dt_TempInfo.Rows.Add(dr_TempInfo13);
                dt_TempInfo.Rows.Add(dr_TempInfo14);
                dt_TempInfo.Rows.Add(dr_TempInfo15);
        
                dt_TempInfo.Rows.Add(dr_TempInfo21);
                dt_TempInfo.Rows.Add(dr_TempInfo23);
                dt_TempInfo.Rows.Add(dr_TempInfo24);
                dt_TempInfo.Rows.Add(dr_TempInfo25);
            }
            else if (sMachineID == "7" || sMachineID == "8")
            {
                dr_TempInfo11["LineNo"] = "A NOZ";
                dr_TempInfo12["LineNo"] = "A HEAD";
                dr_TempInfo13["LineNo"] = "A FRONT";
                dr_TempInfo14["LineNo"] = "A MIDDLE";
                dr_TempInfo15["LineNo"] = "A REAR";

                dt_TempInfo.Rows.Add(dr_TempInfo11);
                dt_TempInfo.Rows.Add(dr_TempInfo12);
                dt_TempInfo.Rows.Add(dr_TempInfo13);
                dt_TempInfo.Rows.Add(dr_TempInfo14);
                dt_TempInfo.Rows.Add(dr_TempInfo15);
            }
            #endregion

            TempInfo = dt_TempInfo;


            #region reduce dr, too much, web page opening speed to low
            DataTable dt_thin = dt.Clone();

            int i = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (i == 4) // 5条中取一条
                {
                    DataRow dr_forThin = dt_thin.NewRow();
                    dr_forThin.ItemArray = dr.ItemArray;

                    dt_thin.Rows.Add(dr_forThin);
                    i = 0;
                }
                i++;
            }
            #endregion

            #region not use
            //dt_thin.Columns.Add("TempAVG_A");
            //dt_thin.Columns.Add("TempAVG_B");
            //dt_thin.Columns.Add("TempAVG_Total");

            //double TempAVG_A = 0;
            //double TempAVG_B = 0;

            //int LineCount = 0;
            //if (sMachineID == "6" || sMachineID == "7")
            //    LineCount = 5;
            //else
            //    LineCount = 4;

            //foreach (DataRow dr in dt_thin.Rows)
            //{
            //    TempAVG_A += double.Parse(dr["tempature11"].ToString());
            //    TempAVG_A += double.Parse(dr["tempature12"].ToString());
            //    TempAVG_A += double.Parse(dr["tempature13"].ToString());
            //    TempAVG_A += double.Parse(dr["tempature14"].ToString());
            //    TempAVG_A += double.Parse(dr["tempature15"].ToString());
            //    TempAVG_A = Math.Round(TempAVG_A / LineCount, 2);

            //    TempAVG_B += double.Parse(dr["tempature21"].ToString());
            //    TempAVG_B += double.Parse(dr["tempature22"].ToString());
            //    TempAVG_B += double.Parse(dr["tempature23"].ToString());
            //    TempAVG_B += double.Parse(dr["tempature24"].ToString());
            //    TempAVG_B += double.Parse(dr["tempature25"].ToString());
            //    TempAVG_B = Math.Round(TempAVG_B / LineCount, 2);

            //    double TempAVG_Total = Math.Round((TempAVG_A+ TempAVG_B) / LineCount*2 , 2);

            //    dr["TempAVG_A"] = TempAVG_A;
            //    dr["TempAVG_B"] = TempAVG_B;
            //    dr["TempAVG_Total"] = TempAVG_Total;
            //}
            #endregion

            return dt_thin;
        }

    }
}
