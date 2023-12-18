using IyzicoApp.Business.Abstract;
using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository repository;
        public ProductManager(IProductRepository repository)
        {
            this.repository = repository;
        }

        public List<Product> List()
        {
            return repository.List().ToList();
        }

        public void Create(Product product)
        {
            repository.Create(product);
        }

        public Product Read(Guid id)
        {
            return repository.Read(id);
        }

        public void Update(Product product)
        {
            repository.Update(product);
        }

        public void Delete(Product product)
        {
            repository.Delete(product);
        }
    }
}
