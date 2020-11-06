using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;

namespace MyChart.ConcreteChartMethod
{
    public class PQCOperatorSummary : IChartMethod
    {

        Common.ExtendClass.PQCOperatorSummaryChart.BLL _bll = new Common.ExtendClass.PQCOperatorSummaryChart.BLL();


        public ChartModel GetChartData(BaseParam param)
        {
            var xAxis = GetXAxisData(param);
            var legend = GetLegend(param);
            var series = GetSeries(param);

            if (series == null)
                return null;

            return new ChartModel()
            {
                XAxisData = xAxis,
                LegendData = legend,
                SeriesData  = series
            };
        }

        public List<string> GetLegend(BaseParam param)
        {
            return new List<string>()
            {
                "Checking",
                "Packing"
            };
        }

        public List<Series> GetSeries(BaseParam param)
        {
            var opSummaryParam = (Taiyo.SearchParam.PQCParam.PQCOperatorSummaryCondition)param;


            Series checkingSeries = new Series();
            checkingSeries.Name = "Checking";
            checkingSeries.Type = "bar";
            checkingSeries.Stack = "output";

            Series packingSeries = new Series();
            packingSeries.Name = "Packing";
            packingSeries.Type = "bar";
            packingSeries.Stack = "output";


            List<Common.ExtendClass.PQCOperatorSummaryChart.Model> dataList = _bll.GetDataList(cond);
            if (dataList == null)
                return null;

            if (opSummaryParam.GroupBy == "Daily")
            {
                var result = from a in dataList
                             group a by new { a.Day, a.Month } into b
                             orderby b.Key.Month ascending, b.Key.Day ascending
                             select new
                             {
                                 Day = $"{Common.CommFunctions.GetMonthName(b.Key.Month, false)} - {b.Key.Day}",
                                 CheckQty = b.Sum(p => p.CheckQty),
                                 PackQty = b.Sum(p => p.PackQty)
                             };
                foreach (var item in result)
                {
                    checkingSeries.Data.Add(item.CheckQty);
                    packingSeries.Data.Add(item.PackQty);
                }
            }
            else if (opSummaryParam.GroupBy == "Monthly")
            {
                var result = from a in dataList
                             group a by a.Month into b
                             orderby b.Key ascending
                             select new
                             {
                                 Month = Common.CommFunctions.GetMonthName(b.Key, false),
                                 CheckQty = b.Sum(p => p.CheckQty),
                                 PackQty = b.Sum(p => p.PackQty)
                             };
                foreach (var item in result)
                {
                    checkingSeries.Data.Add(item.CheckQty);
                    packingSeries.Data.Add(item.PackQty);
                }
            }
            else if (opSummaryParam.GroupBy == "Yearly")
            {
                var result = from a in dataList
                             group a by a.Year into b
                             orderby b.Key ascending
                             select new
                             {
                                 Year = b.Key,
                                 CheckQty = b.Sum(p => p.CheckQty),
                                 PackQty = b.Sum(p => p.PackQty)
                             };
                foreach (var item in result)
                {
                    checkingSeries.Data.Add(item.CheckQty);
                    packingSeries.Data.Add(item.PackQty);
                }
            }
            else
            {
                throw new NullReferenceException("No such type for group by!");
            }

            return new List<Series>()
            {
                checkingSeries,
                packingSeries
            };                       
        }

        public List<string> GetXAxisData(BaseParam param)
        {
            var opSummaryParam = (Taiyo.SearchParam.PQCParam.PQCOperatorSummaryCondition)param;
            List<Common.ExtendClass.PQCOperatorSummaryChart.Model> dataList = _bll.GetDataList(opSummaryParam);
            if (dataList == null)
                return null;

            List<string> xAxisList = new List<string>();

            if (opSummaryParam.GroupBy == "Daily")
            {
                var result = from a in dataList
                             group a by new { a.Day, a.Month } into b
                             orderby b.Key.Month ascending, b.Key.Day ascending
                             select new
                             {
                                 Day = $"{Common.CommFunctions.GetMonthName(b.Key.Month, false)}-{b.Key.Day}"                            
                             };
                foreach (var item in result)
                {
                    xAxisList.Add(item.Day);
                }                  
            }
            else if (opSummaryParam.GroupBy == "Monthly")
            {
                var result = from a in dataList
                             group a by a.Month into b
                             orderby b.Key ascending
                             select new
                             {
                                 Month = Common.CommFunctions.GetMonthName(b.Key, false)
                             };
                foreach (var item in result)
                {
                    xAxisList.Add(item.Month);
                }
            }
            else if (opSummaryParam.GroupBy == "Yearly")
            {
                var result = from a in dataList
                             group a by a.Year into b
                             orderby b.Key ascending
                             select new
                             {
                                 Year = b.Key
                             };
                foreach (var item in result)
                {
                    xAxisList.Add(item.Year.ToString());
                }
            }
            else
            {
                throw new NullReferenceException("No such type for group by!");
            }

            return xAxisList;
        }
    }
}