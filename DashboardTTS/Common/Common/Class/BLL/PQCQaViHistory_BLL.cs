 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.Class.BLL
{
	/// <summary>
	/// PQCQaViHistory_BLL
	/// </summary>
	public class PQCQaViHistory_BLL
	{
		private readonly Common.DAL.PQCQaViHistory_DAL dal=new Common.DAL.PQCQaViHistory_DAL();
		public PQCQaViHistory_BLL()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists()
		{
			DataSet ds = new DataSet();
			ds = dal.Exists();
			if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Common.Class.Model.PQCQaViHistory_Model model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Class.Model.PQCQaViHistory_Model model,SqlCommand cmd=null)
		{
			return dal.AddCommand(model,cmd);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCQaViHistory_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViHistory_Model model)
		{
			return dal.UpdateCommand(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand(int id)
		{
			
			return dal.DeleteCommand(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand()
		{
			
			return dal.DeleteAllCommand();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCQaViHistory_Model GetModel(int id)
		{
			
			return dal.GetModel(id);
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
		public List<Common.Class.Model.PQCQaViHistory_Model> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCQaViHistory_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCQaViHistory_Model> modelList = new List<Common.Class.Model.PQCQaViHistory_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCQaViHistory_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Common.Class.Model.PQCQaViHistory_Model();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.machineID=dt.Rows[n]["machineID"].ToString();
					if(dt.Rows[n]["dateTime"].ToString()!="")
					{
						model.dateTime=DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
					}
					model.partNumber=dt.Rows[n]["partNumber"].ToString();
                    model.jobid = dt.Rows[n]["jobid"].ToString();
					model.processes=dt.Rows[n]["processes"].ToString();
					model.jigNo=dt.Rows[n]["jigNo"].ToString();
					model.model=dt.Rows[n]["model"].ToString();
					if(dt.Rows[n]["cavityCount"].ToString()!="")
					{
						model.cavityCount=decimal.Parse(dt.Rows[n]["cavityCount"].ToString());
					}
					if(dt.Rows[n]["cycleTime"].ToString()!="")
					{
						model.cycleTime=decimal.Parse(dt.Rows[n]["cycleTime"].ToString());
					}
					if(dt.Rows[n]["targetQty"].ToString()!="")
					{
						model.targetQty=decimal.Parse(dt.Rows[n]["targetQty"].ToString());
					}
					model.userName=dt.Rows[n]["userName"].ToString();
					model.userID=dt.Rows[n]["userID"].ToString();
					if(dt.Rows[n]["TotalQty"].ToString()!="")
					{
						model.TotalQty=decimal.Parse(dt.Rows[n]["TotalQty"].ToString());
                    }
                    if (dt.Rows[n]["totalPassQty"].ToString() != "")
                    {
                        model.totalPassQty = decimal.Parse(dt.Rows[n]["totalPassQty"].ToString());
                    }
                    if (dt.Rows[n]["TotalRejectQty"].ToString() != "")
                    {
                        model.TotalRejectQty = decimal.Parse(dt.Rows[n]["TotalRejectQty"].ToString());
                    }
                    if (dt.Rows[n]["rejectQty"].ToString()!="")
					{
						model.rejectQty=decimal.Parse(dt.Rows[n]["rejectQty"].ToString());
					}
					if(dt.Rows[n]["acceptQty"].ToString()!="")
					{
						model.acceptQty=decimal.Parse(dt.Rows[n]["acceptQty"].ToString());
					}
					if(dt.Rows[n]["startTime"].ToString()!="")
					{
						model.startTime=DateTime.Parse(dt.Rows[n]["startTime"].ToString());
					}
					if(dt.Rows[n]["stopTime"].ToString()!="")
					{
						model.stopTime=DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
					}
					model.nextViFlag=dt.Rows[n]["nextViFlag"].ToString();
					if(dt.Rows[n]["day"].ToString()!="")
					{
						model.day=DateTime.Parse(dt.Rows[n]["day"].ToString());
					}
					model.shift=dt.Rows[n]["shift"].ToString();
					model.status=dt.Rows[n]["status"].ToString();
					model.remark_1=dt.Rows[n]["remark_1"].ToString();
					model.remark_2=dt.Rows[n]["remark_2"].ToString();
					model.refField01=dt.Rows[n]["refField01"].ToString();
					model.refField02=dt.Rows[n]["refField02"].ToString();
					model.refField03=dt.Rows[n]["refField03"].ToString();
					model.refField04=dt.Rows[n]["refField04"].ToString();
					model.refField05=dt.Rows[n]["refField05"].ToString();
					model.refField06=dt.Rows[n]["refField06"].ToString();
					model.refField07=dt.Rows[n]["refField07"].ToString();
					model.refField08=dt.Rows[n]["refField08"].ToString();
					model.refField09=dt.Rows[n]["refField09"].ToString();
					model.refField10=dt.Rows[n]["refField10"].ToString();
					model.refField11=dt.Rows[n]["refField11"].ToString();
					model.refField12=dt.Rows[n]["refField12"].ToString();
					model.customer=dt.Rows[n]["customer"].ToString();
					if(dt.Rows[n]["lastUpdatedTime"].ToString()!="")
					{
						model.lastUpdatedTime=DateTime.Parse(dt.Rows[n]["lastUpdatedTime"].ToString());
					}
					model.trackingID=dt.Rows[n]["trackingID"].ToString();
					model.lastTrackingID=dt.Rows[n]["lastTrackingID"].ToString();
					model.remarks=dt.Rows[n]["remarks"].ToString();
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



		#endregion  Method
	}
}

