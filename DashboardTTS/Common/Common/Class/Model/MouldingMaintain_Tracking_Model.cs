/**  版本信息模板在安装目录下，可自行修改。
* MouldingMaintain_Tracking.cs
*
* 功 能： N/A
* 类 名： MouldingMaintain_Tracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/1/8 15:36:32   N/A    初版
*
* Copyright (c) 2012 Common.Class Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Common.Class.Model
{
	/// <summary>
	/// MouldingMaintain_Tracking:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MouldingMaintain_Tracking_Model
	{
		public MouldingMaintain_Tracking_Model()
		{}
		#region Model
		private string _machineid;
		private string _checkperiod;
		private string _checkitem;
		private string _checkresult;
		private string _spareparts;
		private DateTime _changetime;
		private DateTime _checkdate;
		private string _checkby;
		private string _verifyby;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public string MachineID
		{
			set{ _machineid=value;}
			get{return _machineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckPeriod
		{
			set{ _checkperiod=value;}
			get{return _checkperiod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckItem
		{
			set{ _checkitem=value;}
			get{return _checkitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckResult
		{
			set{ _checkresult=value;}
			get{return _checkresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SpareParts
		{
			set{ _spareparts=value;}
			get{return _spareparts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ChangeTime
		{
			set{ _changetime=value;}
			get{return _changetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CheckDate
		{
			set{ _checkdate=value;}
			get{return _checkdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckBy
		{
			set{ _checkby=value;}
			get{return _checkby;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

