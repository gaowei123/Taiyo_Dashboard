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
        private readonly Common.Class.BLL.PQCPackTracking trackingBLL = new Common.Class.BLL.PQCPackTracking();
        private readonly Common.Class.BLL.PQCPackDetailTracking detailBLL = new Common.Class.BLL.PQCPackDetailTracking();
        private readonly Common.Class.BLL.PQCPackDefectTracking defectBLL = new Common.Class.BLL.PQCPackDefectTracking();
        private readonly Common.Class.BLL.PQCQaViBinning binBLL = new Common.Class.BLL.PQCQaViBinning();
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.Class.BLL.PQCQaViBinHistory_BLL binHisBLL = new Common.Class.BLL.PQCQaViBinHistory_BLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string trackingID = Request.QueryString["trackingID"] == null ? "" : Request.QueryString["trackingID"].ToString();

                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "[Page_Load] receive trackingID: " + trackingID);

                    if (trackingID == "")
                    {
                        Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Error, can not get tracking id. Please try again!", "./PQCPackingLiveReport.aspx");
                        return;
                    }



                    this.lbTrackingID.Text = trackingID;

                    //get  vi tracking model
                    Common.Class.Model.PQCPackTracking_Model model = trackingBLL.GetModel(trackingID);


                    //get pack detail tracking model list
                    List<Common.Class.Model.PQCPackDetailTracking_Model> packDetailList = detailBLL.GetModelList(this.lbTrackingID.Text);


                    //如果是去assembly的取最小值为 pack过的数量.
                    decimal passQty = model.shipTo == "Assembly"? packDetailList.Min(p => p.passQty.Value): packDetailList.FirstOrDefault().passQty.Value;
                


                    //UI 赋值
                    this.lbDay.Text = model.day.ToString("yyyy-MM-dd");
                    this.lbShift.Text = model.shift;
                    this.lbJob.Text = model.jobId;
                    this.lbPartNo.Text = model.partNumber;
                    this.lbPackedQty.Text = passQty.ToString("0");


                    

                    //获取显示MRP数量
                    Common.Class.Model.PaintingDeliveryHis_Model paintModel = paintBLL.GetModel(model.jobId, "Paint#1");
                    if (paintModel != null)
                    {
                        this.lbMrpQty.Text = paintModel.inQuantity.ToString("0");
                    }
                   


                    this.txtPackQty.Focus();
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "Page_Load error : " + ex.ToString());
                }
            }
        }


        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {

                #region validation
                if (this.txtPackQty.Text.Trim() == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Checked Qty can not be empty!");
                    this.txtPackQty.Focus();
                    return;
                }

                if (this.txtPackQty.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtPackQty.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Checked Qty must be number!");
                    this.txtPackQty.Text = "";
                    this.txtPackQty.Focus();
                    return;
                }
                #endregion




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




                decimal dPackQty = decimal.Parse(this.txtPackQty.Text);
                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] input info: pack Qty: {0}", dPackQty));



                //修改后增加的数量, 用于赋值bin his中的 from qty.
                decimal increaseQty = dPackQty - decimal.Parse(this.lbPackedQty.Text);




                //pack tracking model
                Common.Class.Model.PQCPackTracking_Model packTrackingModel = trackingBLL.GetModel(this.lbTrackingID.Text);

                //pack detail tracking model
                List<Common.Class.Model.PQCPackDetailTracking_Model> packDetailList = detailBLL.GetModelList(this.lbTrackingID.Text);

                //bin model
                List< Common.Class.Model.PQCQaViBinning> binModelList = binBLL.GetModelList(this.lbTrackingID.Text);
                var packBinList = (from a in binModelList
                                where a.processes == "PACKING"
                                select a).ToList();



                //set pack detail list
                foreach (var model in packDetailList)
                {
                    model.totalQty = dPackQty;
                    model.passQty = dPackQty;
                    model.updatedTime = DateTime.Now;
                    model.lastUpdatedTime = DateTime.Now;
                    model.remarks = "Updated by " + txtUserName.Text;

                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] set detail tracking, trackingID: {0}, totalQty: {1}, passQty: {2}", model.trackingID, model.totalQty, model.passQty));
                }


                //set pack tracking
                packTrackingModel.TotalQty = packDetailList.Sum(p => p.passQty);
                packTrackingModel.acceptQty = packDetailList.Sum(p => p.passQty);
                packTrackingModel.updatedTime = DateTime.Now;
                packTrackingModel.remarks= "Updated by " + txtUserName.Text;
                packTrackingModel.status = "End";
                packTrackingModel.lastUpdatedTime = DateTime.Now;
                packTrackingModel.updatedTime = DateTime.Now;

                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] set tracking, trackingID: {0}, totalQty: {1}, passQty: {2}", packTrackingModel.trackingID, packTrackingModel.TotalQty, packTrackingModel.acceptQty));



                List<Common.Class.Model.PQCQaViBinHistory_Model> binHisModelList = new List<Common.Class.Model.PQCQaViBinHistory_Model>();

                //set pack bin list
                foreach (var model in packBinList)
                {
                    //bin联动累加, 增长量.
                    model.materialQty = model.materialQty + increaseQty;
                    model.updatedTime = DateTime.Now;
                    model.remarks = "Updated by " + txtUserName.Text;

                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] set bin, trackingID: {0}, materialQty: {1}", model.trackingID, model.materialQty));


                    //copy his model
                    Common.Class.Model.PQCQaViBinHistory_Model binHisModel = binHisBLL.CopyModel(model);
                    binHisModel.materialFromQty = model.materialQty - increaseQty;
                    binHisModelList.Add(binHisModel);
                }





                //update pqc data rollback
                bool updateResult = trackingBLL.UpdatePQCJobMaintenance(packTrackingModel, packDetailList, packBinList, binHisModelList);
                if (updateResult == false)
                {
                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", "[btn Submit Click] :update PQC Job Fail");

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