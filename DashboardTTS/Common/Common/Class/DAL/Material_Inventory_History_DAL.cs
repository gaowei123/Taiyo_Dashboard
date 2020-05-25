using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class Material_Inventory_History_DAL
    {
        public Common.Class.Model.Material_Inventory_History copyobj(Common.Class.Model.Material_Inventory model)
        {
            Common.Class.Model.Material_Inventory_History _model = new Model.Material_Inventory_History();
            _model.Material_No = model.Material_No;
            _model.Inventory_Weight = model.Inventory_Weight;
            _model.Transaction_Weight = model.Transaction_Weight;
            _model.User_Name = model.User_Name;
            _model.Last_Event = model.Last_Event;
            _model.Load_Time = model.Load_Time;
            _model.Updated_Time = model.Updated_Time;
            _model.Material_LotNo = model.Material_LotNo;
            _model.Supplier = model.Supplier;
            _model.MachineID = model.MachineID;
            _model.Remarks = model.Remarks;
            _model.REF_FIELD01 = model.REF_FIELD01;
            _model.REF_FIELD02 = model.REF_FIELD02;
            _model.REF_FIELD03 = model.REF_FIELD03;
            return _model;
        }

        public DataSet SelectAll(string sMaterial_Part, DateTime from, DateTime to,string sEvent)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT 
                            Material_No
                            ,Material_LotNo
                            ,convert(varchar, convert(float, a.Inventory_Weight )) + ' kg'  as Inventory_Weight
                            ,convert(varchar, convert(float, a.Inventory_Weight ))  as Inventory_Weight_temp
                            ,convert(varchar, convert(float, a.Transaction_Weight)) + ' kg' as Transaction_Weight
                            ,convert(varchar, convert(float, a.Transaction_Weight))  as Transaction_Weight_temp

                            ,'SGD ' + convert(varchar,convert(float, convert(decimal(18,2), ISNULL( b.Unit_Price,0)))  )  as UnitCost
                            ,'SGD ' + convert(varchar,convert(float, convert(decimal(18,2),ISNULL(b.Unit_Price,0) * ISNULL( a.Transaction_Weight,0))))  as TotalCost
                            ,convert(varchar,convert(float, convert(decimal(18,2),ISNULL(b.Unit_Price,0) * ISNULL( a.Transaction_Weight,0))))  as TotalCost_temp
                            ,case when isnull(b.REF_FIELD01 ,'') = '' then 'NA' else b.REF_FIELD01 end  as ResinType

                            ,Last_Event
                            ,CONVERT(varchar(100), a.Load_Time, 120) as Load_Time
                            ,CONVERT(varchar(100), a.Updated_Time, 120) as Updated_Time 
                            ,Supplier
                            ,MachineID
                            ,a.Remarks
                            ,User_Name
                            FROM Material_Inventory_History a left join Material_Inventory_Bom b on a.Material_No=b.Material_Part

                            where 1=1");

            if (sMaterial_Part != "")
            {
                strSql.Append(" and a.Material_No = @Material_No");
            }

            if (sEvent != "")
            {
                strSql.Append(" and a.Last_Event = @Last_Event");
            }

            strSql.Append(" and a.Updated_Time > @DateFrom ");

            strSql.Append(" and a.Updated_Time < @DateTo ");

       
            strSql.Append(" order by a.Updated_Time desc ");


            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Last_Event",SqlDbType.VarChar),
            };

            if (sMaterial_Part != "")
            {
                paras[0].Value = sMaterial_Part;
            }else
            {
                paras[0] = null;
            }

            paras[1].Value = from;
            paras[2].Value = to.AddDays(1);
            if (sEvent != "")
            {
                paras[3].Value = sEvent;
            }
            else
            {
                paras[3] = null;
            }

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }


        public DataSet SelectAllByMaterial(string sMaterial_Part, DateTime from, DateTime to,string sEvent)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select 
                            Material_No
                            ,Last_Event
                            , CONVERT(varchar, convert(float,Sum(Transaction_Weight))) + ' kg' as Transaction_Weight
                            , CONVERT(varchar, convert(float,Sum(Transaction_Weight)))  as Transaction_Weight_temp
                            ,'SGD ' + CONVERT(varchar, convert(float, convert(dec(18,2), Sum(Transaction_Weight * ISNULL(b.Unit_Price,0)))))  as TotalCost
                            ,CONVERT(varchar, convert(float, convert(dec(18,2), Sum(Transaction_Weight * ISNULL(b.Unit_Price,0)))))  as TotalCost_temp
                            FROM Material_Inventory_History a left join Material_Inventory_Bom b on a.Material_No = b.Material_Part 
                            where 1=1 ");

            if (sMaterial_Part != "")
            {
                strSql.Append(" and Material_No = @sMaterial_Part ");
            }
            if (from.ToString().Length != 0)
            {
                strSql.Append(" and a.Updated_Time > @DateFrom ");
            }
            if (to.ToString().Length != 0)
            {
                strSql.Append(" and a.Updated_Time < @DateTo ");
            }
            if (sEvent.ToString().Length != 0)
            {
                strSql.Append(" and a.Last_Event = @Last_Event ");
            }
            strSql.Append("  group by Material_No, Last_Event ");


            SqlParameter[] paras =
            {
                new SqlParameter("@sMaterial_Part",SqlDbType.VarChar),
                new SqlParameter("@DateFrom",SqlDbType.DateTime),
                new SqlParameter("@DateTo",SqlDbType.DateTime),
                new SqlParameter("@Last_Event",SqlDbType.VarChar),
            };

            if (sMaterial_Part != "")
            {
                paras[0].Value = sMaterial_Part;
            }
            else
            {
                paras[0] = null;
            }
          
            paras[1].Value = from;
            paras[2].Value = to.AddDays(1);
            if (sEvent != "")
            {
                paras[3].Value = sEvent;
            }
            else
            {
                paras[3] = null;
            }

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }

        public DataSet SelectLotNoByPart(string sMaterial_Part, string sLastEvent, string sSupplier)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select  Material_LotNo, Last_Event from Material_Inventory_History where 1=1 ");

            if (sMaterial_Part != "")
            {
                strSql.Append(" and Material_No = @Material_No ");
            }

            if (sLastEvent != "")
            {
                strSql.Append(" and Last_Event = @Last_Event ");
            }
            if (sSupplier != "")
            {
                strSql.Append(" and Supplier = @Supplier ");
            }
            strSql.Append(" order by Material_LotNo asc ");



            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@Last_Event",SqlDbType.VarChar),
                new SqlParameter("@Supplier",SqlDbType.VarChar)
            };

            if (sMaterial_Part != "")
            {
                paras[0].Value = sMaterial_Part;
            }
            else
            {
                paras[0] = null;
            }

            if (sLastEvent != "")
            {
                paras[1].Value = sLastEvent;
            }
            else
            {
                paras[1] = null;
            }

            if (sSupplier != "")
            {
                paras[2].Value = sSupplier;
            }
            else
            {
                paras[2] = null;
            }
            


            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;
        }


        

        public SqlCommand AddCMD(Common.Class.Model.Material_Inventory_History model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into Material_Inventory_History( ");
            strSql.Append(" Material_No,Material_LotNo,Inventory_Weight,Transaction_Weight,Last_Event,Load_Time,Updated_Time,Supplier,MachineID,Remarks,User_Name,REF_FIELD01,REF_FIELD02,REF_FIELD03 ");
            strSql.Append(" ) values ( ");
            strSql.Append(" @Material_No,@Material_LotNo,@Inventory_Weight,@Transaction_Weight,@Last_Event,@Load_Time,@Updated_Time,@Supplier,@MachineID,@Remarks,@User_Name,@REF_FIELD01,@REF_FIELD02,@REF_FIELD03  )");


            SqlParameter[] parameters =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@Material_LotNo",SqlDbType.VarChar),
                new SqlParameter("@Inventory_Weight",SqlDbType.Decimal),
                new SqlParameter("@Transaction_Weight",SqlDbType.Decimal),
                new SqlParameter("@Last_Event",SqlDbType.VarChar),
                new SqlParameter("@Load_Time",SqlDbType.DateTime),
                new SqlParameter("@Updated_Time",SqlDbType.DateTime),
                new SqlParameter("@Supplier",SqlDbType.VarChar),
                new SqlParameter("@MachineID",SqlDbType.VarChar),
                new SqlParameter("@Remarks",SqlDbType.VarChar),
                new SqlParameter("@User_Name",SqlDbType.VarChar),
                new SqlParameter("@REF_FIELD01",SqlDbType.VarChar),
                new SqlParameter("@REF_FIELD02",SqlDbType.VarChar),
                new SqlParameter("@REF_FIELD03",SqlDbType.VarChar),
            };
            parameters[0].Value = model.Material_No == null ? (object)DBNull.Value : model.Material_No;
            parameters[1].Value = model.Material_LotNo == null ? (object)DBNull.Value : model.Material_LotNo;
            parameters[2].Value = model.Inventory_Weight == null ? (object)DBNull.Value : model.Inventory_Weight;
            parameters[3].Value = model.Transaction_Weight == null ? (object)DBNull.Value : model.Transaction_Weight;
            parameters[4].Value = model.Last_Event == null ? (object)DBNull.Value : model.Last_Event;
            parameters[5].Value = model.Load_Time == null ? (object)DBNull.Value : model.Load_Time;
            parameters[6].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
            parameters[7].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
            parameters[8].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
            parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;
            parameters[10].Value = model.User_Name == null ? (object)DBNull.Value : model.User_Name;
            parameters[11].Value = model.REF_FIELD01 == null ? (object)DBNull.Value : model.REF_FIELD01;
            parameters[12].Value = model.REF_FIELD02 == null ? (object)DBNull.Value : model.REF_FIELD02;
            parameters[13].Value = model.REF_FIELD03 == null ? (object)DBNull.Value : model.REF_FIELD03;



            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }



        public DataSet GetMonthLyReport(string sYear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
select 
a.Month
,ISNULL(b.LoadKgs,0) as LoadKgs
,ISNULL(b.UnLoadKgs,0) as UnLoadKgs
,ISNULL(b.ReturnKgs,0) as ReturnKgs
,ISNULL(b.UnLoadKgs - b.ReturnKgs,0) as UsageKgs
,ISNULL(b.UnLoadCost - b.ReturnCost,0) as UsageCost

from 
(
	--拼出12个月的主表
	select 1  as SN ,@Year+'-Jan.' as [Month] union select 2  as SN ,@Year+'-Feb.' as [Month] union 
	select 3  as SN ,@Year+'-Mar.' as [Month] union select 4  as SN ,@Year+'-Apr.' as [Month] union 
	select 5  as SN ,@Year+'-May.' as [Month] union select 6  as SN ,@Year+'-Jun.' as [Month] union 
	select 7  as SN ,@Year+'-Jul.' as [Month] union select 8  as SN ,@Year+'-Aug.' as [Month] union 
	select 9  as SN ,@Year+'-Sep.' as [Month] union select 10 as SN ,@Year+'-Oct.' as [Month] union 
	select 11 as SN ,@Year+'-Nov.' as [Month] union select 12 as SN ,@Year+'-Dec.' as [Month]
) a
left join 
(
	select 
	Month(a.updated_Time) as [Month]
	,convert(decimal(18,2), SUM(ISNULL(case when Last_Event = 'Load' then Transaction_Weight end,0)) ) as LoadKgs
	,convert(decimal(18,2), SUM(ISNULL(case when Last_Event = 'UnLoad' then Transaction_Weight end,0)))  as UnLoadKgs
	,convert(decimal(18,2), SUM(ISNULL(case when Last_Event = 'Return' then Transaction_Weight end,0)))  as ReturnKgs
	,convert(decimal(18,2), SUM(ISNULL(case when Last_Event = 'Delete' then Transaction_Weight end,0)))  as DeleteKgs

	,convert(decimal(18,4), SUM(ISNULL(case when Last_Event = 'Load' then Transaction_Weight* isnull( b.Unit_Price,0) end,0))) as LoadCost
	,convert(decimal(18,4), SUM(ISNULL(case when Last_Event = 'UnLoad' then Transaction_Weight* isnull( b.Unit_Price,0) end,0)))  as UnLoadCost
	,convert(decimal(18,4), SUM(ISNULL(case when Last_Event = 'Return' then Transaction_Weight* isnull( b.Unit_Price,0) end,0)))  as ReturnCost
	,convert(decimal(18,4), SUM(ISNULL(case when Last_Event = 'Delete' then Transaction_Weight* isnull( b.Unit_Price,0) end,0)))  as DeleteCost
	from Material_Inventory_History  a
	left join Material_Inventory_Bom b on a.Material_No = b.Material_Part
	where YEAR(a.updated_Time) = @Year
	group by Month(a.updated_Time) 
) b 
on a.SN = b.Month");
            

            SqlParameter[] paras =
            {
                new SqlParameter("@Year",SqlDbType.VarChar)
            };

            paras[0].Value = sYear;

          

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }




        public DataSet GetMaterialDetailForMonth(int iMonth, int iYear)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"
