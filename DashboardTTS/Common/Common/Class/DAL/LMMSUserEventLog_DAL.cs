using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class LMMSUserEventLog_DAL
    {
        public LMMSUserEventLog_DAL()
        {

        }

        

        public int Insert(Common.Class.Model.LMMSUserEventLog model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" insert into LMMSUserEventLog ");
                strSql.Append(" (userID, dateTime, startTime,endTime,eventType,pageName,action,jobnumber,material,temp1,temp2,temp3 ");
                strSql.Append(" ) values (");
                strSql.Append(" @userID, @dateTime, @startTime,@endTime,@eventType,@pageName,@action,@jobnumber,@material,@temp1,@temp2,@temp3) ");

                SqlParameter[] parameters = {
                    new SqlParameter("@userID", SqlDbType.VarChar),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@startTime", SqlDbType.DateTime),
                    new SqlParameter("@endTime", SqlDbType.DateTime),
                    new SqlParameter("@eventType", SqlDbType.VarChar),
                    new SqlParameter("@pageName", SqlDbType.VarChar),
                    new SqlParameter("@action", SqlDbType.VarChar),
                    new SqlParameter("@jobnumber", SqlDbType.VarChar),
                    new SqlParameter("@material", SqlDbType.VarChar),
                    new SqlParameter("@temp1", SqlDbType.VarChar),
                    new SqlParameter("@temp2", SqlDbType.VarChar),
                    new SqlParameter("@temp3", SqlDbType.VarChar)};

                parameters[0].Value = model.userID == null ? (object)DBNull.Value : model.userID;
                parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
                parameters[2].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
                parameters[3].Value = model.endTime == null ? (object)DBNull.Value : model.endTime;
                parameters[4].Value = model.eventType == null ? (object)DBNull.Value : model.eventType;
                parameters[5].Value = model.pageName == null ? (object)DBNull.Value : model.pageName;
                parameters[6].Value = model.action == null ? (object)DBNull.Value : model.action;
                parameters[7].Value = model.jobnumber == null ? (object)DBNull.Value : model.jobnumber;
                parameters[8].Value = model.material == null ? (object)DBNull.Value : model.material;
                parameters[9].Value = model.temp1 == null ? (object)DBNull.Value : model.temp1;
                parameters[10].Value = model.temp2 == null ? (object)DBNull.Value : model.temp2;
                parameters[11].Value = model.temp3 == null ? (object)DBNull.Value : model.temp3;


                return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LMMSUserLog_DAL", "Insert error :" + ee.ToString());
                return 0;
            }
        }


        public SqlCommand AddCommand(Common.Class.Model.LMMSUserEventLog model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into LMMSUserEventLog ");
            strSql.Append(" (userID, dateTime, startTime,endTime,eventType,pageName,action,jobnumber,material,temp1,temp2,temp3 ");
            strSql.Append(" ) values (");
            strSql.Append(" @userID, @dateTime, @startTime,@endTime,@eventType,@pageName,@action,@jobnumber,@material,@temp1,@temp2,@temp3) ");

            SqlParameter[] parameters = {
                    new SqlParameter("@userID", SqlDbType.VarChar),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@startTime", SqlDbType.DateTime),
                    new SqlParameter("@endTime", SqlDbType.DateTime),
                    new SqlParameter("@eventType", SqlDbType.VarChar),
                    new SqlParameter("@pageName", SqlDbType.VarChar),
                    new SqlParameter("@action", SqlDbType.VarChar),
                    new SqlParameter("@jobnumber", SqlDbType.VarChar),
                    new SqlParameter("@material", SqlDbType.VarChar),
                    new SqlParameter("@temp1", SqlDbType.VarChar),
                    new SqlParameter("@temp2", SqlDbType.VarChar),
                    new SqlParameter("@temp3", SqlDbType.VarChar)};

            parameters[0].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[2].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[3].Value = model.endTime == null ? (object)DBNull.Value : model.endTime;
            parameters[4].Value = model.eventType == null ? (object)DBNull.Value : model.eventType;
            parameters[5].Value = model.pageName == null ? (object)DBNull.Value : model.pageName;
            parameters[6].Value = model.action == null ? (object)DBNull.Value : model.action;
            parameters[7].Value = model.jobnumber == null ? (object)DBNull.Value : model.jobnumber;
            parameters[8].Value = model.material == null ? (object)DBNull.Value : model.material;
            parameters[9].Value = model.temp1 == null ? (object)DBNull.Value : model.temp1;
            parameters[10].Value = model.temp2 == null ? (object)DBNull.Value : model.temp2;
            parameters[11].Value = model.temp3 == null ? (object)DBNull.Value : model.temp3;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);

        }

        public DataTable SearchByJob(string Jobnumber)
        {
            try
            {
                string strSql = "";
                strSql = "select * from LMMSUserEventLog where jobnumber = @jobnumber";

                SqlParameter[] paras =
                {
                    new SqlParameter("@jobnumber",SqlDbType.VarChar)
                };

                paras[0].Value = Jobnumber;

                DataSet ds = DBHelp.SqlDB.Query(strSql, paras);

                if (ds == null || ds.Tables.Count ==0)
                {
                    return null;
                }else
                {
                    return ds.Tables[0];
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LMMSUserLog_DAL", "SearchByJob error :" + ee.ToString());
                return null;
            }
        }

        public DataTable SearchByMaterial(string Jobnumber,string Material)
        {
            try
            {
                string strSql = "";
                strSql = "select * from LMMSUserEventLog where material = @material and jobnumber = @Jobnumber";

                SqlParameter[] paras =
                {
                    new SqlParameter("@material",SqlDbType.VarChar),
                         new SqlParameter("@jobnumber",SqlDbType.VarChar)
                };

                paras[0].Value = Material;
                paras[1].Value = Jobnumber;

                DataSet ds = DBHelp.SqlDB.Query(strSql, paras);

                if (ds == null || ds.Tables.Count == 0)
                {
                    return null;
                }
                else
                {
                    return ds.Tables[0];
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LMMSUserLog_DAL", "SearchByJob error :" + ee.ToString());
                return null;
            }
        }
    }
}
