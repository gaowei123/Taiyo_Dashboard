using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Common.Class.DAL
{
    public class User_DB_DAL
    {
        
        public DataTable GetList(string sDepartment, string sEmployeeID, string sUserGroup, string sPassword)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT 
USER_ID, USER_NAME, PASSWORD, USER_GROUP, UPDATED_TIME, UPDATED_BY, DEPARTMENT, FINGER_TEMPLATE, SHIFT, FINGER_TEMPLATE_1, EMPLOYEE_ID, DEPARTMENT_ID
FROM User_DB 
where 1=1 ");

            if (!string.IsNullOrEmpty(sDepartment)) strSql.Append(" and DEPARTMENT = @sDepartment ");
            if (!string.IsNullOrEmpty(sUserGroup)) strSql.Append(" and USER_GROUP = @sUserGroup ");
            if (!string.IsNullOrEmpty(sEmployeeID)) strSql.Append(" and ( EMPLOYEE_ID = @sEmployeeID or USER_ID = @sEmployeeID or  USER_NAME = @sEmployeeID) ");
            if (!string.IsNullOrEmpty(sPassword)) strSql.Append(" and PASSWORD = @sPassword ");

            strSql.Append(" order by EMPLOYEE_ID asc ");



            SqlParameter[] paras =
            {
                new SqlParameter("@sDepartment",SqlDbType.VarChar),
                new SqlParameter("@sUserGroup",SqlDbType.VarChar),
                new SqlParameter("@sEmployeeID",SqlDbType.VarChar),
                new SqlParameter("@sPassword",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sDepartment)) paras[0].Value = sDepartment;  else paras[0] = null;
            if (!string.IsNullOrEmpty(sUserGroup))  paras[1].Value = sUserGroup;   else paras[1] = null;
            if (!string.IsNullOrEmpty(sEmployeeID))  paras[2].Value = sEmployeeID; else paras[2] = null;
            if (!string.IsNullOrEmpty(sPassword)) paras[3].Value = sPassword; else paras[3] = null;




            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        
        public int Add(Common.Class.Model.User_DB_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into User_DB ( ");
            strSql.Append(" EMPLOYEE_ID, USER_ID,USER_NAME,PASSWORD,USER_GROUP,UPDATED_TIME,UPDATED_BY,DEPARTMENT,FINGER_TEMPLATE,SHIFT,FINGER_TEMPLATE_1 ) ");

            strSql.Append(" Values ( ");
            strSql.Append(" @EMPLOYEE_ID, @USER_ID,@USER_NAME, @PASSWORD, @USER_GROUP, @UPDATED_TIME, @UPDATED_BY, @DEPARTMENT, @FINGER_TEMPLATE, @SHIFT, @FINGER_TEMPLATE_1  ) ");



            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_NAME",SqlDbType.VarChar),
                new SqlParameter("@PASSWORD",SqlDbType.VarChar),
                new SqlParameter("@USER_GROUP",SqlDbType.VarChar),
                new SqlParameter("@UPDATED_TIME",SqlDbType.DateTime),
                new SqlParameter("@UPDATED_BY",SqlDbType.VarChar),
                new SqlParameter("@DEPARTMENT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE",SqlDbType.VarChar),
                new SqlParameter("@SHIFT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE_1",SqlDbType.VarChar)
            };

            paras[0].Value = model.EMPLOYEE_ID == null ? (object)DBNull.Value : model.EMPLOYEE_ID;
            paras[1].Value = model.USER_ID == null ? (object)DBNull.Value : model.USER_ID;
            paras[2].Value = model.USER_NAME == null ? (object)DBNull.Value : model.USER_NAME;
            paras[3].Value = model.PASSWORD == null ? (object)DBNull.Value : model.PASSWORD;
            paras[4].Value = model.USER_GROUP == null ? (object)DBNull.Value : model.USER_GROUP;
            paras[5].Value = model.UPDATED_TIME == null ? (object)DBNull.Value : model.UPDATED_TIME;
            paras[6].Value = model.UPDATED_BY == null ? (object)DBNull.Value : model.UPDATED_BY;
            paras[7].Value = model.DEPARTMENT == null ? (object)DBNull.Value : model.DEPARTMENT;
            paras[8].Value = model.FINGER_TEMPLATE == null ? (object)DBNull.Value : model.FINGER_TEMPLATE;
            paras[9].Value = model.SHIFT == null ? (object)DBNull.Value : model.SHIFT;
            paras[10].Value = model.FINGER_TEMPLATE_1 == null ? (object)DBNull.Value : model.FINGER_TEMPLATE_1;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras);
        }

        public int Add(Common.Class.Model.User_DB_Model model, SqlConnection cn)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into User_DB ( ");
            strSql.Append(" EMPLOYEE_ID, USER_ID,USER_NAME,PASSWORD,USER_GROUP,UPDATED_TIME,UPDATED_BY,DEPARTMENT,FINGER_TEMPLATE,SHIFT,FINGER_TEMPLATE_1 ) ");

            strSql.Append(" Values ( ");
            strSql.Append(" @EMPLOYEE_ID, @USER_ID,@USER_NAME, @PASSWORD, @USER_GROUP, @UPDATED_TIME, @UPDATED_BY, @DEPARTMENT, @FINGER_TEMPLATE, @SHIFT, @FINGER_TEMPLATE_1  ) ");



            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_NAME",SqlDbType.VarChar),
                new SqlParameter("@PASSWORD",SqlDbType.VarChar),
                new SqlParameter("@USER_GROUP",SqlDbType.VarChar),
                new SqlParameter("@UPDATED_TIME",SqlDbType.DateTime),
                new SqlParameter("@UPDATED_BY",SqlDbType.VarChar),
                new SqlParameter("@DEPARTMENT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE",SqlDbType.VarChar),
                new SqlParameter("@SHIFT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE_1",SqlDbType.VarChar)
            };

            paras[0].Value = model.EMPLOYEE_ID == null ? (object)DBNull.Value : model.EMPLOYEE_ID;
            paras[1].Value = model.USER_ID == null ? (object)DBNull.Value : model.USER_ID;
            paras[2].Value = model.USER_NAME == null ? (object)DBNull.Value : model.USER_NAME;
            paras[3].Value = model.PASSWORD == null ? (object)DBNull.Value : model.PASSWORD;
            paras[4].Value = model.USER_GROUP == null ? (object)DBNull.Value : model.USER_GROUP;
            paras[5].Value = model.UPDATED_TIME == null ? (object)DBNull.Value : model.UPDATED_TIME;
            paras[6].Value = model.UPDATED_BY == null ? (object)DBNull.Value : model.UPDATED_BY;
            paras[7].Value = model.DEPARTMENT == null ? (object)DBNull.Value : model.DEPARTMENT;
            paras[8].Value = model.FINGER_TEMPLATE == null ? (object)DBNull.Value : model.FINGER_TEMPLATE;
            paras[9].Value = model.SHIFT == null ? (object)DBNull.Value : model.SHIFT;
            paras[10].Value = model.FINGER_TEMPLATE_1 == null ? (object)DBNull.Value : model.FINGER_TEMPLATE_1;


            

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, cn);
        }

        public int Update(Common.Class.Model.User_DB_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Update  User_DB set  ");

            strSql.Append(" USER_ID = @USER_ID  ");
            strSql.Append(" ,USER_NAME = @USER_NAME  ");
            strSql.Append(" , PASSWORD = @PASSWORD  ");
            strSql.Append(" , USER_GROUP = @USER_GROUP  ");
            strSql.Append(" , UPDATED_TIME = @UPDATED_TIME  ");
            strSql.Append(" , UPDATED_BY = @UPDATED_BY  ");
            strSql.Append(" , DEPARTMENT = @DEPARTMENT  ");
            strSql.Append(" , FINGER_TEMPLATE = @FINGER_TEMPLATE  ");
            strSql.Append(" , SHIFT = @SHIFT  ");
            strSql.Append(" , FINGER_TEMPLATE_1 = @FINGER_TEMPLATE_1  ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and  EMPLOYEE_ID = @EMPLOYEE_ID ");



            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_NAME",SqlDbType.VarChar),
                new SqlParameter("@PASSWORD",SqlDbType.VarChar),
                new SqlParameter("@USER_GROUP",SqlDbType.VarChar),
                new SqlParameter("@UPDATED_TIME",SqlDbType.DateTime),
                new SqlParameter("@UPDATED_BY",SqlDbType.VarChar),
                new SqlParameter("@DEPARTMENT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE",SqlDbType.VarChar),
                new SqlParameter("@SHIFT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE_1",SqlDbType.VarChar)
            };

            paras[0].Value = model.EMPLOYEE_ID == null ? (object)DBNull.Value : model.EMPLOYEE_ID;
            paras[1].Value = model.USER_ID == null ? (object)DBNull.Value : model.USER_ID;
            paras[2].Value = model.USER_NAME == null ? (object)DBNull.Value : model.USER_NAME;
            paras[3].Value = model.PASSWORD == null ? (object)DBNull.Value : model.PASSWORD;
            paras[4].Value = model.USER_GROUP == null ? (object)DBNull.Value : model.USER_GROUP;
            paras[5].Value = model.UPDATED_TIME == null ? (object)DBNull.Value : model.UPDATED_TIME;
            paras[6].Value = model.UPDATED_BY == null ? (object)DBNull.Value : model.UPDATED_BY;
            paras[7].Value = model.DEPARTMENT == null ? (object)DBNull.Value : model.DEPARTMENT;
            paras[8].Value = model.FINGER_TEMPLATE == null ? (object)DBNull.Value : model.FINGER_TEMPLATE;
            paras[9].Value = model.SHIFT == null ? (object)DBNull.Value : model.SHIFT;
            paras[10].Value = model.FINGER_TEMPLATE_1 == null ? (object)DBNull.Value : model.FINGER_TEMPLATE_1;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras);
        }

        public int Update(Common.Class.Model.User_DB_Model model, SqlConnection cn)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Update  User_DB set  ");

            strSql.Append(" USER_ID = @USER_ID  ");
            strSql.Append(" ,USER_NAME = @USER_NAME  ");
            strSql.Append(" , PASSWORD = @PASSWORD  ");
            strSql.Append(" , USER_GROUP = @USER_GROUP  ");
            strSql.Append(" , UPDATED_TIME = @UPDATED_TIME  ");
            strSql.Append(" , UPDATED_BY = @UPDATED_BY  ");
            strSql.Append(" , DEPARTMENT = @DEPARTMENT  ");
            strSql.Append(" , FINGER_TEMPLATE = @FINGER_TEMPLATE  ");
            strSql.Append(" , SHIFT = @SHIFT  ");
            strSql.Append(" , FINGER_TEMPLATE_1 = @FINGER_TEMPLATE_1  ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and  EMPLOYEE_ID = @EMPLOYEE_ID ");



            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_ID",SqlDbType.VarChar),
                new SqlParameter("@USER_NAME",SqlDbType.VarChar),
                new SqlParameter("@PASSWORD",SqlDbType.VarChar),
                new SqlParameter("@USER_GROUP",SqlDbType.VarChar),
                new SqlParameter("@UPDATED_TIME",SqlDbType.DateTime),
                new SqlParameter("@UPDATED_BY",SqlDbType.VarChar),
                new SqlParameter("@DEPARTMENT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE",SqlDbType.VarChar),
                new SqlParameter("@SHIFT",SqlDbType.VarChar),
                new SqlParameter("@FINGER_TEMPLATE_1",SqlDbType.VarChar)
            };

            paras[0].Value = model.EMPLOYEE_ID == null ? (object)DBNull.Value : model.EMPLOYEE_ID;
            paras[1].Value = model.USER_ID == null ? (object)DBNull.Value : model.USER_ID;
            paras[2].Value = model.USER_NAME == null ? (object)DBNull.Value : model.USER_NAME;
            paras[3].Value = model.PASSWORD == null ? (object)DBNull.Value : model.PASSWORD;
            paras[4].Value = model.USER_GROUP == null ? (object)DBNull.Value : model.USER_GROUP;
            paras[5].Value = model.UPDATED_TIME == null ? (object)DBNull.Value : model.UPDATED_TIME;
            paras[6].Value = model.UPDATED_BY == null ? (object)DBNull.Value : model.UPDATED_BY;
            paras[7].Value = model.DEPARTMENT == null ? (object)DBNull.Value : model.DEPARTMENT;
            paras[8].Value = model.FINGER_TEMPLATE == null ? (object)DBNull.Value : model.FINGER_TEMPLATE;
            paras[9].Value = model.SHIFT == null ? (object)DBNull.Value : model.SHIFT;
            paras[10].Value = model.FINGER_TEMPLATE_1 == null ? (object)DBNull.Value : model.FINGER_TEMPLATE_1;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, cn);
        }

        public int Delete(string sEmployeeID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Delete from User_DB where EMPLOYEE_ID = @EMPLOYEE_ID ");

            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar)
            };

            paras[0].Value = sEmployeeID;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras);
        }

        public int Delete(string sEmployeeID, SqlConnection cn)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Delete from User_DB where EMPLOYEE_ID = @EMPLOYEE_ID ");

            SqlParameter[] paras =
            {
                new SqlParameter("@EMPLOYEE_ID",SqlDbType.VarChar)
            };

            paras[0].Value = sEmployeeID;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, cn);
        }


        public DataTable SelectManPower()
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@" 
select 
DEPARTMENT as department
,count(1) as manPower 
from User_DB
where USER_GROUP != 'Admin' and USER_GROUP != 'IPQC'
group by DEPARTMENT ");

            

            DataSet ds =  DBHelp.SqlDB.Query(strSql.ToString());
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }

      

    }
}
