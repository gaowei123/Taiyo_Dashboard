 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:PQCQaViDefectTracking_DAL
	/// </summary>
	public class PQCQaViDefectTracking_DAL
	{
		public PQCQaViDefectTracking_DAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCQaViDefectTracking(");
			strSql.Append("id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,defectDescription,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime)");
			strSql.Append(" values (");
			strSql.Append("@id,@jobid,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@defectCodeID,@defectCode,@defectDescription,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@updatedTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.DateTime2,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@defectDescription", SqlDbType.VarChar,50)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViDefectTracking_DAL" , 
                 "Function:		public void Add(Common.Model.PQCQaViDefectTracking_Model model)"  + 
                 "TableName:PQCQaViDefectTracking" , 
                 ";id = "+ (model.id == null ? "" : model.id.ToString() ) + 
                 ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) + 
                 ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString() ) + 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + 
                 ";partNumber = "+ (model.materialPartNo == null ? "" : model.materialPartNo.ToString() ) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString() ) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString() ) + 
                 ";cavityCount = "+ (model.cavityCount == null ? "" : model.cavityCount.ToString() ) + 
                 ";userName = "+ (model.userName == null ? "" : model.userName.ToString() ) + 
                 ";userID = "+ (model.userID == null ? "" : model.userID.ToString() ) + 
                 ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString() ) + 
                 ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString() ) + 
                 ";day = "+ (model.day == null ? "" : model.day.ToString() ) + 
                 ";shift = "+ (model.shift == null ? "" : model.shift.ToString() ) + 
                 ";status = "+ (model.status == null ? "" : model.status.ToString() ) + 
                 ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString() ) + 
                 ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString() ) + 
                 ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString() ) + 
                 ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString() ) + 
                 ";rejectQty = "+ (model.rejectQty == null ? "" : model.rejectQty.ToString() ) + 
                 ";rejectQtyHour01 = "+ (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString() ) + 
                 ";rejectQtyHour02 = "+ (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString() ) + 
                 ";rejectQtyHour03 = "+ (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString() ) + 
                 ";rejectQtyHour04 = "+ (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString() ) + 
                 ";rejectQtyHour05 = "+ (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString() ) + 
                 ";rejectQtyHour06 = "+ (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString() ) + 
                 ";rejectQtyHour07 = "+ (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString() ) + 
                 ";rejectQtyHour08 = "+ (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString() ) + 
                 ";rejectQtyHour09 = "+ (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString() ) + 
                 ";rejectQtyHour10 = "+ (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString() ) + 
                 ";rejectQtyHour11 = "+ (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString() ) + 
                 ";rejectQtyHour12 = "+ (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString() ) + 
                 ";lastUpdatedTime = "+ (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString() ) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString() ) + 
                 ";processes = " + (model.processes == null ? "" : model.processes.ToString()) + "");

			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID ;
			parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount ;
			parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID ;
			parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day ;
			parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift ;
			parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status ;
			parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
			parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
			parameters[17].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[18].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[19].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty ;
			parameters[20].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01 ;
			parameters[21].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02 ;
			parameters[22].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03 ;
			parameters[23].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04 ;
			parameters[24].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05 ;
			parameters[25].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06 ;
			parameters[26].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07 ;
			parameters[27].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08 ;
			parameters[28].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09 ;
			parameters[29].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10 ;
			parameters[30].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11 ;
			parameters[31].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12 ;
			parameters[32].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime ;
			parameters[33].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[34].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[35].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription;

            DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Class.Model.PQCQaViDefectTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCQaViDefectTracking(");
            strSql.Append("id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,defectDescription,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@jobid,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@defectCodeID,@defectCode,@defectDescription,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,50),
                    new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
                    new SqlParameter("@defectCode", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@defectDescription", SqlDbType.VarChar,50)};
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViDefectTracking_DAL",
                "Function:		public SqlCommand AddCommand(Common.Model.PQCQaViDefectTracking_Model model)" +
                "TableName:PQCQaViDefectTracking",
                ";id = " + (model.id == null ? "" : model.id.ToString()) +
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partNumber = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";defectCodeID = " + (model.defectCodeID == null ? "" : model.defectCodeID.ToString()) +
                ";defectCode = " + (model.defectCode == null ? "" : model.defectCode.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) +
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) +
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) +
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) +
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) +
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) +
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) +
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) +
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) +
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) +
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) +
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) + "");

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID;
            parameters[18].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode;
            parameters[19].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[20].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[21].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[22].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[23].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[24].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[25].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[26].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[27].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[28].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[29].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[30].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[31].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[32].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[33].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[34].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[35].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}




        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViDefectTracking_Model model,SqlCommand cmd=null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update PQCQaViDefectTracking set 
                           [status] = 'End'
                           ,stopTime = case when shift='Day' then DATEADD(hour,20, CONVERT(datetime,convert(varchar(50), convert(datetime2, startTime),23))) 
			                                 when shift='Night' then DATEADD(hour,32, CONVERT(datetime,convert(varchar(50),convert(datetime2, startTime),23))) end
                           ,updatedTime = getdate()
                           ,remark_1 = @remark_1
                            where trackingID = @trackingID ");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                new SqlParameter("@remark_1", SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.trackingID;
            parameters[1].Value = model.remark_1;

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

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update PQCQaViDefectTracking set ");
            strSql.Append("id=@id,");
            strSql.Append("jobid=@jobid,");
            //strSql.Append("trackingID=@trackingID,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("dateTime=@dateTime,");
            //strSql.Append("partNumber=@partNumber,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("model=@model,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("stopTime=@stopTime,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("status=@status,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            //strSql.Append("defectCodeID=@defectCodeID,");
            strSql.Append("defectCode=@defectCode,");
            strSql.Append("defectDescription=@defectDescription,");
            strSql.Append("rejectQty=@rejectQty,");
            strSql.Append("rejectQtyHour01=@rejectQtyHour01,");
            strSql.Append("rejectQtyHour02=@rejectQtyHour02,");
            strSql.Append("rejectQtyHour03=@rejectQtyHour03,");
            strSql.Append("rejectQtyHour04=@rejectQtyHour04,");
            strSql.Append("rejectQtyHour05=@rejectQtyHour05,");
            strSql.Append("rejectQtyHour06=@rejectQtyHour06,");
            strSql.Append("rejectQtyHour07=@rejectQtyHour07,");
            strSql.Append("rejectQtyHour08=@rejectQtyHour08,");
            strSql.Append("rejectQtyHour09=@rejectQtyHour09,");
            strSql.Append("rejectQtyHour10=@rejectQtyHour10,");
            strSql.Append("rejectQtyHour11=@rejectQtyHour11,");
            strSql.Append("rejectQtyHour12=@rejectQtyHour12,");
            strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("processes=@processes,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where trackingID=@trackingID and materialPartNo=@materialPartNo and defectCodeID=@defectCodeID");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.DateTime2,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
					new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
					new SqlParameter("@defectCode", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
					new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@defectDescription", SqlDbType.VarChar,50)};
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID ;
			parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount ;
			parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID ;
			parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day ;
			parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift ;
			parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status ;
			parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
			parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
			parameters[17].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID ;
			parameters[18].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode ;
			parameters[19].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty ;
			parameters[20].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01 ;
			parameters[21].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02 ;
			parameters[22].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03 ;
			parameters[23].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04 ;
			parameters[24].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05 ;
			parameters[25].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06 ;
			parameters[26].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07 ;
			parameters[27].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08 ;
			parameters[28].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09 ;
			parameters[29].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10 ;
			parameters[30].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11 ;
			parameters[31].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12 ;
			parameters[32].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime ;
			parameters[33].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[34].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[35].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                "Class:PQCQaViDefectTracking_DAL" , 
                "Function:		public bool Update(Common.Model.PQCQaViDefectTracking_Model model)"  + 
                "TableName:PQCQaViDefectTracking" , 
                ";id = "+ (model.id == null ? "" : model.id.ToString() ) + 
                ";trackingID = "+ (model.trackingID == null ? "" : model.trackingID.ToString() ) + 
                ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + 
                ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + 
                ";partNumber = "+ (model.materialPartNo == null ? "" : model.materialPartNo.ToString() ) + 
                ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString() ) + 
                ";model = "+ (model.model == null ? "" : model.model.ToString() ) + 
                ";cavityCount = "+ (model.cavityCount == null ? "" : model.cavityCount.ToString() ) + 
                ";userName = "+ (model.userName == null ? "" : model.userName.ToString() ) + 
                ";userID = "+ (model.userID == null ? "" : model.userID.ToString() ) + 
                ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString() ) + 
                ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString() ) + 
                ";day = "+ (model.day == null ? "" : model.day.ToString() ) + 
                ";shift = "+ (model.shift == null ? "" : model.shift.ToString() ) + 
                ";status = "+ (model.status == null ? "" : model.status.ToString() ) + 
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString() ) + 
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString() ) + 
                ";defectCodeID = "+ (model.defectCodeID == null ? "" : model.defectCodeID.ToString() ) + 
                ";defectCode = "+ (model.defectCode == null ? "" : model.defectCode.ToString() ) + 
                ";rejectQty = "+ (model.rejectQty == null ? "" : model.rejectQty.ToString() ) + 
                ";rejectQtyHour01 = "+ (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString() ) + 
                ";rejectQtyHour02 = "+ (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString() ) + 
                ";rejectQtyHour03 = "+ (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString() ) + 
                ";rejectQtyHour04 = "+ (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString() ) + 
                ";rejectQtyHour05 = "+ (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString() ) + 
                ";rejectQtyHour06 = "+ (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString() ) + 
                ";rejectQtyHour07 = "+ (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString() ) + 
                ";rejectQtyHour08 = "+ (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString() ) + 
                ";rejectQtyHour09 = "+ (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString() ) + 
                ";rejectQtyHour10 = "+ (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString() ) + 
                ";rejectQtyHour11 = "+ (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString() ) + 
                ";rejectQtyHour12 = "+ (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString() ) + 
                ";lastUpdatedTime = "+ (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString() ) + 
                ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString() ) + 
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) + "");

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
		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViDefectTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViDefectTracking set ");
            strSql.Append("id=@id,");
            strSql.Append("jobid=@jobid,");
            //strSql.Append("trackingID=@trackingID,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("dateTime=@dateTime,");
            //strSql.Append("partNumber=@partNumber,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("model=@model,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("stopTime=@stopTime,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("status=@status,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            //strSql.Append("defectCodeID=@defectCodeID,");
            strSql.Append("defectCode=@defectCode,");
            strSql.Append("defectDescription=@defectDescription,");
            strSql.Append("rejectQty=@rejectQty,");
            strSql.Append("rejectQtyHour01=@rejectQtyHour01,");
            strSql.Append("rejectQtyHour02=@rejectQtyHour02,");
            strSql.Append("rejectQtyHour03=@rejectQtyHour03,");
            strSql.Append("rejectQtyHour04=@rejectQtyHour04,");
            strSql.Append("rejectQtyHour05=@rejectQtyHour05,");
            strSql.Append("rejectQtyHour06=@rejectQtyHour06,");
            strSql.Append("rejectQtyHour07=@rejectQtyHour07,");
            strSql.Append("rejectQtyHour08=@rejectQtyHour08,");
            strSql.Append("rejectQtyHour09=@rejectQtyHour09,");
            strSql.Append("rejectQtyHour10=@rejectQtyHour10,");
            strSql.Append("rejectQtyHour11=@rejectQtyHour11,");
            strSql.Append("rejectQtyHour12=@rejectQtyHour12,");
            strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("processes=@processes,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where trackingID=@trackingID and materialPartNo=@materialPartNo and defectCodeID=@defectCodeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,50),
                    new SqlParameter("@defectCodeID", SqlDbType.VarChar,50),
                    new SqlParameter("@defectCode", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@defectDescription", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.defectCodeID == null ? (object)DBNull.Value : model.defectCodeID;
            parameters[18].Value = model.defectCode == null ? (object)DBNull.Value : model.defectCode;
            parameters[19].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[20].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[21].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[22].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[23].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[24].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[25].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[26].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[27].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[28].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[29].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[30].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[31].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[32].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[33].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[34].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[35].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViDefectTracking_DAL",
                "Function:		public SqlCommand UpdateCommand(Common.Model.PQCQaViDefectTracking_Model model)" +
                "TableName:PQCQaViDefectTracking",
                ";id = " + (model.id == null ? "" : model.id.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partNumber = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";defectCodeID = " + (model.defectCodeID == null ? "" : model.defectCodeID.ToString()) +
                ";defectCode = " + (model.defectCode == null ? "" : model.defectCode.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) +
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) +
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) +
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) +
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) +
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) +
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) +
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) +
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) +
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) +
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) +
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) + "");

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
        }

  
       
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViDefectTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCQaViDefectTracking_DAL" , "Function:		public bool Delete()"  + "TableName:PQCQaViDefectTracking" , "");
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
		public SqlCommand DeleteCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViDefectTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCQaViDefectTracking_DAL" , "Function:		public SqlCommand DeleteCommand()"  + "TableName:PQCQaViDefectTracking" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViDefectTracking ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:PQCQaViDefectTracking_DAL" , "Function:		public SqlCommand DeleteAllCommand( )"  + "TableName:PQCQaViDefectTracking" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCQaViDefectTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,defectDescription,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime from PQCQaViDefectTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
            };

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCQaViDefectTracking_DAL", "Function:		public Common.Model.PQCQaViDefectTracking_Model GetModel()" + "TableName:PQCQaViDefectTracking", "");
            Common.Class.Model.PQCQaViDefectTracking_Model model = new Common.Class.Model.PQCQaViDefectTracking_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
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
				if(ds.Tables[0].Rows[0]["cavityCount"].ToString()!="")
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
				model.defectCodeID=ds.Tables[0].Rows[0]["defectCodeID"].ToString();
				model.defectCode=ds.Tables[0].Rows[0]["defectCode"].ToString();
                model.defectDescription = ds.Tables[0].Rows[0]["defectDescription"].ToString();
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

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sTouchPC, string sRejType,string sRejCode, string sLotNo, string sPartNo)
		{
            StringBuilder strSql=new StringBuilder();

            strSql.AppendFormat(@"
with PaintDelivery as (
	select Jobnumber, Lotno from OPENDATASOURCE('SQLOLEDB', {0} ).taiyo_painting.dbo.PaintingDeliveryHis
	where updatedtime > dateadd(day, -60, updatedtime)
)", StaticRes.Global.SqlConnection.SqlconnPainting);

            strSql.Append(@"
                select
convert(varchar(50), a.day, 111) as Day
, a.shift as Shift
, 'Station' + a.machineID as touchPC

, a.model as model
, c.partNumber as partNumber
, a.jobId as JobNumber
, b.LotNo as lotNo
, materialPartNo as materialNo
, defectDescription as RejType
, defectCode as RejCode

, c.totalQty as TotalQty
, a.rejectQty as RejQty
,case when ISNULL(c.totalQty, 0) = 0 then '0%'
else  CONVERT(varchar(50), CONVERT(float, ROUND(a.rejectQty / c.totalQty * 100, 2))) + '%'
end as RejRate

, a.userName as Operator
, a.dateTime as DateTime
from PQCQaViDefectTracking a
left
join PaintDelivery b   on a.jobId COLLATE Chinese_PRC_CI_AS = b.JobNumber  COLLATE Chinese_PRC_CI_AS
left join PQCQaViTracking c on a.trackingID = c.trackingID

where 1 = 1 and a.rejectQty > 0
and a.datetime >= @dDateFrom
and a.datetime < @dDateTo ");

            if (sTouchPC.Trim() != "")
                strSql.Append("  and a.machineID = @TouchPC ");
            
            if (sRejType.Trim() != "")
                strSql.Append("  and a.defectDescription = @RejType ");
            
            if (sRejCode.Trim() != "")
                strSql.Append("  and a.defectCode = @RejCode ");
            
            if (sLotNo.Trim() != "")
                strSql.Append(" and b.LotNo = @LotNo");

            if (sPartNo.Trim() != "")
                strSql.Append(" and c.PartNumber = @PartNo");



            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
                new SqlParameter("@TouchPC", SqlDbType.VarChar),
                new SqlParameter("@RejType", SqlDbType.VarChar),
                new SqlParameter("@RejCode", SqlDbType.VarChar),
                new SqlParameter("@LotNo", SqlDbType.VarChar),
                new SqlParameter("@PartNo", SqlDbType.VarChar)
            };


            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            
            if (sTouchPC.Trim() != "")
                paras[2].Value = sTouchPC;
            else
                paras[2] = null;

            if (sRejType.Trim() != "")
                paras[3].Value = sRejType;
            else
                paras[3] = null;

            if (sRejCode.Trim() != "")
                paras[4].Value = sRejCode;
            else
                paras[4] = null;

            if (sLotNo.Trim() != "")
                paras[5].Value = sLotNo;
            else
                paras[5] = null;

            if (sPartNo.Trim() != "")
                paras[6].Value = sPartNo;
            else
                paras[6] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}


        public DataSet GetListByTrackingID(string sTrackingID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select * from PQCQaViDefectTracking where 1=1  ");

            if (sTrackingID.Trim() != "")
            {
                strSql.Append("and trackingID = @trackingID");
            }
      
            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID", SqlDbType.VarChar,100)
            };

            
            if (sTrackingID.Trim() != "")
                paras[0].Value = sTrackingID;
            else
                paras[0] = null;
            
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
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
			strSql.Append(" id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,defectDescription,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime ");
			strSql.Append(" FROM PQCQaViDefectTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

      

        #endregion  Method

        #region MyRegion
      
      
        internal DataTable getList(string jobNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select materialPartNo, defectCode, defectDescription, sum(rejectQty) as rejectQty
                            from PQCQaViDefectTracking
                            where jobId = @jobNumber
                            group by materialPartNo, defectCode, defectDescription ");
      
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar, 50)
            };
            parameters[0].Value = jobNumber;
         

            
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds== null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
            
       
        }
        
        internal DataTable GetDefectDetailForPQCReport(DateTime dDateFrom, DateTime dDateTo, string sPartNumber, string sModel, string sColor, string sType, string sSupplier, string sDescription, string sCoating, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();

            //查询出所有job到临时表
            strSql.Append(@"
with allJobsForReports as (
    select
    distinct UPPER(jobid) as jobID
    from PQCQaViTracking a
    left join PQCBom b on a.partNumber = b.partNumber
    where 1 = 1 and acceptQty + rejectQty >= totalQty

    --Bom中有check2的, 先查check2的nextViFlag
    and case when b.processes like '%Check#2%'
        then  case when a.processes = 'CHECK#2' and a.nextViFlag = 'True' then 'True' else 'False' end
        --只有check1的, 直接以nextViFlag定
		else a.nextViFlag
    end = 'True'

    and a.dateTime >= @DateFrom
    and a.datetime < @DateTo ");


            if (sPartNumber.Trim() != "")
                strSql.AppendLine(" and b.partnumber = @PartNumber ");

            if (sModel.Trim() != "")
                strSql.AppendLine(" and b.model = @Model");

            if (sColor.Trim() != "")
                strSql.AppendLine(" and b.color = @Color  ");

            if (sSupplier.Trim() != "")
                strSql.AppendLine(" and b.remark_1 = @supplier");

            if (sCoating.Trim() != "")
                strSql.AppendLine(" and b.coating = @coating");

            if (sDescription.Trim() != "")
                strSql.AppendLine(" and b.description = @description ");

            if (sType.Trim() != "")
                strSql.AppendLine(" and b.Type = @Type");

            if (sJobNo.Trim() != "")
                strSql.AppendLine(" and a.JobID = @JobNo");


            strSql.AppendLine(")");
            //查询出所有job到临时表
            

            strSql.Append(@"
select 
a.jobID as JobNumber
,'' as PartNumber
,b.materialPartNo as materialNo
,b.processes as process
,case when d.processes like '%Laser%' then 'LASER' else 'WIP' end BomProcess
,'' as LotNo
,'' as LotQty

,case when b.defectDescription = 'Others' then 'OTHERS ' + b.defectCode
	  when b.defectDescription = 'Mould' then 'MOULDING ' + b.defectCode
      when b.defectDescription = 'Paint' then 'PAINTING ' + b.defectCode
	  when b.defectDescription = 'Laser' then 'LASER ' + b.defectCode
end  as defectCode

,b.rejectQty
,b.defectDescription
,b.dateTime
,b.processes

from allJobsForReports a 
left join PQCQaViDefectTracking b on a.jobID = b.jobId
left join PQCQaViTracking c on b.trackingID=c.trackingID
left join pqcbom d on c.partnumber= d.partnumber

where b.datetime > DATEADD(day,-30, @DateFrom)  ");


            
            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@description", SqlDbType.VarChar,50),
                new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Color", SqlDbType.VarChar,50),
                new SqlParameter("@supplier", SqlDbType.VarChar,50),
                new SqlParameter("@coating", SqlDbType.VarChar,50),
                new SqlParameter("@Type", SqlDbType.VarChar,50),
                new SqlParameter("JobNo",SqlDbType.VarChar,50)
            };


            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sDescription != "") parameters[2].Value = sDescription; else parameters[2] = null;
            if (sPartNumber != "") parameters[3].Value = sPartNumber; else parameters[3] = null;
            if (sModel != "") parameters[4].Value = sModel; else parameters[4] = null;
            if (sColor != "") parameters[5].Value = sColor; else parameters[5] = null;
            if (sSupplier != "") parameters[6].Value = sSupplier; else parameters[6] = null;
            if (sCoating != "") parameters[7].Value = sCoating; else parameters[7] = null;
            if (sType != "") parameters[8].Value = sType; else parameters[8] = null;
            if (sJobNo != "") parameters[9].Value = sJobNo; else parameters[9] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        
        internal DataTable GetDefectDetailForBezelPanelReport(DateTime dDateFrom, DateTime dDateTo, string sType, string sDescription, string sNumber)
        {
            StringBuilder strSql = new StringBuilder();



            //所有check完成的jobs 临时表
            strSql.AppendLine(@"
with allJobsForReports as (
	select  
		distinct UPPER(jobid) as jobID
	from PQCQaViTracking a 
	left join PQCBom b on a.partNumber = b.partNumber
	where 1 = 1  and acceptQty + rejectQty >= totalQty 
	and a.dateTime >= @DateFrom
	and a.datetime <  @DateTo
    --Bom中有check2的, 先查check2的nextViFlag
	and case when b.processes like '%Check#2%'
		then  case when a.processes = 'CHECK#2' and a.nextViFlag = 'True' then 'True' else 'False' end
		--只有check1的, 直接以nextViFlag定
		else a.nextViFlag
	end = 'True' 
");
            if (sType.Trim() != "")
                strSql.AppendLine(" and b.type =@Type ");

            if (sDescription.Trim() != "")
                strSql.AppendLine(" and b.description =@Description");

            if (sNumber.Trim() != "")
                strSql.AppendLine(" and b.Number = @Number ");

            strSql.AppendLine(")");
            //所有check完成的jobs 临时表

            
          

            //Paint delivery his 临时表
            strSql.AppendFormat(@"
,PaintingDeliveryHis as (
	SELECT jobNumber,inQuantity FROM OPENDATASOURCE( 'SQLOLEDB', {0} ).[Taiyo_Painting].[dbo].PaintingDeliveryHis  where UpdatedTime > '2019-4-15'
)", StaticRes.Global.SqlConnection.SqlconnPainting);




            strSql.Append(@"
select 
UPPER(a.jobId) as Jobnumber
,convert(decimal(18,0), isnull( f.inQuantity,0)) as LotQty
,b.defectCodeID
,b.defectCode
,b.defectDescription
,isnull( e.unitCost, 0.1) as unitCost
,b.rejectQty

from allJobsForReports a
left join PQCQaViDefectTracking b on a.jobID = b.jobID
left join PQCQaViTracking d on d.trackingID = b.trackingID
left join PQCBom e on e.partNumber = d.partNumber 
left join PaintingDeliveryHis f on a.jobID collate Chinese_PRC_CI_AS = f.jobNumber collate Chinese_PRC_CI_AS ");


            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@Type", SqlDbType.VarChar,50),
                new SqlParameter("@Description", SqlDbType.VarChar,50),
                new SqlParameter("@Number", SqlDbType.VarChar,50)
            };


            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sType.Trim() != "") paras[2].Value = sType; else paras[2] = null;
            if (sDescription.Trim() != "") paras[3].Value = sDescription; else paras[3] = null;
            if (sNumber.Trim() != "") paras[4].Value = sNumber; else paras[4] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        
        #endregion



     

        public DataTable GetVIDefectForButtonReport_NEW(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();           
            strSql.Append(@"
select 
 a.jobID
,a.materialPartNo
,a.defectCodeID
,a.defectCode
,a.rejectQty

,case when a.defectDescription = 'Mould' and c.remark_1 = 'TTS'    then 'TTS'
      when a.defectDescription = 'Mould' and c.remark_1 != 'TTS' then 'Vendor'
      else a.defectDescription end as defectDescription

,b.processes

from PQCQaViDefectTracking a 
left join PQCQaViTracking b on a.trackingID = b.trackingID
left join PQCBom c on  b.partNumber = c.partNumber where 1=1 ");
            
            strSql.Append(" and a.jobID in " + sqlWhere);



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),  DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }




        public DataTable GetViDefect_ForSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
trackingID
,ISNULL(SUM( case when defectdescription = 'Mould' then rejectQty end),0) as mouldRej
,ISNULL(SUM( case when defectdescription = 'Paint' then rejectQty end),0) as paintRej
,ISNULL(SUM( case when defectdescription = 'Laser' then rejectQty end),0) as laserRej
,ISNULL(SUM( case when defectdescription = 'others' then rejectQty end),0) as othersRej

from PQCQaViDefectTracking
where day >= @DateFrom  and day < @DateTo   ");

            if (sShift != "")
            {
                strSql.Append(" and shift = @shift");
            }

            strSql.Append("group by trackingID");


            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sShift.Trim() != "") parameters[2].Value = sShift; else parameters[2] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }




        #region defect detail list
        public DataTable GetMouldDefect(string sJobId,string sTrackingID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
    jobid
    ,materialpartNo
    ,ISNULL(sum(case when defectDescription='Mould' then  rejectQty end ),0) as rejectQty
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Raw Part Scratch' then rejectQty end),0) as [Raw Part Scratch]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Oil Stain' then rejectQty end),0) as [Oil Stain]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Dented' then rejectQty end),0) as [Dented]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Dust' then rejectQty end),0) as [Dust]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Flyout' then rejectQty end),0) as [Flyout]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Over Spray' then rejectQty end),0) as [Over Spray]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Weld line ' then rejectQty end),0) as [Weld line]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Crack' then rejectQty end),0) as [Crack]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Gas mark' then rejectQty end),0) as [Gas mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Sink mark' then rejectQty end),0) as [Sink mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Bubble' then rejectQty end),0) as [Bubble]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='White dot' then rejectQty end),0) as [White dot]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Black dot' then rejectQty end),0) as [Black dot]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Red Dot' then rejectQty end),0) as [Red Dot]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Poor Gate Cut' then rejectQty end),0) as [Poor Gate Cut]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='High Gate' then rejectQty end),0) as [High Gate]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='White Mark' then rejectQty end),0) as [White Mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Drag mark' then rejectQty end),0) as [Drag mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Foreigh Material' then rejectQty end),0) as [Foreigh Material]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Double Claim' then rejectQty end),0) as [Double Claim]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Short mould' then rejectQty end),0) as [Short mould]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Flashing' then rejectQty end),0) as [Flashing]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Pink Mark' then rejectQty end),0) as [Pink Mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Deform' then rejectQty end),0) as [Deform]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Damage' then rejectQty end),0) as [Damage]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Mould Dirt' then rejectQty end),0) as [Mould Dirt]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Yellowish' then rejectQty end),0) as [Yellowish]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Oil Mark' then rejectQty end),0) as [Oil Mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Printing Mark' then rejectQty end),0) as [Printing Mark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Printing Uneven' then rejectQty end),0) as [Printing Uneven]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Printing Color Dark' then rejectQty end),0) as [Printing Color Dark]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Wrong Orietation' then rejectQty end),0) as [Wrong Orietation]
    ,ISNULL(SUM(case when defectDescription='Mould' and defectCode='Other' then rejectQty end),0) as [Other]
