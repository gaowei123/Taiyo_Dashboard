using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingProductionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //init page
                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    string PartNumber = Request.QueryString["PartNumber"] == null ? "" : Request.QueryString["PartNumber"].ToString();
                    string Module = Request.QueryString["Module"] == null ? "" : Request.QueryString["Module"].ToString();
                    DateTime DateFrom = Request.QueryString["DateFrom"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    DateTime DateTo = Request.QueryString["DateTo"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateTo"].ToString());


                    if (MachineID != "")
                    {
                        this.ddlMachineID.Items.FindByValue(MachineID).Selected = true;
                    }
                    this.ddlPartNo.SelectedValue = "6910-6894 T2";
                    this.txt_module.Text = Module != "" ? Module : "";

                    this.infDchFrom.Value = DateFrom;
                    infDchFrom.CalendarLayout.SelectedDate = DateFrom;
                    this.infDchTo.Value = DateTo;
                    infDchTo.CalendarLayout.SelectedDate = DateTo;


                    HideWarning();

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
              
            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("QAProductionReport", "Page_Load Exception :" + ee.ToString());
            }
        }





        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                this.MachineNo.Text = "Machine No";
                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                DateTime dTimeFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dTimeTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNo = this.ddlPartNo.Text;
                //string Shift = this.ddl_Shift.SelectedValue;
                string Module = this.txt_module.Text;

                bool bOnlyMqcProd = this.ckbMQCProduct.Checked;

                DataTable dt = bll.ProductionReport_withMQC(dTimeFrom, dTimeTo, MachineID, PartNo, Module, bOnlyMqcProd);

                if (dt == null || dt.Rows.Count <= 0)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_Report.Visible = true;
                    this.dg_Report.DataSource = dt.DefaultView;
                    this.dg_Report.DataBind();
                    HideWarning();
                }

            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("ProductionReport", "btn_generate_Click Exception:" + ee.ToString());
            }
        }




        void ShowWarning()
        {
            this.lblResult.Text = "There is no record";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
            this.dg_Report.Visible = false;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
        }

        protected void btn_Detail_Click(object sender, EventArgs e)
        {
            try
            {
                this.MachineNo.Text = "Computer No";
                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                DateTime dTimeFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dTimeTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNo = this.ddlPartNo.Text;
                //string Shift = this.ddl_Shift.SelectedValue;
                string Module = this.txt_module.Text;
                string CheckerID = this.ddlChkerID.Text;
                bool bOnlyMqcProd = this.ckbMQCProduct.Checked;

                DataTable dt = bll.ProductionReport_withMQCDetail(dTimeFrom, dTimeTo, MachineID, PartNo, Module, bOnlyMqcProd,CheckerID);

                if (dt == null || dt.Rows.Count <= 0)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_Report.Visible = true;
                    this.dg_Report.DataSource = dt.DefaultView;
                    this.dg_Report.DataBind();
                    HideWarning();
                }

            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("ProductionReport", "btn_generate_Click Exception:" + ee.ToString());
            }
        }
    }
}