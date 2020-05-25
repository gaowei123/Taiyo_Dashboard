using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Common.Class.DAL
{
    public class MouldingBom_DAL
    {

        public MouldingBom_DAL()
        {

        }




        public DataTable GetListForMaterialBudget()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select 

a.partNumberAll
, jigNo
, cavityCount
, matPart01
, matPart02
, isnull(materialWeight01,0) as materialWeight01
, isnull(materialWeight02,0) as materialWeight02
, isnull(b.Unit_Price, 0) as material1stUnitPrice
, isnull(c.Unit_Price, 0) as material2ndUnitPrice
, cycleTime

from MouldingBom a
left
join Material_Inventory_Bom b on a.matPart01 = b.Material_Part
left
join Material_Inventory_Bom c on a.matPart02 = c.Material_Part

group by a.partNumberAll, a.jigNo, a.cavityCount, a.matPart01, a.matPart02, a.materialWeight01, a.materialWeight02, b.Unit_Price, c.Unit_Price, a.cycleTime
order by a.partNumberAll asc");
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetList(string sPartNo, string sPartNoAll)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
SELECT TOP (1000) [partNumber]
      ,[matPart01]
      ,[matPart02]
      ,ISNULL([materialWeight01],0) as [materialWeight01]
	  ,ISNULL([materialWeight02],0) as [materialWeight02]

      ,[customer]
      ,[model]
      ,[jigNo]
      ,isnull( [cavityCount],0) as [cavityCount]
      ,isnull( [partsWeight],0) as [partsWeight]
      ,isnull( [cycleTime],0) as [cycleTime]
      ,isnull( [blockCount],0) as [blockCount]
      ,isnull( [unitCount],0) as [unitCount]
      ,[machineID]
      ,[userName]
      ,[dateTime]
      ,[remarks]
      ,[partNumberAll]
      ,[suppiller]
      ,[refField01]
      ,[refField02]
      ,[refField03]
      ,[refField04]
      ,[refField05]
  FROM [Taiyo_Moulding].[dbo].[MouldingBom] where 1=1 ");


            if (sPartNo.Trim() != "")
            {
                strSql.Append(" and partNumber = @PartNo ");
            }

            if (sPartNoAll.Trim() !="")
            {
                strSql.Append(" and PartNumberAll = @PartNumberAll ");
            }

            SqlParameter[] paras = {
                new SqlParameter("@PartNo", SqlDbType.VarChar),
                new SqlParameter("@PartNumberAll", SqlDbType.VarChar)
            };

            if (sPartNo.Trim() != "") paras[0].Value = sPartNo; else paras[0] = null;
            if (sPartNoAll.Trim() != "") paras[1].Value = sPartNoAll; else paras[1] = null;





            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataSet GetPartNoList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT distinct PartNumber FROM MouldingBom order by  PartNumber asc");
            


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }


        public DataSet GetJigNoList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT distinct jigNo FROM MouldingBom order by jigNo asc");



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }


        public DataSet SelectAll(string sPartNumber)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
SELECT distinct 
	partNumberAll ,
	[matPart01] ,
	[matPart02] ,
	[customer] ,
	[model],
	[jigNo]  ,
	[cavityCount] ,
	[partsWeight]  ,
	[cycleTime] ,
	[blockCount] ,
	[unitCount] ,
	[machineID] ,
	[userName] ,
	CONVERT(varchar(100), dateTime, 120) as [dateTime] ,
	a.[remarks] ,
	[suppiller],
	[refField01],
	[refField02],
	[refField03],
	[refField04] ,
	[refField05],
	case when b1.Unit_Price is null then 'FALSE' else 'TRUE' end as mExistFlag01,
	case when b2.Unit_Price is null then 'FALSE' else 'TRUE' end as mExistFlag02
