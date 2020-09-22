using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChart.SearchingCondition;

namespace MyChart.ConcreteChartMethod
{
    public class LaserProduction : IChartMethod
    {
        public ChartModel GetChartData(BaseCondition bCondition)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLegend(BaseCondition bCondition)
        {
            throw new NotImplementedException();
        }

        public List<Series> GetSeries(BaseCondition bCondition)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(BaseCondition bCondition)
        {
            throw new NotImplementedException();
        }
    }
}
