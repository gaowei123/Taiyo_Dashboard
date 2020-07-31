using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace DashboardTTS.Webform
{
    public partial class BOMList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    SetMachineDDL();

                    this.txtPartNo.Focus();

                    btn_search_Click(null, null);


                    #region Result

                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result =="TRUE")
                        Common.CommFunctions.ShowMessage(this.Page, "Success");
                    else if (Result == "FALSE")
                        Common.CommFunctions.ShowMessage(this.Page, "Fail");

                    #endregion

                }


                this.dg_BOMList.ItemCommand += Dg_BOMList_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserBOMList", "BOMList Page Page_Load --" + ee.ToString());
                ShowWarning(ee.ToString());
            }
        }

        

        private void Dg_BOMList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        #region Delete

                        DataGridItem item = e.Item;
                        string PartNo = item.Cells[4].Text;
                        string MachineID = item.Cells[7].Text;
                        string commandType = "Delete";

                        string strUrl = "./Login.aspx?partNumber=" + PartNo + "&machineID=" + MachineID + "&commandType=" + commandType + "&Department=" + StaticRes.Global.Department.Laser;

                        Response.Redirect(strUrl, false);
                        #endregion
                        break;
                    case "UpdatePart":
                        #region Update
                        string partNumber = e.Item.Cells[4].Text == "&nbsp;" ? "" : e.Item.Cells[4].Text;
                        string machineID = e.Item.Cells[7].Text == "&nbsp;" ? "" : e.Item.Cells[7].Text;

                        Common.Class.BLL.LMMSBom_BLL bll = new Common.Class.BLL.LMMSBom_BLL();
                        Common.Class.Model.LMMSBom_Model model = bll.GetBomModel(partNumber, machineID);
                        
                        Session["LMMSBom_Model"] = model;
                       

                        Response.Redirect("./BomFormMenu.aspx?buttonType=UpdateBom", false);
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "Dg_BOMList_ItemCommand error--" + ee.Message);
                Common.CommFunctions.ShowMessage(this.Page, "Delete Exception:"+ ee.ToString());
            }
        }

      
        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string PartNo = this.txtPartNo.Text.Trim();
                string MachineID = this.ddlMachineNo.SelectedValue;

                Common.Class.BLL.LMMSBom_BLL LMMSBomBll = new Common.Class.BLL.LMMSBom_BLL();
                DataTable dt = LMMSBomBll.GetList(PartNo, MachineID, true);

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserBOMList", "btn_search_Click exception: " + ee.ToString());
                ShowWarning(ee.ToString());
            }
        }
      

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Webform/Laser/BomFormMenu.aspx?buttonType=AddBom", false);

            Response.Redirect("./BomFormMenu.aspx?buttonType=AddBom",false);
        }




        
     

        private void ShowWarning(string sMessage)
        {
            this.lblResult.Visible = true;
            this.dg_BOMList.Visible = false;
            this.lblResult.BackColor = System.Drawing.Color.Red;

            this.lblResult.Text = string.IsNullOrEmpty(sMessage) ? "There is no record! " : sMessage;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_BOMList.Visible = true;
        }


        private void SetMachineDDL()
        {
            this.ddlMachineNo.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlMachineNo.Items.Add(Li);


            for (int i = 1; i < 9; i++)
            {
                Li = new ListItem();
                Li.Text = "No."+i.ToString();
                Li.Value = i.ToString();
                this.ddlMachineNo.Items.Add(Li);
            }

        }


    }
}