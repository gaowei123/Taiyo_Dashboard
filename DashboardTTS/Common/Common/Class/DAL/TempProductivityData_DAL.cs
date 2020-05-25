using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:TempProductivityData
	/// </summary>
	public partial class TempProductivityData_DAL
	{
		public TempProductivityData_DAL()
		{}


        #region  BasicMethod


        public SqlCommand AddCommand(Common.Class.Model.TempProductivityData_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TempProductivityData(");
            strSql.Append("day,shift,department,Type,targetHR,actualHR,targetQty,totalQty,actualQty,rejectQty,bezelRejQty,coverRejQty,escRejQty,btnRejQty,indicatorRejQty,typeRej,utilization,createdTime,updatedTime,updatedBy,mouldRejCount,paintRejCount,laserRejCount,vendorRejCount,printRejCount)");
            strSql.Append(" values (");
            strSql.Append("@day,@shift,@department,@Type,@targetHR,@actualHR,@targetQty,@totalQty,@actualQty,@rejectQty,@bezelRejQty,@coverRejQty,@escRejQty,@btnRejQty,@indicatorRejQty,@typeRej,@utilization,@createdTime,@updatedTime,@updatedBy,@mouldRejCount,@paintRejCount,@laserRejCount,@vendorRejCount,@printRejCount)");
            SqlParameter[] parameters = {
                    new SqlParameter("@day", SqlDbType.DateTime),
                    new SqlParameter("@department", SqlDbType.VarChar,50),
                    new SqlParameter("@Type", SqlDbType.VarChar,50),
                    new SqlParameter("@targetHR", SqlDbType.VarChar,50),
                    new SqlParameter("@actualHR", SqlDbType.VarChar,50),
                    new SqlParameter("@targetQty", SqlDbType.VarChar,50),
                    new SqlParameter("@totalQty", SqlDbType.VarChar,50),
                    new SqlParameter("@actualQty", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQty", SqlDbType.VarChar,50),
                    new SqlParameter("@bezelRejQty", SqlDbType.VarChar,50),
                    new SqlParameter("@coverRejQty", SqlDbType.VarChar,50),
                    new SqlParameter("@escRejQty", SqlDbType.VarChar,50),
                    new SqlParameter("@btnRejQty", SqlDbType.VarChar,50),
                    new SqlParameter("@indicatorRejQty", SqlDbType.VarChar,50),
                    new SqlParameter("@typeRej", SqlDbType.VarChar,50),
                    new SqlParameter("@utilization", SqlDbType.VarChar,50),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime),
                    new SqlParameter("@updatedBy", SqlDbType.VarChar,50),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    
                    new SqlParameter("@mouldRejCount", SqlDbType.VarChar,50),
                    new SqlParameter("@paintRejCount", SqlDbType.VarChar,50),
                    new SqlParameter("@laserRejCount", SqlDbType.VarChar,50),
                    new SqlParameter("@vendorRejCount", SqlDbType.VarChar,50),
                    new SqlParameter("@printRejCount", SqlDbType.VarChar,50)

            };
            parameters[0].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[1].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[2].Value = model.Type == null ? (object)DBNull.Value : model.Type;
            parameters[3].Value = model.targetHR == null ? (object)DBNull.Value : model.targetHR;
            parameters[4].Value = model.actualHR == null ? (object)DBNull.Value : model.actualHR;
            parameters[5].Value = model.targetQty == null ? (object)DBNull.Value : model.targetQty;
            parameters[6].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[7].Value = model.actualQty == null ? (object)DBNull.Value : model.actualQty;
            parameters[8].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[9].Value = model.bezelRejQty == null ? (object)DBNull.Value : model.bezelRejQty;
            parameters[10].Value = model.coverRejQty == null ? (object)DBNull.Value : model.coverRejQty;
            parameters[11].Value = model.escRejQty == null ? (object)DBNull.Value : model.escRejQty;
            parameters[12].Value = model.btnRejQty == null ? (object)DBNull.Value : model.btnRejQty;
            parameters[13].Value = model.indicatorRejQty == null ? (object)DBNull.Value : model.indicatorRejQty;
            parameters[14].Value = model.typeRej == null ? (object)DBNull.Value : model.typeRej;
            parameters[15].Value = model.utilization == null ? (object)DBNull.Value : model.utilization;
            parameters[16].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[17].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[18].Value = model.updatedBy == null ? (object)DBNull.Value : model.updatedBy;
            parameters[19].Value = model.shift == null ? (object)DBNull.Value : model.shift;



            parameters[20].Value = model.mouldRejCount == null ? (object)DBNull.Value : model.mouldRejCount;
            parameters[21].Value = model.paintRejCount == null ? (object)DBNull.Value : model.paintRejCount;
            parameters[22].Value = model.laserRejCount == null ? (object)DBNull.Value : model.laserRejCount;
            parameters[23].Value = model.vendorRejCount == null ? (object)DBNull.Value : model.vendorRejCount;
            parameters[24].Value = model.printRejCount == null ? (object)DBNull.Value : model.printRejCount;




            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
          
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Common.Class.Model.TempProductivityData_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TempProductivityData(");
			strSql.Append("day,department,Type,targetHR,actualHR,targetQty,totalQty,actualQty,rejectQty,bezelRejQty,coverRejQty,escRejQty,btnRejQty,indicatorRejQty,typeRej,utilization,createdTime,updatedTime,updatedBy)");
			strSql.Append(" values (");
			strSql.Append("@day,@department,@Type,@targetHR,@actualHR,@targetQty,@totalQty,@actualQty,@rejectQty,@bezelRejQty,@coverRejQty,@escRejQty,@btnRejQty,@indicatorRejQty,@typeRej,@utilization,@createdTime,@updatedTime,@updatedBy)");
			SqlParameter[] parameters = {
					new SqlParameter("@day", SqlDbType.DateTime),
					new SqlParameter("@department", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@targetHR", SqlDbType.VarChar,50),
					new SqlParameter("@actualHR", SqlDbType.VarChar,50),
					new SqlParameter("@targetQty", SqlDbType.VarChar,50),
					new SqlParameter("@totalQty", SqlDbType.VarChar,50),
					new SqlParameter("@actualQty", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQty", SqlDbType.VarChar,50),
					new SqlParameter("@bezelRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@coverRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@escRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@btnRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@indicatorRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@typeRej", SqlDbType.VarChar,50),
					new SqlParameter("@utilization", SqlDbType.VarChar,50),
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@updatedTime", SqlDbType.DateTime),
					new SqlParameter("@updatedBy", SqlDbType.VarChar,50)};
			parameters[0].Value = model.day;
			parameters[1].Value = model.department;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.targetHR;
			parameters[4].Value = model.actualHR;
			parameters[5].Value = model.targetQty;
			parameters[6].Value = model.totalQty;
			parameters[7].Value = model.actualQty;
			parameters[8].Value = model.rejectQty;
			parameters[9].Value = model.bezelRejQty;
			parameters[10].Value = model.coverRejQty;
			parameters[11].Value = model.escRejQty;
			parameters[12].Value = model.btnRejQty;
			parameters[13].Value = model.indicatorRejQty;
			parameters[14].Value = model.typeRej;
			parameters[15].Value = model.utilization;
			parameters[16].Value = model.createdTime;
			parameters[17].Value = model.updatedTime;
			parameters[18].Value = model.updatedBy;

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
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.TempProductivityData_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TempProductivityData set ");
			strSql.Append("day=@day,");
			strSql.Append("department=@department,");
			strSql.Append("Type=@Type,");
			strSql.Append("targetHR=@targetHR,");
			strSql.Append("actualHR=@actualHR,");
			strSql.Append("targetQty=@targetQty,");
			strSql.Append("totalQty=@totalQty,");
			strSql.Append("actualQty=@actualQty,");
			strSql.Append("rejectQty=@rejectQty,");
			strSql.Append("bezelRejQty=@bezelRejQty,");
			strSql.Append("coverRejQty=@coverRejQty,");
			strSql.Append("escRejQty=@escRejQty,");
			strSql.Append("btnRejQty=@btnRejQty,");
			strSql.Append("indicatorRejQty=@indicatorRejQty,");
			strSql.Append("typeRej=@typeRej,");
			strSql.Append("utilization=@utilization,");
			strSql.Append("createdTime=@createdTime,");
			strSql.Append("updatedTime=@updatedTime,");
			strSql.Append("updatedBy=@updatedBy");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@day", SqlDbType.DateTime),
					new SqlParameter("@department", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@targetHR", SqlDbType.VarChar,50),
					new SqlParameter("@actualHR", SqlDbType.VarChar,50),
					new SqlParameter("@targetQty", SqlDbType.VarChar,50),
					new SqlParameter("@totalQty", SqlDbType.VarChar,50),
					new SqlParameter("@actualQty", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQty", SqlDbType.VarChar,50),
					new SqlParameter("@bezelRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@coverRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@escRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@btnRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@indicatorRejQty", SqlDbType.VarChar,50),
					new SqlParameter("@typeRej", SqlDbType.VarChar,50),
					new SqlParameter("@utilization", SqlDbType.VarChar,50),
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@updatedTime", SqlDbType.DateTime),
					new SqlParameter("@updatedBy", SqlDbType.VarChar,50)};
			parameters[0].Value = model.day;
			parameters[1].Value = model.department;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.targetHR;
			parameters[4].Value = model.actualHR;
			parameters[5].Value = model.targetQty;
			parameters[6].Value = model.totalQty;
			parameters[7].Value = model.actualQty;
			parameters[8].Value = model.rejectQty;
			parameters[9].Value = model.bezelRejQty;
			parameters[10].Value = model.coverRejQty;
			parameters[11].Value = model.escRejQty;
			parameters[12].Value = model.btnRejQty;
			parameters[13].Value = model.indicatorRejQty;
			parameters[14].Value = model.typeRej;
			parameters[15].Value = model.utilization;
			parameters[16].Value = model.createdTime;
			parameters[17].Value = model.updatedTime;
			parameters[18].Value = model.updatedBy;

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




        public SqlCommand DeleteDayCMD(string sDepartment, DateTime? dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete from TempProductivityData ");
           
         
            strSql.Append(" where 1=1 ");

            strSql.Append(" and day=@day ");
            strSql.Append(" and department=@department");

            SqlParameter[] parameters = {
                    new SqlParameter("@day", SqlDbType.DateTime),
                    new SqlParameter("@department", SqlDbType.VarChar,50)
            };
            parameters[0].Value = dDay.Value;
            parameters[1].Value = sDepartment;




         return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
          
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// 
        public Common.Class.Model.TempProductivityData_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 day,department,Type,targetHR,actualHR,targetQty,totalQty,actualQty,rejectQty,bezelRejQty,coverRejQty,escRejQty,btnRejQty,indicatorRejQty,typeRej,utilization,createdTime,updatedTime,updatedBy from TempProductivityData ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

            Common.Class.Model.TempProductivityData_Model model = new Common.Class.Model.TempProductivityData_Model();
			DataSet ds= DBHelp.SqlDB.Query(strSql.ToString(),parameters);
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
		public Common.Class.Model.TempProductivityData_Model DataRowToModel(DataRow row)
		{
            Common.Class.Model.TempProductivityData_Model model =new Common.Class.Model.TempProductivityData_Model();
			if (row != null)
			{
				if(row["day"]!=null && row["day"].ToString()!="")
				{
					model.day=DateTime.Parse(row["day"].ToString());
				}
				if(row["department"]!=null)
				{
					model.department=row["department"].ToString();
				}
				if(row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["targetHR"]!=null)
				{
					model.targetHR=row["targetHR"].ToString();
				}
				if(row["actualHR"]!=null)
				{
					model.actualHR=row["actualHR"].ToString();
				}
				if(row["targetQty"]!=null)
				{
					model.targetQty=row["targetQty"].ToString();
				}
				if(row["totalQty"]!=null)
				{
					model.totalQty=row["totalQty"].ToString();
				}
				if(row["actualQty"]!=null)
				{
					model.actualQty=row["actualQty"].ToString();
				}
				if(row["rejectQty"]!=null)
				{
					model.rejectQty=row["rejectQty"].ToString();
				}
				if(row["bezelRejQty"]!=null)
				{
					model.bezelRejQty=row["bezelRejQty"].ToString();
				}
				if(row["coverRejQty"]!=null)
				{
					model.coverRejQty=row["coverRejQty"].ToString();
				}
				if(row["escRejQty"]!=null)
				{
					model.escRejQty=row["escRejQty"].ToString();
				}
				if(row["btnRejQty"]!=null)
				{
					model.btnRejQty=row["btnRejQty"].ToString();
				}
				if(row["indicatorRejQty"]!=null)
				{
					model.indicatorRejQty=row["indicatorRejQty"].ToString();
				}
				if(row["typeRej"]!=null)
				{
					model.typeRej=row["typeRej"].ToString();
				}
				if(row["utilization"]!=null)
				{
					model.utilization=row["utilization"].ToString();
				}
				if(row["createdTime"]!=null && row["createdTime"].ToString()!="")
				{
					model.createdTime=DateTime.Parse(row["createdTime"].ToString());
				}
				if(row["updatedTime"]!=null && row["updatedTime"].ToString()!="")
				{
					model.updatedTime=DateTime.Parse(row["updatedTime"].ToString());
				}
				if(row["updatedBy"]!=null)
				{
					model.updatedBy=row["updatedBy"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(string sDepartment, DateTime dDay)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from TempProductivityData where 1=1 ");

            strSql.Append(" and day = @day ");

            if (sDepartment != "")
            {
                strSql.Append(" and department = @department ");
            }


            SqlParameter[] paras =
            {
                new SqlParameter("@day",SqlDbType.DateTime),
                new SqlParameter("@department",SqlDbType.VarChar,50)
            };


            paras[0].Value = dDay;
            if (sDepartment != "")
            {
                paras[1].Value = sDepartment;
            }else
            {
                paras[1] = null;
            }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
          
		}





        public DataTable GetgetProductivtiyReportForTemp(string sDepartment, DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            case 
                            --painting--
                            when Type = 'BUTTON(U&M/C)' then 1
                            when type = 'LASER BUTTON(T/C)'  then 2
                            when type = 'WIP BUTTON(T/C)'  then 3
                            when type = '257B-BEZEL(U/C)'  then 4
                            when type = '257B-BEZEL(T/C)'  then 5
                            when type = '320B-BEZEL(T/C)'  then 6
                            when type = '601B-PANEL(T/C)'  then 7
                            when type = 'Project Testing'  then 8
                            --painting--

                            --assy--
                            when Type = 'PASDL 305B' then 1
                            when type = 'PASDL 336B/291B'  then 2
                            when type = 'SWS/785A'  then 3
                            when type = 'SWS 440A/443A'  then 4
                            when type = 'Chrysler JL Dual'  then 5
                            when type = 'Chrysler JL Singel'  then 6
                            when type = '601B-PANEL(T/C)'  then 7
                            when type = 'Other'  then 8
                            --assy--

                            --PQC--
                            when Type = 'LASER' then 1
                            when Type = 'WIP' then 2

                            --when Type = 'Laser Btn' then 1
                            --when type = 'w/o Laser Btn'  then 2
                            --when type = 'SBW TKS784'  then 3
                            --when type = 'TMS TKS824'  then 4
                            --when type = 'TAC TKS833'  then 5
                            --when type = 'TRMI 452'  then 6
                            --when type = 'TRMI 595,656'  then 7
                            --when type = 'PASA 257 Bezel' then 8
                            --when type = '320B TKS830'  then 9
                            --when type = '320B TKS831'  then 10
                            --PQC--

                            else 20   end as SN


                            ,Type as productType
                            ,'' as ManPower
                            ,'' as Attendance
                            ,'' as AttendRate
                            ,targetHR
                            ,ActualHR
                            ,TargetQty
                            ,actualQty
                            ,TotalQty
                            ,RejectQty

                            -- assy --
                            ,bezelRejQty
                            ,coverRejQty
                            ,escRejQty
                            ,btnRejQty
                            ,indicatorRejQty
                            -- asy --

                            -- PQC --
                           ,case when mouldRejCount = '' then '0' else mouldRejCount end as mouldRejCount
                           ,case when paintRejCount = '' then '0' else paintRejCount end as paintRejCount 
                           ,case when laserRejCount = '' then '0' else laserRejCount end as laserRejCount  
                           ,case when vendorRejCount = '' then '0' else vendorRejCount end as vendorRejCount  
                           ,case when printRejCount = '' then '0' else printRejCount end as printRejCount  
                            -- PQC --                            


                            ,case when TotalQty = 0 then '0.00%' else
	                            case 
                                    when department = 'Painting'
		                            then  convert(varchar(50), convert(float, convert(decimal(18,2), convert(decimal(18,2), RejectQty )/convert(decimal(18,2), TotalQty)*100))) + '%'  


		                            when department = 'Assembly'
		                            then convert(varchar(50), convert(float, convert(decimal(18,2),   
		                            (convert(decimal(18,2), bezelRejQty ) + convert(decimal(18,2), coverRejQty ) + convert(decimal(18,2), escRejQty ) + convert(decimal(18,2), btnRejQty ) + convert(decimal(18,2), indicatorRejQty ))  
		                            /
		                            convert(decimal(18,2), TotalQty)*100))) + '%'    


                                    when department = 'PQC'
                                    then 
                                    convert(varchar(50), convert(float, convert(decimal(18,2),   
			                            (convert(decimal(18,2), case when mouldRejCount = '' then '0' else mouldRejCount end ) 
			                            + convert(decimal(18,2), case when paintRejCount = '' then '0' else paintRejCount end ) 
			                            + convert(decimal(18,2), case when laserRejCount = '' then '0' else laserRejCount end ) 
			                            + convert(decimal(18,2), case when vendorRejCount = '' then '0' else vendorRejCount end ) 
			                            + convert(decimal(18,2), case when printRejCount = '' then '0' else printRejCount end )
		                            )  
                                    /
                                    convert(decimal(18,2), TotalQty)*100))) + '%'   
                                end

                            end as Rejrate


                            ,Utilization 
                            from TempProductivityData
                            where 1=1 
                            and department = @department 
                            and day = @day
                            and shift = @shift");
           

            SqlParameter[] paras =
            {
                new SqlParameter("@department",SqlDbType.VarChar,50),
                new SqlParameter("@day",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,50)
            };

          
            paras[0].Value = sDepartment;
            paras[1].Value = dDay;
            paras[2].Value = sShift;
          

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);
            if (ds == null || ds.Tables.Count == 0)
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

