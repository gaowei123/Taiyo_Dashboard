using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.LaserInventory
{
    internal class LaserInventory_DAL
    {
        public DataTable SelectInventoryDailyReport(string partNo, string customer)
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
