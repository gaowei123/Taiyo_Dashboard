using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace DashboardTTS.Webform.Moulding
{
    public partial class Moulding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["VerifyBy"] == null)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operation Timeout,Please Login again!'); window.location.href = \"../Laser/Login.aspx\";", true);
                        return;
                    }

                    string VerifyBy = (string)Session["VerifyBy"];

                    this.lblResult.Visible = false;


                    RefreshList();

                }

                this.dg_VerifyList.ItemCommand += Dg_VerifyList_ItemCommand;
            }
            catch (Exception ee)
            {

            }
        }

        private void Dg_VerifyList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;
            string CheckPeriod = item.Cells[0].Text;
            string CheckItem = item.Cells[1].Text;
            string MachineID = item.Cells[2].Text;
            string CheckResult = item.Cells[3].Text;
            string SpareParts = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;
            string ChangeTime = item.Cells[5].Text;
            string CheckDate = item.Cells[6].Text;
            string CheckBy = item.Cells[7].Text;

            if (e.CommandName == "Verify")
            {
                List<Common.Class.Model.MouldingMaintain_His_Model> modelList = new List<Common.Class.Model.MouldingMaintain_His_Model>();
                Common.Class.Model.MouldingMaintain_His_Model model = new Common.Class.Model.MouldingMaintain_His_Model();
                model.CheckPeriod = CheckPeriod;
                model.CheckItem = CheckItem;
                model.MachineID = MachineID;
                model.CheckResult = CheckResult;
                model.SpareParts = SpareParts;
                model.ChangeTime = DateTime.Parse(ChangeTime);
                model.CheckDate = DateTime.Parse(CheckDate);
                model.CheckBy = CheckBy;
                model.VerifyBy = (string)Session["VerifyBy"];

                modelList.Add(model);



                Common.Class.BLL.MouldingMaintain_His_BLL bll = new Common.Class.BLL.MouldingMaintain_His_BLL();
                if (bll.Verify(modelList))
                {
                    RefreshList();
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Verify success');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Verify fail');", true);
                }
                
            }
            else if (e.CommandName == "Cancel")
            {
                Common.Class.BLL.MouldingMaintain_Tracking_BLL bll = new Common.Class.BLL.MouldingMaintain_Tracking_BLL();

                if (bll.Delete(CheckPeriod, CheckItem, MachineID, DateTime.Parse( CheckDate)))
                {
                    RefreshList();
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Cancel success');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Cancel fail');", true);
                }
            }

        }
        
        protected void btn_VerifyAll_Click(object sender, EventArgs e)
        {
            List<Common.Class.Model.MouldingMaintain_His_Model> modelList = new List<Common.Class.Model.MouldingMaintain_His_Model>();

            foreach (DataGridItem item in this.dg_VerifyList.Items)
            {
                Common.Class.Model.MouldingMaintain_His_Model model = new Common.Class.Model.MouldingMaintain_His_Model();
                model.CheckPeriod = item.Cells[0].Text;
                model.CheckItem = item.Cells[1].Text;
                model.MachineID = item.Cells[2].Text;
                model.CheckResult = item.Cells[3].Text;
                model.SpareParts = item.Cells[4].Text;
                model.ChangeTime = DateTime.Parse(item.Cells[5].Text);
                model.CheckDate = DateTime.Parse(item.Cells[6].Text);
                model.CheckBy = item.Cells[7].Text;
                model.VerifyBy = (string)Session["VerifyBy"];

                modelList.Add(model);
            }


            Common.Class.BLL.MouldingMaintain_His_BLL bll = new Common.Class.BLL.MouldingMaintain_His_BLL();
            if (bll.Verify(modelList))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Verify success');window.location.href = \"./MouldingMaintain.aspx\";", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Verify fail');", true);
            }
        }

        private void RefreshList()
        {
            Common.Class.BLL.MouldingMaintain_Tracking_BLL bll = new Common.Class.BLL.MouldingMaintain_Tracking_BLL();
            DataTable dt = bll.GetList();

            if (dt == null || dt.Rows.Count == 0)
            {
                //this.lblResult.Text = "There is no items!";
                //this.lblResult.BackColor = System.Drawing.Color.Red;
                //this.lblResult.Visible = false;

                ClientScript.RegisterStartupScript(Page.GetType(), "", "window.location.href = \"./MouldingMaintain.aspx\";", true);

            }
            else
            {
                //this.lblResult.Visible = true;
                this.dg_VerifyList.DataSource = dt.DefaultView;
                this.dg_VerifyList.DataBind();
            }
        }

       
    }
}