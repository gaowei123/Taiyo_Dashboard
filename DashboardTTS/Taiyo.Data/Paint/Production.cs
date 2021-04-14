using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Data.Core;
using System.Data.SqlClient;
using Taiyo.Tool;

namespace Taiyo.Data.Paint
{
    public class Production
    {
        
        #region lot info

        public class LotInfo
        {
            public string LotNo { get; set; }
            public string JobNo { get; set; }
            public DateTime? MFGDate { get; set; }
            public decimal? MrpQty { get; set; }
        }
        
        public List<LotInfo> GetLotList(List<string> queryList, SqlParameter[] parameters)
        {
            Core.Paint_Delivery _delivery = new Paint_Delivery(queryList, parameters);
            var result = from a in _delivery.DeliveryList
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
      
            List<string> querylist = new List<string>();
            querylist.Add(" and jobnumber = @jobnumber");

            
            SqlParameter[] parameters =
            {
                new SqlParameter("@jobnumber", System.Data.SqlDbType.VarChar,32)
            };
            parameters[0].Value = sJobNo;



            var list = GetLotList(querylist, parameters);
            if (list == null || list.Count() == 0)
                return null;
            else
                return list.FirstOrDefault();

        }
        
        #endregion


    
        

    }
}
