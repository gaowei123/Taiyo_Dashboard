using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCCheckerOutputChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                   
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.Date;
                    this.infDchFrom.Value = DateTime.Now.Date;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now.Date;
                    this.infDchTo.Value = DateTime.Now.Date;
                    

                    btnGenerate_Click(new object(), new EventArgs());

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCCheckerOutputChart", "Page_Load Exception : " + ex.ToString());
                }
            }

        }




        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //paras 
                DateTime dDateFrom = this.infDchFrom.CalendarLayout.SelectedDate;
                DateTime dDateTo = this.infDchTo.CalendarLayout.SelectedDate.AddDays(1);
                string sType = this.ddlType.SelectedValue;


                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();

                DataTable dt = bll.GetCheckerOutput(sType, dDateFrom, dDateTo);


                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ProdChart.Visible = false;
                    this.lblResult.Text = "No Record !";
                    this.lblResult.Visible = true;
                    this.lblResult.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.ProdChart.Visible = true;
                    this.lblResult.Visible = false;

                    ShowChart(dt);
                }

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCCheckerOutputChart", "btnGenerate_Click Exception : " + ex.ToString());
            }
        }
        


        void ShowChart(DataTable dt)
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
                ProdChart.ChartAreas[0].AxisY.Title = "Total Checked Quantity";
                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Total  Checked  Quantity";
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


                //Y坐标轴颜色
                ProdChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                //Y坐标轴标题
                ProdChart.ChartAreas[0].AxisY2.Title = "Total  Reject  Rate";
                ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                ProdChart.ChartAreas[0].AxisY2.ToolTip = "Total  Reject  Rate";
                ProdChart.ChartAreas[0].AxisY2.Maximum = 100;
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;



                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion
                

                #region legend
                Legend legend = new Legend();
                legend.CustomItems.Add(System.Drawing.Color.SkyBlue, "OnLine"); // 参数：(颜色, 说明)
                legend.CustomItems.Add(System.Drawing.Color.Green, "WIP"); 
                legend.CustomItems.Add(System.Drawing.Color.OrangeRed, "Rej%");
                legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;// 设置显示样式，色块、线条等
                legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
                legend.CustomItems[2].ImageStyle = LegendImageStyle.Line;
                legend.Docking = Docking.Top; // 设置摆放位置
                legend.Alignment = System.Drawing.StringAlignment.Near; // 对齐

                this.ProdChart.Legends.Add(legend);
                #endregion


                #region series css
                Series dataSeries_Output = new Series();
                dataSeries_Output.ChartType = SeriesChartType.Column;
                dataSeries_Output.ChartArea = this.ProdChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                dataSeries_Output.Name = "Job Output";
                dataSeries_Output.XAxisType = AxisType.Primary;
                dataSeries_Output.XValueType = ChartValueType.String;
                dataSeries_Output.YAxisType = AxisType.Primary;
                dataSeries_Output.YValueType = ChartValueType.Double;
                dataSeries_Output.IsVisibleInLegend = false;
                //dataSeries_Output.Label = "#VAL";                //设置显示X Y的值    
                dataSeries_Output.LabelForeColor = Color.SteelBlue;
                dataSeries_Output.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
                dataSeries_Output.Color = Color.Lime;
                dataSeries_Output.LegendText = legend.Name;
                // dataSeries_Output.IsValueShownAsLabel = true;
                //  dataSeries_Output.CustomProperties = "DrawingStyle = Cylinder";
                dataSeries_Output.Palette = ChartColorPalette.Bright;



                Series dataSeries_RejRate = new Series();
                dataSeries_RejRate.ChartType = SeriesChartType.Line;
                dataSeries_RejRate.ChartArea = this.ProdChart.ChartAreas[0].Name;
                dataSeries_RejRate.Name = "total Reject";
                // dataSeries_RejRate.SelectionEnabled = true;
                // dataSeries_RejRate.SelectionMode = SelectionModes.Single; 
                dataSeries_RejRate.XAxisType = AxisType.Primary;
                dataSeries_RejRate.XValueType = ChartValueType.String;
                dataSeries_RejRate.YAxisType = AxisType.Secondary;
                dataSeries_RejRate.YValueType = ChartValueType.Double;
                dataSeries_RejRate.IsVisibleInLegend = false;
                dataSeries_RejRate.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
                dataSeries_RejRate.MarkerColor = Color.White;
                dataSeries_RejRate.MarkerSize = 10;
                dataSeries_RejRate.MarkerStyle = MarkerStyle.Circle;
                dataSeries_RejRate.MarkerBorderWidth = 1;
                dataSeries_RejRate.MarkerBorderColor = Color.DarkRed;
                dataSeries_RejRate.BorderWidth = 2;

                #endregion 


                foreach (DataRow dr in dt.Rows)
                {
                    string axisXName = dr["userID"].ToString();
                    double totalQty = double.Parse(dr["totalQty"].ToString());
                    double totalPass = double.Parse(dr["acceptQty"].ToString());
                    double totalRej = double.Parse(dr["rejectQty"].ToString());
                    double rejRate = totalQty == 0 ? 0 : Math.Round(totalRej / totalQty * 100,2);

                    string userType = dr["userType"].ToString();


                    DataPoint dpColumn = new DataPoint();
                    dpColumn.AxisLabel = axisXName;
                    dpColumn.YValues[0] = totalQty;
                    dpColumn.Label = totalQty.ToString();



                    //dpColumn.Label = "Total Pass: " + totalPass.ToString();
                    if (userType == "ONLINE")
                    {
                        dpColumn.Color = System.Drawing.Color.SkyBlue;
                        dpColumn.BackSecondaryColor = Color.SteelBlue;
                        
                    }
                    else
                    {
                        dpColumn.Color = System.Drawing.Color.LawnGreen;
                        dpColumn.BackSecondaryColor = Color.DarkGreen;
                    }
                    dpColumn.BackGradientStyle = GradientStyle.LeftRight;
                    dpColumn.BorderColor = Color.Black;
                    dpColumn.LabelForeColor = Color.Black;
                    dataSeries_Output.Points.Add(dpColumn);


                    DataPoint dpRejRate = new DataPoint();
                    dpRejRate.AxisLabel = axisXName;
                    dpRejRate.YValues[0] = rejRate;
                    dpRejRate.Label = rejRate.ToString() + "%";
                    dpRejRate.ToolTip = "totalRej: " + totalRej.ToString();
                    dpRejRate.Color = System.Drawing.Color.OrangeRed;
                    dpRejRate.MarkerStyle = MarkerStyle.Circle;
                    dpRejRate.MarkerBorderWidth = 2;
                    dpRejRate.MarkerSize = 12;
                    dataSeries_RejRate.Points.Add(dpRejRate);
                }




                this.ProdChart.Series.Add(dataSeries_Output);
                this.ProdChart.Series.Add(dataSeries_RejRate);

                ProdChart.Titles.Clear();
                ProdChart.Titles.Add(" PQC Checker Output Chart");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCCheckerOutputChart", "ShowChart Exception : " + ee.ToString());
            }
        }

        
    }
}