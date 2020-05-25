using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.DAL
{
    public class PaintingInventory_DAL
    {
        public PaintingInventory_DAL() { }

        public SqlCommand AddCommand(Common.Class.Model.PaintingInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintInventory (");
            strSql.Append("jobNumber ,partNumber,description,quantity,startOnTime,pqcQuantity ,dateTime,day,month,year,showFlag,LotNo");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@description,@quantity,@startOnTime,@pqcQuantity,@dateTime,@day,@month,@year,@showFlag,@LotNo)");
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@description", SqlDbType.VarChar),
                new SqlParameter("@quantity", SqlDbType.Decimal),
                new SqlParameter("@startOnTime", SqlDbType.DateTime),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@day", SqlDbType.VarChar),
                new SqlParameter("@month", SqlDbType.VarChar),
                new SqlParameter("@year", SqlDbType.VarChar),
                new SqlParameter("@pqcQuantity", SqlDbType.VarChar),
                new SqlParameter("@showFlag", SqlDbType.VarChar),
                new SqlParameter("@LotNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.JobNumber == null ? (object)DBNull.Value : model.JobNumber;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.description == null ? (object)DBNull.Value : model.description;
            parameters[3].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[4].Value = model.startOnTime == null ? (object)DBNull.Value : model.startOnTime;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[7].Value = model.month == null ? (object)DBNull.Value : model.month;
            parameters[8].Value = model.year == null ? (object)DBNull.Value : model.year;
            parameters[9].Value = model.PQCQuantity == null ? (object)DBNull.Value : model.PQCQuantity;
            parameters[10].Value = model.ShowFlag == null ? (object)DBNull.Value : model.ShowFlag;
            parameters[11].Value = model.Lotno == null ? (object)DBNull.Value : model.Lotno;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }

        public int Add(Common.Class.Model.PaintingInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintInventory (");
            strSql.Append("jobNumber ,partNumber,description,quantity,startOnTime,pqcQuantity ,dateTime,day,month,year,showFlag,LotNo");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@description,@quantity,@startOnTime,@pqcQuantity,@dateTime,@day,@month,@year,@showFlag,@LotNo)");
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@description", SqlDbType.VarChar),
                new SqlParameter("@quantity", SqlDbType.Decimal),
                new SqlParameter("@startOnTime", SqlDbType.DateTime),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@day", SqlDbType.VarChar),
                new SqlParameter("@month", SqlDbType.VarChar),
                new SqlParameter("@year", SqlDbType.VarChar),
                new SqlParameter("@pqcQuantity", SqlDbType.VarChar),
                new SqlParameter("@showFlag", SqlDbType.VarChar),
                new SqlParameter("@LotNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.JobNumber == null ? (object)DBNull.Value : model.JobNumber;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.description == null ? (object)DBNull.Value : model.description;
            parameters[3].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[4].Value = model.startOnTime == null ? (object)DBNull.Value : model.startOnTime;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[7].Value = model.month == null ? (object)DBNull.Value : model.month;
            parameters[8].Value = model.year == null ? (object)DBNull.Value : model.year;
            parameters[9].Value = model.PQCQuantity == null ? (object)DBNull.Value : model.PQCQuantity;
            parameters[10].Value = model.ShowFlag == null ? (object)DBNull.Value : model.ShowFlag;
            parameters[11].Value = model.Lotno == null ? (object)DBNull.Value : model.Lotno;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }



        public DataTable SelectInventoryDailyReport(string partNo, string customer)
        {
            StringBuilder strSql = new StringBuilder();

            #region latest optimized sql 
            strSql.Append(@"select
                              [jobNumber]
                              ,[partNumber]
                              ,[description]
                              ,[quantity]
                              ,[pqcQuantity]
                              ,[startOnTime]
                              ,[dateTime]
                              ,[day]
                              ,[month]
                              ,[year]
                              ,[showFlag]
                              ,[setUpQTY]
                              ,[buyOffQty]
                              ,[lotNo]
                          FROM [Taiyo_Painting].[dbo].[PaintInventory] where 1=1
                            ");
            #endregion


            if (partNo != "")
            {
                strSql.Append(" and a.partnumber = @partnumber ");
            }

            //if (customer != "")
            //{
            //    strSql.Append(" and Customer = @Customer ");
            //}

            //old sql
            //strSql.Append(" group by a.partNumber , b.materialCount, b.cycleTime, b.blockCount, b.unitCount");
           // strSql.Append(" group by a.partNumber   order by Customer asc, a.partNumber asc ");

            SqlParameter[] parameters = {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@Customer",SqlDbType.VarChar)
            };

            if (partNo == "") { parameters[0] = null; } else { parameters[0].Value = partNo; }

            if (customer == "") { parameters[1] = null; } else { parameters[1].Value = customer; }



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);

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
