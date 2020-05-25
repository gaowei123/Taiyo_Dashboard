using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Common.Class.DAL
{
    public class LMMSBomDetail_DAL
    {
        public LMMSBomDetail_DAL()
        {

        }

        public DataSet GetListByPartNumber(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
          
            strSql.Append(" FROM  LMMSBomDetail    where partNumber = @partNumber ");

            strSql.Append(" order by sn");

            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar) };
            parameters[0].Value = PartNumber;


            return DBHelp.SqlDB.Query(strSql.ToString(),parameters);
        }

        public DataSet SelectMaterialByModel(Common.Class.Model.LMMSBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LMMSBomDetail");
            strSql.Append(" where partNumber = @partNumber and materialPartNo = @materialPartNo");
            //if (type == "add")
            //{
               
            //}
            //else if (type == "update")
            //{
            //    strSql.Append(" where partNumber = @partNumber and materialPartNo not in (@materialPartNo)");
            //}
          
            SqlParameter[] parameters = {
                 new SqlParameter("@partNumber", SqlDbType.VarChar),
                 new SqlParameter("@materialPartNo", SqlDbType.VarChar)
            };
            parameters[0].Value = model.partNumber;
            parameters[1].Value = model.MaterialPartNo;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }


        public DataSet SelectMaterialCount(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LMMSBomDetail");
            strSql.Append(" where partNumber = @partNumber");
        
            SqlParameter[] parameters = {
                 new SqlParameter("@partNumber", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;
         


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        public int DeleteByPart(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBomDetail ");

            if (PartNumber == "")
            {
                DBHelp.Reports.LogFile.Log("Delete warning", "will delete all BOM detail data");
                return -1;
            }
            else
            {
                strSql.Append("where partNumber = @partNumber");
            }


            SqlParameter[] parameters = {
                  new SqlParameter("@partNumber", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;// == null ? (object)DBNull.Value : materialPart;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }


        public DataSet SelectMaterialList(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select sn, partNumber, materialPartNo, partCount, dateTime from LMMSBomDetail  ");

            if (PartNumber != "")
            {
                strSql.Append("  where partNumber  = @partNumber  ");
            }

            strSql.Append("  order by materialPartNo asc  ");

            SqlParameter[] parameters = {
                  new SqlParameter("@partNumber", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        public SqlCommand DeleteCommand(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBomDetail ");

            strSql.Append("where partNumber = @partNumber ");

            SqlParameter[] parameters = {
                  new SqlParameter("@partNumber", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        public SqlCommand DeleteByPartCommadnd(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBomDetail ");

            if (PartNumber == "")
            {
                DBHelp.Reports.LogFile.Log("LMMSBomDetail_DAL DeleteByPartCommadnd :", "PartNumber can't be empty");
                return null;
            }

            strSql.Append("where partNumber = @partNumber");
           
          
            SqlParameter[] parameters = {
                  new SqlParameter("@partNumber", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        public int DeleteMaterial(string MaterialPart, string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBomDetail ");

            if (MaterialPart == "")
            {
                DBHelp.Reports.LogFile.Log("Delete warning", "will delete all BOM detail data");
                return -1;
            }
            else
            {
                strSql.Append("where partNumber = @partNumber and materialPartNo = @materialPartNo");
            }


            SqlParameter[] parameters = {
                  new SqlParameter("@partNumber", SqlDbType.VarChar),
                  new SqlParameter("@materialPartNo", SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;// == null ? (object)DBNull.Value : materialPart;
            parameters[1].Value = MaterialPart;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }



      


        public SqlCommand AddCommand(Common.Class.Model.LMMSBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSBomDetail (");
            strSql.Append("partNumber, sn, materialPartNo, partCount, userName, dateTime )");
            strSql.Append(" values (");
            strSql.Append("@partNumber, @sn, @materialPartNo,@partCount,@userName,@dateTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar),
                    new SqlParameter("@partCount", SqlDbType.Decimal),
                    new SqlParameter("@userName", SqlDbType.VarChar),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@sn", SqlDbType.Int)};

          
            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.MaterialPartNo == null ? (object)DBNull.Value : model.MaterialPartNo;
            parameters[2].Value = model.PartCount == null ? (object)DBNull.Value : model.PartCount;
            parameters[3].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[4].Value = DateTime.Now; //model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[5].Value = model.sn == null ? (object)DBNull.Value : model.sn;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

       

        public bool Add_Rollback(List<SqlCommand> list_cmd)
        {
            if (list_cmd.Count != 0)
            {
                return DBHelp.SqlDB.SetData_Rollback(list_cmd);
            }
            else
            {
                return false;
            }
        }


        public DataTable SearchByPart(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
            strSql.Append(" FROM  LMMSBomDetail  where partNumber = @partNumber ");
            
            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar)};


            parameters[0].Value = PartNumber;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),parameters);


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
