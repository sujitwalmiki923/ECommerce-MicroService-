using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;       
        }

        //ADD Product
        [HttpPost("add")]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            //var product = new Product
            //{
            //    Name = request.Name,
            //    Description = request.Description,
            //    Price = request.Price,
            //    Stock = request.Stock,
            //    CreatedAt = DateTime.UtcNow
            //};

            var result = await _service.CreateAsync(request);

            return Ok(result);
            //return new ProductResponse
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    Stock = product.Stock
            //};
           // var product = await _service.CreateAsync(request);

        }
    }
}
