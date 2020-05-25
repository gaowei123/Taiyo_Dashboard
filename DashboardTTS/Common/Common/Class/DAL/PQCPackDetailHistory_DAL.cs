/**  版本信息模板在安装目录下，可自行修改。
* PQCPackDetailHistory.cs
*
* 功 能： N/A
* 类 名： PQCPackDetailHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/3/9 11:36:05   N/A    初版
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
	/// 数据访问类:PQCPackDetailHistory
	/// </summary>
	public partial class PQCPackDetailHistory_DAL
	{
		public PQCPackDetailHistory_DAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.PQCPackDetailHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCPackDetailHistory(");
			strSql.Append("id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId)");
			strSql.Append(" values (");
			strSql.Append("@id,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@jobId,@totalQty,@updatedTime,@passQty,@totalPassQty,@totalRejectQty,@color,@materialName,@outerBoxQty,@packingTrays,@customer,@shipTo,@module,@sn,@indexId)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime,8),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@day", SqlDbType.DateTime,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
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
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@remarks", SqlDbType.VarChar,50),
					new SqlParameter("@processes", SqlDbType.VarChar,20),
					new SqlParameter("@jobId", SqlDbType.VarChar,20),
					new SqlParameter("@totalQty", SqlDbType.Decimal,9),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@passQty", SqlDbType.Decimal,9),
					new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
					new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@color", SqlDbType.VarChar,50),
					new SqlParameter("@materialName", SqlDbType.VarChar,50),
					new SqlParameter("@outerBoxQty", SqlDbType.Decimal,9),
					new SqlParameter("@packingTrays", SqlDbType.VarChar,50),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@shipTo", SqlDbType.VarChar,50),
					new SqlParameter("@module", SqlDbType.VarChar,50),
					new SqlParameter("@sn", SqlDbType.Int,4),
					new SqlParameter("@indexId", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.trackingID;
			parameters[2].Value = model.machineID;
			parameters[3].Value = model.dateTime;
			parameters[4].Value = model.materialPartNo;
			parameters[5].Value = model.jigNo;
			parameters[6].Value = model.model;
			parameters[7].Value = model.cavityCount;
			parameters[8].Value = model.userName;
			parameters[9].Value = model.userID;
			parameters[10].Value = model.startTime;
			parameters[11].Value = model.stopTime;
			parameters[12].Value = model.day;
			parameters[13].Value = model.shift;
			parameters[14].Value = model.status;
			parameters[15].Value = model.remark_1;
			parameters[16].Value = model.remark_2;
			parameters[17].Value = model.rejectQty;
			parameters[18].Value = model.rejectQtyHour01;
			parameters[19].Value = model.rejectQtyHour02;
			parameters[20].Value = model.rejectQtyHour03;
			parameters[21].Value = model.rejectQtyHour04;
			parameters[22].Value = model.rejectQtyHour05;
			parameters[23].Value = model.rejectQtyHour06;
			parameters[24].Value = model.rejectQtyHour07;
			parameters[25].Value = model.rejectQtyHour08;
			parameters[26].Value = model.rejectQtyHour09;
			parameters[27].Value = model.rejectQtyHour10;
			parameters[28].Value = model.rejectQtyHour11;
			parameters[29].Value = model.rejectQtyHour12;
			parameters[30].Value = model.lastUpdatedTime;
			parameters[31].Value = model.remarks;
			parameters[32].Value = model.processes;
			parameters[33].Value = model.jobId;
			parameters[34].Value = model.totalQty;
			parameters[35].Value = model.updatedTime;
			parameters[36].Value = model.passQty;
			parameters[37].Value = model.totalPassQty;
			parameters[38].Value = model.totalRejectQty;
			parameters[39].Value = model.color;
			parameters[40].Value = model.materialName;
			parameters[41].Value = model.outerBoxQty;
			parameters[42].Value = model.packingTrays;
			parameters[43].Value = model.customer;
			parameters[44].Value = model.shipTo;
			parameters[45].Value = model.module;
			parameters[46].Value = model.sn;
			parameters[47].Value = model.indexId;

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



        public SqlCommand AddCommand(Common.Class.Model.PQCPackDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCPackDetailHistory(");
            strSql.Append("id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId)");
            strSql.Append(" values (");
            strSql.Append("@id,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@jobId,@totalQty,@updatedTime,@passQty,@totalPassQty,@totalRejectQty,@color,@materialName,@outerBoxQty,@packingTrays,@customer,@shipTo,@module,@sn,@indexId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime,8),
                    new SqlParameter("@day", SqlDbType.DateTime,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,50),
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
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,50),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobId", SqlDbType.VarChar,20),
                    new SqlParameter("@totalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@passQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@color", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@outerBoxQty", SqlDbType.Decimal,9),
                    new SqlParameter("@packingTrays", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,50),
                    new SqlParameter("@module", SqlDbType.VarChar,50),
                    new SqlParameter("@sn", SqlDbType.Int,4),
                    new SqlParameter("@indexId", SqlDbType.Int,4)};
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
            parameters[17].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[18].Value = model.rejectQtyHour01== null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[19].Value = model.rejectQtyHour02== null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[20].Value = model.rejectQtyHour03== null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[21].Value = model.rejectQtyHour04== null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[22].Value = model.rejectQtyHour05== null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[23].Value = model.rejectQtyHour06== null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[24].Value = model.rejectQtyHour07== null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[25].Value = model.rejectQtyHour08== null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[26].Value = model.rejectQtyHour09== null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[27].Value = model.rejectQtyHour10== null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[28].Value = model.rejectQtyHour11== null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[29].Value = model.rejectQtyHour12== null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[30].Value = model.lastUpdatedTime== null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[31].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[32].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[33].Value = model.jobId == null ? (object)DBNull.Value : model.jobId;
            parameters[34].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[35].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[36].Value = model.passQty == null ? (object)DBNull.Value : model.passQty;
            parameters[37].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[38].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[39].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[40].Value = model.materialName == null ? (object)DBNull.Value : model.materialName;
            parameters[41].Value = model.outerBoxQty == null ? (object)DBNull.Value : model.outerBoxQty;
            parameters[42].Value = model.packingTrays == null ? (object)DBNull.Value : model.packingTrays;
            parameters[43].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[44].Value = model.shipTo == null ? (object)DBNull.Value : model.shipTo;
            parameters[45].Value = model.module == null ? (object)DBNull.Value : model.module;
            parameters[46].Value = model.sn == null ? (object)DBNull.Value : model.sn;
            parameters[47].Value = model.indexId == null ? (object)DBNull.Value : model.indexId;

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCPackDetailHistory_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCPackDetailHistory set ");
			strSql.Append("id=@id,");
			strSql.Append("trackingID=@trackingID,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("materialPartNo=@materialPartNo,");
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
			strSql.Append("jobId=@jobId,");
			strSql.Append("totalQty=@totalQty,");
			strSql.Append("updatedTime=@updatedTime,");
			strSql.Append("passQty=@passQty,");
			strSql.Append("totalPassQty=@totalPassQty,");
			strSql.Append("totalRejectQty=@totalRejectQty,");
			strSql.Append("color=@color,");
			strSql.Append("materialName=@materialName,");
			strSql.Append("outerBoxQty=@outerBoxQty,");
			strSql.Append("packingTrays=@packingTrays,");
			strSql.Append("customer=@customer,");
			strSql.Append("shipTo=@shipTo,");
			strSql.Append("module=@module,");
			strSql.Append("sn=@sn,");
			strSql.Append("indexId=@indexId");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@trackingID", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.DateTime,8),
					new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@userID", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.DateTime,8),
					new SqlParameter("@stopTime", SqlDbType.DateTime,8),
					new SqlParameter("@day", SqlDbType.DateTime,8),
					new SqlParameter("@shift", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.VarChar,50),
					new SqlParameter("@remark_1", SqlDbType.VarChar,50),
					new SqlParameter("@remark_2", SqlDbType.VarChar,50),
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
					new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@remarks", SqlDbType.VarChar,50),
					new SqlParameter("@processes", SqlDbType.VarChar,20),
					new SqlParameter("@jobId", SqlDbType.VarChar,20),
					new SqlParameter("@totalQty", SqlDbType.Decimal,9),
					new SqlParameter("@updatedTime", SqlDbType.DateTime,8),
					new SqlParameter("@passQty", SqlDbType.Decimal,9),
					new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
					new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
					new SqlParameter("@color", SqlDbType.VarChar,50),
					new SqlParameter("@materialName", SqlDbType.VarChar,50),
					new SqlParameter("@outerBoxQty", SqlDbType.Decimal,9),
					new SqlParameter("@packingTrays", SqlDbType.VarChar,50),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@shipTo", SqlDbType.VarChar,50),
					new SqlParameter("@module", SqlDbType.VarChar,50),
					new SqlParameter("@sn", SqlDbType.Int,4),
					new SqlParameter("@indexId", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.trackingID;
			parameters[2].Value = model.machineID;
			parameters[3].Value = model.dateTime;
			parameters[4].Value = model.materialPartNo;
			parameters[5].Value = model.jigNo;
			parameters[6].Value = model.model;
			parameters[7].Value = model.cavityCount;
			parameters[8].Value = model.userName;
			parameters[9].Value = model.userID;
			parameters[10].Value = model.startTime;
			parameters[11].Value = model.stopTime;
			parameters[12].Value = model.day;
			parameters[13].Value = model.shift;
			parameters[14].Value = model.status;
			parameters[15].Value = model.remark_1;
			parameters[16].Value = model.remark_2;
			parameters[17].Value = model.rejectQty;
			parameters[18].Value = model.rejectQtyHour01;
			parameters[19].Value = model.rejectQtyHour02;
			parameters[20].Value = model.rejectQtyHour03;
			parameters[21].Value = model.rejectQtyHour04;
			parameters[22].Value = model.rejectQtyHour05;
			parameters[23].Value = model.rejectQtyHour06;
			parameters[24].Value = model.rejectQtyHour07;
			parameters[25].Value = model.rejectQtyHour08;
			parameters[26].Value = model.rejectQtyHour09;
			parameters[27].Value = model.rejectQtyHour10;
			parameters[28].Value = model.rejectQtyHour11;
			parameters[29].Value = model.rejectQtyHour12;
			parameters[30].Value = model.lastUpdatedTime;
			parameters[31].Value = model.remarks;
			parameters[32].Value = model.processes;
			parameters[33].Value = model.jobId;
			parameters[34].Value = model.totalQty;
			parameters[35].Value = model.updatedTime;
			parameters[36].Value = model.passQty;
			parameters[37].Value = model.totalPassQty;
			parameters[38].Value = model.totalRejectQty;
			parameters[39].Value = model.color;
			parameters[40].Value = model.materialName;
			parameters[41].Value = model.outerBoxQty;
			parameters[42].Value = model.packingTrays;
			parameters[43].Value = model.customer;
			parameters[44].Value = model.shipTo;
			parameters[45].Value = model.module;
			parameters[46].Value = model.sn;
			parameters[47].Value = model.indexId;

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
			strSql.Append("delete from PQCPackDetailHistory ");
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
		public Common.Class.Model.PQCPackDetailHistory_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId from PQCPackDetailHistory ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Common.Class.Model.PQCPackDetailHistory_Model model =new Common.Class.Model.PQCPackDetailHistory_Model();
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
		public Common.Class.Model.PQCPackDetailHistory_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCPackDetailHistory_Model model =new Common.Class.Model.PQCPackDetailHistory_Model();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["trackingID"]!=null)
				{
					model.trackingID=row["trackingID"].ToString();
				}
				if(row["machineID"]!=null)
				{
					model.machineID=row["machineID"].ToString();
				}
					//model.dateTime=row["dateTime"].ToString();
				if(row["materialPartNo"]!=null)
				{
					model.materialPartNo=row["materialPartNo"].ToString();
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
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["userID"]!=null)
				{
					model.userID=row["userID"].ToString();
				}
					//model.startTime=row["startTime"].ToString();
					//model.stopTime=row["stopTime"].ToString();
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
				if(row["rejectQty"]!=null && row["rejectQty"].ToString()!="")
				{
					model.rejectQty=decimal.Parse(row["rejectQty"].ToString());
				}
				if(row["rejectQtyHour01"]!=null)
				{
					model.rejectQtyHour01=row["rejectQtyHour01"].ToString();
				}
				if(row["rejectQtyHour02"]!=null)
				{
					model.rejectQtyHour02=row["rejectQtyHour02"].ToString();
				}
				if(row["rejectQtyHour03"]!=null)
				{
					model.rejectQtyHour03=row["rejectQtyHour03"].ToString();
				}
				if(row["rejectQtyHour04"]!=null)
				{
					model.rejectQtyHour04=row["rejectQtyHour04"].ToString();
				}
				if(row["rejectQtyHour05"]!=null)
				{
					model.rejectQtyHour05=row["rejectQtyHour05"].ToString();
				}
				if(row["rejectQtyHour06"]!=null)
				{
					model.rejectQtyHour06=row["rejectQtyHour06"].ToString();
				}
				if(row["rejectQtyHour07"]!=null)
				{
					model.rejectQtyHour07=row["rejectQtyHour07"].ToString();
				}
				if(row["rejectQtyHour08"]!=null)
				{
					model.rejectQtyHour08=row["rejectQtyHour08"].ToString();
				}
				if(row["rejectQtyHour09"]!=null)
				{
					model.rejectQtyHour09=row["rejectQtyHour09"].ToString();
				}
				if(row["rejectQtyHour10"]!=null)
				{
					model.rejectQtyHour10=row["rejectQtyHour10"].ToString();
				}
				if(row["rejectQtyHour11"]!=null)
				{
					model.rejectQtyHour11=row["rejectQtyHour11"].ToString();
				}
				if(row["rejectQtyHour12"]!=null)
				{
					model.rejectQtyHour12=row["rejectQtyHour12"].ToString();
				}
					//model.lastUpdatedTime=row["lastUpdatedTime"].ToString();
				if(row["remarks"]!=null)
				{
					model.remarks=row["remarks"].ToString();
				}
				if(row["processes"]!=null)
				{
					model.processes=row["processes"].ToString();
				}
				if(row["jobId"]!=null)
				{
					model.jobId=row["jobId"].ToString();
				}
				if(row["totalQty"]!=null && row["totalQty"].ToString()!="")
				{
					model.totalQty=decimal.Parse(row["totalQty"].ToString());
				}
					//model.updatedTime=row["updatedTime"].ToString();
				if(row["passQty"]!=null && row["passQty"].ToString()!="")
				{
					model.passQty=decimal.Parse(row["passQty"].ToString());
				}
				if(row["totalPassQty"]!=null && row["totalPassQty"].ToString()!="")
				{
					model.totalPassQty=decimal.Parse(row["totalPassQty"].ToString());
				}
				if(row["totalRejectQty"]!=null && row["totalRejectQty"].ToString()!="")
				{
					model.totalRejectQty=decimal.Parse(row["totalRejectQty"].ToString());
				}
				if(row["color"]!=null)
				{
					model.color=row["color"].ToString();
				}
				if(row["materialName"]!=null)
				{
					model.materialName=row["materialName"].ToString();
				}
				if(row["outerBoxQty"]!=null && row["outerBoxQty"].ToString()!="")
				{
					model.outerBoxQty=decimal.Parse(row["outerBoxQty"].ToString());
				}
				if(row["packingTrays"]!=null)
				{
					model.packingTrays=row["packingTrays"].ToString();
				}
				if(row["customer"]!=null)
				{
					model.customer=row["customer"].ToString();
				}
				if(row["shipTo"]!=null)
				{
					model.shipTo=row["shipTo"].ToString();
				}
				if(row["module"]!=null)
				{
					model.module=row["module"].ToString();
				}
				if(row["sn"]!=null && row["sn"].ToString()!="")
				{
					model.sn=int.Parse(row["sn"].ToString());
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
			strSql.Append("select id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId ");
			strSql.Append(" FROM PQCPackDetailHistory ");
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
			strSql.Append(" id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId ");
			strSql.Append(" FROM PQCPackDetailHistory ");
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
			strSql.Append(")AS Row, T.*  from PQCPackDetailHistory T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "PQCPackDetailHistory";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
	}
}

