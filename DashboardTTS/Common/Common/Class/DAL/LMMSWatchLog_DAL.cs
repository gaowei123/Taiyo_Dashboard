 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
    /// <summary>
    /// 数据访问类:LMMSWatchLog_DAL
    /// </summary>
    public class LMMSWatchLog_DAL
    {
        public LMMSWatchLog_DAL()
        { }
      

        public SqlCommand DeleteJob(string sJobNo)
        {
            if (string.IsNullOrEmpty(sJobNo))
                throw new ArgumentNullException("Job No can not be empty!");


            string str = "delete from lmmswatchlog where jobNumber = @jobNo";
            SqlParameter[] parameters = {
                new SqlParameter("@jobNo",SqlDbType.VarChar)
            };
            parameters[0].Value = sJobNo;

            return  DBHelp.SqlDB.generateCommand(str, parameters);
        }

   
        

        public SqlCommand UpdateJobMaintenanceCMD(Common.Model.LMMSWatchLog_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Update LMMSWatchLog SET ");

            strSql.Append("	totalPass	=	@totalPass	");
            strSql.Append("	,totalFail	=	@totalFail	");

            strSql.Append("	,ok1Count	=	@ok1Count	");
            strSql.Append("	,ok2Count	=	@ok2Count	");
            strSql.Append("	,ok3Count	=	@ok3Count	");
            strSql.Append("	,ok4Count	=	@ok4Count	");
            strSql.Append("	,ok5Count	=	@ok5Count	");
            strSql.Append("	,ok6Count	=	@ok6Count	");
            strSql.Append("	,ok7Count	=	@ok7Count	");
            strSql.Append("	,ok8Count	=	@ok8Count	");
            strSql.Append("	,ok9Count	=	@ok9Count	");
            strSql.Append("	,ok10Count	=	@ok10Count	");
            strSql.Append("	,ok11Count	=	@ok11Count	");
            strSql.Append("	,ok12Count	=	@ok12Count	");
            strSql.Append("	,ok13Count	=	@ok13Count	");
            strSql.Append("	,ok14Count	=	@ok14Count	");
            strSql.Append("	,ok15Count	=	@ok15Count	");
            strSql.Append("	,ok16Count	=	@ok16Count	");

            strSql.Append("	,ng1Count	=	@ng1Count	");
            strSql.Append("	,ng2Count	=	@ng2Count	");
            strSql.Append("	,ng3Count	=	@ng3Count	");
            strSql.Append("	,ng4Count	=	@ng4Count	");
            strSql.Append("	,ng5Count	=	@ng5Count	");
            strSql.Append("	,ng6Count	=	@ng6Count	");
            strSql.Append("	,ng7Count	=	@ng7Count	");
            strSql.Append("	,ng8Count	=	@ng8Count	");
            strSql.Append("	,ng9Count	=	@ng9Count	");
            strSql.Append("	,ng10Count	=	@ng10Count	");
            strSql.Append("	,ng11Count	=	@ng11Count	");
            strSql.Append("	,ng12Count	=	@ng12Count	");
            strSql.Append("	,ng13Count	=	@ng13Count	");
            strSql.Append("	,ng14Count	=	@ng14Count	");
            strSql.Append("	,ng15Count	=	@ng15Count	");
            strSql.Append("	,ng16Count	=	@ng16Count	");


            strSql.Append(" Where jobnumber = @jobnumber ");
          

            SqlParameter[] parameters = {
                new SqlParameter("@jobnumber", SqlDbType.VarChar,50),
                new SqlParameter("@totalPass", SqlDbType.Int),
                new SqlParameter("@totalFail", SqlDbType.Int),

                new SqlParameter("@ok1Count", SqlDbType.Int),
                new SqlParameter("@ok2Count", SqlDbType.Int),
                new SqlParameter("@ok3Count", SqlDbType.Int),
                new SqlParameter("@ok4Count", SqlDbType.Int),
                new SqlParameter("@ok5Count", SqlDbType.Int),
                new SqlParameter("@ok6Count", SqlDbType.Int),
                new SqlParameter("@ok7Count", SqlDbType.Int),
                new SqlParameter("@ok8Count", SqlDbType.Int),
                new SqlParameter("@ok9Count", SqlDbType.Int),
                new SqlParameter("@ok10Count", SqlDbType.Int),
                new SqlParameter("@ok11Count", SqlDbType.Int),
                new SqlParameter("@ok12Count", SqlDbType.Int),
                new SqlParameter("@ok13Count", SqlDbType.Int),
                new SqlParameter("@ok14Count", SqlDbType.Int),
                new SqlParameter("@ok15Count", SqlDbType.Int),
                new SqlParameter("@ok16Count", SqlDbType.Int),

                new SqlParameter("@ng1Count", SqlDbType.Int),
                new SqlParameter("@ng2Count", SqlDbType.Int),
                new SqlParameter("@ng3Count", SqlDbType.Int),
                new SqlParameter("@ng4Count", SqlDbType.Int),
                new SqlParameter("@ng5Count", SqlDbType.Int),
                new SqlParameter("@ng6Count", SqlDbType.Int),
                new SqlParameter("@ng7Count", SqlDbType.Int),
                new SqlParameter("@ng8Count", SqlDbType.Int),
                new SqlParameter("@ng9Count", SqlDbType.Int),
                new SqlParameter("@ng10Count", SqlDbType.Int),
                new SqlParameter("@ng11Count", SqlDbType.Int),
                new SqlParameter("@ng12Count", SqlDbType.Int),
                new SqlParameter("@ng13Count", SqlDbType.Int),
                new SqlParameter("@ng14Count", SqlDbType.Int),
                new SqlParameter("@ng15Count", SqlDbType.Int),
                new SqlParameter("@ng16Count", SqlDbType.Int)
            };
            


            parameters[0].Value = model.jobNumber;
            parameters[1].Value = model.totalPass;
            parameters[2].Value = model.totalFail;

            parameters[3].Value = model.ok1Count == null ? 0 : model.ok1Count;
            parameters[4].Value = model.ok2Count == null ? 0 : model.ok2Count;
            parameters[5].Value = model.ok3Count == null ? 0 : model.ok3Count  ;
            parameters[6].Value = model.ok4Count == null ? 0 : model.ok4Count  ;
            parameters[7].Value = model.ok5Count == null ? 0 : model.ok5Count  ;
            parameters[8].Value = model.ok6Count == null ? 0 : model.ok6Count  ;
            parameters[9].Value = model.ok7Count == null ? 0 : model.ok7Count;
            parameters[10].Value = model.ok8Count == null ? 0 : model.ok8Count ;
            parameters[11].Value = model.ok9Count == null ? 0 : model.ok9Count ;
            parameters[12].Value = model.ok10Count == null ? 0 :model.ok10Count ;
            parameters[13].Value = model.ok11Count == null ? 0 :model.ok11Count ;
            parameters[14].Value = model.ok12Count == null ? 0 :model.ok12Count ;
            parameters[15].Value = model.ok13Count == null ? 0 :model.ok13Count ;
            parameters[16].Value = model.ok14Count == null ? 0 :model.ok14Count ;
            parameters[17].Value = model.ok15Count == null ? 0 :model.ok15Count ;
            parameters[18].Value = model.ok16Count == null ? 0 : model.ok16Count;

            parameters[19].Value = model.ng1Count == null ? 0 : model.ng1Count;
            parameters[20].Value = model.ng2Count == null ? 0 : model.ng2Count;
            parameters[21].Value = model.ng3Count == null ? 0 : model.ng3Count;
            parameters[22].Value = model.ng4Count == null ? 0 : model.ng4Count;
            parameters[23].Value = model.ng5Count == null ? 0 : model.ng5Count;
            parameters[24].Value = model.ng6Count == null ? 0 : model.ng6Count;
            parameters[25].Value = model.ng7Count == null ? 0 : model.ng7Count;
            parameters[26].Value = model.ng8Count == null ? 0 : model.ng8Count;
            parameters[27].Value = model.ng9Count == null ? 0 : model.ng9Count;
            parameters[28].Value = model.ng10Count == null ? 0 : model.ng10Count;
            parameters[29].Value = model.ng11Count == null ? 0 : model.ng11Count;
            parameters[30].Value = model.ng12Count == null ? 0 : model.ng12Count;
            parameters[31].Value = model.ng13Count == null ? 0 : model.ng13Count;
            parameters[32].Value = model.ng14Count == null ? 0 : model.ng14Count;
            parameters[33].Value = model.ng15Count == null ? 0 : model.ng15Count;
            parameters[34].Value = model.ng16Count == null ? 0 : model.ng16Count;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        
        
     

        #region MyRegion
        internal DataSet getNGByJobnumber(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append(@"select *
                                ,b.module, b.customer

                                from lmmswatchdog_shift a left join LMMSBom b on a.partNumber = b.partNumber and a.machineID = b.machineID where 1=1 ");

                


                if (sJobnumber != "")
                {
                    strSql.Append(" and  a.jobnumber = @jobnumber ");
                }

                if (sDay != "")
                {
                    strSql.Append(" and  a.day = @day ");
                }

                if (sShift != "" && sShift != "ALL")
                {
                    strSql.Append(" and  a.shift = @shift ");
                }

                if (sMachineID != "")
                {
                    strSql.Append(" and  a.machineid = @machineid ");
                }
                if (sPartNumber != "")
                {
                    strSql.Append(" and  a.partnumber = @partnumber ");
                }

                strSql.Append(" and  a.datetime > @dateFrom ");
                strSql.Append(" and  a.datetime < @dateTo+1 ");


                SqlParameter[] parameters =
                {
                    new SqlParameter("@jobnumber", SqlDbType.VarChar),
                    new SqlParameter("@day", SqlDbType.DateTime),
                    new SqlParameter("@shift", SqlDbType.VarChar),
                    new SqlParameter("@dateFrom", SqlDbType.DateTime),
                    new SqlParameter("@dateTo",SqlDbType.DateTime),
                    new SqlParameter("@machineid",SqlDbType.VarChar),
                    new SqlParameter("@partnumber",SqlDbType.VarChar)
                };

                if (sJobnumber != "")
                    parameters[0].Value = sJobnumber;
                else
                    parameters[0] = null;

                if (sDay != "")

                    parameters[1].Value = DateTime.Parse(sDay).Date;
                else
                    parameters[1] = null;

                if (sShift != "" && sShift != "ALL")
                    parameters[2].Value = sShift;
                else
                    parameters[2] = null;


                parameters[3].Value = dDateFrom;
                parameters[4].Value = dDateTo;

                if (sMachineID != "")
                    parameters[5].Value = sMachineID;
                else
                    parameters[5] = null;

                if (sPartNumber != "")
                    parameters[6].Value = sPartNumber;
                else
                    parameters[6] = null;


                return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LMMSWatchLog", "GetJobList exception:" + ee.ToString());
                return null;
            }
        }

       
        internal DataTable GetSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@"
select 
a.machineID
,ISNULL(b.number ,'') as number
,SUM(a.totalpass) as totalpass
,SUM(a.totalfail) as totalfail
,SUM(a.totalpass + a.totalfail)  as output
from LMMSWatchDog_Shift a 
left join lmmsbom b  on a.partnumber = b.partnumber and a.machineID=b.machineID
where 1=1 and a.day >= @dateFrom and a.day < @dateTo ");


            if (sPartNo != "")
                strSql.Append(" and a.PartNumber = @partNo ");

            if (sShift != "")
                strSql.Append(" and a.shift = @shift ");


            strSql.Append(" group by b.number , a.machineID ");



            SqlParameter[] parameters =
            {
                new SqlParameter("@dateFrom", SqlDbType.DateTime, 30),
                new SqlParameter("@dateTo", SqlDbType.DateTime, 30),
                new SqlParameter("@partNo", SqlDbType.VarChar,50),
                new SqlParameter("@shift", SqlDbType.VarChar,50)
            };

            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;

            if (sPartNo != "") parameters[2].Value = sPartNo; else parameters[2] = null;
            if (sShift != "") parameters[3].Value = sShift; else parameters[3] = null;
            

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

        
        internal DataSet GetJobList(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber,string sMachineID,string sPartNumber, string sModule)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append(@"select a.*,b.* from lmmswatchdog_shift a left join lmmsbom b on a.partnumber = b.partnumber and a.machineID = b.machineID  where 1=1 ");

                if (sJobnumber != "")
                {
                    strSql.Append(" and  a.jobnumber = @jobnumber ");
                }

                if (sDay != "")
                {
                    strSql.Append(" and  a.day = @day ");
                }

                if (sShift != "" && sShift != "ALL")
                {
                    strSql.Append(" and  a.shift = @shift ");
                }

                if (sMachineID != "")
                {
                    strSql.Append(" and  a.machineid = @machineid ");
                }
                if (sPartNumber != "")
                {
                    strSql.Append(" and  a.partnumber = @partnumber ");
                }
                if (!string.IsNullOrEmpty(sModule))
                {
                    strSql.Append(" and  b.module = @module ");
                }



                strSql.Append(" and  a.datetime > @dateFrom ");
                strSql.Append(" and  a.datetime < @dateTo+1 ");

                DBHelp.Reports.LogFile.Log("UpdateNG_Debug", "sqlStr: "+strSql.ToString());

                SqlParameter[] parameters =
                {
                    new SqlParameter("@jobnumber", SqlDbType.VarChar),
                    new SqlParameter("@day", SqlDbType.DateTime),
                    new SqlParameter("@shift", SqlDbType.VarChar),
                    new SqlParameter("@dateFrom", SqlDbType.DateTime),
                    new SqlParameter("@dateTo",SqlDbType.DateTime),
                    new SqlParameter("@machineid",SqlDbType.VarChar),
                    new SqlParameter("@partnumber",SqlDbType.VarChar),
                    new SqlParameter("@module",SqlDbType.VarChar)
                };

                if (sJobnumber != "")
                    parameters[0].Value = sJobnumber;
                else
                    parameters[0] = null;

                if (sDay != "")
                    parameters[1].Value = sDay;
               else
                    parameters[1] = null;

                if (sShift != "" && sShift != "ALL")
                    parameters[2].Value = sShift;
                else
                    parameters[2] = null;
                

                parameters[3].Value = dDateFrom;
                parameters[4].Value = dDateTo;

                if (sMachineID != "")
                    parameters[5].Value = sMachineID;
                else
                    parameters[5] = null;

                if (sPartNumber != "")
                    parameters[6].Value = sPartNumber;
                else
                    parameters[6] = null;

                if (!string.IsNullOrEmpty(sModule))
                    parameters[7].Value = sModule;
                else
                    parameters[7] = null;


                return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LMMSWatchLog", "GetJobList exception:"+ee.ToString());
                return null;
            }
        }
        
        internal DataSet GetLaserQty(string strJobIn)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select 
