using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;


namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:LMMSMachineDownTime
	/// </summary>
	public partial class LMMSMachineDownTime_DAL
	{
		public LMMSMachineDownTime_DAL()
		{}


		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSMachineDownTime_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSMachineDownTime(");
			strSql.Append("machineID,partRunning,cause,action,startTime,stopTime,date,checker,AttachmentPath)");
			strSql.Append(" values (");
			strSql.Append("@machineID,@partRunning,@cause,@action,@startTime,@stopTime,@date,@checker,@AttachmentPath)");
			SqlParameter[] parameters = {
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partRunning", SqlDbType.VarChar,50),
					new SqlParameter("@cause", SqlDbType.VarChar,500),
					new SqlParameter("@action", SqlDbType.VarChar,500),
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@date", SqlDbType.DateTime,8),
					new SqlParameter("@checker", SqlDbType.VarChar,50),
                    new SqlParameter("@AttachmentPath", SqlDbType.VarChar,100)};
            

            parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.partRunning == null ? (object)DBNull.Value : model.partRunning;
            parameters[2].Value = model.cause == null ? (object)DBNull.Value : model.cause;
            parameters[3].Value = model.action == null ? (object)DBNull.Value : model.action;
            parameters[4].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[5].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[6].Value = model.date == null ? (object)DBNull.Value : model.date;
            parameters[7].Value = model.checker == null ? (object)DBNull.Value : model.checker;
            parameters[8].Value = model.attachmentPath == null ? (object)DBNull.Value : model.attachmentPath;

            int rows=  DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);


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
		public bool Update(Common.Class.Model.LMMSMachineDownTime_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSMachineDownTime set ");
			strSql.Append("machineID=@machineID,");
			strSql.Append("partRunning=@partRunning,");
			strSql.Append("cause=@cause,");
			strSql.Append("action=@action,");
			strSql.Append("startTime=@startTime,");
			strSql.Append("stopTime=@stopTime,");
			strSql.Append("date=@date,");
			strSql.Append("checker=@checker");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partRunning", SqlDbType.VarChar,50),
					new SqlParameter("@cause", SqlDbType.VarChar,500),
					new SqlParameter("@action", SqlDbType.VarChar,500),
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@date", SqlDbType.DateTime,8),
					new SqlParameter("@checker", SqlDbType.VarChar,50)};
            parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.partRunning == null ? (object)DBNull.Value : model.partRunning;
            parameters[2].Value = model.cause == null ? (object)DBNull.Value : model.cause;
            parameters[3].Value = model.action == null ? (object)DBNull.Value : model.action;
            parameters[4].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[5].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[6].Value = model.date == null ? (object)DBNull.Value : model.date;
            parameters[7].Value = model.checker == null ? (object)DBNull.Value : model.checker;


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);

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
		public bool Delete()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSMachineDownTime ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);

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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, DateTime? dDay, string sCause)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select  ROW_NUMBER() OVER(ORDER BY date ASC) AS ID
                                , 'Machine'+ machineID as machineID 
                                , partRunning
                                , case when len(cause)  > 25 then convert(varchar(25),cause) + '...' else cause end as cause
                                , case when len(action)  > 25 then convert(varchar(25),action) + '...' else action end as action
                                , cause as completeCause
                                , action as completeAction
                                , date as dateComplete
                                , cause as causeComplete
                                , startTime
                                , stopTime
                                , convert(varchar(50), DATEDIFF(SECOND, startTime,stopTime)) as Time
                                , convert(varchar(100), date,103) as date
                                , checker 
                                , AttachmentPath
                                , AttachmentPath as fileName");
			strSql.Append(" FROM LMMSMachineDownTime where 1=1 ");
            strSql.Append(" and  date >= @DateFrom ");
            strSql.Append(" and  date <= @DateTo ");
            
            if (sMachineID.Trim()!="")
			{
				strSql.Append(" and  machineID = @machineID ");
			}

            if (dDay != null)
            {
                strSql.Append(" and  date = @date ");
            }


            if (sCause != "")
            {
                strSql.Append(" and  cause = @cause ");
            }

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime2),
                new SqlParameter("@DateTo", SqlDbType.DateTime2),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@date", SqlDbType.DateTime),
                new SqlParameter("@cause", SqlDbType.VarChar,500)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (sMachineID.Trim() != "")
            {
                paras[2].Value = sMachineID;
            }else
            {
                paras[2] = null;
            }

            if (dDay != null)
            {
                paras[3].Value = dDay.Value;
            }
            else
            {
                paras[3] = null;
            }

            if (sCause.Trim() != "")
            {
                paras[4].Value = sCause;
            }
            else
            {
                paras[4] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(), paras);
		}
        
		#endregion  BasicMethod


	}
}

