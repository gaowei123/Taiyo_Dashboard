using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DashboardTTS.Webform
{
    public partial class InventoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string Jobnumber = Request.QueryString["Jobnumber"] == null ? "" : Request.QueryString["Jobnumber"];
                    string Partnumber = Request.QueryString["Partnumber"] == null ? "" : Request.QueryString["Partnumber"];
                    string Status = Request.QueryString["Status"] == null ? "" : Request.QueryString["Status"];

                 

                    //跳转连接过来的默认开始时间为2018-4-14. 否则可能查找不到. 
                    if (Jobnumber != "" || Partnumber != "" || Status != "")
                    {
                        this.txtDateFrom.Text = "2018-8-14";
                    }
                    else
                    {
                        this.txtDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    }
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                  

                    this.txtJobNo.Text = Jobnumber;
                    this.txtPartNo.Text = Partnumber;
                    this.txtJobNo.Focus();

                   
                    btn_generate_Click(new object(), new EventArgs());
                }
                
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " Page_Load error -- " + ee.ToString());
                this.lblResult.Text = "No Record.";
                this.lblResult.Visible = true;
                this.lblResult.BackColor = System.Drawing.Color.Red;
            }

            this.dg_inventoryDetail.ItemCommand += Dg_inventoryDetail_ItemCommand;
        }

        private void Dg_inventoryDetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            if (e.CommandName == "UpdateQty")
            {
                string sJobNumber = item.Cells[1].Text;
                Response.Redirect("./LaserJobMaintance.aspx?jobNumber="+sJobNumber+"&Type=UpdateInventory");
            }
            else if (e.CommandName == "delete")
            {
                string sJobnumber = item.Cells[1].Text;


                //check job is run. 
                Common.Class.BLL.LMMSWatchDog_BLL bll = new Common.Class.BLL.LMMSWatchDog_BLL();
                if (bll.IsJobRunning(sJobnumber))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This job is running, please scan stop first!");
                    return;
                }


                Response.Redirect("./Login.aspx?commandType=deleteJob&Department=" + StaticRes.Global.Department.Laser+"&JobNumber="+sJobnumber);
            }
        }

      

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);
                string sJobNo = this.txtJobNo.Text;
                string sPartNo = this.txtPartNo.Text;
                string sStatus = this.ddlStatus.SelectedItem.Value;


                Common.Class.BLL.LMMSInventoty_BLL inventory_bll = new Common.Class.BLL.LMMSInventoty_BLL();
                DataTable dt = inventory_bll.DetailReport(sJobNo, sPartNo, dDateFrom, dDateTo, sStatus);

                if (dt == null ||dt.Rows.Count == 0)
                {
                    this.lblResult.Text = "No Record.";
                    this.lblResult.Visible = true;
                    this.lblResult.BackColor = System.Drawing.Color.Red;
                    this.dg_inventoryDetail.Visible = false;
                }
                else
                {
                    this.lblResult.Visible = false;
                    this.dg_inventoryDetail.Visible = true;
                    dg_inventoryDetail.DataSource = dt.DefaultView;
                    dg_inventoryDetail.DataBind();
                }


                if (ddlStatus.SelectedValue == "Complete")
                {
                    this.dg_inventoryDetail.Columns[12].Visible = false;
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " btn_generate_Click error -- " + ee.ToString());
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('There is the Exception happend when generatez " + ee.ToString() + "');", true);
            }
        }


       
      
    }
}