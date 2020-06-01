
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









        #region 将lmmseventlog 表中的数据按照 year, month, day , shift , machine , status , totalseconds整理 

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
        
        private List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> GetBaseList(DateTime dDateFrom, DateTime dDateTo)
        {

            //源数据   前延2天,后延1天, 防止漏查.
            DataTable dtEvent = dal.GetList(dDateFrom.AddDays(-2), dDateTo.AddDays(1), "", "");
            if (dtEvent == null || dtEvent.Rows.Count == 0)
                return null;






            Common.Model.LMMSEventLog_Model.EventLogModelForChart model;




            //foreach dtEvent
            //按照year, month, daily, shift保存到 allStatusModels中.
            List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> allStatusModels = new List<LMMSEventLog_Model.EventLogModelForChart>();
            foreach (DataRow dr in dtEvent.Rows)
            {
                
                string machineID = dr["machineID"].ToString();
                string OEEtype = dr["currentOperation"].ToString();
                string status = dr["eventTrigger"].ToString();
                DateTime startTime = DateTime.Parse(dr["startTime"].ToString());
                DateTime stopTime = DateTime.Parse(dr["stopTime"].ToString());







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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;

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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;

                    allStatusModels.Add(model);
                    #endregion
                }
                //最大范围在上.
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }
                //小范围在下
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
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
                    model.totalSeconds = (model.stopTime.Value - model.startTime.Value).TotalSeconds;
                    allStatusModels.Add(model);
                    #endregion
                }

            }






            return allStatusModels;
        }
        
        private List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> AddLackData(List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> modelList, DateTime dDateFrom, DateTime dDateTo)
        {

            Common.Model.LMMSEventLog_Model.EventLogModelForChart model;
            List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> lackModelList = new List<LMMSEventLog_Model.EventLogModelForChart>();




            #region 循环每一天, 每台机器, 每个状态的 白晚班次的机器状态信息.  没有补充,时长默认为0.
            DateTime dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1; i < 9; i++)
                {
                    List<string> statusList = GetMachineStatusList();
                    foreach (string strStatus in statusList)
                    {
                        if (strStatus == "RUNNING") continue;


                        //day
                        var dayModel = (from a in modelList
                                        where a.day == dTemp.Date
                                        && a.machineID == i.ToString()
                                        && a.status == strStatus
                                        && a.shift == StaticRes.Global.Shift.Day
                                        select a).FirstOrDefault();
                        if (dayModel == null)
                        {
                            model = new LMMSEventLog_Model.EventLogModelForChart();
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


                        //night
                        var nightModel = (from a in modelList
                                          where a.day == dTemp.Date
                                          && a.machineID == i.ToString()
                                          && a.status == strStatus
                                          && a.shift == StaticRes.Global.Shift.Night
                                          select a).FirstOrDefault();
                        if (nightModel == null)
                        {
                            model = new LMMSEventLog_Model.EventLogModelForChart();
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
                    }
                }
                dTemp = dTemp.AddDays(1);
            }
            #endregion





            //将缺少的数据合并到list中.
            modelList.AddRange(lackModelList);





            #region power off, 查询该机器, 该天总时长是否为0, 是则将power off赋值 12*3600
            
            dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1  ; i < 9; i++)
                {

                    //查询这一天, 这台机器, 白班的所有信息.
                    var machineDayList = from a in modelList
                                  where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Day
                                  select a;


                    //如果总时长为0, 并且start/stop time为null,  则将poweroff的时长设置为12*3600;
                    double totalSecond = machineDayList.Sum(p => p.totalSeconds);
                    if (totalSecond == 0)
                    {
                        var machinePoweroffDayModel = (from a in modelList
                                                       where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Day && a.status == "POWER OFF"
                                                       select a).FirstOrDefault();
                        if (machinePoweroffDayModel.startTime == null && machinePoweroffDayModel.stopTime == null)
                        {
                            machinePoweroffDayModel.totalSeconds = 12 * 3600;
                        }
                    }





                    //查询这一天, 这台机器, 晚班的所有信息.
                    var machineNightList = from a in modelList
                                           where a.day == dTemp && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Night
                                           select a;


                    //如果总时长为0, 并且start/stop time为null,  则将poweroff的时长设置为12*3600;
                    totalSecond = machineNightList.Sum(p => p.totalSeconds);
                    if (totalSecond == 0)
                    {
                        var machinePoweroffNightModel = (from a in modelList
                                                         where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Night && a.status == "POWER OFF"
                                                         select a).FirstOrDefault();
                        if (machinePoweroffNightModel.startTime == null && machinePoweroffNightModel.stopTime == null)
                        {
                            machinePoweroffNightModel.totalSeconds = 12 * 3600;
                        }
                    }

                }

                dTemp = dTemp.AddDays(1);
            }
            #endregion






            #region 添加 running model,   = 12*3600 - 其他各状态总时长 
            
            dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                for (int i = 1; i < 9; i++)
                {

                    //查询这一天, 这台机器, 白班的所有信息.
                    var machineDayList = from a in modelList
                                         where a.day == dTemp.Date && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Day
                                         select a;
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dTemp.Year;
                    model.month = dTemp.Month;
                    model.day = dTemp;
                    model.shift = StaticRes.Global.Shift.Day;
                    model.machineID = i.ToString();
                    model.status = "RUNNING";
                    model.startTime = null;
                    model.stopTime = null;
                    model.totalSeconds = 12 * 3600 - machineDayList.Sum(p => p.totalSeconds);
                    modelList.Add(model);
                    

                    //查询这一天, 这台机器, 晚班的所有信息.
                    var machineNightList = from a in modelList
                                           where a.day == dTemp && a.machineID == i.ToString() && a.shift == StaticRes.Global.Shift.Night
                                           select a;
                    model = new LMMSEventLog_Model.EventLogModelForChart();
                    model.year = dTemp.Year;
                    model.month = dTemp.Month;
                    model.day = dTemp;
                    model.shift = StaticRes.Global.Shift.Night;
                    model.machineID = i.ToString();
                    model.status = "RUNNING";
                    model.startTime = null;
                    model.stopTime = null;
                    model.totalSeconds = 12 * 3600 - machineNightList.Sum(p => p.totalSeconds);
                    modelList.Add(model);
                }


                dTemp = dTemp.AddDays(1);
            }
            #endregion




            return modelList;
        }
        
        public List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> GetStatusModelList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sStatus, string sShift, bool bExceptWeekend)
        {

            //event log表原数据切分, 归类到list中. 
            List<Common.Model.LMMSEventLog_Model.EventLogModelForChart> baseList = GetBaseList(dDateFrom, dDateTo);
            if (baseList == null)
                return null;



            //添加缺失的天, 机器, 状态数据.
            baseList = AddLackData(baseList, dDateFrom, dDateTo);




            //按条件删选记录.   
            sStatus = sStatus == "UTILIZATION" ? "RUNNING" : sStatus;
            string[] arrStatus = string.IsNullOrEmpty(sStatus) ? new string[] { "RUNNING", "BUYOFF", "SETUP", "NO SCHEDULE", "TESTING", "MAINTAINENCE", "BREAKDOWN", "POWER OFF" } : new string[] { sStatus };
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
                          select a).ToList();




            return result;
        }
        
        #endregion


    }
}
