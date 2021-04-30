using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;

namespace Common.ExtendClass.PQCProduction.CheckingLiveReport
{
    public class BLL
    {
        private readonly PQCProduction.Core.Base_BLL _bll;
        public BLL()
        {
            _bll = new Core.Base_BLL();
        }
        

        public List<Model> GetReportList(PQCOutputParam param, string lotNo)
        {
            var checkingList = _bll.GetCheckingList(param);
            if (checkingList == null || checkingList.Count == 0)
                return null;

            // 2021-4-27, 部分 part 有 paint#1,#2, 会导致同一个 job 生成2条记录, 只是显示 lotno, 所有只需要获取都有的 Paint#1的记录  
            var lotInfoList = _bll.GetLotInfoList(param).Where(p => p.PaintProcess == "Paint#1");

            //后重写的checking live report,
            //为了不改core base_dal,  
            //PartNo MachineID JobNo LotNo 这几个查询在代码中实现.
            var temp = from a in checkingList
                       orderby a.StartTime descending
                       where (string.IsNullOrEmpty(param.PartNo) ? true : a.PartNo == param.PartNo)
                       && (string.IsNullOrEmpty(param.MachineID) ? true : a.MachineID == param.MachineID)
                       && (string.IsNullOrEmpty(param.JobNo) ? true : a.JobNo == param.JobNo)
                       //&& a.TotalQty > 0
                       join b in lotInfoList on a.JobNo equals b.JobNo
                       where (string.IsNullOrEmpty(lotNo) ? true : b.LotNo == lotNo)
                       select new
                       {
                           Date = $"{a.Day.ToString("dd/MM/yyyy") }-{a.Shift}",
                           TrackingID = a.TrackingID,
                           Station = "Station" + a.MachineID,
                           PartNo = a.PartNo,
                           JobNo = a.JobNo,
                           LotNo = b.LotNo,
                           Processes = a.Processes,
                           IsComplete = a.NextViFlag,
                           StartTime = a.StartTime.ToString("HH:mm:ss"),
                           EndTime = a.EndTime == null ? "" : a.EndTime.Value.ToString("HH:mm:ss"),
                           CostTime = a.EndTime == null ? "" : CommFunctions.ConvertDateTimeShort(((a.EndTime.Value - a.StartTime).TotalSeconds / 3600).ToString()),
                           Status = a.Status,
                           OKQty = a.PassQty,
                           NGQty = a.RejQty,
                           Output = a.TotalQty,
                           RejRate = a.TotalQty ==0 ? "0.00%" : Math.Round(a.RejQty / a.TotalQty * 100, 2).ToString("0.00") + "%",
                           Operator = a.Opertor
                       };


            var strObj =   Newtonsoft.Json.JsonConvert.SerializeObject(temp);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model>>(strObj);

            return result;
        }




    }
}
