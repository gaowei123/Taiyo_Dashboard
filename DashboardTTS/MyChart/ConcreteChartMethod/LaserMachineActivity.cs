using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;
using Taiyo.SearchParam.LaserParam;


namespace MyChart.ConcreteChartMethod
{
    public class LaserMachineActivity : IChartMethod
    {
        private Common.ExtendClass.LaserActivityChart.LaserActivityChart_BLL _bll = new Common.ExtendClass.LaserActivityChart.LaserActivityChart_BLL();

        public ChartModel GetChartData(BaseParam param)
        {
            return new ChartModel()
            {
                LegendData = GetLegend(param),
                SeriesData = GetSeries(param),
                XAxisData = GetXAxisData(param)
            };
        }

        public List<string> GetLegend(BaseParam param)
        {
            return new List<string>()
            {
                StaticRes.Global.LaserStatus.Run,
                StaticRes.Global.LaserStatus.Setup,
                StaticRes.Global.LaserStatus.Buyoff,
                StaticRes.Global.LaserStatus.Testing,
                StaticRes.Global.LaserStatus.NoSchedule,
                StaticRes.Global.LaserStatus.Maintenance,
                StaticRes.Global.LaserStatus.Breakdown,
                StaticRes.Global.LaserStatus.Shutdown
            };
        }


    

        public List<Series> GetSeries(BaseParam param)
        {
            var activityParam = (Taiyo.SearchParam.LaserParam.LaserActivityCondition)param;
            var dataList = _bll.GetDataList(activityParam);
            if (dataList == null)
                return null;


            List<Series> seriesList = new List<Series>();


            var stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Run orderby a.MachineID ascending select a;
            Series runSeries = new Series();
            runSeries.Name = StaticRes.Global.LaserStatus.Run;
            runSeries.Stack = StaticRes.Global.LaserStatus.Run;
            runSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                runSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Setup orderby a.MachineID ascending select a;
            Series setupSeries = new Series();
            setupSeries.Name = StaticRes.Global.LaserStatus.Setup;
            setupSeries.Stack = StaticRes.Global.LaserStatus.Run;
            setupSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                setupSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Buyoff orderby a.MachineID ascending select a;
            Series buyoffSeries = new Series();
            buyoffSeries.Name = StaticRes.Global.LaserStatus.Buyoff;
            buyoffSeries.Stack = StaticRes.Global.LaserStatus.Run;
            buyoffSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                buyoffSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Testing orderby a.MachineID ascending select a;
            Series testingSeries = new Series();
            testingSeries.Name = StaticRes.Global.LaserStatus.Testing;
            testingSeries.Stack = StaticRes.Global.LaserStatus.Run;
            testingSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                testingSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.NoSchedule orderby a.MachineID ascending select a;
            Series noscheduleSeries = new Series();
            noscheduleSeries.Name = StaticRes.Global.LaserStatus.NoSchedule;
            noscheduleSeries.Stack = StaticRes.Global.LaserStatus.NoSchedule;
            noscheduleSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                noscheduleSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Maintenance orderby a.MachineID ascending select a;
            Series maintenanceSeries = new Series();
            maintenanceSeries.Name = StaticRes.Global.LaserStatus.Maintenance;
            maintenanceSeries.Stack = StaticRes.Global.LaserStatus.NoSchedule;
            maintenanceSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                maintenanceSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Breakdown orderby a.MachineID ascending select a;
            Series breakdownSeries = new Series();
            breakdownSeries.Name = StaticRes.Global.LaserStatus.Breakdown;
            breakdownSeries.Stack = StaticRes.Global.LaserStatus.Breakdown;
            breakdownSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                breakdownSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            stautslist = from a in dataList where a.Status == StaticRes.Global.LaserStatus.Shutdown orderby a.MachineID ascending select a;
            Series shutdownSeries = new Series();
            shutdownSeries.Name = StaticRes.Global.LaserStatus.Shutdown;
            shutdownSeries.Stack = StaticRes.Global.LaserStatus.Shutdown;
            shutdownSeries.Type = "bar";
            foreach (var item in stautslist)
            {
                shutdownSeries.Data.Add((decimal)Math.Round(item.TotalSeconds / 3600, 2));
            }


            seriesList.Add(runSeries);
            seriesList.Add(setupSeries);
            seriesList.Add(buyoffSeries);
            seriesList.Add(testingSeries);
            seriesList.Add(noscheduleSeries);
            seriesList.Add(maintenanceSeries);
            seriesList.Add(breakdownSeries);
            seriesList.Add(shutdownSeries);

            return seriesList;
        }


        public List<string> GetXAxisData(BaseParam param)
        {
            return new List<string>()
            {
                "Machine1",
                "Machine2",
                "Machine3",
                "Machine4",
                "Machine5",
                "Machine6",
                "Machine7",
                "Machine8",
            };
        }
    }
}
