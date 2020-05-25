using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform
{
    public partial class MouldingOEETimeBar : System.Web.UI.Page
    {
        private class SearchParaCls
        {
            public string PartNo;
            public string MachineID;
            public DateTime dateFrom;
            public DateTime dateTo;
            public string JobID;
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SearchParaCls sp = new SearchParaCls();

                try
                {

                    #region label text color

                    this.lb_Color_Running.ForeColor = System.Drawing.Color.Green;// StaticRes.MouldingStatusColor.Running;
                    this.lb_Color_Running.BackColor = System.Drawing.Color.Green;// StaticRes.MouldingStatusColor.Running;
                    this.lb_Text_Running.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Running;

                    this.lb_Color_Adjustment.ForeColor = StaticRes.MouldingStatusColor.Adjustment;
                    this.lb_Color_Adjustment.BackColor = StaticRes.MouldingStatusColor.Adjustment;
                    this.lb_Text_Adjustment.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Adjustment;

                    this.lb_Color_No_Schedule.ForeColor = StaticRes.MouldingStatusColor.No_Schedule;
                    this.lb_Color_No_Schedule.BackColor = StaticRes.MouldingStatusColor.No_Schedule;
                    this.lb_Text_No_Schedule.Text = "No Schedule";// StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Schedule;

                    this.lb_Color_Mould_Testing.ForeColor = StaticRes.MouldingStatusColor.Mould_Testing;
                    this.lb_Color_Mould_Testing.BackColor = StaticRes.MouldingStatusColor.Mould_Testing;
                    this.lb_Text_Mould_Testing.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Mould_Testing;

                    this.lb_Color_Material_Testing.ForeColor = StaticRes.MouldingStatusColor.Material_Testing;
                    this.lb_Color_Material_Testing.BackColor = StaticRes.MouldingStatusColor.Material_Testing;
                    this.lb_Text_Material_Testing.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Material_Testing;

                    this.lb_Color_Change_Model.ForeColor = StaticRes.MouldingStatusColor.Change_Model;
                    this.lb_Color_Change_Model.BackColor = StaticRes.MouldingStatusColor.Change_Model;
                    this.lb_Text_Change_Model.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Change_Model;

                    this.lb_Color_No_Operator.ForeColor = StaticRes.MouldingStatusColor.No_Operator;
                    this.lb_Color_No_Operator.BackColor = StaticRes.MouldingStatusColor.No_Operator;
                    this.lb_Text_No_Operator.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Operator;

                    this.lb_Color_No_Material.ForeColor = StaticRes.MouldingStatusColor.No_Material;
                    this.lb_Color_No_Material.BackColor = StaticRes.MouldingStatusColor.No_Material;
                    this.lb_Text_No_Material.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Material;

                    this.lb_Color_Break_Time.ForeColor = StaticRes.MouldingStatusColor.Break_Time;
                    this.lb_Color_Break_Time.BackColor = StaticRes.MouldingStatusColor.Break_Time;
                    this.lb_Text_Break_Time.Text = "Meal";// StaticRes.Global.clsConstValue.ConstMouldingStatus.Break_Time;

                    this.lb_Color_ShutDown.ForeColor = StaticRes.MouldingStatusColor.ShutDown;
                    this.lb_Color_ShutDown.BackColor = StaticRes.MouldingStatusColor.ShutDown;
                    this.lb_Text_ShutDown.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.ShutDown;

                    this.lb_Color_Login_Out.ForeColor = StaticRes.MouldingStatusColor.Login_Out;
                    this.lb_Color_Login_Out.BackColor = StaticRes.MouldingStatusColor.Login_Out;
                    this.lb_Text_Login_Out.Text = "Changing Shift";//StaticRes.Global.clsConstValue.ConstMouldingStatus.Login_Out;

                    this.lb_Color_BreakDown.ForeColor = StaticRes.MouldingStatusColor.MachineBreak;
                    this.lb_Color_BreakDown.BackColor = StaticRes.MouldingStatusColor.MachineBreak;
                    this.lb_Text_BreakDown.Text = "Machine Break Down";// StaticRes.Global.clsConstValue.ConstMouldingStatus.MachineBreak;
                    #endregion


                    this.lblUserHeader.Text = "Moulding Operation Status ";
                    this.WebUserControlTimeBar1.Visible = false;
                    this.WebUserControlTimeBar2.Visible = false;
                    this.WebUserControlTimeBar3.Visible = false;
                    this.WebUserControlTimeBar4.Visible = false;
                    this.WebUserControlTimeBar5.Visible = false;
                    this.WebUserControlTimeBar6.Visible = false;
                    this.WebUserControlTimeBar7.Visible = false;
                    this.WebUserControlTimeBar8.Visible = false;
                    this.WebUserControlTimeBar9.Visible = false;

                    this.infDchFrom.Value = DateTime.Now.Date;
                    infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.Date;
                    this.infDchTo.Value = DateTime.Now.Date;
                    infDchTo.CalendarLayout.SelectedDate = DateTime.Now.Date;


                    btnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = infDchFrom.CalendarLayout.SelectedDate;
                DateTime dTimeTo = infDchTo.CalendarLayout.SelectedDate;
                string sMachineNo = ddlMachineID.SelectedItem.Value;



                this.infDchFrom.Value = dTimeFrom;
                this.infDchTo.Value = dTimeTo;


                
              

                SearchParaCls sp = new SearchParaCls();
                sp.dateFrom = dTimeFrom;
                sp.dateTo = dTimeTo;
                sp.MachineID = sMachineNo;
                //sp.PartNo = sPartNo;
                Session["SP"] = sp;
                

                Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>> dtMacList = new Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>>();
                for (int i = 1; i < 10; i++)
                {
                    dtMacList.Add(i.ToString(), null);
                }

                //get OEE data
                Common.Class.BLL.MouldingMachineStatus_BLL Mc_Status = new Common.Class.BLL.MouldingMachineStatus_BLL();
                Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();


                if (sMachineNo == "")
                {
                    for (int i = 1; i < 9; i++)
                    {
                        dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        dPoints = Mc_Status.getOEE(dTimeFrom, dTimeTo, i.ToString(), "","",false);


                        if (dPoints == null || dPoints.Count == 0)
                        {

                        }
                        else
                        {
                            dtMacList[i.ToString()] = dPoints;
                        }
                    }
                }
                else
                {
                    dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                    dPoints = Mc_Status.getOEE(dTimeFrom, dTimeTo, sMachineNo, "", "", false);

                    if (dPoints == null || dPoints.Count == 0)
                    {

                    }
                    else
                    {
                        dtMacList[sMachineNo] = dPoints;
                    }
                }

                sp.dateFrom = sp.dateFrom.AddHours(8);

                ChartDisplay_Job(dtMacList, sp);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace: MachineOEETimeBar.aspx.cs", "Class:MachineOEETimeBar", "Function:	btnGenerate_Click " + "TableName:LMMSEventLog", ex.ToString());
            }
        }


        private void ChartDisplay_Job(Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>> dtList, SearchParaCls sp)
        {
            try
            {
                foreach (KeyValuePair<string, Dictionary<DateTime, StaticRes.Global.StatusType>> key in dtList)
                {
                    if (key.Value == null)
                    {
                        #region  set visible false

                        switch (key.Key)
                        {
                            case ("1"):
                                {
                                    this.WebUserControlTimeBar1.Visible = false;
                                    break;
                                }
                            case ("2"):
                                {
                                    this.WebUserControlTimeBar2.Visible = false;
                                    break;
                                }
                            case ("3"):
                                {
                                    this.WebUserControlTimeBar3.Visible = false;
                                    break;
                                }
                            case ("4"):
                                {
                                    this.WebUserControlTimeBar4.Visible = false;
                                    break;
                                }
                            case ("5"):
                                {
                                    this.WebUserControlTimeBar5.Visible = false;
                                    break;
                                }
                            case ("6"):
                                {
                                    this.WebUserControlTimeBar6.Visible = false;
                                    break;
                                }
                            case ("7"):
                                {
                                    this.WebUserControlTimeBar7.Visible = false;
                                    break;
                                }
                            case ("8"):
                                {
                                    this.WebUserControlTimeBar8.Visible = false;
                                    break;
                                }
                        }
                        #endregion
                    }
                    else
                    {
                        sp.MachineID = key.Key;
                        switch (key.Key)
                        {
                            case ("1"):
                                {
                                    this.WebUserControlTimeBar1.Visible = true;
                                    sp.MachineID = "1";
                                    setChart1(key.Value, sp);
                                    break;
                                }
                            case ("2"):
                                {
                                    this.WebUserControlTimeBar2.Visible = true;
                                    sp.MachineID = "2";
                                    setChart2(key.Value, sp);
                                    break;
                                }
                            case ("3"):
                                {
                                    this.WebUserControlTimeBar3.Visible = true;
                                    sp.MachineID = "3";
                                    setChart3(key.Value, sp);
                                    break;
                                }
                            case ("4"):
                                {
                                    this.WebUserControlTimeBar4.Visible = true;
                                    sp.MachineID = "4";
                                    setChart4(key.Value, sp);
                                    break;
                                }
                            case ("5"):
                                {
                                    this.WebUserControlTimeBar5.Visible = true;
                                    sp.MachineID = "5";
                                    setChart5(key.Value, sp);
                                    break;
                                }
                            case ("6"):
                                {
                                    this.WebUserControlTimeBar6.Visible = true;
                                    sp.MachineID = "6";
                                    setChart6(key.Value, sp);
                                    break;
                                }
                            case ("7"):
                                {
                                    this.WebUserControlTimeBar7.Visible = true;
                                    sp.MachineID = "7";
                                    setChart7(key.Value, sp);
                                    break;
                                }
                            case ("8"):
                                {
                                    this.WebUserControlTimeBar8.Visible = true;
                                    sp.MachineID = "8";
                                    setChart8(key.Value, sp);
                                    break;
                                }
                            case ("9"):
                                {
                                    this.WebUserControlTimeBar9.Visible = true;
                                    sp.MachineID = "9";
                                    setChart9(key.Value, sp);
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineOEETimeBar_debug", " Execption:" + ee.ToString());
            }

        }

        protected void WebUserControlTimeBar_TimeBarClickEvent(object sender, UserControl.WebUserControlTimeBar.TimeBarEventArgs e)
        {
         
            btnGenerate_Click(new object(), new EventArgs());
        }
        private void setChart1(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar1.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar1.StartTime = sp.dateFrom;
            WebUserControlTimeBar1.EndTime = sp.dateTo.AddDays(1);

            
            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar1.Points.Add(pPos);
            }


            WebUserControlTimeBar1.reflash();
        }
        private void setChart2(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar2.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar2.StartTime = sp.dateFrom;
            WebUserControlTimeBar2.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar2.Points.Add(pPos);
            }

            WebUserControlTimeBar2.reflash();
        }
        private void setChart3(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar3.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar3.StartTime = sp.dateFrom;
            WebUserControlTimeBar3.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar3.Points.Add(pPos);
            }

            WebUserControlTimeBar3.reflash();
        }
        private void setChart4(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar4.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar4.StartTime = sp.dateFrom;
            WebUserControlTimeBar4.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar4.Points.Add(pPos);
            }

            WebUserControlTimeBar4.reflash();
        }
        private void setChart5(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar5.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar5.StartTime = sp.dateFrom;
            WebUserControlTimeBar5.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar5.Points.Add(pPos);
            }

            WebUserControlTimeBar5.reflash();
        }
        private void setChart6(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar6.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar6.StartTime = sp.dateFrom;
            WebUserControlTimeBar6.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;

                WebUserControlTimeBar6.Points.Add(pPos);
            }

            WebUserControlTimeBar6.reflash();
        }
        private void setChart7(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar7.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar7.StartTime = sp.dateFrom;
            WebUserControlTimeBar7.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
                WebUserControlTimeBar7.Points.Add(pPos);
            }

            WebUserControlTimeBar7.reflash();
        }
        private void setChart8(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar8.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar8.StartTime = sp.dateFrom;
            WebUserControlTimeBar8.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
                WebUserControlTimeBar8.Points.Add(pPos);
            }

            WebUserControlTimeBar8.reflash();
        }
        private void setChart9(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            WebUserControlTimeBar9.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar9.StartTime = sp.dateFrom;
            WebUserControlTimeBar9.EndTime = sp.dateTo.AddDays(1);


            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
                WebUserControlTimeBar9.Points.Add(pPos);
            }

            WebUserControlTimeBar9.reflash();
        }
        
    }
}