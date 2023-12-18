using IyzicoApp.Entity;

namespace IyzicoApp.Business.Abstract
{
    public interface ICartItemService
    {
        List<CartItem> List();
        CartItem Read(Guid id);
        void Create(CartItem cartItem);
        void Update(CartItem cartItem);
        void Delete(CartItem cartItem);
    }
}
