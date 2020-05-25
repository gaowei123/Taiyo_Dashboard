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
    public partial class MouldingProductRecordChart : System.Web.UI.Page
    {
        static private class DataType
        {
            public const string All = "";
            public const string Output = "Output";
            public const string OEE = "OEE";
            public const string Utilization = "Utilization";
            

            public const string Running = "Running";
            public const string Adjustment = "Adjustment";
            public const string NoWIP = "NoWIP";
            public const string Mould_Testing = "Mould_Testing";
            public const string Material_Testing = "Material_Testing";
            public const string Change_Model = "Change_Model";
            public const string No_Operator = "No_Operator";
            public const string No_Material = "No_Material";
            public const string Break_Time = "Break_Time";
            public const string ShutDown = "ShutDown";
            public const string Damage_Mould = "Damage_Mould";
            public const string Machine_Break = "Machine_Break";
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.lblUserHeader.Text = " Moulding Production Record Chart";
                    


                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;


                    btnGenerate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingProductRecordChart", "Page_Load Exception:" + ee.ToString());
            }
        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //get parameters
                DateTime dDateFrom = infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime dDateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1).AddHours(8);
                string sShift = this.ddl_Shift.SelectedValue;
                string sDataType = this.ddl_dataType.SelectedValue;
                string sMachineID = this.ddL_MachineID.SelectedValue;

            

                DataTable dt = new DataTable();
                if (sDataType == DataType.Output)
                {
                    #region product output
                    Common.Class.BLL.MouldingViHistory_BLL MouldingBLL = new Common.Class.BLL.MouldingViHistory_BLL();
                    dt = MouldingBLL.getProductSummaryByMachine(dDateFrom, dDateTo, sShift);
                    #endregion
                }
                else if (sDataType == DataType.OEE)
                {
                    #region OEE New
                    // dt for showing chart
                    dt.Columns.Add("MachineID");
                    dt.Columns.Add("OEE_Percentage");
                    dt.Columns.Add("OEE_Availability"); //   prod time / schedule time = prod time / (prod time + adjustment time) * 100%
                    dt.Columns.Add("OEE_Performence");  //   actual prod QTY /PLan QTY   * 100%
                    dt.Columns.Add("OEE_Quality");      //   actual good  QTY /actual prod QTY   * 100% 


                    double Availability = 0;
                    double Performence = 0;
                    double Quality = 0;
                    double SpendingTime = 0;
                    double UnitCycleTime = 0;
                    double OutputQTY = 0;

                    #region machine status 

                    Common.BLL.LMMSEventLog_BLL EventLog = new Common.BLL.LMMSEventLog_BLL();
                    Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
                    Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                    DataTable dt_OEECount;


                    for (int i = 1; i < 9; i++)
                    {
                        DataRow dr_OEE = dt.NewRow();
                        dr_OEE["MachineID"] = "Machine" + i.ToString();

                        dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        dPoints = EventLog.getOEE(dDateFrom, dDateTo, i.ToString(), "para_sPartNo_notuse", sShift, "", false);
                        dt_OEECount = WatchDogBll.getOEEData(dDateFrom.ToString(), dDateTo.ToString(), i.ToString(), sShift, "", false);


                        if (dt_OEECount == null || dt_OEECount.Rows.Count == 0)
                        {
                            dr_OEE["OEE_Performence"] = "0%";
                            dr_OEE["OEE_Quality"] = "0%";
                        }
                        else
                        {
                            DataRow dr_OEECount = dt_OEECount.Rows[dt_OEECount.Rows.Count - 1];


                            Quality = Math.Round(double.Parse(dr_OEECount["OK"].ToString()) / double.Parse(dr_OEECount["OutputQTY"].ToString()) * 100, 2);

                            Quality = double.IsNaN(Quality) ? 0 : Quality;
                            

                            try
                            {
                                SpendingTime = double.Parse(dr_OEECount["ProcessTime"].ToString());// seconds
                            }
                            catch (Exception)
                            {
                                OutputQTY = 0;
                            }
                        }



                        if (dPoints == null || dPoints.Count == 0)
                        {
                            Availability = 0;
                        }
                        else
                        {
                            double PD_Time_Count = 0;
                            double Idle_Time_Count = 0;
                            double NoWIP_Time_Count = 0;
                            double Adjustmnet_Time_Count = 0;
                            double Shutdown_Time_Count = 0;
                            double BreakDown_Time_Count = 0;

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
                                        case StaticRes.Global.StatusType.ShutDown:
                                            {
                                                Shutdown_Time_Count++;
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

                            if (PD_Time_Count + NoWIP_Time_Count + Adjustmnet_Time_Count + Shutdown_Time_Count + BreakDown_Time_Count == 0)
                            {
                                Availability = 0;
                            }
                            else
                            {
                                Availability = PD_Time_Count / (PD_Time_Count + NoWIP_Time_Count + Adjustmnet_Time_Count + BreakDown_Time_Count + Shutdown_Time_Count) * 100;//少加一个shutdown time
                                Availability = double.IsNaN(Availability) ? 0 : Availability;

                                // Performence = OutputQTY / (PD_Time_Count * 60 / UnitCycleTime);
                                Performence = SpendingTime / 60 / PD_Time_Count * 100;

                                if (Performence > 100)
                                {
                                    Performence = 100;
                                }
                                else if (double.IsNaN(Performence))
                                {
                                    Performence = 0;
                                }
                            }

                            dr_OEE["OEE_Availability"] = Math.Round(Availability, 2).ToString() + "%";
                            dr_OEE["OEE_Performence"] = Math.Round(Performence, 2).ToString() + "%";
                            dr_OEE["OEE_Quality"] = Math.Round(Quality, 2).ToString() + "%";
                            dr_OEE["OEE_Percentage"] = Math.Round((Availability / 100) * (Performence / 100) * (Quality / 100) * 100, 2).ToString() + "%";
                        }

                        dt.Rows.Add(dr_OEE);
                    }
                    #endregion

                    #endregion
                }
                else
                {
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
                    dDateTo = dDateTo.AddDays(-1);
                    for (int i = 1; i < 9; i++)
                    {
                        DataRow dr = dt_Status.NewRow();
                        

                        Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        Points = MachineStatus_bll.getOEE(dDateFrom, dDateTo, i.ToString(),sShift, "", false);


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
                    dt = dt_Status.Copy();
                    #endregion
                }


                

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lblResult.BackColor = System.Drawing.Color.Red;
                    this.lblResult.Text = "There is no record! ";
                    this.lblResult.Visible = true;
                    this.ProdChart.Visible = false;
                }
                else
                {
                    this.lblResult.Visible = false;
                    this.ProdChart.Visible = true;
                 

                    if (sDataType == DataType.All)
                    {
                        OverAllChart(dt);
                    }
                    else
                    {
                        ChartDisplay_Job(dt);
                    }
                }


                if (this.ddl_dataType.SelectedValue != "" && this.ddL_MachineID.SelectedValue != "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Machine option is invalid for Overall Chart!');", true);
                }
            }
            catch (Exception ee)
            {
                this.lblResult.Visible = true;
                this.lblResult.BackColor = System.Drawing.Color.Red;
                this.lblResult.Text = ee.Message;
                DBHelp.Reports.LogFile.Log("MouldingProductRecordChart", "btnGenerate_Click Exception:" + ee.ToString());
            }
        }





        void OverAllChart(DataTable dt)
        {
            try
            {
                int machineCount = this.ddL_MachineID.Text == "" ? 8 : 1;

             
                #region convert datatable
                DataTable dt_AllStatus = new DataTable();
                dt_AllStatus.Columns.Add("Status");
                dt_AllStatus.Columns.Add("TotalTime", typeof(double));
                

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
                foreach (DataRow row in dt.Rows)
                {
                    Running_Count += double.Parse(row["Running"].ToString());
                    Adjustment_Count += double.Parse(row["Adjustment"].ToString());
                    NoWIP_Count += double.Parse(row["NoWIP"].ToString());
                    Mould_Testing_Count += double.Parse(row["Mould_Testing"].ToString());
                    Material_Testing_Count += double.Parse(row["Material_Testing"].ToString());
                    Change_Model_Count += double.Parse(row["Change_Model"].ToString());
                    No_Operator_Count += double.Parse(row["No_Operator"].ToString());
                    No_Material_Count += double.Parse(row["No_Material"].ToString());
                    Break_Time_Count += double.Parse(row["Break_Time"].ToString());
                    ShutDown_Count += double.Parse(row["ShutDown"].ToString());
                    Damage_Mould_Count += double.Parse(row["Damage_Mould"].ToString());
                    Machine_Break_Count += double.Parse(row["Machine_Break"].ToString());

                }

                DataRow dr_1 = dt_AllStatus.NewRow();
                DataRow dr_2 = dt_AllStatus.NewRow();
                DataRow dr_3 = dt_AllStatus.NewRow();
                DataRow dr_4 = dt_AllStatus.NewRow();
                DataRow dr_5 = dt_AllStatus.NewRow();
                DataRow dr_6 = dt_AllStatus.NewRow();
                DataRow dr_7 = dt_AllStatus.NewRow();
                DataRow dr_8 = dt_AllStatus.NewRow();
                DataRow dr_9 = dt_AllStatus.NewRow();
                DataRow dr_10 = dt_AllStatus.NewRow();
                DataRow dr_11 = dt_AllStatus.NewRow();
                DataRow dr_12 = dt_AllStatus.NewRow();

                dr_1[0] = "Running";
                dr_1[1] = Running_Count;
                dr_2[0] = "Adjustment";
                dr_2[1] = Adjustment_Count;
                dr_3[0] = "NoWIP";
                dr_3[1] = NoWIP_Count;
                dr_4[0] = "Mould Testing";
                dr_4[1] = Mould_Testing_Count;
                dr_5[0] = "Material Testing";
                dr_5[1] = Material_Testing_Count;
                dr_6[0] = "Change Model";
                dr_6[1] = Change_Model_Count;
                dr_7[0] = "No Operator";
                dr_7[1] = No_Operator_Count;
                dr_8[0] = "No Material";
                dr_8[1] = No_Material_Count;
                dr_9[0] = "Break Time";
                dr_9[1] = Break_Time_Count;
                dr_10[0] = "ShutDown";
                dr_10[1] = ShutDown_Count;
                dr_11[0] = "Damage Mould";
                dr_11[1] = Damage_Mould_Count;
                dr_12[0] = "Machine Break";
                dr_12[1] = Machine_Break_Count;


                dt_AllStatus.Rows.Add(dr_1);
                dt_AllStatus.Rows.Add(dr_2);
                dt_AllStatus.Rows.Add(dr_3);
                dt_AllStatus.Rows.Add(dr_4);
                dt_AllStatus.Rows.Add(dr_5);
                dt_AllStatus.Rows.Add(dr_6);
                dt_AllStatus.Rows.Add(dr_7);
                dt_AllStatus.Rows.Add(dr_8);
                dt_AllStatus.Rows.Add(dr_9);
                dt_AllStatus.Rows.Add(dr_10);
                dt_AllStatus.Rows.Add(dr_11);
                dt_AllStatus.Rows.Add(dr_12);

                dt_AllStatus.DefaultView.Sort = "TotalTime DESC";
                dt_AllStatus = dt_AllStatus.DefaultView.ToTable();
                #endregion

                this.ProdChart.Series.Clear();
                this.ProdChart.ChartAreas.Clear();
                this.ProdChart.ChartAreas.Add("Area1");

                this.ProdChart.ImageStorageMode = ImageStorageMode.UseImageLocation;

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

                //Y坐标轴标题
                ProdChart.ChartAreas[0].AxisY.Title = "Time Count";
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Time Count";


                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;

                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion

                #region Series
                Series Series_Status = new Series();
                Series_Status.ChartType = SeriesChartType.Column;
                Series_Status.ChartArea = this.ProdChart.ChartAreas[0].Name;
                Series_Status.Name = "Job Output";
                Series_Status.XAxisType = AxisType.Primary;
                Series_Status.XValueType = ChartValueType.String;
                Series_Status.YAxisType = AxisType.Primary;
                Series_Status.YValueType = ChartValueType.Double;
                Series_Status.IsVisibleInLegend = false;
                Series_Status.LabelForeColor = Color.SteelBlue;
                Series_Status.ToolTip = "#VALX -- #VAL" + "H";
                Series_Status.Color = Color.Lime;
                Series_Status.Palette = ChartColorPalette.Bright;
                #endregion

                double TotalSearchTime = 0;
                #region total search time
                DateTime dateFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dateTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                double DayTime_Shift = ddl_Shift.Text == StaticRes.Global.Shift.ALL ? 24 : 12;


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


                    if (ddl_Shift.Text == StaticRes.Global.Shift.Day)
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
                    else if (ddl_Shift.Text == StaticRes.Global.Shift.Night)
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


                foreach (DataRow dr in dt_AllStatus.Rows)
                {
                    #region 
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.AxisLabel = dr[0].ToString();  //status
                    dataPoint.YValues[0] = double.Parse(dr[1].ToString());
                    dataPoint.Label = dr[1].ToString() + "H - " + Math.Round(double.Parse(dr[1].ToString()) / TotalSearchTime / machineCount * 100, 2).ToString() + "%";//total


                    dataPoint.Color = System.Drawing.Color.SkyBlue;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;


                    Series_Status.Points.Add(dataPoint);
                    #endregion
                }

                this.ProdChart.Series.Add(Series_Status);

                //title
                ProdChart.Titles.Clear();
                ProdChart.Titles.Add("Overall Status");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("OutPut Chart", "ChartDisplay_Job Exception : " + ee.Message);
            }
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
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
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

                //Y坐标轴标题
                if (this.ddl_dataType.Text == DataType.OEE)
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Availability / Performence / Quality";
                    ProdChart.ChartAreas[0].AxisY.ToolTip = "Availability / Performence / Quality";
                }
                else if (this.ddl_dataType.Text == DataType.Output)
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Count";
                    ProdChart.ChartAreas[0].AxisY.ToolTip = "Count";
                }
                else if (this.ddl_dataType.Text == DataType.Utilization)
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Operation Status";
                    ProdChart.ChartAreas[0].AxisY.ToolTip = "Operation Status";
                }
                else
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Time Count";
                    ProdChart.ChartAreas[0].AxisY.ToolTip = "Time Count";
                }

                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;

                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


                #region AxisY2 css  for Output / OEE / Utilization
                if (this.ddl_dataType.Text == DataType.Output || this.ddl_dataType.Text == DataType.OEE || this.ddl_dataType.Text == DataType.Utilization)
                {
                    //Y坐标轴颜色
                    ProdChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                    ProdChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                    ProdChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                    //Y坐标轴标题
                    if (this.ddl_dataType.Text == DataType.OEE)
                    {
                        ProdChart.ChartAreas[0].AxisY2.Title = "Machine OEE %";
                        ProdChart.ChartAreas[0].AxisY2.ToolTip = "Machine OEE %";

                        //     ProdChart.ChartAreas[0].AxisY2.TitleFont = new System.Drawing.Font("微软雅黑",100F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point,0);
                    }
                    else if (this.ddl_dataType.Text == DataType.Output)
                    {
                        ProdChart.ChartAreas[0].AxisY2.Title = "Reject Rate %";
                        ProdChart.ChartAreas[0].AxisY2.ToolTip = "Reject Rate %";
                    }
                    else if (this.ddl_dataType.Text == DataType.Utilization)
                    {
                        ProdChart.ChartAreas[0].AxisY2.Title = " Utilization Rate %";
                        ProdChart.ChartAreas[0].AxisY2.ToolTip = " Utilization Rate %";
                        ProdChart.ChartAreas[0].AxisY.Maximum = 100;
                        ProdChart.ChartAreas[0].AxisY2.Maximum = 100;

                    }

                    ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                    ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                    ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                    //ProdChart.ChartAreas[0].AxisY2.ToolTip = "Reject Rate %";
                    //Y轴网格线条
                    ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                    ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                    ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                    ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                }
                #endregion

                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion


                #region Job out put & Machine Status  Series
                Series dataSeries_JobOutPut = new Series();
                dataSeries_JobOutPut.ChartType = SeriesChartType.Column;
                dataSeries_JobOutPut.ChartArea = this.ProdChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                dataSeries_JobOutPut.Name = "Job Output";
                dataSeries_JobOutPut.XAxisType = AxisType.Primary;
                dataSeries_JobOutPut.XValueType = ChartValueType.String;
                dataSeries_JobOutPut.YAxisType = AxisType.Primary;
                dataSeries_JobOutPut.YValueType = ChartValueType.Double;
                dataSeries_JobOutPut.IsVisibleInLegend = false;

                //Legend legend = new Legend("Job Output");
                //legend.Title = "Job Output";

                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                dataSeries_JobOutPut.LabelForeColor = Color.SteelBlue;
                if (this.ddl_dataType.SelectedValue == DataType.Output)
                {
                    dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
                }
                else if (this.ddl_dataType.SelectedValue == DataType.OEE)
                {
                    Legend legend = new Legend();
                    legend.Docking = Docking.Top;
                    legend.CustomItems.Add(System.Drawing.Color.DarkBlue, "OEE"); // 参数：(颜色, 说明)
                    legend.CustomItems[0].ImageStyle = LegendImageStyle.Line;
                    legend.CustomItems.Add(System.Drawing.Color.Red, "OEE Target");
                    legend.CustomItems[1].ImageStyle = LegendImageStyle.Line;
                    legend.CustomItems.Add(System.Drawing.Color.Purple, "Quality");
                    legend.CustomItems.Add(System.Drawing.Color.DeepSkyBlue, "Performence");
                    legend.CustomItems.Add(System.Drawing.Color.LawnGreen, "Availability");

                    ProdChart.Legends.Add(legend);
                    ProdChart.Legends[0].Position.Auto = false;
                    dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL" + "H";
                }
                else if (this.ddl_dataType.Text == DataType.Utilization)
                {
                    Legend legend = new Legend();
                    legend.Docking = Docking.Top;
                    legend.CustomItems.Add(System.Drawing.Color.Blue, "Utilization"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Running, "Running");


                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Adjustment, "Adjustment");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Schedule, "No Schedule");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Mould_Testing, "Mould Testing");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Material_Testing, "Material Testing");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Change_Model, "Change Model");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Operator, "No Operator");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.No_Material, "No Material");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.Break_Time, "Break Time");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.DamageMould, "Mould Damage");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.MachineBreak, "Machine Break");
                    legend.CustomItems.Add(StaticRes.MouldingStatusColor.ShutDown, "Shut Down");
                    

                    legend.CustomItems[0].ImageStyle = LegendImageStyle.Line;

                    ProdChart.Legends.Add(legend);
                    ProdChart.Legends[0].Position.Auto = false;
                    dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL" + "H";
                }
                else
                {
                    dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL" + "H";     //鼠标移动到对应点显示数值
                }


                dataSeries_JobOutPut.Color = Color.Lime;


                //dataSeries_JobOutPut.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //ProdChart.Legends.Add(legend);
                //ProdChart.Legends[0].Position.Auto = false;


                dataSeries_JobOutPut.Palette = ChartColorPalette.Bright;
                #endregion


                #region  Series
                Series dataSeries_OEE = new Series();
                Series dataSeries_OEETarget = new Series();
                Series dataSeries_availability = new Series();
                Series dataSeries_Performence = new Series();
                Series dataSeries_Quality = new Series();

                

                Series Utilization_Running = new Series();
                Series Utilization_Adjustment = new Series();
                Series Utilization_NoWIP = new Series();
                Series Utilization_MouldTesting = new Series();
                Series Utilization_MaterialTesting = new Series();
                Series Utilization_ChangeModel = new Series();
                Series Utilization_NoOperator = new Series();
                Series Utilization_NoMaterial = new Series();
                Series Utilization_BreakTime = new Series();
                Series Utilization_Shutdown= new Series();
                Series Utilization_DamageMould = new Series();
                Series Utilization_MachineBreak = new Series();
                Series Utilization_Rate = new Series();



                if (this.ddl_dataType.Text == DataType.OEE)
                {
                    #region series OEE
                    dataSeries_OEE.ChartType = SeriesChartType.Line;
                    dataSeries_OEE.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_OEE.Name = " OEE Percentage %";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_OEE.XAxisType = AxisType.Primary;
                    dataSeries_OEE.XValueType = ChartValueType.String;
                    dataSeries_OEE.YAxisType = AxisType.Secondary;
                    dataSeries_OEE.YValueType = ChartValueType.Double;
                    dataSeries_OEE.IsVisibleInLegend = true;
                    dataSeries_OEE.ToolTip = "OEE : #VAL" + "%";// "#VALX -- #VAL" + "%";     //鼠标移动到对应点显示数值 

                    dataSeries_OEE.MarkerColor = Color.White;
                    dataSeries_OEE.MarkerSize = 10;
                    dataSeries_OEE.MarkerStyle = MarkerStyle.Star6;
                    dataSeries_OEE.MarkerBorderWidth = 1;
                    dataSeries_OEE.MarkerBorderColor = Color.DarkBlue;
                    dataSeries_OEE.BorderWidth = 3;

                    //dataSeries_OEE.LegendPostBackValue = "#INDEX";
                    //dataSeries_OEE.LegendText = "#VAL";
                    #endregion

                    #region series OEE Target
                    dataSeries_OEETarget.ChartType = SeriesChartType.Spline;
                    dataSeries_OEETarget.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_OEETarget.Name = " OEE Target %";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_OEETarget.XAxisType = AxisType.Primary;
                    dataSeries_OEETarget.XValueType = ChartValueType.String;
                    dataSeries_OEETarget.YAxisType = AxisType.Secondary;
                    dataSeries_OEETarget.YValueType = ChartValueType.Double;
                    dataSeries_OEETarget.IsVisibleInLegend = true;
                    dataSeries_OEETarget.ToolTip = "OEE Target : #VAL" + "%";// "#VALX -- #VAL" + "%";     //鼠标移动到对应点显示数值 
                    dataSeries_OEETarget.BorderWidth = 3;

                    #endregion

                    #region series availability
                    dataSeries_availability.ChartType = SeriesChartType.StackedColumn;
                    dataSeries_availability.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_availability.Name = " OEE availability %";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_availability.XAxisType = AxisType.Primary;
                    dataSeries_availability.XValueType = ChartValueType.String;
                    dataSeries_availability.YAxisType = AxisType.Primary;
                    dataSeries_availability.YValueType = ChartValueType.Double;
                    dataSeries_availability.IsVisibleInLegend = true;
                    dataSeries_availability.ToolTip = "Availability : #VAL" + "%";     //鼠标移动到对应点显示数值 

                    //dataSeries_availability.LegendPostBackValue = "#INDEX";
                    //dataSeries_availability.LegendText = "#VAL";
                    //dataSeries_availability.MarkerColor = Color.White;
                    //dataSeries_availability.MarkerSize = 10;
                    //dataSeries_availability.MarkerStyle = MarkerStyle.Star6;
                    //dataSeries_availability.MarkerBorderWidth = 1;
                    //dataSeries_availability.MarkerBorderColor = Color.DarkBlue;
                    //dataSeries_availability.BorderWidth = 2;
                    #endregion

                    #region series Performence
                    dataSeries_Performence.ChartType = SeriesChartType.StackedColumn;
                    dataSeries_Performence.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_Performence.Name = " OEE Performence %";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_Performence.XAxisType = AxisType.Primary;
                    dataSeries_Performence.XValueType = ChartValueType.String;
                    dataSeries_Performence.YAxisType = AxisType.Primary;
                    dataSeries_Performence.YValueType = ChartValueType.Double;
                    dataSeries_Performence.IsVisibleInLegend = true;
                    dataSeries_Performence.ToolTip = "Performence : #VAL" + "%";     //鼠标移动到对应点显示数值 



                    //dataSeries_Performence.LegendPostBackValue = "#INDEX";
                    //dataSeries_Performence.LegendText = "#VAL";
                    //dataSeries_Performence.MarkerColor = Color.White;
                    //dataSeries_Performence.MarkerSize = 10;
                    //dataSeries_Performence.MarkerStyle = MarkerStyle.Star6;
                    //dataSeries_Performence.MarkerBorderWidth = 1;
                    //dataSeries_Performence.MarkerBorderColor = Color.DarkBlue;
                    //dataSeries_Performence.BorderWidth = 2;
                    #endregion

                    #region series Quality
                    dataSeries_Quality.ChartType = SeriesChartType.StackedColumn;
                    dataSeries_Quality.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_Quality.Name = " OEE Quality %";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_Quality.XAxisType = AxisType.Primary;
                    dataSeries_Quality.XValueType = ChartValueType.String;
                    dataSeries_Quality.YAxisType = AxisType.Primary;
                    dataSeries_Quality.YValueType = ChartValueType.Double;
                    dataSeries_Quality.IsVisibleInLegend = true;
                    dataSeries_Quality.ToolTip = "Quality : #VAL" + "%";     //鼠标移动到对应点显示数值 

                    //dataSeries_Quality.LegendPostBackValue = "#INDEX";
                    //dataSeries_Quality.LegendText = "#VAL";
                    //dataSeries_Quality.MarkerColor = Color.White;
                    //dataSeries_Quality.MarkerSize = 10;
                    //dataSeries_Quality.MarkerStyle = MarkerStyle.Star6;
                    //dataSeries_Quality.MarkerBorderWidth = 1;
                    //dataSeries_Quality.MarkerBorderColor = Color.DarkBlue;
                    //dataSeries_Quality.BorderWidth = 2;
                    #endregion
                }
                else if (this.ddl_dataType.Text == DataType.Utilization)
                {
                    Utilization_Running = Set_UtilizationSeries_CSS("Running");
                    Utilization_Adjustment = Set_UtilizationSeries_CSS("Adjustment");
                    Utilization_NoWIP = Set_UtilizationSeries_CSS("NoWIP");
                    Utilization_MouldTesting = Set_UtilizationSeries_CSS("MouldingTesting");
                    Utilization_MaterialTesting = Set_UtilizationSeries_CSS("MaterialTesting");
                    Utilization_ChangeModel = Set_UtilizationSeries_CSS("ChangeModel");
                    Utilization_NoOperator = Set_UtilizationSeries_CSS("NoOperator");
                    Utilization_NoMaterial = Set_UtilizationSeries_CSS("NoMaterial");
                    Utilization_BreakTime = Set_UtilizationSeries_CSS("BreakTime");
                    Utilization_Shutdown = Set_UtilizationSeries_CSS("Shutdown");
                    Utilization_DamageMould = Set_UtilizationSeries_CSS("DamageMould");
                    Utilization_MachineBreak = Set_UtilizationSeries_CSS("MachineBreak");

                    #region Utilization_Rate
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

                }
                #endregion



                double TotalSearchTime = 0;
                #region total search time
                DateTime dateFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dateTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                double DayTime_Shift = ddl_Shift.Text == StaticRes.Global.Shift.ALL ? 24 : 12;


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


                    if (ddl_Shift.Text == StaticRes.Global.Shift.Day)
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
                    else if (ddl_Shift.Text == StaticRes.Global.Shift.Night)
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



                double count = 0;
                bool hasData = false;
                foreach (DataRow x in dt.Rows)
                {
                    string sMachineID = "";
                    double dRunning = 0;
                    double dAdjustment = 0;
                    double dNoWIP = 0;
                    double dMouldTesting = 0;
                    double dMaterialTesting = 0;
                    double dChangeModel = 0;
                    double dNoOperator = 0;
                    double dNoMaterial = 0;
                    double dBreakTime = 0;
                    double dShutdown = 0;
                    double dDamageMould = 0;
                    double dMachineBreak = 0;

                    if (this.ddl_dataType.SelectedValue != DataType.OEE)
                    {
                       sMachineID = x["MachineID"].ToString();
                       dRunning = double.Parse(x["Running"].ToString());
                       dAdjustment = double.Parse(x["Adjustment"].ToString());
                       dNoWIP = double.Parse(x["NoWIP"].ToString());
                       dMouldTesting = double.Parse(x["Mould_Testing"].ToString());
                       dMaterialTesting = double.Parse(x["Material_Testing"].ToString());
                       dChangeModel = double.Parse(x["Change_Model"].ToString());
                       dNoOperator = double.Parse(x["No_Operator"].ToString());
                       dNoMaterial = double.Parse(x["No_Material"].ToString());
                       dBreakTime = double.Parse(x["Break_Time"].ToString());
                       dShutdown = double.Parse(x["ShutDown"].ToString());
                       dDamageMould = double.Parse(x["Damage_Mould"].ToString());
                       dMachineBreak = double.Parse(x["Machine_Break"].ToString());
                    }

                    switch (this.ddl_dataType.Text)
                    {
                        case DataType.Running:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dRunning;
                                dataPoint.Label = dRunning+ "H - " + Math.Round(dRunning / TotalSearchTime * 100, 2).ToString() + "%";//total
                                
                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;
                                
                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dRunning;
                                #endregion
                            }
                            break;
                        case DataType.Adjustment:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dAdjustment;
                                dataPoint.Label = dAdjustment + "H - " + Math.Round(dAdjustment / TotalSearchTime * 100, 2).ToString() + "%";//total
                                
                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dAdjustment;
                                #endregion
                            }
                            break;
                        case DataType.NoWIP:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dNoWIP;
                                dataPoint.Label = dNoWIP + "H - " + Math.Round(dNoWIP / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dNoWIP;
                                #endregion
                            }
                            break;
                        case DataType.Mould_Testing:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dMouldTesting;
                                dataPoint.Label = dMouldTesting + "H - " + Math.Round(dMouldTesting / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dMouldTesting;
                                #endregion
                            }
                            break;
                        case DataType.Material_Testing:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dMaterialTesting;
                                dataPoint.Label = dMaterialTesting + "H - " + Math.Round(dMaterialTesting / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dMaterialTesting;
                                #endregion
                            }
                            break;
                        case DataType.Change_Model:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dChangeModel;
                                dataPoint.Label = dChangeModel + "H - " + Math.Round(dChangeModel / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dChangeModel;
                                #endregion
                            }
                            break;

                        case DataType.No_Operator:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dNoOperator;
                                dataPoint.Label = dNoOperator + "H - " + Math.Round(dNoOperator / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dNoOperator;
                                #endregion
                            }
                            break;

                        case DataType.No_Material:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dNoMaterial;
                                dataPoint.Label = dNoMaterial + "H - " + Math.Round(dNoMaterial / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dNoMaterial;
                                #endregion
                            }
                            break;

                        case DataType.Break_Time:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dBreakTime;
                                dataPoint.Label = dBreakTime + "H - " + Math.Round(dBreakTime / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dBreakTime;
                                #endregion
                            }
                            break;

                        case DataType.ShutDown:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dShutdown;
                                dataPoint.Label = dShutdown + "H - " + Math.Round(dShutdown / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dShutdown;
                                #endregion
                            }
                            break;
                        case DataType.Damage_Mould:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dDamageMould;
                                dataPoint.Label = dDamageMould + "H - " + Math.Round(dDamageMould / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dDamageMould;
                                #endregion
                            }
                            break;
                        case DataType.Machine_Break:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = sMachineID;
                                dataPoint.YValues[0] = dMachineBreak;
                                dataPoint.Label = dMachineBreak + "H - " + Math.Round(dMachineBreak / TotalSearchTime * 100, 2).ToString() + "%";//total

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;


                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                count += dMachineBreak;
                                #endregion
                            }
                            break;

                        case DataType.Output:
                            {
                                #region 
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.AxisLabel = x[0].ToString();  //time --> machine No

                                try
                                {
                                    dataPoint.YValues[0] = double.Parse(x["OK"].ToString());//OK
                                }
                                catch
                                {
                                    dataPoint.YValues[0] = 0;
                                }

                                dataPoint.Color = System.Drawing.Color.SkyBlue;
                                dataPoint.Label = x["OK"].ToString() + "(" + x[3].ToString() + ")";//OK(total)
                                dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                                dataPoint.BackSecondaryColor = Color.SteelBlue;
                                dataPoint.BorderColor = Color.Black;
                                dataPoint.LabelForeColor = Color.Black;

                                count += dataPoint.YValues[0];

                                if (dataPoint.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_JobOutPut.Points.Add(dataPoint);
                                #endregion
                            }
                            break;

                        case DataType.OEE:
                            {
                                #region 

                                //dt.Columns.Add("MachineID");
                                //dt.Columns.Add("OEE_Percentage");
                                //dt.Columns.Add("OEE_Availability"); 
                                //dt.Columns.Add("OEE_Performence");  
                                //dt.Columns.Add("OEE_Quality");

                                #region availability

                                DataPoint dp_availabilitys = new DataPoint();
                                dp_availabilitys.Color = System.Drawing.Color.LawnGreen;
                                dp_availabilitys.BackSecondaryColor = Color.DarkGreen;
                                dp_availabilitys.BorderColor = Color.LawnGreen;
                                dp_availabilitys.BackGradientStyle = GradientStyle.LeftRight;
                                dp_availabilitys.LabelForeColor = Color.Black;


                                dp_availabilitys.AxisLabel = x[0].ToString();  //machine No

                                try
                                {
                                    dp_availabilitys.YValues[0] = double.Parse(x[2].ToString().Trim('%'));
                                    count += double.Parse(x[2].ToString().Trim('%'));
                                }
                                catch
                                {
                                    dp_availabilitys.YValues[0] = 0;
                                }

                                if (x[2].ToString().Trim('%') != "0")
                                {
                                    dp_availabilitys.Label = x[2].ToString();
                                }
                                else
                                {
                                    dp_availabilitys.Label = "0";
                                }





                                if (dp_availabilitys.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_availability.Points.Add(dp_availabilitys);


                                #endregion

                                #region Performence
                                DataPoint dp_Performence = new DataPoint();
                                dp_Performence.Color = System.Drawing.Color.DeepSkyBlue;
                                dp_Performence.BackSecondaryColor = Color.DeepSkyBlue;
                                dp_Performence.BorderColor = Color.DeepSkyBlue;
                                dp_Performence.BackGradientStyle = GradientStyle.LeftRight;
                                dp_Performence.LabelForeColor = Color.Black;


                                dp_Performence.AxisLabel = x[0].ToString();  //machine No

                                try
                                {
                                    dp_Performence.YValues[0] = double.Parse(x[3].ToString().Trim('%'));
                                    count += double.Parse(x[3].ToString().Trim('%'));
                                }
                                catch
                                {
                                    dp_Performence.YValues[0] = 0;
                                }
                                if (double.Parse(x[3].ToString().Trim('%')) != 0)
                                {
                                    dp_Performence.Label = x[3].ToString();
                                }
                                else
                                {
                                    dp_Performence.Label = "";
                                }





                                if (dp_Performence.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_Performence.Points.Add(dp_Performence);
                                #endregion

                                #region Quality
                                DataPoint dp_Quality = new DataPoint();
                                dp_Quality.Color = System.Drawing.Color.Purple;
                                dp_Quality.BackSecondaryColor = Color.Purple;
                                dp_Quality.BorderColor = Color.Purple;
                                dp_Quality.BackGradientStyle = GradientStyle.LeftRight;
                                dp_Quality.LabelForeColor = Color.Black;


                                dp_Quality.AxisLabel = x[0].ToString();  //machine No

                                try
                                {
                                    dp_Quality.YValues[0] = double.Parse(x[4].ToString().Trim('%'));
                                    count += double.Parse(x[4].ToString().Trim('%'));
                                }
                                catch
                                {
                                    dp_Quality.YValues[0] = 0;
                                }

                                if (double.Parse(x[4].ToString().Trim('%')) != 0)
                                {
                                    dp_Quality.Label = x[4].ToString();//total
                                }
                                else
                                {
                                    dp_Quality.Label = "";
                                }





                                if (dp_Quality.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_Quality.Points.Add(dp_Quality);
                                #endregion

                                #region OEE Percentage

                                DataPoint dp_OEE = new DataPoint();
                                dp_OEE.AxisLabel = x[0].ToString();



                                dp_OEE.YValues[0] = double.Parse(x[1].ToString().Trim('%'));

                                dp_OEE.Label = x[1].ToString();
                                dp_OEE.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
                                dp_OEE.BackGradientStyle = GradientStyle.LeftRight;
                                dp_OEE.LabelForeColor = Color.Yellow;
                                dp_OEE.Color = System.Drawing.Color.DarkBlue;
                                dp_OEE.BackSecondaryColor = Color.DarkBlue;
                                dp_OEE.BorderColor = Color.DarkBlue;
                                dp_OEE.MarkerStyle = MarkerStyle.Circle;
                                dp_OEE.MarkerBorderWidth = 2;



                                if (dp_OEE.YValues[0] > 0)
                                {
                                    hasData = hasData || true;
                                }
                                dataSeries_OEE.Points.Add(dp_OEE);
                                #endregion

                                #region  OEE Target 85%
                                DataPoint dp_OEETarget = new DataPoint();
                                dp_OEETarget.AxisLabel = x[0].ToString();
                                dp_OEETarget.YValues[0] = 85;
                                //dp_OEETarget.Label = 85 + "%";
                                dp_OEETarget.BackGradientStyle = GradientStyle.LeftRight;
                                dp_OEETarget.LabelForeColor = Color.Black;
                                dp_OEETarget.Color = System.Drawing.Color.Red;
                                dp_OEETarget.BackSecondaryColor = Color.DarkRed;
                                dp_OEETarget.BorderColor = Color.DarkRed;


                                dataSeries_OEETarget.Points.Add(dp_OEETarget);
                                #endregion

                                #endregion
                            }
                            break;
                        case DataType.Utilization:
                            {
                                #region
                                #region PD
                                DataPoint dp_PD = new DataPoint();
                                dp_PD.Color = StaticRes.MouldingStatusColor.Running;
                                dp_PD.BackSecondaryColor = StaticRes.MouldingStatusColor.Running_Dark;
                                dp_PD.BorderColor = Color.LawnGreen;
                                dp_PD.BackGradientStyle = GradientStyle.LeftRight;
                                dp_PD.LabelForeColor = Color.Black;

                                dp_PD.AxisLabel = sMachineID;
                                dp_PD.YValues[0] = double.IsNaN(Math.Round(dRunning * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dRunning * 100 / TotalSearchTime, 2);
                                if (dp_PD.YValues[0] != 0)
                                {
                                    dp_PD.Label = dRunning.ToString() + "H";
                                }
                                else
                                {
                                    dp_PD.Label = "";
                                }
                                Utilization_Running.Points.Add(dp_PD);
                                #endregion

                                #region Adjustment
                                DataPoint dp_Adjustment = new DataPoint();
                                dp_Adjustment.Color = StaticRes.MouldingStatusColor.Adjustment;
                                dp_Adjustment.BackSecondaryColor = StaticRes.MouldingStatusColor.Adjustment_Dark;
                                dp_Adjustment.BorderColor = StaticRes.MouldingStatusColor.Adjustment;
                                dp_Adjustment.BackGradientStyle = GradientStyle.LeftRight;
                                dp_Adjustment.LabelForeColor = Color.Black;

                                dp_Adjustment.AxisLabel = sMachineID;
                                dp_Adjustment.YValues[0] = double.IsNaN(Math.Round(dAdjustment * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dAdjustment * 100 / TotalSearchTime, 2);
                                if (dp_Adjustment.YValues[0] != 0)
                                {
                                    dp_Adjustment.Label = dAdjustment.ToString()+ "H";
                                }
                                else
                                {
                                    dp_Adjustment.Label = "";
                                }

                                Utilization_Adjustment.Points.Add(dp_Adjustment);
                                #endregion

                                #region NoWIP
                                DataPoint dp_NoWIP = new DataPoint();
                                dp_NoWIP.Color = StaticRes.MouldingStatusColor.No_Schedule;
                                dp_NoWIP.BackSecondaryColor = StaticRes.MouldingStatusColor.No_Schedule_Dark;
                                dp_NoWIP.BorderColor = StaticRes.MouldingStatusColor.No_Schedule;
                                dp_NoWIP.BackGradientStyle = GradientStyle.LeftRight;
                                dp_NoWIP.LabelForeColor = Color.Black;

                                dp_NoWIP.AxisLabel = sMachineID;
                                dp_NoWIP.YValues[0] = double.IsNaN(Math.Round(dNoWIP * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dNoWIP * 100 / TotalSearchTime, 2);
                                if (dp_NoWIP.YValues[0] != 0)
                                {
                                    dp_NoWIP.Label = dNoWIP.ToString() + "H";
                                }
                                else
                                {
                                    dp_NoWIP.Label = "";
                                }

                                Utilization_NoWIP.Points.Add(dp_NoWIP);
                                #endregion

                                #region Mould Testing
                                DataPoint dp_MouldTesting = new DataPoint();
                                dp_MouldTesting.Color = StaticRes.MouldingStatusColor.Mould_Testing;
                                dp_MouldTesting.BackSecondaryColor = StaticRes.MouldingStatusColor.Mould_Testing_Dark;
                                dp_MouldTesting.BorderColor = StaticRes.MouldingStatusColor.Mould_Testing;
                                dp_MouldTesting.BackGradientStyle = GradientStyle.LeftRight;
                                dp_MouldTesting.LabelForeColor = Color.Black;

                                dp_MouldTesting.AxisLabel = sMachineID;
                                dp_MouldTesting.YValues[0] = double.IsNaN(Math.Round(dMouldTesting * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dMouldTesting * 100 / TotalSearchTime, 2);
                                if (dp_MouldTesting.YValues[0] != 0)
                                {
                                    dp_MouldTesting.Label = dMouldTesting.ToString() + "H";
                                }
                                else
                                {
                                    dp_MouldTesting.Label = "";
                                }

                                Utilization_MouldTesting.Points.Add(dp_MouldTesting);
                                #endregion

                                #region Material Testing
                                DataPoint dp_MaterialTesting = new DataPoint();
                                dp_MaterialTesting.Color = StaticRes.MouldingStatusColor.Material_Testing;
                                dp_MaterialTesting.BackSecondaryColor = StaticRes.MouldingStatusColor.Material_Testing_Dark;
                                dp_MaterialTesting.BorderColor = StaticRes.MouldingStatusColor.Material_Testing;
                                dp_MaterialTesting.BackGradientStyle = GradientStyle.LeftRight;
                                dp_MaterialTesting.LabelForeColor = Color.Black;

                                dp_MaterialTesting.AxisLabel = sMachineID;
                                dp_MaterialTesting.YValues[0] = double.IsNaN(Math.Round(dMaterialTesting * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dMaterialTesting * 100 / TotalSearchTime, 2);
                                if (dp_MaterialTesting.YValues[0] != 0)
                                {
                                    dp_MaterialTesting.Label = dMaterialTesting + "H";
                                }
                                else
                                {
                                    dp_MaterialTesting.Label = "";
                                }

                                Utilization_MaterialTesting.Points.Add(dp_MaterialTesting);
                                #endregion
                                
                                #region Change Model
                                DataPoint dp_ChangeModel = new DataPoint();
                                dp_ChangeModel.Color = StaticRes.MouldingStatusColor.Change_Model;
                                dp_ChangeModel.BackSecondaryColor = StaticRes.MouldingStatusColor.Change_Model_Dark;
                                dp_ChangeModel.BorderColor = StaticRes.MouldingStatusColor.Change_Model;
                                dp_ChangeModel.BackGradientStyle = GradientStyle.LeftRight;
                                dp_ChangeModel.LabelForeColor = Color.Black;

                                dp_ChangeModel.AxisLabel = sMachineID;
                                dp_ChangeModel.YValues[0] = double.IsNaN(Math.Round(dChangeModel * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dChangeModel * 100 / TotalSearchTime, 2);
                                if (dp_ChangeModel.YValues[0] != 0)
                                {
                                    dp_ChangeModel.Label = dChangeModel.ToString() + "H";
                                }
                                else
                                {
                                    dp_ChangeModel.Label = "";
                                }

                                Utilization_ChangeModel.Points.Add(dp_ChangeModel);
                                #endregion
                                
                                #region No Operator
                                DataPoint dp_NoOperator = new DataPoint();
                                dp_NoOperator.Color = StaticRes.MouldingStatusColor.No_Operator;
                                dp_NoOperator.BackSecondaryColor = StaticRes.MouldingStatusColor.No_Operator_Dark;
                                dp_NoOperator.BorderColor = StaticRes.MouldingStatusColor.No_Operator;
                                dp_NoOperator.BackGradientStyle = GradientStyle.LeftRight;
                                dp_NoOperator.LabelForeColor = Color.Black;

                                dp_NoOperator.AxisLabel = sMachineID;
                                dp_NoOperator.YValues[0] = double.IsNaN(Math.Round(dNoOperator * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dNoOperator * 100 / TotalSearchTime, 2);
                                if (dp_NoOperator.YValues[0] != 0)
                                {
                                    dp_NoOperator.Label = dNoOperator.ToString() + "H";
                                }
                                else
                                {
                                    dp_NoOperator.Label = "";
                                }

                                Utilization_NoOperator.Points.Add(dp_NoOperator);
                                #endregion

                                #region No Material
                                DataPoint dp_NoMaterial = new DataPoint();
                                dp_NoMaterial.Color = StaticRes.MouldingStatusColor.No_Material;
                                dp_NoMaterial.BackSecondaryColor = StaticRes.MouldingStatusColor.No_Material_Dark;
                                dp_NoMaterial.BorderColor = StaticRes.MouldingStatusColor.No_Material;
                                dp_NoMaterial.BackGradientStyle = GradientStyle.LeftRight;
                                dp_NoMaterial.LabelForeColor = Color.Black;

                                dp_NoMaterial.AxisLabel = sMachineID;
                                dp_NoMaterial.YValues[0] = double.IsNaN(Math.Round(dNoMaterial * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dNoMaterial * 100 / TotalSearchTime, 2);
                                if (dp_NoMaterial.YValues[0] != 0)
                                {
                                    dp_NoMaterial.Label = dNoMaterial.ToString() + "H";
                                }
                                else
                                {
                                    dp_NoMaterial.Label = "";
                                }

                                Utilization_NoMaterial.Points.Add(dp_NoMaterial);
                                #endregion
                                
                                #region Break Time
                                DataPoint dp_BreakTime = new DataPoint();
                                dp_BreakTime.Color = StaticRes.MouldingStatusColor.Break_Time;
                                dp_BreakTime.BackSecondaryColor = StaticRes.MouldingStatusColor.Break_Time_Dark;
                                dp_BreakTime.BorderColor = StaticRes.MouldingStatusColor.Break_Time;
                                dp_BreakTime.BackGradientStyle = GradientStyle.LeftRight;
                                dp_BreakTime.LabelForeColor = Color.Black;

                                dp_BreakTime.AxisLabel = sMachineID;
                                dp_BreakTime.YValues[0] = double.IsNaN(Math.Round(dBreakTime * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dBreakTime * 100 / TotalSearchTime, 2);
                                if (dp_BreakTime.YValues[0] != 0)
                                {
                                    dp_BreakTime.Label = dBreakTime.ToString() + "H";
                                }
                                else
                                {
                                    dp_BreakTime.Label = "";
                                }

                                Utilization_BreakTime.Points.Add(dp_BreakTime);
                                #endregion
                                
                                #region Damage Mould
                                DataPoint dp_DamageMould = new DataPoint();
                                dp_DamageMould.Color = StaticRes.MouldingStatusColor.DamageMould;
                                dp_DamageMould.BackSecondaryColor = StaticRes.MouldingStatusColor.DamageMould_Dark;
                                dp_DamageMould.BorderColor = StaticRes.MouldingStatusColor.DamageMould;
                                dp_DamageMould.BackGradientStyle = GradientStyle.LeftRight;
                                dp_DamageMould.LabelForeColor = Color.Black;

                                dp_DamageMould.AxisLabel = sMachineID;
                                dp_DamageMould.YValues[0] = double.IsNaN(Math.Round(dDamageMould * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dDamageMould * 100 / TotalSearchTime, 2);
                                if (dp_DamageMould.YValues[0] != 0)
                                {
                                    dp_DamageMould.Label = dDamageMould.ToString() + "H";
                                }
                                else
                                {
                                    dp_DamageMould.Label = "";
                                }

                                Utilization_DamageMould.Points.Add(dp_DamageMould);
                                #endregion

                                #region Machine Break
                                DataPoint dp_MachineBreak = new DataPoint();
                                dp_MachineBreak.Color = StaticRes.MouldingStatusColor.MachineBreak;
                                dp_MachineBreak.BackSecondaryColor = StaticRes.MouldingStatusColor.MachineBreak_Dark;
                                dp_MachineBreak.BorderColor = StaticRes.MouldingStatusColor.MachineBreak;
                                dp_MachineBreak.BackGradientStyle = GradientStyle.LeftRight;
                                dp_MachineBreak.LabelForeColor = Color.Black;

                                dp_MachineBreak.AxisLabel = sMachineID;
                                dp_MachineBreak.YValues[0] = double.IsNaN(Math.Round(dMachineBreak * 100 / TotalSearchTime, 2)) ? 0 : Math.Round(dMachineBreak * 100 / TotalSearchTime, 2);
                                if (dp_MachineBreak.YValues[0] != 0)
                                {
                                    dp_MachineBreak.Label = dMachineBreak.ToString() + "H";
                                }
                                else
                                {
                                    dp_MachineBreak.Label = "";
                                }

                                Utilization_MachineBreak.Points.Add(dp_MachineBreak);
                                #endregion


                                #region ShutDown
                                DataPoint dp_ShutDown = new DataPoint();
                                dp_ShutDown.Color = StaticRes.MouldingStatusColor.ShutDown;
                                dp_ShutDown.BackSecondaryColor = StaticRes.MouldingStatusColor.ShutDown_Dark;
                                dp_ShutDown.BorderColor = StaticRes.MouldingStatusColor.ShutDown;
                                dp_ShutDown.BackGradientStyle = GradientStyle.LeftRight;
                                dp_ShutDown.LabelForeColor = Color.Black;

                                dp_ShutDown.AxisLabel = sMachineID;


                                dp_ShutDown.YValues[0] = Math.Round((TotalSearchTime - dRunning - dAdjustment  - dNoWIP - dMouldTesting - dMaterialTesting - dChangeModel - dNoOperator - dNoMaterial - dBreakTime -dDamageMould - dMachineBreak) / TotalSearchTime * 100, 2);

                                dp_ShutDown.YValues[0] = dp_ShutDown.YValues[0] < 0 ? 0 : dp_ShutDown.YValues[0];

                                if (dRunning + dAdjustment + dNoWIP + dMouldTesting + dMaterialTesting + dChangeModel + dNoOperator + dNoMaterial + dBreakTime + dDamageMould + dMachineBreak == 0)
                                {
                                    dp_ShutDown.Label = TotalSearchTime + "H";
                                }

                                dp_ShutDown.Label = Math.Round((TotalSearchTime - dRunning - dAdjustment - dNoWIP - dMouldTesting - dMaterialTesting - dChangeModel - dNoOperator - dNoMaterial - dBreakTime - dDamageMould - dMachineBreak) / TotalSearchTime * 100, 2).ToString() + "H";


                                Utilization_Shutdown.Points.Add(dp_ShutDown);
                                #endregion



                                #region Utilization Rate
                                DataPoint dp_Rate = new DataPoint();
                                dp_Rate.AxisLabel = sMachineID;
                                double rate = Math.Round(dRunning / TotalSearchTime * 100, 2);
                                dp_Rate.YValues[0] = double.IsNaN(rate) ? 0 : rate;
                                dp_Rate.Label = (double.IsNaN(rate) ? 0 : rate).ToString() + "%";
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
                            break;

                        default:
                            {

                            }
                            break;
                    }
                }


                if (this.ddl_dataType.Text == DataType.Output)
                {
                    this.ProdChart.Series.Add(dataSeries_JobOutPut);

                    #region  rejrate 
                    Series dataSeries_RejRate = new Series();
                    dataSeries_RejRate.ChartType = SeriesChartType.Line;
                    dataSeries_RejRate.ChartArea = this.ProdChart.ChartAreas[0].Name;
                    dataSeries_RejRate.Name = "Reject Rate";
                    // dataSeries_RejRate.SelectionEnabled = true;
                    // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                    dataSeries_RejRate.XAxisType = AxisType.Primary;
                    dataSeries_RejRate.XValueType = ChartValueType.String;
                    dataSeries_RejRate.YAxisType = AxisType.Secondary;
                    dataSeries_RejRate.YValueType = ChartValueType.Double;
                    dataSeries_RejRate.IsVisibleInLegend = false;
                    //dataSeries_RejRate.ToolTip = "#VALX -- #VAL" + "%";     //鼠标移动到对应点显示数值
                    dataSeries_RejRate.ToolTip = "NG: #VAL";     //鼠标移动到对应点显示数值

                    dataSeries_RejRate.MarkerColor = Color.White;
                    dataSeries_RejRate.MarkerSize = 10;
                    dataSeries_RejRate.MarkerStyle = MarkerStyle.Circle;
                    dataSeries_RejRate.MarkerBorderWidth = 1;
                    dataSeries_RejRate.MarkerBorderColor = Color.DarkRed;
                    dataSeries_RejRate.BorderWidth = 2;

                    hasData = false;
                    foreach (DataRow x in dt.Rows)
                    {
                        DataPoint dp_RejRate = new DataPoint();
                        dp_RejRate.AxisLabel = x[0].ToString();//machine ID
                        //dp_RejRate.YValues[0] =  double.Parse(x[4].ToString().Trim(char.Parse("%")));//RejRate
                        //dp_RejRate.Label = "NG:" + x[2].ToString();//NGCount


                        dp_RejRate.YValues[0] = double.Parse(x[2].ToString());
                        //dp_RejRate.Label = "NG:" + x[4].ToString();//RejRate
                        dp_RejRate.Label = x[4].ToString();//RejRate
                        dp_RejRate.Color = System.Drawing.Color.OrangeRed;


                        dp_RejRate.MarkerBorderWidth = 2;
                        dp_RejRate.MarkerStyle = MarkerStyle.Circle;
                        dp_RejRate.BorderColor = Color.Black;
                        dp_RejRate.LabelForeColor = Color.Black;
                       

                        //dp_RejRate.MarkerColor = Color.Red;
                        //dp_RejRate.MarkerStyle = MarkerStyle.;


                        if (dp_RejRate.YValues[0] > 0)
                        {
                            hasData = hasData || true;
                        }
                        dataSeries_RejRate.Points.Add(dp_RejRate);
                    }

                    this.ProdChart.Series.Add(dataSeries_RejRate);
                    
                    #endregion
                }
                else if (this.ddl_dataType.Text == DataType.OEE)
                {
                    this.ProdChart.Series.Add(dataSeries_availability);
                    this.ProdChart.Series.Add(dataSeries_Performence);
                    this.ProdChart.Series.Add(dataSeries_Quality);
                    this.ProdChart.Series.Add(dataSeries_OEETarget);
                    this.ProdChart.Series.Add(dataSeries_OEE);
                }
                else if (this.ddl_dataType.Text == DataType.Utilization)
                {
                    this.ProdChart.Series.Add(Utilization_Running);
                    this.ProdChart.Series.Add(Utilization_Adjustment);
                    this.ProdChart.Series.Add(Utilization_NoWIP);
                    this.ProdChart.Series.Add(Utilization_MouldTesting);
                    this.ProdChart.Series.Add(Utilization_MaterialTesting);
                    this.ProdChart.Series.Add(Utilization_ChangeModel);
                    this.ProdChart.Series.Add(Utilization_NoOperator);
                    this.ProdChart.Series.Add(Utilization_NoMaterial);
                    this.ProdChart.Series.Add(Utilization_BreakTime);
                    this.ProdChart.Series.Add(Utilization_Shutdown);
                    this.ProdChart.Series.Add(Utilization_DamageMould);
                    this.ProdChart.Series.Add(Utilization_MachineBreak);

                    this.ProdChart.Series.Add(Utilization_Rate);
                }
                else
                {
                    this.ProdChart.Series.Add(dataSeries_JobOutPut);
                }


                ProdChart.Titles.Clear();
                ProdChart.Titles.Add("Machine " + this.ddl_dataType.Text + " Data ");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;

           

                if (count == 0 && this.ddl_dataType.SelectedValue != DataType.Output && this.ddl_dataType.SelectedValue != DataType.Utilization)
                {
                    //this.ProdChart.Visible = false;
                    ProdChart.ChartAreas[0].AxisY.Maximum = 100;
                    this.lblResult.Visible = true;
                    this.lblResult.BackColor = System.Drawing.Color.Red;
                    this.lblResult.Text = "There is no record! ";
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("OutPut Chart", "ChartDisplay_Job Exception : " + ee.Message);
            }
        }



        System.Web.UI.DataVisualization.Charting.Series Set_UtilizationSeries_CSS(string type)
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.StackedColumn;
            series.ChartArea = this.ProdChart.ChartAreas[0].Name;
            // dataSeries_JobOutPut.L = true;
            series.Name = type;
            series.XAxisType = AxisType.Primary;
            series.XValueType = ChartValueType.String;
            series.YAxisType = AxisType.Primary;
            series.YValueType = ChartValueType.Double;
            series.IsVisibleInLegend = true;

            //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
            series.LabelForeColor = Color.SteelBlue;
            //series.ToolTip = "#VALX -- #VAL" + "%";     //鼠标移动到对应点显示数值
            series.ToolTip = type + " -- #VAL" + "%";


            Legend legend = new Legend("Operation Status");
            series.Color = Color.Lime;
            series.LegendText = legend.Name;
            // dataSeries_JobOutPut.IsValueShownAsLabel = true;
            // dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";
            series.Palette = ChartColorPalette.Bright;

            return series;
        }


    }
}