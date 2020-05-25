using System;
namespace Common.Class.Model
{
	/// <summary>
	/// LMMSCheckList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSCheckList_Model
	{
		public LMMSCheckList_Model()
		{}
		#region Model
		private string _machineid;
		private DateTime _date;
		private string _detectoksample;
		private string _detectngsample;
		private string _greenlight;
		private string _yellowlight;
		private string _redlight;
		private decimal? _productbeforeblowing;
		private decimal? _productafterblowing;
		private string _filterbagreplace;
		private string _doneby;
		private string _verifyby;
		private DateTime _updatedtime;
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
		public DateTime date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DetectOKSample
		{
			set{ _detectoksample=value;}
			get{return _detectoksample;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DetectNGSample
		{
			set{ _detectngsample=value;}
			get{return _detectngsample;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string greenLight
		{
			set{ _greenlight=value;}
			get{return _greenlight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string yellowLight
		{
			set{ _yellowlight=value;}
			get{return _yellowlight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string redLight
		{
			set{ _redlight=value;}
			get{return _redlight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? productBeforeBlowing
		{
			set{ _productbeforeblowing=value;}
			get{return _productbeforeblowing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? productAfterBlowing
		{
			set{ _productafterblowing=value;}
			get{return _productafterblowing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filterBagReplace
		{
			set{ _filterbagreplace=value;}
			get{return _filterbagreplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string doneBy
		{
			set{ _doneby=value;}
			get{return _doneby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VerifyBy
		{
			set{ _verifyby=value;}
			get{return _verifyby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime updatedTime
		{
			set{ _updatedtime=value;}
			get{return _updatedtime;}
		}
		#endregion Model

	}
}

