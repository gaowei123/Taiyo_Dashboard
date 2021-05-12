
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Taiyo.Enum.Production;
using Taiyo.Tool;
using Taiyo.Tool.Extension;

namespace Common.BLL
{
    /// <summary>
    /// LMMSEventLog_BLL
    /// </summary>
    public class LMMSEventLog_BLL
    {
        private readonly Common.DAL.LMMSEventLog_DAL _dal = new Common.DAL.LMMSEventLog_DAL();


        
        bool IsRightShift(DateTime dt, string shift)
        {
            bool result = false;

            string _strWorkingDayAM = "08:00";
            string _strWorkingDayPM = "20:00";
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            switch (shift)
            {
                case StaticRes.Global.Shift.Day:
                    {
                        TimeSpan dspNow = dt.TimeOfDay;
                        if (dspNow >= dspWorkingDayAM && dspNow < dspWorkingDayPM)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    break;
                case StaticRes.Global.Shift.Night:
                    {
                        TimeSpan dspNow = dt.TimeOfDay;
                        if (dspNow >= dspWorkingDayAM && dspNow < dspWorkingDayPM)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    break;
                case StaticRes.Global.Shift.ALL:
                    result = true;
                    break;

                case "":
                    result = true;
                    break;

                default:
                    break;
            }

            return result;
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
        

        public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo, string shift, string sDateNotIn, bool bExceptWeekend)
        {
            DataSet ds = new DataSet();
            ds = _dal.getOEE(dTimeFrom.AddDays(-1), dTimeTo, sMachineNo);
            Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                //add OEE status , except power on & off
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        //2018 10 24 remove abnormal record whose datetime is only including day without time.
                        if (DateTime.Parse(dr["DateTime"].ToString()) == DateTime.Parse(dr["DateTime"].ToString()).Date && DateTime.Parse(dr["stopTime"].ToString()) < DateTime.Now.AddMinutes(-5))
                        {
                            continue;
                        }

                        if (dr["currentOperation"].ToString().Trim() == "TECHNICIAN_OEE")
                        {
                            DateTime tmp = DateTime.Parse(dr["startTime"].ToString());
                            //2018 04 03 marked by wei lijia , some stop time will be the first minutes of next day. it will cause error
                            //while (tmp <= DateTime.Parse(dr["stopTime"].ToString()))
                            while (tmp < DateTime.Parse(dr["stopTime"].ToString()))
                            {
                                DateTime dTime = new DateTime();
                                dTime = tmp;
                                if (!dPoints.ContainsKey(dTime))
                                {
                                    if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Adjustment
                                      || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Others
                                      || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Teaching
                                      || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.UnderPm
                                      || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.UnderSetup
                                      || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Buyoff)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Buyoff);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Maintainence)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Maintance);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Setup)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Setup);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.NoSchdule)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.NoWIP);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Testing)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Testing);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.BreakDown)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.BreakDown);
                                            }
                                        }


                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOff)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Adjustment);
                                            }
                                        }

                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOn)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Adjustment);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.Adjustment);
                                            }
                                        }
                                    }
                                }
                                tmp = tmp.AddMinutes(1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:		public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", " ID=" + dr["ID"].ToString() + " -- " + ex.ToString());

                    }
                }


                //add  power on & off  status
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        if (dr["currentOperation"].ToString().Trim() == "SYSTEM_OEE")
                        {
                            DateTime tmp = DateTime.Parse(dr["startTime"].ToString());
                            //2018 04 03 marked by wei lijia , some stop time will be the first minutes of next day. it will cause error
                            //while (tmp <= DateTime.Parse(dr["stopTime"].ToString()))
                            while (tmp < DateTime.Parse(dr["stopTime"].ToString()))
                            {
                                DateTime dTime = new DateTime();
                                dTime = tmp;
                                if (!dPoints.ContainsKey(dTime))
                                {
                                    if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOff)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.ShutDown);
                                            }
                                        }
                                    }
                                    else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOn)
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.PD);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dTime >= dTimeFrom.Date.AddHours(8) && dTime < dTimeTo.Date.AddDays(1).AddHours(8))
                                        {
                                            if (IsRightShift(dTime, shift) && IsDateTimeIn(dTime, sDateNotIn, dTimeFrom, bExceptWeekend))
                                            {
                                                dPoints.Add(dTime, StaticRes.Global.StatusType.PD);
                                            }
                                        }
                                    }
                                }
                                tmp = tmp.AddMinutes(1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:		public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", " ID=" + dr["ID"].ToString() + " -- " + ex.ToString());
                    }
                }
                return dPoints;
            }

        }
        
        public double GetStatusTime(DateTime dDay, string sShift, string sMachineID, params string[] sStatus)
        {
            DateTime dDateFrom = new DateTime();
            DateTime dDateTo = new DateTime();
            TimeSpan TimeCount = new TimeSpan();

            if (sShift == StaticRes.Global.Shift.Day)
            {
                dDateFrom = dDay.Date.AddHours(8);
                dDateTo = dDay.Date.AddHours(20);
            }
            else
            {
                dDateFrom = dDay.Date.AddHours(20);
                dDateTo = dDay.Date.AddDays(1).AddHours(8);
            }



            DataTable dt = new DataTable();
            dt = _dal.GetTimeByStatus(dDateFrom, dDateTo, sMachineID, sStatus);
            if (dt == null || dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime StartTime = DateTime.Parse(dr["startTime"].ToString());
                    DateTime StopTime = DateTime.Parse(dr["stopTime"].ToString());

                    TimeCount = TimeCount + (StopTime - StartTime);
                }
            }

            return TimeCount.TotalSeconds;
        }





















        //由于旧逻辑遍历到每一秒, 在生成yearly的数据时处理时间过长,改为以下为新逻辑, 
        //新逻辑 将原数据按照 8:00,  20:00的时间点来切分重组.
        //将lmmseventlog表中的源数据按照 year, month, day , shift , machine , status , totalseconds归类重组.
        #region 

        private List<string> GetMachineStatusList()
        {
            return new List<string>(){
                StaticRes.Global.LaserStatus.Run,
                StaticRes.Global.LaserStatus.Buyoff,
                StaticRes.Global.LaserStatus.Setup,
                StaticRes.Global.LaserStatus.NoSchedule,
                StaticRes.Global.LaserStatus.Testing,
                StaticRes.Global.LaserStatus.Maintenance,
                StaticRes.Global.LaserStatus.Breakdown,
                StaticRes.Global.LaserStatus.Shutdown};
        }
        

        //获取元数据,并切分重组到新集合中.
        private List<Common.Model.LMMSEventLog_Model.Detail> GetBaseList(DateTime dDateFrom, DateTime dDateTo)
        {

            //源数据   前延2天,后延1天, 防止漏查.
            DataTable dtEvent = _dal.GetListForModelList(dDateFrom.AddDays(-2), dDateTo.AddDays(1), "", "");
            if (dtEvent == null || dtEvent.Rows.Count == 0)
                return null;






            Common.Model.LMMSEventLog_Model.Detail model;




            //foreach dtEvent
            //按照year, month, daily, shift保存到 allStatusModels中.
            List<Common.Model.LMMSEventLog_Model.Detail> allStatusModels = new List<LMMSEventLog_Model.Detail>();
            foreach (DataRow dr in dtEvent.Rows)
            {
                
                string machineID = dr["machineID"].ToString();
                string OEEtype = dr["currentOperation"].ToString();
                string status = dr["eventTrigger"].ToString();
                DateTime startTime = DateTime.Parse(dr["startTime"].ToString());
                DateTime stopTime = DateTime.Parse(dr["stopTime"].ToString());


                if (machineID == "1")
                {

                }
           


                //buyoff, setup, testing等状态超过12小时的全部跳过.
                if ( (new string[] { "BUYOFF", "SETUP" ,"TESTING" }).Contains(status) &&  (stopTime - startTime ).TotalHours >= 12)
                    continue;



                //2021-4-14
                //no schedule单独设置, 实际情况确实会有挂机超过12小时的, 放宽到13小时
                if (status == "NO SCHEDULE" && (stopTime - startTime).TotalHours > 14)
                    continue;





                //不考虑startTime, stopTime超过1天的数据, 属于异常数据, 
                //早期client的一个bug导致的.  好像是scan状态后重启Client导致生成2条相同状态的eventlog,
                //但再次scan停止时, 只有一条记录会被更新, 而另一条的stoptime会一直被pingapp不停更新stoptime.
                if ((stopTime - startTime).TotalDays > 1)
                    continue;


             





                //4种情况
                //1. 在8-8点的时间段内
                //2. 跨8:00 或 20:00 一个时间段
                //3. 跨8:00 和 20:00 二个时间段
                //4. 跨三个时间段 (目前只发现,机器停一天, pingApp产生的).

                DateTime dDate = startTime.AddHours(-8).Date;
                if (startTime >= dDate.AddHours(8) && stopTime >= dDate.AddHours(8) && startTime < dDate.AddHours(20) && stopTime < dDate.AddHours(20))
                {
                    #region startTime, stopTime在8-20 白班时间内
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;

                    allStatusModels.Add(model);
                    #endregion
                }
                else if (startTime >= dDate.AddHours(20) && stopTime >= dDate.AddHours(20) && startTime < dDate.AddHours(32) && stopTime < dDate.AddHours(32))
                {
                    #region starttime, stoptime在20 - 8点 夜班时间内                    
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;

                    allStatusModels.Add(model);
                    #endregion
                }
                //最大范围在上.
                else if ((startTime < dDate.AddDays(1).AddHours(8) && stopTime > dDate.AddDays(1).AddHours(8))  //跨 8:00 这个点
                            &&
                           (startTime < dDate.AddDays(1).AddHours(20) && stopTime > dDate.AddDays(1).AddHours(20)))//跨20:00 这个点
                {
                    #region 周日0:00 - 隔天0;00  跨3个时间段,  0;00-8:00,  8:00-20:00,   20:00-隔天0:00

                    //第一部分 startTime - 8:00,  属于前一天night
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddDays(1).AddHours(8);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);

                    //第二部分 8:00 - 20:00,   属于当天day
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.AddDays(1).Year;
                    model.month = dDate.AddDays(1).Month;
                    model.day = dDate.AddDays(1);
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddDays(1).AddHours(8);
                    model.stopTime = dDate.AddDays(1).AddHours(20);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);

                    //第三部分 20:00 - stoptime,  属于当天night
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.AddDays(1).Year;
                    model.month = dDate.AddDays(1).Month;
                    model.day = dDate.AddDays(1);
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddDays(1).AddHours(20);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }
                else if (startTime<dDate.AddHours(20) && stopTime > dDate.AddDays(1).AddHours(8))
                {
                    #region 跨20:00,  8:00 2个时间段

                    //前一部分 属于当天白班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(20);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);


                    //中间部分 属于当天晚班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(20);
                    model.stopTime = dDate.AddDays(1).AddHours(8);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);

                    //后一部分 属于下一天白班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.AddDays(1).Year;
                    model.month = dDate.AddDays(1).Month;
                    model.day = dDate.AddDays(1);
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddDays(1).AddHours(8);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);


                    #endregion 
                }
                //小范围在下
                else if (startTime < dDate.AddHours(8) && stopTime > dDate.AddHours(8))
                {
                    #region starttime, stoptime 跨当天8:00am 这个点

                    //拆分2条记录

                    //前半部分 属于前一天晚班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.AddDays(-1).Year;
                    model.month = dDate.AddDays(-1).Month;
                    model.day = dDate.AddDays(-1);
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(8);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);


                    //后半部分, 属于当天白班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(8);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }

                else if (startTime < dDate.AddDays(1).AddHours(8) && stopTime > dDate.AddDays(1).AddHours(8))
                {
                    #region starttime, stoptime 跨下一天8:00am 这个点

                    //拆分2条记录

                    //前半部分 属于当天晚班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddDays(1).AddHours(8);
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);


                    //后半部分, 属于下一天白班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.AddDays(1).Year;
                    model.month = dDate.AddDays(1).Month;
                    model.day = dDate.AddDays(1);
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddDays(1).AddHours(8);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }

                else if (startTime < dDate.AddHours(20) && stopTime > dDate.AddHours(20))
                {
                    #region starttime, stoptime 跨 20:00这个点

                    //拆分2条记录

                    //前半部分, 属于当天早班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(20); 
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);





                    //后半部分, 属于当天晚班
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(20);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }

            }




            


            return allStatusModels;
        }
        

        //补充缺少的天数, 状态, 避免调用时查询某个状态数据不存在而导致报错.
        private List<Common.Model.LMMSEventLog_Model.Detail> AddLackData(List<Common.Model.LMMSEventLog_Model.Detail> modelList, DateTime dDateFrom, DateTime dDateTo)
        {

            Common.Model.LMMSEventLog_Model.Detail model;
            List<Common.Model.LMMSEventLog_Model.Detail> lackModelList = new List<LMMSEventLog_Model.Detail>();




            #region 循环每一天, 每台机器, 每个状态的 白晚班次的机器状态信息.  没有补充,时长默认为0.
            DateTime dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1; i < 9; i++)
                {
                    List<string> statusList = GetMachineStatusList();
                    foreach (string strStatus in statusList)
                    {
                        if (strStatus == StaticRes.Global.LaserStatus.Run) continue;

                        
                        #region add day model
                        var dayModel = (from a in modelList
                                        where a.day == dTemp.Date
                                        && a.machineID == i.ToString()
                                        && a.status == strStatus
                                        && a.shift == StaticRes.Global.Shift.Day
                                        select a).FirstOrDefault();
                        if (dayModel == null)
                        {
                            model = new LMMSEventLog_Model.Detail();
                            model.year = dTemp.Year;
                            model.month = dTemp.Month;
                            model.day = dTemp;
                            model.shift = StaticRes.Global.Shift.Day;
                            model.machineID = i.ToString();
                            model.status = strStatus;
                            model.startTime = null;
                            model.stopTime = null;
                            model.totalSeconds = 0;
                            lackModelList.Add(model);
                        }
                        #endregion
                        
                 
                        #region add night model
                        var nightModel = (from a in modelList
                                          where a.day == dTemp.Date
                                          && a.machineID == i.ToString()
                                          && a.status == strStatus
                                          && a.shift == StaticRes.Global.Shift.Night
                                          select a).FirstOrDefault();
                        if (nightModel == null)
                        {
                            model = new LMMSEventLog_Model.Detail();
                            model.year = dTemp.Year;
                            model.month = dTemp.Month;
                            model.day = dTemp;
                            model.shift = StaticRes.Global.Shift.Night;
                            model.machineID = i.ToString();
                            model.status = strStatus;
                            model.startTime = null;
                            model.stopTime = null;
                            model.totalSeconds = 0;
                            lackModelList.Add(model);
                        }
                        #endregion
                    }
                }
                dTemp = dTemp.AddDays(1);
            }
            #endregion





            //将缺少的数据合并到list中.
            modelList.AddRange(lackModelList);



            #region power off, 查询该机器, 该天总时长是否为0, 是则将power off赋值 12*3600

            // 早上8点之前
            


            dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1  ; i < 9; i++)
                {

                    #region  处理白班 shutdown
                    // 查询这一天, 这台机器, 白班的所有信息.
                    var machineDayList = from a in modelList
                                         where a.day == dTemp.Date && 
                                         a.machineID == i.ToString() && 
                                         a.shift == Taiyo.Enum.CommonEnum.Shift.Day.GetDescription()
                                         select a;


                    
                    // 白班 shutdown 的记录
                    var machinePoweroffDayModel = (from a in modelList
                                                   where a.day == dTemp.Date && a.machineID == i.ToString() &&
                                                   a.shift == Taiyo.Enum.CommonEnum.Shift.Day.GetDescription() &&
                                                   a.status == StaticRes.Global.LaserStatus.Shutdown
                                                   select a).FirstOrDefault();



                    if (machineDayList.Sum(p => p.totalSeconds) == 0 && 
                        Taiyo.Tool.Common.CurDay != dTemp && 
                        Taiyo.Tool.Common.CurShift != Taiyo.Enum.CommonEnum.Shift.Day)
                    {
                        //如果这一天总时长为0, 并且不是当天白班, 则认为是关机状态, 设置 shutdown 12*3600
                        machinePoweroffDayModel.totalSeconds = 12 * 3600;
                    }
                    else 
                    {
                        try
                        {
                            // 如果是当天白班, 也没有查到 power on 的一条记录, 则也认为是关机状态, 设置 shutdown 12*3600
                            DataRow[] arrDr = _dal.GetSourceList(dTemp, dTemp.AddDays(1), i.ToString()).Select(" eventTrigger = 'POWER ON' ");
                            if (arrDr == null || arrDr.Count() == 0)
                            {
                                machinePoweroffDayModel.totalSeconds = 12 * 3600;
                            }
                        }
                        catch (Exception)
                        {
                            machinePoweroffDayModel.totalSeconds = 12 * 3600;
                        }
                    }

                    #endregion

                    
                    #region 处理晚班 shutdown
                    //查询这一天, 这台机器, 晚班的所有信息.
                    var machineNightList = from a in modelList
                                           where a.day == dTemp && 
                                           a.machineID == i.ToString() && 
                                           a.shift == Taiyo.Enum.CommonEnum.Shift.Night.GetDescription()
                                           select a;

                    var machinePoweroffNightModel = (from a in modelList
                                                     where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Night && a.status == StaticRes.Global.LaserStatus.Shutdown
                                                     select a).FirstOrDefault();


                    if (machineNightList.Sum(p => p.totalSeconds) == 0 &&
                        Taiyo.Tool.Common.CurDay != dTemp &&
                        Taiyo.Tool.Common.CurShift != Taiyo.Enum.CommonEnum.Shift.Night)
                    {
                        //如果这一天总时长为0, 并且不是当天晚班, 则认为是关机状态, 设置 shutdown 12*3600
                        machinePoweroffNightModel.totalSeconds = 12 * 3600;
                    }
                    else
                    {
                        try
                        {
                            // 如果是当天晚班, 也没有查到 power on 的一条记录, 则也认为是关机状态, 设置 shutdown 12*3600
                            DataRow[] arrDr = _dal.GetSourceList(dTemp, dTemp.AddDays(1), i.ToString()).Select(" eventTrigger = 'POWER ON' ");
                            if (arrDr == null || arrDr.Count() == 0)
                            {
                                machinePoweroffNightModel.totalSeconds = 12 * 3600;
                            }
                        }
                        catch (Exception)
                        {
                            machinePoweroffNightModel.totalSeconds = 12 * 3600;
                        }
                    }
                    #endregion

                }

                dTemp = dTemp.AddDays(1);
            }
            #endregion






            #region 添加 running model,   = 12*3600 - 其他各状态总时长

            DateTime curDay = Taiyo.Tool.Common.CurDay;
            string curShift = Taiyo.Tool.Common.CurShift.GetDescription();
      
            
            dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1; i < 9; i++)
                {

                    //查询这一天, 这台机器, 白班的所有信息.
                    var machineDayList = from a in modelList
                                         where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Day
                                         select a;
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dTemp.Year;
                    model.month = dTemp.Month;
                    model.day = dTemp;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = i.ToString();
                    model.status = StaticRes.Global.LaserStatus.Run;
                    model.startTime = null;
                    model.stopTime = null;

                    if (curDay == model.day && curShift == model.shift)
                    {
                        model.totalSeconds = (DateTime.Now - model.day.AddHours(8)).TotalSeconds - machineDayList.Sum(p => p.totalSeconds);
                    }
                    else
                    {
                        model.totalSeconds = 12 * 3600 - machineDayList.Sum(p => p.totalSeconds);
                    }
                    model.totalSeconds = model.totalSeconds < 0 ? 0 : model.totalSeconds;//出去负数的情况
                    
                    modelList.Add(model);
                    

                    //查询这一天, 这台机器, 晚班的所有信息.
                    var machineNightList = from a in modelList
                                           where a.day == dTemp && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Night
                                           select a;
                    model = new LMMSEventLog_Model.Detail();
                    model.year = dTemp.Year;
                    model.month = dTemp.Month;
                    model.day = dTemp;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = i.ToString();
                    model.status = StaticRes.Global.LaserStatus.Run;
                    model.startTime = null;
                    model.stopTime = null;


                    if (curDay == model.day && curShift == model.shift)
                    {
                        model.totalSeconds = (DateTime.Now - model.day.AddHours(20)).TotalSeconds - machineDayList.Sum(p => p.totalSeconds);
                    }
                    else
                    {
                        model.totalSeconds = 12 * 3600 - machineNightList.Sum(p => p.totalSeconds);
                    }
                    model.totalSeconds = model.totalSeconds < 0 ? 0 : model.totalSeconds;//出去负数的情况

                    modelList.Add(model);
                }


                dTemp = dTemp.AddDays(1);
            }
            #endregion




            return modelList;
        }


        /// <summary>
        /// 返回包括 
        /// year,
        /// month, 
        /// day, 
        /// shift, 
        /// machineID, 
        /// status, 
        /// total usage seconds
        /// 的集合
        /// </summary>
        public List<Common.Model.LMMSEventLog_Model.Detail> GetStatusModelList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sStatus, string sShift, bool bExceptWeekend)
        {

            //event log表原数据切分, 归类到list中. 
            List<Common.Model.LMMSEventLog_Model.Detail> baseList = GetBaseList(dDateFrom, dDateTo);
            if (baseList == null)
                return null;




            //添加缺失的天, 机器, 状态数据.
            baseList = AddLackData(baseList, dDateFrom, dDateTo);

            

            //按条件删选记录.   
            sStatus = sStatus == "UTILIZATION" ? StaticRes.Global.LaserStatus.Run : sStatus;
            string[] arrStatus = string.IsNullOrEmpty(sStatus) ? GetMachineStatusList().ToArray() : new string[] { sStatus };
            string[] arrShift = string.IsNullOrEmpty(sShift) ? new string[] { StaticRes.Global.Shift.Day, StaticRes.Global.Shift.Night } : new string[] { sShift };
            int[] arrWeekDay = bExceptWeekend ? new int[] { 1, 2, 3, 4, 5 } : new int[] { 0, 1, 2, 3, 4, 5, 6 };
            string[] arrMachine = string.IsNullOrEmpty(sMachineID) ? new string[] { "1", "2", "3", "4", "5", "6", "7", "8" } : new string[] { sMachineID };
            
            var result = (from a in baseList
                          where a.day >= dDateFrom.Date
                          && a.day < dDateTo
                          && arrShift.Contains(a.shift)
                          && arrMachine.Contains(a.machineID)
                          && arrStatus.Contains(a.status)
                          && arrWeekDay.Contains((int)a.day.DayOfWeek)
                          orderby a.day ascending, a.startTime ascending
                          select a).ToList();



            return result;
        }
        #endregion

        

        public class UsedRate
        {
            public int MachineID { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }
        }
        /// <summary>
        /// 获取当天的机器run时间占比
        /// used rate -->  (run + setup + buyoff + testing) / total(shutdown除外的总时间)
        /// </summary>
        /// <returns>
        /// 返回 机器ID-比率 的键值对
        /// </returns>
        public List<UsedRate> GetCurrentDayUsedRate()
        {
            List<Common.Model.LMMSEventLog_Model.Detail> statusList = GetStatusModelList(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "", "", "", false);
            if (statusList == null)
                return null;

            var test = from a in statusList where a.machineID == "1" select a;


            var runList = from a in statusList
                          where a.status == StaticRes.Global.LaserStatus.Run
                          group a by a.machineID into b
                          select new
                          {
                              MachineID = b.Key,
                              TotalSeconds = b.Sum(p => p.totalSeconds)
                          };
            var setupList = from a in statusList
                          where a.status == StaticRes.Global.LaserStatus.Setup
                          group a by a.machineID into b
                          select new
                          {
                              MachineID = b.Key,
                              TotalSeconds = b.Sum(p => p.totalSeconds)
                          };
            var buyoffList = from a in statusList
                          where a.status == StaticRes.Global.LaserStatus.Buyoff
                          group a by a.machineID into b
                          select new
                          {
                              MachineID = b.Key,
                              TotalSeconds = b.Sum(p => p.totalSeconds)
                          };
            var testingList = from a in statusList
                          where a.status == StaticRes.Global.LaserStatus.Testing
                          group a by a.machineID into b
                          select new
                          {
                              MachineID = b.Key,
                              TotalSeconds = b.Sum(p => p.totalSeconds)
                          };




            var totalTimeList = from a in statusList
                                where a.status != StaticRes.Global.LaserStatus.Shutdown
                                group a by a.machineID into b
                                select new
                                {
                                    MachineID = b.Key,
                                    TotalSeconds = b.Sum(p => p.totalSeconds)
                                };

         
            List<UsedRate> resultList = new List<UsedRate>();
            for (int i = 1; i < 9; i++)
            {
                double runSeconds = (from a in runList where a.MachineID == i.ToString() select a.TotalSeconds).FirstOrDefault();
                double setupSeconds = (from a in setupList where a.MachineID == i.ToString() select a.TotalSeconds).FirstOrDefault();
                double buyoffSeconds = (from a in buyoffList where a.MachineID == i.ToString() select a.TotalSeconds).FirstOrDefault();
                double testingSeconds = (from a in testingList where a.MachineID == i.ToString() select a.TotalSeconds).FirstOrDefault();
         
                double totalSeconds = (from a in totalTimeList where a.MachineID == i.ToString() select a.TotalSeconds).FirstOrDefault();
                


                resultList.Add(new UsedRate()
                {
                    MachineID = i,
                    Value = Math.Round((runSeconds + setupSeconds + buyoffSeconds + testingSeconds) / totalSeconds * 100, 2),
                    Description = string.Format("({0} + {1} + {2} + {3}) / {4} * 100", Math.Round(runSeconds/3600,2)
                                                                                     , Math.Round(setupSeconds / 3600, 2)
                                                                                     , Math.Round(buyoffSeconds / 3600, 2)
                                                                                     , Math.Round(testingSeconds / 3600, 2)
                                                                                     , Math.Round(totalSeconds / 3600, 2))
                });
            }

            return resultList;
        }

       
        /// <summary>
        /// 获取机器当前最新的状态
        /// </summary>
        /// <returns>
        /// 返回 McID-Status 的键值对
        /// </returns>
        public Dictionary<int, LaserStatus> GetCurrentStatus()
        {
            Dictionary<int, LaserStatus> dicStatus = new Dictionary<int, LaserStatus>();

            DataTable dt = _dal.GetCurrentStatusList(string.Empty);
            if (dt != null & dt.Rows.Count != 0)
            {
                dt = dt.Select("", " stoptime desc ,currentOperation desc ").CopyToDataTable();
            }
           

            for (int i = 1; i < 9; i++)
            {
                LaserStatus status;

                DataRow[] drArrTemp = dt.Select("machineID = " + i.ToString() + "", "");
                if (drArrTemp == null || drArrTemp.Length == 0)
                    status = LaserStatus.Shutdown;
                else
                    status = StatusConventor.ConventLaser(drArrTemp[0]["eventTrigger"].ToString());


                dicStatus.Add(i, status);
            }

            return dicStatus;
        }
        
    }
}
