using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Enum.Organization;
using Taiyo.SearchParam;

namespace MyChart.ConcreteChartMethod
{
    public class HomeTrend : IChartMethod
    {
        Common.ExtendClass.Home.Home_BLL _bll = new Common.ExtendClass.Home.Home_BLL();

        public ChartModel GetChartData(BaseParam param)
        {
            var homeParam = (HomeParam)param;
            var chartDataList = _bll.GetDailyTrend(homeParam);


            #region generate xAxis 
            List<string> xAxisLabelList = new List<string>();
            var dayList = from a in chartDataList group a by new { a.Month,a.WeekName, a.Day } into b orderby b.Key.Month ascending, b.Key.Day ascending select new { b.Key.Month, b.Key.WeekName, b.Key.Day };
            foreach (var item in dayList)
            {
                string value = string.Format("{0}/{1}\n{2}",
                    item.Day,
                    Common.CommFunctions.GetMonthName(item.Month, false),
                    item.WeekName);

                xAxisLabelList.Add(value);
            }
            #endregion



            #region generate series
            Series moulding = new Series();
            moulding.Name = Department.Moulding.ToString();
            moulding.Type = "line";
            moulding.Data = (from a in chartDataList where a.Department == Department.Moulding.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();
            
            Series painting = new Series();
            painting.Name = Department.Painting.ToString();
            painting.Type = "line";
            painting.Data = (from a in chartDataList where a.Department == Department.Painting.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();
            
            Series laser = new Series();
            laser.Name = Department.Laser.ToString();
            laser.Type = "line";
            laser.Data = (from a in chartDataList where a.Department == Department.Laser.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();
        
            Series online = new Series();
            online.Name = Department.Online.ToString();
            online.Type = "line";
            online.Data = (from a in chartDataList where a.Department == Department.Online.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();
            
            Series wip = new Series();
            wip.Name = Department.WIP.ToString();
            wip.Type = "line";
            wip.Data = (from a in chartDataList where a.Department == Department.WIP.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();
         
            Series packing = new Series();
            packing.Name = Department.Packing.ToString();
            packing.Type = "line";
            packing.Data = (from a in chartDataList where a.Department == Department.Packing.ToString() orderby a.Month ascending, a.Day ascending select a.Output).ToList();

            List<Series> seriesList = new List<Series>() { moulding, painting, laser, online, wip, packing };
            #endregion



            ChartModel model = new ChartModel();
            model.LegendData = GetLegend(homeParam);
            model.XAxisData = xAxisLabelList;
            model.SeriesData = seriesList;

            return model;
        }

        public List<string> GetLegend(BaseParam param)
        {
            return new List<string>() {
                Department.Moulding.ToString(),
                Department.Painting.ToString(),
                Department.Laser.ToString(),
                Department.Online.ToString(),
                Department.WIP.ToString(),
                Department.Packing.ToString()};
        }

        public List<Series> GetSeries(BaseParam para)
        {
            throw new NotImplementedException();
        }

        public List<string> GetXAxisData(BaseParam param)
        {
            throw new NotImplementedException();
        }
    }
}
