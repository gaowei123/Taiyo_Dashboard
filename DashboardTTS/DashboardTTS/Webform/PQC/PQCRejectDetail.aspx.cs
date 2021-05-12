using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCRejectDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    setRejCode("");
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCRejectDetail", "Page_Load exception:" + ee.ToString());
            }
        }


        protected void ddlRejType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rejType = ((DropDownList)sender).SelectedValue;
            setRejCode(rejType);
        }
         


        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string sMachineID = this.ddlStation.SelectedValue;
                string sRejType = this.ddlRejType.SelectedValue;
                string sRejCode = this.ddlDefectCode.SelectedValue;
                string sPartNo = this.txtPartNo.Text.Trim();
                string sJobNo = this.txtJobNo.Text.Trim();


                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text);
                DateTime DateTo = DateTime.Parse(this.txtDateTo.Text).AddDays(1);



                if ((DateTo-DateFrom).TotalDays > 7)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "From - To can not over 7 days!");
                    return;
                }


                Common.Class.BLL.PQCQaViDefectTracking_BLL bll = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
                DataTable dt = bll.GetList(DateFrom,DateTo, sMachineID, sRejType,sRejCode, sPartNo, sJobNo);

                

                if (dt == null || dt.Rows.Count <= 0)
                {
                    Common.CommFunctions.ShowWarning(this.lbResult, this.dg_RejDetail, StaticRes.Global.ErrorLevel.Warning, "There is no Record");
                }
                else
                {
                    this.dg_RejDetail.DataSource = dt.DefaultView;
                    this.dg_RejDetail.DataBind();

                    Common.CommFunctions.HideWarning(this.lbResult, this.dg_RejDetail);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCRejectDetail", "btn_generate_Click exception:" + ee.ToString());
                Common.CommFunctions.ShowWarning(this.lbResult, this.dg_RejDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }



        
       

        private void setRejCode(string sRejType)
        {
            this.ddlDefectCode.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlDefectCode.Items.Add(Li);


            Common.Class.BLL.PQCDefectSetting_BLL bll = new Common.Class.BLL.PQCDefectSetting_BLL();
            DataTable dt = bll.GetList(sRejType);

            foreach (DataRow dr in dt.Rows)
            {
                Li = new ListItem();
                Li.Text = dr["defectCode"].ToString();
                Li.Value = dr["defectCode"].ToString();
                this.ddlDefectCode.Items.Add(Li);
            }
        }

      
    }
}