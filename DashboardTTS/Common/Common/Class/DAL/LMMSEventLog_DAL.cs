
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:LMMSEventLog_DAL
	/// </summary>
	public class LMMSEventLog_DAL
	{
		public LMMSEventLog_DAL()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public DataSet Exists()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LMMSEventLog");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			return DBHelp.SqlDB.Query(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Common.Model.LMMSEventLog_Model model)
		{
            return 0;
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Model.LMMSEventLog_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSEventLog(");
			strSql.Append("dateTime,machineID,currentOperation,ownerID,eventTrigger,startTime,stopTime,ipSetting)");
			strSql.Append(" values (");
			strSql.Append("@dateTime,@machineID,@currentOperation,@ownerID,@eventTrigger,@startTime,@stopTime,@ipSetting)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@dateTime", SqlDbType.DateTime),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@currentOperation", SqlDbType.VarChar,50),
					new SqlParameter("@ownerID", SqlDbType.VarChar,50),
					new SqlParameter("@eventTrigger", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.SmallDateTime),
					new SqlParameter("@stopTime", SqlDbType.SmallDateTime),
					new SqlParameter("@ipSetting", SqlDbType.VarChar,50)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public SqlCommand AddCommand(Common.Model.LMMSEventLog_Model model)"  + "TableName:LMMSEventLog" , ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + ";currentOperation = "+ (model.currentOperation == null ? "" : model.currentOperation.ToString()) + ";ownerID = "+ (model.ownerID == null ? "" : model.ownerID.ToString()) + ";eventTrigger = "+ (model.eventTrigger == null ? "" : model.eventTrigger.ToString()) + ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString()) + ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString()) + ";ipSetting = "+ (model.ipSetting == null ? "" : model.ipSetting.ToString()) + "");
			parameters[0].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[1].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[2].Value = model.currentOperation == null ? (object)DBNull.Value : model.currentOperation ;
			parameters[3].Value = model.ownerID == null ? (object)DBNull.Value : model.ownerID ;
			parameters[4].Value = model.eventTrigger == null ? (object)DBNull.Value : model.eventTrigger ;
			parameters[5].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[6].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[7].Value = model.ipSetting == null ? (object)DBNull.Value : model.ipSetting ;

			 return  DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Model.LMMSEventLog_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSEventLog set ");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("currentOperation=@currentOperation,");
			strSql.Append("ownerID=@ownerID,");
			strSql.Append("eventTrigger=@eventTrigger,");
			strSql.Append("startTime=@startTime,");
			strSql.Append("stopTime=@stopTime,");
			strSql.Append("ipSetting=@ipSetting");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.DateTime),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@currentOperation", SqlDbType.VarChar,50),
					new SqlParameter("@ownerID", SqlDbType.VarChar,50),
					new SqlParameter("@eventTrigger", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.SmallDateTime),
					new SqlParameter("@stopTime", SqlDbType.SmallDateTime),
					new SqlParameter("@ipSetting", SqlDbType.VarChar,50)};
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[3].Value = model.currentOperation == null ? (object)DBNull.Value : model.currentOperation ;
			parameters[4].Value = model.ownerID == null ? (object)DBNull.Value : model.ownerID ;
			parameters[5].Value = model.eventTrigger == null ? (object)DBNull.Value : model.eventTrigger ;
			parameters[6].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[7].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[8].Value = model.ipSetting == null ? (object)DBNull.Value : model.ipSetting ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public bool Update(Common.Model.LMMSEventLog_Model model)"  + "TableName:LMMSEventLog" , ";id = "+ (model.id == null ? "" : model.id.ToString() ) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + ";currentOperation = "+ (model.currentOperation == null ? "" : model.currentOperation.ToString() ) + ";ownerID = "+ (model.ownerID == null ? "" : model.ownerID.ToString() ) + ";eventTrigger = "+ (model.eventTrigger == null ? "" : model.eventTrigger.ToString() ) + ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString() ) + ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString() ) + ";ipSetting = "+ (model.ipSetting == null ? "" : model.ipSetting.ToString() ) + "");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Model.LMMSEventLog_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSEventLog set ");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("currentOperation=@currentOperation,");
			strSql.Append("ownerID=@ownerID,");
			strSql.Append("eventTrigger=@eventTrigger,");
			strSql.Append("startTime=@startTime,");
			strSql.Append("stopTime=@stopTime,");
			strSql.Append("ipSetting=@ipSetting");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.DateTime),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@currentOperation", SqlDbType.VarChar,50),
					new SqlParameter("@ownerID", SqlDbType.VarChar,50),
					new SqlParameter("@eventTrigger", SqlDbType.VarChar,50),
					new SqlParameter("@startTime", SqlDbType.SmallDateTime),
					new SqlParameter("@stopTime", SqlDbType.SmallDateTime),
					new SqlParameter("@ipSetting", SqlDbType.VarChar,50)};
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[3].Value = model.currentOperation == null ? (object)DBNull.Value : model.currentOperation ;
			parameters[4].Value = model.ownerID == null ? (object)DBNull.Value : model.ownerID ;
			parameters[5].Value = model.eventTrigger == null ? (object)DBNull.Value : model.eventTrigger ;
			parameters[6].Value = model.startTime == null ? (object)DBNull.Value : model.startTime ;
			parameters[7].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime ;
			parameters[8].Value = model.ipSetting == null ? (object)DBNull.Value : model.ipSetting ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public SqlCommand UpdateCommand(Common.Model.LMMSEventLog_Model model)"  + "TableName:LMMSEventLog" , ";id = "+ (model.id == null ? "" : model.id.ToString()) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + ";currentOperation = "+ (model.currentOperation == null ? "" : model.currentOperation.ToString()) + ";ownerID = "+ (model.ownerID == null ? "" : model.ownerID.ToString()) + ";eventTrigger = "+ (model.eventTrigger == null ? "" : model.eventTrigger.ToString()) + ";startTime = "+ (model.startTime == null ? "" : model.startTime.ToString()) + ";stopTime = "+ (model.stopTime == null ? "" : model.stopTime.ToString()) + ";ipSetting = "+ (model.ipSetting == null ? "" : model.ipSetting.ToString()) + "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSEventLog ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public bool Delete(int id)"  + "TableName:LMMSEventLog" , "");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSEventLog ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSEventLog ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public SqlCommand DeleteCommand(int id)"  + "TableName:LMMSEventLog" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSEventLog ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public SqlCommand DeleteAllCommand( )"  + "TableName:LMMSEventLog" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Model.LMMSEventLog_Model GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,dateTime,machineID,currentOperation,ownerID,eventTrigger,startTime,stopTime,ipSetting from LMMSEventLog ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
			parameters[0].Value = id;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSEventLog_DAL" , "Function:		public Common.Model.LMMSEventLog_Model GetModel(int id)"  + "TableName:LMMSEventLog" , "");
			Common.Model.LMMSEventLog_Model model=new Common.Model.LMMSEventLog_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["dateTime"].ToString()!="")
				{
					model.dateTime=DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
				}
				model.machineID=ds.Tables[0].Rows[0]["machineID"].ToString();
				model.currentOperation=ds.Tables[0].Rows[0]["currentOperation"].ToString();
				model.ownerID=ds.Tables[0].Rows[0]["ownerID"].ToString();
				model.eventTrigger=ds.Tables[0].Rows[0]["eventTrigger"].ToString();
				if(ds.Tables[0].Rows[0]["startTime"].ToString()!="")
				{
					model.startTime=DateTime.Parse(ds.Tables[0].Rows[0]["startTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["stopTime"].ToString()!="")
				{
					model.stopTime=DateTime.Parse(ds.Tables[0].Rows[0]["stopTime"].ToString());
				}
				model.ipSetting=ds.Tables[0].Rows[0]["ipSetting"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,dateTime,machineID,currentOperation,ownerID,eventTrigger,startTime,stopTime,ipSetting ");
			strSql.Append(" FROM LMMSEventLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,dateTime,machineID,currentOperation,ownerID,eventTrigger,startTime,stopTime,ipSetting ");
			strSql.Append(" FROM LMMSEventLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

        public DataSet GetTodayList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  machineID, currentOperation, eventTrigger, startTime, stopTime FROM LMMSEventLog   
                            WHERE currentOperation like '%OEE' and dateTime >= CONVERT(varchar, GETDATE(), 23) ");
           
            return DBHelp.SqlDB.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "LMMSEventLog";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method

        #region MyRegion

        internal DataSet getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" select * from (
                            select id,dateTime,machineID,currentoperation,ownerID,eventtrigger,startTime,stopTime,ipSetting
                            ,currentoperation +'_'+eventtrigger as currentoperation_eventtrigger
                            from lmmseventlog ");
            strSql.Append(" ) a  where a.currentoperation_eventtrigger not in ('TECHNICIAN_OEE_POWER ON', 'TECHNICIAN_OEE_POWER OFF')");

            //2018 11 13 by wei lijia for eventlog case
            strSql.Append(@"  and  (select count(x.id) from [dbo].[LMMSWatchDog_Shift] x where  a.machineID = x.machineID  
                             and a.startTime < x.prepDateIn and a.stopTime > x.prepDateIn 
                             and a.eventTrigger <> 'POWER ON'
                              ) = 0  ");


            if (sMachineNo != "")
            {
                strSql.Append("  and MACHINEID = @MachineNo  ");
            }
            
            strSql.Append(" and dateTime >= @TimeFrom  ");
            strSql.Append(" and dateTime <= @TimeTo + 2   ");

            //operation status 排除 techion OEE power on/off
            //strSql.Append(" and dateTime <= @TimeTo + 1   ");

            // strSql.Append(" and currentOperation like '%oee'  ");
            strSql.Append(" ORDER BY starttime  DESC  ");
            SqlParameter[] parameters = {
                        new SqlParameter("@MachineNo", SqlDbType.VarChar,4) ,
                        new SqlParameter("@TimeFrom", SqlDbType.DateTime) ,
                        new SqlParameter("@TimeTo", SqlDbType.DateTime) };
            parameters[0].Value = sMachineNo;
            parameters[1].Value = dTimeFrom;
            parameters[2].Value = dTimeTo;


            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSEventLog_DAL", "Function:		public Common.Model.LMMSEventLog_Model getOEE(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", "");
           // Common.Model.LMMSEventLog_Model model = new Common.Model.LMMSEventLog_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds;
        }
        internal DataSet getOEE_Detail(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id  ");
            strSql.Append("  , machineID  ");
            strSql.Append("  , currentOperation  "); 
            strSql.Append("  , eventTrigger  ");
            strSql.Append("  , startTime  ");
            strSql.Append("  , stopTime  ");
            strSql.Append("   , convert(varchar, [stopTime] - [startTime], 108) as Time ");
            //strSql.Append("  , ownerID  ");
            strSql.Append(" FROM LMMSEventLog  ");
            strSql.Append(" WHERE currentOperation like '%OEE'  ");

            if (sMachineNo != "")
            {
                strSql.Append(" and MACHINEID = @MachineNo    ");
            }
            

            strSql.Append(" and dateTime >= @TimeFrom  ");
            strSql.Append(" and dateTime < @TimeTo + 1   ");

            // strSql.Append(" and currentOperation like '%oee'  ");
            strSql.Append(" ORDER BY starttime  asc  ");
            SqlParameter[] parameters = {
                        new SqlParameter("@MachineNo", SqlDbType.VarChar,4) ,
                        new SqlParameter("@TimeFrom", SqlDbType.DateTime) ,
                        new SqlParameter("@TimeTo", SqlDbType.DateTime) };


            parameters[0].Value = sMachineNo;
            parameters[1].Value = dTimeFrom;
            parameters[2].Value = dTimeTo;


            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:LMMSEventLog_DAL", "Function:		public Common.Model.LMMSEventLog_Model getOEE_Detial(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)" + "TableName:LMMSEventLog", "");
            Common.Model.LMMSEventLog_Model model = new Common.Model.LMMSEventLog_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds;
        }
        
        internal DataSet getCurrentStatus(DateTime dTimeFrom, DateTime dTimeTo, string sMachineNo, string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id  ");
            strSql.Append("  , machineID  ");
            strSql.Append("  , currentOperation  ");
            strSql.Append("  , eventTrigger  ");
            strSql.Append("  , startTime  ");
            strSql.Append("  , stopTime  ");
            strSql.Append("   , convert(varchar, [stopTime] - [startTime], 108) as Time ");
            strSql.Append(" FROM LMMSEventLog  ");
            strSql.Append(" WHERE MACHINEID = @MachineNo  and  currentOperation like '%OEE'");
            strSql.Append(" and dateTime >= @TimeFrom  ");
            strSql.Append(" and dateTime < @TimeTo + 1   ");
            
            strSql.Append(" ORDER BY stopTime  desc ,currentOperation desc  ");
            SqlParameter[] parameters = {
                        new SqlParameter("@MachineNo", SqlDbType.VarChar,4) ,
                        new SqlParameter("@TimeFrom", SqlDbType.DateTime) ,
                        new SqlParameter("@TimeTo", SqlDbType.DateTime) };
            parameters[0].Value = sMachineNo;
            parameters[1].Value = dTimeFrom;
            parameters[2].Value = dTimeTo;


            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds;
        }
        
        public DataTable SelectAdjustEvent(string sMachineID ,DateTime dStartTime, DateTime dStopTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select * from lmmseventlog where 1=1 ");
            strSql.Append(" and currentOperation in ('TECHNICIAN_OEE','SYSTEM_OEE')  ");
            strSql.Append(" and eventTrigger in ('ADJUSTMENT','TESTING','NO SCHEDULE','POWER OFF','SETUP','BUYOFF','MAINTAINENCE') ");
            if (sMachineID != "")
            {
                strSql.Append(" and machineID = @machineID ");
            }
            strSql.Append(" and startTime >= @startTime ");
            strSql.Append(" and stopTime <= @stopTime ");
            strSql.Append(" order by datetime desc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@machineID",SqlDbType.VarChar,16),
                new SqlParameter("@startTime",SqlDbType.DateTime),
                new SqlParameter("@stopTime",SqlDbType.DateTime)
            };

            paras[0].Value = sMachineID.Replace("Machine", "");
            paras[1].Value = dStartTime.AddMinutes(-5);
            paras[2].Value = dStopTime.AddMinutes(5);

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null||ds.Tables.Count ==0)
            {
                return null;
            }

            return ds.Tables[0];
        }

        public DataTable GetTimeByStatus(DateTime dDateFrom, DateTime dDateTo, string sMachineID, params string[] sStatus)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select eventTrigger,starttime,stopTime from LMMSEventLog  ");
            strSql.Append(" where currentOperation = 'TECHNICIAN_OEE' ");
            strSql.Append(" and machineID = @machineID ");
            strSql.Append(" and startTime >= @dDateFrom ");
            strSql.Append(" and stopTime <= @dDateTo ");

            strSql.Append(" and eventTrigger in ( ");

            string strStatus = "";
            foreach (string  status in sStatus)
            {
                strStatus += "'" + status + "',";
            }


            strSql.Append(strStatus.Substring(0,strStatus.Length-1));

            strSql.Append(")");


            SqlParameter[] paras =
            {
                new SqlParameter("@machineID",SqlDbType.VarChar,16),
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime)
            };

            paras[0].Value = sMachineID;
            paras[1].Value = dDateFrom;
            paras[2].Value = dDateTo;

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }


        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select id
,machineID
,currentOperation
,case when eventTrigger = 'ADJUSTMENT' then 'BUYOFF' else eventTrigger end as eventTrigger
,startTime
,stopTime
from LMMSEventLog 
where (currentOperation = 'TECHNICIAN_OEE' or currentOperation = 'SYSTEM_OEE') and eventTrigger != 'POWER ON'
and datetime >= @DateFrom and datetime <= @DateTo");

            if (sMachineID != "")
            {
                strSql.Append(" and machineID = @machineID ");
            }

            if (sStatus != "" && sStatus != "RUNNING" && sStatus != "UTILIZATION")
            {
                strSql.Append(" and eventTrigger = @status ");
            }

            

            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom", SqlDbType.DateTime),
                new SqlParameter("@DateTo", SqlDbType.DateTime),
                new SqlParameter("@machineID", SqlDbType.VarChar,8),
                new SqlParameter("@status", SqlDbType.VarChar,32)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID; else paras[2] = null;
            if (sStatus != "") paras[3].Value = sStatus; else paras[3] = null;
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }





        #endregion
    }
}

