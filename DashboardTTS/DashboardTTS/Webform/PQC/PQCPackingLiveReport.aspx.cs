using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCPackingLiveReport : System.Web.UI.Page
    {
        private readonly Common.Class.BLL.PQCPackTracking packBLL = new Common.Class.BLL.PQCPackTracking();
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
                    Common.CommFunctions.ShowWarning(this.lblResult, this.dgPacking, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
                }
            }

            this.dgPacking.ItemCommand += DgPacking_ItemCommand;
        }


        private void DgPacking_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "jobMaintain")
            {
                string sTrackingID = e.Item.Cells[0].Text;
                string sJobID = e.Item.Cells[4].Text;

                DateTime dateFrom = DateTime.Parse(txtDateFrom.Text).AddMonths(-6);
                DateTime dateTo = DateTime.Parse(txtDateTo.Text).AddDays(1);


                DataTable dt = packBLL.GetProductDetailList(dateFrom, dateTo, "", "", "", sJobID, "");



                //有跨班多条记录的, 并且没有自跳转过的.
                if (dt.Rows.Count > 2 && Request.QueryString["jobNumber"] == null)
                {
                    Response.Redirect("./PQCPackingLiveReport.aspx?jobNumber=" + sJobID);
                }
                else
                {
                    Response.Redirect($"./PQCPackingMaintenance.aspx?trackingID={sTrackingID}&jobNo={sJobID}");
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
                

                DataTable dtPacking = packBLL.GetProductDetailList(DateFrom, DateTo, shift, partNumber, machineID, jobNumber, lotNo);


                if (dtPacking == null || dtPacking.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowWarning(this.lblResult, this.dgPacking, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {
                    Common.CommFunctions.HideWarning(this.lblResult, this.dgPacking);
                

                    this.dgPacking.DataSource = dtPacking.DefaultView;
                    this.dgPacking.DataBind();
                }

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgPacking, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
            }
        }

    }
}