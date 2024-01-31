using Redis.API.Models;
using RedisExampleApp.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis.API.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private const string productKey = "productCaches";
        private readonly IProductRepository _Productrepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _cacherepository;

        public ProductRepositoryWithCache(IProductRepository productrepository, RedisService redisService)
        {
            _Productrepository = productrepository;
            _redisService = redisService;
            _cacherepository = _redisService.Getdb(4);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _Productrepository.CreateAsync(product);

            if(await _cacherepository.KeyExistsAsync(productKey)) 
            {
                await _cacherepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(newProduct));
            }

            return newProduct;
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await _cacherepository.KeyExistsAsync(productKey))
                return await LoadToCacheFromDbAsync();

            var products = new List<Product>();

            var cacheProducts = await _cacherepository.HashGetAllAsync(productKey);
            foreach (var item in cacheProducts.ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);
            }
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if(_cacherepository.KeyExists(productKey))
            {
                var product = await _cacherepository.HashGetAsync(productKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }

            var products = await LoadToCacheFromDbAsync();
            return products.FirstOrDefault(x => x.Id == id);
        }


        public async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var product = await _Productrepository.GetAsync();

            product.ForEach(p =>
            {
                _cacherepository.HashSetAsync(productKey, p.Id, JsonSerializer.Serialize(p));
            });

            return product;
        }


    }
}
