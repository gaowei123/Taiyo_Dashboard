using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMaintainence_His : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.AddDays(-15);
                    this.infDchFrom.Value = DateTime.Now.AddDays(-15);
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;

                    Common.Class.BLL.MouldingMaintain_Tracking_BLL bll = new Common.Class.BLL.MouldingMaintain_Tracking_BLL();
                    DataTable dt = bll.GetList();

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.btn_Verify.Visible = false;
                    }else
                    {
                        this.btn_Verify.Visible = true;
                    }

                    btn_generate_Click(new object(), new EventArgs());


                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result!= "" && Result=="TRUE")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Sucess');", true);
                    }
                    else if(Result != "" && Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Fail');", true);
                    }

                }


            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaintainence_His", "Page_Load Exception: " + ee.ToString());
            }
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime DateFrom = infDchFrom.CalendarLayout.SelectedDate.Date;
                DateTime DateTo = infDchTo.CalendarLayout.SelectedDate.Date;

                string MachineID = ddl_MachineID.SelectedValue;
                string CheckPeriod = ddl_Period.SelectedValue;

                Common.Class.BLL.MouldingMaintain_His_BLL bll = new Common.Class.BLL.MouldingMaintain_His_BLL();

                DataTable dt = bll.GetList(DateFrom,DateTo,MachineID,CheckPeriod);

                if (dt == null || dt.Rows.Count==0)
                {
                    ShowWarning("");
                }
                else
                {
                    HideWarning();
                    this.dg_Maint.Visible = true;
                    this.dg_Maint.DataSource = dt.DefaultView;
                    this.dg_Maint.DataBind();
                }

            }
            catch (Exception ee)
            {
                ShowWarning(ee.ToString());

                DBHelp.Reports.LogFile.Log("MouldingMaintainence_His", "btn_generate_Click Exception: " + ee.ToString());
            }
        }

       

        protected void btn_Maintain_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MouldingMaintainDetail.aspx",false);
        }

        void ShowWarning(string errorMessage)
        {
            if (errorMessage != "")
                this.lblResult.Text = errorMessage;
            else
                this.lblResult.Text = "There is no Record!";

            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
            this.dg_Maint.Visible = false;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no Record!";

            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
        }

        protected void btn_Verify_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Laser/Login.aspx?Department=Moulding&commandType=MaintainVerify", false);
        }
    }
}