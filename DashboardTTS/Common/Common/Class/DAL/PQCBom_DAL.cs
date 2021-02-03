
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:PQCBom_DAL
	/// </summary>
	public class PQCBom_DAL
	{
		public PQCBom_DAL()
		{}



		public int Add(Common.Class.Model.PQCBom_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PQCBom(");
			strSql.Append(@"partNumber,
                            customer,
                            model,
                            jigNo,
                            cavityCount,
                            cycleTime,
                            blockCount,
                            unitCount,
                            machineID,
                            userName,
                            dateTime,
                            color,
                            processes,
                            remark_1,
                            remark_2,
                            remark_3,
                            remark_4,
                            remarks)");
			strSql.Append(" values (");
			strSql.Append(@"@partNumber,
                            @customer,
                            @model,
                            @jigNo,
                            @cavityCount,
                            @cycleTime,
                            @blockCount,
                            @unitCount,
                            @machineID,
                            @userName,
                            @dateTime,
                            @color,
                            @processes,
                            @remark_1,
                            @remark_2,
                            @remark_3,
                            @remark_4,
                            @remarks)");
			SqlParameter[] parameters = {
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@customer", SqlDbType.VarChar,50),
					new SqlParameter("@model", SqlDbType.VarChar,50),
					new SqlParameter("@jigNo", SqlDbType.VarChar,50),
					new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
					new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
					new SqlParameter("@blockCount", SqlDbType.Int,4),
					new SqlParameter("@unitCount", SqlDbType.Int,4),
					new SqlParameter("@machineID", SqlDbType.VarChar,8),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@color", SqlDbType.VarChar, 50),
                    new SqlParameter("@processes", SqlDbType.VarChar, 200),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar,20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500)};
            DBHelp.Reports.LogFile.DebugLog(@"AUTOCODE", "NameSpace:Common.DAL", 
                "Class: PQCBom_DAL",
                "Function: public int Add(Common.Class.Model.PQCBom_Model model)" +
                "TableName: PQCBom",
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) +
                ";blockCount = " + (model.blockCount == null ? "" : model.blockCount.ToString()) +
                ";unitCount = " + (model.unitCount == null ? "" : model.unitCount.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";remark_3 = " + (model.remark_3 == null ? "" : model.remark_3.ToString()) +
                ";remark_4 = " + (model.remark_4 == null ? "" : model.remark_4.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");
			parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[1].Value = model.customer == null ? (object)DBNull.Value : model.customer ;
			parameters[2].Value = model.model == null ? (object)DBNull.Value : model.model ;
			parameters[3].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo ;
			parameters[4].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount ;
			parameters[5].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime ;
			parameters[6].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount ;
			parameters[7].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount ;
			parameters[8].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[9].Value = model.userName == null ? (object)DBNull.Value : model.userName ;
			parameters[10].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[11].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[12].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[13].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[14].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[15].Value = model.remark_3 == null ? (object)DBNull.Value : model.remark_3;
            parameters[16].Value = model.remark_4 == null ? (object)DBNull.Value : model.remark_4;
            parameters[17].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
		}
        		
		public SqlCommand AddCommand(Common.Class.Model.PQCBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PQCBom(");
            strSql.Append(@"partNumber,
                            customer,
                            model,
                            jigNo,
                            cavityCount,
                            cycleTime,
                            blockCount,
                            unitCount,
                            machineID,
                            userName,
                            dateTime,
                            color,
                            processes,
                            remark_1,
                            remark_2,
                            remark_3,
                            remark_4,
                            remarks,
                            shipTo,
                            Type,
                            Description,
                            Coating,
                            Number,
                            UnitCost)");

            strSql.Append(" values (");

            strSql.Append(@"@partNumber,
                            @customer,
                            @model,
                            @jigNo,
                            @cavityCount,
                            @cycleTime,
                            @blockCount,
                            @unitCount,
                            @machineID,
                            @userName,
                            @dateTime,
                            @color,
                            @processes,
                            @remark_1,
                            @remark_2,
                            @remark_3,
                            @remark_4,
                            @remarks,
                            @shipTo,
                            @Type,
                            @Description,
                            @Coating,
                            @Number,
                            @UnitCost)");
            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@blockCount", SqlDbType.Int,4),
                    new SqlParameter("@unitCount", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@color", SqlDbType.VarChar, 50),
                    new SqlParameter("@processes", SqlDbType.VarChar, 200),
                    new SqlParameter("@remark_1", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar,20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar,20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,500),
                    new SqlParameter("@Type", SqlDbType.VarChar,500),
                    new SqlParameter("@Description", SqlDbType.VarChar,50),
                    new SqlParameter("@Coating", SqlDbType.VarChar,50),
                    new SqlParameter("@Number", SqlDbType.VarChar,50),
                    new SqlParameter("@UnitCost", SqlDbType.Decimal)
            };

          
            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[2].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[3].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[4].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[5].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[6].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[7].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[8].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[9].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[10].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[11].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[12].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[13].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[14].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[15].Value = model.remark_3 == null ? (object)DBNull.Value : model.remark_3;
            parameters[16].Value = model.remark_4 == null ? (object)DBNull.Value : model.remark_4;
            parameters[17].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[18].Value = model.ShipTo == null ? (object)DBNull.Value : model.ShipTo;
            parameters[19].Value = model.Type == null ? (object)DBNull.Value : model.Type;
            parameters[20].Value = model.Description == null ? (object)DBNull.Value : model.Description;
            parameters[21].Value = model.Coating == null ? (object)DBNull.Value : model.Coating;
            parameters[22].Value = model.Number == null ? (object)DBNull.Value : model.Number;
            parameters[23].Value = model.UnitCost == null ? (object)DBNull.Value : model.UnitCost;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
  
		public bool Update(Common.Class.Model.PQCBom_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PQCBom set ");
			strSql.Append("partNumber=@partNumber,");
			strSql.Append("customer=@customer,");
			strSql.Append("model=@model,");
			strSql.Append("jigNo=@jigNo,");
			strSql.Append("cavityCount=@cavityCount,");
			strSql.Append("cycleTime=@cycleTime,");
			strSql.Append("blockCount=@blockCount,");
			strSql.Append("unitCount=@unitCount,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("userName=@userName,");
			strSql.Append("dateTime=@dateTime,");
            strSql.Append("color=@color");
            strSql.Append("processes=@processes");
            strSql.Append("remark_1=@remark_1");
            strSql.Append("remark_2=@remark_2");
            strSql.Append("remark_3=@remark_3");
            strSql.Append("remark_4=@remark_4");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@blockCount", SqlDbType.Int,4),
                    new SqlParameter("@unitCount", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@color", SqlDbType.VarChar, 50),
                    new SqlParameter("@processes", SqlDbType.VarChar, 200),
                    new SqlParameter("@remark_1", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar, 20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500)};

            DBHelp.Reports.LogFile.DebugLog(@"AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCBom_DAL",
                "Function:		public bool Update(Common.Class.Model.PQCBom_Model model)" +
                "TableName:PQCBom",
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) +
                ";blockCount = " + (model.blockCount == null ? "" : model.blockCount.ToString()) +
                ";unitCount = " + (model.unitCount == null ? "" : model.unitCount.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";remark_3 = " + (model.remark_3 == null ? "" : model.remark_3.ToString()) +
                ";remark_4 = " + (model.remark_4 == null ? "" : model.remark_4.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");

            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[2].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[3].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[4].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[5].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[6].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[7].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[8].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[9].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[10].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[11].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[12].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[13].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[14].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[15].Value = model.remark_3 == null ? (object)DBNull.Value : model.remark_3;
            parameters[16].Value = model.remark_4 == null ? (object)DBNull.Value : model.remark_4;
            parameters[17].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;

            int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        
		public SqlCommand UpdateCommand(Common.Class.Model.PQCBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PQCBom set ");
            strSql.Append("customer=@customer,");
            strSql.Append("model=@model,");
            strSql.Append("jigNo=@jigNo,");
            strSql.Append("cavityCount=@cavityCount,");
            strSql.Append("cycleTime=@cycleTime,");
            strSql.Append("blockCount=@blockCount,");
            strSql.Append("unitCount=@unitCount,");
            strSql.Append("machineID=@machineID,");
            strSql.Append("userName=@userName,");
            strSql.Append("dateTime=@dateTime,");
            strSql.Append("color=@color,");
            strSql.Append("processes=@processes,");
            strSql.Append("remark_1=@remark_1,");
            strSql.Append("remark_2=@remark_2,");
            strSql.Append("remark_3=@remark_3,");
            strSql.Append("remark_4=@remark_4,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("shipTo=@shipTo,");
            strSql.Append("type=@type,");
            strSql.Append("description=@description,");
            strSql.Append("coating=@coating , ");
            strSql.Append("number=@number , ");
            strSql.Append("unitCost=@unitCost ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and partNumber=@partNumber ");
            

            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@customer", SqlDbType.VarChar,50),
                    new SqlParameter("@model", SqlDbType.VarChar,50),
                    new SqlParameter("@jigNo", SqlDbType.VarChar,50),
                    new SqlParameter("@cavityCount", SqlDbType.Decimal,9),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal,9),
                    new SqlParameter("@blockCount", SqlDbType.Int,4),
                    new SqlParameter("@unitCount", SqlDbType.Int,4),
                    new SqlParameter("@machineID", SqlDbType.VarChar,8),
                    new SqlParameter("@userName", SqlDbType.VarChar,50),
                    new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@color", SqlDbType.VarChar, 50),
                    new SqlParameter("@processes", SqlDbType.VarChar, 200),
                    new SqlParameter("@remark_1", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_2", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_3", SqlDbType.VarChar, 20),
                    new SqlParameter("@remark_4", SqlDbType.VarChar, 20),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@shipTo", SqlDbType.VarChar,500),
                    new SqlParameter("@type", SqlDbType.VarChar,500),
                    new SqlParameter("@description", SqlDbType.VarChar,50),
                    new SqlParameter("@coating", SqlDbType.VarChar,50),
                    new SqlParameter("@number", SqlDbType.VarChar,50),
                    new SqlParameter("@unitCost", SqlDbType.Decimal)
            };

            DBHelp.Reports.LogFile.DebugLog(@"AUTOCODE", "NameSpace:Common.DAL",
                "Class:PQCBom_DAL",
                "Function:		public SqlCommand Update(Common.Class.Model.PQCBom_Model model)" +
                "TableName:PQCBom",
                ";partNumber = " + (model.partNumber == null ? "" : model.partNumber.ToString()) +
                ";customer = " + (model.customer == null ? "" : model.customer.ToString()) +
                ";model = " + (model.model == null ? "" : model.model.ToString()) +
                ";jigNo = " + (model.jigNo == null ? "" : model.jigNo.ToString()) +
                ";cavityCount = " + (model.cavityCount == null ? "" : model.cavityCount.ToString()) +
                ";cycleTime = " + (model.cycleTime == null ? "" : model.cycleTime.ToString()) +
                ";blockCount = " + (model.blockCount == null ? "" : model.blockCount.ToString()) +
                ";unitCount = " + (model.unitCount == null ? "" : model.unitCount.ToString()) +
                ";machineID = " + (model.machineID == null ? "" : model.machineID.ToString()) +
                ";userName = " + (model.userName == null ? "" : model.userName.ToString()) +
                ";dateTime = " + (model.dateTime == null ? "" : model.dateTime.ToString()) +
                ";color = " + (model.color == null ? "" : model.color.ToString()) +
                ";processes = " + (model.processes == null ? "" : model.processes.ToString()) +
                ";remark_1 = " + (model.remark_1 == null ? "" : model.remark_1.ToString()) +
                ";remark_2 = " + (model.remark_2 == null ? "" : model.remark_2.ToString()) +
                ";remark_3 = " + (model.remark_3 == null ? "" : model.remark_3.ToString()) +
                ";remark_4 = " + (model.remark_4 == null ? "" : model.remark_4.ToString()) +
                ";remarks = " + (model.remarks == null ? "" : model.remarks.ToString()) + "");

            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            parameters[2].Value = model.model == null ? (object)DBNull.Value : model.model;
            parameters[3].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            parameters[4].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            parameters[5].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[6].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[7].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[8].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[9].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[10].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[11].Value = model.color == null ? (object)DBNull.Value : model.color;
            parameters[12].Value = model.processes == null ? (object)DBNull.Value : model.processes;
            parameters[13].Value = model.remark_1 == null ? (object)DBNull.Value : model.remark_1;
            parameters[14].Value = model.remark_2 == null ? (object)DBNull.Value : model.remark_2;
            parameters[15].Value = model.remark_3 == null ? (object)DBNull.Value : model.remark_3;
            parameters[16].Value = model.remark_4 == null ? (object)DBNull.Value : model.remark_4;
            parameters[17].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[18].Value = model.ShipTo == null ? (object)DBNull.Value : model.ShipTo;
            parameters[19].Value = model.Type == null ? (object)DBNull.Value : model.Type;
            parameters[20].Value = model.Description == null ? (object)DBNull.Value : model.Description;
            parameters[21].Value = model.Coating == null ? (object)DBNull.Value : model.Coating;

            parameters[22].Value = model.Number == null ? (object)DBNull.Value : model.Number;
            parameters[23].Value = model.UnitCost == null ? (object)DBNull.Value : model.UnitCost;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
        
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCBom ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                                             "Class:PQCBom_DAL" , "Function:		public bool Delete()"  + 
                                             "TableName:PQCBom" , "");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        	
		public SqlCommand DeleteCommand(string sPartNumber)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PQCBom ");
			strSql.Append(" where 1=1 ");



            strSql.Append(" and partNumber=@partNumber ");

            SqlParameter[] parameters = {
                   new SqlParameter("@partNumber", SqlDbType.VarChar,50)
            };
            parameters[0].Value = sPartNumber;

            DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , 
                                             "Class:PQCBom_DAL" , 
                                             "Function:		public SqlCommand DeleteCommand()"  + 
                                             "TableName:PQCBom" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
        	
		public DataSet GetList(string sPartNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(@"select 
                            partNumber,
                            customer,
                            model,
                            jigNo,
                            cavityCount,
                            cycleTime,
                            blockCount,
                            unitCount,
                            machineID,
                            userName,
                            dateTime,
                            color,
                            case when len(processes) >= 20
                            then SUBSTRING(processes,0,20) + '...' 
                            else processes end as processes,
                            processes as compeleteProcess,
                            remark_1 as Supplier,
                            remark_2,
                            remark_3,
                            remark_4,
                            remarks,
                            shipTo,
                            Type,coating,description, isnull( unitCost,0) as unitCost , number ");


            strSql.Append(" FROM PQCBom  where 1=1 ");

			if(sPartNo.Trim()!= "")
			{
                strSql.Append(" and  partNumber = @partNumber");
			}

            strSql.Append(" order by model asc");


            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar,50)
            };

            if (sPartNo.Trim() != "")
            {
                paras[0].Value = sPartNo;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(),paras,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
		}

        public DataSet GetListForModel(string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM PQCBom  where 1=1 ");


            if (sPartNo.Trim() != "")
            {
                strSql.Append(" and  partNumber = @partNumber");
            }

          


            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar,50)
            };

            if (sPartNo.Trim() != "")
            {
                paras[0].Value = sPartNo;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public DataTable GetALL()
        {
            string strSql = "select * FROM PQCBom";
            DataSet ds = DBHelp.SqlDB.Query(strSql, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            if (ds== null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }
        
        public DataSet GetListWithDetail(string sPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
a.[partNumber]
,a.[customer]
,a.[model]
,a.[jigNo]
,a.[cavityCount]
,a.[cycleTime]
,a.[blockCount]
,a.[unitCount]
,a.[machineID]
,a.[userName]
,a.[dateTime]
,a.[color]
,a.[processes]
,a.[remark_1]
,a.[remark_2]
,a.[remark_3]
,a.[remark_4]
,a.[remarks]
,a.[shipTo]
,a.[type]
,a.[coating]
,a.[description]
,a.[unitCost]
,a.[number]

,b.[sn]
,b.[partNumber]
,b.[materialPartNo]
,b.[partCount]
,b.[userName]
,b.[dateTime]
,b.[color]
,b.[imagePath]
,b.[imageAbsolutePath]
,b.[materialName]
,b.[customer]
,b.[outerBoxQty]
,b.[packingTrays]
,b.[module]

from pqcbom a
left join PQCBomDetail b on a.partNumber = b.partNumber where 1=1 ");


            if (sPartNo != "")
            {
                strSql.Append(" and a.partNumber = @partNo ");
            }


     

            SqlParameter[] paras =
            {
                new SqlParameter("@partNo",SqlDbType.VarChar)
            };

            if (sPartNo.Trim() != "")
            {
                paras[0].Value = sPartNo;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
        
    }
}

