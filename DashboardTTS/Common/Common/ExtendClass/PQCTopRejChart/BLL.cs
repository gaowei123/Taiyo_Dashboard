using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.PQCTopRejChart
{
    public class BLL
    {
        private readonly DAL _dal = new DAL();

        
        public List<Model.TopPartNo> GetPartNoList(Taiyo.SearchParam.PQCParam.PQCTopRejectCondition param)
        {
            return _dal.GetTopPartNoRej(param);
        }



        public List<Model.TopDefect> GetDefectList(Taiyo.SearchParam.PQCParam.PQCTopRejectCondition param)
        {
            return _dal.GetTopDefectRej(param);
        }


    }
}