a.jobNumber,
partNumber,
quantity as mrpQty,
isnull(b.outputQty,0) as outputQty,
isnull(b.okQty,0) as okQty,
isnull(b.ngQty,0) as ngQty,
pqcQuantity as shortage,
isnull(setupQty,0) as setupQty,
isnull(buyoffqty, 0) as buyoffQty
from LMMSInventory a 
left join (
	select 
	jobNumber,
	sum(totalpass + totalfail) as outputQty,
	sum(totalPass) as okQty,
	sum(totalFail) as ngQty
	from LMMSWatchDog_Shift group by jobNumber
) b on a.jobNumber = b.jobNumber 
where 1=1 and a.jobnumber in ("+ strJobIn + ")");


            return DBHelp.SqlDB.Query(strSql.ToString());
        }




        
       

        public DataTable GetProductionDetailReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sModel,string sPartNo, string sMachineID, string sJobNo)
        {

        
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select
	convert(varchar(50), a.day,111) + ' - '+a.shift as shift
	,'Machine' + a.machineID as machineID  
	,b.module as model
	,a.jobNumber as jobNo
	,a.partNumber as partNo  
	,startTime as startTime
	,stopTime as endTime
	,convert(varchar,  ISNULL([stopTime], GETDATE()) - [startTime], 108) as time
		
	--PCS
	,a.totalPass as ok
	,a.totalFail as ng
	,a.totalpass + a.totalFail as output
	,totalQuantity as mrpTotal
    ,isnull(a.setUpQTY,0) *  isnull(c.materialCount,1) as setupQty
	--PCS

	--SET
    ,dbo.GetMinOK(ok1Count,ok2Count,ok3Count,ok4Count,ok5Count,ok6Count,ok7Count,ok8Count,ok9Count,ok10Count,ok11Count,ok12Count,ok13Count,ok14Count,ok15Count,ok16Count) as setOK
	,dbo.GetMaxNG(ng1Count,ng2Count,ng3Count,ng4Count,ng5Count,ng6Count,ng7Count,ng8Count,ng9Count,ng10Count,ng11Count,ng12Count,ng13Count,ng14Count,ng15Count,ng16Count) as setNG	
	,dbo.GetMaxNG(ng1Count,ng2Count,ng3Count,ng4Count,ng5Count,ng6Count,ng7Count,ng8Count,ng9Count,ng10Count,ng11Count,ng12Count,ng13Count,ng14Count,ng15Count,ng16Count) 
	 +
	 dbo.GetMinOK(ok1Count,ok2Count,ok3Count,ok4Count,ok5Count,ok6Count,ok7Count,ok8Count,ok9Count,ok10Count,ok11Count,ok12Count,ok13Count,ok14Count,ok15Count,ok16Count) as setOutput
	,totalQuantity / isnull(c.materialcount,1) as setMrpTotal
	,isnull(a.setUpQTY,0) as setSetupQty
	--SET

	--Display  占个格子, 后续在代码中生成.
	,'' as displayOK
	,'' as displayNG
	,'' as displayOutput
	,'' as displayRejRate
	,'' as displaySetup
	,'' as displayMRP
	--Display

FROM[LMMSWatchDog_Shift] a 
left join LMMSBom b on a.partNumber = b.partNumber and a.machineID = b.machineID
left join (select partNumber, count(1) as materialCount from lmmsbomdetail group by partNumber) c on a.partNumber = c.partNumber
where 1=1  
--数量0的记录也显示 --2020 08 19
--and (a.totalPass + a.totalFail) > 0
");

            strSql.Append(" and a.day >= @dateFrom ");
            strSql.Append(" and a.day < @dateTo ");


            if (!string.IsNullOrEmpty(sShift))
                strSql.Append(" and a.shift = @shift ");

            if (!string.IsNullOrEmpty(sModel))
                strSql.Append(" and b.module = @model ");

            if (!string.IsNullOrEmpty(sPartNo))
                strSql.Append(" and a.partNumber = @partNumber ");

            if (!string.IsNullOrEmpty(sMachineID))
                strSql.Append(" and a.machineID = @machineID ");

            if (!string.IsNullOrEmpty(sJobNo))
                strSql.Append(" and a.jobNumber = @jobNumber ");


            strSql.Append(" order by a.datetime desc ");

            



            SqlParameter[] parameters = {
                new SqlParameter("@dateFrom", SqlDbType.DateTime),
                new SqlParameter("@dateTo", SqlDbType.DateTime),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@model", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };


            parameters[0].Value = dDateFrom;
            parameters[1].Value = dDateTo;
            if (!string.IsNullOrEmpty(sShift)) parameters[2].Value = sShift; else parameters[2] = null;
            if (!string.IsNullOrEmpty(sModel)) parameters[3].Value = sModel; else parameters[3] = null;
            if (!string.IsNullOrEmpty(sPartNo)) parameters[4].Value = sPartNo; else parameters[4] = null;
            if (!string.IsNullOrEmpty(sMachineID)) parameters[5].Value = sMachineID; else parameters[5] = null;
            if (!string.IsNullOrEmpty(sJobNo)) parameters[6].Value = sJobNo; else parameters[6] = null;

            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }

       



        internal DataSet getRealJobInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.lotno, a.* from lmmswatchdog a left join lmmsinventory b on a.jobnumber  = b.jobnumber  ");
            return DBHelp.SqlDB.Query(strSql.ToString());
        }
        

        internal DataSet IsJobFinished(string sJobNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select jobNumber, totalPass, totalFail, totalQuantity from LMMSWatchLog where jobNumber = @jobNumber");
       
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar ,30)
            };

            parameters[0].Value = sJobNumber;

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }


        


        #region Daily, Monthly, Yearly  product chart
        internal DataTable getYearlyProduct(string sPartNo, string sShift, string sMachineID, string sModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    Year(a.day) as [Year]
                    ,sum(isnull(a.totalPass,0) + isnull( a.totalFail,0)) as totalQuantity
                    ,sum(isnull(totalPass,0)) as totalPass
                    ,sum(isnull(totalFail,0)) as totalFail
                    ,convert(decimal(18,2), sum(convert(float, isnull( totalFail,0))) / sum(convert(float, isnull(totalPass,0) + ISNULL(totalFail,0))) * 100) as rejReate
                    

                    from LMMSWatchDog_Shift a
                    left join 
                    (
	                    select distinct partNumber , module as model from LMMSBom
                    ) b on a.partNumber = b.partNumber

                    where 1=1 
                    and isnull(totalQuantity,0) > 0 
                    and isnull(totalPass,0) + isnull(totalFail,0) > 0");
            
            
            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sShift != "")
                strSql.AppendLine(" and a.shift = @Shift ");

            if (sMachineID != "")
                strSql.AppendLine(" and a.machineID = @MachineID ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");

            strSql.AppendLine(" group by Year(day) order by Year(day) asc ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50)
            };


            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;
            if (sMachineID != "") parameters[2].Value = sMachineID; else parameters[2] = null;
            if (sModel != "") parameters[3].Value = sModel; else parameters[3] = null;



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
        
        internal DataTable getMonthlyProduct(string sPartNo, string sShift, int iYear, string sMachineID, string sModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    a.Month
                    ,isnull( b.totalPass,0) + isnull( b.totalFail,0) as totalQuantity
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
	                    ,sum(isnull(totalQuantity,0)) as totalQuantity
	                    ,sum(isnull(totalPass,0)) as totalPass
	                    ,sum(isnull(totalFail,0)) as totalFail
	                    ,convert(decimal(18,2), sum(convert(float, isnull( totalFail,0))) / sum(convert(float, isnull(totalPass,0) + ISNULL(totalFail,0))) * 100) as rejReate

	                    from LMMSWatchDog_Shift aa
                        left join 
                        (
	                        select distinct partNumber , module as model from LMMSBom
                        ) bb on aa.partNumber = bb.partNumber


	                    where 1=1
	                    and isnull(totalQuantity,0) > 0
	                    and isnull(totalPass,0) + isnull(totalFail,0) > 0
	                    and Year(aa.day) = @Year");


            if (sPartNo != "")
                strSql.AppendLine(" and aa.partNumber = @PartNo  ");

            if (sShift != "")
                strSql.AppendLine(" and aa.shift = @Shift ");

            if (sMachineID != "")
                strSql.AppendLine(" and aa.machineID = @MachineID ");

            if (sModel != "")
                strSql.AppendLine(" and bb.Model = @Model ");

            strSql.Append(@"	
                        group by Month(aa.day)
                        ) b
                        on a.SN = b.Month 

                        order by a.SN ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@Year",SqlDbType.Int),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50)
            };


            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;
            parameters[2].Value = iYear;
            if (sMachineID != "") parameters[3].Value = sMachineID; else parameters[3] = null;
            if (sModel != "") parameters[4].Value = sModel; else parameters[4] = null;


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
        
        internal DataTable getDailyProduct(string sPartNo, string sShift, DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sModel)
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
,Sum(isnull(totalPass,0) + isnull(totalFail,0)) as totalQuantity
,sum(isnull(totalPass,0)) as totalPass
,sum(isnull(totalFail,0)) as totalFail
,convert(decimal(18,2), sum(convert(float, isnull( totalFail,0))) / sum(convert(float, isnull(totalPass,0) + ISNULL(totalFail,0))) * 100) as rejReate

