using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Taiyo.Tool.Extension;

namespace Taiyo.Tool
{
    public static class Common
    {
        /// <summary>
        /// 通过json将任意对象转换成指定类型的list
        /// 注意 属性名称必须匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> ConvertType<T>(object obj)
        {
            List<T> resultList = default(List<T>);
            
            try
            {
                string strJson = JsonConvert.SerializeObject(obj);
                resultList = JsonConvert.DeserializeObject<List<T>>(strJson);
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return resultList;
        }

        public static Taiyo.Enum.CommonEnum.Shift GetCurrentShift()
        {
            // 8:00 - 20:00 是 day.
            bool isDay = DateTime.Now >= DateTime.Now.Date.AddHours(8) && DateTime.Now < DateTime.Now.Date.AddHours(20);

            return isDay ? Taiyo.Enum.CommonEnum.Shift.Day : Taiyo.Enum.CommonEnum.Shift.Night;
        }


    }
}
