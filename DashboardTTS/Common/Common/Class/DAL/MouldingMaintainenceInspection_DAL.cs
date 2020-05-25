using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingMaintainenceInspection_DAL
    {
        public MouldingMaintainenceInspection_DAL()
        {

        }


        public DataSet SelectList(string sCheckPeriod,string sCheckItem)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * from MouldingMaintainenceInspection where 1=1  ");
            if (sCheckPeriod != "")
            {
                strSql.Append(" and CheckPeriod = @CheckPeriod ");
            }

            if (sCheckItem != "")
            {
                strSql.Append(" and CheckItem = @CheckItem ");
            }


            strSql.Append(" order by No asc ");
            

            SqlParameter[] paras =
            {
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar)
            };
            if (sCheckPeriod != "")
            {
                paras[0].Value = sCheckPeriod;
            }
            else
            {
                paras[0] = null;
            }

            if (sCheckItem != "")
            {
                paras[1].Value = sCheckItem;
            }
            else
            {
                paras[1] = null;
            }



            return DBHelp.SqlDB.Query(strSql.ToString(),paras ,DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public int Add(Common.Class.Model.MouldingMaintenanceInspection_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into MouldingMaintainenceInspection ( ");
            strSql.Append(" No,CheckPeriod,CheckItem,InspectionDescription,MaintainenceDescription,DateTime,UpdateTime ) ");

            strSql.Append(" Values ( ");
            strSql.Append(" @No,@CheckPeriod,@CheckItem,@InspectionDescription,@MaintainenceDescription,@DateTime,@UpdateTime  ) ");



            SqlParameter[] paras =
            {
                new SqlParameter("@No",SqlDbType.VarChar),
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@InspectionDescription",SqlDbType.VarChar),
                new SqlParameter("@MaintainenceDescription",SqlDbType.VarChar),
                new SqlParameter("@DateTime",SqlDbType.DateTime),
                new SqlParameter("@UpdateTime",SqlDbType.DateTime)
            };

            paras[0].Value = model.No == null ? (object)DBNull.Value : model.No;
            paras[1].Value = model.CheckPeriod == null ? (object)DBNull.Value : model.CheckPeriod;
            paras[2].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            paras[3].Value = model.InspectionDescription == null ? (object)DBNull.Value : model.InspectionDescription;
            paras[4].Value = model.MaintainenceDescription == null ? (object)DBNull.Value : model.MaintainenceDescription;
            paras[5].Value =  DateTime.Now;
            paras[6].Value =  DateTime.Now;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public int Update(Common.Class.Model.MouldingMaintenanceInspection_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Update  MouldingMaintainenceInspection set  ");
            strSql.Append("  No = @No  ");
            strSql.Append(" , CheckPeriod = @CheckPeriod  ");
            strSql.Append(" , CheckItem = @CheckItem  ");
            strSql.Append(" , InspectionDescription = @InspectionDescription  ");
            strSql.Append(" , MaintainenceDescription = @MaintainenceDescription  ");
            strSql.Append(" , UpdateTime = @UpdateTime  ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and  UniqID = @UniqID ");

            SqlParameter[] paras =
            {
                new SqlParameter("@CheckPeriod",SqlDbType.VarChar),
                new SqlParameter("@CheckItem",SqlDbType.VarChar),
                new SqlParameter("@InspectionDescription",SqlDbType.VarChar),
                new SqlParameter("@MaintainenceDescription",SqlDbType.VarChar),
                new SqlParameter("@UpdateTime",SqlDbType.DateTime),
                new SqlParameter("@UniqID",SqlDbType.Int),
                new SqlParameter("@No",SqlDbType.Int)
            };
            
            paras[0].Value = model.CheckPeriod == null ? (object)DBNull.Value : model.CheckPeriod;
            paras[1].Value = model.CheckItem == null ? (object)DBNull.Value : model.CheckItem;
            paras[2].Value = model.InspectionDescription == null ? (object)DBNull.Value : model.InspectionDescription;
            paras[3].Value = model.MaintainenceDescription == null ? (object)DBNull.Value : model.MaintainenceDescription;
            paras[4].Value = model.UpdateTime;
            paras[5].Value = model.UniqID == null ? (object)DBNull.Value : model.UniqID;
            paras[6].Value = model.No == null ? (object)DBNull.Value : model.No;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


    }
}
