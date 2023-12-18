using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IyzicoApp.Entity;
using IyzicoApp.Api.Models;
using IyzicoApp.Business.Abstract;
using Iyzipay.Model;
using Iyzipay.Request;
using Iyzipay;

namespace IyzicoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICartService service;
        public CartsController(IMapper mapper, ICartService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public IActionResult List()
        {
            var carts = service.List();
            var cartsDTO = mapper.Map<List<CartDTO>>(carts);

            return Ok(cartsDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CartDTO model)
        {
            if (model == null) { return BadRequest(); }
            var cart = mapper.Map<Cart>(model);
            service.Create(cart);

            return Ok("Cart has been created successfully.");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Read([FromRoute] Guid id)
        {
            var cart = service.Read(id);
            if (cart == null) { return NotFound(); }
            var cartDTO = mapper.Map<CartDTO>(cart);

            return Ok(cartDTO);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Cart model)
        {
            var cart = service.Read(model.Id);
            if (cart == null) { return BadRequest(); }

            cart.Username = model.Username;
            service.Update(cart);

            return Ok("Cart has been updated successfully.");
        }

        [HttpDelete]
        [Route("details/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var cart = service.Read(id);
            if (cart == null) { return BadRequest(); }

            service.Delete(cart);

            return Ok("Cart has been deleted successfully.");
        }

        [HttpGet]
        [Route("get/{Username}")]
        public IActionResult GetCart([FromRoute] string Username)
        {
            var cart = service.GetCart(Username);
            if(cart == null) { return NotFound("Cart does not exist."); }

            return Ok(cart);
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult Checkout()
        {
            Options options = new Options();
            options.ApiKey = "sandbox-A3i0RByJIOmInhGbns099G7BNJXHOF1q";
            options.SecretKey = "sandbox-esf7RHdyc9o2hBtrSXwJoJvecdx2UdrC";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1.0";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);

            if(payment.Status == "success")
            {
                return Ok(payment);
            }

            return BadRequest("Something went wrong.");
        }

    }
}
