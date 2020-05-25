using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Text.RegularExpressions;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MaterialBomDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                    if (CommandName == "Update")
                    {
                        #region init ui
                        Common.Model.Material_Inventory_Bom model = (Common.Model.Material_Inventory_Bom)Session["Material_Inventory_Bom"];

                        if (model == null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Operating time out, Please try again!'); window.location.href=\"./MaterialInventoryBom.aspx\"", true);
                            return;
                        }

                        this.txt_materialPart.Text = model.Material_Part;
                        this.txtUnitPrice_SGD.Text = model.Unit_Price == 0 ? "" : model.Unit_Price.ToString();
                        this.txtUnitPrice_USD.Text = model.Unit_Price_USD == 0 ? "" : model.Unit_Price_USD.ToString();
                        this.txt_ResinType.Text = model.REF_FIELD01 == "NA" ? "" : model.REF_FIELD01;
                        this.txt_materialPart.Enabled = false;
                        this.txt_remarks.Text = model.Remarks;

                        this.txtMakeUp.Text = model.MakeUp == 0 ? "" : model.MakeUp.ToString();
                        this.txtExchangeRate.Text = model.ExchangeRate == 0 ? "" : model.ExchangeRate.ToString();


                        #endregion
                    }
                    else
                    {
                        this.txt_materialPart.Enabled = true;
                        this.txt_materialPart.Focus();
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MaterialBomDetail", "Page_Load Exception" + ee.ToString());
            }
        }




        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {

                #region check value vaildation
                if (this.txt_materialPart.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Material Part!');", true);
                    this.txt_materialPart.Focus();
                    return;
                }

              
                if (this.txtUnitPrice_SGD.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Unit Price (SGD)!');", true);
                    this.txtUnitPrice_SGD.Focus();
                    return;
                }

                if (!Common.CommFunctions.isNumberic(this.txtUnitPrice_SGD.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Unit Price (SGD) must be number!');", true);
                    this.txtUnitPrice_SGD.Text = "";
                    this.txtUnitPrice_SGD.Focus();
                    return;
                }

                if (this.txt_ResinType.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Resin Type!');", true);
                    this.txt_ResinType.Focus();
                    return;
                }



                if (this.txtMakeUp.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input MakeUp!');", true);
                    this.txtMakeUp.Focus();
                    return;
                }
                if ( !Common.CommFunctions.isNumberic(this.txtMakeUp.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('MakeUp must be number!');", true);
                    this.txtMakeUp.Text = "";
                    this.txtMakeUp.Focus();
                    return;
                }


                if (this.txtExchangeRate.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Exchange Rate!');", true);
                    this.txtExchangeRate.Focus();
                    return;
                }
                if ( !Common.CommFunctions.isNumberic(this.txtExchangeRate.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Exchange Rate must be number!');", true);
                    this.txtExchangeRate.Text = "";
                    this.txtExchangeRate.Focus();
                    return;
                }



                //查重
                string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                if (CommandName == "Add")
                {
                    Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                    DataTable dt = bll.GetList(this.txt_materialPart.Text.Trim());
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        AlertMessage("This Material already exist, please confirm!");
                        this.txt_materialPart.Text = "";
                        this.txt_materialPart.Focus();
                        return;
                    }
                }
                #endregion


                #region set model to session
                Common.Model.Material_Inventory_Bom model = new Common.Model.Material_Inventory_Bom();
                
                model.Material_Part = txt_materialPart.Text.Trim();
                model.Unit_Price = decimal.Parse(txtUnitPrice_SGD.Text.Trim());
                
               
                model.REF_FIELD01 = txt_ResinType.Text;

                if (txtMakeUp.Text.Trim() != "")
                {
                    model.MakeUp = decimal.Parse(txtMakeUp.Text);
                }

                if (txtExchangeRate.Text.Trim() != "")
                {
                    model.ExchangeRate = decimal.Parse(txtExchangeRate.Text);
                }
                model.Unit_Price_USD = Math.Round((model.Unit_Price.Value + model.Unit_Price.Value * model.MakeUp.Value/100) * model.ExchangeRate.Value, 4);
                

                model.Remarks = txt_remarks.Text.Trim();
                model.Updated_User = "";
                model.Updated_Time = DateTime.Now;

                Session["MouldingMaterialBom_Model"] = model;

                #endregion

                
                string commandType = "";
                if (CommandName == "Add")
                    commandType = "Moulding_AddMaterialBom";
                else if(CommandName == "Update")
                    commandType =  "Moulding_UpdateMaterialBom";

                Response.Redirect("../Laser/Login.aspx?commandType="+ commandType + "&Department=" + StaticRes.Global.Department.Moulding, false);
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MaterialBomDetail", "btn_submit_Click Exception" + ee.ToString());
            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('You action will not be saved, are you sure?') == true)";
            strJS += " { ";
            strJS += "  window.location.href = \"./MaterialInventoryBom.aspx\"  }";


            ClientScript.RegisterStartupScript(this.GetType(), "", strJS, true);
        }



       



        void AlertMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + message + "');", true);
        }

    }
}