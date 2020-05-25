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
    public partial class MachineTempTrend : System.Web.UI.Page
    {

        private static class TempColor {
          
            public static System.Drawing.Color Temp11_color = ColorTranslator.FromHtml("#EA0000");
            public static System.Drawing.Color Temp12_color = ColorTranslator.FromHtml("#FF00FF");
            public static System.Drawing.Color Temp13_color = ColorTranslator.FromHtml("#0000E3");
            public static System.Drawing.Color Temp14_color = ColorTranslator.FromHtml("#02C874");
            public static System.Drawing.Color Temp15_color = ColorTranslator.FromHtml("#82D900");

            public static System.Drawing.Color Temp21_color = ColorTranslator.FromHtml("#FFFF00");
            public static System.Drawing.Color Temp22_color = ColorTranslator.FromHtml("#9ACD32");
            public static System.Drawing.Color Temp23_color = ColorTranslator.FromHtml("#984B4B");
            public static System.Drawing.Color Temp24_color = ColorTranslator.FromHtml("#408080");
            public static System.Drawing.Color Temp25_color = ColorTranslator.FromHtml("#484891");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;


                    btn_Generate_Click(new object(), new EventArgs());

                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineTempTrend", "Page_Load Exception: " + ee.ToString());
                ShowWarning();
            }
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                string MachineID = this.ddl_Machine.SelectedValue;
                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate;
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate;
                DataTable dt_TempInfo = new DataTable();

                Common.Class.BLL.MouldingPqmHistory_BLL PqmBLL = new Common.Class.BLL.MouldingPqmHistory_BLL();

                DataTable dt = PqmBLL.GetList(DateFrom, DateTo, MachineID ,ref dt_TempInfo);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                    this.TempChart.Visible = true;
                    HideWarning();

                    ShowChart(dt);

                    ShowList(dt_TempInfo);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineTempTrend", "Page_Load btnGenerate_Click: " + ee.ToString());
                ShowWarning();
            }
        }

        void ShowChart(DataTable dt)
        {
            try
            {
                #region Chart Css
                this.TempChart.Series.Clear();
                this.TempChart.ChartAreas.Clear();
                this.TempChart.ChartAreas.Add("Area1");

                TempChart.ImageStorageMode = ImageStorageMode.UseImageLocation;

                TempChart.BackColor = Color.FromArgb(245, 245, 250);
              
                //TempChart.BackSecondaryColor = Color.Transparent;
                TempChart.BackGradientStyle = GradientStyle.None;
                
                //图表区背景
                TempChart.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
                //TempChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
                TempChart.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                TempChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                // TempChart.ChartAreas[0].AxisX.Interval = 0;
                TempChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                TempChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;

                TempChart.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90 | LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont;
                TempChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                TempChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;
                TempChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                TempChart.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

                //X坐标轴颜色
                TempChart.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
                TempChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
                TempChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                TempChart.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
                TempChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

                //X轴网络线条
                TempChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                TempChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

                //Y坐标轴颜色
                TempChart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
                TempChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
                TempChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                //Y坐标轴标题
                TempChart.ChartAreas[0].AxisY.Title = "Temperature";
                TempChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                TempChart.ChartAreas[0].AxisY.TitleForeColor = Color.Red;
                TempChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                TempChart.ChartAreas[0].AxisY.ToolTip = "Temperature";
                //Y轴网格线条
                TempChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                TempChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                TempChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                TempChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;

                //TempChart.ChartAreas[0].AxisY.Maximum = 300;
                //TempChart.ChartAreas[0].AxisY.Minimum = 260;
                //TempChart.ChartAreas[0].AxisY.MaximumAutoSize = 75;

                #region Y2 axis
                //Y坐标轴颜色
                //TempChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
                //TempChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.OrangeRed;
                //TempChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
                //Y坐标轴标题
                //TempChart.ChartAreas[0].AxisY2.Title = "Reject  Count";
                //TempChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                //TempChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
                //TempChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
                //TempChart.ChartAreas[0].AxisY2.ToolTip = "Reject Count";
                //Y轴网格线条
                //TempChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
                //TempChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.OrangeRed;
                //TempChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                //TempChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
                #endregion

                TempChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                #endregion

                Legend legend = new Legend();
                legend.Docking = Docking.Top;

                #region legend Setting
                int MachineID = int.Parse(ddl_Machine.SelectedValue);

                if (MachineID == 1 || MachineID == 6)
                {
                    legend.CustomItems.Add(TempColor.Temp11_color, "A NOZ"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(TempColor.Temp12_color, "A HEAD");
                    legend.CustomItems.Add(TempColor.Temp13_color, "A FRONT");
                    legend.CustomItems.Add(TempColor.Temp14_color, "A MIDDLE");
                    legend.CustomItems.Add(TempColor.Temp15_color, "A REAR");
                    legend.CustomItems.Add(TempColor.Temp21_color, "B NOZ"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(TempColor.Temp22_color, "B HEAD");
                    legend.CustomItems.Add(TempColor.Temp23_color, "B FRONT");
                    legend.CustomItems.Add(TempColor.Temp24_color, "B MIDDLE");
                    legend.CustomItems.Add(TempColor.Temp25_color, "B REAR");
                    legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[3].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[4].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[5].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[6].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[7].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[8].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[9].ImageStyle = LegendImageStyle.Rectangle;
                }
                else if (MachineID == 2 || MachineID == 3 || MachineID == 4 || MachineID == 5)
                {
                    legend.CustomItems.Add(TempColor.Temp11_color, "A NOZ"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(TempColor.Temp13_color, "A HEAD");
                    legend.CustomItems.Add(TempColor.Temp14_color, "A MIDDLE");
                    legend.CustomItems.Add(TempColor.Temp15_color, "A REAR");
                    legend.CustomItems.Add(TempColor.Temp21_color, "B NOZ"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(TempColor.Temp23_color, "B HEAD");
                    legend.CustomItems.Add(TempColor.Temp24_color, "B MIDDLE");
                    legend.CustomItems.Add(TempColor.Temp25_color, "B REAR");
                    legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[3].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[4].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[5].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[6].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[7].ImageStyle = LegendImageStyle.Rectangle;
                }
                else if (MachineID == 7 || MachineID == 8)
                {
                    legend.CustomItems.Add(TempColor.Temp11_color, "NOZ"); // 参数：(颜色, 说明)
                    legend.CustomItems.Add(TempColor.Temp12_color, "HEAD");
                    legend.CustomItems.Add(TempColor.Temp13_color, "FRONT");
                    legend.CustomItems.Add(TempColor.Temp14_color, "MIDDLE");
                    legend.CustomItems.Add(TempColor.Temp15_color, "REAR");
                    legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[1].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[2].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[3].ImageStyle = LegendImageStyle.Rectangle;
                    legend.CustomItems[4].ImageStyle = LegendImageStyle.Rectangle;
                }
                #endregion

                TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                TempChart.Legends[0].Enabled = true;

                #region Temp11-25 Series
                #region  Series temp11
                Series Series_Temp11 = new Series();
                Series_Temp11.ChartType = SeriesChartType.Line;
                Series_Temp11.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp11.Name = "Temperature11";
                Series_Temp11.XValueType = ChartValueType.String;
                Series_Temp11.YAxisType = AxisType.Primary;
                Series_Temp11.YValueType = ChartValueType.Double;
                Series_Temp11.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp11.LabelForeColor = Color.SteelBlue;
                Series_Temp11.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp11.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp11.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp12
                Series Series_Temp12 = new Series();
                Series_Temp12.ChartType = SeriesChartType.Line;
                Series_Temp12.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp12.Name = "Temperature12";
                Series_Temp12.XValueType = ChartValueType.String;
                Series_Temp12.YAxisType = AxisType.Primary;
                Series_Temp12.YValueType = ChartValueType.Double;
                Series_Temp12.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp12.LabelForeColor = Color.SteelBlue;
                Series_Temp12.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp12.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp12.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp13
                Series Series_Temp13 = new Series();
                Series_Temp13.ChartType = SeriesChartType.Line;
                Series_Temp13.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp13.Name = "Temperature13";
                Series_Temp13.XValueType = ChartValueType.String;
                Series_Temp13.YAxisType = AxisType.Primary;
                Series_Temp13.YValueType = ChartValueType.Double;
                Series_Temp13.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp13.LabelForeColor = Color.SteelBlue;
                Series_Temp13.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp13.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp13.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp14
                Series Series_Temp14 = new Series();
                Series_Temp14.ChartType = SeriesChartType.Line;
                Series_Temp14.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp14.Name = "Temperature14";
                Series_Temp14.XValueType = ChartValueType.String;
                Series_Temp14.YAxisType = AxisType.Primary;
                Series_Temp14.YValueType = ChartValueType.Double;
                Series_Temp14.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp14.LabelForeColor = Color.SteelBlue;
                Series_Temp14.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp14.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp14.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp15
                Series Series_Temp15 = new Series();
                Series_Temp15.ChartType = SeriesChartType.Line;
                Series_Temp15.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp15.Name = "Temperature15";
                Series_Temp15.XValueType = ChartValueType.String;
                Series_Temp15.YAxisType = AxisType.Primary;
                Series_Temp15.YValueType = ChartValueType.Double;
                Series_Temp15.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp15.LabelForeColor = Color.SteelBlue;
                Series_Temp15.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp15.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp15.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp21
                Series Series_Temp21 = new Series();
                Series_Temp21.ChartType = SeriesChartType.Line;
                Series_Temp21.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp21.Name = "Temperature21";
                Series_Temp21.XValueType = ChartValueType.String;
                Series_Temp21.YAxisType = AxisType.Primary;
                Series_Temp21.YValueType = ChartValueType.Double;
                Series_Temp21.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp21.LabelForeColor = Color.SteelBlue;
                Series_Temp21.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp21.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp21.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp22
                Series Series_Temp22 = new Series();
                Series_Temp22.ChartType = SeriesChartType.Line;
                Series_Temp22.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp22.Name = "Temperature22";
                Series_Temp22.XValueType = ChartValueType.String;
                Series_Temp22.YAxisType = AxisType.Primary;
                Series_Temp22.YValueType = ChartValueType.Double;
                Series_Temp22.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp22.LabelForeColor = Color.SteelBlue;
                Series_Temp22.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp22.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp22.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp23
                Series Series_Temp23 = new Series();
                Series_Temp23.ChartType = SeriesChartType.Line;
                Series_Temp23.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp23.Name = "Temperature23";
                Series_Temp23.XValueType = ChartValueType.String;
                Series_Temp23.YAxisType = AxisType.Primary;
                Series_Temp23.YValueType = ChartValueType.Double;
                Series_Temp23.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp23.LabelForeColor = Color.SteelBlue;
                Series_Temp23.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp23.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp23.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp24
                Series Series_Temp24 = new Series();
                Series_Temp24.ChartType = SeriesChartType.Line;
                Series_Temp24.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp24.Name = "Temperature24";
                Series_Temp24.XValueType = ChartValueType.String;
                Series_Temp24.YAxisType = AxisType.Primary;
                Series_Temp24.YValueType = ChartValueType.Double;
                Series_Temp24.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp24.LabelForeColor = Color.SteelBlue;
                Series_Temp24.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp24.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp24.Palette = ChartColorPalette.Bright;
                #endregion

                #region  Series temp25
                Series Series_Temp25 = new Series();
                Series_Temp25.ChartType = SeriesChartType.Line;
                Series_Temp25.ChartArea = this.TempChart.ChartAreas[0].Name;
                // dataSeries_JobOutPut.L = true;
                Series_Temp25.Name = "Temperature25";
                Series_Temp25.XValueType = ChartValueType.String;
                Series_Temp25.YAxisType = AxisType.Primary;
                Series_Temp25.YValueType = ChartValueType.Double;
                Series_Temp25.IsVisibleInLegend = false;




                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
                Series_Temp25.LabelForeColor = Color.SteelBlue;
                Series_Temp25.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值


                Series_Temp25.Color = Color.Lime;
                //Series_Temp.LegendText = legend.Name;
                // dataSeries_JobOutPut.IsValueShownAsLabel = true;
                //  dataSeries_JobOutPut.CustomProperties = "DrawingStyle = Cylinder";

                //TempChart.Legends.Add(legend);
                //TempChart.Legends[0].Position.Auto = false;
                Series_Temp25.Palette = ChartColorPalette.Bright;
                #endregion
                #endregion 


                double TempMaxValue = 0;
                double TempMinValue = 9999;

             
                foreach (DataRow dr in dt.Rows)
                {
                    DataPoint dataPoint = new DataPoint();

                    //Axis value  time
                    DateTime time = DateTime.Parse(dr["updatedTime"].ToString());

                    #region Axis Y value temperature 11-25
                    double Temp11 = double.Parse(dr["tempature11"].ToString());
                    double Temp12 = double.Parse(dr["tempature12"].ToString());
                    double Temp13 = double.Parse(dr["tempature13"].ToString());
                    double Temp14 = double.Parse(dr["tempature14"].ToString());
                    double Temp15 = double.Parse(dr["tempature15"].ToString());

                    double Temp21 = double.Parse(dr["tempature21"].ToString());
                    double Temp22 = double.Parse(dr["tempature22"].ToString());
                    double Temp23 = double.Parse(dr["tempature23"].ToString());
                    double Temp24 = double.Parse(dr["tempature24"].ToString());
                    double Temp25 = double.Parse(dr["tempature25"].ToString());
                    #endregion


                    #region Axis Y  Max Min value
                    if (MachineID == 2 || MachineID == 3 || MachineID == 4 || MachineID == 5)
                    {
                        #region Max Value
                        if (Temp11 > TempMaxValue && Temp11 != 0)
                        {
                            TempMaxValue = Temp11;
                        }
                        else if (Temp13 > TempMaxValue && Temp13 != 0)
                        {
                            TempMaxValue = Temp13;
                        }
                        else if (Temp14 > TempMaxValue && Temp14 != 0)
                        {
                            TempMaxValue = Temp14;
                        }
                        else if (Temp15 > TempMaxValue && Temp15 != 0)
                        {
                            TempMaxValue = Temp15;
                        }
                        else if (Temp21 > TempMaxValue && Temp21 != 0)
                        {
                            TempMaxValue = Temp21;
                        }
                        else if (Temp23 > TempMaxValue && Temp23 != 0)
                        {
                            TempMaxValue = Temp23;
                        }
                        else if (Temp24 > TempMaxValue && Temp24 != 0)
                        {
                            TempMaxValue = Temp24;
                        }
                        else if (Temp25 > TempMaxValue && Temp25 != 0)
                        {
                            TempMaxValue = Temp25;
                        }
                        #endregion

                        #region Min Value
                        if (Temp11 < TempMinValue && Temp11 != 0)
                        {
                            TempMinValue = Temp11;
                        }
                        else if (Temp13 < TempMinValue && Temp13 != 0)
                        {
                            TempMinValue = Temp13;
                        }
                        else if (Temp14 < TempMinValue && Temp14 != 0)
                        {
                            TempMinValue = Temp14;
                        }
                        else if (Temp15 < TempMinValue && Temp15 != 0)
                        {
                            TempMinValue = Temp15;
                        }
                        else if (Temp21 < TempMinValue && Temp21 != 0)
                        {
                            TempMinValue = Temp21;
                        }
                        else if (Temp23 < TempMinValue && Temp23 != 0)
                        {
                            TempMinValue = Temp23;
                        }
                        else if (Temp24 < TempMinValue && Temp24 != 0)
                        {
                            TempMinValue = Temp24;
                        }
                        else if (Temp25 < TempMinValue && Temp25 != 0)
                        {
                            TempMinValue = Temp25;
                        }
                        #endregion
                    }
                    else if (MachineID == 6 || MachineID == 1)
                    {
                        #region Max Value
                        if (Temp11 > TempMaxValue && Temp11 != 0)
                        {
                            TempMaxValue = Temp11;
                        }
                        else if (Temp12 > TempMaxValue && Temp12 != 0)
                        {
                            TempMaxValue = Temp12;
                        }
                        else if (Temp13 > TempMaxValue && Temp13 != 0)
                        {
                            TempMaxValue = Temp13;
                        }
                        else if (Temp14 > TempMaxValue && Temp14 != 0)
                        {
                            TempMaxValue = Temp14;
                        }
                        else if (Temp15 > TempMaxValue && Temp15 != 0)
                        {
                            TempMaxValue = Temp15;
                        }
                        else if (Temp21 > TempMaxValue && Temp21 != 0)
                        {
                            TempMaxValue = Temp21;
                        }
                        else if (Temp22 > TempMaxValue && Temp22 != 0)
                        {
                            TempMaxValue = Temp22;
                        }
                        else if (Temp23 > TempMaxValue && Temp23 != 0)
                        {
                            TempMaxValue = Temp23;
                        }
                        else if (Temp24 > TempMaxValue && Temp24 != 0)
                        {
                            TempMaxValue = Temp24;
                        }
                        else if (Temp25 > TempMaxValue && Temp25 != 0)
                        {
                            TempMaxValue = Temp25;
                        }
                        #endregion

                        #region Min Value
                        if (Temp11 < TempMinValue && Temp11 != 0)
                        {
                            TempMinValue = Temp11;
                        }
                        else if (Temp12 < TempMinValue && Temp12 != 0)
                        {
                            TempMinValue = Temp12;
                        }
                        else if (Temp13 < TempMinValue && Temp13 != 0)
                        {
                            TempMinValue = Temp13;
                        }
                        else if (Temp14 < TempMinValue && Temp14 != 0)
                        {
                            TempMinValue = Temp14;
                        }
                        else if (Temp15 < TempMinValue && Temp15 != 0)
                        {
                            TempMinValue = Temp15;
                        }
                        else if (Temp21 < TempMinValue && Temp21 != 0)
                        {
                            TempMinValue = Temp21;
                        }
                        else if (Temp22 < TempMinValue && Temp22 != 0)
                        {
                            TempMinValue = Temp22;
                        }
                        else if (Temp23 < TempMinValue && Temp23 != 0)
                        {
                            TempMinValue = Temp23;
                        }
                        else if (Temp24 < TempMinValue && Temp24 != 0)
                        {
                            TempMinValue = Temp24;
                        }
                        else if (Temp25 < TempMinValue && Temp25 != 0)
                        {
                            TempMinValue = Temp25;
                        }
                        #endregion
                    }
                    else if (MachineID == 7)
                    {
                        #region Max Value
                        if (Temp11 > TempMaxValue && Temp11 != 0)
                        {
                            TempMaxValue = Temp11;
                        }
                        else if (Temp12 > TempMaxValue && Temp12 != 0)
                        {
                            TempMaxValue = Temp12;
                        }
                        else if (Temp13 > TempMaxValue && Temp13 != 0)
                        {
                            TempMaxValue = Temp13;
                        }
                        else if (Temp14 > TempMaxValue && Temp14 != 0)
                        {
                            TempMaxValue = Temp14;
                        }
                        else if (Temp15 > TempMaxValue && Temp15 != 0)
                        {
                            TempMaxValue = Temp15;
                        }
                        #endregion

                        #region Min Value
                        if (Temp11 < TempMinValue && Temp11 != 0)
                        {
                            TempMinValue = Temp11;
                        }
                        else if (Temp12 < TempMinValue && Temp12 != 0)
                        {
                            TempMinValue = Temp12;
                        }
                        else if (Temp13 < TempMinValue && Temp13 != 0)
                        {
                            TempMinValue = Temp13;
                        }
                        else if (Temp14 < TempMinValue && Temp14 != 0)
                        {
                            TempMinValue = Temp14;
                        }
                        else if (Temp15 < TempMinValue && Temp15 != 0)
                        {
                            TempMinValue = Temp15;
                        }
                        #endregion
                    }
                    else if (MachineID == 8)
                    {
                        #region Max Value
                        if (Temp11 > TempMaxValue && Temp11 != 0)
                        {
                            TempMaxValue = Temp11;
                        }
                        else if (Temp12 > TempMaxValue && Temp12 != 0)
                        {
                            TempMaxValue = Temp12;
                        }
                        else if (Temp13 > TempMaxValue && Temp13 != 0)
                        {
                            TempMaxValue = Temp13;
                        }
                        else if (Temp14 > TempMaxValue && Temp14 != 0)
                        {
                            TempMaxValue = Temp14;
                        }
                        else if (Temp15 > TempMaxValue && Temp15 != 0)
                        {
                            TempMaxValue = Temp15;
                        }
                        #endregion

                        #region Min Value
                        if (Temp11 < TempMinValue && Temp11 != 0)
                        {
                            TempMinValue = Temp11;
                        }
                        else if (Temp12 < TempMinValue && Temp12 != 0)
                        {
                            TempMinValue = Temp12;
                        }
                        else if (Temp13 < TempMinValue && Temp13 != 0)
                        {
                            TempMinValue = Temp13;
                        }
                        else if (Temp14 < TempMinValue && Temp14 != 0)
                        {
                            TempMinValue = Temp14;
                        }
                        else if (Temp15 < TempMinValue && Temp15 != 0)
                        {
                            TempMinValue = Temp15;
                        }
                        #endregion
                    }

                    #endregion


                    #region Temp11-25 --- add point to series
                    #region temp11
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp11;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp11_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                    if (Temp11 != 0)
                    {
                        Series_Temp11.Points.Add(dataPoint);
                    }
                    #endregion

                    #region temp12
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp12;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp12_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                    if (Temp12 != 0)
                    {
                        Series_Temp12.Points.Add(dataPoint);
                    }
                    #endregion

                    #region temp13
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp13;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp13_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";
                    
                    if (Temp13 != 0)
                    {
                        Series_Temp13.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp14
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp14;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp14_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";
                    
                    if (Temp14 != 0)
                    {
                        Series_Temp14.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp15
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp15;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp15_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                    
                    if (Temp15 != 0)
                    {
                        Series_Temp15.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp21
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp21;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp21_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                   
                    if (Temp21 != 0)
                    {
                        Series_Temp21.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp22
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp22;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp22_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                
                    if (Temp22 != 0)
                    {
                        Series_Temp22.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp23
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp23;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp23_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                  
                    if (Temp23 != 0)
                    {
                        Series_Temp23.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp24
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp24;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp24_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                 
                    if (Temp24 != 0)
                    {
                        Series_Temp24.Points.Add(dataPoint);
                    }
                    #endregion 

                    #region temp25
                    dataPoint = new DataPoint();
                    dataPoint.AxisLabel = time.ToString("HH:mm");
                    dataPoint.YValues[0] = Temp25;// time.ToString("yyyy-mm-dd HH:mm");

                    dataPoint.Color = TempColor.Temp25_color;
                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
                    dataPoint.BackSecondaryColor = Color.SteelBlue;
                    dataPoint.BorderColor = Color.Black;
                    dataPoint.LabelForeColor = Color.Black;
                    //dataPoint.Url = "~/Webform/ProductivityDetail";

                
                    if (Temp25 != 0)
                    {
                        Series_Temp25.Points.Add(dataPoint);
                    }
                    #endregion
                    #endregion 

                }

                #region AxisY Maximum/Minimum
                if (TempMaxValue == 0 && TempMinValue == 9999)
                {
                    TempMaxValue = 350;
                    TempMinValue = 0;
                }
                else
                {
                    TempMaxValue += 5;
                    TempMinValue = TempMinValue <= 0 ? 0 : TempMinValue;
                }
                TempChart.ChartAreas[0].AxisY.Maximum = TempMaxValue;
                TempChart.ChartAreas[0].AxisY.Minimum = TempMinValue;
                #endregion

                //title
                this.TempChart.Titles.Add("Machine " + this.ddl_Machine.SelectedValue + " Temperature Trend Chart");
                TempChart.Titles[0].ForeColor = Color.Black;
                TempChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                TempChart.Titles[0].Alignment = ContentAlignment.TopCenter;
           

                #region show series
                if (MachineID == 1 || MachineID == 6 )
                {
                    this.TempChart.Series.Add(Series_Temp11);
                    this.TempChart.Series.Add(Series_Temp12);
                    this.TempChart.Series.Add(Series_Temp13);
                    this.TempChart.Series.Add(Series_Temp14);
                    this.TempChart.Series.Add(Series_Temp15);
                    this.TempChart.Series.Add(Series_Temp21);
                    this.TempChart.Series.Add(Series_Temp22);
                    this.TempChart.Series.Add(Series_Temp23);
                    this.TempChart.Series.Add(Series_Temp24);
                    this.TempChart.Series.Add(Series_Temp25);
                }
                else if (MachineID == 2 || MachineID == 3 || MachineID == 4 || MachineID == 5)
                {
                    this.TempChart.Series.Add(Series_Temp11);
                    //this.TempChart.Series.Add(Series_Temp12);
                    this.TempChart.Series.Add(Series_Temp13);
                    this.TempChart.Series.Add(Series_Temp14);
                    this.TempChart.Series.Add(Series_Temp15);
                    this.TempChart.Series.Add(Series_Temp21);
                    //this.TempChart.Series.Add(Series_Temp22);
                    this.TempChart.Series.Add(Series_Temp23);
                    this.TempChart.Series.Add(Series_Temp24);
                    this.TempChart.Series.Add(Series_Temp25);
                }
                else if (MachineID == 7 || MachineID == 8)
                {
                    this.TempChart.Series.Add(Series_Temp11);
                    this.TempChart.Series.Add(Series_Temp12);
                    this.TempChart.Series.Add(Series_Temp13);
                    this.TempChart.Series.Add(Series_Temp14);
                    this.TempChart.Series.Add(Series_Temp15);
                }
                #endregion

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineTempTrend", "Show Chart Exception:" + ee.ToString());
                ShowWarning();
            }
        }

        private void ShowList(DataTable dt)
        {
            this.dg_TempInfo.DataSource = dt.DefaultView;
            this.dg_TempInfo.DataBind();
        }




        private bool IsDisplay(string MachineID, string LineType)
        {
            bool Result = false;



            return Result;
        }

       

       private void ShowWarning()
        {
            this.TempChart.Visible = false;
            this.dg_TempInfo.Visible = false;
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
        }

        private void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
            this.TempChart.Visible = true;
            this.dg_TempInfo.Visible = true;
        }

        
    }
}