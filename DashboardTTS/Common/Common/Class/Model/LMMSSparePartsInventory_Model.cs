using System;

namespace Common.Class.Model
{
	/// <summary>
	/// LMMSSparePartsInventory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSSparePartsInventory_Model
	{
		public LMMSSparePartsInventory_Model()
		{}
		#region Model
		private string _sparepartsname;
		private decimal? _quantity;
		private string _partstype;
		private string _supplier;
		private decimal? _unitprice;
		private string _department;
		private DateTime? _lastupdatedtime;
		private DateTime? _createdtime;
		private string _lastupdatedby;
        private string _action;
        private string _machineID;
        private decimal? _usage;

        public string action
        {
            set { _action = value; }
            get { return _action; }
        }

        public string machineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }

        public decimal? usage
        {
            set { _usage = value; }
            get { return _usage; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string sparePartsName
		{
			set{ _sparepartsname=value;}
			get{return _sparepartsname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string partsType
		{
			set{ _partstype=value;}
			get{return _partstype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string supplier
		{
			set{ _supplier=value;}
			get{return _supplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? unitPrice
		{
			set{ _unitprice=value;}
			get{return _unitprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lastUpdatedTime
		{
			set{ _lastupdatedtime=value;}
			get{return _lastupdatedtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createdTime
		{
			set{ _createdtime=value;}
			get{return _createdtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lastUpdatedBy
		{
			set{ _lastupdatedby=value;}
			get{return _lastupdatedby;}
		}
		#endregion Model

	}
}

