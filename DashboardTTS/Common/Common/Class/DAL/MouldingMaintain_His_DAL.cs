using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingMaintain_His_DAL
    {
        public MouldingMaintain_His_DAL() { }


        public DataSet SelectCheckPeriod()
        {
            string sql = "select distinct No, CheckPeriod from MouldingMaintainenceInspection order by No asc";
            return DBHelp.SqlDB.Query(sql, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet SelectCheckItem(string sCheckPeriod)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from MouldingMaintainenceInspection where 1=1 ");

            if (!string.IsNullOrEmpty(sCheckPeriod))
                sql.Append(" and CheckPeriod = @CheckPeriod ");


            sql.Append(" order by No asc, CheckItem asc");


            SqlParameter[] paras =
            {
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sCheckPeriod))
                paras[0].Value = sCheckPeriod;
            else
                paras[0] = null;
            

            return DBHelp.SqlDB.Query(sql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public int Add(Common.Class.Model.MouldingMaintain_His_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into MouldingMaintain_His ");
            strSql.Append(" ( ");
            strSql.Append(" MachineID,CheckPeriod,CheckItem,CheckResult,SpareParts,ChangeTime,CheckDate,CheckBy,VerifyBy ,Remark ");
            strSql.Append(" ) ");
            strSql.Append(" values ");
            strSql.Append(" ( ");
            strSql.Append("  @MachineID,@CheckPeriod,@CheckItem,@CheckResult,@SpareParts,@ChangeTime,@CheckDate,@CheckBy,@VerifyBy ,@Remark ");
            strSql.Append(" ) ");


            SqlParameter[] paras =
            {
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@CheckResult",SqlDbType.VarChar),
                new SqlParameter("@SpareParts",SqlDbType.VarChar),
                new SqlParameter("@ChangeTime",SqlDbType.DateTime),
                new SqlParameter("@CheckDate",SqlDbType.DateTime),
                new SqlParameter("@CheckBy",SqlDbType.VarChar),
                new SqlParameter("@VerifyBy",SqlDbType.VarChar),
                new SqlParameter("@Remark",SqlDbType.VarChar)
            };

            paras[0].Value = model.MachineID == null ?  (object)DBNull.Value : model.MachineID;
            paras[1].Value = model.CheckPeriod == null ? (object)DBNull.Value : model.CheckPeriod;
            paras[2].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            paras[3].Value = model.CheckResult == null ? (object)DBNull.Value : model.CheckResult;
            paras[4].Value = model.SpareParts == null ? (object)DBNull.Value : model.SpareParts;
            paras[5].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            paras[6].Value = model.CheckDate == null ? (object)DBNull.Value : model.CheckDate;
            paras[7].Value = model.CheckBy == null ? (object)DBNull.Value : model.CheckBy;
            paras[8].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            paras[9].Value = model.Remark == null ? (object)DBNull.Value : model.Remark;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public SqlCommand AddCMD(Common.Class.Model.MouldingMaintain_His_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into MouldingMaintain_His ");
            strSql.Append(" ( ");
            strSql.Append(" MachineID,CheckPeriod,CheckItem,CheckResult,SpareParts,ChangeTime,CheckDate,CheckBy,VerifyBy ,Remark ");
            strSql.Append(" ) ");
            strSql.Append(" values ");
            strSql.Append(" ( ");
            strSql.Append("  @MachineID,@CheckPeriod,@CheckItem,@CheckResult,@SpareParts,@ChangeTime,@CheckDate,@CheckBy,@VerifyBy ,@Remark ");
            strSql.Append(" ) ");


            SqlParameter[] paras =
            {
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@CheckResult",SqlDbType.VarChar),
                new SqlParameter("@SpareParts",SqlDbType.VarChar),
                new SqlParameter("@ChangeTime",SqlDbType.DateTime),
                new SqlParameter("@CheckDate",SqlDbType.DateTime),
                new SqlParameter("@CheckBy",SqlDbType.VarChar),
                new SqlParameter("@VerifyBy",SqlDbType.VarChar),
                new SqlParameter("@Remark",SqlDbType.VarChar)
            };

            paras[0].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            paras[1].Value = model.CheckPeriod == null ? (object)DBNull.Value : model.CheckPeriod;
            paras[2].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            paras[3].Value = model.CheckResult == null ? (object)DBNull.Value : model.CheckResult;
            paras[4].Value = model.SpareParts == null ? (object)DBNull.Value : model.SpareParts;
            paras[5].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            paras[6].Value = model.CheckDate == null ? (object)DBNull.Value : model.CheckDate;
            paras[7].Value = model.CheckBy == null ? (object)DBNull.Value : model.CheckBy;
            paras[8].Value = model.VerifyBy == null ? (object)DBNull.Value : model.VerifyBy;
            paras[9].Value = model.Remark == null ? (object)DBNull.Value : model.Remark;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),paras);
        }


        public int UpdateSpareParts(Common.Class.Model.MouldingMaintain_His_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update MouldingMaintain_His set ");
            strSql.Append(" SpareParts = SpareParts ");
            strSql.Append(" ChangeTime = ChangeTime ");
            strSql.Append(" Remark = Remark ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and MachineID = MachineID ");
            strSql.Append(" and CheckItem = CheckItem ");


            SqlParameter[] paras =
            {
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@SpareParts",SqlDbType.VarChar),
                new SqlParameter("@ChangeTime",SqlDbType.DateTime),
                new SqlParameter("@Remark",SqlDbType.VarChar)
            };

            paras[0].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            paras[1].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            paras[2].Value = model.SpareParts == null ? (object)DBNull.Value : model.SpareParts;
            paras[3].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            paras[4].Value = model.Remark == null ? (object)DBNull.Value : model.Remark;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet Select(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sCheckPeriod)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.MachineID, a.CheckItem,a.CheckPeriod, a.SpareParts, b.MaintainenceDescription, b.InspectionDescription, convert(varchar(50),a.CheckDate,105) as CheckDate , a.CheckBy, a.VerifyBy");

            strSql.Append(@" ,a.CheckResult ");

            //strSql.Append(@" ,case when  a.CheckResult = 'OK' then 'OK' 
            //                 else  a.CheckResult +','+ a.SpareParts end as CheckResult ");

            strSql.Append("  from MouldingMaintain_His a left join MouldingMaintainenceInspection b on a.CheckItem = b.CheckItem where 1=1 ");
            
            strSql.Append(" and a.CheckDate > @dDateFrom ");
            strSql.Append(" and a.CheckDate < @dDateTo ");

            if (!string.IsNullOrEmpty(sMachineID))
            {
                strSql.Append(" and MachineID = @sMachineID ");
            }

            if (!string.IsNullOrEmpty(sCheckPeriod))
            {
                strSql.Append(" and a.CheckPeriod = @sCheckPeriod ");
            }

            strSql.Append(" order by a.machineID asc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@sMachineID",SqlDbType.VarChar),
                new SqlParameter("@sCheckPeriod",SqlDbType.VarChar)
            };


            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (!string.IsNullOrEmpty(sMachineID))
                paras[2].Value = sMachineID;
            else
                paras[2] = null;

            if (!string.IsNullOrEmpty(sCheckPeriod))
                paras[3].Value = sCheckPeriod;
            else
                paras[3] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet CheckExist(DateTime? dDateFrom, string sCheckItem, string sMachineID, string sCheckPeriod)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Select  MachineID, CheckPeriod, CheckItem, CheckBy, VerifyBy
                            FROM MouldingMaintain_His where 1 = 1  ");

            if (dDateFrom != null)
            {
                strSql.Append(" and CheckDate > @dDateFrom ");
            }

            strSql.Append(" and CheckItem = @CheckItem");


            if (sMachineID != "")
            {
                strSql.Append(" and MachineID = @sMachineID");
            }

            if (sCheckPeriod != "")
            {
                strSql.Append(" and CheckPeriod = @sCheckPeriod");
            }

            strSql.Append(" order by CheckDate desc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom", SqlDbType.DateTime),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@sMachineID",SqlDbType.VarChar),
                new SqlParameter("@sCheckPeriod", SqlDbType.VarChar)
            };

            if (dDateFrom != null)
            {
                paras[0].Value = dDateFrom;
            }
            else
            {
                paras[0] = null;
            }

            paras[1].Value = sCheckItem;
            paras[2].Value = sMachineID;
            paras[3].Value = sCheckPeriod;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

    }




}
