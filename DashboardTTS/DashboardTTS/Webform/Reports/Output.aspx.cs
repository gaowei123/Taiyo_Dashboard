using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Reports
{
    public partial class Output : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.lb_Header.Text = "Output Report";
                    DataTable dt = new DataTable();
                   
                    DateTime dDay = DateTime.Now.Date.AddDays(-1); 
                    this.infDchFrom.Value = dDay;
                    infDchFrom.CalendarLayout.SelectedDate = dDay;

                    getReport(dDay);

                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result != "" && Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Update Fail!');", true);
                    }

                }

                //this.dg_Moulding.ItemCommand += Dg_Machine_ItemCommand;
                //this.dg_RobotArm.ItemCommand += Dg_RobotArm_ItemCommand;
                //this.dg_Dryer.ItemCommand += Dg_Dryer_ItemCommand;
                //this.dg_Temp.ItemCommand += Dg_Temp_ItemCommand;
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("Debug", "PageLoad Exception: " + ex.ToString());
            }
        }

        public void getReport(DateTime dDay)
        {

            try
            {

                /// Columns: Module -- MachineCount -- OK -- NG -- Output -- Target 
                Common.BLL.LMMSWatchLog_BLL bllLaser = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dtLaserDayShift = bllLaser.getOutput(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.Laser);
                DataTable dtLaserNightShift = bllLaser.getOutput(dDay, StaticRes.Global.Shift.Night, StaticRes.Global.Department.Laser);
                Common.Class.BLL.MouldingViHistory_BLL bllMoulding = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dtMouldingDayShift = bllMoulding.getOutput(dDay, StaticRes.Global.Shift.Day);
                DataTable dtMouldingNightShift = bllMoulding.getOutput(dDay, StaticRes.Global.Shift.Night);
                //Common.Class.BLL.QA_BLL bllMouldingQA = new Common.Class.BLL.QA_BLL();
                //DataTable dtMouldingQA = bllMouldingQA.getOutput(dDay);

                //Common.Class.BLL.PQC_BLL bllPQC = new Common.Class.BLL.PQC_BLL();
                //DataTable dtPQC = bllPQC.getOutput(dDay);

                //Common.Class.BLL.Painting_BLL bllPainting = new Common.Class.BLL.Painting_BLL();
                //DataTable dtPainting = bllPainting.getOutput(dDay);

                //Common.Class.BLL.Assy_BLL bllAssy = new Common.Class.BLL.Assy_BLL();
                //DataTable dtAssy = bllAssy.getOutput(dDay);
                setOPstatus();

                this.dg_Laser_Day.DataSource = dtLaserDayShift.DefaultView;
                this.dg_Laser_Day.DataBind();
                this.dg_Laser_Night.DataSource = dtLaserNightShift.DefaultView;
                this.dg_Laser_Night.DataBind();
                this.dg_Moulding_Day.DataSource = dtMouldingDayShift.DefaultView;
                this.dg_Moulding_Day.DataBind();
                this.dg_Moulding_Night.DataSource = dtMouldingNightShift.DefaultView;
                this.dg_Moulding_Night.DataBind();

                //this.dg_Moulding_QA.DataSource = dtMouldingQA.DefaultView;
                //this.dg_Moulding_QA.DataBind();
                //this.dg_PQC.DataSource = dtPQC.DefaultView;
                //this.dg_PQC.DataBind(); 
                //this.dg_Painting.DataSource = dtPainting.DefaultView;
                //this.dg_Painting.DataBind();
                //this.dg_Assy.DataSource = dtAssy.DefaultView;
                //this.dg_Assy.DataBind();

            }

            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("Debug", "getReport Exception: " + ex.ToString());


            }
        }
        public void setOPstatus( )
        {

            try
            {
                dg_Laser_Day.Columns[0].FooterText = "Man Power: 11";
                dg_Laser_Day.Columns[1].FooterText = "Sup,Te,Leader: 3";
                dg_Laser_Day.Columns[2].FooterText = "Operator: 8";
                dg_Laser_Day.Columns[3].FooterText = "Attendance: 8";
                dg_Laser_Day.Columns[4].FooterText = "Annual Leave: 1";
                dg_Laser_Day.Columns[5].FooterText = "MC Leave: 1";
                dg_Laser_Day.Columns[6].FooterText = "Absent: 1";


                dg_Laser_Night.Columns[0].FooterText = "Man Power: 6";
                dg_Laser_Night.Columns[1].FooterText = "Sup,Te,Leader: 1";
                dg_Laser_Night.Columns[2].FooterText = "Operator: 5";
                dg_Laser_Night.Columns[3].FooterText = "Attendance: 6";
                dg_Laser_Night.Columns[4].FooterText = "Annual Leave: 1";
                dg_Laser_Night.Columns[5].FooterText = "MC Leave: 0";
                dg_Laser_Night.Columns[6].FooterText = "Absent: 0";


                dg_Moulding_Day.Columns[0].FooterText = "Man Power: 11";
                dg_Moulding_Day.Columns[1].FooterText = "Sup,Te,Leader: 3";
                dg_Moulding_Day.Columns[2].FooterText = "Operator: 8";
                dg_Moulding_Day.Columns[3].FooterText = "Attendance: 8";
                dg_Moulding_Day.Columns[4].FooterText = "Annual Leave: 1";
                dg_Moulding_Day.Columns[5].FooterText = "MC Leave: 1";
                dg_Moulding_Day.Columns[6].FooterText = "Absent: 1";

                dg_Moulding_Night.Columns[0].FooterText = "Man Power: 6";
                dg_Moulding_Night.Columns[1].FooterText = "Sup,Te,Leader: 1";
                dg_Moulding_Night.Columns[2].FooterText = "Operator: 5";
                dg_Moulding_Night.Columns[3].FooterText = "Attendance: 6";
                dg_Moulding_Night.Columns[4].FooterText = "Annual Leave: 1";
                dg_Moulding_Night.Columns[5].FooterText = "MC Leave: 0";
                dg_Moulding_Night.Columns[6].FooterText = "Absent: 0";


            }
            catch (Exception ex)
            {

            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime dDay =    infDchFrom.CalendarLayout.SelectedDate ;
            getReport(dDay);
        }
    }
}