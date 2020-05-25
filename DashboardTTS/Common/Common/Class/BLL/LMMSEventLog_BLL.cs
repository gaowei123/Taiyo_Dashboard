
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Common.Model;

namespace Common.BLL
{
    /// <summary>
    /// LMMSEventLog_BLL
    /// </summary>
    public class LMMSEventLog_BLL
    {
        private readonly Common.DAL.LMMSEventLog_DAL dal = new Common.DAL.LMMSEventLog_DAL();
        public LMMSEventLog_BLL()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            DataSet ds = new DataSet();
            ds = dal.Exists();
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Common.Model.LMMSEventLog_Model model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Model.LMMSEventLog_Model model)
        {
            return dal.AddCommand(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Model.LMMSEventLog_Model model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public SqlCommand UpdateCommand(Common.Model.LMMSEventLog_Model model)
        {
            return dal.UpdateCommand(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public SqlCommand DeleteCommand(int id)
        {

            return dal.DeleteCommand(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public SqlCommand DeleteAllCommand()
        {

            return dal.DeleteAllCommand();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Model.LMMSEventLog_Model GetModel(int id)
        {

            return dal.GetModel(id);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Model.LMMSEventLog_Model> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Model.LMMSEventLog_Model> DataTableToList(DataTable dt)
        {
            List<Common.Model.LMMSEventLog_Model> modelList = new List<Common.Model.LMMSEventLog_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Model.LMMSEventLog_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Model.LMMSEventLog_Model();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    model.currentOperation = dt.Rows[n]["currentOperation"].ToString();
                    model.ownerID = dt.Rows[n]["ownerID"].ToString();
                    model.eventTrigger = dt.Rows[n]["eventTrigger"].ToString();
                    if (dt.Rows[n]["startTime"].ToString() != "")
                    {
                        model.startTime = DateTime.Parse(dt.Rows[n]["startTime"].ToString());
                    }
                    if (dt.Rows[n]["stopTime"].ToString() != "")
                    {
                        model.stopTime = DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
                    }
                    model.ipSetting = dt.Rows[n]["ipSetting"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public Common.Model.LMMSEventLog_Model CopyObj(Common.Model.LMMSEventLog_Model objModel)
        {
            Common.Model.LMMSEventLog_Model model;
            model = new Common.Model.LMMSEventLog_Model();
            model.id = objModel.id;
            model.dateTime = objModel.dateTime;
            model.machineID = objModel.machineID;
            model.currentOperation = objModel.currentOperation;
            model.ownerID = objModel.ownerID;
            model.eventTrigger = objModel.eventTrigger;
            model.startTime = objModel.startTime;
            model.stopTime = objModel.stopTime;
            model.ipSetting = objModel.ipSetting;
            return model;
        }


        #endregion  Method









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


        public DataTable GetTodayList()
        {
            DataSet ds = dal.GetTodayList();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public Dictionary<DateTime, StaticRes.Global.StatusType> getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo, string shift, string sDateNotIn, bool bExceptWeekend)
        {
            DataSet ds = new DataSet();
            ds = dal.getOEE(dTimeFrom.AddDays(-1), dTimeTo, sMachineNo);
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

                        if (dr["currentOperation"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstCategory.Technician)
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
                        if (dr["currentOperation"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstCategory.Sysem)
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


    




        public StaticRes.Global.StatusType getCurrentStatus(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)
        {
            StaticRes.Global.StatusType eCurrentStatus = StaticRes.Global.StatusType.ShutDown;
            DataSet ds = new DataSet();
            ds = dal.getCurrentStatus(dTimeFrom, dTimeTo, sMachineNo, sPartNo);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:	getCurrentStatus" + "TableName:LMMSEventLog", " [1.X] [eCurrentStatus=" + eCurrentStatus.ToString() + "]");
                return eCurrentStatus; ;
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //ds.Tables[0];
                //SELECT id   
                //, dateTime  
                //, machineID  
                //, currentOperation 
                //, ownerID  
                //, eventTrigger  
                //, startTime  
                //, stopTime   
                //, ipSetting   
                //FROM[LMMSEventLog  

                //add OEE status , except power on & off 

                try
                {
                    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:	getCurrentStatus" + "TableName:LMMSEventLog", " [2.0] [ID=" + dr["ID"].ToString() + "][currentOperation=" + dr["currentOperation"].ToString().Trim() + "][eventTrigger=" + dr["eventTrigger"].ToString().Trim() + "]");

                    if (dr["currentOperation"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstCategory.Technician)
                    {
                        if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Adjustment
                          || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Others
                          || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Teaching
                          || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.UnderPm
                          || dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.UnderSetup)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Adjustment;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Buyoff)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Buyoff;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Setup)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Setup;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Maintainence)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Maintance;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.NoSchdule)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.NoWIP;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.Testing)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Testing;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.BreakDown)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.BreakDown;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOff)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.ShutDown;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOn)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Adjustment;
                        }
                        else
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.Adjustment;
                        }
                    }
                    else if (dr["currentOperation"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstCategory.Sysem)
                    {

                        if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOff)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.ShutDown;
                        }
                        else if (dr["eventTrigger"].ToString().Trim() == StaticRes.Global.clsConstValue.ConstStatus.PowerOn)
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.PD;
                        }
                        else
                        {
                            eCurrentStatus = StaticRes.Global.StatusType.PD;
                        }

                    }

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:	getCurrentStatus" + "TableName:LMMSEventLog", " ID=" + dr["ID"].ToString() + " -- " + ex.ToString());

                }

                DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSEventLog_BLL", "Function:	getCurrentStatus" + "TableName:LMMSEventLog", " [9.9] [eCurrentStatus=" + eCurrentStatus.ToString() + "]");

                return eCurrentStatus;
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
            dt = dal.GetTimeByStatus(dDateFrom, dDateTo, sMachineID, sStatus);
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




        private List<string> GetMachineStatusList()
        {
            List<string> list = new List<string>();
            list.Add("RUNNING");
            list.Add("BUYOFF");
            list.Add("SETUP");
            list.Add("NO SCHEDULE");
            list.Add("TESTING");
            list.Add("MAINTAINENCE");
            list.Add("BREAKDOWN");
            list.Add("POWER OFF");
            return list;
        }


        //获取 包含year, month, day, shift等信息的集合
        public List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> GetStatusModelList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sStatus, string sShift, bool bExceptWeekend)
        {
            //源数据   前延2天,后延1天, 防止漏查.
            DataTable dtEvent = dal.GetList(dDateFrom.AddDays(-2), dDateTo.AddDays(1), sMachineID, sStatus);
            if (dtEvent == null || dtEvent.Rows.Count == 0)
            {
                return null;
            }

            #region foreach dtEvent,  按照year, month, daily, shift保存到 allStatusModels中.
            List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> allStatusModels = new List<LMMSEventLog_Model.EventLogModelForChart>();
            foreach (DataRow dr in dtEvent.Rows)
            {
                Common.Model.LMMSEventLog_Model.EventLogModelForChart model;
                string machineID = dr["machineID"].ToString();
                string OEEtype = dr["currentOperation"].ToString();
                string status = dr["eventTrigger"].ToString();
                DateTime startTime = DateTime.Parse(dr["startTime"].ToString());
                DateTime stopTime = DateTime.Parse(dr["stopTime"].ToString());




                //4种情况
                //1. 在8-8点的时间段内
                //2. 跨8:00 或 20:00 一个时间段
                //3. 跨8:00 和 20:00 二个时间段
                //4. 跨三个时间段 (目前只发现,机器停一天, pingApp产生的).
                //不考虑startTime, stopTime超过1天的数据, 属于异常数据, 
                //早期client的一个bug导致的.  好像是scan状态后重启Client导致生成2条相同状态的eventlog, 但再次scan停止时, 只有一条记录会被更新, 而另一条的stoptime会一直被pingapp不停刷新. 


                //超过一天跳过, 
                if ((stopTime - startTime).TotalDays > 1)
                    continue;


                DateTime dDate = startTime.Date;
                if (startTime >= dDate.AddHours(8) && stopTime >= dDate.AddHours(8) && startTime < dDate.AddHours(20) && stopTime < dDate.AddHours(20))
                {
                    #region startTime, stopTime在8-20之间
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;

                    allStatusModels.Add(model);
                    #endregion
                }
                else if (startTime >= dDate.AddHours(20) && stopTime >= dDate.AddHours(20) && startTime < dDate.AddHours(32) && stopTime < dDate.AddHours(32))
                {
                    #region starttime, stoptime 在20 - 隔天8点 之间
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;

                    allStatusModels.Add(model);
                    #endregion
                }
                else if ((startTime < dDate.AddHours(8) && stopTime > dDate.AddHours(8))  //跨 8:00 这个点
                            &&
                           (startTime < dDate.AddHours(20) && stopTime > dDate.AddHours(20)))//跨20:00 这个点
                {
                    #region 周日0:00 - 隔天0;00  跨3个时间段,  0;00-8:00,  8:00-20:00,   20:00-隔天0:00

                    //第一部分 startTime - 8:00,  属于前一天night
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.AddDays(-1).Year;
                    model.month = dDate.AddDays(-1).Month;
                    model.day = dDate.AddDays(-1);
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(8).AddSeconds(-1);
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);

                    //第二部分 8:00 - 20:00,   属于当天day
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(8);
                    model.stopTime = dDate.AddHours(20).AddSeconds(-1);
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);

                    //disanbufen 20:00 - stoptime,  属于当天night
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(20);
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }
                else if (startTime < dDate.AddHours(8) && stopTime > dDate.AddHours(8))
                {
                    #region starttime, stoptime 跨 8:00am 这个点

                    //拆分2条记录

                    //前半部分 属于前一天晚班
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.AddDays(-1).Year;
                    model.month = dDate.AddDays(-1).Month;
                    model.day = dDate.AddDays(-1);
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(8).AddSeconds(-1);//统一包头, 不包尾.  8:00, 20:00这2个时刻分别属于day, night. 
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);


                    //后半部分, 属于当天白班
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(8);//统一包头, 不包尾.  8:00, 20:00这2个时刻分别属于day, night. 
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }
                else if (startTime < dDate.AddHours(20) && stopTime > dDate.AddHours(20))
                {
                    #region starttime, stoptime 跨 20:00这个dian

                    //拆分2条记录

                    //前半部分, 属于当天早班
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = startTime;
                    model.stopTime = dDate.AddHours(20).AddSeconds(-1);//统一包头, 不包尾.  8:00, 20:00这2个时刻分别属于day, night. 
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);


