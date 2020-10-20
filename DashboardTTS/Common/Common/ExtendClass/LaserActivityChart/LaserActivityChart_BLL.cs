using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserActivityChart
{    
    public class LaserActivityChart_BLL
    {
        private readonly Common.BLL.LMMSEventLog_BLL _eventBLL = new BLL.LMMSEventLog_BLL();



        public List<LaserActivityChart_Model> GetDataList(Common.SearchingCondition.LaserActivityCondition condition)
        {
            List<Common.Model.LMMSEventLog_Model.Detail> baseList = _eventBLL.GetStatusModelList(condition.DateFrom.Value, condition.DateTo.Value, "", "", condition.Shift, false);
            if (baseList == null)
                return null;


            var machineGrouped = from a in baseList
                                 group a by new { a.machineID, a.status } into b
                                 select new
                                 {
                                     MachineID = b.Key.machineID,
                                     Status = b.Key.status,
                                     TotalSeconds = b.Sum(p => p.totalSeconds)

                                 };

            List<LaserActivityChart_Model> modelList = new List<LaserActivityChart_Model>();

            foreach (var item in machineGrouped)
            {
                modelList.Add(new LaserActivityChart_Model()
                {
                    MachineID = item.MachineID,
                    Status = item.Status,
                    TotalSeconds = item.TotalSeconds
                });
            }


            return modelList;
        }

    }
}
