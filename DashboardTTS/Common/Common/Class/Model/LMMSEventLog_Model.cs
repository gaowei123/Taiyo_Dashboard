
using System;
namespace Common.Model
{
	/// <summary>
	/// LMMSEventLog_Model:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSEventLog_Model
	{
		public LMMSEventLog_Model()
		{}
		#region Model
		private int _id;
		private DateTime? _datetime;
		private string _machineid;
		private string _currentoperation;
		private string _ownerid;
		private string _eventtrigger;
		private DateTime? _starttime;
		private DateTime? _stoptime;
		private string _ipsetting;
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
		public string currentOperation
		{
			set{ _currentoperation=value;}
			get{return _currentoperation;}
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
		public string eventTrigger
		{
			set{ _eventtrigger=value;}
			get{return _eventtrigger;}
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
		public string ipSetting
		{
			set{ _ipsetting=value;}
			get{return _ipsetting;}
		}
		#endregion Model

        public class Detail
        {
            private int _year;
            private int _month;
            private DateTime _day;
            private string _shift;
            private string _machineid;
            private string _status;
            private DateTime? _startTime;
            private DateTime? _stopTime;
            private double _totalSeconds;


            public int year
            {
                set { _year = value; }
                get { return _year; }
            }

            public int month
            {
                set { _month = value; }
                get { return _month; }
            }

            public DateTime day
            {
                set { _day = value; }
                get { return _day; }
            }
            public string shift
            {
                set { _shift = value; }
                get { return _shift; }
            }
            public string machineID
            {
                set { _machineid = value; }
                get { return _machineid; }
            }
            public string status
            {
                set { _status = value; }
                get { return _status; }
            }
            public DateTime? startTime
            {
                set { _startTime = value; }
                get { return _startTime; }
            }

            public DateTime? stopTime
            {
                set { _stopTime = value; }
                get { return _stopTime; }
            }

            public double totalSeconds
            {
                set { _totalSeconds = value; }
                get { return _totalSeconds; }
            }
        }


    }
}

