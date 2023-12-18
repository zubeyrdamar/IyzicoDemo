using IyzicoApp.Business.Abstract;
using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository repository;
        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }

        public List<Cart> List()
        {
            return repository.List().ToList();
        }

        public void Create(Cart cart)
        {
            repository.Create(cart);
        }

        public Cart Read(Guid id)
        {
            return repository.Read(id);
        }

        public void Update(Cart cart)
        {
            repository.Update(cart);
        }

        public void Delete(Cart cart)
        {
            repository.Delete(cart);
        }
    }
}
