using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiyo.Business
{
    public class PaintDelivery_BLL
    {
        private Taiyo.Data.Core.PaintDelivery _delivery = new Data.Core.PaintDelivery();
        public string GetDeliveryList(DateTime dateFrom, DateTime dateTo, string partNo, string sendingTo, string jobNo)
        {
            var objQuerys = new Data.Query.PaintQuery.Delivery()
            {
                DateFrom = dateFrom,
                DateTo = dateTo.AddDays(1),
                PartNo = partNo,
                SendingTo = sendingTo,
                JobNo = jobNo
            };

            var list = new Taiyo.Data.Core.PaintDelivery(objQuerys).DeliveryList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}
