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
        public ChartModel GetChartData(BaseCondition condition)
        {
            throw new NotImplementedException();
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
