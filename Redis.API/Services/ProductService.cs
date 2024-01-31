using Redis.API.Models;
using Redis.API.Repository;

namespace Redis.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _repository.CreateAsync(product);
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
