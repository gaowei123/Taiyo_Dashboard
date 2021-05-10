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
        private readonly Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_BLL _packMaintainBLL = new Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_BLL();
        private readonly Common.ExtendClass.PQCProduction.Core.Base_BLL _baseBLL = new Common.ExtendClass.PQCProduction.Core.Base_BLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string jobNo = Request.QueryString["jobNo"] == null ? "" : Request.QueryString["jobNo"].ToString();
                    string trackingID = Request.QueryString["trackingID"] == null ? "" : Request.QueryString["trackingID"].ToString();

                    if (!string.IsNullOrEmpty(jobNo))
                    {
                        var maintainModel = _packMaintainBLL.GetMaintainInfo(jobNo, trackingID);
                        initUIJobInfo(maintainModel);
                        initUIMaterialList(maintainModel);
                    }
                    else
                    {
                        this.txtJobNo.Focus();
                    }
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "Page_Load error : " + ex.ToString());
                }
            }
        }
        


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // 1. 根据输入的 job no 查询 pack tracking 的数据
            // 1.1 如果没 pack tracking 记录, 直接传入 job no.
            // 1.2 如果只有一个 pack tracking 记录, 传入 job no, tracking id.
            // 1.3 如果有多个 pack tracking 记录, 则转到 packing live report, 让用户自己选维护那一条.
            

            string jobNo = this.txtJobNo.Text.Trim();
            if (string.IsNullOrEmpty(jobNo))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Job no can not be empty, Please keyin ");
                return;
            }



            // 1. 根据输入的 job no 查询 pack tracking 的数据            
            var packList = _baseBLL.GetPackingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam(){JobNo = jobNo});



            // 1.1 如果没 pack tracking 记录, 直接传入 job no.
            if (packList == null || packList.Count() == 0)
            {
                var maintainModel = _packMaintainBLL.GetMaintainInfo(jobNo, string.Empty);
                if (maintainModel == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "No data found !");
                    return;
                }

                initUIJobInfo(maintainModel);
                initUIMaterialList(maintainModel);
            }
            // 1.2 如果只有一个 pack tracking 记录, 传入 job no, tracking id.
            else if (packList.Count() == 1)
            {
                string trackingID = packList.FirstOrDefault().TrackingID;
                var maintainModel = _packMaintainBLL.GetMaintainInfo(jobNo, trackingID);

                initUIJobInfo(maintainModel);
                initUIMaterialList(maintainModel);
            }
            // 1.3 如果有多个 pack tracking 记录, 则转到 packing live report, 让用户自己选维护那一条.
            else
            {
                string sDateFrom = packList.Min(p => p.Day).ToString("yyyy-MM-dd");
                string sDateTo = packList.Max(p => p.Day).ToString("yyyy-MM-dd");
                Response.Redirect($"./PQCPackingLiveReport.aspx?JobNo={jobNo}&DateFrom={sDateFrom}&DateTo={sDateTo}");
                return;
            }
        }



        protected void btnEnd_Click(object sender, EventArgs e)
        {
            string trackingID = this.lbTrackingID.Text.Trim();
            if (string.IsNullOrEmpty(trackingID))
            {
                Common.CommFunctions.ShowMessage(this.Page, "There is no pack trackingID, can't end!");
                return;
            }
            
            if (!_packMaintainBLL.End(trackingID, ""))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Update Fail!");
                return;
            }
            else
            {
                var packList = _baseBLL.GetPackingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam() { JobNo = this.lbJob.Text });
                string sDateFrom = packList.Min(p => p.Day).ToString("yyyy-MM-dd");
                string sDateTo = packList.Max(p => p.Day).ToString("yyyy-MM-dd");
                Response.Redirect($"./PQCPackingLiveReport.aspx?JobNo={this.lbJob.Text}&DateFrom={sDateFrom}&DateTo={sDateTo}");
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


                var maintainModel = _packMaintainBLL.GetMaintainInfo(this.lbJob.Text, this.lbTrackingID.Text);

                //遍历datagrid的每一行
                foreach (DataGridItem item in this.dgMaterial.Items)
                {
                    string materialName = item.Cells[0].Text;
                    decimal binQty = decimal.Parse(item.Cells[1].Text);
                    decimal scrapQty = decimal.Parse(item.Cells[2].Text);
                    string materialQty = ((Label)item.Cells[3].FindControl("lbCurPackQty")).Text;
                    string packSetQty = ((TextBox)item.Cells[3].FindControl("txtUpdatedQty")).Text;
                    

                    //防止输入的不是数字
                    if (!Common.CommFunctions.isNumberic(packSetQty))
                    {
                        ((TextBox)item.Cells[3].FindControl("txtUpdatedQty")).Text = "";
                        ((TextBox)item.Cells[3].FindControl("txtUpdatedQty")).Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "Please input number!");
                        return;
                    }

                    //防止维护填写的数量比库存还多.
                    if (decimal.Parse(packSetQty) - decimal.Parse(materialQty) > binQty + scrapQty)
                    {
                        ((TextBox)item.Cells[3].FindControl("txtUpdatedQty")).Text = "";
                        ((TextBox)item.Cells[3].FindControl("txtUpdatedQty")).Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "Packing Set Qty can not bigger than inventory qty and scrap qty!");
                        return;
                    }
                    

                    var  materialNames = maintainModel.MaterialPartList.Where(p => p.MaterialName == materialName);
                    foreach (var materialPart in materialNames)
                    {
                        //重新赋值, 维护后的数量.
                        materialPart.UpdatedQty = decimal.Parse(packSetQty);
                    }
                }

                
                if (!_packMaintainBLL.Update(maintainModel, userName))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Update Fail!");
                    return;
                }
                else
                {
                    var packList = _baseBLL.GetPackingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam() { JobNo = this.lbJob.Text });
                    string sDateFrom = packList.Min(p => p.Day).ToString("yyyy-MM-dd");
                    string sDateTo = packList.Max(p => p.Day).ToString("yyyy-MM-dd");
                    Response.Redirect($"./PQCPackingLiveReport.aspx?JobNo={this.lbJob.Text}&DateFrom={sDateFrom}&DateTo={sDateTo}");
                }
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "btn_confirm_Click exception : " + ex.ToString());
            }
        }




        private void initUIJobInfo(Common.ExtendClass.PQCProduction.PackMaintain.PackMaintain_Model model)
        {
            this.lbDay.Text = model.Job.Day == null ? "" : model.Job.Day.Value.ToString("yyyy-MM-dd");
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
                                       InventoryQty = MaterialNameList.Min(p => p.InventoryQty),
                                       MaterialQty = MaterialNameList.Sum(p => p.MaterialQty),
                                       ScrapQty = MaterialNameList.Min(p => p.ScrapQty)
                                   };
            this.dgMaterial.DataSource = materialNameList;
            this.dgMaterial.DataBind();

            foreach (DataGridItem item in this.dgMaterial.Items)
            {
                string packQty = materialNameList.Where(p => p.MaterialName == item.Cells[0].Text).FirstOrDefault().MaterialQty.ToString();

                Label lb = (Label)item.Cells[3].FindControl("lbCurPackQty");
                lb.Text = packQty;

                TextBox tb = (TextBox)item.Cells[3].FindControl("txtUpdatedQty");
                tb.Attributes.Add("placeholder", packQty);
            }
        }

        
    }
}