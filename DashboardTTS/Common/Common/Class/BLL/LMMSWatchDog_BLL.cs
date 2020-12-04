using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Class.BLL
{
  public  class LMMSWatchDog_BLL
    {

        private readonly DAL.LMMSWatchDog_DAL _dal = new DAL.LMMSWatchDog_DAL();


        public bool IsJobRunning(string sJobNo)
        {
            DataTable dt = _dal.GetList(sJobNo, string.Empty);
            return dt == null || dt.Rows.Count == 0 ? false : true;
        }


        public DataTable GetMaterialList(DateTime? dStartTime)
        {
            return  _dal.GetMaterialList(dStartTime);
        }

    }
}
