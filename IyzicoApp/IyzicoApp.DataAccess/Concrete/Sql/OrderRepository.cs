using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class OrderRepository : GenericRepository<Order, SqlDbContext>, IOrderRepository
    {

    }
}
