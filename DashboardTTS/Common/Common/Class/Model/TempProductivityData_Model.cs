/**  版本信息模板在安装目录下，可自行修改。
* TempProductivityData.cs
*
* 功 能： N/A
* 类 名： TempProductivityData
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/5/10 15:41:37   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Common.Class.Model
{
	/// <summary>
	/// TempProductivityData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TempProductivityData_Model
	{
		public TempProductivityData_Model()
		{}
		#region Model
		private DateTime? _day;
        private string _shift;

        private string _department;
		private string _type;
		private string _targethr;
		private string _actualhr;
		private string _targetqty;
		private string _totalqty;
		private string _actualqty;
		private string _rejectqty;
		private string _bezelrejqty;
		private string _coverrejqty;
		private string _escrejqty;
		private string _btnrejqty;
		private string _indicatorrejqty;


        private string _mouldRejCount;
        private string _paintRejCount;
        private string _laserRejCount;
        private string _vendorRejCount;
        private string _printRejCount;


        private string _typerej;
		private string _utilization;
		private DateTime? _createdtime;
		private DateTime? _updatedtime;
		private string _updatedby;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? day
		{
			set{ _day=value;}
			get{return _day;}
		}

        public string shift
        {
            set { _shift = value; }
            get { return _shift; }
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
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string targetHR
		{
			set{ _targethr=value;}
			get{return _targethr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string actualHR
		{
			set{ _actualhr=value;}
			get{return _actualhr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string targetQty
		{
			set{ _targetqty=value;}
			get{return _targetqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string totalQty
		{
			set{ _totalqty=value;}
			get{return _totalqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string actualQty
		{
			set{ _actualqty=value;}
			get{return _actualqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rejectQty
		{
			set{ _rejectqty=value;}
			get{return _rejectqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bezelRejQty
		{
			set{ _bezelrejqty=value;}
			get{return _bezelrejqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string coverRejQty
		{
			set{ _coverrejqty=value;}
			get{return _coverrejqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string escRejQty
		{
			set{ _escrejqty=value;}
			get{return _escrejqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string btnRejQty
		{
			set{ _btnrejqty=value;}
			get{return _btnrejqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string indicatorRejQty
		{
			set{ _indicatorrejqty=value;}
			get{return _indicatorrejqty;}
		}



       



        public string mouldRejCount
        {
            set { _mouldRejCount = value; }
            get { return _mouldRejCount; }
        }

        public string paintRejCount
        {
            set { _paintRejCount = value; }
            get { return _paintRejCount; }
        }

        public string laserRejCount
        {
            set { _laserRejCount = value; }
            get { return _laserRejCount; }
        }

        public string vendorRejCount
        {
            set { _vendorRejCount = value; }
            get { return _vendorRejCount; }
        }

        public string printRejCount
        {
            set { _printRejCount = value; }
            get { return _printRejCount; }
        }












        /// <summary>
        /// 
        /// </summary>
        public string typeRej
		{
			set{ _typerej=value;}
			get{return _typerej;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string utilization
		{
			set{ _utilization=value;}
			get{return _utilization;}
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
		/// <summary>
		/// 
		/// </summary>
		public string updatedBy
		{
			set{ _updatedby=value;}
			get{return _updatedby;}
		}
		#endregion Model

	}
}

