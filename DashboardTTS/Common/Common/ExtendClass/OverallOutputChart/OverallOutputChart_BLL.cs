using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Taiyo.SearchParam;

namespace Common.ExtendClass.OverallOutputChart
{
    public class OverallOutputChart_BLL
    {
        private readonly OverallOutputChart_DAL _dal;
        public OverallOutputChart_BLL()
        {
            _dal = new OverallOutputChart_DAL();
        }

        
        public List<OverallOutputChart_Model> GetDataList(BaseParam param)
        {
            var mould = _dal.GetMouldOutput(param);
            var paint = _dal.GetPaintOutput(param);
            var laser = _dal.GetLaserOutput(param);
            var pqcCheck = _dal.GetCheckingOutput(param);
            var pacPack = _dal.GetPackingOutput(param);

            var result = new List<OverallOutputChart_Model>();
            result.Add(mould);
            result.Add(paint);
            result.Add(laser);
            result.AddRange(pqcCheck);
            result.AddRange(pacPack);

            return result;
        }
    }
}
