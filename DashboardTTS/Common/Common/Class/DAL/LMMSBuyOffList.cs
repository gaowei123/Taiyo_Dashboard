using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class LMMSBuyOffList
    {
        public LMMSBuyOffList()
        {

        }      
        public DataSet GetList(string sJobnumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"BUYOFF_ID, JOB_ID ,PART_NO, MACHINE_ID, DATE_TIME, MC_OPERATOR, BUYOFF_BY, APPROVED_BY,CHECK_BY,
                            BLACK_MARK_1ST,BLACK_DOT_1ST,PIN_HOLE_1ST,JAGGED_1ST,CHECK_GULED_1ST,NAVITAS_1ST,SMART_SCOPE_1ST,
                            BLACK_MARK_2ND,BLACK_DOT_2ND,PIN_HOLE_2ND,JAGGED_2ND,CHECK_GULED_2ND,NAVITAS_2ND,SMART_SCOPE_2ND,
                            BLACK_MARK_IN,BLACK_DOT_IN,PIN_HOLE_IN,JAGGED_IN,CHECK_GULED_IN,NAVITAS_IN,SMART_SCOPE_IN ");
            strSql.Append(" FROM LMMSBUYOFF_LIST where 1=1");

            if (!string.IsNullOrEmpty(sJobnumber))
                strSql.Append(" and JOB_ID = @JOB_ID");

            SqlParameter[] paras =
            {
                new SqlParameter(@"JOB_ID",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sJobnumber)) { paras[0].Value = sJobnumber; }else { paras[0] = null; }
          


            return DBHelp.SqlDB.Query(strSql.ToString(),paras);
        }
        
        public DataSet SelectBuyoffReport(string JobID, string PartNo, string Machine, string MC_Operator, string ApprovdeBy, string CheckBy, DateTime? From, DateTime? To)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT 
                            BUYOFF_ID,JOB_ID,PART_NO,MACHINE_ID,DATE_TIME,MC_OPERATOR,BUYOFF_BY,APPROVED_BY,CHECK_BY
                            ,BLACK_MARK_1ST,BLACK_DOT_1ST,PIN_HOLE_1ST,JAGGED_1ST,CHECK_GULED_1ST,NAVITAS_1ST,SMART_SCOPE_1ST
                            ,BLACK_MARK_2ND,BLACK_DOT_2ND,PIN_HOLE_2ND,JAGGED_2ND,CHECK_GULED_2ND,NAVITAS_2ND,SMART_SCOPE_2ND
                            ,BLACK_MARK_IN,BLACK_DOT_IN,PIN_HOLE_IN,JAGGED_IN,CHECK_GULED_IN,NAVITAS_IN,SMART_SCOPE_IN

                            ,c.LotNo

                            ,case when ISNULL( b.CurrentPower,'') = '' then 'NA' else  b.CurrentPower end as CurrentPower


                            ,CONVERT(varchar(50), c.quantity) + '(' + CONVERT(varchar(50), c.quantity * case when ISNULL(d.materialCount,0) = 0 then 1 else d.materialCount end ) +')' as lotQty


                            FROM LMMSBUYOFF_LIST a 
                            left join lmmsbom b on a.PART_NO = b.partNumber and a.MACHINE_ID =b.machineID 
                            left join lmmsinventory c on a.JOB_ID = c.jobNumber
                            left join
                            (
	                            select partnumber, count(1) as materialCount from LMMSBomDetail group by partNumber
                            ) d on b.partNumber = d.partNumber
                            where 1=1 ");

            if (!string.IsNullOrEmpty(JobID))
                strSql.Append(" and JOB_ID = @JOB_ID  ");

            if (!string.IsNullOrEmpty(PartNo))
                strSql.Append(" and PART_NO = @PART_NO  ");

            if (!string.IsNullOrEmpty(Machine))
                strSql.Append(" and MACHINE_ID = @MACHINE_ID  ");

            if (!string.IsNullOrEmpty(MC_Operator))
                strSql.Append(" and MC_OPERATOR = @MC_OPERATOR  ");

            if (!string.IsNullOrEmpty(ApprovdeBy))
                strSql.Append(" and APPROVED_BY = @APPROVED_BY  ");

            if (!string.IsNullOrEmpty(CheckBy))
                strSql.Append(" and CHECK_BY = @CHECK_BY  ");

            if (From != null)
                strSql.Append(" and DATE_TIME >= @F ");
            if (To != null)
                strSql.Append(" and DATE_TIME < @T ");

            strSql.Append(" order by DATE_TIME desc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@JOB_ID",SqlDbType.VarChar),
                new SqlParameter("@PART_NO",SqlDbType.VarChar),
                new SqlParameter("@MACHINE_ID",SqlDbType.VarChar),
                new SqlParameter("@MC_OPERATOR",SqlDbType.VarChar),
                new SqlParameter("@APPROVED_BY",SqlDbType.VarChar),
                new SqlParameter("@CHECK_BY",SqlDbType.VarChar),
                new SqlParameter("@F",SqlDbType.DateTime),
                new SqlParameter("@T",SqlDbType.DateTime)
            };

            if (!string.IsNullOrEmpty(JobID))       { paras[0].Value = JobID; }         else { paras[0] = null; }
            if (!string.IsNullOrEmpty(PartNo))      { paras[1].Value = PartNo; }        else { paras[1] = null; }
            if (!string.IsNullOrEmpty(Machine))     { paras[2].Value = Machine; }       else { paras[2] = null; }
            if (!string.IsNullOrEmpty(MC_Operator)) { paras[3].Value = MC_Operator; }   else { paras[3] = null; }
            if (!string.IsNullOrEmpty(ApprovdeBy))  { paras[4].Value = ApprovdeBy; }    else { paras[4] = null; }
            if (!string.IsNullOrEmpty(CheckBy))     { paras[5].Value = CheckBy; }       else { paras[5] = null; }
            if (From != null)                       { paras[6].Value = From; }          else { paras[6] = null; }
            if (To != null)                         { paras[7].Value = To; }            else { paras[7] = null; }

            return DBHelp.SqlDB.Query(strSql.ToString(), paras);
        }

        public SqlCommand AddCommand(Common.Class.Model.LMMSBuyOffList_Mode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSBUYOFF_LIST (");
            strSql.Append(@"BUYOFF_ID,JOB_ID,PART_NO,MACHINE_ID,DATE_TIME,MC_OPERATOR,BUYOFF_BY,APPROVED_BY,CHECK_BY,
                            BLACK_MARK_1ST,BLACK_DOT_1ST,PIN_HOLE_1ST,JAGGED_1ST,CHECK_GULED_1ST,NAVITAS_1ST,SMART_SCOPE_1ST,
                            BLACK_MARK_2ND,BLACK_DOT_2ND,PIN_HOLE_2ND,JAGGED_2ND,CHECK_GULED_2ND,NAVITAS_2ND,SMART_SCOPE_2ND,
                            BLACK_MARK_IN,BLACK_DOT_IN,PIN_HOLE_IN,JAGGED_IN,CHECK_GULED_IN,NAVITAS_IN,SMART_SCOPE_IN");
            strSql.Append(") values (");
            strSql.Append(@"@BUYOFF_ID,@JOB_ID,@PART_NO,@MACHINE_ID,@DATE_TIME,@MC_OPERATOR,@BUYOFF_BY,@APPROVED_BY,@CHECK_BY,
                            @BLACK_MARK_1ST,@BLACK_DOT_1ST,@PIN_HOLE_1ST,@JAGGED_1ST,@CHECK_GULED_1ST,@NAVITAS_1ST,@SMART_SCOPE_1ST,
                            @BLACK_MARK_2ND,@BLACK_DOT_2ND,@PIN_HOLE_2ND,@JAGGED_2ND,@CHECK_GULED_2ND,@NAVITAS_2ND,@SMART_SCOPE_2ND,
                            @BLACK_MARK_IN,@BLACK_DOT_IN,@PIN_HOLE_IN,@JAGGED_IN,@CHECK_GULED_IN,@NAVITAS_IN,@SMART_SCOPE_IN)");
            SqlParameter[] parameters = {
                new SqlParameter("@BUYOFF_ID", SqlDbType.VarChar),
                new SqlParameter("@JOB_ID", SqlDbType.VarChar),
                new SqlParameter("@PART_NO", SqlDbType.VarChar),
                new SqlParameter("@MACHINE_ID", SqlDbType.VarChar),
                new SqlParameter("@DATE_TIME", SqlDbType.DateTime),
                new SqlParameter("@MC_OPERATOR", SqlDbType.VarChar),
                new SqlParameter("@BUYOFF_BY", SqlDbType.VarChar),
                new SqlParameter("@APPROVED_BY", SqlDbType.VarChar),
                new SqlParameter("@CHECK_BY", SqlDbType.VarChar),
                new SqlParameter("@BLACK_MARK_1ST", SqlDbType.VarChar),
                new SqlParameter("@BLACK_DOT_1ST", SqlDbType.VarChar),
                new SqlParameter("@PIN_HOLE_1ST", SqlDbType.VarChar),
                new SqlParameter("@JAGGED_1ST", SqlDbType.VarChar),
                new SqlParameter("@CHECK_GULED_1ST", SqlDbType.VarChar),
                new SqlParameter("@NAVITAS_1ST", SqlDbType.VarChar),
                new SqlParameter("@SMART_SCOPE_1ST", SqlDbType.VarChar),
                new SqlParameter("@BLACK_MARK_2ND", SqlDbType.VarChar),
                new SqlParameter("@BLACK_DOT_2ND", SqlDbType.VarChar),
                new SqlParameter("@PIN_HOLE_2ND", SqlDbType.VarChar),
                new SqlParameter("@JAGGED_2ND", SqlDbType.VarChar),
                new SqlParameter("@CHECK_GULED_2ND", SqlDbType.VarChar),
                new SqlParameter("@NAVITAS_2ND", SqlDbType.VarChar),
                new SqlParameter("@SMART_SCOPE_2ND", SqlDbType.VarChar),
                new SqlParameter("@BLACK_MARK_IN", SqlDbType.VarChar),
                new SqlParameter("@BLACK_DOT_IN", SqlDbType.VarChar),
                new SqlParameter("@PIN_HOLE_IN", SqlDbType.VarChar),
                new SqlParameter("@JAGGED_IN", SqlDbType.VarChar),
                new SqlParameter("@CHECK_GULED_IN", SqlDbType.VarChar),
                new SqlParameter("@NAVITAS_IN", SqlDbType.VarChar),
                new SqlParameter("@SMART_SCOPE_IN", SqlDbType.VarChar)};
            parameters[0].Value = model.BUYOFF_ID == null ? (object)DBNull.Value : model.BUYOFF_ID;
            parameters[1].Value = model.JOB_ID == null ? (object)DBNull.Value : model.JOB_ID;
            parameters[2].Value = model.PART_NO == null ? (object)DBNull.Value : model.PART_NO;
            parameters[3].Value = model.MACHINE_ID == null ? (object)DBNull.Value : model.MACHINE_ID;
            parameters[4].Value = model.DATE_TIME == null ? (object)DBNull.Value : model.DATE_TIME;
            parameters[5].Value = model.MC_OPERATOR == null ? (object)DBNull.Value : model.MC_OPERATOR;
            parameters[6].Value = model.BUYOFF_BY == null ? (object)DBNull.Value : model.BUYOFF_BY;
            parameters[7].Value = model.APPROVED_BY == null ? (object)DBNull.Value : model.APPROVED_BY;
            parameters[8].Value = model.CHECK_BY == null ? (object)DBNull.Value : model.CHECK_BY;
            parameters[9].Value = model.BLACK_MARK_1ST == null ? (object)DBNull.Value : model.BLACK_MARK_1ST;
            parameters[10].Value = model.BLACK_DOT_1ST == null ? (object)DBNull.Value : model.BLACK_DOT_1ST;
            parameters[11].Value = model.PIN_HOLE_1ST == null ? (object)DBNull.Value : model.PIN_HOLE_1ST;
            parameters[12].Value = model.JAGGED_1ST == null ? (object)DBNull.Value : model.JAGGED_1ST;
            parameters[13].Value = model.CHECK_GULED_1ST == null ? (object)DBNull.Value : model.CHECK_GULED_1ST;
            parameters[14].Value = model.NAVITAS_1ST == null ? (object)DBNull.Value : model.NAVITAS_1ST;
            parameters[15].Value = model.SMART_SCOPE_1ST == null ? (object)DBNull.Value : model.SMART_SCOPE_1ST;

            parameters[16].Value = model.BLACK_MARK_2ND == null ? (object)DBNull.Value : model.BLACK_MARK_2ND;
            parameters[17].Value = model.BLACK_DOT_2ND == null ? (object)DBNull.Value : model.BLACK_DOT_2ND;
            parameters[18].Value = model.PIN_HOLE_2ND == null ? (object)DBNull.Value : model.PIN_HOLE_2ND;
            parameters[19].Value = model.JAGGED_2ND == null ? (object)DBNull.Value : model.JAGGED_2ND;
            parameters[20].Value = model.CHECK_GULED_2ND == null ? (object)DBNull.Value : model.CHECK_GULED_2ND;
            parameters[21].Value = model.NAVITAS_2ND == null ? (object)DBNull.Value : model.NAVITAS_2ND;
            parameters[22].Value = model.SMART_SCOPE_2ND == null ? (object)DBNull.Value : model.SMART_SCOPE_2ND;

            parameters[23].Value = model.BLACK_MARK_IN == null ? (object)DBNull.Value : model.BLACK_MARK_IN;
            parameters[24].Value = model.BLACK_DOT_IN == null ? (object)DBNull.Value : model.BLACK_DOT_IN;
            parameters[25].Value = model.PIN_HOLE_IN == null ? (object)DBNull.Value : model.PIN_HOLE_IN;
            parameters[26].Value = model.JAGGED_IN == null ? (object)DBNull.Value : model.JAGGED_IN;
            parameters[27].Value = model.CHECK_GULED_IN == null ? (object)DBNull.Value : model.CHECK_GULED_IN;
            parameters[28].Value = model.NAVITAS_IN == null ? (object)DBNull.Value : model.NAVITAS_IN;
            parameters[29].Value = model.SMART_SCOPE_IN == null ? (object)DBNull.Value : model.SMART_SCOPE_IN;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        
        public DataTable GetLaserInfoForButtonReport_NEW(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select 
                            JOB_ID as JobID
                            ,DATE_TIME as dateTime
                            ,MC_OPERATOR as laserOP
                            ,MACHINE_ID as laserMachine
                            from LMMSBUYOFF_LIST
                            where  JOB_ID in  " + strWhere);

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString());
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }


    }
}
