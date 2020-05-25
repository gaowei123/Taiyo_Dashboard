using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMaterialInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region get url paras
                    string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                    string Material_Part = Request.QueryString["Material_Part"] == null ? "" : Request.QueryString["Material_Part"].ToString();
                    string Material_Batch = Request.QueryString["Material_Batch"] == null ? "" : Request.QueryString["Material_Batch"].ToString();
                    string InventoryWeight = Request.QueryString["Weight"] == null ? "" : Request.QueryString["Weight"].ToString();
                    string Customer = Request.QueryString["Customer"] == null ? "" : Request.QueryString["Customer"].ToString();
                    if (CommandName == "Update")
                    {
                        #region init ui
                        this.txt_Material_Part.Text = Material_Part;
                        this.txt_InventoryWeight.Text = InventoryWeight;
                        this.txt_Material_Batch.Text = Material_Batch;
                        this.txt_Customer.Text = Customer;
                        this.ddl_machineID.Enabled = true;
                        this.txt_Weight.Focus();
                        #endregion
                    }
                    else if (CommandName == "Add")
                    {
                        #region  init ui
                        this.ddl_machineID.Enabled = false;
                        #endregion
                    }
                    else if(CommandName == "Unload")
                    {
                        #region  init ui
                        this.ddl_machineID.Enabled = true;
                        #endregion
                    }
                    else
                    {
                        #region  init ui
                        this.ddl_machineID.Enabled = false;
                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialInventory", "Page_Load Exception" + ee.ToString());
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
                //check textbox


                ////查重
                string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                //if (CommandName == "Add")
                //{
                //    Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                //    DataTable dt = bll.GetList();
                //    if (dt != null && dt.Rows.Count != 0)
                //    {
                //        AlertMessage("This part already exist, please confirm!");
                //        //this.txt_PartNumberAll.Text = "";
                //        //this.txt_part.Focus();
                //        return;
                //    }
                //}
                ////查重


                Common.Class.Model.Material_Inventory model = new Common.Class.Model.Material_Inventory();
                #region model

                model.Material_No = this.txt_Material_Part.Text.Trim();                
                model.Inventory_Weight = decimal.Parse(this.txt_Weight.Text.Trim());
                model.Supplier = this.txt_Customer.Text.Trim();
                DateTime dtime = DateTime.Now;
                #endregion




                if (CommandName == "Add")
                {
                    model.Material_LotNo = this.txt_Material_Batch.Text.Trim();
                    model.Load_Time = dtime;
                    model.Last_Event = "Load";
                    model.Updated_Time = dtime;
                    Session["Material_Inventory"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=AddMaterialInventory&Department=Moulding", false);
                }
                else if (CommandName == "Update")
                {
                    //string Weight = Request.QueryString["Weight"] == null ? "" : Request.QueryString["Weight"].ToString();
                    if(ddl_machineID.Text.Length==0)
                    {
                        Response.Write("<script>window.alert(\"Please select machine ID !\")</script>");
                        return;
                    }
                    if (txt_Weight.Text.Length == 0)
                    {
                        Response.Write("<script>window.alert(\"Please input Unload Weight !\")</script>");
                        return;
                    }
                    try
                    {
                        if (float.Parse(txt_Weight.Text) <= 0)
                        {
                            Response.Write("<script>window.alert(\"Unload Weight can not <=0 !\")</script>");
                            return;
                        }
                        if(float.Parse(txt_Weight.Text)>float.Parse(txt_InventoryWeight.Text))
                        {
                            Response.Write("<script>window.alert(\"Unload Weight can not bigger than Inventory Weight !\")</script>");
                            return;
                        }
                    }
                    catch
                    {
                        Response.Write("<script>window.alert(\"Invalid Unload Weight !\")</script>");
                        return;
                    }
                    model.Remarks = txt_InventoryWeight.Text;
                    model.Material_LotNo = this.txt_Material_Batch.Text.Trim();
                    string Load_Time = Request.QueryString["Load_Time"] == null ? "" : Request.QueryString["Load_Time"].ToString();
                    model.Load_Time = DateTime.Parse(Load_Time);
                    model.Last_Event = "Unload";
                    model.Updated_Time = dtime;
                    model.MachineID = ddl_machineID.Text;
                    Session["Material_Inventory"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=UpdateMaterialInventory&Department=Moulding", false);
                }
                else if (CommandName == "Return")
                {
                    model.Material_LotNo = this.txt_Material_Batch.Text.Trim();
                    model.Last_Event = "Return";
                    model.Load_Time = dtime;
                    model.Updated_Time = dtime;
                    Session["Material_Inventory"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=ReturnMaterialInventory&Department=Moulding", false);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialInventory", "btn_submit_Click Exception" + ee.ToString());
            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MouldingMaterialInventory.aspx", false);
        }

        protected void btn_addPart_Click(object sender, EventArgs e)
        {

        }

        private bool TextBoxCheckList()
        {
            if (this.txt_Material_Part.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input PartNumber!');", true);
                this.txt_Material_Batch.Focus();
                return false;
            }
            if (this.txt_Material_Batch.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Model!');", true);
                this.txt_Weight.Focus();
                return false;
            }
            if (this.txt_Weight.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Model!');", true);
                return false;
            }
            //if (this.txt_Customer.Text.Trim() == "")
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input Customer!');", true);
            //    return false;
            //}
            return true;
        }

        void AlertMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + message + "');", true);
        }
    }
    
}