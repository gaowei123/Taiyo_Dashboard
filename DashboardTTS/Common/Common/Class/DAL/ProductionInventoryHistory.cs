using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:ProductionInventoryHistory
	/// </summary>
	public partial class ProductionInventoryHistory
	{
		public ProductionInventoryHistory()
		{}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.ProductionInventoryHistory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ProductionInventoryHistory(");
			strSql.Append("Id,Day,PartNumber,Model,MaterialName,Assembly,FG,AfterPacking,BeforePacking,AfterWIP,BeforeWIP,AfterLaser,BeforeLaser,TCPaint,MCPaint,PrintSupplier,UCPaint,PaintRawPart)");
			strSql.Append(" values (");
			strSql.Append("@Id,@Day,@PartNumber,@Model,@MaterialName,@Assembly,@FG,@AfterPacking,@BeforePacking,@AfterWIP,@BeforeWIP,@AfterLaser,@BeforeLaser,@TCPaint,@MCPaint,@PrintSupplier,@UCPaint,@PaintRawPart)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Day", SqlDbType.DateTime),
					new SqlParameter("@PartNumber", SqlDbType.VarChar,100),
					new SqlParameter("@Model", SqlDbType.VarChar,100),
					new SqlParameter("@MaterialName", SqlDbType.VarChar,100),
					new SqlParameter("@Assembly", SqlDbType.Int,4),
					new SqlParameter("@FG", SqlDbType.Int,4),
					new SqlParameter("@AfterPacking", SqlDbType.Int,4),
					new SqlParameter("@BeforePacking", SqlDbType.Int,4),
					new SqlParameter("@AfterWIP", SqlDbType.Int,4),
					new SqlParameter("@BeforeWIP", SqlDbType.Int,4),
					new SqlParameter("@AfterLaser", SqlDbType.Int,4),
					new SqlParameter("@BeforeLaser", SqlDbType.Int,4),
					new SqlParameter("@TCPaint", SqlDbType.Int,4),
					new SqlParameter("@MCPaint", SqlDbType.Int,4),
					new SqlParameter("@PrintSupplier", SqlDbType.Int,4),
					new SqlParameter("@UCPaint", SqlDbType.Int,4),
					new SqlParameter("@PaintRawPart", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Day;
			parameters[2].Value = model.PartNumber;
			parameters[3].Value = model.Model;
			parameters[4].Value = model.MaterialName;
			parameters[5].Value = model.Assembly;
			parameters[6].Value = model.FG;
			parameters[7].Value = model.AfterPacking;
			parameters[8].Value = model.BeforePacking;
			parameters[9].Value = model.AfterWIP;
			parameters[10].Value = model.BeforeWIP;
			parameters[11].Value = model.AfterLaser;
			parameters[12].Value = model.BeforeLaser;
			parameters[13].Value = model.TCPaint;
			parameters[14].Value = model.MCPaint;
			parameters[15].Value = model.PrintSupplier;
			parameters[16].Value = model.UCPaint;
			parameters[17].Value = model.PaintRawPart;

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


        public SqlCommand   AddCommand(Common.Class.Model.ProductionInventoryHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProductionInventoryHistory(");
            strSql.Append("Day,PartNumber,Model,MaterialName,Assembly,FG,AfterPacking,BeforePacking,AfterWIP,BeforeWIP,AfterLaser,BeforeLaser,TCPaint,MCPaint,PrintSupplier,UCPaint,PaintRawPart)");
            strSql.Append(" values (");
            strSql.Append("@Day,@PartNumber,@Model,@MaterialName,@Assembly,@FG,@AfterPacking,@BeforePacking,@AfterWIP,@BeforeWIP,@AfterLaser,@BeforeLaser,@TCPaint,@MCPaint,@PrintSupplier,@UCPaint,@PaintRawPart)");
            SqlParameter[] parameters = {
                    
                    new SqlParameter("@Day", SqlDbType.DateTime),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar,100),
                    new SqlParameter("@Model", SqlDbType.VarChar,100),
                    new SqlParameter("@MaterialName", SqlDbType.VarChar,100),
                    new SqlParameter("@Assembly", SqlDbType.Int,4),
                    new SqlParameter("@FG", SqlDbType.Int,4),
                    new SqlParameter("@AfterPacking", SqlDbType.Int,4),
                    new SqlParameter("@BeforePacking", SqlDbType.Int,4),
                    new SqlParameter("@AfterWIP", SqlDbType.Int,4),
                    new SqlParameter("@BeforeWIP", SqlDbType.Int,4),
                    new SqlParameter("@AfterLaser", SqlDbType.Int,4),
                    new SqlParameter("@BeforeLaser", SqlDbType.Int,4),
                    new SqlParameter("@TCPaint", SqlDbType.Int,4),
                    new SqlParameter("@MCPaint", SqlDbType.Int,4),
                    new SqlParameter("@PrintSupplier", SqlDbType.Int,4),
                    new SqlParameter("@UCPaint", SqlDbType.Int,4),
                    new SqlParameter("@PaintRawPart", SqlDbType.Int,4)};
      
            parameters[0]. Value = model.Day;
            parameters[1]. Value = model.PartNumber;
            parameters[2]. Value = model.Model;
            parameters[3]. Value = model.MaterialName;
            parameters[4]. Value = model.Assembly;
            parameters[5]. Value = model.FG;
            parameters[6]. Value = model.AfterPacking;
            parameters[7]. Value = model.BeforePacking;
            parameters[8]. Value = model.AfterWIP;
            parameters[9]. Value = model.BeforeWIP;
            parameters[10].Value = model.AfterLaser;
            parameters[11].Value = model.BeforeLaser;
            parameters[12].Value = model.TCPaint;
            parameters[13].Value = model.MCPaint;
            parameters[14].Value = model.PrintSupplier;
            parameters[15].Value = model.UCPaint;
            parameters[16].Value = model.PaintRawPart;
                     
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.ProductionInventoryHistory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ProductionInventoryHistory set ");
			strSql.Append("Id=@Id,");
			strSql.Append("Day=@Day,");
			strSql.Append("PartNumber=@PartNumber,");
			strSql.Append("Model=@Model,");
			strSql.Append("MaterialName=@MaterialName,");
			strSql.Append("Assembly=@Assembly,");
			strSql.Append("FG=@FG,");
			strSql.Append("AfterPacking=@AfterPacking,");
			strSql.Append("BeforePacking=@BeforePacking,");
			strSql.Append("AfterWIP=@AfterWIP,");
			strSql.Append("BeforeWIP=@BeforeWIP,");
			strSql.Append("AfterLaser=@AfterLaser,");
			strSql.Append("BeforeLaser=@BeforeLaser,");
			strSql.Append("TCPaint=@TCPaint,");
			strSql.Append("MCPaint=@MCPaint,");
			strSql.Append("PrintSupplier=@PrintSupplier,");
			strSql.Append("UCPaint=@UCPaint,");
			strSql.Append("PaintRawPart=@PaintRawPart");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Day", SqlDbType.DateTime),
					new SqlParameter("@PartNumber", SqlDbType.VarChar,100),
					new SqlParameter("@Model", SqlDbType.VarChar,100),
					new SqlParameter("@MaterialName", SqlDbType.VarChar,100),
					new SqlParameter("@Assembly", SqlDbType.Int,4),
					new SqlParameter("@FG", SqlDbType.Int,4),
					new SqlParameter("@AfterPacking", SqlDbType.Int,4),
					new SqlParameter("@BeforePacking", SqlDbType.Int,4),
					new SqlParameter("@AfterWIP", SqlDbType.Int,4),
					new SqlParameter("@BeforeWIP", SqlDbType.Int,4),
					new SqlParameter("@AfterLaser", SqlDbType.Int,4),
					new SqlParameter("@BeforeLaser", SqlDbType.Int,4),
					new SqlParameter("@TCPaint", SqlDbType.Int,4),
					new SqlParameter("@MCPaint", SqlDbType.Int,4),
					new SqlParameter("@PrintSupplier", SqlDbType.Int,4),
					new SqlParameter("@UCPaint", SqlDbType.Int,4),
					new SqlParameter("@PaintRawPart", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Day;
			parameters[2].Value = model.PartNumber;
			parameters[3].Value = model.Model;
			parameters[4].Value = model.MaterialName;
			parameters[5].Value = model.Assembly;
			parameters[6].Value = model.FG;
			parameters[7].Value = model.AfterPacking;
			parameters[8].Value = model.BeforePacking;
			parameters[9].Value = model.AfterWIP;
			parameters[10].Value = model.BeforeWIP;
			parameters[11].Value = model.AfterLaser;
			parameters[12].Value = model.BeforeLaser;
			parameters[13].Value = model.TCPaint;
			parameters[14].Value = model.MCPaint;
			parameters[15].Value = model.PrintSupplier;
			parameters[16].Value = model.UCPaint;
			parameters[17].Value = model.PaintRawPart;

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
		/// 删除一天数据
		/// </summary>
		public bool Delete(DateTime? Day)
		{
            if (Day == null )
                throw new ArgumentNullException("Day can not be empty!");


            StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ProductionInventoryHistory where day =@day ");
		
			SqlParameter[] parameters = {
                new SqlParameter("@day",SqlDbType.DateTime)
			};
            parameters[0].Value = Day;

			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
            return rows > 0 ? true : false;		
		}


		/// <summary>
		/// 获取一天的实体列表
		/// </summary>
		public List<Common.Class.Model.ProductionInventoryHistory>  GetDayList(DateTime? Day)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select  Id,Day,PartNumber,Model,MaterialName,Assembly,FG,AfterPacking,BeforePacking,AfterWIP,BeforeWIP,AfterLaser,BeforeLaser,TCPaint,MCPaint,PrintSupplier,UCPaint,PaintRawPart 
                from ProductionInventoryHistory  where 1=1  and day = @day ");




            SqlParameter[] parameters = {
                new SqlParameter("@day",SqlDbType.DateTime)
            };
            parameters[0].Value = Day;


            
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
            if (ds == null ||ds.Tables.Count == 0)
                return null;




            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
                return null;


            List<Common.Class.Model.ProductionInventoryHistory> result = new List<Model.ProductionInventoryHistory>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(DataRowToModel(dr));
            }


            return result;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.ProductionInventoryHistory DataRowToModel(DataRow row)
		{
			Common.Class.Model.ProductionInventoryHistory model=new Common.Class.Model.ProductionInventoryHistory();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Day"]!=null && row["Day"].ToString()!="")
				{
					model.Day=DateTime.Parse(row["Day"].ToString());
				}
				if(row["PartNumber"]!=null)
				{
					model.PartNumber=row["PartNumber"].ToString();
				}
				if(row["Model"]!=null)
				{
					model.Model=row["Model"].ToString();
				}
				if(row["MaterialName"]!=null)
				{
					model.MaterialName=row["MaterialName"].ToString();
				}
				if(row["Assembly"]!=null && row["Assembly"].ToString()!="")
				{
					model.Assembly=int.Parse(row["Assembly"].ToString());
				}
				if(row["FG"]!=null && row["FG"].ToString()!="")
				{
					model.FG=int.Parse(row["FG"].ToString());
				}
				if(row["AfterPacking"]!=null && row["AfterPacking"].ToString()!="")
				{
					model.AfterPacking=int.Parse(row["AfterPacking"].ToString());
				}
				if(row["BeforePacking"]!=null && row["BeforePacking"].ToString()!="")
				{
					model.BeforePacking=int.Parse(row["BeforePacking"].ToString());
				}
				if(row["AfterWIP"]!=null && row["AfterWIP"].ToString()!="")
				{
					model.AfterWIP=int.Parse(row["AfterWIP"].ToString());
				}
				if(row["BeforeWIP"]!=null && row["BeforeWIP"].ToString()!="")
				{
					model.BeforeWIP=int.Parse(row["BeforeWIP"].ToString());
				}
				if(row["AfterLaser"]!=null && row["AfterLaser"].ToString()!="")
				{
					model.AfterLaser=int.Parse(row["AfterLaser"].ToString());
				}
				if(row["BeforeLaser"]!=null && row["BeforeLaser"].ToString()!="")
				{
					model.BeforeLaser=int.Parse(row["BeforeLaser"].ToString());
				}
				if(row["TCPaint"]!=null && row["TCPaint"].ToString()!="")
				{
					model.TCPaint=int.Parse(row["TCPaint"].ToString());
				}
				if(row["MCPaint"]!=null && row["MCPaint"].ToString()!="")
				{
					model.MCPaint=int.Parse(row["MCPaint"].ToString());
				}
				if(row["PrintSupplier"]!=null && row["PrintSupplier"].ToString()!="")
				{
					model.PrintSupplier=int.Parse(row["PrintSupplier"].ToString());
				}
				if(row["UCPaint"]!=null && row["UCPaint"].ToString()!="")
				{
					model.UCPaint=int.Parse(row["UCPaint"].ToString());
				}
				if(row["PaintRawPart"]!=null && row["PaintRawPart"].ToString()!="")
				{
					model.PaintRawPart=int.Parse(row["PaintRawPart"].ToString());
				}
			}
			return model;
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(string strWhere)
		//{
		//	StringBuilder strSql=new StringBuilder();
		//	strSql.Append("select Id,Day,PartNumber,Model,MaterialName,Assembly,FG,AfterPacking,BeforePacking,AfterWIP,BeforeWIP,AfterLaser,BeforeLaser,TCPaint,MCPaint,PrintSupplier,UCPaint,PaintRawPart ");
		//	strSql.Append(" FROM ProductionInventoryHistory ");
		//	if(strWhere.Trim()!="")
		//	{
		//		strSql.Append(" where "+strWhere);
		//	}
		//	return DBHelp.SqlDB.Query(strSql.ToString());
		//}
		
	}
}

