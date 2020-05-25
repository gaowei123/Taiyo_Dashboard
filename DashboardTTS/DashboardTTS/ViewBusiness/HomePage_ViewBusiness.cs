using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;

namespace DashboardTTS.ViewBusiness
{
    public class HomePage_ViewBusiness
    {

        public ViewModel.HomePage_ViewModel GetMouldModle(DateTime dDay)
        {


            string department = "Moulding";
            string output = "";
            string inventory = "NA";

            //get output;
            Common.Class.BLL.MouldingViDefectTracking_BLL bll = new Common.Class.BLL.MouldingViDefectTracking_BLL();
            DataTable dt = bll.GetDayOuput(dDay);

          

            if (dt == null || dt.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetMouldModle]-- output datatable count: 0");
                output = "0<br/>(0)";
            }
            else
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format( "[GetMouldModle]-- totaloutput: {0},   totalshots: {1}", dt.Rows[0]["TotalOuput"].ToString(), dt.Rows[0]["TotalShots"].ToString()));

                int totalOutput = int.Parse(dt.Rows[0]["TotalOuput"].ToString());
                int totalShots = int.Parse(dt.Rows[0]["TotalShots"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", totalShots, totalOutput);
            }

            


            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();

            model.department = department;
            model.output = output;
            model.inventory = inventory;

            model.machineStatusList = getMouldingStatus();


            return model;
        }

