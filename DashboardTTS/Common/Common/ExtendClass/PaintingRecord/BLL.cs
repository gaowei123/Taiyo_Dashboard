using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PaintingParam;

namespace Common.ExtendClass.PaintingRecord
{
    public class BLL
    {
        DAL _dal;
        public BLL()
        {
            _dal = new DAL();
        }


        public List<Model> GetList(DeliveryRecordParam param)
        {
            return _dal.GetList(param);
        }
    }
}
