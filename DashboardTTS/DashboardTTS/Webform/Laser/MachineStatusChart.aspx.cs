using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace DashboardTTS.Webform.Laser
{
    public partial class MachineStatusChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    setYearDDL();
                    setMachineIDDDL();


                    this.txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

          
                    btnGenerate_Click(null, null);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineStatusChart", "Page_Load Exception: " + ee.ToString());
            }
        }



        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                #region 查询条件

                string sReportType = this.ddlReportType.SelectedValue;
           
                DateTime dSearchingFrom = new DateTime();
                DateTime dSearchingTo = new DateTime();
                string sSearchingMachineID = this.ddlMachineNo.SelectedItem.Value;
                string sSearchingStatus = this.ddlType.SelectedItem.Value;
                string sSearchingShift = this.ddlShift.SelectedItem.Value;
                bool exceptWeekend = this.ckbExceptWeekend.Checked;

                
                if (sReportType == "Yearly")
                {
                    //2017年只有后半年数据跳过.
                    dSearchingFrom = DateTime.Parse("2018-1-1");
                    dSearchingTo = DateTime.Now.Date;
                }
                else if (sReportType == "Monthly")
                {
                    dSearchingFrom = DateTime.Parse(this.ddlYear.SelectedValue + "-1-1");
                    dSearchingTo = dSearchingFrom.AddYears(1);
                }
                else if (sReportType == "Daily")
                {
                    dSearchingFrom = DateTime.Parse(this.txtDateFrom.Text);
                    dSearchingTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);
                }
                else if (sReportType == "Machine")
                {
                    dSearchingFrom = DateTime.Parse(this.txtDateFrom.Text);
                    dSearchingTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);
                }
                else if (sReportType == "Status")
                {
                    dSearchingFrom = DateTime.Parse(this.txtDateFrom.Text);
                    dSearchingTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);

                    sSearchingStatus = "";//赋空, 不进行删选, 让所有状态都显示.
                }
                #endregion
                

                Common.BLL.LMMSEventLog_BLL bll = new Common.BLL.LMMSEventLog_BLL();
                List<Common.Model.LMMSEventLog_Model.Detail> models = bll.GetStatusModelList(dSearchingFrom, dSearchingTo, sSearchingMachineID, sSearchingStatus, sSearchingShift, exceptWeekend);
                if (models == null || models.Count()== 0)
                {
                    this.ProdChart.Visible = false;
                    this.lblResult.Text = "There is no record!";
                    this.lblResult.ForeColor = Color.Red;
                    this.lblResult.Visible = true;
                }
                else
                {
                    this.ProdChart.Visible = true;
                    this.lblResult.Visible = false;
                    Display(models);
                }



            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineStatusChart", "btnGenerate_Click Exception: " + ee.ToString());
            }
        }




        void setYearDDL()
        {
            this.ddlYear.Items.Clear();


            int yearStart = 2017;
            int yearCurrent = DateTime.Now.Year;

            for (int i = yearStart; i < yearCurrent + 1; i++)
            {
                this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
        }

        void setMachineIDDDL()
        {
            this.ddlMachineNo.Items.Clear();
            this.ddlMachineNo.Items.Add(new ListItem("ALL", ""));

            for (int i = 1; i < 9; i++)
            {
                this.ddlMachineNo.Items.Add(new ListItem("No." + i.ToString(), i.ToString()));
            }
        }


        void Display(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {
            try
            {
                this.ProdChart.Series.Clear();
                this.ProdChart.ChartAreas.Clear();
                this.ProdChart.ChartAreas.Add("Area1");
                
                #region Chart Style
                ProdChart.BackColor = Color.FromArgb(245, 245, 250);
                //ProdChart.BackSecondaryColor = Color.Transparent;
                ProdChart.BackGradientStyle = GradientStyle.None;

                ProdChart.ImageStorageMode = ImageStorageMode.UseImageLocation;


                //图表区背景
                ProdChart.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
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


                if (this.ddlType.SelectedValue == "UTILIZATION" && this.ddlReportType.SelectedValue != "Status")
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Utilization%";
                }
                else
                {
                    ProdChart.ChartAreas[0].AxisY.Title = "Total Hours";
                }
                ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
                ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
                ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                ProdChart.ChartAreas[0].AxisY.ToolTip = this.ddlType.SelectedValue == "UTILIZATION" ? "Utilization%" : "Total Hours";
                //Y轴网格线条
                ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;

                //y轴最大值
                if (this.ddlType.SelectedValue == "UTILIZATION" && this.ddlReportType.SelectedValue != "Status")
                {
                    ProdChart.ChartAreas[0].AxisY.Maximum = 100;
                }
             


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



                #region Legend  hidden
                //Legend legend = new Legend();
                //legend.Docking = Docking.Top;
                //legend.Alignment = System.Drawing.StringAlignment.Far;

                //legend.CustomItems.Add(System.Drawing.Color.SkyBlue, "Pass Qty");
                //legend.CustomItems.Add(System.Drawing.Color.OrangeRed, "Rej%");
                //legend.CustomItems[0].ImageStyle = LegendImageStyle.Rectangle;
                //legend.CustomItems[1].ImageStyle = LegendImageStyle.Line;

                //this.ProdChart.Legends.Add(legend);
                #endregion




                Series seriesStatus = new Series();
                string reportType = this.ddlReportType.SelectedValue;
                if (reportType == "Yearly")
                {
                    seriesStatus = YearlySeries(models);
                }
                else if (reportType == "Monthly")
                {
                    seriesStatus = MonthlySeries(models);
                }
                else if (reportType == "Daily")
                {
                    seriesStatus = DailySeries(models);
                }
                else if (reportType == "Machine")
                {
                    seriesStatus = MachineSeries(models);
                }
                else if (reportType == "Status")
                {
                    seriesStatus = StatusSeries(models);
                }



                ProdChart.Series.Add(seriesStatus);

                ProdChart.Titles.Clear();
                if (ddlReportType.SelectedItem.Text == "Status")
                {
                    ProdChart.Titles.Add("Mahine Status Chart");
                }
                else
                {
                    ProdChart.Titles.Add(string.Format("Mahine {0} {1} Chart", ddlReportType.Text, ddlType.SelectedItem.Text));
                }
              
                ProdChart.Titles[0].ForeColor = Color.Black;
                ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
                ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductivityChart", "Usage click error : " + ee.Message);

            }
        }


        public Series YearlySeries(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {
            //yearly data
            var result = from a in models
                orderby a.year ascending
                group a by a.year into yearList
                select new
                {
                    year = yearList.Key,
                    totalSeconds = yearList.Sum(p => p.totalSeconds)
                };
              

            
            Series dataSeries_JobOutPut = InitSeries();

            foreach (var item in result)
            {
                #region Point Style
                DataPoint dpColumnPass = new DataPoint();
                dpColumnPass.Color = Color.DeepSkyBlue;
                dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                dpColumnPass.BorderColor = Color.Black;
                dpColumnPass.LabelForeColor = Color.Black;
                #endregion




                if (this.ddlType.SelectedValue == "UTILIZATION")
                {
                    DateTime dFrom = DateTime.Parse(item.year.ToString() + "-1-1");
                    DateTime dTo = dFrom.AddYears(1);
                    double totalSeconds = GetTotalSeconds(dFrom, dTo, ddlShift.SelectedValue, ddlMachineNo.SelectedValue, ckbExceptWeekend.Checked);
                    
                    double utilization = Math.Round(item.totalSeconds / totalSeconds * 100, 2);

                    dpColumnPass.AxisLabel = item.year.ToString();
                    dpColumnPass.YValues[0] = utilization;
                    dpColumnPass.Label = utilization.ToString("0.00") + "%";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", item.year, utilization.ToString("0.00") + "%");
                }
                else
                {
                    //各种状态
                    dpColumnPass.AxisLabel = item.year.ToString();
                    dpColumnPass.YValues[0] = Math.Round(item.totalSeconds / 3600, 2);
                    dpColumnPass.Label = Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", item.year, Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H");
                }



                dataSeries_JobOutPut.Points.Add(dpColumnPass);
            }
            return dataSeries_JobOutPut;
        }
        public Series MonthlySeries(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {

            var result = from a in models
                        orderby a.month ascending
                        group a by a.month into monthList
                        select new
                        {
                            month = monthList.Key,
                            totalSeconds = monthList.Sum(p => p.totalSeconds)
                        };

            
            Series dataSeries_JobOutPut = InitSeries();

            foreach (var item in result)
            {
                #region Point Style
                DataPoint dpColumnPass = new DataPoint();
                dpColumnPass.Color = Color.DeepSkyBlue;
                dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                dpColumnPass.BorderColor = Color.Black;
                dpColumnPass.LabelForeColor = Color.Black;
                #endregion

                if (this.ddlType.SelectedValue == "UTILIZATION")
                {
                    DateTime dFrom = DateTime.Parse(string.Format("{0}-{1}-1", this.ddlYear.SelectedValue, item.month));
                    double totalSeconds = GetTotalSeconds(dFrom, dFrom.AddMonths(1), ddlShift.SelectedValue, ddlMachineNo.SelectedValue, ckbExceptWeekend.Checked);
                    
                    double utilization = Math.Round(item.totalSeconds / totalSeconds * 100, 2);


                    dpColumnPass.AxisLabel = Common.CommFunctions.GetMonthName(item.month, true);
                    dpColumnPass.YValues[0] = utilization;
                    dpColumnPass.Label = utilization.ToString("0.00") + "%";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", Common.CommFunctions.GetMonthName(item.month, true), utilization.ToString("0.00") + "%");
                }
                else
                {
                    dpColumnPass.AxisLabel = Common.CommFunctions.GetMonthName(item.month, true);
                    dpColumnPass.YValues[0] = Math.Round(item.totalSeconds / 3600, 2);
                    dpColumnPass.Label = Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", Common.CommFunctions.GetMonthName(item.month, true), Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H");
                }



                dataSeries_JobOutPut.Points.Add(dpColumnPass);
            }

            return dataSeries_JobOutPut;
        }
        public Series DailySeries(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {
            var result = from a in models                    
                         orderby a.day ascending
                         group a by a.day into dayList
                         select new
                         {
                             day = dayList.Key,
                             totalSeconds = dayList.Sum(p => p.totalSeconds)
                         };
            

            Series dataSeries_JobOutPut = InitSeries();

            foreach (var item in result)
            {
                #region Point Style
                DataPoint dpColumnPass = new DataPoint();
                dpColumnPass.Color = Color.DeepSkyBlue;
                dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                dpColumnPass.BorderColor = Color.Black;
                dpColumnPass.LabelForeColor = Color.Black;
                #endregion


                if (this.ddlType.SelectedValue == "UTILIZATION")
                {
                    double totalSeconds = GetTotalSeconds(item.day, item.day.AddDays(1), ddlShift.SelectedValue, ddlMachineNo.SelectedValue, ckbExceptWeekend.Checked);

                    double utilization = totalSeconds ==0? 0: Math.Round(item.totalSeconds / totalSeconds * 100, 2);

                    
                    dpColumnPass.AxisLabel = Common.CommFunctions.GetMonthName(item.day.Month, false) + "-" + item.day.Day.ToString();
                    dpColumnPass.YValues[0] = utilization;
                    dpColumnPass.Label = utilization.ToString("0.00") + "%";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", item.day.ToShortDateString(), utilization.ToString("0.00") + "%");
                }
                else
                {
                    dpColumnPass.AxisLabel = Common.CommFunctions.GetMonthName(item.day.Month, false) + "-" + item.day.Day.ToString();
                    dpColumnPass.YValues[0] = Math.Round(item.totalSeconds / 3600, 2);
                    dpColumnPass.Label = Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", item.day.ToShortDateString(), Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H");
                }


                dataSeries_JobOutPut.Points.Add(dpColumnPass);
            }

            return dataSeries_JobOutPut;
        }
        public Series MachineSeries(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {
            DateTime dateFrom = DateTime.Parse(this.txtDateFrom.Text);
            DateTime dateTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);
            
            var allMachine = from a in models
                             orderby a.machineID ascending
                             group a by a.machineID into machineList
                             select new
                             {
                                 machineList.Key,
                                 totalSeconds = machineList.Sum(p => p.totalSeconds)
                             };

            
            Series dataSeries_JobOutPut = InitSeries();

            foreach (var item in allMachine)
            {
                #region Point Style
                DataPoint dpColumnPass = new DataPoint();
                dpColumnPass.Color = Color.DeepSkyBlue;
                dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                dpColumnPass.BorderColor = Color.Black;
                dpColumnPass.LabelForeColor = Color.Black;
                #endregion

                

                if (this.ddlType.SelectedValue == "UTILIZATION")
                {
                    double totalSeconds = GetTotalSeconds(dateFrom, dateTo, ddlShift.SelectedValue, ddlMachineNo.SelectedValue, ckbExceptWeekend.Checked);
                    double utilization = Math.Round(item.totalSeconds / totalSeconds * 100 *8 , 2);
                    

                    dpColumnPass.AxisLabel = "Machine" + item.Key;
                    dpColumnPass.YValues[0] = utilization;
                    dpColumnPass.Label = utilization.ToString("0.00") + "%";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", "Machine"+item.Key, utilization.ToString("0.00") + "%");
                }
                else
                {
                    dpColumnPass.AxisLabel = "Machine" + item.Key;
                    dpColumnPass.YValues[0] = Math.Round(item.totalSeconds / 3600, 2);
                    dpColumnPass.Label = Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H";
                    dpColumnPass.ToolTip = string.Format("{0}-{1}", "Machine" + item.Key, Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H");
                }


                dataSeries_JobOutPut.Points.Add(dpColumnPass);
            }

            return dataSeries_JobOutPut;
        }
        public Series StatusSeries(List<Common.Model.LMMSEventLog_Model.Detail> models)
        {
            var result = from a in models
                         group a by a.status into dayList
                         select new
                         {
                             status = dayList.Key,
                             totalSeconds = dayList.Sum(p => p.totalSeconds)
                         };
            
            //时间大到小排序
            var sorted = from a in result
                         orderby a.totalSeconds descending
                         select a;


             Series dataSeries_JobOutPut = InitSeries();

            foreach (var item in sorted)
            {
                #region Point Style
                DataPoint dpColumnPass = new DataPoint();
                dpColumnPass.Color = Color.DeepSkyBlue;
                dpColumnPass.BackSecondaryColor = Color.RoyalBlue;
                dpColumnPass.BackGradientStyle = GradientStyle.LeftRight;
                dpColumnPass.BorderColor = Color.Black;
                dpColumnPass.LabelForeColor = Color.Black;
                #endregion


                dpColumnPass.AxisLabel = item.status == "POWER OFF" ? "SHUTDOWN" : item.status;
                dpColumnPass.YValues[0] = Math.Round(item.totalSeconds / 3600, 2);
                dpColumnPass.Label = Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H";
                dpColumnPass.ToolTip = string.Format("{0}-{1}", item.status, Math.Round(item.totalSeconds / 3600, 2).ToString("0.00") + "H");


                dataSeries_JobOutPut.Points.Add(dpColumnPass);
            }

            return dataSeries_JobOutPut;
        }



        public Series InitSeries()
        {
            System.Web.UI.DataVisualization.Charting.Series dataSeries_JobOutPut = new Series();

            //if (this.ddlType.SelectedValue == "UTILIZATION")
            //    dataSeries_JobOutPut.ChartType = SeriesChartType.Line;
            //else
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

            return dataSeries_JobOutPut;
        }
        
        public double GetTotalSeconds(DateTime dDateFrom, DateTime dDateTo, string sShift,string sMachineID, bool bExceptWeekends)
        {
            
            double hoursForOneDay = sShift == "" ? 24 : 12;
            double machineCount = sMachineID == "" ? 8 : 1;


            double totalSeconds = 0;

            if (DateTime.Now.Date >= dDateFrom && DateTime.Now.Date < dDateTo)
            {
                //from 到 to-1 的总时间    加上最后一天当天的总时间
                totalSeconds = (dDateTo.AddDays(-1) - dDateFrom).TotalDays * hoursForOneDay * 3600 * machineCount;



                DateTime currentDay = DateTime.Now.AddHours(-8).Date;
                string currentShift = DateTime.Now >= DateTime.Now.Date.AddHours(8) && DateTime.Now < DateTime.Now.Date.AddHours(20) ? "Day" : "Night";
                //当天 day/night 班次总时长,   (当天按照 8/20到现在的时长计算)
                double currentDayShiftTotalSeconds = 0;
                double currentNightShiftTotalSeconds = 0;
                if (currentShift == StaticRes.Global.Shift.Day)
                {
                    currentDayShiftTotalSeconds = (DateTime.Now - currentDay.AddHours(8)).TotalSeconds * machineCount; //8点到现在的总时间
                    currentNightShiftTotalSeconds = 0;
                }
                else
                {
                    currentDayShiftTotalSeconds = 12 * 3600 * machineCount;
                    currentNightShiftTotalSeconds = (DateTime.Now - currentDay.AddHours(20)).TotalSeconds  * machineCount;//20:00到现在的总时间
                }
                





                double lastDayTotalSeconds = 0;

                if (sShift == "")
                {
                    lastDayTotalSeconds = currentDayShiftTotalSeconds + currentNightShiftTotalSeconds;
                }
                else if(sShift =="Day")
                {
                    lastDayTotalSeconds = currentDayShiftTotalSeconds;
                }
                else if (sShift == "Night")
                {
                    lastDayTotalSeconds = currentNightShiftTotalSeconds;
                }



                totalSeconds += lastDayTotalSeconds;
            }
            else
            {
                totalSeconds = (dDateTo - dDateFrom).TotalDays * hoursForOneDay * 3600 * machineCount;
            }
             

          



            double totalExceptDays = 0;

            if (bExceptWeekends)
            {
                DateTime dTemp = dDateFrom.Date;
                while (dTemp < dDateTo)
                {
                    if (dTemp.DayOfWeek == DayOfWeek.Sunday || dTemp.DayOfWeek == DayOfWeek.Saturday)
                        totalExceptDays++;

                    dTemp = dTemp.AddDays(1);
                }
            }

            


            double totalExceptSeconds = totalExceptDays * hoursForOneDay * 3600 * machineCount;



            return totalSeconds - totalExceptSeconds;
        }
        
        
    }
}