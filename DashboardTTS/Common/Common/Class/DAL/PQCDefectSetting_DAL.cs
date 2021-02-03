
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:PQCDefectSetting_DAL
	/// </summary>
	public class PQCDefectSetting_DAL
	{
		public PQCDefectSetting_DAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Class.Model.PQCDefectSetting_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCDefectSetting(");
			strSql.Append("defectCodeID,defectCode,defectDescription,materialPartNo,model,jigNo,machineID,userName,dateTime,remarks)");
			strSql.Append(" values (");
			strSql.Append("@defectCodeID,@defectCode,@defectDescription,@materialPartNo,@model,@jigNo,@machineID,@userName,@dateTime,@remarks)");
			SqlParameter[] parameters = {
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@defectDescription", SqlDbType.VarChar,200),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@remarks", SqlDbType.VarChar,500)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCDefectSetting_DAL" , 
                 "Function:		public void Add(Common.Class.Model.PQCDefectSetting_Model model)"  + 
                 "TableName:PQCDefectSetting" , 
                 ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString() ) + 
                 ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString() ) + 
                 ";defectDescription = "+ (model.defectDescription == null ? "" : model.defectDescription.ToString() ) +
                 ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString() ) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString() ) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString() ) + 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + 
                 ";userName = "+ (model.userName == null ? "" : model.userName.ToString() ) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString() ) + "");
			parameters[0].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[1].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[2].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription ;
			parameters[3].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[4].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[7].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[8].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[9].Value = model.remarks == null ? (object)DBNull.Value : model.remarks ;

			DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Class.Model.PQCDefectSetting_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCDefectSetting(");
			strSql.Append("defectCodeID,defectCode,defectDescription,materialPartNo,model,jigNo,machineID,userName,dateTime,remarks)");
			strSql.Append(" values (");
			strSql.Append("@defectCodeID,@defectCode,@defectDescription,@materialPartNo,@model,@jigNo,@machineID,@userName,@dateTime,@remarks)");
			SqlParameter[] parameters = {
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@defectDescription", SqlDbType.VarChar,200),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@remarks", SqlDbType.VarChar,500)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCDefectSetting_DAL" , 
                 "Function:		public SqlCommand AddCommand(Common.Class.Model.PQCDefectSetting_Model model)"  + 
                 "TableName:PQCDefectSetting" , 
                 ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString()) + 
                 ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString()) + 
                 ";defectDescription = "+ (model.defectDescription == null ? "" : model.defectDescription.ToString()) +
                 ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString()) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString()) + 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + 
                 ";userName = "+ (model.userName == null ? "" : model.userName.ToString()) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString()) + "");
			parameters[0].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[1].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[2].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription ;
			parameters[3].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[4].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[7].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[8].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[9].Value = model.remarks == null ? (object)DBNull.Value : model.remarks ;

			 return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCDefectSetting_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCDefectSetting set ");
			strSql.Append("defectCodeID=@defectCodeID,");
			strSql.Append("defectCode=@defectCode,");
			strSql.Append("defectDescription=@defectDescription,");
			strSql.Append("materialPartNo=@materialPartNo,");
			strSql.Append("model=@model,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("userName=@userName,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("remarks=@remarks");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@defectDescription", SqlDbType.VarChar,200),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@remarks", SqlDbType.VarChar,500)};
			parameters[0].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[1].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[2].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription ;
			parameters[3].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[4].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[7].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[8].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[9].Value = model.remarks == null ? (object)DBNull.Value : model.remarks ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCDefectSetting_DAL" , 
                 "Function:		public bool Update(Common.Class.Model.PQCDefectSetting_Model model)"  + 
                 "TableName:PQCDefectSetting" , 
                 ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString() ) + 
                 ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString() ) + 
                 ";defectDescription = "+ (model.defectDescription == null ? "" : model.defectDescription.ToString() ) +
                 ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString() ) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString() ) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString() ) + 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + 
                 ";userName = "+ (model.userName == null ? "" : model.userName.ToString() ) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString() ) + "");
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
		public SqlCommand UpdateCommand(Common.Class.Model.PQCDefectSetting_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCDefectSetting set ");
			strSql.Append("defectCodeID=@defectCodeID,");
			strSql.Append("defectCode=@defectCode,");
			strSql.Append("defectDescription=@defectDescription,");
			strSql.Append("materialPartNo=@materialPartNo,");
			strSql.Append("model=@model,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("userName=@userName,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("remarks=@remarks");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@defectDescription", SqlDbType.VarChar,200),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@remarks", SqlDbType.VarChar,500)};
			parameters[0].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[1].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[2].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription ;
			parameters[3].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[4].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[7].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[8].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[9].Value = model.remarks == null ? (object)DBNull.Value : model.remarks ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCDefectSetting_DAL" , 
                 "Function:		public SqlCommand UpdateCommand(Common.Class.Model.PQCDefectSetting_Model model)"  + 
                 "TableName:PQCDefectSetting" , 
                 ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString()) + 
                 ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString()) + 
                 ";defectDescription = "+ (model.defectDescription == null ? "" : model.defectDescription.ToString()) +
                 ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString()) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString()) + 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + 
                 ";userName = "+ (model.userName == null ? "" : model.userName.ToString()) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString()) + "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCDefectSetting ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCDefectSetting_DAL" , "Function:		public bool Delete()"  + "TableName:PQCDefectSetting" , "");
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
			strSql.Append("delete from PQCDefectSetting ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCDefectSetting_DAL" , "Function:		public SqlCommand DeleteCommand()"  + "TableName:PQCDefectSetting" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCDefectSetting ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCDefectSetting_DAL" , "Function:		public SqlCommand DeleteAllCommand( )"  + "TableName:PQCDefectSetting" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCDefectSetting_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 defectCodeID,defectCode,defectDescription,materialPartNo,model,jigNo,machineID,userName,dateTime,remarks from PQCDefectSetting ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCDefectSetting_DAL" , "Function:		public Common.Class.Model.PQCDefectSetting_Model GetModel()"  + "TableName:PQCDefectSetting" , "");
			Common.Class.Model.PQCDefectSetting_Model model=new Common.Class.Model.PQCDefectSetting_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.defectCodeID=ds.Tables[0].Rows[0]["defectCodeID"].ToString();
				model.defectCode=ds.Tables[0].Rows[0]["defectCode"].ToString();
				model.defectDescription=ds.Tables[0].Rows[0]["defectDescription"].ToString();
				model.materialPartNo = ds.Tables[0].Rows[0]["materialPartNo"].ToString();
				model.model=ds.Tables[0].Rows[0]["model"].ToString();
				model.jigNo=ds.Tables[0].Rows[0]["jigNo"].ToString();
				model.machineID=ds.Tables[0].Rows[0]["machineID"].ToString();
				model.userName=ds.Tables[0].Rows[0]["userName"].ToString();
				if(ds.Tables[0].Rows[0]["dateTime"].ToString()!="")
				{
					model.dateTime=DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
				}
				model.remarks=ds.Tables[0].Rows[0]["remarks"].ToString();
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
		public DataSet GetList(string sRejType)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * FROM PQCDefectSetting");
            strSql.Append(" where  1=1  ");
            
            if (sRejType.Trim()!="")
			{
				strSql.Append(" and defectDescription = @defectDescription ");
			}

            strSql.Append(" order by convert(decimal,defectCodeID) asc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@defectDescription",SqlDbType.VarChar,50)
            };

            if (sRejType.Trim() != "")
            {
                paras[0].Value = sRejType;
            }else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(),paras,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}


        public DataTable GetAllForPQCLaserTotalReport()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            convert(decimal,defectcodeID) as defectcodeID
                            ,defectDescription
                            ,case when defectDescription = 'Others' then 'OTHERS ' + defectCode
	                              when defectDescription = 'Mould' then 'MOULDING ' + defectCode
                                  when defectDescription = 'Paint' then 'PAINTING ' + defectCode
	                              when defectDescription = 'Laser' then 'LASER ' + defectCode
                            end  as defectCode
                            ,defectCode as defectCodeSource
                            FROM PQCDefectSetting");


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_PQC_Server);



            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }


        }




        #endregion  Method
    }
}

