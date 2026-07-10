using ProductService.DTOs;
using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> CreateAsync(CreateProductRequest request);

        Task<IEnumerable<ProductResponse>> GetAllAsync();

        Task<ProductResponse> GetByIdAsync(int id);
    }
}
