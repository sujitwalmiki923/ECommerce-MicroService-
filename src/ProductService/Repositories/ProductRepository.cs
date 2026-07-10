using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
           _context = context;       
        }
        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
           // throw new NotImplementedException();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
           // throw new NotImplementedException();
        }

        public async Task<Product?> GetByIdAsync(int Id)
        {
            return await
                _context.Products
                .FirstOrDefaultAsync(x => x.Id == Id);
            //throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
           // throw new NotImplementedException();
        }
    }
}
