using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;
using Taiyo.Enum.Production;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Common.ExtendClass.PQCProduction.OperatorPerformanceChart
{
    public class BLL
    {
        private readonly PQCProduction.Core.Base_BLL _bll;
        public BLL()
        {
            _bll = new Core.Base_BLL();
        }


        public List<Model> GetChartData(PQCOutputParam param)
        {
            var viList = _bll.GetViList(param);
            if (viList == null)
                return null;

            var temp = from a in viList
                       where a.Opertor != "" && a.Opertor.ToUpper() != "LASER" && a.Opertor.ToUpper() != "ADMIN"
                       group a by a.Opertor.ToUpper() into opList
                       select new
                       {
                           UserID = opList.Key,
                           LaserQty = opList.Sum(p => p.ProductType == PQCReportType.Laser ? p.TotalQty : 0),
                           WIPQty = opList.Sum(p => p.ProductType == PQCReportType.WIP ? p.TotalQty : 0),
                           PackOnlineQty = opList.Sum(p => p.ProductType == PQCReportType.PackOnline ? p.TotalQty : 0),
                           PackOfflineQty = opList.Sum(p => p.ProductType == PQCReportType.PackOffline ? p.TotalQty : 0)
                       };

            string strJson = JsonConvert.SerializeObject(temp);
            var result = JsonConvert.DeserializeObject<List<Model>>(strJson);

            //根据userid中的 数字排序.
            return result.OrderBy(p => int.Parse(Regex.Replace(p.UserID, @"[^0-9]+", ""))).ToList();
        }


    }
}
