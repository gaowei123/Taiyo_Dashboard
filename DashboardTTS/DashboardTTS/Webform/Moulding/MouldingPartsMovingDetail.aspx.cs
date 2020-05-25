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
    public partial class MouldingPartsMovingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)

                {

                    #region get url paras
                    string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();

                    #endregion

                    SetChaseID();

                    string sChaseID = "";
                    try
                    {
                        sChaseID = Session["ChaseID"].ToString().Trim();
                        this.ddlPartNo.SelectedIndex = this.ddlPartNo.Items.IndexOf(this.ddlPartNo.Items.FindByText(sChaseID));
                    }
                    catch (Exception ex)
                    {
                        this.ddlPartNo.SelectedIndex = 0;
                    }

                    if (CommandName == "Add")
                    {
                        #region  init ui

                        #endregion
                    }
                    else
                    {
                        #region  init ui

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
                string CommandName = Request.QueryString["CommandName"] == null ? "" : Request.QueryString["CommandName"].ToString();
                //check textbox
                bool result = TextBoxCheckList();
                if (!result)
                {
                    return;
                }

                Common.Class.Model.MouldingTransfer model = new Common.Class.Model.MouldingTransfer();
                #region model
                model.Material_Part = this.ddlPartNo.Text.Trim();
                model.Transfer_To = this.ddlTransferTo.Text.Trim();
                model.Transfer_Date = DateTime.Parse(this.infTransferDate.Text.Trim());
                model.Production_Date = DateTime.Parse(this.infProductionDate.Text.Trim());
                model.Quantity = this.txt_Quantity.Text.Trim();
                model.Opr_ID = this.txt_OprID.Text.Trim();
                model.Annealing_Process = this.AnnealingProcesslist.Text.Trim();
                if (model.Annealing_Process == "Yes")
                {
                    model.Annealing_Temperature = this.AnnealingTemperaturelist.Text.Trim();
                    model.Annealing_Date_From = DateTime.Parse(this.txt_AnnealingTransferFrom.Text.Trim());
                    model.Annealing_Date_To = DateTime.Parse(this.txt_AnnealingTransferTo.Text.Trim());
                }
                #endregion

                if (CommandName == "Add")
                {
                    Session["MouldingPartsMovment_Model"] = model;
                    Response.Redirect("../Laser/Login.aspx?commandType=Moulding_AddMouldingPartsMovement&Department=" + StaticRes.Global.Department.Moulding, false);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingPartsMovementDetail", "btn_submit_Click Exception" + ee.ToString());
            }
        }

        private void SetChaseID()
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

            this.ddlTransferTo.Items.Clear();
            this.ddlTransferTo.Items.Add("Painting Dept");
            this.ddlTransferTo.Items.Add("Silkprint");
            this.ddlTransferTo.Items.Add("Summer Weli");
            this.ddlTransferTo.Items.Add("store");

            this.AnnealingProcesslist.Items.Clear();
            this.AnnealingProcesslist.Items.Add("Yes");
            this.AnnealingProcesslist.Items.Add("No");

            this.AnnealingTemperaturelist.Items.Clear();
            this.AnnealingTemperaturelist.Items.Add("80");
            this.AnnealingTemperaturelist.Items.Add("90");
            this.AnnealingTemperaturelist.Items.Add("100");
            this.AnnealingTemperaturelist.Items.Add("120");
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MouldingPartsMoving.aspx", false);
        }


        private bool TextBoxCheckList()
        {
            decimal _sValue = 0;
            DateTime dDadte = new DateTime();
            if (this.ddlPartNo.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input PartNumber!');", true);
                this.ddlPartNo.Focus();
                return false;
            }

            if (this.txt_OprID.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input OP ID!');", true);
                this.txt_OprID.Focus();
                return false;
            }

            if (this.txt_Quantity.Text.Trim() == "" || !decimal.TryParse(this.txt_Quantity.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Quantity !');", true);
                this.txt_Quantity.Focus();
                return false;
            }

            bool IsShowDetail = this.AnnealingProcesslist.SelectedValue == "1" ? true : false;
            if (IsShowDetail)
            {
                if (this.txt_AnnealingTransferFrom.Text.Trim() == "" || !DateTime.TryParse(this.txt_AnnealingTransferFrom.Text.Trim(), out dDadte))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input DateTime Annealing Transfer From!');", true);
                    this.txt_AnnealingTransferFrom.Focus();
                    return false;
                }

                if (this.txt_AnnealingTransferTo.Text.Trim() == "" || !DateTime.TryParse(this.txt_AnnealingTransferTo.Text.Trim(), out dDadte))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input DateTime Annealing Transfer From!');", true);
                    this.txt_AnnealingTransferTo.Focus();
                    return false;
                }
            }

            return true;
        }


        void AlertMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + message + "');", true);
        }

        protected void AnnealingProcesslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool IsShowDetail = this.AnnealingProcesslist.SelectedValue == "Yes" ? true : false;
            if(IsShowDetail)
            {
                this.AnnealingTemperaturelist.Enabled = true;
                this.txt_AnnealingTransferFrom.Enabled = true;
                this.txt_AnnealingTransferTo.Enabled = true;
            }
            else
            {
                this.AnnealingTemperaturelist.Enabled = false;
                this.txt_AnnealingTransferFrom.Enabled = false;
                this.txt_AnnealingTransferTo.Enabled = false;
            }
        }
    }
}