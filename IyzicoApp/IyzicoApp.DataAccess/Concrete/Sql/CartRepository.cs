using IyzicoApp.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class CartRepository : GenericRepository<Cart, SqlDbContext>, ICartRepository
    {
        public Cart GetCart(string Username)
        {
            using(var context = new SqlDbContext())
            {
                return context.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefault(i => i.Username == Username);
            }
        }
    }
}
