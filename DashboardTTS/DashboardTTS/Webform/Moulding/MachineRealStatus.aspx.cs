using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Taiyo.Enum.Production;
using Taiyo.Tool;

namespace DashboardTTS.Webform.Molding
{
    public partial class MachineRealStatus : System.Web.UI.Page
    {
        
        private const string _urlImg = "~/Resources/Images/MouldingMachine.png";
        private readonly Common.Class.BLL.MouldingMachineStatus_BLL _statusBLL = new Common.Class.BLL.MouldingMachineStatus_BLL();
        private readonly Common.Class.BLL.MouldingViHistory_BLL _trackingBLL = new Common.Class.BLL.MouldingViHistory_BLL();



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, MouldingStatus> dicStatus = _statusBLL.GetCurrentStatus();
                Dictionary<int, double> dicUsedRate = _statusBLL.GetCurrentUsedRate();
                DataTable dtProd = _trackingBLL.GetList(DateTime.Now.AddHours(-8).Date, DateTime.Now.AddHours(-8).Date.AddDays(1), "","","");

                
                for (int i = 1; i < 10; i++)
                {
                    MouldingStatus status = dicStatus[i];
                    if (status == MouldingStatus.Shutdown)
                    {
                        GetControl(i).SetShutdown("Machine " + i.ToString(), _urlImg);
                    }
                    else
                    {
                        UserControl.UcMoulding.UIModel model = new UserControl.UcMoulding.UIModel();
                        model.MachineID = "Machine " + i.ToString();
                        model.Status = status;
                        model.ImgURL = _urlImg;
                        model.UsedRate = dicUsedRate[i].ToString("0.00") + "%";

                        DataRow[] arrDrTemp = dtProd.Select(" MachineID = '" + i.ToString() + "'");
                        if (arrDrTemp != null && arrDrTemp.Count() != 0)
                        {
                            model.PartNo = arrDrTemp[0]["partNumber"].ToString();
                            model.Model = arrDrTemp[0]["model"].ToString();
                            model.JigNo = arrDrTemp[0]["jigNo"].ToString();
                            model.TotalQty = decimal.Parse(arrDrTemp[0]["acountReading"].ToString());
                            model.OKQty = decimal.Parse(arrDrTemp[0]["acceptQty"].ToString());
                            model.NGQty = decimal.Parse(arrDrTemp[0]["rejectQty"].ToString());
                            model.RejRate = model.TotalQty == 0 ? "0.00%" : Math.Round(model.NGQty / model.TotalQty * 100, 2).ToString("0.00") + "%";
                        }
                        else
                        {
                            model.PartNo = "";
                            model.Model = "";
                            model.JigNo = "";
                            model.TotalQty = 0;
                            model.OKQty = 0;
                            model.NGQty = 0;
                            model.RejRate = "0.00%";
                        }

                        GetControl(i).SetUI(model);
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Exception:" + ee.ToString());
            }
        }


        private UserControl.UcMoulding GetControl(int sMachineID)
        {
            UserControl.UcMoulding control = new UserControl.UcMoulding();
            switch (sMachineID)
            {
                case 1:
                    control = uc1;
                    break;
                case 2:
                    control = uc2;
                    break;
                case 3:
                    control = uc3;
                    break;
                case 4:
                    control = uc4;
                    break;
                case 5:
                    control = uc5;
                    break;
                case 6:
                    control = uc6;
                    break;
                case 7:
                    control = uc7;
                    break;
                case 8:
                    control = uc8;
                    break;
                case 9:
                    control = uc9;
                    break;
                default:
                    throw new NullReferenceException("no such MachineID " + sMachineID);
            }
            return control;
        }


    }
}