namespace IyzicoApp.Api.Models
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
    }
}
