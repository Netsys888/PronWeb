using APIService.Connection;
using APIService.Models;
using SAPbobsCOM;
using System.Data.Odbc;

public class SapConnection : ISapConnection
{
    private Company _company;
    private OdbcConnection _hanaConnection;
    public Company GetCompany()
    {
        if (_company == null || !_company.Connected)
        {
            _company = new Login().Company;
        }

        return _company;
    }

    public OdbcConnection GetHanaConnection()
    {
        if (_hanaConnection == null || _hanaConnection.State != System.Data.ConnectionState.Open)
        {
            _hanaConnection = new LoginOnlyDatabase(LoginOnlyDatabase.Type.SapHana).CN;
        }

        return _hanaConnection;
    }
}