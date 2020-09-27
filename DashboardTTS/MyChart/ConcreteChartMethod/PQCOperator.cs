using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart.ConcreteChartMethod
{
    public class PQCOperator : IChartMethod
    {
        public ChartModel GetChartData(Common.SearchingCondition.BaseCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLegend(Common.SearchingCondition.BaseCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<Series> GetSeries(Common.SearchingCondition.BaseCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(Common.SearchingCondition.BaseCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
