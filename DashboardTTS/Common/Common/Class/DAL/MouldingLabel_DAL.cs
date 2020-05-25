using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
    /// <summary>
    /// 数据访问类:MouldingLabel_DAL
    /// </summary>
    public class MouldingLabel_DAL 
    {
        public MouldingLabel_DAL()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public DataSet Exists(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MouldingLabel");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }

        public DataSet ExistsbytrackingID(string trackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MouldingLabel");
            strSql.Append(" where ");
            strSql.Append("trackingID=@trackingID");
            SqlParameter[] parameters = {
                new SqlParameter("@trackingID",SqlDbType.VarChar,50)
};
            parameters[0].Value = trackingID == null ? null : trackingID;

            return DBHelp.SqlDB.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Common.Model.MouldingLabel_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingLabel(");
            strSql.Append("trackingID,machineID,UsageQTY01,UsageQTY02,UsageQTY03,UsageQTY04,UsageQTY05,UsageQTY06,UsageQTY07,UsageQTY08,UsageQTY09,UsageQTY10,UsageQTY11,UsageQTY12,RejectQTY01,RejectQTY02,RejectQTY03,RejectQTY04,RejectQTY05,RejectQTY06,RejectQTY07,RejectQTY08,RejectQTY09,RejectQTY10,RejectQTY11,RejectQTY12,SerialNo01,SerialNo02,SerialNo03,SerialNo04,SerialNo05,SerialNo06,SerialNo07,SerialNo08,SerialNo09,SerialNo10,SerialNo11,SerialNo12,SerialNoEnd,SerialNo)");
            strSql.Append(" values (");
            strSql.Append("@trackingID,@machineID,@UsageQTY01,@UsageQTY02,@UsageQTY03,@UsageQTY04,@UsageQTY05,@UsageQTY06,@UsageQTY07,@UsageQTY08,@UsageQTY09,@UsageQTY10,@UsageQTY11,@UsageQTY12,@RejectQTY01,@RejectQTY02,@RejectQTY03,@RejectQTY04,@RejectQTY05,@RejectQTY06,@RejectQTY07,@RejectQTY08,@RejectQTY09,@RejectQTY10,@RejectQTY11,@RejectQTY12,@SerialNo01,@SerialNo02,@SerialNo03,@SerialNo04,@SerialNo05,@SerialNo06,@SerialNo07,@SerialNo08,@SerialNo09,@SerialNo10,@SerialNo11,@SerialNo12,@SerialNoEnd,@SerialNo)");
            SqlParameter[] parameters = {
                    new SqlParameter("@trackingID",SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@UsageQTY01", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY02", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY03", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY04", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY05", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY06", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY07", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY08", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY09", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY10", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY11", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY12", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY01", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY02", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY03", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY04", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY05", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY06", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY07", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY08", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY09", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY10", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY11", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY12", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo01", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo02", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo03", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo04", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo05", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo06", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo07", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo08", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo09", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo10", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo11", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo12", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNoEnd", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo", SqlDbType.VarChar,50)};
            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.SQLServerDAL", "Class:MouldingLabel_DAL", "Function:		public void Add(Common.Model.MouldingLabel_Model model)" + "TableName:MouldingLabel", ";trackingID = " + model.trackingID.ToString() + ";machineID = " + model.machineID.ToString() + ";UsageQTY01 = " + model.UsageQTY01.ToString() + ";UsageQTY02 = " + model.UsageQTY02.ToString() + ";UsageQTY03 = " + model.UsageQTY03.ToString() + ";UsageQTY04 = " + model.UsageQTY04.ToString() + ";UsageQTY05 = " + model.UsageQTY05.ToString() + ";UsageQTY06 = " + model.UsageQTY06.ToString() + ";UsageQTY07 = " + model.UsageQTY07.ToString() + ";UsageQTY08 = " + model.UsageQTY08.ToString() + ";UsageQTY09 = " + model.UsageQTY09.ToString() + ";UsageQTY10 = " + model.UsageQTY10.ToString() + ";UsageQTY11 = " + model.UsageQTY11.ToString() + ";UsageQTY12 = " + model.UsageQTY12.ToString() + ";RejectQTY01 = " + model.RejectQTY01.ToString() + ";RejectQTY02 = " + model.RejectQTY02.ToString() + ";RejectQTY03 = " + model.RejectQTY03.ToString() + ";RejectQTY04 = " + model.RejectQTY04.ToString() + ";RejectQTY05 = " + model.RejectQTY05.ToString() + ";RejectQTY06 = " + model.RejectQTY06.ToString() + ";RejectQTY07 = " + model.RejectQTY07.ToString() + ";RejectQTY08 = " + model.RejectQTY08.ToString() + ";RejectQTY09 = " + model.RejectQTY09.ToString() + ";RejectQTY10 = " + model.RejectQTY10.ToString() + ";RejectQTY11 = " + model.RejectQTY11.ToString() + ";RejectQTY12 = " + model.RejectQTY12.ToString() + ";SerialNo01 = " + model.SerialNo01.ToString() + ";SerialNo02 = " + model.SerialNo02.ToString() + ";SerialNo03 = " + model.SerialNo03.ToString() + ";SerialNo04 = " + model.SerialNo04.ToString() + ";SerialNo05 = " + model.SerialNo05.ToString() + ";SerialNo06 = " + model.SerialNo06.ToString() + ";SerialNo07 = " + model.SerialNo07.ToString() + ";SerialNo08 = " + model.SerialNo08.ToString() + ";SerialNo09 = " + model.SerialNo09.ToString() + ";SerialNo10 = " + model.SerialNo10.ToString() + ";SerialNo11 = " + model.SerialNo11.ToString() + ";SerialNo12 = " + model.SerialNo12.ToString() + ";SerialNoEnd = " + model.SerialNoEnd.ToString() + ";SerialNo = " + model.SerialNo.ToString() + "");
            parameters[0].Value = model.trackingID == null ? null : model.trackingID;
            parameters[1].Value = model.machineID == null ? null : model.machineID;
            parameters[2].Value = model.UsageQTY01 == null ? 0 : model.UsageQTY01;
            parameters[3].Value = model.UsageQTY02 == null ? 0 : model.UsageQTY02;
            parameters[4].Value = model.UsageQTY03 == null ? 0 : model.UsageQTY03;
            parameters[5].Value = model.UsageQTY04 == null ? 0 : model.UsageQTY04;
            parameters[6].Value = model.UsageQTY05 == null ? 0 : model.UsageQTY05;
            parameters[7].Value = model.UsageQTY06 == null ? 0 : model.UsageQTY06;
            parameters[8].Value = model.UsageQTY07 == null ? 0 : model.UsageQTY07;
            parameters[9].Value = model.UsageQTY08 == null ? 0 : model.UsageQTY08;
            parameters[10].Value = model.UsageQTY09 == null ? 0 : model.UsageQTY09;
            parameters[11].Value = model.UsageQTY10 == null ? 0 : model.UsageQTY10;
            parameters[12].Value = model.UsageQTY11 == null ? 0 : model.UsageQTY11;
            parameters[13].Value = model.UsageQTY12 == null ? 0 : model.UsageQTY12;
            parameters[14].Value = model.RejectQTY01 == null ? 0 : model.RejectQTY01;
            parameters[15].Value = model.RejectQTY02 == null ? 0 : model.RejectQTY02;
            parameters[16].Value = model.RejectQTY03 == null ? 0 : model.RejectQTY03;
            parameters[17].Value = model.RejectQTY04 == null ? 0 : model.RejectQTY04;
            parameters[18].Value = model.RejectQTY05 == null ? 0 : model.RejectQTY05;
            parameters[19].Value = model.RejectQTY06 == null ? 0 : model.RejectQTY06;
            parameters[20].Value = model.RejectQTY07 == null ? 0 : model.RejectQTY07;
            parameters[21].Value = model.RejectQTY08 == null ? 0 : model.RejectQTY08;
            parameters[22].Value = model.RejectQTY09 == null ? 0 : model.RejectQTY09;
            parameters[23].Value = model.RejectQTY10 == null ? 0 : model.RejectQTY10;
            parameters[24].Value = model.RejectQTY11 == null ? 0 : model.RejectQTY11;
            parameters[25].Value = model.RejectQTY12 == null ? 0 : model.RejectQTY12;
            parameters[26].Value = model.SerialNo01 == null ? 0 : model.SerialNo01;
            parameters[27].Value = model.SerialNo02 == null ? 0 : model.SerialNo02;
            parameters[28].Value = model.SerialNo03 == null ? 0 : model.SerialNo03;
            parameters[29].Value = model.SerialNo04 == null ? 0 : model.SerialNo04;
            parameters[30].Value = model.SerialNo05 == null ? 0 : model.SerialNo05;
            parameters[31].Value = model.SerialNo06 == null ? 0 : model.SerialNo06;
            parameters[32].Value = model.SerialNo07 == null ? 0 : model.SerialNo07;
            parameters[33].Value = model.SerialNo08 == null ? 0 : model.SerialNo08;
            parameters[34].Value = model.SerialNo09 == null ? 0 : model.SerialNo09;
            parameters[35].Value = model.SerialNo10 == null ? 0 : model.SerialNo10;
            parameters[36].Value = model.SerialNo11 == null ? 0 : model.SerialNo11;
            parameters[37].Value = model.SerialNo12 == null ? 0 : model.SerialNo12;
            parameters[38].Value = model.SerialNoEnd == null ? 0 : model.SerialNoEnd;
            parameters[39].Value = model.SerialNo == null ? null : model.SerialNo;

            DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Model.MouldingLabel_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingLabel set ");
            strSql.Append("trackingID=@trackingID,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("UsageQTY01=@UsageQTY01,");
            strSql.Append("UsageQTY02=@UsageQTY02,");
            strSql.Append("UsageQTY03=@UsageQTY03,");
            strSql.Append("UsageQTY04=@UsageQTY04,");
            strSql.Append("UsageQTY05=@UsageQTY05,");
            strSql.Append("UsageQTY06=@UsageQTY06,");
            strSql.Append("UsageQTY07=@UsageQTY07,");
            strSql.Append("UsageQTY08=@UsageQTY08,");
            strSql.Append("UsageQTY09=@UsageQTY09,");
            strSql.Append("UsageQTY10=@UsageQTY10,");
            strSql.Append("UsageQTY11=@UsageQTY11,");
            strSql.Append("UsageQTY12=@UsageQTY12,");
            strSql.Append("RejectQTY01=@RejectQTY01,");
            strSql.Append("RejectQTY02=@RejectQTY02,");
            strSql.Append("RejectQTY03=@RejectQTY03,");
            strSql.Append("RejectQTY04=@RejectQTY04,");
            strSql.Append("RejectQTY05=@RejectQTY05,");
            strSql.Append("RejectQTY06=@RejectQTY06,");
            strSql.Append("RejectQTY07=@RejectQTY07,");
            strSql.Append("RejectQTY08=@RejectQTY08,");
            strSql.Append("RejectQTY09=@RejectQTY09,");
            strSql.Append("RejectQTY10=@RejectQTY10,");
            strSql.Append("RejectQTY11=@RejectQTY11,");
            strSql.Append("RejectQTY12=@RejectQTY12,");
            strSql.Append("SerialNo01=@SerialNo01,");
            strSql.Append("SerialNo02=@SerialNo02,");
            strSql.Append("SerialNo03=@SerialNo03,");
            strSql.Append("SerialNo04=@SerialNo04,");
            strSql.Append("SerialNo05=@SerialNo05,");
            strSql.Append("SerialNo06=@SerialNo06,");
            strSql.Append("SerialNo07=@SerialNo07,");
            strSql.Append("SerialNo08=@SerialNo08,");
            strSql.Append("SerialNo09=@SerialNo09,");
            strSql.Append("SerialNo10=@SerialNo10,");
            strSql.Append("SerialNo11=@SerialNo11,");
            strSql.Append("SerialNo12=@SerialNo12,");
            strSql.Append("SerialNoEnd=@SerialNoEnd,");
            strSql.Append("SerialNo=@SerialNo");
            strSql.Append(" where ");
            strSql.Append("trackingID=@trackingID");
            SqlParameter[] parameters = {
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@UsageQTY01", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY02", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY03", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY04", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY05", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY06", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY07", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY08", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY09", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY10", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY11", SqlDbType.Decimal,9),
                    new SqlParameter("@UsageQTY12", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY01", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY02", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY03", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY04", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY05", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY06", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY07", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY08", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY09", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY10", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY11", SqlDbType.Decimal,9),
                    new SqlParameter("@RejectQTY12", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo01", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo02", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo03", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo04", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo05", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo06", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo07", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo08", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo09", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo10", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo11", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo12", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNoEnd", SqlDbType.Decimal,9),
                    new SqlParameter("@SerialNo", SqlDbType.VarChar,50)};
            parameters[0].Value = model.trackingID == null ? null : model.trackingID;
            parameters[1].Value = model.machineID == null ? null : model.machineID;
            parameters[2].Value = model.UsageQTY01 == null ? 0 : model.UsageQTY01;
            parameters[3].Value = model.UsageQTY02 == null ? 0 : model.UsageQTY02;
            parameters[4].Value = model.UsageQTY03 == null ? 0 : model.UsageQTY03;
            parameters[5].Value = model.UsageQTY04 == null ? 0 : model.UsageQTY04;
            parameters[6].Value = model.UsageQTY05 == null ? 0 : model.UsageQTY05;
            parameters[7].Value = model.UsageQTY06 == null ? 0 : model.UsageQTY06;
            parameters[8].Value = model.UsageQTY07 == null ? 0 : model.UsageQTY07;
            parameters[9].Value = model.UsageQTY08 == null ? 0 : model.UsageQTY08;
            parameters[10].Value = model.UsageQTY09 == null ? 0 : model.UsageQTY09;
            parameters[11].Value = model.UsageQTY10 == null ? 0 : model.UsageQTY10;
            parameters[12].Value = model.UsageQTY11 == null ? 0 : model.UsageQTY11;
            parameters[13].Value = model.UsageQTY12 == null ? 0 : model.UsageQTY12;
            parameters[14].Value = model.RejectQTY01 == null ? 0 : model.RejectQTY01;
            parameters[15].Value = model.RejectQTY02 == null ? 0 : model.RejectQTY02;
            parameters[16].Value = model.RejectQTY03 == null ? 0 : model.RejectQTY03;
            parameters[17].Value = model.RejectQTY04 == null ? 0 : model.RejectQTY04;
            parameters[18].Value = model.RejectQTY05 == null ? 0 : model.RejectQTY05;
            parameters[19].Value = model.RejectQTY06 == null ? 0 : model.RejectQTY06;
            parameters[20].Value = model.RejectQTY07 == null ? 0 : model.RejectQTY07;
            parameters[21].Value = model.RejectQTY08 == null ? 0 : model.RejectQTY08;
            parameters[22].Value = model.RejectQTY09 == null ? 0 : model.RejectQTY09;
            parameters[23].Value = model.RejectQTY10 == null ? 0 : model.RejectQTY10;
            parameters[24].Value = model.RejectQTY11 == null ? 0 : model.RejectQTY11;
            parameters[25].Value = model.RejectQTY12 == null ? 0 : model.RejectQTY12;
            parameters[26].Value = model.SerialNo01 == null ? 0 : model.SerialNo01;
            parameters[27].Value = model.SerialNo02 == null ? 0 : model.SerialNo02;
            parameters[28].Value = model.SerialNo03 == null ? 0 : model.SerialNo03;
            parameters[29].Value = model.SerialNo04 == null ? 0 : model.SerialNo04;
            parameters[30].Value = model.SerialNo05 == null ? 0 : model.SerialNo05;
            parameters[31].Value = model.SerialNo06 == null ? 0 : model.SerialNo06;
            parameters[32].Value = model.SerialNo07 == null ? 0 : model.SerialNo07;
            parameters[33].Value = model.SerialNo08 == null ? 0 : model.SerialNo08;
            parameters[34].Value = model.SerialNo09 == null ? 0 : model.SerialNo09;
            parameters[35].Value = model.SerialNo10 == null ? 0 : model.SerialNo10;
            parameters[36].Value = model.SerialNo11 == null ? 0 : model.SerialNo11;
            parameters[37].Value = model.SerialNo12 == null ? 0 : model.SerialNo12;
            parameters[38].Value = model.SerialNoEnd == null ? 0 : model.SerialNoEnd;
            parameters[39].Value = model.SerialNo == null ? null : model.SerialNo;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.SQLServerDAL", "Class:MouldingLabel_DAL", "Function:		public bool Update(Common.Model.MouldingLabel_Model model)" + "TableName:MouldingLabel", ";trackingID = " + model.trackingID.ToString() + ";machineID = " + model.machineID.ToString() + ";UsageQTY01 = " + model.UsageQTY01.ToString() + ";UsageQTY02 = " + model.UsageQTY02.ToString() + ";UsageQTY03 = " + model.UsageQTY03.ToString() + ";UsageQTY04 = " + model.UsageQTY04.ToString() + ";UsageQTY05 = " + model.UsageQTY05.ToString() + ";UsageQTY06 = " + model.UsageQTY06.ToString() + ";UsageQTY07 = " + model.UsageQTY07.ToString() + ";UsageQTY08 = " + model.UsageQTY08.ToString() + ";UsageQTY09 = " + model.UsageQTY09.ToString() + ";UsageQTY10 = " + model.UsageQTY10.ToString() + ";UsageQTY11 = " + model.UsageQTY11.ToString() + ";UsageQTY12 = " + model.UsageQTY12.ToString() + ";RejectQTY01 = " + model.RejectQTY01.ToString() + ";RejectQTY02 = " + model.RejectQTY02.ToString() + ";RejectQTY03 = " + model.RejectQTY03.ToString() + ";RejectQTY04 = " + model.RejectQTY04.ToString() + ";RejectQTY05 = " + model.RejectQTY05.ToString() + ";RejectQTY06 = " + model.RejectQTY06.ToString() + ";RejectQTY07 = " + model.RejectQTY07.ToString() + ";RejectQTY08 = " + model.RejectQTY08.ToString() + ";RejectQTY09 = " + model.RejectQTY09.ToString() + ";RejectQTY10 = " + model.RejectQTY10.ToString() + ";RejectQTY11 = " + model.RejectQTY11.ToString() + ";RejectQTY12 = " + model.RejectQTY12.ToString() + ";SerialNo01 = " + model.SerialNo01.ToString() + ";SerialNo02 = " + model.SerialNo02.ToString() + ";SerialNo03 = " + model.SerialNo03.ToString() + ";SerialNo04 = " + model.SerialNo04.ToString() + ";SerialNo05 = " + model.SerialNo05.ToString() + ";SerialNo06 = " + model.SerialNo06.ToString() + ";SerialNo07 = " + model.SerialNo07.ToString() + ";SerialNo08 = " + model.SerialNo08.ToString() + ";SerialNo09 = " + model.SerialNo09.ToString() + ";SerialNo10 = " + model.SerialNo10.ToString() + ";SerialNo11 = " + model.SerialNo11.ToString() + ";SerialNo12 = " + model.SerialNo12.ToString() + ";SerialNoEnd = " + model.SerialNoEnd.ToString() + ";SerialNo = " + model.SerialNo.ToString() + "");
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
        public bool Delete(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MouldingLabel ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.SQLServerDAL", "Class:MouldingLabel_DAL", "Function:		public bool Delete(string trackingID,string machineID,decimal UsageQTY01,decimal UsageQTY02,decimal UsageQTY03,decimal UsageQTY04,decimal UsageQTY05,decimal UsageQTY06,decimal UsageQTY07,decimal UsageQTY08,decimal UsageQTY09,decimal UsageQTY10,decimal UsageQTY11,decimal UsageQTY12,decimal RejectQTY01,decimal RejectQTY02,decimal RejectQTY03,decimal RejectQTY04,decimal RejectQTY05,decimal RejectQTY06,decimal RejectQTY07,decimal RejectQTY08,decimal RejectQTY09,decimal RejectQTY10,decimal RejectQTY11,decimal RejectQTY12,decimal SerialNo01,decimal SerialNo02,decimal SerialNo03,decimal SerialNo04,decimal SerialNo05,decimal SerialNo06,decimal SerialNo07,decimal SerialNo08,decimal SerialNo09,decimal SerialNo10,decimal SerialNo11,decimal SerialNo12,decimal SerialNoEnd,string SerialNo)" + "TableName:MouldingLabel", "");
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
        /// 得到一个对象实体
        /// </summary>
        public Common.Model.MouldingLabel_Model GetModel(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 trackingID,machineID,UsageQTY01,UsageQTY02,UsageQTY03,UsageQTY04,UsageQTY05,UsageQTY06,UsageQTY07,UsageQTY08,UsageQTY09,UsageQTY10,UsageQTY11,UsageQTY12,RejectQTY01,RejectQTY02,RejectQTY03,RejectQTY04,RejectQTY05,RejectQTY06,RejectQTY07,RejectQTY08,RejectQTY09,RejectQTY10,RejectQTY11,RejectQTY12,SerialNo01,SerialNo02,SerialNo03,SerialNo04,SerialNo05,SerialNo06,SerialNo07,SerialNo08,SerialNo09,SerialNo10,SerialNo11,SerialNo12,SerialNoEnd,SerialNo from MouldingLabel ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.SQLServerDAL", "Class:MouldingLabel_DAL", "Function:		public Common.Model.MouldingLabel_Model GetModel(string trackingID,string machineID,decimal UsageQTY01,decimal UsageQTY02,decimal UsageQTY03,decimal UsageQTY04,decimal UsageQTY05,decimal UsageQTY06,decimal UsageQTY07,decimal UsageQTY08,decimal UsageQTY09,decimal UsageQTY10,decimal UsageQTY11,decimal UsageQTY12,decimal RejectQTY01,decimal RejectQTY02,decimal RejectQTY03,decimal RejectQTY04,decimal RejectQTY05,decimal RejectQTY06,decimal RejectQTY07,decimal RejectQTY08,decimal RejectQTY09,decimal RejectQTY10,decimal RejectQTY11,decimal RejectQTY12,decimal SerialNo01,decimal SerialNo02,decimal SerialNo03,decimal SerialNo04,decimal SerialNo05,decimal SerialNo06,decimal SerialNo07,decimal SerialNo08,decimal SerialNo09,decimal SerialNo10,decimal SerialNo11,decimal SerialNo12,decimal SerialNoEnd,string SerialNo)" + "TableName:MouldingLabel", "");
            Common.Model.MouldingLabel_Model model = new Common.Model.MouldingLabel_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.trackingID = ds.Tables[0].Rows[0]["trackingID"].ToString();
                model.machineID = ds.Tables[0].Rows[0]["machineID"].ToString();
                if (ds.Tables[0].Rows[0]["UsageQTY01"].ToString() != "")
                {
                    model.UsageQTY01 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY01"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY02"].ToString() != "")
                {
                    model.UsageQTY02 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY02"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY03"].ToString() != "")
                {
                    model.UsageQTY03 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY03"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY04"].ToString() != "")
                {
                    model.UsageQTY04 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY04"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY05"].ToString() != "")
                {
                    model.UsageQTY05 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY05"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY06"].ToString() != "")
                {
                    model.UsageQTY06 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY06"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY07"].ToString() != "")
                {
                    model.UsageQTY07 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY07"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY08"].ToString() != "")
                {
                    model.UsageQTY08 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY08"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY09"].ToString() != "")
                {
                    model.UsageQTY09 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY09"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY10"].ToString() != "")
                {
                    model.UsageQTY10 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY10"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY11"].ToString() != "")
                {
                    model.UsageQTY11 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY11"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsageQTY12"].ToString() != "")
                {
                    model.UsageQTY12 = decimal.Parse(ds.Tables[0].Rows[0]["UsageQTY12"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY01"].ToString() != "")
                {
                    model.RejectQTY01 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY01"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY02"].ToString() != "")
                {
                    model.RejectQTY02 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY02"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY03"].ToString() != "")
                {
                    model.RejectQTY03 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY03"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY04"].ToString() != "")
                {
                    model.RejectQTY04 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY04"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY05"].ToString() != "")
                {
                    model.RejectQTY05 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY05"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY06"].ToString() != "")
                {
                    model.RejectQTY06 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY06"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY07"].ToString() != "")
                {
                    model.RejectQTY07 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY07"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY08"].ToString() != "")
                {
                    model.RejectQTY08 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY08"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY09"].ToString() != "")
                {
                    model.RejectQTY09 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY09"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY10"].ToString() != "")
                {
                    model.RejectQTY10 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY10"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY11"].ToString() != "")
                {
                    model.RejectQTY11 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY11"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RejectQTY12"].ToString() != "")
                {
                    model.RejectQTY12 = decimal.Parse(ds.Tables[0].Rows[0]["RejectQTY12"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo01"].ToString() != "")
                {
                    model.SerialNo01 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo01"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo02"].ToString() != "")
                {
                    model.SerialNo02 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo02"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo03"].ToString() != "")
                {
                    model.SerialNo03 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo03"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo04"].ToString() != "")
                {
                    model.SerialNo04 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo04"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo05"].ToString() != "")
                {
                    model.SerialNo05 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo05"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo06"].ToString() != "")
                {
                    model.SerialNo06 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo06"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo07"].ToString() != "")
                {
                    model.SerialNo07 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo07"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo08"].ToString() != "")
                {
                    model.SerialNo08 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo08"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo09"].ToString() != "")
                {
                    model.SerialNo09 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo09"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo10"].ToString() != "")
                {
                    model.SerialNo10 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo10"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo11"].ToString() != "")
                {
                    model.SerialNo11 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo11"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNo12"].ToString() != "")
                {
                    model.SerialNo12 = decimal.Parse(ds.Tables[0].Rows[0]["SerialNo12"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SerialNoEnd"].ToString() != "")
                {
                    model.SerialNoEnd = decimal.Parse(ds.Tables[0].Rows[0]["SerialNoEnd"].ToString());
                }
                model.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
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
            strSql.Append("select trackingID,machineID,UsageQTY01,UsageQTY02,UsageQTY03,UsageQTY04,UsageQTY05,UsageQTY06,UsageQTY07,UsageQTY08,UsageQTY09,UsageQTY10,UsageQTY11,UsageQTY12,RejectQTY01,RejectQTY02,RejectQTY03,RejectQTY04,RejectQTY05,RejectQTY06,RejectQTY07,RejectQTY08,RejectQTY09,RejectQTY10,RejectQTY11,RejectQTY12,SerialNo01,SerialNo02,SerialNo03,SerialNo04,SerialNo05,SerialNo06,SerialNo07,SerialNo08,SerialNo09,SerialNo10,SerialNo11,SerialNo12,SerialNoEnd,SerialNo ");
            strSql.Append(" FROM MouldingLabel ");
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
            strSql.Append(" trackingID,machineID,UsageQTY01,UsageQTY02,UsageQTY03,UsageQTY04,UsageQTY05,UsageQTY06,UsageQTY07,UsageQTY08,UsageQTY09,UsageQTY10,UsageQTY11,UsageQTY12,RejectQTY01,RejectQTY02,RejectQTY03,RejectQTY04,RejectQTY05,RejectQTY06,RejectQTY07,RejectQTY08,RejectQTY09,RejectQTY10,RejectQTY11,RejectQTY12,SerialNo01,SerialNo02,SerialNo03,SerialNo04,SerialNo05,SerialNo06,SerialNo07,SerialNo08,SerialNo09,SerialNo10,SerialNo11,SerialNo12,SerialNoEnd,SerialNo ");
            strSql.Append(" FROM MouldingLabel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DBHelp.SqlDB.Query(strSql.ToString());
        }

        internal DataSet GetModel_ByTrackingID(string trackingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select trackingID,machineID,UsageQTY01,UsageQTY02,UsageQTY03,UsageQTY04,UsageQTY05,UsageQTY06,UsageQTY07,UsageQTY08,UsageQTY09,UsageQTY10,UsageQTY11,UsageQTY12,RejectQTY01,RejectQTY02,RejectQTY03,RejectQTY04,RejectQTY05,RejectQTY06,RejectQTY07,RejectQTY08,RejectQTY09,RejectQTY10,RejectQTY11,RejectQTY12,SerialNo01,SerialNo02,SerialNo03,SerialNo04,SerialNo05,SerialNo06,SerialNo07,SerialNo08,SerialNo09,SerialNo10,SerialNo11,SerialNo12,SerialNoEnd,SerialNo ");
            strSql.Append(" FROM MouldingLabel ");
            strSql.Append(" where trackingID =@trackingID");
            SqlParameter[] parameters = {
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50)
            };
            parameters[0].Value = trackingID;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.SQLServerDAL", "Class:MouldingLabel_DAL", "Function:		public Common.Model.MouldingLabel_Model GetModel(string trackingID,string machineID,decimal UsageQTY01,decimal UsageQTY02,decimal UsageQTY03,decimal UsageQTY04,decimal UsageQTY05,decimal UsageQTY06,decimal UsageQTY07,decimal UsageQTY08,decimal UsageQTY09,decimal UsageQTY10,decimal UsageQTY11,decimal UsageQTY12,decimal RejectQTY01,decimal RejectQTY02,decimal RejectQTY03,decimal RejectQTY04,decimal RejectQTY05,decimal RejectQTY06,decimal RejectQTY07,decimal RejectQTY08,decimal RejectQTY09,decimal RejectQTY10,decimal RejectQTY11,decimal RejectQTY12,decimal SerialNo01,decimal SerialNo02,decimal SerialNo03,decimal SerialNo04,decimal SerialNo05,decimal SerialNo06,decimal SerialNo07,decimal SerialNo08,decimal SerialNo09,decimal SerialNo10,decimal SerialNo11,decimal SerialNo12,decimal SerialNoEnd,string SerialNo)" + "TableName:MouldingLabel", "");
            //Common.Model.MouldingViTracking_Model model = new Common.Model.MouldingViTracking_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);

            return ds;

        }

        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"    select b.trackingID  as trackingID
		,b.UsageQTY01 as UsageQTY01
      ,b.UsageQTY02 as UsageQTY02
      ,b.UsageQTY03 as UsageQTY03
      ,b.UsageQTY04 as UsageQTY04
      ,b.UsageQTY05 as UsageQTY05
      ,b.UsageQTY06 as UsageQTY06
      ,b.UsageQTY07 as UsageQTY07 
      ,b.UsageQTY08 as UsageQTY08
      ,b.UsageQTY09 as UsageQTY09
      ,b.UsageQTY10 as UsageQTY10
      ,b.UsageQTY11 as UsageQTY11
      ,b.UsageQTY12 as UsageQTY12
      ,b.RejectQTY01 as RejectQTY01
      ,b.RejectQTY02 as RejectQTY02
      ,b.RejectQTY03 as RejectQTY03
      ,b.RejectQTY04 as RejectQTY04
      ,b.RejectQTY05 as RejectQTY05
      ,b.RejectQTY06 as RejectQTY06
      ,b.RejectQTY07 as RejectQTY07
      ,b.RejectQTY08 as RejectQTY08
      ,b.RejectQTY09 as RejectQTY09
      ,b.RejectQTY10 as RejectQTY10
      ,b.RejectQTY11 as RejectQTY11
      ,b.RejectQTY12 as RejectQTY12
      ,b.SerialNo01 as SerialNo01
      ,b.SerialNo02 as SerialNo02
      ,b.SerialNo03 as SerialNo03
      ,b.SerialNo04 as SerialNo04
      ,b.SerialNo05 as SerialNo05
      ,b.SerialNo06 as SerialNo06
      ,b.SerialNo07 as SerialNo07
      ,b.SerialNo08 as SerialNo08
      ,b.SerialNo09 as SerialNo09
      ,b.SerialNo10 as SerialNo10
      ,b.SerialNo11 as SerialNo11
      ,b.SerialNo12 as SerialNo12
      ,b.SerialNoEnd as SerialNoEnd
      ,b.SerialNo as SerialNo
      ,convert(varchar(50),a.Day,105)  as Day
      ,a.shift as shift
      ,a.partNumberAll as PartNumberAll
      ,b.machineID as UserName
                        ,b.SerialNoEnd as UsageQTYSum
                        ,b.SerialNoEnd as RejectQTYSum
            ,b.SerialNoEnd as SerialNoSum

            from MouldingLabel b 
		left join MouldingViTracking a on a.trackingID = b.trackingID
		where a.dateTime > @DateFrom
		and a.dateTime < @DateTo


");

            if (sPartNo != "")
                strSql.Append(" and a.partNumberAll = @partNumberAll");

            if (sShift != "")
                strSql.Append(" and a.shift = @shift");

            if (sModule != "")
                strSql.Append(" and a.model = @model");

            if (sMachineID != "")
                strSql.Append(" and a.machineID = @machineID");

            strSql.Append(" order by  a.datetime desc");

            SqlParameter[] paras =
            {
                new SqlParameter("DateFrom",SqlDbType.DateTime2),
                new SqlParameter("DateTo",SqlDbType.DateTime2),
                new SqlParameter("partNumberAll",SqlDbType.VarChar),
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
                parameters[0].Value = "MouldingLabel";
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


