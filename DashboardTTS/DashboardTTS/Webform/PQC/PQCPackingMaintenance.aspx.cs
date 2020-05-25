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
                        Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Error, can not get tracking id. Please try again!", "./PQCProductivityDetailReport.aspx");
                        return;
                    }



                    //get job info from VI Tracking
                    Common.Class.Model.PQCPackTracking_Model model = trackingBLL.GetModel(trackingID);

                    

                    this.lbDay.Text = model.day.ToString("yyyy-MM-dd");
                    this.lbShift.Text = model.shift;
                    this.lbJob.Text = model.jobId;
                    this.lbTrackingID.Text = trackingID;
                    this.lbPartNo.Text = model.partNumber;


                    int materialCount = 0;

                    Common.Class.BLL.PQCBomDetail_BLL bomDetailBLL = new Common.Class.BLL.PQCBomDetail_BLL();
                    DataTable dt = bomDetailBLL.GetList(model.partNumber);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        materialCount = 1;
                    }

                    materialCount = dt.Rows.Count;
                    this.lbCheckQty.Text = Math.Round(model.TotalQty.Value / materialCount, 0).ToString();



                    Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                    Common.Class.Model.PaintingDeliveryHis_Model paintModel = paintBLL.GetModel(model.jobId, "Paint#1");

                    if (paintModel != null)
                    {
                        this.lbMrpQty.Text = paintModel.inQuantity.ToString("0");
                    }
                   


                    this.txtCheckQty.Focus();
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
                if (this.txtCheckQty.Text.Trim() == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Checked Qty can not be empty!");
                    this.txtCheckQty.Focus();
                    return;
                }

                if (this.txtCheckQty.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtCheckQty.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Checked Qty must be number!");
                    this.txtCheckQty.Text = "";
                    this.txtCheckQty.Focus();
                    return;
                }

                if (this.rbtnComplete.Checked == false && this.rbtnNotComplete.Checked == false)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose job complete status!");
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




                decimal dCheckQty = decimal.Parse(this.txtCheckQty.Text);//set qty
                bool bIsJobComplete = rbtnComplete.Checked;


                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] input info: checked Qty: {0}, lot compelete check: {1}", dCheckQty, bIsJobComplete));



                //pack tracking model
                Common.Class.Model.PQCPackTracking_Model packTrackingModel = trackingBLL.GetModel(this.lbTrackingID.Text);

                //pack detail tracking model
                List<Common.Class.Model.PQCPackDetailTracking_Model> packDetailList = detailBLL.GetModelList(this.lbTrackingID.Text);

                //vi defect tracking 
                List<Common.Class.Model.PQCPackDefectTracking_Model> packDefectList = defectBLL.GetModelList(this.lbTrackingID.Text);



                decimal materialCount = packDetailList.Count;
                decimal totalRejQty = packDetailList.Sum(p => p.rejectQty).Value;






                //set pack vi tracking model
                packTrackingModel.targetQty = dCheckQty * materialCount;
                packTrackingModel.TotalQty = dCheckQty * materialCount;
                packTrackingModel.acceptQty = dCheckQty * materialCount - packTrackingModel.rejectQty;

                packTrackingModel.status = "End";
                packTrackingModel.nextViFlag = bIsJobComplete.ToString();
                packTrackingModel.lastUpdatedTime = DateTime.Now;
                packTrackingModel.updatedTime = DateTime.Now;
                packTrackingModel.remarks = "PQC Pack Job Maintenance, updated by " + userName;

                DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] set Tracking Model Info: JobID: {0}, TotalQty: {1}, acceptQty: {2}", packTrackingModel.jobId, packTrackingModel.TotalQty, packTrackingModel.acceptQty));






                //set detail model list
                foreach (var detailModel in packDetailList)
                {
                    detailModel.totalQty = dCheckQty;
                    detailModel.passQty = dCheckQty - detailModel.rejectQty.Value;

                    detailModel.status = "End";
                    detailModel.lastUpdatedTime = DateTime.Now;
                    detailModel.updatedTime = DateTime.Now;
                    detailModel.remarks = "PQC Pack Job Maintenance, updated by " + userName;

                    DBHelp.Reports.LogFile.Log("PQCPackingMaintenance", string.Format("[btn Submit Click] set Detail Tracking Model Info: Material No: {0}, TotalQty: {1}, acceptQty: {2}", detailModel.materialPartNo, detailModel.totalQty, detailModel.passQty));
                }





                //set defect model list
                foreach (var defectModel in packDefectList)
                {
                    defectModel.status = "End";
                    defectModel.lastUpdatedTime = DateTime.Now;
                    defectModel.updatedTime = DateTime.Now;
                    defectModel.remarks = "PQC Job Maintenance, updated by " + userName;
                }







                //update pqc data rollback
                bool updateResult = trackingBLL.UpdatePQCJobMaintenance(packTrackingModel, packDetailList, packDefectList);
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