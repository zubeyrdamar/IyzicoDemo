using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class CartRepository : GenericRepository<Cart, SqlDbContext>, ICartRepository
    {

    }
}
