using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Data.Query;

namespace Taiyo.Data.Core
{
    public class PaintDataCore : IPaintDataCore
    {
        public List<PaintBuyoff.Buyoff_Model> GetBuyoffList(PaintQuery.Buyoff querys)
        {
            throw new NotImplementedException();
        }

        public List<PaintDelivery.Delivery_Model> GetDeliveryList(PaintQuery.Delivery querys)
        {
            throw new NotImplementedException();
        }
    }
}
