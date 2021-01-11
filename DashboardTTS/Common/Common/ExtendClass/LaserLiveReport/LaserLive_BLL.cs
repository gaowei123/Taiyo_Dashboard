using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.LaserLiveReport
{
    public class LaserLive_BLL
    {
        private readonly LaserLive_DAL _dal;
        public LaserLive_BLL()
        {
            _dal = new LaserLive_DAL();
        }


        public List<LaserLive_Model> GetList(Taiyo.SearchParam.LaserParam.LaserLiveParam param)
        {
            return _dal.GetList(param);
        }
    }
}
