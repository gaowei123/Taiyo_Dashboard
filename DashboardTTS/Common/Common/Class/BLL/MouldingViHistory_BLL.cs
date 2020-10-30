using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class MouldingViHistory_BLL
    {
        Common.Class.DAL.MouldingViHistory_DAL dal = new DAL.MouldingViHistory_DAL();

        public MouldingViHistory_BLL()
        { }





        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sMachineID)
        {
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sPartNo, sJigNo, sMachineID);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetTestingList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineID)
        {
            DataSet ds = dal.GetTestingList(dDateFrom, dDateTo, sShift, sMachineID);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID, sPartNo, sShift, sModule);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            #region 
            foreach (DataRow dr in dt.Rows)
            {
                string opSign01 = dr["opSign01"].ToString();
                string opSign02 = dr["opSign02"].ToString();
                string opSign03 = dr["opSign03"].ToString();
                string opSign04 = dr["opSign04"].ToString();
                string opSign05 = dr["opSign05"].ToString();
                string opSign06 = dr["opSign06"].ToString();
                string opSign07 = dr["opSign07"].ToString();
                string opSign08 = dr["opSign08"].ToString();
                string opSign09 = dr["opSign09"].ToString();
                string opSign10 = dr["opSign10"].ToString();
                string opSign11 = dr["opSign11"].ToString();
                string opSign12 = dr["opSign12"].ToString();

                dr["opSign01"] = opSign01 == "" ? "" : opSign01.Split('|')[0];
                dr["opSign02"] = opSign02 == "" ? "" : opSign02.Split('|')[0];
                dr["opSign03"] = opSign03 == "" ? "" : opSign03.Split('|')[0];
                dr["opSign04"] = opSign04 == "" ? "" : opSign04.Split('|')[0];
                dr["opSign05"] = opSign05 == "" ? "" : opSign05.Split('|')[0];
                dr["opSign06"] = opSign06 == "" ? "" : opSign06.Split('|')[0];
                dr["opSign07"] = opSign07 == "" ? "" : opSign07.Split('|')[0];
                dr["opSign08"] = opSign08 == "" ? "" : opSign08.Split('|')[0];
                dr["opSign09"] = opSign09 == "" ? "" : opSign09.Split('|')[0];
                dr["opSign10"] = opSign10 == "" ? "" : opSign10.Split('|')[0];
                dr["opSign11"] = opSign11 == "" ? "" : opSign11.Split('|')[0];
                dr["opSign12"] = opSign12 == "" ? "" : opSign12.Split('|')[0];

                string qaSign01 = dr["qaSign01"].ToString();
                string qaSign02 = dr["qaSign02"].ToString();
                string qaSign03 = dr["qaSign03"].ToString();
                string qaSign04 = dr["qaSign04"].ToString();
                string qaSign05 = dr["qaSign05"].ToString();
                string qaSign06 = dr["qaSign06"].ToString();
                string qaSign07 = dr["qaSign07"].ToString();
                string qaSign08 = dr["qaSign08"].ToString();
                string qaSign09 = dr["qaSign09"].ToString();
                string qaSign10 = dr["qaSign10"].ToString();
                string qaSign11 = dr["qaSign11"].ToString();
                string qaSign12 = dr["qaSign12"].ToString();

                dr["qaSign01"] = qaSign01 == "" ? "" : qaSign01.Split('|')[0];
                dr["qaSign02"] = qaSign02 == "" ? "" : qaSign02.Split('|')[0];
                dr["qaSign03"] = qaSign03 == "" ? "" : qaSign03.Split('|')[0];
                dr["qaSign04"] = qaSign04 == "" ? "" : qaSign04.Split('|')[0];
                dr["qaSign05"] = qaSign05 == "" ? "" : qaSign05.Split('|')[0];
                dr["qaSign06"] = qaSign06 == "" ? "" : qaSign06.Split('|')[0];
                dr["qaSign07"] = qaSign07 == "" ? "" : qaSign07.Split('|')[0];
                dr["qaSign08"] = qaSign08 == "" ? "" : qaSign08.Split('|')[0];
                dr["qaSign09"] = qaSign09 == "" ? "" : qaSign09.Split('|')[0];
                dr["qaSign10"] = qaSign10 == "" ? "" : qaSign10.Split('|')[0];
                dr["qaSign11"] = qaSign11 == "" ? "" : qaSign11.Split('|')[0];
                dr["qaSign12"] = qaSign12 == "" ? "" : qaSign12.Split('|')[0];

                try
                {
                    DateTime Material_MHCheckTime = DateTime.Parse(dr["Material_MHCheckTime"].ToString());
                    dr["Material_MHCheckTime"] = Material_MHCheckTime.ToShortTimeString();
                }
                catch { }
                try
                {
                    DateTime Material_OpCheckTime = DateTime.Parse(dr["Material_OpCheckTime"].ToString());
                    dr["Material_OpCheckTime"] = Material_OpCheckTime.ToShortTimeString();
                }
                catch { }
                try
                {
                    DateTime Material_LeaderCheckTime = DateTime.Parse(dr["Material_LeaderCheckTime"].ToString());
                    dr["Material_LeaderCheckTime"] = Material_LeaderCheckTime.ToShortTimeString();
                }
                catch { }
                try
                {
                    DateTime LeaderCheckTime = DateTime.Parse(dr["LeaderCheckTime"].ToString());
                    dr["LeaderCheckTime"] = LeaderCheckTime.ToShortTimeString();
                }
                catch { }
                try
                {
                    DateTime SupervisorCheckTime = DateTime.Parse(dr["SupervisorCheckTime"].ToString());
                    dr["SupervisorCheckTime"] = SupervisorCheckTime.ToShortTimeString();
                }
                catch { }

            }
            #endregion

            return dt;
        }

        public DataTable getLastVITracking(string sMachineID)
        {
            DataSet ds = dal.getLaserVITracking(sMachineID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            else
            {
                return ds.Tables[0];
            }
   

        }

        public DataTable SelectHourlyCheck(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            DataSet ds = dal.SelectHourlyCheck(dDateFrom, dDateTo, sMachineID, sPartNo, sShift, sModule);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt_temp = ds.Tables[0];
            dt_temp.Columns.Add("ID");

            int i = 1;
            foreach (DataRow  dr in dt_temp.Rows)
            {
                dr["ID"] = i;
                i++;
            }


            return dt_temp;
        }
        

    
        public string SecondToDateTime(int dSeconds)
        {
            if (dSeconds <= 0 )
            {
                return "00:00:00";
            }

            string HH = "";
            string mm = "";
            string ss = "";

            double dHH = dSeconds / 3600;
            HH = ((Int32)dHH).ToString();
            HH = HH.Length == 1 ? "0" + HH : HH;

            double dmm = (dSeconds % 3600) / 60;
            mm = ((Int32)dmm).ToString();
            mm = mm.Length == 1 ? "0" + mm : mm;


            double dss = (dSeconds % 3600) % 60;
            ss = ((Int32)dss).ToString();
            ss = ss.Length == 1 ? "0" + ss : ss;

            string DateTime = HH + ":" + mm + ":" + ss;

            return DateTime;
        }
        // 2018 12 08 modified by wei lijia , add 2 parameter : date not in & except weekend 
        public DataTable ProductionReport_withMQC(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sModule, bool bOnlyMqc)
        {
            DataSet ds = dal.ProductionReport_withMQC(dDateFrom, dDateTo, sMachineID, sPartNo, sModule, bOnlyMqc);
            if (ds==null || ds.Tables.Count ==0)
            {
                return null;
            }
            else
            {
                DataTable dt =  ds.Tables[0];
                if (ds.Tables[0].Rows[0]["MachineID"] != null)
                {
                    DataTable dt_return = new DataTable();

                    int Total_QTY = 0;
                    int MFG_Reject = 0;
                    int QC_NG = 0;
                    int MFG_Pass = 0;

                    int MQC_1st_Reject = 0;

                    int MQC_2nd_Reject = 0;

                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            Total_QTY += int.Parse(dr["TotalQTY"].ToString());
                            MFG_Reject += int.Parse(dr["MFG_Reject"].ToString());
                            QC_NG += int.Parse(dr["IPQC_NG"].ToString());
                            MFG_Pass += int.Parse(dr["MFG_Pass"].ToString());

                            MQC_1st_Reject += int.Parse(dr["MQC_1st_Reject"].ToString());

                            MQC_2nd_Reject += int.Parse(dr["MQC_2nd_Reject"].ToString());
                        }
                        catch { }
                    }

                    DataRow dr_total = dt.NewRow();
                    dr_total["MachineID"] = "Total :";
                    dr_total["TotalQTY"] = Total_QTY;
                    dr_total["MFG_Reject"] = MFG_Reject;
                    dr_total["IPQC_NG"] = QC_NG;
                    dr_total["MFG_Pass"] = MFG_Pass;
                    dr_total["MQC_1st_Reject"] = MQC_1st_Reject;
                    dr_total["MQC_2nd_Reject"] = MQC_2nd_Reject;
                    dr_total["MQC_1st_RejRate"] = Math.Round((double.Parse(MQC_1st_Reject.ToString())) * 100.00 / double.Parse(MFG_Pass.ToString()), 2).ToString() + "%";
                    dr_total["MQC_2nd_RejRate"] = Math.Round((double.Parse(MQC_2nd_Reject.ToString())) * 100.00 / double.Parse(MFG_Pass.ToString()), 2).ToString() + "%";
                    dr_total["MFG_RejRate"] = Math.Round((double.Parse(QC_NG.ToString()) + double.Parse(MFG_Reject.ToString())) * 100.00 / double.Parse(Total_QTY.ToString()), 2).ToString() + "%";
                    dr_total["MQC_RejRate"] = Math.Round((double.Parse(MQC_1st_Reject.ToString()) + double.Parse(MQC_2nd_Reject.ToString())) * 100.00 / double.Parse(MFG_Pass.ToString()), 2).ToString() + "%";
                    dr_total["Total_RejRate"] = Math.Round((double.Parse(MQC_1st_Reject.ToString()) + double.Parse(MQC_2nd_Reject.ToString()) + double.Parse(QC_NG.ToString()) + double.Parse(MFG_Reject.ToString())) * 100.00 / double.Parse(Total_QTY.ToString()), 2).ToString() + "%";
                    dr_total["MFG_Pass"] = MFG_Pass;
                    dt.Rows.Add(dr_total);
                }
                return dt;
            }
           
        }
        public DataTable ProductionReport_withMQCDetail(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sModule, bool bOnlyMqc,string sCheckerID)
        {
            DataSet ds = dal.ProductionReport_withMQCDetail(dDateFrom, dDateTo, sMachineID, sPartNo, sModule, bOnlyMqc, sCheckerID);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                //if (ds.Tables[0].Rows[0]["MachineID"] != null)
                //{
                //    DataTable dt_return = new DataTable();

                //    int Total_QTY = 0;
                //    int MFG_Reject = 0;
                //    int QC_NG = 0;
                //    int MFG_Pass = 0;

                //    int MQC_1st_Reject = 0;

                //    int MQC_2nd_Reject = 0;

                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        try
                //        {
                //            Total_QTY += int.Parse(dr["TotalQTY"].ToString());
                //            MFG_Reject += int.Parse(dr["MFG_Reject"].ToString());
                //            QC_NG += int.Parse(dr["QC_NG"].ToString());
                //            MFG_Pass += int.Parse(dr["MFG_Pass"].ToString());

                //            MQC_1st_Reject += int.Parse(dr["MQC_1st_Reject"].ToString());

                //            MQC_2nd_Reject += int.Parse(dr["MQC_2nd_Reject"].ToString());
                //        }
                //        catch { }
                //    }

                //    DataRow dr_total = dt.NewRow();
                //    dr_total["MachineID"] = "Total :";
                //    dr_total["TotalQTY"] = Total_QTY;
                //    dr_total["MFG_Reject"] = MFG_Reject;
                //    dr_total["QC_NG"] = QC_NG;
                //    dr_total["MFG_Pass"] = MFG_Pass;
                //    dr_total["MQC_1st_Reject"] = MQC_1st_Reject;
                //    dr_total["MQC_2nd_Reject"] = MQC_2nd_Reject;
                //    dr_total["MFG_RejRate"] = Math.Round((double.Parse(QC_NG.ToString()) + double.Parse(MFG_Reject.ToString())) * 100.00 / double.Parse(Total_QTY.ToString()), 2).ToString() + "%";
                //    dr_total["MQC_RejRate"] = Math.Round((double.Parse(MQC_1st_Reject.ToString()) + double.Parse(MQC_2nd_Reject.ToString())) * 100.00 / double.Parse(MFG_Pass.ToString()), 2).ToString() + "%";
                //    dr_total["Total_RejRate"] = Math.Round((double.Parse(MQC_1st_Reject.ToString()) + double.Parse(MQC_2nd_Reject.ToString()) + double.Parse(QC_NG.ToString()) + double.Parse(MFG_Reject.ToString())) * 100.00 / double.Parse(Total_QTY.ToString()), 2).ToString() + "%";
                //    dr_total["MFG_Pass"] = MFG_Pass;
                //    dt.Rows.Add(dr_total);
                //}
                return dt;
            }

        }


        public DataTable ProductionReport(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
          

            DataSet ds = dal.GetProductionList( dDateFrom,  dDateTo,  sMachineID,  sPartNo, sShift, sModule);
        
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;


            DataTable dt = ds.Tables[0];


            DataTable dtMerged = new DataTable();
            dtMerged = dt.Clone();

       
            if (sShift != "")
            {
                dtMerged = dt;
            }
            else
            {
                #region merge day night 

              
                foreach (DataRow dr in dt.Rows)
                {
               
                    string Day = dr["Day"].ToString();
                    string MachineID = dr["MachineID"].ToString();
                    string PartNumber = dr["PartNumber"].ToString();
                    string Status = dr["Status"].ToString();


                    DataRow[] RowArr = dt.Select("Day='" + Day + "' and MachineID='" + MachineID + "' and PartNumber='" + PartNumber + "' and Status = '" + Status + "'");
                    if (RowArr.Length > 1)
                    {
                        #region day, nigth 做同样的任务的, 进行合并
                        double OK = 0;
                        double NG = 0;
                        double QCNGQTY = 0;
                        double OutputPCS = 0;
                        double OutputShot = 0;
                      
                        double RunningTime = 0;
                        double Accumulate = 0;
                        double MaxAccumulate = 0;
                        double TotalAccumulate = 0;

                        double AdjustScrap = 0;


                        DataRow drNew = dt.NewRow();
                        drNew.ItemArray = RowArr[0].ItemArray;


                     

                        foreach (DataRow x in RowArr)
                        {

                        

                            DBHelp.Reports.LogFile.Log("ProductionReport_debug", string.Format("ok:{0}, ng:{1}, qcngqty:{2}, accumulate:{3}, output: {4}, time:{5}", x["OK"].ToString(), x["NG"].ToString(), x["QCNGQTY"].ToString(), x["Accumulate"].ToString(), x["Output"].ToString(), x["Time"].ToString()));


                            OK += double.Parse(x["OK"].ToString());
                            NG += double.Parse(x["NG"].ToString());
                            QCNGQTY += double.Parse(x["QCNGQTY"].ToString());
                            Accumulate += double.Parse(x["Accumulate"].ToString());
                            
                            string sOutput = x["Output"].ToString();
                            OutputPCS += double.Parse(sOutput.Split('(')[0]);
                            OutputShot += double.Parse(sOutput.Split('(')[1].Trim(')'));
                            try
                            {
                                RunningTime += double.Parse(x["Time"].ToString());
                            } catch {}
                            

                            

                            if (Accumulate > MaxAccumulate)
                            {
                                if (TotalAccumulate < Accumulate)
                                    TotalAccumulate = Accumulate;
                                Accumulate = 0;
                            }
                            else
                            {
                                if (TotalAccumulate < MaxAccumulate)
                                    TotalAccumulate = MaxAccumulate;
                                Accumulate = 0;
                            }

                            AdjustScrap += double.Parse(x["AdjustScrap"].ToString());

                        }

                        drNew["Shift"] = StaticRes.Global.Shift.ALL;
                        drNew["OK"] = OK;
                        drNew["NG"] = NG;
                        drNew["RejRate"] = Math.Round(NG / OutputPCS * 100, 2).ToString("0.00") + "%";
                        drNew["Time"] = RunningTime;
                        drNew["QCNGQTY"] = QCNGQTY;
                        drNew["Output"] = string.Format("{0}({1})", OutputPCS, OutputShot);
                        drNew["Accumulate"] = TotalAccumulate;
                        drNew["NeedProductionTime"] = Common.CommFunctions.ConvertDateTimeShort(drNew["NeedProductionTime"].ToString().ToString() + "H");
                        drNew["AdjustScrap"] = AdjustScrap.ToString("0");

                        dtMerged.Rows.Add(drNew.ItemArray);
                        #endregion
                    }
                    else
                    {
                        //直接添加该行
                        dr["NeedProductionTime"] = Common.CommFunctions.ConvertDateTimeShort(dr["NeedProductionTime"].ToString().ToString() + "H");
                        dr["Shift"] = StaticRes.Global.Shift.ALL;
                        dtMerged.Rows.Add(dr.ItemArray);
                    }
                }
              


                //distinct
                DataView dataView = dtMerged.DefaultView;
                DataTable DataTableDistinct = dataView.ToTable(true, "Day", "Shift", "MachineID", "Model", "Type", "PartNumberAll", "PartNumber", "JigNo", "Status", "TargetQty", "Accumulate", "Output",  "OK", "NG", "QCNGQTY", "RejRate",  "Time", "NeedProductionTime", "AdjustScrap");
                //distinct

                //Add column ID
                DataTableDistinct.Columns.Add("ID");
                int ID = 0;
                foreach (DataRow dr in DataTableDistinct.Rows)
                {
                    ID++;
                    dr["ID"] = ID;
                }
                //Add column ID

                dtMerged = DataTableDistinct;
                #endregion
            }


            #region add dr total 
            double Total_OK = 0;
            double Total_NG = 0;
            double Total_OutputPerPCS = 0;
            double Total_OutputPerShot = 0;
            double Total_Accumulate = 0;
            double Total_TargetQty = 0;
            double Total_QCNG = 0;
            int Total_Time = 0;
            double Total_AdjustScrap = 0;
    

            foreach (DataRow dr in dtMerged.Rows)
            {
                Total_OK += double.Parse(dr["OK"].ToString());
                Total_NG += double.Parse(dr["NG"].ToString());
                Total_OutputPerPCS += double.Parse(dr["Output"].ToString().Split('(')[0]);
                Total_OutputPerShot += double.Parse(dr["Output"].ToString().Split('(')[1].Trim(')'));
                Total_Accumulate += double.Parse(dr["Accumulate"].ToString());
                //Total_TargetQty += double.Parse(dr["TargetQty"].ToString());
                Total_QCNG += double.Parse(dr["QCNGQTY"].ToString());

                Total_AdjustScrap += double.Parse(dr["AdjustScrap"].ToString());



                try
                {
                    Total_Time += int.Parse(dr["Time"].ToString());
                    string QAQ = SecondToDateTime(int.Parse(dr["Time"].ToString()));
                    dr["Time"] = SecondToDateTime(int.Parse(dr["Time"].ToString()));
                }
                catch  {  }
            }

            DataRow dr_toal = dtMerged.NewRow();
            dr_toal["Day"] = "Total :";
            dr_toal["OK"] = Total_OK;
            dr_toal["NG"] = Total_NG;
            dr_toal["QCNGQTY"] = Total_QCNG;
            dr_toal["Output"] = string.Format("{0}({1})", Total_OutputPerPCS, Total_OutputPerShot);
            dr_toal["Accumulate"] = DBNull.Value; //Total_Accumulate;
            dr_toal["RejRate"] = Math.Round( Total_NG/ Total_OutputPerPCS * 100,2).ToString("0.00") + "%";
            dr_toal["Time"] = SecondToDateTime(Total_Time);
            dr_toal["AdjustScrap"] = Total_AdjustScrap.ToString("0");


            dtMerged.Rows.Add(dr_toal);
            #endregion 

            return dtMerged;
        }

        public DataTable UpdateProductionReport(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            DataTable dt = SelectList(dDateFrom, dDateTo, sMachineID, sPartNo, sShift, sModule);

            DataTable dt_return = new DataTable();


            if (sShift != "")
            {
                dt_return = dt;
            }
            else
            {
                //distinct
                DataView dataView = dt.DefaultView;
                DataTable DataTableDistinct = dataView.ToTable(true, "Day", "Shift", "MachineID", "Model", "PartNumberAll", "PartNumber", "JigNo", "StartTime", "OK", "NG", "QCNGQTY", "Output", "Time", "OutputPerPCS", "OutputPerSet", "TargetQty", "RejRate", "Accumulate", "Status", "cavityCount", "Type", "Setup", "NeedProductionTime");
                //distinct

                //Add column ID
                DataTableDistinct.Columns.Add("ID");
                int ID = 0;
                foreach (DataRow dr in DataTableDistinct.Rows)
                {
                    ID++;
                    dr["ID"] = ID;
                }
                //Add column ID

                dt_return = DataTableDistinct;
            }
            return dt_return;
        }

      
        public SqlCommand UpdateCommond(Common.Class.Model.MouldingViHistory_Model model)
        {
            return dal.UpdateSetupWasteMaterialCommond(model);
        }

        public SqlCommand UpdateProduction(Common.Class.Model.MouldingViHistory_Model model)
        {
            return dal.UpdateProduction(model);
        }

        public SqlCommand DeleteProduction(Common.Class.Model.MouldingViHistory_Model model)
        {
            return dal.DeleteProduction(model);
        }

        public SqlCommand InsertProductionHistory(Common.Class.Model.MouldingViHistory_Model model)
        {
            return dal.InsertProductionHistory(model);
        }

        public Common.Class.Model.MouldingViHistory_Model GetModel_ByDayShiftAllPartMachine(Common.Class.Model.MouldingViHistory_Model model)
        {
            Common.Class.Model.MouldingViHistory_Model objVi = new Common.Class.Model.MouldingViHistory_Model();
            DataSet ds = dal.GetModel_ByDayShiftAllPartMachine(model);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                objVi = null;
            }
            else
            {
                objVi = DataTableToList(ds.Tables[0])[0];
            }
            return objVi;
        }
        public List<Common.Class.Model.MouldingViHistory_Model> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.MouldingViHistory_Model> modelList = new List<Common.Class.Model.MouldingViHistory_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.MouldingViHistory_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.MouldingViHistory_Model();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.MachineID = dt.Rows[n]["machineID"].ToString();
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.Datetime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.PartNumber = dt.Rows[n]["partNumber"].ToString();
                    model.JigNo = dt.Rows[n]["jigNo"].ToString();
                    model.Model = dt.Rows[n]["model"].ToString();
                    if (dt.Rows[n]["cavityCount"].ToString() != "")
                    {
                        model.CavityCount = double.Parse(dt.Rows[n]["cavityCount"].ToString());
                    }
                    if (dt.Rows[n]["partsWeight"].ToString() != "")
                    {
                        model.PartsWeight = double.Parse(dt.Rows[n]["partsWeight"].ToString());
                    }
                    if (dt.Rows[n]["cycleTime"].ToString() != "")
                    {
                        model.CycleTime = double.Parse(dt.Rows[n]["cycleTime"].ToString());
                    }
                    if (dt.Rows[n]["targetQty"].ToString() != "")
                    {
                        model.TargetQty = double.Parse(dt.Rows[n]["targetQty"].ToString());
                    }
                    model.UserName = dt.Rows[n]["userName"].ToString();
                    model.UserID = dt.Rows[n]["userID"].ToString();
                    if (dt.Rows[n]["acountReading"].ToString() != "")
                    {
                        model.AcountReading = double.Parse(dt.Rows[n]["acountReading"].ToString());
                    }
                    if (dt.Rows[n]["rejectQty"].ToString() != "")
                    {
                        model.RejectQty = double.Parse(dt.Rows[n]["rejectQty"].ToString());
                    }
                    if (dt.Rows[n]["QCNGQTY"].ToString() != "")
                    {
                        model.QCNGQTY = double.Parse(dt.Rows[n]["QCNGQTY"].ToString());
                    }
                    if (dt.Rows[n]["acceptQty"].ToString() != "")
                    {
                        model.AcceptQty = double.Parse(dt.Rows[n]["acceptQty"].ToString());
                    }
                    if (dt.Rows[n]["startTime"].ToString() != "")
                    {
                        model.StartTime = DateTime.Parse(dt.Rows[n]["startTime"].ToString());
                    }
                    if (dt.Rows[n]["stopTime"].ToString() != "")
                    {
                        model.StopTime = DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
                    }
                    if (dt.Rows[n]["day"].ToString() != "")
                    {
                        model.Day = dt.Rows[n]["day"].ToString();
                    }
                    model.Shift = dt.Rows[n]["shift"].ToString();
                    model.Status = dt.Rows[n]["status"].ToString();
                    model.MatPart01 = dt.Rows[n]["matPart01"].ToString();
                    model.MatPart02 = dt.Rows[n]["matPart02"].ToString();
                    model.MatLot01 = dt.Rows[n]["matLot01"].ToString();
                    model.MatLot02 = dt.Rows[n]["matLot02"].ToString();
                    model.OpSign01 = dt.Rows[n]["opSign01"].ToString();
                    model.OpSign02 = dt.Rows[n]["opSign02"].ToString();
                    model.OpSign03 = dt.Rows[n]["opSign03"].ToString();
                    model.OpSign04 = dt.Rows[n]["opSign04"].ToString();
                    model.OpSign05 = dt.Rows[n]["opSign05"].ToString();
                    model.OpSign06 = dt.Rows[n]["opSign06"].ToString();
                    model.OpSign07 = dt.Rows[n]["opSign07"].ToString();
                    model.OpSign08 = dt.Rows[n]["opSign08"].ToString();
                    model.OpSign09 = dt.Rows[n]["opSign09"].ToString();
                    model.OpSign10 = dt.Rows[n]["opSign10"].ToString();
                    model.OpSign11 = dt.Rows[n]["opSign11"].ToString();
                    model.OpSign12 = dt.Rows[n]["opSign12"].ToString();
                    model.QaSign01 = dt.Rows[n]["qaSign01"].ToString();
                    model.QaSign02 = dt.Rows[n]["qaSign02"].ToString();
                    model.QaSign03 = dt.Rows[n]["qaSign03"].ToString();
                    model.QaSign04 = dt.Rows[n]["qaSign04"].ToString();
                    model.QaSign05 = dt.Rows[n]["qaSign05"].ToString();
                    model.QaSign06 = dt.Rows[n]["qaSign06"].ToString();
                    model.QaSign07 = dt.Rows[n]["qaSign07"].ToString();
                    model.QaSign08 = dt.Rows[n]["qaSign08"].ToString();
                    model.QaSign09 = dt.Rows[n]["qaSign09"].ToString();
                    model.QaSign10 = dt.Rows[n]["qaSign10"].ToString();
                    model.QaSign11 = dt.Rows[n]["qaSign11"].ToString();
                    model.QaSign12 = dt.Rows[n]["qaSign12"].ToString();
                    model.Customer = dt.Rows[n]["customer"].ToString();
                    if (dt.Rows[n]["lastUpdatedTime"].ToString() != "")
                    {
                        model.LastUpdatedTime = DateTime.Parse(dt.Rows[n]["lastUpdatedTime"].ToString());
                    }
                    model.TrackingID = dt.Rows[n]["trackingID"].ToString();
                    model.Remarks = dt.Rows[n]["remarks"].ToString();
                    model.Material_MHCheck = dt.Rows[n]["Material_MHCheck"].ToString();
                    if (dt.Rows[n]["Material_MHCheckTime"].ToString() != "")
                    {
                        model.Material_MHCheckTime = DateTime.Parse(dt.Rows[n]["Material_MHCheckTime"].ToString());
                    }
                    model.Material_Opcheck = dt.Rows[n]["Material_Opcheck"].ToString();
                    if (dt.Rows[n]["Material_OpCheckTime"].ToString() != "")
                    {
                        model.Material_OpCheckTime = DateTime.Parse(dt.Rows[n]["Material_OpCheckTime"].ToString());
                    }
                    model.Material_LeaderCheck = dt.Rows[n]["Material_LeaderCheck"].ToString();
                    if (dt.Rows[n]["Material_LeaderCheckTime"].ToString() != "")
                    {
                        model.Material_LeaderCheckTime = DateTime.Parse(dt.Rows[n]["Material_LeaderCheckTime"].ToString());
                    }
                    model.LeaderCheck = dt.Rows[n]["LeaderCheck"].ToString();
                    if (dt.Rows[n]["LeaderCheckTime"].ToString() != "")
                    {
                        model.LeaderCheckTime = DateTime.Parse(dt.Rows[n]["LeaderCheckTime"].ToString());
                    }
                    model.SupervisorCheck = dt.Rows[n]["SupervisorCheck"].ToString();
                    if (dt.Rows[n]["SupervisorCheckTime"].ToString() != "")
                    {
                        model.SupervisorCheckTime = DateTime.Parse(dt.Rows[n]["SupervisorCheckTime"].ToString());
                    }
                    model.PartNumberAll = dt.Rows[n]["partNumberAll"].ToString();
                    if (dt.Rows[n]["parts2Weight"].ToString() != "")
                    {
                        model.Parts2Weight = double.Parse(dt.Rows[n]["parts2Weight"].ToString());
                    }
                    if (dt.Rows[n]["lastQty"].ToString() != "")
                    {
                        model.lastQty = double.Parse(dt.Rows[n]["lastQty"].ToString());
                    }
                    model.OkAccumulation = dt.Rows[n]["OkAccumulation"].ToString();
                    model.refField01 = dt.Rows[n]["refField01"].ToString();
                    model.refField02 = dt.Rows[n]["refField02"].ToString();
                    model.refField03 = dt.Rows[n]["refField03"].ToString();
                    model.refField04 = dt.Rows[n]["refField04"].ToString();
                    model.refField05 = dt.Rows[n]["refField05"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public Common.Class.Model.MouldingViHistory_Model DatarowToModel(DataRow dr)
        {
            Common.Class.Model.MouldingViHistory_Model model = new Model.MouldingViHistory_Model();
            
            if (dr["id"].ToString() != "")
            {
                model.ID = int.Parse(dr["id"].ToString());
            }
            model.MachineID = dr["machineID"].ToString();
            if (dr["dateTime"].ToString() != "")
            {
                model.Datetime = DateTime.Parse(dr["dateTime"].ToString());
            }
            model.PartNumber = dr["partNumber"].ToString();
            model.JigNo = dr["jigNo"].ToString();
            model.Model = dr["model"].ToString();
            if (dr["cavityCount"].ToString() != "")
            {
                model.CavityCount = double.Parse(dr["cavityCount"].ToString());
            }
            if (dr["partsWeight"].ToString() != "")
            {
                model.PartsWeight = double.Parse(dr["partsWeight"].ToString());
            }
            if (dr["parts2Weight"].ToString() != "")
            {
                model.Parts2Weight = double.Parse(dr["parts2Weight"].ToString());
            }
            if (dr["lastQty"].ToString() != "")
            {
                model.lastQty = double.Parse(dr["lastQty"].ToString());
            }
            if (dr["cycleTime"].ToString() != "")
            {
                model.CycleTime = double.Parse(dr["cycleTime"].ToString());
            }
            if (dr["targetQty"].ToString() != "")
            {
                model.TargetQty = double.Parse(dr["targetQty"].ToString());
            }
            model.UserName = dr["userName"].ToString();
            model.UserID = dr["userID"].ToString();
            if (dr["acountReading"].ToString() != "")
            {
                model.AcountReading = double.Parse(dr["acountReading"].ToString());
            }
            if (dr["rejectQty"].ToString() != "")
            {
                model.RejectQty = double.Parse(dr["rejectQty"].ToString());
            }
            if (dr["QCNGQTY"].ToString() != "")
            {
                model.QCNGQTY = double.Parse(dr["QCNGQTY"].ToString());
            }
            if (dr["acceptQty"].ToString() != "")
            {
                model.AcceptQty = double.Parse(dr["acceptQty"].ToString());
            }
            if (dr["startTime"].ToString() != "")
            {
                model.StartTime = DateTime.Parse(dr["startTime"].ToString());
            }
            if (dr["stopTime"].ToString() != "")
            {
                model.StopTime = DateTime.Parse(dr["stopTime"].ToString());
            }
            if (dr["day"].ToString() != "")
            {
                model.Day = dr["day"].ToString();
            }
            model.Shift = dr["shift"].ToString();
            model.Status = dr["status"].ToString();
            model.MatPart01 = dr["matPart01"].ToString();
            model.MatPart02 = dr["matPart02"].ToString();
            model.MatLot01 = dr["matLot01"].ToString();
            model.MatLot02 = dr["matLot02"].ToString();
            model.OpSign01 = dr["opSign01"].ToString();
            model.OpSign02 = dr["opSign02"].ToString();
            model.OpSign03 = dr["opSign03"].ToString();
            model.OpSign04 = dr["opSign04"].ToString();
            model.OpSign05 = dr["opSign05"].ToString();
            model.OpSign06 = dr["opSign06"].ToString();
            model.OpSign07 = dr["opSign07"].ToString();
            model.OpSign08 = dr["opSign08"].ToString();
            model.OpSign09 = dr["opSign09"].ToString();
            model.OpSign10 = dr["opSign10"].ToString();
            model.OpSign11 = dr["opSign11"].ToString();
            model.OpSign12 = dr["opSign12"].ToString();
            model.QaSign01 = dr["qaSign01"].ToString();
            model.QaSign02 = dr["qaSign02"].ToString();
            model.QaSign03 = dr["qaSign03"].ToString();
            model.QaSign04 = dr["qaSign04"].ToString();
            model.QaSign05 = dr["qaSign05"].ToString();
            model.QaSign06 = dr["qaSign06"].ToString();
            model.QaSign07 = dr["qaSign07"].ToString();
            model.QaSign08 = dr["qaSign08"].ToString();
            model.QaSign09 = dr["qaSign09"].ToString();
            model.QaSign10 = dr["qaSign10"].ToString();
            model.QaSign11 = dr["qaSign11"].ToString();
            model.QaSign12 = dr["qaSign12"].ToString();
            model.Customer = dr["customer"].ToString();
            if (dr["lastUpdatedTime"].ToString() != "")
            {
                model.LastUpdatedTime = DateTime.Parse(dr["lastUpdatedTime"].ToString());
            }
            model.TrackingID = dr["trackingID"].ToString();
            model.Remarks = dr["remarks"].ToString();
            model.Material_MHCheck = dr["Material_MHCheck"].ToString();
            if (dr["Material_MHCheckTime"].ToString() != "")
            {
                model.Material_MHCheckTime = DateTime.Parse(dr["Material_MHCheckTime"].ToString());
            }
            model.Material_Opcheck = dr["Material_Opcheck"].ToString();
            if (dr["Material_OpCheckTime"].ToString() != "")
            {
                model.Material_OpCheckTime = DateTime.Parse(dr["Material_OpCheckTime"].ToString());
            }
            model.Material_LeaderCheck = dr["Material_LeaderCheck"].ToString();
            if (dr["Material_LeaderCheckTime"].ToString() != "")
            {
                model.Material_LeaderCheckTime = DateTime.Parse(dr["Material_LeaderCheckTime"].ToString());
            }
            model.LeaderCheck = dr["LeaderCheck"].ToString();
            if (dr["LeaderCheckTime"].ToString() != "")
            {
                model.LeaderCheckTime = DateTime.Parse(dr["LeaderCheckTime"].ToString());
            }
            model.SupervisorCheck = dr["SupervisorCheck"].ToString();
            if (dr["SupervisorCheckTime"].ToString() != "")
            {
                model.SupervisorCheckTime = DateTime.Parse(dr["SupervisorCheckTime"].ToString());
            }
            model.PartNumberAll = dr["partNumberAll"].ToString();
            if (dr["Setup"].ToString() != "")
            {
                model.Setup = double.Parse(dr["Setup"].ToString());
            }
            if (dr["WastageMaterial01"].ToString() != "")
            {
                model.WastageMaterial01 = double.Parse(dr["WastageMaterial01"].ToString());
            }
            if (dr["WastageMaterial02"].ToString() != "")
            {
                model.WastageMaterial02 = double.Parse(dr["WastageMaterial02"].ToString());
            }
            model.OkAccumulation = dr["OkAccumulation"].ToString();
            model.refField01 = dr["refField01"].ToString();
            model.refField02 = dr["refField02"].ToString();
            model.refField03 = dr["refField03"].ToString();
            model.refField04 = dr["refField04"].ToString();
            model.refField05 = dr["refField05"].ToString();
           


            return model;
        }


        public DataTable getProductivityReportForMoulding(DateTime dDay, string sShift, string sDepartment)
        {

            DataTable dtProduct = dal.getProductivityReportForMoulding(dDay, sShift);

            
            #region attendance 
            Common.BLL.LMMSWatchLog_BLL lmmswatchLogBLL = new Common.BLL.LMMSWatchLog_BLL();

            DataTable dt = lmmswatchLogBLL.getAttendance(dDay, sShift, sDepartment);

            double ManPower = 0;// double.Parse( dt.Rows[0]["Man Power"].ToString());
            double Attendance = 0;
            string AttendRate = "";

            if (dt != null && dt.Rows.Count != 0)
            {
                ManPower = double.Parse(dt.Rows[0]["Man Power"].ToString());
                Attendance = double.Parse(dt.Rows[0]["Attendance"].ToString());
                AttendRate = ManPower == 0 ? "0.00%" : Math.Round(Attendance / ManPower * 100, 2).ToString() + "%";
            }
            #endregion


            #region foreach for total Row
            double Total_ActualTime = 0;
            double Total_ActualQty = 0;//实际良品
            double Total_TargetQty = 0;//理论总量
            double Total_RejQty = 0;
            double Total_BuyoffQty = 0;
            double Total_PCS = 0;

            foreach (DataRow dr in dtProduct.Rows)
            {
                if (dr["ActualHR"].ToString() != "")
                    Total_ActualTime += double.Parse(dr["ActualHR"].ToString()) * 3600;

                if (dr["ActualQty"].ToString() != "")
                    Total_ActualQty += double.Parse(dr["ActualQty"].ToString());

                if (dr["TargetQty"].ToString() != "")
                    Total_TargetQty += double.Parse(dr["TargetQty"].ToString());

                if (dr["RejectQty"].ToString() != "")
                    Total_RejQty += double.Parse(dr["RejectQty"].ToString());

                if (dr["BuyoffQty"].ToString() != "")
                    Total_BuyoffQty += double.Parse(dr["BuyoffQty"].ToString());

                if (dr["TotalPCS"].ToString() != "")
                    Total_PCS += double.Parse(dr["TotalPCS"].ToString());
                
            }

            //Rejection Rate
            double TotalRejRate = Total_ActualQty == 0 ? 0.00 : Math.Round(Total_RejQty / Total_PCS * 100, 2);

            //Utilization
            double dUtilization = Math.Round(Total_ActualTime / 3600 / 96 * 100, 2);
            dUtilization = dUtilization > 100 ? 100 : dUtilization;
            string Utilization = Total_ActualTime  == 0 ? "0.00%" : dUtilization.ToString() + "%";



            //dr total 
            DataRow dr_total = dtProduct.NewRow();
            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";
            dr_total["ActualQty"] = Total_ActualQty;
            dr_total["RejectQty"] = Total_RejQty;
            dr_total["TotalQty"] = Total_ActualQty + Total_RejQty;
            dr_total["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort( Math.Round(Total_ActualTime / 3600, 2).ToString());
            dr_total["TargetQty"] = Total_TargetQty;
            dr_total["TargetHR"] = Common.CommFunctions.ConvertDateTimeShort((12 * 8).ToString());

            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;

            dr_total["BuyoffQty"] = Total_BuyoffQty;
            dr_total["Utilization"] = Utilization;
            dr_total["RejRate"] = TotalRejRate == 0 ? "0.00%" : TotalRejRate.ToString() + "%";

            
            dtProduct.Rows.Add(dr_total);

            #endregion


            #region add all type
            List<string> listType = new List<string>();
            listType.Add(StaticRes.Global.ProductType.BUTTON);
            listType.Add(StaticRes.Global.ProductType.Knob);
            listType.Add(StaticRes.Global.ProductType.LEN);
            listType.Add(StaticRes.Global.ProductType.PANEL);
            listType.Add(StaticRes.Global.ProductType.Mould_Test);



            foreach (string type in listType)
            {
                DataRow[] drArr = dtProduct.Select(" ProductType = '" + type + "'");
                if (drArr.Length == 0)
                {
                    DataRow dr = dtProduct.NewRow();
                    dr["SN"] = GetSN(type);
                    dr["ProductType"] = type;
                    dr["ManPower"] = ManPower;
                    dr["Attendance"] = Attendance;
                    dr["AttendRate"] = AttendRate;
                    dr["TargetQty"] = 0;
                    dr["ActualQty"] = 0;
                    dr["RejectQty"] = 0;
                    dr["TotalQty"] = 0;
                    dr["ActualHR"] = "00:00:00";
                    dr["TargetHR"] = Common.CommFunctions.ConvertDateTimeShort((12 * 8).ToString());
                    dr["BuyoffQty"] = 0;
                    dr["RejRate"] = "0.00%";
                    dr["Utilization"] = Utilization;

                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {
                    drArr[0]["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["ActualHR"].ToString());
                    drArr[0]["TargetHR"] = drArr[0]["TargetHR"].ToString();

                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;
                    drArr[0]["Utilization"] = Utilization;
                }
            }
            #endregion


            //order by sn
            dtProduct.Columns["SN"].DataType = typeof(int);
            dtProduct = dtProduct.Select("", "SN asc").CopyToDataTable();


            return dtProduct;
        }


        private int GetSN(string type)
        {
            int SN = 0;
            switch (type)
            {
                case StaticRes.Global.ProductType.BUTTON:
                    SN = 1;
                    break;
                case StaticRes.Global.ProductType.Knob:
                    SN = 2;
                    break;
                case StaticRes.Global.ProductType.LENS:
                    SN = 3;
                    break;
                case StaticRes.Global.ProductType.LEN:
                    SN = 3;
                    break;
                case StaticRes.Global.ProductType.PANEL:
                    SN = 4;
                    break;
                case StaticRes.Global.ProductType.Mould_Test:
                    SN = 7;
                    break;
                case "Day Shift Total":
                    SN = 99;
                    break;
                case "Night Shift Total":
                    SN = 99;
                    break;
            }
            return SN;
        }


     
        public DataTable GetSummaryReport(DateTime dfrom,DateTime dto,string sType)
        {
            DataTable dtProduct = dal.GetSummaryReport(dfrom, dto, sType);

            if (dtProduct == null || dtProduct.Rows.Count == 0)
            {
                return null;
            }

            double totalPartsProduce = 0;
            double totalOK = 0;
            double totalShots = 0;
            double totalAmout = 0;
            double totalRejQty = 0;
            double totalRejCost = 0;
          

            foreach (DataRow dr in dtProduct.Rows)
            {
                totalPartsProduce += double.Parse(dr["Total_Parts_Produce"].ToString());
                totalOK += double.Parse(dr["OK_QTY"].ToString());
                totalAmout += double.Parse(dr["Amount_SGD"].ToString());
                totalRejQty += double.Parse(dr["Reject_Qty"].ToString());
                totalRejCost += double.Parse(dr["Reject_Cost_SGD"].ToString());


                int cavityCount = int.Parse(dr["cavityCount"].ToString());
                if (cavityCount != 0)
                {
                    double shots = double.Parse(dr["Total_Parts_Produce"].ToString()) / cavityCount;
                    totalShots += shots;

                    if (cavityCount != 1)
                    {
                        dr["Total_Parts_Produce"] = dr["Total_Parts_Produce"].ToString() + "(" + Math.Round(shots, 0).ToString() + ")";
                    }
                }
            }

        
            DataRow drTotal = dtProduct.NewRow();
            drTotal["Parts_No"] = "Grand Total:";
            drTotal["Type"] = "";
            drTotal["Total_Parts_Produce"] = totalPartsProduce.ToString() + "(" + Math.Round( totalShots,0).ToString() + ")";
            drTotal["OK_QTY"] = totalOK;
            drTotal["Amount_SGD"] = totalAmout;
            drTotal["Reject_Qty"] = totalRejQty;
            drTotal["Reject_Cost_SGD"] = totalRejCost;
            dtProduct.Rows.Add(drTotal);

            return dtProduct;
        }

        public DataTable GetMonthlyReport(int iYear, string sType)
        {
            DataTable dtProduct = dal.GetMonthlyReport(iYear, sType);
            if (dtProduct == null || dtProduct.Rows.Count == 0)
            {
                return null;
            }
           
           
            double allButton = 0.0;
            double allKnob = 0.0;
            double allLen = 0.0;
            double allPanel = 0.0;

            double allButtonRejQty = 0.0;
            double allKnobRejQty = 0.0;
            double allLenRejQty = 0.0;
            double allPanelRejQty = 0.0;

            double allTotal_Parts_Produce = 0.0;
            double allTotal_Parts_Cost = 0.0;
            double allGood_Parts_Qty = 0.0;
            double allGood_Parts_Cost = 0.0;
            double allReject_Parts_Qty = 0.0;
            double allReject_Parts_Cost = 0.0;


            foreach (DataRow tmpDr in dtProduct.Rows)
            {
                allButton += double.Parse(tmpDr["Button"].ToString());
                allKnob += double.Parse(tmpDr["Knob"].ToString());
                allLen += double.Parse(tmpDr["Len"].ToString());
                allPanel += double.Parse(tmpDr["Panel"].ToString());

                allButtonRejQty += double.Parse(tmpDr["ButtonRejQty"].ToString());
                allKnobRejQty += double.Parse(tmpDr["KnobRejQty"].ToString());
                allLenRejQty += double.Parse(tmpDr["LenRejQty"].ToString());
                allPanelRejQty += double.Parse(tmpDr["PanelRejQty"].ToString());


                allTotal_Parts_Cost += double.Parse(tmpDr["Total Parts Cost"].ToString());
                allTotal_Parts_Produce += double.Parse(tmpDr["Total Parts Produce"].ToString());
                allGood_Parts_Cost += double.Parse(tmpDr["Good Parts Cost"].ToString());
                allGood_Parts_Qty += double.Parse(tmpDr["Good Parts Qty"].ToString());
                allReject_Parts_Cost += double.Parse(tmpDr["Reject Parts Cost"].ToString());
                allReject_Parts_Qty += double.Parse(tmpDr["Reject Parts Qty"].ToString());
            }


            DataRow drTotal = dtProduct.NewRow();
            drTotal["Month"] = "Grand Total :";
            drTotal["Button"] = allButton;
            drTotal["Knob"] = allKnob;
            drTotal["Len"] = allLen;
            drTotal["Panel"] = allPanel; ;
            drTotal["Total Parts Produce"] = allTotal_Parts_Produce;
            drTotal["Total Parts Cost"] = allTotal_Parts_Cost;
            drTotal["Good Parts Qty"] = allGood_Parts_Qty;
            drTotal["Good Parts Cost"] = allGood_Parts_Cost;
            drTotal["Reject Parts Qty"] = allReject_Parts_Qty;
            drTotal["Reject Parts Cost"] = allReject_Parts_Cost;

            drTotal["ButtonRej%"] = Math.Round(allButtonRejQty / (allButton+allButtonRejQty) * 100, 2).ToString() + "%";
            drTotal["KnobRej%"] = Math.Round(allKnobRejQty / (allKnob + allKnobRejQty) * 100, 2).ToString() + "%";
            drTotal["LenRej%"] = Math.Round(allLenRejQty / (allLen + allLenRejQty) * 100, 2).ToString() + "%";
            drTotal["PanelRej%"] = Math.Round(allPanelRejQty / (allPanel + allPanelRejQty) * 100, 2).ToString() + "%";
            drTotal["Rej%"] = Math.Round(allReject_Parts_Qty / allTotal_Parts_Produce * 100, 2).ToString() + "%";

            dtProduct.Rows.Add(drTotal);

            dtProduct.Columns.Remove(dtProduct.Columns["ButtonRejQty"]);
            dtProduct.Columns.Remove(dtProduct.Columns["KnobRejQty"]);
            dtProduct.Columns.Remove(dtProduct.Columns["LenRejQty"]);
            dtProduct.Columns.Remove(dtProduct.Columns["PanelRejQty"]);


            return dtProduct;
        }
        
        public DataTable GetMonthlyProductionReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sCustomer, string sType)
        {
            DataTable dt = dal.GetMonthlyProductionReport(dDateFrom, dDateTo, sPartNo, sCustomer, sType);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

           
            int iMonthFirstDay = dDateFrom.Day;
            int iMonthLastDay = dDateTo.AddDays(-1).Day;

            //cumu qty, cumu rej  每天累计下来的数量
            double dCumuQty = 0;
            double dCumuRej = 0;
            for (int i = iMonthFirstDay; i <= iMonthLastDay; i++)
            {
                DataRow[] drArrTemp = dt.Select(" Date = '" + i.ToString() + "'");
                if (drArrTemp.Length >= 2)
                {
                    // 累计 cumu, 并赋值cumu qty, rej, ave rej%
                    dCumuQty += double.Parse(drArrTemp[0]["DAILY Total Qty"].ToString());
                    dCumuRej += double.Parse(drArrTemp[0]["Rej Qty"].ToString());
                    dCumuRej += double.Parse(drArrTemp[1]["Rej Qty"].ToString());

                    drArrTemp[0]["Cumu. Qty"] = dCumuQty;
                    drArrTemp[0]["Cumu. Rej"] = dCumuRej;
                    drArrTemp[0]["Ave Rej%"]  = dCumuQty == 0? "0.00%" : Math.Round(dCumuRej/dCumuQty *100,2).ToString("0.00") + "%";

                    drArrTemp[1]["Cumu. Qty"] = dCumuQty;
                    drArrTemp[1]["Cumu. Rej"] = dCumuRej;
                    drArrTemp[1]["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";
                }
                else if (drArrTemp.Length == 1)
                {
                    // 累计 cumu, 并赋值cumu qty, rej, ave rej%,  并补充缺少的半天数据
                    dCumuQty += double.Parse(drArrTemp[0]["DAILY Total Qty"].ToString());
                    dCumuRej += double.Parse(drArrTemp[0]["Rej Qty"].ToString());

                    drArrTemp[0]["Cumu. Qty"] = dCumuQty;
                    drArrTemp[0]["Cumu. Rej"] = dCumuRej;
                    drArrTemp[0]["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";


                    string shift = drArrTemp[0]["shift"].ToString();
                    if (shift == "Day")
                    {
                        #region 补充 night
                        string sToday = string.Format("{0}-{1}-{2}", dDateFrom.Year, dDateFrom.Month, i);

                        DataRow drNewNight = dt.NewRow();

                        drNewNight["WeekDay"] = DateTime.Parse(sToday).DayOfWeek.ToString().Substring(0, 3);
                        drNewNight["Date"] = i.ToString();

                        drNewNight["DAILY Total Qty"] = "";
                        drNewNight["shift"] = "Night";
                        drNewNight["Rej Qty"] = 0;
                        drNewNight["Daily Rej%"] = "0.00%";
                        drNewNight["Total Daily Rej%"] = "0.00%";
                        drNewNight["Defect"] = "";

                        drNewNight["White Dot"] = "";
                        drNewNight["Scratches"] = "";
                        drNewNight["Dented Mark"] = "";
                        drNewNight["Shinning Dot"] = "";
                        drNewNight["Black Mark"] = "";
                        drNewNight["Sink Mark"] = "";
                        drNewNight["Flow Mark"] = "";
                        drNewNight["High Gate"] = "";
                        drNewNight["Silver Steak"] = "";
                        drNewNight["Black Dot"] = "";
                        drNewNight["Oil Stain"] = "";
                        drNewNight["Flow Line"] = "";
                        drNewNight["Over-Cut"] = "";
                        drNewNight["Crack"] = "";
                        drNewNight["Short Mold"] = "";
                        drNewNight["Stain Mark"] = "";
                        drNewNight["Weld Line"] = "";
                        drNewNight["Flashes"] = "";
                        drNewNight["Foreign Materials"] = "";
                        drNewNight["Drag"] = "";
                        drNewNight["Material Bleed"] = "";
                        drNewNight["Bent"] = "";
                        drNewNight["Deform"] = "";
                        drNewNight["Gas Mark"] = "";

                        drNewNight["IPQC Buy Off"] = "";
                        drNewNight["Adjustment (Scrap)"] = "";


                        drNewNight["Cumu. Qty"] = dCumuQty;
                        drNewNight["Cumu. Rej"] = dCumuRej;
                        drNewNight["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";
                        dt.Rows.Add(drNewNight);
                        #endregion
                    }
                    else
                    {
                        #region 补充 day
                        string sToday = string.Format("{0}-{1}-{2}", dDateFrom.Year, dDateFrom.Month, i);

                        DataRow drNewDay = dt.NewRow();

                        drNewDay["WeekDay"] = DateTime.Parse(sToday).DayOfWeek.ToString().Substring(0, 3);
                        drNewDay["Date"] = i.ToString();

                        drNewDay["DAILY Total Qty"] = "";
                        drNewDay["shift"] = "Day";
                        drNewDay["Rej Qty"] = 0;
                        drNewDay["Daily Rej%"] = "0.00%";
                        drNewDay["Total Daily Rej%"] = "0.00%";
                        drNewDay["Defect"] = "";

                        drNewDay["White Dot"] = "";
                        drNewDay["Scratches"] = "";
                        drNewDay["Dented Mark"] = "";
                        drNewDay["Shinning Dot"] = "";
                        drNewDay["Black Mark"] = "";
                        drNewDay["Sink Mark"] = "";
                        drNewDay["Flow Mark"] = "";
                        drNewDay["High Gate"] = "";
                        drNewDay["Silver Steak"] = "";
                        drNewDay["Black Dot"] = "";
                        drNewDay["Oil Stain"] = "";
                        drNewDay["Flow Line"] = "";
                        drNewDay["Over-Cut"] = "";
                        drNewDay["Crack"] = "";
                        drNewDay["Short Mold"] = "";
                        drNewDay["Stain Mark"] = "";
                        drNewDay["Weld Line"] = "";
                        drNewDay["Flashes"] = "";
                        drNewDay["Foreign Materials"] = "";
                        drNewDay["Drag"] = "";
                        drNewDay["Material Bleed"] = "";
                        drNewDay["Bent"] = "";
                        drNewDay["Deform"] = "";
                        drNewDay["Gas Mark"] = "";

                        drNewDay["IPQC Buy Off"] = "";
                        drNewDay["Adjustment (Scrap)"] = "";


                        drNewDay["Cumu. Qty"] = dCumuQty;
                        drNewDay["Cumu. Rej"] = dCumuRej;
                        drNewDay["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";

                        dt.Rows.Add(drNewDay);
                        #endregion
                    }
                }
                else
                {
                    string sToday = string.Format("{0}-{1}-{2}", dDateFrom.Year, dDateFrom.Month, i);
                    string sWeekName = DateTime.Parse(sToday).DayOfWeek.ToString().Substring(0, 3);


                    //补充全天2条数据, 
                    #region 补充 day
                    DataRow drNewDay = dt.NewRow();

                    drNewDay["WeekDay"] = sWeekName;
                    drNewDay["Date"] = i.ToString();

                    drNewDay["DAILY Total Qty"] = "";
                    drNewDay["shift"] = "Day";
                    drNewDay["Rej Qty"] = 0;
                    drNewDay["Daily Rej%"] = "0.00%";
                    drNewDay["Total Daily Rej%"] = "0.00%";
                    drNewDay["Defect"] = "";

                    drNewDay["White Dot"] = "";
                    drNewDay["Scratches"] = "";
                    drNewDay["Dented Mark"] = "";
                    drNewDay["Shinning Dot"] = "";
                    drNewDay["Black Mark"] = "";
                    drNewDay["Sink Mark"] = "";
                    drNewDay["Flow Mark"] = "";
                    drNewDay["High Gate"] = "";
                    drNewDay["Silver Steak"] = "";
                    drNewDay["Black Dot"] = "";
                    drNewDay["Oil Stain"] = "";
                    drNewDay["Flow Line"] = "";
                    drNewDay["Over-Cut"] = "";
                    drNewDay["Crack"] = "";
                    drNewDay["Short Mold"] = "";
                    drNewDay["Stain Mark"] = "";
                    drNewDay["Weld Line"] = "";
                    drNewDay["Flashes"] = "";
                    drNewDay["Foreign Materials"] = "";
                    drNewDay["Drag"] = "";
                    drNewDay["Material Bleed"] = "";
                    drNewDay["Bent"] = "";
                    drNewDay["Deform"] = "";
                    drNewDay["Gas Mark"] = "";

                    drNewDay["IPQC Buy Off"] = "";
                    drNewDay["Adjustment (Scrap)"] = "";


                    drNewDay["Cumu. Qty"] = dCumuQty;
                    drNewDay["Cumu. Rej"] = dCumuRej;
                    drNewDay["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";

                    dt.Rows.Add(drNewDay);
                    #endregion

                    #region 补充 night
                    DataRow drNewNight = dt.NewRow();

                    drNewNight["WeekDay"] = sWeekName;
                    drNewNight["Date"] = i.ToString();

                    drNewNight["DAILY Total Qty"] = "";
                    drNewNight["shift"] = "Night";
                    drNewNight["Rej Qty"] = 0;
                    drNewNight["Daily Rej%"] = "0.00%";
                    drNewNight["Total Daily Rej%"] = "0.00%";
                    drNewNight["Defect"] = "";

                    drNewNight["White Dot"] = "";
                    drNewNight["Scratches"] = "";
                    drNewNight["Dented Mark"] = "";
                    drNewNight["Shinning Dot"] = "";
                    drNewNight["Black Mark"] = "";
                    drNewNight["Sink Mark"] = "";
                    drNewNight["Flow Mark"] = "";
                    drNewNight["High Gate"] = "";
                    drNewNight["Silver Steak"] = "";
                    drNewNight["Black Dot"] = "";
                    drNewNight["Oil Stain"] = "";
                    drNewNight["Flow Line"] = "";
                    drNewNight["Over-Cut"] = "";
                    drNewNight["Crack"] = "";
                    drNewNight["Short Mold"] = "";
                    drNewNight["Stain Mark"] = "";
                    drNewNight["Weld Line"] = "";
                    drNewNight["Flashes"] = "";
                    drNewNight["Foreign Materials"] = "";
                    drNewNight["Drag"] = "";
                    drNewNight["Material Bleed"] = "";
                    drNewNight["Bent"] = "";
                    drNewNight["Deform"] = "";
                    drNewNight["Gas Mark"] = "";

                    drNewNight["IPQC Buy Off"] = "";
                    drNewNight["Adjustment (Scrap)"] = "";


                    drNewNight["Cumu. Qty"] = dCumuQty;
                    drNewNight["Cumu. Rej"] = dCumuRej;
                    drNewNight["Ave Rej%"] = dCumuQty == 0 ? "0.00%" : Math.Round(dCumuRej / dCumuQty * 100, 2).ToString("0.00") + "%";
                    dt.Rows.Add(drNewNight);
                    #endregion
                }
            }



            //重新排序下
            dt.DefaultView.Sort = "Date asc, shift asc";
            dt = dt.DefaultView.ToTable();




            double totalWhiteDot = 0;
            double totalScratches = 0;
            double totalDentedMark = 0;
            double totalShinningDot = 0;
            double totalBlackMark = 0;
            double totalSinkMark = 0;
            double totalFlowMark = 0;
            double totalHighGate = 0;
            double totalSilverSteak = 0;
            double totalBlackDot = 0;
            double totalOilStain = 0;
            double totalFlowLine = 0;
            double totalOverCut = 0;
            double totalCrack = 0;
            double totalShortMold = 0;
            double totalStainMark = 0;
            double totalWeldLine = 0;
            double totalFlashes = 0;
            double totalForeignMaterials = 0;
            double totalDrag = 0;
            double totalMaterialBleed = 0;
            double totalBent = 0;
            double totalDeform = 0;
            double totalGasMark = 0;

            double totalIPQCBuyOff = 0;
            double totalScrap = 0;

            double totalOutput = 0;


            foreach (DataRow dr in dt.Rows)
            {
                totalWhiteDot += double.Parse(dr["White Dot"].ToString() == ""? "0": dr["White Dot"].ToString());
                totalScratches += double.Parse(dr["Scratches"].ToString() == "" ? "0" : dr["Scratches"].ToString());
                totalDentedMark += double.Parse(dr["Dented Mark"].ToString() == "" ? "0" : dr["Dented Mark"].ToString());
                totalShinningDot += double.Parse(dr["Shinning Dot"].ToString() == "" ? "0" : dr["Shinning Dot"].ToString());
                totalBlackMark += double.Parse(dr["Black Mark"].ToString() == "" ? "0" : dr["Black Mark"].ToString());
                totalSinkMark += double.Parse(dr["Sink Mark"].ToString() == "" ? "0" : dr["Sink Mark"].ToString());
                totalFlowMark += double.Parse(dr["Flow Mark"].ToString() == "" ? "0" : dr["Flow Mark"].ToString());
                totalHighGate += double.Parse(dr["High Gate"].ToString() == "" ? "0" : dr["High Gate"].ToString());
                totalSilverSteak += double.Parse(dr["Silver Steak"].ToString() == "" ? "0" : dr["Silver Steak"].ToString());
                totalBlackDot += double.Parse(dr["Black Dot"].ToString() == "" ? "0" : dr["Black Dot"].ToString());
                totalOilStain += double.Parse(dr["Oil Stain"].ToString() == "" ? "0" : dr["Oil Stain"].ToString());
                totalFlowLine += double.Parse(dr["Flow Line"].ToString() == "" ? "0" : dr["Flow Line"].ToString());
                totalOverCut += double.Parse(dr["Over-Cut"].ToString() == "" ? "0" : dr["Over-Cut"].ToString());
                totalCrack += double.Parse(dr["Crack"].ToString() == "" ? "0" : dr["Crack"].ToString());
                totalShortMold += double.Parse(dr["Short Mold"].ToString() == "" ? "0" : dr["Short Mold"].ToString());
                totalStainMark += double.Parse(dr["Stain Mark"].ToString() == "" ? "0" : dr["Stain Mark"].ToString());
                totalWeldLine += double.Parse(dr["Weld Line"].ToString() == "" ? "0" : dr["Weld Line"].ToString());
                totalFlashes += double.Parse(dr["Flashes"].ToString() == "" ? "0" : dr["Flashes"].ToString());
                totalForeignMaterials += double.Parse(dr["Foreign Materials"].ToString() == "" ? "0" : dr["Foreign Materials"].ToString());
                totalDrag += double.Parse(dr["Drag"].ToString() == "" ? "0" : dr["Drag"].ToString());
                totalMaterialBleed += double.Parse(dr["Material Bleed"].ToString() == "" ? "0" : dr["Material Bleed"].ToString());
                totalBent += double.Parse(dr["Bent"].ToString() == "" ? "0" : dr["Bent"].ToString());
                totalDeform += double.Parse(dr["Deform"].ToString() == "" ? "0" : dr["Deform"].ToString());
                totalGasMark += double.Parse(dr["Gas Mark"].ToString() == "" ? "0" : dr["Gas Mark"].ToString());

                totalIPQCBuyOff += double.Parse(dr["IPQC Buy Off"].ToString() == "" ? "0" : dr["IPQC Buy Off"].ToString());
                totalScrap += double.Parse(dr["Adjustment (Scrap)"].ToString() == "" ? "0" : dr["Adjustment (Scrap)"].ToString());

                totalOutput += double.Parse(dr["DAILY Total Qty"].ToString() == "" ? "0" : dr["DAILY Total Qty"].ToString());
            }

            DataRow drTotal = dt.NewRow();
            drTotal["Defect"] = "Total";
            drTotal["White Dot"] = totalWhiteDot;
            drTotal["Scratches"] = totalScratches;
            drTotal["Dented Mark"] = totalDentedMark; ;
            drTotal["Shinning Dot"] = totalShinningDot;
            drTotal["Black Mark"] = totalBlackMark;
            drTotal["Sink Mark"] = totalSinkMark;
            drTotal["Flow Mark"] = totalFlowMark;
            drTotal["High Gate"] = totalHighGate;
            drTotal["Silver Steak"] = totalSilverSteak;
            drTotal["Black Dot"] = totalBlackDot;
            drTotal["Oil Stain"] = totalOilStain;
            drTotal["Flow Line"] = totalFlowLine;
            drTotal["Over-Cut"] = totalOverCut;
            drTotal["Crack"] = totalCrack;
            drTotal["Short Mold"] = totalShortMold;
            drTotal["Stain Mark"] = totalStainMark;
            drTotal["Weld Line"] = totalWeldLine;
            drTotal["Flashes"] = totalFlashes;
            drTotal["Foreign Materials"] = totalForeignMaterials;
            drTotal["Drag"] = totalDrag;
            drTotal["Material Bleed"] = totalMaterialBleed;
            drTotal["Bent"] = totalBent;
            drTotal["Deform"] = totalDeform;
            drTotal["Gas Mark"] = totalGasMark;
            drTotal["IPQC Buy Off"] = totalIPQCBuyOff;
            drTotal["Adjustment (Scrap)"] = totalScrap;

            dt.Rows.Add(drTotal);



            totalOutput = totalOutput / 2;

            DataRow dtTotalRate = dt.NewRow();
            dtTotalRate["Defect"] = "Rej %";
            dtTotalRate["White Dot"] = Math.Round(totalWhiteDot / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Scratches"] = Math.Round(totalScratches / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Dented Mark"] = Math.Round(totalDentedMark / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Shinning Dot"] = Math.Round(totalShinningDot / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Black Mark"] = Math.Round(totalBlackMark / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Sink Mark"] = Math.Round(totalSinkMark / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Flow Mark"] = Math.Round(totalFlowMark / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["High Gate"] = Math.Round(totalHighGate / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Silver Steak"] = Math.Round(totalSilverSteak / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Black Dot"] = Math.Round(totalBlackDot / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Oil Stain"] = Math.Round(totalOilStain / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Flow Line"] = Math.Round(totalFlowLine / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Over-Cut"] = Math.Round(totalOverCut / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Crack"] = Math.Round(totalCrack / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Short Mold"] = Math.Round(totalShortMold / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Stain Mark"] = Math.Round(totalStainMark / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Weld Line"] = Math.Round(totalWeldLine / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Flashes"] = Math.Round(totalFlashes / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Foreign Materials"] = Math.Round(totalForeignMaterials / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Drag"] = Math.Round(totalDrag / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Material Bleed"] = Math.Round(totalMaterialBleed / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Bent"] = Math.Round(totalBent / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Deform"] = Math.Round(totalDeform / totalOutput * 100, 2).ToString("0.00") + "%";
            dtTotalRate["Gas Mark"] = Math.Round(totalGasMark / totalOutput * 100, 2).ToString("0.00") + "%";

            dt.Rows.Add(dtTotalRate);

            
            return dt;
        }
        

        public DataTable GetTopRejPartsList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            DataTable dtParts = dal.GetTopRejParts(dDateFrom, dDateTo, sMachineID);
            if (dtParts == null || dtParts.Rows.Count == 0)
                return null;

            DataTable dtDefects = dal.GetTopRejParts_DefectQty(dDateFrom, dDateTo, sMachineID);
            if (dtDefects == null || dtDefects.Rows.Count == 0)
                return null;


            DataTable dtPartsList = new DataTable();
            dtPartsList.Columns.Add("partNo");
            dtPartsList.Columns.Add("output");
            dtPartsList.Columns.Add("rejQty");
            dtPartsList.Columns.Add("rejRate");
            dtPartsList.Columns.Add("highestDefect_1st");
            dtPartsList.Columns.Add("highestDefect_2nd");
            dtPartsList.Columns.Add("highestDefect_3rd");

            foreach (DataRow drPart in dtParts.Rows)
            {
                string partNo = drPart["partNo"].ToString();
                DataTable dtDefectTemp = dtDefects.Select("partNo = '" + partNo + "'", " rejQty desc").CopyToDataTable();

                string defectCode_1st = dtDefectTemp.Rows[0]["defectCode"].ToString();
                double rejQty_1st = double.Parse(dtDefectTemp.Rows[0]["rejQty"].ToString());
                double rejRate_1st = double.Parse(dtDefectTemp.Rows[0]["rejRate"].ToString());

                string defectCode_2nd = dtDefectTemp.Rows[1]["defectCode"].ToString();
                double rejQty_2nd = double.Parse(dtDefectTemp.Rows[1]["rejQty"].ToString());
                double rejRate_2nd = double.Parse(dtDefectTemp.Rows[1]["rejRate"].ToString());

                string defectCode_3rd = dtDefectTemp.Rows[2]["defectCode"].ToString();
                double rejQty_3rd = double.Parse(dtDefectTemp.Rows[2]["rejQty"].ToString());
                double rejRate_3rd = double.Parse(dtDefectTemp.Rows[2]["rejRate"].ToString());


                string highestDefect_1st = rejQty_1st == 0? "NA" : string.Format("{0}: {1} ({2}%)", defectCode_1st, rejQty_1st, rejRate_1st.ToString("0.00"));
                string highestDefect_2nd = rejQty_2nd == 0 ? "NA" : string.Format("{0}: {1} ({2}%)", defectCode_2nd, rejQty_2nd, rejRate_2nd.ToString("0.00"));
                string highestDefect_3rd = rejQty_3rd == 0 ? "NA" : string.Format("{0}: {1} ({2}%)", defectCode_3rd, rejQty_3rd, rejRate_3rd.ToString("0.00"));

                DataRow drNew = dtPartsList.NewRow();
                drNew["partNo"] = partNo;
                drNew["output"] = drPart["output"].ToString();
                drNew["rejQty"] = drPart["rejQty"].ToString();
                drNew["rejRate"] = drPart["rejRate"].ToString();
                drNew["highestDefect_1st"] = highestDefect_1st;
                drNew["highestDefect_2nd"] = highestDefect_2nd;
                drNew["highestDefect_3rd"] = highestDefect_3rd;


                dtPartsList.Rows.Add(drNew);
            }

            return dtPartsList;
        }



        public DataTable GetTopRejDefectsList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            DataTable dtDefects = dal.GetTopRejDefects(dDateFrom, dDateTo, sMachineID);
            if (dtDefects == null || dtDefects.Rows.Count == 0)
                return null;

            //用来计算rejRate
            double totalRejQty = 0;

            //将sql执行出的只有一行的数据逐列添加到 dictionary中.
            Dictionary<string, double> dicRejDefects = new Dictionary<string, double>();
            for (int i = 0; i < dtDefects.Columns.Count; i++)
            {
                string columnName = dtDefects.Columns[i].ColumnName;
                double rejQty = double.Parse(dtDefects.Rows[0][i].ToString());
                dicRejDefects.Add(columnName, rejQty);
                totalRejQty += rejQty;
            }

        

            DataTable dtPartQty = dal.GetTopRejDefects_PartQty(dDateFrom, dDateTo, sMachineID);
            if (dtPartQty == null || dtPartQty.Rows.Count == 0)
                return null;


            DataTable dtDefectsList = new DataTable();
            dtDefectsList.Columns.Add("defectCode");
            dtDefectsList.Columns.Add("totalRejQty");
            dtDefectsList.Columns.Add("totalRejRate", typeof(double));//按照rejrate排序, 特意定义成double类型
            dtDefectsList.Columns.Add("affectedPart_1st");
            dtDefectsList.Columns.Add("affectedPart_2nd");
            dtDefectsList.Columns.Add("affectedPart_3rd");



            foreach (KeyValuePair<string,double> kv in dicRejDefects)
            {
                string defectCode = kv.Key;
                double rejQty = kv.Value;

                DataTable dtPartQtyTemp = dtPartQty.Select(" defectCode = '" + defectCode + "'", " rejQty desc").CopyToDataTable();
                string partNo_1st = dtPartQtyTemp.Rows[0]["partNo"].ToString();
                double partRejQty_1st = double.Parse(dtPartQtyTemp.Rows[0]["rejQty"].ToString());
                double partRejRate_1st = Math.Round(partRejQty_1st / rejQty * 100, 2);

                string partNo_2nd = dtPartQtyTemp.Rows[1]["partNo"].ToString();
                double partRejQty_2nd = double.Parse(dtPartQtyTemp.Rows[1]["rejQty"].ToString());
                double partRejRate_2nd = Math.Round(partRejQty_2nd / rejQty * 100, 2);

                string partNo_3rd = dtPartQtyTemp.Rows[2]["partNo"].ToString();
                double partRejQty_3rd = double.Parse(dtPartQtyTemp.Rows[2]["rejQty"].ToString());
                double partRejRate_3rd = Math.Round(partRejQty_3rd / rejQty * 100, 2);


                string affectedPart_1st = partRejQty_1st == 0 ? "NA" : string.Format("{0}: {1} ({2}%)", partNo_1st, partRejQty_1st, partRejRate_1st.ToString("0.00"));
                string affectedPart_2nd = partRejQty_2nd == 0 ? "NA" : string.Format("{0}: {1} ({2}%)", partNo_2nd, partRejQty_2nd, partRejRate_2nd.ToString("0.00"));
                string affectedPart_3rd = partRejQty_3rd == 0 ? "NA" : string.Format("{0}: {1} ({2}%)", partNo_3rd, partRejQty_3rd, partRejRate_3rd.ToString("0.00"));


                DataRow drNew = dtDefectsList.NewRow();
                drNew["defectCode"] = defectCode;
                drNew["totalRejQty"] = rejQty;
                drNew["totalRejRate"] = Math.Round(rejQty / totalRejQty * 100, 2);

                drNew["affectedPart_1st"] = affectedPart_1st;
                drNew["affectedPart_2nd"] = affectedPart_2nd;
                drNew["affectedPart_3rd"] = affectedPart_3rd;

                dtDefectsList.Rows.Add(drNew);
            }

            
            //按照rejrate 大到小排
            return dtDefectsList.Select("", "totalRejRate desc").CopyToDataTable();
        }



        public DataTable GetViForDailyReport_NEW(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
            DataTable dt = dal.GetViForDailyReport_NEW(dDateFrom, dDateTo, sPartNo, sJigNo, sShift);

            if (dt == null || dt.Rows.Count ==0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }


        public Common.Class.Model.MouldingViHistory_Model GetModel(string sTrackingID)
        {
            DataTable dt = dal.GetList(sTrackingID);

            if (dt == null)
            {
                return null;
            }

            Common.Class.Model.MouldingViHistory_Model model =    DatarowToModel(dt.Rows[0]);


            return model;
        }


        public bool Maintenace(Common.Class.Model.MouldingViHistory_Model model)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.MaintenanceCMD(model));


            model.refField05 = "Moulding Maintenance";
            cmdList.Add(dal.AddHistoryCMD(model));


            Common.Class.BLL.MouldingViDefectTracking_BLL defectBLL = new MouldingViDefectTracking_BLL();
            cmdList.Add(defectBLL.MaintenanceCommand(model.TrackingID, model.PartNumber, model.Model, model.JigNo));




            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public bool DeleteTransaction(string sTrackingID)
        {
            Common.Class.DAL.MouldingViDefectTracking_DAL defectDAL = new DAL.MouldingViDefectTracking_DAL();

            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(dal.DeleteCommand(sTrackingID));
            cmdList.Add(defectDAL.DeleteCommand(sTrackingID));



            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


    }
}
