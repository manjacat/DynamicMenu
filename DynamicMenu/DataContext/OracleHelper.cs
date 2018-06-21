using DynamicMenu.Utility;
using NLog;

using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public class OracleHelper : DataHelper
    {
        public virtual string ConnectionString
        {
            get
            {
                string connString = ConfigurationManager.AppSettings["ORACLE_DB"];
                return connString;
            }
        }

        private OracleConnection OraConn { get; set; }

        //Darrel_19052016_OracleConnectionTCS
        public override DataTable QueryTable(string sqlScript)
        {
            DataTable objTable = new DataTable();
            try
            {
                OracleCommand objcommand = new OracleCommand();
                CreateORAConnection();
                objcommand.Connection = OraConn;
                objcommand.CommandText = sqlScript;
                objcommand.CommandType = CommandType.Text;
                OracleDataAdapter objAdapter = new OracleDataAdapter();
                objAdapter.SelectCommand = new OracleCommand(sqlScript, OraConn);
                objAdapter.Fill(objTable);
            }
            catch (InvalidOperationException ioe)
            {
                //Log Error
                Logger log = LogManager.GetCurrentClassLogger();
                log.Error(ioe.Message.ToString());
                throw ioe;
            }
            catch (Exception ex)
            {
                //Log Error
                string sqlquery = "ORACLE: " + sqlScript;
                Logging log = new Logging();
                log.Error(ex, sqlquery);
                throw ex;
            }

            CloseORAConnection();
            return objTable;
        }

        public override void ExecNonQuery(string sqlScript)
        {
            try
            {
                OracleCommand objcommand = new OracleCommand();
                CreateORAConnection();
                objcommand.Connection = OraConn;
                objcommand.CommandText = sqlScript;
                objcommand.CommandType = CommandType.Text;
                OracleDataAdapter objAdapter = new OracleDataAdapter();
                objAdapter.UpdateCommand = new OracleCommand(sqlScript, OraConn);
            }
            catch (Exception ex)
            {
                //Log Error
                string sqlquery = "ORACLE: " + sqlScript;
                Logging log = new Logging();
                log.Error(ex, sqlquery);
                throw ex;
            }
            CloseORAConnection();
        }

        private void CreateORAConnection()
        {
            OraConn = new OracleConnection();
            try
            {
                if (OraConn.State != ConnectionState.Open)
                    OraConn.Open();
                else
                    OraConn.Close();
            }
            catch (Exception ex)
            {
                //Log Error
                string sqlquery = "Create ORA Connection";
                Logging log = new Logging();
                log.Error(ex, sqlquery);
                throw ex;
            }
        }

        private void CloseORAConnection()
        {
            try
            {
                if (OraConn.State == ConnectionState.Open)
                {
                    OraConn.Close();
                    OraConn = null;
                }
                else
                {
                    OraConn = null;
                }
            }
            catch (Exception ex)
            {
                //Log Error
                string sqlquery = "Close ORA Connection";
                Logging log = new Logging();
                log.Error(ex, sqlquery);
                throw ex;
            }
        }
    }
}