using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class QAProductionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    DateTime DateFrom = Request.QueryString["DateFrom"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    DateTime DateTo = Request.QueryString["DateTo"] == null ? DateTime.Now.Date : DateTime.Parse(Request.QueryString["DateTo"].ToString());
                    this.infDchFrom.Value = DateFrom;
                    infDchFrom.CalendarLayout.SelectedDate = DateFrom;
                    this.infDchTo.Value = DateTo;
                    infDchTo.CalendarLayout.SelectedDate = DateTo;


                    SetRejTypeDDL();
                    

                    btn_generate_Click(new object() { }, new EventArgs() { });
                }
              
            }
            catch (Exception ee)
            {
                ShowWarning(ee.ToString());
                DBHelp.Reports.LogFile.Log("QAProductionReport", "Page_Load Exception :" + ee.ToString());
            }
        }



       

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.AddHours(8).ToString());
                DateTime dTimeTo = DateTime.Parse(infDchTo.CalendarLayout.SelectedDate.AddHours(8).ToString());
                
                string MachineID = this.ddlMachineID.SelectedValue;
                string PartNo = this.txt_PartNo.Text;
                string Model = this.txt_Model.Text;
                string RejType = this.ddl_RejType.SelectedItem.Text;


                DataTable dt = new DataTable();


                Common.Class.BLL.MouldingQaViDefectTracking_BLL bll = new Common.Class.BLL.MouldingQaViDefectTracking_BLL();
                dt = bll.Getlist(dTimeFrom,dTimeTo,MachineID,PartNo,RejType,Model);


                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_QARejDetail.DataSource = dt.DefaultView;
                    this.dg_QARejDetail.DataBind();
                    HideWarning();
                }
            }
            catch (Exception ee)
            {
                ShowWarning(ee.ToString());
                DBHelp.Reports.LogFile.Log("QAProductionReport", "btn_generate_Click Exception :" + ee.ToString());
            }
        }




        void ShowWarning(string message)
        {
            if (message != "")
            {
                this.lblResult.Text = message;
            }
            else
            {
                this.lblResult.Text = "There is no record";
            }
        
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = true;
            this.dg_QARejDetail.Visible = false;
        }

        void HideWarning()
        {
            this.lblResult.Text = "There is no record!";
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Visible = false;
            this.dg_QARejDetail.Visible = true;
        }


        private void SetRejTypeDDL()
        {
            Common.Class.BLL.MouldingDefectSetting_BLL bll = new Common.Class.BLL.MouldingDefectSetting_BLL();
            List<string> ListRejType = bll.RejTypeList();
            ddl_RejType.DataSource = ListRejType;
            ddl_RejType.DataBind();
        }
    }
}