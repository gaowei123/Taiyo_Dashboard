using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCPackingMaintenance : System.Web.UI.Page
    {
        private readonly Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_BLL _bll = new Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_BLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string jobNo = Request.QueryString["jobNo"] == null ? "" : Request.QueryString["jobNo"].ToString();
                    string trackingID = Request.QueryString["trackingID"] == null ? "" : Request.QueryString["trackingID"].ToString();
                    if (trackingID == "" || jobNo == "")
                    {
                        Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Error, no job info received. Please try again!", "./PQCPackingLiveReport.aspx");
                        return;
                    }

                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", $"Page_Load, receive url paras trackingID:{trackingID}, jobno:{jobNo}");


                    var maintainModel = _bll.GetMaintainInfo(jobNo, trackingID);
                    initUIJobInfo(maintainModel);
                    initUIMaterialList(maintainModel);
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "Page_Load error : " + ex.ToString());
                }
            }
        }

        private void initUIJobInfo(Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_Model model)
        {
            this.lbDay.Text = model.Job.Day.ToString("yyyy-MM-dd");
            this.lbShift.Text = model.Job.Shift;
            this.lbJob.Text = model.Job.JobNo;
            this.lbTrackingID.Text = model.Job.TrackingID;
            this.lbPartNo.Text = model.Job.PartNo;
            this.lbMrpQty.Text = model.Job.MRPQty.ToString();
        }
        private void initUIMaterialList(Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_Model model)
        {
            var materialNameList = from a in model.MaterialPartList
                                   group a by a.MaterialName into MaterialNameList
                                   select new
                                   {
                                       MaterialName = MaterialNameList.Key,
                                       InventoryQty = MaterialNameList.Sum(p => p.InventoryQty),
                                       MaterialQty = MaterialNameList.Sum(p => p.MaterialQty),
                                       ScrapQty = MaterialNameList.Sum(p => p.ScrapQty)
                                   };
            this.dgMaterial.DataSource = materialNameList;
            this.dgMaterial.DataBind();

            foreach (DataGridItem item in this.dgMaterial.Items)
            {
                string packQty = materialNameList.Where(p => p.MaterialName == item.Cells[0].Text).FirstOrDefault().MaterialQty.ToString();

                Label lb = (Label)item.Cells[3].FindControl("lbCurPackQty");
                lb.Text = packQty;

                TextBox tb = (TextBox)item.Cells[3].FindControl("txtPackSetQty");
                tb.Attributes.Add("placeholder", packQty);
            }
        }




        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {

                #region login control 
                string userName = this.txtUserName.Text;
                string password = this.txtPassword.Text;

                if (userName == "")
                {
                    this.txtUserName.Text = "";
                    this.txtUserName.Focus();
                    Common.CommFunctions.ShowMessage(Page, "Username can not be empty!");
                    return;
                }
                if (password == "")
                {
                    this.txtPassword.Text = "";
                    this.txtPassword.Focus();
                    Common.CommFunctions.ShowMessage(Page, "Password can not be empty!");
                    return;
                }

                string errorStr = "";

                Common.Class.BLL.User_DB_BLL UserBll = new Common.Class.BLL.User_DB_BLL();
                bool loginResult = UserBll.Login(userName, password, out errorStr, StaticRes.Global.Department.PQC, StaticRes.Global.UserGroup.OPERATOR);

                if (!loginResult)
                {
                    Common.CommFunctions.ShowMessage(Page, errorStr);
                    return;
                }
                #endregion


                var maintainModel = _bll.GetMaintainInfo(this.lbJob.Text, this.lbTrackingID.Text);

                //遍历datagrid的每一行
                foreach (DataGridItem item in this.dgMaterial.Items)
                {
                    string materialName = item.Cells[0].Text;
                    decimal binQty = decimal.Parse(item.Cells[1].Text);
                    decimal scrapQty = decimal.Parse(item.Cells[2].Text);
                    string materialQty = item.Cells[2].Text;
                    string packSetQty = ((TextBox)item.Cells[3].FindControl("txtPackSetQty")).Text;
                    

                    //防止输入的不是数字
                    if (!Common.CommFunctions.isNumberic(packSetQty))
                    {
                        ((TextBox)item.Cells[3].FindControl("txtPackSetQty")).Text = "";
                        ((TextBox)item.Cells[3].FindControl("txtPackSetQty")).Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "Please input number!");
                        return;
                    }

                    //防止维护填写的数量比库存还多.
                    if (decimal.Parse(packSetQty) - decimal.Parse(materialQty) > binQty + scrapQty)
                    {
                        ((TextBox)item.Cells[3].FindControl("txtPackSetQty")).Text = "";
                        ((TextBox)item.Cells[3].FindControl("txtPackSetQty")).Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "Packing Set Qty can not bigger than inventory qty and scrap qty!");
                        return;
                    }





                    var  materialNames = maintainModel.MaterialPartList.Where(p => p.MaterialName == materialName);
                    foreach (var materialPart in materialNames)
                    {
                        //重新赋值, 维护后的数量.
                        materialPart.MaterialQty = decimal.Parse(packSetQty);
                    }
                }


                bool result = _bll.Update(maintainModel, userName);
                if (!result)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Update Fail!");
                    return;
                }

                Response.Redirect("./PQCPackingLiveReport.aspx?jobNumber=" + this.lbJob.Text, false);
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "btn_confirm_Click exception : " + ex.ToString());
            }
        }


  

    }
}