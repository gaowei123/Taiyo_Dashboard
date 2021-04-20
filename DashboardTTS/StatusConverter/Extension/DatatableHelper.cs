using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Taiyo.Tool.Extension
{
    public static class DatatableHelper
    {
        /// <summary>
        /// 将datatable转换成对应model的集合
        /// 必须注意, sql语句中的字段名一定要和model中的命名一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            // 0.防呆
            if (dt == null || dt.Rows.Count == 0)
                return null;
            

            // 1.定义集合,收集转换后的model
            List<T> newList = new List<T>();
            

            // 2.遍历table的每一行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();


                // 4.遍历要转换对象的每一个属性
                System.Reflection.PropertyInfo[] props = t.GetType().GetProperties();
                foreach (var pi in props)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        // 判断属性是否可写
                        if (!pi.CanWrite)
                            continue;

                        // 获取该属性对应dr中的值
                        object value = dr[pi.Name];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }

                newList.Add(t);
            }
            

            return newList;
        }


    }
}
