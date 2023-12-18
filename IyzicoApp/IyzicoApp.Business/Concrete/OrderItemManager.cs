using IyzicoApp.Business.Abstract;
using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Business.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemRepository repository;
        public OrderItemManager(IOrderItemRepository repository)
        {
            this.repository = repository;
        }

        public List<OrderItem> List()
        {
            return repository.List().ToList();
        }

        public void Create(OrderItem orderItem)
        {
            repository.Create(orderItem);
        }

        public OrderItem Read(Guid id)
        {
            return repository.Read(id);
        }

        public void Update(OrderItem orderItem)
        {
            repository.Update(orderItem);
        }

        public void Delete(OrderItem orderItem)
        {
            repository.Delete(orderItem);
        }
    }
}
