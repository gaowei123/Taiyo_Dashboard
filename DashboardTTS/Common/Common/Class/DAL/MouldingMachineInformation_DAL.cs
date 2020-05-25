using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.DAL
{
    public class MouldingMachineInformation_DAL
    {

        public class Machine_DaL
        {
            public DataSet SelectAll()
            {
                string strSql = @"SELECT 
                                [PartModel]  , 'Machine' + [MachineID] as MachineID,[Type] ,[MakerModel],[Maker],[Model]
                                ,[SerialNo] ,[Date],[CTRL] ,[UpdatedTime] ,[ControllerType],[ControllerSerialNo],Info
                                ,case when DATEPART(MM,DateOfManu) = 1 then 'Jan'
	                            when DATEPART(MM,DateOfManu) = 2 then 'Feb'
                                when DATEPART(MM,DateOfManu) = 3 then 'Mar'
                                when DATEPART(MM,DateOfManu) = 4 then 'Apr'
                                when DATEPART(MM,DateOfManu) = 5 then 'May'
                                when DATEPART(MM,DateOfManu) = 6 then 'Jun'
                                when DATEPART(MM,DateOfManu) = 7 then 'Jul'
                                when DATEPART(MM,DateOfManu) = 8 then 'Aug'
                                when DATEPART(MM,DateOfManu) = 9 then 'Sep'
                                when DATEPART(MM,DateOfManu) = 10 then 'Oct'
                                when DATEPART(MM,DateOfManu) = 11 then 'Nov'
                                when DATEPART(MM,DateOfManu) = 12 then 'Dec'
                                end + '-'+ CONVERT(varchar,DATEPART(YYYY,DateOfManu))  as DateOfManu
                                ,DateOfManu as DateTime

                                ,ISNULL( CONVERT(varchar, ScrewDiameter )+ ' mm','NA') as ScrewDiameter
                                ,ISNULL( CONVERT(varchar, MaxOPNStroke )+ ' mm','Na') as MaxOPNStroke
                                ,ISNULL( CONVERT(varchar, EJTStroke )+ ' mm','NA') as EJTStroke
                                ,ISNULL( TiebarDistance + ' mm','NA') as TiebarDistance
                                ,ISNULL( MinMoldSize + ' mm','NA') as MinMoldSize
                                ,ISNULL( CONVERT(varchar, MinMoldThickness )+ ' mm','NA') as MinMoldThickness 
                                ,ISNULL( Dimensions+' mm','NA') as Dimensions

                                FROM [MouldingMachineInfo]";
                
                return DBHelp.SqlDB.Query(strSql, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            }



            public int Update(Common.Class.Model.MouldingMachineInformation_Model model)
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("Update MouldingMachineInfo set ");

                if (model.PartModel == StaticRes.Global.MouldingModelType.Machine)
                {
                    strSql.Append(" type = @type ");
                    strSql.Append(" ,makermodel = @makermodel ");
                    strSql.Append(" ,serialNo = @serialNo ");
                    strSql.Append(" ,DateOfManu = @DateOfManu ");
                    strSql.Append(" ,CTRL = @CTRL ");
                }
                else if (model.PartModel == StaticRes.Global.MouldingModelType.RobotArm)
                {
                    strSql.Append(" model = @model ");
                    strSql.Append(" ,serialNo = @serialNo ");
                    strSql.Append(" ,controllerType = @controllerType ");
                    strSql.Append(" ,controllerSerialNo = @controllerSerialNo ");
                    strSql.Append(" ,Date = @Date ");
                }
                else if (model.PartModel == StaticRes.Global.MouldingModelType.Temperature)
                {
                    strSql.Append(" Maker = @Maker ");
                    strSql.Append(" ,Model = @Model ");
                    strSql.Append(" ,Date = @Date ");
                }
                else if (model.PartModel == StaticRes.Global.MouldingModelType.Dryer)
                {
                    strSql.Append(" Maker = @Maker ");
                    strSql.Append(" ,Model = @Model ");
                    strSql.Append(" ,Date = @Date ");
                }
                else if (model.PartModel == StaticRes.Global.MouldingModelType.Main)
                {
                    strSql.Append(" maker = @maker ");
                    strSql.Append(" ,Info = @Info ");
                    strSql.Append(" ,Model = @Model ");
                    strSql.Append(" ,DateOfManu = @DateOfManu ");
                    strSql.Append(" ,ScrewDiameter = @ScrewDiameter ");
                    strSql.Append(" ,MaxOPNStroke = @MaxOPNStroke ");
                    strSql.Append(" ,EJTStroke = @EJTStroke ");
                    strSql.Append(" ,TiebarDistance = @TiebarDistance ");
                    strSql.Append(" ,MinMoldSize = @MinMoldSize ");
                    strSql.Append(" ,MinMoldThickness = @MinMoldThickness ");
                    strSql.Append(" ,Dimensions = @Dimensions ");
                }


                strSql.Append(" ,updatedTime = @updatedTime");
                strSql.Append(" where machineid = @machineid ");
                strSql.Append(" and partModel = @partModel ");


                SqlParameter[] paras =
                {
                    new SqlParameter("@type",SqlDbType.VarChar),
                    new SqlParameter("@makermodel",SqlDbType.VarChar),
                    new SqlParameter("@serialNo",SqlDbType.VarChar),
                    new SqlParameter("@DateOfManu",SqlDbType.DateTime),
                    new SqlParameter("@CTRL",SqlDbType.VarChar),
                    new SqlParameter("@model",SqlDbType.VarChar),
            
                    new SqlParameter("@controllerType",SqlDbType.VarChar),
                    new SqlParameter("@controllerSerialNo",SqlDbType.VarChar),
                    new SqlParameter("@Date",SqlDbType.VarChar),
                    new SqlParameter("@Maker",SqlDbType.VarChar),
                    new SqlParameter("@machineid",SqlDbType.VarChar),
                    new SqlParameter("@partModel",SqlDbType.VarChar),
                    new SqlParameter("@updatedTime",SqlDbType.DateTime),

                    new SqlParameter("@Info",SqlDbType.VarChar),
                    new SqlParameter("@ScrewDiameter",SqlDbType.Decimal),
                    new SqlParameter("@MaxOPNStroke",SqlDbType.Decimal),
                    new SqlParameter("@EJTStroke",SqlDbType.Decimal),
                    new SqlParameter("@TiebarDistance",SqlDbType.VarChar),
                    new SqlParameter("@MinMoldSize",SqlDbType.VarChar),
                    new SqlParameter("@MinMoldThickness",SqlDbType.Decimal),
                    new SqlParameter("@Dimensions",SqlDbType.VarChar)
                };

                if (model.Type != "" && model.Type != null)
                    paras[0].Value = model.Type;
                else
                    paras[0] = null;

                if (model.MakerModel != "" && model.MakerModel != null)
                    paras[1].Value = model.MakerModel;
                else
                    paras[1] = null;

                if (model.SerialNo != "" && model.SerialNo != null)
                    paras[2].Value = model.SerialNo;
                else
                    paras[2] = null;

                if (model.DateOfManu != DateTime.MinValue)
                    paras[3].Value = model.DateOfManu;
                else
                    paras[3] = null;

                if (model.CTRL != "" && model.CTRL!= null)
                    paras[4].Value = model.CTRL;
                else
                    paras[4] = null;

                if (model.Model != "" && model.Model != null)
                    paras[5].Value = model.Model;
                else
                    paras[5] = null;

                if (model.ControllerType != "" && model.ControllerType != null)
                    paras[6].Value = model.ControllerType;
                else
                    paras[6] = null;

                if (model.ControllerSerialNo != "" && model.ControllerSerialNo != null)
                    paras[7].Value = model.ControllerSerialNo;
                else
                    paras[7] = null;

                if (model.Date != "" && model.Date != null)
                    paras[8].Value = model.Date;
                else
                    paras[8] = null;

                if (model.Maker != "" && model.Maker != null)
                    paras[9].Value = model.Maker;
                else
                    paras[9] = null;

                if (model.MachineID != "" && model.MachineID!= null)
                    paras[10].Value = model.MachineID;
                else
                    paras[10] = null;

                if (model.PartModel != "" && model.PartModel != null)
                    paras[11].Value = model.PartModel;
                else
                    paras[11] = null;

                paras[12].Value = model.UpdatedDate;

                
             


                if (model.Info != "" && model.Info != null)
                    paras[13].Value = model.Info;
                else
                    paras[13] = null;

                if (model.ScrewDiameter !=  null)
                    paras[14].Value = model.ScrewDiameter;
                else
                    paras[14] = null;

                if (model.MaxOPNStroke != null)
                    paras[15].Value = model.MaxOPNStroke;
                else
                    paras[15] = null;

                if (model.EJTStroke  != null)
                    paras[16].Value = model.EJTStroke;
                else
                    paras[16] = null;

                if (model.TiebarDistance != "" && model.TiebarDistance != null)
                    paras[17].Value = model.TiebarDistance;
                else
                    paras[17] = null;

                if (model.MinMoldSize != "" && model.MinMoldSize != null)
                    paras[18].Value = model.MinMoldSize;
                else
                    paras[18] = null;

                if (model.MinMoldThickness  != null)
                    paras[19].Value = model.MinMoldThickness;
                else
                    paras[19] = null;

                if (model.Dimensions != "" && model.Dimensions != null)
                    paras[20].Value = model.Dimensions;
                else
                    paras[20] = null;

                int Result = DBHelp.SqlDB.ExecuteSql(strSql.ToString(), paras, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

                return Result;
            }

        }

        


    }
}
