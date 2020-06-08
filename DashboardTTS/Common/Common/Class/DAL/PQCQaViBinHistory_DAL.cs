using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.DAL
{
    public class PQCQaViBinHistory_DAL
    {


        public SqlCommand AddCommand(Common.Class.Model.PQCQaViBinHistory_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCQaViBinHistory(");
            strSql.Append("id,trackingID,processes,jobId,PartNumber,materialPartNo,materialName,shipTo,model,jigNo,materialQty,status,nextViFlag,dateTime,day,shift,userName,userID,remark_1,remark_2,remark_3,remark_4,remarks,materialFromQty,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@trackingID,@processes,@jobId,@PartNumber,@materialPartNo,@materialName,@shipTo,@model,@jigNo,@materialQty,@status,@nextViFlag,@dateTime,@day,@shift,@userName,@userID,@remark_1,@remark_2,@remark_3,@remark_4,@remarks,@materialFromQty,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.VarChar,50),
                    new SqlParameter("@trackingID", SqlDbType.VarChar,50),
                    new SqlParameter("@processes", SqlDbType.VarChar,50),
                    new SqlParameter("@jobId", SqlDbType.VarChar,50),
                    new SqlParameter("@PartNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialPartNo", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,20),
                    new SqlParameter("@model", SqlDbType.VarChar,20),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,20),
                    new SqlParameter("@materialQty", SqlDbType.Decimal,9),
                    new SqlParameter("@status", SqlDbType.VarChar,20),
                    new SqlParameter("@nextViFlag", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.DateTime2,8),
                    new SqlParameter("@day", SqlDbType.DateTime2,8),
                    new SqlParameter("@shift", SqlDbType.VarChar,50),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@userID", SqlDbType.VarChar,50),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar,20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@materialFromQty",SqlDbType.Decimal,9),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime2,8)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.trackingID;
            parameters[2].Value = model.processes;
            parameters[3].Value = model.jobId;
            parameters[4].Value = model.PartNumber;
            parameters[5].Value = model.materialPartNo;
            parameters[6].Value = model.materialName;
            parameters[7].Value = model.shipTo;
            parameters[8].Value = model.model;
            parameters[9].Value = model.jigNo;
            parameters[10].Value = model.materialQty;
            parameters[11].Value = model.status;
            parameters[12].Value = model.nextViFlag;
            parameters[13].Value = model.dateTime;
            parameters[14].Value = model.day;
            parameters[15].Value = model.shift;
            parameters[16].Value = model.userName;
            parameters[17].Value = model.userID;
            parameters[18].Value = model.remark_1;
            parameters[19].Value = model.remark_2;
            parameters[20].Value = model.remark_3;
            parameters[21].Value = model.remark_4;
            parameters[22].Value = model.remarks;
            parameters[23].Value = model.materialFromQty;
            parameters[24].Value = model.updatedTime;

            return  DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }


    }
}
