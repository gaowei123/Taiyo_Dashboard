 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.Class.BLL
{
	/// <summary>
	/// PQCDefectSetting_BLL
	/// </summary>
	public class PQCDefectSetting_BLL
	{
		private readonly Common.Class.DAL.PQCDefectSetting_DAL dal=new Common.Class.DAL.PQCDefectSetting_DAL();
		public PQCDefectSetting_BLL()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Class.Model.PQCDefectSetting_Model model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Class.Model.PQCDefectSetting_Model model)
		{
			return dal.AddCommand(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCDefectSetting_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Class.Model.PQCDefectSetting_Model model)
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
		public Common.Class.Model.PQCDefectSetting_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable  GetList(string sRejType)
		{
            DataSet ds = dal.GetList(sRejType);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
		}

        public DataTable GetAllForPQCLaserTotalReport()
        {
            DataTable dt = dal.GetAllForPQCLaserTotalReport();

            

            if (dt.Select(string.Format(" defectCodeSource = '{0}' and defectDescription =  'Paint' ", "Shortage")).Length == 0)
            {
                DataRow drShortage = dt.NewRow();
                drShortage["defectcodeID"] = 101;
                drShortage["defectDescription"] = "Paint";
                drShortage["defectCode"] = "PAINTING Shortage";
                drShortage["defectCodeSource"] = "Shortage";

                dt.Rows.Add(drShortage);
            }

            if (dt.Select(string.Format(" defectCodeSource = '{0}' and defectDescription =  'Laser'", "Set-up")).Length == 0)
            {
                DataRow drSetup = dt.NewRow();
                drSetup["defectcodeID"] = 102;
                drSetup["defectDescription"] = "Laser";
                drSetup["defectCode"] = "LASER Set-up";
                drSetup["defectCodeSource"] = "Set-up";
                dt.Rows.Add(drSetup);
            }

            if (dt.Select(string.Format(" defectCodeSource = '{0}' and defectDescription =  'Laser'", "Buyoff")).Length == 0)
            {
                DataRow drBuyoff = dt.NewRow();
                drBuyoff["defectcodeID"] = 103;
                drBuyoff["defectDescription"] = "Laser";
                drBuyoff["defectCode"] = "LASER Buyoff";
                drBuyoff["defectCodeSource"] = "Buyoff";
                dt.Rows.Add(drBuyoff);
            }

            DataRow drPaintQA = dt.NewRow();
            drPaintQA["defectcodeID"] = 104;
            drPaintQA["defectDescription"] = "Others";
            drPaintQA["defectCode"] = "OTHERS QA";
            drPaintQA["defectCodeSource"] = "QA";
            dt.Rows.Add(drPaintQA);





            return dt;
        }



        /// <summary>
        /// 获得前几行数据
        /// </summary>

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCDefectSetting_Model> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Common.Class.Model.PQCDefectSetting_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCDefectSetting_Model> modelList = new List<Common.Class.Model.PQCDefectSetting_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCDefectSetting_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Common.Class.Model.PQCDefectSetting_Model();
					model.defectCodeID=dt.Rows[n]["defectCodeID"].ToString();
					model.defectCode=dt.Rows[n]["defectCode"].ToString();
					model.defectDescription=dt.Rows[n]["defectDescription"].ToString();
					model.materialPartNo=dt.Rows[n]["materialPartNo"].ToString();
					model.model=dt.Rows[n]["model"].ToString();
					model.jigNo=dt.Rows[n]["jigNo"].ToString();
					model.machineID=dt.Rows[n]["machineID"].ToString();
					model.userName=dt.Rows[n]["userName"].ToString();
					if(dt.Rows[n]["dateTime"].ToString()!="")
					{
						model.dateTime=DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
					}
					model.remarks=dt.Rows[n]["remarks"].ToString();
					modelList.Add(model);
				}
			}
			return modelList;
		}


        /// <summary>
        /// 重复获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCDefectSetting_Model> DataTableToList(List<Common.Class.Model.PQCDefectSetting_Model> modelList, DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCDefectSetting_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.PQCDefectSetting_Model();
                    model.defectCodeID = dt.Rows[n]["defectCodeID"].ToString();
                    model.defectCode = dt.Rows[n]["defectCode"].ToString();
                    model.defectDescription = dt.Rows[n]["defectDescription"].ToString();
                    model.materialPartNo = dt.Rows[n]["materialPartNo"].ToString();
                    model.model = dt.Rows[n]["model"].ToString();
                    model.jigNo = dt.Rows[n]["jigNo"].ToString();
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    model.userName = dt.Rows[n]["userName"].ToString();
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.remarks = dt.Rows[n]["remarks"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }




     
		

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public Common.Class.Model.PQCDefectSetting_Model CopyObj(Common.Class.Model.PQCDefectSetting_Model objModel )
		{
			Common.Class.Model.PQCDefectSetting_Model model;
			model = new Common.Class.Model.PQCDefectSetting_Model();
			model.defectCodeID = objModel.defectCodeID ;
			model.defectCode = objModel.defectCode ;
			model.defectDescription = objModel.defectDescription ;
			model.materialPartNo = objModel.materialPartNo;
			model.model = objModel.model ;
			model.jigNo = objModel.jigNo ;
			model.machineID = objModel.machineID ;
			model.userName = objModel.userName ;
			model.dateTime = objModel.dateTime ;
			model.remarks = objModel.remarks ;
			return model;
		}

        #endregion  Method


        #region MyRegion
        
       
            



        #endregion
    }
}

