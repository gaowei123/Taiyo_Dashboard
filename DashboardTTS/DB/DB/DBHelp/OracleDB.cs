using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace DBHelp
{
    public class OracleDB
    {
        public static int ExecuteSql(string strSql, OracleParameter[] parameters)
        {
            OracleConnection cn = new OracleConnection();

            try
            {
                cn = Connection.Oracle.OracleConn;

                OracleCommand cmd = new OracleCommand(strSql, cn);

                foreach (OracleParameter par in parameters)
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
            OracleConnection cn = new OracleConnection();

            try
            {
                cn = Connection.Oracle.OracleConn;

                OracleCommand cmd = new OracleCommand(strSql, cn); 
               

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
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = strSql;

                ds.Tables.Add(GetData(cmd, Connection.Oracle.OracleConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static System.Data.DataSet Query(string strSql, OracleParameter[] parameters)
        {
            try
            {
                DataSet ds = new DataSet();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = strSql;
                foreach (OracleParameter par in parameters)
                {
                    cmd.Parameters.Add(par);
                }

                ds.Tables.Add(GetData(cmd, Connection.Oracle.OracleConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public static DataTable GetData(OracleCommand cmd, OracleConnection cn)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = cn;
                cn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
            return dt;

        }

        public static bool SetData(OracleCommand cmd, OracleConnection cn)
        {
            try
            {
                cmd.Connection = cn;
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
                cn.Close();
            }

        }

        public static bool SetData_Rollback(List<OracleCommand> cmdlist, OracleConnection cn)
        {
            bool flag = false;
            cn.Open();
            OracleTransaction tran = cn.BeginTransaction();
            try
            {
                for (int i = 0; i <= cmdlist.Count - 1; i++)
                {
                    OracleCommand cmd = cmdlist[i];
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        tran.Rollback();
                        tran.Dispose();
                        tran = null;
                        flag = false;
                        break;
                    }
                    else
                    {
                        flag = true;
                        cmd.Parameters.Clear();
                    }
                }
                if (flag)
                    tran.Commit();
                else
                    throw new System.Exception("Update server failed !!\n更新服务器失败 !!");
                return flag;
            }
            catch(OracleException ee)
            {
                throw ee;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
