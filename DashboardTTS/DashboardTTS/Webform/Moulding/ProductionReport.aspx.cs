using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Molding
{
    public partial class ProductionReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //init page
                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    string PartNumber = Request.QueryString["PartNumber"] == null ? "" : Request.QueryString["PartNumber"].ToString();
                    string Module = Request.QueryString["Module"] == null ? "" : Request.QueryString["Module"].ToString();
                    DateTime DateFrom = Request.QueryString["DateFrom"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    DateTime DateTo = Request.QueryString["DateTo"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateTo"].ToString());


                    if (MachineID != "")
                    {
                        this.ddlMachineID.Items.FindByValue(MachineID).Selected = true;
                    }
                    this.txt_PartNo.Text = PartNumber != "" ? PartNumber : "";
                    this.txt_module.Text = Module != "" ? Module : "";

                    this.infDchFrom.Value = DateFrom;
                    infDchFrom.CalendarLayout.SelectedDate = DateFrom;
                    this.infDchTo.Value = DateTo;
                    infDchTo.CalendarLayout.SelectedDate = DateTo;

                    HideWarning();

                    btn_generate_Click(new object() { }, new EventArgs() { });

                    #region update setup , wastage material result
                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result.ToUpper() == "TRUE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Success!');", true);
                    }
                    else if (Result.ToUpper() == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Fail!');", true);
                    }
                    #endregion
                }

                this.dg_Report.ItemCommand += Dg_Report_ItemCommand;

                
            }
            catch (Exception ee)
            {
                ShowWarning();
                DBHelp.Reports.LogFile.Log("ProductionReport", "Load Page Exception:" + ee.ToString());
            }
        }

        private void Dg_Report_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataGridItem item = e.Item;
                string IsTotalRow = item.Cells[1].Text;
                string MachineID = item.Cells[3].Text;
                string Model = item.Cells[4].Text;
                string PartNumberAll = item.Cells[6].Text;                

                string PartNumber = item.Cells[7].Text;

                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate.Date.AddHours(8);


                if (e.CommandName == "Link" && IsTotalRow != "Total :")
                {
                    string strURL = "./RejDetail.aspx?MachineID=" + MachineID + "&Model=" + Model + "&PartNumber=" + PartNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
                    Response.Redirect(strURL, false);
                }
                else if (e.CommandName == "Update")
                {
                    List<Common.Class.Model.MouldingViHistory_Model> Lmodel = new List<Common.Class.Model.MouldingViHistory_Model>();
                    foreach (DataGridItem Ditem in dg_Report.Items)
                    {
                        

                        if (Ditem.ItemType == ListItemType.Footer || Ditem.ItemType == ListItemType.Header)
                        {
                            continue;
                        }


                        bool bstatus = true;


                        string sSetup = ((TextBox)Ditem.Cells[21].FindControl("txt_Setup")).Text;
                        string sWasteMaterial01 = ((TextBox)Ditem.Cells[22].FindControl("txt_WasteMaterial01")).Text;
                        string sWasteMaterial02 = ((TextBox)Ditem.Cells[23].FindControl("txt_WasteMaterial02")).Text;

                        string Day = Ditem.Cells[1].Text;
                        string Shift = Ditem.Cells[2].Text;
                        if (sSetup == "" && sWasteMaterial01 == "" && sWasteMaterial02 == "")
                        {
                            bstatus = false;
                        }

                        if (bstatus)
                        {
                            // List<Common.Class.Model.MouldingViHistory_Model> Lmodel = new List<Common.Class.Model.MouldingViHistory_Model>();
                            Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                            model.PartNumberAll = Ditem.Cells[6].Text;
                            model.PartNumber = Ditem.Cells[7].Text;
                            model.Setup = sSetup == "" ? 0 : double.Parse(sSetup);
                            model.WastageMaterial01 = sWasteMaterial01 == "" ? 0 : double.Parse(sWasteMaterial01);
                            model.WastageMaterial02 = sWasteMaterial02 == "" ? 0 : double.Parse(sWasteMaterial02);
                            model.MachineID = Ditem.Cells[3].Text.Replace("Machine", "");
                            model.Day = Day;
                            model.Shift = Shift;
                            Lmodel.Add(model);
                            //Session["MouldingViHistory_Model"] = model;
                        }
                    }

                    Session["MouldingViHistory_Model"] = Lmodel;
                    //string sSetup = ((TextBox)item.Cells[19].FindControl("txt_Setup")).Text;
                    //string sWasteMaterial01 = ((TextBox)item.Cells[20].FindControl("txt_WasteMaterial01")).Text;
                    //string sWasteMaterial02 = ((TextBox)item.Cells[21].FindControl("txt_WasteMaterial02")).Text;
                    //string Day = item.Cells[1].Text;
                    //string Shift = item.Cells[2].Text;

                    #region check text box value validation
                    //if (sSetup == "" && sWasteMaterial01 == "" && sWasteMaterial02== "")
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input setup or Wasted Material');", true);
                    //    return;
                    //}

                    //if (!Common.CommFunctions.isNumberic(sSetup) && sSetup != "")
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Must input number in setup!');", true);
                    //    return;
                    //}

                    //if (!Common.CommFunctions.isNumberic(sWasteMaterial01) && sWasteMaterial01 != "")
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Must input number in Wasted Material!');", true);
                    //    return;
                    //}

                    //if (!Common.CommFunctions.isNumberic(sWasteMaterial02) && sWasteMaterial02 != "")
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Must input number in Wasted Material!');", true);
                    //    return;
                    //}
                    #endregion



                    //Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                    //model.PartNumberAll = PartNumberAll;
                    //model.PartNumber = PartNumber;
                    //model.Setup = sSetup == "" ? 0 : double.Parse(sSetup);
                    //model.WastageMaterial01 = sWasteMaterial01 == "" ? 0 : double.Parse(sWasteMaterial01);
                    //model.WastageMaterial02 = sWasteMaterial02 == "" ? 0 : double.Parse(sWasteMaterial02);
                    //model.MachineID = MachineID.Replace("Machine", "");
                    //model.Day = Day;
                    //model.Shift = Shift;

                    //Session["MouldingViHistory_Model"] = model;

                    string strURL = "../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Moulding + "&commandType=UpdateSetUp";

                    Response.Redirect(strURL, false);
                }
                else if (e.CommandName == "LinkPartNumber")
                {
                    this.txt_PartNo.Text = PartNumber;

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }


            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ProductionReport", "Dg_Report_ItemCommand Exception:" + ee.ToString());
            }
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {

                //DBHelp.Reports.LogFile.Log("ProductionReport_debug", "==== step 1 ====");


                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                DateTime dTimeFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dTimeTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNo = this.txt_PartNo.Text;
                string Shift = this.ddl_Shift.SelectedValue;
                string Module = this.txt_module.Text;


                //DBHelp.Reports.LogFile.Log("ProductionReport_debug", "==== step 2 ====");

                DataTable dt = bll.ProductionReport(dTimeFrom, dTimeTo, MachineID, PartNo, Shift, Module);

                if (dt == null || dt.Rows.Count <= 1)
                {
                    ShowWarning();
                }
                else
                {
                    this.dg_Report.Visible = true;
                    this.dg_Report.DataSource = dt.DefaultView;
                    this.dg_Report.DataBind();
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
            this.dg_Report.Visible = false;
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

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string buttonText = this.btn_Update.Text;
            if (buttonText == "Update")
            {
                this.dg_Report.Columns[21].Visible = true;
                this.dg_Report.Columns[22].Visible = true;
                this.dg_Report.Columns[23].Visible = true;
                this.dg_Report.Columns[24].Visible = true;
                //this.dg_Report.Columns[26].Visible = true;
                this.btn_Update.Text = "Hide";

            }
            else if (buttonText == "Hide")
            {
                this.dg_Report.Columns[21].Visible = false;
                this.dg_Report.Columns[22].Visible = false;
                this.dg_Report.Columns[23].Visible = false;
                this.dg_Report.Columns[24].Visible = false;
                //this.dg_Report.Columns[26].Visible = false;
                this.btn_Update.Text = "Update";
            }
        }
    }
}