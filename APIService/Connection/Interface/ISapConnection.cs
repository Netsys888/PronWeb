using System.Data.Odbc;
using SAPbobsCOM;
public interface ISapConnection
    {
    Company GetCompany();
    OdbcConnection GetHanaConnection();
}