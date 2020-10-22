using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.ExtendClass.Home
{
    public class Home_BLL
    {
        Home_DAL _dal = new Home_DAL();


        


        public List<Home_Model.DailyTrend> GetMouldingDaily(SearchingCondition.BaseCondition cond)
        {
            DataTable dt = _dal.GetMouldingDaily(cond);
            if (dt==null || dt.Rows.Count == 0)
                return null;


            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();

                model.Department = "Moulding";
                DateTime day = DateTime.Parse(dr["day"].ToString());
                model.Day = Common.CommFunctions.GetMonthName(day.Month, false) + "." + day.Day.ToString();
                model.Output = decimal.Parse(dr["output"].ToString());

                modelList.Add(model);
            }


            DateTime dTemp = cond.DateFrom.Value.Date;
            while(dTemp < cond.DateTo.Value.Date)
            {
                string day = Common.CommFunctions.GetMonthName(dTemp.Month, false) + "." + dTemp.Day;

                var result = from a in modelList where a.Day == day select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = "Moulding";
                    model.Day = day;
                    model.Output = 0;
                    modelList.Add(model);
                }

                dTemp = dTemp.AddDays(1);
            }

            return (from a in modelList orderby a.Day ascending select a).ToList();
        }


        public List<Home_Model.DailyTrend> GetLaserDaily(SearchingCondition.BaseCondition cond)
        {
            DataTable dt = _dal.GetLaserDaily(cond);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();

                model.Department = "Laser";
                DateTime day = DateTime.Parse(dr["day"].ToString());
                model.Day = Common.CommFunctions.GetMonthName(day.Month, false) + "." + day.Day.ToString();
                model.Output = decimal.Parse(dr["output"].ToString());

                modelList.Add(model);
            }


            DateTime dTemp = cond.DateFrom.Value.Date;
            while (dTemp < cond.DateTo.Value.Date)
            {
                string day = Common.CommFunctions.GetMonthName(dTemp.Month, false) + "." + dTemp.Day;

                var result = from a in modelList where a.Day == day select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = "Laser";
                    model.Day = day;
                    model.Output = 0;
                    modelList.Add(model);
                }
                dTemp = dTemp.AddDays(1);
            }

            return (from a in modelList orderby a.Day ascending select a).ToList();
        }


        public List<Home_Model.DailyTrend> GetPQCOnlineDaily(SearchingCondition.BaseCondition cond)
        {
            DataTable dt = _dal.GetPQCOnlineDaily(cond);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();

                model.Department = "Online";
                DateTime day = DateTime.Parse(dr["day"].ToString());
                model.Day = Common.CommFunctions.GetMonthName(day.Month, false) + "." + day.Day.ToString();
                model.Output = decimal.Parse(dr["output"].ToString());

                modelList.Add(model);
            }


            DateTime dTemp = cond.DateFrom.Value.Date;
            while (dTemp < cond.DateTo.Value.Date)
            {
                string day = Common.CommFunctions.GetMonthName(dTemp.Month, false) + "." + dTemp.Day;

                var result = from a in modelList where a.Day == day select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = "Online";
                    model.Day = day;
                    model.Output = 0;
                    modelList.Add(model);
                }

                dTemp = dTemp.AddDays(1);
            }

            return (from a in modelList orderby a.Day ascending select a).ToList();
        }


        public List<Home_Model.DailyTrend> GetPQCWIPDaily(SearchingCondition.BaseCondition cond)
        {
            DataTable dt = _dal.GetPQCWIPDaily(cond);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();

                model.Department = "WIP";
                DateTime day = DateTime.Parse(dr["day"].ToString());
                model.Day = Common.CommFunctions.GetMonthName(day.Month, false) + "." + day.Day.ToString();
                model.Output = decimal.Parse(dr["output"].ToString());

                modelList.Add(model);
            }


            DateTime dTemp = cond.DateFrom.Value.Date;
            while (dTemp < cond.DateTo.Value.Date)
            {
                string day = Common.CommFunctions.GetMonthName(dTemp.Month, false) + "." + dTemp.Day;

                var result = from a in modelList where a.Day == day select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = "WIP";
                    model.Day = day;
                    model.Output = 0;
                    modelList.Add(model);
                }

                dTemp = dTemp.AddDays(1);
            }

            return (from a in modelList orderby a.Day ascending select a).ToList();
        }


        public List<Home_Model.DailyTrend> GetPQCPackingDaily(SearchingCondition.BaseCondition cond)
        {
            DataTable dt = _dal.GetPQCPackingDaily(cond);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Home_Model.DailyTrend> modelList = new List<Home_Model.DailyTrend>();

            foreach (DataRow dr in dt.Rows)
            {
                Home_Model.DailyTrend model = new Home_Model.DailyTrend();

                model.Department = "Packing";
                DateTime day = DateTime.Parse(dr["day"].ToString());
                model.Day = Common.CommFunctions.GetMonthName(day.Month, false) + "." + day.Day.ToString();
                model.Output = decimal.Parse(dr["output"].ToString());

                modelList.Add(model);
            }


            DateTime dTemp = cond.DateFrom.Value.Date;
            while (dTemp < cond.DateTo.Value.Date)
            {
                string day = Common.CommFunctions.GetMonthName(dTemp.Month, false) + "." + dTemp.Day;

                var result = from a in modelList where a.Day == day select a;
                if (result == null || result.Count() == 0)
                {
                    Home_Model.DailyTrend model = new Home_Model.DailyTrend();
                    model.Department = "Packing";
                    model.Day = day;
                    model.Output = 0;
                    modelList.Add(model);
                }

                dTemp = dTemp.AddDays(1);
            }

            return (from a in modelList orderby a.Day ascending select a).ToList();
        }


        public List<Home_Model.DailyTrend> GetDailyTrend(SearchingCondition.BaseCondition cond)
        {
            List<Home_Model.DailyTrend> all = new List<Home_Model.DailyTrend>();


            all.AddRange(GetMouldingDaily(cond));
            all.AddRange(GetLaserDaily(cond));

            all.AddRange(GetPQCOnlineDaily(cond));
            all.AddRange(GetPQCWIPDaily(cond));
            all.AddRange(GetPQCPackingDaily(cond));



            return all;
        }



    }
}
