/**  版本信息模板在安装目录下，可自行修改。
* PQCPackTracking.cs
*
* 功 能： N/A
* 类 名： PQCPackTracking
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
	/// 数据访问类:PQCPackTracking
	/// </summary>
	public partial class PQCPackTracking_DAL
	{
		public PQCPackTracking_DAL()
		{}
	
        

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCPackTracking_Model GetModel(string trackingID)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,totalRejectQty,updatedTime,totalPassQty,shipTo,indexId from PQCPackTracking ");
			strSql.Append(" where trackingID = @trackingID");
            SqlParameter[] parameters = {
                new SqlParameter("@trackingID",SqlDbType.VarChar)
			};

            parameters[0].Value = trackingID;

			Common.Class.Model.PQCPackTracking_Model model=new Common.Class.Model.PQCPackTracking_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
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
		public Common.Class.Model.PQCPackTracking_Model DataRowToModel(DataRow row)
		{
			Common.Class.Model.PQCPackTracking_Model model=new Common.Class.Model.PQCPackTracking_Model();
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
                if (row["dateTime"] != null && row["dateTime"].ToString() != "")
                {
                    model.dateTime = DateTime.Parse(row["dateTime"].ToString());
                }
                if (row["partNumber"]!=null)
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
                if (row["startTime"] != null && row["startTime"].ToString() != "")
                {
                    model.startTime = DateTime.Parse(row["startTime"].ToString());
                }
                if (row["stopTime"] != null && row["stopTime"].ToString() != "")
                {
                    model.stopTime = DateTime.Parse(row["stopTime"].ToString());
                }
                if (row["nextViFlag"]!=null)
				{
					model.nextViFlag=row["nextViFlag"].ToString();
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
                if (row["lastUpdatedTime"] != null && row["lastUpdatedTime"].ToString() != "")
                {
                    model.lastUpdatedTime = DateTime.Parse(row["lastUpdatedTime"].ToString());
                }
                if (row["trackingID"]!=null)
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
				if(row["totalRejectQty"]!=null && row["totalRejectQty"].ToString()!="")
				{
					model.totalRejectQty=decimal.Parse(row["totalRejectQty"].ToString());
				}
                if (row["updatedTime"] != null && row["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(row["updatedTime"].ToString());
                }
                if (row["totalPassQty"]!=null && row["totalPassQty"].ToString()!="")
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
			strSql.Append("select id,machineID,dateTime,partNumber,jobId,processes,jigNo,model,cavityCount,cycleTime,targetQty,userName,userID,TotalQty,rejectQty,acceptQty,startTime,stopTime,nextViFlag,day,shift,status,remark_1,remark_2,refField01,refField02,refField03,refField04,refField05,refField06,refField07,refField08,refField09,refField10,refField11,refField12,customer,lastUpdatedTime,trackingID,lastTrackingID,remarks,department,totalRejectQty,updatedTime,totalPassQty,shipTo,indexId ");
			strSql.Append(" FROM PQCPackTracking ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo,string sShift, string sPartNo, string sStation, string sPIC, string sJobNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from pqcpacktracking where 1=1 and day >= @dateFrom and day < @dateTo ");


            if (sShift != "") strSql.Append(" and shift = @shift");
            if (sPartNo != "") strSql.Append(" and partNumber  = @partNumber");
            if (sStation != "") strSql.Append(" and machineID = @machineID");
            if (sPIC != "") strSql.Append(" and userID = @userID");
            if (sJobNo != "") strSql.Append(" and jobId = @jobId");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar),
                new SqlParameter("@machineID",SqlDbType.VarChar),
                new SqlParameter("@userID",SqlDbType.VarChar),
                new SqlParameter("@jobId",SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sShift != "") paras[2].Value = sShift; else paras[2] = null;
            if (sStation != "") paras[3].Value = sStation; else paras[3] = null;
            if (sPIC != "") paras[4].Value = sPIC; else paras[4] = null;
            if (sJobNo != "") paras[5].Value = sJobNo; else paras[5] = null;
            if (sPartNo != "") paras[6].Value = sPartNo; else paras[6] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
		}


        public DataTable GetPackForSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
sum(a.TotalQty) as TotalQty
,sum(a.acceptQty ) as acceptQty
,sum(a.rejectQty ) as rejectQty
,case when charindex('Laser',  b.processes,0) > 0  and charindex('Check#2',b.processes,0) = 0 and charindex('Check#3',b.processes,0) = 0 
then 'Online' else 'Offline' end as packType
from pqcpacktracking a 
left join PQCBom b on a.partNumber = b.partNumber
where 1=1 and a.day >= @dateFrom and a.day < @dateTo ");


            if (sShift != "") strSql.Append(" and a.shift = @shift ");
            if (sPartNo != "") strSql.Append(" and a.partNumber  = @partNumber ");

            strSql.Append(@"group by 
case when charindex('Laser', b.processes, 0) > 0  and charindex('Check#2', b.processes, 0) = 0 and charindex('Check#3', b.processes, 0) = 0
then 'Online' else 'Offline' end ");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sShift != "") paras[2].Value = sShift; else paras[2] = null;        
            if (sPartNo != "") paras[3].Value = sPartNo; else paras[3] = null;



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





        public DataTable GetProductDetailList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sMachineID, string sJobNumber)
        {
            StringBuilder strSql = new StringBuilder();



            strSql.Append(@"
SELECT distinct
trackingID
,convert(varchar(32), a.day,111) + ' - ' +  a.shift as shift
,'Station' + a.machineID as machineID
,a.partNumber
,a.jobID
,'' as lotNo
,a.processes
,a.nextViFlag
,a.startTime
,a.stopTime
,a.status
,ISNULL(a.acceptQty,0) as acceptQty
,ISNULL(a.rejectQty,0) as rejectQty
,ISNULL(a.TotalQty,0) as TotalQty
,a.datetime 
FROM PQCPackTracking a
where a.day >= @datefrom and a.day< @dateto ");

            if (sShift != "")
            {
                strSql.Append(" and a.shift = @shift");
            }

            if (sPartNumber != "")
            {
                strSql.Append(" and a.PartNumber = @partNo");
            }
            if (sMachineID != "")
            {
                strSql.Append(" and a.machineID = @machineID");
            }
            if (sJobNumber != "")
            {
                strSql.Append(" and a.jobId = @jobId");
            }

            


            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar),
                new SqlParameter("@partNo",SqlDbType.VarChar),
                new SqlParameter("@machineID",SqlDbType.VarChar),
                new SqlParameter("@jobId",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (sShift != "")       paras[2].Value = sShift;        else paras[2] = null;
            if (sPartNumber != "")  paras[3].Value = sPartNumber;   else paras[3] = null;
            if (sMachineID != "")   paras[4].Value = sMachineID;    else paras[4] = null;
            if (sJobNumber != "")   paras[5].Value = sJobNumber;    else paras[5] = null;





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




        public DataSet GetDayOutput(DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select 

SUM(a.totalQty) as Output_PCS
,convert(float, Round(SUM(a.TotalQty/b.materialCount),0)) as Output_SET

from pqcpacktracking a 
left join 
(
	select partnumber , count(1) as materialCount from PQCBomDetail group by partnumber
) b on a.partNumber = b.partNumber

where day=@day  
group by day  ");


            SqlParameter[] paras =
            {
                new SqlParameter("@day",SqlDbType.DateTime)
            };

            paras[0].Value = dDay;
            


           
            return DBHelp.SqlDB.Query(strSql.ToString(),paras,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



        public SqlCommand UpdatePQCMaintenance(Common.Class.Model.PQCPackTracking_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCPackTracking set
                                targetQty = @targetQty,
                                totalQty = @totalQty, 
                                acceptQty = @acceptQty, 
                                status = @status,
                                nextViFlag = @nextViFlag,
                                lastUpdatedTime = @lastUpdatedTime,
                                updatedTime =  @updatedTime,
                                remarks = @remarks
                            where trackingID  =@trackingID ");

            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar),
                new SqlParameter("@targetQty", SqlDbType.Decimal),
                new SqlParameter("@totalQty", SqlDbType.Decimal),
                new SqlParameter("@acceptQty", SqlDbType.Decimal),
                new SqlParameter("@status", SqlDbType.VarChar),
                new SqlParameter("@nextViFlag", SqlDbType.VarChar),
                new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@updatedTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar)
            };

            parameters[0].Value = model.trackingID;
            parameters[1].Value = model.targetQty;
            parameters[2].Value = model.TotalQty;
            parameters[3].Value = model.acceptQty;
            parameters[4].Value = model.status;
            parameters[5].Value = model.nextViFlag;

            parameters[6].Value = model.lastUpdatedTime;
            parameters[7].Value = model.updatedTime;
            parameters[8].Value = model.remarks;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



        public DataTable GetDailyOperatorList(DateTime dDate, string sShift, string sUserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobID
,startTime
,stopTime
,a.partNumber
,'Packing' as Process
,rejectQty
,acceptQty
,rejectQty *  isnull( c.unitCost,0) as rejPrice
,Upper(userID) as userID
,d.materialCount
from PQCPackTracking a
left join PQCBom c on a.partNumber = c.partNumber
left join  (
    select partNumber, count(1) as materialCount
    from PQCBomDetail
    group by partnumber
) d on c.partNumber = d.partNumber
where 1=1  ");

            strSql.AppendLine(" and a.day = @date ");
            if (!string.IsNullOrEmpty(sShift)) strSql.AppendLine(" and a.shift = @shift ");
            if (!string.IsNullOrEmpty(sUserID)) strSql.AppendLine(" and a.userID = @userID");

            SqlParameter[] parameters = {
                new SqlParameter("@date", SqlDbType.DateTime2),
                new SqlParameter("@shift", SqlDbType.VarChar,50),
                new SqlParameter("@userID", SqlDbType.VarChar,50),
            };

            parameters[0].Value = dDate;
            if (!string.IsNullOrEmpty(sShift)) parameters[1].Value = sShift; else parameters[1] = null;
            if (!string.IsNullOrEmpty(sUserID)) parameters[2].Value = sUserID; else parameters[2] = null;




            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetPackInventoryDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select

 b.customer
,b.model
,a.PartNumber
,a.jobid
,a.materialPartNo

,ISNULL( d.output,0) as afterQty
,a.materialQty as beforeQty

,isnull(c.materialCount ,1) as materialCount
,case when b.partNumber is null then 'false' else 'true' end as bomFlag

from PQCQaViBinning a


left join (
	select 
	partNumber
	,customer
	,model
	,case when charindex('check#3',processes,0)>0 then 'CHECK#3' 
		  when charindex('check#2',processes,0)>0 then 'CHECK#2'
		  else 'CHECK#1'
		  end as lastCheckProcess
	from PQCBom
) b on a.partnumber = b.partNumber


left join (
	select 
	partNumber
	,count(1) as materialCount 
	from PQCBomDetail 
	group by partNumber
) c on a.partNumber  = c.partNumber


left join (
	select
		jobID
		,materialPartNo		
		,SUM(passQty) as output
	from PQCPackDetailTracking 
	where 1=1 
	group by jobID, materialPartNo
) d on a.jobId = d.jobid and a.materialPartNo = d.materialPartNo


where 1=1 and a.processes = b.lastCheckProcess
and a.day >= @dateFrom 
and a.day < @dateTo ");

        
            if (!string.IsNullOrEmpty(sPartNo)) strSql.AppendLine(" and a.PartNumber = @partNo ");
            if (!string.IsNullOrEmpty(sJobNo)) strSql.AppendLine(" and a.jobid = @jobID ");

            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime2),
                new SqlParameter("@dateTo", SqlDbType.DateTime2),
                new SqlParameter("@partNo", SqlDbType.VarChar,50),
                new SqlParameter("@jobID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sPartNo)) parameters[2].Value = sPartNo; else parameters[2] = null;
            if (!string.IsNullOrEmpty(sJobNo)) parameters[3].Value = sJobNo; else parameters[3] = null;




            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }




    }
}

