using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace DashboardTTS.Webform
{
    public partial class PQCBomList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                    btn_search_Click(new object(), new EventArgs());


                    #region Result

                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result =="TRUE")
                        Common.CommFunctions.ShowMessage(this.Page, "Success");
                    else if (Result == "FALSE")
                        Common.CommFunctions.ShowMessage(this.Page, "Fail");

                    #endregion

                }

                this.dg_BOMList.ItemCommand += Dg_BOMList_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCBomList", "Page_Load Exception --" + ee.ToString());
                ShowWarning(ee.ToString());
            }

            
            Common.CommFunctions.SetAutoComplete(this.Page, "#MainContent_txt_partNo", "");
        }

        

        private void Dg_BOMList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {

                string PartNo = e.Item.Cells[3].Text == "&nbsp;" ? "" : e.Item.Cells[3].Text;


                switch (e.CommandName)
                {
                    case "Delete":
               
                        string commandType = "DeleteBom";
                        string strUrl = "../Laser/Login?partNumber=" + PartNo + "&commandType=" + commandType + "&Department=" + StaticRes.Global.Department.PQC;
                        Response.Redirect(strUrl, false);
                        break;


                    case "LinkDetailPage":

                        string partNumber = e.Item.Cells[0].Text == "&nbsp;" ? "" : e.Item.Cells[0].Text;
                        Response.Redirect("./PQCBomFormMenu.aspx?partNumber=" + PartNo, false);
                        break;


                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCBomList", "Page_Load Exception --" + ee.Message);
            }
        }

      
        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string PartNo = this.txt_partNo.Text.Trim();

                Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

                DataTable dt = bll.GetList(PartNo);
                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();
                }



                foreach (DataGridItem item in this.dg_BOMList.Items)
                {
                    item.Cells[8].ToolTip = item.Cells[9].Text;
                }



            }
            catch (Exception ee)
            {
                ShowWarning(ee.ToString());
            }
        }
      



        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("./PQCBomFormMenu.aspx?buttonType=AddBom",false);
        }
        

        private void ShowWarning(string sMessage)
        {
            this.lblResult.Visible = true;
            this.dg_BOMList.Visible = false;
            this.lblResult.BackColor = System.Drawing.Color.Red;

            this.lblResult.Text = string.IsNullOrEmpty(sMessage) ? "There is no record! " : sMessage;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_BOMList.Visible = true;
        }


       
    }
}