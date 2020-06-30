 using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{

	public class LMMSWatchDog_His_DAL
	{
		public LMMSWatchDog_His_DAL()
		{}



        public DataSet GetList(string jobNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from LMMSWatchDog_Shift where 1=1 ");

            if (jobNumber.Trim() != "")
            {
                strSql.Append(" and jobNumber = @jobNumber");

            }


            SqlParameter[] paras =
            {
                new SqlParameter("@jobNumber",SqlDbType.VarChar,32)
            };

            if (jobNumber.Trim() != "")
            {
                paras[0].Value = jobNumber;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(), paras);
        }

        
        public SqlCommand UpdateJobMaintenanceCMD(Common.Model.LMMSWatchDog_His_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Update LMMSWatchDog_Shift SET ");

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
            strSql.Append(" and day = @day ");
            strSql.Append(" and shift = @shift");
            strSql.Append(" and machineID = @machineID");

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
                new SqlParameter("@ng16Count", SqlDbType.Int),

                new SqlParameter("@day", SqlDbType.DateTime),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar)
            };


            parameters[0].Value = model.jobNumber;
            parameters[1].Value = model.totalPass;
            parameters[2].Value = model.totalFail;

            parameters[3].Value = model.ok1Count == null ? 0 : model.ok1Count;
            parameters[4].Value = model.ok2Count == null ? 0 : model.ok2Count;
            parameters[5].Value = model.ok3Count == null ? 0 : model.ok3Count;
            parameters[6].Value = model.ok4Count == null ? 0 : model.ok4Count;
            parameters[7].Value = model.ok5Count == null ? 0 : model.ok5Count;
            parameters[8].Value = model.ok6Count == null ? 0 : model.ok6Count;
            parameters[9].Value = model.ok7Count == null ? 0 : model.ok7Count;
            parameters[10].Value = model.ok8Count == null ? 0 : model.ok8Count;
            parameters[11].Value = model.ok9Count == null ? 0 : model.ok9Count;
            parameters[12].Value = model.ok10Count == null ? 0 : model.ok10Count;
            parameters[13].Value = model.ok11Count == null ? 0 : model.ok11Count;
            parameters[14].Value = model.ok12Count == null ? 0 : model.ok12Count;
            parameters[15].Value = model.ok13Count == null ? 0 : model.ok13Count;
            parameters[16].Value = model.ok14Count == null ? 0 : model.ok14Count;
            parameters[17].Value = model.ok15Count == null ? 0 : model.ok15Count;
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

            parameters[35].Value = model.day;
            parameters[36].Value = model.shift;
            parameters[37].Value = model.machineID;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        
        internal DataSet SelectMachineInfo (string sMachineID)
        {
            StringBuilder strSql = new StringBuilder(); 

            strSql.Append("select top 1 a.*, b.CurrentPower from LMMSWatchDog a left join LMMSBom b on a.partnumber = b.partnumber ");
            strSql.Append(" where a.machineID = @MachineID  ");

            SqlParameter[] parameters = {
                new SqlParameter("@MachineID", SqlDbType.VarChar)
            };

            parameters[0].Value = sMachineID;
            return  DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }


        public DataSet SelectJobInfo(string sJobnumber)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select  top 1 
                            a.partnumber
                            ,b.CurrentPower 
                            ,c.lotNo
                            ,convert(varchar(50), c.startOnTime,23) as MFGDate
                            ,c.quantity as lotqty

                            from LMMSWatchDog a 
                            left join LMMSBom b on a.partnumber = b.partnumber and a.machineID = b.machineID 
                            left join LMMSInventory c on a.jobNumber = c.jobNumber  ");

            strSql.Append(" where a.jobnumber = @jobnumber  ");

            SqlParameter[] parameters = {
                new SqlParameter("@jobnumber", SqlDbType.VarChar)
            };

            parameters[0].Value = sJobnumber;
            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        
        public DataSet GetJobMaterialList(string sJobNo, DateTime? dDay, string sShift, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@"
with dogShift as ( 
	select * from LMMSWatchDog_Shift
	where 1=1 and jobnumber=@jobNumber 
    and day=@day
    and shift=@shift
    and machineID=@machineID
 )


select 
a.sn
,case when a.materialNo = '' and (a.okQty + a.ngQty) != 0 
	then 'Error,unknown material no. Please check vision xml name format!'
	else a.materialNo
end as materialNo
,okQty
,ngQty
from (
	select 1  as sn, model1Name as materialNo, ok1Count as okQty, ng1Count as ngQty from dogShift union all
	select 2  as sn, model2Name as materialNo, ok2Count as okQty, ng2Count as ngQty from dogShift union all
	select 3  as sn, model3Name as materialNo, ok3Count as okQty, ng3Count as ngQty from dogShift union all
	select 4  as sn, model4Name as materialNo, ok4Count as okQty, ng4Count as ngQty from dogShift union all
	select 5  as sn, model5Name as materialNo, ok5Count as okQty, ng5Count as ngQty from dogShift union all
	select 6  as sn, model6Name as materialNo, ok6Count as okQty, ng6Count as ngQty from dogShift union all
	select 7  as sn, model7Name as materialNo, ok7Count as okQty, ng7Count as ngQty from dogShift union all
	select 8  as sn, model8Name as materialNo, ok8Count as okQty, ng8Count as ngQty from dogShift union all
	select 9  as sn, model9Name as materialNo, ok9Count as okQty, ng9Count as ngQty from dogShift union all
	select 10 as sn, model10Name as materialNo, ok10Count as okQty, ng10Count as ngQty from dogShift union all
	select 11 as sn, model11Name as materialNo, ok11Count as okQty, ng11Count as ngQty from dogShift union all
	select 12 as sn, model12Name as materialNo, ok12Count as okQty, ng12Count as ngQty from dogShift union all
	select 13 as sn, model13Name as materialNo, ok13Count as okQty, ng13Count as ngQty from dogShift union all
	select 14 as sn, model14Name as materialNo, ok14Count as okQty, ng14Count as ngQty from dogShift union all
	select 15 as sn, model15Name as materialNo, ok15Count as okQty, ng15Count as ngQty from dogShift union all
	select 16 as sn, model16Name as materialNo, ok16Count as okQty, ng16Count as ngQty from dogShift
) a
where a.materialNo != '' or okQty !=0 or ngQty !=0  ");


            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar),
                new SqlParameter("@day", SqlDbType.DateTime),
                new SqlParameter("@shift", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar)
            };

            parameters[0].Value = sJobNo;
            parameters[1].Value = dDay;
            parameters[2].Value = sShift;
            parameters[3].Value = sMachineID;


            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }


        
    }
}

