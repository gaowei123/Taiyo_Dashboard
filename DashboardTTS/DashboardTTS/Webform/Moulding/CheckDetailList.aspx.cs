using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Moulding
{
    public partial class CheckDetailList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchFrom.Value = DateTime.Now;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now;
                    this.infDchTo.Value = DateTime.Now;



                    btn_generate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("CheckDetailList", "Page_Load Exception : " + ee.ToString());
                ShowWarning();
            }
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate;
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate;
                DateFrom = DateFrom.Date.AddHours(8);
                DateTo = DateTo.AddHours(8).Date.AddHours(8);
                string UserType = this.ddl_UserType.SelectedValue;
                string MachineID = this.ddlMachineID.SelectedValue;
                string Shift = this.ddl_Shift.SelectedValue;
                string PartNo = this.txt_PartNo.Text.Trim();



                #region set header text
                if (Shift == StaticRes.Global.Shift.Night)
                {
                    this.dg_CheckList.Columns[16].HeaderText = "20:00";
                    this.dg_CheckList.Columns[17].HeaderText = "21:00";
                    this.dg_CheckList.Columns[18].HeaderText = "22:00";
                    this.dg_CheckList.Columns[19].HeaderText = "23:00";
                    this.dg_CheckList.Columns[20].HeaderText = "00:00";
                    this.dg_CheckList.Columns[21].HeaderText = "01:00";
                    this.dg_CheckList.Columns[22].HeaderText = "02:00";
                    this.dg_CheckList.Columns[23].HeaderText = "03:00";
                    this.dg_CheckList.Columns[24].HeaderText = "04:00";
                    this.dg_CheckList.Columns[25].HeaderText = "05:00";
                    this.dg_CheckList.Columns[26].HeaderText = "06:00";
                    this.dg_CheckList.Columns[27].HeaderText = "07:00";


                    this.dg_CheckList.Columns[28].HeaderText = "20:00";
                    this.dg_CheckList.Columns[29].HeaderText = "21:00";
                    this.dg_CheckList.Columns[30].HeaderText = "22:00";
                    this.dg_CheckList.Columns[31].HeaderText = "23:00";
                    this.dg_CheckList.Columns[32].HeaderText = "00:00";
                    this.dg_CheckList.Columns[33].HeaderText = "01:00";
                    this.dg_CheckList.Columns[34].HeaderText = "02:00";
                    this.dg_CheckList.Columns[35].HeaderText = "03:00";
                    this.dg_CheckList.Columns[36].HeaderText = "04:00";
                    this.dg_CheckList.Columns[37].HeaderText = "05:00";
                    this.dg_CheckList.Columns[38].HeaderText = "06:00";
                    this.dg_CheckList.Columns[39].HeaderText = "07:00";

                }
                else
                {
                    this.dg_CheckList.Columns[16].HeaderText = "08:00";
                    this.dg_CheckList.Columns[17].HeaderText = "09:00";
                    this.dg_CheckList.Columns[18].HeaderText = "10:00";
                    this.dg_CheckList.Columns[19].HeaderText = "11:00";
                    this.dg_CheckList.Columns[20].HeaderText = "12:00";
                    this.dg_CheckList.Columns[21].HeaderText = "13:00";
                    this.dg_CheckList.Columns[22].HeaderText = "14:00";
                    this.dg_CheckList.Columns[23].HeaderText = "15:00";
                    this.dg_CheckList.Columns[24].HeaderText = "16:00";
                    this.dg_CheckList.Columns[25].HeaderText = "17:00";
                    this.dg_CheckList.Columns[26].HeaderText = "18:00";
                    this.dg_CheckList.Columns[27].HeaderText = "19:00";


                    this.dg_CheckList.Columns[28].HeaderText = "08:00";
                    this.dg_CheckList.Columns[29].HeaderText = "09:00";
                    this.dg_CheckList.Columns[30].HeaderText = "10:00";
                    this.dg_CheckList.Columns[31].HeaderText = "11:00";
                    this.dg_CheckList.Columns[32].HeaderText = "12:00";
                    this.dg_CheckList.Columns[33].HeaderText = "13:00";
                    this.dg_CheckList.Columns[34].HeaderText = "14:00";
                    this.dg_CheckList.Columns[35].HeaderText = "15:00";
                    this.dg_CheckList.Columns[36].HeaderText = "16:00";
                    this.dg_CheckList.Columns[37].HeaderText = "17:00";
                    this.dg_CheckList.Columns[38].HeaderText = "18:00";
                    this.dg_CheckList.Columns[39].HeaderText = "19:00";
                }
                #endregion
                

                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dt = bll.SelectHourlyCheck(DateFrom, DateTo, MachineID, PartNo, Shift, "");

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_CheckList.Visible = true;
                    this.dg_CheckList.DataSource = dt.DefaultView;
                    this.dg_CheckList.DataBind();
                    HideWarning();
                }

                #region visable
                for (int i = 6; i < dg_CheckList.Columns.Count; i++)
                {
                    if (UserType == "OP" && i >= 16 && i <= 27)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "IPQC" && i >= 28)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "Other" && i >=6 && i<=15)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "")
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }else
                    {
                        this.dg_CheckList.Columns[i].Visible = false;
                    }
                }
                #endregion

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("CheckDetailList", "btn_generate_Click Exception : " + ee.ToString());
                ShowWarning();
            }
        }

        void ShowWarning()
        {
            this.dg_CheckList.Visible = false;
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
        }
    }
}