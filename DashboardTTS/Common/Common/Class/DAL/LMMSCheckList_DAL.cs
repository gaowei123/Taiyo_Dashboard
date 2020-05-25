using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:LMMSCheckList
	/// </summary>
	public partial class LMMSCheckList_DAL
	{
		public LMMSCheckList_DAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSCheckList_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSCheckList(");
			strSql.Append("machineID,date,DetectOKSample,DetectNGSample,greenLight,yellowLight,redLight,productBeforeBlowing,productAfterBlowing,filterBagReplace,doneBy,VerifyBy,updatedTime)");
			strSql.Append(" values (");
			strSql.Append("@machineID,@date,@DetectOKSample,@DetectNGSample,@greenLight,@yellowLight,@redLight,@productBeforeBlowing,@productAfterBlowing,@filterBagReplace,@doneBy,@VerifyBy,@updatedTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@date", SqlDbType.DateTime,8),
					new SqlParameter("@DetectOKSample", SqlDbType.VarChar,8),
					new SqlParameter("@DetectNGSample", SqlDbType.VarChar,8),
					new SqlParameter("@greenLight", SqlDbType.VarChar,8),
					new SqlParameter("@yellowLight", SqlDbType.VarChar,8),
					new SqlParameter("@redLight", SqlDbType.VarChar,8),
					new SqlParameter("@productBeforeBlowing", SqlDbType.Decimal,9),
					new SqlParameter("@productAfterBlowing", SqlDbType.Decimal,9),
					new SqlParameter("@filterBagReplace", SqlDbType.VarChar,50),
					new SqlParameter("@doneBy", SqlDbType.VarChar,50),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8)};
			parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.date == null ? (object)DBNull.Value : model.date;
            parameters[2].Value = model.DetectOKSample == null ? (object)DBNull.Value : model.DetectOKSample;
            parameters[3].Value = model.DetectNGSample == null ? (object)DBNull.Value : model.DetectNGSample;
            parameters[4].Value = model.greenLight == null ? (object)DBNull.Value : model.greenLight;
            parameters[5].Value = model.yellowLight == null ? (object)DBNull.Value : model.yellowLight;
            parameters[6].Value = model.redLight == null ? (object)DBNull.Value : model.redLight;
            parameters[7].Value = model.productBeforeBlowing == null ? (object)DBNull.Value : model.productBeforeBlowing;
            parameters[8].Value = model.productAfterBlowing == null ? (object)DBNull.Value : model.productAfterBlowing;
            parameters[9].Value = model.filterBagReplace == null ? (object)DBNull.Value : model.filterBagReplace;
            parameters[10].Value = model.doneBy == null ? (object)DBNull.Value : model.doneBy;
            parameters[11].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;

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


        public SqlCommand AddCMD(Common.Class.Model.LMMSCheckList_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSCheckList(");
            strSql.Append("machineID,date,DetectOKSample,DetectNGSample,greenLight,yellowLight,redLight,productBeforeBlowing,productAfterBlowing,filterBagReplace,doneBy,VerifyBy,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@machineID,@date,@DetectOKSample,@DetectNGSample,@greenLight,@yellowLight,@redLight,@productBeforeBlowing,@productAfterBlowing,@filterBagReplace,@doneBy,@VerifyBy,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@machineID", SqlDbType.VarChar,50),
                    new SqlParameter("@date", SqlDbType.DateTime,8),
                    new SqlParameter("@DetectOKSample", SqlDbType.VarChar,8),
                    new SqlParameter("@DetectNGSample", SqlDbType.VarChar,8),
                    new SqlParameter("@greenLight", SqlDbType.VarChar,8),
                    new SqlParameter("@yellowLight", SqlDbType.VarChar,8),
                    new SqlParameter("@redLight", SqlDbType.VarChar,8),
                    new SqlParameter("@productBeforeBlowing", SqlDbType.Decimal,9),
                    new SqlParameter("@productAfterBlowing", SqlDbType.Decimal,9),
                    new SqlParameter("@filterBagReplace", SqlDbType.VarChar,50),
                    new SqlParameter("@doneBy", SqlDbType.VarChar,50),
                    new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime,8)};
            parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.date == null ? (object)DBNull.Value : model.date;
            parameters[2].Value = model.DetectOKSample == null ? (object)DBNull.Value : model.DetectOKSample;
            parameters[3].Value = model.DetectNGSample == null ? (object)DBNull.Value : model.DetectNGSample;
            parameters[4].Value = model.greenLight == null ? (object)DBNull.Value : model.greenLight;
            parameters[5].Value = model.yellowLight == null ? (object)DBNull.Value : model.yellowLight;
            parameters[6].Value = model.redLight == null ? (object)DBNull.Value : model.redLight;
            parameters[7].Value = model.productBeforeBlowing == null ? (object)DBNull.Value : model.productBeforeBlowing;
            parameters[8].Value = model.productAfterBlowing == null ? (object)DBNull.Value : model.productAfterBlowing;
            parameters[9].Value = model.filterBagReplace == null ? (object)DBNull.Value : model.filterBagReplace;
            parameters[10].Value = model.doneBy == null ? (object)DBNull.Value : model.doneBy;
            parameters[11].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);

        
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.LMMSCheckList_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSCheckList set ");
			strSql.Append("machineID=@machineID,");
			strSql.Append("date=@date,");
			strSql.Append("DetectOKSample=@DetectOKSample,");
			strSql.Append("DetectNGSample=@DetectNGSample,");
			strSql.Append("greenLight=@greenLight,");
			strSql.Append("yellowLight=@yellowLight,");
			strSql.Append("redLight=@redLight,");
			strSql.Append("productBeforeBlowing=@productBeforeBlowing,");
			strSql.Append("productAfterBlowing=@productAfterBlowing,");
			strSql.Append("filterBagReplace=@filterBagReplace,");
			strSql.Append("doneBy=@doneBy,");
			strSql.Append("VerifyBy=@VerifyBy,");
			strSql.Append("updatedTime=@updatedTime");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@date", SqlDbType.DateTime,8),
					new SqlParameter("@DetectOKSample", SqlDbType.VarChar,8),
					new SqlParameter("@DetectNGSample", SqlDbType.VarChar,8),
					new SqlParameter("@greenLight", SqlDbType.VarChar,8),
					new SqlParameter("@yellowLight", SqlDbType.VarChar,8),
					new SqlParameter("@redLight", SqlDbType.VarChar,8),
					new SqlParameter("@productBeforeBlowing", SqlDbType.Decimal,9),
					new SqlParameter("@productAfterBlowing", SqlDbType.Decimal,9),
					new SqlParameter("@filterBagReplace", SqlDbType.VarChar,50),
					new SqlParameter("@doneBy", SqlDbType.VarChar,50),
					new SqlParameter("@VerifyBy", SqlDbType.VarChar,50),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8)};
            parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.date == null ? (object)DBNull.Value : model.date;
            parameters[2].Value = model.DetectOKSample == null ? (object)DBNull.Value : model.DetectOKSample;
            parameters[3].Value = model.DetectNGSample == null ? (object)DBNull.Value : model.DetectNGSample;
            parameters[4].Value = model.greenLight == null ? (object)DBNull.Value : model.greenLight;
            parameters[5].Value = model.yellowLight == null ? (object)DBNull.Value : model.yellowLight;
            parameters[6].Value = model.redLight == null ? (object)DBNull.Value : model.redLight;
            parameters[7].Value = model.productBeforeBlowing == null ? (object)DBNull.Value : model.productBeforeBlowing;
            parameters[8].Value = model.productAfterBlowing == null ? (object)DBNull.Value : model.productAfterBlowing;
            parameters[9].Value = model.filterBagReplace == null ? (object)DBNull.Value : model.filterBagReplace;
            parameters[10].Value = model.doneBy == null ? (object)DBNull.Value : model.doneBy;
            parameters[11].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSCheckList ");
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


		public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select 'Machine' + machineID as machineID, convert(varchar(103), date,103) as date,DetectOKSample,DetectNGSample
                                ,greenLight,yellowLight,redLight
                                , convert(float ,productBeforeBlowing) as productBeforeBlowing
                                , convert(float ,productAfterBlowing) as productAfterBlowing
                                ,filterBagReplace,doneBy,VerifyBy,updatedTime ");
			strSql.Append(" FROM LMMSCheckList where 1=1 ");


            strSql.Append(" and updatedTime >= @DateFrom ");
            strSql.Append(" and updatedTime < @DateTo ");

            if (sMachineID.Trim()!="")
			{
                strSql.Append(" and machineID = @machineID ");
            }

            strSql.Append(" order by machineID asc , updatedtime asc");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (sMachineID.Trim() != "")
            {
                paras[2].Value = sMachineID;
            }else
            {
                paras[2] = null;
            }



            return DBHelp.SqlDB.Query(strSql.ToString(),paras);
		}

        public DataTable IsChecked(string sMachineID, DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM LMMSCheckList  where 1=1 ");
            strSql.Append(" and date = @date ");
            strSql.Append(" and machineID = @machineID ");


            SqlParameter[] paras =
            {
                new SqlParameter("@date",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDay;
            paras[1].Value = sMachineID;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);


            if (ds == null || ds.Tables.Count == 0 )
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }

        }




        #endregion  BasicMethod

    }
}

