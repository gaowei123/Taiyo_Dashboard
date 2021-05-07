using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtendClass.PQCProduction.ScrapReport
{
    public class Scrap_BLL
    {
        private readonly Core.Base_BLL _baseBLL = new Core.Base_BLL();

        // bin history 和 bin 表基本一致
        // 偷懒直接拿来用了.
        public List<Core.BaseBin_Model> GetScrapList(Taiyo.SearchParam.PQCParam.PQCOutputParam param)
        {
            return _baseBLL.GetBinHisScrapList(param);
        }

    }
}
