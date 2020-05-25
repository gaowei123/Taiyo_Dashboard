/**  版本信息模板在安装目录下，可自行修改。
* PQCPackHistory.cs
*
* 功 能： N/A
* 类 名： PQCPackHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/9 11:36:06   N/A    初版
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
	/// PQCPackHistory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PQCPackHistory_Model
	{
		public PQCPackHistory_Model()
		{}
		#region Model
		private int? _id;
		private string _machineid;
		private DateTime _datetime;
		private string _partnumber;
		private string _jobid;
		private string _processes;
		private string _jigno;
		private string _model;
		private decimal? _cavitycount;
		private decimal? _cycletime;
		private decimal? _targetqty;
		private string _username;
		private string _userid;
		private decimal? _totalqty;
		private decimal? _rejectqty;
		private decimal? _acceptqty;
		private DateTime _starttime;
		private DateTime _stoptime;
		private string _nextviflag;
		private DateTime _day;
		private string _shift;
		private string _status;
		private string _remark_1;
		private string _remark_2;
		private string _reffield01;
		private string _reffield02;
		private string _reffield03;
		private string _reffield04;
		private string _reffield05;
		private string _reffield06;
		private string _reffield07;
		private string _reffield08;
		private string _reffield09;
		private string _reffield10;
		private string _reffield11;
		private string _reffield12;
		private string _customer;
		private DateTime _lastupdatedtime;
		private string _trackingid;
		private string _lasttrackingid;
		private string _remarks;
		private string _department;
		private decimal? _totalrejectqty;
		private DateTime _updatedtime;
		private decimal? _totalpassqty;
		private string _shipto;
		private int? _indexid;
		/// <summary>
		/// 
		/// </summary>
		public int? id
		{
			set{ _id=value;}
			get{return _id;}
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
		public DateTime dateTime
		{
			set{ _datetime=value;}
			get{return _datetime;}
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
		public string jobId
		{
			set{ _jobid=value;}
			get{return _jobid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string processes
		{
			set{ _processes=value;}
			get{return _processes;}
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
		public decimal? cavityCount
		{
			set{ _cavitycount=value;}
			get{return _cavitycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? cycleTime
		{
			set{ _cycletime=value;}
			get{return _cycletime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? targetQty
		{
			set{ _targetqty=value;}
			get{return _targetqty;}
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
		public decimal? TotalQty
		{
			set{ _totalqty=value;}
			get{return _totalqty;}
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
		public decimal? acceptQty
		{
			set{ _acceptqty=value;}
			get{return _acceptqty;}
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
		public string nextViFlag
		{
			set{ _nextviflag=value;}
			get{return _nextviflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime day
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
			get{return _remark_1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark_2
		{
			set{ _remark_2=value;}
			get{return _remark_2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField01
		{
			set{ _reffield01=value;}
			get{return _reffield01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField02
		{
			set{ _reffield02=value;}
			get{return _reffield02;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField03
		{
			set{ _reffield03=value;}
			get{return _reffield03;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField04
		{
			set{ _reffield04=value;}
			get{return _reffield04;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField05
		{
			set{ _reffield05=value;}
			get{return _reffield05;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField06
		{
			set{ _reffield06=value;}
			get{return _reffield06;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField07
		{
			set{ _reffield07=value;}
			get{return _reffield07;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField08
		{
			set{ _reffield08=value;}
			get{return _reffield08;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField09
		{
			set{ _reffield09=value;}
			get{return _reffield09;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField10
		{
			set{ _reffield10=value;}
			get{return _reffield10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField11
		{
			set{ _reffield11=value;}
			get{return _reffield11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refField12
		{
			set{ _reffield12=value;}
			get{return _reffield12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string customer
		{
			set{ _customer=value;}
			get{return _customer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime lastUpdatedTime
		{
			set{ _lastupdatedtime=value;}
			get{return _lastupdatedtime;}
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
		public string lastTrackingID
		{
			set{ _lasttrackingid=value;}
			get{return _lasttrackingid;}
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
		public string department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TotalRejectQty
		{
			set{ _totalrejectqty=value;}
			get{return _totalrejectqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime updatedTime
		{
			set{ _updatedtime=value;}
			get{return _updatedtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalPassQty
		{
			set{ _totalpassqty=value;}
			get{return _totalpassqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shipTo
		{
			set{ _shipto=value;}
			get{return _shipto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? indexId
		{
			set{ _indexid=value;}
			get{return _indexid;}
		}
		#endregion Model

	}
}

