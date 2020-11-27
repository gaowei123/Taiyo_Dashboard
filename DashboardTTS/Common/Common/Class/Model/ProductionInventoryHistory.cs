using System;
namespace Common.Class.Model
{
	/// <summary>
	/// ProductionInventoryHistory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductionInventoryHistory
	{
		public ProductionInventoryHistory()
		{}
		#region Model
		private int? _id;
		private DateTime? _day;
		private string _partnumber;
		private string _model;
		private string _materialname;
		private int? _assembly;
		private int? _fg;
		private int? _afterpacking;
		private int? _beforepacking;
		private int? _afterwip;
		private int? _beforewip;
		private int? _afterlaser;
		private int? _beforelaser;
		private int? _tcpaint;
		private int? _mcpaint;
		private int? _printsupplier;
		private int? _ucpaint;
		private int? _paintrawpart;
		/// <summary>
		/// 
		/// </summary>
		public int? Id
		{
			set{ _id=value;}
			get{return _id;}
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
		public string PartNumber
		{
			set{ _partnumber=value;}
			get{return _partnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Model
		{
			set{ _model=value;}
			get{return _model;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MaterialName
		{
			set{ _materialname=value;}
			get{return _materialname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Assembly
		{
			set{ _assembly=value;}
			get{return _assembly;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FG
		{
			set{ _fg=value;}
			get{return _fg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AfterPacking
		{
			set{ _afterpacking=value;}
			get{return _afterpacking;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BeforePacking
		{
			set{ _beforepacking=value;}
			get{return _beforepacking;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AfterWIP
		{
			set{ _afterwip=value;}
			get{return _afterwip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BeforeWIP
		{
			set{ _beforewip=value;}
			get{return _beforewip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AfterLaser
		{
			set{ _afterlaser=value;}
			get{return _afterlaser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BeforeLaser
		{
			set{ _beforelaser=value;}
			get{return _beforelaser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TCPaint
		{
			set{ _tcpaint=value;}
			get{return _tcpaint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MCPaint
		{
			set{ _mcpaint=value;}
			get{return _mcpaint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintSupplier
		{
			set{ _printsupplier=value;}
			get{return _printsupplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UCPaint
		{
			set{ _ucpaint=value;}
			get{return _ucpaint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PaintRawPart
		{
			set{ _paintrawpart=value;}
			get{return _paintrawpart;}
		}
		#endregion Model

	}
}

