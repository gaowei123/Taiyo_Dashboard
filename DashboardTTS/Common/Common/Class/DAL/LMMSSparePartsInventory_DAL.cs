using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:LMMSSparePartsInventory_Model
	/// </summary>
	public partial class LMMSSparePartsInventory_DAL
	{
		public LMMSSparePartsInventory_DAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSSparePartsInventory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSSparePartsInventory(");
			strSql.Append("sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy)");
			strSql.Append(" values (");
			strSql.Append("@sparePartsName,@quantity,@partsType,@supplier,@unitPrice,@department,@lastUpdatedTime,@createdTime,@lastUpdatedBy)");
			SqlParameter[] parameters = {
					new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
					new SqlParameter("@quantity", SqlDbType.Decimal,9),
					new SqlParameter("@partsType", SqlDbType.VarChar,50),
					new SqlParameter("@supplier", SqlDbType.VarChar,50),
					new SqlParameter("@unitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@department", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@lastUpdatedBy", SqlDbType.VarChar,50)};
            parameters[0].Value = model.sparePartsName == null ? (object)DBNull.Value : model.sparePartsName;
            parameters[1].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[2].Value = model.partsType == null ? (object)DBNull.Value : model.partsType;
            parameters[3].Value = model.supplier == null ? (object)DBNull.Value : model.supplier;
            parameters[4].Value = model.unitPrice == null ? (object)DBNull.Value : model.unitPrice;
            parameters[5].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[6].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[7].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[8].Value = model.lastUpdatedBy == null ? (object)DBNull.Value : model.lastUpdatedBy;

            int rows= DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        public SqlCommand AddCMD(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSSparePartsInventory(");
            strSql.Append("sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy)");
            strSql.Append(" values (");
            strSql.Append("@sparePartsName,@quantity,@partsType,@supplier,@unitPrice,@department,@lastUpdatedTime,@createdTime,@lastUpdatedBy)");
            SqlParameter[] parameters = {
                    new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
                    new SqlParameter("@quantity", SqlDbType.Decimal,9),
                    new SqlParameter("@partsType", SqlDbType.VarChar,50),
                    new SqlParameter("@supplier", SqlDbType.VarChar,50),
                    new SqlParameter("@unitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@department", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@lastUpdatedBy", SqlDbType.VarChar,50)};
            parameters[0].Value = model.sparePartsName == null ? (object)DBNull.Value : model.sparePartsName;
            parameters[1].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[2].Value = model.partsType == null ? (object)DBNull.Value : model.partsType;
            parameters[3].Value = model.supplier == null ? (object)DBNull.Value : model.supplier;
            parameters[4].Value = model.unitPrice == null ? (object)DBNull.Value : model.unitPrice;
            parameters[5].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[6].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[7].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[8].Value = model.lastUpdatedBy == null ? (object)DBNull.Value : model.lastUpdatedBy;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        //history 多3个字段.
        public SqlCommand AddHisCMD(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSSparePartsInventory_His (");
            strSql.Append("sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy,action,usage,machineID)");
            strSql.Append(" values (");
            strSql.Append("@sparePartsName,@quantity,@partsType,@supplier,@unitPrice,@department,@lastUpdatedTime,@createdTime,@lastUpdatedBy,@action,@usage,@machineID)");
            SqlParameter[] parameters = {
                    new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
                    new SqlParameter("@quantity", SqlDbType.Decimal,9),
                    new SqlParameter("@partsType", SqlDbType.VarChar,50),
                    new SqlParameter("@supplier", SqlDbType.VarChar,50),
                    new SqlParameter("@unitPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@department", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@lastUpdatedBy", SqlDbType.VarChar,50),
                    new SqlParameter("@action", SqlDbType.VarChar,50),
                    new SqlParameter("@usage", SqlDbType.Decimal),
                    new SqlParameter("@machineID", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.sparePartsName == null ? (object)DBNull.Value : model.sparePartsName;
            parameters[1].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[2].Value = model.partsType == null ? (object)DBNull.Value : model.partsType;
            parameters[3].Value = model.supplier == null ? (object)DBNull.Value : model.supplier;
            parameters[4].Value = model.unitPrice == null ? (object)DBNull.Value : model.unitPrice;
            parameters[5].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[6].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[7].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[8].Value = model.lastUpdatedBy == null ? (object)DBNull.Value : model.lastUpdatedBy;

            parameters[9].Value = model.action == null ? (object)DBNull.Value : model.action;
            parameters[10].Value = model.usage == null ? (object)DBNull.Value : model.usage;
            parameters[11].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
         
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.LMMSSparePartsInventory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSSparePartsInventory set ");
			strSql.Append("sparePartsName=@sparePartsName,");
			strSql.Append("quantity=@quantity,");
			strSql.Append("partsType=@partsType,");
			strSql.Append("supplier=@supplier,");
			strSql.Append("unitPrice=@unitPrice,");
			strSql.Append("department=@department,");
			strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
			strSql.Append("createdTime=@createdTime,");
			strSql.Append("lastUpdatedBy=@lastUpdatedBy");
			strSql.Append(" where sparePartsName=@sparePartsName ");
			SqlParameter[] parameters = {
					new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
					new SqlParameter("@quantity", SqlDbType.Decimal,9),
					new SqlParameter("@partsType", SqlDbType.VarChar,50),
					new SqlParameter("@supplier", SqlDbType.VarChar,50),
					new SqlParameter("@unitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@department", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@lastUpdatedBy", SqlDbType.VarChar,50)};
            parameters[0].Value = model.sparePartsName == null ? (object)DBNull.Value : model.sparePartsName;
            parameters[1].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[2].Value = model.partsType == null ? (object)DBNull.Value : model.partsType;
            parameters[3].Value = model.supplier == null ? (object)DBNull.Value : model.supplier;
            parameters[4].Value = model.unitPrice == null ? (object)DBNull.Value : model.unitPrice;
            parameters[5].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[6].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[7].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[8].Value = model.lastUpdatedBy == null ? (object)DBNull.Value : model.lastUpdatedBy;

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

        public SqlCommand UpdatePartInventoryCMD(Common.Class.Model.LMMSSparePartsInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update LMMSSparePartsInventory set ");
            
            strSql.Append(" quantity=@quantity,");
            strSql.Append(" lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append(" lastUpdatedBy=@lastUpdatedBy ");
            
            strSql.Append(" where 1=1 ");
            strSql.Append(" and sparePartsName=@sparePartsName");
           
           
           
            SqlParameter[] parameters = {
                    new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
                    new SqlParameter("@quantity", SqlDbType.Decimal,9),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                    new SqlParameter("@lastUpdatedBy", SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.sparePartsName;
            parameters[1].Value = model.quantity;
            parameters[2].Value = model.lastUpdatedTime;
            parameters[3].Value = model.lastUpdatedBy;

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSSparePartsInventory ");
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

        public SqlCommand DeleteCMD(string sparePartsName)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSSparePartsInventory ");
            strSql.Append(" where 1=1 and sparePartsName = @sparePartsName ");
            SqlParameter[] parameters = {
                new SqlParameter("@sparePartsName", SqlDbType.VarChar,100),
            };
            parameters[0].Value = sparePartsName;

           return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Class.Model.LMMSSparePartsInventory_Model GetModel(string sSparePartsName)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy from LMMSSparePartsInventory ");
			strSql.Append(" where 1=1 ");

            strSql.Append(" and sparePartsName = @sparePartsName");




            SqlParameter[] parameters = {
                new SqlParameter("@sparePartsName",SqlDbType.VarChar,50)
			};
            parameters[0].Value = sSparePartsName;
            
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.LMMSSparePartsInventory_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.LMMSSparePartsInventory_Model model=new Common.Class.Model.LMMSSparePartsInventory_Model();
			if (row != null)
			{
				if(row["sparePartsName"]!=null)
				{
					model.sparePartsName=row["sparePartsName"].ToString();
				}
				if(row["quantity"]!=null && row["quantity"].ToString()!="")
				{
					model.quantity=decimal.Parse(row["quantity"].ToString());
				}
				if(row["partsType"]!=null)
				{
					model.partsType=row["partsType"].ToString();
				}
				if(row["supplier"]!=null)
				{
					model.supplier=row["supplier"].ToString();
				}
				if(row["unitPrice"]!=null && row["unitPrice"].ToString()!="")
				{
					model.unitPrice=decimal.Parse(row["unitPrice"].ToString());
				}
				if(row["department"]!=null)
				{
					model.department=row["department"].ToString();
				}
				if(row["lastUpdatedTime"]!=null && row["lastUpdatedTime"].ToString()!="")
				{
					model.lastUpdatedTime=DateTime.Parse(row["lastUpdatedTime"].ToString());
				}
				if(row["createdTime"]!=null && row["createdTime"].ToString()!="")
				{
					model.createdTime=DateTime.Parse(row["createdTime"].ToString());
				}
				if(row["lastUpdatedBy"]!=null)
				{
					model.lastUpdatedBy=row["lastUpdatedBy"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string sSparePartsName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy ");
			strSql.Append(" FROM LMMSSparePartsInventory  where 1=1 ");

			if(sSparePartsName.Trim()!="")
			{
				strSql.Append(" and   sparePartsName = @sparePartsName");
			}


            SqlParameter[] paras =
            {
                new SqlParameter("@sparePartsName",SqlDbType.VarChar,100)
            };

            if (sSparePartsName.Trim() != "") paras[0].Value = sSparePartsName; else paras[0] = null;
         

            return DBHelp.SqlDB.Query(strSql.ToString(),paras);
		}


        public DataSet GetInventoryList(string sSparePartsName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [sparePartsName]
                          ,[quantity]
                          ,[partsType]
                          ,[supplier]
                          ,[unitPrice]
                          ,[department]
                          ,[lastUpdatedTime]
                          ,[createdTime]
                          ,[lastUpdatedBy]
                      FROM [LMMSSparePartsInventory]
                      WHERE 1=1 ");

            if (sSparePartsName.Trim() != "")
            {
                strSql.Append(" and sparePartsName = @sparePartsName");
            }


            SqlParameter[] paras =
            {
                new SqlParameter("@sparePartsName",SqlDbType.VarChar,100)
            };

            if (sSparePartsName.Trim() != "") paras[0].Value = sSparePartsName; else paras[0] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras);
        }

        public DataSet GetPartNameList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct sparePartsName FROM LMMSSparePartsInventory order by sparePartsName asc  ");

            return DBHelp.SqlDB.Query(strSql.ToString());
        }

        public DataSet GetHistory( string sMachineID, string sSparePart, string sAction, DateTime dDateFrom, DateTime dDateTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                            [sparePartsName]
                                ,convert(varchar(50), [quantity]) as balance
                                ,[partsType]
                                ,[supplier]
                                ,[unitPrice]
                                ,[department]
                                ,CONVERT(varchar,lastupdatedTime,120)  as lastUpdatedTime
                                ,[createdTime]
                                ,[lastUpdatedBy]
	                        	,action 
	                            ,quantity 
                                ,usage
                                ,[machineID]
                            FROM [LMMS_TAIYO].[dbo].[LMMSSparePartsInventory_His] where 1=1   ");
         
            
            if (sMachineID.Trim() != "")
            {
                strSql.Append(" and machineID = @machineID");
            }

            if (sSparePart.Trim() != "")
            {
                strSql.Append(" and sparePartsName = @sparePartsName");
            }

            if (sAction.Trim() != "")
            {
                strSql.Append(" and action = @sAction");
            }

            strSql.Append(" and lastUpdatedTime >= @dateFrom ");
            strSql.Append(" and lastUpdatedTime < @dateTo ");


            strSql.Append(" order by lastUpdatedTime asc");


            SqlParameter[] paras =
            {
                new SqlParameter("@machineID",SqlDbType.VarChar,50),
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@sparePartsName",SqlDbType.VarChar,100),
                new SqlParameter("@sAction",SqlDbType.VarChar,100)
            };

         
            if (sMachineID.Trim() != "") paras[0].Value = sMachineID; else paras[0] = null;
            paras[1].Value = dDateFrom;
            paras[2].Value = dDateTo;
            if (sSparePart.Trim() != "") paras[3].Value = sSparePart; else paras[3] = null;
            if (sAction.Trim() != "") paras[4].Value = sAction; else paras[4] = null;

            return DBHelp.SqlDB.Query(strSql.ToString(), paras);
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
			strSql.Append(" sparePartsName,quantity,partsType,supplier,unitPrice,department,lastUpdatedTime,createdTime,lastUpdatedBy ");
			strSql.Append(" FROM LMMSSparePartsInventory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		
	

		

		#endregion  BasicMethod
		
	}
}

