using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam;

namespace Common.ExtendClass.PQCOperatorPerformanceChart
{
    public class BLL
    {
        private readonly DAL _dal = new DAL();

        public List<Model> GetOpList(BaseParam param)
        {
            List<Model> list = _dal.GetOpList(param);
            if (list ==null )
                return null;

            return list.OrderBy(p => p.UserID).ToList();
        }

    }
}
