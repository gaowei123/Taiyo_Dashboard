using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;

namespace MyChart.ConcreteChartMethod
{
    public class PQCTopRejPartNo : IChartMethod
    {
        private readonly Common.ExtendClass.PQCTopRejChart.BLL _bll = new Common.ExtendClass.PQCTopRejChart.BLL();

        public ChartModel GetChartData(BaseParam param)
        {
            var dataList = _bll.GetPartNoList((Taiyo.SearchParam.PQCParam.PQCTopRejectCondition)param);
            if (dataList == null)
                return null;

            var legendList = new List<string>() { "RejQty" };
            var xAxisList = new List<string>();

            var seriesRej = new Series();
            seriesRej.Name = "RejQty";
            seriesRej.Type = "bar";
            seriesRej.Stack = "Rej";


            foreach (var item in dataList)
            {
                xAxisList.Add(item.PartNo);
                seriesRej.Data.Add(item.RejQty);
            }
            

            return new ChartModel()
            {
                LegendData = legendList,
                XAxisData = xAxisList,
                SeriesData = new List<Series>() { seriesRej }
            };
        }

        public List<string> GetLegend(BaseParam param)
        {
            throw new NotImplementedException();
        }

        public List<Series> GetSeries(BaseParam param)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(BaseParam param)
        {
            throw new NotImplementedException();
        }
    }
}
