using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common.Class.DAL
{
    public class MouldingPartsMovment_DAL
    {

        public int Add(Common.Class.Model.MouldingTransfer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MouldingTransfer(");
            strSql.Append("Material_Part,Transfer_To,Transfer_Date,Quantity,Opr_ID,Production_Date,Annealing_Process,Annealing_Date_From,Annealing_Date_To,Annealing_Temperature,Update_User,Status,refField01,refField02,refField03,refField04,refField05,refField06,refField07,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@Material_Part,@Transfer_To,@Transfer_Date,@Quantity,@Opr_ID,@Production_Date,@Annealing_Process,@Annealing_Date_From,@Annealing_Date_To,@Annealing_Temperature,@Update_User,@Status,@refField01,@refField02,@refField03,@refField04,@refField05,@refField06,@refField07,@Remarks)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_To", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_Date", SqlDbType.DateTime),
                    new SqlParameter("@Quantity", SqlDbType.VarChar,50),
                    new SqlParameter("@Opr_ID", SqlDbType.VarChar,50),
                    new SqlParameter("@Production_Date", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Process", SqlDbType.VarChar,50),
                    new SqlParameter("@Annealing_Date_From", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Date_To", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Temperature", SqlDbType.VarChar,50),
                    new SqlParameter("@Update_User", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),
                    new SqlParameter("@refField06", SqlDbType.VarChar,50),
                    new SqlParameter("@refField07", SqlDbType.VarChar,50),
                    new SqlParameter("@Remarks", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Material_Part == null ? (object)DBNull.Value : model.Material_Part; 
            parameters[1].Value = model.Transfer_To == null ? (object)DBNull.Value : model.Transfer_To; 
            parameters[2].Value = model.Transfer_Date == null ? (object)DBNull.Value : model.Transfer_Date; 
            parameters[3].Value = model.Quantity == null ? (object)DBNull.Value : model.Quantity; 
            parameters[4].Value = model.Opr_ID == null ? (object)DBNull.Value : model.Opr_ID; 
            parameters[5].Value = model.Production_Date == null ? (object)DBNull.Value : model.Production_Date; 
            parameters[6].Value = model.Annealing_Process == null ? (object)DBNull.Value : model.Annealing_Process; 
            parameters[7].Value = model.Annealing_Date_From == null ? (object)DBNull.Value : model.Annealing_Date_From; 
            parameters[8].Value = model.Annealing_Date_To == null ? (object)DBNull.Value : model.Annealing_Date_To; 
            parameters[9].Value = model.Annealing_Temperature == null ? (object)DBNull.Value : model.Annealing_Temperature ;
            parameters[10].Value = model.Update_User == null ? (object)DBNull.Value : model.Update_User; 
            parameters[11].Value = model.Status == null ? (object)DBNull.Value : model.Status; 
            parameters[12].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[13].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02; 
            parameters[14].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03; 
            parameters[15].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04; 
            parameters[16].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05; 
            parameters[17].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06; 
            parameters[18].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07; 
            parameters[19].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks; 
            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);

        }

        public DataSet GetModel(string Material_Part, string Transfer_To, DateTime Transfer_Date_From, DateTime Transfer_Date_To, DateTime Production_Date)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Material_Part,Transfer_To,Transfer_Date,Quantity,Opr_ID,Production_Date,Annealing_Process,Annealing_Date_From,Annealing_Date_To,Annealing_Temperature,Update_User,Status,refField01,refField02,refField03,refField04,refField05,refField06,refField07,Remarks from MouldingTransfer ");
            strSql.Append(" where  Status = '1' ");
            if (Material_Part.Length > 0)
            {
                strSql.Append(" and Material_Part=@Material_Part ");
            }
            if (Transfer_To.Length > 0)
            {
                strSql.Append(" and Transfer_To=@Transfer_To ");
            }
            if (Transfer_Date_From.ToString().Length > 0)
            {
                strSql.Append(" and Transfer_Date >= @Transfer_Date_From  ");
            }
            if (Transfer_Date_To.ToString().Length > 0)
            {
                strSql.Append(" and Transfer_Date <= @Transfer_Date_To  ");
            }
            //if (Production_Date.ToString().Length > 0)
            //{
            //    strSql.Append(" Production_Date =@Production_Date  ");
            //}
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_To", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_Date_From", SqlDbType.DateTime),
                    new SqlParameter("@Transfer_Date_To", SqlDbType.DateTime),
                    new SqlParameter("@Production_Date", SqlDbType.DateTime),
       };
            parameters[0].Value = Material_Part == null ? (object)DBNull.Value : Material_Part;
            parameters[1].Value = Transfer_To == null ? (object)DBNull.Value : Transfer_To;
            parameters[2].Value = Transfer_Date_From == null ? (object)DBNull.Value : Transfer_Date_From;
            parameters[3].Value = Transfer_Date_To == null ? (object)DBNull.Value : Transfer_Date_To;
            parameters[4].Value = Production_Date == null ? (object)DBNull.Value : Production_Date;

            Common.Class.Model.MouldingTransfer model = new Common.Class.Model.MouldingTransfer();
            DataSet ds = DBHelp.SqlDB.Query(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        public int Update(Common.Class.Model.MouldingTransfer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MouldingTransfer set ");
            //strSql.Append("Material_Part=@Material_Part,");
            //strSql.Append("Transfer_To=@Transfer_To,");
            //strSql.Append("Transfer_Date=@Transfer_Date,");
            //strSql.Append("Quantity=@Quantity,");
            //strSql.Append("Opr_ID=@Opr_ID,");
            //strSql.Append("Production_Date=@Production_Date,");
            //strSql.Append("Annealing_Process=@Annealing_Process,");
            //strSql.Append("Annealing_Date_From=@Annealing_Date_From,");
            //strSql.Append("Annealing_Date_To=@Annealing_Date_To,");
            //strSql.Append("Annealing_Temperature=@Annealing_Temperature,");
            //strSql.Append("Update_User=@Update_User,");
            strSql.Append("Status=@Status");
            //strSql.Append("refField01=@refField01,");
            //strSql.Append("refField02=@refField02,");
            //strSql.Append("refField03=@refField03,");
            //strSql.Append("refField04=@refField04,");
            //strSql.Append("refField05=@refField05,");
            //strSql.Append("refField06=@refField06,");
            //strSql.Append("refField07=@refField07,");
            //strSql.Append("Remarks=@Remarks");
            strSql.Append(" where Material_Part=@Material_Part and Transfer_To=@Transfer_To and Transfer_Date=@Transfer_Date and Quantity=@Quantity and Opr_ID=@Opr_ID and Production_Date=@Production_Date and Annealing_Process=@Annealing_Process ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Material_Part", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_To", SqlDbType.VarChar,50),
                    new SqlParameter("@Transfer_Date", SqlDbType.DateTime),
                    new SqlParameter("@Quantity", SqlDbType.VarChar,50),
                    new SqlParameter("@Opr_ID", SqlDbType.VarChar,50),
                    new SqlParameter("@Production_Date", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Process", SqlDbType.VarChar,50),
                    new SqlParameter("@Annealing_Date_From", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Date_To", SqlDbType.DateTime),
                    new SqlParameter("@Annealing_Temperature", SqlDbType.VarChar,50),
                    new SqlParameter("@Update_User", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                    new SqlParameter("@refField01", SqlDbType.VarChar,50),
                    new SqlParameter("@refField02", SqlDbType.VarChar,50),
                    new SqlParameter("@refField03", SqlDbType.VarChar,50),
                    new SqlParameter("@refField04", SqlDbType.VarChar,50),
                    new SqlParameter("@refField05", SqlDbType.VarChar,50),
                    new SqlParameter("@refField06", SqlDbType.VarChar,50),
                    new SqlParameter("@refField07", SqlDbType.VarChar,50),
                    new SqlParameter("@Remarks", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Material_Part == null ? (object)DBNull.Value : model.Material_Part;
            parameters[1].Value = model.Transfer_To == null ? (object)DBNull.Value : model.Transfer_To;
            parameters[2].Value = model.Transfer_Date == null ? (object)DBNull.Value : model.Transfer_Date;
            parameters[3].Value = model.Quantity == null ? (object)DBNull.Value : model.Quantity;
            parameters[4].Value = model.Opr_ID == null ? (object)DBNull.Value : model.Opr_ID;
            parameters[5].Value = model.Production_Date == null ? (object)DBNull.Value : model.Production_Date;
            parameters[6].Value = model.Annealing_Process == null ? (object)DBNull.Value : model.Annealing_Process;
            parameters[7].Value = model.Annealing_Date_From == null ? (object)DBNull.Value : model.Annealing_Date_From;
            parameters[8].Value = model.Annealing_Date_To == null ? (object)DBNull.Value : model.Annealing_Date_To;
            parameters[9].Value = model.Annealing_Temperature == null ? (object)DBNull.Value : model.Annealing_Temperature;
            parameters[10].Value = model.Update_User == null ? (object)DBNull.Value : model.Update_User;
            parameters[11].Value = model.Status == null ? (object)DBNull.Value : model.Status;
            parameters[12].Value = model.refField01 == null ? (object)DBNull.Value : model.refField01;
            parameters[13].Value = model.refField02 == null ? (object)DBNull.Value : model.refField02;
            parameters[14].Value = model.refField03 == null ? (object)DBNull.Value : model.refField03;
            parameters[15].Value = model.refField04 == null ? (object)DBNull.Value : model.refField04;
            parameters[16].Value = model.refField05 == null ? (object)DBNull.Value : model.refField05;
            parameters[17].Value = model.refField06 == null ? (object)DBNull.Value : model.refField06;
            parameters[18].Value = model.refField07 == null ? (object)DBNull.Value : model.refField07;
            parameters[19].Value = model.Remarks == null ? (object)DBNull.Value : model.Remarks;
            return DBHelp.SqlDB.ExecuteSql(strSql.ToString(), parameters, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server);
        }
    }
}
