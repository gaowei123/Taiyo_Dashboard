using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// ProductionInventoryHistory
	/// </summary>
	public partial class ProductionInventoryHistory
	{
		private readonly Common.Class.DAL.ProductionInventoryHistory dal=new Common.Class.DAL.ProductionInventoryHistory();
		public ProductionInventoryHistory()
		{}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.ProductionInventoryHistory model)
		{
			return dal.Add(model);
		}


        public SqlCommand AddCommand(Common.Class.Model.ProductionInventoryHistory model)
        {
            return dal.AddCommand(model);
        }


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.ProductionInventoryHistory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(DateTime? Day)
		{
			return dal.Delete(Day);
		}


		/// <summary>
		/// 得到一天的实体列表
		/// </summary>
		public List<Common.Class.Model.ProductionInventoryHistory> GetDayList(DateTime Day)
		{
			return dal.GetDayList(Day);
		} 



        

	}
}

