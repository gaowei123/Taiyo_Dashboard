/**  版本信息模板在安装目录下，可自行修改。
* PQCPackDetailTracking.cs
*
* 功 能： N/A
* 类 名： PQCPackDetailTracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/1/30 21:14:51   N/A    初版
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
	/// 数据访问类:PQCPackDetailTracking
	/// </summary>
	public partial class PQCPackDetailTracking_DAL
	{
		public PQCPackDetailTracking_DAL()
		{}
	



		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCPackDetailTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId from PQCPackDetailTracking ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Common.Class.Model.PQCPackDetailTracking_Model model=new Common.Class.Model.PQCPackDetailTracking_Model();
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
		public Common.Class.Model.PQCPackDetailTracking_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCPackDetailTracking_Model model=new Common.Class.Model.PQCPackDetailTracking_Model();
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
                if (row["startTime"] != null && row["startTime"].ToString() != "")
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
		public DataSet GetList(string trackingID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId ");
			strSql.Append(" FROM PQCPackDetailTracking  where trackingID = @trackingID");


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
			strSql.Append(" id,trackingID,machineID,dateTime,materialPartNo,jigNo,model,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,jobId,totalQty,updatedTime,passQty,totalPassQty,totalRejectQty,color,materialName,outerBoxQty,packingTrays,customer,shipTo,module,sn,indexId ");
			strSql.Append(" FROM PQCPackDetailTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}




        public SqlCommand UpdatePQCMaintenance(Common.Class.Model.PQCPackDetailTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCPackDetailTracking set
                                totalQty = @totalQty, 
                                passQty = @passQty, 
                                status = @status,
                                lastUpdatedTime = @lastUpdatedTime,
                                updatedTime =  @updatedTime,
                                remarks = @remarks
                            where trackingID  =@trackingID and materialPartNo = @materialPartNo");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar),
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@passQty", SqlDbType.Decimal),
                new SqlParameter("@status", SqlDbType.VarChar),
                new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@updatedTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@materialPartNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.trackingID;
            parameters[1].Value = model.totalQty;
            parameters[2].Value = model.passQty;
            parameters[3].Value = model.status;
            parameters[4].Value = model.lastUpdatedTime;
            parameters[5].Value = model.updatedTime;
            parameters[6].Value = model.remarks;
            parameters[7].Value = model.materialPartNo;




            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

    }
}

