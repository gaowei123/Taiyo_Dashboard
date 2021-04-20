using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiyo.Data.Core
{
    public interface IPaintDataCore
    {
        /// <summary>
        /// 获取PaintingDeliveryHis表中的数据.
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        List<Core.PaintDelivery.Delivery_Model> GetDeliveryList(Query.PaintQuery.Delivery querys);

        /// <summary>
        /// 获取PaintingTempInfo表中的数据
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        List<PaintBuyoff.Buyoff_Model> GetBuyoffList(Query.PaintQuery.Buyoff querys);



    }
}
