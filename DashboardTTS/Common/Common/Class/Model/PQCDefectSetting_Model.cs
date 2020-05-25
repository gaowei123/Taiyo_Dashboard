
using System;
namespace Common.Class.Model
{
	/// <summary>
	/// PQCDefectSetting_Model:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PQCDefectSetting_Model
	{
		public PQCDefectSetting_Model()
		{}
		#region Model
		private string _defectcodeid;
		private string _defectcode;
		private string _defectdescription;
		private string _materialpartno;
		private string _model;
		private string _jigno;
		private string _machineid;
		private string _username;
		private DateTime? _datetime;
		private string _remarks;
		/// <summary>
		/// 
		/// </summary>
		public string defectCodeID
		{
			set{ _defectcodeid=value;}
			get{return _defectcodeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string defectCode
		{
			set{ _defectcode=value;}
			get{return _defectcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string defectDescription
		{
			set{ _defectdescription=value;}
			get{return _defectdescription;}
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
		public string model
		{
			set{ _model=value;}
			get{return _model;}
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
		public string machineID
		{
			set{ _machineid=value;}
			get{return _machineid;}
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
		public DateTime? dateTime
		{
			set{ _datetime=value;}
			get{return _datetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

