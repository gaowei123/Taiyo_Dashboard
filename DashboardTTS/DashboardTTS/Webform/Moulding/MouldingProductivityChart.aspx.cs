using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingProductivityChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblUserHeader.Text = " Moulding Utilization Chart ";

                    this.ProdChart.Visible = false;

                    this.infDchFrom.Value = DateTime.Now.Date.AddHours(8);
                    infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.Date.AddHours(8);
                    this.infDchTo.Value = DateTime.Now.Date.AddHours(8);
                    infDchTo.CalendarLayout.SelectedDate = DateTime.Now.Date.AddHours(8);
                                

                    btnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ee)
                {
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "Page_Load Exception : " + ee.Message);
                }
            }
        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = infDchFrom.CalendarLayout.SelectedDate;
                DateTime dTimeTo = infDchTo.CalendarLayout.SelectedDate;
                string shift = this.ddl_shift.SelectedValue;
                string sMachine = ddlMachineID.SelectedValue;

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
                    if (sMachine == "")
                    {
                        Points = MachineStatus_bll.getOEE(dTimeFrom, dTimeTo, i.ToString(), shift, "", false);//2018 12 04  by wei lijia , add date not in & except weekend
                    }
                    else
                    {
                        Points = MachineStatus_bll.getOEE(dTimeFrom, dTimeTo, sMachine, shift, "", false);
                        i = 9;
                    }
                    if (Points == null || Points.Count == 0)
                    {
                        #region default 0
                        if (sMachine == "")
                        {
                            dr["MachineID"] = "Machine" + i.ToString();
                        }
                        else
                        {
                            dr["MachineID"] = "Machine" + sMachine;
                        }
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

                        if (sMachine == "")
                        {
                            dr["MachineID"] = "Machine" + i.ToString();
                        }
                        else
                        {
                            dr["MachineID"] = "Machine" + sMachine;
                        }
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

                if (dt_Status == null || dt_Status.Rows.Count == 0)
                {
                    this.lblResult.Visible = true;
                    this.lblResult.BackColor = System.Drawing.Color.Red;
                    this.lblResult.Text = "There is no data";
                    this.ProdChart.Visible = false;
                }
                else
                {
                    this.lblResult.Visible = false;
                    this.ProdChart.Visible = true;

                    ChartDisplay_Job(dt_Status);
                }
            }
            catch (Exception ex)
            {
                this.lblResult.Visible = true;
                this.lblResult.BackColor = System.Drawing.Color.Red;
                this.lblResult.Text = ex.Message;
                DBHelp.Reports.LogFile.Log("OutPut Chart", "btnGenerate_Click error : " + ex.ToString());
            }
        }
        
        System.Web.UI.DataVisualization.Charting.Series Set_UtilizationSeries_CSS(string type)
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.StackedColumn;
            series.ChartArea = this.ProdChart.ChartAreas[0].Name;
            series.Name = type;
            series.XAxisType = AxisType.Primary;
            series.XValueType = ChartValueType.String;
            series.YAxisType = AxisType.Primary;
            series.YValueType = ChartValueType.Double;
            series.IsVisibleInLegend = true;
            series.LabelForeColor = Color.SteelBlue;
            //series.ToolTip = "#VALX -- #VAL" + "%";     //鼠标移动到对应点显示数值
            series.ToolTip = type + " -- #VAL" + "%";


            Legend legend = new Legend("Operation Status");
            series.Color = Color.Lime;
            series.LegendText = legend.Name;
            series.Palette = ChartColorPalette.Bright;

            return series;
        }


      
        void ChartDisplay_Job(DataTable dt)
        {
            try
            {
                this.ProdChart.Series.Clear();
                this.ProdChart.ChartAreas.Clear();
                this.ProdChart.ChartAreas.Add("Area1");

                ProdChart.ImageStorageMode = ImageStorageMode.UseImageLocation;


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
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                ProdChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                ProdChart.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                ProdChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

                //X坐标轴颜色
                ProdChart.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

                //X轴网络线条
                ProdChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

                //Y坐标轴颜色
                ProdChart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
                ProdChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);


                ProdChart.ChartAreas[0].AxisY.Title = "Operation Status";
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Operation Status";


                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
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
                ProdChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);

                ProdChart.ChartAreas[0].AxisY2.Title = " Utilization Rate %";
                ProdChart.ChartAreas[0].AxisY2.ToolTip = " Utilization Rate %";
                ProdChart.ChartAreas[0].AxisY.Maximum = 100;
                ProdChart.ChartAreas[0].AxisY2.Maximum = 100;



                ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //ProdChart.ChartAreas[0].AxisY2.ToolTip = "Reject Rate %";
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                //}
                #endregion

                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion
                

                #region legend
                Legend legend = new Legend();
                legend.Docking = Docking.Top;
                legend.CustomItems.Add(System.Drawing.Color.Blue, "Utilization"); // 参数：(颜色, 说明)
                legend.CustomItems[0].ImageStyle = LegendImageStyle.Line;
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Running, "Running");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Adjustment, "Adjustment");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Mould_Testing, "Mould_Testing");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Material_Testing, "Material_Testing");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Change_Model, "Change_Model");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Schedule, "No_Schedule");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Operator, "No_Operator");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Material, "No_Material");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.Break_Time, "Break_Time");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.ShutDown, "ShutDown");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.MachineBreak, "MachineBreak");
                legend.CustomItems.Add(StaticRes.MouldingStatusColor.DamageMould, "DamageMould");
                
                ProdChart.Legends.Add(legend);
                ProdChart.Legends[0].Position.Auto = false;
                #endregion


                #region  Series
                Series Utilization_Running = new Series();
                Series Utilization_Adjustment = new Series();
                Series Utilization_No_Schedule = new Series();
                Series Utilization_Mould_Testing = new Series();
                Series Utilization_Material_Testing = new Series();
                Series Utilization_Change_Model = new Series();
                Series Utilization_No_Operator = new Series();
                Series Utilization_No_Material = new Series();
                Series Utilization_Break_Time = new Series();
                Series Utilization_ShutDown = new Series();
                Series Utilization_DamageMould = new Series();
                Series Utilization_MachineBreak = new Series();

                Utilization_Running = Set_UtilizationSeries_CSS("Running");
                Utilization_Adjustment = Set_UtilizationSeries_CSS("Adjustment");
                Utilization_No_Schedule = Set_UtilizationSeries_CSS("No_Schedule");
                Utilization_Mould_Testing = Set_UtilizationSeries_CSS("Mould_Testing");
                Utilization_Material_Testing = Set_UtilizationSeries_CSS("Material_Testing");
                Utilization_Change_Model = Set_UtilizationSeries_CSS("Change_Model");
                Utilization_No_Operator = Set_UtilizationSeries_CSS("No_Operator");
                Utilization_No_Material = Set_UtilizationSeries_CSS("No_Material");
                Utilization_Break_Time = Set_UtilizationSeries_CSS("Break_Time");
                Utilization_ShutDown = Set_UtilizationSeries_CSS("ShutDown");
                Utilization_DamageMould = Set_UtilizationSeries_CSS("DamageMould");
                Utilization_MachineBreak = Set_UtilizationSeries_CSS("MachineBreak");
                

                #region Utilization_Rate
                Series Utilization_Rate = new Series();
                Utilization_Rate.ChartType = SeriesChartType.Line;
                Utilization_Rate.ChartArea = this.ProdChart.ChartAreas[0].Name;
                Utilization_Rate.Name = "Machine Utilization Rate %";
                Utilization_Rate.XAxisType = AxisType.Primary;
                Utilization_Rate.XValueType = ChartValueType.String;
                Utilization_Rate.YAxisType = AxisType.Secondary;
                Utilization_Rate.YValueType = ChartValueType.Double;
                Utilization_Rate.IsVisibleInLegend = true;
                Utilization_Rate.ToolTip = "#VALX -- #VAL" + "%";

                Utilization_Rate.MarkerColor = Color.White;
                Utilization_Rate.MarkerSize = 10;
                Utilization_Rate.MarkerStyle = MarkerStyle.Star6;
                Utilization_Rate.MarkerBorderWidth = 1;
                Utilization_Rate.MarkerBorderColor = Color.DarkBlue;
                Utilization_Rate.BorderWidth = 2;
                #endregion
                #endregion

                double TotalSearchTime = 0;
                #region total search time
                DateTime dateFrom = infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime dateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddHours(8);
                double DayTime_Shift = ddl_shift.SelectedValue == "" ? 24 : 12;


                double TotalDays = 0;
                double TimeofLastDay = 0;

                if ((dateTo.AddDays(1) <= DateTime.Now))
                {
                    TotalDays = (dateTo.AddDays(1) - dateFrom).Days;
                    TotalSearchTime = TotalDays * DayTime_Shift;
                }
                else if ((dateTo.AddDays(1) > DateTime.Now))
                {
                    TotalDays = (DateTime.Now.AddHours(-8).Date.AddHours(8) - dateFrom).TotalDays;


                    if (ddl_shift.Text == StaticRes.Global.Shift.Day)
                    {
                        if (DateTime.Now > DateTime.Now.Date.AddHours(20))
                        {
                            TimeofLastDay = 12;
                        }
                        else
                        {
                            TimeofLastDay = (DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(8)).TotalHours;
                        }
                    }
                    else if (ddl_shift.Text == StaticRes.Global.Shift.Night)
                    {
                        if (DateTime.Now < DateTime.Now.Date.AddHours(20))
                        {
                            TimeofLastDay = 0;
                        }
                        else
                        {
                            TimeofLastDay = (DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(20)).TotalHours;
                        }
                    }
                    else
                    {
                        TimeofLastDay = (DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(8)).TotalHours;
                    }


                }
                TotalSearchTime = TotalDays * DayTime_Shift + TimeofLastDay;
                TotalSearchTime = TotalSearchTime < 0 ? 0 : TotalSearchTime;
                #endregion


                foreach (DataRow x in dt.Rows)
                {
                    #region
                    #region Running
                    DataPoint dp_Running = new DataPoint();
                    dp_Running.Color = StaticRes.MouldingStatusColor.Running;
                    dp_Running.BorderColor = StaticRes.MouldingStatusColor.Running;
                    dp_Running.BackSecondaryColor = Color.DarkGreen;
                    dp_Running.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Running.LabelForeColor = Color.Black;
                    dp_Running.AxisLabel = x[0].ToString();

                    double RunningTime = double.Parse(x[1].ToString());
                    double RunningPercentage = TotalSearchTime == 0 ? 0 : Math.Round(RunningTime / TotalSearchTime * 100, 2);

                    dp_Running.YValues[0] = RunningPercentage;

                    if (dp_Running.YValues[0] != 0)
                    {
                        dp_Running.Label = RunningPercentage.ToString() + "%";// double.Parse(x[1].ToString()) + "H";
                    }
                    else
                    {
                        dp_Running.Label = "";
                    }

                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Running AxisLabel: " + dp_Running.AxisLabel + ", YValue: "+ dp_Running.YValues[0] + ", Label: "+ dp_Running.Label);

                    Utilization_Running.Points.Add(dp_Running);
                    #endregion

                    #region Adjustment
                    DataPoint dp_Adjustment = new DataPoint();
                    dp_Adjustment.Color = StaticRes.MouldingStatusColor.Adjustment;
                    dp_Adjustment.BackSecondaryColor = Color.YellowGreen;
                    dp_Adjustment.BorderColor = StaticRes.MouldingStatusColor.Adjustment;
                    dp_Adjustment.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Adjustment.LabelForeColor = Color.Black;

                    double AdjustmentTime = double.Parse(x[2].ToString());
                    double AdjustmentPercentage = TotalSearchTime == 0 ? 0 : Math.Round(AdjustmentTime / TotalSearchTime * 100, 2);



                    dp_Adjustment.AxisLabel = x[0].ToString();//day
                    dp_Adjustment.YValues[0] = AdjustmentPercentage;// double.IsNaN(Math.Round(double.Parse(x[2].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[2].ToString()) * 100 / TotalSearchTime, 2);
                    if (AdjustmentTime != 0)
                    {
                        dp_Adjustment.Label = AdjustmentPercentage.ToString() + "%";// double.Parse(x[2].ToString()) + "H";
                    }
                    else
                    {
                        dp_Adjustment.Label = "";
                    }

                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Adjustment AxisLabel: " + dp_Adjustment.AxisLabel + ", YValue: " + dp_Adjustment.YValues[0] + ", Label: " + dp_Adjustment.Label);

                    Utilization_Adjustment.Points.Add(dp_Adjustment);
                    #endregion

                    #region No WIP
                    DataPoint dp_Schedule = new DataPoint();
                    dp_Schedule.Color = StaticRes.MouldingStatusColor.No_Schedule;
                    dp_Schedule.BackSecondaryColor = Color.YellowGreen;
                    dp_Schedule.BorderColor = StaticRes.MouldingStatusColor.No_Schedule;
                    dp_Schedule.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Schedule.LabelForeColor = Color.Black;

                    double NoScheduleTime = double.Parse(x[3].ToString());
                    double NoSchedulePercentage = TotalSearchTime == 0 ? 0 : Math.Round(NoScheduleTime / TotalSearchTime * 100, 2);

                    dp_Schedule.AxisLabel = x[0].ToString();//day
                    dp_Schedule.YValues[0] = NoSchedulePercentage;// double.IsNaN(Math.Round(double.Parse(x[3].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[3].ToString()) * 100 / TotalSearchTime, 2);
                    if (NoScheduleTime != 0)
                    {
                        dp_Schedule.Label = NoSchedulePercentage + "%";// double.Parse(x[3].ToString()) + "H";
                    }
                    else
                    {
                        dp_Schedule.Label = "";
                    }

                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Schedule AxisLabel: " + dp_Schedule.AxisLabel + ", YValue: " + dp_Schedule.YValues[0] + ", Label: " + dp_Schedule.Label);

                    Utilization_No_Schedule.Points.Add(dp_Schedule);
                    #endregion

                    #region Mould_Testing
                    DataPoint dp_Mould_Testing = new DataPoint();
                    dp_Mould_Testing.Color = StaticRes.MouldingStatusColor.Mould_Testing;
                    dp_Mould_Testing.BackSecondaryColor = Color.DarkRed;
                    dp_Mould_Testing.BorderColor = StaticRes.MouldingStatusColor.Mould_Testing;
                    dp_Mould_Testing.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Mould_Testing.LabelForeColor = Color.Black;

                    double Mould_TestingTime = double.Parse(x[4].ToString());
                    double Mould_TestingPercentage = TotalSearchTime == 0 ? 0 : Math.Round(Mould_TestingTime / TotalSearchTime * 100, 2);

                    dp_Mould_Testing.AxisLabel = x[0].ToString();//day
                    dp_Mould_Testing.YValues[0] = Mould_TestingPercentage;// double.IsNaN(Math.Round(double.Parse(x[4].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[4].ToString()) * 100 / TotalSearchTime, 2);
                    if (Mould_TestingTime != 0)
                    {
                        dp_Mould_Testing.Label = Mould_TestingPercentage + "%";// double.Parse(x[4].ToString()) + "H";
                    }
                    else
                    {
                        dp_Mould_Testing.Label = "";
                    }

                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Mould_Testing AxisLabel: " + dp_Mould_Testing.AxisLabel + ", YValue: " + dp_Mould_Testing.YValues[0] + ", Label: " + dp_Mould_Testing.Label);
                    Utilization_Mould_Testing.Points.Add(dp_Mould_Testing);
                    #endregion

                    #region Material_Testing
                    DataPoint dp_Material_Testing = new DataPoint();
                    dp_Material_Testing.Color = StaticRes.MouldingStatusColor.Material_Testing;
                    dp_Material_Testing.BackSecondaryColor = Color.Blue;
                    dp_Material_Testing.BorderColor = StaticRes.MouldingStatusColor.Material_Testing;
                    dp_Material_Testing.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Material_Testing.LabelForeColor = Color.Black;


                    double Material_TestingTime = double.Parse(x[5].ToString());
                    double Material_TestingTimePercentage = TotalSearchTime == 0 ? 0 : Math.Round(Material_TestingTime / TotalSearchTime * 100, 2);


                    dp_Material_Testing.AxisLabel = x[0].ToString();//day
                    dp_Material_Testing.YValues[0] = Material_TestingTimePercentage;// double.IsNaN(Math.Round(double.Parse(x[5].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[5].ToString()) * 100 / TotalSearchTime, 2);
                    if (Material_TestingTime != 0)
                    {
                        dp_Material_Testing.Label = Material_TestingTimePercentage + "%";// double.Parse(x[5].ToString()) + "H";
                    }
                    else
                    {
                        dp_Material_Testing.Label = "";
                    }

                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Material_Testing AxisLabel: " + dp_Material_Testing.AxisLabel + ", YValue: " + dp_Material_Testing.YValues[0] + ", Label: " + dp_Material_Testing.Label);

                    Utilization_Material_Testing.Points.Add(dp_Material_Testing);
                    #endregion 

                    #region Change_Model
                    DataPoint dp_ChangeModel = new DataPoint();
                    dp_ChangeModel.Color = StaticRes.MouldingStatusColor.Change_Model;
                    dp_ChangeModel.BackSecondaryColor = Color.DarkBlue;
                    dp_ChangeModel.BorderColor = StaticRes.MouldingStatusColor.Change_Model;
                    dp_ChangeModel.BackGradientStyle = GradientStyle.LeftRight;
                    dp_ChangeModel.LabelForeColor = Color.Black;


                    double Change_ModelTime = double.Parse(x[6].ToString());
                    double Change_ModelPercentage = TotalSearchTime == 0 ? 0 : Math.Round(Change_ModelTime / TotalSearchTime * 100, 2);

                    dp_ChangeModel.AxisLabel = x[0].ToString();//day
                    dp_ChangeModel.YValues[0] = Change_ModelPercentage;// double.IsNaN(Math.Round(double.Parse(x[6].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[6].ToString()) * 100 / TotalSearchTime, 2);
                    if (Change_ModelTime != 0)
                    {
                        dp_ChangeModel.Label = Change_ModelPercentage+ "%";// double.Parse(x[6].ToString()) + "H";
                    }
                    else
                    {
                        dp_ChangeModel.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_ChangeModel AxisLabel: " + dp_ChangeModel.AxisLabel + ", YValue: " + dp_ChangeModel.YValues[0] + ", Label: " + dp_ChangeModel.Label);
                    Utilization_Change_Model.Points.Add(dp_ChangeModel);
                    #endregion

                    #region No Operator
                    DataPoint dp_No_Operator = new DataPoint();
                    dp_No_Operator.Color = StaticRes.MouldingStatusColor.No_Operator;
                    dp_No_Operator.BackSecondaryColor = StaticRes.MouldingStatusColor.No_Operator;
                    dp_No_Operator.BorderColor = StaticRes.MouldingStatusColor.No_Operator;
                    dp_No_Operator.BackGradientStyle = GradientStyle.LeftRight;
                    dp_No_Operator.LabelForeColor = Color.Black;

                    double No_OperatorTime = double.Parse(x[7].ToString());
                    double No_OperatorPercentage = TotalSearchTime == 0 ? 0 : Math.Round(No_OperatorTime / TotalSearchTime * 100, 2);


                    dp_No_Operator.AxisLabel = x[0].ToString();//day
                    dp_No_Operator.YValues[0] = No_OperatorPercentage;// double.IsNaN(Math.Round(double.Parse(x[7].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[7].ToString()) * 100 / TotalSearchTime, 2);
                    if (No_OperatorTime != 0)
                    {
                        dp_No_Operator.Label = No_OperatorPercentage + "%";// double.Parse(x[7].ToString()) + "H";
                    }
                    else
                    {
                        dp_No_Operator.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_No_Operator AxisLabel: " + dp_No_Operator.AxisLabel + ", YValue: " + dp_No_Operator.YValues[0] + ", Label: " + dp_No_Operator.Label);

                    Utilization_No_Operator.Points.Add(dp_No_Operator);
                    #endregion

                    #region No_Material
                    DataPoint dp_No_Material = new DataPoint();
                    dp_No_Material.Color = StaticRes.MouldingStatusColor.No_Material;
                    dp_No_Material.BackSecondaryColor = StaticRes.MouldingStatusColor.No_Material;
                    dp_No_Material.BorderColor = StaticRes.MouldingStatusColor.No_Material;
                    dp_No_Material.BackGradientStyle = GradientStyle.LeftRight;
                    dp_No_Material.LabelForeColor = Color.Black;


                    double No_MaterialTime = double.Parse(x[8].ToString());
                    double No_MaterialPercentage = TotalSearchTime == 0 ? 0 : Math.Round(No_MaterialTime / TotalSearchTime * 100, 2);

                    dp_No_Material.AxisLabel = x[0].ToString();//day
                    dp_No_Material.YValues[0] = No_MaterialPercentage;// double.IsNaN(Math.Round(double.Parse(x[8].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[8].ToString()) * 100 / TotalSearchTime, 2);
                    if (No_MaterialTime != 0)
                    {
                        dp_No_Material.Label = No_MaterialPercentage + "%";// double.Parse(x[8].ToString()) + "H";
                    }
                    else
                    {
                        dp_No_Material.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_No_Material AxisLabel: " + dp_No_Material.AxisLabel + ", YValue: " + dp_No_Material.YValues[0] + ", Label: " + dp_No_Material.Label);

                    Utilization_No_Material.Points.Add(dp_No_Material);
                    #endregion

                    #region Break_Time
                    DataPoint dp_Break_Time = new DataPoint();
                    dp_Break_Time.Color = StaticRes.MouldingStatusColor.Break_Time;
                    dp_Break_Time.BackSecondaryColor = StaticRes.MouldingStatusColor.Break_Time;
                    dp_Break_Time.BorderColor = StaticRes.MouldingStatusColor.Break_Time;
                    dp_Break_Time.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Break_Time.LabelForeColor = Color.Black;


                    double Break_TimeTime = double.Parse(x[9].ToString());
                    double Break_TimePercentage = TotalSearchTime == 0 ? 0 : Math.Round(Break_TimeTime / TotalSearchTime * 100, 2);

                    dp_Break_Time.AxisLabel = x[0].ToString();//day
                    dp_Break_Time.YValues[0] = Break_TimePercentage;// double.IsNaN(Math.Round(double.Parse(x[9].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[9].ToString()) * 100 / TotalSearchTime, 2);
                    if (Break_TimeTime != 0)
                    {
                        dp_Break_Time.Label = Break_TimePercentage + "%";// double.Parse(x[9].ToString()) + "H";
                    }
                    else
                    {
                        dp_Break_Time.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Break_Time AxisLabel: " + dp_Break_Time.AxisLabel + ", YValue: " + dp_Break_Time.YValues[0] + ", Label: " + dp_Break_Time.Label);


                    Utilization_Break_Time.Points.Add(dp_Break_Time);
                    #endregion

                    #region ShutDown
                    DataPoint dp_ShutDown = new DataPoint();
                    dp_ShutDown.Color = StaticRes.MouldingStatusColor.ShutDown;
                    dp_ShutDown.BackSecondaryColor = Color.DarkGray;
                    dp_ShutDown.BorderColor = StaticRes.MouldingStatusColor.ShutDown;
                    dp_ShutDown.BackGradientStyle = GradientStyle.LeftRight;
                    dp_ShutDown.LabelForeColor = Color.Black;

                    dp_ShutDown.AxisLabel = x[0].ToString();

                    double ShutdwonTime = TotalSearchTime - double.Parse(x[1].ToString()) - double.Parse(x[2].ToString()) - double.Parse(x[3].ToString()) - double.Parse(x[4].ToString()) - double.Parse(x[5].ToString()) - double.Parse(x[6].ToString()) - double.Parse(x[7].ToString()) - double.Parse(x[8].ToString()) - double.Parse(x[9].ToString()) - double.Parse(x[11].ToString()) - double.Parse(x[12].ToString());
                    ShutdwonTime = ShutdwonTime < 0 ? 0 : Math.Round(ShutdwonTime, 2);

                    double ShutdownPercentage = Math.Round(ShutdwonTime / TotalSearchTime * 100, 2);

                    dp_ShutDown.YValues[0] = ShutdownPercentage;

                    dp_ShutDown.Label = ShutdownPercentage.ToString();  //ShutdwonTime.ToString() + "H";


                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_ShutDown AxisLabel: " + dp_ShutDown.AxisLabel + ", YValue: " + dp_ShutDown.YValues[0] + ", Label: " + dp_ShutDown.Label);

                    Utilization_ShutDown.Points.Add(dp_ShutDown);
                    #endregion

                    #region Damage_Mould
                    DataPoint dp_Damage_Mould = new DataPoint();
                    dp_Damage_Mould.Color = StaticRes.MouldingStatusColor.DamageMould;
                    dp_Damage_Mould.BackSecondaryColor = StaticRes.MouldingStatusColor.DamageMould;
                    dp_Damage_Mould.BorderColor = StaticRes.MouldingStatusColor.DamageMould;
                    dp_Damage_Mould.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Damage_Mould.LabelForeColor = Color.Black;

                    double Damage_MouldTime = double.Parse(x[11].ToString());
                    double Damage_MouldPercentage = TotalSearchTime == 0 ? 0 : Math.Round(Damage_MouldTime / TotalSearchTime * 100, 2);


                    dp_Damage_Mould.AxisLabel = x[0].ToString();//day
                    dp_Damage_Mould.YValues[0] = Damage_MouldPercentage;// double.IsNaN(Math.Round(double.Parse(x[11].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[11].ToString()) * 100 / TotalSearchTime, 2);
                    if (Damage_MouldTime != 0)
                    {
                        dp_Damage_Mould.Label = Damage_MouldPercentage + "%";// double.Parse(x[11].ToString()) + "H";
                    }
                    else
                    {
                        dp_Damage_Mould.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Damage_Mould AxisLabel: " + dp_Damage_Mould.AxisLabel + ", YValue: " + dp_Damage_Mould.YValues[0] + ", Label: " + dp_Damage_Mould.Label);

                    Utilization_DamageMould.Points.Add(dp_Damage_Mould);
                    #endregion

                    #region Machine_Break
                    DataPoint dp_Machine_Break = new DataPoint();
                    dp_Machine_Break.Color = StaticRes.MouldingStatusColor.MachineBreak;
                    dp_Machine_Break.BackSecondaryColor = StaticRes.MouldingStatusColor.MachineBreak;
                    dp_Machine_Break.BorderColor = StaticRes.MouldingStatusColor.MachineBreak;
                    dp_Machine_Break.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Machine_Break.LabelForeColor = Color.Black;


                    double Machine_BreakTime = double.Parse(x[12].ToString());
                    double Machine_BreakPercentage = TotalSearchTime == 0 ? 0 : Math.Round(Machine_BreakTime / TotalSearchTime * 100, 2);

                    dp_Machine_Break.AxisLabel = x[0].ToString();//day
                    dp_Machine_Break.YValues[0] = Machine_BreakPercentage;// double.IsNaN(Math.Round(double.Parse(x[12].ToString()) * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(double.Parse(x[12].ToString()) * 100 / TotalSearchTime, 2);
                    if (Machine_BreakTime != 0)
                    {
                        dp_Machine_Break.Label = Machine_BreakPercentage+"%";// double.Parse(x[12].ToString()) + "H";
                    }
                    else
                    {
                        dp_Machine_Break.Label = "";
                    }
                    DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "dp_Machine_Break AxisLabel: " + dp_Machine_Break.AxisLabel + ", YValue: " + dp_Machine_Break.YValues[0] + ", Label: " + dp_Machine_Break.Label);

                    Utilization_MachineBreak.Points.Add(dp_Machine_Break);
                    #endregion
                    

                    #region Utilization Rate
                    DataPoint dp_Rate = new DataPoint();
                    dp_Rate.AxisLabel = x[0].ToString();//day
                    double Uti_Rate = Math.Round(double.Parse(x[1].ToString()) / TotalSearchTime * 100, 2);

                    Uti_Rate = double.IsNaN(Uti_Rate) ? 0 : Uti_Rate;

                    dp_Rate.YValues[0] = Uti_Rate;
                    dp_Rate.Label = Uti_Rate.ToString() + "%";
                    dp_Rate.BackGradientStyle = GradientStyle.LeftRight;
                    dp_Rate.LabelForeColor = Color.Black;
                    dp_Rate.MarkerBorderWidth = 2;
                    dp_Rate.MarkerStyle = MarkerStyle.Circle;
                    dp_Rate.Color = System.Drawing.Color.DarkBlue;
                    dp_Rate.BackSecondaryColor = Color.DarkBlue;
                    dp_Rate.BorderColor = Color.DarkBlue;

                    Utilization_Rate.Points.Add(dp_Rate);
                    #endregion

                    #endregion
                }

                this.ProdChart.Series.Add(Utilization_Running);
                this.ProdChart.Series.Add(Utilization_Adjustment);
                this.ProdChart.Series.Add(Utilization_No_Schedule);
                this.ProdChart.Series.Add(Utilization_Material_Testing);
                this.ProdChart.Series.Add(Utilization_Mould_Testing);
                this.ProdChart.Series.Add(Utilization_Change_Model);
                this.ProdChart.Series.Add(Utilization_No_Operator);
                this.ProdChart.Series.Add(Utilization_No_Material);
                this.ProdChart.Series.Add(Utilization_Break_Time);
                this.ProdChart.Series.Add(Utilization_ShutDown);
                this.ProdChart.Series.Add(Utilization_DamageMould);
                this.ProdChart.Series.Add(Utilization_MachineBreak);
                //this.ProdChart.Series.Add(Utilization_Rate);

                ProdChart.Titles.Clear();
                ProdChart.Titles.Add("Machine Utilization Data ");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingProductivityChart", "ChartDisplay_Job Exception : " + ee.Message);
            }
        }
       
    }
}