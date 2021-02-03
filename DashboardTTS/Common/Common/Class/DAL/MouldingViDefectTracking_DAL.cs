using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Common.Class.DAL
{
   public  class MouldingViDefectTracking_DAL
    {

        public MouldingViDefectTracking_DAL()
        {

        }


        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo,string sMachineID, string sPartNumber, string sRejType,string sModel,string sShift, string sType)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@"SELECT  ROW_NUMBER() over(order by z.shift asc, z.machineid asc, z.partNumberAll asc) as ID
                                ,z.trackingID ,z.day,z.dayForSearching,z.shift,z.machineID,z.partNumberAll,z.partNumber,z.jigNo,z.model,z.defectCode,z.rejectQty,z.rejectCost,z.rejectCost_1,z.dateTime,z.OutPutQTY,z.Type,z.RejectionRate,z.OPID from (      ");
            strSql.Append(@"SELECT
            ROW_NUMBER() over(order by a.shift asc, a.machineid asc, a.partNumberAll asc) as ID
            ,CONVERT(varchar(16), a.[day], 105) as day
            ,a.trackingID
            ,a.day as dayForSearching
            ,a.[shift] as shift
            ,a.[machineID] as machineID
            ,a.[partNumberAll] as partNumberAll
            ,a.[partNumber] as partNumber
            ,a.[jigNo] as jigNo
            ,a.[model] as model
            ,a.[defectCode] as defectCode
            ,a.[rejectQty] as rejectQty
            ,convert(varchar,isnull((a.rejectQty * c.unitCount) ,0)) as rejectCost
            ,isnull((a.rejectQty * c.unitCount) ,0) as rejectCost_1
            ,a.[dateTime] as dateTime
            ,b.acountReading as OutPutQTY
            ,c.MachineID as Type
            --,convert(varchar,convert(numeric(10,2),(isnull(a.[rejectQty],0)*100/isnull(b.acountReading,0))))+'%' as RejectionRate 

            --fix bug  0除报错
            ,case when ISNULL(b.acountReading,0) = 0 
            then '0%'
            else convert(varchar,convert(numeric(10,2),(isnull(a.[rejectQty],0)*100/isnull(b.acountReading,0))))+'%' 
            end as RejectionRate

            ,b.UserID as OPID
            FROM(
            select shift, machineID,[day], partNumberAll,jigNo, partNumber, model, defectCode, rejectQty, dateTime, trackingID
            from [MouldingViDefectTracking]  ");


            strSql.Append(" where datetime >= @dateFrom ");
            strSql.Append(" and datetime < @dateTo  and rejectQty > 0 ");


            if (sMachineID != "")
            {
                strSql.Append(" and machineid = @machineid ");
            }

            if (sPartNumber != "")
            {
                strSql.Append(" and partNumberAll like '%" + sPartNumber + "%' ");
            }

            if (sRejType != "" && sRejType != "All")
            {
                strSql.Append(" and  defectCode= @defectCode ");
            }

            if (sModel != "")
            {
                strSql.Append(" and  model= @model ");
            }
            if (sShift != "")
            {
                strSql.Append(" and  shift= @shift ");
            }
            strSql.Append("  group by shift, machineID,[day], jigNo,partNumberAll, partNumber, model, defectCode, rejectQty, dateTime, trackingID  )a   ");

            strSql.Append("        left join MouldingViTracking b on a.rejectQty > 0 and a.trackingID = b.trackingID ");

            strSql.Append("        left join MouldingBom c on a.rejectQty > 0 and c.partNumber = a.partNumber ");

            strSql.Append(" group by a.shift,a.machineid,a.partNumberAll,a.jigNo,a.day,a.shift,a.machineID,a.partNumberAll,a.partNumber,a.model,a.defectCode,a.rejectQty,a.dateTime,a.trackingID,b.acountReading,b.UserID,c.unitCount,c.MachineID ");
            //strSql.Append(" order by  shift asc, machineid asc, partNumberAll asc ,rejectQty desc ");
            strSql.Append(" )z   ");
            if (sType != "")
            {
                strSql.Append(" where  Type= @Type ");
            }
            strSql.Append(" order by  shift asc, machineid asc, partNumberAll asc ,rejectQty desc ");

            SqlParameter[] paras =
            {
               new SqlParameter("@dateFrom",SqlDbType.DateTime),
               new SqlParameter("@dateTo",SqlDbType.DateTime),
               new SqlParameter("@machineid",SqlDbType.VarChar,16),
               new SqlParameter("@defectCode",SqlDbType.VarChar),
               new SqlParameter("@model",SqlDbType.VarChar),
                new SqlParameter("@shift",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sMachineID != "")
            {
                paras[2].Value = sMachineID;
            }else
            {
                paras[2] = null;
            }

            if (sRejType != "")
            {
                paras[3].Value = sRejType;
            }else
            {
                paras[3] = null;
            }

            if (sModel != "")
            {
                paras[4].Value = sModel;
            }
            else
            {
                paras[4] = null;
            }

            if (sShift != "")
            {
                paras[5].Value = sShift;
            }
            else
            {
                paras[5] = null;
            }
            if (sType != "")
            {
                paras[6].Value = sType;
            }
            else
            {
                paras[6] = null;
            }
            return DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
              
        public DataSet GetCheckerByPart(string sMaterial_No, DateTime? dDay)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select  userID  from MouldingQaViTracking where 1=1 ");

            if (sMaterial_No != "")
            {
                strSql.Append(" and partNumber = @Material_No ");
            }
            if (dDay != null)
            {
                strSql.Append(" and MfgDate = @dDay ");
            }

            strSql.Append(" order by partNumber asc ");



            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@dDay",SqlDbType.DateTime)
            };

            if (sMaterial_No != "")
            {
                paras[0].Value = sMaterial_No;
            }
            else
            {
                paras[0] = null;
            }

            if (dDay != null)
            {
                paras[1].Value = dDay;
            }
            else
            {
                paras[1] = null;
            }

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;
        }

        public DataSet GetMonthlyRejectReport(string sYear,string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select 
convert(varchar,a.year) + '-' + 
case
	when a.month=1 then 'Jan.' 
	when a.month=2 then 'Feb.' 
	when a.month=3 then 'Mar.' 
	when a.month=4 then 'Apr.' 
	when a.month=5 then 'May.' 
	when a.month=6 then 'Jun.' 
	when a.month=7 then 'Jul.' 
	when a.month=8 then 'Aug.' 
	when a.month=9 then 'Sep.' 
	when a.month=10 then 'Oct.' 
	when a.month=11 then 'Nov.' 
	when a.month=12 then 'Dec.'
end as Month
,a.Output
,CONVERT(decimal(18,0), a.rejectQty) as rejectQty
,convert(varchar, convert(decimal(18,2),round(a.rejectQty/a.Output*100,2))) + '%' as rejRate 
,ISNULL(dc1.rejectQty,0) as 'White Dot'		 
,ISNULL(dc2.rejectQty,0) as 'Scratches'        
,ISNULL(dc3.rejectQty,0) as 'Dented Mark'      
,ISNULL(dc4.rejectQty,0) as 'Shinning Dot'     
,ISNULL(dc5.rejectQty,0) as 'Black Mark'       
,ISNULL(dc6.rejectQty,0) as 'Sink Mark'        
,ISNULL(dc7.rejectQty,0) as 'Flow Mark'        
,ISNULL(dc8.rejectQty,0) as 'High Gate'        
,ISNULL(dc9.rejectQty,0) as 'Silver Steak'     
,ISNULL(dc10.rejectQty,0) as 'Black Dot'        
,ISNULL(dc11.rejectQty,0) as 'Oil Stain'        
,ISNULL(dc12.rejectQty,0) as 'Flow Line'        
,ISNULL(dc13.rejectQty,0) as 'Over-Cut'         
,ISNULL(dc14.rejectQty,0) as 'Crack'            
,ISNULL(dc15.rejectQty,0) as 'Short Mold'       
,ISNULL(dc16.rejectQty,0) as 'Stain Mark'       
,ISNULL(dc17.rejectQty,0) as 'Weld Line'        
,ISNULL(dc18.rejectQty,0) as 'Flashes'          
,ISNULL(dc19.rejectQty,0) as 'Foreign Materials'
,ISNULL(dc20.rejectQty,0) as 'Drag'             
,ISNULL(dc21.rejectQty,0) as 'Material Bleed'   
,ISNULL(dc22.rejectQty,0) as 'Bent'
,ISNULL(dc23.rejectQty,0) as 'Defrom'           
,ISNULL(dc24.rejectQty,0) as 'Gas Mark' ");

            strSql.Append(@"
from
(
    select
    year(day) as year
    , month(day) as month
    , isnull(sum(isnull(acountReading, 0)), 0) as Output
    , isnull(sum(isnull(rejectQty, 0) ), 0) as rejectQty
	, ISNULL(sum(isnull(setup,0)),0) as setup
	, isnull(sum(isnull(QCNGQTY,0)),0) as IPQCRej
    from MouldingViTracking where   isnull(acceptQty,0) + isnull(rejectQty,0) > 0 and acountReading > 0 and status != 'Mould_Testing' and status != 'Material_Testing' ");
            if (sPartNo != "")
            {
                strSql.AppendLine(" and PartNumber = @PartNumber ");
            }
            strSql.Append(@"group by Year(day) , MONTH(day)
) a  ");


            if (sPartNo != "")
            {
                strSql.Append(@"
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'White Dot'		     group by YEAR(day), MONTH(day)) dc1 on  a.year= dc1.year and a.month=dc1.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Scratches'          group by YEAR(day), MONTH(day)) dc2 on  a.year= dc2.year and a.month=dc2.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Dented Mark'        group by YEAR(day), MONTH(day)) dc3 on  a.year= dc3.year and a.month=dc3.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Shinning Dot'       group by YEAR(day), MONTH(day)) dc4 on  a.year= dc4.year and a.month=dc4.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Black Mark'         group by YEAR(day), MONTH(day)) dc5 on  a.year= dc5.year and a.month=dc5.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Sink Mark'          group by YEAR(day), MONTH(day)) dc6 on  a.year= dc6.year and a.month=dc6.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Flow Mark'          group by YEAR(day), MONTH(day)) dc7 on  a.year= dc7.year and a.month=dc7.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'High Gate'          group by YEAR(day), MONTH(day)) dc8 on  a.year= dc8.year and a.month=dc8.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Silver Steak'       group by YEAR(day), MONTH(day)) dc9 on  a.year= dc9.year and a.month=dc9.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Black Dot'          group by YEAR(day), MONTH(day)) dc10 on  a.year=dc10.year and a.month=dc10.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Oil Stain'          group by YEAR(day), MONTH(day)) dc11 on  a.year=dc11.year and a.month=dc11.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Flow Line'          group by YEAR(day), MONTH(day)) dc12 on  a.year=dc12.year and a.month=dc12.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Over-Cut'           group by YEAR(day), MONTH(day)) dc13 on  a.year=dc13.year and a.month=dc13.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Crack'              group by YEAR(day), MONTH(day)) dc14 on  a.year=dc14.year and a.month=dc14.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Short Mold'         group by YEAR(day), MONTH(day)) dc15 on  a.year=dc15.year and a.month=dc15.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Stain Mark'         group by YEAR(day), MONTH(day)) dc16 on  a.year=dc16.year and a.month=dc16.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Weld Line'          group by YEAR(day), MONTH(day)) dc17 on  a.year=dc17.year and a.month=dc17.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Flashes'            group by YEAR(day), MONTH(day)) dc18 on  a.year=dc18.year and a.month=dc18.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Foreign Materials'  group by YEAR(day), MONTH(day)) dc19 on  a.year=dc19.year and a.month=dc19.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Drag'               group by YEAR(day), MONTH(day)) dc20 on  a.year=dc20.year and a.month=dc20.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Material Bleed'     group by YEAR(day), MONTH(day)) dc21 on  a.year=dc21.year and a.month=dc21.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Bent'               group by YEAR(day), MONTH(day)) dc22 on  a.year=dc22.year and a.month=dc22.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Defrom'             group by YEAR(day), MONTH(day)) dc23 on  a.year=dc23.year and a.month=dc23.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and Partnumber=@PartNumber and defectCode = 'Gas Mark'           group by YEAR(day), MONTH(day)) dc24 on  a.year=dc24.year and a.month=dc24.month");
            }
            else
            {
                strSql.Append(@"
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'White Dot'		     group by YEAR(day), MONTH(day)) dc1 on  a.year= dc1.year and a.month=dc1.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Scratches'          group by YEAR(day), MONTH(day)) dc2 on  a.year= dc2.year and a.month=dc2.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Dented Mark'        group by YEAR(day), MONTH(day)) dc3 on  a.year= dc3.year and a.month=dc3.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Shinning Dot'       group by YEAR(day), MONTH(day)) dc4 on  a.year= dc4.year and a.month=dc4.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Black Mark'         group by YEAR(day), MONTH(day)) dc5 on  a.year= dc5.year and a.month=dc5.month     
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Sink Mark'          group by YEAR(day), MONTH(day)) dc6 on  a.year= dc6.year and a.month=dc6.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Flow Mark'          group by YEAR(day), MONTH(day)) dc7 on  a.year= dc7.year and a.month=dc7.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'High Gate'          group by YEAR(day), MONTH(day)) dc8 on  a.year= dc8.year and a.month=dc8.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Silver Steak'       group by YEAR(day), MONTH(day)) dc9 on  a.year= dc9.year and a.month=dc9.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Black Dot'          group by YEAR(day), MONTH(day)) dc10 on  a.year=dc10.year and a.month=dc10.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Oil Stain'          group by YEAR(day), MONTH(day)) dc11 on  a.year=dc11.year and a.month=dc11.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Flow Line'          group by YEAR(day), MONTH(day)) dc12 on  a.year=dc12.year and a.month=dc12.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Over-Cut'           group by YEAR(day), MONTH(day)) dc13 on  a.year=dc13.year and a.month=dc13.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Crack'              group by YEAR(day), MONTH(day)) dc14 on  a.year=dc14.year and a.month=dc14.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Short Mold'         group by YEAR(day), MONTH(day)) dc15 on  a.year=dc15.year and a.month=dc15.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Stain Mark'         group by YEAR(day), MONTH(day)) dc16 on  a.year=dc16.year and a.month=dc16.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Weld Line'          group by YEAR(day), MONTH(day)) dc17 on  a.year=dc17.year and a.month=dc17.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Flashes'            group by YEAR(day), MONTH(day)) dc18 on  a.year=dc18.year and a.month=dc18.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Foreign Materials'  group by YEAR(day), MONTH(day)) dc19 on  a.year=dc19.year and a.month=dc19.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Drag'               group by YEAR(day), MONTH(day)) dc20 on  a.year=dc20.year and a.month=dc20.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Material Bleed'     group by YEAR(day), MONTH(day)) dc21 on  a.year=dc21.year and a.month=dc21.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Bent'               group by YEAR(day), MONTH(day)) dc22 on  a.year=dc22.year and a.month=dc22.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Defrom'             group by YEAR(day), MONTH(day)) dc23 on  a.year=dc23.year and a.month=dc23.month
left join (SELECT year(day) as year ,month(day) as month , sum(ISNULL(rejectQty,0)) as rejectQty from[MouldingViDefectTracking]   where 1=1 and defectCode = 'Gas Mark'           group by YEAR(day), MONTH(day)) dc24 on  a.year=dc24.year and a.month=dc24.month");
            }

            strSql.Append(" where a.year=@year order by a.month ");


            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@year",SqlDbType.VarChar)
            };


            if (sPartNo != "") paras[0].Value = sPartNo; else paras[0] = null;
            if (sYear != "") paras[1].Value = sYear; else paras[1] = null;



       

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }
        
        public DataTable GetDefectForDailyReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
