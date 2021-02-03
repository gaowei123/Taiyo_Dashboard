using System.Text.RegularExpressions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.BLL
{
	/// <summary>
	/// LMMSWatchLog_BLL
	/// </summary>
	public class LMMSWatchLog_BLL
	{
		private readonly Common.DAL.LMMSWatchLog_DAL dal=new Common.DAL.LMMSWatchLog_DAL();
		public LMMSWatchLog_BLL()
		{}
		#region  Method
		

        


        public DataTable GetMaterialList(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber, string sModule)
        {
            Common.Class.BLL.LMMSInventoty_BLL InventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();

            //1. get all job list
            DataTable dt_JobList = GetJobList(sDay, sShift, dDateFrom, dDateTo, sJobnumber, sMachineID, sPartNumber, sModule);

            if (dt_JobList == null || dt_JobList.Rows.Count == 0)
                return null;


            #region material main datatable structure
            DataTable dt_MaterialList = new DataTable();
            dt_MaterialList.Columns.Add("machineID");
            dt_MaterialList.Columns.Add("shift");
            dt_MaterialList.Columns.Add("customer");
            dt_MaterialList.Columns.Add("module");
            dt_MaterialList.Columns.Add("jobNumber");
            dt_MaterialList.Columns.Add("partNumber");
            dt_MaterialList.Columns.Add("StartTime");
            dt_MaterialList.Columns.Add("EndTime");

            dt_MaterialList.Columns.Add("machineID_temp");
            dt_MaterialList.Columns.Add("shift_temp");
            dt_MaterialList.Columns.Add("customer_temp");
            dt_MaterialList.Columns.Add("module_temp");
            dt_MaterialList.Columns.Add("jobNumber_temp");
            dt_MaterialList.Columns.Add("partNumber_temp");
            dt_MaterialList.Columns.Add("StartTime_temp");
            dt_MaterialList.Columns.Add("EndTime_temp");

            dt_MaterialList.Columns.Add("materialPart");
            dt_MaterialList.Columns.Add("NG");
            dt_MaterialList.Columns.Add("OK");
            dt_MaterialList.Columns.Add("output");
            dt_MaterialList.Columns.Add("NGRate");
          
            dt_MaterialList.Columns.Add("UpdatedBy");
            #endregion


            //2. foreach 
            foreach (DataRow row in dt_JobList.Rows)
            {
                if (row["MachineID"].ToString() == "Total :")
                    continue;

                //2.1 get each row para.
                string Job_MachineID = row["MachineID"].ToString();
                string Job_Day = row["day"].ToString();
                string Job_Shift = row["shift"].ToString();
                string Job_Jobnumber = row["JobNumber"].ToString();
                string Job_PartNumber = row["partnumber"].ToString();


                //2.2 **Main Logic**  material segment  
                DataTable dt_MaterialListForSingleJob = JobMaterialDetail(Job_Day, Job_Shift, dDateFrom, dDateTo, Job_Jobnumber, Job_MachineID, Job_PartNumber);

                if (dt_MaterialListForSingleJob != null)
                {
                    dt_MaterialList.Merge(dt_MaterialListForSingleJob, false, MissingSchemaAction.AddWithKey);
                }
            }


            //sort
            DataRow[] drArry = dt_MaterialList.Select("", "MachineID asc, partnumber asc");
            DataTable dt_MaterialListAfterSort = Common.CommFunctions.DataRowToDataTable(drArry);
            //sort



            #region 3. add total summary row 
            double Count_OK = 0;
            double Count_NG = 0;


            foreach (DataRow dr in dt_MaterialListAfterSort.Rows)
            {
                try
                {
                    Count_OK += double.Parse(dr["OK"].ToString());
                }
                catch { Count_OK += 0; }

                try
                {
                    Count_NG += double.Parse(dr["NG"].ToString());
                }
                catch
                {
                    Count_NG += 0;
                }
            }

            DataRow dr_total = dt_MaterialListAfterSort.NewRow();
            dr_total["machineID_temp"] = "Total :";
            dr_total["OK"] = Count_OK;
            dr_total["NG"] = Count_NG;
            dr_total["output"] = Count_OK + Count_NG;
            dr_total["NGRate"] = Math.Round(Count_NG / (Count_OK + Count_NG) * 100, 2).ToString() + "%";

            dt_MaterialListAfterSort.Rows.Add(dr_total);
            #endregion


            return dt_MaterialListAfterSort;
        }

        


        public DataTable GetJobList(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber, string sModule)
        {

            DataSet ds = dal.GetJobList(sDay, sShift, dDateFrom, dDateTo, sJobnumber, sMachineID, sPartNumber, sModule);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }


        public DataTable GetJobNG(DateTime dDateFrom, DateTime dDateTo)
        {

            DataSet ds = dal.GetJobNG(dDateFrom, dDateTo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }




        public DataTable JobMaterialDetail(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber)
        {
            Common.Class.BLL.LMMSUserEventLog_BLL bll = new Class.BLL.LMMSUserEventLog_BLL();
            DataSet ds = dal.getNGByJobnumber(sDay,sShift,dDateFrom,dDateTo,sJobnumber,sMachineID,sPartNumber);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr_job = ds.Tables[0].Rows[0];


            #region material datatable structure
            DataTable dt_Material = new DataTable();
            dt_Material.Columns.Add("machineID");
            dt_Material.Columns.Add("shift");
            dt_Material.Columns.Add("customer");
            dt_Material.Columns.Add("module");
            dt_Material.Columns.Add("jobNumber");
            dt_Material.Columns.Add("partNumber");
            dt_Material.Columns.Add("StartTime");
            dt_Material.Columns.Add("EndTime");

            dt_Material.Columns.Add("machineID_temp");
            dt_Material.Columns.Add("shift_temp");
            dt_Material.Columns.Add("customer_temp");
            dt_Material.Columns.Add("module_temp");
            dt_Material.Columns.Add("jobNumber_temp");
            dt_Material.Columns.Add("partNumber_temp");
            dt_Material.Columns.Add("StartTime_temp");
            dt_Material.Columns.Add("EndTime_temp");

            dt_Material.Columns.Add("materialPart");
            dt_Material.Columns.Add("NG");
            dt_Material.Columns.Add("OK");
            dt_Material.Columns.Add("output");
            dt_Material.Columns.Add("NGRate");
            dt_Material.Columns.Add("UpdatedBy");
            #endregion


            string UpdatedBy = "";
            double NGForMaterial = 0;
            double OKForMaterial = 0;

            if (sMachineID == "1" || sMachineID == "2" || sMachineID == "3" || sMachineID == "4" || sMachineID == "5")
            {
                #region Machine 1- 5
                for (int i = 1; i < 17; i++)
                {
                    DataRow dr = dt_Material.NewRow();
                    

                    string ModelName = dr_job["model" + i.ToString() + "Name"].ToString();
                    if (!string.IsNullOrEmpty(ModelName))
                    {
                        dr["materialPart"] = ModelName;
                        UpdatedBy = bll.GetUpdatedByMaterial(sJobnumber, ModelName);
                    }
                    else
                    {
                        continue;
                    }

                    string GetNG = dr_job["ng" + i.ToString() + "Count"].ToString();
                    if (!string.IsNullOrEmpty(GetNG) && GetNG != "0")
                    {
                        NGForMaterial = double.Parse(GetNG);
                    }
                    else
                    {
                        NGForMaterial = 0;
                    }

                    string GetOK = dr_job["ok" + i.ToString() + "Count"].ToString();
                    if (!string.IsNullOrEmpty(GetOK) && GetOK != "0")
                    {
                        OKForMaterial = double.Parse(GetOK);
                    }
                    else
                    {
                        OKForMaterial = 0;
                    }

                    dr["OK"] = OKForMaterial;
                    dr["NG"] = NGForMaterial;
                    dr["output"] = OKForMaterial + NGForMaterial;
                    if (OKForMaterial + NGForMaterial == 0)
                    {
                        dr["NGRate"] = "0%";
                    }else
                    {
                        dr["NGRate"] = Math.Round(NGForMaterial * 100 / (OKForMaterial + NGForMaterial), 2).ToString() + "%";
                    }

                    dr["machineID"] = "Machine" + sMachineID;
                    dr["jobNumber"] = dr_job["jobnumber"].ToString();
                    dr["partNumber"] = dr_job["partNumber"].ToString();
                    dr["StartTime"] = dr_job["StartTime"].ToString();
                    dr["EndTime"] = dr_job["stopTime"].ToString();
                    dr["module"] = dr_job["module"].ToString();
                    dr["UpdatedBy"] = UpdatedBy;
                    dr["shift"] = DateTime.Parse( dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                    dr["customer"] = dr_job["customer"].ToString();

                    //相同part, 只显示一次
                    if (i == 1)
                    {
                        dr["machineID_temp"] = "Machine" + sMachineID;
                        dr["shift_temp"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                        dr["module_temp"] = dr_job["module"].ToString();
                        dr["jobNumber_temp"] = dr_job["jobnumber"].ToString();
                        dr["partNumber_temp"] = dr_job["partNumber"].ToString();
                        dr["customer_temp"] = dr_job["customer"].ToString();

                        dr["StartTime_temp"] = DateTime.Parse(dr_job["StartTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                        dr["EndTime_temp"] = DateTime.Parse(dr_job["stopTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                   
                    }
                    else
                    {
                        dr["machineID_temp"] = "";
                        dr["shift_temp"] = "";
                        dr["module_temp"] = "";
                        dr["jobNumber_temp"] = "";
                        dr["partNumber_temp"] = "";
                        dr["customer_temp"] = "";
                        dr["StartTime_temp"] = "";
                        dr["EndTime_temp"] = "";
                    }
                    //相同part, 只显示一次


                    dt_Material.Rows.Add(dr);
                  
                }
                #endregion
            }
            else
            {
                #region machine 6-8
                DataRow dr = dt_Material.NewRow();


                UpdatedBy = bll.GetUpdatedByJob(sJobnumber);
                try
                {
                    NGForMaterial = double.Parse(dr_job["totalFail"].ToString());
                }
                catch
                {
                    NGForMaterial = 0;
                }

                try
                {
                    OKForMaterial = double.Parse(dr_job["totalPass"].ToString());
                }
                catch
                {
                    OKForMaterial = 0;
                }
                


                dr["machineID"] = "Machine" + sMachineID;

                dr["shift"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                dr["jobNumber"] = dr_job["jobnumber"].ToString();
                dr["partNumber"] = dr_job["partnumber"].ToString();
                dr["UpdatedBy"] = UpdatedBy;

                dr["NG"] = NGForMaterial;
                dr["OK"] = OKForMaterial;
                dr["output"] = OKForMaterial + NGForMaterial;
                dr["NGRate"] = OKForMaterial+NGForMaterial ==0 ? "0%": Math.Round(NGForMaterial * 100 / (NGForMaterial + OKForMaterial), 2).ToString() + "%";
                dr["StartTime"] = dr_job["startTime"].ToString();
                dr["EndTime"] = dr_job["stopTime"].ToString();
                dr["module"] = dr_job["module"].ToString();
                dr["customer"] = dr_job["customer"].ToString();


                dr["machineID_temp"] = "Machine" + sMachineID;
                dr["shift_temp"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                dr["module_temp"] = dr_job["module"].ToString();
                dr["jobNumber_temp"] = dr_job["jobnumber"].ToString();
                dr["partNumber_temp"] = dr_job["partNumber"].ToString();
                dr["customer_temp"] = dr_job["customer"].ToString();
                //dr["StartTime_temp"] = dr_job["startTime"].ToString();
                //dr["EndTime_temp"] = dr_job["stopTime"].ToString();
                dr["StartTime_temp"] = DateTime.Parse(dr_job["StartTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                dr["EndTime_temp"] = DateTime.Parse(dr_job["stopTime"].ToString()).ToString("dd/MM/yy H:mm:ss");


                Common.Class.BLL.LMMSBomDetail_BLL bll_BomDetail = new Class.BLL.LMMSBomDetail_BLL();
                DataTable dt_bomDetail = bll_BomDetail.GetBomDetailListByPartNumber(dr_job["partnumber"].ToString());
                if (dt_bomDetail == null || dt_bomDetail.Rows.Count == 0)
                {
                    dr["materialPart"] = "No BOM Detail !";
                }
                else
                {
                    dr["materialPart"] = dt_bomDetail.Rows[0]["materialPartNo"].ToString();
                }
               

                dt_Material.Rows.Add(dr);

                #endregion
            }

            return dt_Material;
        }

		


	

        public List<Common.Model.LMMSWatchLog_Model> GetCurJobList()
        {
            List<Common.Model.LMMSWatchLog_Model> lJobList = new List<LMMSWatchLog_Model>();
            DataSet ds = dal.getRealJobInfo();

            if (ds == null && ds.Tables.Count == 0)
                return null;


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int iTotalPass = dr["totalPass"].ToString() == "" ? 0 : int.Parse(dr["totalPass"].ToString());
                int iTotalFail = dr["totalFail"].ToString() == "" ? 0 : int.Parse(dr["totalFail"].ToString());
                int iTotalQty = dr["totalQuantity"].ToString() == "" ? 0 : int.Parse(dr["totalQuantity"].ToString());
                string sJobnumber = dr["jobNumber"].ToString();
                string sPartNumber = dr["partNumber"].ToString();
                string sMachineID = dr["machineID"].ToString();
                string sLotNo = dr["LotNo"].ToString();

                
                Common.Model.LMMSWatchLog_Model wlCurrent = new LMMSWatchLog_Model();

                wlCurrent.totalPass = iTotalPass;
                wlCurrent.totalFail = iTotalFail;
                wlCurrent.totalQuantity = iTotalQty;
                wlCurrent.jobNumber = sJobnumber;
                wlCurrent.partNumber = sPartNumber;
                wlCurrent.machineID = sMachineID;
                wlCurrent.lotNo = sLotNo;
                lJobList.Add(wlCurrent);
            }


            return lJobList;
        }


      




      

       

        public DataTable GetSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
            return dal.GetSummaryReport(dDateFrom, dDateTo, sPartNo, sShift);
        }
      

       
        public DataTable GetProductionDetailReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sModel, string sPartNo, string sMachineID, string sJobNo)
        {
            DataTable dt = dal.GetProductionDetailReport(dDateFrom, dDateTo, sShift, sModel, sPartNo, sMachineID, sJobNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            TimeSpan totalTime = new TimeSpan(0);
            Dictionary<string, double> dicMrpQty = new Dictionary<string, double>();
            Dictionary<string, double> dicSetMrpQty = new Dictionary<string, double>();

            foreach (DataRow dr in dt.Rows)
            {
                string jobNo = dr["jobNo"].ToString();

                double ok = double.Parse(dr["ok"].ToString());
                double ng = double.Parse(dr["ng"].ToString());
                double output = double.Parse(dr["output"].ToString());
                double mrpTotal = double.Parse(dr["mrpTotal"].ToString());
                double setupQty = double.Parse(dr["setupQty"].ToString());

                double setOK = double.Parse(dr["setOK"].ToString());
                double setNG = double.Parse(dr["setNG"].ToString());
                double setOutput = double.Parse(dr["setOutput"].ToString());
                double setMrpTotal = double.Parse(dr["setMrpTotal"].ToString());
                double setSetupQty = double.Parse(dr["setSetupQty"].ToString());

                
                

                dr["displayOK"] = string.Format("{0}({1})", setOK,ok);
                dr["displayNG"] = string.Format("{0}({1})", setNG, ng);
                dr["displayOutput"] = string.Format("{0}({1})", setOutput, output);
                dr["displayMRP"] = string.Format("{0}({1})", setMrpTotal, mrpTotal);
                dr["displaySetup"] = string.Format("{0}({1})", setSetupQty, setupQty);


                double rejRate = Math.Round((ng + setupQty) / output * 100, 2);
                double setRejRate = Math.Round((setNG + setSetupQty) / setOutput * 100, 2);
                dr["displayRejRate"] = string.Format("{0}%({1}%)", setRejRate.ToString("0.00"), rejRate.ToString("0.00"));



                totalTime = totalTime + TimeSpan.Parse(dr["Time"].ToString());



                if (!dicMrpQty.ContainsKey(jobNo))
                    dicMrpQty.Add(jobNo, mrpTotal);
                
                if (!dicSetMrpQty.ContainsKey(jobNo))
                    dicSetMrpQty.Add(jobNo, setMrpTotal);
            }



            double totalOk = double.Parse(dt.Compute("sum(ok)", "").ToString());
            double totalNg = double.Parse(dt.Compute("sum(ng)", "").ToString());
            double totalOutput = double.Parse(dt.Compute("sum(output)", "").ToString());
            double totalSetupQty = double.Parse(dt.Compute("sum(setupQty)", "").ToString());

            double totalSetOK = double.Parse(dt.Compute("sum(setOK)", "").ToString());
            double totalSetNG = double.Parse(dt.Compute("sum(setNG)", "").ToString());
            double totalSetOutput = double.Parse(dt.Compute("sum(setOutput)", "").ToString());
            double totalSetSetupQty = double.Parse(dt.Compute("sum(setSetupQty)", "").ToString());

            double totalMrpTotal = 0;
            double totalSetMrpTotal = 0;
            foreach (KeyValuePair<string, double> item in dicMrpQty)
            {
                totalMrpTotal += item.Value;
            }
            foreach (KeyValuePair<string, double> item in dicSetMrpQty)
            {
                totalSetMrpTotal += item.Value;
            }




            DataRow drSummary = dt.NewRow();
            drSummary["shift"] = "Total";
            drSummary["Time"] = Math.Round(totalTime.TotalHours, 2) + "H";
            drSummary["displayOK"] = string.Format("{0}({1})", totalSetOK, totalOk);
            drSummary["displayNG"] = string.Format("{0}({1})", totalSetNG, totalNg);
            drSummary["displayOutput"] = string.Format("{0}({1})", totalSetOutput, totalOutput);
            drSummary["displayMRP"] = string.Format("{0}({1})", totalSetMrpTotal, totalMrpTotal);
            drSummary["displaySetup"] = string.Format("{0}({1})", totalSetSetupQty, totalSetupQty);

            double totalRejRate = Math.Round((totalNg + totalSetupQty) / totalOutput * 100, 2);
            double totalSetRejRate = Math.Round((totalSetNG + totalSetSetupQty) / totalSetOutput * 100, 2);
            drSummary["displayRejRate"] = string.Format("{0}%({1}%)", totalSetRejRate.ToString("0.00"), totalRejRate.ToString("0.00"));

            dt.Rows.Add(drSummary);

            return dt;
        }

        

        public bool IsJobFinished(string sJobNumber)
        {
            bool Result = false;

            DataSet ds = dal.IsJobFinished(sJobNumber);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Result = false;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                int Pass = int.Parse(dt.Rows[0]["totalPass"].ToString());
                int Fail = int.Parse(dt.Rows[0]["totalFail"].ToString());
                int Total = int.Parse(dt.Rows[0]["totalQuantity"].ToString());


                if (Pass + Fail >= Total)
                {
                    Result = true;
                }else
                {
                    Result = false;
                }

            }

            return Result;
        }

        #endregion  Method





        public DataTable GetProductivityChartData(string sReportType, string sYear, string sMachineID, string sPartNo, string sShift,string sModel, DateTime dDateFrom, DateTime dDateTo)
        {
            DataTable dtOutput = new DataTable();

            if (sReportType == "Yearly")
            {
                dtOutput = dal.getYearlyProduct(sPartNo, sShift, sMachineID,sModel);
            }
            else if (sReportType =="Monthly")
            {
                dtOutput = dal.getMonthlyProduct(sPartNo, sShift, int.Parse(sYear), sMachineID,sModel);
            }
            else if (sReportType == "Daily")
            {
                dtOutput = dal.getDailyProduct(sPartNo, sShift, dDateFrom, dDateTo, sMachineID, sModel);
            }
            else if (sReportType == "Model")
            {
                dtOutput = dal.getProductGroupByModel(sPartNo, sShift, dDateFrom, dDateTo, sMachineID);
            }
            else if (sReportType == "Machine")
            {
                dtOutput = dal.getProductGroupByMachine(sPartNo, sShift, dDateFrom, dDateTo, sModel);
            }



            return dtOutput;
        }





        #region for Output Report

        private string sAttdEmptyChar = "0"; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dDay"></param>
        /// <param name="sShift"></param>
        /// <returns></returns>
        public DataTable getAttendance(DateTime dDay, string sShift, string sDepartment)
        {
            DataSet ds = new DataSet();
            //Columns: Module -- MachineCount -- OK -- NG -- Output -- Target -- ProdHrs
            BLL.LMMSUserAttendanceTracking_BLL bllAttTrk = new LMMSUserAttendanceTracking_BLL();
             
            ds = bllAttTrk.getAttendance(dDay, sShift, sDepartment);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dtAttend = new DataTable();
                dtAttend.Columns.Add("Man Power");
                dtAttend.Columns.Add("Sup Te Leader");
                dtAttend.Columns.Add("Operator");
                dtAttend.Columns.Add("Attendance");
                dtAttend.Columns.Add("Annual Leave");
                dtAttend.Columns.Add("MC");
                dtAttend.Columns.Add("Absent");

                dtAttend.Columns.Add("UnPaid");
                dtAttend.Columns.Add("Maternity");
                dtAttend.Columns.Add("Marriage");
                dtAttend.Columns.Add("Compassionate");
                dtAttend.Columns.Add("ChildCare");
                dtAttend.Columns.Add("Business Trip");




                try
                {
                    int iManPower = 0;
                    int iSupTeLeader = 0;
                    int iOperator = 0;
                    int iAttendance = 0;
                    int iAnnualLeave = 0;
                    int iMCLeave = 0;
                    int iAbsent = 0;

                    int iUnpaid = 0;
                    int iMATERNITY = 0;
                    int iMARRIAGE = 0;
                    int iCOMPASSIONATE = 0;
                    int iCHILDCARE = 0;
                    int iBUSINESSTRIP = 0;

                    foreach (DataRow tmpDr in ds.Tables[0].Rows)
                    {
                        iManPower += int.Parse(tmpDr["UserCount"].ToString());
                        switch (tmpDr["Attendance"].ToString().ToUpper())
                        {
                            case "ATTENDANCE":
                                #region attendance
                                {
                                    switch (tmpDr["UserGroup"].ToString().ToUpper())
                                    {
                                        case "ADMIN":
                                            {
                                                break;
                                            }
                                        case "ENGINEER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "IPQC":
                                            {
                                                break;
                                            }
                                        case "LEADER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "MH":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "OPERATOR":
                                            {
                                                iOperator += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "SUPERVISOR":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "TECHNICIAN":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }

                                        case "CHECKER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }

                                        case "TEMPORARYSTAFF":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                    }
                                    break;
                                }
                            #endregion
                            case ("ANNUAL LEAVE"):
                                {
                                    iAnnualLeave += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MC"):
                                {
                                    iMCLeave += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("ABSENT"):
                                {
                                    iAbsent += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("UNPAID"):
                                {
                                    iUnpaid += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MATERNITY"):
                                {
                                    iMATERNITY += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MARRIAGE"):
                                {
                                    iMARRIAGE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("COMPASSIONATE"):
                                {
                                    iCOMPASSIONATE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("CHILDCARE"):
                                {
                                    iCHILDCARE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("BUSINESS TRIP"):
                                {
                                    iBUSINESSTRIP += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                        }
                             


                    }

                    DataRow dr = dtAttend.NewRow();

                    dr["Man Power"] = iManPower == 0 ? sAttdEmptyChar : iManPower.ToString();
                    dr["Sup Te Leader"] = iSupTeLeader == 0 ? sAttdEmptyChar : iSupTeLeader.ToString();
                    dr["Operator"] = iOperator == 0 ? sAttdEmptyChar : iOperator.ToString();
                    dr["Attendance"] = iAttendance == 0 ? sAttdEmptyChar : iAttendance.ToString();
                    dr["Annual Leave"] = iAnnualLeave == 0 ? sAttdEmptyChar : iAnnualLeave.ToString();
                    dr["MC"] = iMCLeave == 0 ? sAttdEmptyChar : iMCLeave.ToString();
                    dr["Absent"] = iAbsent == 0 ? sAttdEmptyChar : iAbsent.ToString();



                    dr["UnPaid"] = iUnpaid == 0 ? sAttdEmptyChar : iUnpaid.ToString();
                    dr["Maternity"] = iMATERNITY == 0 ? sAttdEmptyChar : iMATERNITY.ToString();
                    dr["Marriage"] = iMARRIAGE == 0 ? sAttdEmptyChar : iMARRIAGE.ToString();
                    dr["Compassionate"] = iCOMPASSIONATE == 0 ? sAttdEmptyChar : iCOMPASSIONATE.ToString();
                    dr["ChildCare"] = iCHILDCARE == 0 ? sAttdEmptyChar : iCHILDCARE.ToString();
                    dr["Business Trip"] = iBUSINESSTRIP == 0 ? sAttdEmptyChar : iBUSINESSTRIP.ToString();
                    



                    dtAttend.Rows.Add(dr);

                    return dtAttend;

                }
                catch (Exception)
                {
                    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSWatchLog_BLL", "Function:  internal DataSet getAttendance(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");
                    return dtAttend;
                }
            }
        }

   


        public DataTable getProductivityReportForLaser(DateTime dDay, string sShift, string sDepartment)
        {

            DataTable dtProduct = dal.getProductivityReportForLaser(dDay, sShift);
            

            //total testing time
            double Total_TestingTime = 0;
            for (int i = 1; i < 9; i++)
            {
                Common.BLL.LMMSEventLog_BLL bll = new LMMSEventLog_BLL();
                Total_TestingTime += bll.GetStatusTime(dDay, sShift, i.ToString(), StaticRes.Global.clsConstValue.ConstStatus.Testing);
            }


            #region attendance 
            
            DataTable dt = getAttendance(dDay, sShift, sDepartment);

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
            }

            //Rejection Rate
            double TotalRejRate = Total_ActualQty == 0? 0.00: Math.Round(Total_RejQty / (Total_ActualQty + Total_RejQty) * 100, 2);

            //Utilization
            double dUtilization = Math.Round((Total_ActualTime + Total_TestingTime) / 3600 / 96 * 100, 2);
            dUtilization = dUtilization > 100 ? 100 : dUtilization;
            string Utilization = Total_ActualTime+ Total_TestingTime == 0? "0.00%" : dUtilization.ToString() + "%";

            //Productivity 
            double dProductivity = Total_TargetQty == 0 ? 0.00 : Math.Round(Total_ActualQty / Total_TargetQty * 100, 2);
            string Productivity = dProductivity > 100? "100%": dProductivity.ToString() + "%";

            //dr total 
            DataRow dr_total = dtProduct.NewRow();
            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";
            dr_total["ActualQty"] = Total_ActualQty;
            dr_total["RejectQty"] = Total_RejQty;
            dr_total["TotalQty"] = Total_ActualQty + Total_RejQty;
            dr_total["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(Total_ActualTime / 3600, 2).ToString());
            dr_total["TargetQty"] = Total_TargetQty;
            dr_total["TargetHR"] = "96:00:00";

            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;
            dr_total["Utilization"] = Utilization;
            dr_total["Productivity"] = Productivity;
            dr_total["RejRate"] = TotalRejRate==0 ? "0.00%" : TotalRejRate.ToString() + "%";

            dtProduct.Rows.Add(dr_total);

            #endregion
            

            #region add all type
            List<string> listType = new List<string>();
            listType.Add(StaticRes.Global.ProductType.BUTTON);
            listType.Add(StaticRes.Global.ProductType.PRINT);
            listType.Add(StaticRes.Global.ProductType.PANEL);
            listType.Add(StaticRes.Global.ProductType.LENS);
            listType.Add(StaticRes.Global.ProductType.BZ_257B);
            listType.Add(StaticRes.Global.ProductType.BZ_320B);
            

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
                    dr["ActualHR"] = "00:00:00"; // 0;;
                    dr["TargetHR"] = "96:00:00"; // 12*8;
                    dr["RejRate"] = "0.00%";
                    dr["Utilization"] = Utilization;
                    dr["Productivity"] = Productivity;

                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {

                    drArr[0]["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["ActualHR"].ToString());
                    drArr[0]["TargetHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["TargetHR"].ToString());

                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;
                    drArr[0]["Utilization"] = Utilization;
                    drArr[0]["Productivity"] = Productivity;
                    
                }
            }

            DataRow drTesting = dtProduct.NewRow();
            drTesting["SN"] = GetSN("Project Testing");
            drTesting["ProductType"] = "Project Testing";
            drTesting["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(Total_TestingTime / 3600, 2).ToString());
            dtProduct.Rows.Add(drTesting.ItemArray);

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
                case StaticRes.Global.ProductType.PRINT:
                    SN = 2;
                    break;
                case StaticRes.Global.ProductType.LENS:
                    SN = 3;
                    break;
                case StaticRes.Global.ProductType.BZ_257B:
                    SN = 4;
                    break;
                case StaticRes.Global.ProductType.BZ_320B:
                    SN = 5;
                    break;
                case StaticRes.Global.ProductType.PANEL:
                    SN = 6;
                    break;
                case "Project Testing":
                    SN = 7;
                    break;
                case "Day Shift Total":
                    SN = 99;
                    break;
                case "Night Shift Total":
                    SN = 99;
                    break;
                default:
                    SN = 10;
                    break;

            }
            return SN;
        }





        public DataTable GetLaserRejButtonReport_NEW(string strWhere )
        {
            DataTable dt = dal.GetLaserRejForButtonReport_NEW(strWhere);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }








        #endregion




        public DataTable GetJobMaterialList(string sJobNo)
        {
            DataSet ds = dal.GetJobMaterialList(sJobNo);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetMaterialListForAllSectionReport(DateTime  dStartTime)
        {
            DataSet ds = dal.GetMaterialListForAllSectionReport(dStartTime);
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