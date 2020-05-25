using System;

namespace Common.Class.Model
{
	/// <summary>
	/// LMMSMachineDownTime:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSMachineDownTime_Model
	{
		public LMMSMachineDownTime_Model()
		{}
		#region Model
		private string _machineid;
		private string _partrunning;
		private string _cause;
		private string _action;
		private DateTime _starttime;
		private DateTime _stoptime;
		private DateTime _date;
		private string _checker;
        private string _AttachmentPath;



        public string attachmentPath
        {
            set { _AttachmentPath = value; }
            get { return _AttachmentPath; }
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
		public string partRunning
		{
			set{ _partrunning=value;}
			get{return _partrunning;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cause
		{
			set{ _cause=value;}
			get{return _cause;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string action
		{
			set{ _action=value;}
			get{return _action;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime startTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime stopTime
		{
			set{ _stoptime=value;}
			get{return _stoptime;}
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
		public string checker
		{
			set{ _checker=value;}
			get{return _checker;}
		}
		#endregion Model

	}
}

