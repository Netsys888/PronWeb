using SAPbobsCOM;

public class SapOrderService
{
    private readonly SapConnectionPool _pool;

    public SapOrderService(SapConnectionPool pool)
    {
        _pool = pool;
    }

    public void CreateOrder()
    {
        var company = _pool.GetConnection();

        try
        {
            var order = (Documents)company.GetBusinessObject(BoObjectTypes.oOrders);

            order.CardCode = "C0001";

            order.Add();
        }
        finally
        {
            _pool.ReturnConnection(company);
        }
    }
}