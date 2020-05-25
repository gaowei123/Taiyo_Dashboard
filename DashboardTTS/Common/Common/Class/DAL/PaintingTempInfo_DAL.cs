using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    /// <summary>
    /// 数据访问类:PaintingTempInfo
    /// </summary>
    public partial class PaintingTempInfo_DAL
    {
        public PaintingTempInfo_DAL()
        { }

        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Common.Class.Model.PaintingTempInfo_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintingTempInfo(");
            strSql.Append("jobNumber,lotNo,partNumber,materialName,lotQty,setupRejQty,qaTestQty,coat_1st,pMachine_1st,coat_2nd,pMachine_2nd,coat_3rd,pMachine_3rd,paintingDate,UCDate,TCDate,laserMachine,laserOperator,laserDate,MFGDate,createdTime,updatedTime)");
            strSql.Append(" values (");
            strSql.Append("@jobNumber,@lotNo,@partNumber,@materialName,@lotQty,@setupRejQty,@qaTestQty,@coat_1st,@pMachine_1st,@coat_2nd,@pMachine_2nd,@coat_3rd,@pMachine_3rd,@paintingDate,@UCDate,@TCDate,@laserMachine,@laserOperator,@laserDate,@MFGDate,@createdTime,@updatedTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@lotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@lotQty", SqlDbType.Decimal,9),
                    new SqlParameter("@setupRejQty", SqlDbType.Decimal,9),
                    new SqlParameter("@qaTestQty", SqlDbType.Decimal,9),
                    new SqlParameter("@coat_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingDate", SqlDbType.DateTime),
                    new SqlParameter("@UCDate", SqlDbType.DateTime),
                    new SqlParameter("@TCDate", SqlDbType.DateTime),
                    new SqlParameter("@laserMachine", SqlDbType.VarChar,50),
                    new SqlParameter("@laserOperator", SqlDbType.VarChar,50),
                    new SqlParameter("@laserDate", SqlDbType.DateTime),
                    new SqlParameter("@MFGDate", SqlDbType.DateTime),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime)};
            parameters[0].Value = model.jobNumber;
            parameters[1].Value = model.lotNo;
            parameters[2].Value = model.partNumber;
            parameters[3].Value = model.materialName;
            parameters[4].Value = model.lotQty;
            parameters[5].Value = model.setupRejQty;
            parameters[6].Value = model.qaTestQty;
            parameters[7].Value = model.coat_1st;
            parameters[8].Value = model.pMachine_1st;
            parameters[9].Value = model.coat_2nd;
            parameters[10].Value = model.pMachine_2nd;
            parameters[11].Value = model.coat_3rd;
            parameters[12].Value = model.pMachine_3rd;
            parameters[13].Value = model.paintingDate;
            parameters[14].Value = model.UCDate;
            parameters[15].Value = model.TCDate;
            parameters[16].Value = model.laserMachine;
            parameters[17].Value = model.laserOperator;
            parameters[18].Value = model.laserDate;
            parameters[19].Value = model.MFGDate;
            parameters[20].Value = model.createdTime;
            parameters[21].Value = model.updatedTime;

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public SqlCommand AddCommand(Common.Class.Model.PaintingTempInfo_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaintingTempInfo(");
            strSql.Append("jobNumber,lotNo,partNumber,materialName,lotQty,setupRejQty,qaTestQty,coat_1st,pMachine_1st,coat_2nd,pMachine_2nd,coat_3rd,pMachine_3rd,paintingDate,UCDate,TCDate,laserMachine,laserOperator,laserDate,MFGDate,createdTime,updatedTime,recordBy,paintingDate_1st,paintingDate_2nd,paintingDate_3rd,thickness_1st,thickness_2nd,thickness_3rd,paintLot_1st,paintLot_2nd,paintLot_3rd,thinnersLot_1st,thinnersLot_2nd,thinnersLot_3rd,paintingPIC_1st,paintingPIC_2nd,paintingPIC_3rd,paintingRunningTime_1st,paintingRunningTime_2nd,paintingRunningTime_3rd,paintingOvenTime_1st,paintingOvenTime_2nd,paintingOvenTime_3rd,CheckProcess,temperatureFront,temperatureRear,humidityFront,humidityRear )");
            strSql.Append(" values (");
            strSql.Append("@jobNumber,@lotNo,@partNumber,@materialName,@lotQty,@setupRejQty,@qaTestQty,@coat_1st,@pMachine_1st,@coat_2nd,@pMachine_2nd,@coat_3rd,@pMachine_3rd,@paintingDate,@UCDate,@TCDate,@laserMachine,@laserOperator,@laserDate,@MFGDate,@createdTime,@updatedTime,@recordBy,@paintingDate_1st,@paintingDate_2nd,@paintingDate_3rd,@thickness_1st,@thickness_2nd,@thickness_3rd,@paintLot_1st,@paintLot_2nd,@paintLot_3rd,@thinnersLot_1st,@thinnersLot_2nd,@thinnersLot_3rd,@paintingPIC_1st,@paintingPIC_2nd,@paintingPIC_3rd,@paintingRunningTime_1st,@paintingRunningTime_2nd,@paintingRunningTime_3rd,@paintingOvenTime_1st,@paintingOvenTime_2nd,@paintingOvenTime_3rd, @CheckProcess,@temperatureFront,@temperatureRear,@humidityFront,@humidityRear )");
            SqlParameter[] parameters = {
                    new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@lotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@lotQty", SqlDbType.Decimal,9),
                    new SqlParameter("@setupRejQty", SqlDbType.Decimal,9),
                    new SqlParameter("@qaTestQty", SqlDbType.Decimal,9),
                    new SqlParameter("@coat_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingDate", SqlDbType.DateTime),
                    new SqlParameter("@UCDate", SqlDbType.DateTime),
                    new SqlParameter("@TCDate", SqlDbType.DateTime),
                    new SqlParameter("@laserMachine", SqlDbType.VarChar,50),
                    new SqlParameter("@laserOperator", SqlDbType.VarChar,50),
                    new SqlParameter("@laserDate", SqlDbType.DateTime),
                    new SqlParameter("@MFGDate", SqlDbType.DateTime),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime),

                    new SqlParameter("@recordBy", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingDate_1st", SqlDbType.DateTime),
                    new SqlParameter("@paintingDate_2nd", SqlDbType.DateTime),
                    new SqlParameter("@paintingDate_3rd", SqlDbType.DateTime),
                    new SqlParameter("@thickness_1st", SqlDbType.Decimal),
                    new SqlParameter("@thickness_2nd", SqlDbType.Decimal),
                    new SqlParameter("@thickness_3rd", SqlDbType.Decimal),

                    new SqlParameter("@paintLot_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@paintLot_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintLot_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@thinnersLot_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@thinnersLot_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@thinnersLot_3rd", SqlDbType.VarChar,50),

                    new SqlParameter("@paintingPIC_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingPIC_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingPIC_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingRunningTime_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingRunningTime_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingRunningTime_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingOvenTime_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingOvenTime_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingOvenTime_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@CheckProcess",SqlDbType.VarChar,50),
        
                    new SqlParameter("@temperatureFront", SqlDbType.Decimal),
                    new SqlParameter("@temperatureRear", SqlDbType.Decimal),
                    new SqlParameter("@humidityFront", SqlDbType.Decimal),
                    new SqlParameter("@humidityRear",SqlDbType.Decimal)

            };





            parameters[0].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber;
            parameters[1].Value = model.lotNo == null ? (object)DBNull.Value : model.lotNo;
            parameters[2].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[3].Value = model.materialName == null ? (object)DBNull.Value : model.materialName;
            parameters[4].Value = model.lotQty == null ? (object)DBNull.Value : model.lotQty;
            parameters[5].Value = model.setupRejQty == null ? (object)DBNull.Value : model.setupRejQty;
            parameters[6].Value = model.qaTestQty == null ? (object)DBNull.Value : model.qaTestQty;
            parameters[7].Value = model.coat_1st == null ? (object)DBNull.Value : model.coat_1st;
            parameters[8].Value = model.pMachine_1st == null ? (object)DBNull.Value : model.pMachine_1st;
            parameters[9].Value = model.coat_2nd == null ? (object)DBNull.Value : model.coat_2nd;
            parameters[10].Value = model.pMachine_2nd == null ? (object)DBNull.Value : model.pMachine_2nd;
            parameters[11].Value = model.coat_3rd == null ? (object)DBNull.Value : model.coat_3rd;
            parameters[12].Value = model.pMachine_3rd == null ? (object)DBNull.Value : model.pMachine_3rd;
            parameters[13].Value = model.paintingDate == null ? (object)DBNull.Value : model.paintingDate;
            parameters[14].Value = model.UCDate == null ? (object)DBNull.Value : model.UCDate;
            parameters[15].Value = model.TCDate == null ? (object)DBNull.Value : model.TCDate;
            parameters[16].Value = model.laserMachine == null ? (object)DBNull.Value : model.laserMachine;
            parameters[17].Value = model.laserOperator == null ? (object)DBNull.Value : model.laserOperator;
            parameters[18].Value = model.laserDate == null ? (object)DBNull.Value : model.laserDate;
            parameters[19].Value = model.MFGDate == null ? (object)DBNull.Value : model.MFGDate;
            parameters[20].Value = model.createdTime == null ? (object)DBNull.Value : model.createdTime;
            parameters[21].Value = model.updatedTime == null ? (object)DBNull.Value : model.updatedTime;
            parameters[22].Value = model.recordBy == null ? (object)DBNull.Value : model.recordBy;

            parameters[23].Value = model.paintingDate_1st == null ? (object)DBNull.Value : model.paintingDate_1st;
            parameters[24].Value = model.paintingDate_2nd == null ? (object)DBNull.Value : model.paintingDate_2nd;
            parameters[25].Value = model.paintingDate_3rd == null ? (object)DBNull.Value : model.paintingDate_3rd;
            parameters[26].Value = model.thickness_1st == null ? (object)DBNull.Value : model.thickness_1st;
            parameters[27].Value = model.thickness_2nd == null ? (object)DBNull.Value : model.thickness_2nd;
            parameters[28].Value = model.thickness_3rd == null ? (object)DBNull.Value : model.thickness_3rd;
            parameters[29].Value = model.paintLot_1st == null ? (object)DBNull.Value : model.paintLot_1st;
            parameters[30].Value = model.paintLot_2nd == null ? (object)DBNull.Value : model.paintLot_2nd;
            parameters[31].Value = model.paintLot_3rd == null ? (object)DBNull.Value : model.paintLot_3rd;
            parameters[32].Value = model.thinnersLot_1st == null ? (object)DBNull.Value : model.thinnersLot_1st;
            parameters[33].Value = model.thinnersLot_2nd == null ? (object)DBNull.Value : model.thinnersLot_2nd;
            parameters[34].Value = model.thinnersLot_3rd == null ? (object)DBNull.Value : model.thinnersLot_3rd;
            parameters[35].Value = model.paintingPIC_1st == null ? (object)DBNull.Value : model.paintingPIC_1st;
            parameters[36].Value = model.paintingPIC_2nd == null ? (object)DBNull.Value : model.paintingPIC_2nd;
            parameters[37].Value = model.paintingPIC_3rd == null ? (object)DBNull.Value : model.paintingPIC_3rd;
            parameters[38].Value = model.paintingRunningTime_1st == null ? (object)DBNull.Value : model.paintingRunningTime_1st;
            parameters[39].Value = model.paintingRunningTime_2nd == null ? (object)DBNull.Value : model.paintingRunningTime_2nd;
            parameters[40].Value = model.paintingRunningTime_3rd == null ? (object)DBNull.Value : model.paintingRunningTime_3rd;
            parameters[41].Value = model.paintingOvenTime_1st == null ? (object)DBNull.Value : model.paintingOvenTime_1st;
            parameters[42].Value = model.paintingOvenTime_2nd == null ? (object)DBNull.Value : model.paintingOvenTime_2nd;
            parameters[43].Value = model.paintingOvenTime_3rd == null ? (object)DBNull.Value : model.paintingOvenTime_3rd;
            parameters[44].Value = model.checkProcess == null ? (object)DBNull.Value : model.checkProcess;


            parameters[45].Value = model.temperateFront == null ? (object)DBNull.Value : model.temperateFront;
            parameters[46].Value = model.temperateRear == null ? (object)DBNull.Value : model.temperateRear;
            parameters[47].Value = model.humidityFront == null ? (object)DBNull.Value : model.humidityFront;
            parameters[48].Value = model.humidityRear == null ? (object)DBNull.Value : model.humidityRear;





            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PaintingTempInfo_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaintingTempInfo set ");
            strSql.Append("jobNumber=@jobNumber,");
            strSql.Append("lotNo=@lotNo,");
            strSql.Append("partNumber=@partNumber,");
            strSql.Append("materialName=@materialName,");
            strSql.Append("lotQty=@lotQty,");
            strSql.Append("setupRejQty=@setupRejQty,");
            strSql.Append("qaTestQty=@qaTestQty,");
            strSql.Append("coat_1st=@coat_1st,");
            strSql.Append("pMachine_1st=@pMachine_1st,");
            strSql.Append("coat_2nd=@coat_2nd,");
            strSql.Append("pMachine_2nd=@pMachine_2nd,");
            strSql.Append("coat_3rd=@coat_3rd,");
            strSql.Append("pMachine_3rd=@pMachine_3rd,");
            strSql.Append("paintingDate=@paintingDate,");
            strSql.Append("UCDate=@UCDate,");
            strSql.Append("TCDate=@TCDate,");
            strSql.Append("laserMachine=@laserMachine,");
            strSql.Append("laserOperator=@laserOperator,");
            strSql.Append("laserDate=@laserDate,");
            strSql.Append("MFGDate=@MFGDate,");
            strSql.Append("createdTime=@createdTime,");
            strSql.Append("updatedTime=@updatedTime");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@lotNo", SqlDbType.VarChar,50),
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@materialName", SqlDbType.VarChar,50),
                    new SqlParameter("@lotQty", SqlDbType.Decimal,9),
                    new SqlParameter("@setupRejQty", SqlDbType.Decimal,9),
                    new SqlParameter("@qaTestQty", SqlDbType.Decimal,9),
                    new SqlParameter("@coat_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_1st", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_2nd", SqlDbType.VarChar,50),
                    new SqlParameter("@coat_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@pMachine_3rd", SqlDbType.VarChar,50),
                    new SqlParameter("@paintingDate", SqlDbType.DateTime),
                    new SqlParameter("@UCDate", SqlDbType.DateTime),
                    new SqlParameter("@TCDate", SqlDbType.DateTime),
                    new SqlParameter("@laserMachine", SqlDbType.VarChar,50),
                    new SqlParameter("@laserOperator", SqlDbType.VarChar,50),
                    new SqlParameter("@laserDate", SqlDbType.DateTime),
                    new SqlParameter("@MFGDate", SqlDbType.DateTime),
                    new SqlParameter("@createdTime", SqlDbType.DateTime),
                    new SqlParameter("@updatedTime", SqlDbType.DateTime)};
            parameters[0].Value = model.jobNumber;
            parameters[1].Value = model.lotNo;
            parameters[2].Value = model.partNumber;
            parameters[3].Value = model.materialName;
            parameters[4].Value = model.lotQty;
            parameters[5].Value = model.setupRejQty;
            parameters[6].Value = model.qaTestQty;
            parameters[7].Value = model.coat_1st;
            parameters[8].Value = model.pMachine_1st;
            parameters[9].Value = model.coat_2nd;
            parameters[10].Value = model.pMachine_2nd;
            parameters[11].Value = model.coat_3rd;
            parameters[12].Value = model.pMachine_3rd;
            parameters[13].Value = model.paintingDate;
            parameters[14].Value = model.UCDate;
            parameters[15].Value = model.TCDate;
            parameters[16].Value = model.laserMachine;
            parameters[17].Value = model.laserOperator;
            parameters[18].Value = model.laserDate;
            parameters[19].Value = model.MFGDate;
            parameters[20].Value = model.createdTime;
            parameters[21].Value = model.updatedTime;

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PaintingTempInfo ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
            };

            int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
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
        /// 得到一个对象实体
        /// </summary>
        public Common.Class.Model.PaintingTempInfo_Model GetModel(string sJobNumber)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 jobNumber,lotNo,partNumber,materialName,lotQty,setupRejQty,qaTestQty,coat_1st,pMachine_1st,coat_2nd,pMachine_2nd,coat_3rd,pMachine_3rd,paintingDate,UCDate,TCDate,laserMachine,laserOperator,laserDate,MFGDate,createdTime,updatedTime, recordBy from PaintingTempInfo ");
            strSql.Append(" where 1=1 ");

            strSql.Append(" and jobNumber = @jobNumber ");



            SqlParameter[] parameters = {
                new SqlParameter("@jobNumber",SqlDbType.VarChar,50)
            };

            parameters[0].Value = sJobNumber;

            Common.Class.Model.PaintingTempInfo_Model model = new Common.Class.Model.PaintingTempInfo_Model();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Class.Model.PaintingTempInfo_Model DataRowToModel(DataRow row)
        {
            Common.Class.Model.PaintingTempInfo_Model model = new Common.Class.Model.PaintingTempInfo_Model();
            if (row != null)
            {
                if (row["jobNumber"] != null)
                {
                    model.jobNumber = row["jobNumber"].ToString();
                }
                if (row["lotNo"] != null)
                {
                    model.lotNo = row["lotNo"].ToString();
                }
                if (row["partNumber"] != null)
                {
                    model.partNumber = row["partNumber"].ToString();
                }
                if (row["materialName"] != null)
                {
                    model.materialName = row["materialName"].ToString();
                }
                if (row["lotQty"] != null && row["lotQty"].ToString() != "")
                {
                    model.lotQty = decimal.Parse(row["lotQty"].ToString());
                }
                if (row["setupRejQty"] != null && row["setupRejQty"].ToString() != "")
                {
                    model.setupRejQty = decimal.Parse(row["setupRejQty"].ToString());
                }
                if (row["qaTestQty"] != null && row["qaTestQty"].ToString() != "")
                {
                    model.qaTestQty = decimal.Parse(row["qaTestQty"].ToString());
                }
                if (row["coat_1st"] != null)
                {
                    model.coat_1st = row["coat_1st"].ToString();
                }
                if (row["pMachine_1st"] != null)
                {
                    model.pMachine_1st = row["pMachine_1st"].ToString();
                }
                if (row["coat_2nd"] != null)
                {
                    model.coat_2nd = row["coat_2nd"].ToString();
                }
                if (row["pMachine_2nd"] != null)
                {
                    model.pMachine_2nd = row["pMachine_2nd"].ToString();
                }
                if (row["coat_3rd"] != null)
                {
                    model.coat_3rd = row["coat_3rd"].ToString();
                }
                if (row["pMachine_3rd"] != null)
                {
                    model.pMachine_3rd = row["pMachine_3rd"].ToString();
                }
                if (row["paintingDate"] != null && row["paintingDate"].ToString() != "")
                {
                    model.paintingDate = DateTime.Parse(row["paintingDate"].ToString());
                }
                if (row["UCDate"] != null && row["UCDate"].ToString() != "")
                {
                    model.UCDate = DateTime.Parse(row["UCDate"].ToString());
                }
                if (row["TCDate"] != null && row["TCDate"].ToString() != "")
                {
                    model.TCDate = DateTime.Parse(row["TCDate"].ToString());
                }
                if (row["laserMachine"] != null)
                {
                    model.laserMachine = row["laserMachine"].ToString();
                }
                if (row["laserOperator"] != null)
                {
                    model.laserOperator = row["laserOperator"].ToString();
                }
                if (row["laserDate"] != null && row["laserDate"].ToString() != "")
                {
                    model.laserDate = DateTime.Parse(row["laserDate"].ToString());
                }
                if (row["MFGDate"] != null && row["MFGDate"].ToString() != "")
                {
                    model.MFGDate = DateTime.Parse(row["MFGDate"].ToString());
                }
                if (row["createdTime"] != null && row["createdTime"].ToString() != "")
                {
                    model.createdTime = DateTime.Parse(row["createdTime"].ToString());
                }
                if (row["updatedTime"] != null && row["updatedTime"].ToString() != "")
                {
                    model.updatedTime = DateTime.Parse(row["updatedTime"].ToString());
                }

                if (row["recordBy"] != null && row["recordBy"].ToString() != "")
                {
                    model.recordBy = row["recordBy"].ToString();
                }


                if (row["thickness_1st"] != null && row["thickness_1st"].ToString() != "")
                {
                    model.thickness_1st = decimal.Parse(row["thickness_1st"].ToString());
                }
                if (row["thickness_2nd"] != null && row["thickness_2nd"].ToString() != "")
                {
                    model.thickness_2nd = decimal.Parse(row["thickness_2nd"].ToString());
                }
                if (row["thickness_3rd"] != null && row["thickness_3rd"].ToString() != "")
                {
                    model.thickness_3rd = decimal.Parse(row["thickness_3rd"].ToString());
                }

            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sJobnumber,  string sCheckProcess)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                            jobNumber
                            ,lotNo
                            ,partNumber
                            ,materialName as materialPartNo 
                            ,lotQty
                            ,setupRejQty
                            ,qaTestQty
                            ,coat_1st
                            ,case when pMachine_1st = '' then '' else 'MC' + pMachine_1st end as pMachine_1st
                            ,coat_2nd
                            ,case when pMachine_2nd = '' then '' else 'MC' + pMachine_2nd end as pMachine_2nd
                            ,coat_3rd
                            ,case when pMachine_3rd = '' then '' else 'MC' + pMachine_3rd end as pMachine_3rd
                            ,convert(varchar(50),paintingDate ,103) as  paintingDate
                            ,convert(varchar(50),UCDate ,103) as UCDate
                            ,convert(varchar(50),TCDate ,103) as TCDate
                            ,case when laserMachine = ''then '' else 'MC' + laserMachine end as laserMachine
                            ,laserOperator
                            ,convert(varchar(50),laserDate ,103) as laserDate
                            ,convert(varchar(50),MFGDate ,103) as MFGDate
                            ,createdTime
                            ,updatedTime
                            ,recordBy 
                            ,case when lotQty = 0 then '0.00%' else convert(varchar, convert(numeric(18,2), setupRejQty/lotQty * 100)) + '%' end  as setupRejRate
                            ,case when lotQty = 0 then '0.00%' else convert(varchar(50), convert(numeric(18,2),qaTestQty/lotQty * 100)) + '%' end as qaTestQtyRate
                            ,paintingDate_1st
                            ,paintingDate_2nd
                            ,paintingDate_3rd
                            ,thickness_1st
                            ,thickness_2nd
                            ,thickness_3rd



            
                            from PaintingTempInfo  where 1=1 ");


            if (sJobnumber.Trim() != "")
            {
                strSql.Append(" and jobNumber = @jobNumber ");
            }
            if (dDateFrom != null)
            {
                strSql.Append(" and createdTime >= @DateFrom ");
            }
            if (dDateTo != null)
            {
                strSql.Append(" and createdTime < @DateTo ");
            }

            if (sCheckProcess.Trim() != "")
            {
                strSql.Append(" and UPPER( CheckProcess ) =UPPER( @CheckProcess ) ");
            }

            if (sPartNo != "")
            {
                strSql.Append(" and partNumber = @partNumber ");
            }
            

            SqlParameter[] paras =
             {
                new SqlParameter("@jobNumber",SqlDbType.VarChar,50),
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@CheckProcess",SqlDbType.VarChar , 32),
                new SqlParameter("@partNumber",SqlDbType.VarChar)
            };

            if (sJobnumber.Trim() != "") paras[0].Value = sJobnumber; else paras[0] = null;
            if (dDateFrom != null) paras[1].Value = dDateFrom; else paras[1] = null;
            if (dDateTo != null) paras[2].Value = dDateTo; else paras[2] = null;
            if (sCheckProcess.Trim() != "") paras[3].Value = sCheckProcess; else paras[3] = null;
            if (sPartNo != "") paras[4].Value = sPartNo; else paras[4] = null;


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" jobNumber,lotNo,partNumber,materialName,lotQty,setupRejQty,qaTestQty,coat_1st,pMachine_1st,coat_2nd,pMachine_2nd,coat_3rd,pMachine_3rd,paintingDate,UCDate,TCDate,laserMachine,laserOperator,laserDate,MFGDate,createdTime,updatedTime ");
            strSql.Append(" FROM PaintingTempInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DBHelp.SqlDB.Query(strSql.ToString());
        }







        #endregion  BasicMethod




        public DataSet GetPaintTempInfoForButtonReport_NEW(DateTime dDateFrom, DateTime dDateTo, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select
                            jobNumber as JobID
                            ,lotNo
                            ,MFGDate
                            ,ISNULL(setupRejQty,0) as setupRejQty
                            ,ISNULL(qaTestQty, 0) as qaTestQty

                            ,paintingDate_1st
                            ,coat_1st
                            ,pMachine_1st

                            ,paintingDate_2nd
                            ,coat_2nd
                            ,pMachine_2nd

                            ,paintingDate_3rd
                            ,coat_3rd
                            ,pMachine_3rd

                            from PaintingTempInfo

                            where createdTime >= @dateFrom
                            and createdTime < @dateTo ");

            if (sJobNo != "")
            {
                strSql.Append(" and jobnumber = @JobNo ");
            }

            SqlParameter[] paras =
           {
                new SqlParameter("@dateFrom",SqlDbType.DateTime),
                new SqlParameter("@dateTo",SqlDbType.DateTime),
                new SqlParameter("@JobNo",SqlDbType.VarChar , 50)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sJobNo != "")
            { paras[2].Value = sJobNo; } else
            {
                paras[2] = null;
            }



                return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }



       


        public DataSet GetBuyoffList(DateTime dDateFrom, DateTime dDateTo,string sPartNo, string sJobNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select  distinct
	jobnumber
    ,materialName
	,Lotno
	,MFGDate
	,convert(varchar(100), MFGDate  ,103) as [MFG Date] 
	,convert(varchar(100), paintingDate_1st  ,103) as [Painting Under Coat Date]
	,paintingRunningTime_1st
	,paintingOvenTime_1st 
	,paintLot_1st 
	,thinnersLot_1st 
	,thickness_1st 
	,paintingPIC_1st 
    ,pMachine_1st

	,convert(varchar(100), paintingDate_2nd  ,103) as [Painting Middle Coat Date]  
	,paintingRunningTime_2nd 
	,paintingOvenTime_2nd 
	,paintLot_2nd  
	,thinnersLot_2nd 
	,thickness_2nd 
	,paintingPIC_2nd 
    ,pMachine_2nd

	,convert(varchar(100), paintingDate_3rd  ,103) as [Painting Top Coat Date]
	,paintingRunningTime_3rd
	,paintingOvenTime_3rd 
	,paintLot_3rd
	,thinnersLot_3rd
	,thickness_3rd
	,paintingPIC_3rd
    ,pMachine_3rd


    ,paintingDate_1st
	,paintingDate_2nd
	,paintingDate_3rd

    ,case when [temperatureFront] = 0 then '' else convert(varchar, temperatureFront) end as temperatureFront
    ,case when [temperatureRear] = 0 then '' else convert(varchar,temperatureRear) end as temperatureRear
    ,case when [humidityFront] = 0 then '' else convert(varchar,humidityFront) end as humidityFront
    ,case when [humidityRear] = 0 then '' else convert(varchar,humidityRear) end as humidityRear

    ,createdTime

    


from  paintingtempinfo where 1=1 and createdtime >= @DateFrom and createdtime < @DateTo ");





            if (sPartNo != "") strSql.Append(" and partNumber = @partNumber ");
            if (sJobNo.Trim() != "") strSql.Append(" and jobNumber = @jobNumber ");




            SqlParameter[] paras =
             {
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@jobNumber",SqlDbType.VarChar,50)              
            };  

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sPartNo != "") paras[2].Value = sPartNo; else paras[2] = null;
            if (sJobNo.Trim() != "") paras[3].Value = sJobNo; else paras[3] = null;
          


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Painting_Server);
        }


    }
}

