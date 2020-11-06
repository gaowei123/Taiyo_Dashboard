using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserProductionChart
{
    public class LaserProduction_BLL
    {
        private LaserProduction_DAL _dal = new LaserProduction_DAL();

        public  List<LaserProduction_Model> GetProductionList(Taiyo.SearchParam.LaserParam.LaserProductionCondition param)
        {
            return _dal.GetProduction(param);
        }

        

    }
}
