using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;

namespace Common.ExtendClass.PQCTopRejChart
{
    public class BLL
    {
        private readonly DAL _dal;

        public BLL()
        {
            _dal = new DAL();
        }

        
        public List<Model.TopPartNo> GetPartNoList(PQCTopRejectCondition param)
        {
            return _dal.GetTopPartNoRej(param);
        }



        public List<Model.TopDefect> GetDefectList(PQCTopRejectCondition param)
        {
            return _dal.GetTopDefectRej(param);
        }


    }
}