select 

a.Material_No
,convert(decimal(18,2), Round(LoadKgs,2))  as  LoadKgs
,convert(decimal(18,4), Round(a.LoadKgs * isnull( b.Unit_Price,0),4)) as LoadCost
,convert(decimal(18,2), Round(a.UnloadKgs,2)) as UsedKgs
,convert(decimal(18,4), Round(a.UnloadKgs *isnull( b.Unit_Price,0),4)) as UsedSGD

from 
(
	select 
	Material_No
	,ISNULL(SUM(case when Last_Event = 'Load' then ISNULL(Transaction_Weight,0) end) ,0)as LoadKgs
	,ISNULL(SUM(case when Last_Event = 'Unload' then ISNULL(Transaction_Weight,0) end),0) as UnloadKgs
	,ISNULL(SUM(case when Last_Event = 'Return' then ISNULL(Transaction_Weight,0) end),0) as ReturnKgs
	from Material_Inventory_History 
	where MONTH(updated_Time) = @Month and YEAR(updated_Time) = @Year
	group by Material_No
) a left join Material_Inventory_Bom  b on a.Material_No = b.Material_Part

order by a.Material_No asc
");

//            strSql.Append(@"
//select 

//Material_No
//,SUM(Transaction_Weight) as LoadKgs
//,convert(decimal(18,4), Round( SUM(Transaction_Weight * b.Unit_Price),4)) as LoadCost

