using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserJobMaintance : System.Web.UI.Page
    {

        private readonly Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
        private readonly Common.BLL.LMMSWatchDog_His_BLL watchdogBLL = new Common.BLL.LMMSWatchDog_His_BLL();
        private readonly Common.BLL.LMMSWatchLog_BLL watchlogBLL = new Common.BLL.LMMSWatchLog_BLL();
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL deliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.Class.BLL.LMMSUserEventLog_BLL userEventBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
        private readonly Common.Class.BLL.LMMSBomDetail_BLL bomDetailBLL = new Common.Class.BLL.LMMSBomDetail_BLL();


      



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sJobNo = Request.QueryString["jobNumber"] == null ? "" : Request.QueryString["jobNumber"].ToString();
                    string sDay = Request.QueryString["day"] == null ? "" : Request.QueryString["day"].ToString();
                    string sShift = Request.QueryString["shift"] == null ? "" : Request.QueryString["shift"].ToString();
                    string sMachineID = Request.QueryString["machineID"] == null ? "" : Request.QueryString["machineID"].ToString();
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] receive job info --  jobno:{0}, day:{1}, shift:{2}, machineID:{3}", sJobNo, sDay, sShift, sMachineID));
                    
                    //缺少参数会导致多条记录更新.
                    if (sJobNo == "" || sDay == ""|| sShift==""||sMachineID=="")
                    {
                        Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Get job info fail, Please try again!", "./ProductivityDetail.aspx");
                        return;
                    }

                    this.lbDay.Text = sDay;
                    this.lbShift.Text = sShift;
                    this.lbMachineID.Text = sMachineID;
                    this.lbJob.Text = sJobNo;









                    //获取当前选中的 watchdog shift记录.
                    Common.Model.LMMSWatchDog_His_Model watchDogShiftModel = new Common.Model.LMMSWatchDog_His_Model();
                    watchDogShiftModel = watchdogBLL.GetModel(sJobNo, DateTime.Parse(sDay), sShift, sMachineID);
                    
                    
                    //设置 setup, buyoff, shortage的数量.
                    this.lbShortage.Text = watchDogShiftModel.shortage.ToString();
                    this.lbSetUp.Text = watchDogShiftModel.setupQty.ToString();
                    this.lbBuyoff.Text = watchDogShiftModel.buyoffQty.ToString();
                    this.txtShortage.Attributes["placeholder"] = watchDogShiftModel.shortage.ToString();
                    this.txtSetupQty.Attributes["placeholder"] = watchDogShiftModel.setupQty.ToString();
                    this.txtBuyoffQty.Attributes["placeholder"] = watchDogShiftModel.buyoffQty.ToString();
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set inventory info --  setup:{0}, buyoff:{1}, shortage:{2}", watchDogShiftModel.setupQty, watchDogShiftModel.buyoffQty, watchDogShiftModel.shortage));








                    //设置material detail info 列表信息
                    setMaterialDetailList(sJobNo, DateTime.Parse(sDay), sShift, sMachineID);


                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", "Page_Load Exception : " + ex.ToString());
                }
            }
        }
        







        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {


                #region textbox value  & Login  validation
                string sSetup = this.txtSetupQty.Text.Trim();
                string sBuyoff = this.txtBuyoffQty.Text.Trim();
                string sShortage = this.txtShortage.Text.Trim();

                //填了, 但填的不是数字报错提示
                if (sSetup != "" && !Common.CommFunctions.isNumberic(sSetup))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Setup must be number !");
                    this.txtSetupQty.Text = "";
                    this.txtSetupQty.Focus();
                    return;
                }

                if (sBuyoff != "" && !Common.CommFunctions.isNumberic(sBuyoff))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Buyoff must be number !");
                    this.txtBuyoffQty.Text = "";
                    this.txtBuyoffQty.Focus();
                    return;
                }

                if (sShortage != "" && !Common.CommFunctions.isNumberic(sShortage))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Shortage must be number !");
                    this.txtShortage.Text = "";
                    this.txtShortage.Focus();
                    return;
                }


                foreach (DataGridItem item in this.dgMaterialMaintain.Items)
                {
                    string sNG = ((TextBox)item.Cells[5].FindControl("txtActualNG")).Text.Trim();
                    if (sNG != "" && !Common.CommFunctions.isNumberic(sNG))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Actual NG must be number !");

                        ((TextBox)item.Cells[5].FindControl("txtActualNG")).Text = "";
                        ((TextBox)item.Cells[5].FindControl("txtActualNG")).Focus();
                        return;
                    }
                }

                if (this.radiobtnList.SelectedItem == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose job complete status !");
                    return;
                }


                //login
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
                bool loginResult = UserBll.Login(userName, password, out errorStr, StaticRes.Global.Department.Laser, StaticRes.Global.UserGroup.OPERATOR);

                if (!loginResult)
                {
                    Common.CommFunctions.ShowMessage(Page, errorStr);
                    return;
                }

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] Login By User --  user:{0}", userName));
                #endregion









                //记录用户维护操作记录
                List<Common.Class.Model.LMMSUserEventLog> userEventLogList = new List<Common.Class.Model.LMMSUserEventLog>();
                Common.Class.Model.LMMSUserEventLog userEventModel;












                #region set inventory model
                int preSetup = int.Parse(this.lbSetUp.Text);
                int preBuyoff = int.Parse(this.lbBuyoff.Text);
                int preShortage = int.Parse(this.lbShortage.Text);

                //setup, buyoff, shortage只要不填, 则取原本的值.
                sSetup = sSetup == "" ? this.lbSetUp.Text : sSetup;
                sBuyoff = sBuyoff == "" ? this.lbBuyoff.Text : sBuyoff;
                sShortage = sShortage == "" ? this.lbShortage.Text : sShortage;
                int iSetup = int.Parse(sSetup);
                int iBuyoff = int.Parse(sBuyoff);
                int iShortage = int.Parse(sShortage);


                //更新 inventory的数量
                Common.Class.Model.LMMSInventory_Model inventoryModel = inventoryBLL.GetModel(this.lbJob.Text);
                inventoryModel.JobNumber = this.lbJob.Text;
                inventoryModel.SetUp += (iSetup - preSetup);
                inventoryModel.Buyoff += (iBuyoff - preBuyoff);
                inventoryModel.PQCQuantity += (iShortage - preShortage);


                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update inventory data info --  jobno:{0}, setup:{1}, buyoff:{2}, shortage:{3}", inventoryModel.JobNumber, inventoryModel.SetUp, inventoryModel.Buyoff, inventoryModel.PQCQuantity));
                #endregion







                //添加 inventory修改记录
                userEventModel = new Common.Class.Model.LMMSUserEventLog();
                userEventModel.jobnumber = this.lbJob.Text;
                userEventModel.material = "";
                userEventModel.dateTime = DateTime.Now;
                userEventModel.startTime = DateTime.Now;
                userEventModel.endTime = DateTime.Now;
                userEventModel.eventType = "LaserJobMaintance";
                userEventModel.pageName = "LaserJobMaintance";
                userEventModel.action = string.Format("SetUp:{0} --> {1}, Buyoff:{2} --> {3}, WIP:{4} -- > {5}", preSetup, iSetup, preBuyoff, iBuyoff, preShortage, iShortage);
                userEventModel.temp1 = "Machine-" + this.lbMachineID.Text;
                userEventModel.temp2 = "";
                userEventModel.userID = userName;
                userEventLogList.Add(userEventModel);







            
                //获取该job的所有watchdog shift记录
                List<Common.Model.LMMSWatchDog_His_Model> watchdogModelList = new List<Common.Model.LMMSWatchDog_His_Model>();
                watchdogModelList = watchdogBLL.GetModelList(this.lbJob.Text);
                

                //从中获取当前选中维护的那条.
                Common.Model.LMMSWatchDog_His_Model curWatchdogModel = (from a in watchdogModelList
                                                                     where a.day == DateTime.Parse(lbDay.Text)
                                                                     && a.shift == this.lbShift.Text
                                                                     && a.machineID == this.lbMachineID.Text
                                                                     select a).FirstOrDefault();



                //用于记录log.
                int beforeTotalPass = curWatchdogModel.totalPass.Value;
                int beforeTotalFail = curWatchdogModel.totalFail.Value;



                curWatchdogModel.setupQty = iSetup;
                curWatchdogModel.buyoffQty = iBuyoff;
                curWatchdogModel.shortage = iShortage;





                //汇总该job总数量, 选中的除外
                var summaryModel = (from a in watchdogModelList
                               where a.day != DateTime.Parse(lbDay.Text)
                                 || a.shift != this.lbShift.Text
                                 || a.machineID != this.lbMachineID.Text
                               group a by a.jobNumber into c
                               select new
                               {
                                   jobNumber = c.Key,
                                   totalPass = c.Sum(p => p.totalPass),
                                   totalFail = c.Sum(p => p.totalFail),

                                   ok1Count = c.Sum(p => p.ok1Count),
                                   ok2Count = c.Sum(p => p.ok2Count),
                                   ok3Count = c.Sum(p => p.ok3Count),
                                   ok4Count = c.Sum(p => p.ok4Count),
                                   ok5Count = c.Sum(p => p.ok5Count),
                                   ok6Count = c.Sum(p => p.ok6Count),
                                   ok7Count = c.Sum(p => p.ok7Count),
                                   ok8Count = c.Sum(p => p.ok8Count),
                                   ok9Count = c.Sum(p => p.ok9Count),
                                   ok10Count = c.Sum(p => p.ok10Count),
                                   ok11Count = c.Sum(p => p.ok11Count),

                                   ng1Count = c.Sum(p => p.ng1Count),
                                   ng2Count = c.Sum(p => p.ng2Count),
                                   ng3Count = c.Sum(p => p.ng3Count),
                                   ng4Count = c.Sum(p => p.ng4Count),
                                   ng5Count = c.Sum(p => p.ng5Count),
                                   ng6Count = c.Sum(p => p.ng6Count),
                                   ng7Count = c.Sum(p => p.ng7Count),
                                   ng8Count = c.Sum(p => p.ng8Count),
                                   ng9Count = c.Sum(p => p.ng9Count),
                                   ng10Count = c.Sum(p => p.ng10Count),
                                   ng11Count = c.Sum(p => p.ng11Count),

                                   setupQty = c.Sum(p=>p.setupQty),
                                   buyoffQty = c.Sum(p => p.buyoffQty),
                                   shortage = c.Sum(p => p.shortage),

                               }).FirstOrDefault();
                


                
              
                foreach (DataGridItem item in this.dgMaterialMaintain.Items)
                {

                    int sn = int.Parse(item.Cells[0].Text);
                    string materialNo = item.Cells[1].Text;
                    string sActualNG = ((TextBox)item.Cells[5].FindControl("txtActualNG")).Text.Trim();
                    int dNG = sActualNG == "" ? int.Parse(item.Cells[4].Text) : int.Parse(sActualNG);

                    //ng维护减少的数量.
                    int decreasedNGQty = int.Parse(item.Cells[4].Text) - dNG;


                    bool jobComplete = this.radiobtnList.SelectedItem.Text == "Yes" ? true : false;
                    if (jobComplete)
                    {
                        #region  complete

                        //material qty按照mrp数量来计算.
                        int materialQty = int.Parse(inventoryModel.quantity.ToString());



                        switch (sn)
                        {
                            case 1:
                                //如果是xml命名异常的material
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model1Name = "";
                                    curWatchdogModel.ok1Count = 0;
                                    curWatchdogModel.ng1Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model1Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng) - setup - buyoff - shortage.
                                    curWatchdogModel.ok1Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok1Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng1Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng1Count = dNG;
                                }                              
                                break;
                            case 2:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model2Name = "";
                                    curWatchdogModel.ok2Count = 0;
                                    curWatchdogModel.ng2Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model2Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok2Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok2Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng2Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng2Count = dNG;
                                }
                                break;
                            case 3:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model3Name = "";
                                    curWatchdogModel.ok3Count = 0;
                                    curWatchdogModel.ng3Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model3Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok3Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok3Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng3Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng3Count = dNG;
                                }
                                break;
                            case 4:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model4Name = "";
                                    curWatchdogModel.ok4Count = 0;
                                    curWatchdogModel.ng4Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model4Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok4Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok4Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng4Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng4Count = dNG;
                                }
                                break;
                            case 5:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model5Name = "";
                                    curWatchdogModel.ok5Count = 0;
                                    curWatchdogModel.ng5Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model5Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok5Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok5Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng5Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng5Count = dNG;
                                }
                                break;
                            case 6:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model6Name = "";
                                    curWatchdogModel.ok6Count = 0;
                                    curWatchdogModel.ng6Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model6Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok6Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok6Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng6Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng6Count = dNG;
                                }
                                break;
                            case 7:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model7Name = "";
                                    curWatchdogModel.ok7Count = 0;
                                    curWatchdogModel.ng7Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model7Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok7Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok7Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng7Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng7Count = dNG;
                                }
                                break;
                            case 8:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model8Name = "";
                                    curWatchdogModel.ok8Count = 0;
                                    curWatchdogModel.ng8Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model8Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok8Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok8Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng8Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng8Count = dNG;
                                }
                                break;
                            case 9:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model9Name = "";
                                    curWatchdogModel.ok9Count = 0;
                                    curWatchdogModel.ng9Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model9Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok9Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok9Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng9Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng9Count = dNG;
                                }
                                break;
                            case 10:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model10Name = "";
                                    curWatchdogModel.ok10Count = 0;
                                    curWatchdogModel.ng10Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model10Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok10Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok10Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng10Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng10Count = dNG;
                                }
                                break;
                            case 11:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model11Name = "";
                                    curWatchdogModel.ok11Count = 0;
                                    curWatchdogModel.ng11Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model11Name = materialNo;
                                    //按照mrp的总数量 - 当前ng - (历史总ok + ng)
                                    curWatchdogModel.ok11Count = materialQty
                                        - dNG - iSetup - iBuyoff - iShortage
                                        - (summaryModel == null ? 0 : summaryModel.ok11Count)
                                        - (summaryModel == null ? 0 : summaryModel.ng11Count)
                                        - (summaryModel == null ? 0 : summaryModel.setupQty)
                                        - (summaryModel == null ? 0 : summaryModel.buyoffQty)
                                        - (summaryModel == null ? 0 : summaryModel.shortage);
                                    curWatchdogModel.ng11Count = dNG;
                                }
                                break;
                            default:
                                break;
                        }


                        #endregion
                    }
                    else
                    {
                        #region  not complete

                        switch (sn)
                        {
                            case 1:
                                //如果是xml命名异常的material
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model1Name = "";
                                    curWatchdogModel.ok1Count = 0;
                                    curWatchdogModel.ng1Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model1Name = materialNo;
                                    curWatchdogModel.ok1Count = curWatchdogModel.ok1Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng1Count = dNG;
                                }
                                break;
                            case 2:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model2Name = "";
                                    curWatchdogModel.ok2Count = 0;
                                    curWatchdogModel.ng2Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model2Name = materialNo;
                                    curWatchdogModel.ok2Count = curWatchdogModel.ok2Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng2Count = dNG;
                                }
                                break;
                            case 3:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model3Name = "";
                                    curWatchdogModel.ok3Count = 0;
                                    curWatchdogModel.ng3Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model3Name = materialNo;
                                    curWatchdogModel.ok3Count = curWatchdogModel.ok3Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng3Count = dNG;
                                }
                                break;
                            case 4:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model4Name = "";
                                    curWatchdogModel.ok4Count = 0;
                                    curWatchdogModel.ng4Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model4Name = materialNo;
                                    curWatchdogModel.ok4Count = curWatchdogModel.ok4Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng4Count = dNG;
                                }
                                break;
                            case 5:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model5Name = "";
                                    curWatchdogModel.ok5Count = 0;
                                    curWatchdogModel.ng5Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model5Name = materialNo;
                                    curWatchdogModel.ok5Count = curWatchdogModel.ok5Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng5Count = dNG;
                                }
                                break;
                            case 6:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model6Name = "";
                                    curWatchdogModel.ok6Count = 0;
                                    curWatchdogModel.ng6Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model6Name = materialNo;
                                    curWatchdogModel.ok6Count = curWatchdogModel.ok6Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng6Count = dNG;
                                }
                                break;
                            case 7:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model7Name = "";
                                    curWatchdogModel.ok7Count = 0;
                                    curWatchdogModel.ng7Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model7Name = materialNo;
                                    curWatchdogModel.ok7Count = curWatchdogModel.ok7Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng7Count = dNG;
                                }
                                break;
                            case 8:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model8Name = "";
                                    curWatchdogModel.ok8Count = 0;
                                    curWatchdogModel.ng8Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model8Name = materialNo;
                                    curWatchdogModel.ok8Count = curWatchdogModel.ok8Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng8Count = dNG;
                                }
                                break;
                            case 9:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model9Name = "";
                                    curWatchdogModel.ok9Count = 0;
                                    curWatchdogModel.ng9Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model9Name = materialNo;
                                    curWatchdogModel.ok9Count = curWatchdogModel.ok9Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng9Count = dNG;
                                }
                                break;
                            case 10:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model10Name = "";
                                    curWatchdogModel.ok10Count = 0;
                                    curWatchdogModel.ng10Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model10Name = materialNo;
                                    curWatchdogModel.ok10Count = curWatchdogModel.ok10Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng10Count = dNG;
                                }
                                break;
                            case 11:
                                if (materialNo.ToUpper().Contains("ERROR"))
                                {
                                    curWatchdogModel.model11Name = "";
                                    curWatchdogModel.ok11Count = 0;
                                    curWatchdogModel.ng11Count = 0;
                                }
                                else
                                {
                                    curWatchdogModel.model11Name = materialNo;
                                    curWatchdogModel.ok11Count = curWatchdogModel.ok11Count + decreasedNGQty - (iSetup - preSetup) - (iBuyoff - preBuyoff) - (iShortage - preShortage);
                                    curWatchdogModel.ng11Count = dNG;
                                }
                                break;
                            default:
                                break;
                        }


                        #endregion
                    }                    
                }

                curWatchdogModel.totalPass = curWatchdogModel.ok1Count + curWatchdogModel.ok2Count + curWatchdogModel.ok3Count
                                                + curWatchdogModel.ok4Count + curWatchdogModel.ok5Count + curWatchdogModel.ok6Count + curWatchdogModel.ok7Count
                                                + curWatchdogModel.ok8Count + curWatchdogModel.ok9Count + curWatchdogModel.ok10Count + curWatchdogModel.ok11Count;
                curWatchdogModel.totalFail = curWatchdogModel.ng1Count + curWatchdogModel.ng2Count + curWatchdogModel.ng3Count
                                             + curWatchdogModel.ng4Count + curWatchdogModel.ng5Count + curWatchdogModel.ng6Count + curWatchdogModel.ng7Count
                                             + curWatchdogModel.ng8Count + curWatchdogModel.ng9Count + curWatchdogModel.ng10Count + curWatchdogModel.ng11Count;


                //添加 watchdog shift修改记录
                userEventModel = new Common.Class.Model.LMMSUserEventLog();
                userEventModel.jobnumber = this.lbJob.Text;
                userEventModel.material = "";
                userEventModel.dateTime = DateTime.Now;
                userEventModel.startTime = DateTime.Now;
                userEventModel.endTime = DateTime.Now;
                userEventModel.eventType = "LaserJobMaintance";
                userEventModel.pageName = "LaserJobMaintance";
                userEventModel.action = string.Format("totalPass:{0} --> {1}, totalFail:{2} --> {3}", beforeTotalPass, curWatchdogModel.totalPass, beforeTotalFail, curWatchdogModel.totalFail);
                userEventModel.temp1 = "Machine-" + this.lbMachineID.Text;
                userEventModel.temp2 = "";
                userEventModel.userID = userName;
                userEventLogList.Add(userEventModel);








                //watch log
                Common.Model.LMMSWatchLog_Model watchlogModel = new Common.Model.LMMSWatchLog_Model();
                watchlogModel.machineID = this.lbMachineID.Text;
                watchlogModel.jobNumber = this.lbJob.Text;
                watchlogModel.totalPass = curWatchdogModel.totalPass + (summaryModel == null ? 0 : summaryModel.totalPass);
                watchlogModel.totalFail = curWatchdogModel.totalFail + (summaryModel == null ? 0 : summaryModel.totalFail);

                watchlogModel.ok1Count = curWatchdogModel.ok1Count + (summaryModel == null ? 0 : summaryModel.ok1Count);
                watchlogModel.ok2Count = curWatchdogModel.ok2Count + (summaryModel == null ? 0 : summaryModel.ok2Count);
                watchlogModel.ok3Count = curWatchdogModel.ok3Count + (summaryModel == null ? 0 : summaryModel.ok3Count);
                watchlogModel.ok4Count = curWatchdogModel.ok4Count + (summaryModel == null ? 0 : summaryModel.ok4Count);
                watchlogModel.ok5Count = curWatchdogModel.ok5Count + (summaryModel == null ? 0 : summaryModel.ok5Count);
                watchlogModel.ok6Count = curWatchdogModel.ok6Count + (summaryModel == null ? 0 : summaryModel.ok6Count);
                watchlogModel.ok7Count = curWatchdogModel.ok7Count + (summaryModel == null ? 0 : summaryModel.ok7Count);
                watchlogModel.ok8Count = curWatchdogModel.ok8Count + (summaryModel == null ? 0 : summaryModel.ok8Count); 
                watchlogModel.ok9Count = curWatchdogModel.ok9Count + (summaryModel == null ? 0 : summaryModel.ok9Count);
                watchlogModel.ok10Count = curWatchdogModel.ok10Count + (summaryModel == null ? 0 : summaryModel.ok10Count);
                watchlogModel.ok11Count = curWatchdogModel.ok11Count + (summaryModel == null ? 0 : summaryModel.ok11Count);
                
                watchlogModel.ng1Count = curWatchdogModel.ng1Count + (summaryModel == null ? 0 : summaryModel.ng1Count);
                watchlogModel.ng2Count = curWatchdogModel.ng2Count + (summaryModel == null ? 0 : summaryModel.ng2Count);
                watchlogModel.ng3Count = curWatchdogModel.ng3Count + (summaryModel == null ? 0 : summaryModel.ng3Count);
                watchlogModel.ng4Count = curWatchdogModel.ng4Count + (summaryModel == null ? 0 : summaryModel.ng4Count);
                watchlogModel.ng5Count = curWatchdogModel.ng5Count + (summaryModel == null ? 0 : summaryModel.ng5Count);
                watchlogModel.ng6Count = curWatchdogModel.ng6Count + (summaryModel == null ? 0 : summaryModel.ng6Count);
                watchlogModel.ng7Count = curWatchdogModel.ng7Count + (summaryModel == null ? 0 : summaryModel.ng7Count);
                watchlogModel.ng8Count = curWatchdogModel.ng8Count + (summaryModel == null ? 0 : summaryModel.ng8Count);
                watchlogModel.ng9Count = curWatchdogModel.ng9Count + (summaryModel == null ? 0 : summaryModel.ng9Count);
                watchlogModel.ng10Count = curWatchdogModel.ng10Count + (summaryModel == null ? 0 : summaryModel.ng10Count);
                watchlogModel.ng11Count = curWatchdogModel.ng11Count + (summaryModel == null ? 0 : summaryModel.ng11Count);

                





                //update laser data rollback
                try
                {
                    bool updateResult = watchdogBLL.JobMaintainRollBack(curWatchdogModel, watchlogModel, inventoryModel);
                    if (updateResult == false)
                    {

                        DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] Update Laser Job Fail!");
                        Common.CommFunctions.ShowMessage(this.Page, "Update Laser Job Fail!");
                        return;
                    }
                }
                catch (Exception ee)
                {
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] Update Laser Job Fail! Exception:"+ee.ToString());
                    Common.CommFunctions.ShowMessage(this.Page, "Update Laser Job Fail! Exception:" + ee.ToString());
                    return;
                }






                //更新 paint delivery his paint rej的数量.
                try
                {
                    if (!deliveryBLL.UpdatePaintRej(this.lbJob.Text, iShortage, "Paint#1"))
                    {
                        DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] update painting delivery history failed !");
                    }
                    else
                    {

                        //添加 paint delivery his的更新记录
                        userEventModel = new Common.Class.Model.LMMSUserEventLog();
                        userEventModel.jobnumber = this.lbJob.Text;
                        userEventModel.material = "";
                        userEventModel.dateTime = DateTime.Now;
                        userEventModel.startTime = DateTime.Now;
                        userEventModel.endTime = DateTime.Now;
                        userEventModel.eventType = "LaserJobMaintance";
                        userEventModel.pageName = "LaserJobMaintance";
                        userEventModel.action = string.Format("[PaintingDeliveryHis]  update paintRejQty to {0}", iShortage);
                        userEventModel.temp1 = "";
                        userEventModel.temp2 = "";
                        userEventModel.userID = userName;
                        userEventLogList.Add(userEventModel);
                    }

                }
                catch (Exception ee)
                {
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] update painting delivery history failed ! Exception:" +ee.ToString());
                }







                //保存user 操作记录.
                userEventBLL.AddRollBack(userEventLogList);







                Response.Redirect("./ProductivityDetail.aspx?jobNumber=" + this.lbJob.Text, false);


            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", "btn_generate_Click Exception : " + ex.ToString());
            }
        }

        
        



     

        private void setMaterialDetailList(string sJobNo, DateTime dDay, string sShift, string sMachineID)
        {
            DataTable dtMaterial = watchdogBLL.GetJobMaterialList(sJobNo, dDay, sShift, sMachineID);
            if (dtMaterial == null || dtMaterial.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set material detail list --  get material datatable null !"));

                Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Get Material Detail Info fail, Please confirm the job is run or not!", "./ProductivityDetail.aspx");
                return;
            }
            

            this.dgMaterialMaintain.DataSource = dtMaterial.DefaultView;
            this.dgMaterialMaintain.DataBind();



            foreach (DataGridItem item in this.dgMaterialMaintain.Items)
            {
                ((TextBox)item.Cells[5].FindControl("txtActualNG")).Attributes["placeholder"] = item.Cells[4].Text;
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set material detail list --  sn: {0}, material no: {1}, ng{2}",item.Cells[0].Text, item.Cells[1].Text, item.Cells[4].Text));
            }
        }






        
    }
}