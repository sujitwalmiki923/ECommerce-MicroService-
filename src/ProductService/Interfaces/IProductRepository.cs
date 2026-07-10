using ProductService.Models;

namespace ProductService.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int Id);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}
