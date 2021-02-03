using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;
using Taiyo.Enum.Production;
using Newtonsoft.Json;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.PQCProduction.OperatorSummaryChart
{
    public class BLL
    {
        private readonly PQCProduction.Core.Base_BLL _bll;
        public BLL()
        {
            _bll = new Core.Base_BLL();
        }
        


        public List<Model> GetDailyList(PQCOperatorParam param)
        {
            var viList = _bll.GetViList(param);
            if (viList == null)
                return null;

            var result = from a in viList
                         orderby a.Day ascending
                         group a by a.Day into dayList
                         select new
                         {
                             AxisXLabelName = string.Format("{0}/{1}", dayList.Key.GetMonthName(false), dayList.Key.Day),
                             Day = dayList.Key.Day,
                             Month = dayList.Key.Month,
                             Year = dayList.Key.Year,

                             LaserQty = dayList.Sum(p => p.ProductType == PQCReportType.Laser ? p.TotalQty : 0),
                             WIPQty = dayList.Sum(p => p.ProductType == PQCReportType.WIP ? p.TotalQty : 0),
                             PackOnlineQty = dayList.Sum(p => p.ProductType == PQCReportType.PackOnline ? p.TotalQty : 0),
                             PackOfflineQty = dayList.Sum(p => p.ProductType == PQCReportType.PackOffline ? p.TotalQty : 0)
                         };

            //将Ienumerable类型通过json转换成opsummarychart的list<model>类型.
            string strJson = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<List<Model>>(strJson);
        }

        public List<Model> GetMonthlyList(PQCOperatorParam param)
        {
            var viList = _bll.GetViList(param);
            if (viList == null)
                return null;

            var result = from a in viList
                         orderby a.Day ascending
                         group a by new { a.Day.Year, a.Day.Month } into monthList
                         select new
                         {
                             AxisXLabelName = (new DateTime(monthList.Key.Year, monthList.Key.Month, 1)).GetMonthName(false),
                             Month = monthList.Key.Year,
                             Year = monthList.Key.Month,

                             LaserQty = monthList.Sum(p => p.ProductType == PQCReportType.Laser ? p.TotalQty : 0),
                             WIPQty = monthList.Sum(p => p.ProductType == PQCReportType.WIP ? p.TotalQty : 0),
                             PackOnlineQty = monthList.Sum(p => p.ProductType == PQCReportType.PackOnline ? p.TotalQty : 0),
                             PackOfflineQty = monthList.Sum(p => p.ProductType == PQCReportType.PackOffline ? p.TotalQty : 0)
                         };

         

            string strJson = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<List<Model>>(strJson);
        }

        public List<Model> GetYearlyList(PQCOperatorParam param)
        {
            var viList = _bll.GetViList(param);
            if (viList == null)
                return null;

            var result = from a in viList
                         group a by a.Day.Year into yearList
                         select new
                         {
                             AxisXLabelName = yearList.Key,
                             Year = yearList.Key,

                             LaserQty = yearList.Sum(p => p.ProductType == PQCReportType.Laser ? p.TotalQty : 0),
                             WIPQty = yearList.Sum(p => p.ProductType == PQCReportType.WIP ? p.TotalQty : 0),
                             PackOnlineQty = yearList.Sum(p => p.ProductType == PQCReportType.PackOnline ? p.TotalQty : 0),
                             PackOfflineQty = yearList.Sum(p => p.ProductType == PQCReportType.PackOffline ? p.TotalQty : 0)
                         };

            string strJson = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<List<Model>>(strJson);
        }





    }
}
