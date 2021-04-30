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

                    string sJobNo = Request.QueryString["JobNo"] == null ? "" : Request.QueryString["JobNo"].ToString();
                    string sTrackingID = Request.QueryString["TrackingID"] == null ? "" : Request.QueryString["TrackingID"].ToString();
                    string sDateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string sDateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    

                    this.txtDateFrom.Text = !string.IsNullOrEmpty(sDateFrom) ? sDateFrom :  DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = !string.IsNullOrEmpty(sDateTo) ? sDateTo : DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtJobNo.Text = sJobNo;

                    

                    btnGenerate_Click(null,null);
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

                Common.ExtendClass.PQCProduction.Core.Base_BLL _bll = new Common.ExtendClass.PQCProduction.Core.Base_BLL();
                var packList = _bll.GetPackingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
                {
                    JobNo = sJobID
                });


                //有跨班多条记录的, 并且没有自跳转过的.
                if (packList.Count >= 2 && Request.QueryString["JobNo"] == null)
                {
                    string sDateFrom = packList.Min(p => p.Day).ToString("yyyy-MM-dd");
                    string sDateTo = packList.Max(p => p.Day).ToString("yyyy-MM-dd");
                    Response.Redirect($"./PQCPackingLiveReport.aspx?JobNo={sJobID}&DateFrom={sDateFrom}&DateTo={sDateTo}");
                }
                else
                {
                    Response.Redirect($"./PQCPackingMaintenance.aspx?TrackingID={sTrackingID}&JobNo={sJobID}");
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