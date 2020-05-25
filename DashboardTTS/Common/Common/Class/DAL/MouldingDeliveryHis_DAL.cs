using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class MouldingDeliveryHis_DAL
    {
        public SqlCommand AddCMD(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingDeliveryHis(");
            strSql.Append("jobNumber,partNumber,sendingTo,inQuantity,lotNo,boxQty,remark,dateTime,updatedTime,signID");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@sendingTo,@inQuantity,@lotNo,@boxQty,@remark,@dateTime,@updatedTime,@signID");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar,32) ,
                new SqlParameter("@partNumber", SqlDbType.VarChar,32) ,
                new SqlParameter("@sendingTo", SqlDbType.VarChar,32) ,
                new SqlParameter("@inQuantity", SqlDbType.Decimal,9) ,
                new SqlParameter("@lotNo", SqlDbType.VarChar,32) ,
                new SqlParameter("@boxQty", SqlDbType.Decimal,9) ,
                new SqlParameter("@remark", SqlDbType.VarChar,100) ,
                new SqlParameter("@dateTime", SqlDbType.DateTime2,8) ,
                new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                new SqlParameter("@signID", SqlDbType.VarChar,32)

            };

            parameters[0].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.sendingTo == null ? (object)DBNull.Value : model.sendingTo;
            parameters[3].Value = model.inQuantity == 0 ? (object)DBNull.Value : model.inQuantity;
            parameters[4].Value = model.lotNo == null ? (object)DBNull.Value : model.lotNo;
            parameters[5].Value = model.boxQty == null ? (object)DBNull.Value : model.boxQty;
            parameters[6].Value = model.remark == null ? (object)DBNull.Value : model.remark;
            parameters[7].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[8].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[9].Value = model.SignID == null ? (object)DBNull.Value : model.SignID;
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }



        public int Add(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingDeliveryHis(");
            strSql.Append("jobNumber,partNumber,sendingTo,inQuantity,lotNo,boxQty,remark,dateTime,updatedTime,signID");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@sendingTo,@inQuantity,@lotNo,@boxQty,@remark,@dateTime,@updatedTime,@signID");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar,32) ,
                new SqlParameter("@partNumber", SqlDbType.VarChar,32) ,
                new SqlParameter("@sendingTo", SqlDbType.VarChar,32) ,
                new SqlParameter("@inQuantity", SqlDbType.Decimal,9) ,
                new SqlParameter("@lotNo", SqlDbType.VarChar,32) ,
                new SqlParameter("@boxQty", SqlDbType.Decimal,9) ,
                new SqlParameter("@remark", SqlDbType.VarChar,100) ,
                new SqlParameter("@dateTime", SqlDbType.DateTime2,8) ,
                new SqlParameter("@updatedTime", SqlDbType.DateTime2,8),
                new SqlParameter("@signID", SqlDbType.VarChar,32)

            };

            parameters[0].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[1].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[2].Value = model.sendingTo == null ? (object)DBNull.Value : model.sendingTo;
            parameters[3].Value = model.inQuantity == 0 ? (object)DBNull.Value : model.inQuantity;
            parameters[4].Value = model.lotNo == null ? (object)DBNull.Value : model.lotNo;
            parameters[5].Value = model.boxQty == null ? (object)DBNull.Value : model.boxQty;
            parameters[6].Value = model.remark == null ? (object)DBNull.Value : model.remark;
            parameters[7].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[8].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[9].Value = model.SignID == null ? (object)DBNull.Value : model.SignID;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public int Update(Common.Class.Model.MouldingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingDeliveryHis set ");

            strSql.Append(" jobNumber = @jobNumber , ");
            strSql.Append(" partNumber = @partNumber , ");
            strSql.Append(" sendingTo = @sendingTo , ");
            strSql.Append(" inQuantity = @inQuantity , ");
            strSql.Append(" lotNo = @lotNo , ");
            strSql.Append(" boxQty = @boxQty , ");
            strSql.Append(" remark = @remark , ");
            strSql.Append(" dateTime = @dateTime , ");
            strSql.Append(" updatedTime = @updatedTime  ");
            strSql.Append(" where  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@jobNumber", SqlDbType.VarChar,32) ,
                        new SqlParameter("@partNumber", SqlDbType.VarChar,32) ,
                        new SqlParameter("@sendingTo", SqlDbType.VarChar,32) ,
                        new SqlParameter("@inQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@lotNo", SqlDbType.VarChar,32) ,
                        new SqlParameter("@boxQty", SqlDbType.Decimal,9) ,
                        new SqlParameter("@remark", SqlDbType.VarChar,100) ,
                        new SqlParameter("@dateTime", SqlDbType.DateTime2,8) ,
                        new SqlParameter("@updatedTime", SqlDbType.DateTime2,8)

            };

            parameters[0].Value = model.jobNumber;
            parameters[1].Value = model.partNumber;
            parameters[2].Value = model.sendingTo;
            parameters[3].Value = model.inQuantity;
            parameters[4].Value = model.lotNo;
            parameters[5].Value = model.boxQty;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.dateTime;
            parameters[8].Value = model.updatedTime;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public bool Delete()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MouldingDeliveryHis ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
            };


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPartNo, string sSendingTo, string sLotno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format( @"select 
                            ROW_NUMBER() over(order by a.partNumber asc, updatedTime desc) as ID
                            ,[jobNumber]
                            ,a.[partNumber]
                            ,[sendingTo]
                            , convert(varchar(50), convert(decimal(18, 0), inQuantity))
                            + '(' +
                            convert(varchar(50), convert(decimal(18, 0), inQuantity * isnull( b.materialCount,1)))
                            + ')'
                            as inQuantity

                            ,isnull( inQuantity,0) as InQtySET
                            ,isnull( inQuantity * isnull( b.materialCount,1),0) as InQtyPCS

                            ,[lotNo]
                            ,[boxQty]
                            ,[remark]
                            ,convert(varchar(50),[dateTime],3) as MFGDate
                            ,convert(varchar(50),[updatedTime],3) as updatedtime 
                            , SignID
                            , isnull(status, 'OK') as status 

                            FROM MouldingDeliveryHis a
                            left join 
                            (
	                            select partnumber, count(1) as materialCount from 
	                            opendatasource('SQLOLEDB',{0}).taiyo_pqc.dbo.pqcbomdetail
	                            group by partnumber
                            )b on a.partNumber collate chinese_prc_ci_as = b.partnumber collate chinese_prc_ci_as

                            where 1 = 1   and updatedTime >= @dDateFrom  and updatedTime <= @dDateTo ",StaticRes.Global.SqlConnection.SqlconnPQC));
         

            if (!string.IsNullOrEmpty(sJobNo))
            {
                strSql.Append(" and  jobNumber = @sJobNo ");
            }
            if (!string.IsNullOrEmpty(sPartNo))
            {
                strSql.Append(" and  partNumber = @sPartNo ");
            }
            if (!string.IsNullOrEmpty(sSendingTo))
            {
                strSql.Append(" and  sendingTo = @sendingTo ");
            }
            if (!string.IsNullOrEmpty(sLotno))
            {
                strSql.Append(" and  lotNo = @lotNo ");
            }

            strSql.Append(" order by partNumber asc, updatedTime desc");

            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@sJobNo",SqlDbType.VarChar),
                new SqlParameter("@sPartNo",SqlDbType.VarChar),
                new SqlParameter("@sendingTo",SqlDbType.VarChar),
                new SqlParameter("@lotNo",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (!string.IsNullOrEmpty(sJobNo)) { paras[2].Value = sJobNo; } else { paras[2] = null; }
            if (!string.IsNullOrEmpty(sPartNo)) { paras[3].Value = sPartNo; } else { paras[3] = null; }
            if (!string.IsNullOrEmpty(sSendingTo)) { paras[4].Value = sSendingTo; } else { paras[4] = null; }
            if (!string.IsNullOrEmpty(sLotno)) { paras[5].Value = sLotno; } else { paras[5] = null; }


          
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }


        public DataSet CheckLotno(string sLotno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select lotNo FROM MouldingDeliveryHis where lotNo = @lotNo ");

            SqlParameter[] paras = {
                new SqlParameter("@lotNo",SqlDbType.VarChar)
            };


            paras[0].Value = sLotno;

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet CheckJobno(string sJobno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JobNumber FROM MouldingDeliveryHis where jobNumber = @jobNumber ");

            SqlParameter[] paras = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar)
            };


            paras[0].Value = sJobno;

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }




    }
}

