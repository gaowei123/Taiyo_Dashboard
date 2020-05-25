using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQC_PIC_PerformReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    DateTime dDay = Common.CommFunctions.GetDefaultReportsSearchingDay();


                    this.txtDateFrom.Text = dDay.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = dDay.ToString("yyyy-MM-dd");




                   


                    BtnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCDailyReport", "Page_Load error : " + ex.ToString());
                }
            }

            Common.CommFunctions.SetAutoComplete(this.Page, "#MainContent_txtPartNumber", "");

        }


        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string jobNo = this.txtJobNo.Text.Trim();
                string partNumber = this.txtPartNo.Text.Trim();
                string shift = this.ddlShift.SelectedItem.Value;
                string pic = this.txtPIC.Text.Trim();
                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text).Date.AddHours(8);
                DateTime DateTo = DateTime.Parse(this.txtDateTo.Text).Date.AddHours(8).AddDays(1);


                Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                DataTable dt = bll.GetPICReport(DateFrom, DateTo, shift, jobNo, partNumber, pic);



                Display(dt);
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCDailyReport", "BtnGenerate_Click error : " + ex.ToString());
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgPQCDailyReport, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
            }
        }


        void Display(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgPQCDailyReport, StaticRes.Global.ErrorLevel.Warning, "");
                return;
            }

            this.dgPQCDailyReport.DataSource = dt.DefaultView;
            this.dgPQCDailyReport.DataBind();

            Common.CommFunctions.HideWarning(this.lblResult, this.dgPQCDailyReport);
        }

        
      
    }
}