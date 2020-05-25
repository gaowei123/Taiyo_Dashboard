using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace DashboardTTS.Webform.Molding
{
    public partial class MoldLife : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //SetMouldID();
                    this.btnOK.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.txt_Remarks.Enabled = false;
                    this.txt_Remarks.Text = "";
                    SetCleanItem();
                    this.ddl_CleaningItem.Enabled = false;
                    this.ddl_OutSideRepair.Enabled = false;  
                    this.ddl_OutSideRepair.SelectedIndex = 0;
                   

                    SetChaseID();

                    string sChaseID = "";
                    try
                    {   
                        sChaseID = Session["ChaseID"].ToString().Trim() ;
                        this.ddl_MouldChase.SelectedIndex = this.ddl_MouldChase.Items.IndexOf(this.ddl_MouldChase.Items.FindByText(sChaseID));
                    }
                    catch
                    {
                        this.ddl_MouldChase.SelectedIndex = 0;
                    }

                    ShowCurrentList(sChaseID);

                    ShowHistoryList(sChaseID);
                     



                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();

                    if (Result== "TRUE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Success');", true);
                    }
                    else if (Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Fail');", true);
                    }
                }

                this.dg_MoldLife.ItemCommand += Dg_MoldLife_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ChangeModel", "Page_Load Exception:" + ee.ToString());
            }
        }

        private void Dg_MoldLife_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName== "submitMouldLife")
            {
                string sMouldID = e.Item.Cells[0].Text;
                string sMouldLife = ((TextBox)e.Item.Cells[1].FindControl("txtMouldLife")).Text;

                if (sMouldLife.Trim() == "" || !Common.CommFunctions.isNumberic(sMouldLife))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please input number!");
                    return;
                }


                Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();
                bool result = bll.UpdateMouldLife(sMouldID, int.Parse(sMouldLife));

                if (result)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "SUCCESS!");
                }
                else
                {
                    Common.CommFunctions.ShowMessage(this.Page, "FAIL!");
                }
            }
           
        }

        private void SetChaseID()
        {
            this.ddl_MouldChase.Items.Clear();
            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();

            List<string> ListPart = new List<string>();
            ListPart = bll.GetMouldID("");

            ListItem Li = new ListItem();
            Li.Text = "Back to main page";
            Li.Value = "";
            this.ddl_MouldChase.Items.Add(Li);

            if (ListPart != null)
            {
                foreach (string ID in ListPart)
                {
                    Li = new ListItem();
                    Li.Text = ID;
                    Li.Value = ID;
                    this.ddl_MouldChase.Items.Add(Li);
                }
            }
        }
        private void SetCleanItem()
        {
            this.ddl_CleaningItem.Items.Clear();

            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();

            List<string> ListCleanItem = new List<string>();
            ListCleanItem = bll.GetCheackCleanList();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";
            this.ddl_CleaningItem.Items.Add(Li);


            if (ListCleanItem != null)
            {
                foreach (string ID in ListCleanItem)
                {
                    Li = new ListItem();
                    Li.Text = ID;
                    Li.Value = ID;
                    this.ddl_CleaningItem.Items.Add(Li);
                }
            }
        }
        private void ShowCurrentList(string sChaseID)
        {
            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();
            DataTable dt = bll.GetList(sChaseID, "");

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowWarning(this.lblResult_ModeLife, dg_MoldLife, "");
            }
            else
            {
                this.dg_MoldLife.DataSource = dt.DefaultView;
                this.dg_MoldLife.DataBind();
                SetDataGridStyle(dg_MoldLife);
                HideWarning(this.lblResult_ModeLife,this.dg_MoldLife);
            }
        }


        void SetDataGridStyle(DataGrid dg_MoldLife)
        {
            int dgDayRowCount = dg_MoldLife.Items.Count;
            for (int i = 0; i < dgDayRowCount; i++)
            {
                string sMouldLife = dg_MoldLife.Items[i].Cells[2].Text;
                string sAccumulate = dg_MoldLife.Items[i].Cells[3].Text;
                string sClean1Qty = dg_MoldLife.Items[i].Cells[4].Text;


                int MouldLife = sMouldLife == "&nbsp;" ? 0:  int.Parse(sMouldLife);
                int Accumulate = sAccumulate == "&nbsp;" ? 0 : int.Parse(sAccumulate);
                int Clean1Qty = sClean1Qty == "&nbsp;" ? 0 : int.Parse(sClean1Qty);

                ((TextBox)dg_MoldLife.Items[i].Cells[2].FindControl("txtMouldLife")).Text = MouldLife.ToString();

                if (Accumulate > 500000 || Clean1Qty > 45000)
                    dg_MoldLife.Items[i].Cells[0].ForeColor = System.Drawing.Color.Red;
            }
        }


        private void ShowHistoryList(string sChaseID)
        {
            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();

            DataTable dt = bll.GetHistoryList(sChaseID, "");


            if (dt == null || dt.Rows.Count == 0)
            {
                ShowWarning(this.lblResult_HisList, this.dg_HisList, "");
            }
            else
            {
                this.dg_HisList.DataSource = dt.DefaultView;
                this.dg_HisList.DataBind();
                HideWarning(this.lblResult_HisList, this.dg_HisList);
            }
        }

        private void ShowWarning(Label lb, DataGrid dg, string Message)
        {
            dg.Visible = false;

            lb.Text = "There is no record!";

            if (Message != "")
            {
                lb.Text = Message;
            }
            
            lb.BackColor = System.Drawing.Color.Red;
            lb.Visible = true;
        }

        private void HideWarning(Label lb, DataGrid dg)
        {
            lb.Visible = false;
            dg.Visible = true;
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            string sChaseID = "";
            sChaseID = ddl_MouldChase.SelectedValue;
            Session["ChaseID"] = sChaseID;
            ShowCurrentList(sChaseID);
            

            ShowHistoryList(sChaseID);
         }


        protected void btn_Clean_Click(object sender, EventArgs e)
        {
            try
            {
                string sChaseID = "";
                try
                {
                    sChaseID = Session["ChaseID"].ToString();
                }
                catch (Exception ex)
                {
                    sChaseID = "";
                }

                if (sChaseID == "")
                {
                    ShowWarning(this.lblResult_ModeLife, dg_MoldLife, "Please select one Chase ID and generate the data first!");
                    ShowWarning(this.lblResult_HisList, dg_HisList, "Please select one Chase ID and generate the data first!");
                    return;
                }
                Common.Class.BLL.MouldingMoldLife_BLL bllMoldLife = new Common.Class.BLL.MouldingMoldLife_BLL();
                Common.Class.Model.MouldingMoldLife_Model model = new Common.Class.Model.MouldingMoldLife_Model();
                model = bllMoldLife.GetModelbyChaseID(sChaseID);
                if (model == null)
                {
                    this.ddl_MouldChase.SelectedIndex = 0;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Do not have this Chase ID information!');", true);
                    return;
                }

                if (model.MachineID != "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Machine is still running this production! Please end this Part No. first!');", true);
                    return;
                }

                Session["MouldingMoldLife_CLean_Model"] = model;

                this.btnCancel.Enabled = true;
                this.btnOK.Enabled = true;
                this.ddl_CleaningItem.Enabled = true;
                this.ddl_CleaningItem.SelectedIndex = 0;


                this.ddl_OutSideRepair.Enabled = true;
                this.ddl_OutSideRepair.SelectedIndex = 0;

                this.txt_Remarks.Text = "";
                this.txt_Remarks.Enabled = true;

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ChangeModel", "btn_Clean_Click Exception:" + ee.ToString());
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                #region set clean qty , clean time   no need 
                //model.Clean1Qty = 0;
                //model.Clean1Time = dTime;
                #endregion
                Common.Class.Model.MouldingMoldLife_Model model = new Common.Class.Model.MouldingMoldLife_Model();

                model = (Common.Class.Model.MouldingMoldLife_Model) Session["MouldingMoldLife_CLean_Model"]  ; 
                model.ChangeBy = ddl_CleaningItem.SelectedItem.Text;
                model.Clean5TimeBy = ddl_OutSideRepair.SelectedItem.Text;
                model.Clean4TimeBy = txt_Remarks.Text.Trim();
                Session["MouldingMoldLife_CLean_Model"] = model;

                this.btnOK.Enabled = false;
                this.btnCancel.Enabled = false;
                this.ddl_CleaningItem.SelectedIndex = 0;
                this.ddl_CleaningItem.Enabled = false; ;

                this.ddl_OutSideRepair.SelectedIndex = 0;
                this.ddl_OutSideRepair.Enabled = false;

                this.txt_Remarks.Text = "";
                this.txt_Remarks.Enabled = false;


                string url = "../Laser/Login.aspx?commandType=CleanMould&Department=Moulding";
                Response.Redirect(url, false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ChangeModel", "btnOK_Click Exception:" + ee.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["MouldingMoldLife_CLean_Model"] = null;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.ddl_CleaningItem.SelectedIndex = 0;
            this.ddl_CleaningItem.Enabled = false;

            this.ddl_OutSideRepair.SelectedIndex = 0;
            this.ddl_OutSideRepair.Enabled = false;

            this.txt_Remarks.Text = "";
            this.txt_Remarks.Enabled = false;


        }

       
    }
}