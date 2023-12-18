using AutoMapper;
using IyzicoApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using IyzicoApp.Business.Abstract;
using IyzicoApp.Entity;

namespace IyzicoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICartItemService service;
        private readonly ICartService cartService;
        private readonly IProductService productService;
        public CartItemsController(IMapper mapper, ICartItemService service, IProductService productService, ICartService cartService)
        {
            this.mapper = mapper;
            this.service = service;
            this.cartService = cartService;
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var cartItems = service.List();
            var cartItemsDTO = mapper.Map<List<CartItemDTO>>(cartItems);

            return Ok(cartItemsDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CartItemDTO model)
        {
            if (model == null) { return BadRequest(); }

            var product = productService.Read(model.ProductId);
            var cart = cartService.Read(model.CartId);

            if(product == null || cart == null)
            {
                return BadRequest("Product or Cart does not exist.");
            }

            var cartItem = mapper.Map<CartItem>(model);
            service.Create(cartItem);

            return Ok("Cart item has been created successfully.");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Read([FromRoute] Guid id)
        {
            var cartItem = service.Read(id);
            if (cartItem == null) { return NotFound(); }
            var cartItemDTO = mapper.Map<CartItemDTO>(cartItem);

            return Ok(cartItemDTO);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CartItem model)
        {
            var cartItem = service.Read(model.Id);
            if (cartItem == null) { return BadRequest(); }

            cartItem.ProductId = model.ProductId;
            cartItem.Quantity = model.Quantity;
            service.Update(cartItem);

            return Ok("Cart item has been updated successfully.");
        }

        [HttpDelete]
        [Route("details/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var cartItem = service.Read(id);
            if (cartItem == null) { return BadRequest(); }

            service.Delete(cartItem);

            return Ok("Cart item has been deleted successfully.");
        }
    }
}
