using System;
using System.Data;
using System.Data.Odbc;
using SAPbobsCOM;

namespace APIService.Connection
{
    public class DefaultLogin
    {
        public DefaultLogin()
        {
            Login();
        }

        public OdbcCommand CMD { get; set; }

        public OdbcConnection CN { get; set; }

        public OdbcDataAdapter AD { get; set; }

        public int lErrCode { get; private set; }

        public string sErrMsg { get; private set; }

        public Company Company { get; internal set; }

        private void Login()
        {
            try
            {
                string connectionstr;
                connectionstr = "Driver={HDBODBC};UID=" + ConnectionString.DbUserName + ";PWD=" +
                                ConnectionString.DbPassword + ";SERVERNODE=" + ConnectionString.Server + ";[DATABASE=" +
                                ConnectionString.CompanyDB + "];";
                CN = new OdbcConnection(connectionstr);
                if (CN.State == ConnectionState.Closed) CN.Open();
                if (CN.State == ConnectionState.Open) lErrCode = 0;
                else lErrCode = 9999;
            }
            catch (Exception ex)
            {
                lErrCode = ex.GetHashCode();
                sErrMsg = ex.Message;
            }
        }
    }
}