FROM MouldingBom a
left join Material_Inventory_Bom b1 on a.matPart01 = b1.Material_Part
left join Material_Inventory_Bom b2 on a.matPart02 = b2.Material_Part
where 1=1 ");
            
            if (sPartNumber != "")
            {
                strSql.Append(" and a.partNumberAll like '%"+ sPartNumber + "%' ");
            }
            
            strSql.Append(" order by a.partNumberAll ");



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }

        public DataSet SelectCleanList(string sPartNumber)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT distinct CheckList ,ChecklistID
                              FROM MouldinglifeCleanList where 1=1 ");

            if (sPartNumber != "")
            {
                strSql.Append(" and CheckList like '%" + sPartNumber + "%' ");
            }

            strSql.Append(" order by ChecklistID ");



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }

        public int Delete(string sPartNumberAll)
        {
            string strSql = " delete from  MouldingBom where PartNumberAll = @PartNumberAll";

         

            SqlParameter[] paras = { new SqlParameter("@PartNumberAll", SqlDbType.VarChar) };
            paras[0].Value = sPartNumberAll;


            int result = DBHelp.SqlDB.ExecuteSql(strSql, paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return result;
        }

        
        public SqlCommand AddCmd(Common.Class.Model.MouldingBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append(@"Insert into MouldingBom (partNumber,matPart01,matPart02, materialWeight01,materialWeight02, customer,model,jigNo,cavityCount,partsWeight,cycleTime,blockCount,unitCount,machineID,userName,dateTime,remarks,partNumberAll,suppiller,refField01,refField02,refField03,refField04,refField05) 
                VALUES  (@partNumber,@matPart01,@matPart02,@materialWeight01,@materialWeight02, @customer,@model,@jigNo,@cavityCount,@partsWeight,@cycleTime,@blockCount,@unitCount,@machineID,@userName,@dateTime,@remarks,@partNumberAll,@suppiller,@refField01,@refField02,@refField03,@refField04,@refField05) ");


            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@matPart01", SqlDbType.VarChar),
                new SqlParameter("@matPart02", SqlDbType.VarChar),
                new SqlParameter("@customer", SqlDbType.VarChar),
                new SqlParameter("@model", SqlDbType.VarChar),
                new SqlParameter("@jigNo", SqlDbType.VarChar),
                new SqlParameter("@cavityCount", SqlDbType.VarChar),
                new SqlParameter("@partsWeight", SqlDbType.VarChar),
                new SqlParameter("@cycleTime", SqlDbType.VarChar),
                new SqlParameter("@blockCount", SqlDbType.VarChar),
                new SqlParameter("@unitCount", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userName", SqlDbType.VarChar),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@partNumberAll", SqlDbType.VarChar),
                new SqlParameter("@suppiller", SqlDbType.VarChar),
                new SqlParameter("@refField01", SqlDbType.VarChar),
                new SqlParameter("@refField02", SqlDbType.VarChar),
                new SqlParameter("@refField03", SqlDbType.VarChar),
                new SqlParameter("@refField04", SqlDbType.VarChar),
                new SqlParameter("@refField05", SqlDbType.VarChar),
                new SqlParameter("@materialWeight01", SqlDbType.Decimal),
                new SqlParameter("@materialWeight02", SqlDbType.Decimal),
            };
            paras[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            paras[1].Value = model.matPart01 == null ? (object)DBNull.Value : model.matPart01;
            paras[2].Value = model.matPart02 == null ? (object)DBNull.Value : model.matPart02;
            paras[3].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            paras[4].Value = model.model == null ? (object)DBNull.Value : model.model;
            paras[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            paras[6].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            paras[7].Value = model.partsWeight == null ? (object)DBNull.Value : model.partsWeight;
            paras[8].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            paras[9].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            paras[10].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            paras[11].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            paras[12].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            paras[13].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            paras[14].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            paras[15].Value = model.partNumberAll == null ? (object)DBNull.Value : model.partNumberAll;
            paras[16].Value = model.suppiller == null ? (object)DBNull.Value : model.suppiller;
            paras[17].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            paras[18].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            paras[19].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            paras[20].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            paras[21].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            paras[22].Value = model.materialWeight01 == null ? (object)DBNull.Value : model.materialWeight01;
            paras[23].Value = model.materialWeight02 == null ? (object)DBNull.Value : model.materialWeight02;

            SqlCommand cmd = new SqlCommand();
            cmd = DBHelp.SqlDB.generateCommand(strSql.ToString(), paras);

            return cmd;
        }



        public SqlCommand UpdateCmd(Common.Class.Model.MouldingBom_Model model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Update MouldingBom set ");
            strSql.Append(" matPart01 = @matPart01 ");
            strSql.Append(" ,matPart02 = @matPart02 ");

            strSql.Append(" ,materialWeight01 = @materialWeight01 ");
            strSql.Append(" ,materialWeight02 = @materialWeight02 ");

            strSql.Append(" ,customer = @customer ");
            strSql.Append(" ,model = @model ");
            strSql.Append(" ,jigNo = @jigNo ");
            strSql.Append(" ,cavityCount = @cavityCount ");
            strSql.Append(" ,partsWeight = @partsWeight ");
            strSql.Append(" ,cycleTime = @cycleTime ");
            strSql.Append(" ,blockCount = @blockCount ");
            strSql.Append(" ,unitCount = @unitCount ");
            strSql.Append(" ,machineID = @machineID ");
            strSql.Append(" ,userName = @userName ");
            strSql.Append(" ,dateTime = @dateTime ");
            strSql.Append(" ,remarks = @remarks ");

            strSql.Append(" ,suppiller = @suppiller ");
            strSql.Append(" ,refField01 = @refField01 ");
            strSql.Append(" ,refField02 = @refField02 ");
            strSql.Append(" ,refField03 = @refField03 ");
            strSql.Append(" ,refField04 = @refField04 ");
            strSql.Append(" ,refField05 = @refField05 ");

            strSql.Append(" where 1=1 ");
            strSql.Append(" and partNumberAll = @partNumberAll");
            strSql.Append(" and partNumber = @partNumber");



            SqlParameter[] paras =
            {
                new SqlParameter("@partNumber", SqlDbType.VarChar),
                new SqlParameter("@matPart01", SqlDbType.VarChar),
                new SqlParameter("@matPart02", SqlDbType.VarChar),
                new SqlParameter("@customer", SqlDbType.VarChar),
                new SqlParameter("@model", SqlDbType.VarChar),
                new SqlParameter("@jigNo", SqlDbType.VarChar),
                new SqlParameter("@cavityCount", SqlDbType.VarChar),
                new SqlParameter("@partsWeight", SqlDbType.VarChar),
                new SqlParameter("@cycleTime", SqlDbType.VarChar),
                new SqlParameter("@blockCount", SqlDbType.VarChar),
                new SqlParameter("@unitCount", SqlDbType.VarChar),
                new SqlParameter("@machineID", SqlDbType.VarChar),
                new SqlParameter("@userName", SqlDbType.VarChar),
                new SqlParameter("@dateTime", SqlDbType.DateTime),
                new SqlParameter("@remarks", SqlDbType.VarChar),
                new SqlParameter("@partNumberAll", SqlDbType.VarChar),
                new SqlParameter("@suppiller", SqlDbType.VarChar),
                new SqlParameter("@refField01", SqlDbType.VarChar),
                new SqlParameter("@refField02", SqlDbType.VarChar),
                new SqlParameter("@refField03", SqlDbType.VarChar),
                new SqlParameter("@refField04", SqlDbType.VarChar),
                new SqlParameter("@refField05", SqlDbType.VarChar),
                new SqlParameter("@materialWeight01", SqlDbType.Decimal),
                new SqlParameter("@materialWeight02", SqlDbType.Decimal)
            };
            paras[0].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber;
            paras[1].Value = model.matPart01 == null ? (object)DBNull.Value : model.matPart01;
            paras[2].Value = model.matPart02 == null ? (object)DBNull.Value : model.matPart02;
            paras[3].Value = model.customer == null ? (object)DBNull.Value : model.customer;
            paras[4].Value = model.model == null ? (object)DBNull.Value : model.model;
            paras[5].Value = model.jigNo == null ? (object)DBNull.Value : model.jigNo;
            paras[6].Value = model.cavityCount == null ? (object)DBNull.Value : model.cavityCount;
            paras[7].Value = model.partsWeight == null ? (object)DBNull.Value : model.partsWeight;
            paras[8].Value = model.cycleTime == null ? (object)DBNull.Value : model.cycleTime;
            paras[9].Value = model.blockCount == null ? (object)DBNull.Value : model.blockCount;
            paras[10].Value = model.unitCount == null ? (object)DBNull.Value : model.unitCount;
            paras[11].Value = model.machineID == null ? (object)DBNull.Value : model.machineID;
            paras[12].Value = model.userName == null ? (object)DBNull.Value : model.userName;
            paras[13].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime;
            paras[14].Value = model.remarks == null ? (object)DBNull.Value : model.remarks;
            paras[15].Value = model.partNumberAll == null ? (object)DBNull.Value : model.partNumberAll;

            paras[16].Value = model.suppiller == null ? (object)DBNull.Value : model.suppiller;
            paras[17].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            paras[18].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            paras[19].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            paras[20].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            paras[21].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            paras[22].Value = model.materialWeight01 == null ? (object)DBNull.Value : model.materialWeight01;
            paras[23].Value = model.materialWeight02 == null ? (object)DBNull.Value : model.materialWeight02;


            SqlCommand cmd = new SqlCommand();
            cmd = DBHelp.SqlDB.generateCommand(strSql.ToString(), paras);

            return cmd;
        }



    }


}
