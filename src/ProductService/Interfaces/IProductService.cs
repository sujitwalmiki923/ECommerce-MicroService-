using ProductService.DTOs;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> CreateAsync(CreateProductRequest request);
    }
}
