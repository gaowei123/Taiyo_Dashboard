 
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
    /// <summary>
    /// 数据访问类:PQCQaViDetailHistory_DAL
    /// </summary>
    public class PQCQaViDetailHistory_DAL
    {
        public PQCQaViDetailHistory_DAL()
        { }
        #region  Method



        /// <summary>p
        /// 增加一条数据
        /// </summary>
        public void Add(Common.Model.PQCQaViDetailHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCQaViDetailHistory(");
            strSql.Append("id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@jobid,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@color,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@totalQty,@totalPassQty,@totalRejectQty,@passQty,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@totalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@passQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", 
                "Class:PQCQaViDetailHistory_DAL", 
                "Function:		public void Add(Common.Model.PQCQaViDetailHistory_Model model)" + 
                "TableName:PQCQaViDetailHistory", ";id = " + (model.id == null ? "" : model.id.ToString()) + 
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) + 
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) + 
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) + 
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) + 
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) + 
                ";model = " + (model.model == null ? "" : model.model.ToString()) + 
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) + 
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) + 
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) + 
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) + 
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) + 
                ";day = " + (model.day == null ? "" : model.day.ToString()) + 
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) + 
                ";status = " + (model.status == null ? "" : model.status.ToString()) + 
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) + 
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) + 
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) + 
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) + 
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) + 
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) + 
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) + 
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) + 
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) + 
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) + 
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) + 
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) + 
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) + 
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) + 
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) + 
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) + 
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + 
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) + "");

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[18].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[19].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[20].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[21].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[22].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[23].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[24].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[25].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[26].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[27].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[28].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[29].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[30].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[31].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[32].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[33].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[34].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[35].Value = model.passQty == null ? (object)DBNull.Value : model.passQty;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[38].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[39].Value = model.color == null ? (object)DBNull.Value : model.color;

            DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Model.PQCQaViDetailHistory_Model model,SqlCommand cmd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCQaViDetailHistory(");
            strSql.Append("id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@jobid,@trackingID,@machineID,@dateTime,@materialPartNo,@jigNo,@model,@color,@cavityCount,@userName,@userID,@startTime,@stopTime,@day,@shift,@status,@remark_1,@remark_2,@totalQty,@totalPassQty,@totalRejectQty,@passQty,@rejectQty,@rejectQtyHour01,@rejectQtyHour02,@rejectQtyHour03,@rejectQtyHour04,@rejectQtyHour05,@rejectQtyHour06,@rejectQtyHour07,@rejectQtyHour08,@rejectQtyHour09,@rejectQtyHour10,@rejectQtyHour11,@rejectQtyHour12,@lastUpdatedTime,@remarks,@processes,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@totalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@passQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViDetailHistory_DAL",
                "Function:		public SqlCommand AddCommand(Common.Model.PQCQaViDetailHistory_Model model)" +
                "TableName:PQCQaViDetailHistory", ";id = " + (model.id == null ? "" : model.id.ToString()) +
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) +
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) +
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) +
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) +
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) +
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) +
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) +
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) +
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) +
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) +
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) +
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) + "");

            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[18].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[19].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[20].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[21].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[22].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[23].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[24].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[25].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[26].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[27].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[28].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[29].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[30].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[31].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[32].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[33].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[34].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[35].Value = model.passQty == null ? (object)DBNull.Value : model.passQty;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[38].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[39].Value = model.color == null ? (object)DBNull.Value : model.color;

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

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Model.PQCQaViDetailHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViDetailHistory set ");
            strSql.Append("id=@id,");
            strSql.Append("jobid=@jobid,");
            strSql.Append("trackingID=@trackingID,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("materialPartNo=@materialPartNo,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("model=@model,");
            strSql.Append("color=@color,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("stopTime=@stopTime,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("status=@status,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            strSql.Append("totalQty=@totalQty,");
            strSql.Append("totalPassQty=@totalPassQty,");
            strSql.Append("totalRejectQty=@totalRejectQty,");
            strSql.Append("passQty=@passQty,");
            strSql.Append("rejectQty=@rejectQty,");
            strSql.Append("rejectQtyHour01=@rejectQtyHour01,");
            strSql.Append("rejectQtyHour02=@rejectQtyHour02,");
            strSql.Append("rejectQtyHour03=@rejectQtyHour03,");
            strSql.Append("rejectQtyHour04=@rejectQtyHour04,");
            strSql.Append("rejectQtyHour05=@rejectQtyHour05,");
            strSql.Append("rejectQtyHour06=@rejectQtyHour06,");
            strSql.Append("rejectQtyHour07=@rejectQtyHour07,");
            strSql.Append("rejectQtyHour08=@rejectQtyHour08,");
            strSql.Append("rejectQtyHour09=@rejectQtyHour09,");
            strSql.Append("rejectQtyHour10=@rejectQtyHour10,");
            strSql.Append("rejectQtyHour11=@rejectQtyHour11,");
            strSql.Append("rejectQtyHour12=@rejectQtyHour12,");
            strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("processes=@processes,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@totalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@passQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[18].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[19].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[20].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[21].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[22].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[23].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[24].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[25].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[26].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[27].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[28].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[29].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[30].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[31].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[32].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[33].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[34].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[35].Value = model.passQty == null ? (object)DBNull.Value : model.passQty;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[38].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[39].Value = model.color == null ? (object)DBNull.Value : model.color;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", 
                "Class:PQCQaViDetailHistory_DAL", 
                "Function:		public bool Update(Common.Model.PQCQaViDetailHistory_Model model)" + 
                "TableName:PQCQaViDetailHistory", ";id = " + (model.id == null ? "" : model.id.ToString()) + 
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) + 
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) + 
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) + 
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) + 
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) + 
                ";model = " + (model.model == null ? "" : model.model.ToString()) + 
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) + 
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) + 
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) + 
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) + 
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) + 
                ";day = " + (model.day == null ? "" : model.day.ToString()) + 
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) + 
                ";status = " + (model.status == null ? "" : model.status.ToString()) + 
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) + 
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) + 
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) + 
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) + 
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) + 
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) + 
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) + 
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) + 
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) + 
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) + 
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) + 
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) + 
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) + 
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) + 
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) + 
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) + 
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + 
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) + "");

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        public SqlCommand UpdateCommand(Common.Model.PQCQaViDetailHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCQaViDetailHistory set ");
            strSql.Append("id=@id,");
            strSql.Append("jobid=@jobid,");
            strSql.Append("trackingID=@trackingID,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("materialPartNo=@materialPartNo,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("model=@model,");
            strSql.Append("color=@color,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("userName=@userName,");
            strSql.Append("userID=@userID,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("stopTime=@stopTime,");
            strSql.Append("day=@day,");
            strSql.Append("shift=@shift,");
            strSql.Append("status=@status,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            strSql.Append("totalQty=@totalQty,");
            strSql.Append("totalPassQty=@totalPassQty,");
            strSql.Append("totalRejectQty=@totalRejectQty,");
            strSql.Append("passQty=@passQty,");
            strSql.Append("rejectQty=@rejectQty,");
            strSql.Append("rejectQtyHour01=@rejectQtyHour01,");
            strSql.Append("rejectQtyHour02=@rejectQtyHour02,");
            strSql.Append("rejectQtyHour03=@rejectQtyHour03,");
            strSql.Append("rejectQtyHour04=@rejectQtyHour04,");
            strSql.Append("rejectQtyHour05=@rejectQtyHour05,");
            strSql.Append("rejectQtyHour06=@rejectQtyHour06,");
            strSql.Append("rejectQtyHour07=@rejectQtyHour07,");
            strSql.Append("rejectQtyHour08=@rejectQtyHour08,");
            strSql.Append("rejectQtyHour09=@rejectQtyHour09,");
            strSql.Append("rejectQtyHour10=@rejectQtyHour10,");
            strSql.Append("rejectQtyHour11=@rejectQtyHour11,");
            strSql.Append("rejectQtyHour12=@rejectQtyHour12,");
            strSql.Append("lastUpdatedTime=@lastUpdatedTime,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("processes=@processes,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@startTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@stopTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@rejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@rejectQtyHour01", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour02", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour03", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour04", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour05", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour06", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour07", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour08", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour09", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour10", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour11", SqlDbType.VarChar,50),
                    new SqlParameter("@rejectQtyHour12", SqlDbType.VarChar,50),
                    new SqlParameter("@lastUpdatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@processes", SqlDbType.VarChar,20),
                    new SqlParameter("@jobid", SqlDbType.VarChar,20),
                    new SqlParameter("@totalQty", SqlDbType.Decimal,9),
                    new SqlParameter("@passQty", SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@totalPassQty", SqlDbType.Decimal,9),
                    new SqlParameter("@totalRejectQty", SqlDbType.Decimal,9),
                    new SqlParameter("@color", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id;
            parameters[1].Value = model.trackingID == null ? (object)DBNull.Value : model.trackingID;
            parameters[2].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[3].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[4].Value = model.materialPartNo == null ? (object)DBNull.Value : model.materialPartNo;
            parameters[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[6].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[7].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[8].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[9].Value = model.userID == null ? (object)DBNull.Value : model.userID;
            parameters[10].Value = model.startTime == null ? (object)DBNull.Value : model.startTime;
            parameters[11].Value = model.stopTime == null ? (object)DBNull.Value : model.stopTime;
            parameters[12].Value = model.day == null ? (object)DBNull.Value : model.day;
            parameters[13].Value = model.shift == null ? (object)DBNull.Value : model.shift;
            parameters[14].Value = model.status == null ? (object)DBNull.Value : model.status;
            parameters[15].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[16].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[17].Value = model.rejectQty == null ? (object)DBNull.Value : model.rejectQty;
            parameters[18].Value = model.rejectQtyHour01 == null ? (object)DBNull.Value : model.rejectQtyHour01;
            parameters[19].Value = model.rejectQtyHour02 == null ? (object)DBNull.Value : model.rejectQtyHour02;
            parameters[20].Value = model.rejectQtyHour03 == null ? (object)DBNull.Value : model.rejectQtyHour03;
            parameters[21].Value = model.rejectQtyHour04 == null ? (object)DBNull.Value : model.rejectQtyHour04;
            parameters[22].Value = model.rejectQtyHour05 == null ? (object)DBNull.Value : model.rejectQtyHour05;
            parameters[23].Value = model.rejectQtyHour06 == null ? (object)DBNull.Value : model.rejectQtyHour06;
            parameters[24].Value = model.rejectQtyHour07 == null ? (object)DBNull.Value : model.rejectQtyHour07;
            parameters[25].Value = model.rejectQtyHour08 == null ? (object)DBNull.Value : model.rejectQtyHour08;
            parameters[26].Value = model.rejectQtyHour09 == null ? (object)DBNull.Value : model.rejectQtyHour09;
            parameters[27].Value = model.rejectQtyHour10 == null ? (object)DBNull.Value : model.rejectQtyHour10;
            parameters[28].Value = model.rejectQtyHour11 == null ? (object)DBNull.Value : model.rejectQtyHour11;
            parameters[29].Value = model.rejectQtyHour12 == null ? (object)DBNull.Value : model.rejectQtyHour12;
            parameters[30].Value = model.lastUpdatedTime == null ? (object)DBNull.Value : model.lastUpdatedTime;
            parameters[31].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[32].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[33].Value = model.jobid == null ? (object)DBNull.Value : model.jobid;
            parameters[34].Value = model.totalQty == null ? (object)DBNull.Value : model.totalQty;
            parameters[35].Value = model.passQty == null ? (object)DBNull.Value : model.passQty;
            parameters[36].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[37].Value = model.totalPassQty == null ? (object)DBNull.Value : model.totalPassQty;
            parameters[38].Value = model.totalRejectQty == null ? (object)DBNull.Value : model.totalRejectQty;
            parameters[39].Value = model.color == null ? (object)DBNull.Value : model.color;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCQaViDetailHistory_DAL",
                "Function:		public SqlCommand UpdateCommand(Common.Model.PQCQaViDetailHistory_Model model)" +
                "TableName:PQCQaViDetailHistory", ";id = " + (model.id == null ? "" : model.id.ToString()) +
                ";jobid = " + (model.jobid == null ? "" : model.jobid.ToString()) +
                ";trackingID = " + (model.trackingID == null ? "" : model.trackingID.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";materialPartNo = " + (model.materialPartNo == null ? "" : model.materialPartNo.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";userID = " + (model.userID == null ? "" : model.userID.ToString()) +
                ";startTime = " + (model.startTime == null ? "" : model.startTime.ToString()) +
                ";stopTime = " + (model.stopTime == null ? "" : model.stopTime.ToString()) +
                ";day = " + (model.day == null ? "" : model.day.ToString()) +
                ";shift = " + (model.shift == null ? "" : model.shift.ToString()) +
                ";status = " + (model.status == null ? "" : model.status.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";rejectQty = " + (model.rejectQty == null ? "" : model.rejectQty.ToString()) +
                ";rejectQtyHour01 = " + (model.rejectQtyHour01 == null ? "" : model.rejectQtyHour01.ToString()) +
                ";rejectQtyHour02 = " + (model.rejectQtyHour02 == null ? "" : model.rejectQtyHour02.ToString()) +
                ";rejectQtyHour03 = " + (model.rejectQtyHour03 == null ? "" : model.rejectQtyHour03.ToString()) +
                ";rejectQtyHour04 = " + (model.rejectQtyHour04 == null ? "" : model.rejectQtyHour04.ToString()) +
                ";rejectQtyHour05 = " + (model.rejectQtyHour05 == null ? "" : model.rejectQtyHour05.ToString()) +
                ";rejectQtyHour06 = " + (model.rejectQtyHour06 == null ? "" : model.rejectQtyHour06.ToString()) +
                ";rejectQtyHour07 = " + (model.rejectQtyHour07 == null ? "" : model.rejectQtyHour07.ToString()) +
                ";rejectQtyHour08 = " + (model.rejectQtyHour08 == null ? "" : model.rejectQtyHour08.ToString()) +
                ";rejectQtyHour09 = " + (model.rejectQtyHour09 == null ? "" : model.rejectQtyHour09.ToString()) +
                ";rejectQtyHour10 = " + (model.rejectQtyHour10 == null ? "" : model.rejectQtyHour10.ToString()) +
                ";rejectQtyHour11 = " + (model.rejectQtyHour11 == null ? "" : model.rejectQtyHour11.ToString()) +
                ";rejectQtyHour12 = " + (model.rejectQtyHour12 == null ? "" : model.rejectQtyHour12.ToString()) +
                ";lastUpdatedTime = " + (model.lastUpdatedTime == null ? "" : model.lastUpdatedTime.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) + "");

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCQaViDetailHistory ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCQaViDetailHistory_DAL", "Function:		public bool Delete()" + "TableName:PQCQaViDetailHistory", "");
            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        public SqlCommand DeleteCommand()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCQaViDetailHistory ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCQaViDetailHistory_DAL", "Function:		public SqlCommand DeleteCommand()" + "TableName:PQCQaViDetailHistory", "");
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public SqlCommand DeleteAllCommand()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PQCQaViDetailHistory ");
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCQaViDetailHistory_DAL", "Function:		public SqlCommand DeleteAllCommand( )" + "TableName:PQCQaViDetailHistory", "");
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Model.PQCQaViDetailHistory_Model GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime from PQCQaViDetailHistory ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.DAL", "Class:PQCQaViDetailHistory_DAL", "Function:		public Common.Model.PQCQaViDetailHistory_Model GetModel()" + "TableName:PQCQaViDetailHistory", "");
            Common.Model.PQCQaViDetailHistory_Model model = new Common.Model.PQCQaViDetailHistory_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.jobid = ds.Tables[0].Rows[0]["jobid"].ToString();
                model.trackingID = ds.Tables[0].Rows[0]["trackingID"].ToString();
                model.machineID = ds.Tables[0].Rows[0]["machineID"].ToString();
                if (ds.Tables[0].Rows[0]["dateTime"].ToString() != "")
                {
                    model.dateTime = DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
                }
                model.materialPartNo = ds.Tables[0].Rows[0]["materialPartNo"].ToString();
                model.jigNo = ds.Tables[0].Rows[0]["jigNo"].ToString();
                model.model = ds.Tables[0].Rows[0]["model"].ToString();
                model.color = ds.Tables[0].Rows[0]["color"].ToString();
                if (ds.Tables[0].Rows[0]["cavityCount"].ToString() != "")
                {
                    model.cavityCount = decimal.Parse(ds.Tables[0].Rows[0]["cavityCount"].ToString());
                }
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                model.userID = ds.Tables[0].Rows[0]["userID"].ToString();
                if (ds.Tables[0].Rows[0]["startTime"].ToString() != "")
                {
                    model.startTime = DateTime.Parse(ds.Tables[0].Rows[0]["startTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["stopTime"].ToString() != "")
                {
                    model.stopTime = DateTime.Parse(ds.Tables[0].Rows[0]["stopTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["day"].ToString() != "")
                {
                    model.day = DateTime.Parse(ds.Tables[0].Rows[0]["day"].ToString());
                }
                model.shift = ds.Tables[0].Rows[0]["shift"].ToString();
                model.status = ds.Tables[0].Rows[0]["status"].ToString();
                model.remark_1 = ds.Tables[0].Rows[0]["remark_1"].ToString();
                model.remark_2 = ds.Tables[0].Rows[0]["remark_2"].ToString();
                if (ds.Tables[0].Rows[0]["totalQty"].ToString() != "")
                {
                    model.totalQty = decimal.Parse(ds.Tables[0].Rows[0]["totalQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalPassQty"].ToString() != "")
                {
                    model.totalPassQty = decimal.Parse(ds.Tables[0].Rows[0]["totalPassQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalRejectQty"].ToString() != "")
                {
                    model.totalRejectQty = decimal.Parse(ds.Tables[0].Rows[0]["totalRejectQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["passQty"].ToString() != "")
                {
                    model.passQty = decimal.Parse(ds.Tables[0].Rows[0]["passQty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rejectQty"].ToString() != "")
                {
                    model.rejectQty = decimal.Parse(ds.Tables[0].Rows[0]["rejectQty"].ToString());
                }
                model.rejectQtyHour01 = ds.Tables[0].Rows[0]["rejectQtyHour01"].ToString();
                model.rejectQtyHour02 = ds.Tables[0].Rows[0]["rejectQtyHour02"].ToString();
                model.rejectQtyHour03 = ds.Tables[0].Rows[0]["rejectQtyHour03"].ToString();
                model.rejectQtyHour04 = ds.Tables[0].Rows[0]["rejectQtyHour04"].ToString();
                model.rejectQtyHour05 = ds.Tables[0].Rows[0]["rejectQtyHour05"].ToString();
                model.rejectQtyHour06 = ds.Tables[0].Rows[0]["rejectQtyHour06"].ToString();
                model.rejectQtyHour07 = ds.Tables[0].Rows[0]["rejectQtyHour07"].ToString();
                model.rejectQtyHour08 = ds.Tables[0].Rows[0]["rejectQtyHour08"].ToString();
                model.rejectQtyHour09 = ds.Tables[0].Rows[0]["rejectQtyHour09"].ToString();
                model.rejectQtyHour10 = ds.Tables[0].Rows[0]["rejectQtyHour10"].ToString();
                model.rejectQtyHour11 = ds.Tables[0].Rows[0]["rejectQtyHour11"].ToString();
                model.rejectQtyHour12 = ds.Tables[0].Rows[0]["rejectQtyHour12"].ToString();
                if (ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString() != "")
                {
                    model.lastUpdatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["lastUpdatedTime"].ToString());
                }
                model.remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
                model.processes = ds.Tables[0].Rows[0]["processes"].ToString();
                if (ds.Tables[0].Rows[0]["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["updatedTime"].ToString());
                }
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime ");
            strSql.Append(" FROM PQCQaViDetailHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelp.SqlDB.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,jobid,trackingID,machineID,dateTime,materialPartNo,jigNo,model,color,cavityCount,userName,userID,startTime,stopTime,day,shift,status,remark_1,remark_2,totalQty,totalPassQty,totalRejectQty,passQty,rejectQty,rejectQtyHour01,rejectQtyHour02,rejectQtyHour03,rejectQtyHour04,rejectQtyHour05,rejectQtyHour06,rejectQtyHour07,rejectQtyHour08,rejectQtyHour09,rejectQtyHour10,rejectQtyHour11,rejectQtyHour12,lastUpdatedTime,remarks,processes,updatedTime ");
            strSql.Append(" FROM PQCQaViDetailHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
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
			parameters[0].Value = "PQCQaViDetailHistory";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  Method
    }
}

