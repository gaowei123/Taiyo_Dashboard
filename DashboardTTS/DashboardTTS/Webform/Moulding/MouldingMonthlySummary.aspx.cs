using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMonthlySummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    setYearDDL();


                    this.ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

                    btnGenerate_Click(null, null);

                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlySummary", "Page_Load Exception: " + ee.ToString());
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string sYear = this.ddlYear.SelectedValue;
                string sMonth = this.ddlMonth.SelectedValue;


                setMain(sYear);
                
                setRejection(sYear);

                setSummary(sYear, sMonth);

                
                setTraceability(sYear, sMonth);
                


            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlySummary", "btnGenerate_Click Exception: " + ee.ToString());
            }
        }


        public void setMain(string sYear)
        {
            try
            {

                string sType = "";// this.dllType.Text.Trim();


                DateTime dDateFrom = DateTime.Now;
                DateTime dDateTo = DateTime.Now;

                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dt = bll.GetMonthlyReport(int.Parse(sYear), sType);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.dgMain.Visible = false;
                    this.lbMainResult.Visible = true;
                    this.lbMainResult.ForeColor = System.Drawing.Color.Red;
                    this.lbMainResult.Text = "There is no record!";

                }
                else
                {
                    this.lbMainResult.Visible = false;
                    this.dgMain.Visible = true;
                    this.dgMain.DataSource = dt.DefaultView;
                    this.dgMain.DataBind();
                }
            }
            catch (Exception ee)
            {

                throw;
            }
        }

        public void setRejection(string sYear)
        {
            try
            {
                Common.Class.BLL.MouldingViDefectTracking_BLL bll = new Common.Class.BLL.MouldingViDefectTracking_BLL();

                DataTable dt = bll.GetMonthlyReport(sYear, "");


                if (dt == null || dt.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowWarning(this.lbRejResult, this.dgRejection, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {
                    Common.CommFunctions.HideWarning(this.lbRejResult, this.dgRejection);


                    this.dgRejection.DataSource = dt.DefaultView;
                    this.dgRejection.DataBind();
                }
            }
            catch (Exception ee)
            {
                
            }
        }


        public void setSummary(string sYear, string sMonth)
        {
            try
            {
                DateTime dDateFrom = DateTime.Parse(string.Format("{0}-{1}-01", sYear, sMonth)).AddHours(8);
                DateTime dDateTo = dDateFrom.AddMonths(1);
                string sType = this.ddlType.Text.Trim();


                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dt = bll.GetSummaryReport(dDateFrom, dDateTo, sType);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lbSummaryResult.Visible = true;
                    this.lbSummaryResult.Text = "There is no record!";
                    this.lbSummaryResult.ForeColor = System.Drawing.Color.Red;

                    this.dgSummary.Visible = false;
                }
                else
                {
                   
                    this.dgSummary.Visible = true;
                    this.dgSummary.DataSource = dt.DefaultView;
                    this.dgSummary.DataBind();

                    this.lbSummaryResult.Visible = false;
                }
            }
            catch (Exception ee)
            {
                
            }
        }


        public void setTraceability(string sYear, string sMonth)
        {
            try
            {
                string sPartNo = "";// this.ddlPartNo.SelectedValue;
                string sCustomer = "";// this.txtCustomer.Text;
                string sType = "";// this.ddlType.SelectedValue;
                DateTime dDateFrom = DateTime.Parse(string.Format("{0}-{1}-01", sYear, sMonth)).AddHours(8);
                DateTime dDateTo = dDateFrom.AddMonths(1);



                Common.Class.BLL.MouldingViHistory_BLL viTrackingBLL = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dt = viTrackingBLL.GetMonthlyProductionReport(dDateFrom, dDateTo, sPartNo, sCustomer, sType);

                //横表变竖表
                DataTable dtConverted = Datatable_RowConvertColumn(dt);

                Display(dtConverted);

            }
            catch (Exception ee)
            {
                
            }
        }




        public DataTable Datatable_RowConvertColumn(DataTable dtOld)
        {
            if (dtOld == null)
                return null;


            DataTable dtNew = new DataTable();

            int newTableColumnCount = dtOld.Rows.Count;
            int newTableRowCount = dtOld.Columns.Count;


            //==== set new table column ====//
            //在datagrid中显示时会屏蔽表头  column没实际意义
            //newTableColumnCount + 1  ==  所有数据行总数 + 表头一行 
            for (int i = 0; i < newTableColumnCount + 1; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }




            //==== set new table row ====//
            for (int i = 0; i < newTableRowCount; i++)
            {
                DataRow drNew = dtNew.NewRow();

                //新一行第一列, 显示当前年月,  其余都为 dtOld 的 column name
                if (i == 0)
                {
                    drNew[0] = string.Format("{0}-{1}", this.ddlMonth.Text, this.ddlYear.SelectedValue);
                }
                else
                {
                    drNew[0] = dtOld.Columns[i].ColumnName;
                }

                //dtOld每列的第i个, 添加到dtNew的一行中
                for (int x = 0; x < dtOld.Rows.Count; x++)
                {
                    drNew[x + 1] = dtOld.Rows[x][i].ToString();
                }

                dtNew.Rows.Add(drNew);
            }
            //==== set new table row ====//


            return dtNew;
        }

        void Display(DataTable dt)
        {
            try
            {
                if (dt == null)
                {
                    Common.CommFunctions.ShowWarning(this.lbTraceabilityResult, this.dgTraceability, StaticRes.Global.ErrorLevel.Warning, "");
                    return;
                }
                else
                {
                    Common.CommFunctions.HideWarning(this.lbTraceabilityResult, this.dgTraceability);


                    this.dgTraceability.DataSource = dt.DefaultView;
                    this.dgTraceability.DataBind();
                }

                this.dgTraceability.ShowHeader = false;



                //特殊行, 行号定义.
                int totalColumns = dt.Columns.Count;
                int totalRows = this.dgTraceability.Items.Count;
                int weekRowIndex = 0;
                int dayRowIndex = 1;
                int totalQtyRowIndex = 2;
                int shiftRowIndex = 3;
                int totalDailyRejRateRowIndex = 6;
                int defectRowIndex = 7;
                int cumuQtyRowIndex = dt.Rows.Count - 3;
                int cumuRejRowIndex = dt.Rows.Count - 2;
                int aveRejRowIndex = dt.Rows.Count - 1;




                //第一列靠左
                for (int i = 0; i < totalRows; i++)
                {
                    this.dgTraceability.Items[i].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                }


                //第一行 字体加粗
                this.dgTraceability.Items[0].Font.Bold = true;


                //第一行 第一列 日期红色
                this.dgTraceability.Items[0].Cells[0].ForeColor = System.Drawing.Color.Red;


                //循环每一列, day赋浅绿色, night赋浅橙色
                for (int i = 1; i < totalColumns; i++)
                {

                    string sFlag = this.dgTraceability.Items[shiftRowIndex].Cells[i].Text;

                    if (sFlag == "Day")
                    {
                        for (int row = shiftRowIndex; row < totalRows - 3; row++)
                        {
                            this.dgTraceability.Items[row].Cells[i].BackColor = System.Drawing.Color.FromArgb(226, 239, 218);
                        }
                    }
                    else if (sFlag == "Night")
                    {
                        for (int row = shiftRowIndex; row < totalRows - 3; row++)
                        {
                            this.dgTraceability.Items[row].Cells[i].BackColor = System.Drawing.Color.FromArgb(255, 242, 204);
                        }
                    }
                }



                //defect字体加粗, 浅灰色背景
                this.dgTraceability.Items[defectRowIndex].Font.Bold = true;
                for (int i = 0; i < totalColumns; i++)
                {
                    this.dgTraceability.Items[defectRowIndex].Cells[i].BackColor = System.Drawing.Color.WhiteSmoke;
                    this.dgTraceability.Items[defectRowIndex].Cells[i].BorderStyle = BorderStyle.None;
                }



                //defect code去0 为空
                for (int x = defectRowIndex; x < cumuQtyRowIndex; x++)
                {
                    for (int y = 0; y < totalColumns; y++)
                    {
                        string sValue = this.dgTraceability.Items[x].Cells[y].Text;
                        if (sValue == "0")
                        {
                            this.dgTraceability.Items[x].Cells[y].Text = "";
                        }
                    }
                }



                //合并单元格  根据前一列day的值判断是否是同一天, 是的话合并.
                string curDay = "";
                string lastDay = "";
                for (int i = 1; i < totalColumns; i++)
                {
                    curDay = this.dgTraceability.Items[dayRowIndex].Cells[i].Text;

                    if (curDay == lastDay)
                    {
                        //week
                        this.dgTraceability.Items[weekRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[weekRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //day
                        this.dgTraceability.Items[dayRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[dayRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //totalQty
                        this.dgTraceability.Items[totalQtyRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[totalQtyRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //totalDailyRejRate
                        this.dgTraceability.Items[totalDailyRejRateRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[totalDailyRejRateRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //cumuqty
                        this.dgTraceability.Items[cumuQtyRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[cumuQtyRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //cumurejqty
                        this.dgTraceability.Items[cumuRejRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[cumuRejRowIndex].Cells[i - 1].ColumnSpan = 2;
                        //averej%
                        this.dgTraceability.Items[aveRejRowIndex].Cells[i].Visible = false;
                        this.dgTraceability.Items[aveRejRowIndex].Cells[i - 1].ColumnSpan = 2;
                    }

                    lastDay = curDay;
                }


                //设置totalqty,  totalDaily Rej Rate 2行 背景色
                for (int i = 0; i < totalColumns; i++)
                {
                    this.dgTraceability.Items[totalDailyRejRateRowIndex].Cells[i].BackColor = System.Drawing.Color.White;

                    if (i != 0)
                    {
                        this.dgTraceability.Items[totalQtyRowIndex].Cells[i].BackColor = System.Drawing.Color.FromArgb(226, 239, 218);
                    }
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlyProductionReport", "Display Exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(this.lbTraceabilityResult, this.dgTraceability, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
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