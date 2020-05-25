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
    public partial class BomDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    

                    SetTypeDDL();
                    SetSuppillerDDL();



                    string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                    if (CommandName == "Update")
                    {
                        #region init ui


                        Common.Class.Model.MouldingBom_Model model = (Common.Class.Model.MouldingBom_Model)Session["MouldingBom_Model"];
                        if (model == null)
                        {
                            Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Get Bom Detail Fail, Please try again!", "./BomList.aspx");
                            return;
                        }

                        this.txt_PartNumberAll.Text = model.partNumberAll;
                        this.txtMaterial01.Text = model.matPart01;
                        this.txtMaterial02.Text = model.matPart02;

                        this.txtMaterial01Weight.Text = model.materialWeight01.ToString();
                        this.txtMaterial02Weight.Text = model.materialWeight02.ToString();


                        this.txt_customer.Text = model.customer;
                        this.txt_model.Text = model.model;
                        this.txt_jigNo.Text = model.jigNo;
                        this.txt_cavityCount.Text = model.cavityCount.ToString();
                        //this.txt_partsWeight.Text = PartsWeight;
                        this.txt_cycleTime.Text = model.cycleTime.ToString() ;
                        this.txt_unit.Text = model.unitCount.ToString() ;
                        this.txt_part.Enabled = false;
                        this.btn_addPart.Enabled = false;
                        this.ddlType.Text = model.machineID;
                        this.ddlSuppiller.Text = model.suppiller;
                        this.txt_remarks.Text = model.remarks;

                        

                        //获取 material bom中设定的 unit cost
                        Common.Class.BLL.Material_Inventory_Bom_BLL materialBLL = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                        Common.Model.Material_Inventory_Bom materialModel01 = materialBLL.GetModel(txtMaterial01.Text);
                        Common.Model.Material_Inventory_Bom materialModel02 = materialBLL.GetModel(txtMaterial02.Text);
                        
                        //1st & 2nd material cost  
                        decimal m1Cost = materialModel01 == null ? 0 : materialModel01.Unit_Price.Value / 1000 * (decimal)model.materialWeight01;
                        decimal m2Cost = materialModel02 == null ? 0 : materialModel02.Unit_Price.Value / 1000 * (decimal)model.materialWeight02;

                        this.txt_unit.Text = Math.Round(m1Cost + m2Cost, 4).ToString();

                      


                        this.Btn_edit.Visible = true;
                        #endregion
                    }
                    else if (CommandName == "Add")
                    {
                        #region  init ui
                        this.txt_PartNumberAll.Enabled = false;
                        this.Btn_edit.Visible = false;

                        #endregion
                    }
                    else
                    {
                        #region  init ui
                        this.txt_PartNumberAll.Enabled = false;

                        #endregion
                    }

                    
                }

              
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomDetail", "Page_Load Exception" + ee.ToString());
            }
        }


        

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {

                //check textbox
                bool result = TextBoxCheckList();
                if (!result)
                {
                    return;
                }
            


                //查重
                string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                if (CommandName == "Add")
                {
                    Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                    DataTable dt = bll.GetList(this.txt_PartNumberAll.Text.Trim());
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        AlertMessage("This part already exist, please confirm!");
                        this.txt_PartNumberAll.Text = "";
                        this.txt_part.Focus();
                        return;
                    }
                }
               

                Common.Class.Model.MouldingBom_Model model = new Common.Class.Model.MouldingBom_Model();
                model.partNumberAll = this.txt_PartNumberAll.Text.Trim();
                model.model = this.txt_model.Text.Trim();
                model.jigNo = this.txt_jigNo.Text.Trim();
                model.matPart01 = this.txtMaterial01.Text.Trim();
                model.matPart02 = this.txtMaterial02.Text.Trim();
                model.materialWeight01 = double.Parse(txtMaterial01Weight.Text);
                model.materialWeight02 = txtMaterial02Weight.Text == ""?0: double.Parse(txtMaterial02Weight.Text);//material2 没有防空判断.
                model.machineID = this.ddlType.Text.Trim();
                model.suppiller = this.ddlSuppiller.Text.Trim();
                model.cavityCount = double.Parse(this.txt_cavityCount.Text.Trim());
                model.cycleTime = double.Parse(this.txt_cycleTime.Text.Trim());
                model.customer = this.txt_customer.Text.Trim();
                model.remarks = this.txt_remarks.Text.Trim();
                model.dateTime = DateTime.Now;



                //获取 material bom中设定的 unit cost
                Common.Class.BLL.Material_Inventory_Bom_BLL materialBLL = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                Common.Model.Material_Inventory_Bom materialModel01 = materialBLL.GetModel(txtMaterial01.Text);
                Common.Model.Material_Inventory_Bom materialModel02 = materialBLL.GetModel(txtMaterial02.Text);

                //1st & 2nd material cost  
                decimal m1Cost = materialModel01 == null ? 0 : materialModel01.Unit_Price.Value / 1000 * (decimal)model.materialWeight01;
                decimal m2Cost = materialModel02 == null ? 0 : materialModel02.Unit_Price.Value / 1000 * (decimal)model.materialWeight02;
                
                model.unitCount = m1Cost + m2Cost;



                if (CommandName == "Add")
                {
                    Session["MouldingBom_Model"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=Moulding_AddBom&Department="+StaticRes.Global.Department.Moulding, false);
                }
                else if (CommandName == "Update")
                {
                    if (this.txt_newPartAllNumber.Text.Length > 0)
                    {
                        model.refField05 = this.txt_newPartAllNumber.Text.Trim();
                        Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                        DataTable dt = bll.GetList(model.refField05);
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            AlertMessage("This part already exist, please confirm!");
                            this.txt_newPartAllNumber.Text = "";
                            this.txt_newPartNumber.Focus();
                            return;
                        }
                    }
                    else
                    {
                        model.refField05 = "";
                    }
                    Session["MouldingBom_Model"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=Moulding_UpdateBom&Department="+ StaticRes.Global.Department.Moulding, false);
                }
                

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomDetail", "btn_submit_Click Exception" + ee.ToString());
            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./BomList.aspx", false);
        }


  

        protected void btn_addPart_Click(object sender, EventArgs e)
        {
            if (this.txt_part.Text.Trim() != "")
            {
                if (this.txt_PartNumberAll.Text.Trim() != "" && !Regex.IsMatch(this.txt_PartNumberAll.Text.Trim(), txt_part.Text.Trim()))
                {
                    this.txt_PartNumberAll.Text += ("," + txt_part.Text.Trim());
                }
                else
                {
                    this.txt_PartNumberAll.Text = this.txt_part.Text.Trim();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input PartNumber!');", true);
            }

            this.txt_part.Text = "";
        }



        private bool TextBoxCheckList()
        {
         
            if (this.txt_PartNumberAll.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input PartNumber!');", true);
                this.txt_part.Focus();
                return false;
            }

            if (this.txt_model.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Model!');", true);
                this.txt_model.Focus();
                return false;
            }

            //至少有1种材料. 有的不需要第二种材料 txtMaterial02不需要判断
            if (this.txtMaterial01.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input MatPart01!');", true);
                this.txtMaterial01.Focus();
                return false;
            }
            
            if (this.txt_customer.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Customer!');", true);
                this.txt_customer.Focus();
                return false;
            }



            if (this.txt_cavityCount.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cavity Count can not be empty !');", true);
                this.txt_cavityCount.Focus();
                return false;
            }
            else if (!Common.CommFunctions.isNumberic(this.txt_cavityCount.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cavity Count must be number !');", true);
                this.txt_cavityCount.Text = "";
                this.txt_cavityCount.Focus();
                return false;
            }
        

            if (this.txt_cycleTime.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cycle Time can not be empty !');", true);
                this.txt_cycleTime.Focus();
                return false;
            }
            else if (!Common.CommFunctions.isNumberic(this.txt_cycleTime.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cycle Time must be number !');", true);
                this.txt_cycleTime.Text = "";
                this.txt_cycleTime.Focus();
                return false;
            }

            if (this.txtMaterial01Weight.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Material Weight Time can not be empty !');", true);
                this.txtMaterial01Weight.Focus();
                return false;
            }
            else if (!Common.CommFunctions.isNumberic(this.txtMaterial01Weight.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Material Weight must be number !');", true);
                this.txtMaterial01Weight.Text = "";
                this.txtMaterial01Weight.Focus();
                return false;
            }

            //material 2  填了重量的情况下. 判断下是不是数字.
            if (this.txtMaterial02Weight.Text.Trim() != "" && (!Common.CommFunctions.isNumberic(this.txtMaterial02Weight.Text.Trim())))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Material Weight must be number !');", true);
                this.txtMaterial02Weight.Text = "";
                this.txtMaterial02Weight.Focus();
                return false;
            }


         

            return true;
        }



        void AlertMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + message + "');", true);
        }


        private void SetTypeDDL()
        {
            ddlType.Items.Clear();
            
            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Button";
            Li.Value = "Button";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Panel";
            Li.Value = "Panel";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Len";
            Li.Value = "Len";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Knob";
            Li.Value = "Knob";
            this.ddlType.Items.Add(Li);

        }


        private void SetSuppillerDDL()
        {
            ddlSuppiller.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Store";
            Li.Value = "Store";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Silkprint";
            Li.Value = "Silkprint";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Summer Weli";
            Li.Value = "Summer Weli";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "QA";
            Li.Value = "QA";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Checker";
            Li.Value = "Checker";
            this.ddlSuppiller.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Paint";
            Li.Value = "Paint";
            this.ddlSuppiller.Items.Add(Li);
        }


        protected void btn_edit_Click(object sender, EventArgs e)
        {
            this.test.Visible = true;
        }

        protected void btn_addNewPart_Click(object sender, EventArgs e)
        {
            if (this.txt_newPartNumber.Text.Trim() != "")
            {
                if (this.txt_newPartAllNumber.Text.Trim() != "" && !Regex.IsMatch(this.txt_newPartAllNumber.Text.Trim(), txt_newPartNumber.Text.Trim()))
                {
                    this.txt_newPartAllNumber.Text += ("," + txt_newPartNumber.Text.Trim());
                }
                else
                {
                    this.txt_newPartAllNumber.Text = this.txt_newPartNumber.Text.Trim();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input PartNumber!');", true);
            }

            this.txt_newPartNumber.Text = "";
        }
    }
}