                    //后半部分, 属于当天晚班
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dDate.Year;
                    model.month = dDate.Month;
                    model.day = dDate;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = machineID;
                    model.status = status;
                    model.startTime = dDate.AddHours(20);//统一包头, 不包尾.  8:00, 20:00这2个时刻分别属于day, night. 
                    model.stopTime = stopTime;
                    model.totalSeconds = (model.stopTime - model.startTime).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }
            }
            #endregion



            #region 补齐所有天数
            List<string> statusList = GetMachineStatusList();

          
            DateTime dTemp = dDateFrom.Date;
            // 循环每一天
            while (dTemp < dDateTo)
            {

                //循环每台机器
                for (int i = 1; i < 9; i++)
                {

                    //如果选定机器, 只有循环到该机器才能继续添加.
                    if (sMachineID != "")
                    {
                        if (i.ToString() != sMachineID)
                            continue;
                    }



                    //循环每个状态
                    foreach (string status in statusList)
                    {
                        //running状态除外, 在后续补充
                        if (status == "RUNNING")
                            continue;


                        //如果选定状态, 只添加该状态. (running除外)
                        if (sStatus != "" && sStatus != "RUNNING")
                        {
                            if (status != sStatus)
                                continue;
                        }



                        //补充 day数据
                        var buyoffDayExist = from a in allStatusModels
                                             where a.day == dTemp && a.shift == "Day" && a.machineID == i.ToString() && a.status == status
                                             select a;
                        if (buyoffDayExist == null || buyoffDayExist.Count() == 0)
                        {
                            LMMSEventLog_Model.EventLogModelForChart model = new LMMSEventLog_Model.EventLogModelForChart();
                            model.year = dTemp.Year;
                            model.month = dTemp.Month;
                            model.day = dTemp;
                            model.shift = StaticRes.Global.Shift.Day;
                            model.machineID = i.ToString();
                            model.status = status;
                            model.totalSeconds = 0;

                            allStatusModels.Add(model);
                        }


                        //补充 night数据
                        var buyoffNightExist = from a in allStatusModels
                                               where a.day == dTemp && a.shift == "Night" && a.machineID == i.ToString() && a.status == status
                                               select a;
                        if (buyoffNightExist == null || buyoffNightExist.Count() == 0)
                        {
                            LMMSEventLog_Model.EventLogModelForChart model = new LMMSEventLog_Model.EventLogModelForChart();
                            model.year = dTemp.Year;
                            model.month = dTemp.Month;
                            model.day = dTemp;
                            model.shift = StaticRes.Global.Shift.Night;
                            model.machineID = i.ToString();
                            model.status = status;
                            model.totalSeconds = 0;

                            allStatusModels.Add(model);
                        }

                    }
                }
              

                dTemp = dTemp.AddDays(1);
            }
            #endregion



            #region 只有选择running或utilization或空的情况下, 才补充running状态时间,  减少运算量
            if (sStatus == "" || sStatus == "RUNNING" || sStatus == "UTILIZATION")
            {
                
                DateTime currentDay = DateTime.Now.AddHours(-8).Date;
                string currentShift = DateTime.Now >= DateTime.Now.Date.AddHours(8) && DateTime.Now < DateTime.Now.Date.AddHours(20) ? "Day" : "Night";

                
                //当天 day/night 班次总时长,   (当天按照 8/20到现在的时长计算)
                double currentDayShiftTotalSeconds = 0;
                double currentNightShiftTotalSeconds = 0;
                if (currentShift == StaticRes.Global.Shift.Day)
                {
                    currentDayShiftTotalSeconds = (DateTime.Now - currentDay.AddHours(8)).TotalSeconds; //8点到现在的总时间
                    currentNightShiftTotalSeconds = 0;
                }
                else
                {
                    currentDayShiftTotalSeconds = 12 * 3600;
                    currentNightShiftTotalSeconds = (DateTime.Now - currentDay.AddHours(20)).TotalSeconds;//20:00到现在的总时间
                }

                //正常班次总时长.
                double shiftTotalSeconds = 12 * 3600;


                //循环每一天
                dTemp = dDateFrom.Date;
                while (dTemp < dDateTo)
                {

                    //循环每台机器
                    for (int i = 1; i < 9; i++)
                    {

                        //如果选定机器, 只有循环到该机器才能继续添加.
                        if (sMachineID != "")
                        {
                            if (i.ToString() != sMachineID)
                                continue;
                        }
                        

                        #region 补充 Day shift 的 running状态时长.
                        var dayShiftResult = from a in allStatusModels
                                             where a.day == dTemp && a.machineID == i.ToString() && a.shift == "Day"
                                             group a by a.day into dayShift
                                             select new
                                             {
                                                 dayShift.Key,
                                                 //running totall time =  总时间 12 * 3600(每班次)  - (其它状态所有时间)
                                                 //如果是当天, 就用当天的 8点到现在的时长, 否则就 12*3600
                                                 RunningSeconds = (dayShift.Key == currentDay ? currentDayShiftTotalSeconds : shiftTotalSeconds) - dayShift.Sum(p => p.totalSeconds)
                                             };
                        Common.Model.LMMSEventLog_Model.EventLogModelForChart dayModel = new LMMSEventLog_Model.EventLogModelForChart();
                        dayModel.year = dTemp.Year;
                        dayModel.month = dTemp.Month;
                        dayModel.day = dTemp;
                        dayModel.shift = StaticRes.Global.Shift.Day;
                        dayModel.machineID = i.ToString();
                        dayModel.status = "RUNNING";
                        dayModel.totalSeconds = dayShiftResult.Count() == 0 ? 0 : dayShiftResult.FirstOrDefault().RunningSeconds;

                        allStatusModels.Add(dayModel);

                        #endregion

                        #region 补充 Night shift 的 running状态时长.
                        var nightShiftResult = from a in allStatusModels
                                               where a.day == dTemp && a.machineID == i.ToString() && a.shift == "Night"
                                               group a by a.day into nightShift
                                               select new
                                               {
                                                   nightShift.Key,
                                                   //running totall time =  总时间 12 * 3600(每班次)  - (其它状态所有时间)
                                                   //如果是当天, 就用当天的 20点到现在的时长, 否则就 12*3600
                                                   RunningSeconds = (nightShift.Key == currentDay ? currentNightShiftTotalSeconds : shiftTotalSeconds) - nightShift.Sum(p => p.totalSeconds)
                                               };
                        Common.Model.LMMSEventLog_Model.EventLogModelForChart nightModel = new LMMSEventLog_Model.EventLogModelForChart();
                        nightModel.year = dTemp.Year;
                        nightModel.month = dTemp.Month;
                        nightModel.day = dTemp;
                        nightModel.shift = StaticRes.Global.Shift.Night;
                        nightModel.machineID = i.ToString();
                        nightModel.status = "RUNNING";
                        nightModel.totalSeconds = nightShiftResult.Count() == 0 ? 0 : nightShiftResult.FirstOrDefault().RunningSeconds;

                        allStatusModels.Add(nightModel);
                        #endregion
                    }

                    dTemp = dTemp.AddDays(1);
                }
            }



            #endregion




            string[] shiftAll;
            string[] statusAll;
            #region 删选 status, shift
            if (sStatus == "UTILIZATION" || sStatus == "RUNNING")
                statusAll = new string[] { "RUNNING" };

            else if (sStatus == "BUYOFF")
                statusAll = new string[] { "BUYOFF" };

            else if (sStatus == "SETUP")
                statusAll = new string[] { "SETUP" };

            else if (sStatus == "NO SCHEDULE")
                statusAll = new string[] { "NO SCHEDULE" };

            else if (sStatus == "TESTING")
                statusAll = new string[] { "TESTING" };

            else if (sStatus == "MAINTAINENCE")
                statusAll = new string[] { "MAINTAINENCE" };

            else if (sStatus == "BREAKDOWN")
                statusAll = new string[] { "BREAKDOWN" };

            else if (sStatus == "POWER OFF")
                statusAll = new string[] { "POWER OFF" };

            else
                statusAll = new string[] { "RUNNING", "BUYOFF", "SETUP", "NO SCHEDULE", "TESTING", "MAINTAINENCE", "BREAKDOWN", "POWER OFF" };


            if (sShift == "Day")
                shiftAll = new string[] { "Day" };

            else if (sShift == "Night")
                shiftAll = new string[] { "Night" };

            else
                shiftAll = new string[] { "Day", "Night" };
            #endregion

            

            var result = from a in allStatusModels
                         where 
                         a.day >= dDateFrom
                         && a.day < dDateTo//选定时间段

                         && statusAll.Contains(a.status)//选定状态
                         && shiftAll.Contains(a.shift)//选定班次
                         && (bExceptWeekend ? a.day.DayOfWeek != DayOfWeek.Sunday && a.day.DayOfWeek != DayOfWeek.Saturday : true)//周末除外
                         select a;

            
            return result.ToList();
        }



    }
}
