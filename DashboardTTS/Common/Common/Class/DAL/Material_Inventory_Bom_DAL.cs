using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common.Class.DAL
{
    public class Material_Inventory_Bom_DAL
    {
        public Material_Inventory_Bom_DAL()
        {

        }

        public DataSet SelectAll(string sMaterialPart)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT 
Material_Part
,Unit_Price
,Updated_User
,CONVERT(varchar(100), Updated_Time, 120) as Updated_Time
,case when isnull(REF_FIELD01,'') ='' then 'NA' else REF_FIELD01 end   as ResinType --树脂型号
,exchangeRate
,convert(varchar, makeup) + '%' as makeup
,Unit_Price_USD
,REF_FIELD02,REF_FIELD03,REF_FIELD04,REF_FIELD05,Remarks
FROM Material_Inventory_Bom where 1=1 ");

            if (sMaterialPart != "")
            {
                strSql.Append(" and Material_Part = '" + sMaterialPart + "' ");
            }

            strSql.Append(" order by Material_Part ");



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }


        public DataTable GetModel(string sMaterialNo)
        {
            if (sMaterialNo.Trim() == "")
            {
                return null;
            }


            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
SELECT top 1 [Material_Part]
      ,ISNULL( [Unit_Price],0) as [Unit_Price]
      ,[Updated_User]
      ,[Updated_Time]
      ,[MakeUp]
      ,[ExchangeRate]
      ,Unit_Price_USD
	  --树脂型号
      ,case when [REF_FIELD01] is null or   [REF_FIELD01] = '' then 'NA' else [REF_FIELD01] end as ResinType
      ,[REF_FIELD02]
      ,[REF_FIELD03]
      ,[REF_FIELD04]
      ,[REF_FIELD05]
      ,[Remarks]
  FROM [Taiyo_Moulding].[dbo].[Material_Inventory_Bom] where 1=1 and Material_Part = @MaterialNo   ");




            SqlParameter[] paras =
            {
                new SqlParameter("@MaterialNo",SqlDbType.VarChar,100)
            };

            paras[0].Value = sMaterialNo;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras,DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public int Add(Common.Model.Material_Inventory_Bom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Material_Inventory_Bom(");
            strSql.Append("Material_Part,Unit_Price,Unit_Price_USD,Updated_User,Updated_Time,REF_FIELD01,MakeUp,ExchangeRate,REF_FIELD02,REF_FIELD03,REF_FIELD04,REF_FIELD05,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@Material_Part,@Unit_Price,@Unit_Price_USD,@Updated_User,@Updated_Time,@REF_FIELD01,@MakeUp ,@ExchangeRate , @REF_FIELD02,@REF_FIELD03,@REF_FIELD04,@REF_FIELD05,@Remarks)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50),
                    new SqlParameter("@Unit_Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Updated_User", SqlDbType.VarChar,50),
                    new SqlParameter("@Updated_Time", SqlDbType.DateTime),
                    new SqlParameter("@REF_FIELD01", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD02", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD03", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD04", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD05", SqlDbType.VarChar,50),
                    new SqlParameter("@Remarks", SqlDbType.VarChar,50),

                    new SqlParameter("@MakeUp", SqlDbType.Decimal),
                    new SqlParameter("@ExchangeRate", SqlDbType.Decimal),
                    new SqlParameter("@Unit_Price_USD", SqlDbType.Decimal)
            };
            parameters[0].Value = model.Material_Part == null ? (object)DBNull.Value : model.Material_Part; 
            parameters[1].Value = model.Unit_Price == null ? (object)DBNull.Value : model.Unit_Price; 
            parameters[2].Value = model.Updated_User == null ? (object)DBNull.Value : model.Updated_User; 
            parameters[3].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time; 
            parameters[4].Value = model.REF_FIELD01 == null ? (object)DBNull.Value : model.REF_FIELD01; 
            parameters[5].Value = model.REF_FIELD02 == null ? (object)DBNull.Value : model.REF_FIELD02; 
            parameters[6].Value = model.REF_FIELD03 == null ? (object)DBNull.Value : model.REF_FIELD03;
            parameters[7].Value = model.REF_FIELD04 == null ? (object)DBNull.Value : model.REF_FIELD04; 
            parameters[8].Value = model.REF_FIELD05 == null ? (object)DBNull.Value : model.REF_FIELD05; 
            parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks; ;

            parameters[10].Value = model.MakeUp == null ? (object)DBNull.Value : model.MakeUp; ;
            parameters[11].Value = model.ExchangeRate == null ? (object)DBNull.Value : model.ExchangeRate;
            parameters[12].Value = model.Unit_Price_USD == null ? (object)DBNull.Value : model.Unit_Price_USD;

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return rows;
        }

        public int Update(Common.Model.Material_Inventory_Bom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Material_Inventory_Bom set ");
            strSql.Append("Material_Part=@Material_Part,");
            strSql.Append("Unit_Price=@Unit_Price,");
            strSql.Append("Unit_Price_USD=@Unit_Price_USD,");
            strSql.Append("Updated_User=@Updated_User,");
            strSql.Append("Updated_Time=@Updated_Time,");
            strSql.Append("REF_FIELD01=@REF_FIELD01,");
            strSql.Append("MakeUp=@MakeUp,");
            strSql.Append("ExchangeRate=@ExchangeRate,");
            strSql.Append("REF_FIELD02=@REF_FIELD02,");
            strSql.Append("REF_FIELD03=@REF_FIELD03,");
            strSql.Append("REF_FIELD04=@REF_FIELD04,");
            strSql.Append("REF_FIELD05=@REF_FIELD05,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where Material_Part=@Material_Part ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50),
                    new SqlParameter("@Unit_Price", SqlDbType.Decimal,9),
                    new SqlParameter("@Updated_User", SqlDbType.VarChar,50),
                    new SqlParameter("@Updated_Time", SqlDbType.DateTime),
                    new SqlParameter("@REF_FIELD01", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD02", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD03", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD04", SqlDbType.VarChar,50),
                    new SqlParameter("@REF_FIELD05", SqlDbType.VarChar,50),
                    new SqlParameter("@Remarks", SqlDbType.VarChar,50),
                    new SqlParameter("@MakeUp", SqlDbType.Decimal),
                    new SqlParameter("@ExchangeRate", SqlDbType.Decimal),
                    new SqlParameter("@Unit_Price_USD", SqlDbType.Decimal)
            };
            parameters[0].Value = model.Material_Part == null ? (object)DBNull.Value : model.Material_Part;
            parameters[1].Value = model.Unit_Price == null ? (object)DBNull.Value : model.Unit_Price;
            parameters[2].Value = model.Updated_User == null ? (object)DBNull.Value : model.Updated_User;
            parameters[3].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
            parameters[4].Value = model.REF_FIELD01 == null ? (object)DBNull.Value : model.REF_FIELD01;
            parameters[5].Value = model.REF_FIELD02 == null ? (object)DBNull.Value : model.REF_FIELD02;
            parameters[6].Value = model.REF_FIELD03 == null ? (object)DBNull.Value : model.REF_FIELD03;
            parameters[7].Value = model.REF_FIELD04 == null ? (object)DBNull.Value : model.REF_FIELD04;
            parameters[8].Value = model.REF_FIELD05 == null ? (object)DBNull.Value : model.REF_FIELD05;
            parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks; ;

            parameters[10].Value = model.MakeUp == null ? (object)DBNull.Value : model.MakeUp; ;
            parameters[11].Value = model.ExchangeRate == null ? (object)DBNull.Value : model.ExchangeRate;
            parameters[12].Value = model.Unit_Price_USD == null ? (object)DBNull.Value : model.Unit_Price_USD;


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return rows;
        }


        public int Delete(string Material_Part)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Material_Inventory_Bom ");
            strSql.Append(" where Material_Part=@Material_Part ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50)
            };
            parameters[0].Value = Material_Part;


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return rows;
        }
    }
}
