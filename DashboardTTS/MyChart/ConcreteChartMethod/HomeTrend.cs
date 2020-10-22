using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SearchingCondition;

namespace MyChart.ConcreteChartMethod
{
    public class HomeTrend : IChartMethod
    {


        Common.ExtendClass.Home.Home_BLL _bll = new Common.ExtendClass.Home.Home_BLL();


        public ChartModel GetChartData(BaseCondition condition)
        {
            ChartModel model = new ChartModel();
            model.LegendData = GetLegend(condition);
            model.XAxisData = GetXAxisData(condition);
            model.SeriesData = GetSeries(condition);

            return model;
        }

        public List<string> GetLegend(BaseCondition condition)
        {
            return new List<string>() { "Moulding", "Laser", "Online", "WIP", "Packing" };
        }

        public List<Series> GetSeries(BaseCondition condition)
        {
            Series moulding = new Series();
            moulding.Name = "Moulding";
            moulding.Type = "line";
            var moudlingList = _bll.GetMouldingDaily(condition);        
            foreach (var item in moudlingList)
            {
                moulding.Data.Add(item.Output);
            }

            Series laser = new Series();
            laser.Name = "Laser";
            laser.Type = "line";
            var laserList = _bll.GetLaserDaily(condition);
            foreach (var item in laserList)
            {
                laser.Data.Add(item.Output);
            }

            Series online = new Series();
            online.Name = "Online";
            online.Type = "line";
            var onlineList = _bll.GetPQCOnlineDaily(condition);
            foreach (var item in onlineList)
            {
                online.Data.Add(item.Output);
            }

            Series wip = new Series();
            wip.Name = "WIP";
            wip.Type = "line";
            var wipList = _bll.GetPQCWIPDaily(condition);
            foreach (var item in wipList)
            {
                wip.Data.Add(item.Output);
            }

            Series packing = new Series();
            packing.Name = "Packing";
            packing.Type = "line";
            var packingList = _bll.GetPQCPackingDaily(condition);
            foreach (var item in packingList)
            {
                packing.Data.Add(item.Output);
            }


            return new List<Series>() { moulding, laser, online, wip, packing };
        }

        public List<string> GetXAxisData(BaseCondition condition)
        {
            List<string> list = new List<string>();
            DateTime dTemp = condition.DateFrom.Value.Date;
            while (dTemp < condition.DateTo.Value.Date)
            {
                string monthName = Common.CommFunctions.GetMonthName(dTemp.Month, false);
                string day = dTemp.Day.ToString();

                list.Add(monthName + "." + day);


                dTemp = dTemp.AddDays(1);
            }
            return list;
        }
    }
}
