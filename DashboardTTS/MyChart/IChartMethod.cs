using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart
{
    public interface IChartMethod
    {
        List<string> GetLegend(SearchingCondition.BaseCondition condition);

        List<string> GetXAxisData(SearchingCondition.BaseCondition condition);

        List<MyChart.Series> GetSeries(SearchingCondition.BaseCondition condition);

        ChartModel GetChartData(SearchingCondition.BaseCondition condition);
    }


}
