using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace DashboardTTS.Webform
{
    public partial class MachineStatus : System.Web.UI.Page
    {
        private Common.BLL.LMMSWatchLog_BLL _watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
        private Common.BLL.LMMSEventLog_BLL _statusBLL = new Common.BLL.LMMSEventLog_BLL();

        private const string strRobortArmImgURL = "~/Resources/Images/LaserMachine_RobortArm.png";
        private const string strTurnTableImgURL = "~/Resources/Images/LaserMachine_TurnTable.png";

        private UserControl.WebUserControlMachineStatus GetControl(int sMachineID)
        {
            UserControl.WebUserControlMachineStatus control = new UserControl.WebUserControlMachineStatus();
            switch (sMachineID)
            {
                case 1:
                    control = ucMachineStatus1;
                    break;
                case 2:
                    control = ucMachineStatus2;
                    break;
                case 3:
                    control = ucMachineStatus3;
                    break;
                case 4:
                    control = ucMachineStatus4;
                    break;
                case 5:
                    control = ucMachineStatus5;
                    break;
                case 6:
                    control = ucMachineStatus6;
                    break;
                case 7:
                    control = ucMachineStatus7;
                    break;
                case 8:
                    control = ucMachineStatus8;
                    break;
                default:
                    throw new NullReferenceException("no such MachineID " + sMachineID);
                    break;
            }

            return control;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Common.Model.LMMSWatchLog_Model> watchDogList = _watchLogBLL.GetCurJobList();
                if (watchDogList == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "No data found from watchdog");
                    return;
                }
                Dictionary<int, double> dicCurDayUsedRate = _statusBLL.GetCurrentDayUsedRate();
                Dictionary<int, string> dicCurStatus = _statusBLL.GetCurrentStatus();


                //遍历8台机器, 赋值
                for (int i = 1; i < 9; i++)
                {
                    UserControl.WebUserControlMachineStatus.UIModel uiModel = new UserControl.WebUserControlMachineStatus.UIModel();

                    //production info
                    var productModel = (from a in watchDogList where a.machineID == i.ToString() select a).First();
                    uiModel.MachineID = "Machine " + i.ToString();
                    uiModel.PartNo = productModel.partNumber;
                    uiModel.LotNo = productModel.lotNo;
                    uiModel.JobNo = productModel.jobNumber;
                    uiModel.LotQty = productModel.totalQuantity.Value;
                    uiModel.OKQty = productModel.totalPass.Value;
                    uiModel.NGQty = productModel.totalFail.Value;
                    uiModel.RejRate = uiModel.LotQty == 0 ? "0.00%" : Math.Round(uiModel.NGQty / uiModel.LotQty * 100, 2).ToString() + "%";

                    //img url      1~5: strRobortArmImgURL,    6,7,8: strTurnTableImgURL
                    uiModel.ImgURL = (new int[] { 1, 2, 3, 4, 5 }).Contains(i) ? strRobortArmImgURL : strTurnTableImgURL;

                    //current status
                    uiModel.Status = dicCurStatus==null? StaticRes.Global.LaserStatus.Shutdown : dicCurStatus[i];

                    //used rate
                    double usedRate = dicCurDayUsedRate == null ? 0: dicCurDayUsedRate[i];
                    uiModel.UsedRate = usedRate.ToString("0.00") + "%";


                    GetControl(i).SetUI(uiModel);
                }
            }
            catch (Exception ee)
            {               
                DBHelp.Reports.LogFile.Log("LaserMachineRealTime_Exception", "Page_Load exception:" + ee.ToString());
                Common.CommFunctions.ShowMessage(this.Page, "Refrush page error, msg:" + ee.ToString());
                return;
            }
        }
    }
}