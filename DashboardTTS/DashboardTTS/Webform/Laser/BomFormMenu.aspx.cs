using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DashboardTTS.Webform
{
    public partial class BomFormMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
         {
            try
            {
                if (!IsPostBack)
                {

                    SetMachineDDL();

                    SetTypeDDL();

                    string buttonType = Request.QueryString["buttonType"] == null ? "" : Request.QueryString["buttonType"].ToString();

                    if (buttonType == "UpdateBom")
                    {
                        #region Update

                        Common.Class.Model.LMMSBom_Model model = (Common.Class.Model.LMMSBom_Model)Session["LMMSBom_Model"];
                        if (model == null)
                        {
                            string Message = "BOM Model Empty!, Update Error, Please try again!";
                            string URL = "./BOMList.aspx";
                            Common.CommFunctions.ShowMessageAndRedirect(this.Page, Message, URL);
                            return;
                        }

                        SetBomUI(model);

                        this.txt_partNo.Enabled = false;
                        this.ddl_machineNo.Enabled = false;

                

                        //set Bom detail table
                        Common.Class.BLL.LMMSBomDetail_BLL BomDetail_bll = new Common.Class.BLL.LMMSBomDetail_BLL();
                        DataTable dt_bomDetail = BomDetail_bll.GetBomDetailListByPartNumber(txt_partNo.Text);

                        if (dt_bomDetail == null)
                        {
                            dt_bomDetail = new DataTable();
                            dt_bomDetail.Columns.Add("sn");
                            dt_bomDetail.Columns.Add("partNumber");
                            dt_bomDetail.Columns.Add("materialPartNo");
                            dt_bomDetail.Columns.Add("partCount");
                            dt_bomDetail.Columns.Add("userName");
                            dt_bomDetail.Columns.Add("dateTime");
                        }

                        List_Refresh(dt_bomDetail);
                        Session["dt_bomDetail"] = dt_bomDetail;
                        
                    
                        #endregion
                    }
                    else if (buttonType == "AddBom")
                    {
                        #region Add
                        this.txt_partNo.Focus();
                        this.txt_materialPart.Enabled = false;
                        this.txt_partCount.Enabled = false;
                        this.btn_add.Enabled = false;
                        this.txt_sn.Enabled = false;


                        DataTable dt_bomDetail = new DataTable();
                        dt_bomDetail.Columns.Add("sn");
                        dt_bomDetail.Columns.Add("partNumber");
                        dt_bomDetail.Columns.Add("materialPartNo");
                        dt_bomDetail.Columns.Add("partCount");
                        dt_bomDetail.Columns.Add("userName");
                        dt_bomDetail.Columns.Add("dateTime");

                        Session["dt_bomDetail"] = dt_bomDetail;

                        #endregion
                    }
                    else if (buttonType == "LinkFromProductionDetail")
                    {
                        string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                        string PartNumber = Request.QueryString["PartNumber"] == null ? "" : Request.QueryString["PartNumber"].ToString();

                        this.txt_partNo.Text = PartNumber;
                        this.ddl_machineNo.SelectedValue = MachineID.Replace("Machine", "");

                        txt_partNo_TextChanged(new object(), new EventArgs());
                    }

                }

                this.dg_MaterialPart.ItemCommand += Dg_MaterialPart_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception","BomFormMenu Load --"+ee.ToString());
            }
        }


        private void Dg_MaterialPart_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataGridItem item = e.Item;

                if (e.CommandName == "Delete")
                {
                    //reset textbox
                    string sn = item.Cells[0].Text == "&nbsp;" ? "" : item.Cells[0].Text;
                    string MaterialPart = item.Cells[2].Text;
                    string partCount = item.Cells[3].Text;
                  
                    this.txt_materialPart.Text = MaterialPart; 
                    this.txt_partCount.Text = partCount;
                    this.txt_sn.Text = sn;

                    
                    //remove from datatable
                    DataTable dt_bomDetail = (DataTable)Session["dt_bomDetail"];
                    if (dt_bomDetail != null)
                    {
                        int rowNo = int.Parse(((Button)item.FindControl("btn_Delete")).Attributes["Index"]);
                        dt_bomDetail.Rows.RemoveAt(rowNo);
                    }

                    List_Refresh(dt_bomDetail);
                    Session["dt_bomDetail"] = dt_bomDetail;
                }
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "Dg_BOMList_ItemCommand error--" + ee.Message);
                Common.CommFunctions.ShowMessage(this.Page, "Delete failed");
            }
        }


        protected void txt_partNo_TextChanged(object sender, EventArgs e)
        {
            string PartNo = this.txt_partNo.Text.Trim();

            Common.Class.BLL.LMMSBom_BLL Bombll = new Common.Class.BLL.LMMSBom_BLL();
            DataTable dt = Bombll.GetList(PartNo, "");

            if (dt.Rows.Count > 0)
            {
                SetBomUI(dt.Rows[0]);

                this.ddl_machineNo.Focus();


                //Set Bom Detail List
                Common.Class.BLL.LMMSBomDetail_BLL bll = new Common.Class.BLL.LMMSBomDetail_BLL();
                DataTable dt_bomDetail = bll.GetBomDetailListByPartNumber(PartNo);

                List_Refresh(dt_bomDetail);
                Session["dt_bomDetail"] = dt_bomDetail;
            }

            this.txt_sn.Enabled = true;
            this.txt_materialPart.Enabled = true;
            this.txt_partCount.Enabled = true;
            this.btn_add.Enabled = true;

            this.ddl_machineNo.Focus();
        }
        

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                #region check text
                if (txt_partNo.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Part No!');", true);
                    return;
                }

                if (ddl_machineNo.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose Machine No!');", true);
                    return;
                }

                if (txt_blockCount.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Block Count!');", true);
                    return;
                }
                else if (!Common.CommFunctions.isNumberic(txt_blockCount.Text))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input number in Block Count');", true);
                    txt_blockCount.Text = "";
                    return;
                }

                if (txt_unitCount.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Unit Count!');", true);
                    return;
                }
                else if (!Common.CommFunctions.isNumberic(txt_unitCount.Text))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input number in Unit Count');", true);
                    txt_unitCount.Text = "";
                    return;
                }

                if (txt_cycleTime.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Cycle Time!');", true);
                    return;
                }
                else if (!Common.CommFunctions.isNumberic(txt_cycleTime.Text))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input number in Cycle Time!');", true);
                    txt_cycleTime.Text = "";
                    return;
                }
                
                if (ddl_machineNo.SelectedItem.Value == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose Machine No!');", true);
                    return;
                }

                if (txt_module.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input module!');", true);
                    return;
                }

                if (ddl_type.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose type!');", true);
                    return;
                }

                if (txt_power.Text != "")
                {
                    if (!Common.CommFunctions.isNumberic(txt_power.Text))
                    {
                        this.txt_power.Text = "";
                        this.txt_power.Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "Must input number in Current Power !");
                        return;
                    }
                }

              


                //check double add       ( PartNo & MachineID )
                string commandType = Request.QueryString["buttonType"] == null ? "" : Request.QueryString["buttonType"].ToString();

                if (commandType == "AddBom")
                {
                    Common.Class.BLL.LMMSBom_BLL bll = new Common.Class.BLL.LMMSBom_BLL();

                    if (bll.IsExist(this.txt_partNo.Text, this.ddl_machineNo.SelectedValue))
                    {
                        this.txt_partNo.Focus();

                        this.ddl_machineNo.SelectedValue = "";
                        Common.CommFunctions.ShowMessage(this.Page, "This Part for this Machine has added, Please confirm!");
                        return;
                    }
                }
                #endregion
                
                #region set Bom Model
                Common.Class.Model.LMMSBom_Model Bom_model = new Common.Class.Model.LMMSBom_Model();
                Bom_model.module = txt_module.Text.Trim();
                Bom_model.partNumber = txt_partNo.Text.Trim();
                Bom_model.machineID = ddl_machineNo.SelectedItem.Value;
                Bom_model.cycleTime = double.Parse(txt_cycleTime.Text.Trim());
                Bom_model.unitCount = int.Parse(txt_unitCount.Text.Trim());
                Bom_model.blockCount = int.Parse(txt_blockCount.Text.Trim());
                Bom_model.Type = ddl_type.SelectedValue;
                Bom_model.remarks = this.txt_remark.Text;
                Bom_model.Customer = this.txt_customer.Text;
                Bom_model.dateTime = DateTime.Now;
               
                Bom_model.Supplier = this.txt_Supplier.Text;
                Bom_model.Number = this.txtNumber.Text.Trim();

                Bom_model.Lighting = this.txt_lighting.Text.Trim();
                Bom_model.Camera = this.txt_Camera.Text.Trim();
                Bom_model.CurrentPower = this.txt_power.Text.Trim();

                Session["Bom_model"] = Bom_model;
                #endregion

                #region set detail model list
                List<Common.Class.Model.LMMSBomDetail_Model> list_detailModel = new List<Common.Class.Model.LMMSBomDetail_Model>();
                foreach (DataGridItem item in this.dg_MaterialPart.Items)
                {
                    Common.Class.Model.LMMSBomDetail_Model model = new Common.Class.Model.LMMSBomDetail_Model();
                    model.sn = int.Parse(item.Cells[0].Text);
                    model.partNumber = item.Cells[1].Text;
                    model.MaterialPartNo = item.Cells[2].Text;
                    model.PartCount = int.Parse(item.Cells[3].Text);
                    model.dateTime = DateTime.Now;

                    list_detailModel.Add(model);
                }

                Session["list_detailModel"] = list_detailModel;
                #endregion

                if (commandType == "LinkFromProductionDetail")
                {
                    commandType = "UpdateBom";
                } 

                Response.Redirect("./Login.aspx?commandType="+ commandType + "&Department=" + StaticRes.Global.Department.Laser + "", false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "btn_submit_click error" + ee.Message);
                Common.CommFunctions.ShowMessage(this.Page, "Add fail!");
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('Your action will not be saved, are you sure?') == true) { ";
            strJS += "window.location.href = \"./BOMList.aspx\";  } ";

            ClientScript.RegisterStartupScript(Page.GetType(), "", strJS, true);
        }

      
        protected void btn_add_Click(object sender, EventArgs e)
        {
            #region textblock check
            if (txt_sn.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "S/N can not be empty, Please input!");
                txt_sn.Focus();
                return;
            }
            else
            {
                if (!Common.CommFunctions.isNumberic(txt_sn.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please input number in S/N!");
                    txt_sn.Text = "";
                    txt_sn.Focus();
                    return;
                }
            }

            if (txt_materialPart.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "MaterialPart can not be empty, Please input!");
                txt_materialPart.Focus();
                return;
            }
            if (txt_partCount.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "PartCount can not be empty, Please input!");
                txt_partCount.Focus();
                return;
            }
            else
            {
                if (!Common.CommFunctions.isNumberic(txt_partCount.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please input number in PartCount!");
                    txt_partCount.Text = "";
                    txt_partCount.Focus();
                    return;
                }
            }
            #endregion

            
            DataTable dt_bomDetail = (DataTable)Session["dt_bomDetail"];

            if (dt_bomDetail != null || dt_bomDetail.Rows.Count != 0)
            {
                #region check double add

                DataRow[] rows = dt_bomDetail.Select(" sn = '" + this.txt_sn.Text.Trim() + "'");

                if (rows.Length > 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This S/N is exist, Please confirm!");
                    txt_sn.Text = "";
                    txt_sn.Focus();
                    return;
                }


                rows = new DataRow[] { };
                rows = dt_bomDetail.Select(" materialPartNo = '" + this.txt_materialPart.Text.Trim() + "'");
                if (rows.Length > 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This material Part has been added, Please confirm!");
                    txt_materialPart.Text = "";
                    txt_materialPart.Focus();
                    return;
                }
                #endregion
            }



            #region add to dt_bomDetail 
            DataRow dr = dt_bomDetail.NewRow();
            dr["sn"] = int.Parse(txt_sn.Text);
            dr["partNumber"] = txt_partNo.Text;
            dr["materialPartNo"] = txt_materialPart.Text;
            dr["partCount"] = int.Parse(txt_partCount.Text);
            dr["dateTime"] = DateTime.Now;
            
            dt_bomDetail.Rows.Add(dr);

            Session["dt_bomDetail"] = dt_bomDetail;

            #endregion


            List_Refresh(dt_bomDetail);


            //init UI
            txt_materialPart.Text = "";
            txt_partCount.Text = "";
            txt_sn.Text = "";
            txt_sn.Focus();
        }
        

        //=============================Func=============================//

        private void SetMachineDDL()
        {
            this.ddl_machineNo.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_machineNo.Items.Add(Li);


            for (int i = 1; i < 9; i++)
            {
                Li = new ListItem();
                Li.Text = "No." + i.ToString();
                Li.Value = i.ToString();
                this.ddl_machineNo.Items.Add(Li);
            }
        }

        private void SetTypeDDL()
        {
            this.ddl_type.Items.Clear();
          
            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_type.Items.Add(Li);
          
            Li = new ListItem();
            Li.Text = "Button";
            Li.Value = StaticRes.Global.ProductType.BUTTON;
            this.ddl_type.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Panel";
            Li.Value = StaticRes.Global.ProductType.PANEL;
            this.ddl_type.Items.Add(Li);



            Li = new ListItem();
            Li.Text = "Bezel";
            Li.Value = "Bezel";
            this.ddl_type.Items.Add(Li);

            

            Li = new ListItem();
            Li.Text = "Lens";
            Li.Value = StaticRes.Global.ProductType.LENS;
            this.ddl_type.Items.Add(Li);
        }

        void List_Refresh(DataTable dt)
        {
            if (dt != null)
            {
                dg_MaterialPart.DataSource = dt.DefaultView;
                dg_MaterialPart.DataBind();
            }
        }


        private void SetBomUI(Common.Class.Model.LMMSBom_Model model)
        {
            //set ui value
            this.txt_partNo.Text = model.partNumber;
            this.txt_blockCount.Text = model.blockCount.ToString();
            this.txt_unitCount.Text = model.unitCount.ToString();
            this.txt_cycleTime.Text = model.cycleTime.ToString();
            this.txt_module.Text = model.module;
            this.ddl_machineNo.SelectedValue = model.machineID;
            this.ddl_type.SelectedValue = model.Type;
            this.txt_customer.Text = model.Customer;
            this.txt_lighting.Text = model.Lighting;
            this.txt_power.Text = model.CurrentPower;
            this.txt_Camera.Text = model.Camera;
            this.txt_remark.Text = model.remarks;
            this.txtNumber.Text = model.Number;

            this.txt_Supplier.Text = model.Supplier;
            //this.ddl_belongTo.SelectedValue = model.PartBelongTo;
        }

        private void SetBomUI(DataRow dr)
        {
            //set ui value
            this.txt_blockCount.Text = dr["blockCount"].ToString();
            this.txt_unitCount.Text = dr["unitCount"].ToString();
            this.txt_cycleTime.Text = dr["cycleTime"].ToString();
            this.txt_module.Text = dr["module"].ToString();
            this.ddl_type.SelectedValue = dr["type"].ToString();
            this.txt_customer.Text = dr["customer"].ToString();
            this.txt_lighting.Text = dr["lighting"].ToString();
            this.txt_Camera.Text = dr["camera"].ToString();
            this.txt_power.Text = dr["currentPower"].ToString();
            this.txt_remark.Text = dr["remarks"].ToString();
        }


        
    }
}