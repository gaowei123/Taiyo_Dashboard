using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserMachineDownTimeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.txtDateFrom.Text = DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    SetMachineDDL();

                    btn_Generate_Click(new object(), new EventArgs());
                }

                this.dg_DownTime.ItemCommand += Dg_DownTime_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserMachineDownTimeList", "Page_Load --" + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_DownTime, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        private void Dg_DownTime_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "OpenPDF")
            {
                string fileName = e.Item.Cells[11].Text;
                //直接打开pdf
                Response.Write("<script>window.open('../../Attachment/"+ fileName + "','_blank')</script>");
            }
            else if (e.CommandName == "LinkDetail")
            {
                string machineID = e.Item.Cells[2].Text.Replace("Machine", "");
                DateTime dDay = DateTime.Parse(e.Item.Cells[13].Text);
                string cause = e.Item.Cells[14].Text;

                Common.Class.Model.LMMSMachineDownTime_Model model = new Common.Class.Model.LMMSMachineDownTime_Model();
                model.machineID = machineID;
                model.date = dDay;
                model.cause = cause;

                Session["LMMSMachineDownTime_Model"] = model;
                
                Response.Redirect("./LaserMachineDownTimeForm.aspx");
            }
        }


        protected void btn_Maintenance_Click(object sender, EventArgs e)
        {
            Response.Redirect("./LaserMachineDownTimeForm.aspx",false);
        }
        

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);
                string MahineID = this.ddlMachineNo.SelectedItem.Value;


                Common.Class.BLL.LMMSMachineDownTime_BLL bll = new Common.Class.BLL.LMMSMachineDownTime_BLL();
                DataTable dt = bll.GetList(dTimeFrom, dTimeTo, MahineID, null, "");

             

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowWarning("");
                }
                else
                {
                    this.dg_DownTime.DataSource = dt.DefaultView;
                    this.dg_DownTime.DataBind();
                    HideWarning();
                }


            }
            catch (Exception ex)
            {
                ShowWarning(ex.ToString());
                DBHelp.Reports.LogFile.Log("ProductivityDetail", "btnGenerate_Click error : " + ex.ToString());
            }
        }


        private void SetMachineDDL()
        {
            this.ddlMachineNo.Items.Clear();

            ListItem li = new ListItem();
            li.Text = "All";
            li.Value = "";
            ddlMachineNo.Items.Add(li);

            for (int i = 1; i < 9; i++)
            {
                li = new ListItem();
                li.Text = "No." + i.ToString();
                li.Value = i.ToString();
                ddlMachineNo.Items.Add(li);
            }
        }
        private void ShowWarning(string message)
        {
            this.lblResult.Visible = true;
            this.dg_DownTime.Visible = false;
            this.lblResult.BackColor = System.Drawing.Color.Red;


            this.lblResult.Text = string.IsNullOrEmpty(message) ? "There is no record! " : message;
        }

        private void HideWarning()
        {
            this.lblResult.Visible = false;
            this.dg_DownTime.Visible = true;
        }


        

    }
}