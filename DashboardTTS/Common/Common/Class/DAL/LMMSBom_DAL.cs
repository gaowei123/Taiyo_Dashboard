using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class LMMSBom_DAL
    {
        public LMMSBom_DAL()
        {

        }

        public DataSet GetSendingTo(string sPartNo)
        {
            if (string.IsNullOrEmpty(sPartNo))
            {
                return null;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PartBelongTo from LMMSBom where partNumber = @partNumber and ISNULL(PartBelongTo,'') <> ''  ");

            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar)
            };
            paras[0].Value = sPartNo;


            return DBHelp.SqlDB.Query(strSql.ToString(),paras);
        }



        public DataSet GetList(string sPartNo, string sMachineID, bool EnableFuzzySearch)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select ");
            strSql.Append(" partNumber, module ,cycleTime, blockCount, unitCount, machineID, remarks,type,customer, lighting, camera, currentPower, Supplier, PartBelongTo, convert(varchar, dateTime,3) as datetime, Number");
            strSql.Append(" FROM LMMSBom where 1=1  ");


            if (EnableFuzzySearch)
            {
                strSql.Append(" and partNumber like '%" + sPartNo + "%' ");
            }
            else
            {
                if (!string.IsNullOrEmpty(sPartNo))
                {
                    strSql.Append(" and partNumber = @partNumber  ");
                }
            }

            if (!string.IsNullOrEmpty(sMachineID))
            {
                strSql.Append(" and MachineID = @MachineID  ");
            }
            
            strSql.Append(" order by module asc, partNumber asc ");
            

            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar)
            };


            if (!string.IsNullOrEmpty(sPartNo))
            {
                paras[0].Value = sPartNo;
            }else { paras[0] = null; }

            if (!string.IsNullOrEmpty(sMachineID))
            {
                paras[1].Value = sMachineID;
            }else { paras[1] = null; }
            

            return DBHelp.SqlDB.Query(strSql.ToString(),paras);
        }


        public DataSet GetAllPartList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct PartNumber from LMMSBom  order by partnumber asc ");

            return DBHelp.SqlDB.Query(strSql.ToString());
        }


        public DataTable GetAllPartNoList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct PartNumber from LMMSBom  order by partnumber asc ");


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString());
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }

     


        public DataSet GetAllModelList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct module as model from LMMSBom order by module asc ");

            return DBHelp.SqlDB.Query(strSql.ToString());
        }




        public DataSet GetOnefoldPartList()
        {
            string sql = @"select 
                            module  
                            ,a.partNumber
                            ,blockCount
                            ,unitCount
                            ,cycleTime
                            ,customer
                            ,type
                            ,Number

                            ,case when PCSCountForSet is null 
                            then 
	                            'No Bom Detail' 
                            else 
                                convert(varchar(8),Convert(decimal(18,1),cycleTime/blockCount/(unitCount/PCSCountForSet)))
                                + 's' + '/' +
	                            case when PCSCountForSet = 1 then 'Pcs' else 'Set' end 
                            end 
                            as setCycleTime
                            
                            ,case when PCSCountForSet is null 
                            then 
	                            'No Bom Detail' 
                            else 
                                convert(varchar(50),
                                    Convert(decimal(18,0),3600 / (cycleTime/blockCount/(unitCount/PCSCountForSet)))
                                )  
                            end  as OutputPerHour


                            from LMMSBom a left join 
                            (
                                select partnumber , count(1) as PCSCountForSet from lmmsBomDetail group by partnumber
                            ) b on a.partnumber = b.partnumber

                            order by module asc , partNumber asc";
            
            return DBHelp.SqlDB.Query(sql);
        }


        public DataTable GetPartList()
        {
            string sql = @"select 
                        a.module
                        ,a.partNumber   
                        ,isnull(b.materialCount ,0) as MaterialCount
                        ,a.cycleTimePerPCS

                        ,case when isnull( b.materialCount,0)  = 0 
                            then 'No Bom Detail'
                            else convert(varchar, convert(numeric(18,0), 3600/( a.cycleTimePerPCS * b.materialCount)))  
			                        +'(' + 
		                         convert(varchar, convert(numeric(18,0), 3600/a.cycleTimePerPCS))  
		                            +')'
                        end  as outputPerHour

                        from  
                        (
	                        select 
	                        module
	                        ,a.partNumber
	                        ,AVG(cycleTime /blockCount/unitCount ) as cycleTimePerPCS
 
	                        from LMMSBom a 
	                        group by a.partNumber, module
                        ) a

                        left join
                        (
	                        select PartNumber, count(1) as MaterialCount  
	                        from LMMSBomDetail 
	                        group by partNumber
                        ) b on a.partnumber = b.partNumber

                        order by a.module asc , a.partNumber asc";


            DataSet ds = DBHelp.SqlDB.Query(sql);
            if (ds == null || ds.Tables.Count==0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataSet GetCycTimeByMultPart(List<string> list_PartNumber,string MachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append("  partNumber ");
            strSql.Append("  ,cycleTime/ blockCount / unitCount as TimePerUnit  ");

            strSql.Append(" FROM LMMSBom ");
          
            strSql.Append(" where partNumber in(  ");

            foreach (string partNumber in list_PartNumber)
            {
                strSql.Append(" '" + partNumber + "' ,");
            }
            strSql.Remove(strSql.ToString().LastIndexOf(','), 1);
            strSql.Append(" )");



            return DBHelp.SqlDB.Query(strSql.ToString());
        }


        public int Delete(string sPartNo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBom ");

            strSql.Append(" where Partnumber = @Partnumber  and  MachineID = @MachineID");

   
            SqlParameter[] parameters = {
                new SqlParameter("@Partnumber",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar)
            };

            parameters[0].Value = sPartNo;
            parameters[1].Value = sMachineID;

            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }

        public SqlCommand DeleteByPart(string PartNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LMMSBom ");

            strSql.Append(" where partNumber = @partNumber");


            SqlParameter[] parameters = {
                new SqlParameter("@partNumber",SqlDbType.VarChar)
            };
            parameters[0].Value = PartNumber;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }

        public int Add(Common.Class.Model.LMMSBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSBom (");
            strSql.Append("partNumber,module,cycleTime,blockCount,unitCount,machineID,userName,dateTime,remarks, number");
            strSql.Append(") values (");
            strSql.Append("@partNumber,@module,@cycleTime,@blockCount,@unitCount,@machineID,@userName,@dateTime,@remarks, @number)");
            SqlParameter[] parameters = {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@cycleTime", SqlDbType.Decimal),
                new SqlParameter("@blockCount", SqlDbType.Decimal),
                new SqlParameter("@unitCount", SqlDbType.Decimal),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userName", SqlDbType.VarChar),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@module", SqlDbType.VarChar),
                new SqlParameter("@number", SqlDbType.VarChar)
            };

            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[2].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[3].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[5].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[6].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[7].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[8].Value = model.module == null ? (object)DBNull.Value : model.module;
            parameters[9].Value = model.Number == null ? (object)DBNull.Value : model.Number;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }


        public SqlCommand AddCommand(Common.Class.Model.LMMSBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LMMSBom (");
            strSql.Append("partNumber,module,cycleTime,blockCount,unitCount,machineID,userName,dateTime,remarks,type,customer,lighting,camera,currentPower,PartBelongTo,Supplier, Number");
            strSql.Append(") values (");
            strSql.Append("@partNumber,@module,@cycleTime,@blockCount,@unitCount,@machineID,@userName,@dateTime,@remarks,@type,@customer,@lighting,@camera,@currentPower,@partBelongTo,@supplier, @Number)");
            SqlParameter[] parameters = {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@cycleTime", SqlDbType.Decimal),
                new SqlParameter("@blockCount", SqlDbType.Decimal),
                new SqlParameter("@unitCount", SqlDbType.Decimal),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userName", SqlDbType.VarChar),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@module", SqlDbType.VarChar),
                new SqlParameter("@type", SqlDbType.VarChar),
                new SqlParameter("@customer", SqlDbType.VarChar),
                new SqlParameter("@lighting", SqlDbType.VarChar),
                new SqlParameter("@camera", SqlDbType.VarChar),
                new SqlParameter("@currentPower", SqlDbType.VarChar),
                new SqlParameter("@supplier",SqlDbType.VarChar),
                new SqlParameter("@partBelongTo",SqlDbType.VarChar),
                new SqlParameter("@Number",SqlDbType.VarChar)
            };


            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[2].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[3].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[5].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[6].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[7].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[8].Value = model.module == null ? (object)DBNull.Value : model.module;
            parameters[9].Value = model.Type == null ? (object)DBNull.Value : model.Type;
            parameters[10].Value = model.Customer == null ? (object)DBNull.Value : model.Customer;
            parameters[11].Value = model.Lighting == null ? (object)DBNull.Value : model.Lighting;
            parameters[12].Value = model.Camera == null ? (object)DBNull.Value : model.Camera;
            parameters[13].Value = model.CurrentPower == null ? (object)DBNull.Value : model.CurrentPower;
            parameters[14].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
            parameters[15].Value = model.PartBelongTo == null ? (object)DBNull.Value : model.PartBelongTo;
            parameters[16].Value = model.Number == null ? (object)DBNull.Value : model.Number;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        public int UpdateByPartNumber(Common.Class.Model.LMMSBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update LMMSBom set ");
            strSql.Append("cycleTime = @cycleTime,");
            strSql.Append("blockCount = @blockCount,");
            strSql.Append("module = @module,");
            strSql.Append("UnitCount= @UnitCount,");
            strSql.Append("machineID = @machineID,");
            strSql.Append("userName = @userName,");
            strSql.Append("dateTime = @dateTime,");
            strSql.Append("remarks = @remarks  ");

            strSql.Append("where partNumber = @partNumber");

            SqlParameter[] parameters = {
                    new SqlParameter("@partNumber", SqlDbType.VarChar),
                    new SqlParameter("@cycleTime", SqlDbType.Decimal),
                    new SqlParameter("@blockCount", SqlDbType.Decimal),
                     new SqlParameter("@unitCount", SqlDbType.Decimal),
                    new SqlParameter("@machineID", SqlDbType.VarChar),
                    new SqlParameter("@userName", SqlDbType.VarChar),
                    new SqlParameter("@dateTime", SqlDbType.DateTime),
                    new SqlParameter("@remarks", SqlDbType.VarChar),
            new SqlParameter("@module", SqlDbType.VarChar)};


            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[2].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[3].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[5].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[6].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[7].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[8].Value = model.module == null ? (object)DBNull.Value : model.module;


            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters);
        }


        public SqlCommand UpdateCommand(Common.Class.Model.LMMSBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update LMMSBom set ");
            strSql.Append("cycleTime = @cycleTime,");
            strSql.Append("blockCount = @blockCount,");
            strSql.Append("module = @module,");
            strSql.Append("UnitCount= @UnitCount,");
            strSql.Append("userName = @userName,");
            strSql.Append("dateTime = @dateTime,");
            strSql.Append("type = @type, ");
            strSql.Append("remarks = @remarks,  ");
            strSql.Append("customer = @customer,  ");
            strSql.Append("lighting = @lighting,  ");
            strSql.Append("camera = @camera,  ");
            strSql.Append("currentPower = @currentPower, ");
            strSql.Append("supplier = @supplier, ");
            strSql.Append("partBelongTo = @partBelongTo, ");
            strSql.Append("Number = @Number ");
            
            strSql.Append("where partNumber = @partNumber and machineID = @machineID");

            SqlParameter[] parameters = {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@cycleTime", SqlDbType.Decimal),
                new SqlParameter("@blockCount", SqlDbType.Decimal),
                new SqlParameter("@unitCount", SqlDbType.Decimal),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userName", SqlDbType.VarChar),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@module", SqlDbType.VarChar),
                new SqlParameter("@type", SqlDbType.VarChar),
                new SqlParameter("@customer", SqlDbType.VarChar),
                new SqlParameter("@lighting", SqlDbType.VarChar),
                new SqlParameter("@camera", SqlDbType.VarChar),
                new SqlParameter("@currentPower", SqlDbType.VarChar),
                new SqlParameter("@supplier", SqlDbType.VarChar),
                new SqlParameter("@partBelongTo", SqlDbType.VarChar),
                new SqlParameter("@Number",SqlDbType.VarChar)
            };


            parameters[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[1].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            parameters[2].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            parameters[3].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            parameters[5].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            parameters[6].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            parameters[7].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            parameters[8].Value = model.module == null ? (object)DBNull.Value : model.module;
            parameters[9].Value = model.Type == null ? (object)DBNull.Value : model.Type;
            parameters[10].Value = model.Customer == null ? (object)DBNull.Value : model.Customer;
            parameters[11].Value = model.Lighting == null ? (object)DBNull.Value : model.Lighting;
            parameters[12].Value = model.Camera == null ? (object)DBNull.Value : model.Camera;
            parameters[13].Value = model.CurrentPower == null ? (object)DBNull.Value : model.CurrentPower;
            parameters[14].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
            parameters[15].Value = model.PartBelongTo == null ? (object)DBNull.Value : model.PartBelongTo;
            parameters[16].Value = model.Number == null ? (object)DBNull.Value : model.Number;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }




        public SqlCommand UpdateVisionParaByPartMachineCommand(Common.Class.Model.LMMSBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update LMMSBom set ");
            
            strSql.Append("lighting = @lighting,  ");
            strSql.Append("camera = @camera,  ");
            strSql.Append("currentPower = @currentPower ");

            strSql.Append("where partNumber = @partNumber and machineID = @machineID");

            SqlParameter[] parameters = {
                new SqlParameter("@lighting", SqlDbType.VarChar),
                new SqlParameter("@camera", SqlDbType.VarChar),
                new SqlParameter("@currentPower", SqlDbType.VarChar),
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar)
            };


            parameters[0].Value = model.Lighting == null ? (object)DBNull.Value : model.Lighting;
            parameters[1].Value = model.Camera == null ? (object)DBNull.Value : model.Camera;
            parameters[2].Value = model.CurrentPower == null ? (object)DBNull.Value : model.CurrentPower;
            parameters[3].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
          
            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }




    }
    
}
