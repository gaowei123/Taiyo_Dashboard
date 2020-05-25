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

             

                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    if (!string.IsNullOrEmpty(MachineID))
                    {
                        this.ddlMachineNo.SelectedValue = MachineID.Replace("Machine", "").Replace("No.", "").Trim();
                    }

                    string Shift = Request.QueryString["Shift"] == null ? "" : Request.QueryString["Shift"].ToString();
                    if (!string.IsNullOrEmpty(Shift))
                    {
                        this.ddlShift.SelectedValue = Shift;
                    }
                  
                    string sDateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string sDateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    DateTime dDateFrom = sDateFrom == "" ? DateTime.Now.Date : DateTime.Parse(sDateFrom);
                    DateTime dDateTo = sDateTo == "" ? DateTime.Now.Date : DateTime.Parse(sDateTo);
                    
                    this.txtDateFrom.Text = dDateFrom.ToString("yyyy-MM-dd"); 
                    this.txtDateTo.Text = dDateTo.ToString("yyyy-MM-dd");

                    

                    // 如果有jobnumber URL参数, 则放宽一年时间时间段, 查出所有job.
                    if (Request.QueryString["jobNumber"] != null)
                    {
                        this.txtDateFrom.Text = dDateFrom.AddYears(-1).ToString("yyyy-MM-dd");
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
            if (e.CommandName == "Link")
            {
                #region Link
                DataGridItem item = e.Item;
                string JobDayShift = item.Cells[1].Text;
                string MachineID = item.Cells[2].Text == "&nbsp;" ? "" : item.Cells[2].Text.Substring(7, 1);
                string JobNumber = item.Cells[25].Text;
                string NG = item.Cells[16].Text == "&nbsp;" ? "0" : item.Cells[16].Text.Split('(')[0];
                string DateTime = item.Cells[23].Text;
                string DateFrom = this.txtDateFrom.Text;
                string DateTo = this.txtDateTo.Text;
                
                if (NG == "0")
                    return;

                if (JobNumber=="" || item.Cells[1].Text == "Total :")
                    return;
                else
                {
                    string[] Temp = JobDayShift.Split('-');
                    string JobDay = Temp[0].Trim();
                    string JobShift = Temp[1].Trim();

                    string URL = "./ProductivityNGDetail.aspx?";
                    URL += "JobDay=" + JobDay;
                    URL += "&JobShift=" + JobShift;
                    URL += "&JobNumber=" + JobNumber;
                    URL += "&DateFrom=" + DateFrom;
                    URL += "&DateTo=" + DateTo;
                    URL += "&MachineID=" + MachineID;
                    URL += "&Shift=" + this.ddlShift.SelectedValue;


                    Response.Redirect(URL);
                }

                #endregion
            }
            else if (e.CommandName == "UpdateBom")
            {
                #region Update Bom

                DataGridItem item = e.Item;

                string Customer = item.Cells[3].Text == "&nbsp;" ? "" : item.Cells[3].Text;
                string Module = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;

                if (Customer =="" && Module =="")
                {
                    string MachineID = item.Cells[2].Text;
                    string PartNumber = item.Cells[6].Text;

                    Response.Redirect("./BomFormMenu.aspx?MachineID=" + MachineID + "&PartNumber=" + PartNumber + "&buttonType=LinkFromProductionDetail");
                }


                #endregion
            }
            else if (e.CommandName == "LaserJobMaintain")
            {
                #region laser job maintain
                DataGridItem item = e.Item;
                string JobDayShift = item.Cells[1].Text;
                string JobNumber = item.Cells[25].Text;
                string JobMachineID = item.Cells[2].Text.Replace("Machine", "");

                string[] Temp = JobDayShift.Split('-');
                string JobDay = Temp[0].Trim();
                string JobShift = Temp[1].Trim();



                // 判断是否是跨班job
                Common.BLL.LMMSWatchDog_His_BLL bll = new Common.BLL.LMMSWatchDog_His_BLL();
                DataTable dt = bll.GetList(JobNumber);
                if (dt == null || dt.Rows.Count ==0 )
                    return;


                //有跨班多条记录的, 并且没有自跳转过的.
                if (dt.Rows.Count >1 && Request.QueryString["jobNumber"] == null)
                {
                    Response.Redirect("./ProductivityDetail.aspx?jobNumber=" + JobNumber);
                }
                else
                {
                    string URL = string.Format("./LaserJobMaintance.aspx?jobNumber={0}&day={1}&shift={2}&machineID={3}",JobNumber,JobDay,JobShift,JobMachineID);
                    Response.Redirect(URL);
                }
                #endregion
            }
        }

     
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取并处理查询参数
                double performance = 0;
                double rejRate = 0;

                string sModel = this.txtModel.Text.Trim();
                string DateNotIn = "";
                string shift = ddlShift.SelectedValue;
                string sPartNo = txtPartNo.Text.Trim();

                string sMachineNo = ddlMachineNo.SelectedValue == "ALL" ? "" : ddlMachineNo.SelectedValue;
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).Date.AddHours(8);
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).Date.AddHours(8);
                string sJobnumber = this.txtJobNo.Text.Trim();

                string sDateNotIn_Confirmed = "";
                if (DateNotIn != "")
                {
                    string[] ArrDay = DateNotIn.Split(',');

                    for (int i = 0; i < ArrDay.Length; i++)
                    {
                        if (Common.CommFunctions.isNumberic( ArrDay[i]))
                        {
                            sDateNotIn_Confirmed += ArrDay[i] + ",";
                        }
                    }

                    sDateNotIn_Confirmed = sDateNotIn_Confirmed.Substring(0, sDateNotIn_Confirmed.Length - 1);
                }



                Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dt = WatchDogBll.getJobReportDetail(dTimeFrom, dTimeTo, sMachineNo, sPartNo, performance, rejRate, shift, sJobnumber, sModel,sDateNotIn_Confirmed);

           
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