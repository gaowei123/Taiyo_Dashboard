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
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:MouldingMaintain_Tracking
	/// </summary>
	public partial class MouldingMaintain_Tracking_DAL
	{
		public MouldingMaintain_Tracking_DAL()
		{}

		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.MouldingMaintain_Tracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MouldingMaintain_Tracking(");
			strSql.Append("MachineID,CheckPeriod,CheckItem,CheckResult,SpareParts,ChangeTime,CheckDate,CheckBy,VerifyBy,Remark)");
			strSql.Append(" values (");
			strSql.Append("@MachineID,@CheckPeriod,@CheckItem,@CheckResult,@SpareParts,@ChangeTime,@CheckDate,@CheckBy,@VerifyBy,@Remark)");
			SqlParameter[] parameters = {
					new SqlParameter("@MachineID", SqlDbType.VarChar,50),
					new SqlParameter("@CheckPeriod", SqlDbType.VarChar,50),
					new SqlParameter("@CheckItem", SqlDbType.VarChar,500),
					new SqlParameter("@CheckResult", SqlDbType.VarChar,50),
					new SqlParameter("@SpareParts", SqlDbType.VarChar,500),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@CheckBy", SqlDbType.VarChar,100),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,1000)};
			parameters[0].Value = model.MachineID;
			parameters[1].Value = model.CheckPeriod;
			parameters[2].Value = model.CheckItem;
			parameters[3].Value = model.CheckResult;
			parameters[4].Value = model.SpareParts;
			parameters[5].Value = model.ChangeTime;
			parameters[6].Value = model.CheckDate;
			parameters[7].Value = model.CheckBy;
			parameters[8].Value = model.VerifyBy;
			parameters[9].Value = model.Remark;

			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.MouldingMaintain_Tracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MouldingMaintain_Tracking set ");
			strSql.Append("MachineID=@MachineID,");
			strSql.Append("CheckPeriod=@CheckPeriod,");
			strSql.Append("CheckItem=@CheckItem,");
			strSql.Append("CheckResult=@CheckResult,");
			strSql.Append("SpareParts=@SpareParts,");
			strSql.Append("ChangeTime=@ChangeTime,");
			strSql.Append("CheckDate=@CheckDate,");
			strSql.Append("CheckBy=@CheckBy,");
			strSql.Append("VerifyBy=@VerifyBy,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@MachineID", SqlDbType.VarChar,50),
					new SqlParameter("@CheckPeriod", SqlDbType.VarChar,50),
					new SqlParameter("@CheckItem", SqlDbType.VarChar,500),
					new SqlParameter("@CheckResult", SqlDbType.VarChar,50),
					new SqlParameter("@SpareParts", SqlDbType.VarChar,500),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@CheckBy", SqlDbType.VarChar,100),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,1000)};
			parameters[0].Value = model.MachineID;
			parameters[1].Value = model.CheckPeriod;
			parameters[2].Value = model.CheckItem;
			parameters[3].Value = model.CheckResult;
			parameters[4].Value = model.SpareParts;
			parameters[5].Value = model.ChangeTime;
			parameters[6].Value = model.CheckDate;
			parameters[7].Value = model.CheckBy;
			parameters[8].Value = model.VerifyBy;
			parameters[9].Value = model.Remark;

			int rows= DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string sCheckPeriod, string sCheckItem, string sMachineID, DateTime dDate)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MouldingMaintain_Tracking ");
            strSql.Append(" where 1=1 ");

            strSql.Append(" and  CheckPeriod = @CheckPeriod");
            strSql.Append(" and  CheckItem = @CheckItem");
            strSql.Append(" and  MachineID = @MachineID");
            strSql.Append(" and  CheckDate = @CheckDate");


            SqlParameter[] parameters = {
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@CheckDate",SqlDbType.DateTime2,0)
            };

            parameters[0].Value = sCheckPeriod;
            parameters[1].Value = sCheckItem;
            parameters[2].Value = sMachineID;
            parameters[3].Value = dDate;

            int rows= DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}




        #endregion  BasicMethod
        
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from MouldingMaintain_Tracking");
            return DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public SqlCommand DeleteCmd(string sCheckPeriod, string sCheckItem, string sMachineID,DateTime dDatetime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MouldingMaintain_Tracking ");
            strSql.Append(" where 1=1 ");

            strSql.Append(" and  CheckPeriod = @CheckPeriod");
            strSql.Append(" and  CheckItem = @CheckItem");
            strSql.Append(" and  MachineID = @MachineID");
            strSql.Append(" and  CheckDate = @CheckDate");


            SqlParameter[] parameters = {
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@CheckDate",SqlDbType.DateTime2,0)
            };

            parameters[0].Value = sCheckPeriod;
            parameters[1].Value = sCheckItem;
            parameters[2].Value = sMachineID;
            parameters[3].Value = dDatetime;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        public SqlCommand AddCmd(Common.Class.Model.MouldingMaintain_Tracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingMaintain_Tracking(");
            strSql.Append("MachineID,CheckPeriod,CheckItem,CheckResult,SpareParts,ChangeTime,CheckDate,CheckBy,VerifyBy,Remark)");
            strSql.Append(" values (");
            strSql.Append("@MachineID,@CheckPeriod,@CheckItem,@CheckResult,@SpareParts,@ChangeTime,@CheckDate,@CheckBy,@VerifyBy,@Remark)");
            SqlParameter[] parameters = {
                    new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                    new SqlParameter("@CheckPeriod", SqlDbType.VarChar,50),
                    new SqlParameter("@CheckItem", SqlDbType.VarChar,500),
                    new SqlParameter("@CheckResult", SqlDbType.VarChar,50),
                    new SqlParameter("@SpareParts", SqlDbType.VarChar,500),
                    new SqlParameter("@ChangeTime", SqlDbType.DateTime),
                    new SqlParameter("@CheckDate", SqlDbType.DateTime),
                    new SqlParameter("@CheckBy", SqlDbType.VarChar,100),
                    new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
                    new SqlParameter("@Remark", SqlDbType.VarChar,1000)};
            parameters[0].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            parameters[1].Value = model.CheckPeriod == null ? (object)DBNull.Value : model.CheckPeriod;
            parameters[2].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            parameters[3].Value = model.CheckResult == null ? (object)DBNull.Value : model.CheckResult;
            parameters[4].Value = model.SpareParts == null ? (object)DBNull.Value : model.SpareParts;
            parameters[5].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            parameters[6].Value = model.CheckDate == null ? (object)DBNull.Value : model.CheckDate;
            parameters[7].Value = model.CheckBy == null ? (object)DBNull.Value : model.CheckBy;
            parameters[8].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            parameters[9].Value = model.Remark == null ? (object)DBNull.Value : model.Remark;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        
    }
}

