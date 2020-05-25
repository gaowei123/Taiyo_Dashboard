using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DashboardTTS.ViewBusiness
{
    public class MOULDMaterial
    {
        private readonly Common.Class.BLL.Material_Inventory_History_BLL materialHisBLL = new Common.Class.BLL.Material_Inventory_History_BLL();
        private readonly Common.Class.BLL.MouldingViHistory_BLL trackingBLL = new Common.Class.BLL.MouldingViHistory_BLL();


        public MOULDMaterial()
        {

        }


       



        public List<ViewModel.MouldMaterialTraceability> GetMaterialTraceability(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            DataTable dtMaterial = materialHisBLL.GetList(dDateFrom, dDateTo, sMachineID);
            DataTable dtTracking = trackingBLL.GetList(dDateFrom, dDateTo, "", "", sMachineID);

            if (dtMaterial == null||dtMaterial.Rows.Count == 0 || dtTracking == null || dtTracking.Rows.Count == 0)
                return null;



            List<ViewModel.MouldMaterialTraceability> modelList = new List<ViewModel.MouldMaterialTraceability>();

            DateTime dTemp = dDateFrom;
            while(dTemp < dDateTo)
            {
                for (int i = 1; i < 10; i++)
                {
                    #region 添加day model
                    DataRow[] drTrackingDay = dtTracking.Select(" day = '" + dTemp.ToString("yyyy-MM-dd") + "' and shift = 'Day' and machineID = '" + i.ToString() + "'");
                    DataRow[] drMaterialDay = dtMaterial.Select("REF_FIELD02 = '"+dTemp.ToString("yyyy-MM-dd HH:mm:ss") + "' and REF_FIELD01 = 'Day' and MachineID = '" + i.ToString()+"' ");


                    ViewModel.MouldMaterialTraceability dayModel = new ViewModel.MouldMaterialTraceability();
                    dayModel.day = dTemp;
                    dayModel.shift = "Day";
                    dayModel.machineID = i.ToString();
                    
                    if (drTrackingDay.Length> 0)
                    {
                        dayModel.partNumberALL = drTrackingDay[0]["PartNumberAll"].ToString();
                        dayModel.clientUserID = drTrackingDay[0]["userID"].ToString();

                        foreach (DataRow dr in drTrackingDay)
                        {
                            string materialNo_1 = dr["matPart01"].ToString();
                            string materialLotNo_1 = dr["matLot01"].ToString();
                            string weight_1 = dr["partsWeight"].ToString();

                            string materialNo_2 = dr["matPart02"].ToString();
                            string materialLotNo_2 = dr["matLot02"].ToString();
                            string weight_2 = dr["parts2Weight"].ToString();

                            if (materialNo_1 != "" && materialLotNo_1!= "" && weight_1!= "")
                                dayModel.clientSubmitInfo += string.Format("[{0} - {1} - {2}]", materialNo_1, materialLotNo_1, weight_1) + "  ,  ";

                            if (materialNo_2 != "" && materialLotNo_2 != "" && weight_2 != "")
                                dayModel.clientSubmitInfo += string.Format("[{0} - {1} - {2}]", materialNo_2, materialLotNo_2, weight_2) + "  ,  ";

                            dayModel.clientSubmitInfo = dayModel.clientSubmitInfo.Substring(0, dayModel.clientSubmitInfo.Length - 3);
                        }
                    }
                    else
                    {
                        continue;
                    }

                    if (drMaterialDay.Length > 0)
                    {
                        dayModel.unloadUserID = drMaterialDay[0]["User_Name"].ToString();

                        foreach (DataRow dr in drMaterialDay)
                        {
                            string materialNo = dr["Material_No"].ToString();
                            string materialLotNo = dr["Material_LotNo"].ToString();
                            string weight = dr["Transaction_Weight"].ToString();
                            
                            dayModel.dashboardUnloadInfo += string.Format("[{0} - {1} - {2}]", materialNo, materialLotNo, weight) + "  ,  ";
                        }

                        dayModel.dashboardUnloadInfo = dayModel.dashboardUnloadInfo.Substring(0, dayModel.dashboardUnloadInfo.Length - 3);
                    }


                    modelList.Add(dayModel);
                    #endregion
                    
                    #region 添加night model
                    DataRow[] drTrackingNight = dtTracking.Select(" day = '" + dTemp.ToString("yyyy-MM-dd") + "' and shift = 'Night' and machineID = '" + i.ToString() + "'");
                    DataRow[] drMaterialNight = dtMaterial.Select("REF_FIELD02 = '" + dTemp.ToString("yyyy-MM-dd HH:mm:ss") + "' and REF_FIELD01 = 'Night' and MachineID = '" + i.ToString() + "' ");


                    ViewModel.MouldMaterialTraceability nightModel = new ViewModel.MouldMaterialTraceability();
                    nightModel.day = dTemp;
                    nightModel.shift = "Night";
                    nightModel.machineID = i.ToString();

                    if (drTrackingDay.Length > 0)
                    {
                        nightModel.partNumberALL = drTrackingDay[0]["PartNumberAll"].ToString();
                        nightModel.clientUserID = drTrackingDay[0]["userID"].ToString();

                        foreach (DataRow dr in drTrackingDay)
                        {
                            string materialNo_1 = dr["matPart01"].ToString();
                            string materialLotNo_1 = dr["matLot01"].ToString();
                            string weight_1 = dr["partsWeight"].ToString();

                            string materialNo_2 = dr["matPart02"].ToString();
                            string materialLotNo_2 = dr["matLot02"].ToString();
                            string weight_2 = dr["parts2Weight"].ToString();

                            if (materialNo_1 != "" && materialLotNo_1 != "" && weight_1 != "")
                                nightModel.clientSubmitInfo += string.Format("[{0} - {1} - {2}]", materialNo_1, materialLotNo_1, weight_1) + "  ,  ";

                            if (materialNo_2 != "" && materialLotNo_2 != "" && weight_2 != "")
                                nightModel.clientSubmitInfo += string.Format("[{0} - {1} - {2}]", materialNo_2, materialLotNo_2, weight_2) + "  ,  ";

                            nightModel.clientSubmitInfo = nightModel.clientSubmitInfo.Substring(0, nightModel.clientSubmitInfo.Length - 3);
                        }
                    }
                    else
                    {
                        continue;
                    }

                    if (drMaterialDay.Length > 0)
                    {
                        nightModel.unloadUserID = drMaterialDay[0]["User_Name"].ToString();

                        foreach (DataRow dr in drMaterialDay)
                        {
                            string materialNo = dr["Material_No"].ToString();
                            string materialLotNo = dr["Material_LotNo"].ToString();
                            string weight = dr["Transaction_Weight"].ToString();

                            nightModel.dashboardUnloadInfo += string.Format("[{0} - {1} - {2}]", materialNo, materialLotNo, weight) + "  ,  ";
                        }

                        nightModel.dashboardUnloadInfo = nightModel.dashboardUnloadInfo.Substring(0, nightModel.dashboardUnloadInfo.Length - 3);
                    }


                    modelList.Add(nightModel);
                    #endregion
                }

                dTemp = dTemp.AddDays(1);
            }




            return modelList;
        }






    }
}