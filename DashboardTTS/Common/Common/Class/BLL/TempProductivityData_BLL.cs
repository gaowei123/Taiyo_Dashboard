using System;
using System.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// TempProductivityData
	/// </summary>
	public partial class TempProductivityData_BLL
	{
		private readonly Common.Class.DAL.TempProductivityData_DAL dal=new Common.Class.DAL.TempProductivityData_DAL();
		public TempProductivityData_BLL()
		{}

		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.TempProductivityData_Model model)
		{
			return dal.Add(model);
		}


        public bool AddAll(List<Common.Class.Model.TempProductivityData_Model> models,string updatedBy)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


           SqlCommand deleteCMD = dal.DeleteDayCMD(models[0].department, models[0].day);

            cmdList.Add(deleteCMD);

            foreach (Common.Class.Model.TempProductivityData_Model model in models)
            {
                model.updatedBy = updatedBy;
                cmdList.Add(dal.AddCommand(model));
            }


            return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.TempProductivityData_Model model)
		{
			return dal.Update(model);
		}

	
		public Common.Class.Model.TempProductivityData_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

	
		public List<Common.Class.Model.TempProductivityData_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.TempProductivityData_Model> modelList = new List<Common.Class.Model.TempProductivityData_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Common.Class.Model.TempProductivityData_Model model;
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
		


        public bool IsUpdated(string sDepartment, DateTime dDay)
        {

            DataTable dt = dal.GetList(sDepartment, dDay);

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }else
            {
                return true;
            }

        }



		
		#endregion  BasicMethod



	}
}

