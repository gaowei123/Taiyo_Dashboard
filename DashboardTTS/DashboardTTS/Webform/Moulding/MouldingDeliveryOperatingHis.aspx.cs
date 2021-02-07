using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingDeliveryOperatingHis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;
                    

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
                DBHelp.Reports.LogFile.Log("MouldingDeliveryOperatingHis", "Page_Load Exception: " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_PaintingScanJobList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        protected void btn_ScanJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MouldingInventoryTransfer.aspx", false);
        }


        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string JobNumber = this.txt_JobNo.Text.Trim();
                string PartNumber = this.txt_PartNo.Text.Trim();
                string SendingTo = "";
                string LotNo = this.txt_Lotno.Text.Trim();

                
                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1).AddHours(8);



                Common.Class.BLL.MouldingDeliveryHis_BLL bll = new Common.Class.BLL.MouldingDeliveryHis_BLL();
                DataTable dt = bll.GetList(DateFrom, DateTo, JobNumber, PartNumber, SendingTo, LotNo);

                if (dt == null || dt.Rows.Count <= 1)
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
                DBHelp.Reports.LogFile.Log("MouldingDeliveryOperatingHis", "btn_generate_Click Exception: " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_PaintingScanJobList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }


     

    }
}