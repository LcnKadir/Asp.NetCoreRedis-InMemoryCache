﻿using Redis.API.Models;
using RedisExampleApp.Cache;

namespace Redis.API.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private readonly IProductRepository _Productrepository;
        private readonly RedisService _redisService;

        public ProductRepositoryWithCache(IProductRepository productrepository, RedisService redisService)
        {
            _Productrepository = productrepository;
            _redisService = redisService;
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}