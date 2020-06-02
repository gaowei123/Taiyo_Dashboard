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
        private readonly Common.Class.BLL.PQCQaViTracking_BLL viTrackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
        private readonly Common.Class.BLL.PQCQaViDetailTracking_BLL detailBLL = new Common.Class.BLL.PQCQaViDetailTracking_BLL();
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL deliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.Class.BLL.LMMSUserEventLog_BLL userEventBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();


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






                    //设置 setup, buyoff, shortage的数量.
                    setInventoryDetail(sJobNo);







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


                #region textbox value validation
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
                    string sOK = ((TextBox)item.Cells[3].FindControl("txtActualOK")).Text.Trim();
                    if (sOK != "" && !Common.CommFunctions.isNumberic(sOK))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Actual OK must be number !");

                        ((TextBox)item.Cells[3].FindControl("txtActualOK")).Text = "";
                        ((TextBox)item.Cells[3].FindControl("txtActualOK")).Focus();
                        return;
                    }


                    string sNG = ((TextBox)item.Cells[6].FindControl("txtActualNG")).Text.Trim();
                    if (sNG != "" && !Common.CommFunctions.isNumberic(sNG))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Actual NG must be number !");

                        ((TextBox)item.Cells[6].FindControl("txtActualNG")).Text = "";
                        ((TextBox)item.Cells[6].FindControl("txtActualNG")).Focus();
                        return;
                    }
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
                bool loginResult = UserBll.Login(userName, password, out errorStr, StaticRes.Global.Department.Laser, StaticRes.Global.UserGroup.OPERATOR);

                if (!loginResult)
                {
                    Common.CommFunctions.ShowMessage(Page, errorStr);
                    return;
                }
                #endregion

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] Login By User --  user:{0}", userName));







                //记录用户维护操作记录
                List<Common.Class.Model.LMMSUserEventLog> userEventLogList = new List<Common.Class.Model.LMMSUserEventLog>();
                Common.Class.Model.LMMSUserEventLog userEventModel;







                #region set inventory model

                //setup, buyoff, shortage只要不填, 则取原本的值.
                sSetup = sSetup == "" ? this.lbSetUp.Text : sSetup;
                sBuyoff = sBuyoff == "" ? this.lbBuyoff.Text : sBuyoff;
                sShortage = sShortage == "" ? this.lbShortage.Text : sShortage;
                
                int dSetup = int.Parse(sSetup);
                int dBuyoff = int.Parse(sBuyoff);
                int dShortage = int.Parse(sShortage);


                //更新 inventory的数量
                Common.Class.Model.LMMSInventory_Model inventoryModel = new Common.Class.Model.LMMSInventory_Model();
                inventoryModel.JobNumber = this.lbJob.Text;
                inventoryModel.SetUp = dSetup;
                inventoryModel.Buyoff = dBuyoff;
                inventoryModel.PQCQuantity = dShortage;
                #endregion

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update inventory data info --  jobno:{0}, setup:{1}, buyoff:{2}, shortage:{3}", inventoryModel.JobNumber, inventoryModel.SetUp, inventoryModel.Buyoff, inventoryModel.PQCQuantity));






                //添加 inventory修改记录
                userEventModel = new Common.Class.Model.LMMSUserEventLog();
                userEventModel.jobnumber = this.lbJob.Text;
                userEventModel.material = "";
                userEventModel.dateTime = DateTime.Now;
                userEventModel.startTime = DateTime.Now;
                userEventModel.endTime = DateTime.Now;
                userEventModel.eventType = "LaserJobMaintance";
                userEventModel.pageName = "LaserJobMaintance";
                userEventModel.action = string.Format("SetUp:{0} --> {1}, Buyoff:{2} --> {3}, WIP:{4} -- > {5}", lbSetUp.Text, inventoryModel.SetUp, lbBuyoff.Text, inventoryModel.Buyoff, lbShortage.Text, inventoryModel.PQCQuantity); ;
                userEventModel.temp1 = "Machine-" + this.lbMachineID.Text;
                userEventModel.temp2 = "";
                userEventModel.userID = userName;
                userEventLogList.Add(userEventModel);








                //获取该job 所有watchdog shift model 记录
                List<Common.Model.LMMSWatchDog_His_Model> watchdogModelList = new List<Common.Model.LMMSWatchDog_His_Model>();
                watchdogModelList = watchdogBLL.GetModelList(this.lbJob.Text);







                #region set watchDogShift
                //watch dog shift model 更新当前选定这条.
                Common.Model.LMMSWatchDog_His_Model watchdogModel = (from a in watchdogModelList
                                                                     where a.day == DateTime.Parse(lbDay.Text)
                                                                     && a.shift == this.lbShift.Text
                                                                     && a.machineID == this.lbMachineID.Text
                                                                     select a).FirstOrDefault();
                foreach (DataGridItem item in this.dgMaterialMaintain.Items)
                {
                    string materialNo = item.Cells[1].Text;
                    if (materialNo.Contains("Error"))
                        continue;

                  
                    string sn = item.Cells[0].Text;


                    //如果actual ok/ng 不填的话保留原本的值.
                    string sActualOK = ((TextBox)item.Cells[3].FindControl("txtActualOK")).Text.Trim();
                    int actualOK = sActualOK == "" ? int.Parse(item.Cells[2].Text) : int.Parse(sActualOK);

                    string sActualNG = ((TextBox)item.Cells[6].FindControl("txtActualNG")).Text.Trim();
                    int actualNG = sActualNG == "" ? int.Parse(item.Cells[5].Text) : int.Parse(sActualNG);

                    
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update watchdogshift material data info --  materialNo:{0}, actual ok:{1}, actual ng:{2}", materialNo, actualOK, actualNG));


                    #region set ok/ng 1-16
                    switch (sn)
                    {
                        case "1":
                            watchdogModel.ok1Count = actualOK;
                            watchdogModel.ng1Count = actualNG;
                            break;

                        case "2":
                            watchdogModel.ok2Count = actualOK;
                            watchdogModel.ng2Count = actualNG;
                            break;
                        case "3":
                            watchdogModel.ok3Count = actualOK;
                            watchdogModel.ng3Count = actualNG;
                            break;
                        case "4":
                            watchdogModel.ok4Count = actualOK;
                            watchdogModel.ng4Count = actualNG;
                            break;
                        case "5":
                            watchdogModel.ok5Count = actualOK;
                            watchdogModel.ng5Count = actualNG;
                            break;
                        case "6":
                            watchdogModel.ok6Count = actualOK;
                            watchdogModel.ng6Count = actualNG;
                            break;
                        case "7":
                            watchdogModel.ok7Count = actualOK;
                            watchdogModel.ng7Count = actualNG;
                            break;
                        case "8":
                            watchdogModel.ok8Count = actualOK;
                            watchdogModel.ng8Count = actualNG;
                            break;
                        case "9":
                            watchdogModel.ok9Count = actualOK;
                            watchdogModel.ng9Count = actualNG;
                            break;
                        case "10":
                            watchdogModel.ok10Count = actualOK;
                            watchdogModel.ng10Count = actualNG;
                            break;
                        case "11":
                            watchdogModel.ok11Count = actualOK;
                            watchdogModel.ng11Count = actualNG;
                            break;
                        case "12":
                            watchdogModel.ok12Count = actualOK;
                            watchdogModel.ng12Count = actualNG;
                            break;
                        case "13":
                            watchdogModel.ok13Count = actualOK;
                            watchdogModel.ng13Count = actualNG;
                            break;
                        case "14":
                            watchdogModel.ok14Count = actualOK;
                            watchdogModel.ng14Count = actualNG;
                            break;
                        case "15":
                            watchdogModel.ok15Count = actualOK;
                            watchdogModel.ng15Count = actualNG;
                            break;
                        case "16":
                            watchdogModel.ok16Count = actualOK;
                            watchdogModel.ng16Count = actualNG;
                            break;

                        default:
                            break;
                    }
                    #endregion






                    //添加 watchdog shift每个material no的修改记录
                    userEventModel = new Common.Class.Model.LMMSUserEventLog();
                    userEventModel.jobnumber = this.lbJob.Text;
                    userEventModel.material = materialNo;
                    userEventModel.dateTime = DateTime.Now;
                    userEventModel.startTime = DateTime.Now;
                    userEventModel.endTime = DateTime.Now;
                    userEventModel.eventType = "LaserJobMaintance";
                    userEventModel.pageName = "LaserJobMaintance";
                    userEventModel.action = string.Format("totalPass:{0} --> {1}, totalFail:{2} --> {3}", item.Cells[2].Text, actualOK, item.Cells[5].Text, actualNG);
                    userEventModel.temp1 = "Machine-" + this.lbMachineID.Text;
                    userEventModel.temp2 = "";
                    userEventModel.userID = userName;
                    userEventLogList.Add(userEventModel);

                }

                int beforeTotalPass = watchdogModel.totalPass.Value;
                int beforeTotalFail = watchdogModel.totalFail.Value;

                watchdogModel.totalPass = (watchdogModel.ok1Count + watchdogModel.ok2Count + watchdogModel.ok3Count + watchdogModel.ok4Count + watchdogModel.ok5Count + watchdogModel.ok6Count + watchdogModel.ok7Count + watchdogModel.ok8Count + watchdogModel.ok9Count + watchdogModel.ok10Count + watchdogModel.ok11Count + watchdogModel.ok12Count + watchdogModel.ok13Count + watchdogModel.ok14Count + watchdogModel.ok15Count + watchdogModel.ok16Count);
                watchdogModel.totalFail = (watchdogModel.ng1Count + watchdogModel.ng2Count + watchdogModel.ng3Count + watchdogModel.ng4Count + watchdogModel.ng5Count + watchdogModel.ng6Count + watchdogModel.ng7Count + watchdogModel.ng8Count + watchdogModel.ng9Count + watchdogModel.ng10Count + watchdogModel.ng11Count + watchdogModel.ng12Count + watchdogModel.ng13Count + watchdogModel.ng14Count + watchdogModel.ng15Count + watchdogModel.ng16Count);

                #endregion 

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update watchdogshift data info --  jobno:{0}, total pass:{1}, total fail:{2}", this.lbJob.Text, watchdogModel.totalPass, watchdogModel.totalFail));








                //添加 watchdog shift修改记录
                userEventModel = new Common.Class.Model.LMMSUserEventLog();
                userEventModel.jobnumber = this.lbJob.Text;
                userEventModel.material = "";
                userEventModel.dateTime = DateTime.Now;
                userEventModel.startTime = DateTime.Now;
                userEventModel.endTime = DateTime.Now;
                userEventModel.eventType = "LaserJobMaintance";
                userEventModel.pageName = "LaserJobMaintance";
                userEventModel.action = string.Format("totalPass:{0} --> {1}, totalFail:{2} --> {3}", beforeTotalPass, watchdogModel.totalPass, beforeTotalFail, watchdogModel.totalFail); ;
                userEventModel.temp1 = "Machine-" + this.lbMachineID.Text;
                userEventModel.temp2 = "";
                userEventModel.userID = userName;
                userEventLogList.Add(userEventModel);










                #region set watchlog model  
                //不管原先watchlog中数量是多少,  统一更新为当前watchdog shift中该job各数量总和.
                //** linq var定义的变量还是指向原list集合, 不是new.  watchdogModel的修改已经同步在watchdogModelList.
                var watchdogShiftSummary = (from a in watchdogModelList
                                            group a by a.jobNumber into summary
                                            select new
                                            {
                                                totalPass = summary.Sum(p => p.totalPass),
                                                totalFail = summary.Sum(p => p.totalFail),
                                                ok1Count = summary.Sum(p => p.ok1Count),
                                                ok2Count = summary.Sum(p => p.ok2Count),
                                                ok3Count = summary.Sum(p => p.ok3Count),
                                                ok4Count = summary.Sum(p => p.ok4Count),
                                                ok5Count = summary.Sum(p => p.ok5Count),
                                                ok6Count = summary.Sum(p => p.ok6Count),
                                                ok7Count = summary.Sum(p => p.ok7Count),
                                                ok8Count = summary.Sum(p => p.ok8Count),
                                                ok9Count = summary.Sum(p => p.ok9Count),
                                                ok10Count = summary.Sum(p => p.ok10Count),
                                                ok11Count = summary.Sum(p => p.ok11Count),
                                                ok12Count = summary.Sum(p => p.ok12Count),
                                                ok13Count = summary.Sum(p => p.ok13Count),
                                                ok14Count = summary.Sum(p => p.ok14Count),
                                                ok15Count = summary.Sum(p => p.ok15Count),
                                                ok16Count = summary.Sum(p => p.ok16Count),
                                                ng1Count = summary.Sum(p => p.ng1Count),
                                                ng2Count = summary.Sum(p => p.ng2Count),
                                                ng3Count = summary.Sum(p => p.ng3Count),
                                                ng4Count = summary.Sum(p => p.ng4Count),
                                                ng5Count = summary.Sum(p => p.ng5Count),
                                                ng6Count = summary.Sum(p => p.ng6Count),
                                                ng7Count = summary.Sum(p => p.ng7Count),
                                                ng8Count = summary.Sum(p => p.ng8Count),
                                                ng9Count = summary.Sum(p => p.ng9Count),
                                                ng10Count = summary.Sum(p => p.ng10Count),
                                                ng11Count = summary.Sum(p => p.ng11Count),
                                                ng12Count = summary.Sum(p => p.ng12Count),
                                                ng13Count = summary.Sum(p => p.ng13Count),
                                                ng14Count = summary.Sum(p => p.ng14Count),
                                                ng15Count = summary.Sum(p => p.ng15Count),
                                                ng16Count = summary.Sum(p => p.ng16Count)
                                            }).FirstOrDefault();


                Common.Model.LMMSWatchLog_Model watchlogModel = new Common.Model.LMMSWatchLog_Model();
                watchlogModel.machineID = this.lbMachineID.Text;
                watchlogModel.jobNumber = this.lbJob.Text;
                watchlogModel.totalPass = watchdogShiftSummary.totalPass;
                watchlogModel.totalFail = watchdogShiftSummary.totalFail;

                watchlogModel.ok1Count = watchdogShiftSummary.ok1Count;
                watchlogModel.ok2Count = watchdogShiftSummary.ok2Count;
                watchlogModel.ok3Count = watchdogShiftSummary.ok3Count;
                watchlogModel.ok4Count = watchdogShiftSummary.ok4Count;
                watchlogModel.ok5Count = watchdogShiftSummary.ok5Count;
                watchlogModel.ok6Count = watchdogShiftSummary.ok6Count;
                watchlogModel.ok7Count = watchdogShiftSummary.ok7Count;
                watchlogModel.ok8Count = watchdogShiftSummary.ok8Count;
                watchlogModel.ok9Count = watchdogShiftSummary.ok9Count;
                watchlogModel.ok10Count = watchdogShiftSummary.ok10Count;
                watchlogModel.ok11Count = watchdogShiftSummary.ok11Count;
                watchlogModel.ok12Count = watchdogShiftSummary.ok12Count;
                watchlogModel.ok13Count = watchdogShiftSummary.ok13Count;
                watchlogModel.ok14Count = watchdogShiftSummary.ok14Count;
                watchlogModel.ok15Count = watchdogShiftSummary.ok15Count;
                watchlogModel.ok16Count = watchdogShiftSummary.ok16Count;

                watchlogModel.ng1Count = watchdogShiftSummary.ng1Count;
                watchlogModel.ng2Count = watchdogShiftSummary.ng2Count;
                watchlogModel.ng3Count = watchdogShiftSummary.ng3Count;
                watchlogModel.ng4Count = watchdogShiftSummary.ng4Count;
                watchlogModel.ng5Count = watchdogShiftSummary.ng5Count;
                watchlogModel.ng6Count = watchdogShiftSummary.ng6Count;
                watchlogModel.ng7Count = watchdogShiftSummary.ng7Count;
                watchlogModel.ng8Count = watchdogShiftSummary.ng8Count;
                watchlogModel.ng9Count = watchdogShiftSummary.ng9Count;
                watchlogModel.ng10Count = watchdogShiftSummary.ng10Count;
                watchlogModel.ng11Count = watchdogShiftSummary.ng11Count;
                watchlogModel.ng12Count = watchdogShiftSummary.ng12Count;
                watchlogModel.ng13Count = watchdogShiftSummary.ng13Count;
                watchlogModel.ng14Count = watchdogShiftSummary.ng14Count;
                watchlogModel.ng15Count = watchdogShiftSummary.ng15Count;
                watchlogModel.ng16Count = watchdogShiftSummary.ng16Count;


                #endregion

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update watchlog data pass info  --  jobno:{0}, total pass:{1}{2}, ok1:{3}, ok2:{4}, ok3:{5}, ok4:{6}, ok5:{7}, ok6:{8}, ok7:{9}, ok8:{10}, ok9:{11}, ok10:{12},ok11:{13}", this.lbJob.Text, watchdogModel.totalPass, "", watchlogModel.ok1Count, watchlogModel.ok2Count, watchlogModel.ok3Count, watchlogModel.ok4Count, watchlogModel.ok5Count, watchlogModel.ok6Count, watchlogModel.ok7Count, watchlogModel.ok8Count, watchlogModel.ok9Count, watchlogModel.ok10Count, watchlogModel.ok11Count));
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] update watchlog data fail info  --  jobno:{0}, total fail:{1}{2}, ng1:{3}, ng2:{4}, ng3:{5}, ng4:{6}, ng5:{7}, ng6:{8}, ng7:{9}, ng8:{10}, ng9:{11}, ng10:{12},ng11:{13}", this.lbJob.Text, "", watchdogModel.totalFail, watchlogModel.ng1Count, watchlogModel.ng2Count, watchlogModel.ng3Count, watchlogModel.ng4Count, watchlogModel.ng5Count, watchlogModel.ng6Count, watchlogModel.ng7Count, watchlogModel.ng8Count, watchlogModel.ng9Count, watchlogModel.ng10Count, watchlogModel.ng11Count));







                //update laser data rollback
                try
                {
                    bool updateResult = watchdogBLL.JobMaintainRollBack(watchdogModel, watchlogModel, inventoryModel);
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
             










                #region update   pqc viTracking , detailTracking model

                bool jobCheckingFlag = viTrackingBLL.IsChecking(this.lbJob.Text);
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click] PQC Job checking flag --  jobno:{0}, jobCheckingFlag:{1}", this.lbJob.Text, jobCheckingFlag));


                // 获取该job  vi tracking最新的记录
                Common.Class.Model.PQCQaViTracking trackingModel = viTrackingBLL.GetLatestModelByJob(this.lbJob.Text);

                if (trackingModel == null) DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] The model got from Vi Tracking  is null ");


                //pqc结束checking后, 才联动更新.
                if (jobCheckingFlag == false  && trackingModel != null)
                {

                    //shortage paint缺少的数量, 不经过pqc, 不做联动.
                    //buyoff laser拿出做buyoff展示用, 由于后来维护, 改数量是算在
                    //setup laser从job中拿出好的用来调试机器,是不经过pqc的 不做联动.
                    int iCurrentBuyoff = int.Parse(this.lbBuyoff.Text);
                    int iActualBuyoff = this.txtBuyoffQty.Text.Trim() == "" ? iCurrentBuyoff : int.Parse(this.txtBuyoffQty.Text.Trim());
                    int increaseBuyoffQty = iActualBuyoff - iCurrentBuyoff;

                    
                    //联动累加给 vitracking model total的数量. 
                    int increaseOKQty = 0;
                    
                    //setup 是set的数量, pqcqacitracking的totalqty是pcs的数量, 计算totalqty, acceptqty要*materialCount
                    int materialCount = this.dgMaterialMaintain.Items.Count;




                   

                    int sourceTrackTotalQty = int.Parse(trackingModel.TotalQty);
                    int sourceTrackAcceptQty = int.Parse(trackingModel.acceptQty);
                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click]  get tracking model  --  trackingID:{0}, totalQty:{1}, acceptQty:{2}", trackingModel.trackingID, sourceTrackTotalQty, sourceTrackAcceptQty));






                    List<Common.Class.Model.PQCQaViDetailTracking_Model> detailModelList = new List<Common.Class.Model.PQCQaViDetailTracking_Model>();

                    //处理 detail tracking model
                    foreach (DataGridItem item in this.dgMaterialMaintain.Items)
                    {
                        //异常的laser material no, 跳过.
                        string materialNo = item.Cells[1].Text;
                        if (materialNo.Contains("Error"))
                            continue;



                        //获取该trackingID的 detail model
                        Common.Class.Model.PQCQaViDetailTracking_Model detailModel = new Common.Class.Model.PQCQaViDetailTracking_Model();
                        detailModel = detailBLL.GetModel(trackingModel.trackingID, materialNo);
                        if (detailModel == null)
                            continue;



                        decimal sourceDetailTotalQty = detailModel.totalQty.Value;
                        decimal sourceDetailPassQty = detailModel.passQty.Value;
                        DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click]  get detail tracking model  --  material no:{0}, totalqty:{1}, passqty:{2}", detailModel.materialPartNo, sourceDetailTotalQty, sourceDetailPassQty));




                        
                        string sCurrentOK = item.Cells[2].Text;
                        string sActualOK = ((TextBox)item.Cells[3].FindControl("txtActualOK")).Text.Trim();
                        //如果不填actual ok数量则保留原本的数量.
                        sActualOK = sActualOK == "" ? sCurrentOK : sActualOK; 


                        int dCurrentOK = int.Parse(sCurrentOK);
                        int dActualOK = int.Parse(sActualOK);
                        int inscreaseMaterialOkQty = (dActualOK - dCurrentOK);//新增ok的数量




                        //累加新增的ok数量并扣除setup新增的数量.
                        detailModel.totalQty = detailModel.totalQty + inscreaseMaterialOkQty - increaseBuyoffQty;
                        detailModel.passQty = detailModel.passQty + inscreaseMaterialOkQty - increaseBuyoffQty;
                        detailModel.lastUpdatedTime = DateTime.Now;
                        detailModel.remarks = "auto update by laser job maintenance";
                        detailModelList.Add(detailModel);



                        DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click]  update detail tracking model  --  material no:{0}, totalqty:{1}-->{2}, passqty:{3}-->{4}", detailModel.materialPartNo, sourceDetailTotalQty, detailModel.totalQty, sourceDetailPassQty, detailModel.passQty));



                        //累加每个material ok的数量.
                        increaseOKQty += inscreaseMaterialOkQty;




                        //添加 pqc vi tracking每个material no的修改记录
                        userEventModel = new Common.Class.Model.LMMSUserEventLog();
                        userEventModel.jobnumber = this.lbJob.Text;
                        userEventModel.material = materialNo;
                        userEventModel.dateTime = DateTime.Now;
                        userEventModel.startTime = DateTime.Now;
                        userEventModel.endTime = DateTime.Now;
                        userEventModel.eventType = "LaserJobMaintance";
                        userEventModel.pageName = "LaserJobMaintance";
                        userEventModel.action = string.Format("[PQCQaViDetailTracking]  totalQty:{0} --> {1}, totalPass:{2} --> {3}", sourceDetailTotalQty, detailModel.totalQty, sourceDetailPassQty, detailModel.passQty);
                        userEventModel.temp1 = "station-" + trackingModel.machineID;
                        userEventModel.temp2 = trackingModel.trackingID;
                        userEventModel.userID = userName;
                        userEventLogList.Add(userEventModel);
                    }


                    

                    //totalqty, acceptQty是string类型, 将错就错,懒得改.

                    //累加新怎ok的数量, 累减setup减少的数量.
                    trackingModel.TotalQty = (int.Parse(trackingModel.TotalQty) - increaseBuyoffQty * materialCount + increaseOKQty).ToString();
                    trackingModel.acceptQty = (int.Parse(trackingModel.acceptQty) - increaseBuyoffQty * materialCount + increaseOKQty).ToString();
                    trackingModel.lastUpdatedTime = DateTime.Now;
                    trackingModel.remarks = "auto update by laser job mainteannce";


                    DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[btn Submit Click]  update tracking model  --  jobno:{0}, totalqty:{1}-->{2}, acceptqty:{3}-->{4}", trackingModel.jobId, sourceTrackTotalQty, trackingModel.TotalQty, sourceTrackAcceptQty, trackingModel.acceptQty));





                    //添加 pqc vi tracking每个material no的修改记录
                    userEventModel = new Common.Class.Model.LMMSUserEventLog();
                    userEventModel.jobnumber = this.lbJob.Text;
                    userEventModel.material = "";
                    userEventModel.dateTime = DateTime.Now;
                    userEventModel.startTime = DateTime.Now;
                    userEventModel.endTime = DateTime.Now;
                    userEventModel.eventType = "LaserJobMaintance";
                    userEventModel.pageName = "LaserJobMaintance";
                    userEventModel.action = string.Format("[PQCQaViTracking]  totalQty:{0} --> {1}, acceptQty:{2} --> {3}", sourceTrackTotalQty, trackingModel.TotalQty, sourceTrackAcceptQty, trackingModel.acceptQty);
                    userEventModel.temp1 = "station-" + trackingModel.machineID;
                    userEventModel.temp2 = trackingModel.trackingID;
                    userEventModel.userID = userName;
                    userEventLogList.Add(userEventModel);





                    //update pqc data rollback
                    try
                    {
                        bool updateResult = viTrackingBLL.UpdateJobByLaserMaintenance(trackingModel, detailModelList);
                        if (updateResult == false)
                        {
                            DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] auto update PQC Job Fail!");
                        }
                    }
                    catch (Exception ee)
                    {
                        DBHelp.Reports.LogFile.Log("LaserJobMaintance", "[btn Submit Click] auto update PQC Job Fail, exception:" + ee.ToString());
                    }

                }
                #endregion







                //更新 paint delivery his paint rej的数量.
                try
                {
                    if (!deliveryBLL.UpdatePaintRej(this.lbJob.Text, dShortage, "Paint#1"))
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
                        userEventModel.action = string.Format("[PaintingDeliveryHis]  update paintRejQty to {0}", dShortage);
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

        
        



        private void setInventoryDetail(string sJobNo)
        {
            DataTable dtInventory = inventoryBLL.GetJobDetailForMaintenance(sJobNo);

            if (dtInventory != null && dtInventory.Rows.Count != 0)
            {
                string shortage = dtInventory.Rows[0]["shortage"].ToString();
                string setup = dtInventory.Rows[0]["setupQty"].ToString();
                string buyoff = dtInventory.Rows[0]["buyoffQty"].ToString();

                this.lbShortage.Text = shortage;
                this.lbSetUp.Text = setup;
                this.lbBuyoff.Text = buyoff;


                this.txtShortage.Attributes["placeholder"] = shortage;
                this.txtSetupQty.Attributes["placeholder"] = setup;
                this.txtBuyoffQty.Attributes["placeholder"] = buyoff;



                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set inventory info --  setup:{0}, buyoff:{1}, shortage:{2}", setup, buyoff, shortage));
            }
            else
            {
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set inventory info --  get datatable null from inventory!"));
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
                ((TextBox)item.Cells[3].FindControl("txtActualOK")).Attributes["placeholder"] = item.Cells[2].Text;
                ((TextBox)item.Cells[6].FindControl("txtActualNG")).Attributes["placeholder"] = item.Cells[5].Text;

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[Page_Load] set material detail list --  sn: {0}, material no: {1}, ok: {2}, ng{3}",item.Cells[0].Text, item.Cells[1].Text, item.Cells[2].Text, item.Cells[5].Text));
            }
        }

        
    }
}