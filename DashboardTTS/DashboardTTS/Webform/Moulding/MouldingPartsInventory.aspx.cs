using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingPartsInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime DateFrom = Request.QueryString["DateFrom"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    DateTime DateTo = Request.QueryString["DateTo"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateTo"].ToString());


                    this.infDchFrom.Value = DateFrom;
                    infDchFrom.CalendarLayout.SelectedDate = DateFrom;
                    this.infDchTo.Value = DateTo;
                    infDchTo.CalendarLayout.SelectedDate = DateTo;

                    this.txt_MaterialPart.Text = "";


                    btn_search_Click(new object(), new EventArgs());



                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result == "TRUE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Success');", true);
                    }
                    else if (Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Fail');", true);
                    }

                }

                this.dg_BOMList.ItemCommand += Dg_BOMList_ItemCommand;
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BomList", "Page_Load Exception:" + ee.ToString());
                ShowWarning();
            }
        }

        private void Dg_BOMList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            #region old 
            //try
            //{
            //    DataGridItem item = e.Item;

            //    if (e.CommandName == "Delete")
            //    {
            //        string Material_No = item.Cells[0].Text == "&nbsp;" ? "" : item.Cells[0].Text;
            //        string Material_LotNo = item.Cells[1].Text == "&nbsp;" ? "" : item.Cells[1].Text;
            //        string LoadDate = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;

            //        string InventoryWeight = item.Cells[3].Text == "&nbsp;" ? "" : item.Cells[3].Text;


            //        Common.Class.Model.Material_Inventory model = new Common.Class.Model.Material_Inventory();
            //        model.Material_No = Material_No;
            //        model.Material_LotNo = Material_LotNo;
            //        model.Load_Time = DateTime.Parse(LoadDate);
            //        model.Last_Event = "Delete";
            //        model.Inventory_Weight = decimal.Parse(InventoryWeight);
            //        model.Transaction_Weight = decimal.Parse(InventoryWeight);



            //        Session["Material_Inventory_Model"] = model;


            //        string strUrl = "../Laser/Login.aspx?commandType=DeleteMaterial&Department=Moulding";

            //        Response.Redirect(strUrl, false);
            //        return;
            //    }

            //}
            //catch (Exception ee)
            //{
            //    DBHelp.Reports.LogFile.Log("MouldingPartsInventory", "Dg_BOMList_ItemCommand Exception:" + ee.ToString());
            //}
            #endregion

            try
            {
                DataGridItem item = e.Item;

                if (e.CommandName == "Delete")
                {
                    string Material_No = item.Cells[0].Text == "&nbsp;" ? "" : item.Cells[0].Text;
                    string Material_LotNo = item.Cells[3].Text == "&nbsp;" ? "" : item.Cells[3].Text;
                    string LoadDate = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;

                  

                    string InventoryWeight = item.Cells[5].Text == "&nbsp;" ? "" : item.Cells[5].Text;
                    InventoryWeight = InventoryWeight.Substring(0, InventoryWeight.IndexOf(" kg"));

                    Common.Class.Model.Material_Inventory model = new Common.Class.Model.Material_Inventory();
                    model.Material_No = Material_No;
                    model.Material_LotNo = Material_LotNo;
                    model.Load_Time = DateTime.Parse(LoadDate);
                    model.Last_Event = "Delete";
                    model.Inventory_Weight = decimal.Parse(InventoryWeight);
                    model.Transaction_Weight = decimal.Parse(InventoryWeight);
                    
                    Session["Material_Inventory_Model"] = model;


                    string strUrl = "../Laser/Login.aspx?commandType=DeleteMaterial&Department=Moulding";
                    Response.Redirect(strUrl, false);
                    return;
                }
                else if (e.CommandName == "LinkMaterial_No")
                {
                    string MaterialPart = item.Cells[0].Text;
                    this.txt_MaterialPart.Text = MaterialPart;

                    btn_search_Click(new object() { }, new EventArgs() { });
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingPartsInventory", "Dg_BOMList_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string materialPart = this.txt_MaterialPart.Text.Trim();
                bool IsShowDetail = this.ddl_Detail.SelectedValue == "Yes" ? true : false;
                string Event = this.ddlEventList.SelectedValue;

                Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();
                DataTable dt = bll.GetList(materialPart, IsShowDetail, Event);


                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                    if (!IsShowDetail)
                    {
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);

                        this.dg_BOMList.Columns.RemoveAt(7);
                        this.dg_BOMList.Columns.RemoveAt(6);
                        this.dg_BOMList.Columns.RemoveAt(4);
                        this.dg_BOMList.Columns.RemoveAt(2);
                    }
                    else
                    {
                        this.dg_BOMList.Columns.RemoveAt(11);
                        this.dg_BOMList.Columns.RemoveAt(10);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(6);
                    }
                    this.dg_BOMList.Visible = true;
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();
                }
            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("MouldingPartsInventory", "btn_search_Click Exception:" + ee.ToString());
            }
        }


        protected void btn_HistorySearch_Click(object sender, EventArgs e)
        {
            try
            {
                string materialPart = this.txt_MaterialPart.Text.Trim();
                DateTime DateFrom = infDchFrom.CalendarLayout.SelectedDate;
                DateTime DateTo = infDchTo.CalendarLayout.SelectedDate;
                bool IsShowDetail = this.ddl_Detail.SelectedValue == "Yes" ? true : false;
                string Event = this.ddlEventList.SelectedValue;

                Common.Class.BLL.Material_Inventory_History_BLL bll = new Common.Class.BLL.Material_Inventory_History_BLL();
                DataTable dt = new DataTable();
                

                dt = bll.GetList(materialPart, DateFrom, DateTo, IsShowDetail,Event);

            
                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning();
                }
                else
                {
                   
                    if (!IsShowDetail)
                    {
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);
                        this.dg_BOMList.Columns.RemoveAt(9);

                        this.dg_BOMList.Columns.RemoveAt(7);
                        this.dg_BOMList.Columns.RemoveAt(5);
                        this.dg_BOMList.Columns.RemoveAt(4);
                        this.dg_BOMList.Columns.RemoveAt(2);
                    }
                    else
                    {
                        this.dg_BOMList.Columns.RemoveAt(14);
                        this.dg_BOMList.Columns[11].HeaderText = "Machine ID / Loan";
                    }

                    //this.dg_BOMList.Columns[2].Visible = false;

                    this.dg_BOMList.Visible = true;
                    this.dg_BOMList.DataSource = dt.DefaultView;
                    this.dg_BOMList.DataBind();

                    HideWarning();
                }

            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("MouldingPartsInventory", "btn_search_Click Exception:" + ee.ToString());
            }
        }



        protected void btn_Return_Click(object sender, EventArgs e)
        {
            string strURL = "./MouldingMaterialBatchload.aspx?CommandName=Return";
            Response.Redirect(strURL, false);
        }

        protected void btn_Unload_Click(object sender, EventArgs e)
        {
            string strURL = "./MouldingMaterialBatchUnload.aspx";
            Response.Redirect(strURL, false);
        }

        protected void btn_Load_Click(object sender, EventArgs e)
        {
            string strURL = "./MouldingMaterialBatchload.aspx";
            Response.Redirect(strURL, false);
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

        protected void Btn_Compare_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_Monthly_Click(object sender, EventArgs e)
        {

        }
    }
}