using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MaterialInventoryBom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region  result
                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result != "")
                    {
                        if (bool.Parse(Result))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Success');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Fail');", true);
                        }
                    }
                    #endregion

                    this.txt_materialPart.Text = "";

                    btn_search_Click(new object(), new EventArgs());
                }

                this.dg_BOMList.ItemCommand += Dg_BOMList_ItemCommand;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "Page_Load Exception:" + ee.ToString());
                ShowWarning();
            }
        }

        private void Dg_BOMList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataGridItem item = e.Item;
                string MaterialPart = item.Cells[1].Text == "&nbsp;" ? "" : item.Cells[1].Text;

                if (e.CommandName == "Delete")
                {
                    string strUrl = "../Laser/Login.aspx?commandType=Moulding_DeleteMaterialBom&Material_Part=" + MaterialPart + "&Department=" + StaticRes.Global.Department.Moulding;
                    strUrl = strUrl.Replace("#", "%23");
                    Response.Redirect(strUrl, false);
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "ConfirmDelete('"+ ParNumberAll + "','Delete');", true);
                    return;
                }
                else if (e.CommandName == "Update")
                {
                    Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                    Common.Model.Material_Inventory_Bom model = bll.GetModel(MaterialPart);
                    Session["Material_Inventory_Bom"] = model;

                    Response.Redirect("./MaterialBomDetail.aspx?CommandName=Update", false);
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MaterialInventoryBom", "Dg_BOMList_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string materialPart = this.txt_materialPart.Text.Trim();
                Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();

                DataTable dt = bll.GetList(materialPart);


                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_BOMList.Visible = true;
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MaterialInventoryBom", "btn_search_Click Exception:" + ee.ToString());
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            string strURL = "./MaterialBomDetail.aspx?CommandName=Add";
            Response.Redirect(strURL, false);
        }

        void ShowWarning()
        {
            this.dg_BOMList.Visible = false;
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