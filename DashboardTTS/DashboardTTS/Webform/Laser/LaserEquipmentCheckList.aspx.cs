using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserEquipmentCheckList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                    this.txtDateFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");



                    Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                    Common.Class.Model.LMMSSparePartsInventory_Model partsModel = bll.getPartsModel("Filter Bag");
                    
        
                    SetMachineDDL();

                    btn_Generate_Click(new object(), new EventArgs());

                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserMachineDownTimeList", "Page_Load --" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_CheckList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }

        }

        protected void btn_Check_Click(object sender, EventArgs e)
        {
            Response.Redirect("./LaserEquipmentCheckForm.aspx");
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);
                string MahineID = this.ddlMachineNo.SelectedValue;


                Common.Class.BLL.LMMSCheckList_BLL bll = new Common.Class.BLL.LMMSCheckList_BLL();
                DataTable dt = bll.GetList(dTimeFrom, dTimeTo, MahineID);
                

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_CheckList.DataSource = dt.DefaultView;
                    this.dg_CheckList.DataBind();
                    HideWarning();
                }
            }
            catch (Exception ex)
            {
                ShowWarning(ex.ToString());
                DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
            }
        }


        private void SetMachineDDL()
        {
            this.ddlMachineNo.Items.Clear();

            ListItem li = new ListItem();
            li.Text = "All";
            li.Value = "";
            ddlMachineNo.Items.Add(li);

            for (int i = 1; i < 9; i++)
            {
                li = new ListItem();
                li.Text = "No." + i.ToString();
                li.Value = i.ToString();
                ddlMachineNo.Items.Add(li);
            }
        }
        private void ShowWarning(string message)
        {
            this.lblResult.Visible = true;
            this.dg_CheckList.Visible = false;
            this.lblResult.BackColor = System.Drawing.Color.Red;


            this.lblResult.Text = string.IsNullOrEmpty(message) ? "There is no record! " : message;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_CheckList.Visible = true;
        }
        
    }
}