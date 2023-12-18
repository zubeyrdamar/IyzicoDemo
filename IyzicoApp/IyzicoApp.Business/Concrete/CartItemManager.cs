using IyzicoApp.Business.Abstract;
using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private readonly ICartItemRepository repository;
        public CartItemManager(ICartItemRepository repository)
        {
            this.repository = repository;
        }

        public List<CartItem> List()
        {
            return repository.List().ToList();
        }

        public void Create(CartItem cartItem)
        {
            repository.Create(cartItem);
        }

        public CartItem Read(Guid id)
        {
            return repository.Read(id);
        }

        public void Update(CartItem cartItem)
        {
            repository.Update(cartItem);
        }

        public void Delete(CartItem cartItem)
        {
            repository.Delete(cartItem);
        }
    }
}
