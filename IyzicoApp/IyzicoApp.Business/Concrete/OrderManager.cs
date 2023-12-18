using IyzicoApp.Business.Abstract;
using IyzicoApp.DataAccess.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository repository;
        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public List<Order> List()
        {
            return repository.List().ToList();
        }

        public void Create(Order order)
        {
            repository.Create(order);
        }

        public Order Read(Guid id)
        {
            return repository.Read(id);
        }

        public void Update(Order order)
        {
            repository.Update(order);
        }

        public void Delete(Order order)
        {
            repository.Delete(order);
        }
    }
}
