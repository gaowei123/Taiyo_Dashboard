using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.Class.DAL
{
    /// <summary>
    /// 数据访问类:MouldingChangeModel_DAL
    /// </summary>
    public class MouldingMoldLife_DAL
    {
        public MouldingMoldLife_DAL()
        { }

        public DataSet SelectList(string sMouldID, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Select MoldID,MachineID,PartNumberAll,Accumulate, MouldLife
                            ,Clean1Qty,CONVERT(varchar(100), Clean1Time, 120) as Clean1Time,Clean1TimeBy,Clean2Qty,CONVERT(varchar(100), Clean2Time, 120) as Clean2Time
                            ,Clean2TimeBy,Clean3Qty,CONVERT(varchar(100), Clean3Time, 120) as Clean3Time,Clean3TimeBy,Clean4Qty
                            ,CONVERT(varchar(100), Clean4Time, 120) as Clean4Time,Clean4TimeBy,Clean5Qty,CONVERT(varchar(100), Clean5Time, 120) as Clean5Time,Clean5TimeBy
                            ,ChangeQty,ChangeTime,ChangeBy,CONVERT(varchar(100), CreateTime, 120) as CreateTime,CONVERT(varchar(100), UpdatedTime, 120) as UpdatedTime 
                            from MouldingMoldLife 
                            where 1=1 ");

            if (sMouldID != "")
            {
                strSql.Append(" and MoldID = @MoldID ");
            }

            if (sMachineID != "")
            {
                strSql.Append(" and MachineID = @MachineID ");
            }

            //strSql.Append(" order by  MachineID asc  ");
            strSql.Append(" order by  case when  Accumulate > MouldLife then  (Accumulate-MouldLife) else 0 end + case when Clean1Qty > 45000  then (Clean1Qty - 45000) else 0 end  desc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@MoldID",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sMouldID))
                paras[0].Value = sMouldID;
            else
                paras[0] = null;

            if (!string.IsNullOrEmpty(sMachineID))
                paras[1].Value = sMachineID;
            else
                paras[1] = null;



            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet SelectHistoryList(string sMouldID, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Select MoldID,MachineID,PartNumberAll,Accumulate
                            ,Clean1Qty,CONVERT(varchar(100), Clean1Time, 120) as Clean1Time,Clean1TimeBy,Clean2Qty,CONVERT(varchar(100), Clean2Time, 120) as Clean2Time
                            ,Clean2TimeBy,Clean3Qty,CONVERT(varchar(100), Clean3Time, 120) as Clean3Time,Clean3TimeBy,Clean4Qty
                            ,CONVERT(varchar(100), Clean4Time, 120) as Clean4Time,Clean4TimeBy,Clean5Qty,CONVERT(varchar(100), Clean5Time, 120) as Clean5Time,Clean5TimeBy
                            ,ChangeQty,ChangeTime,ChangeBy,CONVERT(varchar(100), CreateTime, 120) as CreateTime,CONVERT(varchar(100), UpdatedTime, 120) as UpdatedTime 
                            from MouldingMoldLife_His 
                            where 1=1 ");

            if (sMouldID != "")
            {
                strSql.Append(" and MoldID = @MoldID ");
            }

            if (sMachineID != "")
            {
                strSql.Append(" and MachineID = @MachineID ");
            }

            strSql.Append(" order by UpdatedTime desc  ");

            SqlParameter[] paras =
            {
                new SqlParameter("@MoldID",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar)
            };

            if (!string.IsNullOrEmpty(sMouldID))
                paras[0].Value = sMouldID;
            else
                paras[0] = null;

            if (!string.IsNullOrEmpty(sMachineID))
                paras[1].Value = sMachineID;
            else
                paras[1] = null;



            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        public SqlCommand UpdateCleanCMD(Common.Class.Model.MouldingMoldLife_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingMoldLife set ");
            strSql.Append("MoldID=@MoldID, ");
            strSql.Append("PartNumberAll=@PartNumberAll,");
            strSql.Append("Accumulate=@Accumulate,");

            strSql.Append("Clean1Qty=@Clean1Qty,");
            strSql.Append("Clean1Time=@Clean1Time,");
            strSql.Append("Clean2Qty=@Clean2Qty,");
            strSql.Append("Clean2Time=@Clean2Time,");
            strSql.Append("Clean3Qty=@Clean3Qty,");
            strSql.Append("Clean3Time=@Clean3Time,");
            strSql.Append("Clean4Qty=@Clean4Qty,");
            strSql.Append("Clean4Time=@Clean4Time,");
            strSql.Append("Clean5Qty=@Clean5Qty,");
            strSql.Append("Clean5Time=@Clean5Time,");
            strSql.Append("Clean1TimeBy=@Clean1TimeBy, ");
            strSql.Append("Clean2TimeBy=@Clean2TimeBy, ");
            strSql.Append("Clean3TimeBy=@Clean3TimeBy, ");
            strSql.Append("Clean4TimeBy=@Clean4TimeBy, ");
            strSql.Append("Clean5TimeBy=@Clean5TimeBy, ");

            strSql.Append("ChangeBy=@ChangeBy, ");
            strSql.Append("ChangeQty=@ChangeQty, ");
            strSql.Append("ChangeTime=@ChangeTime, ");
            strSql.Append("UpdatedTime=@UpdatedTime ");

            strSql.Append(" where ");
            strSql.Append(" MachineID=@MachineID ");

            SqlParameter[] parameters = {
                new SqlParameter("@MoldID", SqlDbType.VarChar),
                new SqlParameter("@PartNumberAll", SqlDbType.VarChar),
                new SqlParameter("@Accumulate", SqlDbType.Int),
                new SqlParameter("@Clean1Qty", SqlDbType.Int),
                new SqlParameter("@Clean1Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean2Qty", SqlDbType.Int),
                new SqlParameter("@Clean2Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean3Qty", SqlDbType.Int),
                new SqlParameter("@Clean3Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean4Qty", SqlDbType.Int),
                new SqlParameter("@Clean4Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean5Qty", SqlDbType.Int),
                new SqlParameter("@Clean5Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean1TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean2TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean3TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean4TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean5TimeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeQty", SqlDbType.Int),
                new SqlParameter("@ChangeTime", SqlDbType.DateTime2),
                new SqlParameter("@UpdatedTime", SqlDbType.DateTime2),
                new SqlParameter("@MachineID", SqlDbType.VarChar)
            };

            parameters[0].Value = model.MoldID == null ? (object)DBNull.Value : model.MoldID;
            parameters[1].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            parameters[2].Value = model.Accumulate == null ? (object)DBNull.Value : model.Accumulate;
            parameters[3].Value = model.Clean1Qty == null ? (object)DBNull.Value : model.Clean1Qty;
            parameters[4].Value = model.Clean1Time == null ? (object)DBNull.Value : model.Clean1Time;
            parameters[5].Value = model.Clean2Qty == null ? (object)DBNull.Value : model.Clean2Qty;
            parameters[6].Value = model.Clean2Time == null ? (object)DBNull.Value : model.Clean2Time;
            parameters[7].Value = model.Clean3Qty == null ? (object)DBNull.Value : model.Clean3Qty;
            parameters[8].Value = model.Clean3Time == null ? (object)DBNull.Value : model.Clean3Time;
            parameters[9].Value = model.Clean4Qty == null ? (object)DBNull.Value : model.Clean4Qty;
            parameters[10].Value = model.Clean4Time == null ? (object)DBNull.Value : model.Clean4Time;
            parameters[11].Value = model.Clean5Qty == null ? (object)DBNull.Value : model.Clean5Qty;
            parameters[12].Value = model.Clean5Time == null ? (object)DBNull.Value : model.Clean5Time;
            parameters[13].Value = model.Clean1TimeBy == null ? (object)DBNull.Value : model.Clean1TimeBy;
            parameters[14].Value = model.Clean2TimeBy == null ? (object)DBNull.Value : model.Clean2TimeBy;
            parameters[15].Value = model.Clean3TimeBy == null ? (object)DBNull.Value : model.Clean3TimeBy;
            parameters[16].Value = model.Clean4TimeBy == null ? (object)DBNull.Value : model.Clean4TimeBy;
            parameters[17].Value = model.Clean5TimeBy == null ? (object)DBNull.Value : model.Clean5TimeBy;
            parameters[18].Value = model.ChangeBy == null ? (object)DBNull.Value : model.ChangeBy;
            parameters[19].Value = model.ChangeQty == null ? (object)DBNull.Value : model.ChangeQty.ToString();
            parameters[20].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            parameters[21].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime;
            parameters[22].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);

        }

        public int UpdateClean(Common.Class.Model.MouldingMoldLife_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingMoldLife set ");

            strSql.Append("Clean1Qty=@Clean1Qty,");
            strSql.Append("Clean1Time=@Clean1Time,");
            strSql.Append("Clean2Qty=@Clean2Qty,");
            strSql.Append("Clean2Time=@Clean2Time,");
            strSql.Append("Clean3Qty=@Clean3Qty,");
            strSql.Append("Clean3Time=@Clean3Time,");
            strSql.Append("Clean4Qty=@Clean4Qty,");
            strSql.Append("Clean4Time=@Clean4Time,");
            strSql.Append("Clean5Qty=@Clean5Qty,");
            strSql.Append("Clean5Time=@Clean5Time,");

            strSql.Append("Clean1TimeBy=@Clean1TimeBy, ");
            strSql.Append("Clean2TimeBy=@Clean2TimeBy, ");
            strSql.Append("Clean3TimeBy=@Clean3TimeBy, ");
            strSql.Append("Clean4TimeBy=@Clean4TimeBy, ");
            strSql.Append("Clean5TimeBy=@Clean5TimeBy, ");

            strSql.Append("ChangeBy=@ChangeBy, ");
            strSql.Append("ChangeQty=@ChangeQty, ");
            strSql.Append("ChangeTime=@ChangeTime, ");
            strSql.Append("UpdatedTime=@UpdatedTime ");

            strSql.Append(" where ");
            strSql.Append(" MoldID=@MoldID ");

            SqlParameter[] parameters = {
                new SqlParameter("@Clean1Qty", SqlDbType.Int),
                new SqlParameter("@Clean1Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean2Qty", SqlDbType.Int),
                new SqlParameter("@Clean2Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean3Qty", SqlDbType.Int),
                new SqlParameter("@Clean3Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean4Qty", SqlDbType.Int),
                new SqlParameter("@Clean4Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean5Qty", SqlDbType.Int),
                new SqlParameter("@Clean5Time", SqlDbType.DateTime2),

                new SqlParameter("@Clean1TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean2TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean3TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean4TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean5TimeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeBy", SqlDbType.VarChar),

                new SqlParameter("@ChangeQty", SqlDbType.Int),
                new SqlParameter("@ChangeTime", SqlDbType.DateTime2),
                new SqlParameter("@UpdatedTime", SqlDbType.DateTime2),
                new SqlParameter("@MoldID", SqlDbType.VarChar)
            };
            
            parameters[0].Value = model.Clean1Qty == null ? (object)DBNull.Value : model.Clean1Qty;
            parameters[1].Value = model.Clean1Time == null ? (object)DBNull.Value : model.Clean1Time;
            parameters[2].Value = model.Clean2Qty == 0 ? (object)DBNull.Value : model.Clean2Qty.ToString();
            parameters[3].Value = model.Clean2Time == null ? (object)DBNull.Value : model.Clean2Time;
            parameters[4].Value = model.Clean3Qty == 0 ? (object)DBNull.Value : model.Clean3Qty.ToString();
            parameters[5].Value = model.Clean3Time == null ? (object)DBNull.Value : model.Clean3Time;
            parameters[6].Value = model.Clean4Qty == 0 ? (object)DBNull.Value : model.Clean4Qty.ToString();
            parameters[7].Value = model.Clean4Time == null ? (object)DBNull.Value : model.Clean4Time;
            parameters[8].Value = model.Clean5Qty == 0 ? (object)DBNull.Value : model.Clean5Qty.ToString();
            parameters[9].Value = model.Clean5Time == null ? (object)DBNull.Value : model.Clean5Time;

            parameters[10].Value = model.Clean1TimeBy == null ? (object)DBNull.Value : model.Clean1TimeBy;
            parameters[11].Value = model.Clean2TimeBy == null ? (object)DBNull.Value : model.Clean2TimeBy;
            parameters[12].Value = model.Clean3TimeBy == null ? (object)DBNull.Value : model.Clean3TimeBy;
            parameters[13].Value = model.Clean4TimeBy == null ? (object)DBNull.Value : model.Clean4TimeBy;
            parameters[14].Value = model.Clean5TimeBy == null ? (object)DBNull.Value : model.Clean5TimeBy;
            parameters[15].Value = model.ChangeBy == null ? (object)DBNull.Value : model.ChangeBy;


            parameters[16].Value = model.ChangeQty == 0 ? (object)DBNull.Value : model.ChangeQty.ToString();
            parameters[17].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            parameters[18].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime;
            parameters[19].Value = model.MoldID == null ? (object)DBNull.Value : model.MoldID;


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters,DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return rows;
        }




        public int UpdateMouldLife(string sMouldID, int iMouldLife)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingMoldLife set ");

            strSql.Append("MouldLife=@MouldLife, ");
            strSql.Append("UpdatedTime=@UpdatedTime ");

            strSql.Append(" where ");
            strSql.Append(" MoldID=@MoldID ");

            SqlParameter[] parameters = {
                new SqlParameter("@MouldLife", SqlDbType.Int),
                new SqlParameter("@UpdatedTime", SqlDbType.DateTime2),
                new SqlParameter("@MoldID", SqlDbType.VarChar,100)
            };

            parameters[0].Value = iMouldLife;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = sMouldID;
                                                   


            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return rows;
        }


        public SqlCommand UpdateChangeCMD(Common.Class.Model.MouldingMoldLife_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingMoldLife set ");
            strSql.Append("MoldID=@MoldID, ");
            strSql.Append("PartNumberAll=@PartNumberAll,");
            strSql.Append("Accumulate=@Accumulate,");

            strSql.Append("Clean1Qty=@Clean1Qty,");
            strSql.Append("Clean1Time=@Clean1Time,");
            strSql.Append("Clean2Qty=@Clean2Qty,");
            strSql.Append("Clean2Time=@Clean2Time,");
            strSql.Append("Clean3Qty=@Clean3Qty,");
            strSql.Append("Clean3Time=@Clean3Time,");
            strSql.Append("Clean4Qty=@Clean4Qty,");
            strSql.Append("Clean4Time=@Clean4Time,");
            strSql.Append("Clean5Qty=@Clean5Qty,");
            strSql.Append("Clean5Time=@Clean5Time,");
            strSql.Append("Clean1TimeBy=@Clean1TimeBy, ");
            strSql.Append("Clean2TimeBy=@Clean2TimeBy, ");
            strSql.Append("Clean3TimeBy=@Clean3TimeBy, ");
            strSql.Append("Clean4TimeBy=@Clean4TimeBy, ");
            strSql.Append("Clean5TimeBy=@Clean5TimeBy, ");

            strSql.Append("ChangeBy=@ChangeBy, ");
            strSql.Append("ChangeQty=@ChangeQty, ");
            strSql.Append("ChangeTime=@ChangeTime, ");
            strSql.Append("UpdatedTime=@UpdatedTime ");

            strSql.Append(" where ");
            strSql.Append(" MoldID=@MoldID ");

            SqlParameter[] parameters = {
                new SqlParameter("@MoldID", SqlDbType.VarChar),
                new SqlParameter("@PartNumberAll", SqlDbType.VarChar),
                new SqlParameter("@Accumulate", SqlDbType.Int),
                new SqlParameter("@Clean1Qty", SqlDbType.Int),
                new SqlParameter("@Clean1Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean2Qty", SqlDbType.Int),
                new SqlParameter("@Clean2Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean3Qty", SqlDbType.Int),
                new SqlParameter("@Clean3Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean4Qty", SqlDbType.Int),
                new SqlParameter("@Clean4Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean5Qty", SqlDbType.Int),
                new SqlParameter("@Clean5Time", SqlDbType.DateTime2),
                new SqlParameter("@Clean1TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean2TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean3TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean4TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean5TimeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeQty", SqlDbType.Int),
                new SqlParameter("@ChangeTime", SqlDbType.DateTime2),
                new SqlParameter("@UpdatedTime", SqlDbType.DateTime2),
                new SqlParameter("@MachineID", SqlDbType.VarChar)
            };

            parameters[0].Value = model.MoldID == null ? (object)DBNull.Value : model.MoldID;
            parameters[1].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            parameters[2].Value = model.Accumulate == null ? (object)DBNull.Value : model.Accumulate;
            parameters[3].Value = model.Clean1Qty == null ? (object)DBNull.Value : model.Clean1Qty;
            parameters[4].Value = model.Clean1Time == null ? (object)DBNull.Value : model.Clean1Time;
            parameters[5].Value = model.Clean2Qty == null ? (object)DBNull.Value : model.Clean2Qty;
            parameters[6].Value = model.Clean2Time == null ? (object)DBNull.Value : model.Clean2Time;
            parameters[7].Value = model.Clean3Qty == null ? (object)DBNull.Value : model.Clean3Qty;
            parameters[8].Value = model.Clean3Time == null ? (object)DBNull.Value : model.Clean3Time;
            parameters[9].Value = model.Clean4Qty == null ? (object)DBNull.Value : model.Clean4Qty;
            parameters[10].Value = model.Clean4Time == null ? (object)DBNull.Value : model.Clean4Time;
            parameters[11].Value = model.Clean5Qty == null ? (object)DBNull.Value : model.Clean5Qty;
            parameters[12].Value = model.Clean5Time == null ? (object)DBNull.Value : model.Clean5Time;
            parameters[13].Value = model.Clean1TimeBy == null ? (object)DBNull.Value : model.Clean1TimeBy;
            parameters[14].Value = model.Clean2TimeBy == null ? (object)DBNull.Value : model.Clean2TimeBy;
            parameters[15].Value = model.Clean3TimeBy == null ? (object)DBNull.Value : model.Clean3TimeBy;
            parameters[16].Value = model.Clean4TimeBy == null ? (object)DBNull.Value : model.Clean4TimeBy;
            parameters[17].Value = model.Clean5TimeBy == null ? (object)DBNull.Value : model.Clean5TimeBy;
            parameters[18].Value = model.ChangeBy == null ? (object)DBNull.Value : model.ChangeBy;
            parameters[19].Value = model.ChangeQty == null ? (object)DBNull.Value : model.ChangeQty.ToString();
            parameters[20].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            parameters[21].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime;
            parameters[22].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }



        public SqlCommand AddCMD(Common.Class.Model.MouldingMoldLife_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into MouldingMoldLife_His ");
            strSql.Append(" ( MoldID, MachineID,PartNumberAll,Accumulate,Clean1Qty,Clean1Time,Clean1TimeBy,Clean2Qty,Clean2Time,Clean2TimeBy,Clean3Qty,Clean3Time,Clean3TimeBy,Clean4Qty,Clean4Time,Clean4TimeBy,Clean5Qty,Clean5Time,Clean5TimeBy,ChangeQty,ChangeTime,ChangeBy,CreateTime,UpdatedTime) ");
            strSql.Append(" values ");
            strSql.Append(" (@MoldID, @MachineID,@PartNumberAll,@Accumulate,@Clean1Qty,@Clean1Time,@Clean1TimeBy,@Clean2Qty,@Clean2Time,@Clean2TimeBy,@Clean3Qty,@Clean3Time,@Clean3TimeBy,@Clean4Qty,@Clean4Time,@Clean4TimeBy,@Clean5Qty,@Clean5Time,@Clean5TimeBy,@ChangeQty,@ChangeTime,@ChangeBy,@CreateTime,@UpdatedTime) ");

            
            SqlParameter[] parameters = {
                new SqlParameter("@MachineID", SqlDbType.VarChar),
                new SqlParameter("@PartNumberAll", SqlDbType.VarChar),
                new SqlParameter("@Accumulate", SqlDbType.Int),
                new SqlParameter("@Clean1Qty", SqlDbType.Int),
                new SqlParameter("@Clean1Time", SqlDbType.DateTime),
                new SqlParameter("@Clean1TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean2Qty", SqlDbType.Int),
                new SqlParameter("@Clean2Time", SqlDbType.DateTime),
                new SqlParameter("@Clean2TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean3Qty", SqlDbType.Int),
                new SqlParameter("@Clean3Time", SqlDbType.DateTime),
                new SqlParameter("@Clean3TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean4Qty", SqlDbType.Int),
                new SqlParameter("@Clean4Time", SqlDbType.DateTime),
                new SqlParameter("@Clean4TimeBy", SqlDbType.VarChar),
                new SqlParameter("@Clean5Qty", SqlDbType.Int),
                new SqlParameter("@Clean5Time", SqlDbType.DateTime),
                new SqlParameter("@Clean5TimeBy", SqlDbType.VarChar),
                new SqlParameter("@ChangeQty", SqlDbType.Int),
                new SqlParameter("@ChangeTime", SqlDbType.DateTime),
                new SqlParameter("@ChangeBy", SqlDbType.VarChar),
                new SqlParameter("@CreateTime", SqlDbType.DateTime),
                new SqlParameter("@UpdatedTime", SqlDbType.DateTime),
                new SqlParameter("@MoldID",SqlDbType.VarChar)
            };

            parameters[0].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            parameters[1].Value = model.PartNumberAll == null ? (object)DBNull.Value : model.PartNumberAll;
            parameters[2].Value = model.Accumulate == null ? (object)DBNull.Value : model.Accumulate;
            parameters[3].Value = model.Clean1Qty == null ? (object)DBNull.Value : model.Clean1Qty;
            parameters[4].Value = model.Clean1Time == null ? (object)DBNull.Value : model.Clean1Time;
            parameters[5].Value = model.Clean1TimeBy == null ? (object)DBNull.Value : model.Clean1TimeBy;
            parameters[6].Value = model.Clean2Qty == null ? (object)DBNull.Value : model.Clean2Qty;
            parameters[7].Value = model.Clean2Time == null ? (object)DBNull.Value : model.Clean2Time;
            parameters[8].Value = model.Clean2TimeBy == null ? (object)DBNull.Value : model.Clean2TimeBy;
            parameters[9].Value = model.Clean3Qty == null ? (object)DBNull.Value : model.Clean3Qty;
            parameters[10].Value = model.Clean3Time == null ? (object)DBNull.Value : model.Clean3Time;
            parameters[11].Value = model.Clean3TimeBy == null ? (object)DBNull.Value : model.Clean3TimeBy;
            parameters[12].Value = model.Clean4Qty == null ? (object)DBNull.Value : model.Clean4Qty;
            parameters[13].Value = model.Clean4Time == null ? (object)DBNull.Value : model.Clean4Time;
            parameters[14].Value = model.Clean4TimeBy == null ? (object)DBNull.Value : model.Clean4TimeBy;
            parameters[15].Value = model.Clean5Qty == null ? (object)DBNull.Value : model.Clean5Qty;
            parameters[16].Value = model.Clean5Time == null ? (object)DBNull.Value : model.Clean5Time;
            parameters[17].Value = model.Clean5TimeBy == null ? (object)DBNull.Value : model.Clean5TimeBy;
            parameters[18].Value = model.ChangeQty == null ? (object)DBNull.Value : model.ChangeQty;
            parameters[19].Value = model.ChangeTime == null ? (object)DBNull.Value : model.ChangeTime;
            parameters[20].Value = model.ChangeBy == null ? (object)DBNull.Value : model.ChangeBy;
            parameters[21].Value = DateTime.Now;
            parameters[22].Value = DateTime.Now;
            parameters[23].Value = model.MoldID == null ? (object)DBNull.Value : model.MoldID;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);

        }

       
        


        


       

        
    }
}

