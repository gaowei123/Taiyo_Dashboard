 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.BLL
{
	/// <summary>
	/// LMMSUserAttendanceTracking_BLL
	/// </summary>
	public class LMMSUserAttendanceTracking_BLL
	{
		private readonly Common.DAL.LMMSUserAttendanceTracking_DAL dal=new Common.DAL.LMMSUserAttendanceTracking_DAL();
		public LMMSUserAttendanceTracking_BLL()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			return dal.AddCommand(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			return dal.UpdateCommand(model);
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
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.DeleteCommand();
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.DeleteAllCommand();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Model.LMMSUserAttendanceTracking_Model GetModel()
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
		public List<Common.Model.LMMSUserAttendanceTracking_Model> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Model.LMMSUserAttendanceTracking_Model> DataTableToList(DataTable dt)
		{
			List<Common.Model.LMMSUserAttendanceTracking_Model> modelList = new List<Common.Model.LMMSUserAttendanceTracking_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Model.LMMSUserAttendanceTracking_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Common.Model.LMMSUserAttendanceTracking_Model();
					model.UserID=dt.Rows[n]["UserID"].ToString();
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.UserGroup=dt.Rows[n]["UserGroup"].ToString();
					model.Department=dt.Rows[n]["Department"].ToString();
					model.Shift=dt.Rows[n]["Shift"].ToString();
					model.Attendance=dt.Rows[n]["Attendance"].ToString();
					model.OnLeave=dt.Rows[n]["OnLeave"].ToString();
					if(dt.Rows[n]["Day"].ToString()!="")
					{
						model.Day=DateTime.Parse(dt.Rows[n]["Day"].ToString());
					}
					model.UpdateBy=dt.Rows[n]["UpdateBy"].ToString();
					if(dt.Rows[n]["DateTime"].ToString()!="")
					{
						model.DateTime=DateTime.Parse(dt.Rows[n]["DateTime"].ToString());
					}
					model.Remarks=dt.Rows[n]["Remarks"].ToString();
					modelList.Add(model);
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
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public Common.Model.LMMSUserAttendanceTracking_Model CopyObj(Common.Model.LMMSUserAttendanceTracking_Model objModel )
		{
			Common.Model.LMMSUserAttendanceTracking_Model model;
			model = new Common.Model.LMMSUserAttendanceTracking_Model();
			model.UserID = objModel.UserID ;
			model.UserName = objModel.UserName ;
			model.UserGroup = objModel.UserGroup ;
			model.Department = objModel.Department ;
			model.Shift = objModel.Shift ;
			model.Attendance = objModel.Attendance ;
			model.OnLeave = objModel.OnLeave ;
			model.Day = objModel.Day ;
			model.UpdateBy = objModel.UpdateBy ;
			model.DateTime = objModel.DateTime ;
			model.Remarks = objModel.Remarks ;
			return model;
		}


        #endregion  Method


      
     

        internal DataSet getAttendance(DateTime dDay, string sShift, string  sDepartment)
        {
            return dal.getAttendance(dDay, sShift, sDepartment);
        }
        
        public bool IsSubmited(DateTime dDay, string sDepartment)
        {
            DataTable dt = dal.GetList(dDay, dDay.AddDays(1),sDepartment);
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Common.Model.LMMSUserAttendanceTracking_Model> GetModelList(DateTime dDateFrom, DateTime dDateTo, string sDepartment)
        {
            DataTable dt = dal.GetList(dDateFrom, dDateTo, sDepartment);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<Common.Model.LMMSUserAttendanceTracking_Model> modelList = new List<LMMSUserAttendanceTracking_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                Common.Model.LMMSUserAttendanceTracking_Model model = new LMMSUserAttendanceTracking_Model();
                
                model.EmployeeID = dr["EmployeeID"].ToString();
                model.UserID = dr["UserID"].ToString();
                model.UserName = dr["UserName"].ToString();
                model.UserGroup = dr["UserGroup"].ToString();
                model.Department = dr["Department"].ToString();
                model.Shift = dr["Shift"].ToString();
                model.Attendance = dr["Attendance"].ToString();
                model.OnLeave = dr["OnLeave"].ToString();
                model.UpdateBy = dr["UpdateBy"].ToString();
                model.Remarks = dr["Remarks"].ToString();
                
                if (dr["Day"] == null || dr["Day"].ToString() == "")
                    model.Day = null;
                else
                    model.Day = DateTime.Parse(dr["Day"].ToString());
                
                if (dr["DateTime"] == null || dr["DateTime"].ToString() == "")
                    model.DateTime = null;
                else
                    model.DateTime = DateTime.Parse(dr["DateTime"].ToString());
                

                modelList.Add(model);
            }



            return modelList;
        }
        
        public bool SubmitAttendance(DateTime dDay, string sDepartment, List<Model.LMMSUserAttendanceTracking_Model> modelList)
        {

            Common.BLL.LMMSUserAttendanceHis_BLL hisBLL = new LMMSUserAttendanceHis_BLL();


            List<SqlCommand> cmdList = new List<SqlCommand>();

            cmdList.Add(dal.DeleteAttendanceByDayDept(dDay, sDepartment));
            

            foreach (Common.Model.LMMSUserAttendanceTracking_Model model in modelList)
            {
                cmdList.Add(dal.AddCommand(model));
                cmdList.Add(hisBLL.AddCommand(hisBLL.CopyObj(model)));
            }

            
            return  DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        public DataTable GetUserAttendanceSummary(DateTime dDateFrom, DateTime dDateTo, string sDepartment)
        {
            return dal.GetUserAttendanceSummary(dDateFrom, dDateTo, sDepartment);
        }

   


    }
}

