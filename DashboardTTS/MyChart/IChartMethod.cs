using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart
{
    public interface IChartMethod
    {
        List<string> GetLegend(Common.SearchingCondition.BaseCondition condition);

        List<string> GetXAxisData(Common.SearchingCondition.BaseCondition condition);

        List<MyChart.Series> GetSeries(Common.SearchingCondition.BaseCondition condition);

        ChartModel GetChartData(Common.SearchingCondition.BaseCondition condition);
    }
}
