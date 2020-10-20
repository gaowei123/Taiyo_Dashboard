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
    public partial class InventoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.Response.Cache.SetNoStore();
                    this.txtPartNo.Focus();

                    txtPartNo_TextChanged(new object(), new EventArgs());
                }

                this.dg_inventoryDetail.ItemCommand += Dg_inventoryDetail_ItemCommand;
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " Page_Load error -- "+ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        private void Dg_inventoryDetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "LinkJobDetail")
                {
                    TableRow item1 = e.Item;
                    string partNo = "";
                    string Jobnumber = "";

                    if (item1.Cells[2].Text != "&nbsp;")
                    {
                        partNo = item1.Cells[2].Text;
                    }
                    if (item1.Cells[4].Text != "JOT###")
                    {
                        Jobnumber = item1.Cells[4].Text;
                    }
                 
                    string str_url = "./InventoryDetail.aspx?";
                    str_url += "&Partnumber=" + partNo;
                    str_url += "&Jobnumber=" + Jobnumber;


                    Response.Redirect(str_url.ToString(),false);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "Dg_inventoryDetail_ItemCommand error--" + ee.Message);
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }
     
        

      

        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Common.Class.BLL.LMMSInventoty_BLL inventory_bll = new Common.Class.BLL.LMMSInventoty_BLL();
                string sPartNumber = txtPartNo.Text;
                string sCustomer = "";


                DataTable dt = new DataTable();// inventory_bll.Report(sPartNumber, sCustomer);
                if (dt == null)
                {
                    Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {
                    dg_inventoryDetail.DataSource = dt.DefaultView;
                    dg_inventoryDetail.DataBind();

                    Common.CommFunctions.HideWarning(lblResult, dg_inventoryDetail);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " txtPartNo_TextChanged error -- " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }
    }
}