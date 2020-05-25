 
using System;
namespace Common.Model
{
	/// <summary>
	/// LMMSUserAttendanceHis_Model:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSUserAttendanceHis_Model
	{
		public LMMSUserAttendanceHis_Model()
		{}
        #region Model

        private string _employeeID;
		private string _userid;
		private string _username;
		private string _usergroup;
		private string _department;
		private string _shift;
		private string _attendance;
		private string _onleave;
		private DateTime? _day;
		private string _updateby;
		private DateTime? _datetime;
		private string _remarks;


        public string EmployeeID
        {
            set { _employeeID = value; }
            get { return _employeeID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserGroup
		{
			set{ _usergroup=value;}
			get{return _usergroup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Shift
		{
			set{ _shift=value;}
			get{return _shift;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Attendance
		{
			set{ _attendance=value;}
			get{return _attendance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OnLeave
		{
			set{ _onleave=value;}
			get{return _onleave;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Day
		{
			set{ _day=value;}
			get{return _day;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateBy
		{
			set{ _updateby=value;}
			get{return _updateby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateTime
		{
			set{ _datetime=value;}
			get{return _datetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

