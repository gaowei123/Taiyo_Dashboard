using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingLabel : System.Web.UI.Page
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
                DBHelp.Reports.LogFile.Log("MouldingLabel", "Page_Load Exception : " + ee.ToString());
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
                string Shift = this.ddl_Shift.SelectedValue;
                Common.BLL.MouldingLabel_BLL bll = new Common.BLL.MouldingLabel_BLL();
                DataTable dt = bll.SelectList(DateFrom, DateTo, "", "", Shift, "");

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                    #region set columns's header text
                    if (Shift == StaticRes.Global.Shift.Night)
                    {
                        this.dg_CheckList.Columns[8].HeaderText  = "20:00";
                        this.dg_CheckList.Columns[9].HeaderText  = "21:00";
                        this.dg_CheckList.Columns[10].HeaderText = "22:00";
                        this.dg_CheckList.Columns[11].HeaderText = "23:00";
                        this.dg_CheckList.Columns[12].HeaderText = "00:00";
                        this.dg_CheckList.Columns[13].HeaderText = "01:00";
                        this.dg_CheckList.Columns[14].HeaderText = "02:00";
                        this.dg_CheckList.Columns[15].HeaderText = "03:00";
                        this.dg_CheckList.Columns[16].HeaderText = "04:00";
                        this.dg_CheckList.Columns[17].HeaderText = "05:00";
                        this.dg_CheckList.Columns[18].HeaderText = "06:00";
                        this.dg_CheckList.Columns[19].HeaderText = "07:00";

                        this.dg_CheckList.Columns[20].HeaderText = "20:00";
                        this.dg_CheckList.Columns[21].HeaderText = "21:00";
                        this.dg_CheckList.Columns[22].HeaderText = "22:00";
                        this.dg_CheckList.Columns[23].HeaderText = "23:00";
                        this.dg_CheckList.Columns[24].HeaderText = "00:00";
                        this.dg_CheckList.Columns[25].HeaderText = "01:00";
                        this.dg_CheckList.Columns[26].HeaderText = "02:00";
                        this.dg_CheckList.Columns[27].HeaderText = "03:00";
                        this.dg_CheckList.Columns[28].HeaderText = "04:00";
                        this.dg_CheckList.Columns[29].HeaderText = "05:00";
                        this.dg_CheckList.Columns[30].HeaderText = "06:00";
                        this.dg_CheckList.Columns[31].HeaderText = "07:00";

                        this.dg_CheckList.Columns[32].HeaderText = "20:00";
                        this.dg_CheckList.Columns[33].HeaderText = "21:00";
                        this.dg_CheckList.Columns[34].HeaderText = "22:00";
                        this.dg_CheckList.Columns[35].HeaderText = "23:00";
                        this.dg_CheckList.Columns[36].HeaderText = "00:00";
                        this.dg_CheckList.Columns[37].HeaderText = "01:00";
                        this.dg_CheckList.Columns[38].HeaderText = "02:00";
                        this.dg_CheckList.Columns[39].HeaderText = "03:00";
                        this.dg_CheckList.Columns[40].HeaderText = "04:00";
                        this.dg_CheckList.Columns[41].HeaderText = "05:00";
                        this.dg_CheckList.Columns[42].HeaderText = "06:00";
                        this.dg_CheckList.Columns[43].HeaderText = "07:00";


                    }
                    else
                    {
                        this.dg_CheckList.Columns[8].HeaderText  = "08:00";
                        this.dg_CheckList.Columns[9].HeaderText  = "09:00";
                        this.dg_CheckList.Columns[10].HeaderText = "10:00";
                        this.dg_CheckList.Columns[11].HeaderText = "11:00";
                        this.dg_CheckList.Columns[12].HeaderText = "12:00";
                        this.dg_CheckList.Columns[13].HeaderText = "13:00";
                        this.dg_CheckList.Columns[14].HeaderText = "14:00";
                        this.dg_CheckList.Columns[15].HeaderText = "15:00";
                        this.dg_CheckList.Columns[16].HeaderText = "16:00";
                        this.dg_CheckList.Columns[17].HeaderText = "17:00";
                        this.dg_CheckList.Columns[18].HeaderText = "18:00";
                        this.dg_CheckList.Columns[19].HeaderText = "19:00";


                        this.dg_CheckList.Columns[20].HeaderText = "08:00";
                        this.dg_CheckList.Columns[21].HeaderText = "09:00";
                        this.dg_CheckList.Columns[22].HeaderText = "10:00";
                        this.dg_CheckList.Columns[23].HeaderText = "11:00";
                        this.dg_CheckList.Columns[24].HeaderText = "12:00";
                        this.dg_CheckList.Columns[25].HeaderText = "13:00";
                        this.dg_CheckList.Columns[26].HeaderText = "14:00";
                        this.dg_CheckList.Columns[27].HeaderText = "15:00";
                        this.dg_CheckList.Columns[28].HeaderText = "16:00";
                        this.dg_CheckList.Columns[29].HeaderText = "17:00";
                        this.dg_CheckList.Columns[30].HeaderText = "18:00";
                        this.dg_CheckList.Columns[31].HeaderText = "19:00";


                        this.dg_CheckList.Columns[32].HeaderText = "08:00";
                        this.dg_CheckList.Columns[33].HeaderText = "09:00";
                        this.dg_CheckList.Columns[34].HeaderText = "10:00";
                        this.dg_CheckList.Columns[35].HeaderText = "11:00";
                        this.dg_CheckList.Columns[36].HeaderText = "12:00";
                        this.dg_CheckList.Columns[37].HeaderText = "13:00";
                        this.dg_CheckList.Columns[38].HeaderText = "14:00";
                        this.dg_CheckList.Columns[39].HeaderText = "15:00";
                        this.dg_CheckList.Columns[40].HeaderText = "16:00";
                        this.dg_CheckList.Columns[41].HeaderText = "17:00";
                        this.dg_CheckList.Columns[42].HeaderText = "18:00";
                        this.dg_CheckList.Columns[43].HeaderText = "19:00";

                    }
                    #endregion



                    //dataView.ToTable 第一个参数 bool distinct
                    DataView dataView =  dt.DefaultView;
                    DataTable dataTableDistinct = dataView.ToTable(true, "SerialNo", "Day", "Shift", "PartNumberAll", "UserName", "UsageQTYSum", "RejectQTYSum", "SerialNoSum", "UsageQTY08", "UsageQTY09", "UsageQTY10", "UsageQTY11", "UsageQTY12", "UsageQTY01", "UsageQTY02", "UsageQTY03", "UsageQTY04", "UsageQTY05", "UsageQTY06", "UsageQTY07", "RejectQTY08", "RejectQTY09", "RejectQTY10", "RejectQTY11", "RejectQTY12", "RejectQTY01", "RejectQTY02", "RejectQTY03", "RejectQTY04", "RejectQTY05", "RejectQTY06", "RejectQTY07",  "SerialNo08", "SerialNo09", "SerialNo10", "SerialNo11", "SerialNo12", "SerialNo01", "SerialNo02", "SerialNo03", "SerialNo04", "SerialNo05", "SerialNo06", "SerialNo07", "SerialNoEnd");
                    dataTableDistinct.Columns.Add("ID");
                    int id = 0;
                    foreach (DataRow dr in dataTableDistinct.Rows)
                    {
                        id++;
                        dr["ID"] = id;
                        //dr["UsageQTYSum"] = 0;
                        //dr["RejectQTYSum"] = 0;
                        //dr["SerialNoSum"] = 0;
                    }

                    this.dg_CheckList.Visible = true;
                    this.dg_CheckList.DataSource = dataTableDistinct.DefaultView;
                    this.dg_CheckList.DataBind();
                    HideWarning();
                }


                for (int i = 8; i < dg_CheckList.Columns.Count; i++)
                {
                    if (UserType == "Usage" && i >= 8 && i <= 19)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "Reject" && i >= 20 && i<32)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "SerialNo" && i >= 32 && i <= 45)
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else if (UserType == "")
                    {
                        this.dg_CheckList.Columns[i].Visible = true;
                    }
                    else
                    {
                        this.dg_CheckList.Columns[i].Visible = false;
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingLabel", "btn_generate_Click Exception : " + ee.ToString());
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