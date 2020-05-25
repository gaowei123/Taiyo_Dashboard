using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMaterialBatchload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //init session first, avoid closing page and opening again. 
                    Session["MaterialDB_load"] = null;
                    Session.Remove("MaterialDB_load");

                    Session["Material_Model_List_Load"] = null;
                    Session.Remove("Material_Model_List_Load");
                    //init session first, avoid closing page and opening again. 

                    string CommandType = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();

                    if (CommandType == "Return")
                    {
                        this.lblUserHeader.Text = "Moulding Material Batch Return"; 
                        this.lb_ListTitle.Text = "Return Material List";
                        this.lb_Weight.Text = "Return Weight";
                        this.lb_MachineNo.Visible = true;
                        this.ddlMachineID.Visible = true;
                    }

                    Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                    DataTable dt_MaterialBomList = bll.GetList("");

                    SetDDL(this.ddl_MaterialNo, dt_MaterialBomList, "Material_Part");

             
                    //this.txt_MaterialNo.Enabled = false;
                


                    RefreshList((DataTable)Session["MaterialDB_load"]);

                }


                this.dg_MaterialList.ItemCommand += Dg_MaterialList_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialBatchLoad", "Page_Load Exception" + ee.ToString());
            }

        }

        private void Dg_MaterialList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;


            if (e.CommandName == "Delete")
            {

                string RowNo = ((Button)item.Cells[4].FindControl("btn_delete")).Attributes["Index"].ToString();

                DataTable dt = (DataTable)Session["MaterialDB_load"];

                if (dt != null)
                {
                    dt.Rows.RemoveAt(int.Parse(RowNo));
                }


                Session["MaterialDB_load"] = dt;

                RefreshList(dt);
            }
        }


        protected void ddl_MaterialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ddl_MaterialNo.SelectedValue == "")
            //{
            //    return;
            //}
            //else if (this.ddl_MaterialNo.SelectedValue == "Other")
            //{
            //    this.txt_MaterialNo.Enabled = true;
            //    this.txt_MaterialNo.Focus();
            //    return ;
            //}
            //else
            //{
            //    this.txt_Material_LotNo.Focus();
            //}
        }

        

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            
            #region check materialNo, materialLotNo, Load Weight
            

            //if (this.ddl_MaterialNo.SelectedValue =="" && this.txt_MaterialNo.Text == "")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Material No can not be empty, Please choose or keyin !');", true);
            //    return;
            //}



            if (this.ddl_MaterialNo.SelectedValue == "" )
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Material No can not be empty, Please choose first !');", true);
                return;
            }
            //else if (this.ddl_MaterialNo.SelectedValue == "Other" && this.txt_MaterialNo.Text == "")
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Material No can not be empty, Please keyin !');", true);
            //    return;
            //}

            if (this.txt_LoadWeight.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Load Weight can not be empty, Please keyin !');", true);
                return;
            }
            else if (!Common.CommFunctions.isNumberic(this.txt_LoadWeight.Text))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Must key in number in Load Weight !');", true);
                return;
            }
            string CommandType = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
            if (CommandType == "Return")
            {
                if (this.ddlMachineID.SelectedValue == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Machine ID can not be empty, Please choose first !');", true);
                    return;
                }
            }
            #endregion


            DataTable dt = (DataTable)Session["MaterialDB_load"];
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Material_No");
                dt.Columns.Add("Material_LotNo");
                dt.Columns.Add("Inventory_Weight");
                dt.Columns.Add("Transaction_Weight");
                dt.Columns.Add("Last_Event");
                dt.Columns.Add("Load_Time");
                dt.Columns.Add("Updated_Time");
                dt.Columns.Add("Supplier");
                dt.Columns.Add("MachineID");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("User_Name");

                Session["MaterialDB_load"] = dt;
            }

            DataRow dr = dt.NewRow();

            dr["Material_No"] = this.ddl_MaterialNo.SelectedValue;// == "Other" ? this.txt_MaterialNo.Text : this.ddl_MaterialNo.SelectedValue;
            dr["Material_LotNo"] = this.txt_Material_LotNo.Text;
            dr["Inventory_Weight"] = this.txt_LoadWeight.Text;
            dr["Transaction_Weight"] = this.txt_LoadWeight.Text;

            //string CommandType = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
            dr["Last_Event"] = CommandType == "Return" ? "Return" : "Load";

            dr["Load_Time"] = DateTime.Now;
            dr["Updated_Time"] = DateTime.Now;
            dr["Supplier"] = "";
            if (CommandType == "Return")
            {
                dr["MachineID"] = this.ddlMachineID.SelectedValue;
            }
            else
            {
                dr["MachineID"] = "";
            }
            dr["Remarks"] = "";
            dr["User_Name"] = "";

            dt.Rows.Add(dr);

            Session["MaterialDB_load"] = dt;

            RefreshList(dt);


            //init UI
            this.ddl_MaterialNo.SelectedIndex = 0;
            //this.txt_MaterialNo.Text = "";
            //this.txt_MaterialNo.Enabled = false;

            this.txt_Material_LotNo.Text = "";
            this.txt_LoadWeight.Text = "";
            
        }
        

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            

            if (this.dg_MaterialList.Items.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please add material first!');", true);
                return;
            }


            List<Common.Class.Model.Material_Inventory> modelList = new List<Common.Class.Model.Material_Inventory>();

            DataTable dt = (DataTable)Session["MaterialDB_load"];

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Common.Class.Model.Material_Inventory model = new Common.Class.Model.Material_Inventory();

                    model.Material_No = row["Material_No"].ToString();
                    model.Material_LotNo = row["Material_LotNo"].ToString();
                    model.Inventory_Weight = Decimal.Parse(row["Inventory_Weight"].ToString());
                    model.Transaction_Weight = Decimal.Parse(row["Transaction_Weight"].ToString());
                    model.Last_Event = row["Last_Event"].ToString();
                    model.Load_Time = DateTime.Parse(row["Load_Time"].ToString());
                    model.Updated_Time = DateTime.Now;
                    model.Supplier = "";
                    model.Remarks = row["Remarks"].ToString();
                    model.MachineID = row["MachineID"].ToString();


                    modelList.Add(model);
                }
            }
            else
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialBatchLoad", "btn_submit_Click  Session[\"MaterialDB_Unload\"] is null ");
            }



            Session["Material_Model_List_Load"] = modelList;
            Response.Redirect("../Laser/Login.aspx?commandType=AddMaterialInventory&Department=Moulding", false);
        }


        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "", "if (confirm('Your action will not be saved, are you sure?') == true) { window.location.href = \"./MouldingPartsInventory.aspx\";}", true);
            return;
        }


        //================================ Func ================================//

        private void SetDDL(DropDownList ddl, DataTable dt, string BindColumnName)
        {
            ddl.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";

            ddl.Items.Add(Li);



            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string ColumnValue = row[BindColumnName].ToString();

                    Li = new ListItem();

                    Li.Text = ColumnValue;
                    Li.Value = ColumnValue;

                    ddl.Items.Add(Li);
                }
                catch (Exception ee)
                {
                    DBHelp.Reports.LogFile.Log("MouldingMaterialBatchLoad", "SetDDL Exception" + ee.ToString());
                }
            }

        }


        private void RefreshList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowWarning("");
            }
            else
            {
                this.dg_MaterialList.DataSource = dt.DefaultView;
                this.dg_MaterialList.DataBind();
                HideWarning();
            }
        }


        private void ShowWarning(string Message)
        {
            if (Message != "")
            {
                this.lblResult.Text = Message;
            }
            else
            {
                this.lblResult.Text = "No Record, Please add!";
            }

            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
            this.dg_MaterialList.Visible = false;
        }


        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_MaterialList.Visible = true;
        }

     
    }
}