using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Taiyo.Enum.Organization;

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
            try
            {
                DataTable dtMaterial = materialHisBLL .GetList(dDateFrom, dDateTo, sMachineID);
                DataTable dtTracking = trackingBLL.GetList(dDateFrom, dDateTo, "", "", sMachineID);


                Common.Class.BLL.User_DB_BLL userBLL = new Common.Class.BLL.User_DB_BLL();
                var userList = userBLL.GetModelList(Department.Moulding.ToString(),"","","");
                var mouldingUserMHList = userList.Where(user => user.USER_GROUP == "MH").ToList();


                if (dtMaterial == null || dtMaterial.Rows.Count == 0 || dtTracking == null || dtTracking.Rows.Count == 0)
                    return null;



                List<ViewModel.MouldMaterialTraceability> modelList = new List<ViewModel.MouldMaterialTraceability>();


                //遍历查询的每一天.
                DateTime dTemp = dDateFrom;
                while (dTemp < dDateTo)
                {

                    //遍历每台机器
                    for (int i = 1; i < 10; i++)
                    {
                        #region 添加day model

                        //查询day 信息.
                        DataRow[] drTrackingDay = dtTracking.Select(" day = '" + dTemp.ToString("yyyy-MM-dd") + "' and shift = 'Day' and machineID = '" + i.ToString() + "'");
                        DataRow[] drMaterialDay = dtMaterial.Select("REF_FIELD02 = '" + dTemp.ToString("yyyy-MM-dd HH:mm:ss") + "' and REF_FIELD01 = 'Day' and MachineID = '" + i.ToString() + "' ");




                        ViewModel.MouldMaterialTraceability dayModel = new ViewModel.MouldMaterialTraceability();
                        dayModel.day = dTemp;
                        dayModel.shift = "Day";
                        dayModel.machineID = i.ToString();
                        dayModel.clientSubmitInfo = "";
                        dayModel.dashboardUnloadInfo = "";


                        //获取client信息, 按照[materialNo - LotNo - weigh]的格式组合
                        if (drTrackingDay.Length > 0)
                        {
                            dayModel.partNumberALL = drTrackingDay[0]["PartNumberAll"].ToString();

                            //2021-02-01新增, MH ID列不能显示不是MH组别下的ID
                            {
                                string tempUserID = drTrackingDay[0]["userID"].ToString();                        
                                if (mouldingUserMHList.Where(user=>user.USER_ID.ToUpper() == tempUserID.ToUpper()).Count() > 0)
                                    dayModel.clientUserID = tempUserID;
                                else
                                    dayModel.clientUserID = string.Empty;
                            }


                            foreach (DataRow dr in drTrackingDay)
                            {
                                string materialNo_1 = dr["matPart01"].ToString();
                                string materialLotNo_1 = dr["matLot01"].ToString();
                                string weight_1 = dr["partsWeight"].ToString();

                                string materialNo_2 = dr["matPart02"].ToString();
                                string materialLotNo_2 = dr["matLot02"].ToString();
                                string weight_2 = dr["parts2Weight"].ToString();


                                string strMaterial = "";

                                if (materialNo_1 != "" && materialLotNo_1 != "" && weight_1 != "")
                                {
                                    strMaterial = string.Format("[{0} - {1} - {2}]", materialNo_1, materialLotNo_1, weight_1);

                                    //去重, 避免重复添加相同的material信息
                                    if (!dayModel.clientSubmitInfo.Contains(strMaterial))
                                    {
                                        dayModel.clientSubmitInfo += (strMaterial + "  ,  ");
                                    }
                                }


                                if (materialNo_2 != "" && materialLotNo_2 != "" && weight_2 != "")
                                {
                                    strMaterial = string.Format("[{0} - {1} - {2}]", materialNo_2, materialLotNo_2, weight_2);

                                    //去重, 避免重复添加相同的material信息
                                    if (!dayModel.clientSubmitInfo.Contains(strMaterial))
                                    {
                                        dayModel.clientSubmitInfo += strMaterial + "  ,  ";
                                    }
                                }
                            }

                            if (dayModel.clientSubmitInfo != "")
                            {
                                dayModel.clientSubmitInfo = dayModel.clientSubmitInfo.Substring(0, dayModel.clientSubmitInfo.Length - 3);
                            }
                        }


                        //获取dashboard-unload信息, 按照[materialNo - LotNo - weigh]的格式组合
                        if (drMaterialDay.Length > 0)
                        {

                            //2021-02-01新增, MH ID列不能显示不是MH组别下的ID
                            {
                                string tempUserID = drMaterialDay[0]["User_Name"].ToString();
                                if (mouldingUserMHList.Where(user => user.USER_ID.ToUpper() == tempUserID.ToUpper()).Count() > 0)
                                    dayModel.unloadUserID = tempUserID;
                                else
                                    dayModel.unloadUserID = string.Empty;
                            }

                            foreach (DataRow dr in drMaterialDay)
                            {
                                string materialNo = dr["Material_No"].ToString();
                                string materialLotNo = dr["Material_LotNo"].ToString();
                                string weight = dr["Transaction_Weight"].ToString();

                                dayModel.dashboardUnloadInfo += string.Format("[{0} - {1} - {2}]", materialNo, materialLotNo, weight) + "  ,  ";
                            }

                            if (dayModel.dashboardUnloadInfo != "")
                            {
                                dayModel.dashboardUnloadInfo = dayModel.dashboardUnloadInfo.Substring(0, dayModel.dashboardUnloadInfo.Length - 3);
                            }
                        }

                        if (dayModel.clientSubmitInfo != "" || dayModel.dashboardUnloadInfo != "")
                        {
                            modelList.Add(dayModel);
                        }


                        #endregion

                        #region 添加night model

                        //查询Night 信息.
                        DataRow[] drTrackingNight = dtTracking.Select(" day = '" + dTemp.ToString("yyyy-MM-dd") + "' and shift = 'Night' and machineID = '" + i.ToString() + "'");
                        DataRow[] drMaterialNight = dtMaterial.Select("REF_FIELD02 = '" + dTemp.ToString("yyyy-MM-dd HH:mm:ss") + "' and REF_FIELD01 = 'Night' and MachineID = '" + i.ToString() + "' ");


                        ViewModel.MouldMaterialTraceability nightModel = new ViewModel.MouldMaterialTraceability();
                        nightModel.day = dTemp;
                        nightModel.shift = "Night";
                        nightModel.machineID = i.ToString();
                        nightModel.clientSubmitInfo = "";
                        nightModel.dashboardUnloadInfo = "";


                        //获取client, dashboard-unload的material信息, 按照[materialNo - LotNo - weigh]的格式组合
                        if (drTrackingNight.Length > 0)
                        {
                            nightModel.partNumberALL = drTrackingNight[0]["PartNumberAll"].ToString();

                            //2021-02-01新增, MH ID列不能显示不是MH组别下的ID
                            {
                                string tempUserID = drTrackingDay[0]["userID"].ToString();
                                if (mouldingUserMHList.Where(user => user.USER_ID.ToUpper() == tempUserID.ToUpper()).Count() > 0)
                                    nightModel.clientUserID = tempUserID;
                                else
                                    nightModel.clientUserID = string.Empty;
                            }

                            foreach (DataRow dr in drTrackingNight)
                            {
                                string materialNo_1 = dr["matPart01"].ToString();
                                string materialLotNo_1 = dr["matLot01"].ToString();
                                string weight_1 = dr["partsWeight"].ToString();

                                string materialNo_2 = dr["matPart02"].ToString();
                                string materialLotNo_2 = dr["matLot02"].ToString();
                                string weight_2 = dr["parts2Weight"].ToString();


                                string strMaterial = "";

                                if (materialNo_1 != "" && materialLotNo_1 != "" && weight_1 != "")
                                {
                                    strMaterial = string.Format("[{0} - {1} - {2}]", materialNo_1, materialLotNo_1, weight_1);

                                    //去重, 避免重复添加相同的material信息
                                    if (!nightModel.clientSubmitInfo.Contains(strMaterial))
                                    {
                                        nightModel.clientSubmitInfo += (strMaterial + "  ,  ");
                                    }
                                }


                                if (materialNo_2 != "" && materialLotNo_2 != "" && weight_2 != "")
                                {
                                    strMaterial = string.Format("[{0} - {1} - {2}]", materialNo_2, materialLotNo_2, weight_2);

                                    //去重, 避免重复添加相同的material信息
                                    if (!nightModel.clientSubmitInfo.Contains(strMaterial))
                                    {
                                        nightModel.clientSubmitInfo += strMaterial + "  ,  ";
                                    }
                                }
                            }

                            if (nightModel.clientSubmitInfo != "")
                            {
                                nightModel.clientSubmitInfo = nightModel.clientSubmitInfo.Substring(0, nightModel.clientSubmitInfo.Length - 3);
                            }
                        }


                        //获取dashboard-unload信息, 按照[materialNo - LotNo - weigh]的格式组合
                        if (drMaterialNight.Length > 0)
                        {
                            //2021-02-01新增, MH ID列不能显示不是MH组别下的ID
                            {
                                string tempUserID = drMaterialNight[0]["User_Name"].ToString();
                                if (mouldingUserMHList.Where(user => user.USER_ID.ToUpper() == tempUserID.ToUpper()).Count() > 0)
                                    nightModel.unloadUserID = tempUserID;
                                else
                                    nightModel.unloadUserID = string.Empty;
                            }

                            foreach (DataRow dr in drMaterialDay)
                            {
                                string materialNo = dr["Material_No"].ToString();
                                string materialLotNo = dr["Material_LotNo"].ToString();
                                string weight = dr["Transaction_Weight"].ToString();

                                nightModel.dashboardUnloadInfo += string.Format("[{0} - {1} - {2}]", materialNo, materialLotNo, weight) + "  ,  ";
                            }

                            if (nightModel.dashboardUnloadInfo != "")
                            {
                                nightModel.dashboardUnloadInfo = nightModel.dashboardUnloadInfo.Substring(0, nightModel.dashboardUnloadInfo.Length - 3);
                            }
                        }

                        if (nightModel.clientSubmitInfo != "" || nightModel.dashboardUnloadInfo != "")
                        {
                            modelList.Add(nightModel);
                        }
                        #endregion
                    }

                    dTemp = dTemp.AddDays(1);
                }

                string[] machine = sMachineID == "" ? new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" } : new string[] { sMachineID };


                var result = (from a in modelList
                              where machine.Contains(a.machineID)
                              select a).ToList();

                return result;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MaterialTraceability_Debug", "GetMaterialTraceability catch exception :" + ee.ToString());
                return null;
            }
        }


    }
}