day
,trackingID
, shift
, machineid
, partNumber
, partNumberAll
, SUM(case when defectCode = 'White Dot' then  rejectQty end) as  [White Dot]
, SUM(case when defectCode = 'Scratches' then  rejectQty end) as  [Scratches]
, SUM(case when defectCode = 'Dented Mark' then  rejectQty end) as  [Dented Mark]
, SUM(case when defectCode = 'Shinning Dot' then  rejectQty end) as  [Shinning Dot]
, SUM(case when defectCode = 'Black Mark' then  rejectQty end) as  [Black Mark]
, SUM(case when defectCode = 'Sink Mark' then  rejectQty end) as  [Sink Mark]
, SUM(case when defectCode = 'Flow Mark' then  rejectQty end) as  [Flow Mark]
, SUM(case when defectCode = 'High Gate' then  rejectQty end) as  [High Gate]
, SUM(case when defectCode = 'Silver Steak' then  rejectQty end) as  [Silver Steak]
, SUM(case when defectCode = 'Black Dot' then  rejectQty end) as  [Black Dot]
, SUM(case when defectCode = 'Oil Stain' then  rejectQty end) as  [Oil Stain]
, SUM(case when defectCode = 'Flow Line' then  rejectQty end) as  [Flow Line]
, SUM(case when defectCode = 'Over-Cut' then  rejectQty end) as  [Over-Cut]
, SUM(case when defectCode = 'Crack' then  rejectQty end) as  [Crack]
, SUM(case when defectCode = 'Short Mold' then  rejectQty end) as  [Short Mold]
, SUM(case when defectCode = 'Stain Mark' then  rejectQty end) as  [Stain Mark]
, SUM(case when defectCode = 'Weld Line' then  rejectQty end) as  [Weld Line]
, SUM(case when defectCode = 'Flashes' then  rejectQty end) as  [Flashes]
, SUM(case when defectCode = 'Foreign Materials' then  rejectQty end) as  [Foreign Materials]
, SUM(case when defectCode = 'Drag' then  rejectQty end) as  [Drag]
, SUM(case when defectCode = 'Material Bleed' then  rejectQty end) as  [Material Bleed]
, SUM(case when defectCode = 'Bent' then  rejectQty end) as  [Bent]
, SUM(case when defectCode = 'Deform' then  rejectQty end) as  [Deform]
, SUM(case when defectCode = 'Gas Mark' then  rejectQty end) as  [Gas Mark]
from MouldingViDefectTracking
where status != 'Mould_Testing' and status != 'Material_Testing' and day >= @datefrom and day < @dateto ");

            if (sPartNo != "")
            {
                strSql.Append(" and partNumber = @partNo");
            }

            if (sJigNo != "")
            {
                strSql.Append(" and jigNo = @jigNo ");
            }

            if (sShift != "")
            {
                strSql.Append(" and shift = @shift ");
            }


            strSql.Append(" group by day, shift, machineID, partNumber, partNumberAll, trackingID");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@partNo",SqlDbType.VarChar,64),
                new SqlParameter("@jigNo",SqlDbType.VarChar,64),
                 new SqlParameter("@shift",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sPartNo != "") paras[2].Value = sPartNo; else paras[2] = null;
            if (sJigNo != "") paras[3].Value = sJigNo; else paras[3] = null;
            if (sShift != "") paras[4].Value = sShift; else paras[4] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        
        public DataTable GetRejTimeDetail(DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineID, string sPartNo, string sDefectCode, string sJigNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
day
,shift
,machineID
,partNumber
,jigNo
,defectCode
,rejectQty

,rejectQtyHour01
,rejectQtyHour02
,rejectQtyHour03
,rejectQtyHour04
,rejectQtyHour05
,rejectQtyHour06
,rejectQtyHour07
,rejectQtyHour08
,rejectQtyHour09
,rejectQtyHour10
,rejectQtyHour11
,rejectQtyHour12

from MouldingViDefectTracking where 1=1 and rejectQty != 0  and day >= @datefrom and day < @dateto  ");

            if (sShift != "") strSql.Append(" and shift = @shift ");
            if (sPartNo != "") strSql.Append(" and partNumber = @partNo");
            if (sMachineID != "") strSql.Append(" and machineID = @machineID ");
            if (sDefectCode != "") strSql.Append(" and defectCode = @defectCode ");
            if (sJigNo != "") strSql.Append(" and jigNo = @jigNo ");


            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@shift",SqlDbType.VarChar,16),
                new SqlParameter("@partNo",SqlDbType.VarChar,64),
                new SqlParameter("@machineID",SqlDbType.VarChar,64),
                new SqlParameter("@defectCode",SqlDbType.VarChar,64),
                new SqlParameter("@jigNo",SqlDbType.VarChar,64)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sShift != "") paras[2].Value = sShift; else paras[2] = null;
            if (sPartNo != "") paras[3].Value = sPartNo; else paras[3] = null;
            if (sMachineID != "") paras[4].Value = sMachineID; else paras[4] = null;
            if (sDefectCode != "") paras[5].Value = sDefectCode; else paras[5] = null;
            if (sJigNo != "") paras[6].Value = sJigNo; else paras[6] = null;




            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        
        public SqlCommand DeleteCommand(string sTrackingID)
        {
            if (string.IsNullOrEmpty(sTrackingID))
                return null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Delete from MouldingViDefectTracking  where  trackingID = @trackingID");
            

            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };
            paras[0].Value = sTrackingID;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        
        public SqlCommand MaintenanceCommand(string sTrackingID, string sPartNo, string sModel, string sJigNo)
        {
            string strSql = @"
update MouldingViDefectTracking
set partNumber = @partNumber, 
model = @model, 
jigNo = @jigNo
where trackingID = @trackingID";


            SqlParameter[] paras =
            {    
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@model",SqlDbType.VarChar),
                new SqlParameter("@jigNo",SqlDbType.VarChar),
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };
            paras[0].Value = sPartNo;
            paras[1].Value = sModel;
            paras[2].Value = sJigNo;
            paras[3].Value = sTrackingID;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

    }
}
