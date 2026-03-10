using System.Data.Odbc;
using APIService.Models;
using SAPbobsCOM;

namespace APIService.Connection
{
    public class SapDriverOCompany
    {
        public static void Init_oCompany()
        {
            ConnectionString.oCompany = new Login().Company;
            ConnectionString.OdbcHanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
        }
        public static Company _CheckingStatusOCompany()
        {
            if(ConnectionString.oCompany!=null)
                return (ConnectionString.oCompany.Connected) ? ConnectionString.oCompany : ConnectionString.oCompany = new Login().Company;
            else
                return ConnectionString.oCompany = new Login().Company;
        }
        public static OdbcConnection _CheckingStatusConnectionHana(){
            if (ConnectionString.OdbcHanaConnection != null)
            {
                if (!(ConnectionString.OdbcHanaConnection.State == System.Data.ConnectionState.Open))
                    ConnectionString.OdbcHanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
            }
            else
            {
                ConnectionString.OdbcHanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
            }
            return ConnectionString.OdbcHanaConnection;
        }
    }
}