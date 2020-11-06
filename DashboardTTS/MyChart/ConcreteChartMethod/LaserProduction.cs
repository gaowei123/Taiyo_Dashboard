using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.SearchParam;

namespace MyChart.ConcreteChartMethod
{
    public class LaserProduction : IChartMethod
    {
        private Common.ExtendClass.LaserProductionChart.LaserProduction_BLL _bll = new Common.ExtendClass.LaserProductionChart.LaserProduction_BLL();

        public ChartModel GetChartData(BaseParam param)
        {
            MyChart.ChartModel model = new ChartModel();
            model.SeriesData = GetSeries(param);
            model.XAxisData = GetXAxisData(param);
            model.LegendData = GetLegend(param);


            return model;
        }

        public List<string> GetLegend(BaseParam param)
        {
            return new List<string> { "OK","NG" };
        }

        public List<Series> GetSeries(BaseParam param)
        {
            Taiyo.SearchParam.LaserParam.LaserProductionCondition productionParam = (Taiyo.SearchParam.LaserParam.LaserProductionCondition)param;
            var productionList = _bll.GetProductionList(productionParam);
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

            if (productionParam.ChartType == "Machine")
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
            else if (productionParam.ChartType == "Model")
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
            else if (productionParam.ChartType == "PartNo")
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
                throw new NullReferenceException("No define type:" + productionParam.ChartType);
            }

            return new List<Series> { seriesPass , seriesRej};
        }

        public List<string> GetXAxisData(BaseParam param)
        {
            Taiyo.SearchParam.LaserParam.LaserProductionCondition productionParam = (Taiyo.SearchParam.LaserParam.LaserProductionCondition)param;
            if (productionParam.ChartType == "Machine")
            {
                return new List<string> { "Machine1", "Machine2", "Machine3", "Machine4", "Machine5", "Machine6", "Machine7", "Machine8" };
            }
            else if (productionParam.ChartType == "Model")
            {
                var productionList = _bll.GetProductionList(productionParam);
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
            else if (productionParam.ChartType == "PartNo")
            {
                var productionList = _bll.GetProductionList(productionParam);
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
                throw new NullReferenceException("No define type:" + productionParam.ChartType);
            }
        }
    }
}
