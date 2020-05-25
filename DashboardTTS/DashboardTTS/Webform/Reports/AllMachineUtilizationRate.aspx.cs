using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;

namespace DashboardTTS.Webform.Reports
{
    public partial class AllMachineUtilizationRate : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;
                    

                    btnGenerate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("AllMachineUtilizationRate", "Page_Load Exception: " + ee.ToString());
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                #region old version  no use
                //if (sType == "Utilization")
                //{
                //    ShowUtilization();
                //}
                //else if (sType == "Output")
                //{
                //    ShowOutput();
                //}
                #endregion


                DataTable dt = InitDT();

                DateTime dateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime dateTo = this.infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1).AddHours(8);
                string shift = this.ddlShift.SelectedItem.Value;
                string dateNotIn = this.txt_DateNotIn.Text;
                bool exceptWeekends = this.ckbExceptWeekend.Checked;

                string sType = this.ddlType.SelectedItem.Value;




                if (sType == "Output")
                {
                    #region output
                    Common.Class.BLL.MouldingViHistory_BLL mouldBLL = new Common.Class.BLL.MouldingViHistory_BLL();
                    DataTable dtMouldOutput = mouldBLL.GetOuputForAllMachineChart(dateFrom, dateTo, shift, dateNotIn, exceptWeekends);
                    DataRow dr = dt.NewRow();
                    if (dtMouldOutput != null && dtMouldOutput.Rows.Count != 0)
                    {
                       
                        dr["Department"] = StaticRes.Global.Department.Moulding;
                        dr["TotalOutput"] = dtMouldOutput.Rows[0]["TotalOutput"].ToString();
                        dr["TotalRejQty"] = dtMouldOutput.Rows[0]["TotalRej"].ToString();
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr["Department"] = StaticRes.Global.Department.Moulding;
                        dr["TotalOutput"] = 0;
                        dr["TotalRejQty"] = 0;
                        dt.Rows.Add(dr);
                    }
                    


                    Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                    DataTable dtPaintOutput = paintBLL.GetOuputForAllMachineChart(dateFrom, dateTo, shift, dateNotIn, exceptWeekends);
                    dr = dt.NewRow();
                    if (dtPaintOutput != null && dtPaintOutput.Rows.Count != 0)
                    {
                        dr["Department"] = StaticRes.Global.Department.Painting;
                        dr["TotalOutput"] = dtPaintOutput.Rows[0]["TotalOutput"].ToString();
                        dr["TotalRejQty"] = dtPaintOutput.Rows[0]["TotalRej"].ToString();
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr["Department"] = StaticRes.Global.Department.Painting;
                        dr["TotalOutput"] = 0;
                        dr["TotalRejQty"] = 0;
                        dt.Rows.Add(dr);
                    }
                   


                    Common.BLL.LMMSWatchLog_BLL laserBLL = new Common.BLL.LMMSWatchLog_BLL();
                    DataTable dtLaserOutput = laserBLL.GetOuputForAllMachineChart(dateFrom, dateTo, shift, dateNotIn, exceptWeekends);
                    dr = dt.NewRow();
                    if (dtLaserOutput != null && dtLaserOutput.Rows.Count != 0)
                    {
                        dr["Department"] = StaticRes.Global.Department.Laser;
                        dr["TotalOutput"] = dtLaserOutput.Rows[0]["TotalOutput"].ToString();
                        dr["TotalRejQty"] = dtLaserOutput.Rows[0]["TotalRej"].ToString();
                        dt.Rows.Add(dr);
                    }else
                    {
                        dr["Department"] = StaticRes.Global.Department.Laser;
                        dr["TotalOutput"] = 0;
                        dr["TotalRejQty"] = 0;
                        dt.Rows.Add(dr);
                    }
                  


                    Common.Class.BLL.PQCQaViTracking_BLL pqcBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
                    DataTable dtPQCOutput = pqcBLL.GetOuputForAllMachineChart(dateFrom, dateTo, shift, dateNotIn, exceptWeekends);
                    dr = dt.NewRow();
                    if (dtPQCOutput != null && dtPQCOutput.Rows.Count != 0)
                    {
                        dr["Department"] = StaticRes.Global.Department.PQC;
                        dr["TotalOutput"] = dtPQCOutput.Rows[0]["TotalOutput"].ToString();
                        dr["TotalRejQty"] = dtPQCOutput.Rows[0]["TotalRej"].ToString();
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr["Department"] = StaticRes.Global.Department.PQC;
                        dr["TotalOutput"] = 0;
                        dr["TotalRejQty"] = 0;
                        dt.Rows.Add(dr);
                    }

                    dr = dt.NewRow();
                    dr["Department"] = StaticRes.Global.Department.Assembly;
                    dr["TotalOutput"] = 0;
                    dr["TotalRejQty"] = 0;
                    dt.Rows.Add(dr);
                    #endregion
                }
                else if (sType == "Utilization")
                {
                    #region Utilization

                    DateTime dDatefrom = infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                    DateTime dDateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddHours(8).AddDays(1);
                    double totalHours = GetTotalTime(dDatefrom, dDateTo);

                    

                    #region moulding


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
                        DataRow drStatusNew = dtStatus.NewRow();

                        Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        Points = MachineStatus_bll.getOEE(dDatefrom, dDateTo, i.ToString(), this.ddlShift.SelectedItem.Value,txt_DateNotIn.Text,ckbExceptWeekend.Checked);//2018 12 04  by wei lijia , add date not in & except weekend


                        if (Points == null || Points.Count == 0)
                        {
                            #region default 0
                            drStatusNew["MachineID"] = "Machine" + i.ToString();
                            drStatusNew["Running"] = 0;
                            drStatusNew["Adjustment"] = 0;
                            drStatusNew["NoWIP"] = 0;
                            drStatusNew["Mould_Testing"] = 0;
                            drStatusNew["Material_Testing"] = 0;
                            drStatusNew["Change_Model"] = 0;
                            drStatusNew["No_Operator"] = 0;
                            drStatusNew["No_Material"] = 0;
                            drStatusNew["Break_Time"] = 0;
                            drStatusNew["ShutDown"] = 0;
                            drStatusNew["Damage_Mould"] = 0;
                            drStatusNew["Machine_Break"] = 0;
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

                            drStatusNew["MachineID"] = "Machine" + i.ToString();
                            drStatusNew["Running"] = Math.Round(Running_Count / 60, 2);
                            drStatusNew["Adjustment"] = Math.Round(Adjustment_Count / 60, 2);
                            drStatusNew["NoWIP"] = Math.Round(NoWIP_Count / 60, 2);
                            drStatusNew["Mould_Testing"] = Math.Round(Mould_Testing_Count / 60, 2);
                            drStatusNew["Material_Testing"] = Math.Round(Material_Testing_Count / 60, 2);
                            drStatusNew["Change_Model"] = Math.Round(Change_Model_Count / 60, 2);
                            drStatusNew["No_Operator"] = Math.Round(No_Operator_Count / 60, 2);
                            drStatusNew["No_Material"] = Math.Round(No_Material_Count / 60, 2);
                            drStatusNew["Break_Time"] = Math.Round(Break_Time_Count / 60, 2);
                            drStatusNew["ShutDown"] = Math.Round(ShutDown_Count / 60, 2);
                            drStatusNew["Damage_Mould"] = Math.Round(Damage_Mould_Count / 60, 2);
                            drStatusNew["Machine_Break"] = Math.Round(Machine_Break_Count / 60, 2);
                        }

                        dtStatus.Rows.Add(drStatusNew);
                    }

                    double mouldingTotalRunningTime = 0;
                    foreach (DataRow drStatus in dtStatus.Rows)
                    {
                        string machineID = drStatus["MachineID"].ToString().Replace("Machine", "").Trim();
                        double runningTime = double.Parse(drStatus["Running"].ToString());

                        mouldingTotalRunningTime += runningTime;
                    }
                    #endregion


                    DataRow dr = dt.NewRow();
                    dr["Department"] = StaticRes.Global.Department.Moulding;

                    dr["TotalRunningHours"] = mouldingTotalRunningTime;

                    dr["Utilization"] = Math.Round(mouldingTotalRunningTime / totalHours / 8 * 100, 2);
                    dt.Rows.Add(dr);
                    #endregion



                    #region laser
                    dr = dt.NewRow();

                    Common.BLL.LMMSEventLog_BLL laserBLL = new Common.BLL.LMMSEventLog_BLL();
                    List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> models = laserBLL.GetStatusModelList(dateFrom.Date, dateTo.Date, "", "", shift,false);
                    if (models ==  null || models.Count() == 0)
                    {
                        dr["Department"] = StaticRes.Global.Department.Laser;
                        dr["Utilization"] = 0;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        var laserResult = from a in models
                                          where !txt_DateNotIn.Text.Split(',').Contains(a.day.Day.ToString())
                                          && a.status == "RUNNING"
                                          select a;
                        double runningHours = laserResult.Sum(p => p.totalSeconds) / 3600;

                        dr["Department"] = StaticRes.Global.Department.Laser;

                        dr["TotalRunningHours"] = runningHours;

                        dr["Utilization"] = Math.Round(runningHours / totalHours / 8 * 100, 2).ToString("0.00");
                        dt.Rows.Add(dr);
                    }
                    #endregion

                    //painting
                    dr = dt.NewRow();
                    dr["Department"] = StaticRes.Global.Department.Painting;
                    dr["TotalRunningHours"] = 0;
                    dr["Utilization"] = 0;
                    dt.Rows.Add(dr);

                    //pqc
                    dr = dt.NewRow();
                    dr["Department"] = StaticRes.Global.Department.PQC;
                    dr["TotalRunningHours"] = 0;
                    dr["Utilization"] = 0;
                    dt.Rows.Add(dr);

                    //assembly
                    dr = dt.NewRow();
                    dr["Department"] = StaticRes.Global.Department.Assembly;
                    dr["TotalRunningHours"] = 0;
                    dr["Utilization"] = 0;
                    dt.Rows.Add(dr);
                    #endregion
                }


                ShowChart(dt);
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("AllMachineUtilizationRate", "btnGenerate_Click Exception: " + ee.ToString());
            }
        }

        void ShowChart(DataTable dt)
        {
            try
            {
                chartUsage.Series.Clear();
                chartUsage.ChartAreas.Clear();
                chartUsage.ChartAreas.Add("Area1");

                chartUsage.ImageStorageMode = ImageStorageMode.UseImageLocation;


                #region Chart Css
                chartUsage.BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart.BackSecondaryColor = Color.Transparent;
                chartUsage.BackGradientStyle = GradientStyle.None;


                //图表区背景
                chartUsage.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
                chartUsage.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                chartUsage.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                // ProdChart.ChartAreas[0].AxisX.Interval = 0;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                chartUsage.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                chartUsage.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                chartUsage.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                chartUsage.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

                //X坐标轴颜色
                chartUsage.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                chartUsage.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
                chartUsage.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

                //X轴网络线条
                chartUsage.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chartUsage.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

                //Y坐标轴颜色
                chartUsage.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
                chartUsage.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
                chartUsage.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                //Y坐标轴标题
                chartUsage.ChartAreas[0].AxisY.Title = this.ddlType.SelectedItem.Value;
                chartUsage.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                chartUsage.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                chartUsage.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                chartUsage.ChartAreas[0].AxisY.ToolTip = "Utilization Rate";
                //Y轴网格线条
                chartUsage.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chartUsage.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartUsage.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                chartUsage.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                if (this.ddlType.SelectedItem.Value == "Utilization")
                {
                    chartUsage.ChartAreas[0].AxisY.Maximum = 100;
                }
             


                //Y坐标轴颜色
                chartUsage.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                chartUsage.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                chartUsage.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                //Y坐标轴标题
                chartUsage.ChartAreas[0].AxisY2.Title = "Rej%";
                chartUsage.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                chartUsage.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                chartUsage.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                chartUsage.ChartAreas[0].AxisY2.ToolTip = "Rej%";
                //Y轴网格线条
                chartUsage.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                chartUsage.ChartAreas[0].AxisY2.Maximum = 20;

                chartUsage.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion



                #region legend
                //Legend legend = new Legend();
                //legend.Docking = Docking.Top; // 设置摆放位置
                //legend.Alignment = System.Drawing.StringAlignment.Near; // 对齐


                //legend.CustomItems.Add(System.Drawing.Color.Green, "Moulding"); // 参数：(颜色, 说明)
                //legend.CustomItems.Add(System.Drawing.Color.Red, "Painting");
                //legend.CustomItems.Add(System.Drawing.Color.Blue, "Laser");

                //legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;// 设置显示样式，色块、线条等
                //legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
                //legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;

                //this.chartUsage.Legends.Add(legend);
                #endregion


                string type = this.ddlType.SelectedItem.Value;
                if (type == "Output")
                {

                    this.chartUsage.Series.Add(outputSeries(dt));
                    this.chartUsage.Series.Add(rejSeries(dt));
                }
                else if (type == "Utilization")
                {
                    this.chartUsage.Series.Add(utilizationSeries(dt));
                }

               


                this.chartUsage.Titles.Clear();

                if (type == "Output")
                {
                    this.chartUsage.Titles.Add("Each Section Output & REJ Rate");
                }
                else if (type == "Utilization")
                {
                    this.chartUsage.Titles.Add("Each Section Utilization Chart");
                }
                
                this.chartUsage.Titles[0].ForeColor = Color.Black;
                this.chartUsage.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                this.chartUsage.Titles[0].Alignment = ContentAlignment.TopCenter;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("AllMachineUtilizationRate", "ShowUtilizationChart Exception : " + ee.ToString());
            }
        }



        public Series outputSeries(DataTable dt)
        {
            #region series style
            Series seriesOutput = new Series();
            seriesOutput.ChartType = SeriesChartType.Column;
            seriesOutput.ChartArea = this.chartUsage.ChartAreas[0].Name;
            seriesOutput.Name = "Output";
            seriesOutput.XAxisType = AxisType.Primary;
            seriesOutput.XValueType = ChartValueType.String;
            seriesOutput.YAxisType = AxisType.Primary;
            seriesOutput.YValueType = ChartValueType.Double;
            seriesOutput.IsVisibleInLegend = false;
            //seriesOutput.Label = "#VAL";                //设置显示X Y的值    
            seriesOutput.LabelForeColor = Color.SteelBlue;
            seriesOutput.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
            seriesOutput.Color = Color.Lime;
            //seriesOutput.LegendText = legend.Name;
            // seriesOutput.IsValueShownAsLabel = true;
            //  seriesOutput.CustomProperties = "DrawingStyle = Cylinder";
            seriesOutput.Palette = ChartColorPalette.Bright;
            #endregion


            #region add point
            foreach (DataRow dr in dt.Rows)
            {
                #region point style
                DataPoint dpOutput = new DataPoint();

                dpOutput.Color = StaticRes.MouldingStatusColor.Running;
                dpOutput.BackSecondaryColor = StaticRes.MouldingStatusColor.Running_Dark;
                dpOutput.BackGradientStyle = GradientStyle.LeftRight;
                dpOutput.BorderColor = Color.Black;
                dpOutput.LabelForeColor = Color.Black;
                #endregion


                string dept = dr["Department"].ToString();

              
                int output = int.Parse(dr["TotalOutput"].ToString());
                int rej = int.Parse(dr["TotalRejQty"].ToString());


                dpOutput.AxisLabel = dept;
                dpOutput.YValues[0] = output;
                dpOutput.Label = output.ToString();
                dpOutput.ToolTip = output.ToString();

                seriesOutput.Points.Add(dpOutput);
            }
            #endregion

            return seriesOutput;
        }

        public Series rejSeries(DataTable dt)
        {
            #region series style
            Series seriesRej = new Series();
            seriesRej.ChartType = SeriesChartType.Line;
            seriesRej.ChartArea = this.chartUsage.ChartAreas[0].Name;
            seriesRej.Name = "Rej";
            seriesRej.XAxisType = AxisType.Primary;
            seriesRej.XValueType = ChartValueType.String;
            seriesRej.YAxisType = AxisType.Secondary;
            seriesRej.YValueType = ChartValueType.Double;
            seriesRej.IsVisibleInLegend = false;
            //seriesRej.Label = "#VAL";                //设置显示X Y的值    
            seriesRej.LabelForeColor = Color.SteelBlue;
            seriesRej.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
            seriesRej.Color = Color.Lime;
            //seriesRej.LegendText = legend.Name;
            // seriesRej.IsValueShownAsLabel = true;
            //  seriesRej.CustomProperties = "DrawingStyle = Cylinder";
            seriesRej.Palette = ChartColorPalette.Bright;
            #endregion

            #region add point
            foreach (DataRow dr in dt.Rows)
            {
                #region point style
                DataPoint dpRej = new DataPoint();
                dpRej.Color = Color.OrangeRed;
                dpRej.BorderColor = System.Drawing.Color.OrangeRed;
                dpRej.LabelForeColor = Color.Black;
                dpRej.MarkerStyle = MarkerStyle.Circle;
                dpRej.MarkerSize = 10;
                dpRej.BorderWidth = 3;
                #endregion


                string dept = dr["Department"].ToString();
                double output = double.Parse(dr["TotalOutput"].ToString());
                double rej = double.Parse(dr["TotalRejQty"].ToString());


                dpRej.AxisLabel = dept;
                dpRej.YValues[0] = output == 0 ? 0 : Math.Round(rej / output*100, 2) ;
                dpRej.Label = output == 0? "0.00%" : Math.Round(rej / output * 100, 2).ToString() + "%";
                dpRej.ToolTip = "NG: " + rej.ToString();

                seriesRej.Points.Add(dpRej);
            }
            #endregion


            return seriesRej;
        }

        public Series utilizationSeries(DataTable dt)
        {
            #region series style
            Series seriesUtilization = new Series();
            seriesUtilization.ChartType = SeriesChartType.Column;
            seriesUtilization.ChartArea = this.chartUsage.ChartAreas[0].Name;
            seriesUtilization.Name = "Rej";
            seriesUtilization.XAxisType = AxisType.Primary;
            seriesUtilization.XValueType = ChartValueType.String;
            seriesUtilization.YAxisType = AxisType.Primary;
            seriesUtilization.YValueType = ChartValueType.Double;
            seriesUtilization.IsVisibleInLegend = false;
            //seriesUtilization.Label = "#VAL";                //设置显示X Y的值    
            seriesUtilization.LabelForeColor = Color.SteelBlue;
            seriesUtilization.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
            seriesUtilization.Color = Color.Lime;
            //seriesUtilization.LegendText = legend.Name;
            // seriesUtilization.IsValueShownAsLabel = true;
            //  seriesUtilization.CustomProperties = "DrawingStyle = Cylinder";
            seriesUtilization.Palette = ChartColorPalette.Bright;
            #endregion


            #region add point
            foreach (DataRow dr in dt.Rows)
            {
                // point style
                DataPoint dpOutput = new DataPoint();
                dpOutput.Color = StaticRes.MouldingStatusColor.Running;
                dpOutput.BackSecondaryColor = StaticRes.MouldingStatusColor.Running_Dark;
                dpOutput.BackGradientStyle = GradientStyle.LeftRight;
                dpOutput.BorderColor = Color.Black;
                dpOutput.LabelForeColor = Color.Black;
                


                string dept = dr["Department"].ToString();
                double utilization = double.Parse(dr["Utilization"].ToString());
                double runningHours = double.Parse(dr["TotalRunningHours"].ToString());

              
                dpOutput.AxisLabel = dept;
                dpOutput.YValues[0] = utilization;
                dpOutput.Label = utilization.ToString() + "%";
                dpOutput.ToolTip = string.Format("Operating: {0}H", runningHours.ToString("0.00"));

                seriesUtilization.Points.Add(dpOutput);
            }
            #endregion


            return seriesUtilization;
        }




        #region old version  no use

        //void ShowUtilization()
        //{
        //    DateTime dDateFrom = this.infDchFrom.CalendarLayout.SelectedDate.AddHours(8).Date.AddHours(8);
        //    DateTime dDateTo = this.infDchTo.CalendarLayout.SelectedDate.AddHours(8).Date.AddHours(8);
        //    string sShift = this.ddlShift.SelectedValue;
        //    string sDateNotIn = this.txt_DateNotIn.Text.Trim();


        //    #region moulding utilizaton
        //    DataTable dtMouldingUtilization = new DataTable();
        //    dtMouldingUtilization.Columns.Add("MachineID");
        //    dtMouldingUtilization.Columns.Add("Running");
        //    dtMouldingUtilization.Columns.Add("Adjustment");
        //    dtMouldingUtilization.Columns.Add("NoWIP");
        //    dtMouldingUtilization.Columns.Add("Mould_Testing");
        //    dtMouldingUtilization.Columns.Add("Material_Testing");
        //    dtMouldingUtilization.Columns.Add("Change_Model");
        //    dtMouldingUtilization.Columns.Add("No_Operator");
        //    dtMouldingUtilization.Columns.Add("No_Material");
        //    dtMouldingUtilization.Columns.Add("Break_Time");
        //    dtMouldingUtilization.Columns.Add("ShutDown");
        //    dtMouldingUtilization.Columns.Add("Damage_Mould");
        //    dtMouldingUtilization.Columns.Add("Machine_Break");


        //    Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus_bll = new Common.Class.BLL.MouldingMachineStatus_BLL();
        //    Dictionary<DateTime, StaticRes.Global.StatusType> Points;
        //    for (int i = 1; i < 9; i++)
        //    {
        //        DataRow dr = dtMouldingUtilization.NewRow();

        //        Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
        //        Points = MachineStatus_bll.getOEE(dDateFrom, dDateTo, i.ToString(), sShift, sDateNotIn, false);


        //        if (Points == null || Points.Count == 0)
        //        {
        //            dr["MachineID"] = "Machine" + i.ToString();
        //            dr["Running"] = 0;
        //        }
        //        else
        //        {
        //            #region  catogery the points
        //            double Running_Count = 0;
        //            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> pPoint in Points)
        //            {
        //                try
        //                {
        //                    switch (pPoint.Value)
        //                    {
        //                        case StaticRes.Global.StatusType.Running:
        //                            {
        //                                Running_Count++;
        //                                break;
        //                            }
        //                    }
        //                }
        //                catch (Exception ee)
        //                {
        //                }
        //            }
        //            #endregion

        //            dr["MachineID"] = "Machine" + i.ToString();
        //            dr["Running"] = Math.Round(Running_Count / 60, 2);

        //        }

        //        dtMouldingUtilization.Rows.Add(dr);
        //    }
        //    #endregion


        //    #region laser utilizaton
        //    Common.BLL.LMMSEventLog_BLL bll = new Common.BLL.LMMSEventLog_BLL();

        //    DataTable dtLaserUtilization = new DataTable();
        //    dtLaserUtilization.Columns.Add("MachineID");
        //    dtLaserUtilization.Columns.Add("PD");
        //    dtLaserUtilization.Columns.Add("Idle");
        //    dtLaserUtilization.Columns.Add("NoWIP");
        //    dtLaserUtilization.Columns.Add("Adjustment");
        //    dtLaserUtilization.Columns.Add("ShutDown");
        //    dtLaserUtilization.Columns.Add("Testing");
        //    dtLaserUtilization.Columns.Add("BreakDown");
        //    dtLaserUtilization.Columns.Add("Setup");
        //    dtLaserUtilization.Columns.Add("Maintainence");
        //    dtLaserUtilization.Columns.Add("Buyoff");


        //    Common.BLL.LMMSEventLog_BLL EventLog = new Common.BLL.LMMSEventLog_BLL();
        //    Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();


        //    for (int i = 1; i < 9; i++) // 8 machine data
        //    {
        //        DataRow dr = dtLaserUtilization.NewRow();
        //        dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
        //        dPoints = EventLog.getOEE(dDateFrom.Date, dDateTo, i.ToString(), "para_sPartNo_notuse", sShift, sDateNotIn, false);

        //        if (dPoints == null || dPoints.Count == 0)
        //        {
        //            dr["MachineID"] = "Machine" + i.ToString();
        //            dr["PD"] = 0;
        //        }
        //        else
        //        {
        //            #region  catogery the points
        //            double PD_Time_Count = 0;
        //            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> pPoint in dPoints)
        //            {
        //                try
        //                {
        //                    switch (pPoint.Value)
        //                    {

        //                        case StaticRes.Global.StatusType.PD:
        //                            {
        //                                PD_Time_Count++;   //will be reset 
        //                                break;
        //                            }
        //                    }
        //                }
        //                catch (Exception ee)
        //                {
        //                    DBHelp.Reports.LogFile.Log("MachineStatus_Debug_" + i.ToString(), "ShowEachChart -- OPE Detial exception pPoint.Key.Date:" + pPoint.Key.Date + ", dTimeFrom.Date:" + dDateFrom.Date + ", pPoint.Value:" + pPoint.Value + " " + ee.ToString());
        //                }
        //            }
        //            #endregion

        //            dr["MachineID"] = "Machine" + i.ToString();
        //            dr["PD"] = Math.Round(PD_Time_Count / 60, 2);
        //        }


        //        dtLaserUtilization.Rows.Add(dr);
        //    }
        //    #endregion


        //    #region painting utilizaton


        //    DataTable dtPaintingUtilization = new DataTable();
        //    dtPaintingUtilization.Columns.Add("MachineID");
        //    dtPaintingUtilization.Columns.Add("Running");



        //    for (int i = 1; i < 10; i++)
        //    {
        //        DataRow dr = dtPaintingUtilization.NewRow();

        //        dr["MachineID"] = "Machine" + i.ToString();
        //        dr["Running"] = 0;

        //        dtPaintingUtilization.Rows.Add(dr);
        //    }
        //    #endregion




        //    this.chartUsage.Visible = true;
        //    this.lblResult.Visible = false;

        //    ShowUtilizationChart(dtMouldingUtilization, dtLaserUtilization, dtPaintingUtilization);
        //}

        //void ShowUtilizationChart(DataTable dtMoulding, DataTable dtLaser,  DataTable dtPainting)
        //{
        //    try
        //    {
        //        this.chartUsage.Series.Clear();
        //        this.chartUsage.ChartAreas.Clear();
        //        this.chartUsage.ChartAreas.Add("Area1");


        //        #region Chart Css
        //        chartUsage.BackColor = Color.FromArgb(245, 245, 250);
        //        //ProdChart.BackSecondaryColor = Color.Transparent;
        //        chartUsage.BackGradientStyle = GradientStyle.None;


        //        //图表区背景
        //        chartUsage.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
        //        //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
        //        chartUsage.ChartAreas[0].BorderColor = Color.Transparent;
        //        //X轴标签间距
        //        chartUsage.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //        // ProdChart.ChartAreas[0].AxisX.Interval = 0;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        //        chartUsage.ChartAreas[0].AxisX.IsLabelAutoFit = true;

        //        chartUsage.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
        //        chartUsage.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

        //        //X坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

        //        //X轴网络线条
        //        chartUsage.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

        //        //Y坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
        //        chartUsage.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        //Y坐标轴标题
        //        chartUsage.ChartAreas[0].AxisY.Title = "Utilization Rate";
        //        chartUsage.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //        chartUsage.ChartAreas[0].AxisY.ToolTip = "Utilization Rate";
        //        //Y轴网格线条
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        //        chartUsage.ChartAreas[0].AxisY.Maximum = 100;


        //        //Y坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        //Y坐标轴标题
        //        chartUsage.ChartAreas[0].AxisY2.Title = "Rej%";
        //        chartUsage.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
        //        chartUsage.ChartAreas[0].AxisY2.ToolTip = "Rej%";
        //        //Y轴网格线条
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;

        //        chartUsage.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
        //        #endregion


        //        #region legend
        //        Legend legend = new Legend();
        //        legend.Docking = Docking.Top; // 设置摆放位置
        //        legend.Alignment = System.Drawing.StringAlignment.Near; // 对齐


        //        legend.CustomItems.Add(System.Drawing.Color.Green, "Moulding"); // 参数：(颜色, 说明)
        //        legend.CustomItems.Add(System.Drawing.Color.Red, "Painting");
        //        legend.CustomItems.Add(System.Drawing.Color.Blue, "Laser");

        //        legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;// 设置显示样式，色块、线条等
        //        legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
        //        legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;

        //        this.chartUsage.Legends.Add(legend);
        //        #endregion


        //        #region series css
        //        Series seriesMoulding = new Series();
        //        seriesMoulding.ChartType = SeriesChartType.Line;
        //        seriesMoulding.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesMoulding.Name = "Moulding";
        //        seriesMoulding.XAxisType = AxisType.Primary;
        //        seriesMoulding.XValueType = ChartValueType.String;
        //        seriesMoulding.YAxisType = AxisType.Primary;
        //        seriesMoulding.YValueType = ChartValueType.Double;
        //        //seriesPD.Label = "#VAL";                //设置显示X Y的值    
        //        seriesMoulding.LabelForeColor = Color.SteelBlue;
        //        seriesMoulding.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesMoulding.Color = Color.Lime;
        //        seriesMoulding.LegendText = legend.Name;
        //        seriesMoulding.IsVisibleInLegend = false;
        //        // seriesPD.IsValueShownAsLabel = true;
        //        //  seriesPD.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesMoulding.Palette = ChartColorPalette.Bright;




        //        Series seriesPainting = new Series();
        //        seriesPainting.ChartType = SeriesChartType.Line;
        //        seriesPainting.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesPainting.Name = "Painting";
        //        seriesPainting.XAxisType = AxisType.Primary;
        //        seriesPainting.XValueType = ChartValueType.String;
        //        seriesPainting.YAxisType = AxisType.Primary;
        //        seriesPainting.YValueType = ChartValueType.Double;
        //        seriesPainting.IsVisibleInLegend = false;
        //        //seriesPainting.Label = "#VAL";                //设置显示X Y的值    
        //        seriesPainting.LabelForeColor = Color.SteelBlue;
        //        seriesPainting.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesPainting.Color = Color.Lime;
        //        seriesPainting.LegendText = legend.Name;
        //        // seriesNoWIP.IsValueShownAsLabel = true;
        //        //  seriesNoWIP.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesPainting.Palette = ChartColorPalette.Bright;



        //        Series seriesLaser = new Series();
        //        seriesLaser.ChartType = SeriesChartType.Line;
        //        seriesLaser.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesLaser.Name = "Laser";
        //        seriesLaser.XAxisType = AxisType.Primary;
        //        seriesLaser.XValueType = ChartValueType.String;
        //        seriesLaser.YAxisType = AxisType.Primary;
        //        seriesLaser.YValueType = ChartValueType.Double;
        //        seriesLaser.IsVisibleInLegend = false;
        //        //seriesShutDown.Label = "#VAL";                //设置显示X Y的值    
        //        seriesLaser.LabelForeColor = Color.SteelBlue;
        //        seriesLaser.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesLaser.Color = Color.Lime;
        //        seriesLaser.LegendText = legend.Name;
        //        // seriesShutDown.IsValueShownAsLabel = true;
        //        //  seriesShutDown.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesLaser.Palette = ChartColorPalette.Bright;

        //        #endregion




        //        #region add point


        //        double totalSearchingHours = GetTotalTime();

        //        for (int i = 1; i < 10; i++)
        //        {

        //            #region point style
        //            DataPoint dpMoulding = new DataPoint();
        //            dpMoulding.Color = Color.Green;
        //            dpMoulding.BorderColor = System.Drawing.Color.Green;
        //            dpMoulding.LabelForeColor = Color.Black;
        //            dpMoulding.MarkerStyle = MarkerStyle.Circle;
        //            dpMoulding.MarkerSize = 10;
        //            dpMoulding.BorderWidth = 3;



        //            DataPoint dpPainting = new DataPoint();
        //            dpPainting.Color = Color.Red;
        //            dpPainting.BorderColor = Color.Red;
        //            dpPainting.LabelForeColor = Color.Black;
        //            dpPainting.MarkerStyle = MarkerStyle.Circle;
        //            dpPainting.MarkerSize = 10;
        //            dpPainting.BorderWidth = 3;


        //            DataPoint dpLaser = new DataPoint();
        //            dpLaser.Color = Color.Blue;
        //            dpLaser.BorderColor = Color.Blue;
        //            dpLaser.LabelForeColor = Color.Black;
        //            dpLaser.MarkerStyle = MarkerStyle.Circle;
        //            dpLaser.MarkerSize = 10;
        //            dpLaser.BorderWidth = 3;
        //            #endregion



        //            string machineID = "Machine" + i.ToString();
        //            double mouldingPD = 0;
        //            double paintingPD = 0;
        //            double laserPD = 0;


        //            DataRow[] drMouldingArr = dtMoulding.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drMouldingArr.Length> 0)
        //            {
        //                mouldingPD = double.Parse(drMouldingArr[0]["Running"].ToString());
        //            }

        //            DataRow[] drPaintingArr = dtPainting.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drPaintingArr.Length > 0)
        //            {
        //                paintingPD = double.Parse(drPaintingArr[0]["Running"].ToString());
        //            }

        //            DataRow[] drLaserArr = dtLaser.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drLaserArr.Length > 0)
        //            {
        //                laserPD = double.Parse(drLaserArr[0]["PD"].ToString());
        //            }




        //            dpMoulding.AxisLabel = "Machine" + i.ToString();
        //            dpMoulding.YValues[0] = Math.Round(mouldingPD / totalSearchingHours * 100, 2);
        //            dpMoulding.Label = Math.Round(mouldingPD / totalSearchingHours * 100, 2).ToString() + "%";
        //            dpMoulding.ToolTip = "Machine" + i.ToString() + " - " + mouldingPD.ToString() + "H";

        //            dpPainting.AxisLabel = "Machine" + i.ToString();
        //            dpPainting.YValues[0] = Math.Round(paintingPD / totalSearchingHours * 100, 2); ;
        //            dpPainting.Label = Math.Round(paintingPD / totalSearchingHours * 100, 2).ToString() + "%";
        //            dpPainting.ToolTip = "Machine" + i.ToString() + " - " + paintingPD.ToString() + "H";

        //            dpLaser.AxisLabel = "Machine" + i.ToString();
        //            dpLaser.YValues[0] = Math.Round(laserPD / totalSearchingHours * 100, 2);
        //            dpLaser.Label = Math.Round(laserPD / totalSearchingHours * 100, 2).ToString() + "%";
        //            dpLaser.ToolTip = "Machine" + i.ToString() + " - " + laserPD.ToString() + "H";


        //            if (i == 9)
        //            {
        //                seriesPainting.Points.Add(dpPainting);
        //            }
        //            else
        //            {
        //                seriesMoulding.Points.Add(dpMoulding);
        //                seriesLaser.Points.Add(dpLaser);
        //                seriesPainting.Points.Add(dpPainting);
        //            }

        //        }
        //        #endregion


        //        this.chartUsage.Series.Add(seriesMoulding);
        //        this.chartUsage.Series.Add(seriesPainting);
        //        this.chartUsage.Series.Add(seriesLaser);


        //        this.chartUsage.Titles.Clear();
        //        this.chartUsage.Titles.Add(" All Machine Utilization Chart ");
        //        this.chartUsage.Titles[0].ForeColor = Color.Black;
        //        this.chartUsage.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
        //        this.chartUsage.Titles[0].Alignment = ContentAlignment.TopCenter;

        //    }
        //    catch (Exception ee)
        //    {
        //        DBHelp.Reports.LogFile.Log("AllMachineUtilizationRate", "ShowUtilizationChart Exception : " + ee.ToString());
        //    }
        //}


        //void ShowOutput()
        //{
        //    DateTime dDateFrom = this.infDchFrom.CalendarLayout.SelectedDate.AddHours(8).Date.AddHours(8);
        //    DateTime dDateTo = this.infDchTo.CalendarLayout.SelectedDate.AddHours(8).Date.AddHours(8);
        //    string sShift = this.ddlShift.SelectedValue;
        //    string sDateNotIn = this.txt_DateNotIn.Text.Trim();



        //    Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
        //    DataTable dtLaser = WatchDogBll.getMachineSummary(dDateFrom.ToString(), dDateTo.ToString(), sShift, "", false,""); 


        //    Common.Class.BLL.MouldingViHistory_BLL ViHistory_bll = new Common.Class.BLL.MouldingViHistory_BLL();
        //    DataTable dtMoulding = ViHistory_bll.SummaryByMachine(dDateFrom, dDateTo, sShift, "", false);



        //    DataTable dtPainting = new DataTable();
        //    dtPainting.Columns.Add("MachineID");
        //    dtPainting.Columns.Add("Output");
        //    dtPainting.Columns.Add("NG");

        //    for (int i = 1; i < 10; i++)
        //    {
        //        DataRow dr = dtPainting.NewRow();
        //        dr[0] = "Machine" + i.ToString();
        //        //dr[1] = 1000;
        //        dr[1] = 0;
        //        dr[2] = 0;
        //        dtPainting.Rows.Add(dr);
        //    }




        //    ShowOutputChart(dtMoulding,dtLaser,dtPainting);
        //}

        //void ShowOutputChart(DataTable dtMoulding, DataTable dtLaser, DataTable dtPainting)
        //{
        //    try
        //    {
        //        this.chartUsage.Series.Clear();
        //        this.chartUsage.ChartAreas.Clear();
        //        this.chartUsage.ChartAreas.Add("Area1");


        //        #region Chart Css
        //        chartUsage.BackColor = Color.FromArgb(245, 245, 250);
        //        //ProdChart.BackSecondaryColor = Color.Transparent;
        //        chartUsage.BackGradientStyle = GradientStyle.None;


        //        //图表区背景
        //        chartUsage.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
        //        //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
        //        chartUsage.ChartAreas[0].BorderColor = Color.Transparent;
        //        //X轴标签间距
        //        chartUsage.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //        // ProdChart.ChartAreas[0].AxisX.Interval = 0;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        //        chartUsage.ChartAreas[0].AxisX.IsLabelAutoFit = true;

        //        chartUsage.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
        //        chartUsage.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

        //        //X坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        //        chartUsage.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

        //        //X轴网络线条
        //        chartUsage.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

        //        //Y坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
        //        chartUsage.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        //Y坐标轴标题
        //        chartUsage.ChartAreas[0].AxisY.Title = "Utilization Rate";
        //        chartUsage.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
        //        chartUsage.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //        chartUsage.ChartAreas[0].AxisY.ToolTip = "Utilization Rate";
        //        //Y轴网格线条
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //        chartUsage.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        //        chartUsage.ChartAreas[0].AxisY2.Maximum = 20;


        //        //Y坐标轴颜色
        //        chartUsage.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        //Y坐标轴标题
        //        chartUsage.ChartAreas[0].AxisY2.Title = "Rej%";
        //        chartUsage.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        chartUsage.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
        //        chartUsage.ChartAreas[0].AxisY2.ToolTip = "Rej%";
        //        //Y轴网格线条
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //        chartUsage.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;

        //        chartUsage.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
        //        #endregion


        //        #region legend
        //        Legend legend = new Legend();
        //        legend.Docking = Docking.Top; // 设置摆放位置
        //        legend.Alignment = System.Drawing.StringAlignment.Near; // 对齐


        //        legend.CustomItems.Add(System.Drawing.Color.Green, "Moulding"); // 参数：(颜色, 说明)
        //        legend.CustomItems.Add(System.Drawing.Color.Red, "Painting");
        //        legend.CustomItems.Add(System.Drawing.Color.Blue, "Laser");

        //        legend.CustomItems.Add(System.Drawing.Color.Green, "Moulding"); // 参数：(颜色, 说明)
        //        legend.CustomItems.Add(System.Drawing.Color.Red, "Painting");
        //        legend.CustomItems.Add(System.Drawing.Color.Blue, "Laser");

        //        legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;// 设置显示样式，色块、线条等
        //        legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
        //        legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;

        //        legend.CustomItems[3].ImageStyle = LegendImageStyle.Line;// 设置显示样式，色块、线条等
        //        legend.CustomItems[4].ImageStyle = LegendImageStyle.Line;
        //        legend.CustomItems[5].ImageStyle = LegendImageStyle.Line;

        //        this.chartUsage.Legends.Add(legend);
        //        #endregion


        //        #region series css
        //        Series seriesMoulding = new Series();
        //        seriesMoulding.ChartType = SeriesChartType.Column;
        //        seriesMoulding.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesMoulding.Name = "Moulding";
        //        seriesMoulding.XAxisType = AxisType.Primary;
        //        seriesMoulding.XValueType = ChartValueType.String;
        //        seriesMoulding.YAxisType = AxisType.Primary;
        //        seriesMoulding.YValueType = ChartValueType.Double;
        //        //seriesPD.Label = "#VAL";                //设置显示X Y的值    
        //        seriesMoulding.LabelForeColor = Color.SteelBlue;
        //        seriesMoulding.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesMoulding.Color = Color.Lime;
        //        seriesMoulding.LegendText = legend.Name;
        //        seriesMoulding.IsVisibleInLegend = false;
        //        // seriesPD.IsValueShownAsLabel = true;
        //        //  seriesPD.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesMoulding.Palette = ChartColorPalette.Bright;




        //        Series seriesPainting = new Series();
        //        seriesPainting.ChartType = SeriesChartType.Column;
        //        seriesPainting.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesPainting.Name = "Painting";
        //        seriesPainting.XAxisType = AxisType.Primary;
        //        seriesPainting.XValueType = ChartValueType.String;
        //        seriesPainting.YAxisType = AxisType.Primary;
        //        seriesPainting.YValueType = ChartValueType.Double;
        //        seriesPainting.IsVisibleInLegend = false;
        //        //seriesPainting.Label = "#VAL";                //设置显示X Y的值    
        //        seriesPainting.LabelForeColor = Color.SteelBlue;
        //        seriesPainting.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesPainting.Color = Color.Lime;
        //        seriesPainting.LegendText = legend.Name;
        //        // seriesNoWIP.IsValueShownAsLabel = true;
        //        //  seriesNoWIP.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesPainting.Palette = ChartColorPalette.Bright;



        //        Series seriesLaser = new Series();
        //        seriesLaser.ChartType = SeriesChartType.Column;
        //        seriesLaser.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesLaser.Name = "Laser";
        //        seriesLaser.XAxisType = AxisType.Primary;
        //        seriesLaser.XValueType = ChartValueType.String;
        //        seriesLaser.YAxisType = AxisType.Primary;
        //        seriesLaser.YValueType = ChartValueType.Double;
        //        seriesLaser.IsVisibleInLegend = false;
        //        //seriesShutDown.Label = "#VAL";                //设置显示X Y的值    
        //        seriesLaser.LabelForeColor = Color.SteelBlue;
        //        seriesLaser.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesLaser.Color = Color.Lime;
        //        seriesLaser.LegendText = legend.Name;
        //        // seriesShutDown.IsValueShownAsLabel = true;
        //        //  seriesShutDown.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesLaser.Palette = ChartColorPalette.Bright;








        //        Series seriesMouldingRej = new Series();
        //        seriesMouldingRej.ChartType = SeriesChartType.Line;
        //        seriesMouldingRej.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesMouldingRej.Name = "MouldingRej";
        //        seriesMouldingRej.XAxisType = AxisType.Primary;
        //        seriesMouldingRej.XValueType = ChartValueType.String;
        //        seriesMouldingRej.YAxisType = AxisType.Secondary;
        //        seriesMouldingRej.YValueType = ChartValueType.Double;
        //        //seriesPD.Label = "#VAL";                //设置显示X Y的值    
        //        seriesMouldingRej.LabelForeColor = Color.SteelBlue;
        //        seriesMouldingRej.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesMouldingRej.Color = Color.Lime;
        //        seriesMouldingRej.LegendText = legend.Name;
        //        seriesMouldingRej.IsVisibleInLegend = false;
        //        // seriesPD.IsValueShownAsLabel = true;
        //        //  seriesPD.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesMouldingRej.Palette = ChartColorPalette.Bright;




        //        Series seriesPaintingRej = new Series();
        //        seriesPaintingRej.ChartType = SeriesChartType.Line;
        //        seriesPaintingRej.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesPaintingRej.Name = "PaintingRej";
        //        seriesPaintingRej.XAxisType = AxisType.Primary;
        //        seriesPaintingRej.XValueType = ChartValueType.String;
        //        seriesPaintingRej.YAxisType = AxisType.Secondary;
        //        seriesPaintingRej.YValueType = ChartValueType.Double;
        //        seriesPaintingRej.IsVisibleInLegend = false;
        //        //seriesPainting.Label = "#VAL";                //设置显示X Y的值    
        //        seriesPaintingRej.LabelForeColor = Color.SteelBlue;
        //        seriesPaintingRej.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesPaintingRej.Color = Color.Lime;
        //        seriesPaintingRej.LegendText = legend.Name;
        //        // seriesNoWIP.IsValueShownAsLabel = true;
        //        //  seriesNoWIP.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesPaintingRej.Palette = ChartColorPalette.Bright;



        //        Series seriesLaserRej = new Series();
        //        seriesLaserRej.ChartType = SeriesChartType.Line;
        //        seriesLaserRej.ChartArea = this.chartUsage.ChartAreas[0].Name;
        //        seriesLaserRej.Name = "LaserRej";
        //        seriesLaserRej.XAxisType = AxisType.Primary;
        //        seriesLaserRej.XValueType = ChartValueType.String;
        //        seriesLaserRej.YAxisType = AxisType.Secondary;
        //        seriesLaserRej.YValueType = ChartValueType.Double;
        //        seriesLaserRej.IsVisibleInLegend = false;
        //        //seriesShutDown.Label = "#VAL";                //设置显示X Y的值    
        //        seriesLaserRej.LabelForeColor = Color.SteelBlue;
        //        seriesLaserRej.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
        //        seriesLaserRej.Color = Color.Lime;
        //        seriesLaserRej.LegendText = legend.Name;
        //        // seriesShutDown.IsValueShownAsLabel = true;
        //        //  seriesShutDown.CustomProperties = "DrawingStyle = Cylinder";
        //        seriesLaserRej.Palette = ChartColorPalette.Bright;


        //        #endregion


        //        #region add point
        //        for (int i = 1; i < 10; i++)
        //        {

        //            #region point style
        //            DataPoint dpMoulding = new DataPoint();
        //            dpMoulding.Color = Color.LawnGreen;
        //            dpMoulding.BackSecondaryColor = Color.DarkGreen;
        //            dpMoulding.BackGradientStyle = GradientStyle.LeftRight;
        //            dpMoulding.BorderColor = Color.Black;
        //            dpMoulding.LabelForeColor = Color.Black;
        //            //dpMoulding.MarkerStyle = MarkerStyle.Circle;
        //            //dpMoulding.MarkerSize = 10;
        //            //dpMoulding.BorderWidth = 3;

        //            DataPoint dpPainting = new DataPoint();
        //            dpPainting.Color = Color.OrangeRed;
        //            dpPainting.BackSecondaryColor = Color.DarkRed;
        //            dpPainting.BackGradientStyle = GradientStyle.LeftRight;
        //            dpPainting.BorderColor = Color.Black;
        //            dpPainting.LabelForeColor = Color.Black;
        //            //dpPainting.MarkerStyle = MarkerStyle.Circle;
        //            //dpPainting.MarkerSize = 10;
        //            //dpPainting.BorderWidth = 3;

        //            DataPoint dpLaser = new DataPoint();
        //            dpLaser.Color = Color.Blue;
        //            dpLaser.BackSecondaryColor = Color.DarkBlue;
        //            dpLaser.BackGradientStyle = GradientStyle.LeftRight;
        //            dpLaser.BorderColor = Color.Black;
        //            dpLaser.LabelForeColor = Color.Black;
        //            //dpLaser.MarkerStyle = MarkerStyle.Circle;
        //            //dpLaser.MarkerSize = 10;
        //            //dpLaser.BorderWidth = 3;







        //            DataPoint dpMouldingRej = new DataPoint();
        //            dpMouldingRej.Color = Color.Green;
        //            //dpMouldingRej.BackSecondaryColor = Color.DarkGreen;
        //            //dpMouldingRej.BackGradientStyle = GradientStyle.LeftRight;
        //            dpMouldingRej.BorderColor = Color.Black;
        //            dpMouldingRej.LabelForeColor = Color.Black;
        //            dpMouldingRej.MarkerStyle = MarkerStyle.Circle;
        //            dpMouldingRej.MarkerSize = 10;
        //            dpMouldingRej.BorderWidth = 3;

        //            DataPoint dpPaintingRej = new DataPoint();
        //            dpPaintingRej.Color = Color.Red;
        //            //dpPaintingRej.BackSecondaryColor = Color.DarkRed;
        //            //dpPaintingRej.BackGradientStyle = GradientStyle.LeftRight;
        //            dpPaintingRej.BorderColor = Color.Black;
        //            dpPaintingRej.LabelForeColor = Color.Black;
        //            dpPaintingRej.MarkerStyle = MarkerStyle.Circle;
        //            dpPaintingRej.MarkerSize = 10;
        //            dpPaintingRej.BorderWidth = 3;

        //            DataPoint dpLaserRej = new DataPoint();
        //            dpLaserRej.Color = Color.Blue;
        //            //dpLaserRej.BackSecondaryColor = Color.DarkBlue;
        //            //dpLaserRej.BackGradientStyle = GradientStyle.LeftRight;
        //            dpLaserRej.BorderColor = Color.Black;
        //            dpLaserRej.LabelForeColor = Color.Black;
        //            dpLaserRej.MarkerStyle = MarkerStyle.Circle;
        //            dpLaserRej.MarkerSize = 10;
        //            dpLaserRej.BorderWidth = 3;

        //            #endregion


        //            #region get value
        //            string machineID = "Machine" + i.ToString();
        //            double dMouldingOutput = 0;
        //            double dMouldingRej = 0;
        //            double dPaintingOutput = 0;
        //            double dPaintingRej = 0;
        //            double dLaserOurput = 0;
        //            double dLaserRej = 0;


        //            DataRow[] drMouldingArr = dtMoulding.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drMouldingArr.Length > 0)
        //            {
        //                dMouldingOutput = double.Parse(drMouldingArr[0]["Output"].ToString());
        //                dMouldingRej = double.Parse(drMouldingArr[0]["NG"].ToString());
        //            }

        //            DataRow[] drPaintingArr = dtPainting.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drPaintingArr.Length > 0)
        //            {
        //                dPaintingOutput = double.Parse(drPaintingArr[0]["Output"].ToString());
        //                dPaintingRej = double.Parse(drPaintingArr[0]["NG"].ToString());
        //            }

        //            DataRow[] drLaserArr = dtLaser.Select(string.Format("machineID = '{0}'", machineID));
        //            if (drLaserArr.Length > 0)
        //            {
        //                dLaserOurput = double.Parse(drLaserArr[0]["Output"].ToString());
        //                dLaserRej = double.Parse(drLaserArr[0]["NG"].ToString());
        //            }
        //            #endregion


        //            #region output column
        //            dpMoulding.AxisLabel = "Machine" + i.ToString();
        //            dpMoulding.YValues[0] = dMouldingOutput;
        //            dpMoulding.Label = dMouldingOutput.ToString();
        //            dpMoulding.ToolTip = "Moulding Mc" + i.ToString() + " - " + dMouldingOutput.ToString();

        //            dpPainting.AxisLabel = "Machine" + i.ToString();
        //            dpPainting.YValues[0] = dPaintingOutput;
        //            dpPainting.Label = dPaintingOutput.ToString();
        //            dpPainting.ToolTip = "Painting Mc" + i.ToString() + " - " + dPaintingOutput.ToString();

        //            dpLaser.AxisLabel = "Machine" + i.ToString();
        //            dpLaser.YValues[0] = dLaserOurput;
        //            dpLaser.Label = dLaserOurput.ToString();
        //            dpLaser.ToolTip = "Laser Mc" + i.ToString() + " - " + dLaserOurput.ToString();
        //            #endregion


        //            #region RejRate Line
        //            dpMouldingRej.AxisLabel = "Machine" + i.ToString();
        //            dpMouldingRej.YValues[0] = Math.Round(dMouldingRej / dMouldingOutput * 100, 2);
        //            dpMouldingRej.Label = Math.Round(dMouldingRej / dMouldingOutput * 100, 2).ToString() + "%";
        //            dpMouldingRej.ToolTip = "Moulding Rej% Mc" + i.ToString() + " - " + Math.Round(dMouldingRej / dMouldingOutput * 100, 2).ToString() + "%";

        //            dpPaintingRej.AxisLabel = "Machine" + i.ToString();
        //            dpPaintingRej.YValues[0] = Math.Round(dPaintingRej / dPaintingOutput * 100, 2);
        //            dpPaintingRej.Label = Math.Round(dPaintingRej / dPaintingOutput * 100, 2) + "%";
        //            dpPaintingRej.ToolTip = "Painting Rej% Mc" + i.ToString() + " - " + Math.Round(dPaintingRej / dPaintingOutput * 100, 2) + "%";

        //            dpLaserRej.AxisLabel = "Machine" + i.ToString();
        //            dpLaserRej.YValues[0] = Math.Round(dLaserRej / dLaserOurput * 100, 2);
        //            dpLaserRej.Label = Math.Round(dLaserRej / dLaserOurput * 100, 2).ToString() + "%";
        //            dpLaserRej.ToolTip = "Laser Rej% Mc" + i.ToString() + " - " + Math.Round(dLaserRej / dLaserOurput * 100, 2).ToString() + "%";
        //            #endregion

        //            if (i == 9)
        //            {
        //                seriesPainting.Points.Add(dpPainting);
        //                seriesPaintingRej.Points.Add(dpPaintingRej);
        //            }
        //            else
        //            {
        //                seriesMoulding.Points.Add(dpMoulding);
        //                seriesLaser.Points.Add(dpLaser);
        //                seriesPainting.Points.Add(dpPainting);

        //                seriesMouldingRej.Points.Add(dpMouldingRej);
        //                seriesLaserRej.Points.Add(dpLaserRej);
        //                seriesPaintingRej.Points.Add(dpPaintingRej);

        //            }
        //        }
        //        #endregion


        //        this.chartUsage.Series.Add(seriesMoulding);
        //        this.chartUsage.Series.Add(seriesPainting);
        //        this.chartUsage.Series.Add(seriesLaser);
        //        this.chartUsage.Series.Add(seriesMouldingRej);
        //        this.chartUsage.Series.Add(seriesPaintingRej);
        //        this.chartUsage.Series.Add(seriesLaserRej);


        //        this.chartUsage.Titles.Clear();
        //        this.chartUsage.Titles.Add(" All Machine Output Chart ");
        //        this.chartUsage.Titles[0].ForeColor = Color.Black;
        //        this.chartUsage.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
        //        this.chartUsage.Titles[0].Alignment = ContentAlignment.TopCenter;
        //    }
        //    catch (Exception ee)
        //    {
        //        DBHelp.Reports.LogFile.Log("AllMachineUtilizationRate", "ShowOutputChart Exception : " + ee.ToString());
        //    }
        //}

        #endregion


        private double GetTotalTime(DateTime dDatefrom, DateTime dDateTo)
        {
            //当天时间, 班次
            DateTime currentDay = DateTime.Now.AddHours(-8).Date;
            string currentShift = DateTime.Now >= currentDay.AddHours(8) && DateTime.Now < currentDay.AddHours(20) ? StaticRes.Global.Shift.Day : StaticRes.Global.Shift.Night;


            double dayHours = this.ddlShift.SelectedItem.Value == "" ? 24 : 12;
            double totalHours = 0;

            if (dDateTo < currentDay)
            {
                totalHours = (dDateTo - dDatefrom).TotalDays * dayHours;
            }
            else
            {
                totalHours = ((dDateTo - dDatefrom).TotalDays - 1) * dayHours;

                //白班8点到现在的时间
                if (currentShift == StaticRes.Global.Shift.Day)
                    totalHours += ((DateTime.Now - currentDay.AddHours(8)).TotalSeconds / 3600);

                //白班12小时 + 晚班20:00 到现在的时间.
                else
                    totalHours += ((DateTime.Now - currentDay.AddHours(20)).TotalSeconds / 3600 + 12);
            }


            double dateNotInHours = 0;
            if (this.txt_DateNotIn.Text.Trim() != "")
            {
                string[] notInDays = this.txt_DateNotIn.Text.Split(',');
                DateTime dTemp = dDatefrom.Date;

                while(dTemp < dDateTo)
                {
                    if (notInDays.Contains(dTemp.Day.ToString()))
                    {
                        //白班8点到现在的时间
                        if (dTemp == currentDay && currentShift == StaticRes.Global.Shift.Day)
                            dateNotInHours += ((DateTime.Now - currentDay.AddHours(8)).TotalSeconds / 3600);

                        //白班12小时 + 晚班20:00 到现在的时间.
                        else if (dTemp == currentDay && currentShift == StaticRes.Global.Shift.Night)
                            dateNotInHours += ((DateTime.Now - currentDay.AddHours(20)).TotalSeconds / 3600 + 12);

                        else
                            dateNotInHours += dayHours;
                    }

                    dTemp = dTemp.AddDays(1);
                }
            }


            double exceptWeekends = 0;
            if (ckbExceptWeekend.Checked)
            {
                DateTime dTemp = dDatefrom.Date;
                while (dTemp < dDateTo)
                {
                    if (dTemp.DayOfWeek == DayOfWeek.Sunday || dTemp.DayOfWeek == DayOfWeek.Saturday)
                        exceptWeekends += dayHours;
                    
                    dTemp = dTemp.AddDays(1);
                }
            }
            
            return totalHours - dateNotInHours - exceptWeekends;
        }


        public DataTable InitDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("TotalOutput", typeof(int));
            dt.Columns.Add("TotalRejQty", typeof(int));

            dt.Columns.Add("TotalRunningHours", typeof(double));

            dt.Columns.Add("Utilization", typeof(string));

            return dt;
        }


        

    }
}