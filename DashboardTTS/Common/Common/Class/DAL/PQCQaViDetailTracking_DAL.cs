 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
    /// <summary>
    /// 数据访问类:PQCQaViDetailTracking_DAL
    /// </summary>
    public class PQCQaViDetailTracking_DAL
	{
		public PQCQaViDetailTracking_DAL()
		{}
		#region  Method

		public Common.Class.Model.PQCQaViDetailTracking_Model GetModel(string sTrackingID, string sMaterialNo)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime from PQCQaViDetailTracking ");
			strSql.Append(" where 1=1  and trackingID = @trackingID and materialPartNo = @materialPartNo ");
			SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar,100),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar,100)
            };


            parameters[0].Value = sTrackingID;
            parameters[1].Value = sMaterialNo;


			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCQaViDetailTracking", "Function:		public Common.Class.Model.PQCQaViDetailTracking_Model GetModel()"  + "TableName:PQCQaViDetailTracking", "");
			Common.Class.Model.PQCQaViDetailTracking_Model model=new Common.Class.Model.PQCQaViDetailTracking_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.jobid = ds.Tables[0].Rows[0]["jobid"].ToString();
                model.trackingID=ds.Tables[0].Rows[0]["trackingID"].ToString();
				model.machineID=ds.Tables[0].Rows[0]["machineID"].ToString();
				if(ds.Tables[0].Rows[0]["dateTime"].ToString()!="")
				{
					model.dateTime=DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
				}
				model.materialPartNo=ds.Tables[0].Rows[0]["materialPartNo"].ToString();
				model.jigNo=ds.Tables[0].Rows[0]["jigNo"].ToString();
				model.model=ds.Tables[0].Rows[0]["model"].ToString();
                model.color = ds.Tables[0].Rows[0]["color"].ToString();
                if (ds.Tables[0].Rows[0]["cavityCount"].ToString()!="")
				{
					model.cavityCount=decimal.Parse(ds.Tables[0].Rows[0]["cavityCount"].ToString());
				}
				model.userName=ds.Tables[0].Rows[0]["userName"].ToString();
				model.userID=ds.Tables[0].Rows[0]["userID"].ToString();
				if(ds.Tables[0].Rows[0]["startTime"].ToString()!="")
				{
					model.startTime=DateTime.Parse(ds.Tables[0].Rows[0]["startTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["stopTime"].ToString()!="")
				{
					model.stopTime=DateTime.Parse(ds.Tables[0].Rows[0]["stopTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["day"].ToString()!="")
				{
					model.day=DateTime.Parse(ds.Tables[0].Rows[0]["day"].ToString());
				}
				model.shift=ds.Tables[0].Rows[0]["shift"].ToString();
				model.status=ds.Tables[0].Rows[0]["status"].ToString();
				model.remark_1=ds.Tables[0].Rows[0]["remark_1"].ToString();
				model.remark_2=ds.Tables[0].Rows[0]["remark_2"].ToString();
                if (ds.Tables[0].Rows[0]["totalQty"].ToString() != "")
                {
                    model.totalQty = decimal.Parse(ds.Tables[0].Rows[0]["totalQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalPassQty"].ToString() != "")
                {
                    model.totalPassQty = decimal.Parse(ds.Tables[0].Rows[0]["totalPassQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalRejectQty"].ToString() != "")
                {
                    model.totalRejectQty = decimal.Parse(ds.Tables[0].Rows[0]["totalRejectQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["passQty"].ToString() != "")
                {
                    model.passQty = decimal.Parse(ds.Tables[0].Rows[0]["passQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rejectQty"].ToString() != "")
                {
                    model.rejectQty = decimal.Parse(ds.Tables[0].Rows[0]["rejectQty"].ToString());
                }
				model.rejectQtyHour01=ds.Tables[0].Rows[0]["rejectQtyHour01"].ToString();
				model.rejectQtyHour02=ds.Tables[0].Rows[0]["rejectQtyHour02"].ToString();
				model.rejectQtyHour03=ds.Tables[0].Rows[0]["rejectQtyHour03"].ToString();
				model.rejectQtyHour04=ds.Tables[0].Rows[0]["rejectQtyHour04"].ToString();
				model.rejectQtyHour05=ds.Tables[0].Rows[0]["rejectQtyHour05"].ToString();
				model.rejectQtyHour06=ds.Tables[0].Rows[0]["rejectQtyHour06"].ToString();
				model.rejectQtyHour07=ds.Tables[0].Rows[0]["rejectQtyHour07"].ToString();
				model.rejectQtyHour08=ds.Tables[0].Rows[0]["rejectQtyHour08"].ToString();
				model.rejectQtyHour09=ds.Tables[0].Rows[0]["rejectQtyHour09"].ToString();
				model.rejectQtyHour10=ds.Tables[0].Rows[0]["rejectQtyHour10"].ToString();
				model.rejectQtyHour11=ds.Tables[0].Rows[0]["rejectQtyHour11"].ToString();
				model.rejectQtyHour12=ds.Tables[0].Rows[0]["rejectQtyHour12"].ToString();
				if(ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString()!="")
				{
					model.lastUpdatedTime=DateTime.Parse(ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString());
				}
				model.remarks=ds.Tables[0].Rows[0]["remarks"].ToString();
                model.processes = ds.Tables[0].Rows[0]["processes"].ToString();
                if (ds.Tables[0].Rows[0]["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["updatedTime"].ToString());
                }
                return model;
			}
			else
			{
				return null;
			}
		}

	
		public DataSet GetList(string STrackingID)
		{
			StringBuilder strSql=new StringBuilder();

            strSql.Append("select * FROM PQCQaViDetailTracking where 1=1 ");

            
            strSql.Append(" and trackingID = @trackingID  ");


            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID",SqlDbType.VarChar,100)
            };

            paras[0].Value = STrackingID;
            

			return DBHelp.SqlDB.Query(strSql.ToString(),paras,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}


        public DataSet GetList(string sTrackingID, string sJobID, DateTime? dDatefrom, DateTime? dDateto)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * FROM PQCQaViDetailTracking where 1=1 ");
            if (sTrackingID != "")    strSql.Append(" and trackingID = @trackingID ");
            if (sJobID != "") strSql.Append(" and jobID = @jobID ");
            if (dDatefrom != null) strSql.Append(" and datetime >= @datefrom ");
            if (dDateto != null) strSql.Append(" and datetime < @dateto ");




            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID",SqlDbType.VarChar),
                new SqlParameter("@jobID",SqlDbType.VarChar),
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime)
            };


            if (sTrackingID != "") paras[0].Value = sTrackingID; else paras[0] = null;
            if (sJobID != "") paras[1].Value = sJobID; else paras[1] = null;
            if (dDatefrom != null) paras[2].Value = dDatefrom; else paras[2] = null;
            if (dDateto != null) paras[3].Value = dDateto; else paras[3] = null;



            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }




        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViDetailTracking_Model model,SqlCommand cmd=null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update PQCQaViDetailTracking set 
                           [status] = @status
                           ,stopTime = @stopTime
                           ,updatedTime = getdate()
                          
                           ,passQty = @passQty
                           ,totalQty = @totalQty
                           ,remark_1 = @remark_1

                            where trackingID = @trackingID ");
            SqlParameter[] parameters = {
                new SqlParameter("@passQty", SqlDbType.VarChar,50),
                new SqlParameter("@totalQty", SqlDbType.VarChar,50),
                new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                new SqlParameter("@status", SqlDbType.VarChar,50),
                new SqlParameter("@stopTime", SqlDbType.DateTime,50)
            };

            parameters[0].Value = model.passQty;
            parameters[1].Value = model.totalQty;
            parameters[2].Value = model.remark_1;
            parameters[3].Value = model.trackingID;
            parameters[4].Value = model.status;
            parameters[5].Value = model.stopTime;

            if (cmd != null)
            {
                cmd.CommandText = strSql.ToString();
                foreach (SqlParameter par in parameters)
                {
                    if (par != null)
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = par.ParameterName, Value = par.Value });
                    }
                }
            }

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update PQCQaViDetailTracking set 
                                    totalQty = @totalQty , 
                                    passQty = @passQty, 
                                    updatedTime =  GETDATE()
                                    where trackingID  =@trackingID and materialPartNo = @materialPartNo ");
            SqlParameter[] parameters = {
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@passQty", SqlDbType.Decimal),
                new SqlParameter("@trackingID", SqlDbType.VarChar,100),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar,100)              
            };

            parameters[0].Value = model.totalQty;
            parameters[1].Value = model.passQty;
            parameters[2].Value = model.trackingID;
            parameters[3].Value = model.materialPartNo;
     

         
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


        public SqlCommand UpdateJobByLaserMaintenance(Common.Class.Model.PQCQaViDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCQaViDetailTracking set
                                totalQty = @totalQty, 
                                passQty = @passQty, 
                                lastUpdatedTime = @lastUpdatedTime,
                                updatedTime =  @updatedTime,
                                remarks = @remarks
                            where trackingID  =@trackingID ");

            SqlParameter[] parameters = {
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@passQty", SqlDbType.Decimal),
                new SqlParameter("@trackingID", SqlDbType.VarChar),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@updatedTime", SqlDbType.DateTime)
            };

            parameters[0].Value = model.totalQty;
            parameters[1].Value = model.passQty;
            parameters[2].Value = model.trackingID;
            parameters[3].Value = model.remarks;
            parameters[4].Value = model.lastUpdatedTime;
            parameters[5].Value = model.lastUpdatedTime;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }




        public SqlCommand UpdatePQCMaintenance(Common.Class.Model.PQCQaViDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCQaViDetailTracking set
                                totalQty = @totalQty, 
                                passQty = @passQty, 
                                rejectQty = @rejectQty,
                                status = @status,
                                lastUpdatedTime = @lastUpdatedTime,
                                updatedTime =  @updatedTime,
                                remarks = @remarks
                            where trackingID  =@trackingID and materialPartNo = @materialPartNo");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar),
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@passQty", SqlDbType.Decimal),
                new SqlParameter("@rejectQty", SqlDbType.Decimal),
                new SqlParameter("@status", SqlDbType.VarChar),
                new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@updatedTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.trackingID;
            parameters[1].Value = model.totalQty;
            parameters[2].Value = model.passQty;
            parameters[3].Value = model.rejectQty;
            parameters[4].Value = model.status;
            parameters[5].Value = model.lastUpdatedTime;
            parameters[6].Value = model.updatedTime;
            parameters[7].Value = model.remarks;
            parameters[8].Value = model.materialPartNo;




            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



        public SqlCommand UpdateForQASetup(Common.Class.Model.PQCQaViDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCQaViDetailTracking set
                                totalQty = @totalQty, 
                                passQty = @passQty, 
                                lastUpdatedTime = @lastUpdatedTime,
                                updatedTime =  @updatedTime,
                                remarks = @remarks
                            where trackingID  =@trackingID and materialPartNo = @materialPartNo");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar),
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@passQty", SqlDbType.Decimal),
                new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@updatedTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.trackingID;
            parameters[1].Value = model.totalQty;
            parameters[2].Value = model.passQty;
            parameters[3].Value = model.lastUpdatedTime;
            parameters[4].Value = model.updatedTime;
            parameters[5].Value = model.remarks;
            parameters[6].Value = model.materialPartNo;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



        public SqlCommand DeleteJobCommand(string  sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" delete from PQCQaViDetailTracking where jobId  =@jobID");

            SqlParameter[] parameters = {
                new SqlParameter("@jobID", SqlDbType.VarChar),
            };

            parameters[0].Value = sJobNo;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


        internal DataTable GetPaintTcInventory(DateTime dStartTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
a.jobId,
b.materialName,
SUM(b.passQty) as passQty,
SUM(b.rejectQty) as rejQty
from PQCQaViTracking a
left
join PQCQaViDetailTracking b on a.trackingID = b.trackingID
left
join (
    select
    partNumber,
    processes,
	case when CHARINDEX('Check#3', processes, 0) > 0 then 'CHECK#3'
         when CHARINDEX('Check#2', processes, 0) > 0 then 'CHECK#2'
	     else 'CHECK#1'
    end as lastCheckProcess
    from PQCBom
) c on a.partNumber = c.partNumber
where 1 = 1
and a.processes = 'check#1' and c.lastCheckProcess != 'Check#1'  
and a.day >= @startTime 
group by a.jobId, b.materialName ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@startTime",SqlDbType.DateTime)
            };
            parameters[0].Value = dStartTime;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0];
        }



        #endregion  Method


    }
}

