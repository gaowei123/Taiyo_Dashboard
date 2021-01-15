using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform
{
    public partial class MouldingMaintainDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetDDL_CheckPeriod();

                    btn_generate_Click(new object(), new EventArgs());

                    HideWarning();
                }

                this.dg_CheckItemList.ItemCommand += Dg_CheckItemList_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaintainDetail", "Page_Load Exception:" + ee.ToString());
                ShowWarning(ee.ToString());
            }
        }

        private void Dg_CheckItemList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string CheckPeriod = item.Cells[0].Text == "&nbsp;" ? "" : item.Cells[0].Text;
            string CheckItem = item.Cells[1].Text == "&nbsp;" ? "" : item.Cells[1].Text;


            Common.Class.Model.MouldingMaintenanceInspection_Model model = new Common.Class.Model.MouldingMaintenanceInspection_Model();
            Common.Class.BLL.MouldingMaintainenceInspection_BLL bll = new Common.Class.BLL.MouldingMaintainenceInspection_BLL();
            DataTable dt = bll.GetList(CheckPeriod, CheckItem);

            if (dt == null ||  dt.Rows.Count == 0)
            {

            }
            else
            {
                DataRow dr = dt.Rows[0];
                model.UniqID = int.Parse(dr["UniqID"].ToString());
                model.No = int.Parse(dr["No"].ToString());
                model.CheckPeriod = CheckPeriod;
                model.CheckItem = CheckItem;
                model.InspectionDescription = dr["InspectionDescription"].ToString();
                model.MaintainenceDescription = dr["MaintainenceDescription"].ToString();

                Session["MouldingMaintenanceInspection_Model"] = model;
            }
           

         

            if (e.CommandName == "LinkDetail")
            {
                Response.Redirect("./MouldingMaintainCheckItem.aspx?commandType=UpdateCheckItem", false);
            }

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MouldingMaintainCheckItem.aspx?commandType=AddCheckItem", false);
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string CheckPeriod = this.ddl_CheckPeriod.SelectedValue;

                Common.Class.BLL.MouldingMaintainenceInspection_BLL bll = new Common.Class.BLL.MouldingMaintainenceInspection_BLL();
                DataTable dt = bll.GetList(CheckPeriod,"");

                if (dt== null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_CheckItemList.DataSource = dt.DefaultView;
                    this.dg_CheckItemList.DataBind();
                }

                foreach (DataGridItem item in dg_CheckItemList.Items)
                {
                    DropDownList ddl = (DropDownList)item.Cells[5].FindControl("ddl_CheckBy");
                    SetDDL_CheckBy(ddl);
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMaintainDetail", "btn_generate_Click Exception:" + ee.ToString());
                ShowWarning("");
            }

        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<Common.Class.Model.MouldingMaintain_Tracking_Model> List_Model = new List<Common.Class.Model.MouldingMaintain_Tracking_Model>();
            foreach (DataGridItem item in dg_CheckItemList.Items)
            {
                bool Selected = ((CheckBox)item.Cells[6].FindControl("cb_Update")).Checked;

                if (Selected)
                {
                    string CheckPeriod = item.Cells[0].Text;
                    string CheckItem = item.Cells[1].Text;
                    string MachineID = ((DropDownList)item.Cells[3].FindControl("ddl_MachineID")).SelectedValue;
                    string CheckResult = ((DropDownList)item.Cells[4].FindControl("ddl_CheckResult")).SelectedValue;
                    string SpareParts = ((TextBox)item.Cells[4].FindControl("txt_SpareParts")).Text.Trim();
                    string CheckBy = ((DropDownList)item.Cells[5].FindControl("ddl_CheckBy")).SelectedValue;

                    Common.Class.Model.MouldingMaintain_Tracking_Model model = new Common.Class.Model.MouldingMaintain_Tracking_Model();
                    model.CheckPeriod = CheckPeriod;
                    model.CheckItem = CheckItem;
                    model.MachineID = MachineID;
                    model.CheckResult = CheckResult;
                    model.SpareParts = SpareParts;
                    model.CheckBy = CheckBy;
                    model.ChangeTime = DateTime.Now;
                    model.CheckDate = DateTime.Now;

                    List_Model.Add(model);
                }
            }

            if (List_Model.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('No checking item is chose, Please check !');", true);
                return;
            }


            //insert to maintain tracking datatable.
            Common.Class.BLL.MouldingMaintain_Tracking_BLL bll = new Common.Class.BLL.MouldingMaintain_Tracking_BLL();
            if (bll.BatchAdd(List_Model))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Submit success !'); window.location.href = \"./MouldingMaintain.aspx\";", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Submit fail, Please try again !');", true);
            }



            //Session["List_MouldingMaintain_His_Model"] = List_Model;
            //Response.Redirect("../Laser/Login.aspx?Department="+StaticRes.Global.Department.Moulding+"&commandType=Maintain");
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
            Li.Text = "All";
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

        private void SetDDL_CheckBy(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("",""));
            
            Common.Class.BLL.User_DB_BLL bll = new Common.Class.BLL.User_DB_BLL();
            List<Common.Class.Model.User_DB_Model> modelList = bll.GetModelList(StaticRes.Global.Department.Moulding, "", "","");
            var technicianList = (from a in modelList
                                  where a.USER_GROUP == StaticRes.Global.UserGroup.TECHNICIAN
                                  select a).ToList();
                        
            if (technicianList ==null || technicianList.Count() == 0)
            {
                //由于moulding原本TECHNICIAN的人员辞职了,
                //所以找不到technician的人员下, 定死S0107显示.
                technicianList = (from a in modelList
                                  where a.EMPLOYEE_ID == "S0107"
                                  select a).ToList();
            }
            
            foreach (Common.Class.Model.User_DB_Model model in technicianList)
            {
                ddl.Items.Add(new ListItem(model.USER_NAME,model.USER_NAME));
            }
        }


        private void ShowWarning(string message)
        {
            this.lblResult.BackColor = System.Drawing.Color.Red;

            if (message != "")
            {
                this.lblResult.Text = message;
            }
            else
            {
                this.lblResult.Text = "There is no Record !";
            }
          

            this.lblResult.Visible = true;
            this.dg_CheckItemList.Visible = false;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_CheckItemList.Visible = true;
        }

      
    }
}