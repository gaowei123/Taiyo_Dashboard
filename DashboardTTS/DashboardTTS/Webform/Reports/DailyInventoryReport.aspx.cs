using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Reports
{
    public partial class DailyInventoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblUserHeader.Text = "Daily Inventory Report";

                    this.infDchFrom.CalendarLayout.SelectedDate = DateTime.Now.Date.AddDays(-1).AddHours(8);
                    this.infDchFrom.Value = DateTime.Now.Date.AddDays(-1).AddHours(8);
                    

                    BtnGenerate_Click(new object(), new EventArgs());

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("DailyInventoryReport", "Page_Load error : " + ex.ToString());
                }
            }
        }





        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtLaserPQCTotalTable = new DataTable();

             


                


            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("DailyInventoryReport", "BtnGenerate_Click error : " + ex.ToString());
            }
        }






    }
}