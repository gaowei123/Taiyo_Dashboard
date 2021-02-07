using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using System.Text;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCProductionChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.AddDays(-30);
                    this.infDchFrom.Value = DateTime.Now.AddDays(-30);
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;
                    
                    setYearDDL();
                    

                    btnGenerate_Click(new object(), new EventArgs());
                    
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCProductionChart", "Page_Load Exception : " + ex.ToString());
                }
            }
        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string sReportType = this.ddlReportType.SelectedValue;
                string sYear = this.ddlYear.SelectedValue;
                DateTime dDateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime dDateTo = this.infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1).AddHours(8);
                string sPartNo = this.txtPartNo.Text.Trim();
                string sModel = this.txtModel.Text;
                string sCustomer = this.txtCustomer.Text.Trim();
                string sPIC = this.txtPIC.Text;
              


                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                DataTable dt = bll.GetProductionChart(sReportType, dDateFrom, dDateTo, int.Parse(sYear), sPartNo, sModel, sCustomer, sPIC);


                if (dt==null || dt.Rows.Count ==0)
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
                DBHelp.Reports.LogFile.Log("PQCProductionChart", "btnGenerate_Click Exception : " + ex.ToString());
            }
        }



        void ShowChart(DataTable dt)
        {
            try
            {
                string sReportType = this.ddlReportType.SelectedValue;


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

                if (sReportType == "PartNumber" || sReportType == "Daily" || sReportType == "Model" || sReportType == "Customer")
                {
                    ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                }
                else
                {
                    ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                }

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
                ProdChart.ChartAreas[0].AxisY.Title = "Total Pass Quantity";
                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Total Pass Quantity";
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
                ProdChart.ChartAreas[0].AxisY2.Title = "Total Rej Qty";
                ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                ProdChart.ChartAreas[0].AxisY2.ToolTip = "Total Rej Qty";
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
     


                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion



                #region legend
                Legend legend = new Legend();
                legend.CustomItems.Add(System.Drawing.Color.SkyBlue, "Total Pass Qty"); // 参数：(颜色, 说明)
                legend.CustomItems.Add(System.Drawing.Color.OrangeRed, "Rej%");
                legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;// 设置显示样式，色块、线条等
                legend.CustomItems[1].ImageStyle = LegendImageStyle.Line;
                legend.Docking = Docking.Top; // 设置摆放位置
                legend.Alignment = System.Drawing.StringAlignment.Far; // 对齐

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
                    string axisXName = "";
                    double totalPass = 0;
                    double totalRej = 0;
                    double rejRate = 0;

                    axisXName = dr[0].ToString();
                    totalPass = double.Parse(dr["totalPass"].ToString());
                    totalRej = double.Parse(dr["totalFail"].ToString());
                    rejRate = double.Parse(dr["rejReate"].ToString());


                    #region dataPoint style
                    DataPoint dpColumn = new DataPoint();
                    dpColumn.Color = System.Drawing.Color.SkyBlue;
                    dpColumn.BackGradientStyle = GradientStyle.LeftRight;
                    dpColumn.BackSecondaryColor = Color.SteelBlue;
                    dpColumn.BorderColor = Color.Black;
                    dpColumn.LabelForeColor = Color.Black;


                    DataPoint dpRejRate = new DataPoint();
                    dpRejRate.Color = System.Drawing.Color.OrangeRed;
                    dpRejRate.MarkerStyle = MarkerStyle.Circle;
                    dpRejRate.MarkerBorderWidth = 2;
                    dpRejRate.MarkerSize = 15;
                    #endregion



                    if (sReportType == "Rejection")
                    {
                        dpColumn.AxisLabel = axisXName;
                        dpColumn.YValues[0] = totalRej;
                        dpColumn.Label = rejRate.ToString("0.00") + "%";
                        dataSeries_Output.Points.Add(dpColumn);
                    }
                    else
                    {
                        dpColumn.AxisLabel = axisXName;
                        dpColumn.YValues[0] = totalPass;
                        dpColumn.Label = totalPass.ToString();

                        dpRejRate.AxisLabel = axisXName;
                        dpRejRate.YValues[0] = totalRej;
                        dpRejRate.Label = rejRate + "%";


                        dataSeries_Output.Points.Add(dpColumn);
                        dataSeries_RejRate.Points.Add(dpRejRate);
                    }
                }
               


                this.ProdChart.Series.Add(dataSeries_Output);
                this.ProdChart.Series.Add(dataSeries_RejRate);

                ProdChart.Titles.Clear();
                ProdChart.Titles.Add(string.Format(" PQC {0} Chart ",this.ddlReportType.SelectedValue));
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCProductionChart", "ShowChart Exception : " + ee.ToString());
            }
        }


      
       

        void setYearDDL()
        {
            this.ddlYear.Items.Clear();


            int yearStart = 2017;
            int yearCurrent = DateTime.Now.Year;

            for (int i = yearStart; i < yearCurrent + 1; i++)
            {
                ListItem li = new ListItem();
                li.Text = i.ToString();
                li.Value = i.ToString();
                this.ddlYear.Items.Add(li);
            }

            this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
        }


    }
}