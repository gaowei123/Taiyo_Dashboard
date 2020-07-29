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
        public SqlCommand AddCMD(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintingDeliveryHis(");
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
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }



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

        public int Update(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaintingDeliveryHis set ");

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



        public bool Delete()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PaintingDeliveryHis ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
            };


            int rows =  DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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


        


        public DataSet GetDayOutput(DateTime dDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            sum(aa.MRP_SET) as TotalSET,
                            sum(aa.MRP_PCS) as TotalPCS
                                from
                            (
                                select
                                1 as ID,
                                CONVERT(decimal(18, 0), inQuantity) as MRP_SET,
                                convert(decimal(18, 0), inQuantity * isnull(b.materialCount, 1)) as MRP_PCS
                                from PaintingDeliveryHis a
                                left join (select partnumber, count(1) as materialCount from LMMS_TAIYO.dbo.LMMSBomDetail group by partNumber) b on a.partNumber = b.partnumber
                                where updatedTime >= @DateFrom and updatedTime < @DateTo
                            ) aa  group by ID ");


            SqlParameter[] paras =
            {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime)
            };

            paras[0].Value = dDay.Date.AddHours(8);
            paras[1].Value = dDay.Date.AddDays(1).AddHours(8);
         


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


        public DataTable GetOuput(DateTime DateFrom, DateTime DateTo, string Shift, string DateNotIn, bool ExceptWeekends)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
sum(a.TotalOuput) as TotalOutput
,sum(a.TotalRej) as TotalRej
from 
(
	select 
	1 as id
	,convert(decimal(18,0), isnull( inQuantity,0)) as TotalOuput
	,isnull(paintRejQty,0) as TotalRej
	from PaintingDeliveryHis 
	where updatedTime >= @dateFrom and updatedTime < @dateTo");
          

            if (DateNotIn != "")
            {
                strSql.Append(" and day(updatedTime) not in (");

                string[] strArrDate = DateNotIn.Split(',');
                foreach (string date in strArrDate)
                {
                    if (Common.CommFunctions.isNumberic(date))
                        strSql.Append(" '" + date + "', ");
                }
                strSql.Remove(strSql.Length, 1);

                strSql.Append(" ) ");
            }

            if (ExceptWeekends)
            {
                strSql.Append("  DATEPART(WEEKDAY,updatedTime) not in (1,7) ");
            }



            strSql.Append(" ) a group by a.id ");

            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime)
            };


            paras[0].Value = DateFrom;
            paras[1].Value = DateTo;


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
            if (ds == null || ds.Tables.Count == 0)
                return null;
            else
                return ds.Tables[0];

        }




        public DataSet GetPaintDeliveryForButtonReport_NEW(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
jobNumber
,lotNo
,convert(float,isnull(inQuantity,0)) as MrpQty
from PaintingDeliveryHis 
where 1=1 and jobNumber in " + strWhere);

            return DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }






    }
}
