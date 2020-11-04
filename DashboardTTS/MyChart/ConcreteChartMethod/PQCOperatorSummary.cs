using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SearchingCondition;

namespace MyChart.ConcreteChartMethod
{
    public class PQCOperatorSummary : IChartMethod
    {

        Common.ExtendClass.PQCOperatorSummaryChart.BLL _bll = new Common.ExtendClass.PQCOperatorSummaryChart.BLL();


        public ChartModel GetChartData(BaseCondition condition)
        {
            var xAxis = GetXAxisData(condition);
            var legend = GetLegend(condition);
            var series = GetSeries(condition);

            if (series == null)
                return null;

            return new ChartModel()
            {
                XAxisData = xAxis,
                LegendData = legend,
                SeriesData  = series
            };
        }

        public List<string> GetLegend(BaseCondition condition)
        {
            return new List<string>()
            {
                "Checking",
                "Packing"
            };
        }

        public List<Series> GetSeries(BaseCondition condition)
        {
            var cond = (PQCOperatorSummaryCondition)condition;



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

            if (cond.GroupBy == "Daily")
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
            else if (cond.GroupBy == "Monthly")
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
            else if (cond.GroupBy == "Yearly")
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

        public List<string> GetXAxisData(BaseCondition condition)
        {
            var cond = (PQCOperatorSummaryCondition)condition;
            List<Common.ExtendClass.PQCOperatorSummaryChart.Model> dataList = _bll.GetDataList(cond);
            if (dataList == null)
                return null;

            List<string> xAxisList = new List<string>();

            if (cond.GroupBy == "Daily")
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
            else if (cond.GroupBy == "Monthly")
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
            else if (cond.GroupBy == "Yearly")
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