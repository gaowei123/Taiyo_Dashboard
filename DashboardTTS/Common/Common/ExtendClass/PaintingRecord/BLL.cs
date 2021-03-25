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
            var result = _dal.GetList(param);
            if (result == null) return null;

            //order by a.partNumber asc, a.updatedTime desc
            return result.OrderBy(model => model.PartNo).OrderByDescending(model => model.S_ScanDate).ToList();
        }
    }
}
