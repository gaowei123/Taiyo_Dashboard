using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserEquipmentCheckForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    SetMachineDDL();

                    SetDoneByDDL();

                    this.txt_Date.Enabled = false;
                    this.txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                }
            }
            catch (Exception ee)
            {

            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                #region confirm value
                if (this.ddl_machineNo.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Machine ID!");
                    return;
                }

                if (!this.rbt_DetectOK_OK.Checked && !this.rbt_DetectOK_NG.Checked)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose detect OK sample!");
                    return;
                }
                if (!this.rbt_DetectNG_OK.Checked && !this.rbt_DetectNG_NG.Checked)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose detect NG sample!");
                    return;
                }
                if (!this.rbt_Green_OK.Checked && !this.rbt_Green_NG.Checked)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Running/Auto Green!");
                    return;
                }
                if (!this.rbt_Yellow_OK.Checked && !this.rbt_Yellow_NG.Checked)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Running/Auto Yellow!");
                    return;
                }
                if (!this.rbt_Red_OK.Checked && !this.rbt_Red_NG.Checked)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Fail/Fault Red!");
                    return;
                }

                if (this.txt_ProductBeforeBlowing.Text == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please keyin Production Before Blowing!");
                    return;
                }
                if (this.txt_ProductAfterBlowing.Text == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please keyin Production After Blowing!");
                    return;
                }

                if (this.ddl_DoneBy.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Done By!");
                    return;
                }
                #endregion

                Common.Class.Model.LMMSCheckList_Model model = new Common.Class.Model.LMMSCheckList_Model();
                
                model.machineID = this.ddl_machineNo.SelectedValue;

                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "dd/MM/yyyy";
                model.date = System.Convert.ToDateTime(this.txt_Date.Text, dtFormat);

                model.DetectOKSample = this.rbt_DetectOK_OK.Checked ? "OK" : "NG";
                model.DetectNGSample = this.rbt_DetectNG_OK.Checked ? "OK" : "NG";
                model.greenLight = this.rbt_Green_OK.Checked ? "OK" : "NG";
                model.yellowLight = this.rbt_Yellow_OK.Checked ? "OK" : "NG";
                model.redLight = this.rbt_Red_OK.Checked ? "OK" : "NG";
                model.productBeforeBlowing = decimal.Parse(this.txt_ProductBeforeBlowing.Text);
                model.productAfterBlowing = decimal.Parse(this.txt_ProductAfterBlowing.Text);
                model.filterBagReplace = this.cb_FilterBagReplace.Checked ? "YES" : "NO";
                model.doneBy = this.ddl_DoneBy.SelectedValue;
                model.updatedTime = DateTime.Now;

                Session["LMMSCheckList_Model"] = model;

                //check double add
                Common.Class.BLL.LMMSCheckList_BLL bll = new Common.Class.BLL.LMMSCheckList_BLL();
                bool result = bll.IsChecked(model.machineID, model.date);

                if (result)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This machine is already checked today!");
                    return;
                }
                else
                {
                    Response.Redirect("./Login.aspx?Department=" + StaticRes.Global.Department.Laser + "&commandType=Check", false);
                }
                
            }
            catch (Exception ee)
            {
                
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('Your action will not be saved, are you sure?') == true) { ";
            strJS += "window.location.href = \"./LaserEquipmentCheckList.aspx\";  } ";

            ClientScript.RegisterStartupScript(Page.GetType(), "", strJS, true);
        }


        private void SetMachineDDL()
        {
            this.ddl_machineNo.Items.Clear();

            ListItem li = new ListItem();
            li.Text = "";
            li.Value = "";
            ddl_machineNo.Items.Add(li);

            for (int i = 1; i < 9; i++)
            {
                li = new ListItem();
                li.Text = "No." + i.ToString();
                li.Value = i.ToString();
                ddl_machineNo.Items.Add(li);
            }
        }

        private void SetDoneByDDL()
        {
            this.ddl_DoneBy.Items.Clear();

            this.ddl_DoneBy.Items.Add(new ListItem("", ""));


            Common.Class.BLL.User_DB_BLL bll = new Common.Class.BLL.User_DB_BLL();
            List<string> usernameList = bll.GetUsernameList(StaticRes.Global.Department.Laser);
            foreach (string username in usernameList)
            {
                this.ddl_DoneBy.Items.Add(new ListItem(username, username));
            }
        }
        

    }
}