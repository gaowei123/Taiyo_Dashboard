using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Class.Model
{
    public class MouldingDeliveryHis_Model
    {

        /// <summary>
		/// jobNumber
        /// </summary>		
		private string _jobnumber;
        public string jobNumber
        {
            get { return _jobnumber; }
            set { _jobnumber = value; }
        }
        /// <summary>
        /// partNumber
        /// </summary>		
        private string _partnumber;
        public string partNumber
        {
            get { return _partnumber; }
            set { _partnumber = value; }
        }
        /// <summary>
        /// deliveryTo
        /// </summary>		
        private string _sendingTo;
        public string sendingTo
        {
            get { return _sendingTo; }
            set { _sendingTo = value; }
        }
        /// <summary>
        /// inQuantity
        /// </summary>		
        private decimal _inquantity;
        public decimal inQuantity
        {
            get { return _inquantity; }
            set { _inquantity = value; }
        }
        /// <summary>
        /// lotNo
        /// </summary>		
        private string _lotno;
        public string lotNo
        {
            get { return _lotno; }
            set { _lotno = value; }
        }
        /// <summary>
        /// boxQty
        /// </summary>		
        private decimal _boxqty;
        public decimal boxQty
        {
            get { return _boxqty; }
            set { _boxqty = value; }
        }
        /// <summary>
        /// remark
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// dateTime
        /// </summary>		
        private DateTime? _datetime;
        public DateTime? dateTime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }
        /// <summary>
        /// updatedTime
        /// </summary>		
        private DateTime _updatedtime;
        public DateTime updatedTime
        {
            get { return _updatedtime; }
            set { _updatedtime = value; }
        }

        private string _signID;
        public string SignID
        {
            get { return _signID; }
            set { _signID = value; }
        }

    }
}
