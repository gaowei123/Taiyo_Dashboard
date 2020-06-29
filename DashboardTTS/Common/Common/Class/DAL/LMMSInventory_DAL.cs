using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.DAL
{
    public class LMMSInventory_DAL
    {
        public LMMSInventory_DAL() { }

     
        public SqlCommand deleteCommand(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSInventory where jobNumber = @jobNumber");
  
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            parameters[0].Value = sJobNo;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }


        public int Add(Common.Class.Model.LMMSInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSInventory (");
            strSql.Append("jobNumber ,partNumber,description,quantity,startOnTime,pqcQuantity ,dateTime,day,month,year,showFlag,LotNo");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@description,@quantity,@startOnTime,@pqcQuantity,@dateTime,@day,@month,@year,@showFlag,@LotNo)");
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@description", SqlDbType.VarChar),
                new SqlParameter("@quantity", SqlDbType.Decimal),
                new SqlParameter("@startOnTime", SqlDbType.DateTime),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@day", SqlDbType.VarChar),
                new SqlParameter("@month", SqlDbType.VarChar),
                new SqlParameter("@year", SqlDbType.VarChar),
                new SqlParameter("@pqcQuantity", SqlDbType.VarChar),
                new SqlParameter("@showFlag", SqlDbType.VarChar),
                new SqlParameter("@LotNo", SqlDbType.VarChar)
            };

            parameters[0].Value = model.JobNumber == null ? (object)DBNull.Value : model.JobNumber;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.description == null ? (object)DBNull.Value : model.description;
            parameters[3].Value = model.quantity == null ? (object)DBNull.Value : model.quantity;
            parameters[4].Value = model.startOnTime == null ? (object)DBNull.Value : model.startOnTime;
            parameters[5].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[6].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[7].Value = model.month == null ? (object)DBNull.Value : model.month;
            parameters[8].Value = model.year == null ? (object)DBNull.Value : model.year;
            parameters[9].Value = model.PQCQuantity == null ? (object)DBNull.Value : model.PQCQuantity;
            parameters[10].Value = model.ShowFlag == null ? (object)DBNull.Value : model.ShowFlag;
            parameters[11].Value = model.Lotno == null ? (object)DBNull.Value : model.Lotno;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }

      

        public int UpdateMFGDate(string jobNumber, DateTime MFGDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  LMMSInventory set ");
            strSql.Append(" StartOnTime = @StartOnTime ");
            strSql.Append("   where jobNumber = @jobNumber");

            SqlParameter[] parameters = {
                new SqlParameter("@StartOnTime", SqlDbType.DateTime),
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            parameters[0].Value = MFGDate;
            parameters[1].Value = jobNumber;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }

        public SqlCommand UpdateJobMaintenanceCMD(Common.Class.Model.LMMSInventory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(" Update LMMSInventory set ");
            strSql.Append(" pqcQuantity = @pqcQuantity ");
            strSql.Append(" ,setUpQTY = @setUpQTY ");
            strSql.Append(" ,buyOffQty = @buyOffQty ");
            strSql.Append(" where jobNumber = @jobNumber ");

            
            SqlParameter[] parameters = {
                new SqlParameter("@pqcQuantity", SqlDbType.Int),
                new SqlParameter("@setUpQTY", SqlDbType.Int),
                new SqlParameter("@buyOffQty", SqlDbType.Int),
                new SqlParameter("@jobNumber", SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.PQCQuantity;
            parameters[1].Value = model.SetUp;
            parameters[2].Value = model.Buyoff;
            parameters[3].Value = model.JobNumber;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }


        

        public DataSet GetJobDetailForMaintenance(string JobNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
jobNumber, 
ISNULL( pqcQuantity,0) as shortage, 
isnull(setUpQTY,0) as setupQty, 
isnull(buyOffQty,0) as buyoffQty 
from lmmsinventory   where 1=1 ");

            if (!string.IsNullOrEmpty(JobNumber))
            {
                strSql.Append(" and jobNumber = @jobNumber ");
            }

        

            SqlParameter[] parameters = 
            {
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(JobNumber))
            {
                parameters[0].Value = JobNumber;
            }else
            {
                parameters[0] = null;
            }

           
         

            return DBHelp.SqlDB.Query(strSql.ToString(),parameters);
        }


        public DataSet GetListForPQCReport(DateTime dDateFrom, DateTime dDateTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            jobNumber
                            , isnull(pqcquantity, 0) as Shortage
                            , isnull(setUpQTY, 0) as Setup
                            , isnull(buyoffQty, 0) as Buyoff
                            from LMMSInventory
                            where datetime >= @dDateFrom and datetime < @dDateTo  ");
            

            SqlParameter[] parameters =
            {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        

        public DataSet Exsit(string jobnumber)
        {
            string sql = "select * from LMMSInventory where jobnumber ='" + jobnumber + "' ";
            
            return  DBHelp.SqlDB.Query(sql.ToString());
        }

        public DataTable SelectInventoryDailyReport(string partNo,string customer )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
a.partNumber as PartNumber

,case when  ISNULL((select top 1 module from LMMSBom where partNumber = a.partNumber),'') = '' 
then 'UNKNOWN' 
else (select top 1 module from LMMSBom where partNumber = a.partNumber) 
end as Module

,case when isnull( (select top 1 customer from LMMSBom where partNumber = a.partNumber),'') = '' 
then 'UNKNOWN' 
else (select top 1 customer from LMMSBom where partNumber = a.partNumber) 
end as Customer


,case when COUNT(a.jobNumber) = 1
then
    (
        select aa.jobnumber from LMMSInventory aa  
		left join lmmswatchlog bb on aa.jobNumber = bb.jobNumber
        where aa.partnumber = a.partnumber and aa.datetime > '2018-8-14'
        and isnull(bb.totalPass, 0) + isnull(bb.totalFail, 0)  <  (aa.quantity- aa.pqcQuantity - isnull(aa.setUpQTY, 0) - isnull(aa.buyOffQty, 0)) * avg(material.materialCount) 
    ) 
else 'JOT###'  end as JobNumber



, sum(a.quantity) as SetCount

, sum(a.quantity * material.materialCount) as Pcs

, convert(varchar, sum(a.quantity))
+ '(' +
convert(varchar, sum(a.quantity) * avg(material.materialCount))
+ ')' 
as MRPSet_PCS


, convert(varchar(50), sum(a.quantity)  - isnull(sum((c.totalpass + c.totalFail) / material.materialCount), 0) - sum(isnull(a.pqcQuantity,0))- sum(isnull(a.setUpQTY,0))- sum(isnull(a.buyOffQty,0)) )
+ '(' +
convert(varchar(50), sum((a.quantity  - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY,0) - isnull(a.buyOffQty,0)) * material.materialCount) - isnull(sum(c.totalpass + c.totalFail), 0))
+ ')'  AS BeforeLaser


, convert(varchar(50), isnull(sum((c.totalpass + c.totalFail) / material.materialCount), 0))
+ '(' + convert(varchar(50), isnull(sum(c.totalpass + c.totalFail), 0)) + ')'  
AS AfterLaser

, COUNT(a.jobNumber) as JobCount

--,case when(select count(1) from LMMSBomDetail where partNumber = a.partNumber group by partNumber) is null
--    then  'No Bom Detail' 
--    else
--    convert(varchar(8), Convert(decimal(18, 1), avg(cyc.avg_cycleTime / (block.avg_blockCount * avg_unitCount / material.materialCount))))
--    + 's' + '/' +  case when AVG(material.materialCount) = 1 then 'Pcs' else 'Set' end
--    end
--as CycleTime

    --3600 / cycleTime Per Set
,Convert(varchar, convert(float, Round( 3600 / avg( MergedBOM.avg_SET_CycleTime),0))) 
+ '(' + 
Convert(varchar, convert(float, Round( 3600 / avg(MergedBOM.avg_PCS_CycleTime),0)))
+ ')' as Hourly


-- SET(PCS) CycleTime
,case when(select count(1) from LMMSBomDetail where partNumber = a.partNumber group by partNumber) is null
then  'No Bom Detail' 
else
convert(varchar, convert(decimal(18,2),round(avg( MergedBOM.avg_SET_CycleTime),2 )))
+ '(' +
convert(varchar, convert(decimal(18,2), round(avg(MergedBOM.avg_PCS_CycleTime),2 )))
+ ')'  
end as  SET_PCS_CycleTime


, CONVERT(varchar(100),(case when COUNT(a.jobNumber) = 1
							then (
									select 
										aa.startOnTime
									from LMMSInventory aa 
									left join lmmswatchlog bb on aa.jobNumber = bb.jobNumber
									where aa.partnumber = a.partnumber  
									and  aa.datetime > '2018-8-14'
									and isnull(bb.totalPass, 0) + isnull(bb.totalFail, 0)  <  (aa.quantity  - isnull(aa.pqcQuantity,0) - isnull(aa.setUpQTY, 0) - isnull(aa.buyOffQty, 0)) * AVG(material.materialCount) 
									)
							end )
, 3)  as MFGDate

,Convert(varchar(10),
    CONVERT(numeric(10, 2),
        (SUM((a.quantity - a.pqcQuantity) * material.materialCount) - SUM(ISNULL(c.totalPass, 0) + ISNULL(c.totalFail, 0)))
        *
       avg(MergedBOM.avg_SET_CycleTime) / 3600
    )
) + 'H' as EstProcessTime


,case when ISNULL(AVG(material.materialCount), 0)   = 0
    then 'No'
    else 'Yes'
end as bomExistFlag


from LMMSInventory a
left join
(
    select 
	a.partnumber
	,avg(cycleTime) as avg_cycleTime 
	,avg(blockCount) as avg_blockCount
	,avg(unitCount) as avg_unitCount
	,AVG(cycleTime/blockCount/unitCount) as avg_PCS_CycleTime
	,AVG(cycleTime/blockCount/unitCount * isnull( b.materialCount,1)) as avg_SET_CycleTime
	from LMMSBom a
	left join (select COUNT(1) as materialCount, partnumber from LMMSBomDetail group by partNumber) b 
	on a.partNumber = b.partNumber
	group by a.partNumber
) MergedBOM
on a.partNumber = MergedBOM.partNumber

left join
(
    select COUNT(1) as materialCount, partnumber from LMMSBomDetail group by partNumber
) Material
on a.partNumber = Material.partNumber

left join LMMSWatchLog c on a.jobnumber = c.jobnumber

where 1 = 1
and a.dateTime > '2018-8-14'
and isnull(c.totalPass, 0) + isnull(c.totalFail, 0) < (a.quantity - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY, 0) - isnull(a.buyOffQty,0)) * material.materialCount  ");


            if (partNo != "")
            {
                strSql.Append(" and a.partnumber = @partnumber ");
            }

            if (customer != "")
            {
                strSql.Append(" and Customer = @Customer ");
            }

            strSql.Append(" group by a.partNumber   order by Customer asc, a.partNumber asc ");

            SqlParameter[] parameters = {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@Customer",SqlDbType.VarChar)
            };

            if (partNo == "") { parameters[0] = null; } else { parameters[0].Value = partNo; }

            if (customer == "") { parameters[1] = null; } else { parameters[1].Value = customer; }

   

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            
            if (ds == null || ds.Tables.Count ==0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable SelectInventoryDetailReport(string jobNo, string partNo, DateTime dateFrom, DateTime dateTo, string status)
        {
            StringBuilder strSql = new StringBuilder();

            #region detail report sql
            strSql.Append(@"
                            select 
                            a.jobNumber  
                            ,a.partNumber  
                            ,a.dateTime  
                            ,a.startOnTime
                            ,isnull(a.setupQTY,0) as setUpQty
                            ,isnull( a.pqcQuantity,0) as pqcQuantity
                            ,isnull( a.buyOffQty,0) as buyOffQty

                            ,convert(varchar, a.quantity )+ '(' + convert(varchar, a.quantity * b.materialCount ) +')' as MRPSet_PCS

                            --,convert(varchar(50), 
	                        --    case when ( a.quantity  - isnull((c.totalpass + c.totalFail ),0) / b.materialCount) < 0
	                        --    then 0
	                        --    else ( a.quantity - isnull((c.totalpass + c.totalFail ),0) / b.materialCount)
	                        --    end
                            --)
                            --+'('+
                            --convert(varchar(50), 
	                        --    case when ( a.quantity - isnull( (c.totalpass + c.totalFail ),0) / b.materialCount  ) < 0
	                        --    then 0
	                        --    else  a.quantity * b.materialCount -  isnull(c.totalpass,0)  -  isnull(c.totalFail,0)
	                        --    end
                            --) 
                            --+')' 
                            --AS BeforeLaser

                            ,convert(varchar(50), 
	                            case when ( a.quantity - isnull((c.totalpass + c.totalFail ),0) / b.materialCount - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY,0) - isnull(a.buyOffQty,0)) < 0
	                            then 0
	                            else ( a.quantity - isnull((c.totalpass + c.totalFail ),0) / b.materialCount - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY,0) - isnull(a.buyOffQty,0))
	                            end
                            )
                            +'('+
                            convert(varchar(50), 
	                            case when (a.quantity - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY,0) - isnull(a.buyOffQty,0)) * b.materialCount -  isnull(c.totalpass,0)  -  isnull(c.totalFail,0) < 0
	                            then 0
	                            else  (a.quantity - isnull(a.pqcQuantity,0) - isnull(a.setUpQTY,0) - isnull(a.buyOffQty,0)) * b.materialCount -  isnull(c.totalpass,0)  -  isnull(c.totalFail,0)
	                            end
                            )
                            +')' 
                            AS BeforeLaser


                            ,convert(varchar(50), isnull( (c.totalpass + c.totalFail),0) / b.materialCount )
                            +'('+
                            convert(varchar(50), isnull( (c.totalpass + c.totalFail),0)  ) 
                            +')' AS AfterLaser

                            ,case when b.partNumber is null OR b.materialCount is null OR b.materialCount =0
                            then 'No' 
                            else 'Yes' end  as bomExistFlag
   
                            ,convert(varchar(10),       
	                            convert(numeric(10, 2),         
		                            (case when c.totalQuantity = 0 or c.totalQuantity is null  then (a.quantity  - isnull( a.buyOffQty,0) -ROUND(a.pqcQuantity/b.materialCount, 0) ) * b.materialCount  else c.totalQuantity end )          
		                                / b.blockCount /b.unitCount * b.cycleTime / 3600  
	                            )     
                            ) + 'H'   as  estProcessTime

                            ,case when c.totalQuantity is null then 0 else c.totalQuantity end  as totalQuantity
   
                            ,case
                            when isnull(c.totalFail,0) + isnull(c.totalPass,0) = 0 
	                            then 'Pending'
                            when ( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) >= c.totalQuantity-  (isnull( a.setupQTY,0) + isnull( a.pqcQuantity,0) + isnull(a.buyOffQty,0))  * b.materialCount
	                            then 'Complete'     
                            when ( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) > 0 
		                            and( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) < c.totalQuantity-  (isnull( a.setupQTY,0) + isnull( a.pqcQuantity,0) + isnull(a.buyOffQty,0))  * b.materialCount
	                            then 'Inprocess'    
                            when ( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) < c.totalQuantity-  (isnull( a.setupQTY,0) + isnull( a.pqcQuantity,0) + isnull(a.buyOffQty,0))  * b.materialCount
	                            then 'NoComplete'
                            end as status



                            from LMMSInventory a
                            left join   
                            (  
                                select   
	                            partNumber  
                                ,AVG( cycleTime ) as cycleTime 
	                            ,AVG( blockCount) as blockCount 
	                            ,AVG( unitCount ) as unitCount

                                ,case when isnull( (select COUNT(1) from LMMSBomDetail where partNumber = aa.partNumber group by partNumber),0) = 0 
	                                then 1 
	                                else (select COUNT(1) from LMMSBomDetail where partNumber = aa.partNumber group by partNumber) 
	                                end  as  materialCount
                                from LMMSBom aa
                                group by partNumber
                            ) b   on a.partNumber = b.partNumber  

                            left join LMMSWatchLog c  on a.jobNumber = c.jobNumber  

                            where 1=1  and showFlag = 'TRUE' and  a.datetime  > @dateFrom   and a.datetime  < @dateTo  ");

            if (jobNo != "")
            {
                strSql.Append(" and a.jobNumber = @jobNumber ");
            }

            if (partNo != "")
            {
                strSql.Append(" and a.partNumber = @partNumber ");
            }
            
            if (status != "ALL")
            {
                if (status == StaticRes.Global.clsConstValue.JobStatus.pending)
                {
                    strSql.Append("  and  isnull(c.totalFail,0) + isnull( c.totalPass ,0) = 0  ");
                }
                else if (status == StaticRes.Global.clsConstValue.JobStatus.inprocess)
                {
                    strSql.Append(" and   isnull(c.totalFail,0) + isnull( c.totalPass ,0) >0  ");
                    strSql.Append(" and ( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) <  (a.quantity  -  isnull( a.setupQTY,0) - isnull( a.pqcQuantity,0) - isnull(a.buyOffQty,0))  * b.materialCount ");
                }
                else if (status == StaticRes.Global.clsConstValue.JobStatus.complete)
                {
                    strSql.Append(" and    ( isnull(c.totalFail,0) + isnull(c.totalPass,0) ) >= (a.quantity  -  isnull( a.setupQTY,0) - isnull( a.pqcQuantity,0) - isnull(a.buyOffQty,0))  * b.materialCount ");
                }
                else if (status == StaticRes.Global.clsConstValue.JobStatus.noComplete)
                {
                    strSql.Append(" and isnull( c.totalPass,0) + isnull(c.totalFail,0) < (a.quantity  -  isnull( a.setupQTY,0) - isnull( a.pqcQuantity,0) - isnull(a.buyOffQty,0))  * b.materialCount ");
                }
            }

            strSql.Append(" order by a.datetime asc ");

            #endregion


            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@dateFrom", SqlDbType.DateTime),
                new SqlParameter("@dateTo", SqlDbType.DateTime)
            };
            parameters[0].Value = jobNo == null ? (object)DBNull.Value : jobNo;
            parameters[1].Value = partNo == null ? (object)DBNull.Value : partNo;
            parameters[2].Value = dateFrom == null ? (object)DBNull.Value : dateFrom;
            parameters[3].Value = dateTo == null ? (object)DBNull.Value : dateTo;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }

        }




        public DataSet GetInventoryQty()
        { 
            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"select sum(Inventory_SET) as Inventory_SET, sum(Inventory_PCS) as Inventory_PCS from 
(
	select 

	1 as ID                   
	, a.quantity  - isnull( a.buyOffQty,0) - isnull(a.pqcQuantity,0) - isnull(a.setupQTY,0) - isnull( (c.totalpass + c.totalFail ),0) / b.materialCount as Inventory_SET
	,( a.quantity  - isnull( a.buyOffQty,0) - isnull(a.pqcQuantity,0) - isnull(a.setupQTY,0)) * b.materialCount -  isnull(c.totalpass,0)  -  isnull(c.totalFail,0) as Inventory_PCS



	from LMMSInventory a
	left join (   select partNumber, COUNT(1) as materialCount from LMMSBomDetail group by partNumber ) b   on a.partNumber = b.partNumber  
	left join LMMSWatchLog c  on a.jobNumber = c.jobNumber  

	where 1=1  and showFlag = 'TRUE'      
	and  a.datetime  > '2018-8-14'  
	and a.datetime  < GETDATE()
	and isnull( c.totalPass,0) + isnull(c.totalFail,0) < (a.quantity  -  isnull( a.setupQTY,0) - isnull( a.pqcQuantity,0) - isnull(a.buyOffQty,0))  * b.materialCount  
) a 
group by a.ID");


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString());

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }


        public DataTable GetList(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT TOP (1000) [jobNumber]
      ,[partNumber]
      ,[description]
      ,[quantity]
      ,ISNULL( [pqcQuantity],0) as pqcQuantity
      ,[startOnTime]
      ,[dateTime]
      ,[day]
      ,[month]
      ,[year]
      ,[showFlag]
      ,ISNULL([setUpQTY],0) as setUpQTY
      ,ISNULL([buyOffQty],0) as buyOffQty
      ,[lotNo]
  FROM [LMMS_TAIYO].[dbo].[LMMSInventory] where 1=1 ");

            if (sJobNo != "")  strSql.Append(" and jobNumber = @jobNumber");

            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            if (sJobNo != "") { parameters[0].Value = sJobNo; } else { parameters[0] = null; }
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),parameters);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }

        }



        //laser inventory - watch log,  
        public DataTable GetInventoryInfoForAllInventoryReport(DateTime dStartTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 

a.partnumber
,b.materialPartNo
,sum(a.quantity) as inventoryQty

from lmmsinventory a
left join LMMSBomDetail b on a.partnumber = b.partnumber 
left join LMMSWatchLog c on a.jobnumber = c.jobNumber
left join (select partNumber, count(1) as materialCount from lmmsbomdetail group by partnumber ) d on a.partnumber = d.partnumber 

where 1=1 and a.datetime >= '2020-3-1'
and c.totalPass + totalFail +  isnull(a.pqcQuantity,0) * d.materialCount +  isnull(a.setUpQTY,0) * d.materialCount +  isnull(a.buyOffQty,0) * d.materialCount  < c.totalQuantity 

group by a.partNumber, b.materialPartNo ");

            
            SqlParameter[] parameters = {
                new SqlParameter("@startTime", SqlDbType.DateTime)
            };

            parameters[0].Value = dStartTime;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }



        public DataTable GetListForModel(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [jobNumber]
      ,[partNumber]
      ,[description]
      ,[quantity]
      ,isnull( [pqcQuantity],0) as pqcQuantity
      ,[startOnTime]
      ,[dateTime]
      ,[day]
      ,[month]
      ,[year]
      ,[showFlag]
      ,isnull( [setUpQTY],0) as setUpQTY
      ,isnull( [buyOffQty],0) as buyOffQty
      ,[lotNo]
  FROM LMMSInventory where 1=1  ");

            if (sJobNo != "") strSql.Append(" and jobNumber = @jobNumber");

            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            if (sJobNo != "") { parameters[0].Value = sJobNo; } else { parameters[0] = null; }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }



    }
}
