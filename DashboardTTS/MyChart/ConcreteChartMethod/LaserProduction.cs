using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChart.ConcreteChartMethod
{
    public class LaserProduction : IChartMethod
    {
        private Common.ExtendClass.LaserProductionChart.LaserProduction_BLL _bll = new Common.ExtendClass.LaserProductionChart.LaserProduction_BLL();

        public ChartModel GetChartData(Common.SearchingCondition.BaseCondition bCondition)
        {
            MyChart.ChartModel model = new ChartModel();
            model.SeriesData = GetSeries(bCondition);
            model.XAxisData = GetXAxisData(bCondition);
            model.LegendData = GetLegend(bCondition);


            return model;
        }

        public List<string> GetLegend(Common.SearchingCondition.BaseCondition bCondition)
        {
            return new List<string> { "OK","NG" };
        }

        public List<Series> GetSeries(Common.SearchingCondition.BaseCondition bCondition)
        {
            Common.SearchingCondition.LaserProductionCondition condition = (Common.SearchingCondition.LaserProductionCondition)bCondition;
            var productionList = _bll.GetProductionList(condition);
            if (productionList == null)
                return null;


            MyChart.Series seriesPass = new MyChart.Series();
            seriesPass.Name = "OK";
            seriesPass.Stack = "Output";
            seriesPass.Type = "bar";

            MyChart.Series seriesRej = new MyChart.Series();
            seriesRej.Name = "NG";
            seriesRej.Stack = "Output";
            seriesRej.Type = "bar";

            if (condition.ChartType == "Machine")
            {
                var groupList = from a in productionList
                                  orderby a.MachineID ascending
                                  group a by a.MachineID into b
                                  select new
                                  {
                                      b.Key,
                                      PassQty = b.Sum(p => p.PassQty),
                                      RejQty = b.Sum(p => p.RejQty)
                                  };
                foreach (var item in groupList)
                {
                    seriesPass.Data.Add((decimal)item.PassQty);
                    seriesRej.Data.Add((decimal)item.RejQty);
                }
            }
            else if (condition.ChartType == "Model")
            {
                var groupList = from a in productionList
                                group a by a.Model into b
                                select new
                                {
                                    b.Key,
                                    PassQty = b.Sum(p => p.PassQty),
                                    RejQty = b.Sum(p => p.RejQty)
                                };
                var temp = from a in groupList
                           where a.PassQty + a.RejQty > 0
                           orderby a.PassQty descending
                           select a;

                foreach (var item in temp)
                {
                    seriesPass.Data.Add((decimal)item.PassQty);
                    seriesRej.Data.Add((decimal)item.RejQty);
                }
            }
            else if (condition.ChartType == "PartNo")
            {
                var groupList = from a in productionList
                                  group a by a.PartNo into b
                                  select new
                                  {
                                      b.Key,
                                      PassQty = b.Sum(p => p.PassQty),
                                      RejQty = b.Sum(p => p.RejQty)
                                  };
                var temp = from a in groupList
                           where a.PassQty + a.RejQty > 0
                           orderby a.PassQty descending
                           select a;

                foreach (var item in temp)
                {
                    seriesPass.Data.Add((decimal)item.PassQty);
                    seriesRej.Data.Add((decimal)item.RejQty);
                }
            }
            else
            {
                throw new NullReferenceException("No define type:" + condition.ChartType);
            }

            return new List<Series> { seriesPass , seriesRej};
        }

        public List<string> GetXAxisData(Common.SearchingCondition.BaseCondition bCondition)
        {
            Common.SearchingCondition.LaserProductionCondition condition = (Common.SearchingCondition.LaserProductionCondition)bCondition;
            if (condition.ChartType == "Machine")
            {
                return new List<string> { "Machine1", "Machine2", "Machine3", "Machine4", "Machine5", "Machine6", "Machine7", "Machine8" };
            }
            else if (condition.ChartType == "Model")
            {
                var productionList = _bll.GetProductionList(condition);
                if (productionList == null)
                    return null;

                var groupList = from a in productionList
                                group a by a.Model into b
                                select new
                                {
                                    b.Key,
                                    PassQty = b.Sum(p => p.PassQty),
                                    RejQty = b.Sum(p => p.RejQty)
                                };
                var temp = from a in groupList
                           where a.PassQty + a.RejQty > 0
                           orderby a.PassQty descending
                           select a;

                List<string> xAxisList = new List<string>();
                foreach (var str in temp)
                {
                    xAxisList.Add(str.Key);
                }

                return xAxisList;
            }
            else if (condition.ChartType == "PartNo")
            {
                var productionList = _bll.GetProductionList(condition);
                if (productionList == null)
                    return null;

                var groupList = from a in productionList
                                group a by a.PartNo into b
                                select new
                                {
                                    b.Key,
                                    PassQty = b.Sum(p => p.PassQty),
                                    RejQty = b.Sum(p => p.RejQty)
                                };
                var temp = from a in groupList
                           where a.PassQty + a.RejQty > 0
                           orderby a.PassQty descending
                           select a;

                List<string> xAxisList = new List<string>();
                foreach ( var str in temp)
                {
                    xAxisList.Add(str.Key);
                }

                return xAxisList;
            }
            else
            {
                throw new NullReferenceException("No define type:" + condition.ChartType);
            }
        }
    }
}
