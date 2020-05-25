using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.DAL
{
    public class Material_Inventory_DAL
    {
        public DataSet SelectAll(string sMaterial_Part,string sEvent)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT  
                            a.Material_No
                            ,convert(varchar, convert(float, a.Inventory_Weight )) +' kg' as Inventory_Weight
                            ,convert(varchar, convert(float, a.Inventory_Weight )) as Inventory_Weight_temp
                            ,convert(varchar, convert(float, a.Transaction_Weight)) +' kg'  as Transaction_Weight
                            ,convert(varchar, convert(float, a.Transaction_Weight))  as Transaction_Weight_temp
                            ,a.User_Name
                            ,a.Last_Event
                            ,CONVERT(varchar(100), a.Load_Time, 120) as Load_Time
                            ,CONVERT(varchar(100), a.Updated_Time, 120) as Updated_Time 
                            ,a.Material_LotNo
                            ,a.Supplier
                            ,a.MachineID
                            ,a.Remarks
                            ,'SGD ' + convert(varchar,convert(float, convert(decimal(18,2), ISNULL( b.Unit_Price,0)))  )    as UnitCost
                            ,'SGD ' + convert(varchar,convert(float, convert(decimal(18,2),ISNULL(b.Unit_Price,0) * ISNULL( a.Inventory_Weight,0))))   as TotalCost
                            ,convert(varchar,convert(float, convert(decimal(18,2),ISNULL(b.Unit_Price,0) * ISNULL( a.Inventory_Weight,0))))   as TotalCost_temp
                            ,case when isnull(b.REF_FIELD01 ,'') = '' then 'NA' else b.REF_FIELD01 end  as ResinType

                            FROM Material_Inventory a left join Material_Inventory_Bom b on a.Material_No = b.Material_Part
                            where 1=1 ");

            if (sMaterial_Part != "")
            {
                strSql.Append(" and Material_No = @Material_No ");
            }
            if (sEvent != "")
            {
                strSql.Append(" and Last_Event = @Last_Event ");
            }
            strSql.Append(" order by Updated_Time  desc ");

            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
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
            if (sEvent != "")
            {
                paras[1].Value = sEvent;
            }
            else
            {
                paras[1] = null;
            }
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }






        public DataSet SelectAll(string sMaterialNo, string sMaterialLotNo,DateTime? dLoadDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT [Material_No]
                          ,[Inventory_Weight]
                          ,Transaction_Weight
                          ,[User_Name]
                          ,[Last_Event]
                          ,[Load_Time]
                          ,[Updated_Time]
                          ,Material_LotNo
                          ,Supplier
                          ,MachineID
                          ,[Remarks]
                           FROM Material_Inventory where 1=1 ");

            if (sMaterialNo != "")
            {
                strSql.Append(" and Material_No = @Material_No ");
            }

            if (sMaterialLotNo != "")
            {
                strSql.Append(" and Material_LotNo = @Material_LotNo ");
            }

            if (dLoadDate != null)
            {
                strSql.Append(" and Load_Time = @Load_Time ");
            }
            strSql.Append(" order by Material_No ");

            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@Material_LotNo",SqlDbType.VarChar),
                new SqlParameter("@Load_Time",SqlDbType.VarChar)
            };

            if (sMaterialNo != "")
            {
                paras[0].Value = sMaterialNo;
            }
            else
            {
                paras[0] = null;
            }

            if (sMaterialLotNo != "")
            {
                paras[1].Value = sMaterialLotNo;
            }
            else
            {
                paras[1] = null;
            }

            if (dLoadDate != null)
            {
                paras[2].Value = dLoadDate;
            }
            else
            {
                paras[2] = null;
            }



            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras,DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            return ds;
        }

        public DataSet SelectMaterialList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"Select distinct Material_No  FROM Material_Inventory  order by Material_No ");
            
            return DBHelp.SqlDB.Query(strSql.ToString(), DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet SelectMaterialLotList(string sMaterialNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("Select distinct Material_LotNo  FROM Material_Inventory ");

            if (sMaterialNo != "")
            {
                strSql.Append(" where Material_No = @Material_No  ");
            }

            strSql.Append(" order by Material_LotNo  ");

            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar)
            };

            if (sMaterialNo != "")
            {
                paras[0].Value = sMaterialNo;
            }
            else
            {
                paras[0] = null;
            }


            return DBHelp.SqlDB.Query(strSql.ToString(),paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet GetMaterialMachine(string sMaterialNo,string sMachine,DateTime dfrom, DateTime dto)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select

                            b.Material_No as Mat_1st_CodeToMachine,b.Material_LotNo as Mat_1st_LotToMachine,b.Transaction_Weight as Mat_1st_WeightToMachine,b.User_Name as MHID01
                            ,c.Material_No as Mat_2nd_CodeToMachine,c.Material_LotNo as Mat_2nd_LotToMachine,c.Transaction_Weight as Mat_2nd_WeightToMachine,c.User_Name as MHID02


                            ,a.machineID as MC_ID ,convert(varchar(50),cast(a.dateTime as datetime),105) as Date,a.shift as Shift
                            ,a.partNumber as PartNumber
                            ,a.partNumberAll as PartNumberAll
                            ,a.matPart01 as Mat_1st_Code,a.matLot01 as Mat_1st_Lot,a.partsWeight as Mat_1st_Weight
                            ,a.matPart02 as Mat_2nd_Code,a.matLot02 as Mat_2nd_Lot,a.parts2Weight as Mat_1st_Weight
                            from
                            ( select machineID,
                            CONVERT(varchar(10), dateTime,102) as dateTime
                            ,partNumber
                            ,partNumberAll
                            ,[matPart01]
                            ,[matPart02]
                            ,[matLot01]
                            ,[matLot02]
                            ,[partsWeight]
                            ,[parts2Weight]
                            ,shift
                            from MouldingViTracking
                            where dateTime > @dfrom and dateTime < @dto  and ( partsWeight != 0  or parts2Weight != 0)       
                            ) a
                            left join Material_Inventory_History b 
                            on a.dateTime = CONVERT(varchar(10), cast(b.REF_FIELD02 as datetime),102) and a.matPart01 = b.Material_No and b.Last_Event = 'Unload' and a.machineID = b.MachineID and a.shift = b.REF_FIELD01
                            left join Material_Inventory_History c 
                            on a.dateTime = CONVERT(varchar(10), cast(c.REF_FIELD02 as datetime),102) and a.matPart02 = c.Material_No and c.Last_Event = 'Unload' and a.machineID = c.MachineID and a.shift = c.REF_FIELD01 where 1=1 ");
            if (sMachine != "")
            {
                strSql.Append("  and a.machineID = @machineID ");
            }
            if (sMaterialNo != "")
            {
                strSql.Append("  and (a.matPart01 = @Material_No or a.matPart02 =@Material_No) ");
            }
            strSql.Append("   order by a.dateTime asc, a.machineID asc");

                SqlParameter[] paras =
                 {
                    new SqlParameter("@Material_No",SqlDbType.VarChar),
                    new SqlParameter("@machineID",SqlDbType.VarChar),
                    new SqlParameter("@dfrom",SqlDbType.DateTime),
                    new SqlParameter("@dto",SqlDbType.DateTime),
                };

                if (sMaterialNo != "")
                {
                    paras[0].Value = sMaterialNo;
                }
                else
                {
                    paras[0] = null;
                }

                if (sMachine != "")
                {
                    paras[1].Value = sMachine;
                }
                else
                {
                    paras[1] = null;
                }

                if (dfrom != null)
                {
                    paras[2].Value = dfrom;
                }
                else
                {
                    paras[2] = null;
                }
            if (dto != null)
            {
                paras[3].Value = dto;
            }
            else
            {
                paras[3] = null;
            }
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }

        public DataSet GetMaterialMonthly(DateTime dfrom, DateTime dto)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"          										select left(k.MonthInfo,4) + case  substring(k.MonthInfo,6,(len(k.MonthInfo) )) when '01'
                                                then '-Jan'                                                when '02'
                                                then '-Feb'                                                when '03'
                                                then '-Mar'                                                when '04'
                                                then '-Apr'                                                when '05'
                                                then '-May'                                                when '06'
                                                then '-June'                                                when '07'
                                                then '-July'                                                when '08'
                                                then '-Aug'                                                when '09'
                                                then '-Sept'                                                when '10'
                                                then '-Oct'                                                when '11'
                                                then '-Nov'                                                when '12'
                                                then '-Dec'                                                else '-01'
                                                end as Date
                                        ,convert(int,isnull(aa.Load_Weight,0)) as Purchase_Kgs
                                       ,convert(decimal(18,2),isnull(aa.Load_SGD,0)) as Load_SGD                                            
                                       ,convert(int,isnull(bb.Load_Weight,0)) as Used_Kgs
                                       ,convert(decimal(18,2),isnull(bb.Load_SGD,0)) as Unload_SGD 
                                       ,convert(int,isnull(cc.Load_Weight ,0)) as Return_Kgs
                                       ,convert(decimal(18,2),isnull(cc.Load_SGD,0)) as Return_SGD 
                                        from
                                        (SELECT DATENAME(YEAR, convert(char(10), DATEADD(mm, number, @dfrom), 120)) +'-' + Right('00' + cast(Month(convert(char(10), DATEADD(mm, number, @dfrom ), 120))AS VARCHAR(2)), 2) AS MonthInfo  FROM
                                       master..spt_values        WHERE        type = 'p'  AND DATEDIFF(MI, DATEADD(mm, number,@dfrom),@dto )> 0    )k
                                        left join
                                       (										
										select  p.Last_Event as Last_Event,SUM(p.Load_Weight) as Load_Weight,SUM(p.Load_SGD)as Load_SGD,CONVERT(varchar(7), p.Updated_Time, 120) as monthly from(
										SELECT DATENAME(YEAR, convert(char(10), DATEADD(mm, number, @dfrom), 120)) +'-' + Right('00' + cast(Month(convert(char(10), DATEADD(mm, number, @dfrom ), 120))AS VARCHAR(2)), 2) AS MonthInfo  FROM
                                       master..spt_values        WHERE        type = 'p'  AND DATEDIFF(MI, DATEADD(mm, number,@dfrom),@dto )> 0    )a
                                        left join(
                                        SELECT  									        
                                          [Last_Event]
                                          ,([Transaction_Weight]) as Load_Weight
                                          ,(isnull([Transaction_Weight],0.0)*( SELECT bomB1.Unit_Price from Material_Inventory_Bom bomB1 where  Material_No = bomB1.Material_Part)) as Load_SGD                                            
                                          ,Updated_Time
                                          FROM Material_Inventory_History
                                          where Updated_Time >=@dfrom and Updated_Time < @dto and Last_Event = 'Load'                                                                               
                                          ) p on a.MonthInfo = CONVERT(varchar(7), p.Updated_Time, 120) group by p.Last_Event,CONVERT(varchar(7), p.Updated_Time, 120)
                                        ) aa on aa.monthly = k.MonthInfo
                                        left join
                                       (										
										select  q.Last_Event as Last_Event,SUM(q.Load_Weight) as Load_Weight,SUM(q.Load_SGD)as Load_SGD,CONVERT(varchar(7), q.Updated_Time, 120) as monthly from(
										SELECT DATENAME(YEAR, convert(char(10), DATEADD(mm, number, @dfrom), 120)) +'-' + Right('00' + cast(Month(convert(char(10), DATEADD(mm, number, @dfrom ), 120))AS VARCHAR(2)), 2) AS MonthInfo  FROM
                                       master..spt_values        WHERE        type = 'p'  AND DATEDIFF(MI, DATEADD(mm, number,@dfrom),@dto )> 0    )b
                                        left join(
                                        SELECT  									        
                                          [Last_Event]
                                          ,([Transaction_Weight]) as Load_Weight
                                          ,(isnull([Transaction_Weight],0.0)*( SELECT bomB1.Unit_Price from Material_Inventory_Bom bomB1 where  Material_No = bomB1.Material_Part)) as Load_SGD                                            
                                          ,Updated_Time
                                          FROM Material_Inventory_History
                                          where Updated_Time >=@dfrom and Updated_Time < @dto and Last_Event = 'Unload'                                                                        
                                          ) q on b.MonthInfo = CONVERT(varchar(7), q.Updated_Time, 120) group by q.Last_Event,CONVERT(varchar(7), q.Updated_Time, 120)
                                        ) bb on bb.monthly = k.MonthInfo
                                        left join
                                       (										
										select  m.Last_Event as Last_Event,SUM(m.Load_Weight) as Load_Weight,SUM(m.Load_SGD)as Load_SGD,CONVERT(varchar(7), m.Updated_Time, 120) as monthly from(
										SELECT DATENAME(YEAR, convert(char(10), DATEADD(mm, number, @dfrom), 120)) +'-' + Right('00' + cast(Month(convert(char(10), DATEADD(mm, number, @dfrom ), 120))AS VARCHAR(2)), 2) AS MonthInfo  FROM
                                       master..spt_values        WHERE        type = 'p'  AND DATEDIFF(MI, DATEADD(mm, number,@dfrom),@dto )> 0    )c
                                        left join(
                                        SELECT  									        
                                          [Last_Event]
                                          ,([Transaction_Weight]) as Load_Weight
                                          ,(isnull([Transaction_Weight],0.0)*( SELECT bomB1.Unit_Price from Material_Inventory_Bom bomB1 where  Material_No = bomB1.Material_Part)) as Load_SGD                                            
                                          ,Updated_Time
                                          FROM Material_Inventory_History
                                          where Updated_Time >=@dfrom and Updated_Time < @dto and Last_Event = 'Return'                                                                        
                                          ) m on c.MonthInfo = CONVERT(varchar(7), m.Updated_Time, 120) group by m.Last_Event,CONVERT(varchar(7), m.Updated_Time, 120)
                                        ) cc on cc.monthly = k.MonthInfo ");


            SqlParameter[] paras =
             {
                    new SqlParameter("@dfrom",SqlDbType.DateTime),
                    new SqlParameter("@dto",SqlDbType.DateTime),
                };


            if (dfrom != null)
            {
                paras[0].Value = dfrom;
            }
            else
            {
                paras[0] = null;
            }
            if (dto != null)
            {
                paras[1].Value = dto;
            }
            else
            {
                paras[1] = null;
            }
            return DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
        public DataSet SelectGroupByPart(string sMaterial_No)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" select  
                            Material_No
                            ,CONVERT(varchar, convert(float, convert(dec(18, 2), SUM(Inventory_Weight)))) + ' kg' as Inventory_Weight
                            ,CONVERT(varchar, convert(float, convert(dec(18, 2), SUM(Inventory_Weight)))) as Inventory_Weight_temp
                            ,'SGD ' + CONVERT(varchar, SUM(CONVERT(float, CONVERT(dec(18, 2), a.Inventory_Weight * ISNULL(b.Unit_Price, 0))))) as TotalCost
                            ,CONVERT(varchar, SUM(CONVERT(float, CONVERT(dec(18, 2), a.Inventory_Weight * ISNULL(b.Unit_Price, 0))))) as TotalCost_temp
                            from Material_Inventory a left
                            join Material_Inventory_Bom b on a.Material_No = b.Material_Part
                            where 1=1 ");

            if (sMaterial_No != "")
            {
                strSql.Append(" and Material_No = @Material_No ");
            }

            strSql.Append(" group by Material_No ");

            strSql.Append(" order by Material_No asc ");

            
            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar)
            };

            if (sMaterial_No != "")
            {
                paras[0].Value = sMaterial_No;
            }
            else
            {
                paras[0] = null;
            }
            

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;
        }

        public DataSet SelectLotNoByPart(string sMaterial_No, string sSupplier)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select  Material_LotNo  from Material_Inventory where 1=1 ");

            if (sMaterial_No != "")
            {
                strSql.Append(" and Material_No = @Material_No ");
            }
            if (sSupplier != "")
            {
                strSql.Append(" and Supplier = @Supplier ");
            }

            strSql.Append(" order by Material_LotNo asc ");

            

            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No",SqlDbType.VarChar),
                new SqlParameter("@Supplier",SqlDbType.VarChar)
            };

            if (sMaterial_No != "")
            {
                paras[0].Value = sMaterial_No;
            }
            else
            {
                paras[0] = null;
            }

            if (sSupplier != "")
            {
                paras[1].Value = sSupplier;
            }
            else
            {
                paras[1] = null;
            }

            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

            return ds;
        }






        public SqlCommand DeleteCMD(string sMaterial_No, string sMaterial_LotNo, DateTime? Load_Time)
        {
            if (string.IsNullOrEmpty(sMaterial_No) || string.IsNullOrEmpty(sMaterial_LotNo) || Load_Time == null)
            {
                return null;
            }

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" delete from  Material_Inventory ");
            strSql.Append(" where Material_No = @Material_No ");
            strSql.Append(" and  Material_LotNo = @Material_LotNo ");
            strSql.Append(" and  Load_Time = @Load_Time ");

            SqlParameter[] paras =
            {
                new SqlParameter("@Material_No", SqlDbType.VarChar) ,
                new SqlParameter("@Material_LotNo", SqlDbType.VarChar) ,
                new SqlParameter("@Load_Time", SqlDbType.DateTime)
            };

            paras[0].Value = sMaterial_No;
            paras[1].Value = sMaterial_LotNo;
            paras[2].Value = Load_Time;


            return DBHelp.SqlDB.generateCommand(strSql.ToString(),paras);
        }
        
        public SqlCommand AddCMD(Common.Class.Model.Material_Inventory model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into Material_Inventory( ");
            strSql.Append(" Material_No,Material_LotNo,Inventory_Weight,Transaction_Weight,Last_Event,Load_Time,Updated_Time,Supplier,MachineID,Remarks,User_Name");
            strSql.Append(" ) values (");
            strSql.Append(" @Material_No,@Material_LotNo,@Inventory_Weight,@Transaction_Weight,@Last_Event,@Load_Time,@Updated_Time,@Supplier,@MachineID,@Remarks,@User_Name )");


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
                new SqlParameter("@User_Name",SqlDbType.VarChar)
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
            

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
        }
        
        public SqlCommand UpdateCMD(Common.Class.Model.Material_Inventory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Material_Inventory set ");
           
            strSql.Append("Inventory_Weight=@Inventory_Weight,");
            strSql.Append("Transaction_Weight=@Transaction_Weight,");
            strSql.Append("Last_Event=@Last_Event,");
            strSql.Append("Updated_Time=@Updated_Time,");
            strSql.Append("User_Name=@User_Name, ");

            strSql.Append("MachineID=@MachineID ");

            strSql.Append(" where Material_No=@Material_No ");
            strSql.Append(" and Material_LotNo=@Material_LotNo ");
            strSql.Append(" and Load_Time = @Load_Time ");
            


            SqlParameter[] parameters = {
                    new SqlParameter("@Inventory_Weight",SqlDbType.Decimal,9),
                    new SqlParameter("@Transaction_Weight",SqlDbType.Decimal,9),
                    new SqlParameter("@Last_Event",SqlDbType.VarChar),
                    new SqlParameter("@Updated_Time",SqlDbType.DateTime),
                    new SqlParameter("@User_Name",SqlDbType.VarChar),
                    new SqlParameter("@Material_No",SqlDbType.VarChar),
                    new SqlParameter("@Material_LotNo",SqlDbType.VarChar),
                    new SqlParameter("@Load_Time",SqlDbType.DateTime),
                    new SqlParameter("@MachineID",SqlDbType.VarChar)
          };



            parameters[0].Value = model.Inventory_Weight == null ? (object)DBNull.Value : model.Inventory_Weight;
            parameters[1].Value = model.Transaction_Weight == null ? (object)DBNull.Value : model.Transaction_Weight;
            parameters[2].Value = model.Last_Event == null ? (object)DBNull.Value : model.Last_Event;
            parameters[3].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
            parameters[4].Value = model.User_Name == null ? (object)DBNull.Value : model.User_Name;
            parameters[5].Value = model.Material_No == null ? (object)DBNull.Value : model.Material_No;
            parameters[6].Value = model.Material_LotNo == null ? (object)DBNull.Value : model.Material_LotNo;
            parameters[7].Value = model.Load_Time == null ? (object)DBNull.Value : model.Load_Time;
            parameters[8].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;

            return DBHelp.SqlDB.generateCommand(strSql.ToString(), parameters);
      
        }


        #region not use

        //public bool Update(Common.Class.Model.Material_Inventory model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update Material_Inventory set ");
        //    strSql.Append("Material_Part=@Material_Part,");
        //    strSql.Append("Weight=@Weight,");
        //    strSql.Append("User_Name=@User_Name,");
        //    strSql.Append("Last_Event=@Last_Event,");
        //    strSql.Append("Load_Time=@Load_Time,");
        //    strSql.Append("Updated_Time=@Updated_Time,");
        //    strSql.Append("REF_FIELD01=@REF_FIELD01,");
        //    strSql.Append("REF_FIELD02=@REF_FIELD02,");
        //    strSql.Append("REF_FIELD03=@REF_FIELD03,");
        //    strSql.Append("Remarks=@Remarks");
        //    strSql.Append(" where Material_Part=@Material_Part");
        //    strSql.Append(" and REF_FIELD01=@REF_FIELD01");
        //    strSql.Append(" and Load_Time=@Load_Time");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Material_Part",SqlDbType.VarChar),
        //            new SqlParameter("@Weight",SqlDbType.Decimal,9),
        //            new SqlParameter("@User_Name",SqlDbType.VarChar),
        //            new SqlParameter("@Last_Event",SqlDbType.VarChar),
        //            new SqlParameter("@Load_Time",SqlDbType.DateTime),
        //            new SqlParameter("@Updated_Time",SqlDbType.DateTime),
        //            new SqlParameter("@REF_FIELD01",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD02",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD03",SqlDbType.VarChar),
        //            new SqlParameter("@Remarks",SqlDbType.VarChar)};
        //    parameters[0].Value = model.Material_No == null ? (object)DBNull.Value : model.Material_No;
        //    parameters[1].Value = model.Inventory_Weight == null ? (object)DBNull.Value : model.Inventory_Weight;
        //    parameters[2].Value = model.User_Name == null ? (object)DBNull.Value : model.User_Name;
        //    parameters[3].Value = model.Last_Event == null ? (object)DBNull.Value : model.Last_Event;
        //    parameters[4].Value = model.Load_Time == null ? (object)DBNull.Value : model.Load_Time;
        //    parameters[5].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
        //    parameters[6].Value = model.Material_LotNo == null ? (object)DBNull.Value : model.Material_LotNo;
        //    parameters[7].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
        //    parameters[8].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
        //    parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;
        //    int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool Unload(Common.Class.Model.Material_Inventory model)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("insert into Material_Inventory(");
        //    strSql.Append("Material_Part,Weight,User_Name,Last_Event,Load_Time,Updated_Time,REF_FIELD01,REF_FIELD02,REF_FIELD03,Remarks)");
        //    strSql.Append("values (");
        //    strSql.Append("@Material_Part,@Weight,@User_Name,@Last_Event,@Load_Time,@Updated_Time,@REF_FIELD01,@REF_FIELD02,@REF_FIELD03,@Remarks)");


        //    SqlParameter[] parameters =
        //    {
        //            new SqlParameter("@Material_Part",SqlDbType.VarChar),
        //            new SqlParameter("@Weight",SqlDbType.Decimal,9),
        //            new SqlParameter("@User_Name",SqlDbType.VarChar),
        //            new SqlParameter("@Last_Event",SqlDbType.VarChar),
        //            new SqlParameter("@Load_Time",SqlDbType.DateTime),
        //            new SqlParameter("@Updated_Time",SqlDbType.DateTime),
        //            new SqlParameter("@REF_FIELD01",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD02",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD03",SqlDbType.VarChar),
        //            new SqlParameter("@Remarks",SqlDbType.VarChar),

        //        };
        //    parameters[0].Value = model.Material_No == null ? (object)DBNull.Value : model.Material_No;
        //    parameters[1].Value = model.Inventory_Weight == null ? (object)DBNull.Value : model.Inventory_Weight;
        //    parameters[2].Value = model.User_Name == null ? (object)DBNull.Value : model.User_Name;
        //    parameters[3].Value = model.Last_Event == null ? (object)DBNull.Value : model.Last_Event;
        //    parameters[4].Value = model.Load_Time == null ? (object)DBNull.Value : model.Load_Time;
        //    parameters[5].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
        //    parameters[6].Value = model.Material_LotNo == null ? (object)DBNull.Value : model.Material_LotNo;
        //    parameters[7].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
        //    parameters[8].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
        //    parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;


        //    int rows = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public int Delete(string sMaterial_No, string sMaterial_LotNo, DateTime? dLoad_Time)
        //{
        //    if (string.IsNullOrEmpty(sMaterial_No) || string.IsNullOrEmpty(sMaterial_LotNo) || dLoad_Time == null)
        //    {
        //        return 0;
        //    }

        //    string strSql = " delete from  Material_Inventory where Material_No = @Material_No and  Material_LotNo = @Material_LotNo and  Load_Time = @Load_Time";

        //    SqlParameter[] paras = {
        //        new SqlParameter("@Material_No", SqlDbType.VarChar) ,
        //        new SqlParameter("@Material_LotNo", SqlDbType.VarChar) ,
        //        new SqlParameter("@Load_Time", SqlDbType.DateTime)
        //    };

        //    paras[0].Value = sMaterial_No;
        //    paras[1].Value = sMaterial_LotNo;
        //    paras[2].Value = dLoad_Time;

        //    int result = DBHelp.SqlDB.ExecuteSql(strSql, paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

        //    return result;
        //}

        //public int Add(Common.Class.Model.Material_Inventory model)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("insert into Material_Inventory(");
        //    strSql.Append("Material_Part,Weight,User_Name,Last_Event,Load_Time,Updated_Time,REF_FIELD01,REF_FIELD02,REF_FIELD03,Remarks)");
        //    strSql.Append("values (");
        //    strSql.Append("@Material_Part,@Weight,@User_Name,@Last_Event,@Load_Time,@Updated_Time,@REF_FIELD01,@REF_FIELD02,@REF_FIELD03,@Remarks)");


        //    SqlParameter[] parameters =
        //    {
        //            new SqlParameter("@Material_Part",SqlDbType.VarChar),
        //            new SqlParameter("@Weight",SqlDbType.Decimal,9),
        //            new SqlParameter("@User_Name",SqlDbType.VarChar),
        //            new SqlParameter("@Last_Event",SqlDbType.VarChar),
        //            new SqlParameter("@Load_Time",SqlDbType.DateTime),
        //            new SqlParameter("@Updated_Time",SqlDbType.DateTime),
        //            new SqlParameter("@REF_FIELD01",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD02",SqlDbType.VarChar),
        //            new SqlParameter("@REF_FIELD03",SqlDbType.VarChar),
        //            new SqlParameter("@Remarks",SqlDbType.VarChar),

        //        };
        //    parameters[0].Value = model.Material_No == null ? (object)DBNull.Value : model.Material_No;
        //    parameters[1].Value = model.Inventory_Weight == null ? (object)DBNull.Value : model.Inventory_Weight;
        //    parameters[2].Value = model.User_Name == null ? (object)DBNull.Value : model.User_Name;
        //    parameters[3].Value = model.Last_Event == null ? (object)DBNull.Value : model.Last_Event;
        //    parameters[4].Value = model.Load_Time == null ? (object)DBNull.Value : model.Load_Time;
        //    parameters[5].Value = model.Updated_Time == null ? (object)DBNull.Value : model.Updated_Time;
        //    parameters[6].Value = model.Material_LotNo == null ? (object)DBNull.Value : model.Material_LotNo;
        //    parameters[7].Value = model.Supplier == null ? (object)DBNull.Value : model.Supplier;
        //    parameters[8].Value = model.MachineID == null ? (object)DBNull.Value : model.MachineID;
        //    parameters[9].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;


        //    int Result = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

        //    return Result;
        //}

        #endregion

    }
}

