using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:PQCQaViBinning
	/// </summary>
	public partial class PQCQaViBinning
	{
		public PQCQaViBinning()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PQCQaViBinning model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCQaViBinning(");
			strSql.Append("id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,updatedTime)");
			strSql.Append(" values (");
			strSql.Append("@id,@trackingID,@processes,@jobId,@PartNumber,@materialPartNo,@materialName,@shipTo,@model,@jigNo,@materialQty,@status,@nextViFlag,@dateTime,@day,@shift,@userName,@userID,@remark_1,@remark_2,@remark_3,@remark_4,@remarks,@updatedTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@processes", SqlDbType.VarChar,50),
					new SqlParameter("@jobId", SqlDbType.VarChar,50),
					new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@materialName", SqlDbType.VarChar,50),
					new SqlParameter("@shipTo", SqlDbType.VarChar,20),
					new SqlParameter("@model", SqlDbType.VarChar,20),
					new SqlParameter("@jigNo", SqlDbType.VarChar,20),
					new SqlParameter("@materialQty", SqlDbType.Decimal,9),
					new SqlParameter("@status", SqlDbType.VarChar,20),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,20),
					new SqlParameter("@remark_2", SqlDbType.VarChar,20),
					new SqlParameter("@remark_3", SqlDbType.VarChar,20),
					new SqlParameter("@remark_4", SqlDbType.VarChar,20),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
					new SqlParameter("@updatedTime", SqlDbType.DateTime2,8)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.trackingID;
			parameters[2].Value = model.processes;
			parameters[3].Value = model.jobId;
			parameters[4].Value = model.PartNumber;
			parameters[5].Value = model.materialPartNo;
			parameters[6].Value = model.materialName;
			parameters[7].Value = model.shipTo;
			parameters[8].Value = model.model;
			parameters[9].Value = model.jigNo;
			parameters[10].Value = model.materialQty;
			parameters[11].Value = model.status;
			parameters[12].Value = model.nextViFlag;
			parameters[13].Value = model.dateTime;
			parameters[14].Value = model.day;
			parameters[15].Value = model.shift;
			parameters[16].Value = model.userName;
			parameters[17].Value = model.userID;
			parameters[18].Value = model.remark_1;
			parameters[19].Value = model.remark_2;
			parameters[20].Value = model.remark_3;
			parameters[21].Value = model.remark_4;
			parameters[22].Value = model.remarks;
			parameters[23].Value = model.updatedTime;

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
		public bool Update(Common.Class.Model.PQCQaViBinning model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCQaViBinning set ");
			strSql.Append("id=@id,");
			strSql.Append("trackingID=@trackingID,");
			strSql.Append("processes=@processes,");
			strSql.Append("jobId=@jobId,");
			strSql.Append("PartNumber=@PartNumber,");
			strSql.Append("materialPartNo=@materialPartNo,");
			strSql.Append("materialName=@materialName,");
			strSql.Append("shipTo=@shipTo,");
			strSql.Append("model=@model,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("materialQty=@materialQty,");
			strSql.Append("status=@status,");
			strSql.Append("nextViFlag=@nextViFlag,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("day=@day,");
			strSql.Append("shift=@shift,");
			strSql.Append("userName=@userName,");
			strSql.Append("userID=@userID,");
			strSql.Append("remark_1=@remark_1,");
			strSql.Append("remark_2=@remark_2,");
			strSql.Append("remark_3=@remark_3,");
			strSql.Append("remark_4=@remark_4,");
			strSql.Append("remarks=@remarks,");
			strSql.Append("updatedTime=@updatedTime");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@processes", SqlDbType.VarChar,50),
					new SqlParameter("@jobId", SqlDbType.VarChar,50),
					new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@materialName", SqlDbType.VarChar,50),
					new SqlParameter("@shipTo", SqlDbType.VarChar,20),
					new SqlParameter("@model", SqlDbType.VarChar,20),
					new SqlParameter("@jigNo", SqlDbType.VarChar,20),
					new SqlParameter("@materialQty", SqlDbType.Decimal,9),
					new SqlParameter("@status", SqlDbType.VarChar,20),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,20),
					new SqlParameter("@remark_2", SqlDbType.VarChar,20),
					new SqlParameter("@remark_3", SqlDbType.VarChar,20),
					new SqlParameter("@remark_4", SqlDbType.VarChar,20),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
					new SqlParameter("@updatedTime", SqlDbType.DateTime2,8)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.trackingID;
			parameters[2].Value = model.processes;
			parameters[3].Value = model.jobId;
			parameters[4].Value = model.PartNumber;
			parameters[5].Value = model.materialPartNo;
			parameters[6].Value = model.materialName;
			parameters[7].Value = model.shipTo;
			parameters[8].Value = model.model;
			parameters[9].Value = model.jigNo;
			parameters[10].Value = model.materialQty;
			parameters[11].Value = model.status;
			parameters[12].Value = model.nextViFlag;
			parameters[13].Value = model.dateTime;
			parameters[14].Value = model.day;
			parameters[15].Value = model.shift;
			parameters[16].Value = model.userName;
			parameters[17].Value = model.userID;
			parameters[18].Value = model.remark_1;
			parameters[19].Value = model.remark_2;
			parameters[20].Value = model.remark_3;
			parameters[21].Value = model.remark_4;
			parameters[22].Value = model.remarks;
			parameters[23].Value = model.updatedTime;

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


        public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViBinning model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViBinning set ");
            strSql.Append("id=@id,");
            //strSql.Append("trackingID=@trackingID,");
            strSql.Append("processes=@processes,");
            strSql.Append("jobId=@jobId,");
            strSql.Append("PartNumber=@PartNumber,");
            //strSql.Append("materialPartNo=@materialPartNo,");
            strSql.Append("materialName=@materialName,");
            strSql.Append("shipTo=@shipTo,");
            strSql.Append("model=@model,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("materialQty=@materialQty,");
            strSql.Append("status=@status,");
            strSql.Append("nextViFlag=@nextViFlag,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            strSql.Append("remark_3=@remark_3,");
            strSql.Append("remark_4=@remark_4,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where  trackingID=@trackingID and  materialPartNo=@materialPartNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.VarChar,50),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@processes", SqlDbType.VarChar,50),
                    new SqlParameter("@jobId", SqlDbType.VarChar,50),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,20),
                    new SqlParameter("@model", SqlDbType.VarChar,20),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,20),
                    new SqlParameter("@materialQty", SqlDbType.Decimal,9),
                    new SqlParameter("@status", SqlDbType.VarChar,20),
                    new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar,20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.trackingID;
            parameters[2].Value = model.processes;
            parameters[3].Value = model.jobId;
            parameters[4].Value = model.PartNumber;
            parameters[5].Value = model.materialPartNo;
            parameters[6].Value = model.materialName;
            parameters[7].Value = model.shipTo;
            parameters[8].Value = model.model;
            parameters[9].Value = model.jigNo;
            parameters[10].Value = model.materialQty;
            parameters[11].Value = model.status;
            parameters[12].Value = model.nextViFlag;
            parameters[13].Value = model.dateTime;
            parameters[14].Value = model.day;
            parameters[15].Value = model.shift;
            parameters[16].Value = model.userName;
            parameters[17].Value = model.userID;
            parameters[18].Value = model.remark_1;
            parameters[19].Value = model.remark_2;
            parameters[20].Value = model.remark_3;
            parameters[21].Value = model.remark_4;
            parameters[22].Value = model.remarks;
            parameters[23].Value = model.updatedTime;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }



        public SqlCommand PackMaintenanceCommand(Common.Class.Model.PQCQaViBinning model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViBinning set ");
            strSql.Append("materialQty=@materialQty,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where  trackingID = @trackingID and materialPartNo=@materialPartNo and processes = 'PACKING' ");
            SqlParameter[] parameters = {
                new SqlParameter("@materialQty", SqlDbType.Decimal,9),
                new SqlParameter("@remarks", SqlDbType.VarChar,500),
                new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar,50)};


            parameters[0].Value = model.materialQty;
            parameters[1].Value = model.remarks;
            parameters[2].Value = model.updatedTime;
            parameters[3].Value = model.trackingID;
            parameters[4].Value = model.materialPartNo;
          
           

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViBinning ");
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




		public Common.Class.Model.PQCQaViBinning GetModel(string sTrackingID)
		{

            if (string.IsNullOrEmpty(sTrackingID))
                return null;


            StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,updatedTime from PQCQaViBinning ");
			strSql.Append(" where trackingID = @trackingID ");
			SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar)
			};

		
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
		public Common.Class.Model.PQCQaViBinning DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCQaViBinning model=new Common.Class.Model.PQCQaViBinning();
			if (row != null)
			{
				if(row["id"]!=null)
				{
					model.id=row["id"].ToString();
				}
				if(row["trackingID"]!=null)
				{
					model.trackingID=row["trackingID"].ToString();
				}
				if(row["processes"]!=null)
				{
					model.processes=row["processes"].ToString();
				}
				if(row["jobId"]!=null)
				{
					model.jobId=row["jobId"].ToString();
				}
				if(row["PartNumber"]!=null)
				{
					model.PartNumber=row["PartNumber"].ToString();
				}
				if(row["materialPartNo"]!=null)
				{
					model.materialPartNo=row["materialPartNo"].ToString();
				}
				if(row["materialName"]!=null)
				{
					model.materialName=row["materialName"].ToString();
				}
				if(row["shipTo"]!=null)
				{
					model.shipTo=row["shipTo"].ToString();
				}
				if(row["model"]!=null)
				{
					model.model=row["model"].ToString();
				}
				if(row["jigNo"]!=null)
				{
					model.jigNo=row["jigNo"].ToString();
				}
				if(row["materialQty"]!=null && row["materialQty"].ToString()!="")
				{
					model.materialQty=decimal.Parse(row["materialQty"].ToString());
				}
				if(row["status"]!=null)
				{
					model.status=row["status"].ToString();
				}
				if(row["nextViFlag"]!=null)
				{
					model.nextViFlag=row["nextViFlag"].ToString();
				}

                if (row["dateTime"] != null && row["dateTime"].ToString() != "")
                {
                    model.dateTime = DateTime.Parse(row["dateTime"].ToString());
                }

                if (row["day"] != null && row["day"].ToString() != "")
                {
                    model.day = DateTime.Parse(row["day"].ToString());
                }



             
                if (row["shift"]!=null)
				{
					model.shift=row["shift"].ToString();
				}
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["userID"]!=null)
				{
					model.userID=row["userID"].ToString();
				}
				if(row["remark_1"]!=null)
				{
					model.remark_1=row["remark_1"].ToString();
				}
				if(row["remark_2"]!=null)
				{
					model.remark_2=row["remark_2"].ToString();
				}
				if(row["remark_3"]!=null)
				{
					model.remark_3=row["remark_3"].ToString();
				}
				if(row["remark_4"]!=null)
				{
					model.remark_4=row["remark_4"].ToString();
				}
				if(row["remarks"]!=null)
				{
					model.remarks=row["remarks"].ToString();
				}

                if (row["updatedTime"] != null && row["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(row["updatedTime"].ToString());
                }
           
            }
			return model;
		}


        public DataSet GetList(string sTrackingID)
        {

            if (string.IsNullOrEmpty(sTrackingID))
                return null;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,updatedTime from PQCQaViBinning ");
            strSql.Append(" where trackingID = @trackingID ");
            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            parameters[0].Value = sTrackingID;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,updatedTime ");
			strSql.Append(" FROM PQCQaViBinning  where 1=1 and day >= @dateFrom and day < @dateTo");

            SqlParameter[] parameters = {
                    new SqlParameter("@dateFrom", SqlDbType.DateTime),
                    new SqlParameter("@dateTo", SqlDbType.DateTime)
            };
            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}
        

        public DataSet GetList(DateTime? dDateFrom, DateTime? dDateTo, string sJobID, string sCheckProcess)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,updatedTime ");
            strSql.Append(" FROM PQCQaViBinning  where 1=1 ");

            if (dDateFrom != null) strSql.Append(" and day >= @dateFrom ");
            if (dDateTo != null) strSql.Append(" and day < @dateTo ");
            if (!string.IsNullOrEmpty(sJobID)) strSql.Append(" and jobID = @jobID");
            if (!string.IsNullOrEmpty(sCheckProcess)) strSql.Append(" and processes = @processes");



            SqlParameter[] parameters = {
                    new SqlParameter("@dateFrom", SqlDbType.DateTime),
                    new SqlParameter("@dateTo", SqlDbType.DateTime),
                    new SqlParameter("@jobID", SqlDbType.VarChar),
                    new SqlParameter("@processes",SqlDbType.VarChar)
            };

            if (dDateFrom != null) parameters[0].Value = dDateFrom; else parameters[0] = null;
            if (dDateTo != null) parameters[1].Value = dDateTo; else parameters[1] = null;
            if (!string.IsNullOrEmpty(sJobID)) parameters[2].Value = sJobID; else parameters[2] = null;
            if (!string.IsNullOrEmpty(sCheckProcess)) parameters[3].Value = sCheckProcess; else parameters[3] = null;



            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



		
	
        
		#endregion  BasicMethod





        public DataTable GetBinInfoForAllInventoryReport(DateTime dStartTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 

a.PartNumber
,a.processes
,a.materialname
,sum(a.materialQty) as inventoryQty


from pqcqavibinning a
left join pqcbom b on a.partnumber = b.partnumber 
where 1=1 
and day > @starttime


and 
(	
	--packing
	a.processes = 'PACKING' 

	or 

	--checking的, 只取最后一道工序.
  	(a.nextviflag = 'True' and a.processes = (case	when b.processes like '%Check#3' then 'CHECK#3' 
													when b.processes like '%Check#2' then 'CHECK#2' 
													else 'CHECK#1' end))
)

group by a.PartNumber , a.materialName , a.processes ");


            SqlParameter[] parameters = {
                    new SqlParameter("@starttime", SqlDbType.DateTime)
            };
            parameters[0].Value = dStartTime;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }


	}
}

