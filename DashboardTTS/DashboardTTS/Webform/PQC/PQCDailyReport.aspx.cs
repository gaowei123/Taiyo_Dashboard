using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCDailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DateTime dDay = Common.CommFunctions.GetDefaultReportsSearchingDay();
                    this.txtDateFrom.Text = dDay.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = dDay.ToString("yyyy-MM-dd");


                    
                    BtnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCDailyReport", "Page_Load error : " + ex.ToString());
                }
            }

            this.dgPQCDailyReport.ItemCommand += DgPQCDailyReport_ItemCommand;
        }

        private void DgPQCDailyReport_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string sLotNo = e.Item.Cells[7].Text;
                DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dDateTo = DateTime.Parse(this.txtDateFrom.Text).Date;

                if (e.CommandName == "LinkDetail")
                {
                    string sJobNumber = e.Item.Cells[7].Text;
                    Response.Redirect("../Reports/BuyOffReport.aspx?jobNumber=" + sJobNumber);
                }
                else if (e.CommandName == "LinkMouldingDetail")
                {
                    string mouldingRej = e.Item.Cells[13].Text;
                    if (mouldingRej == "0")
                        return;
                    
                    Response.Redirect(string.Format("../PQC/PQCRejectDetail.aspx?lotNo={0}&Type=Mould&DateFrom={1:G}&DateTo={2:G}", sLotNo, dDateFrom, dDateTo));
                }
                else if (e.CommandName == "LinkPaintingDetail")
                {
                    string paintingRej = e.Item.Cells[14].Text;
                    if (paintingRej == "0")
                        return;

                    Response.Redirect(string.Format("../PQC/PQCRejectDetail.aspx?lotNo={0}&Type=Paint&DateFrom={1:G}&DateTo={2:G}", sLotNo, dDateFrom, dDateTo));
                }
                else if (e.CommandName == "LinkLaserDetail")
                {
                    string laserRej = e.Item.Cells[15].Text;
                    if (laserRej == "0")
                        return;


                    Response.Redirect(string.Format("../PQC/PQCRejectDetail.aspx?lotNo={0}&Type=Laser&DateFrom={1:G}&DateTo={2:G}", sLotNo, dDateFrom, dDateTo));
                }
                else if (e.CommandName == "LinkOthersDetail")
                {
                    string othersRej = e.Item.Cells[16].Text;
                    if (othersRej == "0")
                        return;

                    

                    Response.Redirect(string.Format("../PQC/PQCRejectDetail.aspx?lotNo={0}&Type=Others&DateFrom={1:G}&DateTo={2:G}", sLotNo, dDateFrom, dDateTo));
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCDailyReport", "DgPQCDailyReport_ItemCommand error : " + ee.ToString());
            }
        }
        

        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string partNumber = this.txtPartNo.Text.Trim();
                string shift = this.ddlShift.SelectedValue;
                string sProcess = this.ddlProcess.SelectedValue;
                string sStation = this.ddlStation.SelectedValue;
                string sPIC = this.txtPIC.Text.Trim();
                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text).Date.AddHours(8);
                DateTime DateTo = DateTime.Parse(this.txtDateTo.Text).Date.AddHours(8).AddDays(1);


                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                DataTable dt = new DataTable();// bll.getDailyReport(partNumber, DateFrom, DateTo, shift, sProcess, sStation, sPIC);

               

                Display(dt);
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCDailyReport", "BtnGenerate_Click error : " + ex.ToString());
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgPQCDailyReport, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
            }
        }



        void Display(DataTable dt )
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgPQCDailyReport, StaticRes.Global.ErrorLevel.Warning, "");
                return;
            }

            this.dgPQCDailyReport.DataSource = dt.DefaultView;
            this.dgPQCDailyReport.DataBind();

            Common.CommFunctions.HideWarning(this.lblResult, this.dgPQCDailyReport);
        }


        
    }
}