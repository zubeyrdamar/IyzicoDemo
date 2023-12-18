namespace IyzicoApp.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public Status Status { get; set; }
        public PaymnetTypes PaymnetType { get; set; }

        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; }

        public List<OrderItem> Items { get; set; }
    }

    public enum Status
    {
        waiting = 0,
        unpaid = 1,
        completed = 2,
        failed = 3,
    }

    public enum PaymnetTypes
    {
        card = 0,
        transfer = 1,
        cash = 2,
    }
}
