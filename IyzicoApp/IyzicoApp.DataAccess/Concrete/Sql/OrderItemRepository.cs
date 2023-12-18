using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class OrderItemRepository : GenericRepository<OrderItem, SqlDbContext>, IOrderItemRepository
    {

    }
}