from LMMSWatchDog_Shift a
left join 
(
	select distinct partNumber , module as model from LMMSBom
) b on a.partNumber = b.partNumber

where 1=1
and isnull(totalQuantity,0) > 0
and isnull(totalPass,0) + isnull(totalFail,0) > 0
and a.datetime >= @dDateFrom
and a.datetime < @dDateTo");


            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sShift != "")
                strSql.AppendLine(" and a.shift = @Shift ");

            if (sMachineID != "")
                strSql.AppendLine(" and a.machineID = @MachineID ");

            if (sModel != "")
                strSql.AppendLine(" and b.Model = @Model ");

            strSql.AppendLine(" group by Month(a.day), Day(a.day) order by Month(a.day) asc, Day(a.day) asc ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50),
                new SqlParameter("@Model", SqlDbType.VarChar,50)
            };
            
            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;
            
            parameters[2].Value = dDateFrom;
            parameters[3].Value = dDateTo;

            if (sMachineID != "") parameters[4].Value = sMachineID; else parameters[4] = null;
            if (sModel != "") parameters[5].Value = sModel; else parameters[5] = null;



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

        internal DataTable getProductGroupByModel(string sPartNo ,string sShift, DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select 
                    b.model 
                    ,Sum(isnull(a.totalPass,0) + isnull(a.totalFail,0)) as totalQuantity
                    ,sum(isnull(totalPass,0)) as totalPass
                    ,sum(isnull(totalFail,0)) as totalFail
                    ,convert(decimal(18,2), sum(convert(float, isnull( totalFail,0))) / sum(convert(float, isnull(totalPass,0) + ISNULL(totalFail,0))) * 100) as rejReate

                    from LMMSWatchDog_Shift a 
                    left join 
                    (
	                    select partnumber,module as model from LMMSBom 
                    )b on a.partNumber = b.partNumber
                    where 1=1
                    and isnull(totalQuantity,0) > 0
                    and isnull(totalPass,0) + isnull(totalFail,0) > 0
                    and a.datetime > @dDateFrom
                    and a.datetime < @dDateTo");


            if (sPartNo != "")
                strSql.AppendLine(" and a.partNumber = @PartNo  ");

            if (sShift != "")
                strSql.AppendLine(" and a.shift = @Shift ");

            if (sMachineID != "")
                strSql.AppendLine(" and a.MachineID = @MachineID ");

            strSql.AppendLine(" group by b.model  order by b.model asc  ");




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@MachineID", SqlDbType.VarChar,50)
            };

            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;

            parameters[2].Value = dDateFrom;
            parameters[3].Value = dDateTo;
            if (sMachineID != "") parameters[4].Value = sMachineID; else parameters[4] = null;


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

        internal DataTable getProductGroupByMachine(string sPartNo, string sShift, DateTime dDateFrom, DateTime dDateTo, string sModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    select
                    b.MachineID
                    ,isnull( b.totalPass,0) + isnull( b.totalFail,0) as totalQuantity
                    ,ISNULL(b.totalPass,0) as totalPass
                    ,ISNULL(b.totalFail,0) as totalFail
                    ,ISNULL(b.rejReate,0) as rejReate

                    from
                    (
	                    select 1 as ID union
	                    select 2 as ID union
	                    select 3 as ID union
	                    select 4 as ID union
	                    select 5 as ID union
	                    select 6 as ID union
	                    select 7 as ID union
	                    select 8 as ID 
                    ) a
                    left join
                    (
	                    select 
	                    aa.machineID as ID
	                    ,'Machine' +  aa.machineID as MachineID
	                    ,sum(isnull(totalQuantity,0)) as totalQuantity
	                    ,sum(isnull(totalPass,0)) as totalPass
	                    ,sum(isnull(totalFail,0)) as totalFail
	                    ,convert(decimal(18,2), sum(convert(float, isnull( totalFail,0))) / sum(convert(float, isnull(totalPass,0) + ISNULL(totalFail,0))) * 100) as rejReate

	                    from LMMSWatchDog_Shift  aa
                        left join 
                        (
	                        select distinct partNumber , module as model from LMMSBom
                        ) bb on aa.partNumber = bb.partNumber


	                    where 1=1
	                    and isnull(totalQuantity,0) > 0
	                    and isnull(totalPass,0) + isnull(totalFail,0) > 0
	                    and aa.datetime > @dDateFrom
	                    and aa.datetime < @dDateTo");


            if (sPartNo != "")
                strSql.AppendLine(" and aa.partNumber = @PartNo  ");

            if (sShift != "")
                strSql.AppendLine(" and aa.shift = @Shift ");

            if (sModel != "")
                strSql.AppendLine(" and bb.Model = @Model ");



            strSql.Append(@" 	group by machineID
                                )b on a.ID = b.ID order by a.ID asc  ");

         




            SqlParameter[] parameters = {
                new SqlParameter("@PartNo", SqlDbType.VarChar ,50),
                new SqlParameter("@Shift", SqlDbType.VarChar,50),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@Model",SqlDbType.VarChar)
            };

            if (sPartNo != "") parameters[0].Value = sPartNo; else parameters[0] = null;
            if (sShift != "") parameters[1].Value = sShift; else parameters[1] = null;

            parameters[2].Value = dDateFrom;
            parameters[3].Value = dDateTo;

            if (sModel != "") parameters[4].Value = sModel; else parameters[4] = null;


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

        #endregion

 
        
        public DataTable GetLaserRejForButtonReport_NEW(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
with laserInfo as 
(
    select
    a.jobNumber
    , a.model1Name
    , a.model2Name
    , a.model3Name
    , a.model4Name
    , a.model5Name
    , a.model6Name
    , a.model7Name
    , a.model8Name
    , a.model9Name
    , a.model10Name
    , a.model11Name
    , a.ng1Count
    , a.ng2Count
    , a.ng3Count
    , a.ng4Count
    , a.ng5Count
    , a.ng6Count
    , a.ng7Count
    , a.ng8Count
    , a.ng9Count
    , a.ng10Count
    , a.ng11Count
    , b.pqcQuantity as shortage
    , b.buyOffQty
    , b.setUpQTY
    from LMMSWatchLog a
    left join LMMSInventory b on a.jobnumber = b.jobnumber
    where 1=1  and a.jobNumber in  " + strWhere + " )");


            strSql.Append(@"
select jobNumber, model1Name as materialNo, isnull(ng1Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model2Name as materialNo, isnull(ng2Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model3Name as materialNo, isnull(ng3Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model4Name as materialNo, isnull(ng4Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model5Name as materialNo, isnull(ng5Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model6Name as materialNo, isnull(ng6Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model7Name as materialNo, isnull(ng7Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model8Name as materialNo, isnull(ng8Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model9Name as materialNo, isnull(ng9Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model10Name as materialNo, isnull(ng10Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo union
select jobNumber, model11Name as materialNo, isnull(ng11Count,0) as ng, isnull(shortage,0) as shortage, isnull(setUpQTY,0) as setUpQty, isnull(buyOffQty,0) as buyOffQty from laserInfo");

          
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString());
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        
        internal DataTable getProductivityReportForLaser(DateTime dDay, string sShift)
        {
            StringBuilder strSql = new StringBuilder();
          
            strSql.Append(@" 
                            select
                            case when aa.type='Button' then 1 
                                 when aa.type='Print' then 2
	                             when aa.type='Lens' or aa.type='Button-TKS784'  then 3
	                             when aa.type='Bezel-257B' then 4
	                             when aa.type='Bezel-320B' then 5
	                             when aa.type='Panel' then 6
                                 when isnull(aa.type,'')='' then 8    
                                 else 9
                            end as SN

                            ,case when aa.type ='Button-TKS784' then 'Lens' 
                                  else aa.type 
	                              end as ProductType
                            ,'' as ManPower
                            ,'' as Attendance
                            ,'' as AttendRate 
                            ,'96' as TargetHR                          
                            ,   convert(varchar(50),  CONVERT(numeric(18,2),  CONVERT(numeric(18,4), sum(DATEDIFF(Second, starttime, ISNULL([stopTime], GETDATE())))) / 3600))  as ActualHR
                            ,convert(numeric(18,0),sum(aa.TargetQty)) as TargetQty
                            , sum(aa.OK) as ActualQty
                            , sum(aa.NG) as RejectQty
                            , sum(aa.OK + aa.NG) as TotalQty
                            , 8*12 as TargetHR
                            , CONVERT(varchar, convert(numeric(18,2), convert(numeric(18,4),sum(aa.NG))  / convert(numeric(18,4),sum(aa.OK + aa.NG))* 100)) + '%' as RejRate
                            ,'' as Utilization
                            ,'' as Productivity

                            from(
                                    select
                                    a.[day]
                                    , a.[shift]
                                    , case when isnull(  b.type,'') = '' 
                                      then 'Unknown Type:' + a.partNumber
                                      else b.type 
                                      end as type
                                    , a.startTime
                                    , a.stopTime
                                    , convert(varchar, ISNULL([stopTime], GETDATE()) - [startTime], 108) as Time

		                            ,case  when a.machineID in('6','7','8') then totalPass 
		                            else  (isnull(ok1Count, 0) + isnull(ok2Count, 0) + isnull(ok3Count, 0) + isnull(ok4Count, 0) + isnull(ok5Count, 0) + isnull(ok6Count, 0) + isnull(ok7Count, 0) + isnull(ok8Count, 0) + isnull(ok9Count, 0) + isnull(ok10Count, 0) + isnull(ok11Count, 0) + isnull(ok12Count, 0) + isnull(ok13Count, 0) + isnull(ok14Count, 0) + isnull(ok15Count, 0) + isnull(ok16Count, 0))
                                    end
                                    as OK

		                            ,case  when a.machineID in('6','7','8') then totalFail 
		                            else  (isnull(ng1Count, 0) + isnull(ng2Count, 0) + isnull(ng3Count, 0) + isnull(ng4Count, 0) + isnull(ng5Count, 0) + isnull(ng6Count, 0) + isnull(ng7Count, 0) + isnull(ng8Count, 0) + isnull(ng9Count, 0) + isnull(ng10Count, 0) + isnull(ng11Count, 0) + isnull(ng12Count, 0) + isnull(ng13Count, 0) + isnull(ng14Count, 0) + isnull(ng15Count, 0) + isnull(ng16Count, 0))
                                    end
                                    as NG

		                            , --理论产量
		                            DATEDIFF(Second, starttime, ISNULL([stopTime], GETDATE())) / (b.cycleTime / b.blockcount / b.unitcount)  as TargetQty

                                    from LMMSWatchDog_Shift a left join
		                            (
			                            select partnumber,type, AVG(cycleTime) as cycleTime,  AVG(blockcount) as blockcount, AVG(unitcount) as unitcount  from LMMSBom group by partNumber, type
		                            ) 
		                            b  on a.partNumber = b.partNumber 

                                    where a.day =@Day and a.shift =@Shift and a.totalQuantity > 0
                                    and  a.totalPass + a.totalFail > 0
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
        
        public DataSet GetJobMaterialList(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
with watchLog as ( 
	select * from LMMSWatchLog
	where jobnumber=@jobNumber
 )
select 
a.sn
,a.materialNo
,okQty
,ngQty
from (
	select 1  as sn, model1Name as materialNo, ok1Count as okQty, ng1Count as ngQty from watchLog union all
	select 2  as sn, model2Name as materialNo, ok2Count as okQty, ng2Count as ngQty from watchLog union all
	select 3  as sn, model3Name as materialNo, ok3Count as okQty, ng3Count as ngQty from watchLog union all
	select 4  as sn, model4Name as materialNo, ok4Count as okQty, ng4Count as ngQty from watchLog union all
	select 5  as sn, model5Name as materialNo, ok5Count as okQty, ng5Count as ngQty from watchLog union all
	select 6  as sn, model6Name as materialNo, ok6Count as okQty, ng6Count as ngQty from watchLog union all
	select 7  as sn, model7Name as materialNo, ok7Count as okQty, ng7Count as ngQty from watchLog union all
	select 8  as sn, model8Name as materialNo, ok8Count as okQty, ng8Count as ngQty from watchLog union all
	select 9  as sn, model9Name as materialNo, ok9Count as okQty, ng9Count as ngQty from watchLog union all
	select 10 as sn, model10Name as materialNo, ok10Count as okQty, ng10Count as ngQty from watchLog union all
	select 11 as sn, model11Name as materialNo, ok11Count as okQty, ng11Count as ngQty from watchLog union all
	select 12 as sn, model12Name as materialNo, ok12Count as okQty, ng12Count as ngQty from watchLog union all
	select 13 as sn, model13Name as materialNo, ok13Count as okQty, ng13Count as ngQty from watchLog union all
	select 14 as sn, model14Name as materialNo, ok14Count as okQty, ng14Count as ngQty from watchLog union all
	select 15 as sn, model15Name as materialNo, ok15Count as okQty, ng15Count as ngQty from watchLog union all
	select 16 as sn, model16Name as materialNo, ok16Count as okQty, ng16Count as ngQty from watchLog
) a
where a.materialNo != '' or okQty !=0 or ngQty !=0  ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar)
            };

            parameters[0].Value = sJobNo;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }
        
        public DataSet GetMaterialListForAllSectionReport(DateTime dStartTime)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"  
with watchLog as (
    SELECT
	a.jobNumber, a.partNumber,
    [model1Name],[model2Name],[model3Name],[model4Name],[model5Name],[model6Name],[model7Name],[model8Name],[model9Name],[model10Name],[model11Name]
    ,[ok1Count],[ok2Count],[ok3Count],[ok4Count],[ok5Count],[ok6Count],[ok7Count],[ok8Count],[ok9Count],[ok10Count],[ok11Count]
    ,[ng1Count],[ng2Count],[ng3Count],[ng4Count],[ng5Count],[ng6Count],[ng7Count],[ng8Count],[ng9Count],[ng10Count],[ng11Count]

	,case when a.totalPass + a.totalFail + isnull(b.pqcQuantity,0) * c.materialCount + isnull(b.setUpQTY,0) * c.materialCount + isnull(b.buyOffQty,0) * c.materialCount > = a.totalQuantity 
	then 'DONE'
	else 'ONGOING'
	end as jobStatus

    FROM lmmswatchlog a 
	left join LMMSInventory b on a.jobNumber = b.jobNumber
	left join (select partNumber, count(1) as materialCount from LMMSBomDetail group by partNumber  ) c 
	on c.partNumber = a.partNumber
    where 1 = 1 and a.datetime >= @dStartTime
)


select

partNumber, 
materialNo,
sum(okQty) as okQty,
sum(ngQty) as ngQty
from (
	select jobNumber, partNumber, model1Name as materialNo,  isnull(ok1Count,0) as okQty,  isnull(ng1Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model2Name as materialNo,  isnull(ok2Count,0) as okQty,  isnull(ng2Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model3Name as materialNo,  isnull(ok3Count,0) as okQty,  isnull(ng3Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model4Name as materialNo,  isnull(ok4Count,0) as okQty,  isnull(ng4Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model5Name as materialNo,  isnull(ok5Count,0) as okQty,  isnull(ng5Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model6Name as materialNo,  isnull(ok6Count,0) as okQty,  isnull(ng6Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model7Name as materialNo,  isnull(ok7Count,0) as okQty,  isnull(ng7Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model8Name as materialNo,  isnull(ok8Count,0) as okQty,  isnull(ng8Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model9Name as materialNo,  isnull(ok9Count,0) as okQty,  isnull(ng9Count ,0) as ngQty   ,jobStatus from watchLog union all
	select jobNumber, partNumber, model10Name as materialNo, isnull(ok10Count,0) as okQty, isnull(ng10Count,0)  as ngQty  ,jobStatus from watchLog union all
	select jobNumber, partNumber, model11Name as materialNo, isnull(ok11Count,0) as okQty, isnull(ng11Count,0)  as ngQty  ,jobStatus from watchLog 
) a

where a.materialNo != ''
and jobStatus != 'DONE'

group by a.partNumber, a.materialNo  ");



            SqlParameter[] parameters = {
                new SqlParameter("@dStartTime", SqlDbType.DateTime)
            };

            parameters[0].Value = dStartTime;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }
         

    }
    #endregion
}