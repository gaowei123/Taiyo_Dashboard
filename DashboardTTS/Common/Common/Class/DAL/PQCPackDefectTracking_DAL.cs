/**  版本信息模板在安装目录下，可自行修改。
* PQCPackDefectTracking.cs
*
* 功 能： N/A
* 类 名： PQCPackDefectTracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/1/30 21:14:50   N/A    初版
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
	/// 数据访问类:PQCPackDefectTracking
	/// </summary>
	public partial class PQCPackDefectTracking_DAL
	{
        public PQCPackDefectTracking_DAL()
        {

        }


        


        public Common.Class.Model.PQCPackDefectTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,defectDescription,totalPassQty,totalRejectQty from PQCPackDefectTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Common.Class.Model.PQCPackDefectTracking_Model model=new Common.Class.Model.PQCPackDefectTracking_Model();
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
		public Common.Class.Model.PQCPackDefectTracking_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCPackDefectTracking_Model model=new Common.Class.Model.PQCPackDefectTracking_Model();
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

                if (row["dateTime"] != null && row["dateTime"].ToString() != "")
                {
                    model.dateTime = DateTime.Parse(row["dateTime"].ToString());
                }


                if (row["materialPartNo"]!=null)
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


                if (row["startTime"]!= null && row["startTime"].ToString() != "")
                {
                    model.startTime = DateTime.Parse(row["startTime"].ToString());
                }
                if (row["stopTime"] != null && row["stopTime"].ToString() != "")
                {
                    model.stopTime = DateTime.Parse(row["stopTime"].ToString());
                }
                if (row["day"] != null && row["day"].ToString() != "")
                {
                    model.day = DateTime.Parse(row["day"].ToString());
                }
                

                if (row["shift"]!=null)
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
				if(row["defectCodeID"]!=null)
				{
					model.defectCodeID=row["defectCodeID"].ToString();
				}
				if(row["defectCode"]!=null)
				{
					model.defectCode=row["defectCode"].ToString();
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


                if (row["lastUpdatedTime"] != null && row["lastUpdatedTime"].ToString() != "")
                {
                    model.lastUpdatedTime = DateTime.Parse(row["lastUpdatedTime"].ToString());
                }

                
                if (row["remarks"]!=null)
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

                if (row["updatedTime"] != null && row["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(row["updatedTime"].ToString());
                }

                if (row["passQty"]!=null && row["passQty"].ToString()!="")
				{
					model.passQty=decimal.Parse(row["passQty"].ToString());
				}
				if(row["defectDescription"]!=null)
				{
					model.defectDescription=row["defectDescription"].ToString();
				}
				if(row["totalPassQty"]!=null && row["totalPassQty"].ToString()!="")
				{
					model.totalPassQty=decimal.Parse(row["totalPassQty"].ToString());
				}
				if(row["totalRejectQty"]!=null && row["totalRejectQty"].ToString()!="")
				{
					model.totalRejectQty=decimal.Parse(row["totalRejectQty"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string trackingID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,defectDescription,totalPassQty,totalRejectQty ");
			strSql.Append(" FROM PQCPackDefectTracking  where trackingID = @trackingID");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };

            parameters[0].Value = trackingID;

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
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
			strSql.Append(" id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,defectCodeID,defectCode,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,defectDescription,totalPassQty,totalRejectQty ");
			strSql.Append(" FROM PQCPackDefectTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}


        public SqlCommand UpdateCommand(Common.Class.Model.PQCPackDefectTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCPackDefectTracking set ");
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
            parameters[35].Value = model.jobId == null ? (object)DBNull.Value : model.jobId;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.defectDescription == null ? (object)DBNull.Value : model.defectDescription;

           

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }





    }
}

