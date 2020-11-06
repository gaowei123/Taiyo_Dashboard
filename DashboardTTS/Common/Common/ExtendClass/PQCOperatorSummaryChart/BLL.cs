using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCOperatorSummaryChart
{
    public class BLL
    {
        private readonly DAL _dal = new DAL();
        public List<Model> GetDataList(Taiyo.SearchParam.PQCParam.PQCOperatorSummaryCondition param)
        {

            return _dal.GetDailyList(param);
        }

        
    }
}