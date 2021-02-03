using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data.SqlClient;


namespace Common.Class.BLL
{
	/// <summary>
	/// LMMSSparePartsInventory_Model
	/// </summary>
	public partial class LMMSSparePartsInventory_BLL
	{
		private readonly Common.Class.DAL.LMMSSparePartsInventory_DAL dal=new Common.Class.DAL.LMMSSparePartsInventory_DAL();
		public LMMSSparePartsInventory_BLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSSparePartsInventory_Model model)
		{
			return dal.Add(model);
		}

        public bool AddRollBack(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.AddCMD(model));

            cmdList.Add(dal.AddHisCMD(model));



            return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        public bool UpdateRollBack(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.UpdatePartInventoryCMD(model));
            cmdList.Add(dal.AddHisCMD(model));


            return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        public bool DeleteRollBack(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.DeleteCMD(model.sparePartsName));
            cmdList.Add(dal.AddHisCMD(model));


            return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.LMMSSparePartsInventory_Model model)
		{
			return dal.Update(model);
		}

        public bool UpdateInventory_RollBack(string sSparePartsName, int inQty,string sUser)
        {
            Common.Class.Model.LMMSSparePartsInventory_Model model = new LMMSSparePartsInventory_Model();
            model = getPartsModel(sSparePartsName);
            model.quantity += inQty;
            model.lastUpdatedBy = sUser;
            model.lastUpdatedTime = DateTime.Now;

            model.action = "IN";
            model.machineID = "";
            model.usage = inQty;


            List <SqlCommand> cmdList = new List<SqlCommand>();

            cmdList.Add(dal.UpdatePartInventoryCMD(model));

            cmdList.Add(dal.AddHisCMD(model));

            
            return DBHelp.SqlDB.SetData_Rollback(cmdList);
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
		public Common.Class.Model.LMMSSparePartsInventory_Model GetModelByName(string sSparePartsName)
		{
			return dal.GetModel(sSparePartsName);
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable  GetList(string sSparePartsName)
		{
            DataSet ds = dal.GetList(sSparePartsName);
            if (ds == null || ds.Tables.Count== 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
		}



        public DataTable GetInventoryList(string sSparePartsName)
        {
            DataSet ds = dal.GetInventoryList(sSparePartsName);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetPartNameList()
        {
            DataSet ds = dal.GetPartNameList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetHistory( string sMachineID, string sSparePart,string sAction, DateTime dDateFrom, DateTime dDateTo)
        {
            DataSet ds = dal.GetHistory( sMachineID, sSparePart,sAction, dDateFrom, dDateTo);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            int TotoalQty = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["usage"].ToString() != "")
                    TotoalQty += int.Parse(dr["usage"].ToString());
            }


            DataRow drTotal = dt.NewRow();
            drTotal["lastUpdatedTime"] = "Total";
            drTotal["usage"] = TotoalQty;
            drTotal["balance"] = "Balance: " + dt.Rows[dt.Rows.Count - 1]["balance"].ToString();
            dt.Rows.Add(drTotal);
            return dt;
        }

        public Common.Class.Model.LMMSSparePartsInventory_Model getPartsModel(string sSparePartsName)
        {
            Common.Class.Model.LMMSSparePartsInventory_Model model = new LMMSSparePartsInventory_Model();


            DataTable dt = GetList(sSparePartsName);
            if (dt == null || dt.Rows.Count == 0)
            {
                model = null;
            }else
            {
                model = dal.DataRowToModel(dt.Rows[0]);
            }

            return model;
        }


		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		


		public List<Common.Class.Model.LMMSSparePartsInventory_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.LMMSSparePartsInventory_Model> modelList = new List<Common.Class.Model.LMMSSparePartsInventory_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.LMMSSparePartsInventory_Model model;
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
		

		#endregion  BasicMethod
	
	}
}

