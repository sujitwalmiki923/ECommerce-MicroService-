using ProductService.DTOs;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
           _repository = repository;
        }
        public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
        {
            //throw new NotImplementedException();

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(product);

            return new ProductResponse 
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock= product.Stock,
            };
        }

    }
}
