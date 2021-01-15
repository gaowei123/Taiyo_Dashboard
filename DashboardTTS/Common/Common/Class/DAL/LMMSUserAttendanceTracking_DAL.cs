 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:LMMSUserAttendanceTracking_DAL
	/// </summary>
	public class LMMSUserAttendanceTracking_DAL
	{
		public LMMSUserAttendanceTracking_DAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSUserAttendanceTracking(");
			strSql.Append("UserID,UserName,UserGroup,Department,Shift,Attendance,OnLeave,Day,UpdateBy,DateTime,Remarks)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@UserName,@UserGroup,@Department,@Shift,@Attendance,@OnLeave,@Day,@UpdateBy,@DateTime,@Remarks)");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserGroup", SqlDbType.VarChar,50),
					new SqlParameter("@Department", SqlDbType.VarChar,50),
					new SqlParameter("@Shift", SqlDbType.VarChar,50),
					new SqlParameter("@Attendance", SqlDbType.VarChar,50),
					new SqlParameter("@OnLeave", SqlDbType.VarChar,50),
					new SqlParameter("@Day", SqlDbType.DateTime2,8),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@DateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@Remarks", SqlDbType.VarChar,-1)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public void Add(Common.Model.LMMSUserAttendanceTracking_Model model)"  + "TableName:LMMSUserAttendanceTracking" , ";UserID = "+ (model.UserID == null ? "" : model.UserID.ToString() ) + ";UserName = "+ (model.UserName == null ? "" : model.UserName.ToString() ) + ";UserGroup = "+ (model.UserGroup == null ? "" : model.UserGroup.ToString() ) + ";Department = "+ (model.Department == null ? "" : model.Department.ToString() ) + ";Shift = "+ (model.Shift == null ? "" : model.Shift.ToString() ) + ";Attendance = "+ (model.Attendance == null ? "" : model.Attendance.ToString() ) + ";OnLeave = "+ (model.OnLeave == null ? "" : model.OnLeave.ToString() ) + ";Day = "+ (model.Day == null ? "" : model.Day.ToString() ) + ";UpdateBy = "+ (model.UpdateBy == null ? "" : model.UpdateBy.ToString() ) + ";DateTime = "+ (model.DateTime == null ? "" : model.DateTime.ToString() ) + ";Remarks = "+ (model.Remarks == null ? "" : model.Remarks.ToString() ) + "");
			parameters[0].Value = model.UserID == null ? (object)DBNull.Value : model.UserID ;
			parameters[1].Value = model.UserName == null ? (object)DBNull.Value : model.UserName ;
			parameters[2].Value = model.UserGroup == null ? (object)DBNull.Value : model.UserGroup ;
			parameters[3].Value = model.Department == null ? (object)DBNull.Value : model.Department ;
			parameters[4].Value = model.Shift == null ? (object)DBNull.Value : model.Shift ;
			parameters[5].Value = model.Attendance == null ? (object)DBNull.Value : model.Attendance ;
			parameters[6].Value = model.OnLeave == null ? (object)DBNull.Value : model.OnLeave ;
			parameters[7].Value = model.Day == null ? (object)DBNull.Value : model.Day ;
			parameters[8].Value = model.UpdateBy == null ? (object)DBNull.Value : model.UpdateBy ;
			parameters[9].Value = model.DateTime == null ? (object)DBNull.Value : model.DateTime ;
			parameters[10].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks ;

			DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSUserAttendanceTracking(");
			strSql.Append("UserID,UserName,UserGroup,Department,Shift,Attendance,OnLeave,Day,UpdateBy,DateTime,Remarks,EmployeeID)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@UserName,@UserGroup,@Department,@Shift,@Attendance,@OnLeave,@Day,@UpdateBy,@DateTime,@Remarks,@EmployeeID)");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserGroup", SqlDbType.VarChar,50),
					new SqlParameter("@Department", SqlDbType.VarChar,50),
					new SqlParameter("@Shift", SqlDbType.VarChar,50),
					new SqlParameter("@Attendance", SqlDbType.VarChar,50),
					new SqlParameter("@OnLeave", SqlDbType.VarChar,50),
					new SqlParameter("@Day", SqlDbType.DateTime2,8),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@DateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@Remarks", SqlDbType.VarChar,-1),
                    new SqlParameter("@EmployeeID", SqlDbType.VarChar,50)
            };
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public SqlCommand AddCommand(Common.Model.LMMSUserAttendanceTracking_Model model)"  + "TableName:LMMSUserAttendanceTracking" , ";UserID = "+ (model.UserID == null ? "" : model.UserID.ToString()) + ";UserName = "+ (model.UserName == null ? "" : model.UserName.ToString()) + ";UserGroup = "+ (model.UserGroup == null ? "" : model.UserGroup.ToString()) + ";Department = "+ (model.Department == null ? "" : model.Department.ToString()) + ";Shift = "+ (model.Shift == null ? "" : model.Shift.ToString()) + ";Attendance = "+ (model.Attendance == null ? "" : model.Attendance.ToString()) + ";OnLeave = "+ (model.OnLeave == null ? "" : model.OnLeave.ToString()) + ";Day = "+ (model.Day == null ? "" : model.Day.ToString()) + ";UpdateBy = "+ (model.UpdateBy == null ? "" : model.UpdateBy.ToString()) + ";DateTime = "+ (model.DateTime == null ? "" : model.DateTime.ToString()) + ";Remarks = "+ (model.Remarks == null ? "" : model.Remarks.ToString()) + "");
			parameters[0].Value = model.UserID == null ? (object)DBNull.Value : model.UserID ;
			parameters[1].Value = model.UserName == null ? (object)DBNull.Value : model.UserName ;
			parameters[2].Value = model.UserGroup == null ? (object)DBNull.Value : model.UserGroup ;
			parameters[3].Value = model.Department == null ? (object)DBNull.Value : model.Department ;
			parameters[4].Value = model.Shift == null ? (object)DBNull.Value : model.Shift ;
			parameters[5].Value = model.Attendance == null ? (object)DBNull.Value : model.Attendance ;
			parameters[6].Value = model.OnLeave == null ? (object)DBNull.Value : model.OnLeave ;
			parameters[7].Value = model.Day == null ? (object)DBNull.Value : model.Day ;
			parameters[8].Value = model.UpdateBy == null ? (object)DBNull.Value : model.UpdateBy ;
			parameters[9].Value = model.DateTime == null ? (object)DBNull.Value : model.DateTime ;
			parameters[10].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks ;
            parameters[11].Value = model.EmployeeID == null ? (object)DBNull.Value : model.EmployeeID;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSUserAttendanceTracking set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserGroup=@UserGroup,");
			strSql.Append("Department=@Department,");
			strSql.Append("Shift=@Shift,");
			strSql.Append("Attendance=@Attendance,");
			strSql.Append("OnLeave=@OnLeave,");
			strSql.Append("Day=@Day,");
			strSql.Append("UpdateBy=@UpdateBy,");
			strSql.Append("DateTime=@DateTime,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserGroup", SqlDbType.VarChar,50),
					new SqlParameter("@Department", SqlDbType.VarChar,50),
					new SqlParameter("@Shift", SqlDbType.VarChar,50),
					new SqlParameter("@Attendance", SqlDbType.VarChar,50),
					new SqlParameter("@OnLeave", SqlDbType.VarChar,50),
					new SqlParameter("@Day", SqlDbType.DateTime2,8),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@DateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@Remarks", SqlDbType.VarChar,-1)};
			parameters[0].Value = model.UserID == null ? (object)DBNull.Value : model.UserID ;
			parameters[1].Value = model.UserName == null ? (object)DBNull.Value : model.UserName ;
			parameters[2].Value = model.UserGroup == null ? (object)DBNull.Value : model.UserGroup ;
			parameters[3].Value = model.Department == null ? (object)DBNull.Value : model.Department ;
			parameters[4].Value = model.Shift == null ? (object)DBNull.Value : model.Shift ;
			parameters[5].Value = model.Attendance == null ? (object)DBNull.Value : model.Attendance ;
			parameters[6].Value = model.OnLeave == null ? (object)DBNull.Value : model.OnLeave ;
			parameters[7].Value = model.Day == null ? (object)DBNull.Value : model.Day ;
			parameters[8].Value = model.UpdateBy == null ? (object)DBNull.Value : model.UpdateBy ;
			parameters[9].Value = model.DateTime == null ? (object)DBNull.Value : model.DateTime ;
			parameters[10].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public bool Update(Common.Model.LMMSUserAttendanceTracking_Model model)"  + "TableName:LMMSUserAttendanceTracking" , ";UserID = "+ (model.UserID == null ? "" : model.UserID.ToString() ) + ";UserName = "+ (model.UserName == null ? "" : model.UserName.ToString() ) + ";UserGroup = "+ (model.UserGroup == null ? "" : model.UserGroup.ToString() ) + ";Department = "+ (model.Department == null ? "" : model.Department.ToString() ) + ";Shift = "+ (model.Shift == null ? "" : model.Shift.ToString() ) + ";Attendance = "+ (model.Attendance == null ? "" : model.Attendance.ToString() ) + ";OnLeave = "+ (model.OnLeave == null ? "" : model.OnLeave.ToString() ) + ";Day = "+ (model.Day == null ? "" : model.Day.ToString() ) + ";UpdateBy = "+ (model.UpdateBy == null ? "" : model.UpdateBy.ToString() ) + ";DateTime = "+ (model.DateTime == null ? "" : model.DateTime.ToString() ) + ";Remarks = "+ (model.Remarks == null ? "" : model.Remarks.ToString() ) + "");
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
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Model.LMMSUserAttendanceTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSUserAttendanceTracking set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserGroup=@UserGroup,");
			strSql.Append("Department=@Department,");
			strSql.Append("Shift=@Shift,");
			strSql.Append("Attendance=@Attendance,");
			strSql.Append("OnLeave=@OnLeave,");
			strSql.Append("Day=@Day,");
			strSql.Append("UpdateBy=@UpdateBy,");
			strSql.Append("DateTime=@DateTime,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserGroup", SqlDbType.VarChar,50),
					new SqlParameter("@Department", SqlDbType.VarChar,50),
					new SqlParameter("@Shift", SqlDbType.VarChar,50),
					new SqlParameter("@Attendance", SqlDbType.VarChar,50),
					new SqlParameter("@OnLeave", SqlDbType.VarChar,50),
					new SqlParameter("@Day", SqlDbType.DateTime2,8),
					new SqlParameter("@UpdateBy", SqlDbType.VarChar,50),
					new SqlParameter("@DateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@Remarks", SqlDbType.VarChar,-1)};
			parameters[0].Value = model.UserID == null ? (object)DBNull.Value : model.UserID ;
			parameters[1].Value = model.UserName == null ? (object)DBNull.Value : model.UserName ;
			parameters[2].Value = model.UserGroup == null ? (object)DBNull.Value : model.UserGroup ;
			parameters[3].Value = model.Department == null ? (object)DBNull.Value : model.Department ;
			parameters[4].Value = model.Shift == null ? (object)DBNull.Value : model.Shift ;
			parameters[5].Value = model.Attendance == null ? (object)DBNull.Value : model.Attendance ;
			parameters[6].Value = model.OnLeave == null ? (object)DBNull.Value : model.OnLeave ;
			parameters[7].Value = model.Day == null ? (object)DBNull.Value : model.Day ;
			parameters[8].Value = model.UpdateBy == null ? (object)DBNull.Value : model.UpdateBy ;
			parameters[9].Value = model.DateTime == null ? (object)DBNull.Value : model.DateTime ;
			parameters[10].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public SqlCommand UpdateCommand(Common.Model.LMMSUserAttendanceTracking_Model model)"  + "TableName:LMMSUserAttendanceTracking" , ";UserID = "+ (model.UserID == null ? "" : model.UserID.ToString()) + ";UserName = "+ (model.UserName == null ? "" : model.UserName.ToString()) + ";UserGroup = "+ (model.UserGroup == null ? "" : model.UserGroup.ToString()) + ";Department = "+ (model.Department == null ? "" : model.Department.ToString()) + ";Shift = "+ (model.Shift == null ? "" : model.Shift.ToString()) + ";Attendance = "+ (model.Attendance == null ? "" : model.Attendance.ToString()) + ";OnLeave = "+ (model.OnLeave == null ? "" : model.OnLeave.ToString()) + ";Day = "+ (model.Day == null ? "" : model.Day.ToString()) + ";UpdateBy = "+ (model.UpdateBy == null ? "" : model.UpdateBy.ToString()) + ";DateTime = "+ (model.DateTime == null ? "" : model.DateTime.ToString()) + ";Remarks = "+ (model.Remarks == null ? "" : model.Remarks.ToString()) + "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSUserAttendanceTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public bool Delete()"  + "TableName:LMMSUserAttendanceTracking" , "");
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
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSUserAttendanceTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public SqlCommand DeleteCommand()"  + "TableName:LMMSUserAttendanceTracking" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSUserAttendanceTracking ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public SqlCommand DeleteAllCommand( )"  + "TableName:LMMSUserAttendanceTracking" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Model.LMMSUserAttendanceTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserID,UserName,UserGroup,Department,Shift,Attendance,OnLeave,Day,UpdateBy,DateTime,Remarks from LMMSUserAttendanceTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSUserAttendanceTracking_DAL" , "Function:		public Common.Model.LMMSUserAttendanceTracking_Model GetModel()"  + "TableName:LMMSUserAttendanceTracking" , "");
			Common.Model.LMMSUserAttendanceTracking_Model model=new Common.Model.LMMSUserAttendanceTracking_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.UserID=ds.Tables[0].Rows[0]["UserID"].ToString();
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.UserGroup=ds.Tables[0].Rows[0]["UserGroup"].ToString();
				model.Department=ds.Tables[0].Rows[0]["Department"].ToString();
				model.Shift=ds.Tables[0].Rows[0]["Shift"].ToString();
				model.Attendance=ds.Tables[0].Rows[0]["Attendance"].ToString();
				model.OnLeave=ds.Tables[0].Rows[0]["OnLeave"].ToString();
				if(ds.Tables[0].Rows[0]["Day"].ToString()!="")
				{
					model.Day=DateTime.Parse(ds.Tables[0].Rows[0]["Day"].ToString());
				}
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				if(ds.Tables[0].Rows[0]["DateTime"].ToString()!="")
				{
					model.DateTime=DateTime.Parse(ds.Tables[0].Rows[0]["DateTime"].ToString());
				}
				model.Remarks=ds.Tables[0].Rows[0]["Remarks"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,UserName,UserGroup,Department,Shift,Attendance,OnLeave,Day,UpdateBy,DateTime,Remarks ");
			strSql.Append(" FROM LMMSUserAttendanceTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" UserID,UserName,UserGroup,Department,Shift,Attendance,OnLeave,Day,UpdateBy,DateTime,Remarks ");
			strSql.Append(" FROM LMMSUserAttendanceTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "LMMSUserAttendanceTracking";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method


        #region MyRegion
            
        internal DataSet getAttendance(DateTime dDay, string sShift, string sDepartment)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"   select count(distinct userid)  as UserCount , usergroup  , Attendance FROM [dbo].[LMMSUserAttendanceTracking]
                               where day = @Day and Shift = @Shift and Department =@Department  and UserID is not null and userid != '' group by usergroup  , Attendance ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Day", SqlDbType.DateTime),
                     new SqlParameter("@Shift", SqlDbType.VarChar,50 ),
                      new SqlParameter("@Department", SqlDbType.VarChar , 50)
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;
            parameters[2].Value = sDepartment;


            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSUserAttendanceTracking_DAL", "Function:   internal DataSet getAttendance(DateTime dDay, string sShift, string sDepartment)    "
                + "TableName:LMMSUserAttendanceTracking", ";Day = " + dDay.ToString() + " ; Shift = " + sShift.ToString() + " ;sDepartment = " + sDepartment.ToString() + "");
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);

        }
        
        internal DataSet GetAttendanceByDayDept(DateTime dDay, string sDepartment)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT
                              a.[UserID] AS USER_ID
                              ,a.[UserName] AS USER_NAME
                              ,a.[UserGroup] AS USER_GROUP
                              ,a.[Department] AS DEPARTMENT
                              ,a.[Shift] AS SHIFT
                              ,a.[Attendance] 
                              ,a.[OnLeave]
                              ,a.[Day] 
                              ,a.[UpdateBy]
                              ,a.[DateTime]
                              ,a.[Remarks]
                          FROM [LMMSUserAttendanceTracking] a  ");
            strSql.Append(" where  ");
            strSql.Append(" a.Day=@Day  ");
            if (sDepartment.Trim().Length > 0)
            {
                strSql.Append(" and a.Department = @Department ");
            }
            strSql.Append(" order by a.usergroup , a.userid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Day", SqlDbType.DateTime2,8),
                    new SqlParameter("@Department", SqlDbType.VarChar , 50) };
            parameters[0].Value = dDay == null ? (object)DBNull.Value : dDay;
            parameters[1].Value = sDepartment == null || sDepartment.Trim().Length == 0 ? (object)DBNull.Value : sDepartment;

            
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        public SqlCommand DeleteAttendanceByDayDept(DateTime dDay, string sDepartment)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSUserAttendanceTracking ");
            strSql.Append(" where ");
            strSql.Append("Day=@Day  and Department = @Department  ");
            SqlParameter[] parameters = {
            new SqlParameter("@Day", SqlDbType.DateTime2, 8),
            new SqlParameter("@Department", SqlDbType.VarChar , 50) };
            parameters[0].Value = dDay == null ? (object)DBNull.Value : dDay;
            parameters[1].Value = sDepartment == null ? (object)DBNull.Value : sDepartment;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSUserAttendanceTracking_DAL", "Function:   internal SqlCommand DeleteAttendanceByDay(DateTime dDay, string sDepartment)     "
                + "TableName:LMMSUserAttendanceTracking", ";Day = " + dDay.ToString() + " ;sDepartment = " + sDepartment.ToString() + "" );
 
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        #endregion





        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sDepartment)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT UserID
                                  ,UserName
                                  ,UserGroup
                                  ,Department
                                  ,Shift
                                  ,Attendance
                                  ,OnLeave
                                  ,Day
                                  ,UpdateBy
                                  ,DateTime
                                  ,Remarks
                                  ,EmployeeID
                              FROM LMMSUserAttendanceTracking where 1=1 and Day >= @dateFrom  and Day < @dateTo");

            if (!string.IsNullOrEmpty(sDepartment)) strSql.Append(" and Department = @Department");


            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime),
                new SqlParameter("@dateTo", SqlDbType.DateTime),
                new SqlParameter("@Department", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sDepartment)) parameters[2].Value = sDepartment;else parameters[2] = null;
         
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetUserAttendanceSummary(DateTime dDateFrom, DateTime dDateTo, string sDepartment)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" 
select 
EmployeeID
,b.USER_NAME
,sum(case when Attendance = 'Attendance' then 1 
		  when Attendance in( 'Business Trip','WFH') and OnLeave = 'Day' then 1
		  when Attendance in( 'Business Trip','WFH') and (OnLeave ='AM' or OnLeave = 'PM') then 0.5
          when attendance not in ( 'Business Trip','WFH') and (OnLeave ='AM' or OnLeave = 'PM') then 0.5
		  else 0 end) as attendanceDays
,sum(case when Attendance not in ('Business Trip','WFH','Attendance') and OnLeave = 'Day' then 1
		  when Attendance not in ('Business Trip','WFH','Attendance') and (OnLeave ='AM' or OnLeave = 'PM') then 0.5
		  else 0 end ) as leaveDays
from LMMSUserAttendanceTracking  a 
left join User_DB b on a.EmployeeID = b.EMPLOYEE_ID
where 1=1 and day >= @dateFrom and day < @dateTo ");
            
            if (!string.IsNullOrEmpty(sDepartment)) strSql.AppendLine(" and a.department = @Department ");



            strSql.AppendLine(" group by EmployeeID, b.USER_NAME");


            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2,8),
                new SqlParameter("@dateTo", SqlDbType.DateTime2,8),
                new SqlParameter("@Department", SqlDbType.VarChar , 50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sDepartment)) parameters[2].Value = sDepartment;else parameters[2] = null;


            DataSet ds= DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            if (ds == null|| ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }



    }
}

