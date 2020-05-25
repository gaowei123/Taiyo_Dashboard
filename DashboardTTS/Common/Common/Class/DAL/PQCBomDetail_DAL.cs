using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;

namespace Common.Class.DAL
{
    public class PQCBomDetail_DAL
    {
        public PQCBomDetail_DAL()
        { }

        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Common.Class.Model.PQCBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCBomDetail(");
            strSql.Append("sn,partNumber,materialPartNo,partCount,userName,dateTime,partImage,color)");
            strSql.Append(" values (");
            strSql.Append("@sn,@partNumber,@materialPartNo,@partCount,@userName,@dateTime,@partImage,@color)");
            SqlParameter[] parameters = {
                    new SqlParameter("@sn", SqlDbType.Int),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partCount", SqlDbType.Decimal),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@partImage", SqlDbType.Image),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", 
                "NameSpace:Common.DAL", 
                "Class:PQCBomDetail_DAL",
                "Function:		public int Add(Common.Class.Model.PQCBomDetail_Model model)" + 
                "TableName:PQCBomDetail",
                ";sn = " + (model.sn == null ? "" : model.sn.ToString()) +
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";partCount = " + (model.partCount == null ? "" : model.partCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partImage = " + (model.partImage == null ? "" : model.partImage.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()));
            parameters[0].Value = model.sn == null ? (object)DBNull.Value : model.sn;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[3].Value = model.partCount == null ? (object)DBNull.Value : model.partCount;
            parameters[4].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.partImage == null ? (object)DBNull.Value : model.partImage;
            parameters[7].Value = model.color == null ? (object)DBNull.Value : model.color;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Class.Model.PQCBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCBomDetail(");
            strSql.Append("sn,partNumber,materialPartNo,partCount,userName,dateTime,partImage,color,imagePath,imageAbsolutePath,materialName,customer,outerBoxQty,packingTrays,module )");
            strSql.Append(" values (");
            strSql.Append("@sn,@partNumber,@materialPartNo,@partCount,@userName,@dateTime,@partImage,@color,@imagePath,@imageAbsolutePath ,@materialName,@customer,@outerBoxQty,@packingTrays,@module )");
            SqlParameter[] parameters = {
                    new SqlParameter("@sn", SqlDbType.Int),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partCount", SqlDbType.Decimal),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@partImage", SqlDbType.Image),
                    new SqlParameter("@color", SqlDbType.VarChar,50),
                    new SqlParameter("@imagePath", SqlDbType.VarChar,100),
                    new SqlParameter("@imageAbsolutePath", SqlDbType.VarChar,100),

                    new SqlParameter("@materialName", SqlDbType.VarChar,100),
                    new SqlParameter("@customer", SqlDbType.VarChar,100),
                    new SqlParameter("@outerBoxQty", SqlDbType.VarChar,100),
                    new SqlParameter("@packingTrays", SqlDbType.VarChar,100),
                    new SqlParameter("@module", SqlDbType.VarChar,100)

            };

         
            parameters[0].Value = model.sn == null ? (object)DBNull.Value : model.sn;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[3].Value = model.partCount == null ? (object)DBNull.Value : model.partCount;
            parameters[4].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.partImage == null ? (object)DBNull.Value : model.partImage;
            parameters[7].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[8].Value = model.imagePath == null ? (object)DBNull.Value : model.imagePath;
            parameters[9].Value = model.imageAbsolutePath == null ? (object)DBNull.Value : model.imageAbsolutePath;
            parameters[10].Value = model.materialName == null ? (object)DBNull.Value : model.materialName;
            parameters[11].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[12].Value = model.outerBoxQty == null ? (object)DBNull.Value : model.outerBoxQty;
            parameters[13].Value = model.packingTrays == null ? (object)DBNull.Value : model.packingTrays;
            parameters[14].Value = model.module == null ? (object)DBNull.Value : model.module;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }




    

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCBomDetail set ");
            strSql.Append("sn=@sn,");
            strSql.Append("partNumber=@partNumber,");
            strSql.Append("materialPartNo=@materialPartNo,");
            strSql.Append("partCount=@partCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("partImage=@partImage");
            strSql.Append("color=@color");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@sn", SqlDbType.Int),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partCount", SqlDbType.Decimal),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@partImage", SqlDbType.Image),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            parameters[0].Value = model.sn == null ? (object)DBNull.Value : model.sn;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[3].Value = model.partCount == null ? (object)DBNull.Value : model.partCount;
            parameters[4].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.partImage == null ? (object)DBNull.Value : model.partImage;
            parameters[7].Value = model.color == null ? (object)DBNull.Value : model.color;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCBomDetail_DAL", "Function:		public bool Update(Common.Class.Model.PQCBomDetail_Model model)" +
                "TableName:PQCBomDetail",
                ";sn = " + (model.sn == null ? "" : model.sn.ToString()) +
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";partCount = " + (model.partCount == null ? "" : model.partCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partImage = " + (model.partImage == null ? "" : model.partImage.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()));

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        public SqlCommand UpdateCommand(Common.Class.Model.PQCBomDetail_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCBomDetail set ");
            strSql.Append("materialPartNo=@materialPartNo,");
            strSql.Append("partCount=@partCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("partImage=@partImage,");
            strSql.Append("color=@color,");
            strSql.Append("imagePath=@imagePath,");
            strSql.Append("imageAbsolutePath=@imageAbsolutePath");
            
            strSql.Append(" where 1=1 ");

            strSql.Append(" and  partNumber=@partNumber ");
            strSql.Append(" and  sn=@sn");




            SqlParameter[] parameters = {
                    new SqlParameter("@sn", SqlDbType.Int),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partCount", SqlDbType.Decimal),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@partImage", SqlDbType.Image),
                    new SqlParameter("@color", SqlDbType.VarChar,50),
                    new SqlParameter("@imagePath", SqlDbType.VarChar,100),
                    new SqlParameter("@imageAbsolutePath", SqlDbType.VarChar,100)};

            parameters[0].Value = model.sn == null ? (object)DBNull.Value : model.sn;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[3].Value = model.partCount == null ? (object)DBNull.Value : model.partCount;
            parameters[4].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.partImage == null ? (object)DBNull.Value : model.partImage;
            parameters[7].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[8].Value = model.imagePath == null ? (object)DBNull.Value : model.imagePath;
            parameters[9].Value = model.imageAbsolutePath == null ? (object)DBNull.Value : model.imageAbsolutePath;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCBomDetail_DAL", "Function:		public SqlCommand UpdateCommand(Common.Class.Model.PQCBomDetail_Model model)" +
                "TableName:PQCBomDetail",
                ";sn = " + (model.sn == null ? "" : model.sn.ToString()) +
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";partCount = " + (model.partCount == null ? "" : model.partCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partImage = " + (model.partImage == null ? "" : model.partImage.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()));

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCBomDetail ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCBomDetail_DAL", "Function:		public bool Delete()" + "TableName:PQCBomDetail", "");
            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        public SqlCommand DeleteCommand(string sPartNumber)
        {
            if (sPartNumber == "")
                return null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCBomDetail ");
            strSql.Append(" where 1=1 ");
            strSql.Append(" and partNumber=@partNumber ");

            SqlParameter[] parameters = {
                   new SqlParameter("@partNumber", SqlDbType.VarChar,50)
            };

            parameters[0].Value = sPartNumber;



            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCBomDetail_DAL", "Function:		public SqlCommand DeleteCommand()" + "TableName:PQCBomDetail", "");
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public SqlCommand DeleteAllCommand()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCBomDetail ");
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCBomDetail_DAL", "Function:		public SqlCommand DeleteAllCommand( )" + "TableName:PQCBomDetail", "");
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Class.Model.PQCBomDetail_Model GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 sn,partNumber,materialPartNo,partCount,userName,dateTime,partImage,color from PQCBomDetail ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", 
                "NameSpace:Common.DAL", 
                "Class:PQCBomDetail_DAL", 
                "Function:		public Common.Class.Model.PQCBomDetail_Model GetModel()" + "TableName:PQCBomDetail", "");

            Common.Class.Model.PQCBomDetail_Model model = new Common.Class.Model.PQCBomDetail_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.sn = int.Parse(ds.Tables[0].Rows[0]["sn"].ToString() ?? "0");
                model.partNumber = ds.Tables[0].Rows[0]["partNumber"].ToString()??"";
                model.materialPartNo = ds.Tables[0].Rows[0]["materialPartNo"].ToString() ?? "";
                model.partCount = decimal.Parse(ds.Tables[0].Rows[0]["partCount"].ToString() ?? "0");
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString() ?? "";
                model.dateTime = DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString() ?? System.DateTime.Now.ToString());
                model.partImage = ds.Tables[0].Rows[0]["partImage"] as byte[];
                model.color = ds.Tables[0].Rows[0]["color"].ToString() ?? "";
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
        public DataSet GetList(string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            sn
                            ,partNumber
                            ,materialPartNo
                            ,partCount
                            ,userName
                            ,dateTime
                            ,color
                            ,imagePath
                            ,imageAbsolutePath

                            ,materialName
                            ,customer
                            ,outerBoxQty
                            ,packingTrays
                            ,module

                            FROM PQCBomDetail where 1=1  ");
         
            if (sPartNo != "")
            {
                strSql.Append(" and partNumber = @partNumber");
            }
            strSql.Append(" order by sn asc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar,50)
            };

            if (sPartNo != "")
            {
                paras[0].Value = sPartNo;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(),paras,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" sn,partNumber,materialPartNo,partCount,userName,dateTime,partImage,color ");
            strSql.Append(" FROM PQCBomDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			parameters[0].Value = "PQCBom";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method
    }
}
