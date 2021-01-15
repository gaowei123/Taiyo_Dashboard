using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.LaserParam;
using Newtonsoft.Json;

namespace Common.ExtendClass.LaserProductChart
{
    public class ProductChart_BLL
    {
        private readonly ProductChart_DAL _dal;
        public ProductChart_BLL()
        {
            _dal = new ProductChart_DAL();
        }



        
        public List<ProductChart_Model.Detail> GetBaseList(LaserProductChartParam param)
        {
            var dataList = _dal.GetList(param);

            //遍历并补充缺失的天数
            DateTime dTemp = param.DateFrom.Value.Date;            
            while (dTemp < param.DateTo.Value.Date)
            {
                var result = dataList.Where(item => item.Date == dTemp);
                if (result == null || result.Count()== 0)
                {
                    dataList.Add(new ProductChart_Model.Detail()
                    {
                        Date = dTemp,
                        Year = dTemp.Year,
                        Month = dTemp.Month,
                        Day = dTemp.Day,

                        MachineID = "",
                        PartNo = "",
                        JobNo = "",

                        PassQty = 0,
                        RejQty = 0
                    });
                }

                dTemp = dTemp.AddDays(1);
            }


            return dataList;
        }
           



        public List<ProductChart_Model.Daily> GetDailyList(LaserProductChartParam param)
        {
            var list = GetBaseList(param);
            if (list == null) return null;

            var result = from a in list
                         orderby a.Date ascending
                         group a by a.Date into b
                         select new
                         {
                             Year = b.Key.Date.Year,
                             Month = b.Key.Date.Month,
                             Day = b.Key.Date.Day,

                             PassQty = b.Sum(item => item.PassQty),
                             RejQty = b.Sum(item => item.RejQty)
                         };
            string strJson = JsonConvert.SerializeObject(result);

            List<ProductChart_Model.Daily> dailyList = JsonConvert.DeserializeObject<List<ProductChart_Model.Daily>>(strJson);


            return dailyList;
        }

        public List<ProductChart_Model.Monthly> GetMonthlyList(LaserProductChartParam param)
        {
            var list = GetBaseList(param);
            if (list == null) return null;

            var result = from a in list
                         orderby a.Date ascending
                         group a by new {a.Year,a.Month} into b
                         select new
                         {
                             Year = b.Key.Year,
                             Month = b.Key.Month,

                             PassQty = b.Sum(item => item.PassQty),
                             RejQty = b.Sum(item => item.RejQty)
                         };
            string strJson = JsonConvert.SerializeObject(result);
            List<ProductChart_Model.Monthly> monthlyList = JsonConvert.DeserializeObject<List<ProductChart_Model.Monthly>>(strJson);
            
            return monthlyList;
        }
        
        public List<ProductChart_Model.Yearly> GetYearList(LaserProductChartParam param)
        {
            var list = GetBaseList(param);
            if (list == null) return null;

            var result = from a in list
                         orderby a.Date ascending
                         group a by new { a.Year } into b
                         select new
                         {
                             Year = b.Key.Year,

                             PassQty = b.Sum(item => item.PassQty),
                             RejQty = b.Sum(item => item.RejQty)
                         };
            string strJson = JsonConvert.SerializeObject(result);
            List<ProductChart_Model.Yearly> yearlyList = JsonConvert.DeserializeObject<List<ProductChart_Model.Yearly>>(strJson);

            return yearlyList;
        }
        

    }
}
