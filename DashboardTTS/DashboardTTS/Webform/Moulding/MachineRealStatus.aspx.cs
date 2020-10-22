using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Molding
{
    public partial class MachineRealStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt_Status = new DataTable();
                    DataTable dt_Prod = new DataTable();
                    DataTable dt_MainInfo = new DataTable();


                    // Get Status
                    try
                    {
                        Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus = new Common.Class.BLL.MouldingMachineStatus_BLL();
                        dt_Status = MachineStatus.GetCurrentStatus();
                    }
                    catch (Exception ee1)
                    {
                        DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Get Status Exception:" + ee1.ToString());
                        dt_Status = null;
                    }


                    // Get Current production info.
                    try
                    {
                        Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
                        dt_Prod = bll.SelectList(DateTime.Now.Date.AddHours(8),DateTime.Now.Date.AddHours(8));
                    }
                    catch (Exception ee2)
                    {
                        DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Get Current production info. Exception:" + ee2.ToString());
                        dt_Prod = null;
                    }

                    //Get & Set Main info
                    try
                    {
                        double totalOutput = 0;
                        double totalRej = 0;

                        Common.Class.BLL.MouldingViDefectTracking_BLL bll = new Common.Class.BLL.MouldingViDefectTracking_BLL();
                        DataTable dt = bll.GetDayOuput(DateTime.Now.Date);

                        if (dt == null || dt.Rows.Count  == 0)
                        {
                            this.lbTotalOutput.Text = "0";
                            this.lbTotalRejQty.Text = "0";
                            this.lbTotalRejRate.Text = "0.00%";
                        }
                        else
                        {
                            totalOutput = int.Parse(dt.Rows[0]["TotalOuput"].ToString());
                            totalRej = int.Parse(dt.Rows[0]["TotalRej"].ToString());

                            this.lbTotalOutput.Text = totalOutput.ToString();
                            this.lbTotalRejQty.Text = totalRej.ToString();
                            this.lbTotalRejRate.Text = Math.Round(totalRej / totalOutput * 100, 2).ToString("0.00") + "%";
                        }
                    }
                    catch (Exception ee3)
                    {
                        DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Get Main info. Exception:" + ee3.ToString());
                        dt_MainInfo = null;
                    }



                    #region Get Utilization
                      
                    DataTable dtStatus = new DataTable();
                    dtStatus.Columns.Add("MachineID");
                    dtStatus.Columns.Add("Running");
                    dtStatus.Columns.Add("Adjustment");
                    dtStatus.Columns.Add("NoWIP");
                    dtStatus.Columns.Add("Mould_Testing");
                    dtStatus.Columns.Add("Material_Testing");
                    dtStatus.Columns.Add("Change_Model");
                    dtStatus.Columns.Add("No_Operator");
                    dtStatus.Columns.Add("No_Material");
                    dtStatus.Columns.Add("Break_Time");
                    dtStatus.Columns.Add("ShutDown");
                    dtStatus.Columns.Add("Damage_Mould");
                    dtStatus.Columns.Add("Machine_Break");


                    Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus_bll = new Common.Class.BLL.MouldingMachineStatus_BLL();
                    Dictionary<DateTime, StaticRes.Global.StatusType> Points;
                    for (int i = 1; i < 9; i++)
                    {
                        DataRow dr = dtStatus.NewRow();

                        Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        Points = MachineStatus_bll.getOEE(DateTime.Now.Date.AddHours(8), DateTime.Now.Date.AddHours(8), i.ToString(), "", "", false);//2018 12 04  by wei lijia , add date not in & except weekend


                        if (Points == null || Points.Count == 0)
                        {
                            #region default 0
                            dr["MachineID"] = "Machine" + i.ToString();
                            dr["Running"] = 0;
                            dr["Adjustment"] = 0;
                            dr["NoWIP"] = 0;
                            dr["Mould_Testing"] = 0;
                            dr["Material_Testing"] = 0;
                            dr["Change_Model"] = 0;
                            dr["No_Operator"] = 0;
                            dr["No_Material"] = 0;
                            dr["Break_Time"] = 0;
                            dr["ShutDown"] = 0;
                            dr["Damage_Mould"] = 0;
                            dr["Machine_Break"] = 0;
                            #endregion
                        }
                        else
                        {
                            double Running_Count = 0;
                            double Adjustment_Count = 0;
                            double NoWIP_Count = 0;
                            double Mould_Testing_Count = 0;
                            double Material_Testing_Count = 0;
                            double Change_Model_Count = 0;
                            double No_Operator_Count = 0;
                            double No_Material_Count = 0;
                            double Break_Time_Count = 0;
                            double ShutDown_Count = 0;
                            double Damage_Mould_Count = 0;
                            double Machine_Break_Count = 0;

                            #region  catogery the points
                            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> pPoint in Points)
                            {
                                try
                                {
                                    switch (pPoint.Value)
                                    {
                                        case StaticRes.Global.StatusType.Running:
                                            {
                                                Running_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.Adjustment:
                                            {
                                                Adjustment_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.No_Schedule:
                                            {
                                                NoWIP_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.Mould_Testing:
                                            {
                                                Mould_Testing_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.Material_Testing:
                                            {
                                                Material_Testing_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.Change_Model:
                                            {
                                                Change_Model_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.No_Operator:
                                            {
                                                No_Operator_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.No_Material:
                                            {
                                                No_Material_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.Break_Time:
                                            {
                                                Break_Time_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.DamageMould:
                                            {
                                                Damage_Mould_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.ShutDown:
                                            {
                                                ShutDown_Count++;
                                                break;
                                            }
                                        case StaticRes.Global.StatusType.MachineBreak:
                                            {
                                                Machine_Break_Count++;
                                                break;
                                            }
                                        default:
                                            {
                                                Running_Count++;
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
                            dr["Running"] = Math.Round(Running_Count / 60, 2);
                            dr["Adjustment"] = Math.Round(Adjustment_Count / 60, 2);
                            dr["NoWIP"] = Math.Round(NoWIP_Count / 60, 2);
                            dr["Mould_Testing"] = Math.Round(Mould_Testing_Count / 60, 2);
                            dr["Material_Testing"] = Math.Round(Material_Testing_Count / 60, 2);
                            dr["Change_Model"] = Math.Round(Change_Model_Count / 60, 2);
                            dr["No_Operator"] = Math.Round(No_Operator_Count / 60, 2);
                            dr["No_Material"] = Math.Round(No_Material_Count / 60, 2);
                            dr["Break_Time"] = Math.Round(Break_Time_Count / 60, 2);
                            dr["ShutDown"] = Math.Round(ShutDown_Count / 60, 2);
                            dr["Damage_Mould"] = Math.Round(Damage_Mould_Count / 60, 2);
                            dr["Machine_Break"] = Math.Round(Machine_Break_Count / 60, 2);
                        }

                        dtStatus.Rows.Add(dr);
                    }

                    #endregion

                    
                    //Set each machine utilization & total utilization
                    TimeSpan ts = DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(8);
                    double totalHours = ts.TotalSeconds / 3600;


                    double totalRunningTime = 0;
                    Dictionary<string, double> dicUtilization = new Dictionary<string, double>();


                    foreach (DataRow dr in dtStatus.Rows)
                    {
                        string machineID = dr["MachineID"].ToString().Replace("Machine", "").Trim();
                        double runningTime = double.Parse(dr["Running"].ToString());
                        
                        totalRunningTime += runningTime;
                        
                        dicUtilization.Add(machineID, Math.Round(runningTime / totalHours *100, 2));
                    }

                    this.lbUtilization.Text = Math.Round(totalRunningTime / totalHours / 8 * 100, 2).ToString("0.00") + "%";


                    


                    ShowChart(dt_Status,dt_Prod, dicUtilization);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Exception:" + ee.ToString());
            }
        }

       

        void ShowChart(DataTable dtStatus, DataTable dtProduction, Dictionary<string,double> dicUtilization)
        {
            try
            {

                #region Set Machine Status
                if (dtStatus == null || dtStatus.Rows.Count == 0)
                {
                    //default status  shutdown
                    for (int i = 1; i < 10; i++)
                    {
                        SetMachineStatus(i.ToString(), (UserControl.WebUserControlMouldingMachineStatus.MachineStatus)StaticRes.Global.StatusType.ShutDown,StaticRes.Global.clsConstValue.ConstMouldingStatus.ShutDown);
                    }
                }
                else
                {
                    foreach (DataRow row in dtStatus.Rows)
                    {
                        string MachineID = row["MachineID"].ToString();
                        MachineID = MachineID.Replace("Machine", "");
                        string Status = row["MachineStatus"].ToString();

                        SetMachineStatus(MachineID, ConvertStatus(Status), Status);
                    }
                }

                #endregion


                #region  Set Current Machine Production Info.
                if (dtProduction == null || dtProduction.Rows.Count == 0)
                {
                    //no record   default info 
                    for (int i = 1; i < 10; i++)
                    {
                        SetMachineInfo(i.ToString(), null, 0);
                    }
                }
                else
                {
                    foreach (DataRow dr in dtProduction.Rows)
                    {
                        string MachineID = dr["MachineID"].ToString().Replace("Machine", "").Trim();

                        SetMachineInfo(MachineID, dr, dicUtilization[MachineID]);
                    }
                }
                #endregion


                ucMachineStatus9.Status = DashboardTTS.UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Under_Dev;
              

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "ShowChart Exception:" + ee.ToString());
            }
        }

        

        void SetMachineInfo(string machineID, DataRow dr, double Utilization)
        {
            string partNumber = "";
            string JigNo = "";
            string Model = "";
            int OkQtyCurrent = 0;
            int NgQtyCurrent = 0;
            int TotalQtyCurrent = 0;
            string RejRate = "";
            double RejPPM = 0;


            if (dr == null)
            {
                partNumber = "";
                JigNo = "";
                Model = "";
                RejRate = "";

            }
            else
            {
                partNumber = dr["partNumber"].ToString();
                JigNo = dr["jigNo"].ToString();
                Model = dr["model"].ToString();
                RejRate = dr["RejRate"].ToString().Trim('%');
            }

            


            try
            {
                RejPPM = double.Parse(dr["RejPPM"].ToString());
            }
            catch  { RejPPM = 0; }
         
            try
            {
                OkQtyCurrent = int.Parse(dr["OK"].ToString());
            }
            catch { OkQtyCurrent = 0; }

            try
            {
                NgQtyCurrent = int.Parse(dr["NG"].ToString());
            }
            catch { NgQtyCurrent = 0; }

            TotalQtyCurrent = OkQtyCurrent + NgQtyCurrent;


            switch (machineID)
            {
                case "1":
                    ucMachineStatus1.MachineNo = "Machine " + machineID;
                    ucMachineStatus1.PartNo = partNumber;
                    ucMachineStatus1.Model = Model;
                    ucMachineStatus1.JigNo = JigNo;
                    ucMachineStatus1.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus1.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus1.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus1.Rejrate = RejRate;
                    ucMachineStatus1.RejPPM = RejPPM;
                    ucMachineStatus1.Utilization = Utilization;
                    break;
                case "2":
                    ucMachineStatus2.MachineNo = "Machine " + machineID;
                    ucMachineStatus2.PartNo = partNumber;
                    ucMachineStatus2.Model = Model;
                    ucMachineStatus2.JigNo = JigNo;
                    ucMachineStatus2.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus2.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus2.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus2.Rejrate = RejRate;
                    ucMachineStatus2.RejPPM = RejPPM;
                    ucMachineStatus2.Utilization = Utilization;
                    break;
                case "3":
                    ucMachineStatus3.MachineNo = "Machine " + machineID;
                    ucMachineStatus3.PartNo = partNumber;
                    ucMachineStatus3.Model = Model;
                    ucMachineStatus3.JigNo = JigNo;
                    ucMachineStatus3.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus3.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus3.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus3.Rejrate = RejRate;
                    ucMachineStatus3.RejPPM = RejPPM;
                    ucMachineStatus3.Utilization = Utilization;
                    break;
                case "4":
                    ucMachineStatus4.MachineNo = "Machine " + machineID;
                    ucMachineStatus4.PartNo = partNumber;
                    ucMachineStatus4.Model = Model;
                    ucMachineStatus4.JigNo = JigNo;
                    ucMachineStatus4.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus4.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus4.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus4.Rejrate = RejRate;
                    ucMachineStatus4.RejPPM = RejPPM;
                    ucMachineStatus4.Utilization = Utilization;
                    break;
                case "5":
                    ucMachineStatus5.MachineNo = "Machine " + machineID;
                    ucMachineStatus5.PartNo = partNumber;
                    ucMachineStatus5.Model = Model;
                    ucMachineStatus5.JigNo = JigNo;
                    ucMachineStatus5.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus5.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus5.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus5.Rejrate = RejRate;
                    ucMachineStatus5.RejPPM = RejPPM;
                    ucMachineStatus5.Utilization = Utilization;
                    break;
                case "6":
                    ucMachineStatus6.MachineNo = "Machine " + machineID;
                    ucMachineStatus6.PartNo = partNumber;
                    ucMachineStatus6.Model = Model;
                    ucMachineStatus6.JigNo = JigNo;
                    ucMachineStatus6.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus6.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus6.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus6.Rejrate = RejRate;
                    ucMachineStatus6.RejPPM = RejPPM;
                    ucMachineStatus6.Utilization = Utilization;
                    break;
                case "7":
                    ucMachineStatus7.MachineNo = "Machine " + machineID;
                    ucMachineStatus7.PartNo = partNumber;
                    ucMachineStatus7.Model = Model;
                    ucMachineStatus7.JigNo = JigNo;
                    ucMachineStatus7.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus7.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus7.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus7.Rejrate = RejRate;
                    ucMachineStatus7.RejPPM = RejPPM;
                    ucMachineStatus7.Utilization = Utilization;
                    break;
                case "8":
                    ucMachineStatus8.MachineNo = "Machine " + machineID;
                    ucMachineStatus8.PartNo = partNumber;
                    ucMachineStatus8.Model = Model;
                    ucMachineStatus8.JigNo = JigNo;
                    ucMachineStatus8.OkQtyCurrent = OkQtyCurrent;
                    ucMachineStatus8.NgQtyCurrent = NgQtyCurrent;
                    ucMachineStatus8.TotalQtyCurrent = TotalQtyCurrent;
                    ucMachineStatus8.Rejrate = RejRate;
                    ucMachineStatus8.RejPPM = RejPPM;
                    ucMachineStatus8.Utilization = Utilization;
                    break;
                case "9":
                    ucMachineStatus9.MachineNo = "Machine " + machineID;
                    ucMachineStatus9.PartNo = "";
                    ucMachineStatus9.Model = "";
                    ucMachineStatus9.JigNo = "";
                    ucMachineStatus9.OkQtyCurrent = 0;
                    ucMachineStatus9.NgQtyCurrent = 0;
                    ucMachineStatus9.TotalQtyCurrent = 0; ;
                    break;


                default:
                    break;
            }

           
        }


        void SetMachineStatus(string machineID, UserControl.WebUserControlMouldingMachineStatus.MachineStatus enStatus, string sStatus)
        {
            switch (machineID)
            {
                case "1":
                    ucMachineStatus1.Status = enStatus;
                    //this.lb_machine1.Text = SpecialConvert(sStatus);
                    //div_1.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "2":
                    ucMachineStatus2.Status = enStatus;
                    //this.lb_machine2.Text = SpecialConvert(sStatus);
                    //div_2.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "3":
                    ucMachineStatus3.Status = enStatus;
                    //this.lb_machine3.Text = SpecialConvert(sStatus);
                    //div_3.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "4":
                    ucMachineStatus4.Status = enStatus;
                    //this.lb_machine4.Text = SpecialConvert(sStatus);
                    //div_4.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "5":
                    ucMachineStatus5.Status = enStatus;
                    //this.lb_machine5.Text = SpecialConvert(sStatus);
                    //div_5.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "6":
                    ucMachineStatus6.Status = enStatus;
                    //this.lb_machine6.Text = SpecialConvert(sStatus);
                    //div_6.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "7":
                    ucMachineStatus7.Status = enStatus;
                    //this.lb_machine7.Text = SpecialConvert(sStatus);
                    //div_7.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "8":
                    ucMachineStatus8.Status = enStatus;
                    //this.lb_machine8.Text = SpecialConvert(sStatus);
                    //div_8.Style.Add("background-color", getColor(enStatus).Name);
                    break;
                case "9":
                    ucMachineStatus9.Status = enStatus;
                    break;

                default:
                    break;
            }


        }

        private string SpecialConvert(string str)
        {
            if (str == StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Schedule)
            {
                str = "No Schedule";
            }
            else if (str == StaticRes.Global.clsConstValue.ConstMouldingStatus.Login_Out)
            {
                str = "Changing Shift";
            }
            else if (str == StaticRes.Global.clsConstValue.ConstMouldingStatus.DamageMould)
            {
                str = "Mould Damage";
            }

            return str;
        }


        UserControl.WebUserControlMouldingMachineStatus.MachineStatus ConvertStatus(string sStatus)
        {

            UserControl.WebUserControlMouldingMachineStatus.MachineStatus enStatus = new UserControl.WebUserControlMouldingMachineStatus.MachineStatus();

            if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Running)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Running;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Adjustment)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Adjustment;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Schedule)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Schedule;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Mould_Testing)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Mould_Testing;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Material_Testing)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Material_Testing;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Change_Model)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Change_Model;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Operator)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Operator;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Material)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Material;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Break_Time)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Break_Time;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.ShutDown)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.ShutDown;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.Login_Out)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Login_Out;
            }
            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.DamageMould)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Damage_Mould;
            }

            else if (sStatus == StaticRes.Global.clsConstValue.ConstMouldingStatus.MachineBreak)
            {
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Machine_Break;
            }
            else
            {
                //unknown status   reset shutdown
                enStatus = UserControl.WebUserControlMouldingMachineStatus.MachineStatus.ShutDown;
                DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus_Debug", "unknown status:" + sStatus);
            }

            return enStatus;
        }

        System.Drawing.Color getColor(UserControl.WebUserControlMouldingMachineStatus.MachineStatus status)
        {
            if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Running)
            {
                return StaticRes.MouldingStatusColor.Running;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Adjustment)
            {
                return StaticRes.MouldingStatusColor.Adjustment;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Schedule)
            {
                return StaticRes.MouldingStatusColor.No_Schedule;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Mould_Testing)
            {
                return StaticRes.MouldingStatusColor.Mould_Testing;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Material_Testing)
            {
                return StaticRes.MouldingStatusColor.Material_Testing;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Change_Model)
            {
                return StaticRes.MouldingStatusColor.Change_Model;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Operator)
            {
                return StaticRes.MouldingStatusColor.No_Operator;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.No_Material)
            {
                return StaticRes.MouldingStatusColor.No_Material;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Break_Time)
            {
                return StaticRes.MouldingStatusColor.Break_Time;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.ShutDown)
            {
                return StaticRes.MouldingStatusColor.ShutDown;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Login_Out)
            {
                return StaticRes.MouldingStatusColor.Login_Out;
            }
            else if (status == UserControl.WebUserControlMouldingMachineStatus.MachineStatus.Machine_Break)
            {
                return StaticRes.MouldingStatusColor.MachineBreak;
            }
            else
            {
                return System.Drawing.Color.Gray;
            }

        }
    }
}