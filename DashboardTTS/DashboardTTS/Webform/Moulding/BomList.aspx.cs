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
    public partial class BomList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region  result
                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result != "")
                    {
                        if (bool.Parse(Result))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Success');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Fail');", true);
                        }
                    }
                    #endregion

                    this.txt_partNo.Text = "";

                    btn_search_Click(new object(), new EventArgs());
                }

                this.dg_BOMList.ItemCommand += Dg_BOMList_ItemCommand;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "Page_Load Exception:"+ee.ToString());
                ShowWarning();
            }
        }

        private void Dg_BOMList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string ParNumberAll = e.Item.Cells[2].Text == "&nbsp;" ? "" : e.Item.Cells[2].Text;
              

                if (e.CommandName == "Delete")
                {
                    Response.Redirect("../Laser/Login.aspx?commandType=Moulding_DeleteBom&PartNumberAll=" + ParNumberAll+"&Department="+StaticRes.Global.Department.Moulding, false);
                    return;
                }
                else if (e.CommandName == "Update")
                {

                    Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                    Common.Class.Model.MouldingBom_Model model = bll.GetModel("",ParNumberAll);


                    Session["MouldingBom_Model"] = model;

                    Response.Redirect("./BomDetail.aspx?CommandName=Update", false);
                }
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "Dg_BOMList_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string partNumber = this.txt_partNo.Text.Trim();
                Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();

                DataTable dt = bll.GetList(partNumber);

                 
                if (dt == null || dt.Rows.Count ==0)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_BOMList.Visible = true;
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();


                    Display();
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "btn_search_Click Exception:" + ee.ToString());
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            string strURL = "./BomDetail.aspx?CommandName=Add";
            Response.Redirect(strURL, false);
        }




        void Display()
        {
            
            foreach (DataGridItem item in this.dg_BOMList.Items)
            {
                string sExistFlagM01 = item.Cells[16].Text;
                string sExistFlagM02 = item.Cells[17].Text;


                if (sExistFlagM01 == "FALSE")
                {
                    item.Cells[3].ForeColor = System.Drawing.Color.Red;
                    item.Cells[3].ToolTip = string.Format("Material[{0}] not exist!", item.Cells[3].Text);
                }

                if (sExistFlagM02 == "FALSE")
                {
                    item.Cells[4].ForeColor = System.Drawing.Color.Red;
                    item.Cells[4].ToolTip = string.Format("Material[{0}] not exist!", item.Cells[4].Text);
                }
            }
        }

        


        void ShowWarning()
        {
            this.dg_BOMList.Visible = false;
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
        }
    }
}