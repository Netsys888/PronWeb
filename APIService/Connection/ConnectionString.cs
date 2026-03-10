using System.Data.Odbc;
using SAPbobsCOM;

namespace APIService.Connection
{
    public class ConnectionString
    {
        public static string DbServerType { get; set; } = null!;
        public static string Server { get; set; } = null!;
        public static string ServerGET { get; set; } = null!;
        public static string ServerGETName { get; set; } = null!;
        public static string LicenseServer { get; set; } = null!;
        public static string SLDServer { get; set; } = null!;
        public static string DbUserName { get; set; } = null!;
        public static string DbPassword { get; set; } = null!;
        public static string CompanyDB { get; set; } = null!;
        public static string PronWebDB { get; set; } = null!;
        public static string UserName { get; set; } = null!;
        public static string Password { get; set; } = null!;
        public static string ConnHana { get; set; } = null;
        public static string ConnectionStringHANA1 { get; set; } = null;
        public static string ConnectionStringHANA2 { get; set; } = null;
        public static string ConnectionStringSAP { get; set; } = null;        
        public static string PronWebDb { get; set; } = null;
        public static Company oCompany { get; set; }
        public static OdbcConnection OdbcHanaConnection { get; set; }
    }
}