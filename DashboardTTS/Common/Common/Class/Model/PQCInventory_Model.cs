using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public partial class PQCInventory_Model
    {
        public PQCInventory_Model()
        { }
        #region Model
        private string _lotno;
        private string _jobnumber;
        private string _partnumber;
        private decimal? _inmrpquantity;
        private DateTime _mfgdate;
        private string _remark;
        private DateTime _updatedtime;
        private string _inventorytype;
        private string _checkProcess;
        private int _shortage;
        /// <summary>
        /// 
        /// </summary>
        public string LotNo
        {
            set { _lotno = value; }
            get { return _lotno; }
        }

        public string CheckProcess
        {
            set { _checkProcess = value; }
            get { return _checkProcess; }
        }

        public int shortage
        {
            set { _shortage = value; }
            get { return _shortage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobNumber
        {
            set { _jobnumber = value; }
            get { return _jobnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PartNumber
        {
            set { _partnumber = value; }
            get { return _partnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InMRPQuantity
        {
            set { _inmrpquantity = value; }
            get { return _inmrpquantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime MFGDate
        {
            set { _mfgdate = value; }
            get { return _mfgdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedTime
        {
            set { _updatedtime = value; }
            get { return _updatedtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InventoryType
        {
            set { _inventorytype = value; }
            get { return _inventorytype; }
        }
        #endregion Model

    }
}
