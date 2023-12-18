using IyzicoApp.Entity;

namespace IyzicoApp.Business.Abstract
{
    public interface IProductService
    {
        List<Product> List();
        Product Read(Guid id);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
