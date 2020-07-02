using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMaterialBatchUnload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["MaterialDB_Unload"] = null;
                    Session.Remove("MaterialDB_Unload");
                    Session["Material_Model_List_Unload"] = null;
                    Session.Remove("Material_Model_List_Unload");

                    Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();
                    DataTable dt_Inventory = bll.GetList("", "", null);

                    dt_Inventory.Columns["Load_Time"].DataType = typeof(DateTime);
                    dt_Inventory.Columns["Inventory_Weight"].DataType = typeof(decimal);

                    Session["dt_Inventory"] = dt_Inventory;

                    DataView dv = dt_Inventory.DefaultView;
                    DataTable dt_MaterialNo = dv.ToTable(true, "Material_No");
                     
                    SetDDL(this.ddl_MaterialNo, dt_MaterialNo, "Material_No");

                    SetMachineID();

                    this.ddl_MaterialLotNo.Enabled = false;
                    this.ddl_UnloadDate.Enabled = false;
                    this.txt_UnloadWeight.Enabled = false;

                    RefreshList((DataTable)Session["MaterialDB_Unload"]);
                }
                this.dg_MaterialList.ItemCommand += Dg_MaterialList_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialBatchUnload", "Page_Load Exception" + ee.ToString());
            }
        }

        private void Dg_MaterialList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;
            string Material_No = item.Cells[1].Text;
            string Material_LotNo = item.Cells[2].Text;
            DateTime Load_Time = DateTime.Parse(item.Cells[3].Text);
            double Transaction_Weight = double.Parse(item.Cells[4].Text);
            if (e.CommandName == "Delete")
            {
                string RowNo = ((Button)item.Cells[5].FindControl("btn_delete")).Attributes["Index"].ToString();
                DataTable dt = (DataTable)Session["MaterialDB_Unload"];
                if (dt != null)
                {
                    dt.Rows.RemoveAt(int.Parse(RowNo));
                }
                Session["MaterialDB_Unload"] = dt;
                RefreshList(dt);
                DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
                if (dt_Inventory != null)
                {
                    double Inventory = double.Parse(dt_Inventory.Select("Material_No = '" + Material_No + "' and Material_LotNo = '" + Material_LotNo + "' and Load_Time = '" + Load_Time + "' ")[0]["Inventory_Weight"].ToString());
                    dt_Inventory.Select("Material_No = '" + Material_No + "' and Material_LotNo = '" + Material_LotNo + "' and Load_Time = '" + Load_Time + "' ")[0]["Inventory_Weight"] = Transaction_Weight + Inventory;
                }
            }
        }

        protected void ddl_MaterialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddl_MaterialNo.SelectedValue == "")
            {
                return;
            }
            string sMaterialNo = this.ddl_MaterialNo.SelectedValue;
            try
            {
                DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
                if (dt_Inventory != null)
                {               
                    DataRow[] Rows = dt_Inventory.Select("Material_No = '" + sMaterialNo + "'  and Inventory_Weight <> '0'");
                    if (Rows.Length != 0)
                    {
                        DataView dv = Common.CommFunctions.DataRowToDataTable(Rows).DefaultView;
                        DataTable dt_Material_LotNo = dv.ToTable(true, "Material_LotNo");
                        SetDDL(this.ddl_MaterialLotNo, dt_Material_LotNo, "Material_LotNo");
                        this.ddl_MaterialLotNo.Enabled = true;
                        if (dt_Material_LotNo.Rows.Count == 1)
                        {
                            this.ddl_MaterialLotNo.SelectedIndex = 1;
                            ddl_MaterialLotNo_SelectedIndexChanged(new object(), new EventArgs());
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        protected void ddl_MaterialLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMaterialNo = this.ddl_MaterialNo.SelectedValue;
            string sMaterialLotNo = this.ddl_MaterialLotNo.SelectedValue;
            DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
            if (dt_Inventory != null)
            {
                DataRow[] Rows = dt_Inventory.Select("Material_No = '" + sMaterialNo + "' and Material_LotNo = '"+ sMaterialLotNo + "' and Inventory_Weight <> '0' ");
                if (Rows.Length != 0)
                {
                    DataView dv = Common.CommFunctions.DataRowToDataTable(Rows).DefaultView;
                    DataTable dt_LoadTime = dv.ToTable(true, "Load_Time");
                    SetDDL(this.ddl_UnloadDate, dt_LoadTime, "Load_Time");
                    this.ddl_UnloadDate.Enabled = true;
                    if (dt_LoadTime.Rows.Count == 1)
                    {
                        ddl_UnloadDate.SelectedIndex = 1;
                        ddl_UnloadDate_SelectedIndexChanged(new object(), new EventArgs());
                    }
                }
            }
        }

        protected void ddl_UnloadDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMaterialNo = this.ddl_MaterialNo.SelectedValue;
            string sMaterialLotNo = this.ddl_MaterialLotNo.SelectedValue;
            string sUnloadDate = this.ddl_UnloadDate.SelectedValue;          
            if (string.IsNullOrEmpty(sUnloadDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Load Date can not be empty!');", true);
                return;
            }
            DateTime dUnloadDate = DateTime.Parse(sUnloadDate);
            DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
            if (dt_Inventory != null)
            {
                DataRow[] Rows = dt_Inventory.Select("Material_No = '" + sMaterialNo + "' and Material_LotNo = '" + sMaterialLotNo + "' and Load_Time = '" + dUnloadDate + "' and Inventory_Weight <> '0' ");
                string Current_Wight = Rows[0]["Inventory_Weight"].ToString();
                this.txt_UnloadWeight.Attributes["placeholder"] = "Max " + Current_Wight + "(kg)";
                this.txt_UnloadWeight.Enabled = true;
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            #region check validation
            if (this.ddl_MachineNo.SelectedValue == "" )
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose Machine ID!');", true);
                return;
            }
            if (this.txt_UnloadWeight.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input unload quantity!');", true);
                return;
            }
            else if (!Common.CommFunctions.isNumberic(this.txt_UnloadWeight.Text))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Must input quantity in unload weight!');", true);
                return;
            }
            string temp_Wight = this.txt_UnloadWeight.Attributes["placeholder"].ToString().Replace("Max", "").Replace("(kg)", "").Trim();
            double InventoryWeight = double.Parse(temp_Wight);
            double InputWeight = double.Parse(this.txt_UnloadWeight.Text);
            if (InputWeight > InventoryWeight)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Current material is not enough!');", true);
                return;
            }

            //判断所选material no是不是机器最后一次做的material no, 不是的话提示确认. 
            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
            DataTable dtLastViTrakcing = bll.getLastVITracking(this.ddl_MachineNo.SelectedValue);
            if (dtLastViTrakcing != null && dtLastViTrakcing.Rows.Count !=0)
            {
                string sMaterial01 = dtLastViTrakcing.Rows[0]["matPart01"].ToString();
                string sMaterial02 = dtLastViTrakcing.Rows[0]["matPart02"].ToString();
                string sSelectedMaterialNo = this.ddl_MaterialNo.SelectedValue;

                if (sSelectedMaterialNo != sMaterial01 && sSelectedMaterialNo != sMaterial02)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", string.Format("alert('Warning! Machine{0} running material: ({1}),({2}) at last time, Please confirm !');", ddl_MachineNo.SelectedValue,sMaterial01,sMaterial02), true);
                }
            }
            #endregion

            string Material_No= this.ddl_MaterialNo.SelectedValue;
            string Material_LotNo = this.ddl_MaterialLotNo.SelectedValue;
            DateTime Load_Time = DateTime.Parse(this.ddl_UnloadDate.SelectedValue);
            string MachineID = this.ddl_MachineNo.SelectedValue;

            #region set datatable   & refresh List
            DataTable dt = (DataTable)Session["MaterialDB_Unload"];
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

                Session["MaterialDB_Unload"] = dt;
            }

            DataRow dr = dt.NewRow();

            dr["Material_No"] = Material_No;
            dr["Material_LotNo"] = Material_LotNo;
            dr["Inventory_Weight"] = InventoryWeight - InputWeight;
            dr["Transaction_Weight"] = InputWeight;
            dr["Last_Event"] = "Unload";
            dr["Load_Time"] = Load_Time;
            dr["Updated_Time"] = DateTime.Now;
            dr["Supplier"] = "";
            dr["MachineID"] = MachineID;
            dr["Remarks"] = "";
            dr["User_Name"] = "";
            dt.Rows.Add(dr);
            Session["MaterialDB_Unload"] = dt;
            RefreshList(dt);
            #endregion

            #region init UI
            this.ddl_MaterialLotNo.Enabled = false;
            this.ddl_MaterialLotNo.Text = "";
            this.ddl_UnloadDate.Enabled = false;
            this.ddl_UnloadDate.Text = "";
            this.txt_UnloadWeight.Attributes["placeholder"] = "";
            this.txt_UnloadWeight.Text = "";
            this.txt_UnloadWeight.Enabled = false;
            this.ddl_MaterialNo.SelectedIndex = 0;
            SetMachineID();
            #endregion

            DBHelp.Reports.LogFile.Log("MouldingMaterialBatchUnload", "btn_Add_Click Material_No:" + this.ddl_MaterialNo.SelectedValue + ", Weight:" + InputWeight + ", Load_Time;" + this.ddl_UnloadDate.SelectedValue + ",  Material_LotNo:" + this.ddl_MaterialLotNo.SelectedValue + ", MachineID:" + this.ddl_MachineNo.SelectedValue + "  ");
            
            #region update dt_inventory
            DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
            if (dt_Inventory != null)
            {
                if (InventoryWeight - InputWeight == 0)
                {
                    dt_Inventory.Select("Material_No = '" + Material_No + "' and Material_LotNo = '" + Material_LotNo + "' and Load_Time = '" + Load_Time + "' ")[0]["Inventory_Weight"] = 0;
                }
                else if (InventoryWeight - InputWeight > 0)
                {
                    dt_Inventory.Select("Material_No = '" + Material_No + "' and Material_LotNo = '" + Material_LotNo + "' and Load_Time = '" + Load_Time + "' ")[0]["Inventory_Weight"] = InventoryWeight - InputWeight;
                }
            }
            #endregion 
        }
       
        #region   not use
        protected void ddl_LoadDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RowNumber = int.Parse(((DropDownList)sender).Attributes["Index"].Trim());

            string sMaterialNo = ((DropDownList)this.dg_MaterialList.Items[RowNumber].Cells[1].FindControl("ddl_MaterialNo")).SelectedValue;
            string sMaterialLotNo = ((DropDownList)this.dg_MaterialList.Items[RowNumber].Cells[2].FindControl("ddl_MaterialLotNo")).SelectedValue;


            string sLoadDate = ((DropDownList)this.dg_MaterialList.Items[RowNumber].Cells[3].FindControl("ddl_LoadDate")).SelectedValue;
            if (string.IsNullOrEmpty(sLoadDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Load Date can not be empty!');", true);
                return;
            }
          
            DateTime? dLoadDate  =  DateTime.Parse(sLoadDate);

            Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();
            DataTable dt = bll.GetList(sMaterialNo, sMaterialLotNo, dLoadDate);

            string Current_Wight = dt.Rows[0]["Weight"].ToString();

            TextBox tb = (TextBox)this.dg_MaterialList.Items[RowNumber].Cells[4].FindControl("txt_UnloadWeight");
            tb.Enabled = true;
            tb.Attributes["placeholder"] = "Max " + Current_Wight + "(kg)";
        }

        protected void cb_OneMore_CheckedChanged(object sender, EventArgs e)
        {
            int RowNumber = int.Parse(((CheckBox)sender).Attributes["Index"].Trim());

            DataGridItem Item = (DataGridItem)this.dg_MaterialList.Items[RowNumber];

            CheckBox cb = (CheckBox)Item.Cells[5].FindControl("cb_OneMore");
            

            if (cb.Checked)
            {
              

                DropDownList ddl_MaterialLotNo = (DropDownList)Item.Cells[2].FindControl("ddl_MaterialLotNo");
                ddl_MaterialLotNo.Items.Clear();
                ddl_MaterialLotNo.Enabled = false;
            

                DropDownList ddl_LoadDate = (DropDownList)Item.Cells[3].FindControl("ddl_LoadDate");
                ddl_LoadDate.Items.Clear();
                ddl_LoadDate.Enabled = false;
          

                TextBox txt_Weight = (TextBox)Item.Cells[4].FindControl("txt_UnloadWeight");
                txt_Weight.Text = "";
                txt_Weight.Enabled = false;
           

                CheckBox cb_OneMore = (CheckBox)Item.Cells[5].FindControl("cb_OneMore");
                cb_OneMore.Checked = false;
         

        

            }
            else
            {
                this.dg_MaterialList.Controls[0].Controls.RemoveAt(RowNumber);
            }
         
            
        }

        #endregion

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (this.dg_MaterialList.Items.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please add material first!');", true);
                return;
            }
            List<Common.Class.Model.Material_Inventory> modelList = new List<Common.Class.Model.Material_Inventory>();
            DataTable dt = (DataTable)Session["MaterialDB_Unload"];
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
                    if (int.Parse(DateTime.Now.Hour.ToString()) >= 8 && int.Parse(DateTime.Now.Hour.ToString()) < 20)
                    {
                        model.REF_FIELD01 = StaticRes.Global.Shift.Day;
                        model.REF_FIELD02 = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (int.Parse(DateTime.Now.Hour.ToString()) >= 20 && int.Parse(DateTime.Now.Hour.ToString()) < 24)
                    {
                        model.REF_FIELD01 = StaticRes.Global.Shift.Night;
                        model.REF_FIELD02 = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        model.REF_FIELD01 = StaticRes.Global.Shift.Night;
                        model.REF_FIELD02 = DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    modelList.Add(model);
                }
            }
            else
            {
                DBHelp.Reports.LogFile.Log("MouldingMaterialBatchUnload", "btn_submit_Click  Session[\"MaterialDB_Unload\"] is null ");
            }           
            Session["Material_Model_List_Unload"] = modelList;
            Response.Redirect("../Laser/Login.aspx?commandType=UpdateMaterialInventory&Department=Moulding", false);
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
                    DBHelp.Reports.LogFile.Log("MouldingMaterialBatchUnload", "SetDDL Exception" + ee.ToString());
                }
            }
        }


        private void SetMachineID()
        {
            this.ddl_MachineNo.Items.Clear();
            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_MachineNo.Items.Add(Li);
            for (int i = 1; i < 10; i++)
            {
                Li = new ListItem();
                Li.Value = i.ToString();
                Li.Text = "No." + i.ToString();
                this.ddl_MachineNo.Items.Add(Li);
            }            
            Li = new ListItem();
            Li.Value = "Sale";
            Li.Text = "Sale";
            this.ddl_MachineNo.Items.Add(Li);

            Li = new ListItem();
            Li.Value = "Meiki";
            Li.Text = "Meiki";
            this.ddl_MachineNo.Items.Add(Li);

            Li = new ListItem();
            Li.Value = "Sintech";
            Li.Text = "Sintech";
            this.ddl_MachineNo.Items.Add(Li);

            Li = new ListItem();
            Li.Value = "Koei Tool";
            Li.Text = "Koei Tool";
            this.ddl_MachineNo.Items.Add(Li);

            Li = new ListItem();
            Li.Value = "Kay Kay";
            Li.Text = "Kay Kay";
            this.ddl_MachineNo.Items.Add(Li);
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