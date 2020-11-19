using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Painting
{
    public partial class PaintingDeliveryRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    
                    btn_generate_Click(new object(), new EventArgs());

                    #region result
                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result == "TRUE")
                    {
                        Common.CommFunctions.ShowMessage(Page, "Success");
                    }
                    else if (Result == "FALSE")
                    {
                        Common.CommFunctions.ShowMessage(Page, "Fail");
                    }
                    #endregion
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PaintingDeliveryRecord", "Page_Load Exception: " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_PaintingScanJobList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }

            Common.CommFunctions.SetAutoComplete(this.Page, "#MainContent_txtPartNo", "");
        }

        protected void btn_ScanJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("./InventoryRecord.aspx",false);
        }


        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string JobNumber = this.txtJobNo.Text.Trim();
                string PartNumber = this.txtPartNo.Text.Trim();
                string SendingTo = this.ddlSendingTo.SelectedValue;
                string LotNo = this.txtLotNo.Text.Trim();

              
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);

                Common.Class.BLL.PaintingDeliveryHis_BLL bll = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                DataTable dt = bll.GetList(dTimeFrom, dTimeTo, JobNumber, PartNumber, SendingTo, LotNo);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowWarning(lblResult, dg_PaintingScanJobList, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {
                    this.dg_PaintingScanJobList.DataSource = dt.DefaultView;
                    this.dg_PaintingScanJobList.DataBind();

                    Common.CommFunctions.HideWarning(lblResult, dg_PaintingScanJobList);
                }
            }
            catch (Exception ee)
            {
                Common.CommFunctions.ShowWarning(lblResult, dg_PaintingScanJobList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

      
    }
}