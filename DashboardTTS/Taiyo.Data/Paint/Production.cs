using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Data.Core;
using System.Data.SqlClient;
using Taiyo.Tool;
using Taiyo.Data.Query;

namespace Taiyo.Data.Paint
{
    public class Production
    {
     

        public Production()
        {
            // 构造时初始化下, 以免直接调用时报错null exception
            LotList = new List<LotInfo>();
            
        }


        #region lot info

        public class LotInfo
        {
            public string LotNo { get; set; }
            public string JobNo { get; set; }
            public DateTime? MFGDate { get; set; }
            public decimal? MrpQty { get; set; }
        }

        public List<LotInfo> LotList { get; set; }
        
        
        public List<LotInfo> GetLotList(PaintQuery.Delivery querys)
        {
            Core.PaintDelivery delivery = new PaintDelivery(querys);
            var result = from a in delivery.DeliveryList
                         select new
                         {
                             LotNo = a.LotNo,
                             JobNo = a.JobNo,
                             MFGDate = a.MFGDate,
                             MrpQty = a.MrpQty
                         };

            return Common.ConvertType<LotInfo>(result);
        }

        public LotInfo GetLot(string sJobNo)
        {
            var list = GetLotList(new PaintQuery.Delivery() {
                JobNo = sJobNo
            });

            return list == null || list.Count() == 0? null: list.FirstOrDefault();
        }
        #endregion


    

        #region buyoff



        #endregion







    }
}
