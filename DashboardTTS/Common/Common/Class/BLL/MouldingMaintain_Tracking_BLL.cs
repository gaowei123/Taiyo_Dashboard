/**  版本信息模板在安装目录下，可自行修改。
* MouldingMaintain_Tracking.cs
*
* 功 能： N/A
* 类 名： MouldingMaintain_Tracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/1/8 15:36:32   N/A    初版
*
* Copyright (c) 2012 Common.Class Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// MouldingMaintain_Tracking
	/// </summary>
	public partial class MouldingMaintain_Tracking_BLL
	{
		private readonly Common.Class.DAL.MouldingMaintain_Tracking_DAL dal=new Common.Class.DAL.MouldingMaintain_Tracking_DAL();
		public MouldingMaintain_Tracking_BLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.MouldingMaintain_Tracking_Model model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.MouldingMaintain_Tracking_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string sCheckPeriod, string sCheckItem, string sMachineID, DateTime dDate)
		{
			
			return dal.Delete( sCheckPeriod,  sCheckItem,  sMachineID, dDate);
		}
        
		#endregion  BasicMethod


		#region  MyFunc

        public DataTable GetList()
        {
            DataSet ds = dal.GetList();
            if (ds==null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }

        public bool BatchAdd(List<Common.Class.Model.MouldingMaintain_Tracking_Model> modelList)
        {

            List<SqlCommand> CmdList = new List<SqlCommand>();

          

            foreach (Common.Class.Model.MouldingMaintain_Tracking_Model model in modelList)
            {
                CmdList.Add(dal.AddCmd(model));
            }


            return DBHelp.SqlDB.SetData_Rollback(CmdList, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }



        #endregion
    }
}

