using AutoMapper;
using IyzicoApp.Api.Models;
using IyzicoApp.Business.Abstract;
using IyzicoApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IProductService service;
        public ProductsController(IMapper mapper, IProductService service)
        {
            this.mapper = mapper;
            this.service = service;
        }


        [HttpGet]
        public IActionResult List()
        {
            var products = service.List();
            var productsDTO = mapper.Map<List<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO model)
        {
            if (model == null) { return BadRequest(); }
            var product = mapper.Map<Product>(model);
            service.Create(product);

            return Ok("Product has been created successfully.");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Read([FromRoute] Guid id)
        {
            var product = service.Read(id);
            if (product == null) { return NotFound(); }
            var productDTO = mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product model)
        {
            var product = service.Read(model.Id);
            if (product == null) { return BadRequest(); }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            service.Update(product);

            return Ok("Product has been updated successfully.");
        }

        [HttpDelete]
        [Route("details/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var product = service.Read(id);
            if (product == null) { return BadRequest(); }

            service.Delete(product);

            return Ok("Product has been deleted successfully.");
        }
    }
}
