using IyzicoApp.Entity;

namespace IyzicoApp.Business.Abstract
{
    public interface ICartService
    {
        List<Cart> List();
        Cart Read(Guid id);
        void Create(Cart cart);
        void Update(Cart cart);
        void Delete(Cart cart);

        Cart GetCart(string Username);
    }
}
