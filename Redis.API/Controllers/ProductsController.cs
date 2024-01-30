using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Models;
using Redis.API.Repository;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAsync());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {

            return Created(string.Empty, await _repository.CreateAsync(product));
        }

    }
}
