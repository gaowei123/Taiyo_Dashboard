using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Text;


namespace DashboardTTS.Webform
{
    public partial class ProductivityChart : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    setYearDDL();
                    setMachineIDDDL();



                    this.txtDateFrom.Text = DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    

                    btnGenerate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductivityChart", "Page_Load Exception :" + ee.ToString());
                showWarning(ee.ToString());
            }
            
        }
    


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string sReportType = this.ddlReportType.SelectedValue;
                string sYear = this.ddlYear.SelectedValue;
                string sMachineID = this.ddlMachineNo.SelectedValue;
                string sPartNo = this.txtPartNo.Text.Trim();
                string sShift = this.ddlShift.SelectedValue;
                string sModel = this.txtModel.Text.Trim();
                DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).AddHours(8);
                DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1).AddHours(8);




                Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dt = WatchDogBll.GetProductivityChartData(sReportType, sYear, sMachineID, sPartNo, sShift,sModel, dDateFrom, dDateTo);


          

                if (dt == null || dt.Rows.Count == 0)
                {
                    showWarning("There is no record!");
                }
                else
                {
                    hideWarning();
                    ChartDisplay_Job(dt);
                }
            }
            catch ( Exception ex)
            {
                showWarning(ex.ToString());

                DBHelp.Reports.LogFile.Log("ProductivityChart", "btnGenerate_Click error : " + ex.ToString());

            }
        }
        
        
        void ChartDisplay_Job(DataTable dt)
        {
            try
            {
                this.ProdChart.Series.Clear();
                this.ProdChart.ChartAreas.Clear();
                this.ProdChart.ChartAreas.Add("Area1");

              
               

                #region Chart Style
                ProdChart.BackColor =  Color.FromArgb( 245, 245, 250);
                //ProdChart.BackSecondaryColor = Color.Transparent;
                ProdChart.BackGradientStyle = GradientStyle.None;

                ProdChart.ImageStorageMode = ImageStorageMode.UseImageLocation;


                //图表区背景
                ProdChart.ChartAreas[0].BackColor =  Color.FromArgb(245, 245, 250);
                //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
                ProdChart.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                ProdChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                // ProdChart.ChartAreas[0].AxisX.Interval = 0;

                //model 型号太长竖着显示
                ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = this.ddlReportType.SelectedValue == "Model" || this.ddlReportType.SelectedValue == "Daily" ? -90 : 0;
                ProdChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                ProdChart.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                ProdChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                ProdChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular);
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
                ProdChart.ChartAreas[0].AxisY.Title = "Total Pass Qty";
                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                ProdChart.ChartAreas[0].AxisY.ToolTip = "Total Pass Qty";
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
                ProdChart.ChartAreas[0].AxisY2.Title = "Rej%";
                ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                ProdChart.ChartAreas[0].AxisY2.ToolTip = "Rej%";
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;

                ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion


                #region series Style

                System.Web.UI.DataVisualization.Charting.Series dataSeries_JobOutPut = new Series();
                dataSeries_JobOutPut.ChartType = SeriesChartType.Column;
                dataSeries_JobOutPut.ChartArea = this.ProdChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                dataSeries_JobOutPut.Name = "Job Output";
                dataSeries_JobOutPut.XAxisType = AxisType.Primary;
                dataSeries_JobOutPut.XValueType = ChartValueType.String;
                dataSeries_JobOutPut.YAxisType = AxisType.Primary;
                dataSeries_JobOutPut.YValueType = ChartValueType.Double;
                dataSeries_JobOutPut.IsVisibleInLegend = false;
                dataSeries_JobOutPut.LabelForeColor = Color.SteelBlue;
                dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值
                dataSeries_JobOutPut.Color = Color.Lime;
                dataSeries_JobOutPut.Palette = ChartColorPalette.Bright;




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
                dataSeries_RejRate.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                dataSeries_RejRate.MarkerColor = Color.White;
                dataSeries_RejRate.MarkerSize = 10;
                dataSeries_RejRate.MarkerStyle = MarkerStyle.Circle;
                dataSeries_RejRate.MarkerBorderWidth = 1;
                dataSeries_RejRate.MarkerBorderColor = Color.DarkRed;
                dataSeries_RejRate.BorderWidth = 2;

                #endregion


                #region Legend
                Legend legend = new Legend();
                legend.Docking = Docking.Top;
                legend.Alignment = System.Drawing.StringAlignment.Far;

                legend.CustomItems.Add(System.Drawing.Color.SkyBlue, "Pass Qty");
                legend.CustomItems.Add(System.Drawing.Color.OrangeRed, "Rej%");
                legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;
                legend.CustomItems[1].ImageStyle = LegendImageStyle.Line;
                
                this.ProdChart.Legends.Add(legend);
                #endregion


                foreach (DataRow dr in dt.Rows)
                {
                    #region Point Style
                    DataPoint dpColumnPass = new DataPoint();
                    dpColumnPass.Color = Color.DeepSkyBlue;
                    dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                    dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                    dpColumnPass.BorderColor = Color.Black;
                    dpColumnPass.LabelForeColor = Color.Black;
                    
                    DataPoint dpLineRejRate = new DataPoint();
                    dpLineRejRate.Color = System.Drawing.Color.OrangeRed;
                    dpLineRejRate.MarkerStyle = MarkerStyle.Circle;
                    dpLineRejRate.MarkerBorderWidth = 3;
                    #endregion

                    string xValue = dr[0].ToString();
                    double totalPass = double.Parse(dr["totalPass"].ToString());
                    double totalFail = double.Parse(dr["totalFail"].ToString());
                    //double totalQuantity = double.Parse(dr["totalQuantity"].ToString());
                    double rejReate = double.Parse(dr["rejReate"].ToString());
                    

                    dpColumnPass.AxisLabel = xValue;
                    dpColumnPass.YValues[0] = totalPass;
                    dpColumnPass.Label = totalPass.ToString();
                    dpColumnPass.ToolTip = "OK: " + totalPass.ToString();


                    dpLineRejRate.AxisLabel = xValue;
                    dpLineRejRate.YValues[0] = rejReate;
                    dpLineRejRate.Label = rejReate + "%";
                    dpLineRejRate.ToolTip = "NG: " + totalFail.ToString();

                    dataSeries_JobOutPut.Points.Add(dpColumnPass);
                    dataSeries_RejRate.Points.Add(dpLineRejRate);
                }


                ProdChart.Series.Add(dataSeries_JobOutPut);
                ProdChart.Series.Add(dataSeries_RejRate);


                ProdChart.Titles.Clear();
                ProdChart.Titles.Add(this.ddlReportType.SelectedValue + " Productivity Data ");
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;

               
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductivityChart", "Usage click error : " + ee.Message);
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

        void setMachineIDDDL()
        {
            this.ddlMachineNo.Items.Clear();
            this.ddlMachineNo.Items.Add(new ListItem("ALL", ""));
            
            for (int i =1; i < 9; i++)
            {
                this.ddlMachineNo.Items.Add(new ListItem("No."+i.ToString(), i.ToString()));
            }
        }

        void showWarning(string message)
        {
            this.lblResult.Text = message;
            this.lblResult.ForeColor = Color.Red;
            this.lblResult.Visible = true;

            this.ProdChart.Visible = false;
        }

        void hideWarning()
        {
            this.lblResult.Visible = false;
            this.ProdChart.Visible = true;
        }

        

        
    }
}