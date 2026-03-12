using APIService.Connection;
using SAPbobsCOM;
using System.Collections.Concurrent;
public class SapConnectionPool
{
    private readonly ConcurrentBag<Company> _connections = new();
    private readonly int _maxConnections = 5;

    public SapConnectionPool()
    {
        for (int i = 0; i < _maxConnections; i++)
        {
            _connections.Add(CreateConnection());
        }
    }

    private Company CreateConnection()
    {
        return new Login().Company;
    }

    public Company GetConnection()
    {
        if (_connections.TryTake(out Company company))
        {
            if (!company.Connected)
            {
                company = CreateConnection();
            }

            return company;
        }

        return CreateConnection();
    }

    public void ReturnConnection(Company company)
    {
        if (company != null && company.Connected)
        {
            _connections.Add(company);
        }
    }
}