using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;
using Taiyo.SearchParam.LaserParam;
using Taiyo.Tool.Extension;

namespace MyChart.ConcreteChartMethod
{
    public class LaserProduction : IChartMethod
    {
        private Common.ExtendClass.LaserProductChart.ProductChart_BLL _bll = new Common.ExtendClass.LaserProductChart.ProductChart_BLL();


        public ChartModel GetChartData(BaseParam param)
        {
            var productChartParam = (LaserProductChartParam)param;

            MyChart.ChartModel model = new ChartModel();            
            model.LegendData = GetLegend(param);

            Series seriesPass = new Series();
            Series seriesRej = new Series();

            seriesPass.Name = "OK";
            seriesPass.Type = "bar";
            seriesPass.Stack = "output";

            seriesRej.Name = "NG";
            seriesRej.Type = "line";
            seriesRej.Stack = "output";

            if (productChartParam.Type == "Daily")
            {
                #region daily
                var dailyList = _bll.GetDailyList(productChartParam);                
                foreach (var item in dailyList)
                {
                    model.XAxisData.Add(string.Format("{0}/{1}", item.Day, (new DateTime(item.Year, item.Month, item.Day)).GetMonthName(false)));
                    seriesPass.Data.Add(item.PassQty);
                    seriesRej.Data.Add(item.RejQty);
                }

                model.SeriesData = new List<Series>() { seriesPass, seriesRej };
                #endregion
            }
            else if(productChartParam.Type == "Monthly")
            {
                #region daily
                var monthlyList = _bll.GetMonthlyList(productChartParam);
                foreach (var item in monthlyList)
                {
                    model.XAxisData.Add(string.Format("{0}/{1}", (new DateTime(item.Year, item.Month,1)).GetMonthName(false), item.Year));
                    seriesPass.Data.Add(item.PassQty);
                    seriesRej.Data.Add(item.RejQty);
                }

                model.SeriesData = new List<Series>() { seriesPass, seriesRej };
                #endregion
            }
            else if (productChartParam.Type == "Yearly")
            {
                #region daily
                var yearlyList = _bll.GetYearList(productChartParam);
                foreach (var item in yearlyList)
                {
                    model.XAxisData.Add(item.Year.ToString());
                    seriesPass.Data.Add(item.PassQty);
                    seriesRej.Data.Add(item.RejQty);
                }

                model.SeriesData = new List<Series>() { seriesPass, seriesRej };
                #endregion
            }
            
            return model;
        }

        public List<string> GetLegend(BaseParam param)
        {
            return new List<string> { "OK","NG" }; 
        }

        public List<Series> GetSeries(BaseParam param)
        {
            throw new NotImplementedException("");
        }

        public List<string> GetXAxisData(BaseParam param)
        {
            throw new NotImplementedException("");
        }
    }
}