//from Material_Inventory_History a
//left join Material_Inventory_Bom  b on a.Material_No = b.Material_Part

//where MONTH(a.updated_Time) = @Month and YEAR(a.updated_Time) = @Year
//and a.Last_Event = 'Load'
//group by Material_No");


            SqlParameter[] paras =
            {
                new SqlParameter("@Month",SqlDbType.Int),
                new SqlParameter("@Year",SqlDbType.Int)
            };

            paras[0].Value = iMonth;
            paras[1].Value = iYear;



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }


        public DataSet GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" 
SELECT [Material_Part]
      ,[Weight]
      ,[User_Name]
      ,[Last_Event]
      ,[Load_Time]
      ,[Updated_Time]
      ,[REF_FIELD01]
      ,[REF_FIELD02]
      ,[REF_FIELD03]
      ,[Remarks]
      ,[Material_No]
      ,[Material_LotNo]
      ,[Inventory_Weight]
      ,[Transaction_Weight]
      ,[Supplier]
      ,[MachineID]
  FROM Material_Inventory_History  where 1=1 and Updated_Time >= @dateFrom and Updated_Time < @dateTo ");

            if (sMachineID != "") strSql.Append(" and machineID = @machineID");



            SqlParameter[] paras =
            {
                new SqlParameter("@dateFrom",SqlDbType.DateTime2),
                new SqlParameter("@dateTo",SqlDbType.DateTime2),
                new SqlParameter("@machineID",SqlDbType.VarChar)
            };

            paras[0].Value = dDateFrom;
            paras[1].Value = dDateTo;
            if (sMachineID != "") paras[2].Value = sMachineID;else paras[2] = null;




            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);          
        }



    }
}
