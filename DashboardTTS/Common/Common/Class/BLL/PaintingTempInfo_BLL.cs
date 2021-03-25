using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// PaintingTempInfo
	/// </summary>
	public partial class PaintingTempInfo
	{
		private readonly Common.Class.DAL.PaintingTempInfo_DAL dal=new Common.Class.DAL.PaintingTempInfo_DAL();
		public PaintingTempInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PaintingTempInfo_Model model)
		{
			return dal.Add(model);
		}

        public bool AddRollBack(List<Common.Class.Model.PaintingTempInfo_Model> listPaintingTempInfo ,string sUserName)
        {
           
            List<SqlCommand> cmdList = new List<SqlCommand>();

            foreach (Common.Class.Model.PaintingTempInfo_Model model in listPaintingTempInfo)
            {
               
                model.recordBy = sUserName;
                cmdList.Add(dal.AddCommand(model));
            }

            
            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PaintingTempInfo_Model model)
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
		public Common.Class.Model.PaintingTempInfo_Model GetModel(string sJobNumber)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(sJobNumber);
		}

        public bool UpdateQASetup(List<PaintingTempInfo_Model> modelList)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();
            foreach (var model in modelList)
            {
                cmdList.Add(dal.UpdateQASetupCommand(model));
            }
            

            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sJobnumber )
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo, "", sJobnumber, "");

            if (ds== null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
		}

        public bool Exist(string sJobnumber, string sCheckProcess)
        {
            DataSet ds = dal.GetList(null, null, "", sJobnumber,sCheckProcess);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }



        #endregion  BasicMethod






        public DataTable  GetPaintTempInfoForButtonReport_NEW(string strWhere)
        {
            DataSet ds = dal.GetPaintTempInfoForButtonReport_NEW(strWhere);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;     
        }


        public DataTable GetBuyoffList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJobNo)
        {
            DataSet ds = dal.GetBuyoffList(dDateFrom, dDateTo, sPartNo, sJobNo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }

    }
}

