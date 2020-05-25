using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class Buy_Off_List : System.Web.UI.Page
    {      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.txtDateFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    
                    btn_Search_Click(new object(), new EventArgs());
                    
                    #region Result
                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result == "TRUE")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Success");
                    }
                    else if (Result == "FALSE")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Fail");
                    }
                    #endregion
                }

                this.dg_Buyoff_List.ItemCommand += Dg_Buyoff_List_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Buy_Off_List", "Buy off List Page Page_Load --" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_Buyoff_List, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        private void Dg_Buyoff_List_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "ShowBuyoffReport")
            {
                string JobNumber = e.Item.Cells[2].Text;

                Response.Redirect("./Add_BUY_OFF.aspx?JobNumber="+ JobNumber, false);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string sJobNo = this.txtJobNo.Text.Trim();
            string sMachineNo = this.ddlMachineNo.SelectedItem.Value;
            DateTime dDateFtom = DateTime.Parse(this.txtDateFrom.Text);
            DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);


            Common.Class.BLL.LMMSBUYOFFLIST_BLL LMMSBuyoffBll = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
            DataTable dt = LMMSBuyoffBll.GetBuyofflist(sJobNo, "", sMachineNo, string.Empty, string.Empty, string.Empty, dDateFtom, dDateTo);
            
            if (dt == null || dt.Rows.Count == 0)
            {
                Common.CommFunctions.ShowWarning(lblResult, dg_Buyoff_List, StaticRes.Global.ErrorLevel.Warning, "");
            }
            else
            {
                this.dg_Buyoff_List.DataSource = dt.DefaultView;
                this.dg_Buyoff_List.DataBind();
                Common.CommFunctions.HideWarning(lblResult, dg_Buyoff_List);
            }



            #region set column
            string type = "1st";
            switch (type)
            {
                case "1st":
                    Show1st();
                    Hide2nd();
                    HideInpr();
                    break;

                case "2nd":
                    Hide1st();
                    Show2nd();
                    HideInpr();
                    break;

                case "Inprocess":
                    Hide1st();
                    Hide2nd();
                    ShowInpr();
                    break;

                case "ALL":
                    Show1st();
                    Show2nd();
                    ShowInpr();
                    break;
            }
            #endregion

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Add_BUY_OFF.aspx", false); 
        }

        private void Show1st()
        {
            this.dg_Buyoff_List.Columns[10].Visible = true;
            this.dg_Buyoff_List.Columns[11].Visible = true;
            this.dg_Buyoff_List.Columns[12].Visible = true;
            this.dg_Buyoff_List.Columns[13].Visible = true;
            this.dg_Buyoff_List.Columns[14].Visible = true;
            this.dg_Buyoff_List.Columns[15].Visible = true;
            this.dg_Buyoff_List.Columns[16].Visible = true;
        }

        private void Hide1st()
        {
            this.dg_Buyoff_List.Columns[10].Visible = false;
            this.dg_Buyoff_List.Columns[11].Visible = false;
            this.dg_Buyoff_List.Columns[12].Visible = false;
            this.dg_Buyoff_List.Columns[13].Visible = false;
            this.dg_Buyoff_List.Columns[14].Visible = false;
            this.dg_Buyoff_List.Columns[15].Visible = false;
            this.dg_Buyoff_List.Columns[16].Visible = false;
        }

        private void Show2nd()
        {
            this.dg_Buyoff_List.Columns[17].Visible = true;
            this.dg_Buyoff_List.Columns[18].Visible = true;
            this.dg_Buyoff_List.Columns[19].Visible = true;
            this.dg_Buyoff_List.Columns[20].Visible = true;
            this.dg_Buyoff_List.Columns[21].Visible = true;
            this.dg_Buyoff_List.Columns[22].Visible = true;
            this.dg_Buyoff_List.Columns[23].Visible = true;
        }

        private void Hide2nd()
        {
            this.dg_Buyoff_List.Columns[17].Visible = false;
            this.dg_Buyoff_List.Columns[18].Visible = false;
            this.dg_Buyoff_List.Columns[19].Visible = false;
            this.dg_Buyoff_List.Columns[20].Visible = false;
            this.dg_Buyoff_List.Columns[21].Visible = false;
            this.dg_Buyoff_List.Columns[22].Visible = false;
            this.dg_Buyoff_List.Columns[23].Visible = false;
        }

        private void ShowInpr()
        {
            this.dg_Buyoff_List.Columns[24].Visible = true;
            this.dg_Buyoff_List.Columns[25].Visible = true;
            this.dg_Buyoff_List.Columns[26].Visible = true;
            this.dg_Buyoff_List.Columns[27].Visible = true;
            this.dg_Buyoff_List.Columns[28].Visible = true;
            this.dg_Buyoff_List.Columns[29].Visible = true;
            this.dg_Buyoff_List.Columns[30].Visible = true;
        }

        private void HideInpr()
        {
            this.dg_Buyoff_List.Columns[24].Visible = false;
            this.dg_Buyoff_List.Columns[25].Visible = false;
            this.dg_Buyoff_List.Columns[26].Visible = false;
            this.dg_Buyoff_List.Columns[27].Visible = false;
            this.dg_Buyoff_List.Columns[28].Visible = false;
            this.dg_Buyoff_List.Columns[29].Visible = false;
            this.dg_Buyoff_List.Columns[30].Visible = false;
        }
    }
}