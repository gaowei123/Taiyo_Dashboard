using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class Base_BLL
    {
        private readonly Base_DAL _dal;
        public Base_BLL()
        {
            _dal = new Base_DAL();
        }


        public List<BaseVI_Model> GetViList(PQCOperatorParam param)
        {
            var checkViList = _dal.GetCheckViList(param);
            var packViList = _dal.GetPackViList(param);

            List<BaseVI_Model> result = new List<BaseVI_Model>();

            if (checkViList != null)
                result.AddRange(checkViList);

            if (packViList != null)
                result.AddRange(packViList);

            
            return result;
        }

        public List<BaseVI_Model> GetCheckingList(PQCOperatorParam param)
        {
            return _dal.GetCheckViList(param);           
        }

        public List<BaseDefect_Model> GetDefectList(PQCOperatorParam param)
        {
            return _dal.GetDefectList(param);
        }

        public List<BaseLotInfo_Model> GetLotInfoList(PQCOperatorParam param)
        {
            return _dal.GetLotInfoList(param);
        }

    }
}
