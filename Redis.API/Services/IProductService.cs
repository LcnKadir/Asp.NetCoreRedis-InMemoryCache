using Redis.API.Models;

namespace Redis.API.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
    }
}
