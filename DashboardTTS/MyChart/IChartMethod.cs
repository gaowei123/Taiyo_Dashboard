using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;

namespace MyChart
{
    public interface IChartMethod
    {
        List<string> GetLegend(BaseParam para);

        List<string> GetXAxisData(BaseParam para);

        List<MyChart.Series> GetSeries(BaseParam para);

        ChartModel GetChartData(BaseParam para);
    }
}
