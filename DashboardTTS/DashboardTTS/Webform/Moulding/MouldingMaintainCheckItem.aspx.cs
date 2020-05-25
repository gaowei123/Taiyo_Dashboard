using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMaintainCheckItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetDDL_CheckPeriod();

                    string commandType = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();
                    Common.Class.Model.MouldingMaintenanceInspection_Model model = new Common.Class.Model.MouldingMaintenanceInspection_Model();
                    model = (Common.Class.Model.MouldingMaintenanceInspection_Model)Session["MouldingMaintenanceInspection_Model"];

                    if (commandType == "UpdateCheckItem")
                    {
                        this.ddl_CheckPeriod.Items.FindByValue(model.CheckPeriod).Selected = true;
                        this.txt_CheckItem.Text = model.CheckItem;
                        this.txt_Inspection.Text = model.InspectionDescription;
                        this.txt_maintain.Text = model.MaintainenceDescription;

                        this.txt_CheckItem.Focus();
                    }

                    Session["MouldingMaintenanceInspection_Model"] = model;
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaintainCheckItem", "Page_Load Exception:" + ee.ToString());
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
               
                string CheckPeriod = this.ddl_CheckPeriod.SelectedValue;
                string CheckItem = this.txt_CheckItem.Text;
                string Inspection = this.txt_Inspection.Text;
                string Maintain = this.txt_maintain.Text;

              

                if (CheckPeriod == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please choose Check Period!');", true);
                    return;
                }

                if (CheckItem == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Check Item!');", true);
                    return;
                }

                string commandType = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();
                

                if (commandType == "Add")
                {
                    Common.Class.BLL.MouldingMaintainenceInspection_BLL bll = new Common.Class.BLL.MouldingMaintainenceInspection_BLL();
                    bool Exist = bll.Exist(CheckItem, CheckPeriod);
                    if (Exist)
                    {
                        this.txt_CheckItem.Text = "";
                        this.txt_CheckItem.Focus();
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('This Checking item has added!');", true);
                        return;
                    }
                }
                Inspection = Inspection == "" ? "NA" : Inspection;
                Maintain = Maintain == "" ? "NA" : Maintain;

               

                Common.Class.Model.MouldingMaintenanceInspection_Model model = new Common.Class.Model.MouldingMaintenanceInspection_Model();
                if (Session["MouldingMaintenanceInspection_Model"] != null)
                {
                    model = (Common.Class.Model.MouldingMaintenanceInspection_Model)Session["MouldingMaintenanceInspection_Model"];
                }
              
                model.CheckPeriod = CheckPeriod;
                model.CheckItem = CheckItem;
                model.InspectionDescription = Inspection;
                model.MaintainenceDescription = Maintain;

                if (CheckPeriod == "Daily")
                {
                    model.No = 0;
                }
                else if (CheckPeriod == "Weekly")
                {
                    model.No = 1;
                }
                else if (CheckPeriod == "Monthly")
                {
                    model.No = 2;
                }
                else if (CheckPeriod == "Halfyear")
                {
                    model.No = 3;
                }
                else if (CheckPeriod == "Quarter")
                {
                    model.No = 4;
                }
                else if (CheckPeriod == "Yearly")
                {
                    model.No = 5;
                }
                

                Session["MouldingMaintenanceInspection_Model"] = model;

            

                Response.Redirect("../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Moulding + "&commandType="+ commandType,false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaintainCheckItem", "btn_submit_Click Exception:" + ee.ToString());
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "", "Cancel();", true);
        }


        //===================== function =====================//
        private void SetDDL_CheckPeriod()
        {
            this.ddl_CheckPeriod.Items.Clear();

            Common.Class.BLL.MouldingMaintain_His_BLL bll = new Common.Class.BLL.MouldingMaintain_His_BLL();
            List<string> list_CheckPeriod = bll.GetCheckPeriod();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";

            this.ddl_CheckPeriod.Items.Add(Li);

            if (list_CheckPeriod != null)
            {
                foreach (string str in list_CheckPeriod)
                {
                    Li = new ListItem();
                    Li.Text = str;
                    Li.Value = str;

                    this.ddl_CheckPeriod.Items.Add(Li);
                }
            }
        }

    }
}