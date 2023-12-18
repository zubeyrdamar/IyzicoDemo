using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IyzicoApp.Entity;
using IyzicoApp.Api.Models;
using IyzicoApp.Business.Abstract;

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
    }
}
