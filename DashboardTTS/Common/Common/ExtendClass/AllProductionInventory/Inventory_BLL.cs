using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExtendClass.AllProductionInventory
{
    public  class Inventory_BLL
    {
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL paintDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.BLL.LMMSWatchLog_BLL watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
        private readonly Common.Class.BLL.PQCQaViDetailTracking_BLL detialTrackingBLL = new Common.Class.BLL.PQCQaViDetailTracking_BLL();
        private readonly Common.Class.BLL.PQCQaViBinning pqcBinBLL = new Common.Class.BLL.PQCQaViBinning();
        private readonly Common.Class.BLL.PQCBom_BLL pqcBomBLL = new Common.Class.BLL.PQCBom_BLL();
        private readonly Common.Class.BLL.PQCQaViTracking_BLL viTrackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
        private readonly Common.Class.BLL.PQCInventory_BLL pqcInventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
        private readonly Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
        private readonly Common.Class.BLL.PaintingTempInfo paintTempBLL = new Common.Class.BLL.PaintingTempInfo();




    }
}
