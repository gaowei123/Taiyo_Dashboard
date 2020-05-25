/**  版本信息模板在安装目录下，可自行修改。
* LMMSVisionMachineSettingHis.cs
*
* 功 能： N/A
* 类 名： LMMSVisionMachineSettingHis
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/12/30 11:12:51   N/A    初版
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
	/// LMMSVisionMachineSettingHis:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LMMSVisionMachineSettingHis_Model
	{
		public LMMSVisionMachineSettingHis_Model()
		{}
		#region Model
		private DateTime? _day;
		private string _shift;
		private string _jobnumber;
		private string _partnumber;
        private string _machineID;
        private string _lighting;
		private string _camera;
		private string _power;
		private string _rate;
		private string _frequency;
		private string _fill;
		private string _repeat;
		private DateTime? _datetime;
		private DateTime? _updatedtime;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? day
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
		public string jobNumber
		{
			set{ _jobnumber=value;}
			get{return _jobnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string partNumber
		{
			set{ _partnumber=value;}
			get{return _partnumber;}
		}

        public string machineID
        {
            set { _machineID = value; }
            get { return _machineID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lighting
		{
			set{ _lighting=value;}
			get{return _lighting;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string camera
		{
			set{ _camera=value;}
			get{return _camera;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string power
		{
			set{ _power=value;}
			get{return _power;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rate
		{
			set{ _rate=value;}
			get{return _rate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string frequency
		{
			set{ _frequency=value;}
			get{return _frequency;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fill
		{
			set{ _fill=value;}
			get{return _fill;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string repeat
		{
			set{ _repeat=value;}
			get{return _repeat;}
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
		public DateTime? updatedTime
		{
			set{ _updatedtime=value;}
			get{return _updatedtime;}
		}
		#endregion Model

	}
}

