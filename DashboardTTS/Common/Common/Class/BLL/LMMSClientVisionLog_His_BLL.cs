
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.BLL
{
	/// <summary>
	/// LMMSClientVisionLog_His_BLL
	/// </summary>
	public class LMMSClientVisionLog_His_BLL
	{
		private readonly Common.DAL.LMMSClientVisionLog_His_DAL dal=new Common.DAL.LMMSClientVisionLog_His_DAL();
		public LMMSClientVisionLog_His_BLL()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			return dal.AddCommand(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Model.LMMSClientVisionLog_His_Model model)
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
		public Common.Model.LMMSClientVisionLog_His_Model GetModel()
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
		public List<Common.Model.LMMSClientVisionLog_His_Model> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Model.LMMSClientVisionLog_His_Model> DataTableToList(DataTable dt)
		{
			List<Common.Model.LMMSClientVisionLog_His_Model> modelList = new List<Common.Model.LMMSClientVisionLog_His_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Model.LMMSClientVisionLog_His_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Common.Model.LMMSClientVisionLog_His_Model();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["dateTime"].ToString()!="")
					{
						model.dateTime=DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
					}
					if(dt.Rows[n]["UpdatedTime"].ToString()!="")
					{
						model.UpdatedTime=DateTime.Parse(dt.Rows[n]["UpdatedTime"].ToString());
					}
					model.TransType=dt.Rows[n]["TransType"].ToString();
					model.machineID=dt.Rows[n]["machineID"].ToString();
					model.partNumber=dt.Rows[n]["partNumber"].ToString();
					model.jobNumber=dt.Rows[n]["jobNumber"].ToString();
					model.modeStatus=dt.Rows[n]["modeStatus"].ToString();
					model.currentStatus=dt.Rows[n]["currentStatus"].ToString();
					if(dt.Rows[n]["totalGraphic"].ToString()!="")
					{
						model.totalGraphic=int.Parse(dt.Rows[n]["totalGraphic"].ToString());
					}
					if(dt.Rows[n]["currentQuantity"].ToString()!="")
					{
						model.currentQuantity=int.Parse(dt.Rows[n]["currentQuantity"].ToString());
					}
					if(dt.Rows[n]["totalQuantity"].ToString()!="")
					{
						model.totalQuantity=int.Parse(dt.Rows[n]["totalQuantity"].ToString());
					}
					model.mainResult=dt.Rows[n]["mainResult"].ToString();
					model.vSystemOk=dt.Rows[n]["vSystemOk"].ToString();
					model.runningOk=dt.Rows[n]["runningOk"].ToString();
					model.inspectOk=dt.Rows[n]["inspectOk"].ToString();
					model.passOk=dt.Rows[n]["passOk"].ToString();
					model.failOk=dt.Rows[n]["failOk"].ToString();
					model.graXYname1=dt.Rows[n]["graXYname1"].ToString();
					model.graXYval1=dt.Rows[n]["graXYval1"].ToString();
					model.graXYres1=dt.Rows[n]["graXYres1"].ToString();
					model.graXYname2=dt.Rows[n]["graXYname2"].ToString();
					model.graXYval2=dt.Rows[n]["graXYval2"].ToString();
					model.graXYres2=dt.Rows[n]["graXYres2"].ToString();
					model.graXYname3=dt.Rows[n]["graXYname3"].ToString();
					model.graXYval3=dt.Rows[n]["graXYval3"].ToString();
					model.graXYres3=dt.Rows[n]["graXYres3"].ToString();
					model.graXYname4=dt.Rows[n]["graXYname4"].ToString();
					model.graXYval4=dt.Rows[n]["graXYval4"].ToString();
					model.graXYres4=dt.Rows[n]["graXYres4"].ToString();
					model.graXYname5=dt.Rows[n]["graXYname5"].ToString();
					model.graXYval5=dt.Rows[n]["graXYval5"].ToString();
					model.graXYres5=dt.Rows[n]["graXYres5"].ToString();
					model.graXYname6=dt.Rows[n]["graXYname6"].ToString();
					model.graXYval6=dt.Rows[n]["graXYval6"].ToString();
					model.graXYres6=dt.Rows[n]["graXYres6"].ToString();
					model.graXYname7=dt.Rows[n]["graXYname7"].ToString();
					model.graXYval7=dt.Rows[n]["graXYval7"].ToString();
					model.graXYres7=dt.Rows[n]["graXYres7"].ToString();
					model.graXYname8=dt.Rows[n]["graXYname8"].ToString();
					model.graXYval8=dt.Rows[n]["graXYval8"].ToString();
					model.graXYres8=dt.Rows[n]["graXYres8"].ToString();
					model.graXYname9=dt.Rows[n]["graXYname9"].ToString();
					model.graXYval9=dt.Rows[n]["graXYval9"].ToString();
					model.graXYres9=dt.Rows[n]["graXYres9"].ToString();
					model.graXYname10=dt.Rows[n]["graXYname10"].ToString();
					model.graXYval10=dt.Rows[n]["graXYval10"].ToString();
					model.graXYres10=dt.Rows[n]["graXYres10"].ToString();
					model.graXYname11=dt.Rows[n]["graXYname11"].ToString();
					model.graXYval11=dt.Rows[n]["graXYval11"].ToString();
					model.graXYres11=dt.Rows[n]["graXYres11"].ToString();
					model.graXYname12=dt.Rows[n]["graXYname12"].ToString();
					model.graXYval12=dt.Rows[n]["graXYval12"].ToString();
					model.graXYres12=dt.Rows[n]["graXYres12"].ToString();
					model.graXYname13=dt.Rows[n]["graXYname13"].ToString();
					model.graXYval13=dt.Rows[n]["graXYval13"].ToString();
					model.graXYres13=dt.Rows[n]["graXYres13"].ToString();
					model.graXYname14=dt.Rows[n]["graXYname14"].ToString();
					model.graXYval14=dt.Rows[n]["graXYval14"].ToString();
					model.graXYres14=dt.Rows[n]["graXYres14"].ToString();
					model.graXYname15=dt.Rows[n]["graXYname15"].ToString();
					model.graXYval15=dt.Rows[n]["graXYval15"].ToString();
					model.graXYres15=dt.Rows[n]["graXYres15"].ToString();
					model.graXYname16=dt.Rows[n]["graXYname16"].ToString();
					model.graXYval16=dt.Rows[n]["graXYval16"].ToString();
					model.graXYres16=dt.Rows[n]["graXYres16"].ToString();
					model.graXYname17=dt.Rows[n]["graXYname17"].ToString();
					model.graXYval17=dt.Rows[n]["graXYval17"].ToString();
					model.graXYres17=dt.Rows[n]["graXYres17"].ToString();
					model.graXYname18=dt.Rows[n]["graXYname18"].ToString();
					model.graXYval18=dt.Rows[n]["graXYval18"].ToString();
					model.graXYres18=dt.Rows[n]["graXYres18"].ToString();
					model.graXYname19=dt.Rows[n]["graXYname19"].ToString();
					model.graXYval19=dt.Rows[n]["graXYval19"].ToString();
					model.graXYres19=dt.Rows[n]["graXYres19"].ToString();
					model.graXYname20=dt.Rows[n]["graXYname20"].ToString();
					model.graXYval20=dt.Rows[n]["graXYval20"].ToString();
					model.graXYres20=dt.Rows[n]["graXYres20"].ToString();
					model.graXYname21=dt.Rows[n]["graXYname21"].ToString();
					model.graXYval21=dt.Rows[n]["graXYval21"].ToString();
					model.graXYres21=dt.Rows[n]["graXYres21"].ToString();
					model.graXYname22=dt.Rows[n]["graXYname22"].ToString();
					model.graXYval22=dt.Rows[n]["graXYval22"].ToString();
					model.graXYres22=dt.Rows[n]["graXYres22"].ToString();
					model.graXYname23=dt.Rows[n]["graXYname23"].ToString();
					model.graXYval23=dt.Rows[n]["graXYval23"].ToString();
					model.graXYres23=dt.Rows[n]["graXYres23"].ToString();
					model.graXYname24=dt.Rows[n]["graXYname24"].ToString();
					model.graXYval24=dt.Rows[n]["graXYval24"].ToString();
					model.graXYres24=dt.Rows[n]["graXYres24"].ToString();
					model.graXYname25=dt.Rows[n]["graXYname25"].ToString();
					model.graXYval25=dt.Rows[n]["graXYval25"].ToString();
					model.graXYres25=dt.Rows[n]["graXYres25"].ToString();
					model.graXYname26=dt.Rows[n]["graXYname26"].ToString();
					model.graXYval26=dt.Rows[n]["graXYval26"].ToString();
					model.graXYres26=dt.Rows[n]["graXYres26"].ToString();
					model.graXYname27=dt.Rows[n]["graXYname27"].ToString();
					model.graXYval27=dt.Rows[n]["graXYval27"].ToString();
					model.graXYres27=dt.Rows[n]["graXYres27"].ToString();
					model.graXYname28=dt.Rows[n]["graXYname28"].ToString();
					model.graXYval28=dt.Rows[n]["graXYval28"].ToString();
					model.graXYres28=dt.Rows[n]["graXYres28"].ToString();
					model.graXYname29=dt.Rows[n]["graXYname29"].ToString();
					model.graXYval29=dt.Rows[n]["graXYval29"].ToString();
					model.graXYres29=dt.Rows[n]["graXYres29"].ToString();
					model.graXYname30=dt.Rows[n]["graXYname30"].ToString();
					model.graXYval30=dt.Rows[n]["graXYval30"].ToString();
					model.graXYres30=dt.Rows[n]["graXYres30"].ToString();
					model.overallResult=dt.Rows[n]["overallResult"].ToString();
					if(dt.Rows[n]["totalJig"].ToString()!="")
					{
						model.totalJig=int.Parse(dt.Rows[n]["totalJig"].ToString());
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
		public Common.Model.LMMSClientVisionLog_His_Model CopyObj(Common.Model.LMMSClientVisionLog_His_Model objModel )
		{
			Common.Model.LMMSClientVisionLog_His_Model model;
			model = new Common.Model.LMMSClientVisionLog_His_Model();
			model.id = objModel.id ;
			model.dateTime = objModel.dateTime ;
			model.UpdatedTime = objModel.UpdatedTime ;
			model.TransType = objModel.TransType ;
			model.machineID = objModel.machineID ;
			model.partNumber = objModel.partNumber ;
			model.jobNumber = objModel.jobNumber ;
			model.modeStatus = objModel.modeStatus ;
			model.currentStatus = objModel.currentStatus ;
			model.totalGraphic = objModel.totalGraphic ;
			model.currentQuantity = objModel.currentQuantity ;
			model.totalQuantity = objModel.totalQuantity ;
			model.mainResult = objModel.mainResult ;
			model.vSystemOk = objModel.vSystemOk ;
			model.runningOk = objModel.runningOk ;
			model.inspectOk = objModel.inspectOk ;
			model.passOk = objModel.passOk ;
			model.failOk = objModel.failOk ;
			model.graXYname1 = objModel.graXYname1 ;
			model.graXYval1 = objModel.graXYval1 ;
			model.graXYres1 = objModel.graXYres1 ;
			model.graXYname2 = objModel.graXYname2 ;
			model.graXYval2 = objModel.graXYval2 ;
			model.graXYres2 = objModel.graXYres2 ;
			model.graXYname3 = objModel.graXYname3 ;
			model.graXYval3 = objModel.graXYval3 ;
			model.graXYres3 = objModel.graXYres3 ;
			model.graXYname4 = objModel.graXYname4 ;
			model.graXYval4 = objModel.graXYval4 ;
			model.graXYres4 = objModel.graXYres4 ;
			model.graXYname5 = objModel.graXYname5 ;
			model.graXYval5 = objModel.graXYval5 ;
			model.graXYres5 = objModel.graXYres5 ;
			model.graXYname6 = objModel.graXYname6 ;
			model.graXYval6 = objModel.graXYval6 ;
			model.graXYres6 = objModel.graXYres6 ;
			model.graXYname7 = objModel.graXYname7 ;
			model.graXYval7 = objModel.graXYval7 ;
			model.graXYres7 = objModel.graXYres7 ;
			model.graXYname8 = objModel.graXYname8 ;
			model.graXYval8 = objModel.graXYval8 ;
			model.graXYres8 = objModel.graXYres8 ;
			model.graXYname9 = objModel.graXYname9 ;
			model.graXYval9 = objModel.graXYval9 ;
			model.graXYres9 = objModel.graXYres9 ;
			model.graXYname10 = objModel.graXYname10 ;
			model.graXYval10 = objModel.graXYval10 ;
			model.graXYres10 = objModel.graXYres10 ;
			model.graXYname11 = objModel.graXYname11 ;
			model.graXYval11 = objModel.graXYval11 ;
			model.graXYres11 = objModel.graXYres11 ;
			model.graXYname12 = objModel.graXYname12 ;
			model.graXYval12 = objModel.graXYval12 ;
			model.graXYres12 = objModel.graXYres12 ;
			model.graXYname13 = objModel.graXYname13 ;
			model.graXYval13 = objModel.graXYval13 ;
			model.graXYres13 = objModel.graXYres13 ;
			model.graXYname14 = objModel.graXYname14 ;
			model.graXYval14 = objModel.graXYval14 ;
			model.graXYres14 = objModel.graXYres14 ;
			model.graXYname15 = objModel.graXYname15 ;
			model.graXYval15 = objModel.graXYval15 ;
			model.graXYres15 = objModel.graXYres15 ;
			model.graXYname16 = objModel.graXYname16 ;
			model.graXYval16 = objModel.graXYval16 ;
			model.graXYres16 = objModel.graXYres16 ;
			model.graXYname17 = objModel.graXYname17 ;
			model.graXYval17 = objModel.graXYval17 ;
			model.graXYres17 = objModel.graXYres17 ;
			model.graXYname18 = objModel.graXYname18 ;
			model.graXYval18 = objModel.graXYval18 ;
			model.graXYres18 = objModel.graXYres18 ;
			model.graXYname19 = objModel.graXYname19 ;
			model.graXYval19 = objModel.graXYval19 ;
			model.graXYres19 = objModel.graXYres19 ;
			model.graXYname20 = objModel.graXYname20 ;
			model.graXYval20 = objModel.graXYval20 ;
			model.graXYres20 = objModel.graXYres20 ;
			model.graXYname21 = objModel.graXYname21 ;
			model.graXYval21 = objModel.graXYval21 ;
			model.graXYres21 = objModel.graXYres21 ;
			model.graXYname22 = objModel.graXYname22 ;
			model.graXYval22 = objModel.graXYval22 ;
			model.graXYres22 = objModel.graXYres22 ;
			model.graXYname23 = objModel.graXYname23 ;
			model.graXYval23 = objModel.graXYval23 ;
			model.graXYres23 = objModel.graXYres23 ;
			model.graXYname24 = objModel.graXYname24 ;
			model.graXYval24 = objModel.graXYval24 ;
			model.graXYres24 = objModel.graXYres24 ;
			model.graXYname25 = objModel.graXYname25 ;
			model.graXYval25 = objModel.graXYval25 ;
			model.graXYres25 = objModel.graXYres25 ;
			model.graXYname26 = objModel.graXYname26 ;
			model.graXYval26 = objModel.graXYval26 ;
			model.graXYres26 = objModel.graXYres26 ;
			model.graXYname27 = objModel.graXYname27 ;
			model.graXYval27 = objModel.graXYval27 ;
			model.graXYres27 = objModel.graXYres27 ;
			model.graXYname28 = objModel.graXYname28 ;
			model.graXYval28 = objModel.graXYval28 ;
			model.graXYres28 = objModel.graXYres28 ;
			model.graXYname29 = objModel.graXYname29 ;
			model.graXYval29 = objModel.graXYval29 ;
			model.graXYres29 = objModel.graXYres29 ;
			model.graXYname30 = objModel.graXYname30 ;
			model.graXYval30 = objModel.graXYval30 ;
			model.graXYres30 = objModel.graXYres30 ;
			model.overallResult = objModel.overallResult ;
			model.totalJig = objModel.totalJig ;
			return model;
		}

		#endregion  Method
	}
}

