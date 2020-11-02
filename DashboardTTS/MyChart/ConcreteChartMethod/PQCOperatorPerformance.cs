using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SearchingCondition;

namespace MyChart.ConcreteChartMethod
{
    class PQCOperatorPerformance : IChartMethod
    {

        Common.ExtendClass.PQCOperatorPerformanceChart.BLL _bll = new Common.ExtendClass.PQCOperatorPerformanceChart.BLL();




        public ChartModel GetChartData(BaseCondition condition)
        {
            var lengenList = GetLegend(condition);
            var xAxisList = GetXAxisData(condition);
            var seriesList = GetSeries(condition);

            if (seriesList == null)
                return null;

            ChartModel model = new ChartModel();
            model.LegendData = lengenList;
            model.XAxisData = xAxisList;
            model.SeriesData = seriesList;


            return model;
        }

        public List<string> GetLegend(BaseCondition condition)
        {
            return new List<string>()
            {
               "Checking",
               "Packing"
            };
        }

        public List<Series> GetSeries(BaseCondition condition)
        {
            List<Common.ExtendClass.PQCOperatorPerformanceChart.Model> dataList = _bll.GetOpList(condition);
            if (dataList == null)
                return null;


            Series checkingSeries = new Series();
            checkingSeries.Name = "Checking";
            checkingSeries.Type = "bar";
            checkingSeries.Stack = "output";
            
            Series packingSeries = new Series();
            packingSeries.Name = "Packing";
            packingSeries.Type = "bar";
            packingSeries.Stack = "output";

            foreach (var item in dataList)
            {
                checkingSeries.Data.Add(item.CheckQty);
                packingSeries.Data.Add(item.PackQty);
            }



            return new List<Series>()
               {
                   checkingSeries, packingSeries
               };
        }

        public List<string> GetXAxisData(BaseCondition condition)
        {
            List<Common.ExtendClass.PQCOperatorPerformanceChart.Model> dataList = _bll.GetOpList(condition);
            if (dataList == null )
                return null;


            List<string> userList = new List<string>();

            foreach (var item in dataList)
            {
                userList.Add(item.UserID);
            }


            return userList;
        }




    }
}
