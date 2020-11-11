using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Taiyo.SearchParam;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.Home
{
    public class Home_BLL
    {
        Home_DAL _dal = new Home_DAL();


        /// <summary>
        /// sql统一字段 month, day, output
        /// </summary>
        private List<Home_Model.DailyTrend> ConvertList(DataTable dt, Department dpt)
        {           
            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                model.Department = dpt.ToString();
                model.Day = int.Parse(dr["day"].ToString());
                model.Month = int.Parse(dr["month"].ToString());
                model.Output = decimal.Parse(dr["output"].ToString());

                DateTime dTemp = DateTime.Parse($"{DateTime.Now.Date.Year}-{model.Month}-{model.Day}");
                model.WeekName = dTemp.GetWeekName(false);
                modelList.Add(model);
            }

            return modelList;
        }



        /// <summary>
        /// 周末, 节假日没数据, 查询不到对应的天
        /// 循环from-to, 补齐全部天数, 缺失天output默认0;
        /// </summary>
        private List<Home_Model.DailyTrend> FillAllDate(List<Home_Model.DailyTrend> modelList, HomeParam param, Department dpt)
        {
            DateTime dTemp = param.DateFrom.Value.Date;
            while (dTemp < param.DateTo.Value.Date)
            {
                var result = from a in modelList where a.Day == dTemp.Day && a.Month == dTemp.Month select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = dpt.ToString();
                    model.Day = dTemp.Day;
                    model.Month = dTemp.Month;
                    model.WeekName = dTemp.GetWeekName(false);
                    model.Output = 0;
                    modelList.Add(model);
                }

                dTemp = dTemp.AddDays(1);
            }

            return modelList.OrderBy(p => p.Month).OrderBy(p=>p.Day).ToList();
        }




        #region 各部门daily list
        public List<Home_Model.DailyTrend> GetMouldingDaily(HomeParam param)
        {
            DataTable dt = _dal.GetMouldingDaily(param);
            if (dt==null || dt.Rows.Count == 0)
                return null;

            var modelList = ConvertList(dt, Department.Moulding);
            return FillAllDate(modelList, param, Department.Moulding);
        }
        public List<Home_Model.DailyTrend> GetPaintingDaily(HomeParam param)
        {
            DataTable dt = _dal.GetPaintingDaily(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var modelList = ConvertList(dt, Department.Painting);
            return FillAllDate(modelList, param, Department.Painting);
        }
        public List<Home_Model.DailyTrend> GetLaserDaily(HomeParam param)
        {
            DataTable dt = _dal.GetLaserDaily(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var modelList = ConvertList(dt, Department.Laser);
            return FillAllDate(modelList, param, Department.Laser);
        }
        public List<Home_Model.DailyTrend> GetPQCOnlineDaily(HomeParam param)
        {
            DataTable dt = _dal.GetPQCOnlineDaily(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var modelList = ConvertList(dt, Department.Online);
            return FillAllDate(modelList, param, Department.Online);
        }
        public List<Home_Model.DailyTrend> GetPQCWIPDaily(HomeParam param)
        {
            DataTable dt = _dal.GetPQCWIPDaily(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var modelList = ConvertList(dt, Department.WIP);
            return FillAllDate(modelList, param, Department.WIP);
        }
        public List<Home_Model.DailyTrend> GetPQCPackingDaily(HomeParam param)
        {
            DataTable dt = _dal.GetPQCPackingDaily(param);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            
            var modelList = ConvertList(dt, Department.Packing);
            return FillAllDate(modelList, param, Department.Packing);
        }
        #endregion



        /// <summary>
        /// 用于主页饼图数据
        /// 获取各部门当天的output
        /// </summary>
        public List<Home_Model.DailyTrend> GetDailyTrend(HomeParam param)
        {
            List<Home_Model.DailyTrend> all = new List<Home_Model.DailyTrend>();


            var mouldingList = GetMouldingDaily(param);
            if (mouldingList != null)
                all.AddRange(mouldingList);

            var paintingList = GetPaintingDaily(param);
            if (paintingList != null)
                all.AddRange(paintingList);

            var laserList = GetLaserDaily(param);
            if (laserList != null)
                all.AddRange(laserList);

            var onlineList = GetPQCOnlineDaily(param);
            if (onlineList != null)
                all.AddRange(onlineList);

            var wipList = GetPQCWIPDaily(param);
            if (wipList != null)
                all.AddRange(wipList);

            var packingList = GetPQCPackingDaily(param);
            if (packingList != null)
                all.AddRange(packingList);
       






            if (param.IsDisplayOffday)
            {
                return all.OrderBy(p => p.Month).OrderBy(p => p.Day).ToList();
            }
            else
            {
                List<Home_Model.DailyTrend> allWithoutOffday = new List<Home_Model.DailyTrend>();

                var dayList = from a in all
                              group a by new { a.Month, a.Day } into b
                              select new
                              {
                                  b.Key.Month,
                                  b.Key.Day,
                                  Output = b.Sum(p => p.Output)
                              };
                foreach (var item in dayList)
                {
                    if (item.Output != 0)
                    {
                        var curDayList = (from a in all where a.Month == item.Month && a.Day == item.Day select a).ToList();
                        allWithoutOffday.AddRange(curDayList);
                    }
                }



                var test = from a in all where a.Month == 11 && a.Day == 1 select a;





                return allWithoutOffday.OrderBy(p => p.Month).OrderBy(p => p.Day).ToList();
            }
        }



    }
}
