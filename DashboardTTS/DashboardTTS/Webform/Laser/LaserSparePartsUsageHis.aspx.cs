using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserSparePartsUsageHis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.txtDateFrom.Text = DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    
                    SetSparePartsDDL();

                    btn_Generate_Click(new object(), new EventArgs());

                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsUsageHis", "Page_Load Exception" + ee.ToString());
            }
        }


        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);
                string sMachineID = "";
                string sAction = ddlAction.SelectedValue;
                string sSparePart= this.ddlPartName.SelectedValue;


                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                DataTable dt = bll.GetHistory(sMachineID,sSparePart,sAction, dDateFrom, dDateTo);


              

                Display(dt);
            }
            catch (Exception ex)
            {
                ShowWarning(ex.ToString());
                DBHelp.Reports.LogFile.Log("LaserSparePartsUsageHis", "btnGenerate_Click Exception : " + ex.ToString());
            }
        }
        
        void Display(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowWarning("");
                return;
            }
            else
            {
                this.dg_CheckList.DataSource = dt.DefaultView;
                this.dg_CheckList.DataBind();
                HideWarning();
            }

            string sPart = this.ddlPartName.SelectedValue;

            if (sPart == "")
            {
                this.dg_CheckList.Items[this.dg_CheckList.Items.Count - 1].Visible = false;
            }else
            {
                this.dg_CheckList.Items[this.dg_CheckList.Items.Count - 1].Visible = true;
                this.dg_CheckList.Items[this.dg_CheckList.Items.Count - 1].BackColor = System.Drawing.Color.Beige;

                string sAction = this.ddlAction.SelectedValue;
                if (sAction == "")
                {
                    this.dg_CheckList.Items[this.dg_CheckList.Items.Count - 1].Cells[4].Text = "";
                }
            }
        }

        private void SetSparePartsDDL()
        {
            this.ddlPartName.Items.Clear();
            ddlPartName.Items.Add(new ListItem("All",""));

            Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
            DataTable dt = bll.GetPartNameList();

            foreach (DataRow dr in dt.Rows)
            {

                string sSparePart = dr["sparePartsName"].ToString();
                ddlPartName.Items.Add(new ListItem(sSparePart, sSparePart));
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