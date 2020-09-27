using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DashboardTTS.Webform
{

    public partial class PQCBomFormMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    SetProcessDDL();

                    SetTypeDDL();

                    SetColorDDL();

                    SetShipToDDL();


                    #region  reset UI for update
                    string partNumber = Request.QueryString["partNumber"] == null ? "" : Request.QueryString["partNumber"].ToString();

                    if (partNumber != "")
                    {
                        Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

                        //Bom Model
                        Common.Class.Model.PQCBom_Model pqcBomModel = bll.GetModel(partNumber);
                        this.txtPartNo.Text = pqcBomModel.partNumber;
                        this.txtPartNo.Enabled = false;
                        this.txtModel.Text = pqcBomModel.model;
                        this.txtCustomer.Text = pqcBomModel.customer;

                        this.ddlType.SelectedValue = pqcBomModel.Type;
                        this.ddlShipTo.SelectedValue = pqcBomModel.ShipTo;
                        this.ddlColor.SelectedValue = pqcBomModel.color;

                        this.lbProcess.Text = pqcBomModel.processes;
                        this.txt_remark.Text = pqcBomModel.remarks;
                        this.txtSupplier.Text = pqcBomModel.remark_1;
                        this.ddlCoating.SelectedValue = pqcBomModel.Coating;
                        this.ddlDescription.SelectedValue = pqcBomModel.Description;
                        this.txtNumber.Text = pqcBomModel.Number;
                        this.txtUnitCost.Text = pqcBomModel.UnitCost.ToString();
                        this.txtcycleTime.Text = pqcBomModel.cycleTime.ToString();

                        //Bom Model

                        

                        //Bom Detail
                        Common.Class.BLL.PQCBomDetail_BLL bomDetailBLL = new Common.Class.BLL.PQCBomDetail_BLL();
                        DataTable dtBomDetail = bomDetailBLL.GetList(partNumber);
                        List_Refresh(dtBomDetail);

                    }
                    else
                    {
                        DataTable dt_bomDetail = SetMaterialTable();
                        Session["dt_PQCbomDetail"] = dt_bomDetail;
                    }
                    #endregion

                }

                this.dg_MaterialPart.ItemCommand += Dg_MaterialPart_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCBomFormMenu", "Page_Load Exception" + ee.ToString());
            }
        }


        protected void txt_partNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //search from laser bom
                string partNumber = txtPartNo.Text;

                if (partNumber == "")
                    return;


                //check double add
                Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
                bool isExist = bll.Exist(partNumber);

                if (isExist)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This Part has been added, Please check!");
                    return;
                }


                //if added at laser bom, some same data set for pqc bom
                Common.Class.BLL.LMMSBom_BLL laserBomBLL = new Common.Class.BLL.LMMSBom_BLL();
                Common.Class.Model.LMMSBom_Model laserBomModel = new Common.Class.Model.LMMSBom_Model();

                if (laserBomModel == null)
                    return;

                laserBomModel = laserBomBLL.GetBomModel(partNumber,"");
                if (laserBomModel != null)
                {
                    this.txtModel.Text = laserBomModel.module;
                    this.txtCustomer.Text = laserBomModel.Customer;
                    this.txtSupplier.Text = laserBomModel.Supplier;
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCBomFormMenu", "txt_partNo_TextChanged Exception " + ee.Message);
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
                    txt_sn.Text = item.Cells[0].Text;
                    txt_materialPart.Text = item.Cells[1].Text;
                    txtMaterialName.Text = item.Cells[2].Text;
                    txtModule.Text = item.Cells[3].Text;
                    txt_partCount.Text = item.Cells[4].Text;
                    txtOuterBoxQty.Text = item.Cells[5].Text;
                    txtPackingTrays.Text = item.Cells[6].Text;
            

                    //remove from datatable
                    DataTable dt_bomDetail = (DataTable)Session["dt_PQCbomDetail"];
                    if (dt_bomDetail != null)
                    {
                        int rowNo = int.Parse(((Button)item.FindControl("btn_Delete")).Attributes["Index"]);
                        dt_bomDetail.Rows.RemoveAt(rowNo);
                    }

                    List_Refresh(dt_bomDetail);
                    Session["dt_PQCbomDetail"] = dt_bomDetail;

                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "Dg_BOMList_ItemCommand error--" + ee.Message);
                Common.CommFunctions.ShowMessage(this.Page, "Delete failed");
            }
        }
        
        protected void btn_add_Click(object sender, EventArgs e)
        {

            #region check textblock 
            if (txt_sn.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "S/N can not be empty, Please input!");
                txt_sn.Focus();
                return;
            }
            else if (!Common.CommFunctions.isNumberic(txt_sn.Text))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input number in S/N!");
                txt_sn.Text = "";
                txt_sn.Focus();
                return;
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
            else if (!Common.CommFunctions.isNumberic(txt_partCount.Text))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input number in PartCount!");
                txt_partCount.Text = "";
                txt_partCount.Focus();
                return;
            }

            if (txtModule.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input module!");
                txtModule.Text = "";
                txtModule.Focus();
                return;
            }

            if (txtOuterBoxQty.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input Outer Box Qty!");
                txtOuterBoxQty.Text = "";
                txtOuterBoxQty.Focus();
                return;
            }
            else if (!Common.CommFunctions.isNumberic(txtOuterBoxQty.Text))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Outer Box Qty must be number!");
                txtOuterBoxQty.Text = "";
                txtOuterBoxQty.Focus();
                return;
            }

            if (txtMaterialName.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input material name!");
                txtMaterialName.Text = "";
                txtMaterialName.Focus();
                return;
            }

            if (txtPackingTrays.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(this.Page, "Please input packing trays!");
                txtPackingTrays.Text = "";
                txtPackingTrays.Focus();
                return;
            }
           


            #endregion


            DataTable dt_bomDetail = (DataTable)Session["dt_PQCbomDetail"];

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
            dr["partNumber"] = txtPartNo.Text;
            dr["materialPartNo"] = txt_materialPart.Text;
            dr["partCount"] = int.Parse(txt_partCount.Text);

            string ImagePath = "../../MaterialPartImg/";
            ImagePath += txtPartNo.Text + "/";
            string fileName = fpImg.PostedFile.FileName;

            string RootPath = Server.MapPath(ImagePath);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(RootPath);
            if (!dir.Exists)
                dir.Create();


            if (fpImg.HasFile)
            {
                fpImg.SaveAs(RootPath + fileName);
            }
            else
            {
                //判断下server文件下有没有这个图片
                Common.Class.BLL.PQCBomDetail_BLL bll = new Common.Class.BLL.PQCBomDetail_BLL();
                List<Common.Class.Model.PQCBomDetail_Model> materialList = bll.GetModelList(txtPartNo.Text);

                if (materialList == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose image for the material part " + txt_materialPart.Text + " to update");
                    return;
                }

                var material = (from a in materialList where a.sn == int.Parse(txt_sn.Text) select a).FirstOrDefault();
                if (material == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose image for the material part " + txt_materialPart.Text + " to update");
                    return;
                }


                string[] arrPath = material.imagePath.Split('/');
                fileName = arrPath[arrPath.Length - 1];

                if (!System.IO.File.Exists(Server.MapPath(material.imagePath)))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose image for the material part " + txt_materialPart.Text + " to update");
                    return;
                }
            }

            dr["imageAbsolutePath"] = RootPath + fileName;
            dr["imagePath"] = ImagePath + fileName;
            dr["dateTime"] = DateTime.Now;

            //new parameters
            dr["color"] = ddlColor.SelectedValue;
            dr["materialName"] = txtMaterialName.Text;
            dr["module"] = txtModule.Text;
            dr["outerBoxQty"] = txtOuterBoxQty.Text;
            dr["packingTrays"] = txtPackingTrays.Text;
            


            dt_bomDetail.Rows.Add(dr);

            Session["dt_PQCbomDetail"] = dt_bomDetail;

            #endregion


            List_Refresh(dt_bomDetail);

            //init UI
            txt_materialPart.Text = "";
            txt_partCount.Text = "";
            txt_sn.Text = "";
            txtModule.Text = "";
            txtOuterBoxQty.Text = "";
            txtMaterialName.Text = "";
            txtPackingTrays.Text = "";
            txt_sn.Focus();
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                #region check text
                if (txtPartNo.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Part No!');", true);
                    return;
                }
                
                if (txtModel.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input module!');", true);
                    return;
                }

                if (txtCustomer.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input customer!');", true);
                    return;
                }

                //if (txtSupplier.Text == "")
                //{
                //    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input supplier!');", true);
                //    return;
                //}

                if (ddlColor.SelectedValue == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose color!');", true);
                    return;
                }

                if (ddlShipTo.SelectedValue == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose ship to!');", true);
                    return;
                }

                if (ddlType.SelectedValue == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose type!');", true);
                    return;
                }

                if (lbProcess.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose process!');", true);
                    return;
                }

                if (this.dg_MaterialPart.Items.Count == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please add material part!');", true);
                    return;
                }
                #endregion


                string commandType = "AddBom";
                string partNo = Request.QueryString["partNumber"] == null ? "" : Request.QueryString["partNumber"].ToString();
                if (partNo != "")
                {
                    commandType = "UpdateBom";
                }

                //check double add
                Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
                bool isExist = bll.Exist(txtPartNo.Text.Trim());

                if (isExist && commandType == "AddBom" )
                {
                    Common.CommFunctions.ShowMessage(this.Page, "This Part has been added, Please check!");
                    return;
                }






                #region set Bom Model
                Common.Class.Model.PQCBom_Model model = new Common.Class.Model.PQCBom_Model();
                model.partNumber = txtPartNo.Text;
                model.model = txtModel.Text;
                model.customer = txtCustomer.Text;
                model.remark_1 = txtSupplier.Text;
                model.color = ddlColor.SelectedValue;
                model.ShipTo = ddlShipTo.SelectedValue;
                model.Type = ddlType.SelectedValue;
                model.processes = lbProcess.Text;
                model.remarks = txt_remark.Text;
                model.dateTime = DateTime.Now;
                model.ShipTo = ddlShipTo.SelectedValue;
                model.Type = ddlType.SelectedValue;

                model.Description = ddlDescription.SelectedValue;
                model.Coating = ddlCoating.SelectedValue;

                model.UnitCost = txtUnitCost.Text == "" ? 0 : decimal.Parse(txtUnitCost.Text);
                model.Number = txtNumber.Text.Trim();
                model.cycleTime = txtcycleTime.Text == "" ? 0 : decimal.Parse(txtcycleTime.Text);


                

                //default setting
                model.machineID = "1";
                model.jigNo = txtPartNo.Text;
                model.cavityCount = 1;
                model.blockCount = 1;
                model.unitCount = 1;
                //default setting

                Session["PQCBom_Model"] = model;
                #endregion

                #region set detail model list

                DataTable dt_bomDetail = (DataTable)Session["dt_PQCbomDetail"];

                List<Common.Class.Model.PQCBomDetail_Model> list_detailModel = new List<Common.Class.Model.PQCBomDetail_Model>();
                foreach (DataRow dr in dt_bomDetail.Rows)
                {
                    Common.Class.Model.PQCBomDetail_Model detailModel = new Common.Class.Model.PQCBomDetail_Model();
                    detailModel.sn = int.Parse(dr["sn"].ToString());
                    detailModel.partNumber = dr["partNumber"].ToString();
                    detailModel.materialPartNo = dr["materialPartNo"].ToString();
                    detailModel.partCount = decimal.Parse(dr["partCount"].ToString());
                    detailModel.imagePath = dr["imagePath"].ToString();
                    
                    //img file  to  byte[]
                    string filePath = dr["imageAbsolutePath"].ToString();
                    if (filePath != "")
                    {
                        FileStream files = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
                        byte[] fImageByte = new byte[(int)files.Length];
                        files.Read(fImageByte, 0, fImageByte.Length);
                        detailModel.partImage = fImageByte;
                    }
                    //img file  to  byte[]
                    
                    detailModel.imageAbsolutePath = filePath;
                    detailModel.dateTime = DateTime.Now;
                    
                    
                    //new parameters
                    detailModel.materialName = dr["materialName"].ToString();
                    detailModel.customer = txtCustomer.Text;
                    detailModel.outerBoxQty = int.Parse(dr["outerBoxQty"].ToString());
                    detailModel.packingTrays = dr["packingTrays"].ToString();
                    detailModel.module = dr["module"].ToString();
                    detailModel.color = ddlColor.SelectedValue;
                    //new parameters

                    list_detailModel.Add(detailModel);
                }

                Session["list_PQCdetailModel"] = list_detailModel;

                #endregion

                

                Response.Redirect("../Laser/Login.aspx?commandType=" + commandType + "&Department=" + StaticRes.Global.Department.PQC + "", false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "btn_submit_click error" + ee.Message);
                Common.CommFunctions.ShowMessage(this.Page, "Add fail!");
            }
        }
        
        //cancel  back to list page
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('Your action will not be saved, are you sure?') == true) { ";
            strJS += "window.location.href = \"./PQCBomList.aspx\";  } ";

            ClientScript.RegisterStartupScript(Page.GetType(), "", strJS, true);
        }
        
        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string process = lbProcess.Text.Trim();
            if (process.Length != 0)
            {
                process += "-";
            }


            process += this.ddlProcess.SelectedValue;

            this.lbProcess.Text = process;
        }

        //cancel process
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (lbProcess.Text.Trim().Length == 0)
                return;

            string[] processArr = lbProcess.Text.Split('-');

            string process = "";


            for (int i = 0; i < processArr.Length - 1; i++)
            {
                process += processArr[i];
                process += "-";
            }
            if (processArr.Length > 1)
            {
                process = process.Substring(0, process.Length - 1);
            }


            this.lbProcess.Text = process;
        }



        //=============================Func=============================//
        void List_Refresh(DataTable dt)
        {
            if (dt != null)
            {
                dg_MaterialPart.DataSource = dt.DefaultView;
                dg_MaterialPart.DataBind();

                
                foreach (DataGridItem item in this.dg_MaterialPart.Items)
                {
                    ((Image)item.Cells[8].FindControl("imgMaterialPart")).ImageUrl = item.Cells[7].Text;
                }

                Session["dt_PQCbomDetail"] = dt;
            }
        }
        
        private DataTable SetMaterialTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("sn");
            dt.Columns.Add("partNumber");
            dt.Columns.Add("materialPartNo");
            dt.Columns.Add("partCount");
            dt.Columns.Add("imagePath");//image控件读取图片文件的路径, 相对路径
            dt.Columns.Add("imageAbsolutePath");//server上实际保存文件的路径
            dt.Columns.Add("color");
            dt.Columns.Add("datetime");


            dt.Columns.Add("materialName");
            dt.Columns.Add("module");
            dt.Columns.Add("outerBoxQty");
            dt.Columns.Add("packingTrays");




            return dt;
        }
        






        private void SetProcessDDL()
        {
            this.ddlProcess.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Mould;
            Li.Value = StaticRes.Global.PQC.Process.Mould;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Print;
            Li.Value = StaticRes.Global.PQC.Process.Print;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Paint1st;
            Li.Value = StaticRes.Global.PQC.Process.Paint1st;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Paint2nd;
            Li.Value = StaticRes.Global.PQC.Process.Paint2nd;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Laser;
            Li.Value = StaticRes.Global.PQC.Process.Laser;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Check1st;
            Li.Value = StaticRes.Global.PQC.Process.Check1st;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Check2nd;
            Li.Value = StaticRes.Global.PQC.Process.Check2nd;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Check3rd;
            Li.Value = StaticRes.Global.PQC.Process.Check3rd;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Check4th;
            Li.Value = StaticRes.Global.PQC.Process.Check4th;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.QA;
            Li.Value = StaticRes.Global.PQC.Process.QA;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Packing;
            Li.Value = StaticRes.Global.PQC.Process.Packing;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.Assembly;
            Li.Value = StaticRes.Global.PQC.Process.Assembly;
            this.ddlProcess.Items.Add(Li);

            Li = new ListItem();
            Li.Text = StaticRes.Global.PQC.Process.FG;
            Li.Value = StaticRes.Global.PQC.Process.FG;
            this.ddlProcess.Items.Add(Li);

        }
       
        private void SetTypeDDL()
        {
            this.ddlType.Items.Clear();
            
      
            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "LASER";
            Li.Value = "LASER";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "WIP";
            Li.Value = "WIP";
            this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "SBW TKS784";
            //Li.Value = StaticRes.Global.ProductType.SBW_TKS784;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TMS TKS824";
            //Li.Value = StaticRes.Global.ProductType.TMS_TKS824;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TAC TKS833";
            //Li.Value = StaticRes.Global.ProductType.TAC_TKS833;
            //this.ddlType.Items.Add(Li);
            
            //Li = new ListItem();
            //Li.Text = "TRMI 452";
            //Li.Value = StaticRes.Global.ProductType.TRMI_452;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TRMI 595,656";
            //Li.Value = StaticRes.Global.ProductType.TRMI_595_656;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "320B TKS830";
            //Li.Value = StaticRes.Global.ProductType.TKS830_320B;
            //this.ddlType.Items.Add(Li);


            //Li = new ListItem();
            //Li.Text = "Packers";
            //Li.Value = StaticRes.Global.ProductType.Packers;
            //this.ddlType.Items.Add(Li);
            
        }

        private void SetColorDDL()
        {
            this.ddlColor.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlColor.Items.Add(Li);


            Li = new ListItem();
            Li.Text = "Black";
            Li.Value = "Black";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Silver";
            Li.Value = "Silver";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "High gloss";
            Li.Value = "High gloss";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Mat black";
            Li.Value = "Mat black";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Texture line";
            Li.Value = "Texture line";
            this.ddlColor.Items.Add(Li);
        }
        
        private void SetShipToDDL()
        {

            this.ddlShipTo.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlShipTo.Items.Add(Li);


            Li = new ListItem();
            Li.Text = "FG";
            Li.Value = "FG";
            this.ddlShipTo.Items.Add(Li);


            Li = new ListItem();
            Li.Text = "Assembly";
            Li.Value = "Assembly";
            this.ddlShipTo.Items.Add(Li);
          
        }

    }
}