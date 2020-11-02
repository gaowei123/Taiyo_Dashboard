using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCOperatorPerformanceChart
{
    public class BLL
    {
        private readonly DAL _dal = new DAL();

        public List<Model> GetOpList(SearchingCondition.BaseCondition condition)
        {
            List<Model> list = _dal.GetOpList(condition);
            if (list ==null )
                return null;

            return list.OrderBy(p => p.UserID).ToList();
        }

    }
}
