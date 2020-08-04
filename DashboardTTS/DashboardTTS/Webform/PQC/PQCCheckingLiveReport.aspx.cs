using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCProductivityDetailReport : System.Web.UI.Page
    {
        private readonly Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sJobNo = Request.QueryString["jobNumber"] == null ? "" : Request.QueryString["jobNumber"].ToString();



                    DateTime dDateFrom = new DateTime();
                    DateTime dDateTo = new DateTime();


                    if (sJobNo != "")
                    {
                        dDateFrom = DateTime.Parse("2019-1-1");
                        dDateTo = DateTime.Now.Date;

                        this.txtJobNo.Text = sJobNo;
                    }
                    else
                    {
                        dDateFrom = DateTime.Now.Date;
                        dDateTo = DateTime.Now.Date;
                    }



                    this.txtDateFrom.Text = dDateFrom.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = dDateTo.ToString("yyyy-MM-dd");

                    


                    btnGenerate_Click(new object(), new EventArgs());
                    
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
                    Common.CommFunctions.ShowWarning(this.lblResult,this.dgChecking, StaticRes.Global.ErrorLevel.Exception,ex.ToString());
                }
            }
                
            this.dgChecking.ItemCommand += DgJob_ItemCommand;
        }

        

        private void DgJob_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "jobMaintain")
            {
                string sTrackingID = e.Item.Cells[0].Text;
                string sJobID = e.Item.Cells[4].Text;


                DateTime dateFrom = DateTime.Parse(txtDateFrom.Text).AddMonths(-6);
                DateTime dateTo = DateTime.Parse(txtDateTo.Text).AddDays(1);

                DataTable dt = trackingBLL.GetList("", sJobID, dateFrom, dateTo, "", "", "", "", "");



                //有跨班多条记录的, 并且没有自跳转过的.
                if (dt.Rows.Count > 2 && Request.QueryString["jobNumber"] == null)
                {
                    Response.Redirect("./PQCCheckingLiveReport.aspx?jobNumber=" + sJobID);
                }
                else
                {
                    Response.Redirect("../../PQC_Product/Maintenance?JobID=" + sJobID + "&TrackingID=" + sTrackingID);
                }
            }
        }


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // search paras
                string machineID = this.ddlStation.SelectedItem.Value;
                string lotNo = this.txtLotNo.Text;
                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text.Trim()).Date;
                DateTime DateTo = DateTime.Parse(this.txtDateTo.Text.Trim()).Date.AddDays(1);
                string partNumber = this.txtPartNo.Text;
                string jobNumber = this.txtJobNo.Text;
                string shift = this.ddlShift.SelectedItem.Value;

         


               
                DataTable dtTracking = trackingBLL.GetList(partNumber, jobNumber, DateFrom, DateTo, shift, machineID, lotNo, "","");

                if (dtTracking == null || dtTracking.Rows.Count == 0)
                {
                    this.dgChecking.Visible = false;

                    this.lblResult.Text = "No Record";
                    this.lblResult.ForeColor = System.Drawing.Color.Red;
                    this.lblResult.Visible = true;

                    Common.CommFunctions.ShowWarning(this.lblResult, this.dgChecking, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {
                    Common.CommFunctions.HideWarning(this.lblResult, this.dgChecking);

                    this.dgChecking.Visible = true;
                    this.dgChecking.DataSource = dtTracking.DefaultView;
                    this.dgChecking.DataBind();
                }

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgChecking, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
            }
        }


    }
}