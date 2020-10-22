using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyChart
{
    public class ChartFactory
    {
        public  MyChart.IChartMethod CreateInstance(string sChartName)
        {
            string name = "MyChart.ConcreteChartMethod." + sChartName;


            //加载程序集，创建程序集里面的 命名空间.类型名 实例
            object obj = Assembly.GetExecutingAssembly().CreateInstance(name, true, System.Reflection.BindingFlags.Default, null, null, null, null);

            IChartMethod chart = (IChartMethod)obj;
            return chart;
        }
    }
}
