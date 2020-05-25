using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Reports
{
    public partial class MouldingDailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.lb_Header.Text = "Daily Report";
                    DateTime dDay = DateTime.Now.Date.AddDays(-1); 
                    this.infDchFrom.Value = dDay;
                    this.infDchFrom.CalendarLayout.SelectedDate = dDay;
                    

                    btnGenerate_Click(new object(), new EventArgs());


                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result != "" && Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Update Fail!');", true);
                    }
                }

                this.dg_Moulding_Day.ItemCommand += Dg_Moulding_Day_ItemCommand;
                this.dg_Moulding_Night.ItemCommand += Dg_Moulding_Night_ItemCommand;
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("RejectDailyReport", "Page_Load Exception: " + ex.ToString());
            }
        }

        private void Dg_Moulding_Night_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateTime day = this.infDchFrom.CalendarLayout.SelectedDate.Date;
            string shift = "Night";
            string machineID = e.Item.Cells[0].Text;


            Response.Redirect(string.Format("../Moulding/MouldingStopReasonDetailList.aspx?Day={0}&Shift={1}&MachineID={2}", day, shift,machineID));
        }

        private void Dg_Moulding_Day_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateTime day = this.infDchFrom.CalendarLayout.SelectedDate.Date;
            string shift = "Day";
            string machineID = e.Item.Cells[0].Text;

            Response.Redirect(string.Format("../Moulding/MouldingStopReasonDetailList.aspx?Day={0}&Shift={1}&MachineID={2}", day, shift, machineID));
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dDay = infDchFrom.CalendarLayout.SelectedDate;

                Common.Class.BLL.MouldingViHistory_BLL bllMoulding = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dtMouldingDayShift = bllMoulding.getDailyReject(dDay, StaticRes.Global.Shift.Day);
                DataTable dtMouldingNightShift = bllMoulding.getDailyReject(dDay, StaticRes.Global.Shift.Night);

                DataTable dtMouldingTotal = bllMoulding.getTotalReject(dtMouldingDayShift, dtMouldingNightShift);


                this.dg_Moulding_Day.DataSource = dtMouldingDayShift.DefaultView;
                this.dg_Moulding_Day.DataBind();
                this.dg_Moulding_Night.DataSource = dtMouldingNightShift.DefaultView;
                this.dg_Moulding_Night.DataBind();
                this.dg_Moulding_Total.DataSource = dtMouldingTotal.DefaultView;
                this.dg_Moulding_Total.DataBind();


                SetDataGridStyle(dg_Moulding_Day, dg_Moulding_Night);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("RejectDailyReport", "getReport Exception: " + ex.ToString());
            }
        }

        protected void btnDayCheck_Click(object sender, EventArgs e)
        {
            Common.Class.Model.MouldingCheckReport_Model model = new Common.Class.Model.MouldingCheckReport_Model();
            model.Report = "Day";
            model.refField01 = infDchFrom.CalendarLayout.SelectedDate;
            Session["MouldingCheckReport_Model"] = model;
            Response.Redirect("../Laser/Login.aspx?commandType=CheckDailyReport&Department=" + StaticRes.Global.Department.Moulding, false);
        }

        protected void btnNightCheck_Click(object sender, EventArgs e)
        {
            Common.Class.Model.MouldingCheckReport_Model model = new Common.Class.Model.MouldingCheckReport_Model();
            model.Report = "Night";
            model.refField01 = infDchFrom.CalendarLayout.SelectedDate;
            Session["MouldingCheckReport_Model"] = model;
            Response.Redirect("../Laser/Login.aspx?commandType=CheckDailyReport&Department=" + StaticRes.Global.Department.Moulding, false);
        }

        

        void SetDataGridStyle(DataGrid dgDay, DataGrid dgNight)
        {
            int columnStart = 10;
            int columnEnd = 33;
            int columnCount = dgDay.Columns.Count;

            int dayDgItemCount = dgDay.Items.Count;
            int nightDgItemCount = dgNight.Items.Count;


            for (int i = columnStart; i <= columnEnd; i++)
            {
                int dgDayDefectValue = int.Parse(dgDay.Items[dayDgItemCount -1].Cells[i].Text);
                int dgNightDefectValue = int.Parse(dgNight.Items[nightDgItemCount -1].Cells[i].Text);

                if (dgDayDefectValue == 0 && dgNightDefectValue == 0)
                {
                    dgDay.Columns[i].Visible = false;
                    dgNight.Columns[i].Visible = false;
                }
                else
                {
                    dgDay.Columns[i].Visible = true;
                    dgNight.Columns[i].Visible = true;
                }
            }



            int dgDayRowCount = dgDay.Items.Count;
            if (dgDay.Items[dgDayRowCount - 1].Cells[39].Text == "Not Verify Yet")
                dgDay.Items[dgDayRowCount - 1].Cells[39].ForeColor = System.Drawing.Color.Red;

            int dgNightRowCount = dgNight.Items.Count;
            if (dgNight.Items[dgNightRowCount - 1].Cells[39].Text == "Not Verify Yet")
                dgNight.Items[dgNightRowCount - 1].Cells[39].ForeColor = System.Drawing.Color.Red;

        }

    }
}