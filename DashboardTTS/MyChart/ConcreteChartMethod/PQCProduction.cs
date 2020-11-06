using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart.ConcreteChartMethod
{
    public class PQCProduction : IChartMethod
    {
        public ChartModel GetChartData(Taiyo.SearchParam.BaseParam param)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLegend(Taiyo.SearchParam.BaseParam param)
        {
            throw new NotImplementedException();
        }

        public List<Series> GetSeries(Taiyo.SearchParam.BaseParam param)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(Taiyo.SearchParam.BaseParam param)
        {
            throw new NotImplementedException();
        }
    }
}
