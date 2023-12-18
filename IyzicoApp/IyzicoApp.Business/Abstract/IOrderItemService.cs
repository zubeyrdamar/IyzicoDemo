using IyzicoApp.Entity;

namespace IyzicoApp.Business.Abstract
{
    public interface IOrderItemService
    {
        List<OrderItem> List();
        OrderItem Read(Guid id);
        void Create(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(OrderItem orderItem);
    }
}
