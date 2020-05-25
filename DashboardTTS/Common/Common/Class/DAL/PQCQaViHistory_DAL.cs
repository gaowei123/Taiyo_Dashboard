 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:PQCQaViHistory_DAL
	/// </summary>
	public class PQCQaViHistory_DAL
	{
		public PQCQaViHistory_DAL()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public DataSet Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PQCQaViHistory");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			return DBHelp.SqlDB.Query(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Common.Class.Model.PQCQaViHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCQaViHistory(");
			strSql.Append("machineID,dateTime,partNumber,jobid,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,totalPassQty,TotalRejectQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,updatedTime)");
			strSql.Append(" values (");
			strSql.Append("@machineID,@dateTime,@partNumber,@jobid,@processes,@jigNo,@model,@cavityCount,@cycleTime,@targetQty,@userName,@userID,@TotalQty,@totalPassQty,@TotalRejectQty,@rejectQty,@acceptQty,@startTime,@stopTime,@nextViFlag,@day,@shift,@status,@remark_1,@remark_2,@refField01,@refField02,@refField03,@refField04,@refField05,@refField06,@refField07,@refField08,@refField09,@refField10,@refField11,@refField12,@customer,@lastUpdatedTime,@trackingID,@lastTrackingID,@remarks,@department,@updatedTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobid", SqlDbType.VarChar,20),
					new SqlParameter("@processes", SqlDbType.VarChar,100),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
					new SqlParameter("@targetQty", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@TotalQty", SqlDbType.Decimal,9),
					new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
					new SqlParameter("@startTime", SqlDbType.DateTime2,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,10),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
					new SqlParameter("@refField01", SqlDbType.VarChar,50),
					new SqlParameter("@refField02", SqlDbType.VarChar,50),
					new SqlParameter("@refField03", SqlDbType.VarChar,50),
					new SqlParameter("@refField04", SqlDbType.VarChar,50),
					new SqlParameter("@refField05", SqlDbType.VarChar,50),
					new SqlParameter("@refField06", SqlDbType.VarChar,50),
					new SqlParameter("@refField07", SqlDbType.VarChar,50),
					new SqlParameter("@refField08", SqlDbType.VarChar,100),
					new SqlParameter("@refField09", SqlDbType.VarChar,100),
					new SqlParameter("@refField10", SqlDbType.VarChar,200),
					new SqlParameter("@refField11", SqlDbType.VarChar,200),
					new SqlParameter("@refField12", SqlDbType.VarChar,500),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@department", SqlDbType.VarChar,20),
                    new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViHistory_DAL" , "Function:		public int Add(Common.Class.Model.PQCQaViHistory_Model model)"  + 
                 "TableName:PQCQaViHistory" , 
                 ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + 
                 ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + 
                 ";partNumber = "+ (model.partNumber == null ? "" : model.partNumber.ToString() ) + 
                 ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString() ) + 
                 ";processes = " + (model.processes == null ? "" : model.processes.ToString() ) + 
                 ";jigNo = "+ (model.jigNo == null ? "" : model.jigNo.ToString() ) + 
                 ";model = "+ (model.model == null ? "" : model.model.ToString() ) + 
                 ";cavityCount = "+ (model.cavityCount == null ? "" : model.cavityCount.ToString() ) + 
                 ";cycleTime = "+ (model.cycleTime == null ? "" : model.cycleTime.ToString() ) + 
                 ";targetQty = "+ (model.targetQty == null ? "" : model.targetQty.ToString()) + 
                 ";TotalRejectQty = " + (model.TotalRejectQty == null ? "" : model.TotalRejectQty.ToString()) + 
                 ";userName = " + (model.userName == null ? "" : model.userName.ToString() ) + 
                 ";userID = "+ (model.userID == null ? "" : model.userID.ToString() ) + 
                 ";TotalQty = "+ (model.TotalQty == null ? "" : model.TotalQty.ToString() ) + 
                 ";rejectQty = "+ (model.rejectQty == null ? "" : model.rejectQty.ToString() ) + 
                 ";acceptQty = "+ (model.acceptQty == null ? "" : model.acceptQty.ToString() ) + 
                 ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString() ) + 
                 ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString() ) + 
                 ";nextViFlag = "+ (model.nextViFlag == null ? "" : model.nextViFlag.ToString() ) + 
                 ";day = "+ (model.day == null ? "" : model.day.ToString() ) + 
                 ";shift = "+ (model.shift == null ? "" : model.shift.ToString() ) + 
                 ";status = "+ (model.status == null ? "" : model.status.ToString() ) + 
                 ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString() ) + 
                 ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString() ) + 
                 ";refField01 = "+ (model.refField01 == null ? "" : model.refField01.ToString() ) + 
                 ";refField02 = "+ (model.refField02 == null ? "" : model.refField02.ToString() ) + 
                 ";refField03 = "+ (model.refField03 == null ? "" : model.refField03.ToString() ) + 
                 ";refField04 = "+ (model.refField04 == null ? "" : model.refField04.ToString() ) + 
                 ";refField05 = "+ (model.refField05 == null ? "" : model.refField05.ToString() ) + 
                 ";refField06 = "+ (model.refField06 == null ? "" : model.refField06.ToString() ) + 
                 ";refField07 = "+ (model.refField07 == null ? "" : model.refField07.ToString() ) + 
                 ";refField08 = "+ (model.refField08 == null ? "" : model.refField08.ToString() ) + 
                 ";refField09 = "+ (model.refField09 == null ? "" : model.refField09.ToString() ) + 
                 ";refField10 = "+ (model.refField10 == null ? "" : model.refField10.ToString() ) + 
                 ";refField11 = "+ (model.refField11 == null ? "" : model.refField11.ToString() ) + 
                 ";refField12 = "+ (model.refField12 == null ? "" : model.refField12.ToString() ) + 
                 ";customer = "+ (model.customer == null ? "" : model.customer.ToString() ) + 
                 ";lastUpdatedTime = "+ (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString() ) + 
                 ";trackingID = "+ (model.trackingID == null ? "" : model.trackingID.ToString() ) + 
                 ";lastTrackingID = "+ (model.lastTrackingID == null ? "" : model.lastTrackingID.ToString() ) + 
                 ";remarks = "+ (model.remarks == null ? "" : model.remarks.ToString() ) + "");
			parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[3].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
			parameters[4].Value = model.processes == null ? (object)DBNull.Value : model.processes;
			parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount ;
			parameters[8].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime ;
			parameters[9].Value = model.targetQty == null ? (object)DBNull.Value : model.targetQty ;
			parameters[10].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[11].Value = model.userID == null ? (object)DBNull.Value : model.userID ;
			parameters[12].Value = model.TotalQty == null ? (object)DBNull.Value : model.TotalQty ;
			parameters[13].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty ;
			parameters[14].Value = model.acceptQty == null ? (object)DBNull.Value : model.acceptQty ;
			parameters[15].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[16].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[17].Value = model.nextViFlag == null ? (object)DBNull.Value : model.nextViFlag ;
			parameters[18].Value = model.day == null ? (object)DBNull.Value : model.day ;
			parameters[19].Value = model.shift == null ? (object)DBNull.Value : model.shift ;
			parameters[20].Value = model.status == null ? (object)DBNull.Value : model.status ;
			parameters[21].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
			parameters[22].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
			parameters[23].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01 ;
			parameters[24].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02 ;
			parameters[25].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03 ;
			parameters[26].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04 ;
			parameters[27].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05 ;
			parameters[28].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06 ;
			parameters[29].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07 ;
			parameters[30].Value = model.refField08 == null ? (object)DBNull.Value : model.refField08 ;
			parameters[31].Value = model.refField09 == null ? (object)DBNull.Value : model.refField09 ;
			parameters[32].Value = model.refField10 == null ? (object)DBNull.Value : model.refField10 ;
			parameters[33].Value = model.refField11 == null ? (object)DBNull.Value : model.refField11 ;
			parameters[34].Value = model.refField12 == null ? (object)DBNull.Value : model.refField12 ;
			parameters[35].Value = model.customer == null ? (object)DBNull.Value : model.customer ;
			parameters[36].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime ;
			parameters[37].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID ;
			parameters[38].Value = model.lastTrackingID == null ? (object)DBNull.Value : model.lastTrackingID ;
			parameters[39].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[40].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[41].Value = model.TotalRejectQty == null ? (object)DBNull.Value : model.TotalRejectQty;
            parameters[42].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[43].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;

            int obj = DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
            if (obj < 1)
            {
                return 1;
            }
            else
            {
                return obj;
            }
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Class.Model.PQCQaViHistory_Model model,SqlCommand cmd=null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCQaViHistory(");
            strSql.Append("machineID,dateTime,partNumber,jobid,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,totalPassQty,TotalRejectQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@machineID,@dateTime,@partNumber,@jobid,@processes,@jigNo,@model,@cavityCount,@cycleTime,@targetQty,@userName,@userID,@TotalQty,@totalPassQty,@TotalRejectQty,@rejectQty,@acceptQty,@startTime,@stopTime,@nextViFlag,@day,@shift,@status,@remark_1,@remark_2,@refField01,@refField02,@refField03,@refField04,@refField05,@refField06,@refField07,@refField08,@refField09,@refField10,@refField11,@refField12,@customer,@lastUpdatedTime,@trackingID,@lastTrackingID,@remarks,@department,@updatedTime)");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@processes", SqlDbType.VarChar,100),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@targetQty", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@TotalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@nextViFlag", SqlDbType.VarChar,10),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),
                    new SqlParameter("@refField06", SqlDbType.VarChar,50),
                    new SqlParameter("@refField07", SqlDbType.VarChar,50),
                    new SqlParameter("@refField08", SqlDbType.VarChar,100),
                    new SqlParameter("@refField09", SqlDbType.VarChar,100),
                    new SqlParameter("@refField10", SqlDbType.VarChar,200),
                    new SqlParameter("@refField11", SqlDbType.VarChar,200),
                    new SqlParameter("@refField12", SqlDbType.VarChar,500),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@department", SqlDbType.VarChar,20),
                    new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9)};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViHistory_DAL", "Function:		public SqlCommand AddCommand(Common.Class.Model.PQCQaViHistory_Model model)" +
                "TableName:PQCQaViHistory",
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) +
                ";targetQty = " + (model.targetQty == null ? "" : model.targetQty.ToString()) +
                ";TotalRejectQty = " + (model.TotalRejectQty == null ? "" : model.TotalRejectQty.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";TotalQty = " + (model.TotalQty == null ? "" : model.TotalQty.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";acceptQty = " + (model.acceptQty == null ? "" : model.acceptQty.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";nextViFlag = " + (model.nextViFlag == null ? "" : model.nextViFlag.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";refField01 = " + (model.refField01 == null ? "" : model.refField01.ToString()) +
                ";refField02 = " + (model.refField02 == null ? "" : model.refField02.ToString()) +
                ";refField03 = " + (model.refField03 == null ? "" : model.refField03.ToString()) +
                ";refField04 = " + (model.refField04 == null ? "" : model.refField04.ToString()) +
                ";refField05 = " + (model.refField05 == null ? "" : model.refField05.ToString()) +
                ";refField06 = " + (model.refField06 == null ? "" : model.refField06.ToString()) +
                ";refField07 = " + (model.refField07 == null ? "" : model.refField07.ToString()) +
                ";refField08 = " + (model.refField08 == null ? "" : model.refField08.ToString()) +
                ";refField09 = " + (model.refField09 == null ? "" : model.refField09.ToString()) +
                ";refField10 = " + (model.refField10 == null ? "" : model.refField10.ToString()) +
                ";refField11 = " + (model.refField11 == null ? "" : model.refField11.ToString()) +
                ";refField12 = " + (model.refField12 == null ? "" : model.refField12.ToString()) +
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";lastTrackingID = " + (model.lastTrackingID == null ? "" : model.lastTrackingID.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");

            parameters[0].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[2].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[3].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[4].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[9].Value = model.targetQty == null ? (object)DBNull.Value : model.targetQty;
            parameters[10].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[11].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[12].Value = model.TotalQty == null ? (object)DBNull.Value : model.TotalQty;
            parameters[13].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[14].Value = model.acceptQty == null ? (object)DBNull.Value : model.acceptQty;
            parameters[15].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;

            parameters[16].Value = model.stopTime == null  ? (object)DBNull.Value : model.stopTime;
            parameters[17].Value = model.nextViFlag == null ? (object)DBNull.Value : model.nextViFlag;
            parameters[18].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[19].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[20].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[21].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[22].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[23].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[24].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            parameters[25].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            parameters[26].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            parameters[27].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            parameters[28].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06;
            parameters[29].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07;
            parameters[30].Value = model.refField08 == null ? (object)DBNull.Value : model.refField08;
            parameters[31].Value = model.refField09 == null ? (object)DBNull.Value : model.refField09;
            parameters[32].Value = model.refField10 == null ? (object)DBNull.Value : model.refField10;
            parameters[33].Value = model.refField11 == null ? (object)DBNull.Value : model.refField11;
            parameters[34].Value = model.refField12 == null ? (object)DBNull.Value : model.refField12;
            parameters[35].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[36].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[37].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[38].Value = model.lastTrackingID == null ? (object)DBNull.Value : model.lastTrackingID;
            parameters[39].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[40].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[41].Value = model.TotalRejectQty == null ? (object)DBNull.Value : model.TotalRejectQty;
            parameters[42].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[43].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;


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
            

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCQaViHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCQaViHistory set ");
			strSql.Append("machineID=@machineID,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("partNumber=@partNumber,");
			strSql.Append("jobid=@jobid,");
			strSql.Append("processes=@processes,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("model=@model,");
			strSql.Append("cavityCount=@cavityCount,");
			strSql.Append("cycleTime=@cycleTime,");
			strSql.Append("targetQty=@targetQty,");
			strSql.Append("userName=@userName,");
			strSql.Append("userID=@userID,");
			strSql.Append("TotalQty=@TotalQty,");
            strSql.Append("totalPassQty=@totalPassQty,");
            strSql.Append("rejectQty=@rejectQty,");
			strSql.Append("acceptQty=@acceptQty,");
			strSql.Append("startTime=@startTime,");
			strSql.Append("stopTime=@stopTime,");
			strSql.Append("nextViFlag=@nextViFlag,");
			strSql.Append("day=@day,");
			strSql.Append("shift=@shift,");
			strSql.Append("status=@status,");
			strSql.Append("remark_1=@remark_1,");
			strSql.Append("remark_2=@remark_2,");
			strSql.Append("refField01=@refField01,");
			strSql.Append("refField02=@refField02,");
			strSql.Append("refField03=@refField03,");
			strSql.Append("refField04=@refField04,");
			strSql.Append("refField05=@refField05,");
			strSql.Append("refField06=@refField06,");
			strSql.Append("refField07=@refField07,");
			strSql.Append("refField08=@refField08,");
			strSql.Append("refField09=@refField09,");
			strSql.Append("refField10=@refField10,");
			strSql.Append("refField11=@refField11,");
			strSql.Append("refField12=@refField12,");
			strSql.Append("customer=@customer,");
			strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
			strSql.Append("trackingID=@trackingID,");
			strSql.Append("lastTrackingID=@lastTrackingID,");
			strSql.Append("remarks=@remarks, ");
            strSql.Append("department=@department, ");
            strSql.Append("TotalRejectQty=@TotalRejectQty");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where id=@id");

            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobid", SqlDbType.DateTime2,8),
					new SqlParameter("@processes", SqlDbType.VarChar,100),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
					new SqlParameter("@targetQty", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@TotalQty", SqlDbType.Decimal,9),
					new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
					new SqlParameter("@startTime", SqlDbType.DateTime2,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,10),
					new SqlParameter("@day", SqlDbType.DateTime2,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
					new SqlParameter("@refField01", SqlDbType.VarChar,50),
					new SqlParameter("@refField02", SqlDbType.VarChar,50),
					new SqlParameter("@refField03", SqlDbType.VarChar,50),
					new SqlParameter("@refField04", SqlDbType.VarChar,50),
					new SqlParameter("@refField05", SqlDbType.VarChar,50),
					new SqlParameter("@refField06", SqlDbType.VarChar,50),
					new SqlParameter("@refField07", SqlDbType.VarChar,50),
					new SqlParameter("@refField08", SqlDbType.VarChar,100),
					new SqlParameter("@refField09", SqlDbType.VarChar,100),
					new SqlParameter("@refField10", SqlDbType.VarChar,200),
					new SqlParameter("@refField11", SqlDbType.VarChar,200),
					new SqlParameter("@refField12", SqlDbType.VarChar,500),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@department", SqlDbType.VarChar,20),
                    new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9)};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", 
                "Class:PQCQaViHistory_DAL", 
                "Function:		public bool Update(Common.Class.Model.PQCQaViHistory_Model model)" + 
                "TableName:PQCQaViHistory", 
                ";id = " + (model.id == null ? "" : model.id.ToString()) + 
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) + 
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) + 
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) + 
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) + 
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) + 
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) + 
                ";model = " + (model.model == null ? "" : model.model.ToString()) + 
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) + 
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) + 
                ";targetQty = " + (model.targetQty == null ? "" : model.targetQty.ToString()) + 
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) + 
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) + 
                ";TotalQty = " + (model.TotalQty == null ? "" : model.TotalQty.ToString()) + 
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) + 
                ";acceptQty = " + (model.acceptQty == null ? "" : model.acceptQty.ToString()) + 
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) + 
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) + 
                ";nextViFlag = " + (model.nextViFlag == null ? "" : model.nextViFlag.ToString()) + 
                ";day = " + (model.day == null ? "" : model.day.ToString()) + 
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) + 
                ";status = " + (model.status == null ? "" : model.status.ToString()) + 
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) + 
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) + 
                ";refField01 = " + (model.refField01 == null ? "" : model.refField01.ToString()) + 
                ";refField02 = " + (model.refField02 == null ? "" : model.refField02.ToString()) + 
                ";refField03 = " + (model.refField03 == null ? "" : model.refField03.ToString()) + 
                ";refField04 = " + (model.refField04 == null ? "" : model.refField04.ToString()) + 
                ";refField05 = " + (model.refField05 == null ? "" : model.refField05.ToString()) + 
                ";refField06 = " + (model.refField06 == null ? "" : model.refField06.ToString()) + 
                ";refField07 = " + (model.refField07 == null ? "" : model.refField07.ToString()) + 
                ";refField08 = " + (model.refField08 == null ? "" : model.refField08.ToString()) + 
                ";refField09 = " + (model.refField09 == null ? "" : model.refField09.ToString()) + 
                ";refField10 = " + (model.refField10 == null ? "" : model.refField10.ToString()) + 
                ";refField11 = " + (model.refField11 == null ? "" : model.refField11.ToString()) + 
                ";refField12 = " + (model.refField12 == null ? "" : model.refField12.ToString()) + 
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) + 
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) + 
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) + 
                ";lastTrackingID = " + (model.lastTrackingID == null ? "" : model.lastTrackingID.ToString()) + 
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[2].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[4].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
			parameters[5].Value = model.processes == null ? (object)DBNull.Value : model.processes;
			parameters[6].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[7].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[8].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount ;
			parameters[9].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime ;
			parameters[10].Value = model.targetQty == null ? (object)DBNull.Value : model.targetQty ;
			parameters[11].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[12].Value = model.userID == null ? (object)DBNull.Value : model.userID ;
			parameters[13].Value = model.TotalQty == null ? (object)DBNull.Value : model.TotalQty ;
			parameters[14].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty ;
			parameters[15].Value = model.acceptQty == null ? (object)DBNull.Value : model.acceptQty ;
			parameters[16].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[17].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[18].Value = model.nextViFlag == null ? (object)DBNull.Value : model.nextViFlag ;
			parameters[19].Value = model.day == null ? (object)DBNull.Value : model.day ;
			parameters[20].Value = model.shift == null ? (object)DBNull.Value : model.shift ;
			parameters[21].Value = model.status == null ? (object)DBNull.Value : model.status ;
			parameters[22].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
			parameters[23].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
			parameters[24].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01 ;
			parameters[25].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02 ;
			parameters[26].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03 ;
			parameters[27].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04 ;
			parameters[28].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05 ;
			parameters[29].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06 ;
			parameters[30].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07 ;
			parameters[31].Value = model.refField08 == null ? (object)DBNull.Value : model.refField08 ;
			parameters[32].Value = model.refField09 == null ? (object)DBNull.Value : model.refField09 ;
			parameters[33].Value = model.refField10 == null ? (object)DBNull.Value : model.refField10 ;
			parameters[34].Value = model.refField11 == null ? (object)DBNull.Value : model.refField11 ;
			parameters[35].Value = model.refField12 == null ? (object)DBNull.Value : model.refField12 ;
			parameters[36].Value = model.customer == null ? (object)DBNull.Value : model.customer ;
			parameters[37].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime ;
			parameters[38].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID ;
			parameters[39].Value = model.lastTrackingID == null ? (object)DBNull.Value : model.lastTrackingID ;
            parameters[40].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[41].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[42].Value = model.TotalRejectQty == null ? (object)DBNull.Value : model.TotalRejectQty;
            parameters[43].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[44].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;

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
		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViHistory set ");
            strSql.Append("machineID=@machineID,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("partNumber=@partNumber,");
            strSql.Append("jobid=@jobid,");
            strSql.Append("processes=@processes,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("model=@model,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("cycleTime=@cycleTime,");
            strSql.Append("targetQty=@targetQty,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("TotalQty=@TotalQty,");
            strSql.Append("totalPassQty=@totalPassQty,");
            strSql.Append("rejectQty=@rejectQty,");
            strSql.Append("acceptQty=@acceptQty,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("stopTime=@stopTime,");
            strSql.Append("nextViFlag=@nextViFlag,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("status=@status,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            strSql.Append("refField01=@refField01,");
            strSql.Append("refField02=@refField02,");
            strSql.Append("refField03=@refField03,");
            strSql.Append("refField04=@refField04,");
            strSql.Append("refField05=@refField05,");
            strSql.Append("refField06=@refField06,");
            strSql.Append("refField07=@refField07,");
            strSql.Append("refField08=@refField08,");
            strSql.Append("refField09=@refField09,");
            strSql.Append("refField10=@refField10,");
            strSql.Append("refField11=@refField11,");
            strSql.Append("refField12=@refField12,");
            strSql.Append("customer=@customer,");
            strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append("trackingID=@trackingID,");
            strSql.Append("lastTrackingID=@lastTrackingID,");
            strSql.Append("remarks=@remarks, ");
            strSql.Append("department=@department, ");
            strSql.Append("TotalRejectQty=@TotalRejectQty");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where id=@id");

            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@jobid", SqlDbType.DateTime2,8),
                    new SqlParameter("@processes", SqlDbType.VarChar,100),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@targetQty", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@TotalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@nextViFlag", SqlDbType.VarChar,10),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),
                    new SqlParameter("@refField06", SqlDbType.VarChar,50),
                    new SqlParameter("@refField07", SqlDbType.VarChar,50),
                    new SqlParameter("@refField08", SqlDbType.VarChar,100),
                    new SqlParameter("@refField09", SqlDbType.VarChar,100),
                    new SqlParameter("@refField10", SqlDbType.VarChar,200),
                    new SqlParameter("@refField11", SqlDbType.VarChar,200),
                    new SqlParameter("@refField12", SqlDbType.VarChar,500),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@department", SqlDbType.VarChar,20),
                    new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9)};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViHistory_DAL",
                "Function:		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViHistory_Model model)" +
                "TableName:PQCQaViHistory",
                ";id = " + (model.id == null ? "" : model.id.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) +
                ";targetQty = " + (model.targetQty == null ? "" : model.targetQty.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";TotalQty = " + (model.TotalQty == null ? "" : model.TotalQty.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";acceptQty = " + (model.acceptQty == null ? "" : model.acceptQty.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";nextViFlag = " + (model.nextViFlag == null ? "" : model.nextViFlag.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";refField01 = " + (model.refField01 == null ? "" : model.refField01.ToString()) +
                ";refField02 = " + (model.refField02 == null ? "" : model.refField02.ToString()) +
                ";refField03 = " + (model.refField03 == null ? "" : model.refField03.ToString()) +
                ";refField04 = " + (model.refField04 == null ? "" : model.refField04.ToString()) +
                ";refField05 = " + (model.refField05 == null ? "" : model.refField05.ToString()) +
                ";refField06 = " + (model.refField06 == null ? "" : model.refField06.ToString()) +
                ";refField07 = " + (model.refField07 == null ? "" : model.refField07.ToString()) +
                ";refField08 = " + (model.refField08 == null ? "" : model.refField08.ToString()) +
                ";refField09 = " + (model.refField09 == null ? "" : model.refField09.ToString()) +
                ";refField10 = " + (model.refField10 == null ? "" : model.refField10.ToString()) +
                ";refField11 = " + (model.refField11 == null ? "" : model.refField11.ToString()) +
                ";refField12 = " + (model.refField12 == null ? "" : model.refField12.ToString()) +
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";lastTrackingID = " + (model.lastTrackingID == null ? "" : model.lastTrackingID.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[2].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[5].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[6].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[7].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[8].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[9].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[10].Value = model.targetQty == null ? (object)DBNull.Value : model.targetQty;
            parameters[11].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[12].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[13].Value = model.TotalQty == null ? (object)DBNull.Value : model.TotalQty;
            parameters[14].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[15].Value = model.acceptQty == null ? (object)DBNull.Value : model.acceptQty;
            parameters[16].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[17].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[18].Value = model.nextViFlag == null ? (object)DBNull.Value : model.nextViFlag;
            parameters[19].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[20].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[21].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[22].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[23].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[24].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[25].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            parameters[26].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            parameters[27].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            parameters[28].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            parameters[29].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06;
            parameters[30].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07;
            parameters[31].Value = model.refField08 == null ? (object)DBNull.Value : model.refField08;
            parameters[32].Value = model.refField09 == null ? (object)DBNull.Value : model.refField09;
            parameters[33].Value = model.refField10 == null ? (object)DBNull.Value : model.refField10;
            parameters[34].Value = model.refField11 == null ? (object)DBNull.Value : model.refField11;
            parameters[35].Value = model.refField12 == null ? (object)DBNull.Value : model.refField12;
            parameters[36].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[37].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[38].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[39].Value = model.lastTrackingID == null ? (object)DBNull.Value : model.lastTrackingID;
            parameters[40].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[41].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[42].Value = model.TotalRejectQty == null ? (object)DBNull.Value : model.TotalRejectQty;
            parameters[43].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[44].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViHistory ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViHistory_DAL" , 
                 "Function:		public bool Delete(int id)"  + 
                 "TableName:PQCQaViHistory" , "");
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViHistory ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString());
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
		public SqlCommand DeleteCommand(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViHistory ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViHistory_DAL" , 
                 "Function:		public SqlCommand DeleteCommand(int id)"  + 
                 "TableName:PQCQaViHistory" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCQaViHistory ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViHistory_DAL" , 
                 "Function:		public SqlCommand DeleteAllCommand( )"  + 
                 "TableName:PQCQaViHistory" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCQaViHistory_Model GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,machineID,dateTime,partNumber,jobid,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,totalPassQty,TotalRejectQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,updatedTime from PQCQaViHistory ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                 "Class:PQCQaViHistory_DAL" , 
                 "Function:		public Common.Class.Model.PQCQaViHistory_Model GetModel(int id)"  + 
                 "TableName:PQCQaViHistory" , "");
			Common.Class.Model.PQCQaViHistory_Model model=new Common.Class.Model.PQCQaViHistory_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.machineID=ds.Tables[0].Rows[0]["machineID"].ToString();
				if(ds.Tables[0].Rows[0]["dateTime"].ToString()!="")
				{
					model.dateTime=DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
				}
				model.partNumber=ds.Tables[0].Rows[0]["partNumber"].ToString();
                model.jobid = ds.Tables[0].Rows[0]["jobid"].ToString();
				model.processes=ds.Tables[0].Rows[0]["processes"].ToString();
				model.jigNo=ds.Tables[0].Rows[0]["jigNo"].ToString();
				model.model=ds.Tables[0].Rows[0]["model"].ToString();
				if(ds.Tables[0].Rows[0]["cavityCount"].ToString()!="")
				{
					model.cavityCount=decimal.Parse(ds.Tables[0].Rows[0]["cavityCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["cycleTime"].ToString()!="")
				{
					model.cycleTime=decimal.Parse(ds.Tables[0].Rows[0]["cycleTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["targetQty"].ToString()!="")
				{
					model.targetQty=decimal.Parse(ds.Tables[0].Rows[0]["targetQty"].ToString());
				}
				model.userName=ds.Tables[0].Rows[0]["userName"].ToString();
				model.userID=ds.Tables[0].Rows[0]["userID"].ToString();
				if(ds.Tables[0].Rows[0]["TotalQty"].ToString()!="")
				{
					model.TotalQty=decimal.Parse(ds.Tables[0].Rows[0]["TotalQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalPassQty"].ToString() != "")
                {
                    model.totalPassQty = decimal.Parse(ds.Tables[0].Rows[0]["totalPassQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalRejectQty"].ToString() != "")
                {
                    model.TotalRejectQty = decimal.Parse(ds.Tables[0].Rows[0]["TotalRejectQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rejectQty"].ToString()!="")
				{
					model.rejectQty=decimal.Parse(ds.Tables[0].Rows[0]["rejectQty"].ToString());
				}
				if(ds.Tables[0].Rows[0]["acceptQty"].ToString()!="")
				{
					model.acceptQty=decimal.Parse(ds.Tables[0].Rows[0]["acceptQty"].ToString());
				}
				if(ds.Tables[0].Rows[0]["startTime"].ToString()!="")
				{
					model.startTime=DateTime.Parse(ds.Tables[0].Rows[0]["startTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["stopTime"].ToString()!="")
				{
					model.stopTime=DateTime.Parse(ds.Tables[0].Rows[0]["stopTime"].ToString());
				}
				model.nextViFlag=ds.Tables[0].Rows[0]["nextViFlag"].ToString();
				if(ds.Tables[0].Rows[0]["day"].ToString()!="")
				{
					model.day=DateTime.Parse(ds.Tables[0].Rows[0]["day"].ToString());
				}
				model.shift=ds.Tables[0].Rows[0]["shift"].ToString();
				model.status=ds.Tables[0].Rows[0]["status"].ToString();
                model.remark_1 = ds.Tables[0].Rows[0]["remark_1"].ToString();
				model.remark_2 = ds.Tables[0].Rows[0]["remark_2"].ToString();
				model.refField01=ds.Tables[0].Rows[0]["refField01"].ToString();
				model.refField02=ds.Tables[0].Rows[0]["refField02"].ToString();
				model.refField03=ds.Tables[0].Rows[0]["refField03"].ToString();
				model.refField04=ds.Tables[0].Rows[0]["refField04"].ToString();
				model.refField05=ds.Tables[0].Rows[0]["refField05"].ToString();
				model.refField06=ds.Tables[0].Rows[0]["refField06"].ToString();
				model.refField07=ds.Tables[0].Rows[0]["refField07"].ToString();
				model.refField08=ds.Tables[0].Rows[0]["refField08"].ToString();
				model.refField09=ds.Tables[0].Rows[0]["refField09"].ToString();
				model.refField10=ds.Tables[0].Rows[0]["refField10"].ToString();
				model.refField11=ds.Tables[0].Rows[0]["refField11"].ToString();
				model.refField12=ds.Tables[0].Rows[0]["refField12"].ToString();
				model.customer=ds.Tables[0].Rows[0]["customer"].ToString();
				if(ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString()!="")
				{
					model.lastUpdatedTime=DateTime.Parse(ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString());
				}
				model.trackingID=ds.Tables[0].Rows[0]["trackingID"].ToString();
				model.lastTrackingID=ds.Tables[0].Rows[0]["lastTrackingID"].ToString();
				model.remarks=ds.Tables[0].Rows[0]["remarks"].ToString();
                model.department = ds.Tables[0].Rows[0]["department"].ToString();
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
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select id,machineID,dateTime,partNumber,jobid,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,totalPassQty,TotalRejectQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,
                                remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,updatedTime ");
			strSql.Append(" FROM PQCQaViHistory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DBHelp.SqlDB.Query(strSql.ToString());
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
			strSql.Append(@" id,machineID,dateTime,partNumber,jobid,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,totalPassQty,TotalRejectQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,
                            remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,updatedTime ");
			strSql.Append(" FROM PQCQaViHistory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "PQCQaViHistory";
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

