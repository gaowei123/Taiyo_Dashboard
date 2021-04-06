using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data.SqlClient;
namespace Common.Class.BLL
{
	/// <summary>
	/// PQCPackHistory
	/// </summary>
	public partial class PQCPackHistory
	{
		private readonly Common.Class.DAL.PQCPackHistory_DAL dal=new Common.Class.DAL.PQCPackHistory_DAL();
		public PQCPackHistory()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PQCPackHistory_Model model)
		{
			return dal.Add(model);
		}

        public SqlCommand AddCommand(Common.Class.Model.PQCPackTracking_Model model)
        {
            return dal.AddCommand(model);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCPackHistory_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCPackHistory_Model GetModel(string sTrackingID)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(sTrackingID);
		}

	

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCPackHistory_Model> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCPackHistory_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCPackHistory_Model> modelList = new List<Common.Class.Model.PQCPackHistory_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCPackHistory_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

	
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod

	}
}

