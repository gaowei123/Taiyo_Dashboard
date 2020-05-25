
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:LMMSVisionMachineSettingHis
	/// </summary>
	public partial class LMMSVisionMachineSettingHis_DAL
	{
		public LMMSVisionMachineSettingHis_DAL()
		{}


		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSVisionMachineSettingHis_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSVisionMachineSettingHis(");
			strSql.Append("day,shift,jobNumber,partNumber,machineID, lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime)");
			strSql.Append(" values (");
			strSql.Append("@day,@shift,@jobNumber,@partNumber,@machineID, @lighting,@camera,@power,@rate,@frequency,@fill,@repeat,@dateTime,@updatedTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,32),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,32),
					new SqlParameter("@partNumber", SqlDbType.VarChar,64),
                    new SqlParameter("@lighting", SqlDbType.VarChar,64),
					new SqlParameter("@camera", SqlDbType.VarChar,64),
					new SqlParameter("@power", SqlDbType.VarChar,64),
					new SqlParameter("@rate", SqlDbType.VarChar,64),
					new SqlParameter("@frequency", SqlDbType.VarChar,64),
					new SqlParameter("@fill", SqlDbType.VarChar,64),
					new SqlParameter("@repeat", SqlDbType.VarChar,64),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@machineID", SqlDbType.VarChar,64)
            };
            parameters[0].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[1].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[2].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.lighting == null ? (object)DBNull.Value : model.lighting;
            parameters[5].Value = model.camera == null ? (object)DBNull.Value : model.camera;
            parameters[6].Value = model.power == null ? (object)DBNull.Value : model.power;
            parameters[7].Value = model.rate == null ? (object)DBNull.Value : model.rate;
            parameters[8].Value = model.frequency == null ? (object)DBNull.Value : model.frequency;
            parameters[9].Value = model.fill == null ? (object)DBNull.Value : model.fill;
            parameters[10].Value = model.repeat == null ? (object)DBNull.Value : model.repeat;
            parameters[11].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[13].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;

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

        public SqlCommand AddCommand(Common.Class.Model.LMMSVisionMachineSettingHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSVisionMachineSettingHis(");
            strSql.Append("day,shift,jobNumber,partNumber,machineID, lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@day,@shift,@jobNumber,@partNumber,@machineID, @lighting,@camera,@power,@rate,@frequency,@fill,@repeat,@dateTime,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,32),
                    new SqlParameter("@jobNumber", SqlDbType.VarChar,32),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,64),
                    new SqlParameter("@lighting", SqlDbType.VarChar,64),
                    new SqlParameter("@camera", SqlDbType.VarChar,64),
                    new SqlParameter("@power", SqlDbType.VarChar,64),
                    new SqlParameter("@rate", SqlDbType.VarChar,64),
                    new SqlParameter("@frequency", SqlDbType.VarChar,64),
                    new SqlParameter("@fill", SqlDbType.VarChar,64),
                    new SqlParameter("@repeat", SqlDbType.VarChar,64),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@machineID", SqlDbType.VarChar,64)
            };
            parameters[0].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[1].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[2].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.lighting == null ? (object)DBNull.Value : model.lighting;
            parameters[5].Value = model.camera == null ? (object)DBNull.Value : model.camera;
            parameters[6].Value = model.power == null ? (object)DBNull.Value : model.power;
            parameters[7].Value = model.rate == null ? (object)DBNull.Value : model.rate;
            parameters[8].Value = model.frequency == null ? (object)DBNull.Value : model.frequency;
            parameters[9].Value = model.fill == null ? (object)DBNull.Value : model.fill;
            parameters[10].Value = model.repeat == null ? (object)DBNull.Value : model.repeat;
            parameters[11].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[13].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;

           return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);

           
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.LMMSVisionMachineSettingHis_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSVisionMachineSettingHis set ");
			strSql.Append("day=@day,");
			strSql.Append("shift=@shift,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("partNumber=@partNumber,");
			strSql.Append("lighting=@lighting,");
			strSql.Append("camera=@camera,");
			strSql.Append("power=@power,");
			strSql.Append("rate=@rate,");
			strSql.Append("frequency=@frequency,");
			strSql.Append("fill=@fill,");
			strSql.Append("repeat=@repeat,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("updatedTime=@updatedTime");
			strSql.Append(" where 1=1 ");
            strSql.Append(" and jobNumber=@jobNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,32),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,32),
					new SqlParameter("@partNumber", SqlDbType.VarChar,64),
					new SqlParameter("@lighting", SqlDbType.VarChar,64),
					new SqlParameter("@camera", SqlDbType.VarChar,64),
					new SqlParameter("@power", SqlDbType.VarChar,64),
					new SqlParameter("@rate", SqlDbType.VarChar,64),
					new SqlParameter("@frequency", SqlDbType.VarChar,64),
					new SqlParameter("@fill", SqlDbType.VarChar,64),
					new SqlParameter("@repeat", SqlDbType.VarChar,64),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@machineID", SqlDbType.VarChar,64)
            };
            parameters[0].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[1].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[2].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.lighting == null ? (object)DBNull.Value : model.lighting;
            parameters[5].Value = model.camera == null ? (object)DBNull.Value : model.camera;
            parameters[6].Value = model.power == null ? (object)DBNull.Value : model.power;
            parameters[7].Value = model.rate == null ? (object)DBNull.Value : model.rate;
            parameters[8].Value = model.frequency == null ? (object)DBNull.Value : model.frequency;
            parameters[9].Value = model.fill == null ? (object)DBNull.Value : model.fill;
            parameters[10].Value = model.repeat == null ? (object)DBNull.Value : model.repeat;
            parameters[11].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[12].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[13].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;

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
			strSql.Append("delete from LMMSVisionMachineSettingHis ");
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


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.LMMSVisionMachineSettingHis_Model GetModel(string sJobNo)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 day,shift,jobNumber,partNumber,machineID,lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime from LMMSVisionMachineSettingHis where 1=1  ");


            if (sJobNo.Trim() != "")
            {
                strSql.Append(" and jobNumber = @jobNumber");
            }

			SqlParameter[] parameters = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar, 32)
			};

            if (sJobNo.Trim() != "")
            {
                parameters[0].Value = sJobNo;
            }else
            {
                parameters[0] = null;
            }

		
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
		public Common.Class.Model.LMMSVisionMachineSettingHis_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.LMMSVisionMachineSettingHis_Model model =new Common.Class.Model.LMMSVisionMachineSettingHis_Model();
			if (row != null)
			{
					//model.day=row["day"].ToString();
				if(row["shift"]!=null)
				{
					model.shift=row["shift"].ToString();
				}
				if(row["jobNumber"]!=null)
				{
					model.jobNumber=row["jobNumber"].ToString();
				}
				if(row["partNumber"]!=null)
				{
					model.partNumber=row["partNumber"].ToString();
				}
                if (row["machineID"] != null)
                {
                    model.machineID = row["machineID"].ToString();
                }
                if (row["lighting"]!=null)
				{
					model.lighting=row["lighting"].ToString();
				}
				if(row["camera"]!=null)
				{
					model.camera=row["camera"].ToString();
				}
				if(row["power"]!=null)
				{
					model.power=row["power"].ToString();
				}
				if(row["rate"]!=null)
				{
					model.rate=row["rate"].ToString();
				}
				if(row["frequency"]!=null)
				{
					model.frequency=row["frequency"].ToString();
				}
				if(row["fill"]!=null)
				{
					model.fill=row["fill"].ToString();
				}
				if(row["repeat"]!=null)
				{
					model.repeat=row["repeat"].ToString();
				}

                if (row["dateTime"] != null)
                {
                    model.dateTime = DateTime.Parse(row["dateTime"].ToString());
                }

                if (row["updatedTime"] != null)
                {
                    model.updatedTime = DateTime.Parse(row["updatedTime"].ToString());
                }
                
            }
            return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		

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
			strSql.Append(" day,shift,jobNumber,partNumber,lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime ");
			strSql.Append(" FROM LMMSVisionMachineSettingHis ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

	
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from LMMSVisionMachineSettingHis T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}


        #endregion  BasicMethod


        public DataSet GetList(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select day,shift,jobNumber,partNumber,machineID, lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime FROM LMMSVisionMachineSettingHis where 1=1 ");


         

            if (sJobNo != "") strSql.Append(" and jobNumber = @jobNumber");

            
            SqlParameter[] parameters = {
                    new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            if (sJobNo != "") parameters[0].Value = sJobNo; else parameters[0] = null;



            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }


        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string[] arrMachineID, string[] arrPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select day,shift,jobNumber,partNumber,machineID, lighting,camera,power,rate,frequency,fill,repeat,dateTime,updatedTime FROM LMMSVisionMachineSettingHis where 1=1 and dateTime >= @dateFrom and dateTime < @dateTo ");


            if (arrMachineID.Length != 0 && arrMachineID[0].Trim() != "")
            {
                strSql.Append(" and machineID in (");

                foreach (string str in arrMachineID)
                {
                    strSql.Append("'" + str + "',");
                }
                strSql.Remove(strSql.Length - 1, 1);

                strSql.Append(" ) ");
            }


            if (arrPartNo.Length != 0 && arrPartNo[0].Trim() != "")
            {
                strSql.Append(" and partNumber in (");

                foreach (string str in arrPartNo)
                {
                    strSql.Append("'" + str + "',");
                }
                strSql.Remove(strSql.Length - 1, 1);

                strSql.Append(" ) ");
            }




            SqlParameter[] parameters = {
                    new SqlParameter("@dateFrom", SqlDbType.DateTime2,8),
                    new SqlParameter("@dateTo", SqlDbType.DateTime2,8)

            };
            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }



    }
}

