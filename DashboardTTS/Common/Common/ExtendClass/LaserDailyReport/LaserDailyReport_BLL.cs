using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserDailyReport
{
  
    public class LaserDailyReport_BLL
    {
        private readonly LaserDailyReport_DAL _dal = new LaserDailyReport_DAL();
        private readonly Common.BLL.LMMSEventLog_BLL _eventBLL = new BLL.LMMSEventLog_BLL();



        public List<LaserDailyReport_Model.Main> GetMainList(DateTime dDay)
        {
            List<LaserDailyReport_Model.DetailOutput> detailOutputList = _dal.GetList(dDay);
            List<Common.Model.LMMSEventLog_Model.Detail> statusList = _eventBLL.GetStatusModelList(dDay, dDay.AddDays(1), "", "", "", false);

            if (detailOutputList == null || statusList == null)
                return null;


            var outputGrouped = from a in detailOutputList
                                orderby a.MachineID ascending
                                group a by a.MachineID into b
                                select new
                                {
                                    MachineID = b.Key,
                                    PassQty = b.Sum(p => p.PassQty),
                                    RejQty = b.Sum(p => p.RejQty),
                                    Setup = b.Sum(p => p.Setup),
                                    Buyoff = b.Sum(p => p.Buyoff),
                                    Output = b.Sum(p => p.Output)
                                };

            var runStatusGrouped = from a in statusList
                                   where a.status == StaticRes.Global.LaserStatus.Run
                                   || a.status == StaticRes.Global.LaserStatus.Setup
                                   || a.status == StaticRes.Global.LaserStatus.Buyoff
                                   || a.status == StaticRes.Global.LaserStatus.Testing
                                   group a by a.machineID into b
                                   select new
                                   {
                                       MachineID = b.Key,
                                       TotalSeconds = b.Sum(p => p.totalSeconds)
                                   };
            var idleStatusGrouped = from a in statusList
                                   where a.status == StaticRes.Global.LaserStatus.NoSchedule
                                   || a.status == StaticRes.Global.LaserStatus.Maintenance
                                   group a by a.machineID into b
                                   select new
                                   {
                                       MachineID = b.Key,
                                       TotalSeconds = b.Sum(p => p.totalSeconds)
                                   };
            var breakdownStatusGrouped = from a in statusList
                                   where a.status == StaticRes.Global.LaserStatus.Breakdown
                                   group a by a.machineID into b
                                   select new
                                   {
                                       MachineID = b.Key,
                                       TotalSeconds = b.Sum(p => p.totalSeconds)
                                   };
            var shutdownStatusGrouped = from a in statusList
                                   where a.status == StaticRes.Global.LaserStatus.Shutdown
                                   group a by a.machineID into b
                                   select new
                                   {
                                       MachineID = b.Key,
                                       TotalSeconds = b.Sum(p => p.totalSeconds)
                                   };


            List<LaserDailyReport_Model.Main> mainList = new List<LaserDailyReport_Model.Main>();
            for (int i = 1; i < 9; i++)
            {
                var output = (from a in outputGrouped where a.MachineID == i.ToString() select a).FirstOrDefault();
                var run = (from a in runStatusGrouped where a.MachineID == i.ToString() select a).FirstOrDefault();
                var idle = (from a in idleStatusGrouped where a.MachineID == i.ToString() select a).FirstOrDefault();
                var breakdown = (from a in breakdownStatusGrouped where a.MachineID == i.ToString() select a).FirstOrDefault();
                var shutdown = (from a in shutdownStatusGrouped where a.MachineID == i.ToString() select a).FirstOrDefault();




                LaserDailyReport_Model.Main model = new LaserDailyReport_Model.Main();
                model.MachineID = i.ToString();
                model.Run = Common.CommFunctions.ConvertDateTimeShort((run.TotalSeconds / 3600).ToString());
                model.Idle = Common.CommFunctions.ConvertDateTimeShort((idle.TotalSeconds / 3600).ToString());
                model.Breakdown = Common.CommFunctions.ConvertDateTimeShort((breakdown.TotalSeconds / 3600).ToString());
                model.Shutdown = Common.CommFunctions.ConvertDateTimeShort((shutdown.TotalSeconds / 3600).ToString());

                model.PassQty = output.PassQty;
                model.RejQty = output.RejQty;
                model.Setup = output.Setup;
                model.Buyoff = output.Buyoff;
                model.Output = output.Output;

                mainList.Add(model);
            }

            return mainList;
        }


        public List<LaserDailyReport_Model.DetailStatus> GetDetailStatusList(DateTime dDay,string sShift, string sMachineID)
        {
            List<Common.Model.LMMSEventLog_Model.Detail> statusList = _eventBLL.GetStatusModelList(dDay, dDay.AddDays(1), sMachineID, "", sShift, false);
            if (statusList == null)
                return null;


            List<LaserDailyReport_Model.DetailStatus> detailStatusList = new List<LaserDailyReport_Model.DetailStatus>();
            foreach (var item in statusList)
            {
                if (item.totalSeconds == 0)
                    continue;

                LaserDailyReport_Model.DetailStatus model = new LaserDailyReport_Model.DetailStatus();
                model.MachineID = item.machineID;
                model.Shift = item.shift;
                model.StartTime = item.startTime == null? "": item.startTime.Value.ToString("HH:mm");
                model.StopTime = item.stopTime == null ? "" :item.stopTime.Value.ToString("HH:mm");
                model.TakeTime = Common.CommFunctions.ConvertDateTimeShort((item.totalSeconds / 3600).ToString());
                model.Status = item.status;

                detailStatusList.Add(model);
            }



            LaserDailyReport_Model.DetailStatus total = new LaserDailyReport_Model.DetailStatus();
            double totalHours = detailStatusList.Sum(p => Common.CommFunctions.ConvertDateTimeToDouble(p.TakeTime));
            total.TakeTime = Common.CommFunctions.ConvertDateTimeShort(totalHours.ToString());

            detailStatusList.Add(total);




            return detailStatusList;
        }

        public List<LaserDailyReport_Model.DetailOutput> GetDetailOutputList(DateTime dDay,string sShift, string sMachineID)
        {
            var list = _dal.GetList(dDay);
            if (list == null)
                return null;


            string[] arrShift = sShift == "" ? new string[] { "Day", "Night" } : new string[] { sShift };

            var result = (from a in list
                          where a.MachineID == sMachineID && arrShift.Contains(a.Shift)
                          orderby a.StartTime ascending
                          select a).ToList();

            LaserDailyReport_Model.DetailOutput total = new LaserDailyReport_Model.DetailOutput();
            double totalSeconds = result.Sum(p => Common.CommFunctions.ConvertDateTimeToDouble(p.TakeTime));
            total.TakeTime = Common.CommFunctions.ConvertDateTimeShort(totalSeconds.ToString());
            total.PassQty = result.Sum(p => p.PassQty);
            total.RejQty = result.Sum(p => p.RejQty);
            total.Setup = result.Sum(p => p.Setup);
            total.Buyoff = result.Sum(p => p.Buyoff);
            total.Output = result.Sum(p => p.Output);


            result.Add(total);


            return result;
        }

    }
}
