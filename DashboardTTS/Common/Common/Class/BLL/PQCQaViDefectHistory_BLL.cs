 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.Class.BLL
{
	/// <summary>
	/// PQCQaViDefectHistory_BLL
	/// </summary>
	public class PQCQaViDefectHistory_BLL
	{
		private readonly Common.DAL.PQCQaViDefectHistory_DAL dal=new Common.DAL.PQCQaViDefectHistory_DAL();
		public PQCQaViDefectHistory_BLL()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Class.Model.PQCQaViDefectHistory_Model model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Class.Model.PQCQaViDefectHistory_Model model,SqlCommand cmd=null)
		{
			return dal.AddCommand(model,cmd);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCQaViDefectHistory_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViDefectHistory_Model model)
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
		public Common.Class.Model.PQCQaViDefectHistory_Model GetModel()
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
		public List<Common.Class.Model.PQCQaViDefectHistory_Model> GetModelListByTrackingID(string sTrackingID)
		{
			DataSet ds = dal.GetList(sTrackingID);



			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCQaViDefectHistory_Model> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.PQCQaViDefectHistory_Model> modelList = new List<Common.Class.Model.PQCQaViDefectHistory_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCQaViDefectHistory_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.PQCQaViDefectHistory_Model();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.jobid = dt.Rows[n]["jobid"].ToString();
                    model.trackingID = dt.Rows[n]["trackingID"].ToString();
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.materialPartNo = dt.Rows[n]["materialPartNo"].ToString();
                    model.jigNo = dt.Rows[n]["jigNo"].ToString();
                    model.model = dt.Rows[n]["model"].ToString();
                    if (dt.Rows[n]["cavityCount"].ToString() != "")
                    {
                        model.cavityCount = decimal.Parse(dt.Rows[n]["cavityCount"].ToString());
                    }
                    model.userName = dt.Rows[n]["userName"].ToString();
                    model.userID = dt.Rows[n]["userID"].ToString();
                    if (dt.Rows[n]["startTime"].ToString() != "")
                    {
                        model.startTime = DateTime.Parse(dt.Rows[n]["startTime"].ToString());
                    }
                    if (dt.Rows[n]["stopTime"].ToString() != "")
                    {
                        model.stopTime = DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
                    }
                    if (dt.Rows[n]["day"].ToString() != "")
                    {
                        model.day = DateTime.Parse(dt.Rows[n]["day"].ToString());
                    }
                    model.shift = dt.Rows[n]["shift"].ToString();
                    model.status = dt.Rows[n]["status"].ToString();
                    model.remark_1 = dt.Rows[n]["remark_1"].ToString();
                    model.remark_2 = dt.Rows[n]["remark_2"].ToString();
                    model.defectCodeID = dt.Rows[n]["defectCodeID"].ToString();
                    model.defectCode = dt.Rows[n]["defectCode"].ToString();
                    model.defectDescription = dt.Rows[n]["defectDescription"].ToString();
                    if (dt.Rows[n]["rejectQty"].ToString() != "")
                    {
                        model.rejectQty = decimal.Parse(dt.Rows[n]["rejectQty"].ToString());
                    }
                    model.rejectQtyHour01 = dt.Rows[n]["rejectQtyHour01"].ToString();
                    model.rejectQtyHour02 = dt.Rows[n]["rejectQtyHour02"].ToString();
                    model.rejectQtyHour03 = dt.Rows[n]["rejectQtyHour03"].ToString();
                    model.rejectQtyHour04 = dt.Rows[n]["rejectQtyHour04"].ToString();
                    model.rejectQtyHour05 = dt.Rows[n]["rejectQtyHour05"].ToString();
                    model.rejectQtyHour06 = dt.Rows[n]["rejectQtyHour06"].ToString();
                    model.rejectQtyHour07 = dt.Rows[n]["rejectQtyHour07"].ToString();
                    model.rejectQtyHour08 = dt.Rows[n]["rejectQtyHour08"].ToString();
                    model.rejectQtyHour09 = dt.Rows[n]["rejectQtyHour09"].ToString();
                    model.rejectQtyHour10 = dt.Rows[n]["rejectQtyHour10"].ToString();
                    model.rejectQtyHour11 = dt.Rows[n]["rejectQtyHour11"].ToString();
                    model.rejectQtyHour12 = dt.Rows[n]["rejectQtyHour12"].ToString();
                    if (dt.Rows[n]["lastUpdatedTime"].ToString() != "")
                    {
                        model.lastUpdatedTime = DateTime.Parse(dt.Rows[n]["lastUpdatedTime"].ToString());
                    }
                    model.remarks = dt.Rows[n]["remarks"].ToString();
                    model.processes = dt.Rows[n]["processes"].ToString();
                    if (dt.Rows[n]["updatedTime"].ToString() != "")
                    {
                        model.updatedTime = DateTime.Parse(dt.Rows[n]["updatedTime"].ToString());
                    }
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
		public Common.Class.Model.PQCQaViDefectHistory_Model CopyObj(Common.Class.Model.PQCQaViDefectTracking_Model objModel )
		{
			Common.Class.Model.PQCQaViDefectHistory_Model model;
			model = new Common.Class.Model.PQCQaViDefectHistory_Model();
			model.id = objModel.id;
            model.jobid = objModel.jobid;
            model.trackingID = objModel.trackingID ;
			model.machineID = objModel.machineID ;
			model.dateTime = objModel.dateTime ;
			model.materialPartNo = objModel.materialPartNo;
			model.jigNo = objModel.jigNo ;
			model.model = objModel.model ;
			model.cavityCount = objModel.cavityCount ;
			model.userName = objModel.userName ;
			model.userID = objModel.userID ;
			model.startTime = objModel.startTime ;
			model.stopTime = objModel.stopTime ;
			model.day = objModel.day ;
			model.shift = objModel.shift ;
			model.status = objModel.status ;
			model.remark_1 = objModel.remark_1;
			model.remark_2 = objModel.remark_2;
			model.defectCodeID = objModel.defectCodeID ;
			model.defectCode = objModel.defectCode;
            model.defectDescription = objModel.defectDescription;
            model.rejectQty = objModel.rejectQty ;
			model.rejectQtyHour01 = objModel.rejectQtyHour01 ;
			model.rejectQtyHour02 = objModel.rejectQtyHour02 ;
			model.rejectQtyHour03 = objModel.rejectQtyHour03 ;
			model.rejectQtyHour04 = objModel.rejectQtyHour04 ;
			model.rejectQtyHour05 = objModel.rejectQtyHour05 ;
			model.rejectQtyHour06 = objModel.rejectQtyHour06 ;
			model.rejectQtyHour07 = objModel.rejectQtyHour07 ;
			model.rejectQtyHour08 = objModel.rejectQtyHour08 ;
			model.rejectQtyHour09 = objModel.rejectQtyHour09 ;
			model.rejectQtyHour10 = objModel.rejectQtyHour10 ;
			model.rejectQtyHour11 = objModel.rejectQtyHour11 ;
			model.rejectQtyHour12 = objModel.rejectQtyHour12 ;
			model.lastUpdatedTime = objModel.lastUpdatedTime ;
			model.remarks = objModel.remarks;
            model.processes = objModel.processes;
            model.updatedTime = objModel.updatedTime;
            return model;
		}




      


        #endregion  Method
    }
}

