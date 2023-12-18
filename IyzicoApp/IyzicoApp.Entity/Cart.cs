namespace IyzicoApp.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public List<CartItem> Items { get; set; }
    }
}
