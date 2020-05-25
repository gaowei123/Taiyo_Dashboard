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
    public partial class MouldingPartsMoving : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //init page
                    string TransferTo = Request.QueryString["TransferTo"] == null ? "" : Request.QueryString["TransferTo"].ToString();
                    string PartNumber = Request.QueryString["PartNumber"] == null ? "" : Request.QueryString["PartNumber"].ToString();
                    DateTime ProductionDate = Request.QueryString["ProductionDate"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["ProductionDate"].ToString());
                    DateTime DateFrom = Request.QueryString["DateFrom"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    DateTime DateTo = Request.QueryString["DateTo"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateTo"].ToString());
                    this.ddlTransferTo.Items.Clear();
                    this.ddlTransferTo.Items.Add("");
                    this.ddlTransferTo.Items.Add("Painting Dept");
                    this.ddlTransferTo.Items.Add("Silkprint");
                    this.ddlTransferTo.Items.Add("Summer Weli");
                    this.ddlTransferTo.Items.Add("store");

                    if (TransferTo != "")
                    {
                        this.ddlTransferTo.Items.FindByValue(TransferTo).Selected = true;
                    }
                    SetPartNo();
                    //this.infProductionDate.Value = ProductionDate;
                    //infProductionDate.CalendarLayout.SelectedDate = ProductionDate;

                    this.infDchFrom.Value = DateFrom;
                    infDchFrom.CalendarLayout.SelectedDate = DateFrom;
                    this.infDchTo.Value = DateTo;
                    infDchTo.CalendarLayout.SelectedDate = DateTo;


                    HideWarning();

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
                this.dg_BOMList.ItemCommand += Dg_Report_ItemCommand;
            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("QAProductionReport", "Page_Load Exception :" + ee.ToString());
            }
        }


        private void SetPartNo()
        {
            this.ddlPartNo.Items.Clear();

            //Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();

            //List<string> ListPart = new List<string>();
            //ListPart = bll.GetPartNumberAllList();

            //ListItem Li = new ListItem();
            //Li.Text = "";
            //Li.Value = "";
            //this.ddl_MouldChase.Items.Add(Li);

            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();

            List<string> ListPart = new List<string>();
            ListPart = bll.GetPartNumberAllList();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddlPartNo.Items.Add(Li);

            if (ListPart != null)
            {
                foreach (string ID in ListPart)
                {
                    Li = new ListItem();
                    Li.Text = ID;
                    Li.Value = ID;
                    this.ddlPartNo.Items.Add(Li);
                }
            }
        }

        private void Dg_Report_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataGridItem item = e.Item;

                #region URL paras
                string Material_Part = item.Cells[1].Text == "&nbsp;" ? "" : item.Cells[1].Text;
                string Transfer_To = item.Cells[2].Text == "&nbsp;" ? "" : item.Cells[2].Text;
                string Transfer_Date = item.Cells[3].Text == "&nbsp;" ? "" : item.Cells[3].Text;
                string Quantity = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;
                string Opr_ID = item.Cells[5].Text == "&nbsp;" ? "" : item.Cells[5].Text;
                string Production_Date = item.Cells[6].Text == "&nbsp;" ? "" : item.Cells[6].Text;
                string Annealing_Process = item.Cells[7].Text == "&nbsp;" ? "" : item.Cells[7].Text;
                string Annealing_Date_From = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;
                string Annealing_Date_To = item.Cells[9].Text == "&nbsp;" ? "" : item.Cells[9].Text;
                string Annealing_Temperature = item.Cells[10].Text == "&nbsp;" ? "" : item.Cells[10].Text;
                string Update_User = item.Cells[11].Text == "&nbsp;" ? "" : item.Cells[11].Text;

                //StringBuilder strURL = new StringBuilder();
                //strURL.Append("./BomDetail.aspx?");
                //strURL.Append("ParNumberAll=" + ParNumberAll);
                //strURL.Append("&MatPart01=" + MatPart01);
                //strURL.Append("&MatPart02=" + MatPart02);
                //strURL.Append("&Customer=" + Customer);
                //strURL.Append("&Model=" + Model);
                //strURL.Append("&JigNo=" + JigNo);
                //strURL.Append("&CavityCount=" + CavityCount);
                //strURL.Append("&PartsWeight=" + PartsWeight);
                //strURL.Append("&unitCount=" + Unit);
                //strURL.Append("&CycleTime=" + CycleTime);
                //strURL.Append("&UserName=" + UserName);
                #endregion

                if (e.CommandName == "Delete")
                {
                    Common.Class.Model.MouldingTransfer model = new Common.Class.Model.MouldingTransfer();
                    #region model
                    model.Material_Part = Material_Part;
                    model.Transfer_To = Transfer_To;
                    model.Transfer_Date = DateTime.Parse(Transfer_Date);
                    model.Production_Date = DateTime.Parse(Production_Date);
                    model.Quantity = Quantity;
                    model.Opr_ID = Opr_ID;
                    model.Annealing_Process = Annealing_Process;
                    model.Annealing_Temperature = Annealing_Temperature;
                    model.Annealing_Date_From = DateTime.Parse(Annealing_Date_From);
                    model.Annealing_Date_To = DateTime.Parse(Annealing_Date_To);
                    model.Update_User = Update_User;
                    #endregion

                    Session["MouldingPartsMovment_Model"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=Moulding_DeleteMouldingPartsMovement&Material_Part=" + Material_Part + "&Department=" + StaticRes.Global.Department.Moulding, false);
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "ConfirmDelete('"+ ParNumberAll + "','Delete');", true);
                    return;
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "Dg_BOMList_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string PartNumber = this.ddlPartNo.Text.Trim();
                string TransferTo = this.ddlTransferTo.Text.Trim();
                DateTime ProductionDate = Request.QueryString["ProductionDate"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["ProductionDate"].ToString());                        
                DateTime DateFrom = DateTime.Parse(this.infDchFrom.Text.Trim());
                DateTime DateTo = DateTime.Parse(this.infDchTo.Text.Trim());                

                Common.Class.BLL.MouldingPartsMovment_BLL bll = new Common.Class.BLL.MouldingPartsMovment_BLL();
                DataTable dt = bll.GetList(PartNumber, TransferTo, DateFrom, DateTo, ProductionDate);

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
                ShowWarning();
                DBHelp.Reports.LogFile.Log("ProductionReport", "btn_generate_Click Exception:" + ee.ToString());
            }
        }




        void ShowWarning()
        {
            this.lblResult.Text = "There is no record";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
            this.dg_BOMList.Visible = false;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string strURL = "./MouldingPartsMovingDetail.aspx?CommandName=Add";
            Response.Redirect(strURL, false);
        }


    }
}