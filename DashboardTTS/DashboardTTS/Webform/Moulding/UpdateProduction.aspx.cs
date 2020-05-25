using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Molding
{
    public partial class UpdateProduction : System.Web.UI.Page
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
                        string sJigNo = ((TextBox)Ditem.Cells[21].FindControl("txt_JigNo")).Text;
                        string sOutPut = ((TextBox)Ditem.Cells[22].FindControl("txt_OutPut")).Text;
                        string sOK = ((TextBox)Ditem.Cells[23].FindControl("txt_OK")).Text;
                        string sNG = ((TextBox)Ditem.Cells[24].FindControl("txt_NG")).Text;

                        
                        string[] sTemp = item.Cells[1].Text.Split('-');
                        string Day = sTemp[2] + "-" + sTemp[1] + "-" + sTemp[0];

                        string Shift = Ditem.Cells[2].Text;
                        if (sJigNo == "" && sOutPut == "" && sOK == "" && sNG == "")
                        {
                            bstatus = false;
                        }
                        if (bstatus)
                        {
                            Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                            model.PartNumberAll = Ditem.Cells[6].Text;
                            model.PartNumber = Ditem.Cells[7].Text;
                            if (sJigNo != "")
                            {
                                model.JigNo = sJigNo;
                            }
                            else
                            {
                                model.JigNo = Ditem.Cells[9].Text;
                            }
                            if (sOutPut != "")
                            {
                                model.AcountReading =  double.Parse(sOutPut);
                            }
                            else
                            {
                                model.AcountReading = double.Parse(Ditem.Cells[16].Text) + double.Parse(Ditem.Cells[17].Text)+ double.Parse(Ditem.Cells[19].Text); 
                            }
                            if (sOK != "")
                            {
                                model.AcceptQty = double.Parse(sOK);
                            }
                            else
                            {
                                model.AcceptQty = double.Parse(Ditem.Cells[16].Text);
                            }
                            if (sNG != "")
                            {
                                model.RejectQty = double.Parse(sNG);
                            }
                            else
                            {
                                model.RejectQty = double.Parse(Ditem.Cells[17].Text);
                            }
                            model.MachineID = Ditem.Cells[3].Text.Replace("Machine", "");
                            model.Day = Day;
                            model.Shift = Shift;
                            Lmodel.Add(model);
                        }
                    }
                    Session["MouldingViHistory_Model"] = Lmodel;

                    string strURL = "../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Moulding + "&commandType=UpdateProduction";

                    Response.Redirect(strURL, false);
                }
                else if (e.CommandName == "LinkPartNumber")
                {
                    this.txt_PartNo.Text = PartNumber;

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
                else if (e.CommandName == "Delete")
                {
                    List<Common.Class.Model.MouldingViHistory_Model> Lmodel = new List<Common.Class.Model.MouldingViHistory_Model>();

                   

                    string[] sTemp = item.Cells[1].Text.Split('-');
                    string Day = sTemp[2] + "-" + sTemp[1] + "-" + sTemp[0];

                    string Shift = item.Cells[2].Text;


                    Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                    model.PartNumberAll = item.Cells[6].Text;
                    model.PartNumber = item.Cells[7].Text;
                    model.AcceptQty = double.Parse(item.Cells[16].Text);
                    model.RejectQty = double.Parse(item.Cells[17].Text);
                    model.AcountReading = double.Parse(item.Cells[16].Text) + double.Parse(item.Cells[17].Text) + double.Parse(item.Cells[19].Text);
                    model.MachineID = item.Cells[3].Text.Replace("Machine", "");
                    model.Day = Day;
                    model.Shift = Shift;
                    Lmodel.Add(model);

                    Session["MouldingViHistory_Model"] = Lmodel;

                    string strURL = "../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Moulding + "&commandType=DeleteProduction";

                    Response.Redirect(strURL, false);
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
                Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                DateTime dTimeFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dTimeTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNo = this.txt_PartNo.Text;
                string Shift = this.ddl_Shift.SelectedValue;
                string Module = this.txt_module.Text;


                DataTable dt = bll.UpdateProductionReport(dTimeFrom, dTimeTo, MachineID, PartNo, Shift, Module);

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
                this.dg_Report.Columns[22].Visible = true;
                this.dg_Report.Columns[23].Visible = true;
                this.dg_Report.Columns[24].Visible = true;
                this.dg_Report.Columns[25].Visible = true;
                this.dg_Report.Columns[26].Visible = true;
                this.dg_Report.Columns[27].Visible = true;
                this.dg_Report.Columns[28].Visible = true;
                this.btn_Update.Text = "Hide";

            }
            else if (buttonText == "Hide")
            {
                this.dg_Report.Columns[22].Visible = false;
                this.dg_Report.Columns[23].Visible = false;
                this.dg_Report.Columns[24].Visible = false;
                this.dg_Report.Columns[25].Visible = false;
                this.dg_Report.Columns[26].Visible = false;
                this.dg_Report.Columns[27].Visible = false;
                this.dg_Report.Columns[28].Visible = false;
                this.btn_Update.Text = "Update";
            }
        }
    }
}