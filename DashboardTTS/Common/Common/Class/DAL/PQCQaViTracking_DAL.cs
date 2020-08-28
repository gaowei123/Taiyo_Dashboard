using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class PQCQaViTracking_DAL
    {
        public DataTable GetJobCheck1Output(string jobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" 
select a.jobId,a.OK from 
(
	select 
	jobId,
	convert(decimal(18,0), sum(acceptQty / b.materialCount)) as OK,
	max(isnull(nextviflag,'false')) as nextviflag


	from PQCQaViTracking  a
	left join (select partNumber, count(1) as  materialCount from PQCBomDetail group by partNumber) b 
	on a.partNumber = b.partNumber 

	where jobid = @jobID and a.processes = 'Check#1' 
	group by jobId 
)a 
where a.nextviflag = 'true'");
            
            SqlParameter[] parameters = {
                new SqlParameter("@jobID", SqlDbType.VarChar,32)
            };

            parameters[0].Value = jobNo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


        public DataTable GetJobCheck2Output(string jobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" 
select a.jobId,a.OK from 
(
	select 
	jobId,
	convert(decimal(18,0), sum(acceptQty / b.materialCount)) as OK,
	max(isnull(nextviflag,'false')) as nextviflag


	from PQCQaViTracking  a
	left join (select partNumber, count(1) as  materialCount from PQCBomDetail group by partNumber) b 
	on a.partNumber = b.partNumber 

	where jobid = @jobID and a.processes = 'Check#2' 
	group by jobId 
)a 
where a.nextviflag = 'true' ");

            SqlParameter[] parameters = {
                new SqlParameter("@jobID", SqlDbType.VarChar,32)
            };

            parameters[0].Value = jobNo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable GetList(string sPartNumber, string sJobNumber, DateTime dDateFrom, DateTime dDateTo, string sShift,string sMachineType,string sLotNo,string sDateNotIn, string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
SELECT distinct
	CONVERT(varchar(100), a.day, 111)  + ' - ' + a.shift  as  shift
    ,trackingID
	,day
	,'Station' + a.machineID as machineID
    ,a.datetime
	,a.customer
	,a.model
	,jobId
    ,c.lotno
	,a.partNumber
	,b.color
	,a.processes
    ,a.status
    ,a.nextViFlag
	,[startTime]
    ,stopTime as stopTime
    ,case when isnull(stopTime,'') = '' then '' else  convert(varchar,convert(datetime,stopTime) - convert(datetime,startTime), 108) end as Time
	,[rejectQty] as NG
    ,[acceptQty] as OK
	,[TotalQty]  as Total
	,rejectQty + acceptQty as Output
    ,a.userID
	,case when acceptQty + rejectQty =0 then '0%' else  convert(varchar,convert(decimal(18,2), rejectQty/(acceptQty + rejectQty)*100)) + '%' end  as RejRate
    ,c.LotQty
FROM PQCQaViTracking  a
left join PQCBom b on a.partNumber = b.partNumber 
left join
(
	select distinct jobNumber, lotno, convert(float,inQuantity) as LotQty  from OPENDATASOURCE( 'SQLOLEDB', {0} ).taiyo_painting.dbo.PaintingDeliveryHis
    where paintProcess is null or paintProcess =  'Paint#1'
) c 
on a.jobId COLLATE Chinese_PRC_CI_AS = c.jobNumber COLLATE Chinese_PRC_CI_AS 
where 1 = 1  and a.day >= @DateFrom  and a.day < @DateTo ", StaticRes.Global.SqlConnection.SqlconnPainting);
            
            
         
            
            if (!string.IsNullOrEmpty(sPartNumber))
            {
                strSql.Append("  and a.partNumber = @partNumber ");
            }
            if (!string.IsNullOrEmpty(sJobNumber))
            {
                strSql.Append("  and a.jobId = @jobId ");
            }
            if (!string.IsNullOrEmpty(sShift))
            {
                strSql.Append("  and a.shift = @shift ");
            }
            if (sMachineType != "")
            {
                strSql.Append("  and a.machineID = @machineID ");
            }
            if (sLotNo != "")
            {
                strSql.Append("  and c.lotNo = @lotNo ");
            }

            if (sDateNotIn != "")
            {
                strSql.Append(" and DATEPART(dd,a.day) not in  ( ");

                foreach (string sNotInDay in sDateNotIn.Split(','))
                {
                    strSql.Append(sNotInDay + ",");
                }

                strSql.Remove(strSql.Length - 1, 1);
                strSql.Append(")");
            }


            if (sTrackingID != "")
            {
                strSql.Append(" and a.trackingID = @trackingID ");
            }


            strSql.Append(" order by a.datetime desc ");
            

            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime2),
                new SqlParameter("@DateTo", SqlDbType.DateTime2),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@jobId", SqlDbType.VarChar),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@lotNo", SqlDbType.VarChar),
                new SqlParameter("@trackingID", SqlDbType.VarChar)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;

            if (!string.IsNullOrEmpty(sPartNumber))
            {
                parameters[2].Value = sPartNumber;
            }   
            else { parameters[2] = null; }
            if (!string.IsNullOrEmpty(sJobNumber))
            {
                parameters[3].Value = sJobNumber;
            }
            else { parameters[3] = null; }
            if (!string.IsNullOrEmpty(sShift))
            {
                parameters[4].Value = sShift;
            }
            else { parameters[4] = null; }

            if (!string.IsNullOrEmpty(sMachineType))
            {
                parameters[5].Value = sMachineType;
            }
            else { parameters[5] = null; }

            if (!string.IsNullOrEmpty(sLotNo))
            {
                parameters[6].Value = sLotNo;
            }
            else { parameters[6] = null; }

            if (!string.IsNullOrEmpty(sTrackingID))
            {
                parameters[7].Value = sLotNo;
            }
            else { parameters[7] = null; }

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
        
        public DataTable GetModel(string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT  * FROM PQCQaViTracking where 1 = 1 and trackingID = @trackingID ");


            
            SqlParameter[] parameters = {
                new SqlParameter("@trackingID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = sTrackingID;



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

        public DataTable GetLatestModelByJob(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT top 1  * FROM PQCQaViTracking where 1 = 1 and jobId = @jobId  order by datetime desc");



            SqlParameter[] parameters = {
                new SqlParameter("@jobId", SqlDbType.VarChar,50)
            };

            parameters[0].Value = sJobNo;



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











        /// <summary>
        /// new logic , 以tracking为一条记录, 汇总defect中 mould,paint,laser,other的rejqty, 以及bom中num, type等设定值
        /// 在代码中在判断归类.
        /// </summary>
        /// <param name="dDateFrom">UI查询参数</param>
        /// <param name="dDateTo">UI查询参数</param>
        /// <param name="sShift">UI查询参数</param>
        /// <param name="sPartNo">UI查询参数</param>
        /// <returns></returns>
        public DataTable GetSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();


            string searchShift = sShift == "" ? "" : " and a.shift = @shift";
            string searchPartNo = sPartNo == "" ? "" : " and a.partNumber = @partNumber";

            strSql.AppendFormat(@"
select 
 a.trackingID
,a.partnumber  
,a.TotalQty
,a.acceptQty
,a.rejectQty
,a.processes as currentProcess
,c.description
,c.number
,c.isContainLaser
,c.lastCheckProcess

,isnull( case when c.MouldRejType  = 'TTS' then b.mouldRej end ,0) as ttsRej
,isnull( case when c.MouldRejType != 'TTS' then b.mouldrej end ,0) as vendorRej
,b.paintRej
,b.laserRej
,b.othersRej

from PQCQaViTracking a

left join (
	select 
	trackingID
	,ISNULL(SUM( case when defectdescription = 'Mould' then rejectQty end),0) as mouldRej
	,ISNULL(SUM( case when defectdescription = 'Paint' then rejectQty end),0) as paintRej
	,ISNULL(SUM( case when defectdescription = 'Laser' then rejectQty end),0) as laserRej
	,ISNULL(SUM( case when defectdescription = 'others' then rejectQty end),0) as othersRej
	from PQCQaViDefectTracking
	where day >= @DateFrom and day < @DateTo
	group by trackingID
) b on a.trackingID = b.trackingID

left join (
	select 
	partNumber
	,description
	,isnull(number,'') as number
	,case when charindex('Laser', processes,0) > 0 then 'TRUE' ELSE 'FALSE' END AS isContainLaser
	,case when charindex('Check#3',processes,0) > 0 then 'CHECK#3' 
		  when charindex('Check#2',processes,0) > 0 then 'CHECK#2'
		  else 'CHECK#1' 
	END as lastCheckProcess
	,case when remark_1 = 'TTS' then 'TTS' else 'Vendor' end as MouldRejType
	from PQCBom
) c on a.partNumber = c.partNumber

where a.day >= @DateFrom and a.day < @DateTo
 {1}  {2} ", searchShift, searchShift,searchPartNo);


            
            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sShift.Trim() != "") parameters[2].Value = sShift; else parameters[2] = null;
            if (sPartNo.Trim() != "") parameters[3].Value = sPartNo; else parameters[3] = null;



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
        
        
       
        public DataTable GetVIDetailForButtonReport_NEW(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
a.jobID
,a.model
,a.partNumber
,b.materialPartNo
,Sum(b.passQty) as passQty
,sum(b.rejectQty) as rejectQty
,c.remark_1 as supplier
,case when  CHARINDEX('Laser', c.processes,0)> 0 then 'Laser' else 'WIP' end as PartsType
,case when c.remark_1 = 'TTS' then 'TTS' else 'Vendor' end as mouldType
,a.processes
,OP = stuff((SELECT  ',' +  t.userID 
			FROM (select jobId,processes, userID from PQCQaViTracking group by jobId,processes,userID) t  
			WHERE t.jobId = a.jobId and t.processes = a.processes  FOR xml path('')) 
			, 1 , 1 , '')
from PQCQaViTracking a
left join PQCQaViDetailTracking b on a.trackingID = b.trackingID
left join PQCBom c on a.partNumber = c.partnumber
where 1=1 ");

            strSql.Append(" and a.jobId in " + strWhere);
            strSql.Append(" group by a.jobId, a.model, a.partNumber , b.materialPartNo, c.remark_1, a.processes, c.processes ");
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }




        public DataTable GetAllDisplayJobs(DateTime dDateFrom, DateTime dDateTo, string sDescription, string sPartNumber, string sJobNo, string sModel, string sSupplier, string sColor, string sCoating)
        {
            StringBuilder strSql = new StringBuilder();

            //查询出所有job到临时表
            strSql.Append(@"
select a.jobId
from (
	select distinct jobid from pqcqavitracking 
	where  1=1 and nextViFlag = 'true'
	and day >= @DateFrom
	and day < @DateTo
) a 


left join 
(
	select jobId, partnumber, max(processes) as curCheckProcess  from PQCQaViTracking   
	group by jobid, partNumber
) b  on a.jobId = b.jobId


left join (
	select 
	partNumber,
	case when charindex('Check#3',processes,0) > 0 then 'CHECK#3'
		 when charindex('Check#2',processes,0) > 0 then 'CHECK#2' else 'CHECK#1' 
	end as lastCheckingProcess,
	description,
	coating,
	color,
	remark_1,
	model
	from pqcbom 
) c   on c.partNumber = b.partNumber 


where b.curCheckProcess = c.lastCheckingProcess ");

            if (sDescription.Trim() == "BUTTON")
                strSql.AppendLine(" and c.description != 'BEZEL' and  c.description != 'PANEL'  ");
            else if (sDescription.Trim() == "BEZEL"  || sDescription.Trim() == "PANEL")
                strSql.AppendLine(" and c.description = '"+ sDescription + "' ");



            if (sJobNo.Trim() != "")
                strSql.AppendLine(" and a.JobID = @JobNo ");

            if (sPartNumber.Trim() != "")
                strSql.AppendLine(" and c.partnumber = @PartNumber ");
            
            if (sModel.Trim() != "")
                strSql.AppendLine(" and c.model = @Model");

            if (sColor.Trim() != "")
                strSql.AppendLine(" and c.color = @Color  ");

            if (sSupplier.Trim() != "")
                strSql.AppendLine(" and c.remark_1 = @supplier");

            if (sCoating.Trim() != "")
                strSql.AppendLine(" and c.coating = @coating");

         
          

            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                new SqlParameter("@JobNo",SqlDbType.VarChar,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Color", SqlDbType.VarChar,50),
                new SqlParameter("@supplier", SqlDbType.VarChar,50),
                new SqlParameter("@coating", SqlDbType.VarChar,50)
            };


            if (dDateFrom != null) parameters[0].Value = dDateFrom; else parameters[0] = null;
            if (dDateTo != null) parameters[1].Value = dDateTo; else parameters[1] = null;
            if (sPartNumber != "") parameters[2].Value = sPartNumber; else parameters[2] = null;
            if (sJobNo != "") parameters[3].Value = sJobNo; else parameters[3] = null;
            if (sModel != "") parameters[4].Value = sModel; else parameters[4] = null;
            if (sColor != "") parameters[5].Value = sColor; else parameters[5] = null;
            if (sSupplier != "") parameters[6].Value = sSupplier; else parameters[6] = null;
            if (sCoating != "") parameters[7].Value = sCoating; else parameters[7] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }





        public DataTable GetCheckingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sStation, string sPIC, string sType)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"
select
CONVERT(varchar, a.day,111) + ' - ' + a.shift as  [day]
,'Station' + a.machineID as Station
,a.partNumber
,UPPER(a.jobId) as JobNumber

,'' as lotNo
,'' as MrpQtyPcs
,'' as MrpQtySet


--pcs
,a.totalQty as totalQtyPcs
,a.acceptQty as acceptQtyPcs

--set
,d.totalQtySet
,d.passQtySet


,isnull( b.MouldingRej,0) as MouldingRej
,isnull( b.PaintingRej,0) as PaintingRej
,isnull( b.LaserRej,0) as LaserRej
,isnull( b.OthersRej,0) as  OthersRej
,a.rejectQty




,a.userID as PIC
,a.startTime
,a.stoptime
,convert(varchar, datediff(second,startTime,stopTime)) as totalSecond

,e.materialCount

from PQCQaViTracking a

left join 
(
	select
		trackingID
		, ISNULL(SUM( case when defectDescription = 'Mould' then rejectQty end),0) as MouldingRej
		, ISNULL(SUM( case when defectDescription = 'Paint' then rejectQty end),0) as PaintingRej
		, ISNULL(SUM( case when defectDescription = 'Laser' then rejectQty end),0) as LaserRej
		, ISNULL(SUM( case when defectDescription = 'Others' then rejectQty end),0) as OthersRej
	from PQCQaViDefectTracking   
    where 1=1 
    and day >= @dDateFrom
	and day < @dDateTo
	group by trackingID
)b on a.trackingID = b.trackingID



left join PQCBom c on a.partNumber = c.partNumber



left join 
(
	select trackingID, min(rejectQty + passQty) as totalQtySet, min(passQty) as passQtySet from PQCQaViDetailTracking  
	where  day >= @dDateFrom
	and day < @dDateTo
	group by trackingID 
) d on a.trackingID = d.trackingID

left join (select partnumber, count(1) as materialCount  from PQCBomDetail group by partNumber) e on e.partNumber = a.partNumber


where 1=1
and a.TotalQty  > 0
and (a.acceptQty + a.rejectQty) > 0 
and a.day >= @dDateFrom
and a.day < @dDateTo ");


            //online
            if (sType == "Online") strSql.Append(" and c.processes like '%Laser%' and a.processes = 'Check#1' ");
            //wip
            else if (sType == "WIP") strSql.Append(@" and (c.processes not like '%Laser%' 
                                                           or ( c.processes like '%Laser%' and a.processes = 'Check#2' )
                                                           or ( c.processes like '%Laser%' and a.processes = 'Check#3' ) 
                                                    )");
           



            if (sPartNumber.Trim() != "") strSql.Append(" and a.partNumber = @PartNumber ");
            if (sShift.Trim() != "") strSql.Append(" and a.Shift = @Shift ");
            if (sStation != "") strSql.Append(" and a.MachineID =  @MachineID");
            if (sPIC != "") strSql.Append(" and a.userID = @userID");



            strSql.Append(" order by a.datetime desc ");



            SqlParameter[] parameters = {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
                new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@userID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sPartNumber.Trim() != "") parameters[2].Value = sPartNumber; else parameters[2] = null;
            if (sShift.Trim() != "") parameters[3].Value = sShift; else parameters[3] = null;
            if (sStation.Trim() != "") parameters[4].Value = sStation; else parameters[4] = null;
            if (sPIC.Trim() != "") parameters[5].Value = sPIC; else parameters[5] = null;


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


        public DataTable GetPackingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sStation, string sPIC)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"
select
CONVERT(varchar, a.day,111) + ' - ' + a.shift as  [day]
,'Station' + a.machineID as Station
,a.partNumber
,UPPER(a.jobId) as JobNumber

,'' as lotNo
,'' as MrpQtyPcs
,'' as MrpQtySet


--pcs
,a.totalQty as totalQtyPcs
,a.acceptQty as acceptQtyPcs

--set
,d.totalQtySet
,d.passQtySet



,a.rejectQty




, a.userID as PIC
,a.startTime
,a.stoptime
,convert(varchar, datediff(second,startTime,stopTime)) as totalSecond

,e.materialCount

from PQCPackTracking a
left join PQCBom c on a.partNumber = c.partNumber
left join 
(
	select trackingID, min(rejectQty + passQty) as totalQtySet, min(passQty) as passQtySet from PQCPackDetailTracking  
	where  day >= @dDateFrom
	and day < @dDateTo
	group by trackingID 
) d on a.trackingID = d.trackingID

left join (select partnumber, count(1) as materialCount  from PQCBomDetail group by partNumber) e on e.partNumber = a.partnumber


where 1=1
and a.TotalQty  > 0
and (a.acceptQty + a.rejectQty) > 0 
and a.day >= @dDateFrom
and a.day < @dDateTo ");


         


            if (sPartNumber.Trim() != "") strSql.Append(" and a.partNumber = @PartNumber ");
            if (sShift.Trim() != "") strSql.Append(" and a.Shift = @Shift ");
            if (sStation != "") strSql.Append(" and a.MachineID =  @MachineID");
            if (sPIC != "") strSql.Append(" and a.userID = @userID");



            strSql.Append(" order by a.datetime desc ");



            SqlParameter[] parameters = {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
                new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@userID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sPartNumber.Trim() != "") parameters[2].Value = sPartNumber; else parameters[2] = null;
            if (sShift.Trim() != "") parameters[3].Value = sShift; else parameters[3] = null;
            if (sStation.Trim() != "") parameters[4].Value = sStation; else parameters[4] = null;
            if (sPIC.Trim() != "") parameters[5].Value = sPIC; else parameters[5] = null;


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




        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViTracking model,SqlCommand cmd=null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update PQCQaViTracking set 
                           [status] = @status
                           ,stopTime = @stopTime
                           ,updatedTime = getdate()

                           ,targetQty = @targetQty
                           ,acceptQty = @acceptQty
                           ,totalQty = @totalQty
                           ,nextViFlag = @nextViFlag

                           ,remark_1 = @remark_1

                            where trackingID = @trackingID ");
            SqlParameter[] parameters = {
                new SqlParameter("@targetQty", SqlDbType.VarChar,50),
                new SqlParameter("@acceptQty", SqlDbType.VarChar,50),
                new SqlParameter("@totalQty", SqlDbType.VarChar,50),
                new SqlParameter("@remark_1", SqlDbType.VarChar,50),
                new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                new SqlParameter("@stopTime", SqlDbType.DateTime),

                new SqlParameter("@status", SqlDbType.VarChar,50),
                new SqlParameter("@nextViFlag", SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.targetQty;
            parameters[1].Value = model.acceptQty;
            parameters[2].Value = model.TotalQty;
            parameters[3].Value = model.remark_1;
            parameters[4].Value = model.trackingID;
            parameters[5].Value = model.stopTime;

            parameters[6].Value = model.status;
            parameters[7].Value = model.nextViFlag;

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

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
        

        internal DataTable getProductivityReportForPQC(DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                        select
	                    --case 
	                    --	when aa.number='Laser Btn'		then 1 
	                    --	when aa.number='w/o Laser Btn'	then 2
	                    --	when aa.number='SBW TKS784'		then 3
	                    --	when aa.number='TMS TKS824'		then 4
	                    --	when aa.number='TAC TKS833'		then 5
	                    --	when aa.number='TRMI 452'		then 6
	                    --	when aa.number='TRMI 595,656'	then 7
	                    --	when aa.number='320B TKS830'	then 8
	                    --	when aa.number='320B TKS831'	then 9
	                    --	when aa.number='Packers'		then 10
	                    --else 20
	                    --end as SN
	                    case 
		                    when aa.number='LASER'		then 1 
		                    when aa.number='WIP'		then 2
		                    when aa.number='784'		then 3
		                    when aa.number='824'		then 4
		                    when aa.number='833'		then 5
		                    when aa.number='452'		then 6
		                    when aa.number='595'	    then 7
		                    when aa.number='830'		then 8
		                    when aa.number='831'		then 9
	                    else 20
	                    end as SN

	                    ,aa.number as ProductType

	                    ,'' as ManPower
	                    ,'' as Attendance
	                    ,'' as AttendRate
	                    ,'96:00:00' as TargetHR
	                    , convert(varchar(50), CONVERT(numeric(18,4), SUM( aa.ActualHR)) / 3600) as ActualHR --hour
	 
	                    ,'0' as TargetQty
                        , sum(aa.TotalQty) as TotalQty
	                    , sum(aa.acceptQty) as ActualQty

	                    , sum(aa.LaserRejCount) as LaserRejCount
                        , sum(aa.PaintRejCount) as PaintRejCount
                        , sum(aa.MouldRejCount) as MouldRejCount
                        , sum(aa.OthersRejCount) as OthersRejCount


                        , case when sum( ISNULL(aa.TotalQty,0)) = 0   
		                    then '0.00%' 
		                    else CONVERT(varchar, convert(numeric(18,2),  (sum(aa.LaserRejCount)+ sum(aa.PaintRejCount) + sum(aa.MouldRejCount) + sum(aa.OthersRejCount))  / sum(aa.TotalQty) * 100)) + '%' 
                          end as RejRate

                    from(
	                    select
	                    DATEDIFF(
			                    Second, 
			                    a.starttime, 
			                    --stop time 为 null, 默认为白班当天20点, 或者晚班隔天8点.
			                    case when stoptime is null 
			                    then DATEADD(HOUR, (case when shift = 'Day' then  20 else 32 end) ,day) 
			                    else stopTime end
	                    ) as ActualHR

	                    ,'' as TargetQty
	                    ,isnull(a.TotalQty,0) as TotalQty
	                    ,isnull(a.acceptQty,0) as acceptQty
	                    ,c.LaserRejCount
	                    ,c.MouldRejCount
	                    ,c.PaintRejCount
	                    ,c.OthersRejCount
	                    ,case when ISNULL(  b.number,'') = '' then 'Unknown Type:' + b.partNumber else b.number end as number

	                    from PQCQaViTracking a 
	                    left join PQCBom b on a.partNumber = b.partNumber 
	                    left join (
		                    select trackingID
		                    , Sum( case when defectDescription = 'Paint'  then  ISNULL(rejectQty,0) end) as PaintRejCount
		                    , Sum( case when defectDescription = 'Mould'  then  ISNULL(rejectQty,0) end) as MouldRejCount
		                    , Sum( case when defectDescription = 'Laser'  then  ISNULL(rejectQty,0) end) as LaserRejCount
		                    , Sum( case when defectDescription = 'Others' then  ISNULL(rejectQty,0) end) as OthersRejCount
		                    from PQCQaViDefectTracking group by trackingID 
	                    ) c  on a.trackingID = c.trackingID

	                    where 1=1 and a.day = @day and a.shift = @shift
                    ) aa

                    group by aa.number");


            SqlParameter[] parameters = {
                new SqlParameter("@day", SqlDbType.DateTime,50),
                new SqlParameter("@shift", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds== null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }


        internal DataTable getBezelPanelReport(DateTime dDateFrom, DateTime dDateTo, string sType, string sDescription, string sNumber,DateTime? dPaintingDate, string sPIC, DateTime? dMFGDate)
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
	and a.day >= @DateFrom
	and a.day <  @DateTo
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
                strSql.AppendLine(" and b.partNumber = @Number ");

            strSql.AppendLine(")");
            //所有check完成的jobs 临时表



            // Painting Info 临时表
            strSql.AppendFormat(@"
,paintingInfo as (
	select  distinct
	jobnumber
	,Lotno
	,MFGDate
	,convert(varchar(100), MFGDate  ,103) as [MFG Date] 
	,convert(varchar(100), paintingDate_1st  ,103) as [Painting Under Coat Date]
	,paintingRunningTime_1st
	,paintingOvenTime_1st 
	,paintLot_1st 
	,thinnersLot_1st 
	,thickness_1st 
	,paintingPIC_1st 
    ,pMachine_1st

	,convert(varchar(100), paintingDate_2nd  ,103) as [Painting Middle Coat Date]  
	,paintingRunningTime_2nd 
	,paintingOvenTime_2nd 
	,paintLot_2nd  
	,thinnersLot_2nd 
	,thickness_2nd 
	,paintingPIC_2nd 
    ,pMachine_2nd

	,convert(varchar(100), paintingDate_3rd  ,103) as [Painting Top Coat Date]
	,paintingRunningTime_3rd
	,paintingOvenTime_3rd 
	,paintLot_3rd
	,thinnersLot_3rd
	,thickness_3rd
	,paintingPIC_3rd
    ,pMachine_3rd


    ,paintingDate_1st
	,paintingDate_2nd
	,paintingDate_3rd

    ,[temperatureFront]
    ,[temperatureRear]
    ,[humidityFront]
    ,[humidityRear]

    from  OPENDATASOURCE( 'SQLOLEDB', {0} ).Taiyo_Painting.dbo.paintingtempinfo
)", StaticRes.Global.SqlConnection.SqlconnPainting);



            // Laser Info 临时表
            strSql.AppendFormat(@"
,LaserInfo as (
	SELECT [MACHINE_ID],[JOB_ID],convert(varchar, [DATE_TIME],103) as DATE_TIME,[MC_OPERATOR] FROM OPENDATASOURCE( 'SQLOLEDB', {0} ).[LMMS_TAIYO].[dbo].[LMMSBUYOFF_LIST]
)", StaticRes.Global.SqlConnection.SqlconnLaser);




            strSql.AppendFormat(@"
select 
b.partNumber
,UPPER(a.jobid) as JobNumber
,b.totalQty as [Total Checked Qty]
,b.acceptQty as [OK QTY]
,b.userID as [PQC PIC]

--Painting Info
,d.Lotno
,f.MFGDate as [MFG Date]
,d.[Painting Under Coat Date]
,d.pMachine_1st as [Painting Under Coat M/C No]
,d.paintingRunningTime_1st as [Painting Under Coat M/C running time]
,d.paintingOvenTime_1st as [Painting Under Coat oven time]
,d.paintLot_1st as [Paint Lot Under Coat]
,d.thinnersLot_1st as [Thinners Lot Under Coat]
,d.thickness_1st as [Painting Thickness Under Coat]
,d.paintingPIC_1st as [Painting PIC Under Coat]

,d.[Painting Middle Coat Date]
,d.pMachine_2nd as [Painting Middle Coat M/C No]
,d.paintingRunningTime_2nd as [Painting Middle Coat M/C running time]
,d.paintingOvenTime_2nd as [Painting Middle Coat oven time]
,d.paintLot_2nd as [Paint Lot Middle Coat]
,d.thinnersLot_2nd as [Thinners Lot Middle Coat]
,d.thickness_2nd as [Painting Thickness Middle Coat]
,d.paintingPIC_2nd as [Painting PIC Middle Coat]

,d.[Painting Top Coat Date]
,d.pMachine_3rd as [Painting Top Coat M/C No]
,d.paintingRunningTime_3rd as [Painting Top Coat M/C running time]
,d.paintingOvenTime_3rd as [Painting Top Coat oven time]
,d.paintLot_3rd as [Paint Lot Top Coat]
,d.thinnersLot_3rd as [Thinners Lot Top Coat]
,d.thickness_3rd as [Painting Thickness Top Coat]
,d.paintingPIC_3rd as [Painting PIC Top Coat]

,case when d.[temperatureFront] = 0 then '' else  convert(varchar, d.[temperatureFront]) + '&#176;C' end as [Temperature Front]
,case when d.[temperatureRear] = 0 then '' else convert(varchar, d.[temperatureRear])  + '&#176;C' end  as [Temperature Rear]
,case when d.[humidityFront] = 0 then '' else convert(varchar, d.[humidityFront]) + '%' end as [Humidity Front]
,case when d.[humidityRear] = 0 then '' else  convert(varchar, d.[humidityRear]) +'%' end as [Humidity Rear]

--Laser Info
,c.MACHINE_ID as [Laser M/C No]
,c.DATE_TIME as [Laser & Checking Date]
,c.MC_OPERATOR as [Laser PIC]

from allJobsForReports a

left join 
(
	select 
		jobid
        ,userID = stuff( (SELECT distinct ',' +  t.userID FROM (select jobId, userID from PQCQaViTracking group by jobId, userID) t  WHERE t.jobId = a.jobId  FOR xml path('')) , 1 , 1 , '')
		,partnumber
        ,sum(totalQty) as totalQty
		,sum( acceptQty) as acceptQty
	from PQCQaViTracking a 
	group by jobid, partNumber
) b  on a.jobID = b.jobID

left join laserInfo c  on a.jobid collate chinese_prc_ci_as = c.JOB_ID collate chinese_prc_ci_as 

left join paintingInfo d  on a.jobid collate chinese_prc_ci_as = d.JobNumber collate chinese_prc_ci_as 

left join 
(
    select distinct jobnumber , convert(varchar(50), datetime,103) as MFGDate from  OPENDATASOURCE( 'SQLOLEDB', {0} ).Taiyo_Painting.dbo.PaintingDeliveryHis
) f  on a.jobid collate chinese_prc_ci_as = f.JobNumber collate chinese_prc_ci_as  ", StaticRes.Global.SqlConnection.SqlconnPainting);

            strSql.AppendLine(" where 1=1 ");

            strSql.AppendLine(" and b.partNumber != 'TKS869AA-A1' ");


            if (dPaintingDate != null)
                strSql.AppendLine(" and ( d.paintingDate_1st = @PaintingDate or  d.paintingDate_2nd = @PaintingDate  or d.paintingDate_3rd = @PaintingDate  ) ");
            
            if (sPIC != "")
                strSql.AppendLine(" and b.userID like '%"+ sPIC + "%' ");

            if (dMFGDate != null)
                strSql.AppendLine(" and  d.MFGDate = @MFGDate   ");



            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@Type", SqlDbType.VarChar,50),
                new SqlParameter("@Description", SqlDbType.VarChar,50),
                new SqlParameter("@Number", SqlDbType.VarChar,50),
                new SqlParameter("@PaintingDate", SqlDbType.DateTime),
                new SqlParameter("@MFGDate", SqlDbType.DateTime)
            };


            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sType.Trim() != "") paras[2].Value = sType; else paras[2] = null;
            if (sDescription.Trim() != "") paras[3].Value = sDescription; else paras[3] = null;
            if (sNumber.Trim() != "") paras[4].Value = sNumber; else paras[4] = null;
            if (dPaintingDate != null) paras[5].Value = dPaintingDate.Value; else paras[5] = null;
            if (dMFGDate != null) paras[6].Value = dMFGDate.Value; else paras[6] = null;




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

        internal DataTable GetCheckerOutput(string sType, DateTime dDateFrom, DateTime dDateTo)
        {

            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"select 

                            userID
                            ,userType
                            ,sum(totalqty) as totalqty
                            ,sum(acceptQty) as acceptQty
                            ,sum(rejectQty) as rejectQty

                            from 
                            (
	                            select
	                            userID
	                            ,case when machineID in (1,2,3,4,5,6,7,8) then 'ONLINE' else 'WIP' end as userType
	                            ,totalqty
	                            ,acceptQty
	                            ,rejectQty

	                            from pqcqavitracking

	                            where 1=1 
	                            and dateTime > @DateFrom
	                            and dateTime < @DateTo ");

            if (sType == "ONLINE")
            {
                strSql.Append(@" and machineID in ('1','2','3','4','5','6','7','8')");
            }
            else if (sType == "WIP")
            {
                strSql.Append(@" and machineID in ('11','12','13','14','15','16','17','18',  '21','22','23','24','25','26','27','28'  )");
            }

            strSql.Append(@") a 
                            group by userID, userType
                            order by userType");

            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime,50),
                new SqlParameter("@dateTo", SqlDbType.DateTime,50)
            };
            
            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;


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

        internal DataSet GetPICReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sJobNo, string sPartNo, string sPIC)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.AppendFormat(@"
with paintDelivery as (
	select jobnumber, inQuantity from OPENDATASOURCE( 'SQLOLEDB', {0} ).Taiyo_Painting.dbo.PaintingDeliveryHis
)

select 
Row_Number() OVER (order by a.userID asc) as SN
,a.userID
,a.MRPQty
,isnull(a.LaserBtn,0) as LaserBtn
,isnull(a.WIPBtn,0) as WIPBtn
,isnull(c.Packing,0) as Packing
,isnull(b.MouldingRej,0) as MouldingRej
,isnull(b.PaintingRej,0) as PaintingRej
,isnull(b.LaserRej,0) as LaserRej
,isnull(b.OthersRej,0) as OthersRej
,isnull(a.totalRej,0) as totalRej
,isnull(a.Output,0)as Output
,convert(varchar, isnull(a.TotalSeconds,0)) as TotalSeconds

from
(
	select 
	userID
	,convert(varchar, convert(float, sum(b.inQuantity))) + '(' + convert(varchar, convert(float, sum(b.inQuantity * d.materialCount))) + ')' as MRPQty
	,SUM( case when a.processes = 'Check#1' and c.processes like '%Laser%' then a.acceptQty end ) as LaserBtn
	,SUM( case when (a.processes = 'Check#2' and c.processes like '%Laser%') or c.processes not like '%Laser%'  then a.acceptQty end ) as WIPBtn
    ,convert(varchar, convert(float,  SUM(a.TotalQty))) + '(' + convert(varchar, convert(float, sum(a.TotalQty * d.materialCount))) + ')' as Output
	,SUM(case when a.startTime is not null and  a.stopTime is not null then DATEDIFF(second, a.startTime, a.stopTime) end)  as TotalSeconds
	,SUM(a.rejectQty) as totalRej
	from PQCQaViTracking a 
	left join paintDelivery b on a.jobid collate Chinese_PRC_CI_AS = b.jobnumber collate Chinese_PRC_CI_AS
	left join PQCBom c on a.PartNumber = c.PartNumber
	left join (select partNumber, count(1) as materialCount from PQCBomDetail group by partNumber) d on a.partNumber = d.partnumber
	where 1=1 
    and a.datetime >= @datefrom
	and a.datetime < @dateto", StaticRes.Global.SqlConnection.SqlconnPainting);

            if (sShift != "")
                strSql.AppendLine(" and a.shift = @shift");

            if (sJobNo != "")
                strSql.AppendLine(" and a.jobID = @jobID");

            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @partNumber");

            if (sPIC != "")
                strSql.AppendLine(" and a.userID = @userID");


            strSql.AppendLine(@"
	group by a.userID
) a
left join 
(
	select 
	a.userID
	,SUM( case when a.defectDescription = 'Mould' then isnull(a.rejectQty,0) end ) as MouldingRej
	,SUM( case when a.defectDescription = 'Paint' then isnull(a.rejectQty,0) end ) as PaintingRej
	,SUM( case when a.defectDescription = 'Laser' then isnull(a.rejectQty,0) end ) as LaserRej
	,SUM( case when a.defectDescription = 'Others' then isnull(a.rejectQty,0) end) as OthersRej
	from PQCQaViDefectTracking a
	where 1=1 
	and a.datetime >= @datefrom
	and a.datetime < @dateto ");

            if (sShift != "")
                strSql.AppendLine(" and a.shift = @shift");
                                    
            if (sJobNo != "")       
                strSql.AppendLine(" and a.jobID = @jobID");
                                    
            if (sPartNo != "")      
                strSql.AppendLine(" and a.partNumber = @partNumber");
                                    
            if (sPIC != "")     
                strSql.AppendLine(" and a.userID = @userID");

            strSql.AppendLine(@"
group by a.userID
) b on a.userID = b.userID

left join 
(
	select 
	userID
	,SUM(acceptQty) as Packing
	from PQCPackTracking 
	where 1=1 
	and datetime >= @datefrom
	and datetime < @dateto ");

            if (sShift != "")
                strSql.AppendLine(" and  shift = @shift");
                                    
            if (sJobNo != "")       
                strSql.AppendLine(" and  jobID = @jobID");
                                    
            if (sPartNo != "")      
                strSql.AppendLine(" and  partNumber = @partNumber");
                                    
            if (sPIC != "")     
                strSql.AppendLine(" and  userID = @userID");

            strSql.AppendLine(@"	
group by userID
) c on a.userID = c.userID

order by a.userID asc");



            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime,50),
                new SqlParameter("@dateTo", SqlDbType.DateTime,50),

                new SqlParameter("@shift", SqlDbType.VarChar,50),
                new SqlParameter("@jobID", SqlDbType.VarChar,50),
                new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                new SqlParameter("@userID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sShift != "") parameters[2].Value = sShift; else parameters[2] = null;
            if (sJobNo != "") parameters[3].Value = sJobNo; else parameters[3] = null;
            if (sPartNo != "") parameters[4].Value = sPartNo; else parameters[4] = null;
            if (sPIC != "") parameters[5].Value = sPIC; else parameters[5] = null;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            return ds;
        }



        #region PQC Production Chart
        internal DataTable GetYearlyProduct(string sPartNo, string sModel, string sCustomer, string sPIC)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    Year(a.day) as [Year]
                    ,sum(isnull(TotalQty,0)) as totalQuantity
                    ,sum(isnull(acceptQty,0)) as totalPass
                    ,sum(isnull(rejectQty,0)) as totalFail
                    ,convert(decimal(18,2), sum(convert(float, isnull( rejectQty,0))) / sum(convert(float, isnull(TotalQty,0))) * 100) as rejReate

                    from PQCQaViTracking a
                    left join PQCBom b on a.partNumber = b.partNumber

                    where 1=1 
                    and isnull(TotalQty,0) > 0 
                    and isnull(acceptQty,0) + isnull(rejectQty,0) > 0 ");


            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");
            
            if (sCustomer != "")
                strSql.AppendLine(" and b.customer = @customer ");

            if (sPIC != "")
                strSql.AppendLine(" and a.userID = @userID ");
            

            strSql.AppendLine(" group by Year(a.day) order by Year(a.day) asc ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@customer", SqlDbType.VarChar,50),
                new SqlParameter("@userID", SqlDbType.VarChar,50)
            };


            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sModel != "") parameters[1].Value = sModel; else parameters[1] = null;
            if (sCustomer != "") parameters[2].Value = sCustomer; else parameters[2] = null;
            if (sPIC != "") parameters[3].Value = sPIC; else parameters[3] = null;
            



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

        internal DataTable GetMonthlyProduct(int iYear, string sPartNo, string sModel, string sCustomer, string sPIC)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    a.Month
                    ,isnull( b.totalQuantity,0) as totalQuantity
                    ,isnull( b.totalPass,0) as totalPass
                    ,isnull( b.totalFail,0) as totalFail
                    ,isnull( b.rejReate,0) as rejReate
                    from (
	                    --拼出12个月的主表
	                    select 1  as SN ,'Jan' as [Month] union select 2  as SN ,'Feb' as [Month] union select 3  as SN ,'Mar' as [Month] union 
	                    select 4  as SN ,'Apr' as [Month] union select 5  as SN ,'May' as [Month] union select 6  as SN ,'Jun' as [Month] union 
	                    select 7  as SN ,'Jul' as [Month] union select 8  as SN ,'Aug' as [Month] union select 9  as SN ,'Sep' as [Month] union 
	                    select 10 as SN ,'Oct' as [Month] union select 11 as SN ,'Nov' as [Month] union select 12 as SN ,'Dec' as [Month]
                    ) a 
                    left join 
                    (
	                    select 
	                    Month(aa.day) as [Month]
	                    ,sum(isnull(aa.TotalQty,0)) as totalQuantity
	                    ,sum(isnull(aa.acceptQty,0)) as totalPass
	                    ,sum(isnull(aa.rejectQty,0)) as totalFail
	                    ,convert(decimal(18,2), sum(convert(float, isnull( aa.rejectQty,0))) / sum(convert(float, isnull(aa.TotalQty,0))) * 100) as rejReate

	                    from PQCQaViTracking aa
	                    left join PQCBom bb on aa.partNumber = bb.partNumber

	                    where 1=1
	                    and isnull(aa.TotalQty,0) > 0
	                    and isnull(aa.acceptQty,0) + isnull(aa.rejectQty,0) > 0
	                    and Year(aa.day) = @Year ");


            if (sPartNo != "")
                strSql.AppendLine(" and aa.partNumber = @PartNo  ");
            
            if (sModel != "")
                strSql.AppendLine(" and bb.Model = @Model ");
            
            if (sCustomer != "")
                strSql.AppendLine(" and bb.Customer = @Customer ");

            if (sPIC != "")
                strSql.AppendLine(" and aa.userID = @UserID ");



            strSql.Append(@"	
                        group by Month(aa.day)
                    ) b

                    on a.SN = b.Month 
                    order by a.SN  ");

            
            SqlParameter[] parameters = {
                new SqlParameter("@Year",SqlDbType.Int),
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Customer", SqlDbType.VarChar ,50),
                new SqlParameter("@UserID", SqlDbType.VarChar,50)
            };

            parameters[0].Value = iYear;
            if (sPartNo != "") parameters[1].Value = sPartNo; else parameters[1] = null;
            if (sModel != "") parameters[2].Value = sModel; else parameters[2] = null;
            if (sCustomer != "") parameters[3].Value = sCustomer; else parameters[3] = null;
            if (sPIC != "") parameters[4].Value = sPIC; else parameters[4] = null;



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

        internal DataTable GetDailyProduct(string sPartNo, string sModel, string sCustomer, string sPIC, DateTime dDateFrom, DateTime dDateTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    case 
	                    when month(a.day) = 1 then 'Jan-'
	                    when month(a.day) = 2 then 'Feb-'
	                    when month(a.day) = 3 then 'Mar-'
	                    when month(a.day) = 4 then 'Apr-'
	                    when month(a.day) = 5 then 'May-'
	                    when month(a.day) = 6 then 'Jun-'
	                    when month(a.day) = 7 then 'Jul-'
	                    when month(a.day) = 8 then 'Aug-'
	                    when month(a.day) = 9 then 'Sep-'
	                    when month(a.day) = 10 then 'Oct-'
	                    when month(a.day) = 11 then 'Nov-'
	                    when month(a.day) = 12 then 'Dec-' 
                    end + convert(varchar, Day(a.day)) as [Day]
                    ,sum(isnull(a.TotalQty,0)) as totalQuantity
                    ,sum(isnull(a.acceptQty,0)) as totalPass
                    ,sum(isnull(a.rejectQty,0)) as totalFail
                    ,convert(decimal(18,2), sum(convert(float, isnull(a.rejectQty,0))) / sum(convert(float, isnull(a.TotalQty,0))) * 100) as rejReate

                    from PQCQaViTracking a
                    left join PQCBom b on a.partNumber = b.partNumber

                    where 1=1
                    and isnull(a.TotalQty,0) > 0
                    and isnull(a.acceptQty,0) + isnull(a.rejectQty,0) > 0
                    and a.datetime >= @dDateFrom
                    and a.datetime < @dDateTo  ");


            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");

            if ( sCustomer!= "")
                strSql.AppendLine(" and b.Customer = @Customer ");

            if (sPIC != "")
                strSql.AppendLine(" and a.UserID = @UserID ");

            

            strSql.AppendLine(" group by Month(a.day), Day(a.day) order by Month(a.day) asc, Day(a.day) asc  ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Customer", SqlDbType.VarChar,50),
                new SqlParameter("@UserID",SqlDbType.VarChar,50),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
            };

            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sModel != "") parameters[1].Value = sModel; else parameters[1] = null;
            if (sCustomer != "") parameters[2].Value = sCustomer; else parameters[2] = null;
            if (sPIC != "") parameters[3].Value = sPIC; else parameters[3] = null;
            parameters[4].Value = dDateFrom;
            parameters[5].Value = dDateTo;
            



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
        
        internal DataTable GetProductGroupByReportType(string sReportType, string sPartNo, string sModel, string sCustomer, string sPIC, DateTime dDateFrom, DateTime dDateTo)
        {
            string sGroupBy = "";
            if (sReportType == "PartNumber")
            {
                sGroupBy = "a.PartNumber";
            }
            else if (sReportType == "Model")
            {
                sGroupBy = "b.Model";
            }
            else if (sReportType =="Customer")
            {
                sGroupBy = "b.Customer";
            }
            else if (sReportType == "Type")
            {
                sGroupBy = "b.Type";
            }
            else if (sReportType == "PIC")
            {
                sGroupBy = "a.userID";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
                                select 
                                {0}
                                ,sum(isnull(a.TotalQty,0)) as totalQuantity
                                ,sum(isnull(a.acceptQty,0)) as totalPass
                                ,sum(isnull(a.rejectQty,0)) as totalFail
                                ,convert(decimal(18,2), sum(convert(float, isnull( a.rejectQty,0))) / sum(convert(float, isnull(a.TotalQty,0))) * 100) as rejReate

                                from PQCQaViTracking a 
                                left join PQCBom b on a.partNumber = b.partNumber
                                where 1=1
                                and isnull(a.TotalQty,0) > 0
                                and isnull(a.acceptQty,0) + isnull(a.rejectQty,0) > 0 
                                and a.datetime > @dDateFrom
                                and a.datetime < @dDateTo ", sGroupBy);
            
            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");

            if (sCustomer != "")
                strSql.AppendLine(" and b.Customer = @Customer ");

            if (sPIC != "")
                strSql.AppendLine(" and a.UserID = @UserID ");

            strSql.AppendFormat(" group by {0}  order by {1} asc  ", sGroupBy, sGroupBy);


            SqlParameter[] parameters = {
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Customer", SqlDbType.VarChar,50),
                new SqlParameter("@UserID", SqlDbType.VarChar,50)
            };


            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sPartNo != "") parameters[2].Value = sPartNo; else parameters[2] = null;
            if (sModel != "") parameters[3].Value = sModel; else parameters[3] = null;
            if (sCustomer != "") parameters[4].Value = sCustomer; else parameters[4] = null;
            if (sPIC != "") parameters[5].Value = sPIC; else parameters[5] = null;


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

        internal DataTable GetProductGroupByReject(string sPartNo, string sModel, string sCustomer, string sPIC, DateTime dDateFrom, DateTime dDateTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select  
                    b.defectDescription
                    ,case 
	                when b.defectDescription = 'Mould' then 1 
	                when b.defectDescription = 'Paint' then 2 
	                when b.defectDescription='Laser' then 3 
	                when b.defectDescription = 'Others' then 4 end as ID  --只是用来排序而已

                    ,sum(a.TotalQty) as totalQuantity
                    ,sum(isnull(a.acceptQty,0)) as totalPass
                    ,sum(b.RejQty) as totalFail
                    ,round(sum(b.RejQty) / sum(a.TotalQty) *100, 2) as rejReate
                  

                    from PQCQaViTracking a 
                    left join 
                    (
	                    select 
	                    trackingID
	                    ,defectDescription
	                    ,sum(isnull(rejectQty,0)) as RejQty
	                    from PQCQaViDefectTracking 
	                    group by trackingID, defectDescription
                    ) b on a.trackingID  = b.trackingID 

                    where isnull(a.TotalQty,0) > 0 and isnull(a.acceptQty,0) + isnull(a.rejectQty,0) > 0
                    and a.datetime > @dDateFrom and a.dateTime < @dDateTo");


            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");

            if (sCustomer != "")
                strSql.AppendLine(" and b.Customer = @Customer ");

            if (sPIC != "")
                strSql.AppendLine(" and a.UserID = @UserID ");



            strSql.AppendLine(" group by b.defectDescription order by ID asc ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50),
                new SqlParameter("@Customer", SqlDbType.VarChar,50),
                new SqlParameter("@UserID",SqlDbType.VarChar,50),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
            };

            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sModel != "") parameters[1].Value = sModel; else parameters[1] = null;
            if (sCustomer != "") parameters[2].Value = sCustomer; else parameters[2] = null;
            if (sPIC != "") parameters[3].Value = sPIC; else parameters[3] = null;
            parameters[4].Value = dDateFrom;
            parameters[5].Value = dDateTo;




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

        #endregion



        public DataTable GetOuput(DateTime DateFrom, DateTime DateTo, string Shift, string DateNotIn, bool ExceptWeekends)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
sum(a.TotalOuput) as TotalOutput
,sum(a.TotalRej) as TotalRej
from 
(
	select 
	1 as id
	,isnull(TotalQty,0) as TotalOuput
	,isnull(rejectQty,0) as TotalRej
	from PQCQaViTracking 
	where datetime >= @dateFrom and datetime < @dateTo");
            if (Shift != "")
            {
                strSql.Append(" and shift = @shift ");
            }

            if (DateNotIn != "")
            {
                strSql.Append(" and day(day) not in (");

                string[] strArrDate = DateNotIn.Split(',');
                foreach (string date in strArrDate)
                {
                    if (Common.CommFunctions.isNumberic(date))
                        strSql.Append(" '" + date + "', ");
                }
                strSql.Remove(strSql.Length, 1);

                strSql.Append(" ) ");
            }

            if (ExceptWeekends)
            {
                strSql.Append("  DATEPART(WEEKDAY,datetime) not in (1,7) ");
            }



            strSql.Append(" ) a group by a.id ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16)
            };


            paras[0].Value = DateFrom;
            paras[1].Value = DateTo;
            if (Shift != "")
            {
                paras[2].Value = DateTo;
            }else
            {
                paras[2] = null;
            }
           


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }




        public DataTable GetOnlineDayOutput(DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select 
SUM(a.TotalQty) as Output_PCS
,CONVERT(decimal(18,0), ROUND( SUM(a.TotalQty / b.materialCount ),0)) as Output_SET
from pqcqavitracking a 
left join (select partnumber, count(1) as materialCount from PQCBomDetail group by partNumber) b 
on a.partNumber = b.partnumber
where a.machineID in (1,2,3,4,5,6,7,8) and day = @day
group by a.day ");



            SqlParameter[] parameters = {
                new SqlParameter("@day", SqlDbType.DateTime)
            };

            parameters[0].Value = dDay;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        
        public DataTable GetWIPDayOutput(DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select 
SUM(a.TotalQty) as Output_PCS
,CONVERT(decimal(18,0), ROUND( SUM(a.TotalQty / b.materialCount),0)) as Output_SET
from pqcqavitracking a 
left join (select partnumber, count(1) as materialCount from PQCBomDetail group by partNumber) b 
on a.partNumber = b.partnumber
where a.machineID in (11,12,13,14,15,16,17,21,22,23,24,25) and day = @day
group by a.day ");



            SqlParameter[] parameters = {
                new SqlParameter("@day", SqlDbType.DateTime)
            };

            parameters[0].Value = dDay;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }



        public DataTable GetRealTimeForOnline(DateTime day)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.AppendFormat(@"
with paintHis as (
    select jobNumber, LotNo, PartNumber, convert(float,inquantity) as mrpTotal
    from OPENDATASOURCE('SQLOLEDB',{0}).Taiyo_Painting.dbo.PaintingDeliveryHis
    where datetime >= DATEADD(day, -30, @day) 
)", StaticRes.Global.SqlConnection.SqlconnPainting);

            
            strSql.Append(@"
select 
a.machineID
,a.nextViFlag
,b.lotNo
,a.jobId
,a.partNumber 
,ISNULL(b.mrpTotal,0) as mrpTotal
,a.acceptQty
,a.rejectQty
,userID
,dateTime

from PQCQaViTracking a
left join paintHis b 
on a.jobId collate Chinese_PRC_CI_AS = b.jobNumber collate Chinese_PRC_CI_AS
where 1=1 and machineID in (1,2,3,4,5,6,7,8)
and a.day=@day ");
            

            SqlParameter[] paras ={new SqlParameter("@day",SqlDbType.DateTime)};
            paras[0].Value = day;
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }

        public DataTable GetRealTimeForWIP(DateTime day)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.AppendFormat(@"
with paintHis as (
    select jobNumber, LotNo, PartNumber, convert(float,inquantity) as mrpTotal
    from OPENDATASOURCE('SQLOLEDB',{0}).Taiyo_Painting.dbo.PaintingDeliveryHis
    where datetime >= DATEADD(day, -30, @day) 
)", StaticRes.Global.SqlConnection.SqlconnPainting);


            strSql.Append(@"
select 
a.machineID
,a.nextViFlag
,b.lotNo
,a.jobId
,a.partNumber 
,ISNULL(b.mrpTotal,0) as mrpTotal
,a.acceptQty
,a.rejectQty
,a.TotalQty
,userID
,dateTime
from PQCQaViTracking a
left join paintHis b 
on a.jobId collate Chinese_PRC_CI_AS = b.jobNumber collate Chinese_PRC_CI_AS
where 1=1 and machineID in (11,13,14,15,16,17)
and a.day=@day ");


            SqlParameter[] paras ={new SqlParameter("@day",SqlDbType.DateTime)};
            paras[0].Value = day;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable GetRealTimeForPack(DateTime day)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.AppendFormat(@"
with paintHis as (
    select jobNumber, LotNo, PartNumber, convert(float,inquantity) as mrpTotal
    from OPENDATASOURCE('SQLOLEDB',{0}).Taiyo_Painting.dbo.PaintingDeliveryHis
    where datetime >= DATEADD(day, -30, @day) 
)", StaticRes.Global.SqlConnection.SqlconnPainting);


            strSql.Append(@"
select 
a.machineID
,a.nextViFlag
,b.lotNo
,a.jobId
,a.partNumber 
,ISNULL(b.mrpTotal,0) as mrpTotal
,a.acceptQty
,a.rejectQty
,a.TotalQty
,userID
,dateTime
from PQCPackTracking a
left join paintHis b 
on a.jobId collate Chinese_PRC_CI_AS = b.jobNumber collate Chinese_PRC_CI_AS
where 1=1 and machineID in (12,21,22,23,24,25)
and a.day=@day ");


            SqlParameter[] paras ={new SqlParameter("@day",SqlDbType.DateTime)};
            paras[0].Value = day;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

      

        public SqlCommand UpdatePQCMaintenance(Common.Class.Model.PQCQaViTracking model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" update PQCQaViTracking set
                                targetQty = @targetQty,
                                totalQty = @totalQty, 
                                acceptQty = @acceptQty, 
                                rejectQty = @rejectQty,
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
                new SqlParameter("@rejectQty", SqlDbType.Decimal),
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
            parameters[4].Value = model.rejectQty;


            parameters[5].Value = model.status;
            parameters[6].Value = model.nextViFlag;

            parameters[7].Value = model.lastUpdatedTime;
            parameters[8].Value = model.updatedTime;
            parameters[9].Value = model.remarks;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }




        public DataTable GetBuyoffJobList(DateTime? dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select jobId from pqcqavitracking where 1=1 and nextviflag = 'True'  ");

            if (dDay != null) strSql.Append("  and day = @day ");

            strSql.Append(" order by jobId asc ");

            SqlParameter[] parameters = {
                new SqlParameter("@day", SqlDbType.DateTime)
            };



            if (dDay != null) parameters[0].Value = dDay.Value; else parameters[0] = null;



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



        public DataTable GetWIPOutputForAllInventoryReport(DateTime dStartTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 

a.partnumber
,b.materialname
,sum(b.passQty) as passQty
,sum(b.rejectQty) as rejectQty

from PQCQaViTracking a
left join pqcqavidetailtracking b on a.trackingid = b.trackingid 
left join pqcbom c on a.partnumber = c.partnumber


where 1=1 
and a.day >= '2020-6-1'

--如果不是最后一道check工序 , 或者 nextviflag false的,  (做了但没做完的)都算作是after wip
and 
(
    a.processes != (case	when c.processes like '%Check#3' then 'CHECK#3'  when c.processes like '%Check#2' then 'CHECK#2' else 'CHECK#1' end)
    or 
    a.nextviflag != 'true' 
)

group by a.partNumber, b.materialName ");




            SqlParameter[] parameters = {
                new SqlParameter("@startTime", SqlDbType.DateTime)
            };
            parameters[0].Value = dStartTime;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }


        public DataTable GetCheckingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
with defectList as (
	select
	trackingID
	,isnull(sum( case when defectDescription = 'Mould' then rejectQty end),0) as MouldRej
	,isnull(sum( case when defectDescription = 'Paint' then rejectQty end),0) as PaintRej
	,isnull(sum( case when defectDescription = 'Laser' then rejectQty end),0) as LaserRej
	,isnull(sum( case when defectDescription = 'Others' then rejectQty end),0) as OthersRej
	from PQCQaViDefectTracking 
	where day >= @dateFrom and day < @dateTo
");

            if (sPartNo != "") strSql.Append(" and partnumber= @partnumber ");
            if (sStation != "") strSql.Append(" and machineID= @machineID ");
            if (sPIC != "") strSql.Append(" and userID= @userID ");
            if (sJobNo != "") strSql.Append(" and jobId= @jobId ");


            strSql.Append(" group by trackingID ) ");





            strSql.Append(@"
select 
a.trackingid
,day
,shift
,model
,partnumber 
,machineid
,processes
,jobId
,TotalQty
,acceptQty
,rejectQty
,userid

,b.mouldrej
,b.paintrej
,b.laserrej
,b.OthersRej

,a.startTime
,a.stopTime

,a.datetime

from PQCQaViTracking a
left join defectList b on a.trackingID = b.trackingID
where day >= @dateFrom and day < @dateTo ");

            if (sPartNo != "") strSql.Append(" and partnumber= @partnumber ");
            if (sStation != "") strSql.Append(" and machineID= @machineID ");
            if (sPIC != "") strSql.Append(" and userID= @userID ");
            if (sJobNo != "") strSql.Append(" and jobId= @jobId ");


            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime),
                new SqlParameter("@dateTo", SqlDbType.DateTime),
                new SqlParameter("@partnumber", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userID", SqlDbType.VarChar),
                new SqlParameter("@jobId", SqlDbType.VarChar)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sPartNo != "") parameters[2].Value = sPartNo; else parameters[2] = null;
            if (sStation != "") parameters[3].Value = sStation; else parameters[3] = null;
            if (sPIC != "") parameters[4].Value = sPIC; else parameters[4] = null;
            if (sJobNo != "") parameters[5].Value = sJobNo; else parameters[5] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }



        public DataTable GetCheckingDateByJob(string sJob)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from PQCQaViTracking where jobid= @jobid order by datetime desc");
            
            SqlParameter[] parameters = {
                new SqlParameter("@jobid", SqlDbType.VarChar,50)
            };

            parameters[0].Value = sJob;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
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
,case when  CHARINDEX('Laser',c.processes,0) > 0 and a.processes = 'CHECK#1' 
	then 'Laser'
	else 'WIP' 
end as Process
,b.MouldRej
,b.PaintRej
,b.LaserRej
,b.OthersRej
,rejectQty
,acceptQty
,rejectQty *  isnull( c.unitCost,0) as rejPrice
,Upper(userID) as userID
,d.materialCount
from PQCQaViTracking a
left join (
	select 
	trackingID
	,isnull(sum(case when defectDescription = 'Mould' then isnull(rejectQty,0) end),0) as MouldRej
	,isnull(sum(case when defectDescription = 'Paint' then isnull(rejectQty,0) end) ,0) as PaintRej
	,isnull(sum(case when defectDescription = 'Laser' then isnull(rejectQty,0) end) ,0) as LaserRej
	,isnull(sum(case when defectDescription = 'Others' then isnull(rejectQty,0) end),0)  as OthersRej
	from PQCQaViDefectTracking
    where day = @date
	group by trackingID
) b on a.trackingID = b.trackingID
left join PQCBom c on a.partNumber = c.partNumber
left join  (
    select partNumber , count(1) as materialCount 
    from PQCBomDetail 
    group by partnumber 
) d  on c.partNumber = d.partNumber
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



    }
}



