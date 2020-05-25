using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
   public class MouldingQaViDefectTracking_DAL
    {
        public MouldingQaViDefectTracking_DAL()
        {

        }


        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNumber, string sRejType, string sModel)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(@"SELECT
                            ROW_NUMBER() over(order by a.shift asc, a.machineid asc, a.partNumber asc) as ID
                            ,CONVERT(varchar(16), a.[day], 105) as day
                            ,CONVERT(varchar(16), b.[MfgDate], 105) as MfgDate
                            ,a.shift
                            ,a.machineID
                            ,a.model
                            ,a.partNumber
                            ,a.model
                            ,a.rejectQty
                            ,isnull((a.rejectQty * c.unitCount) ,0) as rejectCost
                            ,a.defectCode
                            ,b.UserID as OPID
                            -- ,b.acountReading as OutPutQTY
                            ,a.dateTime

                            FROM MouldingQaViDefectTracking a 
                            left join MouldingQaViTracking b on a.trackingID = b.trackingID 
                            left join MouldingBom c on  a.partNumber = c.partNumber

                            where a.rejectQty > 0 ");


            strSql.Append("  and a.datetime >= @dateFrom ");
            strSql.Append(" and a.datetime < @dateTo ");


            if (sMachineID != "")
            {
                strSql.Append(" and a.machineid = @machineid ");
            }

            if (sPartNumber != "")
            {
                strSql.Append(" and a.partNumber like  @sPartNumber");
            }

            if (sRejType != "" && sRejType != "All")
            {
                strSql.Append(" and  a.defectCode= @defectCode ");
            }

            if (sModel != "")
            {
                strSql.Append(" and  a.model= @model ");
            }

            strSql.Append(" order by a.shift asc, a.machineid asc, a.partNumber asc ");

            SqlParameter[] paras =
            {
               new SqlParameter("@dateFrom",SqlDbType.DateTime),
               new SqlParameter("@dateTo",SqlDbType.DateTime),
               new SqlParameter("@machineid",SqlDbType.VarChar,16),
               new SqlParameter("@defectCode",SqlDbType.VarChar),
               new SqlParameter("@model",SqlDbType.VarChar),
               new SqlParameter("@sPartNumber",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo.AddDays(1);

            if (sMachineID != "")
            {
                paras[2].Value = sMachineID;
            }
            else
            {
                paras[2] = null;
            }

            if (sRejType != "" && sRejType != "All")
            {
                paras[3].Value = sRejType;
            }
            else
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

            if (sPartNumber != "")
            {
                paras[5].Value = sPartNumber;
            }
            else
            {
                paras[5] = null;
            }

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }



    }
}
