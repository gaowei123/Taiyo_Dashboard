using System;
namespace Common.Class.Model
{
	/// <summary>
	/// PaintingTempInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PaintingTempInfo_Model
	{
		public PaintingTempInfo_Model()
		{}

		#region Model
		private string _jobnumber;
		private string _lotno;
		private string _partnumber;
		private string _materialname;
		private decimal? _lotqty;
		private decimal? _setuprejqty;
		private decimal? _qatestqty;
		private string _coat_1st;
		private string _pmachine_1st;
		private string _coat_2nd;
		private string _pmachine_2nd;
		private string _coat_3rd;
		private string _pmachine_3rd;
		private DateTime? _paintingdate;
		private DateTime? _ucdate;
		private DateTime? _tcdate;
		private string _lasermachine;
		private string _laseroperator;
		private DateTime? _laserdate;
		private DateTime? _mfgdate;
		private DateTime? _createdtime;
		private DateTime? _updatedtime;
        private string _recordBy;

        private DateTime? _paintingDate_1st;
        private DateTime? _paintingDate_2nd;
        private DateTime? _paintingDate_3rd;

        private decimal? _thickness_1st;
        private decimal? _thickness_2nd;
        private decimal? _thickness_3rd;




        private string _paintLot_1st;
        private string _paintLot_2nd;
        private string _paintLot_3rd;
        private string _thinnersLot_1st;
        private string _thinnersLot_2nd;
        private string _thinnersLot_3rd;
        private string _paintingPIC_1st;
        private string _paintingPIC_2nd;
        private string _paintingPIC_3rd;
        private string _paintingRunningTime_1st;
        private string _paintingRunningTime_2nd;
        private string _paintingRunningTime_3rd;
        private string _paintingOvenTime_1st;
        private string _paintingOvenTime_2nd;
        private string _paintingOvenTime_3rd;

        private string _checkProcess;




        private double _temperateFront;
        private double _temperateRear;
        private double _humidityFront;
        private double _humidityRear;



        public double temperateFront
        {
            set { _temperateFront = value; }
            get { return _temperateFront; }
        }

        public double temperateRear
        {
            set { _temperateRear = value; }
            get { return _temperateRear; }
        }
        public double humidityFront
        {
            set { _humidityFront = value; }
            get { return _humidityFront; }
        }
        public double humidityRear
        {
            set { _humidityRear = value; }
            get { return _humidityRear; }
        }



        public string checkProcess
        {
            set { _checkProcess = value; }
            get { return _checkProcess; }
        }

        public string paintingOvenTime_1st
        {
            set { _paintingOvenTime_1st = value; }
            get { return _paintingOvenTime_1st; }
        }
        public string paintingOvenTime_2nd
        {
            set { _paintingOvenTime_2nd = value; }
            get { return _paintingOvenTime_2nd; }
        }
        public string paintingOvenTime_3rd
        {
            set { _paintingOvenTime_3rd = value; }
            get { return _paintingOvenTime_3rd; }
        }

        public string paintingRunningTime_1st
        {
            set { _paintingRunningTime_1st = value; }
            get { return _paintingRunningTime_1st; }
        }
        public string paintingRunningTime_2nd
        {
            set { _paintingRunningTime_2nd = value; }
            get { return _paintingRunningTime_2nd; }
        }
        public string paintingRunningTime_3rd
        {
            set { _paintingRunningTime_3rd = value; }
            get { return _paintingRunningTime_3rd; }
        }

        public string paintingPIC_1st
        {
            set { _paintingPIC_1st = value; }
            get { return _paintingPIC_1st; }
        }
        public string paintingPIC_2nd
        {
            set { _paintingPIC_2nd = value; }
            get { return _paintingPIC_2nd; }
        }
        public string paintingPIC_3rd
        {
            set { _paintingPIC_3rd = value; }
            get { return _paintingPIC_3rd; }
        }

        public string thinnersLot_1st
        {
            set { _thinnersLot_1st = value; }
            get { return _thinnersLot_1st; }
        }
        public string thinnersLot_2nd
        {
            set { _thinnersLot_2nd = value; }
            get { return _thinnersLot_2nd; }
        }
        public string thinnersLot_3rd
        {
            set { _thinnersLot_3rd = value; }
            get { return _thinnersLot_3rd; }
        }


        public string paintLot_1st
        {
            set { _paintLot_1st = value; }
            get { return _paintLot_1st; }
        }
        public string paintLot_2nd
        {
            set { _paintLot_2nd = value; }
            get { return _paintLot_2nd; }
        }
        public string paintLot_3rd
        {
            set { _paintLot_3rd = value; }
            get { return _paintLot_3rd; }
        }




        public decimal? thickness_1st
        {
            set { _thickness_1st = value; }
            get { return _thickness_1st; }
        }
        public decimal? thickness_2nd
        {
            set { _thickness_2nd = value; }
            get { return _thickness_2nd; }
        }
        public decimal? thickness_3rd
        {
            set { _thickness_3rd = value; }
            get { return _thickness_3rd; }
        }

        public string recordBy
        {
            set { _recordBy = value; }
            get { return _recordBy; }
        }
        
        public string jobNumber
		{
			set{ _jobnumber=value;}
			get{return _jobnumber;}
		}

		public string lotNo
		{
			set{ _lotno=value;}
			get{return _lotno;}
		}
		
		public string partNumber
		{
			set{ _partnumber=value;}
			get{return _partnumber;}
		}
		
		public string materialName
		{
			set{ _materialname=value;}
			get{return _materialname;}
		}
	
		public decimal? lotQty
		{
			set{ _lotqty=value;}
			get{return _lotqty;}
		}
	
		public decimal? setupRejQty
		{
			set{ _setuprejqty=value;}
			get{return _setuprejqty;}
		}
	
	
		public decimal? qaTestQty
		{
			set{ _qatestqty=value;}
			get{return _qatestqty;}
		}

		public string coat_1st
		{
			set{ _coat_1st=value;}
			get{return _coat_1st;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pMachine_1st
		{
			set{ _pmachine_1st=value;}
			get{return _pmachine_1st;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coat_2nd
		{
			set{ _coat_2nd=value;}
			get{return _coat_2nd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pMachine_2nd
		{
			set{ _pmachine_2nd=value;}
			get{return _pmachine_2nd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coat_3rd
		{
			set{ _coat_3rd=value;}
			get{return _coat_3rd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pMachine_3rd
		{
			set{ _pmachine_3rd=value;}
			get{return _pmachine_3rd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? paintingDate
		{
			set{ _paintingdate=value;}
			get{return _paintingdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UCDate
		{
			set{ _ucdate=value;}
			get{return _ucdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TCDate
		{
			set{ _tcdate=value;}
			get{return _tcdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string laserMachine
		{
			set{ _lasermachine=value;}
			get{return _lasermachine;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string laserOperator
		{
			set{ _laseroperator=value;}
			get{return _laseroperator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? laserDate
		{
			set{ _laserdate=value;}
			get{return _laserdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? MFGDate
		{
			set{ _mfgdate=value;}
			get{return _mfgdate;}
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
		public DateTime? updatedTime
		{
			set{ _updatedtime=value;}
			get{return _updatedtime;}
		}



        public DateTime? paintingDate_1st
        {
            set { _paintingDate_1st = value; }
            get { return _paintingDate_1st; }
        }

        public DateTime? paintingDate_2nd
        {
            set { _paintingDate_2nd = value; }
            get { return _paintingDate_2nd; }
        }

        public DateTime? paintingDate_3rd
        {
            set { _paintingDate_3rd = value; }
            get { return _paintingDate_3rd; }
        }


        #endregion Model

    }
}

