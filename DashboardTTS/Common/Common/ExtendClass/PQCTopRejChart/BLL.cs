using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCTopRejChart
{
    public class BLL
    {
        private readonly DAL _dal = new DAL();

        
        public List<Model.TopPartNo> GetPartNoList(SearchingCondition.PQCTopRejectCondition condition)
        {
            return _dal.GetTopPartNoRej(condition);
        }



        public List<Model.TopDefect> GetDefectList(SearchingCondition.PQCTopRejectCondition condition)
        {
            return _dal.GetTopDefectRej(condition);
        }


    }
}
