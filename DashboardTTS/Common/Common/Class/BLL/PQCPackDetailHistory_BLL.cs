using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// PQCPackDetailHistory
	/// </summary>
	public partial class PQCPackDetailHistory_BLL
	{
		private readonly Common.Class.DAL.PQCPackDetailHistory_DAL dal=new Common.Class.DAL.PQCPackDetailHistory_DAL();
		public PQCPackDetailHistory_BLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PQCPackDetailHistory_Model model)
		{
			return dal.Add(model);
		}

        public SqlCommand AddCommand(Common.Class.Model.PQCPackDetailTracking_Model model)
        {
            return dal.AddCommand(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCPackDetailHistory_Model model)
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
		public Common.Class.Model.PQCPackDetailHistory_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		



		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCPackDetailHistory_Model> GetModelList(string sTrackingID)
		{
			DataSet ds = dal.GetList(sTrackingID);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCPackDetailHistory_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCPackDetailHistory_Model> modelList = new List<Common.Class.Model.PQCPackDetailHistory_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCPackDetailHistory_Model model;
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

