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

                        //命名空间.类型名
            string name = "MyChart.ConcreteChartMethod." + sChartName;


            //通过assembly 动态创建实例    
            object obj = Assembly.GetExecutingAssembly().CreateInstance(name, true, System.Reflection.BindingFlags.Default, null, null, null, null);

            IChartMethod chart = (IChartMethod)obj;
            return chart;
        }
    }
}
