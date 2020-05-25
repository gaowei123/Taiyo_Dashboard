using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common.Class.DAL
{
    public class MouldingCheckReport_DAL
    {
        public  MouldingCheckReport_DAL()
        {        
        }

        public bool Add(Common.Class.Model.MouldingCheckReport_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingCheckReport(");
            strSql.Append("Report,Verify_User,Verify_ID,Verify_Time,refField01,refField02,refField03,refField04,refField05,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@Report,@Verify_User,@Verify_ID,@Verify_Time,@refField01,@refField02,@refField03,@refField04,@refField05,@Remarks)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@Report", SqlDbType.VarChar,50),
                    new SqlParameter("@Verify_User", SqlDbType.VarChar,50),
                    new SqlParameter("@Verify_ID", SqlDbType.VarChar,50),
                    new SqlParameter("@Verify_Time", SqlDbType.DateTime),
                    new SqlParameter("@refField01", SqlDbType.DateTime),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),
                    new SqlParameter("@Remarks", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID == null ? (object)DBNull.Value : model.ID;
            parameters[1].Value = model.Report == null ? (object)DBNull.Value : model.Report;
            parameters[2].Value = model.Verify_User == null ? (object)DBNull.Value : model.Verify_User;
            parameters[3].Value = model.Verify_ID == null ? (object)DBNull.Value : model.Verify_ID;
            parameters[4].Value = model.Verify_Time == null ? (object)DBNull.Value : model.Verify_Time;
            parameters[5].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[6].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            parameters[7].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            parameters[8].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            parameters[9].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            parameters[10].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetList(DateTime? dDay,string  sShift)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select 
ID,
refField01 as day,
Report as shift,
Verify_User as verityBy,
Verify_Time as verifyDate,

Verify_ID,
refField01,
refField02,
refField03,
refField04,
refField05,
Remarks
FROM MouldingCheckReport where 1=1 ");

            if (dDay != null)
                strSql.Append(" and refField01 = @date ");
            if (sShift != "")
                strSql.Append(" and Report = @shift ");
            

            SqlParameter[] parameters = {
                new SqlParameter("@date", SqlDbType.DateTime2),
                new SqlParameter("@shift", SqlDbType.VarChar)
            };



            if (dDay != null) parameters[0].Value = dDay;else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;

            
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

    }
}
