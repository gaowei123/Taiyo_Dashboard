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
            if (sChartName == "LaserMachine")
            {
                return new ConcreteChartMethod.LaserMachine();               
            }
            else if (sChartName == "LaserProduction")
            {
                return new ConcreteChartMethod.LaserProduction();
            }
            else if (sChartName == "PQCProduction")
            {
                return new ConcreteChartMethod.PQCProduction();
            }
            else if (sChartName == "PQCOperator")
            {
                return new ConcreteChartMethod.PQCOperator();
            }
            else if (sChartName == "Activity")
            {
                return new ConcreteChartMethod.LaserMachineActivity();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
