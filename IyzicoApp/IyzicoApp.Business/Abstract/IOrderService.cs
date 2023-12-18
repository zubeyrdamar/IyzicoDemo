using IyzicoApp.Entity;

namespace IyzicoApp.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> List();
        Order Read(Guid id);
        void Create(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
