using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class PaintingDeliveryHis_DAL
    {

        public int Add(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintingDeliveryHis(");
            strSql.Append("jobNumber,partNumber,sendingTo,inQuantity,lotNo,boxQty,remark,dateTime,updatedTime,signID, paintProcess, paintRejQty");
            strSql.Append(") values (");
            strSql.Append("@jobNumber,@partNumber,@sendingTo,@inQuantity,@lotNo,@boxQty,@remark,@dateTime,@updatedTime,@signID, @paintProcess, @paintRejQty");
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
                new SqlParameter("@signID", SqlDbType.VarChar,32),
                new SqlParameter("@paintProcess", SqlDbType.VarChar,32),
                new SqlParameter("@paintRejQty", SqlDbType.VarChar,32)
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
            parameters[10].Value = model.paintProcess == null ? (object)DBNull.Value : model.paintProcess;
            parameters[11].Value = model.paintRejQty == null ? (object)DBNull.Value : model.paintRejQty;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }

     
        public int UpdatePaintRej(string jobNumber, int rejQty, string process)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaintingDeliveryHis set PaintRejQty = @PaintRejQty where jobNumber = @jobNumber and UPPER(paintProcess) = UPPER(@paintProcess) ");

         
            SqlParameter[] parameters = {
                new SqlParameter("@PaintRejQty", SqlDbType.Decimal,18),
                new SqlParameter("@jobNumber", SqlDbType.VarChar,32),
                new SqlParameter("@paintProcess", SqlDbType.VarChar,32)
            };

            parameters[0].Value = rejQty;
            parameters[1].Value = jobNumber;
            parameters[2].Value = process;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


        public int UpdateMFGDate(string jobNumber, DateTime MFGDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaintingDeliveryHis set ");
            strSql.Append(" dateTime = @dateTime  ");
            strSql.Append(" where  ");
            strSql.Append(" jobNumber = @jobNumber  ");
            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber", SqlDbType.VarChar,32) ,
                new SqlParameter("@dateTime", SqlDbType.DateTime2,8) 
            };

            parameters[0].Value = jobNumber;
            parameters[1].Value = MFGDate;
      

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }

        public SqlCommand CancelJob(string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Taiyo_Painting].[dbo].[PaintingDeliveryHis] set ");

            
            strSql.Append(" status = 'Cancel' ,");
          
            strSql.Append(" updatedTime =getdate()  ");
            strSql.Append(" where  ");
            strSql.Append(" jobNumber = @jobNumber ");

            SqlParameter[] parameters = {
                        new SqlParameter("@jobNumber", SqlDbType.VarChar,32) 
            };

            parameters[0].Value = sJobNo;
        
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }
        

        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPaintProcess)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT [jobNumber]
      ,[partNumber]
      ,[sendingTo]
      ,[inQuantity]
      ,[lotNo]
      ,[boxQty]
      ,[remark]
      ,[dateTime]
      ,[updatedTime]
      ,[signID]
      ,[status]
      ,[paintProcess]
      ,[PaintRejQty]
  FROM[PaintingDeliveryHis] ");
            strSql.Append("where updatedTime >= @dDateFrom  and updatedTime <= @dDateTo ");

            if (sJobNo.Trim() != "")
            {
                strSql.Append(" and jobNumber = @jobNo");
            }
            if (sPaintProcess.Trim() != "")
            {
                strSql.Append(" and paintProcess = @paintProcess ");
            }


            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@jobNo",SqlDbType.VarChar),
                new SqlParameter("@paintProcess",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sJobNo.Trim() != "") paras[2].Value = sJobNo; else paras[2] = null;
            if (sPaintProcess.Trim() != "")  paras[3].Value = sPaintProcess; else paras[3] = null;




            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }

        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPartNo, string sSendingTo,string sLotno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            ROW_NUMBER() over(order by a.partNumber asc, updatedTime desc) as ID
                            ,[jobNumber]
                            , a.[partNumber]
                            ,[sendingTo]
                            , convert(varchar(50), convert(decimal(18, 0), inQuantity))
                            + '(' +
                            convert(varchar(50), convert(decimal(18, 0), inQuantity * case when isnull(b.materialCount, '') = '' then 1 else b.materialCount end))
                            + ')'
                            as inQuantity
                            ,paintRejQty

                            ,inQuantity as InQtySET
                            ,inQuantity * case when isnull(b.materialCount, '') = '' then 1 else b.materialCount end as InQtyPCS
                            ,paintProcess
                            ,[lotNo]
                            ,[boxQty]
                            ,[remark]
                            ,[dateTime]
                            ,[updatedTime]
                            , SignID
                            , isnull(status, 'OK') as status ");
            strSql.Append(@" FROM PaintingDeliveryHis a
                             left join ( select aa.partNumber, count(1) as materialCount from");
            strSql.Append("  opendatasource('SQLOLEDB',"+StaticRes.Global.SqlConnection.SqlconnLaser+").LMMS_TAIYO.dbo.lmmsbomDetail  aa group by partNumber");
            strSql.Append   (@" ) b on a.partNumber collate Chinese_PRC_CI_AS  = b.partNumber collate Chinese_PRC_CI_AS
                             where 1 = 1   and updatedTime >= @dDateFrom  and updatedTime <= @dDateTo ");
            
            if (!string.IsNullOrEmpty(sJobNo))
            {
                strSql.Append(" and  a.jobNumber = @sJobNo ");
            }
            if (!string.IsNullOrEmpty(sPartNo))
            {
                strSql.Append(" and  a.partNumber = @sPartNo ");
            }
            if (!string.IsNullOrEmpty(sSendingTo))
            {
                strSql.Append(" and  a.sendingTo = @sendingTo ");
            }
            if (!string.IsNullOrEmpty(sLotno))
            {
                strSql.Append(" and  a.lotNo = @lotNo ");
            }

            strSql.Append(" order by a.partNumber asc, a.updatedTime desc");

            SqlParameter[] paras =
            {
                new SqlParameter("@dDateFrom",SqlDbType.DateTime),
                new SqlParameter("@dDateTo",SqlDbType.DateTime),
                new SqlParameter("@sJobNo",SqlDbType.VarChar),
                new SqlParameter("@sPartNo",SqlDbType.VarChar),
                new SqlParameter("@sendingTo",SqlDbType.VarChar),
                new SqlParameter("@lotNo",SqlDbType.VarChar)

               //new SqlParameter("@dataSource",SqlDbType.VarChar),
               //new SqlParameter("@userID",SqlDbType.VarChar),
               //new SqlParameter("@password",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;

            if (!string.IsNullOrEmpty(sJobNo))  {  paras[2].Value = sJobNo; }else { paras[2] = null; }
            if (!string.IsNullOrEmpty(sPartNo)) {  paras[3].Value = sPartNo;  }else { paras[3] = null; }
            if (!string.IsNullOrEmpty(sSendingTo)) { paras[4].Value = sSendingTo; } else { paras[4] = null; }
            if (!string.IsNullOrEmpty(sLotno)) { paras[5].Value = sLotno; } else { paras[5] = null; }

        
            //paras[6].Value = ;
            //paras[7].Value = ;
            //paras[8].Value = ;

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


       
        public DataSet CheckJobno(string sJobno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JobNumber FROM PaintingDeliveryHis where jobNumber = @jobNumber ");

            SqlParameter[] paras = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar)
            };


            paras[0].Value = sJobno;

            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }
        

        public DataSet GetPaintDeliveryForButtonReport_NEW(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobNumber
,lotNo
,convert(float,isnull(inQuantity,0)) as MrpQty
,paintProcess
from PaintingDeliveryHis 
where 1=1 and jobNumber in " + strWhere);

            return DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


    }
}
