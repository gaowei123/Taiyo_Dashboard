using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace DashboardTTS.Webform.Moulding
{
    public partial class RejDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region init UI
                    //URl paras
                    string DateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string DateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    string PartNumber = Request.QueryString["PartNumber"] == null ? "" : Request.QueryString["PartNumber"].ToString();
                    string Model = Request.QueryString["Model"] == null ? "" : Request.QueryString["Model"].ToString();
                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    MachineID = MachineID.Replace("Machine", "");

                    this.ddlPartNo.Text = PartNumber == "" ? "" : PartNumber;
                    this.txt_module.Text = Model == "" ? "" : Model;
                    this.infDchFrom.Value = DateFrom == "" ? DateTime.Now.Date : DateTime.Parse(DateFrom).Date;
                    this.infDchFrom.CalendarLayout.SelectedDate = DateFrom == "" ? DateTime.Now.Date : DateTime.Parse(DateFrom).Date;
                    this.infDchTo.Value = DateTo == "" ? DateTime.Now.Date : DateTime.Parse(DateTo).Date;
                    this.infDchTo.CalendarLayout.SelectedDate = DateTo == "" ? DateTime.Now.Date : DateTime.Parse(DateTo).Date;


                    if (MachineID != "")
                    {
                        this.ddlMachineID.Items.FindByValue(MachineID).Selected = true;
                    }
                    SetTypeDDL();
                    SetShiftDDL();
                    SetPartNoDDL();



                    //ddl rej type
                    Common.Class.BLL.MouldingDefectSetting_BLL bll = new Common.Class.BLL.MouldingDefectSetting_BLL();
                    List<string> ListRejType = bll.RejTypeList();
                    ddl_RejType.DataSource = ListRejType;
                    ddl_RejType.DataBind();

                    #endregion

                    btn_generate_Click(new object(), new EventArgs());
                }

                this.dg_RejDetail.ItemCommand += Dg_RejDetail_ItemCommand;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("RejDetail", "Page_Load exception:" + ee.ToString());
                ShowWarning();
            }
        }
        private void Dg_RejDetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataGridItem item = e.Item;
                string Model = item.Cells[6].Text;
                string PartNumberAll = item.Cells[8].Text;
                
                if (e.CommandName == "LinkPartNumberAll")
                {
                    this.ddlPartNo.Text = PartNumberAll;

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
                else if (e.CommandName == "LinkModel")
                {
                    this.txt_module.Text = Model;

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("RejDetail", "Dg_RejDetail_ItemCommand Exception:" + ee.ToString());
            }
        }
        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                DateTime DateFrom = infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime DateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddHours(8);
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNumber = this.ddlPartNo.Text;
                string Model = this.txt_module.Text;
                string RejType = this.ddl_RejType.SelectedValue;

                string Shift = this.ddl_Shift.SelectedValue;
                string Type = this.ddl_Type.SelectedValue;

                Common.Class.BLL.MouldingViDefectTracking_BLL bll = new Common.Class.BLL.MouldingViDefectTracking_BLL();
                dt = bll.Getlist(DateFrom, DateTo, MachineID, PartNumber, RejType, Model, Shift, Type);

                if (dt == null || dt.Rows.Count <=1)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_RejDetail.Visible = true;
                    this.dg_RejDetail.DataSource = dt.DefaultView;
                    this.dg_RejDetail.DataBind();

                    HideWarning();
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("RejDetail", "btn_generate_Click exception:" + ee.ToString());
                ShowWarning();
            }
        }

        void ShowWarning()
        {
            this.dg_RejDetail.Visible = false;
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

        private void SetTypeDDL()
        {
            ddl_Type.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_Type.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Button";
            Li.Value = "Button";
            this.ddl_Type.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Panel";
            Li.Value = "Panel";
            this.ddl_Type.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Len";
            Li.Value = "Len";
            this.ddl_Type.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Knob";
            Li.Value = "Knob";
            this.ddl_Type.Items.Add(Li);

        }

        private void SetShiftDDL()
        {
            ddl_Shift.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_Shift.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Day";
            Li.Value = "Day";
            this.ddl_Shift.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Night";
            Li.Value = "Night";
            this.ddl_Shift.Items.Add(Li);

        }

        private void SetPartNoDDL()
        {
            ddlPartNo.Items.Clear();

            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
            List<string> ListRejType = bll.GetPartNoList();
            ddlPartNo.DataSource = ListRejType;
            ddlPartNo.DataBind();

        }
    }
}