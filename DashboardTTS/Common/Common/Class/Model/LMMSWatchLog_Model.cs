
using System;
namespace Common.Model
{
	/// <summary>
	/// LMMSWatchLog_Model:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSWatchLog_Model
	{
		public LMMSWatchLog_Model()
		{}
		#region Model
		private int _id;
		private DateTime? _datetime;
		private string _machineid;
		private string _partnumber;
		private string _jobnumber;
		private string _description;
        private string _lotNo;
		private int? _currentquantity;
		private int? _totalquantity;
		private string _currentoperation;
		private string _prepdateout;
		private string _prepdatein;
		private string _paintdateout;
		private string _paintdatein;
		private DateTime? _laserdateout;
		private DateTime? _laserdatein;
		private DateTime? _productdateout;
		private DateTime? _productdatein;
		private string _ownerid;
		private string _laserfilea;
		private string _laserfileb;
		private int? _totalpass;
		private int? _totalfail;
		private DateTime? _lastupdated;
		private DateTime? _starttime;
		private DateTime? _stoptime;
		private string _rmsstatus;
		private string _status;
		private int? _paintquantity;
		private int? _pqcquantity;
		private int? _laserdefectquantity;
		private int? _paintdefectquantity;
		private int? _moulddefectquantity;
		private string _modelname;
		private int? _currenttotalpass;
		private int? _currenttotalfail;

        private string _model1name;
        private string _model2name;
        private string _model3name;
        private string _model4name;
        private string _model5name;
        private string _model6name;
        private string _model7name;
        private string _model8name;
        private string _model9name;
        private string _model10name;
        private string _model11name;
        private string _model12name;
        private string _model13name;
        private string _model14name;
        private string _model15name;
        private string _model16name;
        

        private int? _ok1count;
        private int? _ok2count;
        private int? _ok3count;
        private int? _ok4count;
        private int? _ok5count;
        private int? _ok6count;
        private int? _ok7count;
        private int? _ok8count;
        private int? _ok9count;
        private int? _ok10count;
        private int? _ok11count;
        private int? _ok12count;
        private int? _ok13count;
        private int? _ok14count;
        private int? _ok15count;
        private int? _ok16count;
        

        private int? _ng1count;
        private int? _ng2count;
        private int? _ng3count;
        private int? _ng4count;
        private int? _ng5count;
        private int? _ng6count;
        private int? _ng7count;
        private int? _ng8count;
        private int? _ng9count;
        private int? _ng10count;
        private int? _ng11count;
        private int? _ng12count;
        private int? _ng13count;
        private int? _ng14count;
        private int? _ng15count;
        private int? _ng16count;


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
		public DateTime? dateTime
		{
			set{ _datetime=value;}
			get{return _datetime;}
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
		public string partNumber
		{
			set{ _partnumber=value;}
			get{return _partnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string jobNumber
		{
			set{ _jobnumber=value;}
			get{return _jobnumber;}
		}
        /// <summary>
        /// 
        /// </summary>
        /// 

        public string lotNo
        {
            set { _lotNo = value; }
            get { return _lotNo; }
        }
        public string description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? currentQuantity
		{
			set{ _currentquantity=value;}
			get{return _currentquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? totalQuantity
		{
			set{ _totalquantity=value;}
			get{return _totalquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string currentOperation
		{
			set{ _currentoperation=value;}
			get{return _currentoperation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string prepDateOut
		{
			set{ _prepdateout=value;}
			get{return _prepdateout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string prepDateIn
		{
			set{ _prepdatein=value;}
			get{return _prepdatein;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string paintDateOut
		{
			set{ _paintdateout=value;}
			get{return _paintdateout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string paintDateIn
		{
			set{ _paintdatein=value;}
			get{return _paintdatein;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? laserDateOut
		{
			set{ _laserdateout=value;}
			get{return _laserdateout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? laserDateIn
		{
			set{ _laserdatein=value;}
			get{return _laserdatein;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? productDateOut
		{
			set{ _productdateout=value;}
			get{return _productdateout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? productDateIn
		{
			set{ _productdatein=value;}
			get{return _productdatein;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ownerID
		{
			set{ _ownerid=value;}
			get{return _ownerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string laserFileA
		{
			set{ _laserfilea=value;}
			get{return _laserfilea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string laserFileB
		{
			set{ _laserfileb=value;}
			get{return _laserfileb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? totalPass
		{
			set{ _totalpass=value;}
			get{return _totalpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? totalFail
		{
			set{ _totalfail=value;}
			get{return _totalfail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lastUpdated
		{
			set{ _lastupdated=value;}
			get{return _lastupdated;}
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
		public string rmsStatus
		{
			set{ _rmsstatus=value;}
			get{return _rmsstatus;}
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
		public int? paintQuantity
		{
			set{ _paintquantity=value;}
			get{return _paintquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? pqcQuantity
		{
			set{ _pqcquantity=value;}
			get{return _pqcquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? laserDefectQuantity
		{
			set{ _laserdefectquantity=value;}
			get{return _laserdefectquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? paintDefectQuantity
		{
			set{ _paintdefectquantity=value;}
			get{return _paintdefectquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? mouldDefectQuantity
		{
			set{ _moulddefectquantity=value;}
			get{return _moulddefectquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string modelName
		{
			set{ _modelname=value;}
			get{return _modelname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? currentTotalPass
		{
			set{ _currenttotalpass=value;}
			get{return _currenttotalpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? currentTotalFail
		{
			set{ _currenttotalfail=value;}
			get{return _currenttotalfail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string model1Name
		{
			set{ _model1name=value;}
			get{return _model1name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string model2Name
		{
			set{ _model2name=value;}
			get{return _model2name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string model3Name
		{
			set{ _model3name=value;}
			get{return _model3name;}
		}
        public string model4Name
        {
            set { _model4name = value; }
            get { return _model4name; }
        }
        public string model5Name
        {
            set { _model5name = value; }
            get { return _model5name; }
        }
        public string model6Name
        {
            set { _model6name = value; }
            get { return _model6name; }
        }
        public string model7Name
        {
            set { _model7name = value; }
            get { return _model7name; }
        }
        public string model8Name
        {
            set { _model8name = value; }
            get { return _model8name; }
        }
        public string model9Name
        {
            set { _model9name = value; }
            get { return _model9name; }
        }
        public string model10Name
        {
            set { _model10name = value; }
            get { return _model10name; }
        }
        public string model11Name
        {
            set { _model11name = value; }
            get { return _model11name; }
        }
        public string model12Name
        {
            set { _model12name = value; }
            get { return _model12name; }
        }
        public string model13Name
        {
            set { _model13name = value; }
            get { return _model13name; }
        }
        public string model14Name
        {
            set { _model14name = value; }
            get { return _model14name; }
        }
        public string model15Name
        {
            set { _model15name = value; }
            get { return _model15name; }
        }
        public string model16Name
        {
            set { _model16name = value; }
            get { return _model16name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ok1Count
		{
			set{ _ok1count=value;}
			get{return _ok1count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ok2Count
		{
			set{ _ok2count=value;}
			get{return _ok2count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ok3Count
		{
			set{ _ok3count=value;}
			get{return _ok3count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ng1Count
		{
			set{ _ng1count=value;}
			get{return _ng1count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ng2Count
		{
			set{ _ng2count=value;}
			get{return _ng2count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ng3Count
		{
			set{ _ng3count=value;}
			get{return _ng3count;}
		}

        public int? ok4Count
        {
            set { _ok4count = value; }
            get { return _ok4count; }
        }

        public int? ng4Count
        {
            set { _ng4count = value; }
            get { return _ng4count; }
        }
        public int? ok5Count
        {
            set { _ok5count = value; }
            get { return _ok5count; }
        }

        public int? ng5Count
        {
            set { _ng5count = value; }
            get { return _ng5count; }
        }
        public int? ok6Count
        {
            set { _ok6count = value; }
            get { return _ok6count; }
        }

        public int? ng6Count
        {
            set { _ng6count = value; }
            get { return _ng6count; }
        }
        public int? ok7Count
        {
            set { _ok7count = value; }
            get { return _ok7count; }
        }

        public int? ng7Count
        {
            set { _ng7count = value; }
            get { return _ng7count; }
        }
        public int? ok8Count
        {
            set { _ok8count = value; }
            get { return _ok8count; }
        }

        public int? ng8Count
        {
            set { _ng8count = value; }
            get { return _ng8count; }
        }
        public int? ok9Count
        {
            set { _ok9count = value; }
            get { return _ok9count; }
        }

        public int? ng9Count
        {
            set { _ng9count = value; }
            get { return _ng9count; }
        }

        public int? ok10Count
        {
            set { _ok10count = value; }
            get { return _ok10count; }
        }

        public int? ng10Count
        {
            set { _ng10count = value; }
            get { return _ng10count; }
        }
        public int? ok11Count
        {
            set { _ok11count = value; }
            get { return _ok11count; }
        }

        public int? ng11Count
        {
            set { _ng11count = value; }
            get { return _ng11count; }
        }

        public int? ok12Count
        {
            set { _ok12count = value; }
            get { return _ok12count; }
        }

        public int? ng12Count
        {
            set { _ng12count = value; }
            get { return _ng12count; }
        }

        public int? ok13Count
        {
            set { _ok13count = value; }
            get { return _ok13count; }
        }

        public int? ng13Count
        {
            set { _ng13count = value; }
            get { return _ng13count; }
        }


        public int? ok14Count
        {
            set { _ok14count = value; }
            get { return _ok14count; }
        }

        public int? ng14Count
        {
            set { _ng14count = value; }
            get { return _ng14count; }
        }
        public int? ok15Count
        {
            set { _ok15count = value; }
            get { return _ok15count; }
        }

        public int? ng15Count
        {
            set { _ng15count = value; }
            get { return _ng15count; }
        }
        public int? ok16Count
        {
            set { _ok16count = value; }
            get { return _ok16count; }
        }

        public int? ng16Count
        {
            set { _ng16count = value; }
            get { return _ng16count; }
        }






        #endregion Model

    }
}

