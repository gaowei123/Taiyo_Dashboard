using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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

        


    }
}
