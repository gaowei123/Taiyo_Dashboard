using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;
using Taiyo.Enum.Production;
using Taiyo.Tool;
using Taiyo.Tool.Extension;

namespace DashboardTTS.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        

        public ActionResult GetMouldStatus()
        {
            Common.Class.BLL.MouldingMachineStatus_BLL MachineStatusBLL = new Common.Class.BLL.MouldingMachineStatus_BLL();
            Dictionary<int,string> dicStatus = MachineStatusBLL.GetCurrentStatus();
            
            return Content(JsonConvert.SerializeObject(dicStatus));
        }

        public ActionResult GetLaserStatus()
        {
            Common.BLL.LMMSEventLog_BLL bll = new Common.BLL.LMMSEventLog_BLL();
            Dictionary<int, string> dicCurStatus = bll.GetCurrentStatus();
            
            
            string result = JsonConvert.SerializeObject(dicCurStatus);

       

            return Content(result);
        }

        public JsonResult GetPQCStatus()
        {
            Common.Class.BLL.PQCQaViTracking_BLL pqcbll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dtTracking = pqcbll.GetRealTime();


            var onlineList = new List<UserControl.WebUserControlPQCStatus.UIModel>();
            var wipList = new List<UserControl.WebUserControlPQCStatus.UIModel>();
            var packingList = new List<UserControl.WebUserControlPQCStatus.UIModel>();
            

            #region  online 1-8
            for (int i = 1; i < 9; i++)
            {
                string status = "";
                if (dtTracking == null )
                    status = PQCStatus.Shutdown.ToString();
                else
                {
                    DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + i + "'", " datetime desc");
                    if (drArrTemp == null || drArrTemp.Length == 0)
                        status = PQCStatus.Shutdown.ToString();
                    else
                        status = drArrTemp[0]["stopTime"].ToString() == "" ? PQCStatus.Checking.GetDescription() : PQCStatus.NoSchedule.GetDescription();
                }

                UserControl.WebUserControlPQCStatus.UIModel model = new UserControl.WebUserControlPQCStatus.UIModel();
                model.Station = $"Online{i}(Sta{i})";
                model.Status = status;
                onlineList.Add(model);
            }
            #endregion

            #region wip  16, 17, 14, 15, 11, 13
            if (dtTracking == null)
            {
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP1(Sta16)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP2(Sta17)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP3(Sta14)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP4(Sta15)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP5(Sta11)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP6(Sta13)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
            }
            else
            {
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP1(Sta16)",
                    Status = GetPQCStatusFromDt(dtTracking,16).GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP2(Sta17)",
                    Status = GetPQCStatusFromDt(dtTracking, 17).GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP3(Sta14)",
                    Status = GetPQCStatusFromDt(dtTracking, 114).GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP4(Sta15)",
                    Status = GetPQCStatusFromDt(dtTracking, 15).GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP5(Sta11)",
                    Status = GetPQCStatusFromDt(dtTracking, 11).GetDescription()
                });
                wipList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"WIP6(Sta13)",
                    Status = GetPQCStatusFromDt(dtTracking, 13).GetDescription()
                });
            }
            #endregion

            #region packing 25, 23, 22, 21, 24, 12
            if (dtTracking == null)
            {
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing1(Sta25)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing2(Sta23)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing3(Sta22)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing4(Sta21)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing5(Sta24)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing6(Sta12)",
                    Status = PQCStatus.Shutdown.GetDescription()
                });
            }
            else
            {
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing1(Sta25)",
                    Status = GetPQCStatusFromDt(dtTracking, 25).GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing2(Sta23)",
                    Status = GetPQCStatusFromDt(dtTracking, 23).GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing3(Sta22)",
                    Status = GetPQCStatusFromDt(dtTracking, 22).GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing4(Sta21)",
                    Status = GetPQCStatusFromDt(dtTracking, 21).GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing5(Sta24)",
                    Status = GetPQCStatusFromDt(dtTracking, 24).GetDescription()
                });
                packingList.Add(new UserControl.WebUserControlPQCStatus.UIModel()
                {
                    Station = $"Packing6(Sta12)",
                    Status = GetPQCStatusFromDt(dtTracking, 12).GetDescription()
                });
            }
            #endregion

            
            //按照特定的顺序排列.
            var resultList = new List<UserControl.WebUserControlPQCStatus.UIModel>()
            {
                //对应网页排版 pqc状态第一行
                onlineList[0],
                onlineList[1],
                wipList[0],
                wipList[1],
                packingList[0],
                packingList[1],

                 //对应网页排版 pqc状态第二行
                onlineList[2],
                onlineList[3],
                wipList[2],
                wipList[3],
                packingList[2],
                packingList[3],

                //对应网页排版 pqc状态第三行
                onlineList[4],
                onlineList[5],
                wipList[4],
                wipList[5],
                packingList[4],
                packingList[5],

                //对应网页排版 pqc状态第四行, 只有online2个
                onlineList[6],
                onlineList[7]
            };
          



            return Json(resultList);
        }


        /// <summary>
        /// 根据当天tracking表记录
        /// 有记录, stoptime为空, 说明正在check/pack, 根据machineid分
        /// 有记录, stoptime不空, 说明做完了 为no schecdule
        /// 没记录, 说明还没做, 为shutdown
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        private PQCStatus GetPQCStatusFromDt(DataTable dt, int machineID)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return PQCStatus.Shutdown;
            }
            else
            {
                DataRow[] drArrTemp = dt.Select(" machineID = '" + machineID + "'", " datetime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    return PQCStatus.Shutdown;
                }
                else
                {
                    if (drArrTemp[0]["stopTime"].ToString() == "")
                    {
                        int[] checkingArr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 11, 13, 14, 15, 16, 17 };
                        if (checkingArr.Contains(machineID))
                        {
                            return PQCStatus.Checking;
                        }
                        else
                        {
                            return PQCStatus.Packing;
                        }
                    }
                    else
                    {
                        return PQCStatus.NoSchedule;
                    }
                }

            }
        }
        

    }
}