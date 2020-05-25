using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMachineSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!this.IsPostBack)
                {
                    //init
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;

                    this.ckbExceptWeekend.Checked = true;



                    btn_Generate_Click(new object(), new EventArgs());
                }



                this.dg_MachineSummary.ItemCommand += Dg_MachineSummary_ItemCommand1;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineSummary", "Page_Load Exception: " + ee.ToString());
            }
        }

        private void Dg_MachineSummary_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;
            string MachineID = item.Cells[0].Text;

            MachineID = MachineID != "Total :" ? MachineID = MachineID.Substring(7, 1) : MachineID = "";


            string shift = ddl_shift.SelectedValue;
            string DateFrom = infDchFrom.CalendarLayout.SelectedDate.ToString("yyyy-MM-dd");
            string DateTo = infDchTo.CalendarLayout.SelectedDate.ToString("yyyy-MM-dd");


            if (e.CommandName == "LinkDamage_Mould")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=" + StaticRes.Global.StatusType.DamageMould + "", true);
                return;
            }
            else if (e.CommandName == "LinkMachine_Break")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=" + StaticRes.Global.StatusType.MachineBreak + "", true);
                return;
            }
            else if (e.CommandName == "LinkMould_Testing")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=Mould Testing", true);
                return;
            }
            else if (e.CommandName == "LinkMaterial_Testing")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=Material Testing", true);
                return;
            }
            else if (e.CommandName == "LinkAdjustment")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=" + StaticRes.Global.StatusType.Adjustment + "", true);
                return;
            }
            else if (e.CommandName == "LinkChange_Model")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=" + StaticRes.Global.StatusType.Change_Model + "", true);
                return;
            }
            else if (e.CommandName == "No_Material")
            {
                Response.Redirect("./MouldingMachineStatus.aspx?MachineID=" + MachineID + "&Shift=" + shift + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Status=" + StaticRes.Global.StatusType.No_Material + "", true);
                return;
            }
        }

       

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                string DateNotIn = txt_DateNotIn.Text.Trim();
                string sDateNotIn_Confirmed = "";

                if (DateNotIn != "")
                {
                    string[] ArrDay = DateNotIn.Split('/');

                    for (int i = 0; i < ArrDay.Length; i++)
                    {
                        if (Common.CommFunctions.isNumberic(ArrDay[i]))
                        {
                            sDateNotIn_Confirmed += ArrDay[i] + ",";
                        }
                    }

                    sDateNotIn_Confirmed = sDateNotIn_Confirmed.Substring(0, sDateNotIn_Confirmed.Length - 1);
                }

                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date;
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate.Date;
                string Shift = ddl_shift.SelectedValue;
                double TotalSearchTime = GetTotalTime2();


               

                //output datatable
                Common.Class.BLL.MouldingViHistory_BLL ViHistory_bll = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dt_Output = ViHistory_bll.SummaryByMachine(DateFrom, DateTo, Shift, sDateNotIn_Confirmed, ckbExceptWeekend.Checked ); //2018 12 04  by wei lijia , add date not in & except weekend
               



                //status datatable
                #region  Machine Status
                DataTable dt_Status = new DataTable();
                dt_Status.Columns.Add("MachineID");
                dt_Status.Columns.Add("Running");
                dt_Status.Columns.Add("Adjustment");
                dt_Status.Columns.Add("NoWIP");
                dt_Status.Columns.Add("Mould_Testing");
                dt_Status.Columns.Add("Material_Testing");
                dt_Status.Columns.Add("Change_Model");
                dt_Status.Columns.Add("No_Operator");
                dt_Status.Columns.Add("No_Material");
                dt_Status.Columns.Add("Break_Time");
                dt_Status.Columns.Add("ShutDown");
                dt_Status.Columns.Add("Damage_Mould");
                dt_Status.Columns.Add("Machine_Break");


                Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus_bll = new Common.Class.BLL.MouldingMachineStatus_BLL();
                Dictionary<DateTime, StaticRes.Global.StatusType> Points;
                for (int i = 1; i < 9; i++)
                {
                    DataRow dr = dt_Status.NewRow();

                    Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                    Points = MachineStatus_bll.getOEE(DateFrom, DateTo, i.ToString(), Shift, sDateNotIn_Confirmed, ckbExceptWeekend.Checked);//2018 12 04  by wei lijia , add date not in & except weekend


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

                    dt_Status.Rows.Add(dr);
                }

                #endregion


                //summary datatable
                DataTable dt_Summary = new DataTable();
                dt_Summary.Columns.Add("MachineID");
                dt_Summary.Columns.Add("OK");
                dt_Summary.Columns.Add("NG");
                dt_Summary.Columns.Add("Output");
                dt_Summary.Columns.Add("RejRate");

                dt_Summary.Columns.Add("Running");
                dt_Summary.Columns.Add("Adjustment");
                dt_Summary.Columns.Add("NoWIP");
                dt_Summary.Columns.Add("Mould_Testing");
                dt_Summary.Columns.Add("Material_Testing");
                dt_Summary.Columns.Add("Change_Model");
                dt_Summary.Columns.Add("No_Operator");
                dt_Summary.Columns.Add("No_Material");
                dt_Summary.Columns.Add("Break_Time");
                dt_Summary.Columns.Add("ShutDown");
                dt_Summary.Columns.Add("Damage_Mould");
                dt_Summary.Columns.Add("Machine_Break");

                dt_Summary.Columns.Add("Quality");
                dt_Summary.Columns.Add("Utilization");
                dt_Summary.Columns.Add("Performance");

                // for  dt summary  total  datarow
                double Total_OK = 0;
                double Total_NG = 0;
                double Total_Output = 0;
                double Total_Time_BaseonCycleTime = 0;

           
                double Total_Running_Count = 0;
                double Total_Adjustment_Count = 0;
                double Total_NoWIP_Count = 0;
                double Total_Mould_Testing_Count = 0;
                double Total_Material_Testing_Count = 0;
                double Total_Change_Model_Count = 0;
                double Total_No_Operator_Count = 0;
                double Total_No_Material_Count = 0;
                double Total_Break_Time_Count = 0;
                double Total_ShutDown_Count = 0;
                double Total_Damage_Mould_Count = 0;
                double Total_Machine_Break_Count = 0;

 
                for (int i = 1; i < 9; i++)
                {
                    #region dt_Summary  add  8 Machine datarow

                    DataRow dr_Summary = dt_Summary.NewRow();
                    DataRow[] dr_Output = dt_Output.Select("MachineID='Machine" + i.ToString() + "'");
                    DataRow[] dr_Status = dt_Status.Select("MachineID='Machine" + i.ToString() + "'");


                    dr_Summary["MachineID"] = dr_Output[0]["MachineID"].ToString();
                    dr_Summary["OK"] = dr_Output[0]["OK"].ToString();
                    dr_Summary["NG"] = dr_Output[0]["NG"].ToString();
                    dr_Summary["Output"] = dr_Output[0]["Output"].ToString();
                    dr_Summary["RejRate"] = dr_Output[0]["RejRate"].ToString();
                    dr_Summary["Quality"] = dr_Output[0]["Quality"].ToString();

                    dr_Summary["Running"] = Common.CommFunctions.ConvertDateTimeShort( dr_Status[0]["Running"].ToString() + "H");
                    dr_Summary["Adjustment"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Adjustment"].ToString() + "H");
                    dr_Summary["NoWIP"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["NoWIP"].ToString() + "H");
                    dr_Summary["Mould_Testing"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Mould_Testing"].ToString() + "H");
                    dr_Summary["Material_Testing"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Material_Testing"].ToString() + "H");
                    dr_Summary["Change_Model"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Change_Model"].ToString() + "H");
                    dr_Summary["No_Operator"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["No_Operator"].ToString() + "H");
                    dr_Summary["No_Material"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["No_Material"].ToString() + "H");
                    dr_Summary["Break_Time"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Break_Time"].ToString() + "H");
                    dr_Summary["ShutDown"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["ShutDown"].ToString() + "H");
                    dr_Summary["Damage_Mould"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Damage_Mould"].ToString() + "H");
                    dr_Summary["Machine_Break"] = Common.CommFunctions.ConvertDateTimeShort(dr_Status[0]["Machine_Break"].ToString() + "H");


                    //Utilization
                    double Utilization = Math.Round(double.Parse(dr_Status[0]["Running"].ToString()) / TotalSearchTime * 100, 2);
                    Utilization = double.IsNaN(Utilization) ? 0 : (Utilization < 0 ? 0 : (Utilization > 100 ? 100 : Utilization));
                    dr_Summary["Utilization"] = Utilization.ToString() + "%";
                    //Utilization


                    //Performance
                    double TotalTime_BaseonCycTime = double.Parse(dr_Output[0]["TotalTime"].ToString());
                    double RunningTime = double.Parse(dr_Status[0]["Running"].ToString()) * 3600;

                    double Performance = Math.Round(TotalTime_BaseonCycTime / RunningTime *100, 2);
                    Performance = double.IsNaN(Performance) ? 0 : (Performance < 0 ? 0 : (Performance > 100 ? 100 : Performance));
                    dr_Summary["Performance"] = Performance.ToString() + "%";
                    //Performance

                    dt_Summary.Rows.Add(dr_Summary);

                    #endregion

                    Total_OK += double.Parse(dr_Output[0]["OK"].ToString());
                    Total_NG += double.Parse(dr_Output[0]["NG"].ToString());
                    Total_Output += double.Parse(dr_Output[0]["Output"].ToString());
                    Total_Time_BaseonCycleTime += double.Parse(dr_Output[0]["TotalTime"].ToString());


                    Total_Running_Count += double.Parse(dr_Status[0]["Running"].ToString());
                    Total_Adjustment_Count += double.Parse(dr_Status[0]["Adjustment"].ToString());
                    Total_NoWIP_Count += double.Parse(dr_Status[0]["NoWIP"].ToString());
                    Total_Mould_Testing_Count += double.Parse(dr_Status[0]["Mould_Testing"].ToString());
                    Total_Material_Testing_Count += double.Parse(dr_Status[0]["Material_Testing"].ToString());
                    Total_Change_Model_Count += double.Parse(dr_Status[0]["Change_Model"].ToString());
                    Total_No_Operator_Count += double.Parse(dr_Status[0]["No_Operator"].ToString());
                    Total_No_Material_Count += double.Parse(dr_Status[0]["No_Material"].ToString());
                    Total_Break_Time_Count += double.Parse(dr_Status[0]["Break_Time"].ToString());
                    Total_ShutDown_Count += double.Parse(dr_Status[0]["ShutDown"].ToString());
                    Total_Damage_Mould_Count += double.Parse(dr_Status[0]["Damage_Mould"].ToString());
                    Total_Machine_Break_Count += double.Parse(dr_Status[0]["Machine_Break"].ToString());
                }

                #region add dr total for summary 
                DataRow dr_Total = dt_Summary.NewRow();
                dr_Total[0] = "Total :";
                dr_Total["OK"] = Total_OK;
                dr_Total["NG"] = Total_NG;
                dr_Total["Output"] = Total_Output;

                double Total_RejRate = Math.Round(Total_NG / Total_Output * 100, 2);
                Total_RejRate = double.IsNaN(Total_RejRate) ? 0 : (Total_RejRate < 0 ? 0 : (Total_RejRate > 100 ? 100 : Total_RejRate));
                dr_Total["RejRate"] = Total_RejRate.ToString() + "%";
                
                dr_Total["Running"] = Common.CommFunctions.ConvertDateTimeShort(Total_Running_Count + "H");
                dr_Total["Adjustment"] = Common.CommFunctions.ConvertDateTimeShort(Total_Adjustment_Count + "H");
                dr_Total["NoWIP"] = Common.CommFunctions.ConvertDateTimeShort(Total_NoWIP_Count + "H");
                dr_Total["Mould_Testing"] = Common.CommFunctions.ConvertDateTimeShort(Total_Mould_Testing_Count + "H");
                dr_Total["Material_Testing"] = Common.CommFunctions.ConvertDateTimeShort(Total_Material_Testing_Count + "H");
                dr_Total["Change_Model"] = Common.CommFunctions.ConvertDateTimeShort(Total_Change_Model_Count + "H");
                dr_Total["No_Operator"] = Common.CommFunctions.ConvertDateTimeShort(Total_No_Operator_Count + "H");
                dr_Total["No_Material"] = Common.CommFunctions.ConvertDateTimeShort(Total_No_Material_Count + "H");
                dr_Total["Break_Time"] = Common.CommFunctions.ConvertDateTimeShort(Total_Break_Time_Count + "H");
                dr_Total["ShutDown"] = Common.CommFunctions.ConvertDateTimeShort(Total_ShutDown_Count + "H");
                dr_Total["Damage_Mould"] = Common.CommFunctions.ConvertDateTimeShort(Total_Damage_Mould_Count + "H");
                dr_Total["Machine_Break"] = Common.CommFunctions.ConvertDateTimeShort(Total_Machine_Break_Count + "H");


                double Total_Quality = Math.Round(Total_NG / Total_Output * 100, 2);
                Total_Quality = double.IsNaN(Total_Quality) ? 0 : (Total_Quality < 0 ? 0 : (Total_Quality > 100 ? 100 : Total_Quality));
                dr_Total["Quality"] = Total_Quality.ToString() + "%";

                //utilization  =  running Time / total time
                double Total_Utilization = Math.Round(Total_Running_Count / TotalSearchTime / 8 * 100, 2);
                Total_Utilization = double.IsNaN(Total_Utilization) ? 0 : (Total_Utilization < 0 ? 0 : (Total_Utilization > 100 ? 100 : Total_Utilization));
                dr_Total["Utilization"] = Total_Utilization.ToString() + "%";

                //performance = total time base on cycletime   /   real running time
                double Total_Performance = Math.Round(Total_Time_BaseonCycleTime / (Total_Running_Count * 3600) * 100, 2);
                Total_Performance = double.IsNaN(Total_Performance) ? 0 : (Total_Performance < 0 ? 0 : (Total_Performance > 100 ? 100 : Total_Performance));
                dr_Total["Performance"] = Total_Performance.ToString() + "%";


                dt_Summary.Rows.Add(dr_Total);
                #endregion

             

                this.dg_MachineSummary.DataSource = dt_Summary.DefaultView;
                this.dg_MachineSummary.DataBind();

                ShowChart(dt_Summary);

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineSummary", "btn_Generate_Click Exception; " + ee.ToString());
            }

        }


        private void ShowChart( DataTable dt)
        {
            double timeCountMc1toMc6 = 0;
            double timeCountMc7toMc8 = 0;



            #region machine 1-8 utilization 
            try
            {
                this.ProdChart.Series.Clear();
                this.ProdChart.ChartAreas.Clear();
                this.ProdChart.ChartAreas.Add("Area1");

                ProdChart.ImageStorageMode = ImageStorageMode.UseImageLocation;//启用用户自定义图片临时文件夹


                #region Chart Css
                ProdChart.BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart.BackSecondaryColor = Color.Transparent;
                ProdChart.BackGradientStyle = GradientStyle.None;


                //图表区背景
                ProdChart.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
                ProdChart.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                ProdChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                // ProdChart.ChartAreas[0].AxisX.Interval = 0;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                ProdChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                ProdChart.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                ProdChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

                //X坐标轴颜色
                ProdChart.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

                //X轴网络线条
                ProdChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

                //Y坐标轴颜色
                ProdChart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
                ProdChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart.ChartAreas[0].AxisY.Maximum = 100;

                ProdChart.ChartAreas[0].AxisY.Title = "Utilization Rate %";
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Utilization Rate %";


                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;

                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


                #region AxisY2 css  for Output / OEE / Utilization
                //if (this.ddl_dataType.Text == DataType.Output || this.ddl_dataType.Text == DataType.OEE || this.ddl_dataType.Text == DataType.Utilization)
                //{
                //Y坐标轴颜色
                //ProdChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                //ProdChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                //ProdChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);

                //ProdChart.ChartAreas[0].AxisY2.Title = " Utilization Rate %";
                //ProdChart.ChartAreas[0].AxisY2.ToolTip = " Utilization Rate %";
                
                //ProdChart.ChartAreas[0].AxisY2.Maximum = 100;



                //ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                //ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                //ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //ProdChart.ChartAreas[0].AxisY2.ToolTip = "Reject Rate %";
                //Y轴网格线条
                //ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                //ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                //ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                //ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                //}
                #endregion

                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion


                #region legend
                //Legend legend = new Legend();
                //legend.Docking = Docking.Top;
                //legend.CustomItems.Add(System.Drawing.Color.Blue, "Utilization"); // 参数：(颜色, 说明)
                //legend.CustomItems[0].ImageStyle = LegendImageStyle.Line;
                //legend.CustomItems.Add(StaticRes.MouldingStatusColor.Running, "Running");

                //ProdChart.Legends.Add(legend);
                //ProdChart.Legends[0].Position.Auto = false;
                #endregion


                Series seriesUtilization = new Series();
                seriesUtilization.ChartType = SeriesChartType.StackedColumn;
                seriesUtilization.ChartArea = this.ProdChart.ChartAreas[0].Name;
                seriesUtilization.Name = "Utilization Rate %";
                seriesUtilization.XAxisType = AxisType.Primary;
                seriesUtilization.XValueType = ChartValueType.String;
                seriesUtilization.YAxisType = AxisType.Primary;
                seriesUtilization.YValueType = ChartValueType.Double;
                seriesUtilization.IsVisibleInLegend = false;
                seriesUtilization.LabelForeColor = Color.SteelBlue;


                double TotalSearchTime = GetTotalTime2();
                

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString() == "Total :")
                        continue;


                    string sRunTime = dr["Running"].ToString();
                    string[] strArrRunTime = sRunTime.Split(':');

                    double dRunTime = double.Parse(strArrRunTime[0]) * 3600 + double.Parse(strArrRunTime[1]) * 60 + double.Parse(strArrRunTime[2]);
                    dRunTime = Math.Round(dRunTime / 3600, 2);


                    DataPoint dp_Running = new DataPoint();
                    dp_Running.Color = StaticRes.MouldingStatusColor.Running;
                    dp_Running.BorderColor = StaticRes.MouldingStatusColor.Running;
                    dp_Running.BackSecondaryColor = Color.DarkGreen;
                    dp_Running.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Running.LabelForeColor = Color.Black;

                    dp_Running.AxisLabel = dr[0].ToString();
                    dp_Running.YValues[0] = TotalSearchTime == 0 ? 0 : Math.Round(dRunTime / TotalSearchTime * 100, 2);
                    dp_Running.Label = TotalSearchTime == 0? "0.00%" : Math.Round(dRunTime / TotalSearchTime * 100, 2).ToString("f2") + "%";
                    dp_Running.ToolTip = "Running Time: " + dRunTime.ToString() + "H";

                   
                    seriesUtilization.Points.Add(dp_Running);



                    int mcID = int.Parse(dr[0].ToString().Replace("Machine", ""));
                    if ( mcID >= 1 && mcID <= 6)
                    {
                        timeCountMc1toMc6 += dRunTime;
                    }else
                    {
                        timeCountMc7toMc8 += dRunTime;
                    }
                }

                this.ProdChart.Series.Add(seriesUtilization);


                ProdChart.Titles.Clear();
                ProdChart.Titles.Add("Machine Utilization Chart");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineSummary", "ShowChart Machine 1-8 exception: " + ee.ToString());
            }
            #endregion

            
            #region double/single color utilization
            try
            {
                this.ProdChart2.Series.Clear();
                this.ProdChart2.ChartAreas.Clear();
                this.ProdChart2.ChartAreas.Add("Area1");

                ProdChart2.ImageStorageMode = ImageStorageMode.UseImageLocation;//启用用户自定义图片临时文件夹


                #region Chart Css
                ProdChart2.BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart2.BackSecondaryColor = Color.Transparent;
                ProdChart2.BackGradientStyle = GradientStyle.None;


                //图表区背景
                ProdChart2.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart2.ChartAreas[0].BackSecondaryColor = Color.Transparent;
                ProdChart2.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                ProdChart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                // ProdChart2.ChartAreas[0].AxisX.Interval = 0;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                ProdChart2.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                ProdChart2.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                ProdChart2.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart2.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

                //X坐标轴颜色
                ProdChart2.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
                ProdChart2.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

                //X轴网络线条
                ProdChart2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                ProdChart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

                //Y坐标轴颜色
                ProdChart2.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
                ProdChart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                ProdChart2.ChartAreas[0].AxisY.Maximum = 100;

                ProdChart2.ChartAreas[0].AxisY.Title = "Utilization Rate %";
                ProdChart2.ChartAreas[0].AxisY.ToolTip = "Utilization Rate %";


                ProdChart2.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                ProdChart2.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart2.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;

                //Y轴网格线条
                ProdChart2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


                #region AxisY2 css  for Output / OEE / Utilization
                //if (this.ddl_dataType.Text == DataType.Output || this.ddl_dataType.Text == DataType.OEE || this.ddl_dataType.Text == DataType.Utilization)
                //{
                //Y坐标轴颜色
                //ProdChart2.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                //ProdChart2.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                //ProdChart2.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);

                //ProdChart2.ChartAreas[0].AxisY2.Title = " Utilization Rate %";
                //ProdChart2.ChartAreas[0].AxisY2.ToolTip = " Utilization Rate %";

                //ProdChart2.ChartAreas[0].AxisY2.Maximum = 100;



                //ProdChart2.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                //ProdChart2.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                //ProdChart2.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //ProdChart2.ChartAreas[0].AxisY2.ToolTip = "Reject Rate %";
                //Y轴网格线条
                //ProdChart2.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                //ProdChart2.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                //ProdChart2.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                //ProdChart2.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                //}
                #endregion

                ProdChart2.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion


                #region legend
                //Legend legend = new Legend();
                //legend.Docking = Docking.Top;
                //legend.CustomItems.Add(System.Drawing.Color.Blue, "Utilization"); // 参数：(颜色, 说明)
                //legend.CustomItems[0].ImageStyle = LegendImageStyle.Line;
                //legend.CustomItems.Add(StaticRes.MouldingStatusColor.Running, "Running");

                //ProdChart2.Legends.Add(legend);
                //ProdChart2.Legends[0].Position.Auto = false;
                #endregion


                Series seriesUtilization = new Series();
                seriesUtilization.ChartType = SeriesChartType.StackedColumn;
                seriesUtilization.ChartArea = this.ProdChart2.ChartAreas[0].Name;
                seriesUtilization.Name = "Utilization Rate %";
                seriesUtilization.XAxisType = AxisType.Primary;
                seriesUtilization.XValueType = ChartValueType.String;
                seriesUtilization.YAxisType = AxisType.Primary;
                seriesUtilization.YValueType = ChartValueType.Double;
                seriesUtilization.IsVisibleInLegend = false;
                seriesUtilization.LabelForeColor = Color.SteelBlue;


                double TotalSearchTime = GetTotalTime2();



                DataPoint dpDouble = new DataPoint();
                dpDouble.Color = StaticRes.MouldingStatusColor.Running;
                dpDouble.BorderColor = StaticRes.MouldingStatusColor.Running;
                dpDouble.BackSecondaryColor = Color.DarkGreen;
                dpDouble.BackGradientStyle = GradientStyle.LeftRight;
                dpDouble.LabelForeColor = Color.Black;

                dpDouble.AxisLabel = "Double Color (Mc1~6)";
                dpDouble.YValues[0] = TotalSearchTime == 0 ? 0:  Math.Round(timeCountMc1toMc6  / TotalSearchTime / 6 * 100, 2);
                dpDouble.Label = TotalSearchTime == 0 ? "0.00%" : Math.Round(timeCountMc1toMc6 / TotalSearchTime /6 * 100, 2).ToString("f2") + "%";
                dpDouble.ToolTip = "Running Time: " + timeCountMc1toMc6.ToString() + "H";
                seriesUtilization.Points.Add(dpDouble);


                DataPoint dpSingle = new DataPoint();
                dpSingle.Color = StaticRes.MouldingStatusColor.Running;
                dpSingle.BorderColor = StaticRes.MouldingStatusColor.Running;
                dpSingle.BackSecondaryColor = Color.DarkGreen;
                dpSingle.BackGradientStyle = GradientStyle.LeftRight;
                dpSingle.LabelForeColor = Color.Black;

                dpSingle.AxisLabel = "Single Color (Mc7,8)";
                dpSingle.YValues[0] = TotalSearchTime == 0 ? 0 : Math.Round(timeCountMc7toMc8 / TotalSearchTime / 2 * 100, 2);
                dpSingle.Label = TotalSearchTime == 0 ? "0.00%" : Math.Round(timeCountMc7toMc8 / TotalSearchTime / 2  * 100, 2).ToString("f2") + "%";
                dpSingle.ToolTip = "Running Time: " + timeCountMc7toMc8.ToString() + "H";
                seriesUtilization.Points.Add(dpSingle);
                



                this.ProdChart2.Series.Add(seriesUtilization);


                ProdChart2.Titles.Clear();
                ProdChart2.Titles.Add("Machine Utilization Chart");
                ProdChart2.Titles[0].ForeColor = Color.Black;
                ProdChart2.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart2.Titles[0].Alignment = ContentAlignment.TopCenter;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineSummary", "ShowChart double/single color exception: " + ee.ToString());
            }
            #endregion

        }
        

        //当天时间算作一天的时间. 
        private double GetTotalTime2()
        {
            DateTime dDatefrom = infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
            DateTime dDateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddHours(8);

            double TotalSearchTime = 0;
            DateTime dateFrom = dDatefrom;
            DateTime dateTo = dDateTo.AddDays(1);
            double HourPerDay = ddl_shift.SelectedValue == "" ? 24 : 12;


            double TotalDays = 0;
          
            
            if ((dateTo <= DateTime.Now))
            {
                TotalDays = (dateTo - dateFrom).Days;
                TotalSearchTime = TotalDays * HourPerDay;
            }
            else if (dateTo > DateTime.Now)
            {
                TotalDays = (DateTime.Now.AddHours(-8).Date.AddDays(1).AddHours(8) - dateFrom).TotalDays;
            }
            TotalSearchTime = TotalDays * HourPerDay;
            TotalSearchTime = TotalSearchTime < 0 ? 0 : TotalSearchTime;


            double Total_DateNotIn_Time = 0;
            if (this.txt_DateNotIn.Text.Trim() != "")
            {
                string[] strTemp_arr = this.txt_DateNotIn.Text.Trim().Split('/');
                
                double NotInDateCount = 0;
                foreach (string str in strTemp_arr)
                {
                    int iNotInDate = int.Parse(str);

                    if (iNotInDate >= dateFrom.Day && iNotInDate <= dateTo.Day)
                    {
                        NotInDateCount++;
                    }
                }


                Total_DateNotIn_Time = NotInDateCount * HourPerDay;

            }
            else
            {
                Total_DateNotIn_Time = 0;
            }


            //2019 01 02
            double Total_Weekend_Time = 0;
            if (ckbExceptWeekend.Checked)
            {
                int WeekendCount = 0;

                //check the last day is whole day or partial day
                if (DateTime.Now.Date == dateTo.Date)
                {
                    for (int i = 0; i < (dateTo - dateFrom).TotalDays; i++)
                    {
                        if (dateFrom.AddDays(i).DayOfWeek == DayOfWeek.Saturday || dateFrom.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        {
                            WeekendCount++;
                        }
                    }
                    

                    Total_Weekend_Time = WeekendCount * HourPerDay;
                }
                else
                {
                    for (int i = 0; i < (dateTo - dateFrom).TotalDays; i++)
                    {
                        if (dateFrom.AddDays(i).DayOfWeek == DayOfWeek.Saturday || dateFrom.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        {
                            WeekendCount++;
                        }
                    }
                    Total_Weekend_Time = WeekendCount * HourPerDay;
                }

            }
            else
            {
                Total_Weekend_Time = 0;
            }


            TotalSearchTime = TotalSearchTime - Total_DateNotIn_Time - Total_Weekend_Time;
            

            return TotalSearchTime;
        }
        

    }
}