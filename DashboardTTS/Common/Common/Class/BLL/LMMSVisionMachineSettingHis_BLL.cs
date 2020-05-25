
using System;
using System.Data;
using System.Collections.Generic;
using System.Data;

namespace Common.Class.BLL
{
	/// <summary>
	/// LMMSVisionMachineSettingHis
	/// </summary>
	public partial class LMMSVisionMachineSettingHis_BLL
	{
		private readonly Common.Class.DAL.LMMSVisionMachineSettingHis_DAL dal=new Common.Class.DAL.LMMSVisionMachineSettingHis_DAL();
		public LMMSVisionMachineSettingHis_BLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSVisionMachineSettingHis_Model model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.LMMSVisionMachineSettingHis_Model model)
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
		public Common.Class.Model.LMMSVisionMachineSettingHis_Model GetModel(string sJobNo)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(sJobNo);
		}

	

		/// <summary>
		/// 获得数据列表
		/// </summary>
		
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.LMMSVisionMachineSettingHis_Model> GetModelList(DateTime dDateFrom, DateTime dDateTo, string[] arrMachineID, string[] arrPartNo)
        {
            DataSet ds = dal.GetList(dDateFrom, dDateTo, arrMachineID, arrPartNo);
            return DataTableToList(ds.Tables[0]);
        }


        public DataTable GetList(string sJobNo)
        {
            DataSet ds = dal.GetList(sJobNo);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else

                return ds.Tables[0];
        }


        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string[] arrMachineID, string[] arrPartNo)
        {
            DataSet ds = dal.GetList(dDateFrom, dDateTo, arrMachineID, arrPartNo);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else

                return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.LMMSVisionMachineSettingHis_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.LMMSVisionMachineSettingHis_Model> modelList = new List<Common.Class.Model.LMMSVisionMachineSettingHis_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.LMMSVisionMachineSettingHis_Model model;
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

