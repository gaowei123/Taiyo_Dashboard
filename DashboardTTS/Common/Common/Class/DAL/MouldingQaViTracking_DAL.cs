using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public  class MouldingQaViTracking_DAL
    {
        public MouldingQaViTracking_DAL()
        {

        }

        public DataSet SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sSfhift, string sModule)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT
                            ROW_NUMBER() over(order by machineid) as ID
                            ,CONVERT(varchar(50),day,23) as Day
                            ,shift
                            ,machineID
                            ,partNumber    
                            ,model
                            ,jigNo
                            ,boxID
                            ,MfgDate
                            ,acceptQty as OK
                            ,rejectQty as NG
                            ,(acceptQty+rejectQty) as Output
                            ,CONVERT(varchar(50), convert(numeric(10,2), rejectQty / (acceptQty + rejectQty) * 100)) + '%' as RejRate    
                            from MouldingQaViTracking  where acceptQty + rejectQty > 0");

            strSql.Append(" and dateTime > @DateFrom ");
            strSql.Append(" and dateTime < @DateTo ");

            if (sPartNo != "")
                strSql.Append(" and partNumber = @partNumber");

            if (sSfhift != "")
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

            if (sSfhift != "")
                paras[3].Value = sSfhift;
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

        

    }
}
