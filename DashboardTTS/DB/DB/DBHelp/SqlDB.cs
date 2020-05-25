using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
//using DB;

namespace DBHelp
{
    public class SqlDB
    {
      
        public static int ExecuteSql(string strSql, SqlParameter[] parameters)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn = Connection.SqlServer.SqlConn;

                SqlCommand cmd = new SqlCommand(strSql, cn);

                foreach (SqlParameter par in parameters)
                {
                    if (par!= null)
                    {
                        cmd.Parameters.Add(par);
                    }
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

        public static int ExecuteSql(string strSql, SqlParameter[] parameters, SqlConnection cn)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand(strSql, cn);

                foreach (SqlParameter par in parameters)
                {
                    if (par != null)
                    {
                        cmd.Parameters.Add(par);
                    }
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
            SqlConnection cn = new SqlConnection();

            try
            {
                cn = Connection.SqlServer.SqlConn;

                SqlCommand cmd = new SqlCommand(strSql, cn);

              
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strSql;
                ds.Tables.Add(GetData(cmd, Connection.SqlServer.SqlConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        

        public static System.Data.DataSet  Query(string strSql , SqlParameter[] parameters)
        {
            try
            { 
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strSql;
                foreach (SqlParameter par in parameters)
                {
                    if (par != null)  //2017 02 13 remove null parameter
                    {
                        cmd.Parameters.Add(par);
                    }
                }

                ds.Tables.Add(GetData(cmd, Connection.SqlServer.SqlConn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //2017 02 28
        public static SqlCommand generateCommand(string strSql, SqlParameter[] spParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSql;
            foreach (SqlParameter par in spParameters)
            {
                if (par != null)  //2017 02 13 remove null parameter
                {
                    cmd.Parameters.Add(par);
                }
            }
            return cmd;
        }

        public static SqlCommand generateCommand(string strSql, SqlParameter[] spParameters, SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cn);
            //cmd.CommandText = strSql;
            foreach (SqlParameter par in spParameters)
            {
                if (par != null)  //2017 02 13 remove null parameter
                {
                    cmd.Parameters.Add(par);
                }
            }
            return cmd;
        }



        //2017 02 28
        public static System.Data.DataSet Query(string strSql, SqlConnection cn)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strSql;

                ds.Tables.Add(GetData(cmd, cn));
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //2017 02 28
        public static System.Data.DataSet Query(string strSql, SqlParameter[] parameters, SqlConnection cn)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strSql;
                
                foreach (SqlParameter par in parameters)
                {
                    if (par != null)  //2017 02 13 remove null parameter
                    {
                        cmd.Parameters.Add(par);
                    }
                }

                ds.Tables.Add(GetData(cmd, cn));
              
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public static System.Data.DataTable GetData(SqlCommand cmd)
        {
            return  GetData(cmd, Connection.SqlServer.SqlConn);
        }
        public static System.Data.DataTable GetData(SqlCommand cmd, SqlConnection cn)
        {
            cmd.Connection = cn;
            
            DataTable dt = new DataTable();
            try
            {
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

           

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
     
        public static bool SetData(SqlCommand cmd, SqlConnection cn)
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
                cn.Close();
            }

        }

        public static bool SetData_Rollback(List<SqlCommand> cmdlist)
        {
            return SetData_Rollback(cmdlist, Connection.SqlServer.SqlConn);
        }



        public static bool SetData_Rollback(List<SqlCommand> cmdlist, SqlConnection cn)
        {
          
            bool flag = true;
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction("test");
            tran.Save("start");
            try
            {
                for (int i = 0; i <= cmdlist.Count - 1; i++)
                {
                    SqlCommand cmd = cmdlist[i];
                
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    
                    int result = cmd.ExecuteNonQuery();
                    
                    if (result < 0)
                    {
                        tran.Rollback("start");
                        tran.Dispose();
                        tran = null;
                        flag = false;
                        break;
                    }
                    else
                    {
                        cmd.Parameters.Clear();
                    }
                }

        
                if (flag)
                {
                    tran.Commit();
                }
                return flag;
            }
            catch(Exception ee)
            {
                tran.Rollback("start");
                return false;
            }
            finally
            {
                cn.Close();
            }
        }


        public static bool SetData_SqlBulk(DataSet ds)
        {
            return SetData_SqlBulk(ds, DBHelp.Connection.SqlServer.SqlConn);
        }


        public static bool SetData_SqlBulk(DataSet ds, SqlConnection cn)
        {
            bool flag = true;
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction("test");
            tran.Save("start");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(cn, SqlBulkCopyOptions.Default, tran);

            try
            {
                if (ds.Tables == null ||
                    ds.Tables.Count == 0)
                    flag = false;
                else
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            bulkCopy.DestinationTableName = dt.TableName;
                            bulkCopy.BatchSize = dt.Rows.Count;
                            bulkCopy.WriteToServer(dt);
                        }
                    }

                    tran.Commit();
                }
                return flag;
            }
            catch (Exception ex)
            {
                Reports.LogFile.Log("Oracle", "[SetData_Rollback][ex=" + ex.ToString());

                tran.Rollback("start");
                return false;
            }
            finally
            {
                cn.Close();
            }
        }


    }
}
