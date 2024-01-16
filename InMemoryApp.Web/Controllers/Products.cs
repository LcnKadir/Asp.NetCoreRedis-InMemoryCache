using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class Products : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public Products(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //Key değerinin Memory'de olup olmadığını tespit etmek için 1.Yol //1 To determine whether the Key value is in Memory or not.The Road//

            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}

            //Key değerinin Memory'de olup olmadığını tespit etmek için 2.Yol //2 To determine whether the Key value is in Memory or not.The Road//
            //if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}


            //Absolute ve Sliding Expiration' un birlikte kullanımı.////Combined use of Absolute and Sliding Expiration.//
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

            //options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.High;

            options.RegisterPostEvictionCallback((key, value, reason, state) => //Cache hangi sebepten ötürü ve ne zaman silindi. //For what reason and when was the cache deleted.
            {
                _memoryCache.Set("callback", $"{key}-> {value} => sebep: {reason}");
            });

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);


            Product p = new Product { Id = 1, Name= "Kalem", Price = 200}; //Model de ki değerlerin cach'lenmesi. //Say the model is the caching of values.//
            _memoryCache.Set<Product>("product:1", p);


            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue<string>("zaman", out string zamancache);
            _memoryCache.TryGetValue <string>("callback", out string callback);
            ViewBag.zaman = zamancache;
            ViewBag.callback = callback;

            ViewBag.product = _memoryCache.Get<Product>("product:1"); 

            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
