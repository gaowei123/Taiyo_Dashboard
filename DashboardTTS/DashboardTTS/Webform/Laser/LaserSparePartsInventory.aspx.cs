using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserSparePartsInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    this.lbCurQty.Visible = false;
                    this.lbSparePart.Visible = false;
                    this.txtQty.Visible = false;
                    this.btnUnload.Visible = false;
                    this.btnLoad.Visible = false;
                    this.lbCurQty_Text.Visible = false;


                    btn_Generate_Click(null, null);




                    string result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (result == "TRUE")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Success");
                    }
                    else if (result == "FALSE")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Fail");
                    }
                }

                this.dgSparePartsInventory.ItemCommand += DgSparePartsInventory_ItemCommand;
                this.dgAdd.ItemCommand += DgAdd_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsInventory", "Page_Load Exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgSparePartsInventory, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        private void DgAdd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
               

                string sPartName  = ((TextBox)e.Item.Cells[1].FindControl("txtPartsName")).Text.Trim();
                string sQty = ((TextBox)e.Item.Cells[2].FindControl("txtCurQty")).Text.Trim();

                if (sPartName == "" )
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in Part Name!");
                    ((TextBox)e.Item.Cells[1].FindControl("txtPartsName")).Focus();
                    return;
                }

                if (!Common.CommFunctions.isNumberic(sQty))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Must key in number!");
                    ((TextBox)e.Item.Cells[2].FindControl("txtCurQty")).Text = "";
                    ((TextBox)e.Item.Cells[2].FindControl("txtCurQty")).Focus();
                    return;
                }
                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                Common.Class.Model.LMMSSparePartsInventory_Model model = new Common.Class.Model.LMMSSparePartsInventory_Model();

                model.sparePartsName = sPartName;
                model.department = StaticRes.Global.Department.Laser;
                model.createdTime = DateTime.Now;
                model.lastUpdatedTime = DateTime.Now;
                model.quantity = int.Parse(sQty);

                model.action = "IN";
                model.usage = decimal.Parse(sQty);
                model.machineID = "";


                Session["LMMSSparePartsInventory_Model"] = model;
                Response.Redirect("./Login.aspx?commandType=AddSparePart&Department=" + StaticRes.Global.Department.Laser + "", false);
            }
        }

        private void DgSparePartsInventory_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string sPartsName = e.Item.Cells[0].Text;
                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                Common.Class.Model.LMMSSparePartsInventory_Model model = bll.GetModelByName(sPartsName);

                if (e.CommandName=="ShowAction")
                {
                    this.lbSparePart.Visible = true;
                    this.lbCurQty.Visible = true;
                    this.txtQty.Visible = true;
                    this.btnLoad.Visible = true;
                    this.btnUnload.Visible = true;
                    this.lbCurQty_Text.Visible = true;

                    this.lbSparePart.Text = sPartsName;
                    this.lbCurQty.Text = model.quantity.ToString();
                }
                else if (e.CommandName=="Delete")
                {
                    model.usage = model.quantity;
                    model.action = "Delete";
                    model.lastUpdatedTime = DateTime.Now;
                    Session["LMMSSparePartsInventory_Model"] = model;

                    Response.Redirect("./Login.aspx?commandType=DeleteSparePart&Department=" + StaticRes.Global.Department.Laser + "", false);
                }
              
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsInventory", "DgSparePartsInventory_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                string sPartsName = "";// this.txtPartName.Text.Trim();


                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                DataTable dt = bll.GetInventoryList(sPartsName);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.dgSparePartsInventory.Visible = false;
                    this.dgAdd.Visible = false;
                    this.lblResult.Text = "There is no record!";
                    this.lblResult.Visible = true;
                    this.lblResult.ForeColor = System.Drawing.Color.Red;
                }else
                {
                    this.dgSparePartsInventory.Visible = true;
                    this.dgAdd.Visible = true;
                    this.lblResult.Visible = false;

                    this.dgSparePartsInventory.DataSource = dt.DefaultView;
                    this.dgSparePartsInventory.DataBind();


                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("ID");
                    dtTemp.Rows.Add(dtTemp.NewRow());

                    this.dgAdd.DataSource = dtTemp.DefaultView;
                    this.dgAdd.DataBind();
                    this.dgAdd.ShowHeader = false;
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsInventory", "btn_Generate_Click Exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgSparePartsInventory, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Common.CommFunctions.isNumberic(this.txtQty.Text.Trim()))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Must Key in number!");
                    this.txtQty.Text = "";
                    this.txtQty.Focus();
                    return;
                }

                string sPartName = this.lbSparePart.Text;
                int iQuantity = int.Parse(this.txtQty.Text.Trim());


                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                Common.Class.Model.LMMSSparePartsInventory_Model model = bll.GetModelByName(sPartName);
                model.quantity += iQuantity;
                model.lastUpdatedTime = DateTime.Now;

                model.action = "IN";
                model.usage = iQuantity;
                model.machineID = "";
                

                Session["LMMSSparePartsInventory_Model"] = model;
                Response.Redirect("./Login.aspx?commandType=UpdateSparePart&Department=" + StaticRes.Global.Department.Laser + "", false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsInventory", "btnLoad_Click Exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgSparePartsInventory, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        protected void btnUnload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Common.CommFunctions.isNumberic(this.txtQty.Text.Trim()))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Must Key in number!");
                    this.txtQty.Text = "";
                    this.txtQty.Focus();
                    return;
                }


                string sPartName = this.lbSparePart.Text;
                int iQuantity = int.Parse(this.txtQty.Text.Trim());


                Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                Common.Class.Model.LMMSSparePartsInventory_Model model = bll.GetModelByName(sPartName);

                if (model.quantity - iQuantity < 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Inventory quantity is not enough!");
                    return;
                }


                model.quantity -= iQuantity;
                model.lastUpdatedTime = DateTime.Now;
                model.usage = iQuantity;
                model.action = "OUT";


                Session["LMMSSparePartsInventory_Model"] = model;
                Response.Redirect("./Login.aspx?commandType=UpdateSparePart&Department=" + StaticRes.Global.Department.Laser + "", false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserSparePartsInventory", "btnUnload_Click Exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgSparePartsInventory, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        
    }
}