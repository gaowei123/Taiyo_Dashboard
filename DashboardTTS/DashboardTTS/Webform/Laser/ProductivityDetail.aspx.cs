using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;



namespace DashboardTTS.Webform
{
    public partial class ProductivityDetail : System.Web.UI.Page
    {
    
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                   
                    if (!string.IsNullOrEmpty(Request.QueryString["MachineID"]))
                    {
                        this.ddlMachineNo.SelectedValue = Request.QueryString["MachineID"].Replace("Machine", "").Replace("No.", "").Trim();
                    }

                
                    if (!string.IsNullOrEmpty(Request.QueryString["Shift"]))
                    {
                        this.ddlShift.SelectedValue = Request.QueryString["Shift"];
                    }
                  
                    string sDateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string sDateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    DateTime dDateFrom = sDateFrom == "" ? DateTime.Now.Date : DateTime.Parse(sDateFrom);
                    DateTime dDateTo = sDateTo == "" ? DateTime.Now.Date : DateTime.Parse(sDateTo);
                    
                    this.txtDateFrom.Text = dDateFrom.ToString("yyyy-MM-dd"); 
                    this.txtDateTo.Text = dDateTo.ToString("yyyy-MM-dd");

                    

                    //如果有jobnumber URL参数, 则放宽3年时间时间段, 查出所有job.
                    if (Request.QueryString["jobNumber"] != null)
                    {
                        this.txtDateFrom.Text = dDateFrom.AddYears(-3).ToString("yyyy-MM-dd");
                        this.txtJobNo.Text = Request.QueryString["jobNumber"].ToString();
                    }


                    btnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                    ShowWarning(ex.ToString());
                    DBHelp.Reports.LogFile.Log("ProductivityDetail", "Page_Load Exception : " + ex.ToString());
                }
            }
            
            dgJob.ItemCommand += DgJob_ItemCommand;

        }

        private void DgJob_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string jobNo = e.Item.Cells[0].Text;
            string day = e.Item.Cells[1].Text.Split('-')[0].Trim();
            string shift = e.Item.Cells[1].Text.Split('-')[1].Trim();
            string dateFrom = this.txtDateFrom.Text;
            string dateTo = this.txtDateTo.Text;
            string machineID = e.Item.Cells[2].Text.Replace("Machine", "");
            string ng = e.Item.Cells[11].Text == "&nbsp;" ? "0" : e.Item.Cells[11].Text.Split('(')[0];

            

            if (e.CommandName == "Link")
            {
                if (ng == "0"  || jobNo == "" || e.Item.Cells[1].Text == "Total")
                    return;
                               
                string URL = "./ProductivityNGDetail.aspx?";
                URL += "JobDay=" + day;
                URL += "&JobShift=" + shift;
                URL += "&JobNumber=" + jobNo;
                URL += "&DateFrom=" + dateFrom;
                URL += "&DateTo=" + dateTo;
                URL += "&MachineID=" + machineID;
                Response.Redirect(URL);
            }
            else if (e.CommandName == "LaserJobMaintain")
            {
                //有跨班多条记录的, 并且没有自跳转过的.
                Common.BLL.LMMSWatchDog_His_BLL bll = new Common.BLL.LMMSWatchDog_His_BLL();
                DataTable dt = bll.GetList(jobNo);
                if (dt == null || dt.Rows.Count ==0 )
                    return;
                
                if (dt.Rows.Count >1 && Request.QueryString["jobNumber"] == null)
                {
                    Response.Redirect("./ProductivityDetail.aspx?jobNumber=" + jobNo);
                }
                else
                {
                    string URL = string.Format("./LaserJobMaintance.aspx?jobNumber={0}&day={1}&shift={2}&machineID={3}", jobNo, day, shift, machineID);
                    Response.Redirect(URL);
                }
            }
        }

     
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //获取并处理查询参数
                DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).Date;
                dDateTo = dDateTo.AddDays(1);


                string sShift = ddlShift.SelectedValue;
                string sModel = this.txtModel.Text.Trim();
                string sPartNo = txtPartNo.Text.Trim();
                string sMachineID = this.ddlMachineNo.SelectedItem.Value;
                string sJobNo = this.txtJobNo.Text.Trim();




                Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dt = WatchDogBll.GetProductionDetailReport(dDateFrom, dDateTo, sShift, sModel, sPartNo, sMachineID, sJobNo);



           
                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dgJob.DataSource = dt.DefaultView;
                    this.dgJob.DataBind();
                    HideWarning();
                }
            }
            catch ( Exception ex)
            {
                ShowWarning(ex.ToString());
                DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
            }
        }





        //==========================func==========================//
        private void ShowWarning(string message)
        {
            this.lblResult.Visible = true;
            this.dgJob.Visible = false;
            this.lblResult.BackColor = System.Drawing.Color.Red;


            this.lblResult.Text = string.IsNullOrEmpty(message) ? "There is no record! " : message;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dgJob.Visible = true;
        }

        

    }
}