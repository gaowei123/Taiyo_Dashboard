using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingUserDB
    {
        public MouldingUserDB()  {  }

        public DataTable GetList(string UserID, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select [USER_ID] ,[USER_NAME] ,[PASSWORD] ,[USER_GROUP] ,[UPDATED_TIME] ,[UPDATED_BY] ,[DEPARTMENT] ,[FINGER_TEMPLATE] ,[SHIFT] ,[FINGER_TEMPLATE_1] from User_DB  ");
            strSql.Append("  where 1 = 1  ");

            if (!string.IsNullOrEmpty(UserID))
            {
                strSql.Append("  and (USER_ID = @userID or USER_NAME = @userID) ");
            }

            if (!string.IsNullOrEmpty(password))
            {
                strSql.Append("  and PASSWORD = @password");
            }
            


            SqlParameter[] parameters = {
                    new SqlParameter("@userID", SqlDbType.VarChar),
               new SqlParameter("@password", SqlDbType.VarChar)};

            parameters[0].Value = UserID;
            parameters[1].Value = password;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        internal DataTable GetUserList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select User_ID as userID, USER_NAME as userName, USER_GROUP as userGroup ,UPDATED_BY as updateBy, department,  shift, UPDATED_TIME as datetime,password from User_DB  ");
            strSql.Append("  where USER_GROUP <> '"+StaticRes.Global.UserGroup.ADMIN+"'  and USER_GROUP <> '"+StaticRes.Global.UserGroup.IPQC+"'  ");
            strSql.Append("  order by USER_GROUP, UserID");

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }

        }


    }
}
