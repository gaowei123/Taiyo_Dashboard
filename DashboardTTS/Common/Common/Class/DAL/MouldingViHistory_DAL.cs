using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingViHistory_DAL
    {
        Common.Class.Model.MouldingViHistory_Model model = new Model.MouldingViHistory_Model();
        public MouldingViHistory_DAL()
        {

        }


        internal DataTable GetMonthlyReport(int iYear, string sType)
        {

            StringBuilder strSql = new StringBuilder();
         



            strSql.Append(@"
select 
    Main.SN,
	main.Month, 
	isnull(data.Button,0) as Button,
	isnull(data.ButtonRejRate,'0.00%') as [ButtonRej%],
	isnull(data.Knob,0) as Knob,
	isnull(data.KnobRejRate,'0.00%') as [KnobRej%],
	isnull(data.Len,0) as Len,
	isnull(data.LenRejRate,'0.00%') as [LenRej%],
	isnull(data.Panel,0) as Panel,
	isnull(data.PanelRejRate,'0.00%') as [PanelRej%],

    isnull(data.ButtonRejQty,0) as ButtonRejQty,
    isnull(data.KnobRejQty,0) as KnobRejQty,
    isnull(data.LenRejQty,0) as LenRejQty,
    isnull(data.PanelRejQty,0) as PanelRejQty,

	isnull(data.Total_Parts_Produce,0) as [Total Parts Produce],
	isnull(data.asTotal_Parts_Cost,0) as  [Total Parts Cost],
	isnull(data.Good_Parts_Qty,0) as  [Good Parts Qty],
	isnull(data.Good_Parts_Cost,0) as  [Good Parts Cost],
	CONVERT(decimal(18,0), isnull(data.Reject_Parts_Qty,0)) as  [Reject Parts Qty],
	isnull(data.Reject_Parts_Cost,0) as  [Reject Parts Cost],
	isnull(data.RejReate,'0.00%') as [Rej%]
from
(
	--拼出12个月的主表
	select 1  as SN ,@sYear+'-Jan.' as [Month] union select 2  as SN ,@sYear+'-Feb.' as [Month] union 
	select 3  as SN ,@sYear+'-Mar.' as [Month] union select 4  as SN ,@sYear+'-Apr.' as [Month] union 
	select 5  as SN ,@sYear+'-May.' as [Month] union select 6  as SN ,@sYear+'-Jun.' as [Month] union 
	select 7  as SN ,@sYear+'-Jul.' as [Month] union select 8  as SN ,@sYear+'-Aug.' as [Month] union 
	select 9  as SN ,@sYear+'-Sep.' as [Month] union select 10 as SN ,@sYear+'-Oct.' as [Month] union 
	select 11 as SN ,@sYear+'-Nov.' as [Month] union select 12 as SN ,@sYear+'-Dec.' as [Month]
) main
left join 
(
	select 
		MONTH(a.day) as [Month],
		sum(case when b.machineID='Button'	then isnull(acceptQty,0) end ) as Button,
		sum(case when b.machineID='Knob'	then isnull(acceptQty,0) end ) as Knob,
		sum(case when b.machineID='Len'		then isnull(acceptQty,0) end ) as Len,
		sum(case when b.machineID='Panel'	then isnull(acceptQty,0) end ) as Panel,

        sum(case when b.machineID='Button'	then isnull(rejectQty,0) end ) as ButtonRejQty,
		sum(case when b.machineID='Knob'	then isnull(rejectQty,0) end ) as KnobRejQty,
		sum(case when b.machineID='Len'		then isnull(rejectQty,0) end ) as LenRejQty,
		sum(case when b.machineID='Panel'	then isnull(rejectQty,0) end ) as PanelRejQty,

	    convert(varchar(50),convert(decimal(18,2),(sum(case when b.machineID='Button'	then isnull(rejectQty,0) end ) / sum(case when b.machineID='Button'	then isnull(acountReading,0) end ) * 100 ))) + '%' as ButtonRejRate,
		convert(varchar(50),convert(decimal(18,2),(sum(case when b.machineID='Knob'		then isnull(rejectQty,0) end ) / sum(case when b.machineID='Knob'		then isnull(acountReading,0) end ) * 100 ))) + '%' as KnobRejRate,
		convert(varchar(50),convert(decimal(18,2),(sum(case when b.machineID='Len'		then isnull(rejectQty,0) end ) / sum(case when b.machineID='Len'		then isnull(acountReading,0) end ) * 100 ))) + '%' as LenRejRate,
		convert(varchar(50),convert(decimal(18,2),(sum(case when b.machineID='Panel'	then isnull(rejectQty,0) end ) / sum(case when b.machineID='Panel'	then isnull(acountReading,0) end ) * 100 ))) + '%' as PanelRejRate,


		sum(isnull(a.acountReading,0)) as Total_Parts_Produce,
		sum(isnull(a.acceptQty,0)) as Good_Parts_Qty,
		sum(isnull(a.rejectQty,0)) as Reject_Parts_Qty,
		convert(varchar(50),convert(decimal(18,2),(sum(isnull(a.rejectQty,0)) / sum(isnull(a.acountReading,0)) * 100 ))) + '%' as RejReate,


		CONVERT(decimal(18,4), sum((isnull(a.acountReading,0)) * isnull(b.unitCount, 0.1))) as asTotal_Parts_Cost,
		CONVERT(decimal(18,4), sum(isnull(a.acceptQty,0) * isnull(b.unitCount, 0))) as Good_Parts_Cost,
		CONVERT(decimal(18,4), sum((isnull(a.rejectQty,0)) * isnull(b.unitCount, 0))) as Reject_Parts_Cost
	from MouldingViTracking a
	left join MouldingBom b on a.partNumber = b.partNumber
	where isnull(acceptQty,0) + isnull(rejectQty,0) > 0 and acountReading > 0 and status != 'Mould_Testing' and status != 'Material_Testing'
	and a.dateTime >= @dDateFrom and a.dateTime <= @dDateTo");

            if (sType.Trim() != "")
            {
                strSql.Append(" and b.machineID = @Type ");
            }

	        strSql.Append(@"
group by MONTH(a.day)
        ) data on main.SN = data.Month");

            SqlParameter[] parameters = {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@dDateTo", SqlDbType.DateTime),
                new SqlParameter("@sYear", SqlDbType.VarChar,8),
                new SqlParameter("@Type", SqlDbType.VarChar,50 )
            };

            //set the first and last day for the year
            DateTime dDateFrom = DateTime.Parse(string.Format("{0}-1-1 8:00", iYear));
            DateTime dDateTo = dDateFrom.AddYears(1);



            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            parameters[2].Value = iYear.ToString();
            if (sType.Trim() != "")
            {
                parameters[3].Value = sType;
            }else
            {
                parameters[3] = null;
            }
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataSet getProductSummaryByMachine(DateTime dDateFrom, DateTime dDateTo, string sSfhift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select 
                            'Machine' + b.machineID as MachineID,
                            b.ok,
                            b.ng,
                            b.total,
                            b.RejRate
                            from
                            (
                                select 1 as MachineID union select 2 as MachineID union
                                select 3 as MachineID union select 4 as MachineID union
                                select 5 as MachineID union select 6 as MachineID union
                                select 7 as MachineID union select 8 as MachineID
                            ) a
                            left join
                            (
                                select
                                machineID,
                                sum(isnull(acceptQty, 0)) as OK,
                                sum(isnull(rejectQty, 0)) as NG,
                                sum(isnull(acountReading, 0)) as Total,
                                convert(decimal(18, 2), sum(isnull(rejectQty, 0)) / sum(isnull(acountReading, 0)) * 100) as RejRate
                                from MouldingViTracking
                                where datetime >= @dDateFrom and datetime < @dDateTo ");

            if (sSfhift != "" && sSfhift.ToUpper() != "ALL")
                strSql.Append(" and shift = @sShift ");
            
            strSql.Append(@" group by machineID
                             ) b on a.MachineID = b.machineID  order by a.machineID ");
            
            SqlParameter[] paras =
            {
                new SqlParameter("dDateFrom",SqlDbType.DateTime2),
                new SqlParameter("dDateTo",SqlDbType.DateTime2),
                new SqlParameter("sShift",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);
            if (sSfhift != "")
                paras[2].Value = sSfhift;
            else
                paras[2] = null;

        


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        


        public DataSet GetTestingList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" SELECT  * from MouldingViTracking where status in ('Material_Testing','Mould_Testing' ) and day >= @DateFrom and day < @DateTo ");
            
            if (sShift != "") strSql.Append(" and shift = @shift");
            if (sMachineID != "") strSql.Append(" and machineID = @machineID");


            
            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("shift",SqlDbType.VarChar),
                new SqlParameter("machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (sShift != "") paras[2].Value = sShift; else paras[2] = null;
            if (sMachineID != "") paras[3].Value = sMachineID; else paras[3] = null;
            

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
SELECT [machineID]
      ,[dateTime]
      ,[partNumber]
      ,[jigNo]
      ,[model]
      ,[cavityCount]
      ,[partsWeight]
      ,[parts2Weight]
      ,[lastQty]
      ,[cycleTime]
      ,[targetQty]
      ,[userName]
      ,[userID]
      ,[acountReading]
      ,[rejectQty]
      ,[QCNGQTY]
      ,[acceptQty]
      ,[startTime]
      ,[stopTime]
      ,[day]
      ,[shift]
      ,[status]
      ,[matPart01]
      ,[matPart02]
      ,[matLot01]
      ,[matLot02]
      ,[customer]
      ,[lastUpdatedTime]
      ,[trackingID]
      ,[remarks]
      ,[Material_MHCheck]
      ,[Material_MHCheckTime]
      ,[Material_Opcheck]
      ,[Material_OpCheckTime]
      ,[Material_LeaderCheck]
      ,[Material_LeaderCheckTime]
      ,[LeaderCheck]
      ,[LeaderCheckTime]
      ,[SupervisorCheck]
      ,[SupervisorCheckTime]
      ,[partNumberAll]
      ,[Setup]
      ,[WastageMaterial01]
      ,[WastageMaterial02]
      ,[OkAccumulation]
  FROM[Taiyo_Moulding].[dbo].[MouldingViTracking] where 1=1 and day >= @DateFrom and day < @DateTo ");



            if (sPartNo != "") strSql.Append(" and partNumber = @partNumber");
            if (sJigNo != "") strSql.Append(" and jigNo = @jigNo");
            if (sMachineID != "") strSql.Append(" and machineID = @machineID");


         


            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("partNumber",SqlDbType.VarChar),
                new SqlParameter("jigNo",SqlDbType.VarChar),
                new SqlParameter("machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (sPartNo != "") paras[2].Value = sPartNo; else paras[2] = null;
            if (sJigNo != "") paras[3].Value = sJigNo; else paras[3] = null;
            if (sMachineID != "") paras[4].Value = sMachineID; else paras[4] = null;





            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet getLaserVITracking(string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("Select top 1  *   from MouldingViTracking  where 1=1  ");

            if (sMachineID.Trim() != "")
            {
                strSql.Append(" and machineID = @sMachineID ");
            }

            strSql.Append(" order by datetime desc ");

            SqlParameter[] paras =
            {
                new SqlParameter("sMachineID",SqlDbType.VarChar)
            };
            

            if (sMachineID != "")
                paras[0].Value = sMachineID;
            else
                paras[0] = null;

        


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }



        public DataSet GetProductionList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            StringBuilder strSql = new StringBuilder();
            //2018/12/04 add Status
            strSql.Append(@"
Select 
ROW_NUMBER() over(order by a.machineid asc, a.datetime desc) as ID
,CONVERT(varchar(50), a.day,105) as Day
,a.shift as Shift
,'Machine' + a.machineID as MachineID
,a.model as Model
,c.machineID as Type
,a.partNumberAll as PartNumberAll
,a.partNumber as PartNumber
,case when isnull(a.jigNo,'') = '' then 'NA' else a.jigNo end as JigNo      
,a.status as Status
,case when  isnull( b.refField01,'') = '' then 0 else b.refField01 end  as TargetQty

,case when a.OkAccumulation = '' or a.OkAccumulation is null 
then 0 
else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) 
end as Accumulate


,convert(varchar(50),a.acountReading ) + '(' + convert(varchar(50), convert(numeric(10,0), a.acountReading/a.cavityCount) )  + ')'  as Output
,ISNULL(a.acceptQty,0) as OK
,CONVERT(decimal(18,0), ISNULL(a.rejectQty,0) + ISNULL(a.Setup,0 ) + ISNULL(a.QCNGQTY ,0))  as NG
,a.QCNGQTY as QCNGQTY

,CONVERT(varchar(50), CONVERT(decimal(18,2),Round((ISNULL(a.rejectQty,0) + ISNULL(a.Setup,0 ) + ISNULL(a.QCNGQTY ,0))/a.acountReading * 100,2 ))) + '%' as RejRate


,convert(varchar(50),	case when a.stopTime IS not null 
						then  datediff(second ,convert(datetime, a.startTime) , convert(datetime, a.stopTime))
						else (
								select top 1 convert(varchar(50), Sum(aa.RunningSeconds) ,108) 
								from ( 
										select top 5    --top 1 top 5  for optimize the performance
										datediff(second ,convert(datetime, StartTime) 
										, convert(datetime, isnull( EndTime, getdate()))) as RunningSeconds
										, MachineStatus
										from MouldingMachineStatus             
										where MachineStatus='Running' and MachineID = a.machineID  and shif = a.shift and day = a.day           
									) aa 
								where aa.MachineStatus='Running' 
								group by aa.MachineStatus     
						)  end 
) as Time


,case when    
	case when  isnull( b.refField01,'') = '' then 0 else b.refField01 end  
	-    
	case  when isnull( a.OkAccumulation,'')  = '' then 0  else CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty  end
	> 0										
then	CONVERT(VARCHAR(8), 
				CONVERT (decimal(19,3),
						( 
							case when ISNULL( b.refField01,'') = '' then 0 else b.refField01 end  
							- 
							case when ISNULL(a.OkAccumulation , '') = '' then 0  else  CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty end
						) 
						/
						( 3600 /  case when a.cycleTime = 0 then 42 else a.cycleTime end )
				)
		)    
else '0'
end as NeedProductionTime



from MouldingViTracking a 
left join MouldingPqm b on a.MachineID = SUBSTRING( b.machineID ,4,1 )  left join MouldingBom c on c.partNumber = a.partNumber                  

where a.acceptQty +a.rejectQty > 0 and a.acountReading >0 and a.status != 'Mould_Testing'
and a.dateTime > @DateFrom  and a.dateTime < @DateTo  ");


         


            if (sPartNo != "")
                strSql.Append(" and a.partNumber = @partNumber");

            if (sShift != "")
                strSql.Append(" and a.shift = @shift");

            if (sModule != "")
                strSql.Append(" and a.model = @model");

            if (sMachineID != "")
                strSql.Append(" and a.machineID = @machineID");

            strSql.Append(" order by a.machineid asc, a.datetime desc");


            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("partNumber",SqlDbType.VarChar),
                new SqlParameter("shift",SqlDbType.VarChar),
                new SqlParameter("model",SqlDbType.VarChar),
                new SqlParameter("machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sPartNo != "")
                paras[2].Value = sPartNo;
            else
                paras[2] = null;

            if (sShift != "")
                paras[3].Value = sShift;
            else
                paras[3] = null;

            if (sModule != "")
                paras[4].Value = sModule;
            else
                paras[4] = null;

            if (sMachineID != "")
                paras[5].Value = sMachineID;
            else
                paras[5] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet SelectList(DateTime dDateFrom,DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            StringBuilder strSql = new StringBuilder();
            //2018/12/04 add Status
            strSql.Append(@"Select 
                            ROW_NUMBER() over(order by a.machineid asc, a.datetime desc) as ID
                            ,CONVERT(varchar(50), a.day,105) as Day
                            ,a.shift as Shift
                            ,a.dateTime as DateTime
                            ,'Machine' + a.machineID as MachineID
                            ,a.model as Model
                            ,c.machineID as Type
                            ,a.cavityCount as CavityCount
                            ,a.partNumberAll as PartNumberAll
                            ,a.partNumber as PartNumber
                            ,case when a.jigNo is null or a.jigNo = '' then 'NA' else a.jigNo end as JigNo
                            ,a.cavityCount as cavityCount      
                              --,(CONVERT (decimal(19,0),a.remarks ) + a.acountReading) as Accumulate     
                            ,case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end as Accumulate     
                            --,case when ((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) ) > 0) then  CONVERT(VARCHAR(8), CONVERT (decimal(19,3),((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) )) / (3600/(case when a.cycleTime = 0 then 42 else a.cycleTime end))))    else CONVERT(VARCHAR(8),0)  end as NeedProductionTime                                                      
                            
                            --,case when ((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) ) > 0) then   ((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) )) / (3600/a.cycleTime)    else 0  end as NeedProductionTime                                                      
                            -- ,case when ((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) ) > 0) then   CONVERT(VARCHAR(8),CONVERT(TIME,DATEADD(ss, ((isnull( b.refField01,0) - (case when a.OkAccumulation = '' or a.OkAccumulation is null then 0 else  (CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty) end) )) / (3600/a.cycleTime) * 86400  ,'1900-01-01')))  else (CONVERT(VARCHAR(8),CONVERT(TIME,DATEADD(ss,0,'1900-01-01'))))  end as NeedProductionTime                           
                            
                            -- fuck this shit
                            ,case 
                            when 
                            (
	                            (       
		                            case when  isnull( b.refField01,'') = '' then 0 else b.refField01 end  -    (
									                                case 
										                            when a.OkAccumulation = '' or a.OkAccumulation is null 
										                            then 0 
									                                else ( 
												                            CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty
											                              ) end
									                               ) 
	                            ) > 0
                            )
											
                            then  CONVERT(VARCHAR(8), 
			                              CONVERT (decimal(19,3),
						                            ( 
							                            ( 
								                            case when  isnull( b.refField01,'') = '' then 0 else b.refField01 end  - ( case 
															                            when a.OkAccumulation = '' or a.OkAccumulation is null 
															                            then 0 
															                            else  (
																	                            CONVERT (decimal(19,0), a.OkAccumulation ) + a.acceptQty
																                              ) 
														                                end) 
							                             )
						                            ) 
						
						                            /
						
						                            ( 
							                            3600 / ( case 
									                             when a.cycleTime = 0 
									                             then 42 
									                             else a.cycleTime 
									                             end)
						                            )
				                            )
		                            )    

                            else CONVERT(VARCHAR(8),0)

                            end as NeedProductionTime

                            ,a.status as Status
                            ,isnull( a.Setup,0) as Setup
                            ,a.startTime as StartTime ");


            //RunningTime
            strSql.Append(",convert(varchar(50), ");
            strSql.Append(@"case when a.stopTime IS not null 
	                            then  datediff(second ,convert(datetime, a.startTime) , convert(datetime, a.stopTime))
                            else ");
            strSql.Append("     ( ");
            strSql.Append(@"       select top 1 convert(varchar(50), Sum(aa.RunningSeconds) ,108) from 
			                       ( ");
            strSql.Append(@"           select top 5    --top 1 top 5  for optimize the performance
					                        datediff(second ,convert(datetime, StartTime) , convert(datetime, isnull( EndTime, getdate()))) as RunningSeconds
					                        , MachineStatus
				                       from MouldingMachineStatus ");
            strSql.Append("            where MachineStatus='Running' and MachineID = a.machineID  and shif = a.shift and day = a.day   ");
            strSql.Append("        ) aa where aa.MachineStatus='Running' group by aa.MachineStatus ");

            strSql.Append("    ) ");
            strSql.Append(" end ) as Time ");
            //RunningTimea.
            

            strSql.Append(@",convert(numeric(10,0),(a.acceptQty - ISNULL(a.Setup,0) *a.cavityCount)) as OK
                           ,convert(numeric(10,0),(a.rejectQty + ISNULL(a.Setup,0) *a.cavityCount)) as NG
                            --,a.rejectQty as NG
                            ,a.QCNGQTY as QCNGQTY
                            ,a.acountReading  as OutputPerPCS
                            ,a.acountReading/a.cavityCount as OutputPerSet
                            ,convert(varchar(50),a.acountReading ) + '(' + convert(varchar(50), convert(numeric(10,0), a.acountReading/a.cavityCount) )  + ')'  as Output
                            --,CONVERT(varchar(50), convert(numeric(10,2), a.rejectQty / a.acountReading  * 100)) + '%' as RejRate
                            ,CONVERT(varchar(50), convert(numeric(10,2), (a.rejectQty + ISNULL(a.Setup,0) *a.cavityCount) / a.acountReading  * 100)) + '%' as RejRate                            
                            ,convert(numeric(10,0), a.rejectQty / a.acountReading *1000000) as RejPPM
                            ,isnull( b.refField01,0) as TargetQty

                            ");

            //hourly check time&userID
            strSql.Append(@",a.userName as UserName
                            ,a.opSign01,a.opSign02,a.opSign03,a.opSign04,a.opSign05,a.opSign06,a.opSign07,a.opSign08,a.opSign09,a.opSign10,a.opSign11,a.opSign12
                            ,a.qaSign01,a.qaSign02,a.qaSign03,a.qaSign04,a.qaSign05,a.qaSign06,a.qaSign07,a.qaSign08,a.qaSign09,a.qaSign10,a.qaSign11,a.qaSign12
                            ,a.Material_MHCheck,a.Material_MHCheckTime,a.Material_Opcheck,a.Material_OpCheckTime,a.Material_LeaderCheck,a.Material_LeaderCheckTime,a.LeaderCheck,a.LeaderCheckTime,a.SupervisorCheck,a.SupervisorCheckTime,a.remarks,a.status,a.OkAccumulation,a.cycleTime "
                        );

            strSql.Append(@"from MouldingViTracking a 
                            left join MouldingPqm b on a.MachineID = SUBSTRING( b.machineID ,4,1 ) ");
            strSql.Append(" left join MouldingBom c on c.partNumber = a.partNumber  ");
            strSql.Append("                where a.acceptQty +a.rejectQty > 0 and a.acountReading >0  ");

            strSql.Append(" and a.dateTime > @DateFrom ");
            strSql.Append(" and a.dateTime < @DateTo ");

            if (sPartNo != "")
                strSql.Append(" and a.partNumber = @partNumber");

            if (sShift != "")
                strSql.Append(" and a.shift = @shift");

            if (sModule != "")
                strSql.Append(" and a.model = @model");

            if (sMachineID != "")
                strSql.Append(" and a.machineID = @machineID");
            
            strSql.Append(" order by a.machineid asc, a.datetime desc");


            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("partNumber",SqlDbType.VarChar),
                new SqlParameter("shift",SqlDbType.VarChar),
                new SqlParameter("model",SqlDbType.VarChar),
                new SqlParameter("machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sPartNo != "")
                paras[2].Value = sPartNo;
            else
                paras[2] = null;

            if (sShift != "")
                paras[3].Value = sShift;
            else
                paras[3] = null;

            if (sModule != "")
                paras[4].Value = sModule;
            else
                paras[4] = null;

            if (sMachineID != "")
                paras[5].Value = sMachineID;
            else
                paras[5] = null;


            return  DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet SelectHourlyCheck(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"Select  distinct               
            --ROW_NUMBER() over(order by machineid asc, datetime desc) as ID
            CONVERT(varchar(16), Day, 105) as day
            ,Shift
            ,MachineID
            ,PartNumberAll
            ,userName as UserName
            ,dateTime


					                                        --OP Sign--
            ,case when opSign08 != '' then SUBSTRING(opSign08,1,5) when opSign08 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '9:00'  else '21:00' end))  then '' else 'NA' end as OP08
            ,case when opSign09 != '' then SUBSTRING(opSign09,1,5) when opSign09 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '10:00'  else '22:00' end))  then '' else 'NA' end as OP09
            ,case when opSign10 != '' then SUBSTRING(opSign10,1,5) when opSign10 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '11:00'  else '23:00' end))  then '' else 'NA' end as OP10
            ,case when opSign11 != '' then SUBSTRING(opSign11,1,5) when opSign11 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '12:00' else '00:00' end))  then '' else 'NA' end as OP11
            ,case when opSign12 != '' then SUBSTRING(opSign12,1,5) when opSign12 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '13:00' else '1:00' end))   then '' else 'NA' end as OP12
            ,case when opSign01 != '' then SUBSTRING(opSign01,1,5) when opSign01 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '14:00' else '2:00' end))   then '' else 'NA' end as OP01
            ,case when opSign02 != '' then SUBSTRING(opSign02,1,5) when opSign02 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '15:00' else '3:00' end))   then '' else 'NA' end as OP02
            ,case when opSign03 != '' then SUBSTRING(opSign03,1,5) when opSign03 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '16:00' else '4:00' end))   then '' else 'NA' end as OP03
            ,case when opSign04 != '' then SUBSTRING(opSign04,1,5) when opSign04 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '17:00' else '5:00' end))   then '' else 'NA' end as OP04
            ,case when opSign05 != '' then SUBSTRING(opSign05,1,5) when opSign05 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '18:00' else '6:00' end))   then '' else 'NA' end as OP05
            ,case when opSign06 != '' then SUBSTRING(opSign06,1,5) when opSign06 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '19:00' else '7:00' end))   then '' else 'NA' end as OP06
            ,case when opSign07 != '' then SUBSTRING(opSign07,1,5) when opSign07 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '20:00' else '8:00' end))   then '' else 'NA' end as OP07

					                                        --QA Sign--
            ,case when qaSign08 != '' then SUBSTRING(qaSign08,1,5) when qaSign08 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '9:00'  else '21:00' end))  then '' else 'NA' end as QA08
            ,case when qaSign09 != '' then SUBSTRING(qaSign09,1,5) when qaSign09 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '10:00'  else '22:00' end))  then '' else 'NA' end as QA09
            ,case when qaSign10 != '' then SUBSTRING(qaSign10,1,5) when qaSign10 = '' and  startTime < CONVERT (datetime,convert(varchar(32),day,23) +' '+ (case when shift='Day' then '11:00'  else '23:00' end))  then '' else 'NA' end as QA10
            ,case when qaSign11 != '' then SUBSTRING(qaSign11,1,5) when qaSign11 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '12:00' else '00:00' end))  then '' else 'NA' end as QA11
            ,case when qaSign12 != '' then SUBSTRING(qaSign12,1,5) when qaSign12 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '13:00' else '1:00' end))   then '' else 'NA' end as QA12
            ,case when qaSign01 != '' then SUBSTRING(qaSign01,1,5) when qaSign01 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '14:00' else '2:00' end))   then '' else 'NA' end as QA01
            ,case when qaSign02 != '' then SUBSTRING(qaSign02,1,5) when qaSign02 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '15:00' else '3:00' end))   then '' else 'NA' end as QA02
            ,case when qaSign03 != '' then SUBSTRING(qaSign03,1,5) when qaSign03 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '16:00' else '4:00' end))   then '' else 'NA' end as QA03
            ,case when qaSign04 != '' then SUBSTRING(qaSign04,1,5) when qaSign04 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '17:00' else '5:00' end))   then '' else 'NA' end as QA04
            ,case when qaSign05 != '' then SUBSTRING(qaSign05,1,5) when qaSign05 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '18:00' else '6:00' end))   then '' else 'NA' end as QA05
            ,case when qaSign06 != '' then SUBSTRING(qaSign06,1,5) when qaSign06 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '19:00' else '7:00' end))   then '' else 'NA' end as QA06
            ,case when qaSign07 != '' then SUBSTRING(qaSign07,1,5) when qaSign07 = '' and  startTime < CONVERT (datetime,convert(varchar(32),case when shift='Day' then day else DATEADD(DAY,1,day) end,23) +' '+ (case when shift='Day' then '20:00' else '8:00' end))   then '' else 'NA' end as QA07


            ,Material_MHCheck
            ,SUBSTRING(CONVERT(varchar(30),Material_MHCheckTime,108),1,5) as Material_MHCheckTime
            ,Material_Opcheck
            ,SUBSTRING(CONVERT(varchar(30),Material_OpCheckTime,108),1,5) as Material_OpCheckTime
            ,Material_LeaderCheck
            ,SUBSTRING(CONVERT(varchar(30),Material_LeaderCheckTime,108),1,5) as Material_LeaderCheckTime
            ,LeaderCheck
            ,SUBSTRING(CONVERT(varchar(30),LeaderCheckTime,108),1,5) as LeaderCheckTime
            ,SupervisorCheck
            ,SUBSTRING(CONVERT(varchar(30),SupervisorCheckTime,108),1,5) as SupervisorCheckTime

            from MouldingViTracking

            where acceptQty +rejectQty > 0 and acountReading >0  ");

      

            strSql.Append(" and dateTime > @DateFrom ");
            strSql.Append(" and dateTime < @DateTo ");

            if (sPartNo != "")
                strSql.Append(" and partNumber = @partNumber");

            if (sShift != "")
                strSql.Append(" and shift = @shift");

            if (sModule != "")
                strSql.Append(" and model = @model");

            if (sMachineID != "")
                strSql.Append(" and machineID = @machineID");

            strSql.Append(" order by machineid asc, datetime desc");


            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("partNumber",SqlDbType.VarChar),
                new SqlParameter("shift",SqlDbType.VarChar),
                new SqlParameter("model",SqlDbType.VarChar),
                new SqlParameter("machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sPartNo != "")
                paras[2].Value = sPartNo;
            else
                paras[2] = null;

            if (sShift != "")
                paras[3].Value = sShift;
            else
                paras[3] = null;

            if (sModule != "")
                paras[4].Value = sModule;
            else
                paras[4] = null;

            if (sMachineID != "")
                paras[5].Value = sMachineID;
            else
                paras[5] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }



        // 2018 12 08 modified by wei lijia , add 2 parameter : date not in & except weekend  
        internal DataSet ProductionReport_withMQC(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sModule, bool bOnlyMqc)
        {
            string sConditionMFG = "";
            string sConditionMQC = "";
            
            if (sMachineID.Trim().Length >0 )
            {
                sConditionMFG += " and MachineID = @MachineID ";
            }

            if (sPartNo.Trim().Length > 0)
            {
                sConditionMFG += " and partNumber = @PartNo ";
                sConditionMQC += " and partNumber = @PartNo ";
            }

            if (sModule.Trim().Length > 0)
            {
                sConditionMFG += " and Model = @Model ";
                sConditionMQC += " and Model = @Model ";
            }

            string sCondition_OnlyMqc = "";
            if(bOnlyMqc)
            {
                sCondition_OnlyMqc += " and y1.MQC_Reject is not null ";
            }
            
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select 'Machine' + x.machineID as MachineID ,x.partNumber as PartNo,x.MFG_Date,x.model as Model ,x.jigNo as JigNo ,x.customer as Customer ,x.TotalQTY as TotalQTY ");
            strSql.Append("select 'Machine' + x.machineID as MachineID ,x.partNumber as PartNo, convert(varchar(50),cast(x.MFG_Date as datetime),105) as  MFG_Date,x.model as Model  ,x.customer as Customer ,x.TotalQTY as TotalQTY ");
            strSql.Append(" , x.TotalReject as MFG_Reject , x.QCNG as IPQC_NG, x.TotalPass as MFG_Pass                                                       ");
            strSql.Append(" , convert(varchar, convert(numeric(10,2),(( isnull(x.TotalReject,0)+isnull(x.QCNG,0))*100/x.TotalQTY)))+'%' as MFG_RejRate    ");
            strSql.Append(" , case when isnull(y1.MQC_Date ,'') = '' then '' else convert(varchar(50),cast(isnull(y1.MQC_Date ,'') as datetime),105) end as MQC_1st_CHK   ");
            strSql.Append(" , isnull(y1.MQC_Reject,0) as MQC_1st_Reject ");
            //strSql.Append(" , isnull(y1.MQC_Pass,0) as MQC_1st_Pass ");
            strSql.Append(" , convert(varchar,convert(numeric(10,2),((isnull(y1.MQC_Reject,0))*100/x.TotalPass)))+'%'as MQC_1st_RejRate ");
            strSql.Append(", case when isnull(y2.MQC_Date ,'') = '' then '' else  convert(varchar(50),cast(isnull(y2.MQC_Date ,'') as datetime),105) end  as MQC_2nd_CHK    ");
            strSql.Append(" , isnull(y2.MQC_Reject,0) as MQC_2nd_Reject ");
            //strSql.Append(", isnull(y2.MQC_Pass,0) as MQC_2nd_Pass   ");
            strSql.Append(" , convert(varchar,convert(numeric(10,2),((isnull(y2.MQC_Reject,0) )*100/x.TotalPass)))+'%'as MQC_2nd_RejRate ");
            strSql.Append(" , convert(varchar,convert(numeric(10,2),((isnull(y1.MQC_Reject,0)+isnull(y2.MQC_Reject,0) )*100/x.TotalPass)))+'%'as MQC_RejRate ");
            strSql.Append(" , convert(varchar,convert(numeric(10,2), ((isnull(y1.MQC_Reject,0)+isnull(y2.MQC_Reject,0)+isnull(x.TotalReject,0)+isnull(x.QCNG,0))*100/x.TotalQTY)) ) + '%' as Total_RejRate ");
            strSql.Append(" from ( SELECT [machineID]                                                                                                     ");
            strSql.Append("    ,convert(varchar,day,23) as MFG_Date ,[partNumber] ,[jigNo] ,[model] ,[cavityCount] ,[customer] ,[partNumberAll]           ");
            strSql.Append("    ,sum([acountReading]) as TotalQTY ,sum([rejectQty]) as TotalReject,sum([QCNGQTY]) as QCNG,sum([acceptQty]) as TotalPass    ");
            strSql.Append("    FROM [MouldingViTracking]                                                                                                  ");
            strSql.Append("   where day >= @DateFrom and Day <= @DateTo                                                                                   ");
            strSql.Append(sConditionMFG);                                                                                                                 
            strSql.Append("   group by [machineID] ,[day]  ,[partNumber]  ,[jigNo]  ,[model]  ,[cavityCount] ,[customer] ,[partNumberAll]                 ");
            strSql.Append("   ) x                                                                                                                         ");
            strSql.Append(" left join                                                                                                                     ");
            strSql.Append(" (SELECT [partNumber] ,convert(varchar, [MfgDate],23) as MFG_Date,[jigNo],[model] ,[cavityCount],[customer]       ");
            strSql.Append("  ,sum(acceptQty) as MQC_Pass ,sum([rejectQty]) as MQC_Reject   ,convert(varchar,min([day]),23)  as MQC_Date                    ");
            strSql.Append("  FROM [MouldingQaViTracking]                                                                                                  ");
            //strSql.Append("  where day >= @DateFrom and Day <= @DateTo   and refField01 = 1                                                               ");
            strSql.Append("  where  refField01 = 1                                                               ");
            strSql.Append(sConditionMQC);                                                                                                                 
            strSql.Append("  group by                                                                                                                     ");
            strSql.Append("  [partNumber],[MfgDate],[jigNo],[model],[cavityCount],[customer]                                                  ");
            strSql.Append("  ) y1                                                                                                                         ");
            strSql.Append("  on x.MFG_Date = y1.MFG_Date and x.partNumber = y1.partNumber                                                                 ");
            strSql.Append("  left join                                                                                                                    ");
            strSql.Append("  (SELECT [partNumber] ,convert(varchar, [MfgDate],23) as MFG_Date,[jigNo],[model],[cavityCount],[customer]       ");
            strSql.Append("  ,sum(acceptQty) as MQC_Pass ,sum([rejectQty]) as MQC_Reject   ,convert(varchar,min([day]),23)  as MQC_Date                    ");
            strSql.Append("  FROM [MouldingQaViTracking]                                                                                                  ");
            //strSql.Append("  where day >= @DateFrom and Day <= @DateTo   and refField01 = 2                                                               ");
            strSql.Append("  where  refField01 = 2                                                               ");
            strSql.Append(sConditionMQC);                                                                                                                 
            strSql.Append("  group by                                                                                                                     ");
            strSql.Append("  [partNumber],[MfgDate],[jigNo],[model],[cavityCount],[customer]                                                  ");
            strSql.Append("  ) y2                                                                                                                         ");
            strSql.Append("  on x.MFG_Date = y2.MFG_Date and x.partNumber = y2.partNumber                                                                 ");
            strSql.Append("  where x.TotalPass is not null and x.TotalPass >0   and x.TotalQTY is not null and x.TotalQTY >0                              ");
            strSql.Append(sCondition_OnlyMqc);
            strSql.Append("  order by x.MFG_Date asc, x.machineID asc                                                                                     ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@PartNo",SqlDbType.VarChar) ,
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Model",SqlDbType.VarChar)  
            };

            paras[0].Value = dDateFrom.Date;
            paras[1].Value = dDateTo.Date;
            paras[2].Value = sPartNo;
            paras[3].Value = sMachineID;
            paras[4].Value = sModule;

            if (sPartNo    != "") { paras[2].Value = sPartNo;    } else { paras[2] = null; }
            if (sMachineID != "") { paras[3].Value = sMachineID; } else { paras[3] = null; } 
            if (sModule    != "") { paras[4].Value = sModule;    } else { paras[4] = null; }
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        internal DataSet ProductionReport_withMQCDetail(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sModule, bool bOnlyMqc,string sCheckerID)
        {
            string sConditionMFG = "";
            string sConditionMQC = "";

            if (sMachineID.Trim().Length > 0)
            {
                sConditionMFG += " and MachineID = @MachineID ";
            }

            if (sPartNo.Trim().Length > 0)
            {
                sConditionMFG += " and partNumber = @PartNo ";
                sConditionMQC += " and partNumber = @PartNo ";
            }

            if (sModule.Trim().Length > 0)
            {
                sConditionMFG += " and Model = @Model ";
                sConditionMQC += " and Model = @Model ";
            }
            if (sCheckerID.Trim().Length > 0)
            {
                sConditionMFG += " and userID = @userID ";
            }

            string sCondition_OnlyMqc = "";
            if (bOnlyMqc)
            {
                sCondition_OnlyMqc += " and y1.MQC_Reject is not null ";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT machineID as MachineID, CONVERT(VARCHAR(10), datetime, 105) as Date, ");
            strSql.Append(" partNumber as PartNumber, boxID as BoxID, model as Model,refField01 as No_OF_Check ,userID as  ChkerID ");
            strSql.Append("  , isnull(TotalQty, 0) + isnull(rejectQty, 0) as RejectQTY , 672 - isnull(TotalQty, 0) - isnull(rejectQty, 0) as AcceptQty ");
            strSql.Append(" ,  convert(varchar, convert(numeric(10, 2), ISNULL((isnull(TotalQty, 0) + isnull(rejectQty, 0)) / 672 * 100, 0))) + '%' as Reject ");
            strSql.Append(" from MouldingQaViTracking             where   day >= @DateFrom and Day <= @DateTo ");
            strSql.Append(sConditionMFG);
            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@PartNo",SqlDbType.VarChar) ,
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Model",SqlDbType.VarChar),
                new SqlParameter("@userID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom.Date;
            paras[1].Value = dDateTo.Date;
            paras[2].Value = sPartNo;
            paras[3].Value = sMachineID;
            paras[4].Value = sModule;
            paras[5].Value = sCheckerID;

            if (sPartNo != "") { paras[2].Value = sPartNo; } else { paras[2] = null; }
            if (sMachineID != "") { paras[3].Value = sMachineID; } else { paras[3] = null; }
            if (sModule != "") { paras[4].Value = sModule; } else { paras[4] = null; }
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        // 2018 12 04 modified by wei lijia , add 2 parameter : date not in & except weekend  
        public DataSet SummaryByMachine(DateTime dDateFrom, DateTime dDateTo, string sShift,string sDateNotIn, bool bExceptWeekend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"Select 
                            'Machine' + machineID as MachineID
                            , SUM(acceptQty) as OK
                            , SUM(rejectQty) as NG
                            , SUM(acceptQty + rejectQty) as Output
                            , CONVERT(varchar(50), convert(numeric(10, 2), SUM(rejectQty) / (SUM(acceptQty) + SUM(rejectQty)) * 100)) + '%' as RejRate 
                            , CONVERT(varchar(50), convert(numeric(10, 2), SUM(acceptQty) / (SUM(acceptQty) + SUM(rejectQty)) * 100)) + '%' as Quality ");


            strSql.Append(" ,isnull( ( ");

            strSql.Append(@" select SUM( (aa.acceptQty + aa.rejectQty) / aa.cavityCount * bb.cycleTime ) from MouldingViTracking aa left join MouldingBom bb on aa.partNumberAll = bb.partNumberAll where ");

            strSql.Append(" aa.dateTime >= @DateFrom ");
            strSql.Append(" and aa.dateTime< @DateTo ");
            strSql.Append(" and aa.machineID = a.machineID ");

            if (sShift != "")
            {
                strSql.Append(" and aa.shift = @Shift ");
            }
         

            strSql.Append(" ) ,0)as TotalTime ");



            strSql.Append(" from MouldingViTracking a ");

            strSql.Append(@"where acceptQty +rejectQty > 0 ");

            strSql.Append(" and a.dateTime >= @DateFrom ");
            strSql.Append(" and a.dateTime < @DateTo ");

            if (sShift != "")
            {
                strSql.Append(" and a.Shift = @Shift ");
            }

            //2018 12 04
            if (sDateNotIn != "")
            {
                strSql.Append(" and DATEPART(day, a.Day) not in (");

                string[] DayArr = sDateNotIn.Split(',');
                for (int i = 0; i < DayArr.Length; i++)
                {
                    strSql.Append(DayArr[i] + ",");
                }

                strSql.Remove(strSql.Length - 1, 1);

                strSql.Append(" ) ");
            }
            //2018 12 04
            if (bExceptWeekend)
            {
                strSql.Append(" and  datepart(weekday,a.Day)  not in ('7','1') ");
            }

            strSql.Append(@" group by a.machineID ");

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Shift",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);


            if (sShift != "")
            {
                paras[2].Value = sShift;
            }
            else
            {
                paras[2] = null;
            }



            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }   
        
        

        public SqlCommand UpdateSetupWasteMaterialCommond(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update MouldingViTracking  set ");
            strSql.Append(" Setup = @Setup ");
            strSql.Append(" ,WastageMaterial01 = @WastageMaterial01 ");
            strSql.Append(" ,WastageMaterial02 = @WastageMaterial02 ");

            strSql.Append(" where Day = @Day  ");
            strSql.Append(" and partNumberAll = @partNumberAll  ");
            strSql.Append(" and MachineID = @MachineID  ");

            if (model.Shift != StaticRes.Global.Shift.ALL && model.Shift != "All")
            {
                strSql.Append(" and Shift = @Shift  ");
            }


            SqlParameter[] paras =
            {
                new SqlParameter("@Setup",SqlDbType.Decimal),
                new SqlParameter("@WastageMaterial01",SqlDbType.Decimal),
                new SqlParameter("@WastageMaterial02",SqlDbType.Decimal),
                new SqlParameter("@Day",SqlDbType.DateTime),
                new SqlParameter("@partNumberAll",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Shift",SqlDbType.VarChar)
            };


            paras[0].Value = model.Setup == null ? (object)DBNull.Value : model.Setup;
            paras[1].Value = model.WastageMaterial01 == null ? (object)DBNull.Value : model.WastageMaterial01;
            paras[2].Value = model.WastageMaterial02 == null ? (object)DBNull.Value : model.WastageMaterial02;
            paras[3].Value = model.Day == null ? (object)DBNull.Value : model.Day;
            paras[4].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            paras[5].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;

            if (model.Shift != StaticRes.Global.Shift.ALL && model.Shift != "All")
            {
                paras[6].Value = model.Shift;
            }
            else
            {
                paras[6] = null;
            }







            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public SqlCommand UpdateProduction(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update MouldingViTracking  set ");
            strSql.Append(" AcceptQty = @AcceptQty ");
            strSql.Append(" ,RejectQty = @RejectQty ");
            strSql.Append(" ,AcountReading = @AcountReading ");
            strSql.Append(" ,JigNo = @JigNo ");

            strSql.Append(" where Day = @Day  ");
            strSql.Append(" and partNumberAll = @partNumberAll  ");
            strSql.Append(" and partNumber = @partNumber  ");
            strSql.Append(" and MachineID = @MachineID  ");
            strSql.Append(" and Shift = @Shift  ");
            


            SqlParameter[] paras =
            {
                new SqlParameter("@AcceptQty",SqlDbType.Decimal),
                new SqlParameter("@RejectQty",SqlDbType.Decimal),
                new SqlParameter("@AcountReading",SqlDbType.Decimal),
                new SqlParameter("@JigNo",SqlDbType.VarChar),
                new SqlParameter("@Day",SqlDbType.DateTime),
                new SqlParameter("@partNumberAll",SqlDbType.VarChar),
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Shift",SqlDbType.VarChar)
            };


            paras[0].Value = model.AcceptQty == null ? (object)DBNull.Value : model.AcceptQty;
            paras[1].Value = model.RejectQty == null ? (object)DBNull.Value : model.RejectQty;
            paras[2].Value = model.AcountReading == null ? (object)DBNull.Value : model.AcountReading;
            paras[3].Value = model.JigNo == null ? (object)DBNull.Value : model.JigNo;
            paras[4].Value = model.Day == null ? (object)DBNull.Value : model.Day;
            paras[5].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            paras[6].Value = model.PartNumber == null ? (object)DBNull.Value : model.PartNumber;
            paras[7].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            paras[8].Value = model.Shift == null ? (object)DBNull.Value : model.Shift;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public SqlCommand DeleteProduction(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Delete MouldingViTracking  where ");
            strSql.Append(" AcceptQty = @AcceptQty ");
            strSql.Append(" and RejectQty = @RejectQty ");
            strSql.Append(" and AcountReading = @AcountReading ");

            strSql.Append(" and  Day = @Day  ");
            strSql.Append(" and partNumberAll = @partNumberAll  ");
            strSql.Append(" and partNumber = @partNumber  ");
            strSql.Append(" and MachineID = @MachineID  ");
            strSql.Append(" and Shift = @Shift  ");



            SqlParameter[] paras =
            {
                new SqlParameter("@AcceptQty",SqlDbType.Decimal),
                new SqlParameter("@RejectQty",SqlDbType.Decimal),
                new SqlParameter("@AcountReading",SqlDbType.Decimal),
                new SqlParameter("@Day",SqlDbType.DateTime),
                new SqlParameter("@partNumberAll",SqlDbType.VarChar),
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Shift",SqlDbType.VarChar)
            };


            paras[0].Value = model.AcceptQty == null ? (object)DBNull.Value : model.AcceptQty;
            paras[1].Value = model.RejectQty == null ? (object)DBNull.Value : model.RejectQty;
            paras[2].Value = model.AcountReading == null ? (object)DBNull.Value : model.AcountReading;
            paras[3].Value = model.Day == null ? (object)DBNull.Value : model.Day;
            paras[4].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            paras[5].Value = model.PartNumber == null ? (object)DBNull.Value : model.PartNumber;
            paras[6].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            paras[7].Value = model.Shift == null ? (object)DBNull.Value : model.Shift;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public SqlCommand InsertProductionHistory(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingViHistory(");
            strSql.Append("id,machineID,dateTime,partNumber,jigNo,model,cavityCount,partsWeight,cycleTime,targetQty,userName,userID,acountReading,rejectQty,acceptQty,startTime,stopTime,day,shift,status,matPart01,matPart02,matLot01,matLot02,opSign01,opSign02,opSign03,opSign04,opSign05,opSign06,opSign07,opSign08,opSign09,opSign10,opSign11,opSign12,qaSign01,qaSign02,qaSign03,qaSign04,qaSign05,qaSign06,qaSign07,qaSign08,qaSign09,qaSign10,qaSign11,qaSign12,customer,lastUpdatedTime,trackingID,remarks,QCNGQTY,Material_MHCheck,Material_MHCheckTime,Material_Opcheck,Material_OpCheckTime,Material_LeaderCheck,Material_LeaderCheckTime,LeaderCheck,LeaderCheckTime,SupervisorCheck,SupervisorCheckTime,partNumberAll,parts2Weight,lastQty,OkAccumulation,refField01,refField02,refField03,refField04,refField05)");
            strSql.Append(" values (");
            strSql.Append("@id,@machineID,@dateTime,@partNumber,@jigNo,@model,@cavityCount,@partsWeight,@cycleTime,@targetQty,@userName,@userID,@acountReading,@rejectQty,@acceptQty,@startTime,@stopTime,@day,@shift,@status,@matPart01,@matPart02,@matLot01,@matLot02,@opSign01,@opSign02,@opSign03,@opSign04,@opSign05,@opSign06,@opSign07,@opSign08,@opSign09,@opSign10,@opSign11,@opSign12,@qaSign01,@qaSign02,@qaSign03,@qaSign04,@qaSign05,@qaSign06,@qaSign07,@qaSign08,@qaSign09,@qaSign10,@qaSign11,@qaSign12,@customer,@lastUpdatedTime,@trackingID,@remarks,@QCNGQTY,@Material_MHCheck,@Material_MHCheckTime,@Material_Opcheck,@Material_OpCheckTime,@Material_LeaderCheck,@Material_LeaderCheckTime,@LeaderCheck,@LeaderCheckTime,@SupervisorCheck,@SupervisorCheckTime,@partNumberAll,@parts2Weight,@lastQty,@OkAccumulation,@refField01,@refField02,@refField03,@refField04,@refField05)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@partsWeight", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@targetQty", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@acountReading", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@matPart01", SqlDbType.VarChar,50),
                    new SqlParameter("@matPart02", SqlDbType.VarChar,50),
                    new SqlParameter("@matLot01", SqlDbType.VarChar,50),
                    new SqlParameter("@matLot02", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign01", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign02", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign03", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign04", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign05", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign06", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign07", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign08", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign09", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign10", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign11", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign12", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign01", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign02", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign03", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign04", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign05", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign06", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign07", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign08", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign09", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign10", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign11", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign12", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@QCNGQTY", SqlDbType.Decimal,9),
                    new SqlParameter("@Material_MHCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_MHCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@Material_Opcheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_OpCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@Material_LeaderCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_LeaderCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@LeaderCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@LeaderCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@SupervisorCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@SupervisorCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@partNumberAll", SqlDbType.VarChar,500),
                    new SqlParameter("@parts2Weight", SqlDbType.Decimal,9),
                    new SqlParameter("@lastQty", SqlDbType.Decimal,9),
                    new SqlParameter("@OkAccumulation", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),};
            parameters[0].Value = model.ID == null ? (object)DBNull.Value : model.ID;
            parameters[1].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            parameters[2].Value = model.Datetime == null ? (object)DBNull.Value : model.Datetime;
            parameters[3].Value = model.PartNumber == null ? (object)DBNull.Value : model.PartNumber;
            parameters[4].Value = model.JigNo == null ? (object)DBNull.Value : model.JigNo;
            parameters[5].Value = model.Model == null ? (object)DBNull.Value : model.Model;
            parameters[6].Value = model.CavityCount == null ? (object)DBNull.Value : model.CavityCount;
            parameters[7].Value = model.PartsWeight == null ? (object)DBNull.Value : model.PartsWeight;
            parameters[8].Value = model.CycleTime == null ? (object)DBNull.Value : model.CycleTime;
            parameters[9].Value = model.TargetQty == null ? (object)DBNull.Value : model.TargetQty;
            parameters[10].Value = model.UserName == null ? (object)DBNull.Value : model.UserName;
            parameters[11].Value = model.UserID == null ? (object)DBNull.Value : model.UserID;
            parameters[12].Value = model.AcountReading == null ? (object)DBNull.Value : model.AcountReading;
            parameters[13].Value = model.RejectQty == null ? (object)DBNull.Value : model.RejectQty;
            parameters[14].Value = model.AcceptQty == null ? (object)DBNull.Value : model.AcceptQty;
            parameters[15].Value = model.StartTime == null ? (object)DBNull.Value : model.StartTime;
            parameters[16].Value = model.StopTime == null ? (object)DBNull.Value : model.StopTime;
            parameters[17].Value = model.Day == null ? (object)DBNull.Value : model.Day;
            parameters[18].Value = model.Shift == null ? (object)DBNull.Value : model.Shift;
            parameters[19].Value = model.Status == null ? (object)DBNull.Value : model.Status;
            parameters[20].Value = model.MatPart01 == null ? (object)DBNull.Value : model.MatPart01;
            parameters[21].Value = model.MatPart02 == null ? (object)DBNull.Value : model.MatPart02;
            parameters[22].Value = model.MatLot01 == null ? (object)DBNull.Value : model.MatLot01;
            parameters[23].Value = model.MatLot02 == null ? (object)DBNull.Value : model.MatLot02;
            parameters[24].Value = model.OpSign01 == null ? (object)DBNull.Value : model.OpSign01;
            parameters[25].Value = model.OpSign02 == null ? (object)DBNull.Value : model.OpSign02;
            parameters[26].Value = model.OpSign03 == null ? (object)DBNull.Value : model.OpSign03;
            parameters[27].Value = model.OpSign04 == null ? (object)DBNull.Value : model.OpSign04;
            parameters[28].Value = model.OpSign05 == null ? (object)DBNull.Value : model.OpSign05;
            parameters[29].Value = model.OpSign06 == null ? (object)DBNull.Value : model.OpSign06;
            parameters[30].Value = model.OpSign07 == null ? (object)DBNull.Value : model.OpSign07;
            parameters[31].Value = model.OpSign08 == null ? (object)DBNull.Value : model.OpSign08;
            parameters[32].Value = model.OpSign09 == null ? (object)DBNull.Value : model.OpSign09;
            parameters[33].Value = model.OpSign10 == null ? (object)DBNull.Value : model.OpSign10;
            parameters[34].Value = model.OpSign11 == null ? (object)DBNull.Value : model.OpSign11;
            parameters[35].Value = model.OpSign12 == null ? (object)DBNull.Value : model.OpSign12;
            parameters[36].Value = model.QaSign01 == null ? (object)DBNull.Value : model.QaSign01;
            parameters[37].Value = model.QaSign02 == null ? (object)DBNull.Value : model.QaSign02;
            parameters[38].Value = model.QaSign03 == null ? (object)DBNull.Value : model.QaSign03;
            parameters[39].Value = model.QaSign04 == null ? (object)DBNull.Value : model.QaSign04;
            parameters[40].Value = model.QaSign05 == null ? (object)DBNull.Value : model.QaSign05;
            parameters[41].Value = model.QaSign06 == null ? (object)DBNull.Value : model.QaSign06;
            parameters[42].Value = model.QaSign07 == null ? (object)DBNull.Value : model.QaSign07;
            parameters[43].Value = model.QaSign08 == null ? (object)DBNull.Value : model.QaSign08;
            parameters[44].Value = model.QaSign09 == null ? (object)DBNull.Value : model.QaSign09;
            parameters[45].Value = model.QaSign10 == null ? (object)DBNull.Value : model.QaSign10;
            parameters[46].Value = model.QaSign11 == null ? (object)DBNull.Value : model.QaSign11;
            parameters[47].Value = model.QaSign12 == null ? (object)DBNull.Value : model.QaSign12;
            parameters[48].Value = model.Customer == null ? (object)DBNull.Value : model.Customer;
            parameters[49].Value = model.LastUpdatedTime == null ? (object)DBNull.Value : model.LastUpdatedTime;
            parameters[50].Value = model.TrackingID == null ? (object)DBNull.Value : model.TrackingID;
            parameters[51].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;
            parameters[52].Value = model.QCNGQTY == null ? (object)DBNull.Value : model.QCNGQTY;
            parameters[53].Value = model.Material_MHCheck == null ? (object)DBNull.Value : model.Material_MHCheck;
            parameters[54].Value = model.Material_MHCheckTime == null ? (object)DBNull.Value : model.Material_MHCheckTime;
            parameters[55].Value = model.Material_Opcheck == null ? (object)DBNull.Value : model.Material_Opcheck;
            parameters[56].Value = model.Material_OpCheckTime == null ? (object)DBNull.Value : model.Material_OpCheckTime;
            parameters[57].Value = model.Material_LeaderCheck == null ? (object)DBNull.Value : model.Material_LeaderCheck;
            parameters[58].Value = model.Material_LeaderCheckTime == null ? (object)DBNull.Value : model.Material_LeaderCheckTime;
            parameters[59].Value = model.LeaderCheck == null ? (object)DBNull.Value : model.LeaderCheck;
            parameters[60].Value = model.LeaderCheckTime == null ? (object)DBNull.Value : model.LeaderCheckTime;
            parameters[61].Value = model.SupervisorCheck == null ? (object)DBNull.Value : model.SupervisorCheck;
            parameters[62].Value = model.SupervisorCheckTime == null ? (object)DBNull.Value : model.SupervisorCheckTime;
            parameters[63].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            parameters[64].Value = model.Parts2Weight == null ? (object)DBNull.Value : model.Parts2Weight;
            parameters[65].Value = model.lastQty == null ? (object)DBNull.Value : model.lastQty;

            parameters[66].Value = model.OkAccumulation == null ? (object)DBNull.Value : model.OkAccumulation;
            parameters[67].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[68].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            parameters[69].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            parameters[70].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            parameters[71].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        internal DataSet GetModel_ByDayShiftAllPartMachine(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1  id,machineID,dateTime,partNumber,jigNo,model,cavityCount,partsWeight,cycleTime,targetQty,userName,userID,isnull(acountReading,0) as acountReading,rejectQty,isnull(acceptQty,0) as acceptQty,startTime,stopTime,day,shift,status,matPart01,matPart02,matLot01,matLot02,opSign01,opSign02,opSign03,opSign04,opSign05,opSign06,opSign07,opSign08,opSign09,opSign10,opSign11,opSign12,qaSign01,qaSign02,qaSign03,qaSign04,qaSign05,qaSign06,qaSign07,qaSign08,qaSign09,qaSign10,qaSign11,qaSign12,customer,lastUpdatedTime,trackingID,remarks,QCNGQTY,Material_MHCheck,Material_MHCheckTime,Material_Opcheck,Material_OpCheckTime,Material_LeaderCheck,Material_LeaderCheckTime,LeaderCheck,LeaderCheckTime,SupervisorCheck,SupervisorCheckTime,partNumberAll,parts2Weight,isnull(lastQty,0) as lastQty,OkAccumulation,refField01,refField02,refField03,refField04,refField05 from MouldingViTracking ");
            strSql.Append(" where Day=@Day and Shift=@hift and partNumber = @partNumber and MachineID = @MachineID");
            //2018 12 04 same data just status different
            strSql.Append(" order by dateTime desc  ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Day", SqlDbType.DateTime),
                    new SqlParameter("@hift", SqlDbType.VarChar, -1),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar , -1 ),
                    new SqlParameter("@MachineID", SqlDbType.VarChar , -1 ),
            };
            parameters[0].Value = model.Day == null ? (object)DBNull.Value : model.Day;
            parameters[1].Value = model.Shift == null ? (object)DBNull.Value : model.Shift;
            parameters[2].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            parameters[3].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:MouldingViTracking_DAL", "Function:		public DataSet GetModel_ByDayShiftPart(DateTime curDay, string curShift, string partNumber , string machineID)" + "TableName:MouldingViTracking", "");

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;

        }


        #region for output report 
        internal DataSet getAttendance(DateTime dDay, string sShift)
        {
               StringBuilder strSql = new StringBuilder();
            strSql.Append(@" 
                            select  count(distinct UserID)  as UserCount, b.USER_GROUP as UserGroup from 
                            (SELECT  distinct userid as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                             union all 
                             SELECT  distinct   material_mhcheck as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                               union all 
                             SELECT  distinct   material_opcheck as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                               union all 
                             SELECT  distinct   material_leaderCheck as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                               union all 
                             SELECT  distinct    leaderCheck as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                               union all 
                             SELECT  distinct    supervisorcheck as UserID
                              FROM [dbo].[MouldingViTracking]
                              where day = @Day and Shift = @Shift
                              ) a 
                              left join  [dbo].[User_DB] b on a.UserID = b.USER_ID 
                              where a.UserID is not null and userid != ''
                              group by b.USER_GROUP
                              order by b.USER_GROUP   "); 

            SqlParameter[] parameters = {
                    new SqlParameter("@Day", SqlDbType.DateTime),
                     new SqlParameter("@Shift", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:MouldingViHistory_DAL", "Function:  internal DataSet getAttendance(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");
            Common.Model.LMMSWatchLog_Model model = new Common.Model.LMMSWatchLog_Model();
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters,DBHelp. Connection.SqlServer.SqlConn_Moulding_Server);
        }
        /// <summary>
        ///  Columns: Module -- MachineCount -- OK -- NG -- Output -- Target --ProdHrs
        /// </summary>
        /// <param name="dDay"></param>
        /// <param name="sShift"></param>
        /// <returns></returns>
        internal DataSet getOutput(DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select x.[model] as Module , sum(modulecount) as MachineCount , sum (OK) as OK , sum(NG) as NG , sum(total) as OutPut , Sum(Target) as Target , 0 as ProdHrs
                                from
                                (SELECT  distinct [machineID]
                                   ,convert(decimal(10,1),  1.0/( 
                                   SELECT count( distinct model )
                                  FROM [dbo].[MouldingViTracking]
                                    where day = @Day
                                  and shift = @Shift
                                  and a.machineid = machineid 
                                  )) as ModuleCount 
                                      ,[model] 
                                      ,max([targetQty]) as target  
                                      ,max([acountReading]) as total
                                      ,max([rejectQty]+[QCNGQTY]) as NG
                                      ,min([acceptQty]) as OK  
                                  FROM  [dbo].[MouldingViTracking] a
                                  where day = @Day
                                  and shift = @Shift
                                  group by 
                                  [machineID] 
                                      ,[model] ) x
	                                  group by x.[model] "); 

            SqlParameter[] parameters = {
                    new SqlParameter("@Day", SqlDbType.DateTime),
                     new SqlParameter("@Shift", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:MouldingViHistory_DAL", "Function:  internal DataSet getOutput(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");
            Common.Model.LMMSWatchLog_Model model = new Common.Model.LMMSWatchLog_Model();
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters,DBHelp. Connection.SqlServer.SqlConn_Moulding_Server);
        }

        internal DataSet getDailyReject(DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();

            //new sql   add running time, 
            strSql.Append(@"select 
	x.machineid
	,x.Day
    ,x.shift
	,isnull(y.Model, '') as Model
	,isnull(y.PartNo, '') as PartNo


	,isnull( McStatus.totalHours, 0) as MCRunHours          
	,isnull( y.cavityCount, 1) as CavityCount
	,isnull( CONVERT(decimal(18,0), ( y.TotalQTY / y.cavityCount)),0) as TotalShots
	,isnull(y.TotalPassQTY,0) as TotalPassPCS
	,isnull(y.TotalRejectQTY,0) as RejectQtyPCS
	,CONVERT(varchar(50), convert(decimal(18,2), isnull( (y.TotalRejectQTY/y.TotalQTY *100),0))) + '%' as ProductionRejRate
	,CONVERT(float, isnull(y.MCAdjustment,0)) as SetupRejectPCS
	,CONVERT(varchar(50), convert(decimal(18,2), isnull( (isnull(y.MCAdjustment,0))/y.TotalQTY *100,0))) + '%' as SetupRejRate



	,isnull(y.JigNo, '') as JigNo                                          
	,isnull(y.Unit, 'Pcs') as Unit                                        
	,isnull(y.TotalQTY, '0') as TotalQTY      
	,isnull(isnull(y.TotalPassQTY, '0')-(isnull(y.MCAdjustment, '0') * y.cavityCount),0) as TotalPassQTY
	,isnull(isnull(y.TotalRejectQTY, '0')+(isnull(y.MCAdjustment, '0') * y.cavityCount),0) as TotalRejectQTY                       
	,isnull(y.TotalRejectQTY, 0) *  (isnull(( select distinct (unitcount) from mouldingBom where partNumber = y.PartNo) ,0))  as RejCost      
	,convert(varchar, isnull(y.[Total Rej.%], '0')) + '%' as [Total RejRate] 



	,isnull(z01.RejectQty, x.[White Dot]) as [White Dot]                   
	,isnull(z02.RejectQty, x.[Scratches]) as [Scratches]                   
	,isnull(z03.RejectQty, x.[Dented Mark]) as [Dented Mark]               
	,isnull(z04.RejectQty, x.[Shinning Dot]) as [Shinning Dot]             
	,isnull(z05.RejectQty, x.[Black Mark]) as [Black Mark]                 
	,isnull(z06.RejectQty, x.[Sink Mark]) as [Sink Mark]                   
	,isnull(z07.RejectQty, x.[Flow Mark]) as [Flow Mark]                   
	,isnull(z08.RejectQty, x.[High Gate]) as [High Gate]                   
	,isnull(z09.RejectQty, x.[Silver Steak]) as [Silver Steak]             
	,isnull(z10.RejectQty, x.[Black Dot]) as [Black Dot]                   
	,isnull(z11.RejectQty, x.[Oil Stain]) as [Oil Stain]                   
	,isnull(z12.RejectQty, x.[Flow Line]) as [Flow Line]                   
	,isnull(z13.RejectQty, x.[Over - Cut]) as [Over - Cut]
	,isnull(z14.RejectQty, x.[Crack]) as [Crack]                           
	,isnull(z15.RejectQty, x.[Short Mold]) as [Short Mold]                 
	,isnull(z16.RejectQty, x.[Stain Mark]) as [Stain Mark]                 
	,isnull(z17.RejectQty, x.[Weld Line]) as [Weld Line]                   
	,isnull(z18.RejectQty, x.[Flashes]) as [Flashes]                       
	,isnull(z19.RejectQty, x.[Foreign Materials]) as [Foreign Materials]   
	,isnull(z20.RejectQty, x.[Drag]) as [Drag]                             
	,isnull(z21.RejectQty, x.[Material Bleed]) as [Material Bleed]         
	,isnull(z22.RejectQty, x.[Bent]) as [Bent]                             
	,isnull(z23.RejectQty, x.[Defrom]) as [Defrom]                     
	,isnull(z24.RejectQty, x.[Gas Mark]) as [Gas Mark]         
	            
	,isnull(y.[IPQC Buyoff], '0') as [IPQC Buyoff]                         
	,convert(numeric(8,0) , isnull(y.MCAdjustment, '0')) as MCAdjustment                                 
	,isnull(y.MCAdjustment, 0) *  (isnull(( select distinct (unitcount) from mouldingBom where partNumber = y.PartNo) ,0))  as MCAdjustmentCost    
	  
	,isnull(y.[MC Adjust Scrap Short01], 0.0) + isnull(y.[MC Adjust Scrap Short02], 0.0) as [MC Adjust Scrap Short]  
	,( isnull(y.[MC Adjust Scrap Short01], 0.0) * (isnull((  select max(bomB1.Unit_Price) from mouldingBom bomA1 , Material_Inventory_Bom bomB1 where bomA1.partNumber = y.PartNo and bomA1.matPart01 = bomB1.Material_Part ) ,0.0))   + isnull(y.[MC Adjust Scrap Short02], 0.0) * (isnull((  select max(bomB2.Unit_Price) from mouldingBom bomA2 , Material_Inventory_Bom bomB2 where bomA2.partNumber = y.PartNo and bomA2.matPart02 = bomB2.Material_Part ) ,0.0))    ) as [MC Adjust Scrap Short Cost]  
	,isnull(y.Operator, '') as Operator                                    
	,isnull(y.[Inspd By], '') as [Inspd By]
                        
	--,isnull(y.Remarks, McStopReason.Reason) as Remarks
	,case when  isnull( McStopReason.Reason,'') = 'Create By Ping App' then '' else isnull( McStopReason.Reason,'') end  as Remarks

	from                                                                   
	(                                                                                
		select '1' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '2' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '3' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '4' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '5' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '6' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '7' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
		union all 
		select '8' as machineid, @shift as shift, @day as day, 0 as [White Dot], 0 as [Scratches], 0 as [Dented Mark], 0 as [Shinning Dot], 0 as [Black Mark], 0 as [Sink Mark], 0 as [Flow Mark], 0 as [High Gate], 0 as [Silver Steak], 0 as [Black Dot], 0 as [Oil Stain], 0 as [Flow Line], 0 as [Over - Cut], 0 as [Crack], 0 as [Short Mold], 0 as [Stain Mark], 0 as [Weld Line], 0 as [Flashes], 0 as [Foreign Materials], 0 as [Drag], 0 as [Material Bleed], 0 as [Bent], 0 as [Defrom], 0 as [Gas Mark]   
	) x                                                                                                               
	
	left join
	(
		SELECT 
			[machineID]                            
			,convert(varchar, [day], 23) as [Day]  
			,[shift]                               
			,[model] as Model 
			,[partNumber]   as PartNo    
			,[JigNo] as JigNo                      
			,'Pcs' as Unit   
			,cavityCount                           
			,sum([acountReading]) as TotalQTY      
			,sum([acceptQty]) as TotalPassQTY   

			-- ipqc rej. WastageMaterial01 , WastageMaterial02 算作 reject的数量   
			,sum(isnull(RejectQty,0) + isnull( QCNGQTY,0) + isnull(WastageMaterial01 *cavityCount ,0)+ isnull(WastageMaterial02 *cavityCount ,0)) as TotalRejectQTY

                                                           	  
			, convert(numeric(10, 2), sum(isnull( RejectQty,0) + isnull( QCNGQTY,0)) * 100 / sum([acountReading])) as 'Total Rej.%'
			,sum(QCNGQTY) as 'IPQC Buyoff'	  
			,sum(Setup) as MCAdjustment                                                                                 	  
			,sum(WastageMaterial01) as 'MC Adjust Scrap Short01'                                                            	  
			,sum(WastageMaterial02) as 'MC Adjust Scrap Short02'                                                            	  
			,Max([UserID]) as Operator                                                                                  	  
			,MAX([SupervisorCheck]) as 'Inspd By'                                                                                           	  
			,Status as Remarks                                                                                                
		FROM[MouldingViTracking]   
		where[acountReading] is not null and[acountReading] > 0  
		and  shift = @shift 
		and day = @day            
		group by[machineID],[Day],[shift],[model] ,[partNumber], [JigNo],[cavityCount] ,Status 
	) y 
	on x.machineid = y.machineid and x.shift = y.shift and x.day = y.Day

	left join  
	(
		select 
			MachineID
			,MachineStatus
			,SUM(DATEDIFF(second, starttime,case when endTime is null then getdate() else endtime end)) / 3600.00  as totalHours 
		from MouldingMachineStatus 
		where 1=1 and day = @day and Shif = @shift  and machinestatus = 'Running'
		group by MachineStatus, MachineID

	) McStatus
	on McStatus.MachineID = y.machineID

    left join 
    (
	    select day, shif, machineID,
	    SUBSTRING(
		    (SELECT  distinct ',' +  temp.Remark
		    FROM  ( select MachineID, Remark from MouldingMachineStatus where  Remark is not null and Remark != '' and Day=@day and Shif=@shift ) temp  
		    where temp.MachineID =  aaa.MachineID FOR xml path(''))     ,2,99) 
	    as Reason
	    from MouldingMachineStatus aaa
	    where day=@day and shif=@shift and DATEDIFF(second,starttime, EndTime) > 3600
	    group by day, shif, machineID
    )McStopReason on McStopReason.MachineID = x.machineid

	
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'White Dot'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z01 on z01.machineID = x.machineid  and z01.shift = x.shift and z01.Day = x.day  and z01.PartNo = y.PartNo 	
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Scratches'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z02 on z02.machineID = x.machineid  and z02.shift = x.shift and z02.Day = x.day  and z02.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Dented Mark'        group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z03 on z03.machineID = x.machineid  and z03.shift = x.shift and z03.Day = x.day  and z03.PartNo = y.PartNo 
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Shinning Dot'       group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z04 on z04.machineID = x.machineid  and z04.shift = x.shift and z04.Day = x.day  and z04.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Black Mark'         group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z05 on z05.machineID = x.machineid  and z05.shift = x.shift and z05.Day = x.day  and z05.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Sink Mark'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z06 on z06.machineID = x.machineid  and z06.shift = x.shift and z06.Day = x.day  and z06.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Flow Mark'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z07 on z07.machineID = x.machineid  and z07.shift = x.shift and z07.Day = x.day  and z07.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'High Gate'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z08 on z08.machineID = x.machineid  and z08.shift = x.shift and z08.Day = x.day  and z08.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Silver Steak'       group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z09 on z09.machineID = x.machineid  and z09.shift = x.shift and z09.Day = x.day  and z09.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Black Dot'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z10 on z10.machineID = x.machineid  and z10.shift = x.shift and z10.Day = x.day  and z10.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Oil Stain'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z11 on z11.machineID = x.machineid  and z11.shift = x.shift and z11.Day = x.day  and z11.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Flow Line'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z12 on z12.machineID = x.machineid  and z12.shift = x.shift and z12.Day = x.day  and z12.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Over-Cut'           group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z13 on z13.machineID = x.machineid  and z13.shift = x.shift and z13.Day = x.day  and z13.PartNo = y.PartNo  
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Crack'              group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z14 on z14.machineID = x.machineid  and z14.shift = x.shift and z14.Day = x.day  and z14.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Short Mold'         group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z15 on z15.machineID = x.machineid  and z15.shift = x.shift and z15.Day = x.day  and z15.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Stain Mark'         group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z16 on z16.machineID = x.machineid  and z16.shift = x.shift and z16.Day = x.day  and z16.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Weld Line'          group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z17 on z17.machineID = x.machineid  and z17.shift = x.shift and z17.Day = x.day  and z17.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Flashes'            group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z18 on z18.machineID = x.machineid  and z18.shift = x.shift and z18.Day = x.day  and z18.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Foreign Materials'  group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z19 on z19.machineID = x.machineid  and z19.shift = x.shift and z19.Day = x.day  and z19.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Drag'               group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z20 on z20.machineID = x.machineid  and z20.shift = x.shift and z20.Day = x.day  and z20.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Material Bleed'     group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z21 on z21.machineID = x.machineid  and z21.shift = x.shift and z21.Day = x.day  and z21.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Bent'               group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z22 on z22.machineID = x.machineid  and z22.shift = x.shift and z22.Day = x.day  and z22.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Defrom'             group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z23 on z23.machineID = x.machineid  and z23.shift = x.shift and z23.Day = x.day  and z23.PartNo = y.PartNo       
	left join (SELECT[machineID] , convert(varchar, [day], 23) as [Day],[shift],[partNumber] as PartNo, defectCode, SUM(rejectQty) as RejectQty  from[MouldingViDefectTracking]   where  shift = @shift and day = @day  and defectCode = 'Gas Mark'           group by[machineID],[Day],[shift], [partNumber]  , defectCode ) z24 on z24.machineID = x.machineid  and z24.shift = x.shift and z24.Day = x.day  and z24.PartNo = y.PartNo       

order by x.Day,x.shift,x.machineid");

          

            SqlParameter[] parameters = {
                new SqlParameter( "@Day", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:MouldingViHistory_DAL", "Function:  internal DataSet getOutput(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");
                
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        #endregion





        internal DataTable getProductivityReportForMoulding(DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" 
                    select
	                    case when aa.type='Button' then 1 
		                        when aa.type='Knob'   then 2
		                        when aa.type='Len'    then 3
		                        when aa.type='Panel'  then 4
   		                        when aa.type='Mould Test'  then 5
		                        when isnull(aa.type,'')='' then 10    
			                    else 11
		                        end as SN

	                    ,aa.type as ProductType

	                    ,'' as ManPower
	                    ,'' as Attendance
	                    ,'' as AttendRate                 
	                    ,convert(varchar(50),    CONVERT(numeric(18,4), sum(DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE()))) / 3600.0)) as ActualHR
	                    ,convert(numeric(18,0),sum(aa.TargetQty)) as TargetQty
	                    , sum(aa.OK) as ActualQty
	                    , sum(aa.NG) as RejectQty
	                    , sum(aa.TotalQty) as TotalQty
                        , sum(aa.TotalPCS) as TotalPCS
	                    , '96:00:00' as TargetHR
	                    , CONVERT(varchar, convert(numeric(18,2), convert(numeric(18,4),sum(aa.NG))  / convert(numeric(18,4),sum(aa.OK + aa.NG))* 100)) + '%' as RejRate
                        , sum(aa.BuyoffQty) as BuyoffQty
	                    ,'' as Utilization
         
                    from(
                            select
                            a.[day]
                            , a.[shift]
                            , case when ISNULL(b.machineID,'') = ''
		                      then 'Unknown Type:' + a.partNumberAll
		                      else b.machineID
		                      end as type
		
		
		 
                            , a.startTime
                            , convert(datetime2(0), a.lastUpdatedTime) as lastUpdatedTime
		                    , convert(numeric(10,0), MAX(a.acceptQty/a.cavityCount) )   as OK --shots
		                    , convert(numeric(10,0), sum(a.rejectQty) + ISNULL(a.Setup,0) * a.cavityCount ) as NG --PCS
		                    , convert(numeric(10,0), MAX(a.acountReading/a.cavityCount) )   as TotalQty --shots
                            , convert(numeric(10,0), SUM(a.acountReading) )   as TotalPCS --PCS

		                    --理论产量
		                    , case when isnull(a.cycleTime,0) = 0 then DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE())) / 42 else DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE())) / a.cycleTime end as TargetQty
		                    , SUM( QCNGQTY) as BuyoffQty

                            from MouldingViTracking a left join MouldingBom b on a.partNumberAll = b.partNumberAll
		 
                            where  a.acceptQty +a.rejectQty > 0 and a.acountReading >0 
		                    and  a.day =@Day 
		                    and a.shift =@Shift 

		                    group by a.day,a.shift,a.startTime,a.lastUpdatedTime, b.machineID, a.partNumberAll,a.cavityCount,a.Setup, a.acountReading,a.cycleTime

                    ) aa

                    group by aa.type

                    union -- Testing quantity

                    select

	                    case when aa.type='Button' then 1 
		                        when aa.type='Knob'   then 2
		                        when aa.type='Len'    then 3
		                        when aa.type='Panel'  then 4
   		                        when aa.type='Mould Test'  then 5
		                        when isnull(aa.type,'')='' then 10    
			                    else 11
		                        end as SN

	                    ,aa.type as ProductType

	                    ,'' as ManPower
	                    ,'' as Attendance
	                    ,'' as AttendRate                
	                    ,  convert(varchar(50),  CONVERT(numeric(18,2),  CONVERT(numeric(18,4), sum(DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE())))) / 3600.0)) as ActualHR
	                    ,convert(numeric(18,0),sum(aa.TargetQty)) as TargetQty
	                    , sum(aa.OK) as ActualQty
	                    , sum(aa.NG) as RejectQty
	                    , sum(aa.TotalQty) as TotalQty
                        , sum(aa.TotalPCS) as TotalPCS
	                    , '96:00:00' as TargetHR
	                    , CONVERT(varchar, convert(numeric(18,2), convert(numeric(18,4),sum(aa.NG))  / convert(numeric(18,4),sum(aa.OK + aa.NG))* 100)) + '%' as RejRate
                        , sum(aa.BuyoffQty) as BuyoffQty
	                    ,'' as Utilization
         
                    from(
                            select
                            a.[day]
                            , a.[shift]
                            , case when ISNULL(b.machineID,'') = ''
		                      then 'Unknown Type:' + a.partNumberAll
		                      else 'Mould Test'
		                      end as type
                            , a.startTime
                            , a.lastUpdatedTime
		                    , convert(numeric(10,0), MAX(a.acceptQty/a.cavityCount -ISNULL(a.Setup,0)) )   as OK
		                    , convert(numeric(10,0), SUM(a.rejectQty) + ISNULL(a.Setup,0) *a.cavityCount ) as NG
		                    , convert(numeric(10,0), MAX(a.acountReading/a.cavityCount) )   as TotalQty --shots
                            , convert(numeric(10,0), SUM(a.acountReading) )   as TotalPCS --PCS

		                      --理论产量
		                    , case when isnull(a.cycleTime,0) = 0 then DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE())) / 42 else DATEDIFF(Second, starttime, ISNULL(lastUpdatedTime, GETDATE())) / a.cycleTime end as TargetQty
		                    , SUM( QCNGQTY) as BuyoffQty

                            from MouldingViTracking a left join MouldingBom b on a.partNumberAll = b.partNumberAll
		 
                            where  a.acceptQty +a.rejectQty > 0 and a.acountReading >0 
		                    and  a.day =@Day 
		                    and a.shift =@Shift 
		                    and a.status in ('Material_Testing', 'Mould_Testing')

		                    group by a.day,a.shift,a.startTime,a.lastUpdatedTime, b.machineID, a.partNumberAll,a.cavityCount,a.Setup,a.acountReading,a.cycleTime
		
                    ) aa

                    group by aa.type
            ");


            SqlParameter[] parameters = {
                new SqlParameter("@Day", SqlDbType.DateTime),
                new SqlParameter("@Shift", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dDay;
            parameters[1].Value = sShift;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSWatchLog_DAL", "Function:  internal DataSet getOutput(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        internal DataTable GetSummaryReport(DateTime dfrom, DateTime dto, string sType)
        {

            StringBuilder strSql = new StringBuilder();
           

            strSql.Append(@"
select 
    a.partNumberAll,
	a.[partNumber] as Parts_No , 
	b.machineID as Type,
    isnull( b.cavityCount,0) as cavityCount,
	convert(varchar, Sum(isnull(acountReading,0))) as Total_Parts_Produce,
	sum(isnull(acceptQty,0)) as OK_QTY,
	sum(isnull(acceptQty * b.unitCount,0))  as Amount_SGD,
	CONVERT(decimal(18,0) , sum(isnull(rejectQty,0))) as Reject_Qty,
	CONVERT(decimal(18,4), sum((isnull(rejectQty,0)) *  b.unitCount))  as Reject_Cost_SGD

FROM MouldingViTracking a
left join MouldingBom b on b.partNumber = a.[partNumber]

where isnull(acceptQty,0) + isnull(rejectQty,0) > 0 and acountReading > 0 and status != 'Mould_Testing' and status != 'Material_Testing'
and a.dateTime >@From and a.dateTime < @To ");
            if (sType.Trim() != "")
            {
                strSql.Append(" and b.machineID = @Type");
            }
            strSql.Append(@" group by a.partNumber, b.machineID, a.partNumberAll, b.cavityCount order by a.partNumber ");

           
            SqlParameter[] parameters = {
                new SqlParameter("@From", SqlDbType.DateTime),
                new SqlParameter("@To", SqlDbType.DateTime),
                new SqlParameter("@Type", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dfrom;
            parameters[1].Value = dto;
            parameters[2].Value = sType;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }



        internal DataTable GetMonthlyProductionReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sCustomer, string sType)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select 
case
	when Main.week = 1 then 'Mon'
	when Main.week = 2 then 'Tue'
	when Main.week = 3 then 'Wed'
	when Main.week = 4 then 'Thu'
	when Main.week = 5 then 'Fri'
	when Main.week = 6 then 'Sat'
	when Main.week = 7 then 'Sun'
end as [WeekDay]
,Main.day as [Date]
,convert(varchar(50), ISNULL(Daily.totalQty,0)) as [DAILY Total Qty]
,Main.shift
,convert(varchar(50), ISNULL(Main.ng,0))  as [Rej Qty]
,Main.shiftRejRate as [Daily Rej%]
,Daily.totalDailyRejRate as [Total Daily Rej%]
 
,'' as Defect --execl 中defect一行空格的效果.

,convert(varchar(50),ISNULL(c1.NG,0)) as  [White Dot]	
,convert(varchar(50),ISNULL(c2.NG,0)) as  [Scratches]	
,convert(varchar(50),ISNULL(c3.NG,0)) as  [Dented Mark]	
,convert(varchar(50),ISNULL(c4.NG,0)) as  [Shinning Dot]	
,convert(varchar(50),ISNULL(c5.NG,0)) as  [Black Mark]	
,convert(varchar(50),ISNULL(c6.NG,0)) as  [Sink Mark]	
,convert(varchar(50),ISNULL(c7.NG,0)) as  [Flow Mark]	
,convert(varchar(50),ISNULL(c8.NG,0)) as  [High Gate]	
,convert(varchar(50),ISNULL(c9.NG,0)) as  [Silver Steak]	
,convert(varchar(50),ISNULL(c10.NG,0)) as [Black Dot]	
,convert(varchar(50),ISNULL(c11.NG,0)) as [Oil Stain]	
,convert(varchar(50),ISNULL(c12.NG,0)) as [Flow Line]	
,convert(varchar(50),ISNULL(c13.NG,0)) as [Over-Cut]		
,convert(varchar(50),ISNULL(c14.NG,0)) as [Crack]		
,convert(varchar(50),ISNULL(c15.NG,0)) as [Short Mold]	
,convert(varchar(50),ISNULL(c16.NG,0)) as [Stain Mark]	
,convert(varchar(50),ISNULL(c17.NG,0)) as [Weld Line]	
,convert(varchar(50),ISNULL(c18.NG,0)) as [Flashes]      
,convert(varchar(50),ISNULL(c19.NG,0)) as [Foreign Materials]	
,convert(varchar(50),ISNULL(c20.NG,0)) as [Drag]				
,convert(varchar(50),ISNULL(c21.NG,0)) as [Material Bleed]		
,convert(varchar(50),ISNULL(c22.NG,0)) as [Bent]				
,convert(varchar(50),ISNULL(c23.NG,0)) as [Deform]				
,convert(varchar(50),ISNULL(c24.NG,0)) as [Gas Mark]
,convert(varchar(50),ISNULL(Main.IPQCRej,0)) as [IPQC Buy Off]
,convert(varchar(50),ISNULL(Main.Setup,0)) as [Adjustment (Scrap)]

,'' as [Cumu. Qty]
,'' as [Cumu. Rej]
,'' as [Ave Rej%]
");

            #region  Main
            strSql.Append(@"
from 
(
	select 
		Datepart(weekday, day) - 1 as [week] ,
		day(day) as [day] ,
		shift ,
		sum(rejectQty ) as ng, 
		convert(varchar(50), convert(decimal(18,2),ROUND(sum(rejectQty )/sum(acountReading)*100 ,2))) + '%' as shiftRejRate,
		convert(int, sum(isnull(QCNGQTY,0))) as IPQCRej,
		convert(int, sum(isnull(Setup,0))) as Setup
	from MouldingViTracking a
	left join MouldingBom b on a.partNumber = b.partNumber
	where isnull(acountReading,0) >0 and isnull(rejectQty,0) + ISNULL(acceptQty,0) > 0  and status != 'Mould_Testing' and status != 'Material_Testing'
    and a.datetime >= @DateFrom and a.datetime < @DateTo ");
            
            if (sCustomer != "")
                strSql.Append(@" and b.customer = @customer ");
            
            if (sType != "")
                strSql.Append(@" and b.machineID = @type ");
            
            if (sPartNo != "")
                strSql.Append(@" and b.partNumber = @PartNo ");

            strSql.Append(@"group by day , shift ) Main");
            #endregion

            #region Daily
            strSql.Append(@"
left join 
(
	select 
		day(day) as [day], 
		sum(acountReading) as totalQty , 
		convert(varchar(50), convert(decimal(18,2),ROUND(sum(rejectQty)/sum(acountReading)*100 ,2))) + '%' as totalDailyRejRate
	from MouldingViTracking a
	left join MouldingBom b on a.partNumber = b.partNumber
	where isnull(acountReading,0) >0 and isnull(rejectQty,0) + ISNULL(acceptQty,0) > 0 and status != 'Mould_Testing'
    and  a.datetime >= @DateFrom and a.datetime < @DateTo ");

            if (sCustomer != "")
                strSql.Append(@" and b.customer = @customer ");

            if (sType != "")
                strSql.Append(@" and b.machineID = @type ");

            if (sPartNo != "")
                strSql.Append(@" and b.partNumber = @PartNo ");

            strSql.Append("	group by day ) Daily on Main.day = Daily.day ");
            #endregion

            #region  24 Moulding defect code
            //c1
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'White Dot'");
            if (sCustomer  != "")  strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo  != "")  strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c1 on Main.day=c1.day and Main.shift=c1.shift ");

            //c2
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Scratches'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c2 on Main.day=c2.day and Main.shift=c2.shift ");

            //c3
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Dented Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c3 on Main.day=c3.day and Main.shift=c3.shift ");

            //c4
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Shinning Dot'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c4 on Main.day=c4.day and Main.shift=c4.shift ");

            //c5
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Black Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c5 on Main.day=c5.day and Main.shift=c5.shift ");

            //c6
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Sink Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c6 on Main.day=c6.day and Main.shift=c6.shift ");

            //c7
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Flow Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c7 on Main.day=c7.day and Main.shift=c7.shift ");

            //c8
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'High Gate'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c8 on Main.day=c8.day and Main.shift=c8.shift ");

            //c9
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Silver Steak'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c9 on Main.day=c9.day and Main.shift=c9.shift ");

            //c10
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Black Dot'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c10 on Main.day=c10.day and Main.shift=c10.shift ");

            //c11
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Oil Stain'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c11 on Main.day=c11.day and Main.shift=c11.shift ");

            //c12
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Flow Line'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c12 on Main.day=c12.day and Main.shift=c12.shift ");

            //c13
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Over-Cut'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c13 on Main.day=c13.day and Main.shift=c13.shift ");

            //c14
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Crack'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c14 on Main.day=c14.day and Main.shift=c14.shift ");

            //c15
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Short Mold'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c15 on Main.day=c15.day and Main.shift=c15.shift ");

            //c16
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Stain Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c16 on Main.day=c16.day and Main.shift=c16.shift ");

            //c17
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Weld Line'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c17 on Main.day=c17.day and Main.shift=c17.shift ");

            //c18
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Flashes'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c18 on Main.day=c18.day and Main.shift=c18.shift ");

            //c19
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Foreign Materials'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c19 on Main.day=c19.day and Main.shift=c19.shift ");

            //c20
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Drag'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c20 on Main.day=c20.day and Main.shift=c20.shift ");

            //c21
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Material Bleed'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c21 on Main.day=c21.day and Main.shift=c21.shift ");

            //c22
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Bent'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c22 on Main.day=c22.day and Main.shift=c22.shift ");

            //c23
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Deform'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c23 on Main.day=c23.day and Main.shift=c23.shift ");

            //c24
            strSql.AppendLine(@"left join (select day(day) as [day],shift,sum(rejectQty) as NG	from MouldingViDefectTracking a left join MouldingBom b on a.partNumber = b.partNumber where a.datetime >= @DateFrom and a.datetime < @DateTo and defectCode = 'Gas Mark'");
            if (sCustomer != "") strSql.Append(@" and b.customer = @customer ");
            if (sType != "") strSql.Append(@" and b.machineID = @type ");
            if (sPartNo != "") strSql.Append(@" and b.partNumber = @PartNo ");
            strSql.Append(@" group by day , shift) c24 on Main.day=c24.day and Main.shift=c24.shift ");

            #endregion


            strSql.Append(" order by Main.day asc ,Main.shift asc	");





            SqlParameter[] parameters = {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@PartNo", SqlDbType.VarChar,50 ),
                new SqlParameter("@type", SqlDbType.VarChar,50 ),
                new SqlParameter("@customer", SqlDbType.VarChar,50 )
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (sPartNo != "") parameters[2].Value = sPartNo; else parameters[2] = null;
            if (sType != "") parameters[3].Value = sType; else parameters[3] = null;
            if (sCustomer  != "") parameters[4].Value = sCustomer; else parameters[4] = null;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }




        public DataTable GetOuput(DateTime DateFrom, DateTime DateTo, string Shift, string DateNotIn, bool ExceptWeekends)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
sum(a.TotalOuput) as TotalOutput
,sum(a.TotalShots) as TotalShots
,sum(a.TotalRej) as TotalRej
from 
(
	select 
	1 as id
	,isnull( acountReading,0) as TotalOuput
	,isnull(acountReading,0)/isnull(cavityCount,1)  as TotalShots
	,isnull(rejectQty,0) as TotalRej
	from MouldingViTracking 
	where datetime >= @dateFrom and datetime < @dateTo");
            if (Shift != "")
            {
                strSql.Append(" and shift = @shift ");
            }

            if (DateNotIn != "")
            {
                strSql.Append(" and day(day) not in (");

                string[] strArrDate = DateNotIn.Split(',');
                foreach (string  date in strArrDate)
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
                paras[2].Value = Shift;
            }else
            {
                paras[2] = null;
            }


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }








        #region Moulding Monthly Top Reject Report
        public DataTable GetTopRejParts(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select

a.partnumber as partNo
,SUM(ISNULL(a.acountReading,0)) as output
,SUM(ISNULL(a.rejectQty,0)) as rejQty
,case when SUM(ISNULL(a.acountReading,0)) = 0 
	then 0
	else convert(decimal(18,2), round(SUM( ISNULL(a.rejectQty,0))/SUM(ISNULL( a.acountReading,0))*100,2))
end  as rejRate

from MouldingViTracking a

where 1=1 
and day >= @datefrom
and day < @dateto ");

            if (sMachineID != "")
                strSql.Append(" and machineID = @machineID ");
            
            strSql.Append(@" 
group by partNumber 

order by 
case when SUM(ISNULL(a.acountReading,0)) = 0 
then 0 
else SUM( ISNULL(a.rejectQty,0))/SUM(ISNULL( a.acountReading,0)) 
end  desc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;



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

        public DataTable GetTopRejParts_DefectQty(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
with defectDetail as (
	select
	partNumber as [partNo]
	,SUM(rejectQty) as [totalRej]
	,SUM(case when defectCode = 'White Dot' then rejectQty end ) as [White Dot]
	,SUM(case when defectCode = 'Scratches' then rejectQty end ) as [Scratches]
	,SUM(case when defectCode = 'Dented Mark' then rejectQty end ) as [Dented Mark]
	,SUM(case when defectCode = 'Shinning Dot' then rejectQty end ) as [Shinning Dot]
	,SUM(case when defectCode = 'Black Mark' then rejectQty end ) as [Black Mark]
	,SUM(case when defectCode = 'Sink Mark' then rejectQty end ) as [Sink Mark]
	,SUM(case when defectCode = 'Flow Mark' then rejectQty end ) as [Flow Mark]
	,SUM(case when defectCode = 'High Gate' then rejectQty end ) as [High Gate]
	,SUM(case when defectCode = 'Silver Steak' then rejectQty end ) as [Silver Steak]
	,SUM(case when defectCode = 'Black Dot' then rejectQty end ) as [Black Dot]
	,SUM(case when defectCode = 'Oil Stain' then rejectQty end ) as [Oil Stain]
	,SUM(case when defectCode = 'Flow Line' then rejectQty end ) as [Flow Line]
	,SUM(case when defectCode = 'Over-Cut' then rejectQty end ) as [Over-Cut]
	,SUM(case when defectCode = 'Crack' then rejectQty end ) as [Crack]
	,SUM(case when defectCode = 'Short Mold' then rejectQty end ) as [Short Mold]
	,SUM(case when defectCode = 'Stain Mark' then rejectQty end ) as [Stain Mark]
	,SUM(case when defectCode = 'Weld Line' then rejectQty end ) as [Weld Line]
	,SUM(case when defectCode = 'Flashes' then rejectQty end ) as [Flashes]
	,SUM(case when defectCode = 'Foreign Materials' then rejectQty end ) as [Foreign Materials]
	,SUM(case when defectCode = 'Drag' then rejectQty end ) as [Drag]
	,SUM(case when defectCode = 'Material Bleed' then rejectQty end ) as [Material Bleed]
	,SUM(case when defectCode = 'Bent' then rejectQty end ) as [Bent]
	,SUM(case when defectCode = 'Deform' then rejectQty end ) as [Deform]
	,SUM(case when defectCode = 'Gas Mark' then rejectQty end ) as [Gas Mark]
	from MouldingViDefectTracking
	where 1=1 
	and day >= @datefrom
	and day < @dateto ");
            

            if (sMachineID != "")
                strSql.Append(" and machineID = @machineID ");



            strSql.Append(@"
	group by partNumber 
)
select partNo, defectCode, rejQty, case when totalRej=0 then 0 else rejQty/totalRej*100 end as rejRate from (
	select partNo, totalRej, 'White Dot' as defectCode, [White Dot] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Scratches' as defectCode, [Scratches] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Dented Mark' as defectCode, [Dented Mark] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Shinning Dot' as defectCode, [Shinning Dot] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Black Mark' as defectCode, [Black Mark] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Sink Mark' as defectCode, [Sink Mark] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Flow Mark' as defectCode, [Flow Mark] as rejQty from defectDetail union all 
	select partNo, totalRej, 'High Gate' as defectCode, [High Gate] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Silver Steak' as defectCode, [Silver Steak] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Black Dot' as defectCode, [Black Dot] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Oil Stain' as defectCode, [Oil Stain] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Flow Line' as defectCode, [Flow Line] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Over-Cut' as defectCode, [Over-Cut] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Crack' as defectCode, [Crack] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Short Mold' as defectCode, [Short Mold] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Stain Mark' as defectCode, [Stain Mark] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Weld Line' as defectCode, [Weld Line] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Flashes' as defectCode, [Flashes] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Foreign Materials' as defectCode, [Foreign Materials] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Drag' as defectCode, [Drag] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Material Bleed' as defectCode, [Material Bleed] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Bent' as defectCode, [Bent] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Deform' as defectCode, [Deform] as rejQty from defectDetail union all 
	select partNo, totalRej, 'Gas Mark' as defectCode, [Gas Mark] as rejQty from defectDetail
) a order by a.partNo asc , a.rejQty  desc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;



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
        


        public DataTable GetTopRejDefects(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            string searchMachine = sMachineID == "" ? "" : " and machineID = @MachineID";
           

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
select 
ISNULL(SUM( ISNULL([White Dot],0)),0) as [White Dot]
,ISNULL(SUM( ISNULL([Scratches],0)),0) as [Scratches]
,ISNULL(SUM( ISNULL([Dented Mark],0)),0) as [Dented Mark]
,ISNULL(SUM( ISNULL([Shinning Dot],0)),0) as [Shinning Dot]
,ISNULL(SUM( ISNULL([Black Mark],0)),0) as [Black Mark]
,ISNULL(SUM( ISNULL([Sink Mark],0)),0) as [Sink Mark]
,ISNULL(SUM( ISNULL([Flow Mark],0)),0) as [Flow Mark]
,ISNULL(SUM( ISNULL([High Gate],0)),0) as [High Gate]
,ISNULL(SUM( ISNULL([Silver Steak],0)),0) as [Silver Steak]
,ISNULL(SUM( ISNULL([Black Dot],0)),0) as [Black Dot]
,ISNULL(SUM( ISNULL([Oil Stain],0)),0) as [Oil Stain]
,ISNULL(SUM( ISNULL([Flow Line],0)),0) as [Flow Line]
,ISNULL(SUM( ISNULL([Over-Cut],0)),0) as [Over-Cut]
,ISNULL(SUM( ISNULL([Crack],0)),0) as [Crack]
,ISNULL(SUM( ISNULL([Short Mold],0)),0) as [Short Mold]
,ISNULL(SUM( ISNULL([Stain Mark],0)),0) as [Stain Mark]
,ISNULL(SUM( ISNULL([Weld Line],0)),0) as [Weld Line]
,ISNULL(SUM( ISNULL([Flashes],0)),0) as [Flashes]
,ISNULL(SUM( ISNULL([Foreign Materials],0)),0) as [Foreign Materials]
,ISNULL(SUM( ISNULL([Drag],0)),0) as [Drag]
,ISNULL(SUM( ISNULL([Material Bleed],0)),0) as [Material Bleed]
,ISNULL(SUM( ISNULL([Bent],0)),0) as [Bent]
,ISNULL(SUM( ISNULL([Deform],0)),0) as [Deform]
,ISNULL(SUM( ISNULL([Gas Mark],0)),0) as [Gas Mark]

from 
(
	select 

	'1' as groupFlag

	,SUM(case when defectCode = 'White Dot' then rejectQty end )			as [White Dot]
	,SUM(case when defectCode = 'Scratches' then rejectQty end )			as [Scratches]
	,SUM(case when defectCode = 'Dented Mark' then rejectQty end )			as [Dented Mark]
	,SUM(case when defectCode = 'Shinning Dot' then rejectQty end )			as [Shinning Dot]
	,SUM(case when defectCode = 'Black Mark' then rejectQty end )			as [Black Mark]
	,SUM(case when defectCode = 'Sink Mark' then rejectQty end )			as [Sink Mark]
	,SUM(case when defectCode = 'Flow Mark' then rejectQty end )			as [Flow Mark]
	,SUM(case when defectCode = 'High Gate' then rejectQty end )			as [High Gate]
	,SUM(case when defectCode = 'Silver Steak' then rejectQty end )			as [Silver Steak]
	,SUM(case when defectCode = 'Black Dot' then rejectQty end )			as [Black Dot]
	,SUM(case when defectCode = 'Oil Stain' then rejectQty end )			as [Oil Stain]
	,SUM(case when defectCode = 'Flow Line' then rejectQty end )			as [Flow Line]
	,SUM(case when defectCode = 'Over-Cut' then rejectQty end )				as [Over-Cut]
	,SUM(case when defectCode = 'Crack' then rejectQty end )				as [Crack]
	,SUM(case when defectCode = 'Short Mold' then rejectQty end )			as [Short Mold]
	,SUM(case when defectCode = 'Stain Mark' then rejectQty end )			as [Stain Mark]
	,SUM(case when defectCode = 'Weld Line' then rejectQty end )			as [Weld Line]
	,SUM(case when defectCode = 'Flashes' then rejectQty end )				as [Flashes]
	,SUM(case when defectCode = 'Foreign Materials' then rejectQty end )	as [Foreign Materials]
	,SUM(case when defectCode = 'Drag' then rejectQty end )					as [Drag]
	,SUM(case when defectCode = 'Material Bleed' then rejectQty end )		as [Material Bleed]
	,SUM(case when defectCode = 'Bent' then rejectQty end )					as [Bent]
	,SUM(case when defectCode = 'Deform' then rejectQty end )				as [Deform]
	,SUM(case when defectCode = 'Gas Mark' then rejectQty end )				as [Gas Mark]

	from mouldingvidefecttracking

	where 1=1 
	and day >= @datefrom
	and day < @dateto
    {0}

	group by defectCode
) a  group by groupFlag", searchMachine);

         


            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;



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

        public DataTable GetTopRejDefects_PartQty(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            string searchMachine = sMachineID == "" ? "" : " and machineID = @MachineID";
            
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
select 
partNumber as partNo
,defectCode
,sum(rejectQty) as rejqty

from MouldingViDefectTracking 
where 1=1 
and day >= @datefrom
and day < @dateto
{0}
group by partNumber, defectCode ", searchMachine);

            SqlParameter[] paras =
            {
                new SqlParameter("@datefrom",SqlDbType.DateTime),
                new SqlParameter("@dateto",SqlDbType.DateTime),
                new SqlParameter("@machineID",SqlDbType.VarChar,16)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;



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
        #endregion



        //new daily report 
        public DataTable GetViForDailyReport_NEW(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
day
,shift
,machineid
,jigNo
,partNumber
,SUM(ISNULL(acountReading,0)) as output 
,SUM(ISNULL(acceptQty,0)) as passQty
,SUM(ISNULL(rejectQty,0)) as rejQty

,case when SUM(ISNULL(acountReading,0)) = 0 
then 0 
else convert(decimal(18,2), round(SUM(ISNULL(rejectQty,0)) / SUM(acountReading), 2)) 
end as  rejRate
,userID = stuff( (SELECT ',' + t.userID FROM (select userID from mouldingvitracking where day =a.day and shift = a.shift and machineID=a.machineID and partNumber = a.partNumber ) t  FOR xml path('')) , 1 , 1 , '')

from mouldingvitracking a 
where status != 'Mould_Testing' and status != 'Material_Testing' and  day >= @dateFrom and day < @dateTo ");

            if (sPartNo != "")
            {
                strSql.Append(" and partNumber = @partNo");
            }
            if (sJigNo !="")
            {
                strSql.Append(" and jigNo = @jigNo ");
            }
            if (sShift != "")
            {
                strSql.Append(" and shift = @shift ");
            }

            strSql.Append(" group by day, shift,machineID, partNumber, jigNo ");


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



        public DataTable GetList(string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from mouldingvitracking where trackingID = @trackingID ");



            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };


            paras[0].Value = sTrackingID;


            DataSet ds =  DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
              return   ds.Tables[0];
            }
        }


        public SqlCommand MaintenanceCMD(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" update MouldingViTracking  set ");



            strSql.Append(" model = @model ");
            strSql.Append(" ,partNumber = @partNumber ");
            strSql.Append(" ,jigNo = @jigNo ");

            strSql.Append(" ,AcountReading = @AcountReading ");
            strSql.Append(" ,AcceptQty = @AcceptQty ");
            strSql.Append(" ,RejectQty = @RejectQty ");

            strSql.Append(" ,Setup = @Setup ");
            strSql.Append(" ,WastageMaterial01 = @WastageMaterial01 ");
            strSql.Append(" ,WastageMaterial02 = @WastageMaterial02 ");

            strSql.Append(" ,lastUpdatedTime = @lastUpdatedTime ");


            strSql.Append(" where trackingID = @trackingID  ");


            SqlParameter[] paras =
            {
                new SqlParameter("@model",SqlDbType.VarChar),
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@jigNo",SqlDbType.VarChar),
                new SqlParameter("@AcountReading",SqlDbType.Decimal),
                new SqlParameter("@AcceptQty",SqlDbType.Decimal),
                new SqlParameter("@RejectQty",SqlDbType.Decimal),
                new SqlParameter("@Setup",SqlDbType.Decimal),
                new SqlParameter("@WastageMaterial01",SqlDbType.Decimal),
                new SqlParameter("@WastageMaterial02",SqlDbType.Decimal),
                new SqlParameter("@lastUpdatedTime",SqlDbType.DateTime),
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };


            paras[0].Value = model.Model == null ? (object)DBNull.Value : model.Model;
            paras[1].Value = model.PartNumber == null ? (object)DBNull.Value : model.PartNumber;
            paras[2].Value = model.JigNo == null ? (object)DBNull.Value : model.JigNo;
            paras[3].Value = model.AcountReading == null ? (object)DBNull.Value : model.AcountReading;
            paras[4].Value = model.AcceptQty == null ? (object)DBNull.Value : model.AcceptQty;
            paras[5].Value = model.RejectQty == null ? (object)DBNull.Value : model.RejectQty;
            paras[6].Value = model.Setup == null ? (object)DBNull.Value : model.Setup;
            paras[7].Value = model.WastageMaterial01 == null ? (object)DBNull.Value : model.WastageMaterial01;
            paras[8].Value = model.WastageMaterial02 == null ? (object)DBNull.Value : model.WastageMaterial02;

            paras[9].Value = model.LastUpdatedTime == null ? (object)DBNull.Value : model.LastUpdatedTime;
            paras[10].Value = model.TrackingID == null ? (object)DBNull.Value : model.TrackingID;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public SqlCommand AddHistoryCMD(Common.Class.Model.MouldingViHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingViHistory(");
            strSql.Append("id,machineID,dateTime,partNumber,jigNo,model,cavityCount,partsWeight,parts2Weight,lastQty,cycleTime,targetQty,userName,userID,acountReading,rejectQty,QCNGQTY,acceptQty,startTime,stopTime,day,shift,status,matPart01,matPart02,matLot01,matLot02,opSign01,opSign02,opSign03,opSign04,opSign05,opSign06,opSign07,opSign08,opSign09,opSign10,opSign11,opSign12,qaSign01,qaSign02,qaSign03,qaSign04,qaSign05,qaSign06,qaSign07,qaSign08,qaSign09,qaSign10,qaSign11,qaSign12,customer,lastUpdatedTime,trackingID,remarks,Material_MHCheck,Material_MHCheckTime,Material_Opcheck,Material_OpCheckTime,Material_LeaderCheck,Material_LeaderCheckTime,LeaderCheck,LeaderCheckTime,SupervisorCheck,SupervisorCheckTime,partNumberAll,Setup,WastageMaterial01,WastageMaterial02,OkAccumulation,refField01,refField02,refField03,refField04,refField05)");
            strSql.Append(" values (");
            strSql.Append("@id,@machineID,@dateTime,@partNumber,@jigNo,@model,@cavityCount,@partsWeight,@parts2Weight,@lastQty,@cycleTime,@targetQty,@userName,@userID,@acountReading,@rejectQty,@QCNGQTY,@acceptQty,@startTime,@stopTime,@day,@shift,@status,@matPart01,@matPart02,@matLot01,@matLot02,@opSign01,@opSign02,@opSign03,@opSign04,@opSign05,@opSign06,@opSign07,@opSign08,@opSign09,@opSign10,@opSign11,@opSign12,@qaSign01,@qaSign02,@qaSign03,@qaSign04,@qaSign05,@qaSign06,@qaSign07,@qaSign08,@qaSign09,@qaSign10,@qaSign11,@qaSign12,@customer,@lastUpdatedTime,@trackingID,@remarks,@Material_MHCheck,@Material_MHCheckTime,@Material_Opcheck,@Material_OpCheckTime,@Material_LeaderCheck,@Material_LeaderCheckTime,@LeaderCheck,@LeaderCheckTime,@SupervisorCheck,@SupervisorCheckTime,@partNumberAll,@Setup,@WastageMaterial01,@WastageMaterial02,@OkAccumulation,@refField01,@refField02,@refField03,@refField04,@refField05)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@partsWeight", SqlDbType.Decimal,9),
                    new SqlParameter("@parts2Weight", SqlDbType.Decimal,9),
                    new SqlParameter("@lastQty", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@targetQty", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@acountReading", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@QCNGQTY", SqlDbType.Decimal,9),
                    new SqlParameter("@acceptQty", SqlDbType.Decimal,9),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@matPart01", SqlDbType.VarChar,50),
                    new SqlParameter("@matPart02", SqlDbType.VarChar,50),
                    new SqlParameter("@matLot01", SqlDbType.VarChar,50),
                    new SqlParameter("@matLot02", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign01", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign02", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign03", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign04", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign05", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign06", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign07", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign08", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign09", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign10", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign11", SqlDbType.VarChar,50),
                    new SqlParameter("@opSign12", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign01", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign02", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign03", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign04", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign05", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign06", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign07", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign08", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign09", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign10", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign11", SqlDbType.VarChar,50),
                    new SqlParameter("@qaSign12", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@Material_MHCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_MHCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@Material_Opcheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_OpCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@Material_LeaderCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@Material_LeaderCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@LeaderCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@LeaderCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@SupervisorCheck", SqlDbType.VarChar,50),
                    new SqlParameter("@SupervisorCheckTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@partNumberAll", SqlDbType.VarChar,500),
                    new SqlParameter("@Setup", SqlDbType.Decimal,9),
                    new SqlParameter("@WastageMaterial01", SqlDbType.Decimal,9),
                    new SqlParameter("@WastageMaterial02", SqlDbType.Decimal,9),
                    new SqlParameter("@OkAccumulation", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.MachineID;
            parameters[2].Value = model.Datetime;
            parameters[3].Value = model.PartNumber;
            parameters[4].Value = model.JigNo;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.CavityCount;
            parameters[7].Value = model.PartsWeight;
            parameters[8].Value = model.Parts2Weight;
            parameters[9].Value = model.lastQty;
            parameters[10].Value = model.CycleTime;
            parameters[11].Value = model.TargetQty;
            parameters[12].Value = model.UserName;
            parameters[13].Value = model.UserID;
            parameters[14].Value = model.AcountReading;
            parameters[15].Value = model.RejectQty;
            parameters[16].Value = model.QCNGQTY;
            parameters[17].Value = model.AcceptQty;
            parameters[18].Value = model.StartTime == DateTime.MinValue ? (object)DBNull.Value : model.StopTime;
            parameters[19].Value = model.StopTime == DateTime.MinValue ? (object)DBNull.Value : model.StopTime;
            parameters[20].Value = model.Day; 
            parameters[21].Value = model.Shift;
            parameters[22].Value = model.Status;
            parameters[23].Value = model.MatPart01;
            parameters[24].Value = model.MatPart02;
            parameters[25].Value = model.MatLot01;
            parameters[26].Value = model.MatLot02;
            parameters[27].Value = model.OpSign01;
            parameters[28].Value = model.OpSign02;
            parameters[29].Value = model.OpSign03;
            parameters[30].Value = model.OpSign04;
            parameters[31].Value = model.OpSign05;
            parameters[32].Value = model.OpSign06;
            parameters[33].Value = model.OpSign07;
            parameters[34].Value = model.OpSign08;
            parameters[35].Value = model.OpSign09;
            parameters[36].Value = model.OpSign10;
            parameters[37].Value = model.OpSign11;
            parameters[38].Value = model.OpSign12;
            parameters[39].Value = model.QaSign01;
            parameters[40].Value = model.QaSign02;
            parameters[41].Value = model.QaSign03;
            parameters[42].Value = model.QaSign04;
            parameters[43].Value = model.QaSign05;
            parameters[44].Value = model.QaSign06;
            parameters[45].Value = model.QaSign07;
            parameters[46].Value = model.QaSign08;
            parameters[47].Value = model.QaSign09;
            parameters[48].Value = model.QaSign10;
            parameters[49].Value = model.QaSign11;
            parameters[50].Value = model.QaSign12;
            parameters[51].Value = model.Customer;
            parameters[52].Value = model.LastUpdatedTime;
            parameters[53].Value = model.TrackingID;
            parameters[54].Value = model.Remarks;
            parameters[55].Value = model.Material_MHCheck;
            parameters[56].Value = model.Material_MHCheckTime == DateTime.MinValue ? (object)DBNull.Value : model.Material_MHCheckTime;
            parameters[57].Value = model.Material_Opcheck;
            parameters[58].Value = model.Material_OpCheckTime == DateTime.MinValue ? (object)DBNull.Value : model.Material_OpCheckTime;
            parameters[59].Value = model.Material_LeaderCheck;
            parameters[60].Value = model.Material_LeaderCheckTime == DateTime.MinValue ? (object)DBNull.Value : model.Material_LeaderCheckTime;
            parameters[61].Value = model.LeaderCheck;
            parameters[62].Value = model.LeaderCheckTime == DateTime.MinValue ? (object)DBNull.Value : model.LeaderCheckTime;
            parameters[63].Value = model.SupervisorCheck;
            parameters[64].Value = model.SupervisorCheckTime == DateTime.MinValue ? (object)DBNull.Value : model.SupervisorCheckTime;
            parameters[65].Value = model.PartNumberAll;
            parameters[66].Value = model.Setup;
            parameters[67].Value = model.WastageMaterial01;
            parameters[68].Value = model.WastageMaterial02;
            parameters[69].Value = model.OkAccumulation;
            parameters[70].Value = model.refField01;
            parameters[71].Value = model.refField02;
            parameters[72].Value = model.refField03;
            parameters[73].Value = model.refField04;
            parameters[74].Value = model.refField05;

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
      
        }


        public int Delete(string sTrackingID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Delete MouldingViTracking  where  trackingID = @trackingID");
           


            SqlParameter[] paras =
            {
                new SqlParameter("@trackingID",SqlDbType.VarChar)
            };


            paras[0].Value = sTrackingID;



            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }




    }
}
