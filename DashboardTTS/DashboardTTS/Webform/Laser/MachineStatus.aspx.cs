using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace DashboardTTS.Webform
{
    public partial class MachineStatus : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SearchParaCls sp = new SearchParaCls();

             
                try
                {
                    Session["SP"] = sp;

                    ucMachineStatus1.MachineNo = "1";
                    ucMachineStatus2.MachineNo = "2";
                    ucMachineStatus3.MachineNo = "3";
                    ucMachineStatus4.MachineNo = "4";
                    ucMachineStatus5.MachineNo = "5";
                    ucMachineStatus6.MachineNo = "6";
                    ucMachineStatus7.MachineNo = "7";
                    ucMachineStatus8.MachineNo = "8";


                    btnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                    sp.dateFrom = DateTime.Now.Date;
                    sp.dateTo = DateTime.Now.Date;
                    sp.PartNo = "";
                    sp.MachineID = "8";
                    sp.JobID = "";
                }

            }
        }
        

        private class SearchParaCls
        {
            public string PartNo;
            public string MachineID;
            public DateTime dateFrom;
            public DateTime dateTo;
            public string JobID;

        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try { ShowEachChart("1"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 1 : " + ex.ToString()); }
            try { ShowEachChart("2"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 2 : " + ex.ToString()); }
            try { ShowEachChart("3"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 3 : " + ex.ToString()); }
            try { ShowEachChart("4"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 4 : " + ex.ToString()); }
            try { ShowEachChart("5"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 5 : " + ex.ToString()); }
            try { ShowEachChart("6"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 6 : " + ex.ToString()); }
            try { ShowEachChart("7"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 7 : " + ex.ToString()); }
            try { ShowEachChart("8"); } catch (Exception ex) { DBHelp.Reports.LogFile.Log("MachineStatus ", "btnGenerate_Click show chart 8 : " + ex.ToString()); }


            try
            {
                //  JobList info
                Common.BLL.LMMSWatchLog_BLL WatchLog = new Common.BLL.LMMSWatchLog_BLL();
                List<Common.Model.LMMSWatchLog_Model> lJobList = new List<Common.Model.LMMSWatchLog_Model>();
                lJobList = WatchLog.GetQty_CurrentnToday();


                // Get Set Main Info
                Common.BLL.LMMSWatchLog_BLL bll = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dt = bll.GetDayOutput(DateTime.Now.Date);
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lbOutput.Text = "0";
                    this.lbRejQty.Text = "0";
                    this.lbRejRate.Text = "0.00%";
                }
                else
                {
                    double totalOutput_PCS = int.Parse(dt.Rows[0]["TotalOuput"].ToString());
                    double totalOutput_SET = int.Parse(dt.Rows[0]["TotalSet"].ToString());
                    double totalRej = int.Parse(dt.Rows[0]["TotalRej"].ToString());

                    this.lbOutput.Text = string.Format("{0:N0} ({1:N0})", totalOutput_SET, totalOutput_PCS);
                    this.lbRejQty.Text = totalRej.ToString();
                    this.lbRejRate.Text = Math.Round(totalRej / totalOutput_PCS * 100, 2).ToString("0.00") + "%";
                }





                // utilization
                DataTable dt_Utilization = new DataTable();
                #region Utilization
                dt_Utilization.Columns.Add("MachineID");
                dt_Utilization.Columns.Add("PD");
                dt_Utilization.Columns.Add("Idle");
                dt_Utilization.Columns.Add("NoWIP");
                dt_Utilization.Columns.Add("Adjustment");
                dt_Utilization.Columns.Add("ShutDown");
                dt_Utilization.Columns.Add("Testing");
                dt_Utilization.Columns.Add("BreakDown");
                dt_Utilization.Columns.Add("Setup");
                dt_Utilization.Columns.Add("Maintainence");
                dt_Utilization.Columns.Add("Buyoff");


                Common.BLL.LMMSEventLog_BLL EventLog = new Common.BLL.LMMSEventLog_BLL();
                Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();


                for (int i = 1; i < 9; i++) // 8 machine data
                {
                    DataRow dr = dt_Utilization.NewRow();

                    dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                    dPoints = EventLog.getOEE(DateTime.Now.Date, DateTime.Now.Date, i.ToString(), "para_sPartNo_notuse", "", "", false);

                    if (dPoints == null || dPoints.Count == 0)
                    {
                        dr["MachineID"] = "Machine" + i.ToString();
                        dr["PD"] = 0;
                        dr["Idle"] = 0;
                        dr["NoWIP"] = 0;
                        dr["Adjustment"] = 0;
                        dr["Testing"] = 0;
                        dr["ShutDown"] = 0;
                        dr["BreakDown"] = 0;
                        dr["Setup"] = 0;
                        dr["Maintainence"] = 0;
                        dr["Buyoff"] = 0;
                    }
                    else
                    {
                        double PD_Time_Count = 0;
                        double Idle_Time_Count = 0;
                        double NoWIP_Time_Count = 0;
                        double Adjustmnet_Time_Count = 0;
                        double Shutdown_Time_Count = 0;
                        double Testing_Time_Count = 0;
                        double BreakDown_Time_Count = 0;

                        double Setup_Time_Count = 0;
                        double Maintainence_Time_Count = 0;
                        double Buyoff_Time_Count = 0;


                        #region  catogery the points
                        foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> pPoint in dPoints)
                        {
                            try
                            {
                                switch (pPoint.Value)
                                {
                                    case StaticRes.Global.StatusType.Adjustment:
                                        {
                                            Adjustmnet_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.Idle:
                                        {
                                            Idle_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.NoWIP:
                                        {
                                            NoWIP_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.PD:
                                        {
                                            PD_Time_Count++;   //will be reset 
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.BreakDown:
                                        {
                                            BreakDown_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.Testing:
                                        {
                                            Testing_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.ShutDown:
                                        {
                                            Shutdown_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.Setup:
                                        {
                                            Setup_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.Maintance:
                                        {
                                            Maintainence_Time_Count++;
                                            break;
                                        }
                                    case StaticRes.Global.StatusType.Buyoff:
                                        {
                                            Buyoff_Time_Count++;
                                            break;
                                        }
                                    default:
                                        {
                                            PD_Time_Count++;   //will be reset 
                                            break;
                                        }
                                }
                            }
                            catch (Exception ee)
                            {
                                
                            }
                        }
                        #endregion

                        dr["MachineID"] = "Machine" + i.ToString();
                        dr["PD"] = Math.Round(PD_Time_Count / 60, 2);
                        dr["Idle"] = Math.Round(Idle_Time_Count / 60, 2);
                        dr["NoWIP"] = Math.Round(NoWIP_Time_Count / 60, 2);
                        dr["Adjustment"] = Math.Round(Adjustmnet_Time_Count / 60, 2);
                        dr["ShutDown"] = Math.Round(Shutdown_Time_Count / 60, 2);
                        dr["Testing"] = Math.Round(Testing_Time_Count / 60, 2);
                        dr["BreakDown"] = Math.Round(BreakDown_Time_Count / 60, 2);
                        dr["Setup"] = Math.Round(Setup_Time_Count / 60, 2);
                        dr["Maintainence"] = Math.Round(Maintainence_Time_Count / 60, 2);
                        dr["Buyoff"] = Math.Round(Buyoff_Time_Count / 60, 2);
                    }


                    dt_Utilization.Rows.Add(dr);

                }
                #endregion


                TimeSpan ts = DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(8);
                double totalHours = ts.TotalSeconds / 3600;

                Dictionary<string, double> dicUtilization = new Dictionary<string, double>();
                double totalRunningTime = 0;

                foreach (DataRow dr in dt_Utilization.Rows)
                {
                    string machineID = dr["MachineID"].ToString().Replace("Machine", "").Trim();
                    double runningTime = double.Parse(dr["PD"].ToString());


                    totalRunningTime += runningTime;
                    dicUtilization.Add(machineID, Math.Round(runningTime/totalHours*100,2));
                }

                this.lbUtilization.Text = Math.Round(totalRunningTime / totalHours / 8 * 100, 2).ToString("0.00") + "%";
                

                SetCurrentJobInfo(lJobList, dicUtilization);
            }
            catch (Exception ex)
            { DBHelp.Reports.LogFile.Log("MachineStatus", "btnGenerate_Click Set Current Job Info : " + ex.ToString()); }
        }



        void ShowEachChart(string sMachineNo)
        {
            DateTime dTimeFrom = DateTime.Now.Date;
            DateTime dTimeTo = DateTime.Now.Date;
            string sPartNo = "";
      

            SearchParaCls sp = new SearchParaCls();
            sp.dateFrom = dTimeFrom;
            sp.dateTo = dTimeTo;
            sp.MachineID = sMachineNo;
            sp.PartNo = "";

            Session["SP"] = sp;

            //Machine Status
            Common.BLL.LMMSEventLog_BLL EventLog = new Common.BLL.LMMSEventLog_BLL();
            StaticRes.Global.StatusType eMachineStatus = EventLog.getCurrentStatus(dTimeFrom, dTimeTo, sMachineNo, sPartNo);
            

            SetStatus(eMachineStatus, sp);
        }

        void SetStatus(StaticRes.Global.StatusType eMachineStatus, SearchParaCls sp)
        {
            try
            {
                GetControl(sp.MachineID).Visible = true;
                GetControl(sp.MachineID).MachineNo = sp.MachineID;
                
                switch (eMachineStatus)
                {
                    case (StaticRes.Global.StatusType.Adjustment):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Adjustment;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Adjustment);
                            break;
                        }
                    case (StaticRes.Global.StatusType.BreakDown):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Down;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Down);
                            break;
                        }
                    case (StaticRes.Global.StatusType.Idle):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Idle;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Idle);
                            break;
                        }
                    case (StaticRes.Global.StatusType.NoWIP):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.NoWIP;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.NoWIP);
                            break;
                        }
                    case (StaticRes.Global.StatusType.PD):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Run;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Run);
                            break;
                        }
                    case (StaticRes.Global.StatusType.ShutDown):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.ShutDown;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.ShutDown);
                            break;
                        }
                    case (StaticRes.Global.StatusType.Testing):
                        {
                            //Testing
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Testing;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Testing);
                            break;
                        }
                    case (StaticRes.Global.StatusType.Maintance):
                        {
                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Maintance;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Maintance);
                            break;
                        }
                    case (StaticRes.Global.StatusType.Setup):
                        {

                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Setup;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Setup);
                            break;
                        }
                    case (StaticRes.Global.StatusType.Buyoff):
                        {

                            GetControl(sp.MachineID).Status = UserControl.WebUserControlMachineStatus.MachineStatus.Buyoff;
                            //SetStatusList(sp.MachineID, UserControl.WebUserControlMachineStatus.MachineStatus.Buyoff);
                            break;
                        }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineStatus", "SetStatus exception : " + ee.Message);
            }
        }
   
        UserControl.WebUserControlMachineStatus GetControl(string sMachineID)
        {
            if (sMachineID == "1")
            {
                 return ucMachineStatus1;
            }
            else if (sMachineID == "2")
            {
                return ucMachineStatus2;
            }
            else if (sMachineID == "3")
            {
                return ucMachineStatus3;
            }
            else if (sMachineID == "4")
            {
                return ucMachineStatus4;
            }
            else if (sMachineID == "5")
            {
                return ucMachineStatus5;
            }
            else if (sMachineID == "6")
            {
                return ucMachineStatus6;
            }
            else if (sMachineID == "7")
            {
                return ucMachineStatus7;
            }
            else if(sMachineID == "8")
            {
                return ucMachineStatus8;
            }else
            {
                return null;
            }
        }

      

        void SetCurrentJobInfo(List<Common.Model.LMMSWatchLog_Model> IJobList, Dictionary<string,double> dicUtilization)
        {
            foreach (Common.Model.LMMSWatchLog_Model model in IJobList)
            {
                UserControl.WebUserControlMachineStatus uControl = GetControl(model.machineID.Replace("Machine", "").Trim());

                uControl.LotNo = model.lotNo;
                uControl.JobID = model.jobNumber;
                uControl.PartNo = model.partNumber;
                uControl.OkQtyCurrent = model.totalPass.Value;
                uControl.OkQtyToday = model.totalPass.Value;
                uControl.NgQtyCurrent = model.totalFail.Value;
                uControl.NgQtyToday = model.totalFail.Value;
                uControl.TotalQtyCurrent = model.totalQuantity.Value;
                uControl.TotalQtyToday = model.totalQuantity.Value;
                uControl.Utilization = dicUtilization[model.machineID.Replace("Machine", "").Trim()];

                if (model.totalQuantity == 0)
                {
                    uControl.Rejrate = "0.00";
                    uControl.RejPPM = 0;
                }
                else
                {
                    double rejrate = double.Parse(model.totalFail.Value.ToString()) / double.Parse(model.totalQuantity.Value.ToString()) * 100;// 
                    uControl.Rejrate = Math.Round(rejrate, 2).ToString("f2");
                    uControl.RejPPM = Math.Round(rejrate * 10000, 0);
                }
            }
        }

    }
}