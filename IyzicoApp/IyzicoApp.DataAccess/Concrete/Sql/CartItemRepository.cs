using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class CartItemRepository : GenericRepository<CartItem, SqlDbContext>, ICartItemRepository
    {

    }
}
