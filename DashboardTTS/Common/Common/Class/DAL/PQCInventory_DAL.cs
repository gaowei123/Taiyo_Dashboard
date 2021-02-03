using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class PQCInventory_DAL
    {

        public bool Add(Common.Class.Model.PQCInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCInventory(");
            strSql.Append("LotNo,JobNumber,PartNumber,InMRPQuantity,MFGDate,Remark,UpdatedTime,InventoryType, checkProcess)");
            strSql.Append(" values (");
            strSql.Append("@LotNo,@JobNumber,@PartNumber,@InMRPQuantity,@MFGDate,@Remark,@UpdatedTime,@InventoryType, @checkProcess)");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@JobNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@InMRPQuantity", SqlDbType.Decimal,9),
                    new SqlParameter("@MFGDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@UpdatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@InventoryType", SqlDbType.VarChar,50),
                    new SqlParameter("@checkProcess", SqlDbType.VarChar,32)};
            parameters[0].Value = model.LotNo == null ? (object)DBNull.Value : model.LotNo;
            parameters[1].Value = model.JobNumber == null ? (object)DBNull.Value : model.JobNumber;
            parameters[2].Value = model.PartNumber == null ? (object)DBNull.Value : model.PartNumber;
            parameters[3].Value = model.InMRPQuantity == null ? (object)DBNull.Value : model.InMRPQuantity;
            parameters[4].Value = model.MFGDate == null ? (object)DBNull.Value : model.MFGDate;
            parameters[5].Value = model.Remark == null ? (object)DBNull.Value : model.Remark;
            parameters[6].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime;
            parameters[7].Value = model.InventoryType == null ? (object)DBNull.Value : model.InventoryType;
            parameters[8].Value = model.CheckProcess == null ? (object)DBNull.Value : model.CheckProcess;

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Update(Common.Class.Model.PQCInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCInventory set ");
            strSql.Append("LotNo=@LotNo,");
            strSql.Append("JobNumber=@JobNumber,");
            strSql.Append("PartNumber=@PartNumber,");
            strSql.Append("InMRPQuantity=@InMRPQuantity,");
            strSql.Append("MFGDate=@MFGDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdatedTime=@UpdatedTime,");
            strSql.Append("InventoryType=@InventoryType");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LotNo", SqlDbType.VarChar,1),
                    new SqlParameter("@JobNumber", SqlDbType.VarChar,1),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar,1),
                    new SqlParameter("@InMRPQuantity", SqlDbType.Decimal,9),
                    new SqlParameter("@MFGDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Remark", SqlDbType.VarChar,1),
                    new SqlParameter("@UpdatedTime", SqlDbType.DateTime,8),
                    new SqlParameter("@InventoryType", SqlDbType.VarChar,1)};
            parameters[0].Value = model.LotNo;
            parameters[1].Value = model.JobNumber;
            parameters[2].Value = model.PartNumber;
            parameters[3].Value = model.InMRPQuantity;
            parameters[4].Value = model.MFGDate;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.UpdatedTime;
            parameters[7].Value = model.InventoryType;

            return  DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public int UpdateMFGDate(string jobNumber, DateTime MFGDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCInventory set ");
            strSql.Append("MFGDate=@MFGDate");
            strSql.Append(" where ");
            strSql.Append("Jobnumber=@Jobnumber");
            SqlParameter[] parameters = {
                new SqlParameter("@MFGDate", SqlDbType.DateTime,8),
                new SqlParameter("@JobNumber", SqlDbType.VarChar,32),

            };
            parameters[0].Value = MFGDate;
            parameters[1].Value = jobNumber;
         

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public DataSet GetList(string sJobNumber, string sProcess)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM PQCInventory where 1=1  ");

            if (!string.IsNullOrEmpty(sJobNumber))
            {
                strSql.Append(" and jobNumber = @jobNumber ");
            }

            if (!string.IsNullOrEmpty(sProcess))
            {
                strSql.Append(" and CheckProcess = @CheckProcess ");
            }

            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar),
                new SqlParameter("@CheckProcess",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sJobNumber)) parameters[0].Value = sJobNumber; else parameters[0] = null;
            if (!string.IsNullOrEmpty(sProcess)) parameters[1].Value = sProcess; else parameters[1] = null;




            return DBHelp.SqlDB.Query(strSql.ToString(),parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public int Delete(string sJobNo)
        {
            if (sJobNo.Trim() == "")
            {
                return 0;
            }


            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from PQCInventory where JobNumber = @jobNumber ");
         
     
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar)
            };
            parameters[0].Value = sJobNo;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public DataTable GetWIPInventoryReport(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sModel, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
	 b.customer
	,b.model
	,b.partNumber
	,a.jobNumber
	,a.MFGDate
	,case when (select 1 from PQCBom where partNumber = a.PartNumber) is null then 'False' else 'True' end as BomFlag
	,case	when ISNULL(c.TotalQtyPCS,0) = 0
				then 'Pending'
			when ISNULL(c.TotalQtyPCS,0) > 0 and ISNULL(c.TotalQtyPCS,0) < a.InMrpQuantity * b.materialCount
				then 'Inprocess' 
			when ISNULL(c.TotalQtyPCS,0) = a.InMrpQuantity * b.materialCount
				then 'Complete' 
	end as jobStatus



	,a.InMRPQuantity as MrpQtySET
	,a.InMrpQuantity * b.materialCount as MrpQtyPCS

	,a.InMRPQuantity - ISNULL(d.totalQtySET,0) as BeforeSET
	,a.InMrpQuantity * b.materialCount - ISNULL(c.TotalQtyPCS,0) as BeforePCS

	,ISNULL(d.totalQtySET,0) as AfterSET
	,ISNULL(c.TotalQtyPCS,0) as AfterPCS

from PQCInventory a 

left join
	(
		select 
			a.partNumber
			,a.customer
			,a.model
			,b.materialCount 
		from PQCBom a left join (select partNumber , count(1) as materialCount from PQCBomDetail group by partNumber) b 
		on a.partNumber = b.partNumber 
	) b 
on b.partNumber = a.PartNumber


left join 
	(
		--vi tracking按照jobid, processes分组, 汇总total qty作为 PCS output
		select 
			jobId
			,processes
			,sum(totalQty) as TotalQtyPCS
		from PQCQaViTracking 
		group by jobId, processes
	) c
on a.jobnumber = c.jobId and a.CheckProcess = c.processes

left join
	(
		--detail tracking按照jobid, processes分组 取material part no数量最小的为  SET output
		select 
		jobId
		,processes
		,min(totalQty) as totalQtySET
		from 
			(
				select
					jobId
					,materialPartNo
					,processes
					,Sum(passQty + rejectQty) as totalQty
				from PQCQaViDetailTracking
				group by jobId, materialPartNo, processes
			) a
		group by a.jobId, a.processes
	) d 
on a.JobNumber = d.jobId and a.CheckProcess = d.processes

where 1=1   ");

            if (dDateFrom != null)
                strSql.Append(" and a.updatedTime >= @dateFrom ");
            else
                strSql.Append(" and a.UpdatedTime > '2020-1-1' ");


            if (dDateTo != null) strSql.Append(" and a.updatedTime < @dateTo");

            if (sPartNo != "") strSql.Append(" and a.partNumber = @partNumber");

            if (sModel != "") strSql.Append(" and b.model = @model");

            if (sJobNo != "") strSql.Append(" and a.jobNumber = @jobNumber");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom", SqlDbType.DateTime),
                new SqlParameter("@dateTo", SqlDbType.DateTime),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@model", SqlDbType.VarChar),
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };


            if (dDateFrom != null) paras[0].Value = dDateFrom; else paras[0] = null;
            if (dDateTo != null) paras[1].Value = dDateTo; else paras[1] = null;
            if (sPartNo != "") paras[2].Value = sPartNo; else paras[2] = null;
            if (sModel != "") paras[3].Value = sModel; else paras[3] = null;
            if (sJobNo != "") paras[4].Value = sJobNo; else paras[4] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count ==0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }
        
        public DataTable GetInventoryForAllSectionReport(DateTime dStartTime)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
