using IyzicoApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace IyzicoApp.DataAccess.Concrete.Sql
{
    public class SqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=db_iyzico_demo;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