from PQCQaViDefectTracking  
where 1=1  ");

            if (sJobId != "") strSql.Append(" and jobId = @jobId ");
            if (sTrackingID != "") strSql.Append(" and trackingID = @trackingID");

            strSql.Append(" group by jobid, materialPartNo ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobId", SqlDbType.VarChar),
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            if (sJobId != "") parameters[0].Value = sJobId; else parameters[0] = null;
            if (sTrackingID != "") parameters[1].Value = sTrackingID; else parameters[1] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        
        public DataTable GetPaintDefect(string sJobId, string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobid
,materialpartNo
,ISNULL(SUM(case when defectDescription='Paint' then  rejectQty end ),0) as rejectQty
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Particle' then rejectQty end),0) as [Particle]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Fibre' then rejectQty end),0) as [Fibre]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Many particle' then rejectQty end),0) as [Many particle]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Stain mark' then rejectQty end),0) as [Stain mark]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Uneven paint' then rejectQty end),0) as [Uneven paint]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Under coat uneven paint' then rejectQty end),0) as [Under coat uneven paint]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Under spray' then rejectQty end),0) as [Under spray]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='White dot' then rejectQty end),0) as [White dot]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Silver dot' then rejectQty end),0) as [Silver dot]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Dust' then rejectQty end),0) as [Dust]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Paint crack' then rejectQty end),0) as [Paint crack]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Bubble' then rejectQty end),0) as [Bubble]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Scratch' then rejectQty end),0) as [Scratch]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Abrasion Mark' then rejectQty end),0) as [Abrasion Mark]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Paint Dripping' then rejectQty end),0) as [Paint Dripping]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Rough Surface' then rejectQty end),0) as [Rough Surface]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Shinning' then rejectQty end),0) as [Shinning]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Matt' then rejectQty end),0) as [Matt]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Paint Pin Hole' then rejectQty end),0) as [Paint Pin Hole]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Light Leakage' then rejectQty end),0) as [Light Leakage]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='White Mark' then rejectQty end),0) as [White Mark]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Dented' then rejectQty end),0) as [Dented]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Particle for laser setup' then rejectQty end),0) as [Particle for laser setup]
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Buyoff' then rejectQty end),0) as [Buyoff]
,'0' as Shortage
,'0' as QA
,ISNULL(SUM(case when defectDescription='Paint' and defectCode='Other' then rejectQty end),0) as [Other]
from PQCQaViDefectTracking 
where 1=1  ");

            if (sJobId != "") strSql.Append(" and jobId = @jobId ");
            if (sTrackingID != "") strSql.Append(" and trackingID = @trackingID");

            strSql.Append(" group by jobid, materialPartNo ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobId", SqlDbType.VarChar),
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            if (sJobId != "") parameters[0].Value = sJobId; else parameters[0] = null;
            if (sTrackingID != "") parameters[1].Value = sTrackingID; else parameters[1] = null;


            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        
        public DataTable GetLaserDefect(string sJobId, string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobid
,materialpartNo
,ISNULL(sum(case when defectDescription='Laser' then  rejectQty end ),0) as rejectQty
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Black Mark' then rejectQty end),0) as [Black Mark]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Black Dot' then rejectQty end),0) as [Black Dot]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Graphic Shift check by PQC' then rejectQty end),0) as [Graphic Shift check by PQC]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Graphic Shift check by M/C' then rejectQty end),0) as [Graphic Shift check by M/C]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Scratch' then rejectQty end),0) as [Scratch]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Jagged' then rejectQty end),0) as [Jagged]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Laser Bubble' then rejectQty end),0) as [Laser Bubble]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='double outer line' then rejectQty end),0) as [double outer line]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Pin hold' then rejectQty end),0) as [Pin hold]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Poor Laser' then rejectQty end),0) as [Poor Laser]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Burm Mark' then rejectQty end),0) as [Burm Mark]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Stain Mark' then rejectQty end),0) as [Stain Mark]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Graphic Small' then rejectQty end),0) as [Graphic Small]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Double Laser' then rejectQty end),0) as [Double Laser]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Color Yellow' then rejectQty end),0) as [Color Yellow]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Crack' then rejectQty end),0) as [Crack]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Smoke' then rejectQty end),0) as [Smoke]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Wrong Orientation' then rejectQty end),0) as [Wrong Orientation]
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Dented' then rejectQty end),0) as [Dented]
,'0' as Setup
,'0' as Buyoff
,ISNULL(SUM(case when defectDescription='Laser' and defectCode='Other' then rejectQty end),0) as [Other]
from PQCQaViDefectTracking 
where 1=1  ");

            if (sJobId != "") strSql.Append(" and jobId = @jobId ");
            if (sTrackingID != "") strSql.Append(" and trackingID = @trackingID");

            strSql.Append(" group by jobid, materialPartNo ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobId", SqlDbType.VarChar),
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            if (sJobId != "") parameters[0].Value = sJobId; else parameters[0] = null;
            if (sTrackingID != "") parameters[1].Value = sTrackingID; else parameters[1] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        
        public DataTable GetOthersDefect(string sJobId, string sTrackingID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobid
,materialpartNo
,ISNULL(sum(case when defectDescription='Others' then  rejectQty end ),0) as rejectQty
,ISNULL(SUM(case when defectDescription='Others' and defectCode='PQC Scratch' then rejectQty end),0) as [PQC Scratch]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Over Spray' then rejectQty end),0) as [Over Spray]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Bubble' then rejectQty end),0) as [Bubble]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Oil Stain' then rejectQty end),0) as [Oil Stain]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Drag Mark' then rejectQty end),0) as [Drag Mark]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Light Leakage' then rejectQty end),0) as [Light Leakage]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Light Bubble' then rejectQty end),0) as [Light Bubble]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='White Dot in Material' then rejectQty end),0) as [White Dot in Material]
,ISNULL(SUM(case when defectDescription='Others' and defectCode='Other' then rejectQty end),0) as [Other]
,'0' as QA
from PQCQaViDefectTracking 
where 1=1  ");

            if (sJobId != "") strSql.Append(" and jobId = @jobId ");
            if (sTrackingID != "") strSql.Append(" and trackingID = @trackingID");

            strSql.Append(" group by jobid, materialPartNo ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobId", SqlDbType.VarChar),
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            if (sJobId != "") parameters[0].Value = sJobId; else parameters[0] = null;
            if (sTrackingID != "") parameters[1].Value = sTrackingID; else parameters[1] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        #endregion


    }
}

