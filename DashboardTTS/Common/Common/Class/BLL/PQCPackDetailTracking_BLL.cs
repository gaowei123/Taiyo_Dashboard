/**  版本信息模板在安装目录下，可自行修改。
* PQCPackDetailTracking.cs
*
* 功 能： N/A
* 类 名： PQCPackDetailTracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/1/30 21:14:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
namespace Common.Class.BLL
{
	/// <summary>
	/// PQCPackDetailTracking
	/// </summary>
	public partial class PQCPackDetailTracking
	{
		private readonly Common.Class.DAL.PQCPackDetailTracking_DAL dal=new Common.Class.DAL.PQCPackDetailTracking_DAL();
		public PQCPackDetailTracking()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCPackDetailTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
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
		public List<Common.Class.Model.PQCPackDetailTracking_Model> GetModelList(string trackingID)
		{
			DataSet ds = dal.GetList(trackingID);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCPackDetailTracking_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCPackDetailTracking_Model> modelList = new List<Common.Class.Model.PQCPackDetailTracking_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCPackDetailTracking_Model model;
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






	

		#endregion  BasicMethod
	}
}

