/**  版本信息模板在安装目录下，可自行修改。
* PQCPackHistory.cs
*
* 功 能： N/A
* 类 名： PQCPackHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/9 11:36:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
	/// <summary>
	/// 数据访问类:PQCPackHistory
	/// </summary>
	public partial class PQCPackHistory_DAL
	{
		public PQCPackHistory_DAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PQCPackHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCPackHistory(");
			strSql.Append("id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,TotalRejectQty,updatedTime,totalPassQty,shipTo,indexId)");
			strSql.Append(" values (");
			strSql.Append("@id,@machineID,@dateTime,@partNumber,@jobId,@processes,@jigNo,@model,@cavityCount,@cycleTime,@targetQty,@userName,@userID,@TotalQty,@rejectQty,@acceptQty,@startTime,@stopTime,@nextViFlag,@day,@shift,@status,@remark_1,@remark_2,@refField01,@refField02,@refField03,@refField04,@refField05,@refField06,@refField07,@refField08,@refField09,@refField10,@refField11,@refField12,@customer,@lastUpdatedTime,@trackingID,@lastTrackingID,@remarks,@department,@TotalRejectQty,@updatedTime,@totalPassQty,@shipTo,@indexId)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime,8),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobId", SqlDbType.VarChar,20),
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
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
					new SqlParameter("@day", SqlDbType.DateTime,8),
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
					new SqlParameter("@refField08", SqlDbType.VarChar,50),
					new SqlParameter("@refField09", SqlDbType.VarChar,50),
					new SqlParameter("@refField10", SqlDbType.VarChar,50),
					new SqlParameter("@refField11", SqlDbType.VarChar,50),
					new SqlParameter("@refField12", SqlDbType.VarChar,50),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,50),
					new SqlParameter("@department", SqlDbType.VarChar,20),
					new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
					new SqlParameter("@shipTo", SqlDbType.VarChar,50),
					new SqlParameter("@indexId", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.machineID;
			parameters[2].Value = model.dateTime;
			parameters[3].Value = model.partNumber;
			parameters[4].Value = model.jobId;
			parameters[5].Value = model.processes;
			parameters[6].Value = model.jigNo;
			parameters[7].Value = model.model;
			parameters[8].Value = model.cavityCount;
			parameters[9].Value = model.cycleTime;
			parameters[10].Value = model.targetQty;
			parameters[11].Value = model.userName;
			parameters[12].Value = model.userID;
			parameters[13].Value = model.TotalQty;
			parameters[14].Value = model.rejectQty;
			parameters[15].Value = model.acceptQty;
			parameters[16].Value = model.startTime;
			parameters[17].Value = model.stopTime;
			parameters[18].Value = model.nextViFlag;
			parameters[19].Value = model.day;
			parameters[20].Value = model.shift;
			parameters[21].Value = model.status;
			parameters[22].Value = model.remark_1;
			parameters[23].Value = model.remark_2;
			parameters[24].Value = model.refField01;
			parameters[25].Value = model.refField02;
			parameters[26].Value = model.refField03;
			parameters[27].Value = model.refField04;
			parameters[28].Value = model.refField05;
			parameters[29].Value = model.refField06;
			parameters[30].Value = model.refField07;
			parameters[31].Value = model.refField08;
			parameters[32].Value = model.refField09;
			parameters[33].Value = model.refField10;
			parameters[34].Value = model.refField11;
			parameters[35].Value = model.refField12;
			parameters[36].Value = model.customer;
			parameters[37].Value = model.lastUpdatedTime;
			parameters[38].Value = model.trackingID;
			parameters[39].Value = model.lastTrackingID;
			parameters[40].Value = model.remarks;
			parameters[41].Value = model.department;
			parameters[42].Value = model.TotalRejectQty;
			parameters[43].Value = model.updatedTime;
			parameters[44].Value = model.totalPassQty;
			parameters[45].Value = model.shipTo;
			parameters[46].Value = model.indexId;

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


        public SqlCommand AddCommand(Common.Class.Model.PQCPackTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCPackHistory(");
            strSql.Append("id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,TotalRejectQty,updatedTime,totalPassQty,shipTo,indexId)");
            strSql.Append(" values (");
            strSql.Append("@id,@machineID,@dateTime,@partNumber,@jobId,@processes,@jigNo,@model,@cavityCount,@cycleTime,@targetQty,@userName,@userID,@TotalQty,@rejectQty,@acceptQty,@startTime,@stopTime,@nextViFlag,@day,@shift,@status,@remark_1,@remark_2,@refField01,@refField02,@refField03,@refField04,@refField05,@refField06,@refField07,@refField08,@refField09,@refField10,@refField11,@refField12,@customer,@lastUpdatedTime,@trackingID,@lastTrackingID,@remarks,@department,@TotalRejectQty,@updatedTime,@totalPassQty,@shipTo,@indexId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime,8),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@jobId", SqlDbType.VarChar,20),
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
                    new SqlParameter("@startTime", SqlDbType.DateTime,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime,8),
                    new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
                    new SqlParameter("@day", SqlDbType.DateTime,8),
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
                    new SqlParameter("@refField08", SqlDbType.VarChar,50),
                    new SqlParameter("@refField09", SqlDbType.VarChar,50),
                    new SqlParameter("@refField10", SqlDbType.VarChar,50),
                    new SqlParameter("@refField11", SqlDbType.VarChar,50),
                    new SqlParameter("@refField12", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,50),
                    new SqlParameter("@department", SqlDbType.VarChar,20),
                    new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,50),
                    new SqlParameter("@indexId", SqlDbType.Int,4)};

          

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[2].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.jobId == null ? (object)DBNull.Value : model.jobId;
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
            parameters[24].Value = model.refField01== null ? (object)DBNull.Value : model.refField01;
            parameters[25].Value = model.refField02== null ? (object)DBNull.Value : model.refField02;
            parameters[26].Value = model.refField03== null ? (object)DBNull.Value : model.refField03;
            parameters[27].Value = model.refField04== null ? (object)DBNull.Value : model.refField04;
            parameters[28].Value = model.refField05== null ? (object)DBNull.Value : model.refField05;
            parameters[29].Value = model.refField06== null ? (object)DBNull.Value : model.refField06;
            parameters[30].Value = model.refField07== null ? (object)DBNull.Value : model.refField07;
            parameters[31].Value = model.refField08== null ? (object)DBNull.Value : model.refField08;
            parameters[32].Value = model.refField09== null ? (object)DBNull.Value : model.refField09;
            parameters[33].Value = model.refField10== null ? (object)DBNull.Value : model.refField10;
            parameters[34].Value = model.refField11 == null ? (object)DBNull.Value :model.refField11;
            parameters[35].Value = model.refField12 == null ? (object)DBNull.Value :model.refField12;
            parameters[36].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[37].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[38].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[39].Value = model.lastTrackingID == null ? (object)DBNull.Value : model.lastTrackingID;
            parameters[40].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[41].Value = model.department == null ? (object)DBNull.Value : model.department;
            parameters[42].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[43].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[44].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[45].Value = model.shipTo == null ? (object)DBNull.Value : model.shipTo;
            parameters[46].Value = model.indexId == null ? (object)DBNull.Value : model.indexId;

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
         
        }


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCPackHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCPackHistory set ");
			strSql.Append("id=@id,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("partNumber=@partNumber,");
			strSql.Append("jobId=@jobId,");
			strSql.Append("processes=@processes,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("model=@model,");
			strSql.Append("cavityCount=@cavityCount,");
			strSql.Append("cycleTime=@cycleTime,");
			strSql.Append("targetQty=@targetQty,");
			strSql.Append("userName=@userName,");
			strSql.Append("userID=@userID,");
			strSql.Append("TotalQty=@TotalQty,");
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
			strSql.Append("remarks=@remarks,");
			strSql.Append("department=@department,");
			strSql.Append("TotalRejectQty=@TotalRejectQty,");
			strSql.Append("updatedTime=@updatedTime,");
			strSql.Append("totalPassQty=@totalPassQty,");
			strSql.Append("shipTo=@shipTo,");
			strSql.Append("indexId=@indexId");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime,8),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobId", SqlDbType.VarChar,20),
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
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
					new SqlParameter("@day", SqlDbType.DateTime,8),
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
					new SqlParameter("@refField08", SqlDbType.VarChar,50),
					new SqlParameter("@refField09", SqlDbType.VarChar,50),
					new SqlParameter("@refField10", SqlDbType.VarChar,50),
					new SqlParameter("@refField11", SqlDbType.VarChar,50),
					new SqlParameter("@refField12", SqlDbType.VarChar,50),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@lastTrackingID", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,50),
					new SqlParameter("@department", SqlDbType.VarChar,20),
					new SqlParameter("@TotalRejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
					new SqlParameter("@shipTo", SqlDbType.VarChar,50),
					new SqlParameter("@indexId", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.machineID;
			parameters[2].Value = model.dateTime;
			parameters[3].Value = model.partNumber;
			parameters[4].Value = model.jobId;
			parameters[5].Value = model.processes;
			parameters[6].Value = model.jigNo;
			parameters[7].Value = model.model;
			parameters[8].Value = model.cavityCount;
			parameters[9].Value = model.cycleTime;
			parameters[10].Value = model.targetQty;
			parameters[11].Value = model.userName;
			parameters[12].Value = model.userID;
			parameters[13].Value = model.TotalQty;
			parameters[14].Value = model.rejectQty;
			parameters[15].Value = model.acceptQty;
			parameters[16].Value = model.startTime;
			parameters[17].Value = model.stopTime;
			parameters[18].Value = model.nextViFlag;
			parameters[19].Value = model.day;
			parameters[20].Value = model.shift;
			parameters[21].Value = model.status;
			parameters[22].Value = model.remark_1;
			parameters[23].Value = model.remark_2;
			parameters[24].Value = model.refField01;
			parameters[25].Value = model.refField02;
			parameters[26].Value = model.refField03;
			parameters[27].Value = model.refField04;
			parameters[28].Value = model.refField05;
			parameters[29].Value = model.refField06;
			parameters[30].Value = model.refField07;
			parameters[31].Value = model.refField08;
			parameters[32].Value = model.refField09;
			parameters[33].Value = model.refField10;
			parameters[34].Value = model.refField11;
			parameters[35].Value = model.refField12;
			parameters[36].Value = model.customer;
			parameters[37].Value = model.lastUpdatedTime;
			parameters[38].Value = model.trackingID;
			parameters[39].Value = model.lastTrackingID;
			parameters[40].Value = model.remarks;
			parameters[41].Value = model.department;
			parameters[42].Value = model.TotalRejectQty;
			parameters[43].Value = model.updatedTime;
			parameters[44].Value = model.totalPassQty;
			parameters[45].Value = model.shipTo;
			parameters[46].Value = model.indexId;

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
			strSql.Append("delete from PQCPackHistory ");
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



		public Common.Class.Model.PQCPackHistory_Model GetModel(string sTrackingID)
		{
            if (string.IsNullOrEmpty(sTrackingID))
                throw new Exception("Tracking ID can not be empty!");

            StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,TotalRejectQty,updatedTime,totalPassQty,shipTo,indexId from PQCPackHistory ");
			strSql.Append(" where 1=1 and trackingID = @trackingID");
            SqlParameter[] parameters = {
                new SqlParameter("@trackingID",SqlDbType.VarChar,64)
			};
            parameters[0].Value = sTrackingID;

            Common.Class.Model.PQCPackHistory_Model model =new Common.Class.Model.PQCPackHistory_Model();
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
		public Common.Class.Model.PQCPackHistory_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCPackHistory_Model model =new Common.Class.Model.PQCPackHistory_Model();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["machineID"]!=null)
				{
					model.machineID=row["machineID"].ToString();
				}
					//model.dateTime=row["dateTime"].ToString();
				if(row["partNumber"]!=null)
				{
					model.partNumber=row["partNumber"].ToString();
				}
				if(row["jobId"]!=null)
				{
					model.jobId=row["jobId"].ToString();
				}
				if(row["processes"]!=null)
				{
					model.processes=row["processes"].ToString();
				}
				if(row["jigNo"]!=null)
				{
					model.jigNo=row["jigNo"].ToString();
				}
				if(row["model"]!=null)
				{
					model.model=row["model"].ToString();
				}
				if(row["cavityCount"]!=null && row["cavityCount"].ToString()!="")
				{
					model.cavityCount=decimal.Parse(row["cavityCount"].ToString());
				}
				if(row["cycleTime"]!=null && row["cycleTime"].ToString()!="")
				{
					model.cycleTime=decimal.Parse(row["cycleTime"].ToString());
				}
				if(row["targetQty"]!=null && row["targetQty"].ToString()!="")
				{
					model.targetQty=decimal.Parse(row["targetQty"].ToString());
				}
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["userID"]!=null)
				{
					model.userID=row["userID"].ToString();
				}
				if(row["TotalQty"]!=null && row["TotalQty"].ToString()!="")
				{
					model.TotalQty=decimal.Parse(row["TotalQty"].ToString());
				}
				if(row["rejectQty"]!=null && row["rejectQty"].ToString()!="")
				{
					model.rejectQty=decimal.Parse(row["rejectQty"].ToString());
				}
				if(row["acceptQty"]!=null && row["acceptQty"].ToString()!="")
				{
					model.acceptQty=decimal.Parse(row["acceptQty"].ToString());
				}
					//model.startTime=row["startTime"].ToString();
					//model.stopTime=row["stopTime"].ToString();
				if(row["nextViFlag"]!=null)
				{
					model.nextViFlag=row["nextViFlag"].ToString();
				}
					//model.day=row["day"].ToString();
				if(row["shift"]!=null)
				{
					model.shift=row["shift"].ToString();
				}
				if(row["status"]!=null)
				{
					model.status=row["status"].ToString();
				}
				if(row["remark_1"]!=null)
				{
					model.remark_1=row["remark_1"].ToString();
				}
				if(row["remark_2"]!=null)
				{
					model.remark_2=row["remark_2"].ToString();
				}
				if(row["refField01"]!=null)
				{
					model.refField01=row["refField01"].ToString();
				}
				if(row["refField02"]!=null)
				{
					model.refField02=row["refField02"].ToString();
				}
				if(row["refField03"]!=null)
				{
					model.refField03=row["refField03"].ToString();
				}
				if(row["refField04"]!=null)
				{
					model.refField04=row["refField04"].ToString();
				}
				if(row["refField05"]!=null)
				{
					model.refField05=row["refField05"].ToString();
				}
				if(row["refField06"]!=null)
				{
					model.refField06=row["refField06"].ToString();
				}
				if(row["refField07"]!=null)
				{
					model.refField07=row["refField07"].ToString();
				}
				if(row["refField08"]!=null)
				{
					model.refField08=row["refField08"].ToString();
				}
				if(row["refField09"]!=null)
				{
					model.refField09=row["refField09"].ToString();
				}
				if(row["refField10"]!=null)
				{
					model.refField10=row["refField10"].ToString();
				}
				if(row["refField11"]!=null)
				{
					model.refField11=row["refField11"].ToString();
				}
				if(row["refField12"]!=null)
				{
					model.refField12=row["refField12"].ToString();
				}
				if(row["customer"]!=null)
				{
					model.customer=row["customer"].ToString();
				}
					//model.lastUpdatedTime=row["lastUpdatedTime"].ToString();
				if(row["trackingID"]!=null)
				{
					model.trackingID=row["trackingID"].ToString();
				}
				if(row["lastTrackingID"]!=null)
				{
					model.lastTrackingID=row["lastTrackingID"].ToString();
				}
				if(row["remarks"]!=null)
				{
					model.remarks=row["remarks"].ToString();
				}
				if(row["department"]!=null)
				{
					model.department=row["department"].ToString();
				}
				if(row["TotalRejectQty"]!=null && row["TotalRejectQty"].ToString()!="")
				{
					model.TotalRejectQty=decimal.Parse(row["TotalRejectQty"].ToString());
				}
					//model.updatedTime=row["updatedTime"].ToString();
				if(row["totalPassQty"]!=null && row["totalPassQty"].ToString()!="")
				{
					model.totalPassQty=decimal.Parse(row["totalPassQty"].ToString());
				}
				if(row["shipTo"]!=null)
				{
					model.shipTo=row["shipTo"].ToString();
				}
				if(row["indexId"]!=null && row["indexId"].ToString()!="")
				{
					model.indexId=int.Parse(row["indexId"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,TotalRejectQty,updatedTime,totalPassQty,shipTo,indexId ");
			strSql.Append(" FROM PQCPackHistory ");
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
			strSql.Append(" id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,TotalRejectQty,updatedTime,totalPassQty,shipTo,indexId ");
			strSql.Append(" FROM PQCPackHistory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		
		/// <summary>
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
			strSql.Append(")AS Row, T.*  from PQCPackHistory T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}
        

		#endregion  BasicMethod


	}
}

