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

                    this.lblUserHeader.Text = "PQC Rejection Detail Report";


                    //request parameters
                    string RejType = Request.QueryString["Type"] == null ? "" : Request.QueryString["Type"].ToString();
                    DateTime? dDateFrom = null;
                    DateTime? dDateTo = null;
                    if (Request.QueryString["DateFrom"] != null)
                    {
                        dDateFrom = DateTime.Parse(Request.QueryString["DateFrom"].ToString());
                    }
                    if (Request.QueryString["DateTo"] != null)
                    {
                        dDateTo = DateTime.Parse(Request.QueryString["DateTo"].ToString());
                    }



                    //init
                    SetMachineTypeDDL();
                    setRejType(RejType);
                    setRejCode("");

                    if (dDateFrom != null)
                    {
                        this.infDchFrom.CalendarLayout.SelectedDate = dDateFrom.Value.Date;
                        this.infDchFrom.Value = dDateFrom.Value.Date;
                    }
                    else
                    {
                        this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.Date;
                        this.infDchFrom.Value = DateTime.Now.Date;
                    }
                    if (dDateTo !=null)
                    {
                        this.infDchTo.CalendarLayout.SelectedDate = dDateTo.Value.Date;
                        this.infDchTo.Value = dDateTo.Value.Date;
                    }else
                    {
                        this.infDchTo.CalendarLayout.SelectedDate = DateTime.Now.Date;
                        this.infDchTo.Value = DateTime.Now.Date;
                    }
                    

                    btn_generate_Click(new object(), new EventArgs());

                }


                Common.CommFunctions.SetAutoComplete(this.Page, "#MainContent_txtPartNo", "");
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

                string sLotNo = Request.QueryString["lotNo"] == null ? "" : Request.QueryString["lotNo"].ToString();
                string sMachineID = this.ddlMachineType.SelectedValue;
                string sRejType = this.ddlRejType.SelectedValue;
                string sRejCode = this.ddlRejCode.SelectedValue;
                string sPartNo = this.txtPartNo.Text.Trim();
            

                DateTime DateFrom = infDchFrom.CalendarLayout.SelectedDate.Date;
                DateTime DateTo = infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1);


                Common.Class.BLL.PQCQaViDefectTracking_BLL bll = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
                DataTable dt = bll.GetList(DateFrom,DateTo, sMachineID, sRejType,sRejCode,sLotNo, sPartNo);

                

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





        //============== func ==============//

        private void SetMachineTypeDDL()
        {

            this.ddlMachineType.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlMachineType.Items.Add(Li);


            for (int i = 1; i < 29; i++)
            {
                if (i == 9 || i == 10 || i == 18 || i == 19 || i == 20)
                {
                    continue;
                }

                Li = new ListItem();
                Li.Text = "Station" + i.ToString();
                Li.Value = i.ToString();
                this.ddlMachineType.Items.Add(Li);
            }

        }



        private void setRejType(string sType)
        {

            this.ddlRejType.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlRejType.Items.Add(Li);
            
            Li = new ListItem();
            Li.Text = "Mould";
            Li.Value = "Mould";
            this.ddlRejType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Paint";
            Li.Value = "Paint";
            this.ddlRejType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Laser";
            Li.Value = "Laser";
            this.ddlRejType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Others";
            Li.Value = "Others";
            this.ddlRejType.Items.Add(Li);

            if (sType.Trim() != "")
            {
                this.ddlRejType.SelectedValue = sType;
            }

        }


        private void setRejCode(string sRejType)
        {

            this.ddlRejCode.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlRejCode.Items.Add(Li);


            Common.Class.BLL.PQCDefectSetting_BLL bll = new Common.Class.BLL.PQCDefectSetting_BLL();
            DataTable dt = bll.GetList(sRejType);

            foreach (DataRow dr in dt.Rows)
            {
                Li = new ListItem();
                Li.Text = dr["defectCode"].ToString();
                Li.Value = dr["defectCode"].ToString();
                this.ddlRejCode.Items.Add(Li);
            }
            
        }

      
    }
}