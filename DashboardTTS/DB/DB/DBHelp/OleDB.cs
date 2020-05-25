using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace DBHelp
{
   public class OleDB
    {
        public static int ExecuteSql(string strSql, OleDbParameter[] parameters)
        {
            OleDbConnection cn = new OleDbConnection();

            try
            {
                cn = Connection.OleDb.OleDbConn;

                OleDbCommand cmd = new OleDbCommand(strSql, cn);

                foreach (OleDbParameter par in parameters)
                {
                    cmd.Parameters.Add(par);
                }

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }

        }
        public static int ExecuteSql(string strSql)
        {
            OleDbConnection cn = new OleDbConnection();

            try
            {
                cn = Connection.OleDb.OleDbConn;

                OleDbCommand cmd = new OleDbCommand(strSql, cn); 

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }

        }

        public static System.Data.DataSet Query(string strSql)
        {
            try
            {
                DataSet ds = new DataSet();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = strSql;

                ds.Tables.Add(GetData(cmd, Connection.OleDb.OleDbConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static System.Data.DataSet Query(string strSql, OleDbParameter[] parameters)
        {
            try
            {
                DataSet ds = new DataSet();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = strSql;
                foreach (OleDbParameter par in parameters)
                {
                    cmd.Parameters.Add(par);
                }

                ds.Tables.Add(GetData(cmd, Connection.OleDb.OleDbConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public static DataTable GetData(OleDbCommand cmd, OleDbConnection cn)
        {
            cmd.Connection = cn;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();

            //dt.Locale = CultureInfo.InvariantCulture;
            try
            {
                da.Fill(dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return dt;

        }

        public static bool SetData(OleDbCommand cmd, OleDbConnection cn)
        {
            cmd.Connection = cn;
            try
            {
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
    }
}
