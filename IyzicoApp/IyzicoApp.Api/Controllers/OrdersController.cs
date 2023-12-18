using AutoMapper;
using IyzicoApp.Api.Models;
using IyzicoApp.Business.Abstract;
using IyzicoApp.Entity;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderService service;
        private readonly IOrderItemService orderItemService;
        private readonly ICartService cartService;
        public OrdersController(IMapper mapper, IOrderService service, IOrderItemService orderItemService, ICartService cartService)
        {
            this.mapper = mapper;
            this.service = service;
            this.orderItemService = orderItemService;
            this.cartService = cartService;
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult Checkout([FromBody] OrderDTO model)
        {
            var cart = cartService.GetCart(model.Username);
            decimal total = CalculateTotal(cart);
            model.Cart = cart;

            var payment = pay(model, total);
            if (payment.Status == "success")
            {
                SaveOrder(model, payment);
                return Ok("Success");
            }

            return BadRequest("Something went wrong.");
        }

        private decimal CalculateTotal(Cart cart)
        {
            decimal total = 0;
            foreach (var item in cart.Items)
            {
                total += item.Product.Price;
            }

            return total;
        }

        private void SaveOrder(OrderDTO model, Payment payment)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderDate = new DateTime();
            order.Username = model.Username;

            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Address = model.Address;
            order.City = model.City;
            order.Phone = model.Phone;
            order.Email = model.Email;
            order.Note = model.Note;

            order.Status = Entity.Status.completed;
            order.PaymentType = Entity.PaymentTypes.card;

            order.ConversationId = payment.ConversationId;

            service.Create(order);

            foreach (var item in model.Cart.Items)
            {
                var orderItem = new Entity.OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                };

                orderItemService.Create(orderItem);
            }
        }

        private Payment pay(OrderDTO model, decimal total)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-A3i0RByJIOmInhGbns099G7BNJXHOF1q";
            options.SecretKey = "sandbox-esf7RHdyc9o2hBtrSXwJoJvecdx2UdrC";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = total.ToString().Split(',')[0];
            request.PaidPrice = total.ToString().Split(',')[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = model.Cart.Id.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = model.Username;
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
            buyer.GsmNumber = model.Phone;
            buyer.Email = model.Email;
            buyer.IdentityNumber = model.Username;
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = model.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = model.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34494";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = model.FirstName + " " + model.LastName;
            shippingAddress.City = model.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.Address;
            shippingAddress.ZipCode = "34494";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = model.FirstName + " " + model.LastName;
            billingAddress.City = model.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model.Address;
            billingAddress.ZipCode = "34494";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem = new BasketItem();

            foreach (var item in model.Cart.Items)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.Product.Id.ToString();
                basketItem.Name = item.Product.Name;
                basketItem.Category1 = "Phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = item.Product.Price.ToString().Split(',')[0];
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
    }
}