with jobProcess as (
	select
	jobid
	,max(a.processes) as curProcess
	,case when  max(nextviflag) = 'true' and    max(a.processes) = (case when b.processes like '%Check#3%' then 'CHECK#3' when b.processes like '%Check#2%' then 'CHECK#2' else 'CHECK#1' end)
	      then 'true'  else 'false' 
     end as completeFlag
	from pqcqavitracking a left join pqcbom b on a.partnumber = b.partnumber 
	where a.day > @startTime
	group by jobid, b.processes
) ");


            strSql.Append(@"
select

a.PartNumber
,b.materialName
,sum(a.InMRPQuantity) as InMRPQuantity
,sum(isnull( d.passqty,0) ) as passqty
,sum(isnull( d.rejectqty,0) ) as rejectqty
,sum(a.InMRPQuantity) - sum(isnull( d.passqty,0) ) - sum(isnull( d.rejectqty,0) ) as inventoryQty



from PQCInventory a 
left join PQCBomDetail b on a.partnumber = b.partnumber
left join jobProcess c on c.jobId = a.JobNumber
left join PQCQaViDetailTracking d on d.jobId = a.JobNumber and d.processes = a.CheckProcess

where a.updatedTime > @startTime 
and
(
	--完全没动工的
	c.completeFlag is null

	--做一部分没完成的
	or c.completeFlag = 'false'
)
group by a.PartNumber, b.materialname ");

        
            

            SqlParameter[] paras =
            {
                new SqlParameter("@startTime", SqlDbType.DateTime)
            };
            paras[0].Value = dStartTime;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }


    }
}
