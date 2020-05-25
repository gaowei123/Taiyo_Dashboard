using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform
{
    public partial class ProductivityNGDetail : System.Web.UI.Page
    {
    

     
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //Get URL Parameters
                    string JobShift = Request.QueryString["JobShift"] == null ? "" : Request.QueryString["JobShift"];
                    string JobNumber = Request.QueryString["JobNumber"] == null ? "" : Request.QueryString["JobNumber"];
                    string DateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string DateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    string shift = Request.QueryString["Shift"] == null ? "" : Request.QueryString["Shift"].ToString();

                    //Init search condition
                    this.txtJobNo.Text = JobNumber;

                    if (DateFrom != "" && DateTo != "")
                    {
                        this.txtDateFrom.Text = DateTime.Parse(DateFrom).Date.ToString("yyyy-MM-dd");
                        this.txtDateTo.Text = DateTime.Parse(DateTo).Date.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        this.txtDateFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    }
                   
                    if (shift != "")
                    {
                        this.ddlShift.SelectedValue = shift;
                    }
                    
                    btnGenerate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductivityNGDetail", "Page_Load Exception: " + ee.ToString());
            }
        }

        
       

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                #region Get param
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).AddHours(8);
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).AddHours(8);

                string sMachineID = ddlMachineNo.SelectedValue;
                string sPartNo = txtPartNo.Text;
                string sJobNumber = txtJobNo.Text;
                string sShift = Request.QueryString["JobShift"] == null ? ddlShift.SelectedValue : Request.QueryString["JobShift"];
                string JobDay = Request.QueryString["JobDay"] == null ? "" : Request.QueryString["JobDay"];
                string sModule = "";
                #endregion

                Common.BLL.LMMSWatchLog_BLL WatchLogBll = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dt = WatchLogBll.GetMaterialList(JobDay, sShift, dTimeFrom, dTimeTo, sJobNumber, sMachineID, sPartNo, sModule);
                

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.lblResult.Text = "There is no record!";
                    this.lblResult.BackColor = System.Drawing.Color.Red;
                    this.lblResult.Visible = true;
                    this.dg_NG.Visible = false;
                }
                else
                {
                    this.dg_NG.Visible = true;
                    this.lblResult.Visible = false;
                    this.dg_NG.DataSource = dt.DefaultView;
                    this.dg_NG.DataBind();
                }
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductivityNGDetail", "Page_Load Exception: " + ee.ToString());
            }
        }

        
        
    }
}