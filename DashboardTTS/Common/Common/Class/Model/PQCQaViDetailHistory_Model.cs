 
using System;
namespace Common.Model
{
    /// <summary>
    /// PQCQaViDetailHistory_Model:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
	public partial class PQCQaViDetailHistory_Model
    {
		public PQCQaViDetailHistory_Model()
		{}
		#region Model
		private int _id;
        private string _jobid;
        private string _trackingid;
		private string _machineid;
		private DateTime? _datetime;
		private string _materialpartno;
		private string _jigno;
		private string _model;
        private string _color;
        private decimal? _cavitycount;
		private string _username;
		private string _userid;
		private DateTime? _starttime;
		private DateTime? _stoptime;
		private DateTime? _day;
		private string _shift;
		private string _status;
		private string _remark_1;
		private string _remark_2;
        private decimal? _totalqty;
        private decimal? _totalpassqty;
        private decimal? _totalrejectqty;
        private decimal? _passqty;
        private decimal? _rejectqty;
		private string _rejectqtyhour01;
		private string _rejectqtyhour02;
		private string _rejectqtyhour03;
		private string _rejectqtyhour04;
		private string _rejectqtyhour05;
		private string _rejectqtyhour06;
		private string _rejectqtyhour07;
		private string _rejectqtyhour08;
		private string _rejectqtyhour09;
		private string _rejectqtyhour10;
		private string _rejectqtyhour11;
		private string _rejectqtyhour12;
		private DateTime? _lastupdatedtime;
		private string _remarks;
        private string _processes;
        private DateTime? _updated_time;
        private string _shipto;
        private string _materialName;
        private string _customer;
        private decimal _outerBoxQty;
        private string _packingTrays;
        private string _module;
        /// <summary>
        /// 
        /// </summary>
        public int id
		{
			set{ _id=value;}
			get{return _id;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string jobid
        {
            set { _jobid = value; }
            get { return _jobid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string trackingID
		{
			set{ _trackingid=value;}
			get{return _trackingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string machineID
		{
			set{ _machineid=value;}
			get{return _machineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dateTime
		{
			set{ _datetime=value;}
			get{return _datetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string materialPartNo
        {
			set{ _materialpartno = value;}
			get{return _materialpartno; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string jigNo
		{
			set{ _jigno=value;}
			get{return _jigno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string model
		{
			set{ _model=value;}
			get{return _model;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string color
        {
            set { _color = value; }
            get { return _color; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? cavityCount
		{
			set{ _cavitycount=value;}
			get{return _cavitycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? startTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? stopTime
		{
			set{ _stoptime=value;}
			get{return _stoptime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? day
		{
			set{ _day=value;}
			get{return _day;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shift
		{
			set{ _shift=value;}
			get{return _shift;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark_1
        {
			set{ _remark_1=value;}
			get{return _remark_1; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark_2
        {
			set{ _remark_2 = value;}
			get{return _remark_2; }
		}
        /// <summary>
        /// 
        /// </summary>
        public decimal? totalPassQty
        {
            set { _totalpassqty = value; }
            get { return _totalpassqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? totalRejectQty
        {
            set { _totalrejectqty = value; }
            get { return _totalrejectqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? rejectQty
		{
			set{ _rejectqty=value;}
			get{return _rejectqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour01
		{
			set{ _rejectqtyhour01=value;}
			get{return _rejectqtyhour01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour02
		{
			set{ _rejectqtyhour02=value;}
			get{return _rejectqtyhour02;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour03
		{
			set{ _rejectqtyhour03=value;}
			get{return _rejectqtyhour03;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour04
		{
			set{ _rejectqtyhour04=value;}
			get{return _rejectqtyhour04;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour05
		{
			set{ _rejectqtyhour05=value;}
			get{return _rejectqtyhour05;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour06
		{
			set{ _rejectqtyhour06=value;}
			get{return _rejectqtyhour06;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour07
		{
			set{ _rejectqtyhour07=value;}
			get{return _rejectqtyhour07;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour08
		{
			set{ _rejectqtyhour08=value;}
			get{return _rejectqtyhour08;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour09
		{
			set{ _rejectqtyhour09=value;}
			get{return _rejectqtyhour09;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour10
		{
			set{ _rejectqtyhour10=value;}
			get{return _rejectqtyhour10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour11
		{
			set{ _rejectqtyhour11=value;}
			get{return _rejectqtyhour11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQtyHour12
		{
			set{ _rejectqtyhour12=value;}
			get{return _rejectqtyhour12;}
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
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string processes
        {
            set { _processes = value; }
            get { return _processes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? totalQty
        {
            set { _totalqty = value; }
            get { return _totalqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? passQty
        {
            set { _passqty = value; }
            get { return _passqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updatedTime
        {
            set { _updated_time = value; }
            get { return _updated_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shipTo
        {
            set { _shipto = value; }
            get { return _shipto; }
        }
        public string materialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }
        public string customer
        {
            get { return _customer; }
            set { _customer = value; }
        }
        public decimal outerBoxQty
        {
            get { return _outerBoxQty; }
            set { _outerBoxQty = value; }
        }
        public string packingTrays
        {
            get { return _packingTrays; }
            set { _packingTrays = value; }
        }
        public string module
        {
            get { return _module; }
            set { _module = value; }
        }
        #endregion Model

    }
}

