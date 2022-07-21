using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Configuration;

namespace PSI_WEBFORM
{
    public class DBConnection
    {
        public static string strConnectionStringANDON = ConfigurationManager.ConnectionStrings["ConnectionStringANDON"].ToString();
        public static string strConnectionStringLV = ConfigurationManager.ConnectionStrings["ConnectionStringLV"].ToString();

        public static OracleConnection GetConnectionObject(string srtConnectionString)
        {
            OracleConnection theConnection = new OracleConnection(srtConnectionString);

            try
            {
                theConnection.Open();
                return theConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static OracleCommand GetCommandObject(string _strCommandText)
        {
            OracleCommand theCommand = new OracleCommand();
            theCommand.Connection = GetConnectionObject(strConnectionStringANDON);
            theCommand.CommandText = _strCommandText;

            return theCommand;
        }

        #region "Using for Andon".

        public static DataTable GetDataTableByCommandText(string _strCommandText)
        {
            DataTable theData = new DataTable();
            OracleCommand theCommand = new OracleCommand();

            try
            {
                theCommand = GetCommandObject(_strCommandText);
                OracleDataAdapter theDataAdapter = new OracleDataAdapter(theCommand);
                DataSet ds = new DataSet();
                theDataAdapter.Fill(ds);

                return (ds.Tables[0]);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                theCommand.Connection.Close();
                theCommand.Connection.Dispose();
            }

            return null;

        }

        #endregion

        #region "Using for GMES"
        public static OracleCommand GetCommandObjectGMES(string _strCommandText)
        {
            OracleCommand theCommand = new OracleCommand();
            theCommand.Connection = GetConnectionObject(strConnectionStringLV);
            theCommand.CommandText = _strCommandText;

            return theCommand;
        }
        public static Int32 ExcuteCommandText(string _strCommandText)
        {
            DataTable theData = new DataTable();
            OracleCommand theCommand = new OracleCommand();
            Int32 amoutOfAffectedRow = -1;

            try
            {
                theCommand = GetCommandObject(_strCommandText);
                amoutOfAffectedRow = theCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                theCommand.Connection.Close();
                theCommand.Connection.Dispose();
            }

            return amoutOfAffectedRow;
        }


        public static DataTable GetDataTableByCommandTextGMES(string _strCommandText)
        {
            DataTable theData = new DataTable();
            OracleCommand theCommand = new OracleCommand();

            try
            {
                theCommand = GetCommandObjectGMES(_strCommandText);
                OracleDataAdapter theDataAdapter = new OracleDataAdapter(theCommand);
                DataSet ds = new DataSet();
                theDataAdapter.Fill(ds);

                return (ds.Tables[0]);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                theCommand.Connection.Close();
                theCommand.Connection.Dispose();
            }

            return null;

        }

        #endregion
    }

}