﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyChart.ConcreteChartMethod
{
    public class LaserMachine : IChartMethod
    {

    

        public ChartModel GetChartData(Common.SearchingCondition.BaseCondition bCondition)
        {
            


            ChartModel chart = new ChartModel();
            //chart.LegendData = GetLegend(condition);
            //chart.XAxisData = GetXAxisData(condition);
            //chart.SeriesData = GetSeries(condition);

            return chart;
        }


        public List<string> GetLegend(Common.SearchingCondition.BaseCondition bCondition)
        {
            return new List<string>() { "Run", "Shutdown", "Idle", "Breakdown" };
        }

        public List<MyChart.Series> GetSeries(Common.SearchingCondition.BaseCondition bCondition)
        {
            List<MyChart.Series> listSeries = new List<MyChart.Series>();

            listSeries.Add(new MyChart.Series()
            {
                Name = "Run",
                Type = "Bar",
                Stack = "TimeSpan",
                Data = new List<decimal> { 10, 9, 9, 9, 11, 12, 7, 8 }
            });

            listSeries.Add(new MyChart.Series()
            {
                Name = "Shutdown",
                Type = "Bar",
                Stack = "TimeSpan",
                Data = new List<decimal> { 1, 1, 1, 1, 1, 0, 1, 1 }
            });

            listSeries.Add(new MyChart.Series()
            {
                Name = "Idle",
                Type = "Bar",
                Stack = "TimeSpan",
                Data = new List<decimal> { 0, 1, 1, 1, 0, 0, 3, 2 }
            });
            listSeries.Add(new MyChart.Series()
            {
                Name = "Breakdown",
                Type = "Bar",
                Stack = "TimeSpan",
                Data = new List<decimal> { 0, 1, 1, 1, 1, 0, 1, 1 }
            });


            return listSeries;
        }

        public List<string> GetXAxisData(Common.SearchingCondition.BaseCondition bCondition)
        {
            return new List<string>() { "Machine1", "Machine2", "Machine3", "Machine4", "Machine5", "Machine6", "Machine7", "Machine8" };
        }
        
    }
}