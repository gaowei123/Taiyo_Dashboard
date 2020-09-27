using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart
{
    public class ChartModel
    {
        public List<string> LegendData;

        public List<string> XAxisData;

        public List<Series> SeriesData;
               
    }

    public class Series
    {
        public Series()
        {
            this.Data = new List<decimal>();
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Stack { get; set; }
        public List<decimal> Data { get; set; }
    }
}
