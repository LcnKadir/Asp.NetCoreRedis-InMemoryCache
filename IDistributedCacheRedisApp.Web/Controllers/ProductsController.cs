using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace IDistributedCacheRedisApp.Web.Controllers
{

    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            Products products = new Products { Id = 1, Name = "Kalem", Price = 100 };
            string jsonproduct = JsonConvert.SerializeObject(products);


            Byte[] bytes = Encoding.UTF8.GetBytes(jsonproduct);
            _distributedCache.Set("product:1", bytes);


            //await _distributedCache.SetStringAsync("product:1", jsonproduct, cacheEntryOptions);



            return View();
        }

        public async Task<IActionResult> Show()

        {

            Byte[] bytes = _distributedCache.Get("product:1");

            string jsonproduct = Encoding.UTF8.GetString(bytes);




            Products product = JsonConvert.DeserializeObject<Products>(jsonproduct);


            ViewBag.product = product;
            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache.Remove("name");

            return View();
        }
    }
}
