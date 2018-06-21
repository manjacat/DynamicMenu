using DynamicMenu.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public class SQLHelper : DataHelper
    {
        public virtual string ConnectionString
        {
            get
            {
                string connString = ConfigurationManager.AppSettings["YugiMoto"];
                return connString;
            }
        }

        public SQLHelper()
        {
        }

        #region QuerySet

        public DataSet QuerySet(string sqlquery)
        {
            return QuerySet(sqlquery, null);
        }

        public DataSet QuerySet(string sqlquery, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet("result");
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(sqlquery, myConnection);
                if (parameters != null)
                    myCommand.Parameters.AddRange(parameters);
                myCommand.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = myCommand;
                myCommand = null;
                da.Fill(ds);
                myConnection.Close();
            }
            return ds;
        }
        #endregion


        #region QueryTable
        public override DataTable QueryTable(string sqlquery)
        {
            return QueryTable(sqlquery, null);
        }

        public DataTable QueryTable(string sqlquery, SqlParameter[] parameters)
        {
            DataSet ds = QueryTable(sqlquery, parameters, CommandType.Text);
            return ds.Tables[0];
        }

        public DataSet QueryTable(string StoredProcName, SqlParameter[] parameters, CommandType sqlCommandType)
        {
            DataSet ds = new DataSet("result");

            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
                try
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(StoredProcName, myConnection);
                    if (parameters != null)
                    {
                        myCommand.Parameters.AddRange(parameters);
                    }
                    myCommand.CommandType = sqlCommandType;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = myCommand;
                    myCommand = null;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                        ds.Tables[0].TableName = "main result";
                }
                catch (Exception ex)
                {
                    //Log Error
                    string sqlquery = "Stored Proc: " + StoredProcName;
                    Logging log = new Logging();
                    log.Error(ex, sqlquery);
                    throw ex;
                }
                finally
                {
                    //Ensure connection is always closed even if error
                    myConnection.Close();
                }
            }
            return ds;
        }
        #endregion

        #region ExecNonQuery
        public void ExecNonQuery(string StoredProcName, SqlParameter[] parameters, CommandType sqlCommandType)
        {
            DataSet ds = new DataSet("result");
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (myConnection.State == ConnectionState.Open)
                        myConnection.Close();
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(StoredProcName, myConnection);
                    myCommand.Parameters.AddRange(parameters);
                    myCommand.CommandType = sqlCommandType;
                    myCommand.ExecuteNonQuery();
                    myCommand = null;
                }
                catch (Exception ex)
                {
                    //Log Error
                    string sqlquery = "Stored Proc: " + StoredProcName;
                    Logging log = new Logging();
                    log.Error(ex, sqlquery);
                    throw ex;
                }
                finally
                {
                    //Ensure connection is always closed even if error
                    myConnection.Close();
                }
            }
        }

        public void ExecNonQuery(string SQLscript, SqlParameter[] parameters)
        {
            ExecNonQuery(SQLscript, parameters, CommandType.Text);
        }

        public override void ExecNonQuery(string SQLScript)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            ExecNonQuery(SQLScript, parameters.ToArray());
        }

        public void ExecNonQuery(string sqlscript, string[] parameters)
        {
            for (int i = 0; i < parameters.Count(); i++)
            {
                sqlscript = sqlscript.Replace(":param" + i, parameters[i].ToString());
            }
            DataSet ds = new DataSet("result");
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (myConnection.State == ConnectionState.Open)
                        myConnection.Close();
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(sqlscript, myConnection);
                    myCommand.CommandType = CommandType.Text;
                    myCommand.ExecuteNonQuery();
                    myCommand = null;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    //Log Error
                    string sqlquery = sqlscript;
                    Logging log = new Logging();
                    log.Error(ex, sqlquery);
                    throw ex;
                }
                finally
                {
                    //Ensure connection is always closed even if error
                    myConnection.Close();
                }
            }

        }
        #endregion

    }
}