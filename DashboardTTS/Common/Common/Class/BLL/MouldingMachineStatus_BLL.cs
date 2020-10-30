using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


namespace Common.Class.BLL
{
    public class MouldingMachineStatus_BLL
    {
        Common.Class.DAL.MouldingMachineStatus_DAL dal = new DAL.MouldingMachineStatus_DAL();

        public Dictionary<int, string> GetCurrentStatus()
        {
            Dictionary<int, string> dicStatus = new Dictionary<int, string>();

            DataTable dt = dal.GetTodayList();
            if (dt == null ||dt.Rows.Count == 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    dicStatus.Add(i, StaticRes.Global.MouldingStatus.ShutDown);
                }               
            }
            else
            {
                for (int i = 1; i < 10; i++)
                {
                    DataRow[] arrDrTemp = dt.Select(" MachineID = '" + i.ToString() + "'");
                    if (arrDrTemp == null || arrDrTemp.Count() == 0)
                    {
                        dicStatus.Add(i, StaticRes.Global.MouldingStatus.ShutDown);
                    }
                    else
                    {
                        dicStatus.Add(i, arrDrTemp[0]["MachineStatus"].ToString());
                    }
                }
            }           

            return dicStatus;
        }




        
        public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sShift, string sDateNotIn, bool bExceptWeekend)
        {
            Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();

            #region Date time for db record
            DataSet ds = dal.SelectList(dTimeFrom, dTimeTo, sMachineNo, sShift);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            
            DataTable dt = ds.Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        string MachineStatus = dr["MachineStatus"].ToString().Trim();

                        DateTime tmp = DateTime.Parse(dr["StartTime"].ToString());
                        DateTime EndTime = DateTime.Parse(dr["EndTime"].ToString());

                        while (tmp <= EndTime)
                        {
                            if (!dPoints.ContainsKey(tmp))
                            {
                                #region add each minute status
                                switch (MachineStatus)
                                {
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Running:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Running);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Adjustment:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Adjustment);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Schedule:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.No_Schedule);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Mould_Testing:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Mould_Testing);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Material_Testing:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Material_Testing);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Change_Model:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Change_Model);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Operator:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.No_Operator);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Material:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.No_Material);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Break_Time:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Break_Time);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.ShutDown:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.ShutDown);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.Login_Out:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.Login_Out);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.DamageMould:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.DamageMould);
                                        }
                                        break;
                                    case StaticRes.Global.clsConstValue.ConstMouldingStatus.MachineBreak:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.MachineBreak);
                                        }
                                        break;
                                    default:
                                        if (IsTimeValid(tmp, dTimeFrom, dTimeTo) && IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))
                                        {
                                            dPoints.Add(tmp, StaticRes.Global.StatusType.ShutDown);
                                        }
                                        break;
                                }
                                #endregion
                            }

                            tmp = tmp.AddMinutes(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:		public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", " ID=" + dr["ID"].ToString() + " -- " + ex.ToString());
                    }
                }
            }
            #endregion 

            #region check whole time,   unknow time as shutdown time  -- not use
            //try
            //{
            //    DateTime StartTime = dTimeFrom.Date.AddHours(8);
            //    DateTime StopTime = dTimeTo.Date.AddHours(32) > DateTime.Now ? DateTime.Now : dTimeTo.Date.AddHours(32);

            //    DateTime tmp = StartTime;

            //    while (tmp < StopTime)
            //    {
            //        if (dPoints != null)
            //        {
            //            if (!dPoints.ContainsKey(tmp))
            //            {
            //                if (  IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))   //2018 12 04 
            //                {
            //                    dPoints.Add(tmp, StaticRes.Global.StatusType.ShutDown);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (IsDateTimeIn(tmp, sDateNotIn, dTimeFrom, bExceptWeekend))   //2018 12 04 
            //            {
            //                dPoints.Add(tmp, StaticRes.Global.StatusType.ShutDown);
            //            }
            //        }
                  

            //        tmp = tmp.AddMinutes(1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:		public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", " -- " + ex.ToString());
            //}
            #endregion

            return dPoints;
        }
        
        bool IsDateTimeIn(DateTime dTime, string sDateNotIn, DateTime dTimeFrom, bool bExceptWeekend)
        {
            bool Result = false;
            try
            { 
                if (bExceptWeekend)
                {
                    if (dTime.AddHours(-8).DayOfWeek == DayOfWeek.Saturday || dTime.AddHours(-8).DayOfWeek == DayOfWeek.Sunday)
                    {
                        return false;
                    }
                }

                if (sDateNotIn == "")
                {
                    return true;
                }
                string[] DateArr = sDateNotIn.Split(',');

                int i;
                for (i = 0; i < DateArr.Length; i++)
                {
                    string StrNotInDate = dTimeFrom.ToString("yyyy-MM") + "-" + DateArr[i];

                    DateTime DTNotInDateFrom = DateTime.Parse(StrNotInDate).AddHours(8);
                    DateTime DTNotInDateTo = DateTime.Parse(StrNotInDate).AddDays(1).AddHours(8);

                    if (dTime >= DTNotInDateFrom && dTime < DTNotInDateTo)
                    {
                        Result = false;
                        break;
                    }
                    else
                    {

                        Result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Result = false;
                string msg = ex.ToString();
            }
            return Result;
        }
        
        private bool IsTimeValid( DateTime dTime, DateTime DateFrom, DateTime DateTo)
        {
            bool Result = true;

            DateFrom = DateFrom.Date.AddHours(8);
            DateTo = DateTo.Date.AddDays(1).AddHours(8);

            if (dTime < DateTo && dTime >= DateFrom)
            {
                Result = true;
            }
            else
            {
                Result = false;
            }


            return Result;
        }





        public List<Common.Class.Model.MouldingMachineStatus_Model> GetModelList(DateTime? dDateTime, DateTime? dDateTo, string sShift, string sMachineID, string sStatus)
        {
            DataTable dt = dal.GetList(dDateTime, dDateTo, sShift, sMachineID, sStatus);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<Common.Class.Model.MouldingMachineStatus_Model> modelList = new List<Model.MouldingMachineStatus_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                Common.Class.Model.MouldingMachineStatus_Model model = new Model.MouldingMachineStatus_Model();
                
                model.MachineID = dr["MachineID"].ToString();
                model.Day = DateTime.Parse(dr["Day"].ToString());
                model.Shift = dr["Shif"].ToString();
                model.MachineStatus = dr["MachineStatus"].ToString();
                model.OEEStatus = dr["OEEStatus"].ToString(); 
                model.StartTime = DateTime.Parse(dr["StartTime"].ToString());
               

                //当天 默认赋值 datetime now, 不是和默认赋值 当时班次的最后时间点.
                if (dr["EndTime"] == null || dr["EndTime"].ToString() == "")
                {
                    if (model.Day == DateTime.Now.Date)
                        model.EndTime = DateTime.Now;
                    else
                        model.EndTime = model.Shift == "Day" ? model.Day.AddHours(20) : model.Day.AddDays(1).AddHours(8);
                }
                else
                {
                    model.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                }


             
                model.PartNo = dr["PartNo"].ToString();
                model.UserID = dr["UserID"].ToString();
                model.Remark = dr["Remark"].ToString();
                
                modelList.Add(model);                
            }


            return modelList;
        }



    }
}