        public ViewModel.HomePage_ViewModel GetPaintModel(DateTime dDay)
        {
            string department = "Painting";
            string output = "";
            string inventory = "NA";

            //output
            Common.Class.BLL.PaintingDeliveryHis_BLL bll = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            DataTable dt = bll.GetDayOutput(dDay);


            if (dt == null || dt.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetPaintModel]-- output datatable count: 0");
                output = "0<br/>(0)";
            }
            else
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetPaintModel]-- totaloutput: {0},   totalshots: {1}", dt.Rows[0]["TotalPCS"].ToString(), dt.Rows[0]["TotalSET"].ToString()));


                int totalPCS = int.Parse(dt.Rows[0]["TotalPCS"].ToString());
                int totalSET = int.Parse(dt.Rows[0]["TotalSET"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", totalSET, totalPCS);
            }


            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();
            model.department = department;
            model.output = output;
            model.inventory = inventory;

            model.machineStatusList = getPaintingStatus();

            return model;
        }

        public ViewModel.HomePage_ViewModel GetLaserModel(DateTime dDay)
        {
            string department = "Laser";
            string output = "";
            string inventory = "";

            //output
            Common.BLL.LMMSWatchLog_BLL logBLL = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dtLog = logBLL.GetDayOutput(dDay);

            

            if (dtLog == null || dtLog.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetLaserModel]-- output datatable count: 0");

                output = "0<br/>(0)";
            }
            else
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetLaserModel]-- totaloutput: {0},   totalshots: {1}", dtLog.Rows[0]["TotalOuput"].ToString(), dtLog.Rows[0]["TotalSet"].ToString()));

                int totalOutput_PCS = int.Parse(dtLog.Rows[0]["TotalOuput"].ToString());
                int totalOutput_SET = int.Parse(dtLog.Rows[0]["TotalSet"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", totalOutput_SET, totalOutput_PCS);
            } 


            //inventory
            Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
            DataTable dtInventory = inventoryBLL.GetInventoryQty();

            DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetLaserModel]-- inventory datatable count:{0}", dtInventory.Rows.Count));

            if (dtInventory == null || dtInventory.Rows.Count == 0)
            {
                inventory = "0<br/>(0)";
            }
            else
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetLaserModel]-- inventoryPCS: {0},   inventorySET: {1}", dtInventory.Rows[0]["Inventory_PCS"].ToString(), dtInventory.Rows[0]["Inventory_SET"].ToString()));


                int totalInventory_PCS = int.Parse(dtInventory.Rows[0]["Inventory_PCS"].ToString());
                int totalInventory_SET = int.Parse(dtInventory.Rows[0]["Inventory_SET"].ToString());
                inventory = string.Format("{0:N0}<br/>({1:N0})", totalInventory_SET, totalInventory_PCS);
            }


            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();
            model.department = department;
            model.output = output;
            model.inventory = inventory;
            model.machineStatusList = getLaserStatus();




            return model;
        }
        
        public ViewModel.HomePage_ViewModel GetPQCOnlineModel(DateTime dDay)
        {
            string department = "PQC</br>Online";
            string output = "";
            string inventory = "NA";


            //output
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetOnlineDayOutput(dDay);

            if (dt == null || dt.Rows.Count == 0)
            {

                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetPQCOnlineModel]-- output datatable count: 0");


                output = "0<br/>(0)";
            }
            else
            {

                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetPQCOnlineModel]-- output pcs: {0},   output set: {1}", dt.Rows[0]["Output_PCS"].ToString(), dt.Rows[0]["Output_SET"].ToString()));

                int OnLine_PCS = int.Parse(dt.Rows[0]["Output_PCS"].ToString());
                int OnLine_SET = int.Parse(dt.Rows[0]["Output_SET"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", OnLine_SET, OnLine_PCS);
            }


            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();
            model.department = department;
            model.output = output;
            model.inventory = inventory;
            model.machineStatusList = getPQCOnlineStatus(dDay);


            return model;
        }
        
        public ViewModel.HomePage_ViewModel GetPQCWIPModel(DateTime dDay)
        {
            string department = "PQC</br>WIP";
            string output = "";
            string inventory = "";


            //output
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dtOutput = bll.GetWIPDayOutput(dDay);


           


            if (dtOutput == null || dtOutput.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetPQCWIPModel]-- output datatable count: 0");

                output = "0<br/>(0)";
            }
            else
            {

                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetPQCWIPModel]-- output pcs: {0},   output set: {1}", dtOutput.Rows[0]["Output_PCS"].ToString(), dtOutput.Rows[0]["Output_SET"].ToString()));


                int WIP_PCS = int.Parse(dtOutput.Rows[0]["Output_PCS"].ToString());
                int WIP_SET = int.Parse(dtOutput.Rows[0]["Output_SET"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", WIP_SET, WIP_PCS);
            }


            //inventory
            //Common.Class.BLL.PQCInventory_BLL inventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
            //DataTable dtInventory =  inventoryBLL.GetInventoryReport(DateTime.Parse("2019-7-30"));


            //DBHelp.Reports.LogFile.Log("Home_Debug", "[GetPQCWIPModel]-- inventory datatable count:" + dtInventory.Rows.Count);


            //DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetPQCWIPModel]-- inventory :{0}", dtInventory.Rows[dtInventory.Rows.Count - 1]["Before"].ToString()));


            //inventory = dtInventory.Rows[dtInventory.Rows.Count - 1]["Before"].ToString();
            //inventory = inventory.Replace("(", "</br>(");
            

            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();
            model.department = department;
            model.output = output;
            model.inventory = "NA";
            model.machineStatusList = getPQCWIPStatus(dDay);


            return model;
        }
        
        public ViewModel.HomePage_ViewModel GetPQCPackingModel(DateTime dDay)
        {
            string department = "PQC</br>Packing";
            string output = "";
            string inventory = "NA";


            //output
            Common.Class.BLL.PQCPackTracking bll = new Common.Class.BLL.PQCPackTracking();
            DataTable dtOutput = bll.GetDayOutput(dDay);

            

            if (dtOutput == null || dtOutput.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("Home_Debug", "[GetPQCPackingModel]-- output datatable count: 0");

                output = "0<br/>(0)";
            }
            else
            {

                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[GetPQCPackingModel]-- output pcs: {0},   output set: {1}", dtOutput.Rows[0]["Output_PCS"].ToString(), dtOutput.Rows[0]["Output_SET"].ToString()));


                int Packing_PCS = int.Parse(dtOutput.Rows[0]["Output_PCS"].ToString());
                int Packing_SET = int.Parse(dtOutput.Rows[0]["Output_SET"].ToString());
                output = string.Format("{0:N0}<br/>({1:N0})", Packing_SET, Packing_PCS);
            }

            //inventory
          



            ViewModel.HomePage_ViewModel model = new ViewModel.HomePage_ViewModel();
            model.department = department;
            model.output = output;
            model.inventory = inventory;
            model.machineStatusList = getPQCPackingStatus(dDay);


            return model;
        }









        private List<ViewModel.HomePage_ViewModel.machineStatus> getMouldingStatus()
        {
            try
            {
                //machine status list
                List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();
              

                //get current machien status
                Common.Class.BLL.MouldingMachineStatus_BLL MachineStatus = new Common.Class.BLL.MouldingMachineStatus_BLL();
                DataTable dtMouldingStatus = MachineStatus.GetCurrentStatus();

                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[getMouldingStatus]-- dtMouldingStatus count: {0}",dtMouldingStatus.Rows.Count));

                foreach (DataRow row in dtMouldingStatus.Rows)
                {

                    DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[getMouldingStatus]-- machineID: {0},   machineStatus: {1}", row["MachineID"].ToString(), row["MachineStatus"].ToString()));

                    string MachineID = row["MachineID"].ToString().Replace("Machine", "");
                    string Status = row["MachineStatus"].ToString();

                    if (int.Parse(MachineID) > 8)
                    {
                        continue;
                    }


                    //add machine status model to list
                    ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                    machineStatusModel.machineID = "Machine "+ MachineID;
                    machineStatusModel.status = Status;
                    machineStatusModel.statusColor = getColor(Status);

                    machineStatusList.Add(machineStatusModel);
                }

                ViewModel.HomePage_ViewModel.machineStatus mc9 = new ViewModel.HomePage_ViewModel.machineStatus();
                mc9.machineID = "Machine 9";
                mc9.status = "Under develop";
                mc9.statusColor = getColor("");


                machineStatusList.Add(mc9);


                return machineStatusList;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("HomePage_ViewBusiness", "setMouldingStatus Exception : " + ee.ToString());
                return null;
            }
        }
        private List<ViewModel.HomePage_ViewModel.machineStatus> getPaintingStatus()
        {
            try
            {
                //machine status list
                List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();


               
                for (int i = 1; i < 10; i++)
                {
                    string machineID = "Machine " + i.ToString();
                    string status = "Under develop";


                    ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                    machineStatusModel.machineID = machineID;
                    machineStatusModel.status = status;
                    machineStatusModel.statusColor = getColor("");

                    machineStatusList.Add(machineStatusModel);
                }


                return machineStatusList;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("HomePage_ViewBusiness", "getPaintingStatus Exception : " + ee.ToString());
                return null;
            }
        }
        private List<ViewModel.HomePage_ViewModel.machineStatus> getLaserStatus()
        {
            try
            {
                //machine status list
                List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();



                Common.BLL.LMMSEventLog_BLL eventBLL = new Common.BLL.LMMSEventLog_BLL();
                DataTable dt = eventBLL.GetTodayList();


                DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[getLaserStatus]-- dtlaserEvent count: {0}", dt.Rows.Count));

                for (int i = 1; i < 9; i++)
                {
                    string machineID = "Machine " + i.ToString();
                    string status = "";

                    if (dt != null || dt.Rows.Count != 0)
                    {
                        DataRow[] drArr = dt.Select(string.Format("machineID='{0:G}'", i.ToString()), " stopTime  desc , currentOperation desc  ");

                        if (drArr.Length > 0)
                            status = drArr[0]["eventTrigger"].ToString();
                        else
                            status = "ShutDown";
                    }
                    else
                    {
                        status = "ShutDown";
                    }

                    status = convertStatus(status);



                    DBHelp.Reports.LogFile.Log("Home_Debug", string.Format("[getLaserStatus]-- machineID: {0},  status: {1}", machineID,status));



                    ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                    machineStatusModel.machineID = machineID;
                    machineStatusModel.status = status;
                    machineStatusModel.statusColor = getColor(status);

                    machineStatusList.Add(machineStatusModel);
                }




                return machineStatusList;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("HomePage_ViewBusiness", "getLaserStatus Exception : " + ee.ToString());
                return null;
            }
        }



        private List<ViewModel.HomePage_ViewModel.machineStatus> getPQCOnlineStatus(DateTime dDay)
        {
            List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();

            int[] onLineStationList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int stationNo = 1;

            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetRealTimeData("Online");
            


            foreach (int i in onLineStationList)
            {
                DataRow[] drArr = null;
                if (dt != null && dt.Rows.Count != 0)
                {
                    drArr = dt.Select("machineID = '" + i.ToString() + "'", " dateTime desc ");
                }
                    


                ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                machineStatusModel.machineID = "Online " + stationNo.ToString();
                stationNo++;

                if (drArr == null || drArr.Length == 0)
                {
                    machineStatusModel.status = "Shutdown";
                    machineStatusModel.statusColor = getColor("Shutdown");
                }
                else
                {
                   
                    string status = drArr[0]["nextViFlag"].ToString().ToUpper() == "FALSE" ? "Checking" : "NO SCHEDULE";
                    machineStatusModel.status = status;
                    machineStatusModel.statusColor = getColor(status);
                }


                
                machineStatusList.Add(machineStatusModel);
            }
            
        
            return machineStatusList;
        }
        private List<ViewModel.HomePage_ViewModel.machineStatus> getPQCWIPStatus(DateTime dDay)
        {
            List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();

            int[] wipStationList = new int[] { 16,17,14,15,11,13 };//对应wip1~6的顺序
            int stationNo = 1;

            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetRealTimeData("WIP");

            foreach (int i in wipStationList)
            {
                DataRow[] drArr = null;
                if (dt != null && dt.Rows.Count != 0)
                {
                    drArr = dt.Select("machineID = '" + i.ToString() + "'", " dateTime desc ");
                }



                ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                machineStatusModel.machineID = "WIP " + stationNo.ToString();
                stationNo++;

                if (drArr == null || drArr.Length == 0)
                {
                    machineStatusModel.status = "Shutdown";
                    machineStatusModel.statusColor = getColor("Shutdown");
                }
                else
                {
                    string status = drArr[0]["nextViFlag"].ToString().ToUpper() == "FALSE" ? "Checking" : "NO SCHEDULE";
                    machineStatusModel.status = status;
                    machineStatusModel.statusColor = getColor(status);
                }

                machineStatusList.Add(machineStatusModel);
            }


            return machineStatusList;
        }
        private List<ViewModel.HomePage_ViewModel.machineStatus> getPQCPackingStatus(DateTime dDay)
        {
            List<ViewModel.HomePage_ViewModel.machineStatus> machineStatusList = new List<ViewModel.HomePage_ViewModel.machineStatus>();

            int[] packStationList = new int[] { 25, 23, 22, 21, 24, 12 };
            int stationNo = 1;

            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetRealTimeData("Packing");

            foreach (int i in packStationList)
            {
                DataRow[] drArr = null;
                if (dt != null && dt.Rows.Count != 0)
                {
                    drArr = dt.Select("machineID = '" + i.ToString() + "'", " dateTime desc ");
                }



                ViewModel.HomePage_ViewModel.machineStatus machineStatusModel = new ViewModel.HomePage_ViewModel.machineStatus();
                machineStatusModel.machineID = "Packing " + stationNo.ToString();
                stationNo++;

                if (drArr == null || drArr.Length == 0)
                {
                    machineStatusModel.status = "Shutdown";
                    machineStatusModel.statusColor = getColor("Shutdown");
                }
                else
                {
                    string status = drArr[0]["nextViFlag"].ToString().ToUpper() == "FALSE" ? "Packing" : "NO SCHEDULE";
                    machineStatusModel.status = status;
                    machineStatusModel.statusColor = getColor(status);
                }

                machineStatusList.Add(machineStatusModel);
            }
            

            return machineStatusList;
        }








        private string getColor(string status)
        {

            System.Drawing.Color color = new System.Drawing.Color();

            switch (status)
            {
                case ("Adjustment"):
                    color = StaticRes.MouldingStatusColor.Adjustment;
                    break;
                case ("NoWIP"):
                    color = StaticRes.MouldingStatusColor.No_Schedule;
                    break;
                case ("No_Schedule"):
                    color = StaticRes.MouldingStatusColor.No_Schedule;
                    break;
                case ("MachineBreak"):
                    color = StaticRes.MouldingStatusColor.MachineBreak;
                    break;
                case ("Running"):
                    color = StaticRes.MouldingStatusColor.Running;
                    break;
                case ("ShutDown"):
                    color = StaticRes.MouldingStatusColor.ShutDown;
                    break;
                case ("Mould Testing"):
                    color = StaticRes.MouldingStatusColor.Mould_Testing;
                    break;
                case ("Material Testing"):
                    color = StaticRes.MouldingStatusColor.Material_Testing;
                    break;
                case ("Change Model"):
                    color = StaticRes.MouldingStatusColor.Change_Model;
                    break;
                case ("No Operator"):
                    color = StaticRes.MouldingStatusColor.No_Operator;
                    break;
                case ("Break Time"):
                    color = StaticRes.MouldingStatusColor.Break_Time;
                    break;
                case ("Mould Damaged"):
                    color = StaticRes.MouldingStatusColor.DamageMould;
                    break;


                //laser
                case ("POWER ON"):
                    color = StaticRes.McStatusColor.Operating;
                    break;
                case ("POWER OFF"):
                    color = StaticRes.McStatusColor.ShutDown;
                    break;
                case ("MAINTAINENCE"):
                    color = StaticRes.McStatusColor.Maintainence;
                    break;
                case ("NO SCHEDULE"):
                    color = StaticRes.McStatusColor.NoWIP;
                    break;
                case ("TESTING"):
                    color = StaticRes.McStatusColor.Testing;
                    break;
                case ("BREAK DOWN"):
                    color = StaticRes.McStatusColor.BreakDown;
                    break;
                case ("BUYOFF"):
                    color = StaticRes.McStatusColor.Buyoff;
                    break;
                case ("SETUP"):
                    color = StaticRes.McStatusColor.Setup;
                    break;
                case ("ADJUSTMENT"):
                    color = StaticRes.McStatusColor.Buyoff;
                    break;
                case ("BREAKDOWN"):
                    color = StaticRes.McStatusColor.BreakDown;
                    break;

                case ("Checking"):
                    color = StaticRes.McStatusColor.Operating;
                    break;
                case ("Packing"):
                    color = StaticRes.McStatusColor.Operating;
                    break;

                case (""):
                    color = StaticRes.McStatusColor.ShutDown;
                    break;

                default:
                    color = StaticRes.McStatusColor.ShutDown;
                    break;

            }

            return ColorTranslator.ToHtml(color);
        }


        private string convertStatus(string status)
        {
            if (status == "POWER ON")
            {
                return "Running";
            }
            else if (status =="POWER OFF")
            {
                return "Shutdown";
            }else
            {
                return status;
            }

        }




    }
}