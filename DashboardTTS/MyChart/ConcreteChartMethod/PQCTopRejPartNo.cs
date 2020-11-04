using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SearchingCondition;

namespace MyChart.ConcreteChartMethod
{
    public class PQCTopRejPartNo : IChartMethod
    {
        private readonly Common.ExtendClass.PQCTopRejChart.BLL _bll = new Common.ExtendClass.PQCTopRejChart.BLL();

        public ChartModel GetChartData(BaseCondition condition)
        {
            var dataList = _bll.GetPartNoList((Common.SearchingCondition.PQCTopRejectCondition)condition);
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

        public List<string> GetLegend(BaseCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<Series> GetSeries(BaseCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(BaseCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
