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

        public SqlCommand AddCMD(Common.Class.Model.PQCInventory_Model model)
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


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
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


        /// <summary>
        /// 获得数据列表
        /// </summary>
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

        public DataSet GetList(string sJobNumber, string sPartNumber, string sStatus, DateTime? dDateFrom, DateTime? dDateTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM PQCInventory where 1=1 ");

            if (!string.IsNullOrEmpty(sJobNumber))
            {
                strSql.Append(" and jobNumber = @jobNumber ");
            }

            if (!string.IsNullOrEmpty(sPartNumber))
            {
                strSql.Append(" and partNumber = @partNumber ");
            }

            if (!string.IsNullOrEmpty(sStatus))
            {
                //strSql.Append(" partNumber = @partNumber ");
            }

            if (dDateFrom != null)
            {
                strSql.Append(" and inDate >= @dDateFrom ");
            }

            if (dDateTo != null)
            {
                strSql.Append(" and inDate < @dDateTo ");
            }


            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar),
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime)
            };


            if (!string.IsNullOrEmpty(sJobNumber)) { parameters[0].Value = sJobNumber;  } else { parameters[0] = null; }
            if (!string.IsNullOrEmpty(sPartNumber)) {  parameters[1].Value = sPartNumber; } else { parameters[1] = null; }
            if (dDateFrom != null) { parameters[2].Value = dDateFrom; }  else { parameters[2] = null; }
            if (dDateTo != null) { parameters[3].Value = dDateTo; } else { parameters[3] = null; }


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


        public DataSet InventoryReport(DateTime dDateFrom)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@"

                        select
                        ISNULL((select top 1 customer from pqcbom where partnumber = a.partnumber), 'Unknow') as customer
                        ,ISNULL((select top 1 model from pqcbom where partnumber = a.partnumber) , 'Unknow') as model
                        ,a.partNumber

                        ,case when count(1) = 1
                        then (
                         select top 1 JobNumber from PQCInventory aa
                         left join PQCQaViTracking bb on aa.JobNumber = bb.jobId
                         where  aa.UpdatedTime > @DateFrom
                         and aa.PartNumber = a.PartNumber
                            and isnull(bb.totalqty,0) < aa.InMRPQuantity * isnull( avg(d.materialCount),1) 
                        ) 
                        else 'JOT###'
                        end as JobNumber


                        , CONVERT(varchar, sum(a.InMRPQuantity)) + '('
                        + convert(varchar, sum(a.InMRPQuantity) * avg(isnull(d.materialCount, 1))) + ')'
                        as MRPQty

                        , CONVERT(varchar, sum(a.InMRPQuantity) - SUM(isnull(round(b.totalQty / d.materialCount, 0), 0))) + '('
                        + CONVERT(varchar, sum(a.InMRPQuantity) * AVG(ISNULL(d.materialCount, 1)) - SUM(isnull(b.totalQty, 0))) + ')'
                        as Before

                        , CONVERT(varchar, CONVERT(float, sum(isnull(b.totalQty / d.materialCount, 0)))) + '('
                        + CONVERT(varchar, sum(isnull(b.totalQty, 0))) + ')'
                        as After

                        , Count(1) as JobCount

                        ,case when count(1) = 1
                        then ( convert(varchar(50), 
                         (select top 1 MFGDate from PQCInventory aa
                         left join PQCQaViTracking bb on aa.JobNumber = bb.jobId
                         where  aa.UpdatedTime > @DateFrom
                         and aa.PartNumber = a.PartNumber
                         and isnull(bb.totalqty,0) < aa.InMRPQuantity * isnull( avg(d.materialCount),1) ) , 3)
                        ) 
                        end as MFGDate

                        ,case when AVG(d.materialCount) is null then 'No' else 'Yes' end as BomExistFlag


                        from PQCInventory a

                        left join (
                        select
	                    aa.jobid,
	                    aa.partnumber,
	                    aa.processes,
	                    sum(aa.totalqty) as totalQty,
	                    sum(aa.acceptqty) as acceptQty,
	                    sum(aa.rejectQty) as rejectQty

	                    from PQCQaViTracking aa
	                    where aa.day <= '2019-12-5'
	                    group by aa.jobId, aa.partNumber, aa.processes


	                    union 

	                    select
	                    aa.jobid,
	                    aa.partnumber,
	                    aa.processes,
	                    sum(aa.totalqty) as totalQty,
	                    sum(aa.acceptqty) as acceptQty,
	                    sum(aa.rejectQty) as rejectQty

	                    from PQCQaViTracking aa left join PQCBom bb on aa.partNumber = bb.partNumber
	                    where aa.day > '2019-12-5' and ( (aa.processes = 'Check#2')  or (aa.processes = 'Check#1' and  bb.processes not like '%Laser%') )
	                    group by aa.jobId, aa.partNumber, aa.processes
                        ) b on a.JobNumber = b.jobId

                        left join PQCBom c on a.PartNumber = c.partNumber
                        left join (select partNumber, count(1) as materialCount from PQCBomDetail group by partNumber  ) d on a.PartNumber = d.partNumber

                        where 1=1  
                        and a.UpdatedTime > @DateFrom
                        and isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1) 

                        group by a.PartNumber

                        order by customer, a.PartNumber ");


            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom",SqlDbType.DateTime)
            };

            parameters[0].Value = dDateFrom;




            return DBHelp.SqlDB.Query(strSql.ToString(), parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
        

        public DataSet InventoryDetailReport(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPartNo, string sStatus)
        {
            StringBuilder strSql = new StringBuilder();

            #region old sql

            //            strSql.Append(@"
            //select
            //ROW_NUMBER() over(order by customer, a.PartNumber) as sn
            //,c.customer
            //,c.model
            //,a.JobNumber
            //,a.partNumber
            //,convert(varchar(50), a.MFGDate,3) as MFGDate

            //, CONVERT(varchar, (a.InMRPQuantity)) + '('
            //+ convert(varchar, (a.InMRPQuantity) * (isnull(d.materialCount, 1))) + ')'
            //as MRPQty

            //, CONVERT(varchar, convert(float, a.InMRPQuantity - isnull(round(b.totalQty / isnull(d.materialCount,1) , 0), 0))) + '('
            //+ CONVERT(varchar, (a.InMRPQuantity) * (ISNULL(d.materialCount, 1)) - (isnull(b.totalQty, 0))) + ')'
            //as Before

            //, CONVERT(varchar, CONVERT(float, (isnull(b.totalQty / d.materialCount, 0)))) + '('
            //+ CONVERT(varchar, (isnull(b.totalQty, 0))) + ')'
            //as After



            //, case when isnull(b.totalqty, 0) <= 0
            //then 'Pending'

            //when isnull(b.totalqty,0) > 0 and ISNULL(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1) 
            //then 'InProcess'

            //when ISNULL(b.totalqty,0) >=  a.InMRPQuantity * isnull(d.materialCount,1) 
            //then 'Complete'

            //when isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1) 
            //then 'NoComplete'

            //end as Status



            //,case when d.materialCount is null then 'No' else 'Yes' end as BomExistFlag
            //,a.UpdatedTime


            //from PQCInventory a

            //left join (
            //select
            //aa.jobid,
            //aa.partnumber,
            //sum(aa.totalqty) as totalQty,
            //sum(aa.acceptqty) as acceptQty,
            //sum(aa.rejectQty) as rejectQty

            //from PQCQaViTracking aa
            //group by aa.jobId, aa.partNumber

            //) b on a.JobNumber = b.jobId

            //left join PQCBom c on a.PartNumber = c.partNumber
            //left join (select partNumber, count(1) as materialCount from PQCBomDetail group by partNumber  ) d on a.PartNumber = d.partNumber

            //where 1=1  
            //and a.UpdatedTime > @DateFrom  and a.UpdatedTime < @DateTo");

            #endregion

            strSql.Append(@"
select
ROW_NUMBER() over(order by customer, a.PartNumber) as sn
,c.customer
,c.model
,a.JobNumber
,a.partNumber
,convert(varchar(50), a.MFGDate,3) as MFGDate

, CONVERT(varchar, (a.InMRPQuantity)) + '('
+ convert(varchar, (a.InMRPQuantity) * (isnull(d.materialCount, 1))) + ')'
as MRPQty
, CONVERT(varchar, convert(float, a.InMRPQuantity - isnull(round(b.totalQty / isnull(d.materialCount,1) , 0), 0))) + '('
+ CONVERT(varchar, (a.InMRPQuantity) * (ISNULL(d.materialCount, 1)) - (isnull(b.totalQty, 0))) + ')'
as Before
, CONVERT(varchar, CONVERT(float, (isnull(b.totalQty / d.materialCount, 0)))) + '('
+ CONVERT(varchar, (isnull(b.totalQty, 0))) + ')'
as After

, case when isnull(b.totalqty, 0) <= 0
then 'Pending'
when isnull(b.totalqty,0) > 0 and ISNULL(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1) 
then 'InProcess'
when ISNULL(b.totalqty,0) >=  a.InMRPQuantity * isnull(d.materialCount,1) 
then 'Complete'
when isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1) 
then 'NoComplete'
end as Status

,case when d.materialCount is null then 'No' else 'Yes' end as BomExistFlag
,a.UpdatedTime


from PQCInventory a

left join (
	select
	aa.jobid,
	aa.partnumber,
	aa.processes,
	sum(aa.totalqty) as totalQty,
	sum(aa.acceptqty) as acceptQty,
	sum(aa.rejectQty) as rejectQty

	from PQCQaViTracking aa
	where aa.day <= '2019-12-5'
	group by aa.jobId, aa.partNumber, aa.processes


	union 

	select
	aa.jobid,
	aa.partnumber,
	aa.processes,
	sum(aa.totalqty) as totalQty,
	sum(aa.acceptqty) as acceptQty,
	sum(aa.rejectQty) as rejectQty

	from PQCQaViTracking aa left join PQCBom bb on aa.partNumber = bb.partNumber
	where aa.day > '2019-12-5' and ( (aa.processes = 'Check#2')  or (aa.processes = 'Check#1' and  bb.processes not like '%Laser%') )
	group by aa.jobId, aa.partNumber, aa.processes


) b on a.JobNumber = b.jobId

left join PQCBom c on a.PartNumber = c.partNumber
left join (select partNumber, count(1) as materialCount from PQCBomDetail group by partNumber  ) d on a.PartNumber = d.partNumber

where 1=1  
and a.UpdatedTime > @DateFrom  
and a.UpdatedTime < @DateTo 
and isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1)
");


            if (sStatus == "Pending")
            {
                strSql.Append(" and isnull(b.totalqty, 0) <= 0 ");
            }
            else if (sStatus == "InProcess")
            {
                strSql.Append(" and isnull(b.totalqty,0) > 0 and ISNULL(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1)  ");
            }
            else if (sStatus == "Complete")
            {
                strSql.Append(" and ISNULL(b.totalqty,0) >=  a.InMRPQuantity * isnull(d.materialCount,1)   ");
            }
            else if (sStatus == "NoComplete")
            {
                strSql.Append(" and isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1)   ");
            }

            if (sJobNo.Trim() != "")
            {
                strSql.Append(" and a.JobNumber = @JobNo");
            }
            if (sPartNo.Trim() != "")
            {
                strSql.Append(" and a.PartNumber = @PartNo ");
            }


            strSql.Append(" order by customer, a.PartNumber");





            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@JobNo", SqlDbType.VarChar,50),
                new SqlParameter("@PartNo",SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sJobNo != "") parameters[2].Value = sJobNo; else parameters[2] = null;
            if (sPartNo != "") parameters[3].Value = sPartNo; else parameters[3] = null;
            

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


        public DataSet GetInventoryQty()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select sum(isnull(Inventory_SET,0)) as Inventory_SET, sum(isnull(Inventory_PCS,0)) as Inventory_PCS  from 
(
	select
	1 as ID
	,convert(float, a.InMRPQuantity - isnull(round(b.totalQty / isnull(d.materialCount,1) , 0), 0)) as Inventory_SET
	,(a.InMRPQuantity) * (ISNULL(d.materialCount, 1)) - (isnull(b.totalQty, 0)) as Inventory_PCS
	from PQCInventory a
	left join (
		select
		aa.jobid,
		aa.partnumber,
		sum(aa.totalqty) as totalQty,
		sum(aa.acceptqty) as acceptQty,
		sum(aa.rejectQty) as rejectQty

		from PQCQaViTracking aa
		group by aa.jobId, aa.partNumber
	) b on a.JobNumber = b.jobId

	left join (select partNumber, count(1) as materialCount from PQCBomDetail group by partNumber  ) d on a.PartNumber = d.partNumber

	where 1=1  
	and a.UpdatedTime > '2019-7-30'  and a.UpdatedTime < GETDATE()
	and isnull(b.totalqty,0) < a.InMRPQuantity * isnull(d.materialCount,1)
) 
a group by a.ID ");
         

        

            return DBHelp.SqlDB.Query(strSql.ToString(),  DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
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
,a.InMRPQuantity as MrpQtySET
,a.InMrpQuantity * d.materialCount as MrpQtyPCS


,a.InMRPQuantity - ISNULL(e.totalQtySET,0) as BeforeSET
,a.InMrpQuantity * d.materialCount - ISNULL(c.TotalQty,0) as BeforePCS


,ISNULL(e.totalQtySET,0) as AfterSET
,ISNULL(c.TotalQty,0) as AfterPCS

,a.MFGDate

,case when (select 1 from PQCBom where partNumber = a.PartNumber) is null then 'False' else 'True' end as BomFlag

,case	when ISNULL(c.TotalQty,0) = 0
			then 'Pending'
		when ISNULL(c.TotalQty,0) > 0 and ISNULL(c.TotalQty,0) < a.InMrpQuantity * d.materialCount
			then 'Inprocess' 
		when ISNULL(c.TotalQty,0) = a.InMrpQuantity * d.materialCount
			then 'Complete' 
end as jobStatus



from PQCInventory a 
left join PQCBom b on a.PartNumber = b.PartNumber 
left join (select jobId, sum(totalQty) as totalQty from PQCQaViTracking group by jobId) c on a.jobNumber = c.jobId
left join (select PartNumber , count(1) as materialCount from PQCBomDetail group by partNumber ) d on a.PartNumber = d.partNumber
left join
(
	select jobid , min(totalQty) as totalQtySET from (

		select jobId,materialPartNo, sum(passQty) + sum(rejectQty) as totalQty  
		from PQCQaViDetailTracking 
		group by jobId, materialPartNo
	) a group by a.jobId
) e on a.JobNumber = e.jobId

where 1=1 

--charindex 从process中查询laser出现在第几个位置, 0就是没有laser 即wip.    
--(允许processes是null. 否则无bom的记录无法显示)
and (CHARINDEX('Laser', b.processes,0) = 0 or CHARINDEX('Laser', b.processes,0) is null) ");

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
