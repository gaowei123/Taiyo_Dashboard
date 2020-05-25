using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMonthlyMaterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    setYearDDL();
                    
                    btnGenerate_Click(null, null);
                }

                this.dgMaterialMonthly.ItemCommand += DgMaterialMonthly_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlyMaterial", "Page_Load Exception: " + ee.ToString());
            }
        }

        private void DgMaterialMonthly_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                int rowIndex = e.Item.ItemIndex;

                int month = rowIndex + 1;

                setDetail(month, int.Parse(this.ddlYear.SelectedValue));

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlyMaterial", "btnGenerate_Click Exception: " + ee.ToString());
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string sYear = this.ddlYear.SelectedValue;
                
                setMonthly(sYear);

                setDetail(DateTime.Now.Month,int.Parse(sYear));
                
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMonthlyMaterial", "btnGenerate_Click Exception: " + ee.ToString());
            }
        }


        void setMonthly(string sYear)
        {
            Common.Class.BLL.Material_Inventory_History_BLL bll = new Common.Class.BLL.Material_Inventory_History_BLL();
            DataTable dt = bll.GetMonthlyReport(sYear);

            if (dt == null || dt.Rows.Count == 0)
            {
                this.dgMaterialMonthly.Visible = false;
            }
            else
            {
                this.dgMaterialMonthly.Visible = true;
                dgMaterialMonthly.DataSource = dt.DefaultView;
                dgMaterialMonthly.DataBind();
            }
        }


        void setDetail(int iMonth, int iYear)
        {
            Common.Class.BLL.Material_Inventory_History_BLL bll = new Common.Class.BLL.Material_Inventory_History_BLL();

            DataTable dt = bll.GetMaterialDetailForMonth(iMonth, iYear);

            if (dt == null || dt.Rows.Count == 0)
            {
                this.dgMaterialDetail.Visible = false;
            }
            else
            {
                this.dgMaterialDetail.Visible = true;
                this.dgMaterialDetail.DataSource = dt.DefaultView;
                this.dgMaterialDetail.DataBind();
            }

            lbMonth.Text = GetMonth(iMonth);
        }




        void setYearDDL()
        {
            this.ddlYear.Items.Clear();


            int yearStart = 2017;
            int yearCurrent = DateTime.Now.Year;

            for (int i = yearStart; i < yearCurrent + 1; i++)
            {
                ListItem li = new ListItem();
                li.Text = i.ToString();
                li.Value = i.ToString();
                this.ddlYear.Items.Add(li);
            }

            this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
        }


        private string GetMonth(int iMonth)
        {
            string sMonth = "";
            switch (iMonth)
            {
                case 1:
                    sMonth = "Jan";
                    break;
                case 2:
                    sMonth = "Feb";
                    break;
                case 3:
                    sMonth = "Mar";
                    break;
                case 4:
                    sMonth = "Apr";
                    break;
                case 5:
                    sMonth = "May";
                    break;
                case 6:
                    sMonth = "Jun";
                    break;
                case 8:
                    sMonth = "Jul";
                    break;
                case 9:
                    sMonth = "Sep";
                    break;
                case 10:
                    sMonth = "Oct";
                    break;
                case 11:
                    sMonth = "Nov";
                    break;
                case 12:
                    sMonth = "Dec";
                    break;
             
                default:
                    break;
            }

            return string.Format("Date: {0}-{1}", sMonth, this.ddlYear.SelectedValue);

        }
    }
}