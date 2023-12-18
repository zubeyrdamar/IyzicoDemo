using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class ProductRepository : GenericRepository<Product, SqlDbContext>, IProductRepository
    {

    }
}
