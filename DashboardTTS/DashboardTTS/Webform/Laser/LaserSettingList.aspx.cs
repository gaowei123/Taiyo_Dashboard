using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserVisionSettingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.txtDateFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    
                    SetDDLMachineID();
                    
                    btn_Generate_Click(null, null);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserVisionSettingList", "Page_Load exception: " + ee.ToString());
                Common.CommFunctions.ShowWarning(this.lblResult, this.dgVisionList, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
            
        }

        protected void btn_Generate_Click(object sender, EventArgs e)
        {

            string[] arrPartNo = new string[] { this.txtPartNo.Text.Trim() };
            string[] arrMachineNo = new string[] { this.ddlMachineNo.SelectedItem.Value };
            DateTime dDateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
            DateTime dDateTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);



            Common.Class.BLL.LMMSVisionMachineSettingHis_BLL bll = new Common.Class.BLL.LMMSVisionMachineSettingHis_BLL();
            DataTable dt = bll.GetList(dDateFrom, dDateTo, arrMachineNo, arrPartNo);

            if (dt == null || dt.Rows.Count == 0)
            {
                this.dgVisionList.Visible = false;
                this.lblResult.Visible = true;
                this.lblResult.Text = "There is no record!";
                this.lblResult.BackColor = System.Drawing.Color.Red;
            }else
            {
                this.dgVisionList.Visible = true;
                this.lblResult.Visible = false;

                this.dgVisionList.DataSource = dt.DefaultView;
                this.dgVisionList.DataBind();
            }
           
        }




        void SetDDLMachineID()
        {
            this.ddlMachineNo.Items.Clear();


            this.ddlMachineNo.Items.Add(new ListItem("ALL", ""));


            for (int i = 1; i < 9; i++)
            {
                this.ddlMachineNo.Items.Add(new ListItem("No." + i.ToString(), i.ToString()));
            }

        }

        
    }
}