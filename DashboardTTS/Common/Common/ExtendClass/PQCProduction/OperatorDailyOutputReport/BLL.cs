using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;
using Taiyo.Enum.Production;
using Newtonsoft.Json;

namespace Common.ExtendClass.PQCProduction.OperatorDailyOutputReport
{
    public class BLL
    {
        private readonly PQCProduction.Core.Base_BLL _bll;
        public BLL()
        {
            _bll = new Core.Base_BLL();
        }
        

        public List<Model> GetReportList(PQCOperatorParam param)
        {
            var viList = _bll.GetViList(param);
            if (viList == null || viList.Count== 0)
                return null;

            var defectList = _bll.GetDefectList(param);
            var lotInfoList = _bll.GetLotInfoList(param);

           
            var temp = from a in viList
                       where a.EndTime != null && a.TotalQty != 0
                       join b in defectList on a.TrackingID equals b.TrackingID
                       into temp1
                       join c in lotInfoList on a.JobNo equals c.JobNo
                       into temp2 orderby a.StartTime ascending
                       select new
                       {
                           StartTime = a.StartTime.ToString("HH:mm:ss"),
                           EndTime = a.EndTime.Value.ToString("HH:mm:ss"),
                           OperatedTime = Common.CommFunctions.ConvertDateTimeShort(((a.EndTime.Value - a.StartTime).TotalSeconds / 3600).ToString()),
                           PartNo = a.PartNo,
                           LotNo = temp2.Select(t => t.LotNo).FirstOrDefault(),
                           CheckOnlineQty = a.ProductType == PQCReportType.Laser ? a.TotalQty : 0,
                           CheckWIPQty = a.ProductType == PQCReportType.WIP ? a.TotalQty : 0,
                           PackOnlineQty = a.ProductType == PQCReportType.PackOnline ? a.TotalQty : 0,
                           PackOfflineQty = a.ProductType == PQCReportType.PackOffline ? a.TotalQty : 0,
                           LotQty = temp2.Select(t => t.LotQty).FirstOrDefault(),
                           MouldRej = $"{ temp1.Select(t => t.MouldRej).FirstOrDefault()}({Math.Round(temp1.Select(t => t.MouldRej).FirstOrDefault() / a.TotalQty * 100, 2).ToString("0.00")}%)",
                           PaintRej = $"{temp1.Select(t => t.PaintRej).FirstOrDefault()}({Math.Round(temp1.Select(t => t.PaintRej).FirstOrDefault() / a.TotalQty * 100, 2).ToString("0.00")}%)",
                           LaserRej = $"{temp1.Select(t => t.LaserRej).FirstOrDefault()}({Math.Round(temp1.Select(t => t.LaserRej).FirstOrDefault() / a.TotalQty * 100, 2).ToString("0.00")}%)",
                           OthersRej = $"{temp1.Select(t => t.OthersRej).FirstOrDefault()}({Math.Round(temp1.Select(t => t.OthersRej).FirstOrDefault() / a.TotalQty * 100, 2).ToString("0.00")}%)",
                           TotalRej = $"{a.RejQty}({Math.Round(a.RejQty / a.TotalQty * 100, 2).ToString("0.00")}%)",
                           PassQty = a.PassQty,
                           LoseAmounts = a.LoseAmounts,
                           Operator = a.Opertor.ToUpper(),
                           TotalQty = a.TotalQty,
                       };


            #region summary row
            var summary = new Model();
            summary.StartTime = "Total";
            summary.EndTime = "";

            double totalSec = temp.Sum(p => Common.CommFunctions.ConvertDateTimeToDouble(p.OperatedTime));
            summary.OperatedTime = Common.CommFunctions.ConvertDateTimeShort((totalSec / 3600).ToString());

            summary.CheckOnlineQty = temp.Sum(p => p.CheckOnlineQty);
            summary.CheckWIPQty = temp.Sum(p => p.CheckWIPQty);
            summary.PackOnlineQty = temp.Sum(p => p.PackOnlineQty);
            summary.PackOfflineQty = temp.Sum(p => p.PackOfflineQty);


            decimal totalQty = temp.Sum(p => p.TotalQty);
            decimal mouldRej = temp.Sum(p => decimal.Parse(p.MouldRej.Split('(')[0]));
            decimal paintRej = temp.Sum(p => decimal.Parse(p.PaintRej.Split('(')[0]));
            decimal laserRej = temp.Sum(p => decimal.Parse(p.LaserRej.Split('(')[0]));
            decimal othersRej = temp.Sum(p => decimal.Parse(p.OthersRej.Split('(')[0]));
            decimal totalrej = temp.Sum(p => decimal.Parse(p.TotalRej.Split('(')[0]));

            summary.MouldRej = string.Format("{0}({1}%)", mouldRej, Math.Round(mouldRej / totalQty * 100, 2).ToString("0.00"));
            summary.PaintRej = string.Format("{0}({1}%)", paintRej, Math.Round(paintRej / totalQty * 100, 2).ToString("0.00"));
            summary.LaserRej = string.Format("{0}({1}%)", laserRej, Math.Round(laserRej / totalQty * 100, 2).ToString("0.00"));
            summary.OthersRej = string.Format("{0}({1}%)", othersRej, Math.Round(othersRej / totalQty * 100, 2).ToString("0.00"));
            summary.TotalRej = string.Format("{0}({1}%)", totalrej, Math.Round(totalrej / totalQty * 100, 2).ToString("0.00"));

            summary.LotQty = temp.Sum(p => p.LotQty);
            summary.PassQty = temp.Sum(p => p.PassQty);
            summary.LoseAmounts = temp.Sum(p => p.LoseAmounts);
            #endregion




            string strJson = JsonConvert.SerializeObject(temp);
            var result = JsonConvert.DeserializeObject<List<Model>>(strJson);
            result.Add(summary);

            return result;
        }

    }
}
